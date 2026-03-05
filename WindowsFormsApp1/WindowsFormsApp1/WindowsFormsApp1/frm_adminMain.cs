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
        }

        private void btnUserMana_Click(object sender, EventArgs e)
        {
            userData1.Visible = true;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            staffsData1.Visible = false;
            invoicesData1.Visible = false;
        }

        private void btnStaffMana_Click(object sender, EventArgs e)
        {
            userData1.Visible = false;
            staffsData1.Visible = true;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = false;
        }

        private void btnRoomMana_Click(object sender, EventArgs e)
        {
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = true;
            areasData1.Visible = false;
            staffsData1.Visible = false;
            invoicesData1.Visible = false;
        }

        private void btnAreaMana_Click(object sender, EventArgs e)
        {
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = true;
            staffsData1.Visible = false;
            invoicesData1.Visible = false;
        }

        private void btnStudentMana_Click(object sender, EventArgs e)
        {
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = true;
            invoicesData1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            userData1.Visible = false;
            staffsData1.Visible = false;
            roomsData1.Visible = false;
            areasData1.Visible = false;
            studentsData1.Visible = false;
            invoicesData1.Visible = true;
        }
    }
}
