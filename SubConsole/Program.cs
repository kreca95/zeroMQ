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
                    //string a = router.ReceiveFrameString(Encoding.ASCII,out more);
                    //var nick = router.Options.Identity;

                    //Console.WriteLine(nick);
                    //Console.WriteLine(a);

                    var clientMessage = router.ReceiveMultipartMessage();
                    PrintFrames("Server receiving", clientMessage);


                    var msgToClient = new NetMQMessage();
                    msgToClient.Append(clientMessage[0]);
                    msgToClient.Append(clientMessage[1]);
                    msgToClient.Append(clientMessage[2]);

                    router.SendMultipartMessage(msgToClient);
                }


            }
        }

        static void PrintFrames(string operationType, NetMQMessage message)
        {
            for (int i = 0; i < message.FrameCount; i++)
            {
                Console.WriteLine("{0} Socket : Frame[{1}] = {2}", operationType, i,
                    message[i].ConvertToString());
            }
        }
    }
}