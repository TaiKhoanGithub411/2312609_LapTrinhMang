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

namespace AsyncSocketClient_Form
{
    public partial class Client : Form
    {
        private AsyncSocketTCPClient client;
        public Client()
        {
            InitializeComponent();
            client = new AsyncSocketTCPClient();
            client.MessageReceived += Client_MessageReceived;

        }
        private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // Cập nhật txtMessRcv từ thread khác (cần Invoke)
            if (txtMessRcv.InvokeRequired)
            {
                txtMessRcv.Invoke((MethodInvoker)delegate
                {
                    txtMessRcv.AppendText($"Server: {e.Message}\r\n");
                });
            }
            else
            {
                txtMessRcv.AppendText($"Server: {e.Message}\r\n");
            }
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy địa chỉ IP và Port từ TextBox
                string ipAddress = txtIpAddress.Text;
                string port = txtPort.Text;
                // Set IP Address và Port
                if (!client.SetServerIPAddress(ipAddress))
                {
                    MessageBox.Show("Địa chỉ IP không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!client.SetPortNumber(port))
                {
                    MessageBox.Show("Port không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kết nối tới server
                await client.ConnectToServer();

                MessageBox.Show("Đã kết nối tới server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tin nhắn từ txtMessSend
                string message = txtMessSend.Text;

                if (string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("Vui lòng nhập tin nhắn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gửi tin nhắn lên server
                await client.SendToServer(message);

                // Xóa nội dung sau khi gửi
                txtMessSend.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi tin nhắn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
