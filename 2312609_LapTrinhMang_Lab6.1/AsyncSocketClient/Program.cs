using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncSocketTCP;

namespace AsyncSocketClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                AsyncSocketTCPClient client = new AsyncSocketTCPClient();
                Console.WriteLine("*** Welcome to Async Socket Client ***");
                Console.WriteLine($"Default IP: 127.0.0.1, Default Port: 9001");
                Console.WriteLine("Press Enter to use default values");
                Console.WriteLine();

                Console.Write("Enter Server IP Address: ");
                string strIPAddress = Console.ReadLine();

                Console.Write("Enter Port Number (0 - 65535): ");
                string strPortInput = Console.ReadLine();

                if (!client.SetServerIPAddress(strIPAddress) ||
                    !client.SetPortNumber(strPortInput))
                {
                    Console.WriteLine(
                        string.Format(
                            "IP Address or port number invalid - {0} - {1} - Press a key to exit",
                            strIPAddress, strPortInput));
                    Console.ReadKey();
                    return;
                }

                await client.ConnectToServer();

                string strInputUser = null;
                Console.WriteLine("Enter message to send (empty line to disconnect):");
                do
                {
                    strInputUser = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(strInputUser))
                    {
                        await client.SendToServer(strInputUser);
                    }
                    else if (string.IsNullOrEmpty(strInputUser))
                    {
                        client.CloseAndDisconnect();
                        break;
                    }
                } while (client.IsConnected);

                Console.WriteLine("Client terminated. Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
