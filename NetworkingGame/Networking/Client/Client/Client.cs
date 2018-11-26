using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketHelper;

namespace ClientHelper
{
    public class Client
    {
        private static SimpleSocket socket;
        private string address;
        private int port;
        public event EventHandler<string> OnClientReceived;

        public Client()
        {

        }

        public Client(string address, int port)
        {
            this.address = address;
            this.port = port;
        }

        public void Start()
        {
            socket = new SimpleSocket("TCP");
            socket.OnDataReceived += OnDataReceived;
            Task task = new Task(() => socket.Listen(this.address, this.port));
            task.Start();

        }

        public int Send(string data)
        {
            return socket.Send("192.168.0.2", 1000, data);
        }

        private void OnDataReceived(object sender, string data)
        {
            OnClientReceived?.Invoke(this, data);
        }
    }
}
