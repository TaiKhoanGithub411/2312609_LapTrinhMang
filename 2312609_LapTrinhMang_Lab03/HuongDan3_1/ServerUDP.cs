using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuongDan3_1
{
    class ServerUDP
    {
        //Cải tiến chương trình UDP có thể nhận và gửi dữ liệu liên tục
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverSocket.Bind(serverEndPoint);
            Console.WriteLine("Chờ kết nối từ client ...");
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] buff = new byte[1024];
                int bytesReceived = serverSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
                string str= Encoding.ASCII.GetString(buff, 0, bytesReceived);
                Console.WriteLine(str);
                //serverSocket.SendTo(buff, 0, buff.Lengh, SocketFlags.None, remote);
            }    
        }
    }
}
