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
    public partial class InvoicesData : UserControl
    {
        private string conn = @"Data Source=NHAT-PC;Initial Catalog=QL;Integrated Security=True";
        public InvoicesData()
        {
            InitializeComponent();
            LoadRoomData();
            LoadInvoice();
            LoadStaffData();
        }
        private void LoadInvoice()
        {
            try
            {
                using(SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT *FROM Invoices";
                    SqlDataAdapter adapter = new SqlDataAdapter(query,connect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
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

                    cbbRC.DisplayMember = "MaPhong";
                    cbbRC.ValueMember = "MaPhong";
                    cbbRC.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadStaffData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT MaNV, TenNV FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbbMaNV.DisplayMember = "MaNV";
                    cbbMaNV.ValueMember = "MaNV";
                    cbbMaNV.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string invoiceCode = txtIC.Text;
            string roomCode = cbbRC.SelectedValue.ToString();
            string employeeCode = cbbMaNV.SelectedValue.ToString();
            string invoiceDate = dtpDOB.Value.ToString("yyyy-MM-dd");
            string invoiceStatus = cbbStatus.SelectedItem.ToString();
            int electricityUsage = int.Parse(txtSD.Text);
            decimal electricityPrice = decimal.Parse(txtDG1.Text);
            int waterUsage = int.Parse(txtSN.Text);
            decimal waterPrice = decimal.Parse(txtDG2.Text);
            

            string query = @"INSERT INTO Invoices 
                             (InvoiceCode,MaNhanVien, RoomCode, Ngay, TinhTrang, SoDien, DonGiaDien, SoNuoc, DonGiaNuoc ) 
                             VALUES 
                             (@InvoiceCode, @MaNhanVien, @RoomCode, @Ngay, @TinhTrang, @SoDien, @DonGiaDien, @SoNuoc, @DonGiaNuoc)";

            try
            {
            
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@InvoiceCode", invoiceCode);
                        command.Parameters.AddWithValue("@RoomCode", roomCode);
                        command.Parameters.AddWithValue("@MaNhanVien", employeeCode);
                        command.Parameters.AddWithValue("@Ngay", invoiceDate);
                        command.Parameters.AddWithValue("@TinhTrang", invoiceStatus);
                        command.Parameters.AddWithValue("@SoDien", electricityUsage);
                        command.Parameters.AddWithValue("@DonGiaDien", electricityPrice);
                        command.Parameters.AddWithValue("@SoNuoc", waterUsage);
                        command.Parameters.AddWithValue("@DonGiaNuoc", waterPrice);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInvoice();
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                string invoiceCode = txtIC.Text;
                string roomCode = cbbRC.SelectedValue.ToString();
                string employeeCode = cbbMaNV.SelectedValue.ToString();
                string invoiceDate = dtpDOB.Value.ToString("yyyy-MM-dd");
                string invoiceStatus = cbbStatus.SelectedItem.ToString();
                int electricityUsage = int.Parse(txtSD.Text);
                decimal electricityPrice = decimal.Parse(txtDG1.Text);
                int waterUsage = int.Parse(txtSN.Text);
                decimal waterPrice = decimal.Parse(txtDG2.Text);


                string query = @"UPDATE Invoices
                                 SET RoomCode = @RoomCode,
                                     Ngay = @Ngay,
                                     TinhTrang = @TinhTrang,
                                     SoDien = @SoDien,
                                     DonGiaDien = @DonGiaDien,
                                     SoNuoc = @SoNuoc,
                                     DonGiaNuoc = @DonGiaNuoc,
                                     MaNhanVien = @MaNhanVien
                                 WHERE InvoiceCode = @InvoiceCode";

                
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@InvoiceCode", invoiceCode);
                        command.Parameters.AddWithValue("@RoomCode", roomCode);
                        command.Parameters.AddWithValue("@MaNhanVien", employeeCode);
                        command.Parameters.AddWithValue("@Ngay", invoiceDate);
                        command.Parameters.AddWithValue("@TinhTrang", invoiceStatus);
                        command.Parameters.AddWithValue("@SoDien", electricityUsage);
                        command.Parameters.AddWithValue("@DonGiaDien", electricityPrice);
                        command.Parameters.AddWithValue("@SoNuoc", waterUsage);
                        command.Parameters.AddWithValue("@DonGiaNuoc", waterPrice);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                
                txtIC.Text = row.Cells["InvoiceCode"].Value.ToString();
                cbbRC.SelectedValue = row.Cells["RoomCode"].Value.ToString();
                dtpDOB.Value = DateTime.Parse(row.Cells["Ngay"].Value.ToString());
                cbbStatus.SelectedItem = row.Cells["TinhTrang"].Value.ToString();
                txtSD.Text = row.Cells["SoDien"].Value.ToString();
                txtDG1.Text = row.Cells["DonGiaDien"].Value.ToString();
                txtSN.Text = row.Cells["SoNuoc"].Value.ToString();
                txtDG2.Text = row.Cells["DonGiaNuoc"].Value.ToString();
                cbbMaNV.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                string invoiceCode = txtIC.Text;
                string roomCode = cbbRC.SelectedValue.ToString();
                string employeeCode = cbbMaNV.SelectedValue.ToString();
                string invoiceDate = dtpDOB.Value.ToString("yyyy-MM-dd");
                string invoiceStatus = cbbStatus.SelectedItem.ToString();
                int electricityUsage = int.Parse(txtSD.Text);
                decimal electricityPrice = decimal.Parse(txtDG1.Text);
                int waterUsage = int.Parse(txtSN.Text);
                decimal waterPrice = decimal.Parse(txtDG2.Text);


                string query = @"DELETE FROM Invoices WHERE InvoiceCode = @InvoiceCode";


                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@InvoiceCode", invoiceCode);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIC.Clear();
            txtSN.Clear();
            txtSD.Clear();
            txtDG1.Clear();
            txtDG2.Clear();
          
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin! " , "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (rdbIS.Checked)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string query = @"SELECT * FROM Invoices
                             WHERE TinhTrang LIKE @SearchQuery";

                        SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + txtSearch.Text.Trim() + "%");
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if(rdbIC.Checked)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string query = @"SELECT * FROM Invoices
                             WHERE InvoiceCode LIKE @SearchQuery ";

                        SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + txtSearch.Text.Trim() + "%");
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (rdbRC.Checked)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string query = @"SELECT * FROM Invoices
                             WHERE RoomCode LIKE @SearchQuery";

                        SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + txtSearch.Text.Trim() + "%");
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string invoiceCode = txtIC.Text;
                string roomCode = cbbRC.SelectedValue.ToString();
                string employeeCode = cbbMaNV.SelectedValue.ToString();
                string invoiceDate = dtpDOB.Value.ToString("yyyy-MM-dd");
                string invoiceStatus = cbbStatus.SelectedItem.ToString();
                int electricityUsage = int.Parse(txtSD.Text);
                decimal electricityPrice = decimal.Parse(txtDG1.Text);
                int waterUsage = int.Parse(txtSN.Text);
                decimal waterPrice = decimal.Parse(txtDG2.Text);


                string query = @"UPDATE Invoices
                                 SET 
                                     TinhTrang = 'Đã thanh toán'
                                     
                                 WHERE InvoiceCode = @InvoiceCode";


                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@InvoiceCode", invoiceCode);
                        
                        command.Parameters.AddWithValue("@TinhTrang", invoiceStatus);
                       
                        command.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
