using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class RoomsData : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";

        public RoomsData()
        {
            InitializeComponent();
            LoadRoomsData();
            LoadDaysData();
            UpdateStatus();
            updateRoomsstudent();
        }

        // Cập nhật trạng thái phòng (Thiếu hoặc Đủ)
        private void UpdateStatus()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    string updateQuery1 = "UPDATE Phong SET TinhTrang = N'Thiếu' WHERE SoSV < SoSVToiDa";
                    using (SqlCommand cmd = new SqlCommand(updateQuery1, connect))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string updateQuery2 = "UPDATE Phong SET TinhTrang = N'Đủ' WHERE SoSV = SoSVToiDa";
                    using (SqlCommand cmd = new SqlCommand(updateQuery2, connect))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    LoadRoomsData(); // Cập nhật lại danh sách phòng sau khi cập nhật trạng thái
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật số lượng sinh viên cho phòng
        private void updateRoomsstudent()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    string updateQuery = "UPDATE Phong SET SoSV = (SELECT COUNT(*) FROM SinhVien WHERE SinhVien.MaPhong = Phong.MaPhong)";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    LoadRoomsData(); // Cập nhật lại danh sách phòng sau khi cập nhật số lượng sinh viên
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật số lượng sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tải danh sách phòng
        private void LoadRoomsData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = @"SELECT Phong.ID, Phong.MaPhong, Phong.TenPhong, Phong.SoSV, Phong.SoSVToiDa, 
                                    Phong.TinhTrang, Phong.LoaiPhong, Phong.MaDay, Day.TenDay AS TenDay 
                             FROM Phong
                             INNER JOIN Day ON Phong.MaDay = Day.MaDay";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvRoomData.DataSource = table; // Gán dữ liệu vào DataGridView
                    dgvRoomData.Columns["MaDay"].Visible = false;
                    dgvRoomData.Columns["ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tải dữ liệu các dãy phòng
        private void LoadDaysData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT MaDay, TenDay FROM Day";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbbRoomArea.DisplayMember = "TenDay";
                    cbbRoomArea.ValueMember = "MaDay";
                    cbbRoomArea.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu dãy phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm phòng mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maphong = txtRoomCode.Text.Trim();
            string tenphong = txtRoomName.Text.Trim();
            string max = txtMaxQuantity.Text.Trim();
            string roomtype = cbbRoomType.SelectedItem?.ToString();
            string roomarea = cbbRoomArea.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(maphong) || string.IsNullOrEmpty(tenphong) || string.IsNullOrEmpty(max)
                || string.IsNullOrEmpty(roomtype) || string.IsNullOrEmpty(roomarea))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string checkQuery = "SELECT COUNT(*) FROM Phong WHERE MaPhong = @maphong";
                        using (SqlCommand check = new SqlCommand(checkQuery, connect))
                        {
                            check.Parameters.AddWithValue("@maphong", maphong);

                            int roomExists = (int)check.ExecuteScalar();
                            if (roomExists > 0)
                            {
                                MessageBox.Show($"Mã phòng: {maphong} đã tồn tại. Vui lòng chọn mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string insertQuery = "INSERT INTO Phong(MaPhong, TenPhong, SoSV, SoSVToiDa, TinhTrang, LoaiPhong, MaDay) " +
                                             "VALUES(@maphong, @tenphong, 0, @sosvtoida, N'Thiếu', @loai, @maday)"; // SoSV = 0 khi thêm phòng mới

                        using (SqlCommand cmd = new SqlCommand(insertQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@maphong", maphong);
                            cmd.Parameters.AddWithValue("@tenphong", tenphong);
                            cmd.Parameters.AddWithValue("@sosvtoida", max);
                            cmd.Parameters.AddWithValue("@loai", roomtype);
                            cmd.Parameters.AddWithValue("@maday", roomarea);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRoomsData();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm phòng. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Cập nhật phòng
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string maphong = txtRoomCode.Text.Trim();
            string tenphong = txtRoomName.Text.Trim();
            string max = txtMaxQuantity.Text.Trim();
            string roomtype = cbbRoomType.SelectedItem?.ToString();
            string roomarea = cbbRoomArea.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(maphong) || string.IsNullOrEmpty(tenphong) || string.IsNullOrEmpty(max)
                || string.IsNullOrEmpty(roomtype) || string.IsNullOrEmpty(roomarea))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string updateQuery = @"UPDATE Phong
                                       SET 
                                           TenPhong = @tenphong,
                                           SoSVToiDa = @sosvtoida,
                                           LoaiPhong = @loai,
                                           MaDay = @maday
                                       WHERE MaPhong = @maphong";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@maphong", maphong);
                            cmd.Parameters.AddWithValue("@tenphong", tenphong);
                            cmd.Parameters.AddWithValue("@sosvtoida", max);
                            cmd.Parameters.AddWithValue("@loai", roomtype);
                            cmd.Parameters.AddWithValue("@maday", roomarea);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRoomsData();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy phòng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Xóa phòng
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string deleteQuery = "DELETE FROM Phong WHERE MaPhong = @maphong";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@maphong", txtRoomCode.Text.Trim());
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRoomsData();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy phòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Xử lý sự kiện khi chọn một phòng từ DataGridView
        private int id;
        private void dgvRoomData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvRoomData.Rows[e.RowIndex];
                id = (int)row.Cells["ID"].Value;
                txtRoomCode.Text = row.Cells["MaPhong"].Value.ToString();
                txtRoomName.Text = row.Cells["TenPhong"].Value.ToString();
                txtCurrQuantity.Text = row.Cells["SoSV"].Value.ToString();
                txtMaxQuantity.Text = row.Cells["SoSVToiDa"].Value.ToString();
                txtStatus.Text = row.Cells["TinhTrang"].Value.ToString();
                cbbRoomType.SelectedItem = row.Cells["LoaiPhong"].Value.ToString();
                cbbRoomArea.SelectedValue = row.Cells["MaDay"].Value.ToString();
            }
        }

        // Cập nhật dữ liệu các dãy phòng khi ComboBox được mở
        private void cbbRoomArea_DropDown(object sender, EventArgs e)
        {
            LoadDaysData();
        }

        private void txtCurrQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReLoad_Click_1(object sender, EventArgs e)
        {
            LoadRoomsData();
            LoadDaysData();
            UpdateStatus();
            updateRoomsstudent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string searchColumn = "";

            if (rdbRoomCode.Checked)
            {
                searchColumn = "MaPhong";
            }
            else if (rdbRoomName.Checked)
            {
                searchColumn = "TenPhong";
            }
            else
            {
                searchColumn = "TenDay";
            }

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo câu truy vấn động
            // Tạo câu truy vấn động, kết hợp bảng Phong và Day
            string query = @"
                    SELECT Phong.*, Day.TenDay
                    FROM Phong
                    JOIN Day ON Phong.MaDay = Day.MaDay
                    WHERE Day.TenDay LIKE @search
            ";

            using (SqlConnection connect = new SqlConnection(conn))
            {
                try
                {
                    connect.Open();
                    SqlCommand sql = new SqlCommand(query, connect);
                    sql.Parameters.AddWithValue("@search", searchColumn == "TenDay" ? search : $"%{search}%");

                    SqlDataAdapter adapter = new SqlDataAdapter(sql);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvRoomData.DataSource = table;
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

    }
}
