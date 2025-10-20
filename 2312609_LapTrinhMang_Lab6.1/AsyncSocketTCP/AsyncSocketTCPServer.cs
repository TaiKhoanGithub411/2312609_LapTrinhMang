using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;

namespace AsyncSocketTCP
{
    public class AsyncSocketTCPServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTcpListener;
        List<TcpClient> mClient;
        public bool KeepRunning {  get; set; }
        public AsyncSocketTCPServer()
        {
            mClient = new List<TcpClient>();
        }
        public async void StartListeningForIncomingConnection(IPAddress ipaddr=null, int port=9001)
        {
            if(ipaddr==null)
            {
                ipaddr = IPAddress.Any;
            }    
            if(port<=0)
            {
                port = 9001;
            }
            mIP = ipaddr;
            mPort = port;
            System.Diagnostics.Debug.WriteLine(string.Format("IP Address: {0} - Port: {1}", mIP.ToString(), mPort));
            mTcpListener=new TcpListener(mIP, mPort);
            try
            {
                mTcpListener.Start();
                KeepRunning = true;
                while(KeepRunning)
                {
                    var returnedByAccept = await mTcpListener.AcceptTcpClientAsync();
                    mClient.Add(returnedByAccept);
                    Debug.WriteLine(string.Format("Client connected susscessfully, count: {0} - {1}",
                        mClient.Count, returnedByAccept.Client.RemoteEndPoint));
                    TakeCareOfTCPClient(returnedByAccept);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
        private void RemoveClient(TcpClient paramClient)
        {
            if(mClient.Contains(paramClient))
            {
                mClient.Remove(paramClient);
                Debug.WriteLine(String.Format("Client removed, count: {0}", mClient.Count));
            }
        }
        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;
            try
            {
                stream =paramClient.GetStream();
                reader = new StreamReader(stream);
                char[] buff = new char[64];
                while(KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");
                    int nRet=await reader.ReadAsync(buff, 0, buff.Length);
                    System.Diagnostics.Debug.WriteLine("Returned: " + nRet);
                    if(nRet == 0)
                    {
                        RemoveClient(paramClient);
                        System.Diagnostics.Debug.WriteLine("Socket disconnected");
                        break;
                    }
                }
                string receivedText=new string(buff);
                System.Diagnostics.Debug.WriteLine("*** RECEIVED: "+ receivedText);
                Array.Clear(buff, 0, buff.Length);
            }
            catch(Exception e)
            {
                RemoveClient(paramClient);
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
        public async void SendToAll(string leMessage)
        {
            if (String.IsNullOrEmpty(leMessage))
            {
                return;
            }
            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage);
                foreach (TcpClient c in mClient)
                {
                    await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                }
            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }
        public void StopServer()
        {
            try
            {
                if(mTcpListener!=null)
                {
                    mTcpListener.Stop();
                }    
                foreach(TcpClient c in mClient)
                {
                    c.Close();
                }    
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
