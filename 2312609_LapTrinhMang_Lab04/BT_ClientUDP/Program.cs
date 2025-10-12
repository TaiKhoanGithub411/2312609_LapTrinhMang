using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BT_ClientUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            string tiepTuc;
            do
            {
                try
                {
                    // Nhập dữ liệu từ bàn phím
                    Employee emp = new Employee();
                    Console.WriteLine("------ Nhập thông tin nhân viên ------");
                    Console.Write("ID nhân viên: ");
                    emp.EmployeeID = int.Parse(Console.ReadLine());
                    Console.Write("Họ: ");
                    emp.LastName = Console.ReadLine();
                    Console.Write("Tên: ");
                    emp.FirstName = Console.ReadLine();
                    Console.Write("Số năm kinh nghiệm: ");
                    emp.YearsService = int.Parse(Console.ReadLine());
                    Console.Write("Lương: ");
                    emp.Salary = double.Parse(Console.ReadLine());

                    // Chuyển đối tượng thành mảng byte
                    byte[] data = emp.GetBytes();

                    // Tạo mảng byte chứa dữ liệu thực tế (chỉ lấy phần có dữ liệu)
                    byte[] sendData = new byte[emp.size];
                    Buffer.BlockCopy(data, 0, sendData, 0, emp.size);

                    // Gửi dữ liệu qua UDP
                    using (UdpClient client = new UdpClient())
                    {
                        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
                        client.Send(sendData, sendData.Length, serverEndPoint);

                        Console.WriteLine($"Kích thước gói tin đã gửi: {sendData.Length} bytes");
                        Console.WriteLine("==> Đã gửi dữ liệu thành công đến server.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Lỗi định dạng đầu vào. Vui lòng nhập đúng kiểu dữ liệu.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Lỗi: " + e.Message);
                }

                Console.Write("\nBạn có muốn tiếp tục không? (Nhập 'khong' để thoát): ");
                tiepTuc = Console.ReadLine();
            } while (tiepTuc.Trim().ToLower() != "khong");

            Console.WriteLine("Chương trình kết thúc.");
            Console.ReadKey();
        }
    }
}
