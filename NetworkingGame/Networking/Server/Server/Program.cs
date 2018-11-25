using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketHelper;

namespace Server
{
    class Program
    {
        public static SimpleSocket socket;
        public static SimpleSocket socketTwo;
        public static List<string> addressList = new List<string>();
        static void Main(string[] args)
        {
            Program server = new Program();
            socket = new SimpleSocket("TCP");
            socketTwo = new SimpleSocket("TCP");
            socket.OnDataReceived += server.OnDataReceived;
            Console.WriteLine("Listening");
            socket.Listen("192.168.1.67", 1000);
            while (true) ;
        }

        public void OnDataReceived(object sender, string data)
        {
            Console.WriteLine("Received Data");
            socket.Send("192.168.1.67", 800, "From Server: " + data);
            socket.Send("192.168.1.67", 900, "From Server: " + data);
        }
    }
}
