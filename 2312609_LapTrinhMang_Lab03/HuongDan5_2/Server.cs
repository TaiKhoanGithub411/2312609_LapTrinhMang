using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuongDan5_2
{
    class Server
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            serverSocket.Bind(serverEndPoint);

            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Chờ nhận dữ liệu...");

            // Nhận 5 thông điệp từ client
            for (int i = 1; i <= 5; i++)
            {
                byte[] buff = new byte[1024];
                int byteReceive = serverSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
                string str = Encoding.UTF8.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
            }

            Console.WriteLine("Đã nhận thông điệp");
            serverSocket.Close();
            Console.ReadKey();
        }
    }
}
