using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HD3_ClientEmployee
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Employee emp1 = new Employee();
            emp1.EmployeeID = 1;
            emp1.LastName = "Nguyen";
            emp1.FirstName = "An";
            emp1.YearsService = 5;
            emp1.Salary = 50000;
            TcpClient client;
            try
            {
                client = new TcpClient("127.0.0.1", 9050);
            }
            catch(SocketException)
            {
                Console.WriteLine("Không thể kết nối với server");
                return;
            }
            NetworkStream ns = client.GetStream();
            byte[] data = emp1.GetBytes();
            int size = emp1.size;
            byte[] packsize = new byte[2];
            Console.WriteLine($"Kích thước của gói tim = {size}");
            packsize = BitConverter.GetBytes(size);
            ns.Write(packsize, 0, 2);
            ns.Write(data, 0, size);
            ns.Flush();
            ns.Close();
            client.Close();
            Console.ReadKey();
        }
    }
}
