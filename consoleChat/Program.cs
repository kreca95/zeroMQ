using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZeroMQ;

namespace Examples
{
    static partial class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Unesite nick");
            string nick = Console.ReadLine();
            using (var client = new RequestSocket())
            {
                client.Connect("tcp://localhost:5555");
                for (int i = 0; i < 10; i++)
                {

                    Console.WriteLine("-----");
                    var mess = Console.ReadLine();
                    client.SendFrame(nick + " "+mess);
                    var message = client.ReceiveFrameString();
                    Console.WriteLine("Received {0}", message);

                }
            }
        }
    }
}