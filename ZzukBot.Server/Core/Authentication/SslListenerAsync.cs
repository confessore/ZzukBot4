using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using ZzukBot.Server.Core.Authentication.Objects;
using ZzukBot.Server.Core.Utilities.Extensions;
using ZzukBot.Server.Settings;
using ZzukBot.Server.Core.Authentication.Interfaces;
using ZzukBot.Server.Services.Interfaces;

namespace ZzukBot.Server.Core.Authentication
{
    internal class SslListenerAsync : ISslListenerAsync
    {
        readonly IMySqlQuerier mySqlQuerier;

        string EOF { get; set; }
        int ServerPort { get; set; }
        int Timeout { get; set; }

        public SslListenerAsync(IMySqlQuerier mySqlQuerier)
        {
            this.mySqlQuerier = mySqlQuerier;

            EOF = Default.EndOfFile;
            ServerPort = Default.ServerPort;
            Timeout = Default.Timeout;
        }

        ManualResetEvent connectDone = new ManualResetEvent(false);

        public void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, ServerPort);

            Socket socket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.ReceiveTimeout = Timeout;
                socket.SendTimeout = Timeout;
                socket.Bind(localEndPoint);
                socket.Listen(100);

                while (true)
                {
                    connectDone.Reset();

                    Console.WriteLine("Waiting for a connection...");

                    socket.BeginAccept(
                        new AsyncCallback(AcceptCallback), socket);

                    connectDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        void AuthenticateCallback(IAsyncResult ar)
        {
            try
            {
                SslStream ssl = (SslStream)ar.AsyncState;
                ssl.EndAuthenticateAsServer(ar);

                Receive(ssl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                connectDone.Set();

                Socket socket = (Socket)ar.AsyncState;
                socket = socket.EndAccept(ar);

                SslStream ssl = new SslStream(new NetworkStream(socket), false);
                ssl.BeginAuthenticateAsServer(new X509Certificate2(Default.CertificateName, Default.CertificatePassword),
                    false, false, new AsyncCallback(AuthenticateCallback), ssl);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }

        void Receive(SslStream ssl)
        {
            try
            {
                StateObject state = new StateObject();
                state.workStream = ssl;

                ssl.BeginRead(state.buffer, 0, StateObject.BufferSize,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        async void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                string content = string.Empty;
                StateObject state = (StateObject)ar.AsyncState;
                SslStream ssl = state.workStream;

                int bytesRead = ssl.EndRead(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    content = state.sb.ToString();

                    if (content.IndexOf(EOF) > -1)
                    {
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);

                        if (content.GetOpcode(EOF) == (uint)Opcodes.Profile)
                            Send(ssl, Opcodes.Profile.NewPacket(EOF, await mySqlQuerier.FindIdByUsernameAsync(content.GetContentAtIndex(EOF, 0))));
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Ping)
                            Send(ssl, Opcodes.Ping.NewPacket(EOF));
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Version)
                        {
                            if (Default.Version == content.GetContentAtIndex(EOF, 0))
                                Send(ssl, Opcodes.Version.NewPacket(EOF));
                            else
                                Send(ssl, Opcodes.Update.NewPacket(EOF));
                        }
                        if (content.GetOpcode(EOF) == (uint)Opcodes.User)
                        {
                            var email = content.GetContentAtIndex(EOF, 0);
                            var password = content.GetContentAtIndex(EOF, 1);
                            if (await mySqlQuerier.FindEmailConfirmedByEmailAsync(email))
                            {
                                var hash = await mySqlQuerier.FindPasswordHashByEmailAsync(email);
                                if (hash != null &&
                                    new PasswordHasher<IdentityUser>().VerifyHashedPassword(null, hash, password) == PasswordVerificationResult.Success)
                                {
                                    var valid = await mySqlQuerier.FindSubscriptionExpirationByEmailAsync(email) > DateTime.Now;

                                    Send(ssl, Opcodes.User.NewPacket(EOF, 
                                        valid ? Convert.ToString((uint)Opcodes.Full) : Convert.ToString((uint)Opcodes.Trial),
                                        await mySqlQuerier.FindIdByEmailAsync(email),
                                        await mySqlQuerier.FindUsernameByEmailAsync(email),
                                        Convert.ToString(await mySqlQuerier.FindSubscriptionExpirationByEmailAsync(email))));
                                }
                                else
                                    Send(ssl, Opcodes.Failure.NewPacket(EOF, "Invalid login"));
                            }
                            else
                                Send(ssl, Opcodes.Failure.NewPacket(EOF, "Email not confirmed"));
                        }
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Warden)
                            Send(ssl, Opcodes.Warden.NewPacket(EOF, SendOvers.EventSignal, SendOvers.EventSignal0,
                                SendOvers.WardenLoadDetour, SendOvers.WardenMemScan, SendOvers.WardenPageScan));
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Success)
                            Send(ssl, Opcodes.Success.NewPacket(EOF));
                    }
                    else
                        ssl.BeginRead(state.buffer, 0, StateObject.BufferSize,
                            new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void Send(SslStream ssl, string data)
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                ssl.BeginWrite(byteData, 0, byteData.Length,
                    new AsyncCallback(SendCallback), ssl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void SendCallback(IAsyncResult ar)
        {
            try
            {
                SslStream ssl = (SslStream)ar.AsyncState;
                ssl.EndWrite(ar);
                Receive(ssl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
