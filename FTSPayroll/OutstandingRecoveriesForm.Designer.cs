namespace FTSPayroll
{
    partial class OutstandingRecoveriesForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbDirectPay = new System.Windows.Forms.GroupBox();
            this.dtpDP = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.btnDirectPayment = new System.Windows.Forms.Button();
            this.txtPayReason = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPayAmount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.gbTerminate = new System.Windows.Forms.GroupBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnTerminate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRefNo = new System.Windows.Forms.Label();
            this.txtBalanceAmount = new System.Windows.Forms.TextBox();
            this.txtNoofInstallments = new System.Windows.Forms.TextBox();
            this.txtDeductAmount = new System.Windows.Forms.TextBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvFixedDeductions = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.txt_employeeNo = new System.Windows.Forms.TextBox();
            this.chkAllEmployee = new System.Windows.Forms.CheckBox();
            this.cmbDeductionGroup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAllDeduction = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAllDivision = new System.Windows.Forms.CheckBox();
            this.cmbDeductCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSelectedGroup = new System.Windows.Forms.ComboBox();
            this.cmbSelectedCode = new System.Windows.Forms.ComboBox();
            this.txtSelectedEmp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbDirectPay.SuspendLayout();
            this.gbTerminate.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFixedDeductions)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.gbDirectPay);
            this.panel1.Controls.Add(this.gbTerminate);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 517);
            this.panel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(15, 251);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(178, 51);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(90, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbDirectPay
            // 
            this.gbDirectPay.Controls.Add(this.dtpDP);
            this.gbDirectPay.Controls.Add(this.label18);
            this.gbDirectPay.Controls.Add(this.btnDirectPayment);
            this.gbDirectPay.Controls.Add(this.txtPayReason);
            this.gbDirectPay.Controls.Add(this.label17);
            this.gbDirectPay.Controls.Add(this.txtRefNo);
            this.gbDirectPay.Controls.Add(this.label16);
            this.gbDirectPay.Controls.Add(this.txtPayAmount);
            this.gbDirectPay.Controls.Add(this.label15);
            this.gbDirectPay.Enabled = false;
            this.gbDirectPay.Location = new System.Drawing.Point(362, 143);
            this.gbDirectPay.Name = "gbDirectPay";
            this.gbDirectPay.Size = new System.Drawing.Size(341, 107);
            this.gbDirectPay.TabIndex = 29;
            this.gbDirectPay.TabStop = false;
            this.gbDirectPay.Text = "Direct Payments";
            // 
            // dtpDP
            // 
            this.dtpDP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDP.Location = new System.Drawing.Point(68, 27);
            this.dtpDP.Name = "dtpDP";
            this.dtpDP.Size = new System.Drawing.Size(99, 20);
            this.dtpDP.TabIndex = 19;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(30, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "Date";
            // 
            // btnDirectPayment
            // 
            this.btnDirectPayment.Location = new System.Drawing.Point(240, 78);
            this.btnDirectPayment.Name = "btnDirectPayment";
            this.btnDirectPayment.Size = new System.Drawing.Size(70, 23);
            this.btnDirectPayment.TabIndex = 12;
            this.btnDirectPayment.Text = "Pay";
            this.btnDirectPayment.UseVisualStyleBackColor = true;
            // 
            // txtPayReason
            // 
            this.txtPayReason.Location = new System.Drawing.Point(68, 53);
            this.txtPayReason.Name = "txtPayReason";
            this.txtPayReason.Size = new System.Drawing.Size(101, 20);
            this.txtPayReason.TabIndex = 17;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "Reason";
            // 
            // txtRefNo
            // 
            this.txtRefNo.Location = new System.Drawing.Point(240, 53);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(68, 20);
            this.txtRefNo.TabIndex = 15;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(185, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Ref. No";
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.Location = new System.Drawing.Point(240, 27);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Size = new System.Drawing.Size(70, 20);
            this.txtPayAmount.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(191, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Amount";
            // 
            // gbTerminate
            // 
            this.gbTerminate.Controls.Add(this.txtReason);
            this.gbTerminate.Controls.Add(this.label14);
            this.gbTerminate.Controls.Add(this.btnTerminate);
            this.gbTerminate.Location = new System.Drawing.Point(15, 143);
            this.gbTerminate.Name = "gbTerminate";
            this.gbTerminate.Size = new System.Drawing.Size(341, 107);
            this.gbTerminate.TabIndex = 28;
            this.gbTerminate.TabStop = false;
            this.gbTerminate.Text = "Terminate Fixed Deduction";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(108, 27);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(101, 20);
            this.txtReason.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Reason";
            // 
            // btnTerminate
            // 
            this.btnTerminate.Location = new System.Drawing.Point(108, 54);
            this.btnTerminate.Name = "btnTerminate";
            this.btnTerminate.Size = new System.Drawing.Size(70, 23);
            this.btnTerminate.TabIndex = 5;
            this.btnTerminate.Text = "Terminate";
            this.btnTerminate.UseVisualStyleBackColor = true;
            this.btnTerminate.Click += new System.EventHandler(this.btnTerminate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSelectedEmp);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.cmbSelectedCode);
            this.groupBox3.Controls.Add(this.cmbSelectedGroup);
            this.groupBox3.Controls.Add(this.lblRefNo);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtBalanceAmount);
            this.groupBox3.Controls.Add(this.txtNoofInstallments);
            this.groupBox3.Controls.Add(this.txtDeductAmount);
            this.groupBox3.Controls.Add(this.txtMonth);
            this.groupBox3.Controls.Add(this.txtYear);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(359, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 127);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selected";
            // 
            // lblRefNo
            // 
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Location = new System.Drawing.Point(140, 101);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(13, 13);
            this.lblRefNo.TabIndex = 14;
            this.lblRefNo.Text = "0";
            // 
            // txtBalanceAmount
            // 
            this.txtBalanceAmount.Enabled = false;
            this.txtBalanceAmount.Location = new System.Drawing.Point(272, 68);
            this.txtBalanceAmount.Name = "txtBalanceAmount";
            this.txtBalanceAmount.Size = new System.Drawing.Size(56, 20);
            this.txtBalanceAmount.TabIndex = 11;
            // 
            // txtNoofInstallments
            // 
            this.txtNoofInstallments.Enabled = false;
            this.txtNoofInstallments.Location = new System.Drawing.Point(81, 98);
            this.txtNoofInstallments.Name = "txtNoofInstallments";
            this.txtNoofInstallments.Size = new System.Drawing.Size(34, 20);
            this.txtNoofInstallments.TabIndex = 10;
            // 
            // txtDeductAmount
            // 
            this.txtDeductAmount.Location = new System.Drawing.Point(272, 42);
            this.txtDeductAmount.Name = "txtDeductAmount";
            this.txtDeductAmount.Size = new System.Drawing.Size(56, 20);
            this.txtDeductAmount.TabIndex = 9;
            // 
            // txtMonth
            // 
            this.txtMonth.Enabled = false;
            this.txtMonth.Location = new System.Drawing.Point(272, 17);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(56, 20);
            this.txtMonth.TabIndex = 7;
            // 
            // txtYear
            // 
            this.txtYear.Enabled = false;
            this.txtYear.Location = new System.Drawing.Point(81, 19);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(98, 20);
            this.txtYear.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "No Of Insta.";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(185, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Balance Amount";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(185, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "DeductAmount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Start Month";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start Year";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvFixedDeductions);
            this.groupBox2.Location = new System.Drawing.Point(15, 304);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(964, 203);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // gvFixedDeductions
            // 
            this.gvFixedDeductions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFixedDeductions.Location = new System.Drawing.Point(9, 19);
            this.gvFixedDeductions.Name = "gvFixedDeductions";
            this.gvFixedDeductions.Size = new System.Drawing.Size(949, 178);
            this.gvFixedDeductions.TabIndex = 0;
            this.gvFixedDeductions.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFixedDeductions_CellContentDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbDivision);
            this.groupBox1.Controls.Add(this.txt_employeeNo);
            this.groupBox1.Controls.Add(this.chkAllEmployee);
            this.groupBox1.Controls.Add(this.cmbDeductionGroup);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkAllDeduction);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkAllDivision);
            this.groupBox1.Controls.Add(this.cmbDeductCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 127);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(115, 19);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 14;
            // 
            // txt_employeeNo
            // 
            this.txt_employeeNo.Location = new System.Drawing.Point(115, 100);
            this.txt_employeeNo.Name = "txt_employeeNo";
            this.txt_employeeNo.Size = new System.Drawing.Size(121, 20);
            this.txt_employeeNo.TabIndex = 22;
            this.txt_employeeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_employeeNo_KeyPress);
            // 
            // chkAllEmployee
            // 
            this.chkAllEmployee.AutoSize = true;
            this.chkAllEmployee.Location = new System.Drawing.Point(247, 100);
            this.chkAllEmployee.Name = "chkAllEmployee";
            this.chkAllEmployee.Size = new System.Drawing.Size(91, 17);
            this.chkAllEmployee.TabIndex = 19;
            this.chkAllEmployee.Text = "All Employees";
            this.chkAllEmployee.UseVisualStyleBackColor = true;
            // 
            // cmbDeductionGroup
            // 
            this.cmbDeductionGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductionGroup.FormattingEnabled = true;
            this.cmbDeductionGroup.Location = new System.Drawing.Point(115, 46);
            this.cmbDeductionGroup.Name = "cmbDeductionGroup";
            this.cmbDeductionGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbDeductionGroup.TabIndex = 24;
            this.cmbDeductionGroup.SelectedIndexChanged += new System.EventHandler(this.cmbDeductionGroup_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "EmployeeNo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Deduction Group";
            // 
            // chkAllDeduction
            // 
            this.chkAllDeduction.AutoSize = true;
            this.chkAllDeduction.Location = new System.Drawing.Point(247, 73);
            this.chkAllDeduction.Name = "chkAllDeduction";
            this.chkAllDeduction.Size = new System.Drawing.Size(94, 17);
            this.chkAllDeduction.TabIndex = 20;
            this.chkAllDeduction.Text = "All Deductions";
            this.chkAllDeduction.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Division";
            // 
            // chkAllDivision
            // 
            this.chkAllDivision.AutoSize = true;
            this.chkAllDivision.Location = new System.Drawing.Point(247, 19);
            this.chkAllDivision.Name = "chkAllDivision";
            this.chkAllDivision.Size = new System.Drawing.Size(37, 17);
            this.chkAllDivision.TabIndex = 21;
            this.chkAllDivision.Text = "All";
            this.chkAllDivision.UseVisualStyleBackColor = true;
            // 
            // cmbDeductCode
            // 
            this.cmbDeductCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductCode.FormattingEnabled = true;
            this.cmbDeductCode.Location = new System.Drawing.Point(115, 73);
            this.cmbDeductCode.Name = "cmbDeductCode";
            this.cmbDeductCode.Size = new System.Drawing.Size(121, 21);
            this.cmbDeductCode.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Deduction Code";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Deduct Code";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Deduct Group";
            // 
            // cmbSelectedGroup
            // 
            this.cmbSelectedGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectedGroup.FormattingEnabled = true;
            this.cmbSelectedGroup.Location = new System.Drawing.Point(81, 45);
            this.cmbSelectedGroup.Name = "cmbSelectedGroup";
            this.cmbSelectedGroup.Size = new System.Drawing.Size(98, 21);
            this.cmbSelectedGroup.TabIndex = 25;
            this.cmbSelectedGroup.SelectedIndexChanged += new System.EventHandler(this.cmbSelectedGroup_SelectedIndexChanged);
            // 
            // cmbSelectedCode
            // 
            this.cmbSelectedCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectedCode.FormattingEnabled = true;
            this.cmbSelectedCode.Location = new System.Drawing.Point(81, 72);
            this.cmbSelectedCode.Name = "cmbSelectedCode";
            this.cmbSelectedCode.Size = new System.Drawing.Size(98, 21);
            this.cmbSelectedCode.TabIndex = 26;
            // 
            // txtSelectedEmp
            // 
            this.txtSelectedEmp.Enabled = false;
            this.txtSelectedEmp.Location = new System.Drawing.Point(272, 94);
            this.txtSelectedEmp.Name = "txtSelectedEmp";
            this.txtSelectedEmp.Size = new System.Drawing.Size(56, 20);
            this.txtSelectedEmp.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(185, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "EmpNo";
            // 
            // OutstandingRecoveriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 522);
            this.Controls.Add(this.panel1);
            this.Name = "OutstandingRecoveriesForm";
            this.Text = "Outstanding Recoveries ";
            this.Load += new System.EventHandler(this.OutstandingRecoveriesForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gbDirectPay.ResumeLayout(false);
            this.gbDirectPay.PerformLayout();
            this.gbTerminate.ResumeLayout(false);
            this.gbTerminate.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvFixedDeductions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gvFixedDeductions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.CheckBox chkAllEmployee;
        private System.Windows.Forms.ComboBox cmbDeductionGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAllDeduction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAllDivision;
        private System.Windows.Forms.ComboBox cmbDeductCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBalanceAmount;
        private System.Windows.Forms.TextBox txtNoofInstallments;
        private System.Windows.Forms.TextBox txtDeductAmount;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.GroupBox gbTerminate;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnTerminate;
        private System.Windows.Forms.GroupBox gbDirectPay;
        private System.Windows.Forms.DateTimePicker dtpDP;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnDirectPayment;
        private System.Windows.Forms.TextBox txtPayReason;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPayAmount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblRefNo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txt_employeeNo;
        private System.Windows.Forms.ComboBox cmbSelectedCode;
        private System.Windows.Forms.ComboBox cmbSelectedGroup;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSelectedEmp;
        private System.Windows.Forms.Label label12;
    }
}