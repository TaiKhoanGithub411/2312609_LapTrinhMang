using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BT_ServerUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            UdpClient server = null;

            try
            {
                server = new UdpClient(9050);
                Console.WriteLine("Server UDP đã khởi động và đang chờ dữ liệu...");

                using (StreamWriter sw = new StreamWriter("employee_data.txt", append: true, encoding: Encoding.UTF8))
                {
                    while (true)
                    {
                        try
                        {
                            // Nhận dữ liệu từ client
                            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                            byte[] receivedData = server.Receive(ref clientEndPoint);

                            Console.WriteLine($"\nĐã nhận dữ liệu từ client: {clientEndPoint.Address}:{clientEndPoint.Port}");
                            Console.WriteLine($"Kích thước dữ liệu nhận được: {receivedData.Length} bytes");

                            // Tạo đối tượng Employee từ mảng byte
                            Employee emp = new Employee(receivedData);

                            // Xuất dữ liệu ra màn hình
                            Console.WriteLine("Dữ liệu nhân viên nhận được:");
                            Console.WriteLine(emp.ToString());

                            // Ghi dữ liệu vào file .txt
                            sw.WriteLine(emp.ToString());
                            sw.Flush();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi xử lý dữ liệu: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi nghiêm trọng của Server: {e.Message}");
            }
            finally
            {
                server?.Close();
                Console.WriteLine("Server đã dừng.");
            }

            Console.ReadKey();
        }
    }
}
