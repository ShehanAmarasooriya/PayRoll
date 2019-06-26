namespace FTSPayroll
{
    partial class RFTDeductionRegister
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
            this.cmbDeductGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDeduct = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDisplay2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAllEmp = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmpTo = new System.Windows.Forms.TextBox();
            this.txtEmpFrom = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cmbDeductGroup);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbDeduct);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 192);
            this.panel1.TabIndex = 0;
            // 
            // cmbDeductGroup
            // 
            this.cmbDeductGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductGroup.FormattingEnabled = true;
            this.cmbDeductGroup.Location = new System.Drawing.Point(120, 90);
            this.cmbDeductGroup.Name = "cmbDeductGroup";
            this.cmbDeductGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbDeductGroup.TabIndex = 42;
            this.cmbDeductGroup.SelectedIndexChanged += new System.EventHandler(this.cmbDeductGroup_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Deduction Group";
            // 
            // cmbDeduct
            // 
            this.cmbDeduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeduct.FormattingEnabled = true;
            this.cmbDeduct.Location = new System.Drawing.Point(120, 115);
            this.cmbDeduct.Name = "cmbDeduct";
            this.cmbDeduct.Size = new System.Drawing.Size(121, 21);
            this.cmbDeduct.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Deduction ";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(120, 9);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Division";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(120, 63);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Month";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Items.AddRange(new object[] {
            "2011",
            "2012",
            "2013",
            "2014",
            "2015"});
            this.cmbYear.Location = new System.Drawing.Point(120, 36);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Year";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(169, 199);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(90, 23);
            this.cmdClose.TabIndex = 20;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdDisplay2
            // 
            this.cmdDisplay2.Location = new System.Drawing.Point(50, 199);
            this.cmdDisplay2.Name = "cmdDisplay2";
            this.cmdDisplay2.Size = new System.Drawing.Size(113, 23);
            this.cmdDisplay2.TabIndex = 21;
            this.cmdDisplay2.Text = "Display Report";
            this.cmdDisplay2.UseVisualStyleBackColor = true;
            this.cmdDisplay2.Click += new System.EventHandler(this.cmdDisplay2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEmpTo);
            this.groupBox1.Controls.Add(this.txtEmpFrom);
            this.groupBox1.Controls.Add(this.chkAllEmp);
            this.groupBox1.Location = new System.Drawing.Point(11, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 50);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            // 
            // chkAllEmp
            // 
            this.chkAllEmp.AutoSize = true;
            this.chkAllEmp.Location = new System.Drawing.Point(13, 0);
            this.chkAllEmp.Name = "chkAllEmp";
            this.chkAllEmp.Size = new System.Drawing.Size(91, 17);
            this.chkAllEmp.TabIndex = 0;
            this.chkAllEmp.Text = "All Employees";
            this.chkAllEmp.UseVisualStyleBackColor = true;
            this.chkAllEmp.CheckedChanged += new System.EventHandler(this.chkAllEmp_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "To";
            // 
            // txtEmpTo
            // 
            this.txtEmpTo.Location = new System.Drawing.Point(159, 20);
            this.txtEmpTo.MaxLength = 4;
            this.txtEmpTo.Name = "txtEmpTo";
            this.txtEmpTo.Size = new System.Drawing.Size(63, 20);
            this.txtEmpTo.TabIndex = 48;
            // 
            // txtEmpFrom
            // 
            this.txtEmpFrom.Location = new System.Drawing.Point(68, 21);
            this.txtEmpFrom.MaxLength = 4;
            this.txtEmpFrom.Name = "txtEmpFrom";
            this.txtEmpFrom.Size = new System.Drawing.Size(63, 20);
            this.txtEmpFrom.TabIndex = 47;
            // 
            // RFTDeductionRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 231);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdDisplay2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RFTDeductionRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFTDeductionRegister";
            this.Load += new System.EventHandler(this.RFTDeductionRegister_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDisplay2;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDeduct;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbDeductGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAllEmp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmpTo;
        private System.Windows.Forms.TextBox txtEmpFrom;
    }
}