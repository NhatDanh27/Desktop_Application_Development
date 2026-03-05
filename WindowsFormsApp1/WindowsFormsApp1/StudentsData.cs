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

namespace WindowsFormsApp1
{
    public partial class StudentsData : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public StudentsData()
        {
            InitializeComponent();
            LoadStudentData();
            LoadRoomData();
        
        }

        private void LoadStudentData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT * FROM SinhVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvStudentData.DataSource = table;
                    dgvStudentData.Columns["ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoomData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT MaPhong, TenPhong FROM Phong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbbRoomCode.DisplayMember = "MaPhong";
                    cbbRoomCode.ValueMember = "MaPhong";
                    cbbRoomCode.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string masv = txtStudentCode.Text.Trim();
            string tensv = txtStudentName.Text.Trim();
            string gender = cbbGender.SelectedItem?.ToString();
            DateTime dop = dtpDOB.Value;
            string que = txtHometown.Text.Trim();
            string khoa = cbbKhoa.SelectedItem?.ToString();
            string lop = txtClass.Text.Trim();
            string maphong = cbbRoomCode.SelectedValue?.ToString();
            DateTime dor = dateTimePicker1.Value;

            if (string.IsNullOrEmpty(masv) || string.IsNullOrEmpty(tensv) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(que)
                || string.IsNullOrEmpty(khoa) || string.IsNullOrEmpty(lop) || string.IsNullOrEmpty(maphong))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    // Kiểm tra số lượng sinh viên trong phòng
                    string roomCapacityQuery = "SELECT COUNT(*) FROM SinhVien WHERE MaPhong = @maPhong";
                    string roomLimitQuery = "SELECT SoSVToiDa FROM Phong WHERE MaPhong = @maPhong";

                    int currentCount = 0, maxCapacity = 0;

                    using (SqlCommand cmd = new SqlCommand(roomCapacityQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@maPhong", maphong);
                        currentCount = (int)cmd.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand(roomLimitQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@maPhong", maphong);
                        maxCapacity = (int)cmd.ExecuteScalar();
                    }

                    if (currentCount >= maxCapacity)
                    {
                        MessageBox.Show("Phòng này đã đầy sinh viên, vui lòng chọn phòng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra mã sinh viên đã tồn tại
                    string checkQuery = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @masv";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@masv", masv);
                        int studentExists = (int)cmd.ExecuteScalar();
                        if (studentExists > 0)
                        {
                            MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Thêm sinh viên
                    string insertQuery = "INSERT INTO SinhVien (MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, Khoa, Lop, MaPhong,NgayDK) " +
                                         "VALUES (@maSV, @tenSV, @gioiTinh, @ngaySinh, @queQuan, @khoa, @lop, @maPhong,@ngaydangky)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@maSV", masv);
                        cmd.Parameters.AddWithValue("@tenSV", tensv);
                        cmd.Parameters.AddWithValue("@gioiTinh", gender);
                        cmd.Parameters.AddWithValue("@ngaySinh", dop);
                        cmd.Parameters.AddWithValue("@queQuan", que);
                        cmd.Parameters.AddWithValue("@khoa", khoa);
                        cmd.Parameters.AddWithValue("@lop", lop);
                        cmd.Parameters.AddWithValue("@maPhong", maphong);
                        cmd.Parameters.AddWithValue("@ngaydangky", dor);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudentData();
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void cbbRoomCode_DropDown(object sender, EventArgs e)
        {
            LoadRoomData();
        
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string masv = txtStudentCode.Text.Trim();
            string tensv = txtStudentName.Text.Trim();
            string gender = cbbGender.SelectedItem?.ToString();
            DateTime dop = dtpDOB.Value;
            string que = txtHometown.Text.Trim();
            string khoa = cbbKhoa.SelectedItem?.ToString();
            string lop = txtClass.Text.Trim();
            string maphong = cbbRoomCode.SelectedValue?.ToString();


            if (string.IsNullOrEmpty(masv) || string.IsNullOrEmpty(tensv) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(que)
                || string.IsNullOrEmpty(khoa) || string.IsNullOrEmpty(lop) || string.IsNullOrEmpty(maphong))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    // Kiểm tra số lượng sinh viên trong phòng mới nếu thay đổi phòng
                    string roomCapacityQuery = "SELECT COUNT(*) FROM SinhVien WHERE MaPhong = @maPhong";
                    string roomLimitQuery = "SELECT SoSVToiDa FROM Phong WHERE MaPhong = @maPhong";

                    int currentCount = 0, maxCapacity = 0;

                    using (SqlCommand cmd = new SqlCommand(roomCapacityQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@maPhong", maphong);
                        currentCount = (int)cmd.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand(roomLimitQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@maPhong", maphong);
                        maxCapacity = (int)cmd.ExecuteScalar();
                    }

                    if (currentCount >= maxCapacity)
                    {
                        MessageBox.Show("Phòng này đã đầy sinh viên, vui lòng chọn phòng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin sinh viên
                    string updateQuery = "UPDATE SinhVien SET MaSV = @maSV, TenSV = @tenSV, GioiTinh = @gioiTinh, NgaySinh = @ngaySinh, QueQuan = @queQuan, Khoa = @khoa, Lop = @lop, MaPhong = @maPhong WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@maSV", masv);
                        cmd.Parameters.AddWithValue("@tenSV", tensv);
                        cmd.Parameters.AddWithValue("@gioiTinh", gender);
                        cmd.Parameters.AddWithValue("@ngaySinh", dop);
                        cmd.Parameters.AddWithValue("@queQuan", que);
                        cmd.Parameters.AddWithValue("@khoa", khoa);
                        cmd.Parameters.AddWithValue("@lop", lop);
                        cmd.Parameters.AddWithValue("@maPhong", maphong);
                        cmd.Parameters.AddWithValue("@id", id);


                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudentData();
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id <= 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    // Xóa sinh viên
                    string deleteQuery = "DELETE FROM SinhVien WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudentData();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int id;
        private void dgvStudentData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudentData.Rows[e.RowIndex];

                id = (int)row.Cells["ID"].Value;
                txtStudentCode.Text = row.Cells["MaSV"].Value?.ToString();
                txtStudentName.Text = row.Cells["TenSV"].Value?.ToString();
                cbbGender.SelectedItem = row.Cells["GioiTinh"].Value?.ToString();
                dtpDOB.Value = row.Cells["NgaySinh"].Value != null ? Convert.ToDateTime(row.Cells["NgaySinh"].Value) : DateTime.Now;
                dateTimePicker1.Value = row.Cells["NgayDK"].Value != null ? Convert.ToDateTime(row.Cells["NgayDK"].Value) : DateTime.Now;
                txtHometown.Text = row.Cells["QueQuan"].Value?.ToString();
                cbbKhoa.SelectedItem = row.Cells["Khoa"].Value?.ToString();
                txtClass.Text = row.Cells["Lop"].Value?.ToString();
                cbbRoomCode.SelectedValue = row.Cells["MaPhong"].Value?.ToString();

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string searchColumn = "";

            if (rdbStudentCode.Checked)
            {
                searchColumn = "MaSV";
            }
            else if (rdbStudentName.Checked)
            {
                searchColumn = "TenSV";
            }
            else
            {
                searchColumn = "MaPhong";
            }

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo câu truy vấn động
            // Tạo câu truy vấn động, kết hợp bảng Phong và Day
            string query = $"SELECT * FROM SinhVien WHERE {searchColumn} LIKE @search";

            using (SqlConnection connect = new SqlConnection(conn))
            {
                try
                {
                    connect.Open();
                    SqlCommand sql = new SqlCommand(query, connect);
                    sql.Parameters.AddWithValue("@search", $"%{search}%");

                    SqlDataAdapter adapter = new SqlDataAdapter(sql);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvStudentData.DataSource = table;
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Lỗi truy vấn SQL: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi lại các phương thức để tải dữ liệu
                LoadStudentData();
                LoadRoomData();

                // Đặt lại các giá trị input trên form
                txtStudentCode.Clear();
                txtStudentName.Clear();
                cbbGender.SelectedIndex = -1;
                dtpDOB.Value = DateTime.Now;
                txtHometown.Clear();
                cbbKhoa.SelectedIndex = -1;
                txtClass.Clear();
                cbbRoomCode.SelectedIndex = -1;

                // Reset các trạng thái hoặc bộ lọc tìm kiếm
                txtSearch.Clear();
                rdbStudentCode.Checked = true;
                MessageBox.Show("Dữ liệu đã được làm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm mới dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
