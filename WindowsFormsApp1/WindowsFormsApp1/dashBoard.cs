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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class dashBoard : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public dashBoard()
        {
            InitializeComponent();
            LoadDashboardData();
        }


        private void LoadDashboardData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Lấy tổng số sinh viên
                    string studentQuery = "SELECT COUNT(*) FROM SinhVien";
                    SqlCommand studentCommand = new SqlCommand(studentQuery, connection);
                    var resulttt = studentCommand.ExecuteScalar();
                    lblnumbertotalsudent.Text = resulttt.ToString();
                    lblnumbertotalsudent.Refresh();

                    // Lấy số phòng còn trống
                    string roomQuery = "SELECT COUNT(*) FROM Phong WHERE TinhTrang = N'Thiếu'";
                    SqlCommand roomCommand = new SqlCommand(roomQuery, connection);
                    var resultt = roomCommand.ExecuteScalar();
                    lblRoomavailable.Text = resultt.ToString();
                    lblRoomavailable.Refresh();

                    // Lấy số dãy còn đang bảo trì
                    string dayQuery = "SELECT COUNT(*) FROM Day WHERE TrangThai = N'Hoạt động'";
                    //MessageBox.Show(dayQuery);
                    SqlCommand dayCommand = new SqlCommand(dayQuery, connection);
                    var result = dayCommand.ExecuteScalar();
                    lblnumbersuitesactive.Text = result.ToString();
                    lblnumbersuitesactive.Refresh();


                    //MessageBox.Show(result.ToString());  // Hiển thị giá trị trả về

                    string nvQuery = "SELECT COUNT(*) FROM NhanVien";
                    //MessageBox.Show(dayQuery);
                    SqlCommand nvCommand = new SqlCommand(nvQuery, connection);
                    var result2 = nvCommand.ExecuteScalar();
                    lbltotalemploye.Text = result2.ToString();
                    lbltotalemploye.Refresh();


                    // Load các biểu đồ hoặc dữ liệu chi tiết khác nếu cần

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate1_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }
    }
}
