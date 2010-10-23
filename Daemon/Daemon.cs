using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Daemon
{
    class Daemon
    {
        private const int DeamonPort = 8001;
        private const int IperfPort = 5001;
        private const int BufferSize = 4096;

        private readonly TcpListener _tcpListener;
        private readonly TcpClient _tcpClient;
        private readonly TcpClient _tcpServerClient;
        private readonly Thread _clientSideThread;
        private readonly Thread _serverSideThread;
        private readonly NetworkStream _serverStream;
        private readonly NetworkStream _clientStream;

        public Daemon()
        {
            IPAddress ipAd = IPAddress.Parse("127.0.0.1");
            this._tcpListener = new TcpListener(ipAd, DeamonPort);
            this._clientSideThread = new Thread(new ThreadStart(ClientHandler));
            this._serverSideThread = new Thread(new ThreadStart(ServerHandler));
            this._tcpListener.Start();
            Console.Write("Waiting for Iperf client connection... ");

            _tcpClient = this._tcpListener.AcceptTcpClient();
            Console.WriteLine("Esteblished");
            Console.Write("Closing daemon server socket... ");
            this._tcpListener.Stop();
            Console.WriteLine("Done");

            _tcpServerClient = new TcpClient();
            Console.Write("Connecting to Iperf server...");
            _tcpServerClient.Connect("127.0.0.1", IperfPort);
            Console.WriteLine("Done");
            _serverStream = _tcpServerClient.GetStream();
            _clientStream = _tcpClient.GetStream();

            this._clientSideThread.Start();
            this._serverSideThread.Start();
        }

        private void ClientHandler()
        {
            var message = new byte[BufferSize];

            while (true)
            {
                int bytesRead = 0;
                try
                {
                    //blocks until a client sends a message
                    bytesRead = _clientStream.Read(message, 0, 4096);
                    _serverStream.Write(message, 0, bytesRead);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Error: " + e.Message);
                    break;
                }

                if (bytesRead == 0)
                {
                    Console.WriteLine("Client connection closed");
                    if (_tcpClient.Client.Connected)
                    {
                        _tcpClient.Client.Close();
                        _tcpClient.Close();
                    }
                    if (_tcpServerClient.Client.Connected)
                    {
                        _tcpServerClient.Client.Close();
                        _tcpServerClient.Close();
                    }
                    break;
                }
            }
        }

        private void ServerHandler()
        {
            var message = new byte[BufferSize];

            while (true)
            {
                int bytesRead = 0;
                try
                {
                    //blocks until a client sends a message
                    bytesRead = _serverStream.Read(message, 0, 4096);
                    _clientStream.Write(message, 0, bytesRead);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Error: " + e.Message);
                    break;
                }

                if (bytesRead == 0)
                {
                    Console.WriteLine("Server connection closed");
                    _tcpServerClient.Client.Close();
                    _tcpServerClient.Close();
                    break;
                }
            }
        }

        public static void Main()
        {
            Console.WriteLine("Daemon activated");
            var daemon = new Daemon();
            Console.ReadKey();
        }
    }
}
