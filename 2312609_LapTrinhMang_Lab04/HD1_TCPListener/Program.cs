using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HD1_TCPListener
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            int recv;
            byte[] data = new byte[1024];
            TcpListener newsock = new TcpListener(9000);
            newsock.Start();
            Console.WriteLine("Waitting for a client ...");
            TcpClient client = newsock.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            Console.WriteLine(client.Client.RemoteEndPoint.ToString());
            string welcome = "Welcome to the server";
            data = Encoding.UTF8.GetBytes(welcome);
            ns.Write(data, 0, data.Length);
            while(true)
            {
                data = new byte[1024];
                recv = ns.Read(data, 0, data.Length);
                if (recv == 0)
                    break;
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
                ns.Write(data, 0, recv);
            }
            ns.Close();
            client.Close();
            newsock.Stop();
        }
    }
}
