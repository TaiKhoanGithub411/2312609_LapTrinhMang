using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Lab05_BT2_TcpEchoServerThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length != 1)
                throw new ArgumentException("Parameter(s): <Port>");
            int serverPort = Int32.Parse(args[0]);

            TcpListener listener = new TcpListener(IPAddress.Any, serverPort);
            ILogger logger = new ConsoleLogger();
            listener.Start();

            Console.WriteLine("Server đang lắng nghe trên port " + serverPort + "...");
            Console.WriteLine("Đang chờ client kết nối...");

            for (; ; )
            {
                try
                {
                    Socket client = listener.AcceptSocket();
                    EchoProtocol protocol = new EchoProtocol(client, logger);
                    Thread thread = new Thread(new ThreadStart(protocol.handleclient));
                    thread.Start();
                    logger.writeEntry("Created and started Thread = " + thread.GetHashCode());
                }
                catch (System.IO.IOException e)
                {
                    logger.writeEntry("Error: " + e.Message);
                }
            }            
        }
    }
}
