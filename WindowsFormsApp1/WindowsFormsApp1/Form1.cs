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
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private string GetRole(string username)
        {
            string role = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Câu truy vấn để lấy vai trò người dùng
                    string query = "SELECT VaiTro FROM NguoiDung WHERE TenDangNhap = @username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return role;
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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập cả tên người dùng và mật khẩu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tendangnhap AND MatKhau = @matkhau";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@tendangnhap", username);
                        cmd.Parameters.AddWithValue("@matkhau", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 1)
                        {
                            frm_adminMain main = new frm_adminMain();
                            main.Show();
                            string role = GetRole(username);
                            main.SetUserInfo(username, role);
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Tên người dùng hoặc mật khẩu không hợp lệ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
