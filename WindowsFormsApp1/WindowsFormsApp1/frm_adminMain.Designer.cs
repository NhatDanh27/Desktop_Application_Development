namespace WindowsFormsApp1
{
    partial class frm_adminMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_adminMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnstatistical = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnStudentMana = new System.Windows.Forms.Button();
            this.btnAreaMana = new System.Windows.Forms.Button();
            this.btnRoomMana = new System.Windows.Forms.Button();
            this.btnStaffMana = new System.Windows.Forms.Button();
            this.btnUserMana = new System.Windows.Forms.Button();
            this.dashBoard1 = new WindowsFormsApp1.dashBoard();
            this.excel1 = new WindowsFormsApp1.excel();
            this.invoicesData1 = new WindowsFormsApp1.InvoicesData();
            this.studentsData1 = new WindowsFormsApp1.StudentsData();
            this.roomsData1 = new WindowsFormsApp1.RoomsData();
            this.areasData1 = new WindowsFormsApp1.AreasData();
            this.staffsData1 = new WindowsFormsApp1.StaffsData();
            this.userData1 = new WindowsFormsApp1.UserData();
            this.userData2 = new WindowsFormsApp1.UserData();
            this.dashBoard2 = new WindowsFormsApp1.dashBoard();
            this.thongKe1 = new WindowsFormsApp1.ThongKe();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(126)))), ((int)(((byte)(140)))));
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnstatistical);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.btnStudentMana);
            this.panel1.Controls.Add(this.btnAreaMana);
            this.panel1.Controls.Add(this.btnRoomMana);
            this.panel1.Controls.Add(this.btnStaffMana);
            this.panel1.Controls.Add(this.btnUserMana);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 786);
            this.panel1.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(0, 706);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(325, 70);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnstatistical
            // 
            this.btnstatistical.FlatAppearance.BorderSize = 0;
            this.btnstatistical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstatistical.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstatistical.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnstatistical.Image = ((System.Drawing.Image)(resources.GetObject("btnstatistical.Image")));
            this.btnstatistical.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstatistical.Location = new System.Drawing.Point(0, 630);
            this.btnstatistical.Name = "btnstatistical";
            this.btnstatistical.Size = new System.Drawing.Size(325, 70);
            this.btnstatistical.TabIndex = 9;
            this.btnstatistical.Text = "Statistical";
            this.btnstatistical.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnstatistical.UseVisualStyleBackColor = true;
            this.btnstatistical.Click += new System.EventHandler(this.btnstatistical_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(325, 89);
            this.button1.TabIndex = 8;
            this.button1.Text = "ADMINISTRATOR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button7
            // 
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(0, 554);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(325, 70);
            this.button7.TabIndex = 7;
            this.button7.Text = "Export Excel File";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(0, 478);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(325, 70);
            this.button6.TabIndex = 6;
            this.button6.Text = "Invoice Management";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnStudentMana
            // 
            this.btnStudentMana.FlatAppearance.BorderSize = 0;
            this.btnStudentMana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentMana.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudentMana.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStudentMana.Image = ((System.Drawing.Image)(resources.GetObject("btnStudentMana.Image")));
            this.btnStudentMana.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStudentMana.Location = new System.Drawing.Point(0, 402);
            this.btnStudentMana.Name = "btnStudentMana";
            this.btnStudentMana.Size = new System.Drawing.Size(325, 70);
            this.btnStudentMana.TabIndex = 5;
            this.btnStudentMana.Text = "Student Management";
            this.btnStudentMana.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStudentMana.UseVisualStyleBackColor = true;
            this.btnStudentMana.Click += new System.EventHandler(this.btnStudentMana_Click);
            // 
            // btnAreaMana
            // 
            this.btnAreaMana.FlatAppearance.BorderSize = 0;
            this.btnAreaMana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAreaMana.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAreaMana.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAreaMana.Image = ((System.Drawing.Image)(resources.GetObject("btnAreaMana.Image")));
            this.btnAreaMana.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAreaMana.Location = new System.Drawing.Point(0, 250);
            this.btnAreaMana.Name = "btnAreaMana";
            this.btnAreaMana.Size = new System.Drawing.Size(325, 70);
            this.btnAreaMana.TabIndex = 4;
            this.btnAreaMana.Text = "Area Management";
            this.btnAreaMana.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAreaMana.UseVisualStyleBackColor = true;
            this.btnAreaMana.Click += new System.EventHandler(this.btnAreaMana_Click);
            // 
            // btnRoomMana
            // 
            this.btnRoomMana.FlatAppearance.BorderSize = 0;
            this.btnRoomMana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomMana.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomMana.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRoomMana.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomMana.Image")));
            this.btnRoomMana.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoomMana.Location = new System.Drawing.Point(0, 326);
            this.btnRoomMana.Name = "btnRoomMana";
            this.btnRoomMana.Size = new System.Drawing.Size(325, 70);
            this.btnRoomMana.TabIndex = 3;
            this.btnRoomMana.Text = "Room Management";
            this.btnRoomMana.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRoomMana.UseVisualStyleBackColor = true;
            this.btnRoomMana.Click += new System.EventHandler(this.btnRoomMana_Click);
            // 
            // btnStaffMana
            // 
            this.btnStaffMana.FlatAppearance.BorderSize = 0;
            this.btnStaffMana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaffMana.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaffMana.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStaffMana.Image = ((System.Drawing.Image)(resources.GetObject("btnStaffMana.Image")));
            this.btnStaffMana.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaffMana.Location = new System.Drawing.Point(0, 174);
            this.btnStaffMana.Name = "btnStaffMana";
            this.btnStaffMana.Size = new System.Drawing.Size(325, 70);
            this.btnStaffMana.TabIndex = 2;
            this.btnStaffMana.Text = "Staff Management";
            this.btnStaffMana.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStaffMana.UseVisualStyleBackColor = true;
            this.btnStaffMana.Click += new System.EventHandler(this.btnStaffMana_Click);
            // 
            // btnUserMana
            // 
            this.btnUserMana.FlatAppearance.BorderSize = 0;
            this.btnUserMana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserMana.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserMana.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUserMana.Image = ((System.Drawing.Image)(resources.GetObject("btnUserMana.Image")));
            this.btnUserMana.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserMana.Location = new System.Drawing.Point(0, 98);
            this.btnUserMana.Name = "btnUserMana";
            this.btnUserMana.Size = new System.Drawing.Size(325, 70);
            this.btnUserMana.TabIndex = 1;
            this.btnUserMana.Text = "User Management";
            this.btnUserMana.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserMana.UseVisualStyleBackColor = true;
            this.btnUserMana.Click += new System.EventHandler(this.btnUserMana_Click);
            // 
            // dashBoard1
            // 
            this.dashBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashBoard1.Location = new System.Drawing.Point(0, 0);
            this.dashBoard1.Name = "dashBoard1";
            this.dashBoard1.Size = new System.Drawing.Size(1068, 786);
            this.dashBoard1.TabIndex = 7;
            // 
            // excel1
            // 
            this.excel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.excel1.Location = new System.Drawing.Point(325, 0);
            this.excel1.Name = "excel1";
            this.excel1.Size = new System.Drawing.Size(1068, 786);
            this.excel1.TabIndex = 1;
            // 
            // invoicesData1
            // 
            this.invoicesData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invoicesData1.Location = new System.Drawing.Point(325, 0);
            this.invoicesData1.Name = "invoicesData1";
            this.invoicesData1.Size = new System.Drawing.Size(1068, 786);
            this.invoicesData1.TabIndex = 2;
            // 
            // studentsData1
            // 
            this.studentsData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentsData1.Location = new System.Drawing.Point(325, 0);
            this.studentsData1.Name = "studentsData1";
            this.studentsData1.Size = new System.Drawing.Size(1068, 786);
            this.studentsData1.TabIndex = 3;
            // 
            // roomsData1
            // 
            this.roomsData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomsData1.Location = new System.Drawing.Point(325, 0);
            this.roomsData1.Name = "roomsData1";
            this.roomsData1.Size = new System.Drawing.Size(1068, 786);
            this.roomsData1.TabIndex = 4;
            // 
            // areasData1
            // 
            this.areasData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areasData1.Location = new System.Drawing.Point(325, 0);
            this.areasData1.Name = "areasData1";
            this.areasData1.Size = new System.Drawing.Size(1068, 786);
            this.areasData1.TabIndex = 5;
            // 
            // staffsData1
            // 
            this.staffsData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staffsData1.Location = new System.Drawing.Point(325, 0);
            this.staffsData1.Name = "staffsData1";
            this.staffsData1.Size = new System.Drawing.Size(1068, 786);
            this.staffsData1.TabIndex = 6;
            // 
            // userData1
            // 
            this.userData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userData1.Location = new System.Drawing.Point(325, 0);
            this.userData1.Name = "userData1";
            this.userData1.Size = new System.Drawing.Size(1068, 786);
            this.userData1.TabIndex = 7;
            // 
            // userData2
            // 
            this.userData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userData2.Location = new System.Drawing.Point(325, 0);
            this.userData2.Name = "userData2";
            this.userData2.Size = new System.Drawing.Size(1068, 786);
            this.userData2.TabIndex = 8;
            // 
            // dashBoard2
            // 
            this.dashBoard2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashBoard2.Location = new System.Drawing.Point(325, 0);
            this.dashBoard2.Name = "dashBoard2";
            this.dashBoard2.Size = new System.Drawing.Size(1068, 786);
            this.dashBoard2.TabIndex = 9;
            // 
            // thongKe1
            // 
            this.thongKe1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thongKe1.Location = new System.Drawing.Point(325, 0);
            this.thongKe1.Name = "thongKe1";
            this.thongKe1.Size = new System.Drawing.Size(1068, 786);
            this.thongKe1.TabIndex = 10;
            // 
            // frm_adminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 786);
            this.Controls.Add(this.thongKe1);
            this.Controls.Add(this.dashBoard2);
            this.Controls.Add(this.userData2);
            this.Controls.Add(this.userData1);
            this.Controls.Add(this.staffsData1);
            this.Controls.Add(this.areasData1);
            this.Controls.Add(this.roomsData1);
            this.Controls.Add(this.studentsData1);
            this.Controls.Add(this.invoicesData1);
            this.Controls.Add(this.excel1);
            this.Controls.Add(this.panel1);
            this.Name = "frm_adminMain";
            this.Text = "frm_adminMain";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private dashBoard dashBoard1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnStudentMana;
        private System.Windows.Forms.Button btnAreaMana;
        private System.Windows.Forms.Button btnRoomMana;
        private System.Windows.Forms.Button btnStaffMana;
        private System.Windows.Forms.Button btnUserMana;
        private System.Windows.Forms.Button btnstatistical;
        private System.Windows.Forms.Button btnLogout;
        private excel excel1;
        private InvoicesData invoicesData1;
        private StudentsData studentsData1;
        private RoomsData roomsData1;
        private AreasData areasData1;
        private StaffsData staffsData1;
        private UserData userData1;
        private UserData userData2;
        private dashBoard dashBoard2;
        private ThongKe thongKe1;
    }
}