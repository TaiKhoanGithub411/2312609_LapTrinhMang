using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_ServerTCP
{
    public class Employee
    {
        public int EmployeeID;
        public string LastName;
        public string FirstName;
        public int YearsService;
        public double Salary;
        public int size; // Kích thước thực tế của mảng byte

        public Employee() { }

        // Constructor để tái tạo đối tượng từ mảng byte
        public Employee(byte[] data)
        {
            int place = 0;
            // Đọc EmployeeID (4 bytes)
            this.EmployeeID = BitConverter.ToInt32(data, place);
            place += 4;

            // Đọc độ dài của LastName, rồi đọc chuỗi LastName bằng UTF8
            int lastNameSize = BitConverter.ToInt32(data, place);
            place += 4;
            this.LastName = Encoding.UTF8.GetString(data, place, lastNameSize); // SỬA Ở ĐÂY
            place += lastNameSize;

            // Đọc độ dài của FirstName, rồi đọc chuỗi FirstName bằng UTF8
            int firstNameSize = BitConverter.ToInt32(data, place);
            place += 4;
            this.FirstName = Encoding.UTF8.GetString(data, place, firstNameSize); // SỬA Ở ĐÂY
            place += firstNameSize;

            // Đọc YearsService (4 bytes)
            this.YearsService = BitConverter.ToInt32(data, place);
            place += 4;

            // Đọc Salary (8 bytes)
            this.Salary = BitConverter.ToDouble(data, place);
        }

        // Phương thức chuyển đối tượng thành mảng byte
        public byte[] GetBytes()
        {
            byte[] data = new byte[1024]; // Khởi tạo mảng byte đủ lớn
            int place = 0;

            // Chuyển EmployeeID (int) vào mảng byte
            Buffer.BlockCopy(BitConverter.GetBytes(EmployeeID), 0, data, place, 4);
            place += 4;

            
            // Chuyển LastName (string) sang byte bằng UTF8
            int lastNameByteCount = Encoding.UTF8.GetByteCount(LastName); // Tính số byte chính xác
            Buffer.BlockCopy(BitConverter.GetBytes(lastNameByteCount), 0, data, place, 4);
            place += 4;
            Encoding.UTF8.GetBytes(LastName, 0, LastName.Length, data, place); // Mã hóa và chép vào buffer
            place += lastNameByteCount;

            // Chuyển FirstName (string) sang byte bằng UTF8
            int firstNameByteCount = Encoding.UTF8.GetByteCount(FirstName); // Tính số byte chính xác
            Buffer.BlockCopy(BitConverter.GetBytes(firstNameByteCount), 0, data, place, 4);
            place += 4;
            Encoding.UTF8.GetBytes(FirstName, 0, FirstName.Length, data, place); // Mã hóa và chép vào buffer
            place += firstNameByteCount;

            // Chuyển YearsService (int) vào mảng byte
            Buffer.BlockCopy(BitConverter.GetBytes(YearsService), 0, data, place, 4);
            place += 4;

            // Chuyển Salary (double) vào mảng byte
            Buffer.BlockCopy(BitConverter.GetBytes(Salary), 0, data, place, 8);
            place += 8;

            this.size = place; // Gán kích thước thực tế
            return data;
        }

        public override string ToString()
        {
            return $"ID: {EmployeeID}, Họ: {LastName}, Tên: {FirstName}, Năm kinh nghiệm: {YearsService}, Lương: {Salary:C}";
        }
    }
}
