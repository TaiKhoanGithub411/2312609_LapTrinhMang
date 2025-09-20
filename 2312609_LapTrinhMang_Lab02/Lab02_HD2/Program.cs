using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab02_HD2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);            
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);
            Console.WriteLine("Đang chờ kết nối từ client ...");
            Socket clientSocket = serverSocket.Accept();            
            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine("Chấp nhận kết nối từ: " + clientEndPoint.ToString());
            string hello = "Hello client";
            //Mảng chứa dữ liệu
            byte[] buff;
            //Chuyển chuỗi thành mảng byte
            buff = Encoding.ASCII.GetBytes(hello);
            //Chuyển nội dung câu chào đến client
            clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);
            Console.ReadLine();
        }
    }
}
