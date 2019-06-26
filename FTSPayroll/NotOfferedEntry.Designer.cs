namespace FTSPayroll
{
    partial class NotOfferedEntry
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
            this.lblRefNo = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkETF = new System.Windows.Forms.CheckBox();
            this.chkDailyPay = new System.Windows.Forms.CheckBox();
            this.chkEPF = new System.Windows.Forms.CheckBox();
            this.gvNotOffered = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmbNotOfferedCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEmpNo = new System.Windows.Forms.ComboBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.gbPeriod = new System.Windows.Forms.GroupBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDivision = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotOffered)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRefNo);
            this.panel1.Controls.Add(this.cmbCategory);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmdUpdate);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.gvNotOffered);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.cmbNotOfferedCode);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbEmpNo);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.txtEmpName);
            this.panel1.Controls.Add(this.gbPeriod);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblDivision);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 416);
            this.panel1.TabIndex = 0;
            // 
            // lblRefNo
            // 
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Location = new System.Drawing.Point(250, 133);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(0, 13);
            this.lblRefNo.TabIndex = 14;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(90, 36);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(136, 21);
            this.cmbCategory.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Emp Category";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Enabled = false;
            this.cmdUpdate.Location = new System.Drawing.Point(377, 123);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
            this.cmdUpdate.TabIndex = 1;
            this.cmdUpdate.Text = "UPDATE";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Visible = false;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkETF);
            this.groupBox1.Controls.Add(this.chkDailyPay);
            this.groupBox1.Controls.Add(this.chkEPF);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(10, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Eligible For";
            // 
            // chkETF
            // 
            this.chkETF.AutoSize = true;
            this.chkETF.Location = new System.Drawing.Point(73, 19);
            this.chkETF.Name = "chkETF";
            this.chkETF.Size = new System.Drawing.Size(46, 17);
            this.chkETF.TabIndex = 1;
            this.chkETF.Text = "ETF";
            this.chkETF.UseVisualStyleBackColor = true;
            // 
            // chkDailyPay
            // 
            this.chkDailyPay.AutoSize = true;
            this.chkDailyPay.Location = new System.Drawing.Point(134, 19);
            this.chkDailyPay.Name = "chkDailyPay";
            this.chkDailyPay.Size = new System.Drawing.Size(75, 17);
            this.chkDailyPay.TabIndex = 2;
            this.chkDailyPay.Text = "Name Pay";
            this.chkDailyPay.UseVisualStyleBackColor = true;
            // 
            // chkEPF
            // 
            this.chkEPF.AutoSize = true;
            this.chkEPF.Location = new System.Drawing.Point(9, 19);
            this.chkEPF.Name = "chkEPF";
            this.chkEPF.Size = new System.Drawing.Size(46, 17);
            this.chkEPF.TabIndex = 0;
            this.chkEPF.Text = "EPF";
            this.chkEPF.UseVisualStyleBackColor = true;
            // 
            // gvNotOffered
            // 
            this.gvNotOffered.AllowUserToAddRows = false;
            this.gvNotOffered.AllowUserToDeleteRows = false;
            this.gvNotOffered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvNotOffered.Location = new System.Drawing.Point(10, 218);
            this.gvNotOffered.Name = "gvNotOffered";
            this.gvNotOffered.ReadOnly = true;
            this.gvNotOffered.Size = new System.Drawing.Size(452, 186);
            this.gvNotOffered.TabIndex = 4;
            this.gvNotOffered.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvNotOffered_CellContentDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdDelete);
            this.groupBox2.Controls.Add(this.cmdClose);
            this.groupBox2.Controls.Add(this.cmdClear);
            this.groupBox2.Controls.Add(this.cmdAdd);
            this.groupBox2.Location = new System.Drawing.Point(10, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 47);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(87, 18);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "DELETE";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(249, 18);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(168, 18);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 3;
            this.cmdClear.Text = "CLEAR";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(6, 18);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 0;
            this.cmdAdd.Text = "ADD";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmbNotOfferedCode
            // 
            this.cmbNotOfferedCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNotOfferedCode.FormattingEnabled = true;
            this.cmbNotOfferedCode.Location = new System.Drawing.Point(348, 92);
            this.cmbNotOfferedCode.Name = "cmbNotOfferedCode";
            this.cmbNotOfferedCode.Size = new System.Drawing.Size(104, 21);
            this.cmbNotOfferedCode.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Not Offered Code";
            // 
            // cmbEmpNo
            // 
            this.cmbEmpNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpNo.FormattingEnabled = true;
            this.cmbEmpNo.Location = new System.Drawing.Point(90, 61);
            this.cmbEmpNo.Name = "cmbEmpNo";
            this.cmbEmpNo.Size = new System.Drawing.Size(136, 21);
            this.cmbEmpNo.TabIndex = 2;
            this.cmbEmpNo.SelectedIndexChanged += new System.EventHandler(this.cmbEmpNo_SelectedIndexChanged);
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(90, 11);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(136, 21);
            this.cmbDivision.TabIndex = 0;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // txtEmpName
            // 
            this.txtEmpName.Enabled = false;
            this.txtEmpName.Location = new System.Drawing.Point(90, 86);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(136, 20);
            this.txtEmpName.TabIndex = 3;
            // 
            // gbPeriod
            // 
            this.gbPeriod.Controls.Add(this.dtpToDate);
            this.gbPeriod.Controls.Add(this.dtpFromDate);
            this.gbPeriod.Controls.Add(this.label4);
            this.gbPeriod.Controls.Add(this.label3);
            this.gbPeriod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbPeriod.Location = new System.Drawing.Point(240, 11);
            this.gbPeriod.Name = "gbPeriod";
            this.gbPeriod.Size = new System.Drawing.Size(222, 75);
            this.gbPeriod.TabIndex = 1;
            this.gbPeriod.TabStop = false;
            this.gbPeriod.Text = "Date Period";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(99, 44);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(113, 20);
            this.dtpToDate.TabIndex = 1;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(99, 20);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(113, 20);
            this.dtpFromDate.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "To Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Emp Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "EmpNo";
            // 
            // lblDivision
            // 
            this.lblDivision.AutoSize = true;
            this.lblDivision.Location = new System.Drawing.Point(16, 11);
            this.lblDivision.Name = "lblDivision";
            this.lblDivision.Size = new System.Drawing.Size(44, 13);
            this.lblDivision.TabIndex = 0;
            this.lblDivision.Text = "Division";
            // 
            // NotOfferedEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 422);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotOfferedEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Not Offered Entry";
            this.Load += new System.EventHandler(this.NotOfferedEntry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotOffered)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.gbPeriod.ResumeLayout(false);
            this.gbPeriod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbEmpNo;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.GroupBox gbPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDivision;
        private System.Windows.Forms.ComboBox cmbNotOfferedCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.DataGridView gvNotOffered;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkETF;
        private System.Windows.Forms.CheckBox chkDailyPay;
        private System.Windows.Forms.CheckBox chkEPF;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblRefNo;
    }
}