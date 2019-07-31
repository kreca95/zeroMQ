using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace Examples.Server
{
    static partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Server");
            using (var router = new RouterSocket())
            {
                router.Options.RouterMandatory = true;
                router.Bind("tcp://127.0.0.1:5555");

                bool more = false;
                while (true)
                {
                    string a = router.ReceiveFrameString(Encoding.ASCII,out more);

                    Console.WriteLine(a);
                }
                

            }
        }
    }
}