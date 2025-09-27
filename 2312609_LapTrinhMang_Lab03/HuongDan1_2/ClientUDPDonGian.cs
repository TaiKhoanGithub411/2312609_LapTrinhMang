using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace HuongDan1_2
{
    class ClientUDPDonGian
    {
        static void Main(string[] args)
        {
            //CLIENT UDP ĐƠN GIẢN
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);// đối tượng gửi dữ liệu kiểu UDP.
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);//Địa chỉ, port cửa server nhận dữ liệu
            //Phần cải tiến theo yêu cầu bài tập
            while(true)
            {
                Console.WriteLine("Nhập tin nhắn gửi server (nhập exit để thoát, nhập exit all để thoát client và server): ");
                string msg = Console.ReadLine();
                //Chuỗi lưu thông điệp
                byte[] buff = Encoding.UTF8.GetBytes(msg);
                //Gửi dữ liệu
                clientSocket.SendTo(buff, buff.Length, SocketFlags.None, serverEndPoint);
                //Kiểm tra điều kiện thoát
                if(msg.ToLower()=="exit")
                {
                    Console.WriteLine("Đóng client ...");
                    break;
                }
                if(msg.ToLower()=="exit all")
                {
                    Console.WriteLine("Đang đóng server. Client tự động đóng");
                    break;
                }
            }
            clientSocket.Close();
            Console.WriteLine("Đã đóng client. Nhấn phím bất kỳ để thoát");
            Console.ReadKey();
        }
    }
}
