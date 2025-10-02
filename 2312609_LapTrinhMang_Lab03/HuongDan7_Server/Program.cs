using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HuongDan7_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
            SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            serverSocket.Bind(serverEndPoint);

            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Server đang chờ nhận dữ liệu...");

            while (true)
            {
                byte[] buff = new byte[1024];
                int byteReceive = serverSocket.ReceiveFrom(buff, ref remote);
                string receivedContent = Encoding.UTF8.GetString(buff, 0, byteReceive);
                Console.WriteLine("Nhận từ client: " + receivedContent);

                // Giả lập mất gói tin bằng cách delay lâu để client timeout
                if (receivedContent == "test delay")
                {
                    Thread.Sleep(5000); // delay 5 giây
                }

                // Gửi lại dữ liệu cho client làm hồi đáp
                serverSocket.SendTo(buff, 0, byteReceive, SocketFlags.None, remote);
            }
        }
    }
}
