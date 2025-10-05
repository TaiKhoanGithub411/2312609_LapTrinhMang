using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BT_ClientTCP
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

                    // Kết nối tới server
                    using (TcpClient client = new TcpClient("127.0.0.1", 9050))
                    using (NetworkStream ns = client.GetStream())
                    {
                        // Bước 1: Gửi 2 byte chứa kích thước của gói tin
                        Console.WriteLine($"Kích thước gói tin sẽ gửi: {emp.size} bytes");
                        ns.Write(BitConverter.GetBytes((short)emp.size), 0, 2);

                        // Bước 2: Gửi mảng byte dữ liệu chính
                        ns.Write(data, 0, emp.size);
                        ns.Flush();

                        Console.WriteLine("==> Đã gửi dữ liệu thành công đến server.");
                    } // client và stream sẽ tự động đóng ở đây
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
