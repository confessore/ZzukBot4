using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using ZzukBot.Core.Authentication.Objects;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Core.Utilities.Helpers;
using ZzukBot.Settings;

namespace ZzukBot.Core.Authentication
{
    internal class SslProfileClientAsync
    {
        bool Authenticated { get; set; }
        string Username { get; set; }
        string EOF { get; set; }
        string Profile { get; set; }
        string ServerAddress { get; set; }
        int ServerPort { get; set; }
        int Timeout { get; set; }

        internal SslProfileClientAsync(string username, string profile)
        {
            Username = username;
            Profile = profile;
            EOF = Default.EndOfFile;
            ServerAddress = Default.ServerAddress;
            ServerPort = Default.ServerPort;
            Timeout = Default.Timeout;
        }

        ManualResetEvent connectDone = new ManualResetEvent(false);

        internal bool StartClient(out string profile)
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

                profile = Profile;

                return Authenticated;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                profile = e.Message;
                return false;
            }
        }

        void AuthenticateCallback(IAsyncResult ar)
        {
            try
            {
                SslStream ssl = (SslStream)ar.AsyncState;
                ssl.EndAuthenticateAsClient(ar);

                Send(ssl, Opcodes.Profile.NewPacket(EOF, Username));
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

        void ReceiveCallback(IAsyncResult ar)
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
                        {
                            Profile = Cryptography.DecryptStringAES(Profile, content.GetContentAtIndex(EOF, 0));
                            Authenticated = true;
                            connectDone.Set();
                        }
                        else
                        {
                            Profile = "invalid";
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
