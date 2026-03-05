using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frm_adminMain : Form
    {
        public frm_adminMain()
        {
            InitializeComponent();

            // Đảm bảo tất cả UserControl đã được thêm vào Form
            this.Controls.Add(userData1);
            this.Controls.Add(staffsData1);
            this.Controls.Add(roomsData1);
            this.Controls.Add(areasData1);
            this.Controls.Add(studentsData1);
            this.Controls.Add(invoicesData1);
            this.Controls.Add(excel1);
            this.Controls.Add(dashBoard1);
            this.Controls.Add(thongKe1);

            // Ban đầu ẩn tất cả UserControl
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void btnUserMana_Click(object sender, EventArgs e)
        {
            userData1.Visible = true;
            userData1.BringToFront();  // Đưa userData1 lên trên cùng
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void btnStaffMana_Click(object sender, EventArgs e)
        {
            staffsData1.Visible = true;
            staffsData1.BringToFront();  // Đưa staffsData1 lên trên cùng
            userData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void btnRoomMana_Click(object sender, EventArgs e)
        {
            roomsData1.Visible = true;
            roomsData1.BringToFront();  // Đưa roomsData1 lên trên cùng
            userData1.Visible = false;
            staffsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void btnAreaMana_Click(object sender, EventArgs e)
        {
            areasData1.Visible = true;
            areasData1.BringToFront();  // Đưa areasData1 lên trên cùng
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void btnStudentMana_Click(object sender, EventArgs e)
        {
            studentsData1.Visible = true;
            studentsData1.BringToFront();  // Đưa studentsData1 lên trên cùng
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            invoicesData1.Visible = true;
            invoicesData1.BringToFront();  // Đưa invoicesData1 lên trên cùng
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            excel1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            excel1.Visible = true;
            excel1.BringToFront();  // Đưa excel1 lên trên cùng
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            dashBoard1.Visible = false;
            thongKe1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashBoard1.Visible = true;
            dashBoard1.BringToFront();  // Đưa dashBoard1 lên trên cùng
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            thongKe1.Visible = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        public void SetUserInfo(string username, string role)
        {
            button1.Text = username + " " + '(' + role + ')';

        }

        private void btnstatistical_Click(object sender, EventArgs e)
        {
            dashBoard1.Visible = false;
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
            excel1.Visible = false;
            thongKe1.Visible = true;
            thongKe1.BringToFront();
        }
    }
}
