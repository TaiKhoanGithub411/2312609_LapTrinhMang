using System.Net;
using System.Net.Sockets;
using System;

namespace Lab02_HD1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // 1. Tạo Server EndPoint: Điểm cuối của máy chủ, xác định địa chỉ IP và cổng mà server sẽ lắng nghe.
            // IPAddress.Any cho phép server chấp nhận kết nối từ bất kỳ giao diện mạng nào.
            // 5000 là số cổng (port) mà server sẽ lắng nghe.
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
            // 2. Tạo Server Socket: Đây là socket chính của server, dùng để lắng nghe các kết nối đến.
            // - AddressFamily.InterNetwork: Sử dụng địa chỉ IPv4.
            // - SocketType.Stream: Sử dụng kết nối hướng luồng (TCP).
            // - ProtocolType.Tcp: Giao thức truyền tải là TCP.
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 3. Gắn socket với EndPoint (địa chỉ IP và cổng)
            serverSocket.Bind(serverEndPoint);
            // 4. Bắt đầu lắng nghe các kết nối đến. Số 10 là số lượng kết nối tối đa có thể chờ trong hàng đợi.
            serverSocket.Listen(10);
            Console.WriteLine("Đang chờ kết nối từ client ...");
            // 5. Chấp nhận một kết nối từ client.
            // Chương trình sẽ dừng ở dòng này cho đến khi có một client kết nối tới.
            // Khi có kết nối, một socket mới (clientSocket) được tạo ra để giao tiếp với client đó.
            Socket clientSocket = serverSocket.Accept();
            // 6. Lấy thông tin của client vừa kết nối và hiển thị ra màn hình.
            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine("Chấp nhận kết nối từ: " + clientEndPoint.ToString());
            // Giữ cho chương trình server không bị tắt ngay lập tức để bạn có thể xem kết quả.
            Console.ReadLine();            
        }
    }
}
