using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class RoomsData : UserControl
    {
        private string conn = @"Data Source=NHAT-PC;Initial Catalog=QL;Integrated Security=True";
        public RoomsData()
        {
            InitializeComponent();
            LoadRoomsData();
            LoadDaysData();
            UpdateStatus();
            updateRoomsstudent();
        }


        private void UpdateStatus()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    string updateQuery = "UPDATE Phong SET TinhTrang = N'Thiếu' WHERE SoSV <SoSVToiDa";

                    using (SqlCommand cmd = new SqlCommand(updateQuery,connect))
                    {
                        int affect = cmd.ExecuteNonQuery();
                        if (affect > 0)
                        {
                            //code
                        }
                    }

                    string updateQuery2 = "UPDATE Phong SET TinhTrang = N'Đủ' WHERE SoSV = SoSVToiDa";
                    using (SqlCommand cmd = new SqlCommand(updateQuery2, connect))
                    {
                        int affect2 = cmd.ExecuteNonQuery();
                        if (affect2 > 0)
                        {
                        }
                        //code
                    }

                    LoadRoomsData();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateRoomsstudent()
        {
            try
            {
                using(SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string updateQuery = "UPDATE Phong SET SoSV = (SELECT COUNT(*) FROM SinhVien WHERE SinhVien.MaPhong = Phong.MaPhong)";

                    using(SqlCommand cmd = new SqlCommand(updateQuery,connect))
                    {
                        int affect = cmd.ExecuteNonQuery();
                        if(affect > 0)
                        {
                            //code
                        }
                    }
                    LoadRoomsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadRoomsData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    // Sử dụng JOIN để lấy tên dãy
                    string query = @"SELECT Phong.ID, Phong.MaPhong, Phong.TenPhong, Phong.SoSV, Phong.SoSVToiDa, 
                                    Phong.TinhTrang, Phong.LoaiPhong, Phong.MaDay, Day.TenDay AS  TenDay 
                             FROM Phong
                             INNER JOIN Day ON Phong.MaDay = Day.MaDay";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvRoomData.DataSource = table; // Gán dữ liệu vào DataGridView
                    dgvRoomData.Columns["MaDay"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                    // Gán dữ liệu vào ComboBox
                    cbbRoomArea.DisplayMember = "TenDay";  
                    cbbRoomArea.ValueMember = "MaDay";     
                    cbbRoomArea.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maphong = txtRoomCode.Text.Trim();
            string tenphong = txtRoomName.Text.Trim();
            string tinhtrang = txtStatus.Text.Trim();
            string max = txtMaxQuantity.Text.Trim();
            string current = txtCurrQuantity.Text.Trim();
            string roomtype = cbbRoomType.SelectedItem?.ToString();
            string roomarea = cbbRoomArea.SelectedValue?.ToString();

            if(string.IsNullOrEmpty(maphong) || string.IsNullOrEmpty(tenphong) || string.IsNullOrEmpty(tinhtrang)
                || string.IsNullOrEmpty(max) || string.IsNullOrEmpty(current) || string.IsNullOrEmpty(roomtype) || string.IsNullOrEmpty(roomarea)) 
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

                        string checkQuery = "SELECT COUNT(*) FROM Phong WHERE MaPhong = @maphong";

                        using (SqlCommand check = new SqlCommand(checkQuery, connect))
                        {
                            check.Parameters.AddWithValue("@maphong", maphong);

                            int roomExists = check.ExecuteNonQuery();
                            if (roomExists > 0)
                            {
                                MessageBox.Show($"Mã phòng: {maphong} đã tồn tại. Vui lòng chọn mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string insertQuery = "INSERT INTO Phong(MaPhong, TenPhong, SoSV, SoSVToiDa, TinhTrang, LoaiPhong, MaDay) " +
                                             "VALUES(@maphong, @tenphong, @sosv, @sosvtoida, @tinhtrang, @loai, @maday)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@maphong", maphong);
                            cmd.Parameters.AddWithValue("@tenphong", tenphong);
                            cmd.Parameters.AddWithValue("@sosv", current);
                            cmd.Parameters.AddWithValue("@sosvtoida", max);
                            cmd.Parameters.AddWithValue("@tinhtrang", tinhtrang);
                            cmd.Parameters.AddWithValue("@loai", roomtype);
                            cmd.Parameters.AddWithValue("@maday", roomarea);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Thêm dãy phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRoomsData(); // Cập nhật lại danh sách người dùng
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm dãy phòng. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }


                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateRoom()
        {
            string maphong = txtRoomCode.Text.Trim();
            string tenphong = txtRoomName.Text.Trim();
            string tinhtrang = txtStatus.Text.Trim();
            string max = txtMaxQuantity.Text.Trim();
            string current = txtCurrQuantity.Text.Trim();
            string roomtype = cbbRoomType.SelectedItem?.ToString();
            string roomarea = cbbRoomArea.SelectedValue?.ToString(); // Lấy MaDay từ ComboBox

            if (string.IsNullOrEmpty(maphong) || string.IsNullOrEmpty(tenphong) || string.IsNullOrEmpty(tinhtrang)
                || string.IsNullOrEmpty(max) || string.IsNullOrEmpty(current) || string.IsNullOrEmpty(roomtype) || string.IsNullOrEmpty(roomarea))
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

                        // Kiểm tra nếu MaDay tồn tại trong bảng Day
                        string checkDayQuery = "SELECT COUNT(*) FROM Day WHERE MaDay = @maday";
                        using (SqlCommand checkDayCmd = new SqlCommand(checkDayQuery, connect))
                        {
                            checkDayCmd.Parameters.AddWithValue("@maday", roomarea);
                            int dayExists = (int)checkDayCmd.ExecuteScalar();
                            if (dayExists == 0)
                            {
                                MessageBox.Show("Mã dãy không hợp lệ. Vui lòng chọn dãy hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Câu lệnh cập nhật
                        string updateQuery = @"UPDATE Phong
                                       SET 
                                           MaPhong = @maphong,
                                           TenPhong = @tenphong,
                                           SoSV = @sosv,
                                           SoSVToiDa = @sosvtoida,
                                           TinhTrang = @tinhtrang,
                                           LoaiPhong = @loai,
                                           MaDay = @maday
                                       WHERE ID = @id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@maphong", maphong);
                            cmd.Parameters.AddWithValue("@tenphong", tenphong);
                            cmd.Parameters.AddWithValue("@sosv", current);
                            cmd.Parameters.AddWithValue("@sosvtoida", max);
                            cmd.Parameters.AddWithValue("@tinhtrang", tinhtrang);
                            cmd.Parameters.AddWithValue("@loai", roomtype);
                            cmd.Parameters.AddWithValue("@maday", roomarea);
                            cmd.Parameters.AddWithValue("@ID", id);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRoomsData(); // Tải lại dữ liệu phòng
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
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateRoom();
        }

        private int id;
        private void dgvRoomData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvRoomData.Rows[e.RowIndex];
                id = (int)row.Cells["ID"].Value;
                txtRoomCode.Text = row.Cells["MaPhong"].Value.ToString();
                txtRoomName.Text = row.Cells["TenPhong"].Value.ToString();
                txtStatus.Text = row.Cells["TinhTrang"].Value.ToString();
                txtMaxQuantity.Text = row.Cells["SoSVToiDa"].Value.ToString();
                txtCurrQuantity.Text = row.Cells["SoSV"].Value.ToString();
                cbbRoomType.SelectedItem = row.Cells["LoaiPhong"].Value.ToString();
                cbbRoomArea.SelectedValue = row.Cells["MaDay"].Value.ToString() ;    
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void cbbRoomArea_DropDown(object sender, EventArgs e)
        {
            LoadDaysData();
        }
    }
}
