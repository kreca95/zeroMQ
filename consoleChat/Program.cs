using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZeroMQ;

namespace Examples.Client
{
    static partial class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Klijent");   
            Console.WriteLine("Unesite nick");
            string nick = Console.ReadLine();
            string message = string.Empty;
            string sendMessageTo = Console.ReadLine();
            using (var dealer = new DealerSocket())
            {
                dealer.Connect("tcp://127.0.0.1:5555");

                dealer.Options.Identity = Encoding.ASCII.GetBytes(nick);
                while (true)
                {
                    message=Console.ReadLine();
                    var msgToSrv = new NetMQMessage();
                    msgToSrv.Append(message);
                    msgToSrv.Append(nick);
                    dealer.SendMultipartMessage(msgToSrv);
                    //dealer.SendFrame(message);
                    var receiveFromServer=dealer.ReceiveMultipartMessage();
                    Console.WriteLine(receiveFromServer);
                    
                }
            }
        }
    }
}