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
    public partial class UserData : UserControl
    {
        private string conn = @"Data Source=NHAT-PC;Initial Catalog=QL;Integrated Security=True";
        public UserData()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using(SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT * FROM NguoiDung";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvUserData.DataSource = table;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cbbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || role == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                try
                {
                    using(SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tendangnhap";
                        using(SqlCommand check = new SqlCommand(checkQuery, connect))
                        {
                            check.Parameters.AddWithValue("@tendangnhap", username);

                            int userExists = (int)check.ExecuteScalar();
                            if (userExists > 0)
                            {
                                MessageBox.Show($"Người dùng: {username} đã tồn tại. Vui lòng chọn Username khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Dừng thực hiện nếu Username đã tồn tại
                            }
                        }

                        string insertQuery = "INSERT INTO NguoiDung(TenDangNhap, MatKhau, VaiTro) VALUES (@tendangnhap, @matkhau, @vaitro)";
                        using(SqlCommand cmd = new SqlCommand(insertQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@tendangnhap", username);
                            cmd.Parameters.AddWithValue("@matkhau", password);
                            cmd.Parameters.AddWithValue("@vaitro", role);

                            int rows = cmd.ExecuteNonQuery(); 
                            if (rows > 0)
                            {
                                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadUserData(); // Cập nhật lại danh sách người dùng
                                clearFields();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm người dùng. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int id;
        private void dgvUserData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dgvUserData.Rows[e.RowIndex];
                id = (int)row.Cells["ID"].Value;
                txtUserName.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtPassword.Text = row.Cells["MatKhau"].Value.ToString();
                cbbRole.SelectedItem = row.Cells["VaiTro"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cbbRole.SelectedItem?.ToString();

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || role == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn cập nhật người dùng '{username}' không?", "Thong Bao", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(conn))
                        {
                            connect.Open();

                            string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tendangnhap AND ID != @id";
                            using (SqlCommand check = new SqlCommand(checkQuery, connect))
                            {
                                check.Parameters.AddWithValue("@tendangnhap", username);
                                check.Parameters.AddWithValue("@id", id);

                                int userExists = (int)check.ExecuteScalar();
                                if (userExists > 0)
                                {
                                    MessageBox.Show(username + "đã tồn tại. Vui lòng chọn Username khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Dừng thực hiện nếu Username đã tồn tại
                                }
                            }

                            string updateQuery = "UPDATE NguoiDung SET TenDangNhap = @tendangnhap, MatKhau = @matkhau, VaiTro = @vaitro WHERE ID =@id";
                            using (SqlCommand update = new SqlCommand(updateQuery, connect))
                            {
                                update.Parameters.AddWithValue("@tendangnhap", username);
                                update.Parameters.AddWithValue("@matkhau", password);
                                update.Parameters.AddWithValue("@vaitro", role);
                                update.Parameters.AddWithValue("@id", id);

                                int count = update.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadUserData();
                                    clearFields();
                                }
                                else
                                {
                                    MessageBox.Show("Không thể cập nhật thông tin. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();

            if(string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{username}' không?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using(SqlConnection connect = new SqlConnection(conn))
                        {
                            connect.Open();

                            string deleteQuery = "DELETE FROM NguoiDung WHERE TenDangNhap = @tendangnhap";

                            using (SqlCommand delete = new SqlCommand(deleteQuery, connect))
                            {
                                delete.Parameters.AddWithValue("@tendangnhap", username);

                                int count = delete.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadUserData();
                                    clearFields();
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy người dùng để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string searchType = "";

            if (rdbUsername.Checked)
            {
                searchType = "TenDangNhap";
            }
            else
            {
                searchType = "VaiTro";
            }

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "";

            if(searchType == "TenDangNhap")
            {
                query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @search";
            }
            else
            {
                query = "SELECT * FROM NguoiDung WHERE VaiTro = @search";
            }

            using (SqlConnection connect = new SqlConnection(conn))
            {
                try
                {
                    connect.Open();
                    SqlCommand sql = new SqlCommand(query, connect);
                    sql.Parameters.AddWithValue("@search", search);

                    SqlDataAdapter adapter = new SqlDataAdapter(sql);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvUserData.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        public void clearFields()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtSearch.Text = "";
            cbbRole.SelectedIndex = -1;

            dgvUserData.DataSource = null;
            LoadUserData();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }
}
