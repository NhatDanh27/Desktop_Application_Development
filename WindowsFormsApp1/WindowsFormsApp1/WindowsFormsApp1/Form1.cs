using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string conn = @"Data Source=NHAT-PC;Initial Catalog=QL;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void lnllkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_dangky frm_Dangky = new frm_dangky();
            frm_Dangky.Show();
            this.Hide();
        }

        private void chkpass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkpass.Checked ? '\0' : '*';
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập cả tên người dùng và mật khẩu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    string query = "SELECT COUNT(1) FROM NguoiDung WHERE TenDangNhap = @tendangnhap AND MatKhau = @matkhau";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@tendangnhap", username);
                        cmd.Parameters.AddWithValue("@matkhau", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 1)
                        {
                            frm_adminMain main = new frm_adminMain();
                            main.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Tên người dùng hoặc mật khẩu không hợp lệ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
