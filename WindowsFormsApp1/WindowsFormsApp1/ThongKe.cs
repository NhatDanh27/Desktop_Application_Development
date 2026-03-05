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
    public partial class ThongKe : UserControl
    {
        private string conn = @"Data Source=HUAN_PC;Initial Catalog=QLKTX;Integrated Security=True";
        public ThongKe()
        {
            InitializeComponent();
            LoadRoomStatusChart();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    // Câu truy vấn SQL để lấy số lượng sinh viên đăng ký trong các năm 2023 và 2024
                    string query = "SELECT YEAR(NgayDK) AS Year, COUNT(*) AS StudentCount FROM SinhVien " +
                                   "WHERE YEAR(NgayDK) BETWEEN 2023 AND 2025 " +
                                   "GROUP BY YEAR(NgayDK)";

                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Dọn dẹp dữ liệu biểu đồ cũ
                    chart1.Series.Clear();  // Xóa các series cũ

                    // Tạo mới Series cho biểu đồ
                    Series studentCountSeries = new Series("StudentCount");
                    studentCountSeries.ChartType = SeriesChartType.Column;  // Loại biểu đồ là cột

                    // Kích hoạt hiển thị giá trị trên đầu cột
                    studentCountSeries.IsValueShownAsLabel = true;

                    // Đọc dữ liệu từ SqlDataReader và thêm vào Series
                    while (reader.Read())
                    {
                        int year = reader.GetInt32(0);  // Lấy năm
                        int studentCount = reader.GetInt32(1);  // Lấy số lượng sinh viên

                        // Thêm dữ liệu vào series
                        studentCountSeries.Points.AddXY(year, studentCount);
                    }

                    // Thêm series vào chart
                    chart1.Series.Add(studentCountSeries);

                    // Kiểm tra nếu không có dữ liệu
                    if (studentCountSeries.Points.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu thống kê cho các năm 2023-2025!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thống kê thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi khi thống kê sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoomStatusChart()
        {
            // Khởi tạo biểu đồ tròn
            chartRoomStatus.Series.Clear();
            chartRoomStatus.ChartAreas.Clear();

            // Tạo Chart Area
            ChartArea chartArea = new ChartArea();
            chartRoomStatus.ChartAreas.Add(chartArea);

            // Tạo Series cho biểu đồ tròn
            Series series = new Series("RoomStatus");
            series.ChartType = SeriesChartType.Pie;  // Biểu đồ tròn
            series.IsValueShownAsLabel = true;  // Hiển thị giá trị trên các phần

            // Lấy số lượng dãy phòng từ cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                // Câu truy vấn để lấy số dãy phòng trong hoạt động, bảo trì và còn trống
                string query = @"
        SELECT 
            (SELECT COUNT(*) FROM Day WHERE TrangThai = N'Hoạt động') AS ActiveDays,
            (SELECT COUNT(*) FROM Day WHERE TrangThai = N'Bảo trì') AS MaintenanceDays,
            (SELECT COUNT(*) FROM Day WHERE TrangThai = N'Còn trống') AS EmptyDays";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Lấy số dãy phòng trong từng trạng thái
                    int activeDays = reader.GetInt32(0);
                    int maintenanceDays = reader.GetInt32(1);
                    int emptyDays = reader.GetInt32(2);

                    // Thêm dữ liệu vào Series
                    series.Points.AddXY("Dãy phòng hoạt động", activeDays);
                    series.Points.AddXY("Dãy phòng bảo trì", maintenanceDays);
                    series.Points.AddXY("Dãy phòng còn trống", emptyDays);
                }

                reader.Close();
            }

            // Thêm Series vào biểu đồ
            chartRoomStatus.Series.Add(series);
        }
    }
}
