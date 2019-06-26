namespace FTSPayroll
{
    partial class EmployeeDeduction
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
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkPeriodYesNo = new System.Windows.Forms.CheckBox();
            this.cmbDeduction = new System.Windows.Forms.ComboBox();
            this.gbPeriod = new System.Windows.Forms.GroupBox();
            this.dtpPeriodTo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpPeriodFrom = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGroupBox = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gvEmployeeDeduction = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEmpNO = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbPeriod.SuspendLayout();
            this.btnGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployeeDeduction)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRefNo);
            this.panel1.Controls.Add(this.txtAmount);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.chkPeriodYesNo);
            this.panel1.Controls.Add(this.cmbDeduction);
            this.panel1.Controls.Add(this.gbPeriod);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnGroupBox);
            this.panel1.Controls.Add(this.gvEmployeeDeduction);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.cmbCategory);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtYear);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(7, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 493);
            this.panel1.TabIndex = 1;
            // 
            // lblRefNo
            // 
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Location = new System.Drawing.Point(352, 249);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(0, 13);
            this.lblRefNo.TabIndex = 24;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(273, 143);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 20);
            this.txtAmount.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Amount";
            // 
            // chkPeriodYesNo
            // 
            this.chkPeriodYesNo.AutoSize = true;
            this.chkPeriodYesNo.Location = new System.Drawing.Point(220, 65);
            this.chkPeriodYesNo.Name = "chkPeriodYesNo";
            this.chkPeriodYesNo.Size = new System.Drawing.Size(74, 17);
            this.chkPeriodYesNo.TabIndex = 4;
            this.chkPeriodYesNo.Text = "For Period";
            this.chkPeriodYesNo.UseVisualStyleBackColor = true;
            this.chkPeriodYesNo.CheckedChanged += new System.EventHandler(this.chkPeriodYesNo_CheckedChanged);
            // 
            // cmbDeduction
            // 
            this.cmbDeduction.FormattingEnabled = true;
            this.cmbDeduction.Location = new System.Drawing.Point(82, 140);
            this.cmbDeduction.Name = "cmbDeduction";
            this.cmbDeduction.Size = new System.Drawing.Size(121, 21);
            this.cmbDeduction.TabIndex = 5;
            // 
            // gbPeriod
            // 
            this.gbPeriod.Controls.Add(this.dtpPeriodTo);
            this.gbPeriod.Controls.Add(this.label10);
            this.gbPeriod.Controls.Add(this.dtpPeriodFrom);
            this.gbPeriod.Controls.Add(this.label9);
            this.gbPeriod.Location = new System.Drawing.Point(8, 88);
            this.gbPeriod.Name = "gbPeriod";
            this.gbPeriod.Size = new System.Drawing.Size(403, 46);
            this.gbPeriod.TabIndex = 22;
            this.gbPeriod.TabStop = false;
            this.gbPeriod.Text = "Period";
            // 
            // dtpPeriodTo
            // 
            this.dtpPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPeriodTo.Location = new System.Drawing.Point(265, 16);
            this.dtpPeriodTo.Name = "dtpPeriodTo";
            this.dtpPeriodTo.Size = new System.Drawing.Size(107, 20);
            this.dtpPeriodTo.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(211, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "To";
            // 
            // dtpPeriodFrom
            // 
            this.dtpPeriodFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPeriodFrom.Location = new System.Drawing.Point(81, 16);
            this.dtpPeriodFrom.Name = "dtpPeriodFrom";
            this.dtpPeriodFrom.Size = new System.Drawing.Size(107, 20);
            this.dtpPeriodFrom.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Deduction";
            // 
            // btnGroupBox
            // 
            this.btnGroupBox.Controls.Add(this.btnCancel);
            this.btnGroupBox.Controls.Add(this.btnDelete);
            this.btnGroupBox.Controls.Add(this.btnUpdate);
            this.btnGroupBox.Controls.Add(this.btnAdd);
            this.btnGroupBox.Location = new System.Drawing.Point(8, 236);
            this.btnGroupBox.Name = "btnGroupBox";
            this.btnGroupBox.Size = new System.Drawing.Size(337, 40);
            this.btnGroupBox.TabIndex = 21;
            this.btnGroupBox.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(254, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(173, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(92, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(11, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gvEmployeeDeduction
            // 
            this.gvEmployeeDeduction.AllowUserToAddRows = false;
            this.gvEmployeeDeduction.AllowUserToDeleteRows = false;
            this.gvEmployeeDeduction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmployeeDeduction.Location = new System.Drawing.Point(4, 280);
            this.gvEmployeeDeduction.Name = "gvEmployeeDeduction";
            this.gvEmployeeDeduction.ReadOnly = true;
            this.gvEmployeeDeduction.Size = new System.Drawing.Size(420, 205);
            this.gvEmployeeDeduction.TabIndex = 8;
            this.gvEmployeeDeduction.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmployeeDeduction_CellContentDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbEmpNO);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(8, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 46);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(252, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(152, 20);
            this.txtName.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(203, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Name";
            // 
            // cmbEmpNO
            // 
            this.cmbEmpNO.FormattingEnabled = true;
            this.cmbEmpNO.Location = new System.Drawing.Point(68, 18);
            this.cmbEmpNO.Name = "cmbEmpNO";
            this.cmbEmpNO.Size = new System.Drawing.Size(127, 21);
            this.cmbEmpNO.TabIndex = 0;
            this.cmbEmpNO.SelectedIndexChanged += new System.EventHandler(this.cmbEmpNO_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Emp NO";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(15, 169);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(107, 17);
            this.chkAll.TabIndex = 7;
            this.chkAll.Text = "for All in category";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(266, 7);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Category";
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(82, 7);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Division";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(82, 62);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(121, 20);
            this.txtYear.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Year";
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(82, 35);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Month";
            // 
            // EmployeeDeduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 500);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeDeduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeDeduction";
            this.Load += new System.EventHandler(this.EmployeeDeduction_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbPeriod.ResumeLayout(false);
            this.gbPeriod.PerformLayout();
            this.btnGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployeeDeduction)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvEmployeeDeduction;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDeduction;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox btnGroupBox;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gbPeriod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpPeriodTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpPeriodFrom;
        private System.Windows.Forms.CheckBox chkPeriodYesNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbEmpNO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRefNo;
    }
}