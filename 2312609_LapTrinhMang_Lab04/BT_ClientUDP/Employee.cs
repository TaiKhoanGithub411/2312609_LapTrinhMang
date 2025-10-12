using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_ClientUDP
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

        // Phương thức chuyển đối tượng thành mảng byte
        public byte[] GetBytes()
        {
            byte[] data = new byte[1024];
            int place = 0;

            // Chuyển EmployeeID (int) vào mảng byte
            Buffer.BlockCopy(BitConverter.GetBytes(EmployeeID), 0, data, place, 4);
            place += 4;

            // Chuyển LastName (string) sang byte bằng UTF8
            int lastNameByteCount = Encoding.UTF8.GetByteCount(LastName);
            Buffer.BlockCopy(BitConverter.GetBytes(lastNameByteCount), 0, data, place, 4);
            place += 4;
            Encoding.UTF8.GetBytes(LastName, 0, LastName.Length, data, place);
            place += lastNameByteCount;

            // Chuyển FirstName (string) sang byte bằng UTF8
            int firstNameByteCount = Encoding.UTF8.GetByteCount(FirstName);
            Buffer.BlockCopy(BitConverter.GetBytes(firstNameByteCount), 0, data, place, 4);
            place += 4;
            Encoding.UTF8.GetBytes(FirstName, 0, FirstName.Length, data, place);
            place += firstNameByteCount;

            // Chuyển YearsService (int) vào mảng byte
            Buffer.BlockCopy(BitConverter.GetBytes(YearsService), 0, data, place, 4);
            place += 4;

            // Chuyển Salary (double) vào mảng byte
            Buffer.BlockCopy(BitConverter.GetBytes(Salary), 0, data, place, 8);
            place += 8;

            this.size = place;
            return data;
        }

        public override string ToString()
        {
            return $"ID: {EmployeeID}, Họ: {LastName}, Tên: {FirstName}, Năm kinh nghiệm: {YearsService}, Lương: {Salary:C}";
        }
    }
}
