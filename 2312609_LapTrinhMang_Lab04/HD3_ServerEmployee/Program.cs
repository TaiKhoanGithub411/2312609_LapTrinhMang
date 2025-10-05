using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HD3_ServerEmployee
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            byte[] data = new byte[1024];
            TcpListener server = new TcpListener(IPAddress.Any, 9050);
            server.Start();
            Console.WriteLine("Chờ client kết nối ...");
            TcpClient client = server.AcceptTcpClient();
            NetworkStream ns = client.GetStream();               
            byte[] size = new byte[2];
            int recv = ns.Read(size, 0, 2);
            int packsize = BitConverter.ToInt16(size, 0);
            Console.WriteLine($"Kích thước gói tin = {packsize}");
            recv = ns.Read(data, 0, packsize);
            Employee emp1 = new Employee(data);
            Console.WriteLine($"emp1.EmployeeID = {emp1.EmployeeID}");
            Console.WriteLine($"emp1.LastName = {emp1.LastName}", emp1.LastName);
            Console.WriteLine($"emp1.FirstName = {emp1.FirstName}", emp1.FirstName);
            Console.WriteLine($"emp1.YearsService = {emp1.YearsService}");
            Console.WriteLine($"emp1.Salary = {emp1.Salary:n}" );

            ns.Close();
            client.Close();
            server.Stop();
            Console.ReadKey();
        }
    }
}
