using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HuongDan7_Client
{
    class Program
    {
        private static int SndRcvData(Socket s, byte[] message, EndPoint rmtdevice)
        {
            int recv;
            int retry = 0;
            byte[] data = new byte[1024];
            while (true)
            {
                Console.WriteLine($"Truyền lại lần thứ: #{retry}");
                try
                {
                    s.SendTo(message, message.Length, SocketFlags.None, rmtdevice);
                    recv = s.ReceiveFrom(data, ref rmtdevice);
                }
                catch (SocketException)
                {
                    recv = 0;
                }
                if (recv > 0)
                {
                    return recv; // nhận được dữ liệu trả về
                }
                else
                {
                    retry++;
                    if (retry > 4) // thử lại tối đa 5 lần
                    {
                        return 0;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);

            // Thiết lập thời gian chờ nhận (timeout) 3000 ms = 3 giây
            clientSocket.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.ReceiveTimeout, 3000);

            Console.WriteLine("Client sẵn sàng gửi dữ liệu. Gõ 'exit' để thoát.");

            while (true)
            {
                Console.Write("Nhập thông điệp gửi server: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    break;

                byte[] sendData = Encoding.UTF8.GetBytes(input);
                int recv = SndRcvData(clientSocket, sendData, ipep);
                if (recv > 0)
                {
                    string response = Encoding.UTF8.GetString(sendData, 0, recv);
                    Console.WriteLine("Phản hồi từ server: " + response);
                }
                else
                {
                    Console.WriteLine("Không nhận được phản hồi sau nhiều lần thử");
                }
            }
            clientSocket.Close();
        }
    }
}
