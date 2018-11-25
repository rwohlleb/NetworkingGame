using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SocketHelper
{
    public class SimpleSocket
    {
        private bool isConnected = false;
        private string protocolType = "TCP";
        public event EventHandler<string> OnDataReceived;

        public SimpleSocket(string protocolType)
        {
            this.protocolType = protocolType;
        }

        public bool IsConnected()
        {
            return isConnected;
        }

        public void Disconnect()
        {
            isConnected = false;
        }

        public void Listen(string address, int port)
        {
            isConnected = true;
            Socket socket = this.protocolType.Equals("TCP") ? new Socket(SocketType.Stream, ProtocolType.Tcp) : new Socket(SocketType.Dgram, ProtocolType.Udp);
            IPHostEntry entry = Dns.GetHostEntry(address);
            IPEndPoint localEP = new IPEndPoint(entry.AddressList[0], port);
            socket.NoDelay = true;
            socket.Bind(localEP);
            socket.Listen(10);
            while (isConnected)
            {
                Socket handler = socket.Accept();
                string message = "";

                byte[] buffer = new byte[1024];
                int bytesRec = handler.Receive(buffer);
                message += Encoding.ASCII.GetString(buffer, 0, bytesRec);
                OnDataReceived?.Invoke(this, message);
            }
            socket.Close();

        }

        public int Send(string address, int port, string message)
        {
            try
            {
                Socket socket = this.protocolType.Equals("TCP") ? new Socket(SocketType.Stream, ProtocolType.Tcp) : new Socket(SocketType.Dgram, ProtocolType.Udp);
                socket.NoDelay = true;
                byte[] data = Encoding.ASCII.GetBytes(message);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostEntry(address).AddressList[0], port);
                socket.Connect(endPoint);
                socket.SendTo(data, endPoint);
                socket.Close();

            }
            catch (Exception ex)
            {
                return 0;
            }

            return 1;
        }


    }
}
