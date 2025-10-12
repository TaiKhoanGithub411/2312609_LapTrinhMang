using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_ServerUDP
{
    public class Employee
    {
        public int EmployeeID;
        public string LastName;
        public string FirstName;
        public int YearsService;
        public double Salary;
        public int size;

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
            this.LastName = Encoding.UTF8.GetString(data, place, lastNameSize);
            place += lastNameSize;

            // Đọc độ dài của FirstName, rồi đọc chuỗi FirstName bằng UTF8
            int firstNameSize = BitConverter.ToInt32(data, place);
            place += 4;
            this.FirstName = Encoding.UTF8.GetString(data, place, firstNameSize);
            place += firstNameSize;

            // Đọc YearsService (4 bytes)
            this.YearsService = BitConverter.ToInt32(data, place);
            place += 4;

            // Đọc Salary (8 bytes)
            this.Salary = BitConverter.ToDouble(data, place);
        }

        public override string ToString()
        {
            return $"ID: {EmployeeID}, Họ: {LastName}, Tên: {FirstName}, Năm kinh nghiệm: {YearsService}, Lương: {Salary:C}";
        }
    }
}
