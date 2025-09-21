using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Lab02_BT2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Đang kết nối với server ...");
            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Không thể kết nối với server: " + se.Message);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Kết nôi thành công với server...");
            // Nhận câu chào đầu tiên từ server
            byte[] buff = new byte[1024];
            int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
            string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
            Console.WriteLine("Server: " + str);

            while (true)
            {
                Console.Write("Client: ");
                str = Console.ReadLine();

                // Kiểm tra nếu người dùng nhập "exit"
                if (str.ToLower() == "exit")
                {
                    // Gửi tin nhắn "exit" đến server để server biết
                    buff = Encoding.ASCII.GetBytes(str);
                    serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
                    break; // Thoát khỏi vòng lặp
                }

                buff = Encoding.ASCII.GetBytes(str);
                serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);

                buff = new byte[1024];
                byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                str = Encoding.ASCII.GetString(buff, 0, byteReceive);

                Console.WriteLine("Server: " + str);
            }
            // Đóng kết nối trước khi thoát
            serverSocket.Close();
            Console.WriteLine("Đã đóng kết nối. Nhấn Enter để thoát");
            Console.ReadLine();
        }
    }
}
