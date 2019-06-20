using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZzukBot.Core.Authentication.Objects;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Settings;

namespace ZzukBot.Core.Authentication
{
    internal class SslAuthClientAsync
    {
        bool Authenticated { get; set; }
        string BotUsername { get; set; }
        string BotPassword { get; set; }
        string EOF { get; set; }
        string Reason { get; set; }
        string ServerAddress { get; set; }
        int ServerPort { get; set; }
        int Timeout { get; set; }

        internal SslAuthClientAsync(string botUsername, string botPassword)
        {
            BotUsername = botUsername;
            BotPassword = botPassword;
            EOF = Default.EndOfFile;
            ServerAddress = Default.ServerAddress;
            ServerPort = Default.ServerPort;
            Timeout = Default.Timeout;
        }

        ManualResetEvent connectDone = new ManualResetEvent(false);

        internal bool StartClient(out string reason)
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(ServerAddress);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, ServerPort);

                Socket socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                socket.ReceiveTimeout = Timeout;
                socket.SendTimeout = Timeout;

                socket.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), socket);

                connectDone.WaitOne();

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                reason = Reason;

                return Authenticated;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                reason = e.Message;
                return false;
            }
        }

        void AuthenticateCallback(IAsyncResult ar)
        {
            try
            {
                SslStream ssl = (SslStream)ar.AsyncState;
                ssl.EndAuthenticateAsClient(ar);

                Send(ssl, Opcodes.Ping.NewPacket(EOF));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    socket.RemoteEndPoint.ToString());

                SslStream ssl = new SslStream(new NetworkStream(socket), false, new RemoteCertificateValidationCallback(ValidateCert));
                ssl.BeginAuthenticateAsClient(socket.RemoteEndPoint.ToString(),
                    new AsyncCallback(AuthenticateCallback), ssl);
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

                        if (content.GetOpcode(EOF) == (uint)Opcodes.Ping)
                            Send(ssl, Opcodes.Version.NewPacket(EOF, Assembly.GetExecutingAssembly().GetMd5AsBase64()));
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Version)
                            Send(ssl, Opcodes.User.NewPacket(EOF, BotUsername, BotPassword));
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Update)
                        {
                            Reason = "Update required";
                            Authenticated = false;
                            connectDone.Set();
                        }
                        if (content.GetOpcode(EOF) == (uint)Opcodes.User)
                        {
                            await Task.Run(() =>
                            {
                                UserProfile.SubscriptionType = Convert.ToUInt32(content.GetContentAtIndex(EOF, 0));
                                UserProfile.Id = content.GetContentAtIndex(EOF, 1);
                                UserProfile.UserName = content.GetContentAtIndex(EOF, 2);
                                UserProfile.SubscriptionExpiration = content.GetContentAtIndex(EOF, 3);
                            });
                            Send(ssl, Opcodes.Warden.NewPacket(EOF));
                        }
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Warden)
                        {
                            await Task.Run(() =>
                            {
                                SendOvers.EventSignal = content.GetContentAtIndex(EOF, 0).Split(new string[] { "[|]" }, StringSplitOptions.RemoveEmptyEntries);
                                SendOvers.EventSignal0 = content.GetContentAtIndex(EOF, 1).Split(new string[] { "[|]" }, StringSplitOptions.RemoveEmptyEntries);
                                SendOvers.WardenLoadDetour = content.GetContentAtIndex(EOF, 2).Split(new string[] { "[|]" }, StringSplitOptions.RemoveEmptyEntries);
                                SendOvers.WardenMemCpyDetour = content.GetContentAtIndex(EOF, 3).Split(new string[] { "[|]" }, StringSplitOptions.RemoveEmptyEntries);
                                SendOvers.WardenPageScanDetour = content.GetContentAtIndex(EOF, 4).Split(new string[] { "[|]" }, StringSplitOptions.RemoveEmptyEntries);
                            });
                            Send(ssl, Opcodes.Success.NewPacket(EOF));
                        }
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Success)
                        {
                            if (UserProfile.SubscriptionType == (uint)Opcodes.Full)
                                Reason = string.Empty;
                            else
                                Reason = "Subscription expired";
                            Authenticated = true;
                            connectDone.Set();
                        }
                        if (content.GetOpcode(EOF) == (uint)Opcodes.Failure)
                        {
                            Reason = content.GetContentAtIndex(EOF, 0);
                            Authenticated = false;
                            connectDone.Set();
                        }
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

        bool ValidateCert(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
