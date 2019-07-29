using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace Examples
{
    static partial class Program
    {
        public static void Main(string[] args)
        {
            List<string> poruke = new List<string>();
            using (var server = new ResponseSocket())
            {
                server.Bind("tcp://*:5555");
                while (true)
                {
                    var message = server.ReceiveFrameString();
                    poruke.Add(message);
                    //Console.WriteLine("Received {0}", message);
                    Thread.Sleep(100);

                    foreach (var item in poruke)
                    {
                        Console.WriteLine(item);
                    }
                    server.SendFrame(message);
                }
            }

        }
    }
}