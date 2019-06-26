namespace FTSPayroll
{
    partial class OutstandingRecoveries
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFixDisplay = new System.Windows.Forms.Button();
            this.btnLoanDisplay = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.cmdDisplay1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDeductionGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_empName = new System.Windows.Forms.Label();
            this.txt_employeeNo = new System.Windows.Forms.TextBox();
            this.chkAllEmployee = new System.Windows.Forms.CheckBox();
            this.chkAllDivision = new System.Windows.Forms.CheckBox();
            this.chkAllDeduction = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDeductCode = new System.Windows.Forms.ComboBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDisplayrec = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFixDisplay);
            this.groupBox1.Controls.Add(this.btnLoanDisplay);
            this.groupBox1.Controls.Add(this.cmdClose);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Location = new System.Drawing.Point(12, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 49);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // btnFixDisplay
            // 
            this.btnFixDisplay.Enabled = false;
            this.btnFixDisplay.Location = new System.Drawing.Point(8, 19);
            this.btnFixDisplay.Name = "btnFixDisplay";
            this.btnFixDisplay.Size = new System.Drawing.Size(19, 23);
            this.btnFixDisplay.TabIndex = 16;
            this.btnFixDisplay.Text = "Display Fixed Deductions";
            this.btnFixDisplay.UseVisualStyleBackColor = true;
            this.btnFixDisplay.Visible = false;
            this.btnFixDisplay.Click += new System.EventHandler(this.btnFixDisplay_Click);
            // 
            // btnLoanDisplay
            // 
            this.btnLoanDisplay.Enabled = false;
            this.btnLoanDisplay.Location = new System.Drawing.Point(30, 19);
            this.btnLoanDisplay.Name = "btnLoanDisplay";
            this.btnLoanDisplay.Size = new System.Drawing.Size(24, 23);
            this.btnLoanDisplay.TabIndex = 17;
            this.btnLoanDisplay.Text = "Display Loan Deductions";
            this.btnLoanDisplay.UseVisualStyleBackColor = true;
            this.btnLoanDisplay.Visible = false;
            this.btnLoanDisplay.Click += new System.EventHandler(this.btnLoanDisplay_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(262, 19);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 14;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(85, 19);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(171, 23);
            this.btnDisplay.TabIndex = 18;
            this.btnDisplay.Text = "Display Deductions";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // cmdDisplay1
            // 
            this.cmdDisplay1.Enabled = false;
            this.cmdDisplay1.Location = new System.Drawing.Point(300, 21);
            this.cmdDisplay1.Name = "cmdDisplay1";
            this.cmdDisplay1.Size = new System.Drawing.Size(30, 23);
            this.cmdDisplay1.TabIndex = 15;
            this.cmdDisplay1.Text = "Display Report";
            this.cmdDisplay1.UseVisualStyleBackColor = true;
            this.cmdDisplay1.Visible = false;
            this.cmdDisplay1.Click += new System.EventHandler(this.cmdDisplay1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDisplayrec);
            this.groupBox2.Controls.Add(this.cmbDeductionGroup);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmdDisplay1);
            this.groupBox2.Controls.Add(this.lbl_empName);
            this.groupBox2.Controls.Add(this.txt_employeeNo);
            this.groupBox2.Controls.Add(this.chkAllEmployee);
            this.groupBox2.Controls.Add(this.chkAllDivision);
            this.groupBox2.Controls.Add(this.chkAllDeduction);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbDeductCode);
            this.groupBox2.Controls.Add(this.cmbDivision);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 136);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // cmbDeductionGroup
            // 
            this.cmbDeductionGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductionGroup.FormattingEnabled = true;
            this.cmbDeductionGroup.Location = new System.Drawing.Point(107, 46);
            this.cmbDeductionGroup.Name = "cmbDeductionGroup";
            this.cmbDeductionGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbDeductionGroup.TabIndex = 13;
            this.cmbDeductionGroup.SelectedIndexChanged += new System.EventHandler(this.cmbDeductionGroup_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Deduction Group";
            // 
            // lbl_empName
            // 
            this.lbl_empName.AutoSize = true;
            this.lbl_empName.Location = new System.Drawing.Point(107, 129);
            this.lbl_empName.Name = "lbl_empName";
            this.lbl_empName.Size = new System.Drawing.Size(0, 13);
            this.lbl_empName.TabIndex = 11;
            // 
            // txt_employeeNo
            // 
            this.txt_employeeNo.Location = new System.Drawing.Point(107, 100);
            this.txt_employeeNo.Name = "txt_employeeNo";
            this.txt_employeeNo.Size = new System.Drawing.Size(121, 20);
            this.txt_employeeNo.TabIndex = 10;
            this.txt_employeeNo.Validating += new System.ComponentModel.CancelEventHandler(this.txt_employeeNo_Validating);
            // 
            // chkAllEmployee
            // 
            this.chkAllEmployee.AutoSize = true;
            this.chkAllEmployee.Location = new System.Drawing.Point(234, 102);
            this.chkAllEmployee.Name = "chkAllEmployee";
            this.chkAllEmployee.Size = new System.Drawing.Size(91, 17);
            this.chkAllEmployee.TabIndex = 1;
            this.chkAllEmployee.Text = "All Employees";
            this.chkAllEmployee.UseVisualStyleBackColor = true;
            // 
            // chkAllDivision
            // 
            this.chkAllDivision.AutoSize = true;
            this.chkAllDivision.Location = new System.Drawing.Point(236, 21);
            this.chkAllDivision.Name = "chkAllDivision";
            this.chkAllDivision.Size = new System.Drawing.Size(37, 17);
            this.chkAllDivision.TabIndex = 9;
            this.chkAllDivision.Text = "All";
            this.chkAllDivision.UseVisualStyleBackColor = true;
            // 
            // chkAllDeduction
            // 
            this.chkAllDeduction.AutoSize = true;
            this.chkAllDeduction.Location = new System.Drawing.Point(236, 75);
            this.chkAllDeduction.Name = "chkAllDeduction";
            this.chkAllDeduction.Size = new System.Drawing.Size(94, 17);
            this.chkAllDeduction.TabIndex = 2;
            this.chkAllDeduction.Text = "All Deductions";
            this.chkAllDeduction.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "EmployeeNo";
            // 
            // cmbDeductCode
            // 
            this.cmbDeductCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductCode.FormattingEnabled = true;
            this.cmbDeductCode.Location = new System.Drawing.Point(107, 73);
            this.cmbDeductCode.Name = "cmbDeductCode";
            this.cmbDeductCode.Size = new System.Drawing.Size(121, 21);
            this.cmbDeductCode.TabIndex = 1;
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(107, 19);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 0;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Deduction Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Division";
            // 
            // btnDisplayrec
            // 
            this.btnDisplayrec.Location = new System.Drawing.Point(262, 46);
            this.btnDisplayrec.Name = "btnDisplayrec";
            this.btnDisplayrec.Size = new System.Drawing.Size(68, 23);
            this.btnDisplayrec.TabIndex = 16;
            this.btnDisplayrec.Text = "form";
            this.btnDisplayrec.UseVisualStyleBackColor = true;
            this.btnDisplayrec.Click += new System.EventHandler(this.btnDisplayrec_Click);
            // 
            // OutstandingRecoveries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 200);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutstandingRecoveries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Outstanding Recoveries";
            this.Load += new System.EventHandler(this.OutstandingRecoveries_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDisplay1;
        private System.Windows.Forms.Button btnLoanDisplay;
        private System.Windows.Forms.Button btnFixDisplay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAllEmployee;
        private System.Windows.Forms.CheckBox chkAllDivision;
        private System.Windows.Forms.CheckBox chkAllDeduction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDeductCode;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_employeeNo;
        private System.Windows.Forms.Label lbl_empName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeductionGroup;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnDisplayrec;
    }
}