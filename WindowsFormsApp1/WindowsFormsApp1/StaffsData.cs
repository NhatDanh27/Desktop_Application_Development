using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsApp1
{
    public partial class StaffsData : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public StaffsData()
        {
            InitializeComponent();
            LoadStaffData();
        }

        

        private void LoadStaffData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT * FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvStaffData.DataSource = table;
                    dgvStaffData.Columns["ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string manv = txtEmployeeCode.Text.Trim();
            string tennv = txtEmployeeName.Text.Trim();
            string gender = cbbGender.SelectedItem?.ToString();
            DateTime dob = dtpDOB.Value;
            string address = txtAddress.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();

            if (string.IsNullOrEmpty(manv) || string.IsNullOrEmpty(tennv) || string.IsNullOrEmpty(gender)
                || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @manv";

                        using (SqlCommand check = new SqlCommand(checkQuery, connect))
                        {
                            check.Parameters.AddWithValue("@manv", manv);

                            int staffExists = (int)check.ExecuteScalar();
                            if (staffExists > 0)
                            {
                                MessageBox.Show($"Mã nhân viên: {manv} đã tồn tại. Vui lòng chọn mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string insertQuery = "INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DiaChi, SoDienThoai)" +
                                                 "VALUES(@manv, @tennv, @gender, @dob, @address, @phone)";

                        using (SqlCommand insert = new SqlCommand(insertQuery, connect))
                        {
                            insert.Parameters.AddWithValue("@manv", manv);
                            insert.Parameters.AddWithValue("@tennv", tennv);
                            insert.Parameters.AddWithValue("@gender", gender);
                            insert.Parameters.AddWithValue("@dob", dob);
                            insert.Parameters.AddWithValue("@address", address);
                            insert.Parameters.AddWithValue("@phone", phone);

                            int count = insert.ExecuteNonQuery();
                            if (count > 0)
                            {
                                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadStaffData();
                                clearFields();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm nhân viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể thêm nhân viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int id;
        private void dgvStaffData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvStaffData.Rows[e.RowIndex];
                id = (int)row.Cells["ID"].Value;
                txtEmployeeCode.Text = row.Cells["MaNV"].Value.ToString();
                txtEmployeeName.Text = row.Cells["TenNV"].Value.ToString();
                cbbGender.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtAddress.Text = row.Cells["DiaChi"].Value.ToString();
                txtPhoneNumber.Text = row.Cells["SoDienThoai"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string manv = txtEmployeeCode.Text.Trim();
            string tennv = txtEmployeeName.Text.Trim();
            string gender = cbbGender.SelectedItem?.ToString();
            DateTime dob = dtpDOB.Value;
            string address = txtAddress.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();

            if (string.IsNullOrEmpty(manv) || string.IsNullOrEmpty(tennv) || string.IsNullOrEmpty(gender)
                || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn chỉnh sửa nhân viên {tennv} không?", "Thong Bao",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(conn))
                        {
                            connect.Open();

                            string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @manv AND ID != @id";

                            using (SqlCommand check = new SqlCommand(checkQuery, connect))
                            {
                                check.Parameters.AddWithValue("@manv", manv);
                                check.Parameters.AddWithValue("@id", id);

                                int staffExists = (int)check.ExecuteScalar();
                                if (staffExists > 0)
                                {
                                    MessageBox.Show($"Mã nhân viên: {manv} đã tồn tại. Vui lòng chọn mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            string updateQuery = "UPDATE NhanVien SET " +
                                                 "MaNV = @manv, " +
                                                 "TenNV = @tennv, " +
                                                 "GioiTinh = @gender, " +
                                                 "NgaySinh = @dob, " +
                                                 "DiaChi = @address, " +
                                                 "SoDienThoai = @phone " +
                                                 "WHERE ID = @id";

                            using (SqlCommand update = new SqlCommand(updateQuery, connect))
                            {
                                update.Parameters.AddWithValue("@manv", manv);
                                update.Parameters.AddWithValue("@tennv", tennv);
                                update.Parameters.AddWithValue("@gender", gender);
                                update.Parameters.AddWithValue("@dob", dob);
                                update.Parameters.AddWithValue("@address", address);
                                update.Parameters.AddWithValue("@phone", phone);
                                update.Parameters.AddWithValue("@id", id);

                                int count = update.ExecuteNonQuery();
                                if (count > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadStaffData();
                                    clearFields();
                                }
                                else
                                {
                                    MessageBox.Show("Không thể chỉnh sửa thông tin. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch
                    {
                         MessageBox.Show("Không thể sửa nhân viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string manv = txtEmployeeCode.Text.Trim();

            if (string.IsNullOrEmpty(manv))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên có mã ('{manv}') không?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(conn))
                        {
                            connect.Open();

                            string deleteQuery = "DELETE FROM NhanVien WHERE MaNV = @manv";

                            using (SqlCommand delete = new SqlCommand(deleteQuery, connect))
                            {
                                delete.Parameters.AddWithValue("@manv", manv);
                                

                                int count = delete.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadStaffData();
                                    clearFields();
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy nhân viên để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void clearFields()
        {
            txtEmployeeCode.Text = "";
            txtEmployeeName.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtSearch.Text = "";
            cbbGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            rdbEmployeeCode.Checked = false;
            rdbEmployeeName.Checked = false;

            dgvStaffData.DataSource = null;
            LoadStaffData();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string searchType = "";

            if (rdbEmployeeCode.Checked)
            {
                searchType = "MaNV";
            }
            else
            {
                searchType = "TenNV";
            }

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "";
            if(searchType == "MaNV")
            {
                query = "SELECT * FROM NhanVien WHERE MaNV = @search";
            }
            else
            {
                query = "SELECT * FROM NhanVien WHERE TenNV = @search";
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

                    dgvStaffData.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
    }
}
