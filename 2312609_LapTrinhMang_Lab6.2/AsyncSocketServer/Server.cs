using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncSocketTCP;

namespace AsyncSocketServer
{
    public partial class Server : Form
    {
        AsyncSocketTCPServer mServer;

        public Server()
        {
            InitializeComponent();
            mServer = new AsyncSocketTCPServer();
            mServer.ClientConnectedEvent += HandleClientConnected;
            mServer.ClientDisconnectedEvent += HandleClientDisconnected;
            mServer.MessageReceivedEvent += HandleMessageReceived;
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs e)
        {
            // Invoke để cập nhật UI từ background thread
            if (txtClientInfo.InvokeRequired)
            {
                txtClientInfo.Invoke(new Action(() =>
                {
                    txtClientInfo.AppendText(string.Format("{0} - New client connected - {1}\r\n",
                        DateTime.Now, e.NewClient));
                }));
            }
            else
            {
                txtClientInfo.AppendText(string.Format("{0} - New client connected - {1}\r\n",
                    DateTime.Now, e.NewClient));
            }
        }

        void HandleClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            // Invoke để cập nhật UI từ background thread
            if (txtClientInfo.InvokeRequired)
            {
                txtClientInfo.Invoke(new Action(() =>
                {
                    txtClientInfo.AppendText(string.Format("{0} - Client disconnected - {1} - Remaining clients: {2}\r\n",
                        DateTime.Now, e.DisconnectedClient, e.RemainingClients));
                }));
            }
            else
            {
                txtClientInfo.AppendText(string.Format("{0} - Client disconnected - {1} - Remaining clients: {2}\r\n",
                    DateTime.Now, e.DisconnectedClient, e.RemainingClients));
            }
        }

        void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // Invoke để cập nhật UI từ background thread
            if (txtMessageRcv.InvokeRequired)
            {
                txtMessageRcv.Invoke(new Action(() =>
                {
                    txtMessageRcv.AppendText(string.Format("{0} - From {1}: {2}\r\n",
                        DateTime.Now, e.ClientInfo, e.Message));
                }));
            }
            else
            {
                txtMessageRcv.AppendText(string.Format("{0} - From {1}: {2}\r\n",
                    DateTime.Now, e.ClientInfo, e.Message));
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(txtMessageSend.Text.Trim());
            txtMessageSend.Clear();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
            txtClientInfo.AppendText(string.Format("{0} - Server stopped\r\n", DateTime.Now));
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }
    }
}
