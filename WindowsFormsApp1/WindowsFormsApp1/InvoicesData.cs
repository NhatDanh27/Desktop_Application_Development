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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class InvoicesData : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public InvoicesData()
        {
            InitializeComponent();
            LoadInvoice();
            LoadRoomData();
            loadmahopdong();
            loadmanhanvien();
        }

        private void LoadInvoice()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT * FROM HoaDon";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvInvoice.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = @"
                    SELECT 
                        H.MaHoaDon AS [Mã Hóa Đơn],
                        H.MaHopDong AS [Mã Hợp Đồng],
                        H.MaNhanVien AS [Mã Nhân Viên],
                        H.Ngay AS [Ngày],
                        HD.TienPhong AS [Tiền Phòng],
                        H.SoDien AS [Số Điện],
                        H.SoNuoc AS [Số Nước],
                        HD.DonGiaDien AS [Đơn Giá Điện],
                        HD.DonGiaNuoc AS [Đơn Giá Nước],
                        (H.SoDien * HD.DonGiaDien) AS [Tổng Tiền Điện],
                        (H.SoNuoc * HD.DonGiaNuoc) AS [Tổng Tiền Nước],
                        (HD.TienPhong + (H.SoDien * HD.DonGiaDien) + (H.SoNuoc * HD.DonGiaNuoc)) AS [Tổng Tiền],
                        H.TinhTrang AS [Trạng Thái]
                    FROM HoaDon H
                    INNER JOIN HopDong HD ON H.MaHopDong = HD.MaHopDong
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            dgvInvoice.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + sqlEx.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadmahopdong()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT MaHopDong FROM HopDong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbbMaHD.DisplayMember = "MaHopDong";
                    cbbMaHD.ValueMember = "MaHopDong";
                    cbbMaHD.DataSource = table;
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

        private void loadmanhanvien()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT MaNV FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbbInvoiceEmployee.DisplayMember = "MaNV";
                    cbbInvoiceEmployee.ValueMember = "MaNV";
                    cbbInvoiceEmployee.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string maHopDong = cbbMaHD.SelectedValue.ToString();
            DateTime ngay = dtpInvoiceDate.Value;
            int soDien = int.Parse(txtSoDien.Text);
            int soNuoc = int.Parse(txtSoNuoc.Text);
            string maHoaDon = txtInvoiceCode.Text.Trim();
            string tinhtrang = cbbInvoiceStatus.SelectedItem.ToString();
            string MaNV = cbbInvoiceEmployee.SelectedValue.ToString();

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string checkQuery = "SELECT COUNT(*) FROM HoaDon WHERE MaHopDong = @MaHopDong";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connect))
                    {
                        checkCommand.Parameters.AddWithValue("@MaHopDong", maHopDong);
                        int count = (int)checkCommand.ExecuteScalar();


                        if (count > 0)
                        {
                            MessageBox.Show("Hóa đơn đã tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    string query = @"INSERT INTO HoaDon (MaHopDong, MaHoaDon,MaNhanVien, Ngay, TinhTrang, SoDien, SoNuoc)
                                 VALUES (@MaHopDong, @MaHoaDon,@MaNhanVien, @Ngay, @TinhTrang, @SoDien, @SoNuoc)";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {

                        cmd.Parameters.AddWithValue("@MaHopDong", maHopDong);
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmd.Parameters.AddWithValue("@MaNhanVien", MaNV);
                        cmd.Parameters.AddWithValue("@Ngay", ngay);
                        cmd.Parameters.AddWithValue("@TinhTrang", tinhtrang);

                        cmd.Parameters.AddWithValue("@SoDien", soDien);
                        cmd.Parameters.AddWithValue("@SoNuoc", soNuoc);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hóa đơn đã được thêm thành công!");
                        LoadData();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);

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
                string query = $"SELECT * FROM HoaDon WHERE MaHoaDon LIKE @search";



                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@search", "%" + search + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvInvoice.DataSource = dt;
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
                string MaHoaDon = txtInvoiceCode.Text.Trim();
                string MaHopDong = cbbMaHD.SelectedValue.ToString();
                string MaNV = cbbInvoiceEmployee.SelectedValue.ToString();
                string invoiceDate = dtpInvoiceDate.Value.ToString("yyyy-MM-dd");
                string Tinhtrang = cbbInvoiceStatus.SelectedItem.ToString();
                int Sonuoc = int.Parse(txtSoNuoc.Text);
                int Sodien = int.Parse(txtSoDien.Text);






                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();




                    string updateQuery = @"UPDATE HoaDon
                                   SET Ngay=@ngay,SoNuoc=@sonuoc,SoDien=@sodien,MaNhanVien=@maNv,
                                       
                                    
                                       
                                       TinhTrang = @TinhTrang
                                       
                                   WHERE MaHoaDon = @MaHoaDon";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {

                        updateCommand.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);

                        updateCommand.Parameters.AddWithValue("@ngay", invoiceDate);
                        updateCommand.Parameters.AddWithValue("@sonuoc", Sonuoc);
                        updateCommand.Parameters.AddWithValue("@sodien", Sodien);
                        updateCommand.Parameters.AddWithValue("@maNv", MaNV);




                        updateCommand.Parameters.AddWithValue("@TinhTrang", Tinhtrang);


                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Thanh toán thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInvoice();
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy dòng được chọn
            DataGridViewRow row = dgvInvoice.Rows[e.RowIndex];

            // Gán dữ liệu từ DataGridView vào các điều khiển trên form
            txtInvoiceCode.Text = row.Cells["MaHoaDon"].Value.ToString();
            cbbMaHD.SelectedValue = row.Cells["MaHopDong"].Value.ToString();
            cbbInvoiceEmployee.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
            dtpInvoiceDate.Value = DateTime.Parse(row.Cells["Ngay"].Value.ToString());
            txtSoDien.Text = row.Cells["SoDien"].Value.ToString();
            txtSoNuoc.Text = row.Cells["SoNuoc"].Value.ToString();
            cbbInvoiceStatus.SelectedItem = row.Cells["TinhTrang"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string MaHoaDon = txtInvoiceCode.Text.Trim();

            if (string.IsNullOrEmpty(MaHoaDon))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông báo xác nhận xóa
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa hợp đồng '{MaHoaDon}' không?",
                                                   "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";

                    using (SqlConnection connection = new SqlConnection(conn))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadInvoice(); // Refresh lại danh sách hóa đơn
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
                //textBox1.Clear();
                //textBox2.Clear();
                //textBox3.Clear();

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

        private void cbbInvoiceEmployee_DropDown(object sender, EventArgs e)
        {
           loadmanhanvien();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void bntHopDong_Click(object sender, EventArgs e)
        {
            frm_Contract hopdong = new frm_Contract();
            hopdong.Show();
        }
    }
}
