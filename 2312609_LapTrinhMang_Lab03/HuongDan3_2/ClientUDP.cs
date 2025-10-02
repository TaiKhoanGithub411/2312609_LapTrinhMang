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

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint remote = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            //Mục III.4
            clientSocket.Connect(remote);

            while (true)
            {
                Console.Write("Nhập thông điệp gởi đến server: ");
                string str = Console.ReadLine();
                byte[] buff = Encoding.UTF8.GetBytes(str);
                clientSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote);

                byte[] receiveBuff = new byte[1024];
                int byteReceive = clientSocket.ReceiveFrom(receiveBuff, 0, receiveBuff.Length, SocketFlags.None, ref remote);
                string receivedStr = Encoding.UTF8.GetString(receiveBuff, 0, byteReceive);
                Console.WriteLine("Server trả về: " + receivedStr);
            }            
        }
    }
}
