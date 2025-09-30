using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HuongDan3_2
{
    class ClientUDP
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);//Gửi dữ liệu tới port và ip này.
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//Socket cho client
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                Console.Write("Nhập tin nhắn gửi tới server: ");
                string str = Console.ReadLine();
                byte[] sendbuff = new byte[1024];
                sendbuff = Encoding.UTF8.GetBytes(str);
                clientSocket.SendTo(sendbuff, 0, sendbuff.Length, SocketFlags.None, serverEndPoint);
                byte[] receivebuff = new byte[1024];
                int byteReceive = clientSocket.ReceiveFrom(receivebuff, 0, receivebuff.Length, SocketFlags.None, ref remote);
                str = Encoding.UTF8.GetString(receivebuff, 0, byteReceive);
                Console.WriteLine(str);

                if (str.ToLower() == "exit all")
                    break;
            }
            clientSocket.Close();
            Console.ReadKey();
        }
    }
}
