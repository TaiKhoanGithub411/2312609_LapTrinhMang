using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab02_HD3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            // Tạo Socket cho client
            // Cấu hình socket phải giống với server (TCP/IP).
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Đang kết nối với Server ...");
            // Kết nối đến server
            serverSocket.Connect(serverEndPoint);
            // Kiểm tra kết nối và nhận dữ liệu
            if (serverSocket.Connected)
            {
                Console.WriteLine("Kết nối thành công với server...");              
                byte[] buff = new byte[1024];
                // Nhận dữ liệu từ server và lưu vào buffer.
                // Phương thức Receive trả về số byte thực sự nhận được.
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                // Chuyển đổi mảng byte nhận được thành chuỗi.
                // Chỉ chuyển đổi số byte thực sự nhận được (byteReceive).
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine("Server trả lời " + str);
            }
            Console.ReadLine();
        }
    }
}
