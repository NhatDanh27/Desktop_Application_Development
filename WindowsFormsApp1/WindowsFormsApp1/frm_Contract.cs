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
    public partial class frm_Contract : Form
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public frm_Contract()
        {
            InitializeComponent();
            LoadContract();
            LoadRoomData();
            LoadSVData();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadContract()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT * FROM HopDong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvContract.DataSource = dt;
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

        private void LoadSVData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT MaSV, TenSV FROM SinhVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbbMaSV.DisplayMember = "MaSV";
                    cbbMaSV.ValueMember = "MaSV";
                    cbbMaSV.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string invoiceCode = txtHD.Text.Trim();
            string roomCode = cbbRoomCode.SelectedValue.ToString();
            string employeeCode = cbbMaSV.SelectedValue.ToString();
            string invoiceDate = dtpInvoiceDate.Value.ToString("yyyy-MM-dd");
            string invoiceDate1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            decimal electricityPrice = decimal.Parse(txtDonGia1.Text);

            decimal waterPrice = decimal.Parse(txtDonGia2.Text);
            decimal TienPhong = decimal.Parse(textBox1.Text);

            string query = @"INSERT INTO HopDong
                             (MaHopDong,MaSV, MaPhong, NgayBatDau,NgayKetThuc, DonGiaDien, DonGiaNuoc, TienPhong) 
                             VALUES 
                             (@MaHopDong, @MaSV, @MaPhong, @NgayBatDau, @NgayKetThuc, @DonGiaDien, @DonGiaNuoc, @TienPhong)";

            try
            {

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@MaHopDong", invoiceCode);
                        command.Parameters.AddWithValue("@MaPhong", roomCode);
                        command.Parameters.AddWithValue("@MaSV", employeeCode);
                        command.Parameters.AddWithValue("@NgayBatDau", invoiceDate);
                        command.Parameters.AddWithValue("@NgayKetThuc", invoiceDate1);

                        command.Parameters.AddWithValue("@DonGiaDien", electricityPrice);

                        command.Parameters.AddWithValue("@DonGiaNuoc", waterPrice);
                        command.Parameters.AddWithValue("@TienPhong", TienPhong);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadContract();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string search = txtSearch.Text.Trim();


                if (string.IsNullOrEmpty(search))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string query = $"SELECT * FROM HopDong WHERE MaHopDong LIKE @search";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@search", "%" + search + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvContract.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string invoiceCode = txtHD.Text.Trim();
                string roomCode = cbbRoomCode.SelectedValue.ToString();
                string employeeCode = cbbMaSV.SelectedValue.ToString();
                string invoiceDate = dtpInvoiceDate.Value.ToString("yyyy-MM-dd");
                string invoiceDate1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                decimal electricityPrice = decimal.Parse(txtDonGia1.Text);

                decimal waterPrice = decimal.Parse(txtDonGia2.Text);
                decimal priceroom = decimal.Parse(textBox1.Text);


                string checkQuery = "SELECT COUNT(*) FROM HopDong WHERE MaHopDong = @mahopdong";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@mahopdong", invoiceCode);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Hợp đồng k tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }


                    string updateQuery = @"UPDATE HopDong
                                   SET 
                                       NgayBatDau = @Ngaybd,
                                       NgayKetThuc = @Ngaykt,
                                      
                                       MaPhong=@maphong,
                                    
                                       
                                       MaSV = @Masv,
                                       TienPhong = @TienPhong
                                   WHERE MaHopDong = @mahopdong";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@mahopdong", invoiceCode);
                        updateCommand.Parameters.AddWithValue("@maphong", roomCode);
                        updateCommand.Parameters.AddWithValue("@Masv", employeeCode);
                        updateCommand.Parameters.AddWithValue("@Ngaybd", invoiceDate);
                        updateCommand.Parameters.AddWithValue("@Ngaykt", invoiceDate1);




                        updateCommand.Parameters.AddWithValue("@TienPhong", priceroom);

                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadContract();
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvContract_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvContract.Rows[e.RowIndex];
                txtHD.Text = row.Cells["MaHopDong"].Value.ToString();
                cbbRoomCode.SelectedValue = row.Cells["MaPhong"].Value.ToString();
                cbbMaSV.SelectedValue = row.Cells["MaSV"].Value.ToString();
                dtpInvoiceDate.Value = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                dateTimePicker1.Value = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());

                txtDonGia1.Text = row.Cells["DonGiaDien"].Value.ToString();

                txtDonGia2.Text = row.Cells["DonGiaNuoc"].Value.ToString();
                textBox1.Text = row.Cells["TienPhong"].Value.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Lấy mã hóa đơn từ TextBox
            string invoiceCode = txtHD.Text.Trim();

            if (string.IsNullOrEmpty(invoiceCode))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông báo xác nhận xóa
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa hợp đồng '{invoiceCode}' không?",
                                                   "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM HopDong WHERE MaHopDong = @mahopdong";

                    using (SqlConnection connection = new SqlConnection(conn))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@mahopdong", invoiceCode);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadContract(); // Refresh lại danh sách hóa đơn
                            }
                            else
                            {
                                MessageBox.Show("Hóa đơn không tồn tại hoặc đã bị xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {

                txtHD.Clear();

                textBox1.Clear();


                if (cbbRoomCode.Items.Count > 0)
                    cbbRoomCode.SelectedIndex = 0;

                if (cbbMaSV.Items.Count > 0)
                    cbbMaSV.SelectedIndex = 0;




                dtpInvoiceDate.Value = DateTime.Now;


                if (dgvContract.SelectedRows.Count > 0)
                {
                    dgvContract.ClearSelection();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đặt lại giao diện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbRoomCode_DropDown(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void cbbMaSV_DropDown(object sender, EventArgs e)
        {
            LoadSVData();
        }
    }
}
