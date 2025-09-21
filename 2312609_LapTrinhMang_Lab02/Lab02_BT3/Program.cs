using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab02_BT3
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
            catch (SocketException)
            {
                Console.WriteLine("Không thể kết nối với server.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Kết nối thành công. Nhập phép tính (vd 3*5) hoặc gõ 'exit' để thoát");

            byte[] buff = new byte[1024];

            while (true)
            {
                Console.Write("Phép tính: ");
                string expression = Console.ReadLine();
                if (string.IsNullOrEmpty(expression)) continue;
                if (expression.ToLower() == "exit")
                {
                    break;
                }
                // Gửi phép tính đến server
                byte[] expressionBuff = Encoding.ASCII.GetBytes(expression);
                serverSocket.Send(expressionBuff, 0, expressionBuff.Length, SocketFlags.None);
                // Nhận kết quả từ server
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                string result = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine("Kết quả: " + result);
            }
            serverSocket.Close();
            Console.WriteLine("Đã ngắt kết nối. Nhấn Enter để thoát");
            Console.ReadLine();
        }
    }
}
