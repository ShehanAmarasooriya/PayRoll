namespace FTSPayroll
{
    partial class AddExtraNames
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAllDivision = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnAllActive = new System.Windows.Forms.RadioButton();
            this.rbtnEmpAttended = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.chkPaidHoliday = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkPaidHoliday);
            this.panel1.Controls.Add(this.chkAllDivision);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 214);
            this.panel1.TabIndex = 0;
            // 
            // chkAllDivision
            // 
            this.chkAllDivision.AutoSize = true;
            this.chkAllDivision.Location = new System.Drawing.Point(208, 39);
            this.chkAllDivision.Name = "chkAllDivision";
            this.chkAllDivision.Size = new System.Drawing.Size(77, 17);
            this.chkAllDivision.TabIndex = 18;
            this.chkAllDivision.Text = "All Division";
            this.chkAllDivision.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "* You can delete or add Individual free names using \"Daily Harvest\"";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnAllActive);
            this.groupBox1.Controls.Add(this.rbtnEmpAttended);
            this.groupBox1.Location = new System.Drawing.Point(83, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 66);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // rbtnAllActive
            // 
            this.rbtnAllActive.AutoSize = true;
            this.rbtnAllActive.Location = new System.Drawing.Point(6, 34);
            this.rbtnAllActive.Name = "rbtnAllActive";
            this.rbtnAllActive.Size = new System.Drawing.Size(161, 17);
            this.rbtnAllActive.TabIndex = 1;
            this.rbtnAllActive.TabStop = true;
            this.rbtnAllActive.Text = "Add To All Active Employees";
            this.rbtnAllActive.UseVisualStyleBackColor = true;
            // 
            // rbtnEmpAttended
            // 
            this.rbtnEmpAttended.AutoSize = true;
            this.rbtnEmpAttended.Location = new System.Drawing.Point(7, 11);
            this.rbtnEmpAttended.Name = "rbtnEmpAttended";
            this.rbtnEmpAttended.Size = new System.Drawing.Size(230, 17);
            this.rbtnEmpAttended.TabIndex = 0;
            this.rbtnEmpAttended.TabStop = true;
            this.rbtnEmpAttended.Text = "Add To All Employees Attended This Month";
            this.rbtnEmpAttended.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(83, 11);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(83, 37);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Division";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdDelete);
            this.groupBox2.Controls.Add(this.cmdClose);
            this.groupBox2.Controls.Add(this.cmdAdd);
            this.groupBox2.Location = new System.Drawing.Point(83, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 47);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(104, 15);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(106, 23);
            this.cmdDelete.TabIndex = 5;
            this.cmdDelete.Text = "DELETE ALL";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(216, 15);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(106, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(11, 15);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(87, 23);
            this.cmdAdd.TabIndex = 0;
            this.cmdAdd.Text = "ADD";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // chkPaidHoliday
            // 
            this.chkPaidHoliday.AutoSize = true;
            this.chkPaidHoliday.Enabled = false;
            this.chkPaidHoliday.Location = new System.Drawing.Point(208, 13);
            this.chkPaidHoliday.Name = "chkPaidHoliday";
            this.chkPaidHoliday.Size = new System.Drawing.Size(85, 17);
            this.chkPaidHoliday.TabIndex = 19;
            this.chkPaidHoliday.Text = "Paid Holiday";
            this.chkPaidHoliday.UseVisualStyleBackColor = true;
            // 
            // AddExtraNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 215);
            this.Controls.Add(this.panel1);
            this.Name = "AddExtraNames";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Paid Holiday Free Names";
            this.Load += new System.EventHandler(this.AddExtraNames_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnAllActive;
        private System.Windows.Forms.RadioButton rbtnEmpAttended;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAllDivision;
        private System.Windows.Forms.CheckBox chkPaidHoliday;
    }
}