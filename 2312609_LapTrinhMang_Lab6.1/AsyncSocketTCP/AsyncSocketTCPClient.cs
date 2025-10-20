using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class AsyncSocketTCPClient
    {
        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;
        public IPAddress ServerIPAddress 
        {
            get { return mServerIPAddress; } 
        }
        public int ServerPort
        {
            get { return mServerPort; }
        }
        public AsyncSocketTCPClient()
        {
            mClient=null;
            mServerPort = -1;
            mServerIPAddress=null;
        }
        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ip = null;
            if(!IPAddress.TryParse(_IPAddressServer, out ip))
            {
                Console.WriteLine("Onvalis IP address.");
                return false;
            }
            mServerIPAddress = ip;
            return true;
        }
        public bool SetPortNumber(string _ServerPort)
        {
            int pnum = 0;
            if(!int.TryParse(_ServerPort, out pnum))
            {
                Console.WriteLine("Invalid port number.");
            }
            if(pnum<=0||pnum>65535)
            {
                Console.WriteLine("Port number must be between 0 and 65535");
                return false;
            }
            mServerPort= pnum;
            return true;
        }
        public void CloseAndDisconnect()
        {
            if(mClient!=null)
            {
                if(mClient.Connected)
                    mClient.Close();
            }
        }
        public async Task ConnectToServer()
        {
            if(mClient==null)
                mClient=new TcpClient();
            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                Console.WriteLine(string.Format("Connected to server IP/Port: {0} / {1}",
                    mServerIPAddress, mServerPort));
                //await ReDateAsync(mClient);
            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        public async Task SendToServer(string strInput)
        {
            if(string.IsNullOrEmpty(strInput))
            {
                Console.WriteLine("Empty message, no data send.");
                return;
            }
            if(mClient!=null)
            {
                if (mClient.Connected)
                {
                    StreamWriter streamWriter=new StreamWriter(mClient.GetStream());
                    streamWriter.AutoFlush=true;
                    await streamWriter.WriteAsync(strInput);
                    Console.WriteLine("Data sent ...");
                }
            }
        }
        private async Task ReadDataAsync(TcpClient mClient)
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(mClient.GetStream());
                char[] buff = new char[64];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);

                    if (readByteCount <= 0)
                    {
                        Console.WriteLine("Disconnected from server.");
                        mClient.Close();
                        break;
                    }

                    Console.WriteLine(string.Format("Received bytes: {0} – Message: {1}",
                        readByteCount, new string(buff)));

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                throw;
            }
        }

    }
}
