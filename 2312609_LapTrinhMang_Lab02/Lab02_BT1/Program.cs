using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Data;

namespace Lab02_BT1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập để hiển thị tiếng Việt đúng trong Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            // 1. Khởi tạo Server
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverSocket.Bind(serverEndPoint);
                serverSocket.Listen(10);
                Console.WriteLine("Máy tính Server đã khởi động. Đang chờ kết nối...");
                // 2. Chấp nhận một kết nối từ Client
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("Đã chấp nhận kết nối từ: " + clientSocket.RemoteEndPoint.ToString());
                // 3. Gửi lời chào đến Client
                string welcomeMessage = "Chào mừng đến với Máy tính Server! Hãy gửi phép tính của bạn.";
                byte[] data = Encoding.ASCII.GetBytes(welcomeMessage);
                clientSocket.Send(data);
                // 4. Bắt đầu vòng lặp nhận và xử lý dữ liệu
                while (true)
                {                    
                    data = new byte[1024];
                    int bytesReceived = clientSocket.Receive(data);
                    // Nếu nhận 0 byte, client đã đóng kết nối một cách bình thường
                    if (bytesReceived == 0)
                    {
                        Console.WriteLine("Client đã ngắt kết nối.");
                        break; // Thoát khỏi vòng lặp
                    }
                    // Chuyển dữ liệu nhận được thành chuỗi (biểu thức)
                    string expression = Encoding.ASCII.GetString(data, 0, bytesReceived);
                    Console.WriteLine($"Nhận từ Client: {expression}");
                    // Kiểm tra lệnh thoát từ client
                    if (expression.Trim().ToLower() == "exit")
                    {
                        Console.WriteLine("Client đã yêu cầu thoát.");
                        break;
                    }
                    // 5. Xử lý tính toán
                    string result;
                    try
                    {
                        // Sử dụng DataTable.Compute để tính toán biểu thức
                        DataTable table = new DataTable();
                        result = table.Compute(expression, string.Empty).ToString();
                    }
                    catch (Exception)
                    {
                        // Nếu biểu thức không hợp lệ, gửi lại thông báo lỗi
                        result = "Lỗi: Biểu thức không hợp lệ.";
                    }
                    // 6. Gửi kết quả lại cho Client
                    data = Encoding.ASCII.GetBytes(result);
                    clientSocket.Send(data);
                    Console.WriteLine($"Đã gửi kết quả: {result}");
                }
                // 7. Đóng kết nối với client
                clientSocket.Close();
                serverSocket.Close();
            }
            catch (SocketException ex)
            {
                // Xử lý các lỗi socket khác (ví dụ: client đóng đột ngột)
                Console.WriteLine($"Lỗi Socket: {ex.Message}");
                Console.WriteLine("Client có thể đã ngắt kết nối đột ngột.");
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung khác
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Server đã đóng. Nhấn Enter để thoát.");
                Console.ReadLine();
            }
        }
    }
}
