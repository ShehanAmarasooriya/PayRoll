namespace FTSPayroll
{
    partial class FixedDeductions
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
            this.chkDirectPay = new System.Windows.Forms.CheckBox();
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
            this.lblTotal = new System.Windows.Forms.Label();
            this.gbTerminate = new System.Windows.Forms.GroupBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnTerminate = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtG2EmpName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbG2Division = new System.Windows.Forms.ComboBox();
            this.txtG2EmpNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtG1EmpName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbG1Division = new System.Windows.Forms.ComboBox();
            this.txtG1EmpNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gvFixedDeductions = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFixed = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRefNo = new System.Windows.Forms.Label();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkGurantorRec = new System.Windows.Forms.CheckBox();
            this.btnEmpSearch = new System.Windows.Forms.Button();
            this.txtEmpNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNoOfMonths = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDeductAmount = new System.Windows.Forms.TextBox();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDeduction = new System.Windows.Forms.Label();
            this.lblDeductionGroup = new System.Windows.Forms.Label();
            this.cmbDeductions = new System.Windows.Forms.ComboBox();
            this.cmbDeductionGroup = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.gbDirectPay.SuspendLayout();
            this.gbTerminate.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFixedDeductions)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDirectPay);
            this.panel1.Controls.Add(this.gbDirectPay);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.gbTerminate);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.gvFixedDeductions);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Location = new System.Drawing.Point(7, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 501);
            this.panel1.TabIndex = 0;
            // 
            // chkDirectPay
            // 
            this.chkDirectPay.AutoSize = true;
            this.chkDirectPay.Enabled = false;
            this.chkDirectPay.Location = new System.Drawing.Point(753, 120);
            this.chkDirectPay.Name = "chkDirectPay";
            this.chkDirectPay.Size = new System.Drawing.Size(98, 17);
            this.chkDirectPay.TabIndex = 9;
            this.chkDirectPay.Text = "Direct Payment";
            this.chkDirectPay.UseVisualStyleBackColor = true;
            this.chkDirectPay.CheckedChanged += new System.EventHandler(this.chkDirectPay_CheckedChanged);
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
            this.gbDirectPay.Location = new System.Drawing.Point(744, 141);
            this.gbDirectPay.Name = "gbDirectPay";
            this.gbDirectPay.Size = new System.Drawing.Size(175, 161);
            this.gbDirectPay.TabIndex = 8;
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
            this.btnDirectPayment.Location = new System.Drawing.Point(99, 131);
            this.btnDirectPayment.Name = "btnDirectPayment";
            this.btnDirectPayment.Size = new System.Drawing.Size(70, 23);
            this.btnDirectPayment.TabIndex = 12;
            this.btnDirectPayment.Text = "Pay";
            this.btnDirectPayment.UseVisualStyleBackColor = true;
            this.btnDirectPayment.Click += new System.EventHandler(this.btnDirectPayment_Click);
            // 
            // txtPayReason
            // 
            this.txtPayReason.Location = new System.Drawing.Point(68, 105);
            this.txtPayReason.Name = "txtPayReason";
            this.txtPayReason.Size = new System.Drawing.Size(101, 20);
            this.txtPayReason.TabIndex = 17;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 106);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "Reason";
            // 
            // txtRefNo
            // 
            this.txtRefNo.Location = new System.Drawing.Point(68, 79);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(101, 20);
            this.txtRefNo.TabIndex = 15;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Ref. No";
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.Location = new System.Drawing.Point(68, 53);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Size = new System.Drawing.Size(101, 20);
            this.txtPayAmount.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Amount";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(642, 351);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(30, 25);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "...";
            // 
            // gbTerminate
            // 
            this.gbTerminate.Controls.Add(this.txtReason);
            this.gbTerminate.Controls.Add(this.label14);
            this.gbTerminate.Controls.Add(this.btnTerminate);
            this.gbTerminate.Location = new System.Drawing.Point(744, 4);
            this.gbTerminate.Name = "gbTerminate";
            this.gbTerminate.Size = new System.Drawing.Size(177, 110);
            this.gbTerminate.TabIndex = 6;
            this.gbTerminate.TabStop = false;
            this.gbTerminate.Text = "Terminate Fixed Deduction";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(70, 45);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(101, 20);
            this.txtReason.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Reason";
            // 
            // btnTerminate
            // 
            this.btnTerminate.Location = new System.Drawing.Point(101, 78);
            this.btnTerminate.Name = "btnTerminate";
            this.btnTerminate.Size = new System.Drawing.Size(70, 23);
            this.btnTerminate.TabIndex = 5;
            this.btnTerminate.Text = "Terminate";
            this.btnTerminate.UseVisualStyleBackColor = true;
            this.btnTerminate.Click += new System.EventHandler(this.btnTerminate_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(12, 206);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(727, 126);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtG2EmpName);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.cmbG2Division);
            this.groupBox7.Controls.Add(this.txtG2EmpNo);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Location = new System.Drawing.Point(15, 72);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(697, 49);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Guarantor2";
            // 
            // txtG2EmpName
            // 
            this.txtG2EmpName.Location = new System.Drawing.Point(379, 22);
            this.txtG2EmpName.Name = "txtG2EmpName";
            this.txtG2EmpName.Size = new System.Drawing.Size(153, 20);
            this.txtG2EmpName.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Division";
            // 
            // cmbG2Division
            // 
            this.cmbG2Division.FormattingEnabled = true;
            this.cmbG2Division.Location = new System.Drawing.Point(82, 21);
            this.cmbG2Division.Name = "cmbG2Division";
            this.cmbG2Division.Size = new System.Drawing.Size(125, 21);
            this.cmbG2Division.TabIndex = 0;
            this.cmbG2Division.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbG2Division_KeyPress);
            // 
            // txtG2EmpNo
            // 
            this.txtG2EmpNo.Location = new System.Drawing.Point(280, 22);
            this.txtG2EmpNo.Name = "txtG2EmpNo";
            this.txtG2EmpNo.Size = new System.Drawing.Size(93, 20);
            this.txtG2EmpNo.TabIndex = 1;
            this.txtG2EmpNo.Leave += new System.EventHandler(this.txtG2EmpNo_Leave);
            this.txtG2EmpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtG2EmpNo_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(217, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "EmpNo";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtG1EmpName);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.cmbG1Division);
            this.groupBox6.Controls.Add(this.txtG1EmpNo);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Location = new System.Drawing.Point(10, 14);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(702, 52);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Guarantor1";
            // 
            // txtG1EmpName
            // 
            this.txtG1EmpName.Location = new System.Drawing.Point(384, 22);
            this.txtG1EmpName.Name = "txtG1EmpName";
            this.txtG1EmpName.Size = new System.Drawing.Size(160, 20);
            this.txtG1EmpName.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Division";
            // 
            // cmbG1Division
            // 
            this.cmbG1Division.FormattingEnabled = true;
            this.cmbG1Division.Location = new System.Drawing.Point(82, 21);
            this.cmbG1Division.Name = "cmbG1Division";
            this.cmbG1Division.Size = new System.Drawing.Size(125, 21);
            this.cmbG1Division.TabIndex = 0;
            this.cmbG1Division.SelectedIndexChanged += new System.EventHandler(this.cmbG1Division_SelectedIndexChanged);
            this.cmbG1Division.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbG1Division_KeyPress);
            // 
            // txtG1EmpNo
            // 
            this.txtG1EmpNo.Location = new System.Drawing.Point(285, 22);
            this.txtG1EmpNo.Name = "txtG1EmpNo";
            this.txtG1EmpNo.Size = new System.Drawing.Size(93, 20);
            this.txtG1EmpNo.TabIndex = 1;
            this.txtG1EmpNo.TextChanged += new System.EventHandler(this.txtG1EmpNo_TextChanged);
            this.txtG1EmpNo.Leave += new System.EventHandler(this.txtG1EmpNo_Leave);
            this.txtG1EmpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtG1EmpNo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "EmpNo";
            // 
            // gvFixedDeductions
            // 
            this.gvFixedDeductions.AllowUserToAddRows = false;
            this.gvFixedDeductions.AllowUserToDeleteRows = false;
            this.gvFixedDeductions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFixedDeductions.Location = new System.Drawing.Point(8, 384);
            this.gvFixedDeductions.Name = "gvFixedDeductions";
            this.gvFixedDeductions.ReadOnly = true;
            this.gvFixedDeductions.Size = new System.Drawing.Size(762, 110);
            this.gvFixedDeductions.TabIndex = 4;
            this.gvFixedDeductions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFixedDeductions_CellDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkFixed);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cmbMonth);
            this.groupBox3.Controls.Add(this.cmbYear);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(11, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(727, 41);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Starting Month";
            // 
            // chkFixed
            // 
            this.chkFixed.AutoSize = true;
            this.chkFixed.Location = new System.Drawing.Point(588, 19);
            this.chkFixed.Name = "chkFixed";
            this.chkFixed.Size = new System.Drawing.Size(51, 17);
            this.chkFixed.TabIndex = 2;
            this.chkFixed.Text = "Fixed";
            this.chkFixed.UseVisualStyleBackColor = true;
            this.chkFixed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkFixed_KeyPress_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Month";
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(359, 15);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(125, 21);
            this.cmbMonth.TabIndex = 1;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            this.cmbMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMonth_KeyPress);
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(110, 15);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(125, 21);
            this.cmbYear.TabIndex = 0;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            this.cmbYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbYear_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Year";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRefNo);
            this.groupBox2.Controls.Add(this.cmdDelete);
            this.groupBox2.Controls.Add(this.cmdClose);
            this.groupBox2.Controls.Add(this.cmdClear);
            this.groupBox2.Controls.Add(this.cmdUpdate);
            this.groupBox2.Controls.Add(this.cmdAdd);
            this.groupBox2.Location = new System.Drawing.Point(10, 331);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 47);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // lblRefNo
            // 
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Location = new System.Drawing.Point(460, 20);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(0, 13);
            this.lblRefNo.TabIndex = 5;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Enabled = false;
            this.cmdDelete.Location = new System.Drawing.Point(167, 18);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "DELETE";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(329, 18);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(248, 18);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 3;
            this.cmdClear.Text = "CLEAR";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Enabled = false;
            this.cmdUpdate.Location = new System.Drawing.Point(86, 18);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
            this.cmdUpdate.TabIndex = 1;
            this.cmdUpdate.Text = "UPDATE";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkGurantorRec);
            this.groupBox1.Controls.Add(this.btnEmpSearch);
            this.groupBox1.Controls.Add(this.txtEmpNo);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtNoOfMonths);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDeductAmount);
            this.groupBox1.Controls.Add(this.txtEmpName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(11, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(729, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ChkGurantorRec
            // 
            this.ChkGurantorRec.AutoSize = true;
            this.ChkGurantorRec.Location = new System.Drawing.Point(455, 42);
            this.ChkGurantorRec.Name = "ChkGurantorRec";
            this.ChkGurantorRec.Size = new System.Drawing.Size(124, 17);
            this.ChkGurantorRec.TabIndex = 5;
            this.ChkGurantorRec.Text = "Add Gurantor Details";
            this.ChkGurantorRec.UseVisualStyleBackColor = true;
            this.ChkGurantorRec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChkGurantorRec_KeyPress);
            this.ChkGurantorRec.CheckedChanged += new System.EventHandler(this.ChkGurantorRec_CheckedChanged);
            // 
            // btnEmpSearch
            // 
            this.btnEmpSearch.Enabled = false;
            this.btnEmpSearch.Location = new System.Drawing.Point(639, 13);
            this.btnEmpSearch.Name = "btnEmpSearch";
            this.btnEmpSearch.Size = new System.Drawing.Size(75, 23);
            this.btnEmpSearch.TabIndex = 1;
            this.btnEmpSearch.Text = "Search";
            this.btnEmpSearch.UseVisualStyleBackColor = true;
            this.btnEmpSearch.Visible = false;
            this.btnEmpSearch.Click += new System.EventHandler(this.btnEmpSearch_Click);
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Location = new System.Drawing.Point(111, 13);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(93, 20);
            this.txtEmpNo.TabIndex = 0;
            this.txtEmpNo.Leave += new System.EventHandler(this.txtEmpNo_Leave);
            this.txtEmpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpNo_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(255, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Employee Name";
            // 
            // txtNoOfMonths
            // 
            this.txtNoOfMonths.Location = new System.Drawing.Point(359, 39);
            this.txtNoOfMonths.Name = "txtNoOfMonths";
            this.txtNoOfMonths.Size = new System.Drawing.Size(90, 20);
            this.txtNoOfMonths.TabIndex = 4;
            this.txtNoOfMonths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfMonths_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "No Of Months";
            // 
            // txtDeductAmount
            // 
            this.txtDeductAmount.Location = new System.Drawing.Point(134, 39);
            this.txtDeductAmount.Name = "txtDeductAmount";
            this.txtDeductAmount.Size = new System.Drawing.Size(70, 20);
            this.txtDeductAmount.TabIndex = 3;
            this.txtDeductAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeductAmount_KeyPress);
            // 
            // txtEmpName
            // 
            this.txtEmpName.Enabled = false;
            this.txtEmpName.Location = new System.Drawing.Point(359, 13);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(149, 20);
            this.txtEmpName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "EmployeeNo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Monthly Deduct Amount";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.lblDeduction);
            this.groupBox4.Controls.Add(this.lblDeductionGroup);
            this.groupBox4.Controls.Add(this.cmbDeductions);
            this.groupBox4.Controls.Add(this.cmbDeductionGroup);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cmbDivision);
            this.groupBox4.Location = new System.Drawing.Point(10, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(729, 92);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(542, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Deduction Management";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblDeduction
            // 
            this.lblDeduction.AutoSize = true;
            this.lblDeduction.Location = new System.Drawing.Point(215, 70);
            this.lblDeduction.Name = "lblDeduction";
            this.lblDeduction.Size = new System.Drawing.Size(16, 13);
            this.lblDeduction.TabIndex = 33;
            this.lblDeduction.Text = "...";
            // 
            // lblDeductionGroup
            // 
            this.lblDeductionGroup.AutoSize = true;
            this.lblDeductionGroup.Location = new System.Drawing.Point(270, 43);
            this.lblDeductionGroup.Name = "lblDeductionGroup";
            this.lblDeductionGroup.Size = new System.Drawing.Size(16, 13);
            this.lblDeductionGroup.TabIndex = 32;
            this.lblDeductionGroup.Text = "...";
            // 
            // cmbDeductions
            // 
            this.cmbDeductions.FormattingEnabled = true;
            this.cmbDeductions.Location = new System.Drawing.Point(111, 68);
            this.cmbDeductions.Name = "cmbDeductions";
            this.cmbDeductions.Size = new System.Drawing.Size(94, 21);
            this.cmbDeductions.TabIndex = 2;
            this.cmbDeductions.SelectedIndexChanged += new System.EventHandler(this.cmbDeductions_SelectedIndexChanged);
            this.cmbDeductions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDeductions_KeyPress);
            // 
            // cmbDeductionGroup
            // 
            this.cmbDeductionGroup.FormattingEnabled = true;
            this.cmbDeductionGroup.Location = new System.Drawing.Point(111, 41);
            this.cmbDeductionGroup.Name = "cmbDeductionGroup";
            this.cmbDeductionGroup.Size = new System.Drawing.Size(149, 21);
            this.cmbDeductionGroup.TabIndex = 1;
            this.cmbDeductionGroup.SelectedIndexChanged += new System.EventHandler(this.cmbDeductionGroup_SelectedIndexChanged);
            this.cmbDeductionGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDeductionGroup_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Deduction ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Deduction Group";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(111, 14);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(94, 21);
            this.cmbDivision.TabIndex = 0;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            this.cmbDivision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDivision_KeyPress);
            // 
            // FixedDeductions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 504);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FixedDeductions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fixed Deductions";
            this.Load += new System.EventHandler(this.FixedDeductions_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbDirectPay.ResumeLayout(false);
            this.gbDirectPay.PerformLayout();
            this.gbTerminate.ResumeLayout(false);
            this.gbTerminate.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFixedDeductions)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Label lblRefNo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtG1EmpName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbG1Division;
        private System.Windows.Forms.TextBox txtG1EmpNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtG2EmpName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbG2Division;
        private System.Windows.Forms.TextBox txtG2EmpNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkGurantorRec;
        private System.Windows.Forms.Button btnEmpSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNoOfMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDeductAmount;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbDeductions;
        private System.Windows.Forms.ComboBox cmbDeductionGroup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.GroupBox gbTerminate;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnTerminate;
        public System.Windows.Forms.TextBox txtEmpNo;
        private System.Windows.Forms.CheckBox chkFixed;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView gvFixedDeductions;
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
        private System.Windows.Forms.CheckBox chkDirectPay;
        private System.Windows.Forms.Label lblDeduction;
        private System.Windows.Forms.Label lblDeductionGroup;
        private System.Windows.Forms.Button button1;
    }
}