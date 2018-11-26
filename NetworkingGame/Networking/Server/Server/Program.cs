using System;
using System.Collections.Generic;
using System.Configuration;
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
        public string playerOneAddress = ConfigurationSettings.AppSettings.GetValues("playerOne")[0];
        public string playerTwoAddress = ConfigurationSettings.AppSettings.GetValues("playerTwo")[0];
        public int playerOneScore = 0;
        public int playerTwoScore = 0;
        public static List<string> addressList = new List<string>();
        public int numberOfPlayers = 0;
        public bool ready = false;


        static void Main(string[] args)
        {
            Program server = new Program();
            socket = new SimpleSocket("TCP");
            socketTwo = new SimpleSocket("TCP");
            socket.OnDataReceived += server.OnDataReceived;
            Console.WriteLine("Listening");
            socket.Listen("192.168.0.1", 1000);
            while (true) ;
        }

        public void OnDataReceived(object sender, string data)
        {
            Console.WriteLine(this.playerTwoAddress);

            if (data.Equals(playerOneAddress))
            {
                playerOneScore++;
            }
            else if (data.Equals(playerTwoAddress))
            {
                playerTwoScore++;
            }
            else
            {
                numberOfPlayers++;
            }
            
            if(ready)
            {
                   // socket.Send(playerOneAddress, 800, playerOneAddress + "," + playerOneScore + "," + playerTwoAddress + "," + playerTwoScore);
                    socket.Send(playerTwoAddress, 900, playerOneAddress + "," + playerOneScore + "," + playerTwoAddress + "," + playerTwoScore);
            }
            else if(numberOfPlayers > 1)
            {
                ready = true;
              //  socket.Send(playerOneAddress, 800, "start");
                socket.Send(playerTwoAddress, 900, "start");
            }

        }
    }
}
