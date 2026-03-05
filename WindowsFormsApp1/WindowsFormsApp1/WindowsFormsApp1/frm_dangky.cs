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
    public partial class frm_dangky : Form
    {
        private string conn = @"Data Source=NHAT-PC;Initial Catalog=QL;Integrated Security=True";
        public frm_dangky()
        {
            InitializeComponent();
        }

        private void lnllkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegistrt_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmpassword = txtConfirmpass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmpassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(password !=  confirmpassword)
            {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string checkU = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tendangnhap";

                        using (SqlCommand check = new SqlCommand(checkU, connect))
                        {
                            check.Parameters.AddWithValue("@tendangnhap", username);
                            int userCount = (int)check.ExecuteScalar();

                            if (userCount > 0)
                            {
                                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }


                        string insertQuery = "INSERT INTO NguoiDung(TenDangNhap, MatKhau, VaiTro) VALUES (@tendangnhap, @matkhau, @vaitro)";
                        using(SqlCommand cmd = new SqlCommand(insertQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@tendangnhap", username);
                            cmd.Parameters.AddWithValue("@matkhau", password);
                            cmd.Parameters.AddWithValue("@vaitro", "Staff");

                            int result = cmd.ExecuteNonQuery();

                            if(result > 0)
                            {
                                MessageBox.Show("Dang Ky Thanh Cong", "Thong Bao",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Đăng ký không thành công, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
