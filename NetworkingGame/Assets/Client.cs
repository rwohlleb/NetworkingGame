using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketHelper;
using System.IO;
using System.Windows;

namespace ClientHelper
{
    public class Client
    {
        private static SimpleSocket socket;
        private string address;
        private int port;
        public event EventHandler<string> OnClientReceived;
        public string ip;

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
            string dir = Directory.GetCurrentDirectory();
            StreamReader reader = new StreamReader(dir+ @"/Assets/IP.txt");
            ip = reader.ReadLine();
            reader.Close();

            socket = new SimpleSocket("TCP");
            socket.OnDataReceived += OnDataReceived;
            Task task = new Task(() => socket.Listen(this.address, this.port));
            task.Start();

        }

        public int Send(string data)
        {
            return socket.Send(ip, 1000, data);
        }

        private void OnDataReceived(object sender, string data)
        {
            if (OnClientReceived != null)
            {
                OnClientReceived.Invoke(this, data);
            }
        }
    }
}
