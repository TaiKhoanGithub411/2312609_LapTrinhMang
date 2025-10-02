using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuongDan6_2
{
    class Client
    {
        static void Main(string[] args)
        {
            //chạy server của project HuongDan3_1
            //client giúp cảnh báo khi mất dữ liệu
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            int bufferSize = 10; // kích thước bộ đệm ban đầu
            byte[] data = new byte[bufferSize];
            EndPoint remote = (EndPoint)remoteEndPoint;

            while (true)
            {
                Console.Write("Nhập thông điệp gửi server (nhập 'exit' để thoát): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    break;

                try
                {
                    clientSocket.SendTo(Encoding.UTF8.GetBytes(input), remote);
                    int recv = clientSocket.ReceiveFrom(data, ref remote);
                    string response = Encoding.UTF8.GetString(data, 0, recv);
                    Console.WriteLine("Server trả về: " + response);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Cảnh báo: dữ liệu bị mất (buffer nhỏ), thử lại với buffer lớn hơn...");
                    bufferSize += 10; // tăng dần kích thước bộ đệm
                    data = new byte[bufferSize];
                }
            }
            clientSocket.Close();
        }
    }
}
