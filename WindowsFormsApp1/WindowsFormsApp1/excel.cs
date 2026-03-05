using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class excel : UserControl
    {
        public excel()
        {
            InitializeComponent();
        }

        private void btnExportStudent_Click(object sender, EventArgs e)
        {
            frm_BaoCaoStudent frm_BaoCaoStudent = new frm_BaoCaoStudent();
            frm_BaoCaoStudent.Show();
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            frm_BaoCaoInvoices frm_BaoCaoInvoices = new frm_BaoCaoInvoices();
            frm_BaoCaoInvoices.Show();
        }

        private void btnExportRooms_Click(object sender, EventArgs e)
        {
            frm_BaoCaoRoom frm_BaoCaoRoom = new frm_BaoCaoRoom();
            frm_BaoCaoRoom.Show();
        }

        private void btnExportEmployee_Click(object sender, EventArgs e)
        {
            frm_BaoCaoStaff frm_BaoCaoStaff = new frm_BaoCaoStaff();
            frm_BaoCaoStaff.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
