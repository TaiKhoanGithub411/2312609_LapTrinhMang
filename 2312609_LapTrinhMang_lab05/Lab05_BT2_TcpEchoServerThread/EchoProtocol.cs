using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Lab05_BT2_TcpEchoServerThread
{
    public class EchoProtocol:IProtocol
    {
        public const int BUFSIZE = 32;
        private Socket clnSock;
        private ILogger logger;
        public EchoProtocol(Socket clnSock, ILogger logger)
        {
            this.clnSock = clnSock;
            this.logger = logger;
        }
        public void handleclient()
        {
            ArrayList entry = new ArrayList();
            entry.Add("Clietn address and port = " + clnSock.RemoteEndPoint);
            entry.Add("Thread = " + Thread.CurrentThread.GetHashCode());
            try
            {
                int recvMsgSize;
                int totalBytesEchoed = 0;
                byte[] rcvBuffer = new byte[BUFSIZE];
                try
                {
                    while((recvMsgSize=clnSock.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None))>0)
                    {
                        clnSock.Send(rcvBuffer, 0, recvMsgSize, SocketFlags.None);
                        totalBytesEchoed += recvMsgSize;
                    }
                }
                catch(SocketException se)
                {
                    entry.Add(se.ErrorCode + ":" + se.Message);
                }
                entry.Add("Total bytes echoed = " + totalBytesEchoed);
            }
            catch (SocketException se) 
            {
                entry.Add(se.ErrorCode + ":" + se.Message);
            }
            clnSock.Close();
            logger.writeEntry(entry);
        }
    }
}
