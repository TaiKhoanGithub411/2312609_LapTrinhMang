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
        private bool isListening = false;

        // Events
        public EventHandler<ClientConnectedEventArgs> ClientConnectedEvent;
        public EventHandler<ClientDisconnectedEventArgs> ClientDisconnectedEvent;
        public EventHandler<MessageReceivedEventArgs> MessageReceivedEvent;

        public bool KeepRunning { get; set; }

        public AsyncSocketTCPServer()
        {
            mClient = new List<TcpClient>();
        }

        public async void StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 9001)
        {
            if (isListening)
            {
                Debug.WriteLine("Server is already listening!");
                return;
            }

            if (ipaddr == null)
            {
                ipaddr = IPAddress.Any;
            }
            if (port <= 0)
            {
                port = 9001;
            }
            mIP = ipaddr;
            mPort = port;
            System.Diagnostics.Debug.WriteLine(string.Format("IP Address: {0} - Port: {1}", mIP.ToString(), mPort));
            mTcpListener = new TcpListener(mIP, mPort);

            try
            {
                mTcpListener.Start();
                isListening = true;
                KeepRunning = true;
                Debug.WriteLine("Server started successfully");

                while (KeepRunning)
                {
                    var returnedByAccept = await mTcpListener.AcceptTcpClientAsync();
                    mClient.Add(returnedByAccept);

                    string clientEndPoint = returnedByAccept.Client.RemoteEndPoint.ToString();
                    OnClientConnectedEvent(new ClientConnectedEventArgs(clientEndPoint));

                    Debug.WriteLine(string.Format("Client connected successfully, count: {0} - {1}",
                        mClient.Count, clientEndPoint));

                    TakeCareOfTCPClient(returnedByAccept);
                }
            }
            catch (SocketException sockEx)
            {
                Debug.WriteLine($"Socket error: {sockEx.Message}");
                isListening = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                isListening = false;
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClient.Contains(paramClient))
            {
                string clientEndPoint = "Unknown";
                try
                {
                    if (paramClient.Client != null && paramClient.Client.RemoteEndPoint != null)
                    {
                        clientEndPoint = paramClient.Client.RemoteEndPoint.ToString();
                    }
                }
                catch { }

                mClient.Remove(paramClient);
                Debug.WriteLine(String.Format("Client removed, count: {0}", mClient.Count));

                // Raise ClientDisconnectedEvent
                OnClientDisconnectedEvent(new ClientDisconnectedEventArgs(clientEndPoint, mClient.Count));
            }
        }

        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;
            string clientEndPoint = paramClient.Client.RemoteEndPoint.ToString();

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);
                char[] buff = new char[64];

                while (KeepRunning && paramClient.Connected)
                {
                    Debug.WriteLine("*** Ready to read");
                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);
                    System.Diagnostics.Debug.WriteLine("Returned: " + nRet);

                    if (nRet == 0)
                    {
                        RemoveClient(paramClient);
                        System.Diagnostics.Debug.WriteLine("Socket disconnected");
                        break;
                    }

                    string receivedText = new string(buff).TrimEnd('\0');
                    System.Diagnostics.Debug.WriteLine("*** RECEIVED: " + receivedText);

                    // Raise MessageReceivedEvent
                    OnMessageReceivedEvent(new MessageReceivedEventArgs(receivedText, clientEndPoint));

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (ObjectDisposedException objEx)
            {
                Debug.WriteLine($"Client disconnected (stream disposed): {objEx.Message}");
                RemoveClient(paramClient);
            }
            catch (IOException ioEx)
            {
                Debug.WriteLine($"IO error reading from client: {ioEx.Message}");
                RemoveClient(paramClient);
            }
            catch (Exception e)
            {
                RemoveClient(paramClient);
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                reader?.Dispose();
                paramClient?.Close();
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
                foreach (TcpClient c in mClient.ToList())
                {
                    try
                    {
                        if (c.Connected)
                        {
                            await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error sending to client: {ex.Message}");
                        RemoveClient(c);
                    }
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
                KeepRunning = false;
                isListening = false;

                if (mTcpListener != null)
                {
                    mTcpListener.Stop();
                }
                foreach (TcpClient c in mClient.ToList())
                {
                    c.Close();
                }
                mClient.Clear();
                Debug.WriteLine("Server stopped");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        // Event raising methods
        protected virtual void OnClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = ClientConnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClientDisconnectedEvent(ClientDisconnectedEventArgs e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = ClientDisconnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMessageReceivedEvent(MessageReceivedEventArgs e)
        {
            EventHandler<MessageReceivedEventArgs> handler = MessageReceivedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
