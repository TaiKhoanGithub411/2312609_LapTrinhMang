namespace Lab01_03
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTenMien = new System.Windows.Forms.TextBox();
			this.btnPhanGiai = new System.Windows.Forms.Button();
			this.txtKQ = new System.Windows.Forms.TextBox();
			this.btnThoat = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(22, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nhập thông tin tên miền:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 85);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 14);
			this.label2.TabIndex = 1;
			this.label2.Text = "Thông tin giao thức IP";
			// 
			// txtTenMien
			// 
			this.txtTenMien.Location = new System.Drawing.Point(155, 24);
			this.txtTenMien.Name = "txtTenMien";
			this.txtTenMien.Size = new System.Drawing.Size(255, 20);
			this.txtTenMien.TabIndex = 2;
			// 
			// btnPhanGiai
			// 
			this.btnPhanGiai.Location = new System.Drawing.Point(198, 50);
			this.btnPhanGiai.Name = "btnPhanGiai";
			this.btnPhanGiai.Size = new System.Drawing.Size(75, 23);
			this.btnPhanGiai.TabIndex = 3;
			this.btnPhanGiai.Text = "Phân giải";
			this.btnPhanGiai.UseVisualStyleBackColor = true;
			this.btnPhanGiai.Click += new System.EventHandler(this.btnPhanGiai_Click);
			// 
			// txtKQ
			// 
			this.txtKQ.Location = new System.Drawing.Point(26, 102);
			this.txtKQ.Multiline = true;
			this.txtKQ.Name = "txtKQ";
			this.txtKQ.ReadOnly = true;
			this.txtKQ.Size = new System.Drawing.Size(384, 130);
			this.txtKQ.TabIndex = 4;
			// 
			// btnThoat
			// 
			this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnThoat.ForeColor = System.Drawing.Color.Red;
			this.btnThoat.Location = new System.Drawing.Point(292, 50);
			this.btnThoat.Name = "btnThoat";
			this.btnThoat.Size = new System.Drawing.Size(75, 23);
			this.btnThoat.TabIndex = 3;
			this.btnThoat.Text = "Thoát";
			this.btnThoat.UseVisualStyleBackColor = true;
			this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(438, 244);
			this.Controls.Add(this.txtKQ);
			this.Controls.Add(this.btnThoat);
			this.Controls.Add(this.btnPhanGiai);
			this.Controls.Add(this.txtTenMien);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form1";
			this.Text = "Phân giải thông tin IP";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtTenMien;
		private System.Windows.Forms.Button btnPhanGiai;
		private System.Windows.Forms.TextBox txtKQ;
		private System.Windows.Forms.Button btnThoat;
	}
}

