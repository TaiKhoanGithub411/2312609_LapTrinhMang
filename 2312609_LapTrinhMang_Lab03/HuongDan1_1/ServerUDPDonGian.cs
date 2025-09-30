using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace HuongDan1_1
{
    class ServerUDPDonGian
    {
        static void Main(string[] args)
        {   
            //SERVER UDP ĐƠN GIẢN
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);//lắng nghe thông điệp từ IP và Port này
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//Điểm giao tiếp với client
            serverSocket.Bind(serverEndPoint);//Liên kết server với end point
            Console.WriteLine("Chờ kết nối ...");
            //Phần cải tiến theo yêu cầu bài tập 
            //Chương trình này đang gấy mất thông điệp
            while(true)//Vòng lặp nhận tin nhắn từ client liên tục
            {
                byte[] buff = new byte[1024];//chứa dữ liệu nhận được
                EndPoint remote = new IPEndPoint(IPAddress.Any, 0);//remote lưu thông tin client
                int bytesReceived = serverSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
                //Hiển thị thông điệp từ client
                string msg = Encoding.UTF8.GetString(buff, 0, bytesReceived);
                Console.WriteLine($"Nội dung tin nhắn từ client {remote}: {msg}");
                //Đóng server nếu tin nhắn là 'exit all'
                if(msg.ToLower()=="exit all")
                {
                    Console.WriteLine($"Đóng server ...");
                    break;
                }
            }
            serverSocket.Close();
            Console.WriteLine("Đã đóng server. Nhấn phím bất kỳ để thoát.");
            Console.ReadKey();
        }
    }
}
