using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab02_HD4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Đang kết nôi với server ...");
            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Không thể kết nối với server");
                Console.ReadLine();
                return;
            }
            if(serverSocket.Connected)
            {
                Console.WriteLine("Kết nối với Server thành công");
                // Nhận câu chào đầu tiên từ server
                byte[] buff = new byte[1024];
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine("Server: " + str);
                //Phần cải tiến theo mục (III.5)
                while(true)
                {
                    //Nhập nội dung gửi
                    Console.Write("Client: ");
                    str = Console.ReadLine();
                    buff = Encoding.ASCII.GetBytes(str);
                    serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
                    buff = new byte[1024];
                    byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                    str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                    Console.WriteLine("Server: " + str);
                }
            }
        }
    }
}
