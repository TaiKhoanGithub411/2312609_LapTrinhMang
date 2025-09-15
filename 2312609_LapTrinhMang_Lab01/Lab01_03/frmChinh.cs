using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab01_03
{
	public partial class frmChinh : Form
	{
		public frmChinh()
		{
			InitializeComponent();
		}
        // Hàm hiển thị thông tin IP của máy cục bộ
        private void ShowLocalIPInfo()
        {
            try
            {
                string hostName = Dns.GetHostName();
                txtKQ.AppendText("Thông tin IP của máy cục bộ:\r\n");
                txtKQ.AppendText($"Tên máy: {hostName}\r\n");

                IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                txtKQ.AppendText("Các địa chỉ IP:\r\n");

                foreach (IPAddress addr in addresses)
                {
                    txtKQ.AppendText("- " + addr.ToString() + "\r\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lấy thông tin IP. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnPhanGiai_Click(object sender, EventArgs e)
		{
            string tenmien = txtTenMien.Text.Trim();

            // 1. Kiểm tra xem người dùng đã nhập tên miền chưa
            if (string.IsNullOrEmpty(tenmien))
            {
                MessageBox.Show("Vui lòng nhập tên miền cần phân giải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Biểu thức này kiểm tra các ký tự hợp lệ (a-z, 0-9, -) và cấu trúc của tên miền.
            string pattern = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";
            if (!Regex.IsMatch(tenmien, pattern))
            {
                MessageBox.Show("Tên miền chứa ký tự không hợp lệ hoặc có định dạng sai.\nVui lòng kiểm tra lại.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Thực hiện phân giải tên miền
                IPAddress[] addresses = Dns.GetHostAddresses(tenmien);

                txtKQ.Clear();
                txtKQ.AppendText($"Kết quả phân giải cho tên miền: {tenmien}\r\n");
                txtKQ.AppendText("Các địa chỉ IP được tìm thấy:\r\n");

                foreach (IPAddress addr in addresses)
                {
                    txtKQ.AppendText("- " + addr.ToString() + "\r\n");
                }
            }
            // 3. Bắt lỗi cụ thể khi không tìm thấy tên miền
            catch (SocketException)
            {
                MessageBox.Show($"Không tìm thấy máy chủ cho tên miền '{tenmien}'.\nĐây có thể là tên miền không tồn tại.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // 4. Bắt các lỗi chung khác
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
