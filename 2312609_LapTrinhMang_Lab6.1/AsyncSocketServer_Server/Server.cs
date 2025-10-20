﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncSocketTCP;

namespace AsyncSocketServer_Server
{
    public partial class Server : Form
    {
        AsyncSocketTCPServer mServer;
        public Server()
        {
            InitializeComponent();
            mServer=new AsyncSocketTCPServer();
            
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(txtMessage.Text.Trim());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }
    }
}
