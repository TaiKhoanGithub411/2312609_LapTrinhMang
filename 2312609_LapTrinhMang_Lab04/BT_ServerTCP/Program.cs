using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BT_ServerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Any, 9050);
                server.Start();
                Console.WriteLine("Server đã khởi động và đang chờ kết nối...");

                // Sử dụng StreamWriter để ghi file, đảm bảo file được đóng đúng cách khi thoát
                using (StreamWriter sw = new StreamWriter("employee_data.txt", append: true, encoding: Encoding.UTF8))
                {
                    // Vòng lặp vô hạn để chấp nhận nhiều client
                    while (true)
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("\nĐã kết nối với một client!");

                        try
                        {
                            NetworkStream ns = client.GetStream();
                            
                            // Bước 1: Đọc 2 byte đầu tiên để biết kích thước gói tin
                            byte[] sizeInfo = new byte[2];
                            ReadFully(ns, sizeInfo, 2); // Sử dụng hàm đọc đảm bảo đủ byte
                            int packageSize = BitConverter.ToInt16(sizeInfo, 0);
                            Console.WriteLine($"Kích thước gói tin cần đọc: {packageSize} bytes");

                            // Bước 2: Đọc mảng byte dữ liệu chính
                            byte[] data = new byte[packageSize];
                            ReadFully(ns, data, packageSize); // Sử dụng hàm đọc đảm bảo đủ byte
                            Console.WriteLine("Đã nhận đủ dữ liệu từ client.");

                            // Tạo đối tượng Employee từ mảng byte
                            Employee emp = new Employee(data);

                            // Xuất dữ liệu ra màn hình
                            Console.WriteLine("Dữ liệu nhân viên nhận được:");
                            Console.WriteLine(emp.ToString());

                            // Ghi dữ liệu vào file .txt
                            sw.WriteLine(emp.ToString());
                            sw.Flush(); // Ghi ngay vào file
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi xử lý client: {ex.Message}");
                        }
                        finally
                        {
                            client.Close();
                            Console.WriteLine("Đã ngắt kết nối với client.");
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
                server?.Stop();
                Console.WriteLine("Server đã dừng.");
            }
            Console.ReadKey();
        }

        // Hàm để đảm bảo đọc đủ số byte yêu cầu từ stream
        private static void ReadFully(NetworkStream stream, byte[] buffer, int bytesToRead)
        {
            int readSoFar = 0;
            while (readSoFar < bytesToRead)
            {
                int bytesRead = stream.Read(buffer, readSoFar, bytesToRead - readSoFar);
                if (bytesRead == 0) // Kết nối đã bị đóng bởi client
                    throw new IOException("Mất kết nối với client.");
                readSoFar += bytesRead;
            }
        }
    }
}
