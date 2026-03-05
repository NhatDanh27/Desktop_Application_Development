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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public partial class AreasData : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public AreasData()
        {
            InitializeComponent();
            LoadAreasData();
            LoadEmployeesData();
        }

        private void LoadAreasData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    string query = "SELECT Day.ID, Day.MaDay, Day.TenDay, Day.TrangThai, NhanVien.TenNV AS QuanLy " +
                                   "FROM Day " +
                                   "JOIN NhanVien ON Day.QuanLy = NhanVien.MaNV";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvArea.DataSource = table;
                    dgvArea.Columns["ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployeesData()
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

                    // Gán dữ liệu vào ComboBox
                    cbbManager.DisplayMember = "TenNV";  // Hiển thị tên nhân viên
                    cbbManager.ValueMember = "MaNV";     // Lưu trữ mã nhân viên
                    cbbManager.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maday = txtAreaCode.Text.Trim();
            string tenday = txtAreaName.Text.Trim();
            int manager = (int)cbbManager.SelectedValue;
            string status = cbbStatus.SelectedItem?.ToString();

            if(string.IsNullOrEmpty(maday) || string.IsNullOrEmpty(tenday) || string.IsNullOrEmpty(status) || manager == 0)
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

                        string checkQuery = "SELECT COUNT(*) FROM Day WHERE MaDay = @maday";
                        using (SqlCommand check = new SqlCommand(checkQuery, connect))
                        {
                            check.Parameters.AddWithValue("@maday", maday);

                            int madayExists = (int)check.ExecuteScalar();
                            if (madayExists > 0)
                            {
                                MessageBox.Show($"Mã dãy: {maday} đã tồn tại. Vui lòng chọn Username khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Dừng thực hiện nếu Username đã tồn tại
                            }
                        }

                        string query = "INSERT INTO Day (MaDay, TenDay, QuanLy, TrangThai) " +
                                       "VALUES (@maday, @tenday, @manager, @status)";

                        using(SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@maday", maday);
                            cmd.Parameters.AddWithValue("@tenday", tenday);
                            cmd.Parameters.AddWithValue("@manager", manager);
                            cmd.Parameters.AddWithValue("@status", status);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Thêm dãy phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAreasData(); // Cập nhật lại danh sách người dùng
                                clearFields();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm dãy phòng. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void dgvArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvArea.Rows[e.RowIndex];

                // Lấy ID dãy phòng
                id = (int)row.Cells["ID"].Value;

                // Cập nhật các TextBox
                txtAreaCode.Text = row.Cells["MaDay"].Value.ToString();
                txtAreaName.Text = row.Cells["TenDay"].Value.ToString();
                cbbStatus.Text = row.Cells["TrangThai"].Value.ToString();

                // Cập nhật ComboBox với tên nhân viên trong cột QuanLy
                string managerName = row.Cells["QuanLy"].Value.ToString();

                // Kiểm tra nếu tên nhân viên trong cột QuanLy không rỗng
                if (!string.IsNullOrEmpty(managerName))
                {
                    // Cập nhật ComboBox bằng tên nhân viên
                    foreach (DataRow rowItem in ((DataTable)cbbManager.DataSource).Rows)
                    {
                        if (rowItem["TenNV"].ToString() == managerName)
                        {
                            cbbManager.SelectedValue = rowItem["MaNV"];
                            break;
                        }
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string maday = txtAreaCode.Text.Trim();
            string tenday = txtAreaName.Text.Trim();
            int manager = (int)cbbManager.SelectedValue;
            string status = cbbStatus.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(maday) || string.IsNullOrEmpty(tenday) || string.IsNullOrEmpty(status) || manager == 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn cập nhật dãy '{maday}' không?", "Thong Bao",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(conn))
                        {
                            connect.Open();

                            string checkQuery = "SELECT COUNT(*) FROM Day WHERE MaDay = @maday AND ID != @id";
                            using (SqlCommand check = new SqlCommand(checkQuery, connect))
                            {
                                check.Parameters.AddWithValue("@maday", maday);
                                check.Parameters.AddWithValue("@id", id);

                                int madayExists = (int)check.ExecuteScalar();
                                if (madayExists > 0)
                                {
                                    MessageBox.Show($"Mã dãy: {maday} đã tồn tại. Vui lòng chọn Username khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Dừng thực hiện nếu Username đã tồn tại
                                }
                            }

                            string updateQuery = "UPDATE Day SET MaDay = @maday, TenDay = @tenday, TrangThai = @status, QuanLy = @manager WHERE ID =@id";
                            using (SqlCommand update = new SqlCommand(updateQuery, connect))
                            {
                                update.Parameters.AddWithValue("@maday", maday);
                                update.Parameters.AddWithValue("@tenday", tenday);
                                update.Parameters.AddWithValue("@manager", manager);
                                update.Parameters.AddWithValue("@status", status);
                                update.Parameters.AddWithValue("@id", id);

                                int count = update.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadAreasData();
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
            string maday = txtAreaCode.Text.Trim();

            if (string.IsNullOrEmpty(maday))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dãy có mã ('{maday}') không?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(conn))
                        {
                            connect.Open();

                            string deleteQuery = "DELETE FROM Day WHERE MaDay = @maday";

                            using (SqlCommand delete = new SqlCommand(deleteQuery, connect))
                            {
                                delete.Parameters.AddWithValue("@maday", maday);

                                int count = delete.ExecuteNonQuery();

                                if (count > 0)
                                {
                                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadAreasData();
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
            txtAreaCode.Text = "";
            txtAreaName.Text = "";
            cbbStatus.SelectedIndex = -1;
            cbbManager.SelectedIndex = -1;

            dgvArea.DataSource = null;
            LoadAreasData();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void cbbManager_DropDown(object sender, EventArgs e)
        {
            LoadEmployeesData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadAreasData();
        }
    }
}
