using System;
using System.Net;
using System.Net.NetworkInformation;

namespace Lab01_02
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			ThongTinIP();
			Console.ReadKey();
		}
		static void ThongTinIP()
		{
			try
			{
				//Lấy các Network interface
				NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
				foreach(var nic in nics)
				{
					//Kiểm tra card mạng có hoạt động
					if(nic.OperationalStatus==OperationalStatus.Up)
					{
						//lấy thông tin IP trong mạng này
						IPInterfaceProperties pro = nic.GetIPProperties();
						//Lấy IPV4
						foreach(UnicastIPAddressInformation ip in pro.UnicastAddresses)
						{
							if(ip.Address.AddressFamily==System.Net.Sockets.AddressFamily.InterNetwork)
							{
								Console.WriteLine("Địa chỉ IP: " + ip.Address.ToString());
								Console.WriteLine("Subnet mask: " + ip.IPv4Mask.ToString());
							}	
						}
						//lấy default gateway
						foreach(GatewayIPAddressInformation gateway in pro.GatewayAddresses)
						{
							Console.WriteLine("Default Gateway: " + gateway.Address.ToString());
						}
						Console.WriteLine();
					}	
				}	

			}
			catch(Exception ex)
			{
				Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
			}
		}
	}
}
