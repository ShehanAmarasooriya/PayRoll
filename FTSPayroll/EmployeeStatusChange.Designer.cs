namespace FTSPayroll
{
    partial class EmployeeStatusChange
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAutoProcess = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkAllDiv = new System.Windows.Forms.CheckBox();
            this.rbtnBatchWise = new System.Windows.Forms.RadioButton();
            this.rbtnEmpWise = new System.Windows.Forms.RadioButton();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEstate = new System.Windows.Forms.ComboBox();
            this.gbBatchWise = new System.Windows.Forms.GroupBox();
            this.lblTerminateDuration = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblInactiveDuration = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkAllEmp = new System.Windows.Forms.CheckBox();
            this.txtBatchWiseReason = new System.Windows.Forms.TextBox();
            this.cmbBatchNewStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBatchCurrentStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gvlist = new System.Windows.Forms.DataGridView();
            this.gbEmpWise = new System.Windows.Forms.GroupBox();
            this.txtLastRate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLastDate = new System.Windows.Forms.TextBox();
            this.lblLastWorkDate = new System.Windows.Forms.Label();
            this.lblEmpName = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbtnInactive = new System.Windows.Forms.RadioButton();
            this.rbtnActive = new System.Windows.Forms.RadioButton();
            this.cmbNewStatus = new System.Windows.Forms.ComboBox();
            this.txtCurrentStatus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmpNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAboveToChange = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbBatchWise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvlist)).BeginInit();
            this.gbEmpWise.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1027, 505);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAboveToChange);
            this.groupBox2.Controls.Add(this.btnAutoProcess);
            this.groupBox2.Controls.Add(this.btnReport);
            this.groupBox2.Controls.Add(this.btnChangeStatus);
            this.groupBox2.Location = new System.Drawing.Point(9, 445);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(985, 43);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnAutoProcess
            // 
            this.btnAutoProcess.Location = new System.Drawing.Point(644, 13);
            this.btnAutoProcess.Name = "btnAutoProcess";
            this.btnAutoProcess.Size = new System.Drawing.Size(318, 23);
            this.btnAutoProcess.TabIndex = 2;
            this.btnAutoProcess.Text = "AutoProcess";
            this.btnAutoProcess.UseVisualStyleBackColor = true;
            this.btnAutoProcess.Click += new System.EventHandler(this.btnAutoProcess_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(194, 13);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(202, 23);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "Employee Status Change Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnChangeStatus
            // 
            this.btnChangeStatus.Location = new System.Drawing.Point(15, 13);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(173, 23);
            this.btnChangeStatus.TabIndex = 0;
            this.btnChangeStatus.Text = "Change Status";
            this.btnChangeStatus.UseVisualStyleBackColor = true;
            this.btnChangeStatus.Click += new System.EventHandler(this.btnChangeStatus_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.gbBatchWise);
            this.groupBox1.Controls.Add(this.gbEmpWise);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 436);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkAllDiv);
            this.groupBox5.Controls.Add(this.rbtnBatchWise);
            this.groupBox5.Controls.Add(this.rbtnEmpWise);
            this.groupBox5.Controls.Add(this.cmbDivision);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.cmbEstate);
            this.groupBox5.Location = new System.Drawing.Point(19, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(730, 51);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            // 
            // chkAllDiv
            // 
            this.chkAllDiv.AutoSize = true;
            this.chkAllDiv.Location = new System.Drawing.Point(464, 20);
            this.chkAllDiv.Name = "chkAllDiv";
            this.chkAllDiv.Size = new System.Drawing.Size(45, 17);
            this.chkAllDiv.TabIndex = 24;
            this.chkAllDiv.Text = "ALL";
            this.chkAllDiv.UseVisualStyleBackColor = true;
            // 
            // rbtnBatchWise
            // 
            this.rbtnBatchWise.AutoSize = true;
            this.rbtnBatchWise.Enabled = false;
            this.rbtnBatchWise.Location = new System.Drawing.Point(642, 19);
            this.rbtnBatchWise.Name = "rbtnBatchWise";
            this.rbtnBatchWise.Size = new System.Drawing.Size(80, 17);
            this.rbtnBatchWise.TabIndex = 23;
            this.rbtnBatchWise.TabStop = true;
            this.rbtnBatchWise.Text = "Batch Wise";
            this.rbtnBatchWise.UseVisualStyleBackColor = true;
            this.rbtnBatchWise.CheckedChanged += new System.EventHandler(this.rbtnBatchWise_CheckedChanged);
            // 
            // rbtnEmpWise
            // 
            this.rbtnEmpWise.AutoSize = true;
            this.rbtnEmpWise.Enabled = false;
            this.rbtnEmpWise.Location = new System.Drawing.Point(537, 19);
            this.rbtnEmpWise.Name = "rbtnEmpWise";
            this.rbtnEmpWise.Size = new System.Drawing.Size(98, 17);
            this.rbtnEmpWise.TabIndex = 22;
            this.rbtnEmpWise.TabStop = true;
            this.rbtnEmpWise.Text = "Employee Wise";
            this.rbtnEmpWise.UseVisualStyleBackColor = true;
            this.rbtnEmpWise.CheckedChanged += new System.EventHandler(this.rbtnEmpWise_CheckedChanged);
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(322, 19);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(135, 21);
            this.cmbDivision.TabIndex = 1;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Division";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Estate";
            // 
            // cmbEstate
            // 
            this.cmbEstate.FormattingEnabled = true;
            this.cmbEstate.Location = new System.Drawing.Point(55, 19);
            this.cmbEstate.Name = "cmbEstate";
            this.cmbEstate.Size = new System.Drawing.Size(185, 21);
            this.cmbEstate.TabIndex = 0;
            this.cmbEstate.SelectedIndexChanged += new System.EventHandler(this.cmbEstate_SelectedIndexChanged);
            // 
            // gbBatchWise
            // 
            this.gbBatchWise.Controls.Add(this.lblTerminateDuration);
            this.gbBatchWise.Controls.Add(this.label11);
            this.gbBatchWise.Controls.Add(this.label10);
            this.gbBatchWise.Controls.Add(this.lblInactiveDuration);
            this.gbBatchWise.Controls.Add(this.label9);
            this.gbBatchWise.Controls.Add(this.chkAllEmp);
            this.gbBatchWise.Controls.Add(this.txtBatchWiseReason);
            this.gbBatchWise.Controls.Add(this.cmbBatchNewStatus);
            this.gbBatchWise.Controls.Add(this.label8);
            this.gbBatchWise.Controls.Add(this.cmbBatchCurrentStatus);
            this.gbBatchWise.Controls.Add(this.label7);
            this.gbBatchWise.Controls.Add(this.gvlist);
            this.gbBatchWise.Location = new System.Drawing.Point(19, 171);
            this.gbBatchWise.Name = "gbBatchWise";
            this.gbBatchWise.Size = new System.Drawing.Size(968, 253);
            this.gbBatchWise.TabIndex = 29;
            this.gbBatchWise.TabStop = false;
            this.gbBatchWise.Text = "Batch Wise Status Change";
            // 
            // lblTerminateDuration
            // 
            this.lblTerminateDuration.AutoSize = true;
            this.lblTerminateDuration.Location = new System.Drawing.Point(872, 52);
            this.lblTerminateDuration.Name = "lblTerminateDuration";
            this.lblTerminateDuration.Size = new System.Drawing.Size(16, 13);
            this.lblTerminateDuration.TabIndex = 45;
            this.lblTerminateDuration.Text = "...";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(737, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "Terminate Absent Days";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(737, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Inactive Absent Days";
            // 
            // lblInactiveDuration
            // 
            this.lblInactiveDuration.AutoSize = true;
            this.lblInactiveDuration.Location = new System.Drawing.Point(872, 29);
            this.lblInactiveDuration.Name = "lblInactiveDuration";
            this.lblInactiveDuration.Size = new System.Drawing.Size(16, 13);
            this.lblInactiveDuration.TabIndex = 42;
            this.lblInactiveDuration.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Reason";
            // 
            // chkAllEmp
            // 
            this.chkAllEmp.AutoSize = true;
            this.chkAllEmp.Enabled = false;
            this.chkAllEmp.Location = new System.Drawing.Point(492, 48);
            this.chkAllEmp.Name = "chkAllEmp";
            this.chkAllEmp.Size = new System.Drawing.Size(70, 17);
            this.chkAllEmp.TabIndex = 41;
            this.chkAllEmp.Text = "Select All";
            this.chkAllEmp.UseVisualStyleBackColor = true;
            this.chkAllEmp.CheckedChanged += new System.EventHandler(this.chkAllEmp_CheckedChanged);
            // 
            // txtBatchWiseReason
            // 
            this.txtBatchWiseReason.Location = new System.Drawing.Point(96, 49);
            this.txtBatchWiseReason.Name = "txtBatchWiseReason";
            this.txtBatchWiseReason.Size = new System.Drawing.Size(376, 20);
            this.txtBatchWiseReason.TabIndex = 40;
            // 
            // cmbBatchNewStatus
            // 
            this.cmbBatchNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchNewStatus.FormattingEnabled = true;
            this.cmbBatchNewStatus.Location = new System.Drawing.Point(335, 22);
            this.cmbBatchNewStatus.Name = "cmbBatchNewStatus";
            this.cmbBatchNewStatus.Size = new System.Drawing.Size(137, 21);
            this.cmbBatchNewStatus.TabIndex = 38;
            this.cmbBatchNewStatus.SelectedIndexChanged += new System.EventHandler(this.cmbBatchNewStatus_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(267, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "New Status";
            // 
            // cmbBatchCurrentStatus
            // 
            this.cmbBatchCurrentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchCurrentStatus.FormattingEnabled = true;
            this.cmbBatchCurrentStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive",
            "Terminated",
            "Paid Off"});
            this.cmbBatchCurrentStatus.Location = new System.Drawing.Point(96, 22);
            this.cmbBatchCurrentStatus.Name = "cmbBatchCurrentStatus";
            this.cmbBatchCurrentStatus.Size = new System.Drawing.Size(137, 21);
            this.cmbBatchCurrentStatus.TabIndex = 36;
            this.cmbBatchCurrentStatus.SelectedIndexChanged += new System.EventHandler(this.cmbBatchCurrentStatus_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Current Status";
            // 
            // gvlist
            // 
            this.gvlist.AllowUserToAddRows = false;
            this.gvlist.AllowUserToDeleteRows = false;
            this.gvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvlist.Location = new System.Drawing.Point(15, 77);
            this.gvlist.Name = "gvlist";
            this.gvlist.Size = new System.Drawing.Size(930, 169);
            this.gvlist.TabIndex = 0;
            // 
            // gbEmpWise
            // 
            this.gbEmpWise.Controls.Add(this.txtLastRate);
            this.gbEmpWise.Controls.Add(this.label12);
            this.gbEmpWise.Controls.Add(this.txtLastDate);
            this.gbEmpWise.Controls.Add(this.lblLastWorkDate);
            this.gbEmpWise.Controls.Add(this.lblEmpName);
            this.gbEmpWise.Controls.Add(this.txtReason);
            this.gbEmpWise.Controls.Add(this.label6);
            this.gbEmpWise.Controls.Add(this.rbtnInactive);
            this.gbEmpWise.Controls.Add(this.rbtnActive);
            this.gbEmpWise.Controls.Add(this.cmbNewStatus);
            this.gbEmpWise.Controls.Add(this.txtCurrentStatus);
            this.gbEmpWise.Controls.Add(this.label5);
            this.gbEmpWise.Controls.Add(this.label4);
            this.gbEmpWise.Controls.Add(this.txtEmpNo);
            this.gbEmpWise.Controls.Add(this.label2);
            this.gbEmpWise.Location = new System.Drawing.Point(19, 63);
            this.gbEmpWise.Name = "gbEmpWise";
            this.gbEmpWise.Size = new System.Drawing.Size(730, 102);
            this.gbEmpWise.TabIndex = 28;
            this.gbEmpWise.TabStop = false;
            this.gbEmpWise.Text = "Employee Wise Status Change";
            // 
            // txtLastRate
            // 
            this.txtLastRate.Location = new System.Drawing.Point(571, 70);
            this.txtLastRate.Name = "txtLastRate";
            this.txtLastRate.Size = new System.Drawing.Size(122, 20);
            this.txtLastRate.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(489, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 41;
            this.label12.Text = "LastWorkRate";
            // 
            // txtLastDate
            // 
            this.txtLastDate.Location = new System.Drawing.Point(571, 44);
            this.txtLastDate.Name = "txtLastDate";
            this.txtLastDate.Size = new System.Drawing.Size(122, 20);
            this.txtLastDate.TabIndex = 40;
            // 
            // lblLastWorkDate
            // 
            this.lblLastWorkDate.AutoSize = true;
            this.lblLastWorkDate.Location = new System.Drawing.Point(489, 47);
            this.lblLastWorkDate.Name = "lblLastWorkDate";
            this.lblLastWorkDate.Size = new System.Drawing.Size(76, 13);
            this.lblLastWorkDate.TabIndex = 39;
            this.lblLastWorkDate.Text = "LastWorkDate";
            // 
            // lblEmpName
            // 
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.Location = new System.Drawing.Point(272, 22);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(16, 13);
            this.lblEmpName.TabIndex = 38;
            this.lblEmpName.Text = "...";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(96, 71);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(376, 20);
            this.txtReason.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Reason";
            // 
            // rbtnInactive
            // 
            this.rbtnInactive.AutoSize = true;
            this.rbtnInactive.Enabled = false;
            this.rbtnInactive.Location = new System.Drawing.Point(557, 22);
            this.rbtnInactive.Name = "rbtnInactive";
            this.rbtnInactive.Size = new System.Drawing.Size(63, 17);
            this.rbtnInactive.TabIndex = 32;
            this.rbtnInactive.TabStop = true;
            this.rbtnInactive.Text = "Inactive";
            this.rbtnInactive.UseVisualStyleBackColor = true;
            // 
            // rbtnActive
            // 
            this.rbtnActive.AutoSize = true;
            this.rbtnActive.Enabled = false;
            this.rbtnActive.Location = new System.Drawing.Point(492, 22);
            this.rbtnActive.Name = "rbtnActive";
            this.rbtnActive.Size = new System.Drawing.Size(55, 17);
            this.rbtnActive.TabIndex = 31;
            this.rbtnActive.TabStop = true;
            this.rbtnActive.Text = "Active";
            this.rbtnActive.UseVisualStyleBackColor = true;
            // 
            // cmbNewStatus
            // 
            this.cmbNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewStatus.FormattingEnabled = true;
            this.cmbNewStatus.Location = new System.Drawing.Point(335, 44);
            this.cmbNewStatus.Name = "cmbNewStatus";
            this.cmbNewStatus.Size = new System.Drawing.Size(137, 21);
            this.cmbNewStatus.TabIndex = 30;
            this.cmbNewStatus.SelectedIndexChanged += new System.EventHandler(this.cmbNewStatus_SelectedIndexChanged_1);
            // 
            // txtCurrentStatus
            // 
            this.txtCurrentStatus.Enabled = false;
            this.txtCurrentStatus.Location = new System.Drawing.Point(96, 45);
            this.txtCurrentStatus.Name = "txtCurrentStatus";
            this.txtCurrentStatus.Size = new System.Drawing.Size(140, 20);
            this.txtCurrentStatus.TabIndex = 29;
            this.txtCurrentStatus.TextChanged += new System.EventHandler(this.txtCurrentStatus_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "New Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Current Status";
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Location = new System.Drawing.Point(96, 19);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(140, 20);
            this.txtEmpNo.TabIndex = 28;
            this.txtEmpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpNo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Employee";
            // 
            // btnAboveToChange
            // 
            this.btnAboveToChange.Location = new System.Drawing.Point(402, 13);
            this.btnAboveToChange.Name = "btnAboveToChange";
            this.btnAboveToChange.Size = new System.Drawing.Size(202, 23);
            this.btnAboveToChange.TabIndex = 3;
            this.btnAboveToChange.Text = "Employees Above To Inactive";
            this.btnAboveToChange.UseVisualStyleBackColor = true;
            this.btnAboveToChange.Click += new System.EventHandler(this.btnAboveToChange_Click);
            // 
            // EmployeeStatusChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 512);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeStatusChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Status Change";
            this.Load += new System.EventHandler(this.EmployeeStatusChange_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbBatchWise.ResumeLayout(false);
            this.gbBatchWise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvlist)).EndInit();
            this.gbEmpWise.ResumeLayout(false);
            this.gbEmpWise.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEstate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox gbEmpWise;
        private System.Windows.Forms.Label lblEmpName;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbtnInactive;
        private System.Windows.Forms.RadioButton rbtnActive;
        private System.Windows.Forms.ComboBox cmbNewStatus;
        private System.Windows.Forms.TextBox txtCurrentStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtEmpNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbBatchWise;
        private System.Windows.Forms.DataGridView gvlist;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbtnBatchWise;
        private System.Windows.Forms.RadioButton rbtnEmpWise;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkAllEmp;
        private System.Windows.Forms.TextBox txtBatchWiseReason;
        private System.Windows.Forms.ComboBox cmbBatchNewStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBatchCurrentStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAutoProcess;
        private System.Windows.Forms.Label lblInactiveDuration;
        private System.Windows.Forms.CheckBox chkAllDiv;
        private System.Windows.Forms.Label lblTerminateDuration;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblLastWorkDate;
        private System.Windows.Forms.TextBox txtLastRate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLastDate;
        private System.Windows.Forms.Button btnAboveToChange;
    }
}