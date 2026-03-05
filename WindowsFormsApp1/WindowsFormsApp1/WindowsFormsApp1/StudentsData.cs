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
        private string conn = @"Data Source=NHAT-PC;Initial Catalog=QL;Integrated Security=True";
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
            string gender = cbbGender.SelectedItem.ToString();
            DateTime dop = dtpDOB.Value;
            string que = txtHometown.Text.Trim();
            string khoa = cbbKhoa.SelectedItem.ToString();
            string lop = txtClass.Text.Trim();
            string maphong = cbbRoomCode.SelectedValue.ToString();

            if(string.IsNullOrEmpty(masv) || string.IsNullOrEmpty(tensv) ||string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(que)
                || string.IsNullOrEmpty(khoa) || string.IsNullOrEmpty(lop) || string.IsNullOrEmpty(maphong))
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

                        string insertQuery = "INSERT INTO SinhVien (MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, Khoa, Lop, MaPhong) " +
                            "VALUES (@maSV, @tenSV, @gioiTinh, @ngaySinh, @queQuan, @khoa, @lop, @maPhong)";

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

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadStudentData(); // Refresh the data grid view
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



        }

        private void cbbRoomCode_DropDown(object sender, EventArgs e)
        {
            LoadRoomData();
        
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
