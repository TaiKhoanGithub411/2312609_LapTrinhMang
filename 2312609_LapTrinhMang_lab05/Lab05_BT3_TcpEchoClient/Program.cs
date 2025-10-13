using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab05_BT3_TcpEchoClient
{
    class Program
    {
        private const int RCVBUFSIZE = 32;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Kiểm tra tham số đầu vào
            if (args.Length != 3)
            {
                Console.WriteLine("Cách sử dụng: TcpEchoClient <Server IP> <Server Port> <Message>");
                Console.WriteLine("Ví dụ: TcpEchoClient 127.0.0.1 9001 \"Data send to server\"");
                return;
            }

            string server = args[0];
            int port = int.Parse(args[1]);
            string message = args[2];

            Socket clientSocket = null;

            try
            {
                // Chuyển đổi địa chỉ IP
                IPAddress serverAddr = IPAddress.Parse(server);
                IPEndPoint endPoint = new IPEndPoint(serverAddr, port);

                // Tạo socket TCP
                clientSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);

                // Kết nối đến server
                Console.WriteLine($"Đang kết nối đến {server}:{port}...");
                clientSocket.Connect(endPoint);
                Console.WriteLine("Đã kết nối thành công!");

                // Chuyển đổi message sang byte array
                byte[] byteBuffer = Encoding.UTF8.GetBytes(message);

                // Gửi dữ liệu đến server
                Console.WriteLine($"Gửi: {message}");
                clientSocket.Send(byteBuffer, 0, byteBuffer.Length, SocketFlags.None);

                // Nhận dữ liệu echo từ server
                int totalBytesRcvd = 0;
                int bytesRcvd = 0;
                byte[] rcvBuffer = new byte[RCVBUFSIZE];

                Console.Write("Nhận: ");
                while (totalBytesRcvd < byteBuffer.Length)
                {
                    bytesRcvd = clientSocket.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None);

                    if (bytesRcvd == 0)
                    {
                        Console.WriteLine("\nKết nối bị đóng bởi server");
                        break;
                    }

                    totalBytesRcvd += bytesRcvd;
                    Console.Write(Encoding.UTF8.GetString(rcvBuffer, 0, bytesRcvd));
                }

                Console.WriteLine($"\nTổng số byte nhận được: {totalBytesRcvd}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Lỗi: Địa chỉ IP không hợp lệ");
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Lỗi Socket: {se.Message} (ErrorCode: {se.ErrorCode})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi: {e.Message}");
            }
            finally
            {
                // Đóng socket
                if (clientSocket != null)
                {
                    clientSocket.Close();
                    Console.WriteLine("Đã đóng kết nối");
                }
            }

            Console.WriteLine("\nNhấn Enter để thoát...");
            Console.ReadLine();
        }
    }
}
