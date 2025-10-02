using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuongDan5_1
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint remote = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            for (int i = 1; i <= 5; i++)
            {
                byte[] buff = Encoding.UTF8.GetBytes("Thông điệp " + i.ToString());
                clientSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote);
            }

            Console.WriteLine("Đã gửi");
            clientSocket.Close();
            Console.ReadKey();
        }
    }
}
