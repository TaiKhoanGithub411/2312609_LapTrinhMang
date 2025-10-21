using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class AsyncSocketTCPClient
    {
        // Giá trị mặc định
        private const string DEFAULT_IP = "127.0.0.1";
        private const int DEFAULT_PORT = 9001;

        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;
        private bool isConnected = false;

        public IPAddress ServerIPAddress
        {
            get { return mServerIPAddress; }
        }

        public int ServerPort
        {
            get { return mServerPort; }
        }

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public AsyncSocketTCPClient()
        {
            mClient = null;
            mServerPort = DEFAULT_PORT;
            mServerIPAddress = IPAddress.Parse(DEFAULT_IP);
        }

        public bool SetServerIPAddress(string _IPAddressServer)
        {
            // Nếu người dùng không nhập hoặc nhập rỗng, sử dụng giá trị mặc định
            if (string.IsNullOrWhiteSpace(_IPAddressServer))
            {
                mServerIPAddress = IPAddress.Parse(DEFAULT_IP);
                Console.WriteLine($"Using default IP address: {DEFAULT_IP}");
                return true;
            }

            IPAddress ip = null;
            if (!IPAddress.TryParse(_IPAddressServer, out ip))
            {
                Console.WriteLine("Invalid IP address.");
                return false;
            }
            mServerIPAddress = ip;
            return true;
        }

        public bool SetPortNumber(string _ServerPort)
        {
            // Nếu người dùng không nhập hoặc nhập rỗng, sử dụng giá trị mặc định
            if (string.IsNullOrWhiteSpace(_ServerPort))
            {
                mServerPort = DEFAULT_PORT;
                Console.WriteLine($"Using default port: {DEFAULT_PORT}");
                return true;
            }

            int pnum = 0;
            if (!int.TryParse(_ServerPort, out pnum))
            {
                Console.WriteLine("Invalid port number.");
                return false;
            }
            if (pnum <= 0 || pnum > 65535)
            {
                Console.WriteLine("Port number must be between 0 and 65535");
                return false;
            }
            mServerPort = pnum;
            return true;
        }

        public void CloseAndDisconnect()
        {
            try
            {
                if (mClient != null)
                {
                    if (mClient.Connected)
                        mClient.Close();
                    isConnected = false;
                    Console.WriteLine("Connection closed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
            }
        }

        public async Task ConnectToServer()
        {
            if (mClient == null)
                mClient = new TcpClient();

            try
            {
                Console.WriteLine($"Connecting to {mServerIPAddress}:{mServerPort}...");
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                isConnected = true;
                Console.WriteLine($"Connected to server IP/Port: {mServerIPAddress} / {mServerPort}");

                // Chạy ReadDataAsync trong background task để không block
                _ = Task.Run(() => ReadDataAsync(mClient));
            }
            catch (SocketException sockEx)
            {
                Console.WriteLine($"Connection failed: {sockEx.Message}");
                Console.WriteLine("Unable to connect to server. Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error connecting to server: {e.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        public async Task SendToServer(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                Console.WriteLine("Empty message, no data sent.");
                return;
            }

            try
            {
                if (mClient != null && mClient.Connected)
                {
                    StreamWriter streamWriter = new StreamWriter(mClient.GetStream());
                    streamWriter.AutoFlush = true;
                    await streamWriter.WriteAsync(strInput);
                    Console.WriteLine("Data sent...");
                }
                else
                {
                    throw new InvalidOperationException("Not connected to server");
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error sending data: {ioEx.Message}");
                Console.WriteLine("Connection lost. Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        private async Task ReadDataAsync(TcpClient mClient)
        {
            StreamReader clientStreamReader = null;
            try
            {
                clientStreamReader = new StreamReader(mClient.GetStream());
                char[] buff = new char[64];
                int readByteCount = 0;

                while (isConnected)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);
                    if (readByteCount <= 0)
                    {
                        Console.WriteLine("Server disconnected.");
                        mClient.Close();
                        isConnected = false;
                        Console.WriteLine("Connection closed. Press any key to exit...");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    }
                    Console.WriteLine($"Received bytes: {readByteCount} - Message: {new string(buff)}");
                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error receiving data: {ioEx.Message}");
                isConnected = false;
                Console.WriteLine("Connection lost. Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (Exception excp)
            {
                Console.WriteLine($"Error in receiving data: {excp.Message}");
                isConnected = false;
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            finally
            {
                clientStreamReader?.Dispose();
            }
        }
    }
}
