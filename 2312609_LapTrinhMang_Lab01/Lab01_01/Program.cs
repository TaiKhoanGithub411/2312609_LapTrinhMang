using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_01
{
	class Program
	{
		static void GetHostInfo(string host)
		{
			try
			{
				IPHostEntry hostInfo = Dns.GetHostEntry(host);
				Console.WriteLine("Tên miền: " + hostInfo.HostName);
				Console.Write("Địa chỉ IP: ");
				foreach (IPAddress ipadrr in hostInfo.AddressList)
				{
					Console.WriteLine(ipadrr.ToString() + " ");
				}
				Console.WriteLine();
			}
			catch (Exception)
			{
				Console.WriteLine("Không phân giải đươc tên miền: " + host + "\n");
			}
		}
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			foreach (String arg in args)
			{
				Console.WriteLine("Phân giải tên miền: " + arg);
				GetHostInfo(arg);
			}
			Console.ReadKey();
		}
	}
}
