namespace FTSPayroll
{
    partial class HolidayPay
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
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancelConfirmation = new System.Windows.Forms.Button();
            this.btnUpdateData = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnDownloadOldSystemData = new System.Windows.Forms.Button();
            this.hpData = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addEmp = new System.Windows.Forms.Button();
            this.txtNewEmp = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClearHP = new System.Windows.Forms.Button();
            this.btnGetHP = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnCancelConfirmation);
            this.panel1.Controls.Add(this.btnUpdateData);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.btnDownloadOldSystemData);
            this.panel1.Controls.Add(this.hpData);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnClearHP);
            this.panel1.Controls.Add(this.btnGetHP);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.gvList);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 491);
            this.panel1.TabIndex = 0;
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(60, 25);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 34;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Year";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(59, 109);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(340, 23);
            this.progressBar1.TabIndex = 33;
            // 
            // btnCancelConfirmation
            // 
            this.btnCancelConfirmation.Location = new System.Drawing.Point(790, 451);
            this.btnCancelConfirmation.Name = "btnCancelConfirmation";
            this.btnCancelConfirmation.Size = new System.Drawing.Size(124, 23);
            this.btnCancelConfirmation.TabIndex = 32;
            this.btnCancelConfirmation.Text = "Cancel Confirmation";
            this.btnCancelConfirmation.UseVisualStyleBackColor = true;
            this.btnCancelConfirmation.Click += new System.EventHandler(this.btnCancelConfirmation_Click);
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Location = new System.Drawing.Point(474, 79);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(92, 23);
            this.btnUpdateData.TabIndex = 31;
            this.btnUpdateData.Text = "Update Entries";
            this.btnUpdateData.UseVisualStyleBackColor = true;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "To";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(250, 52);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker2.TabIndex = 29;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "From";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(60, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 27;
            // 
            // btnDownloadOldSystemData
            // 
            this.btnDownloadOldSystemData.Location = new System.Drawing.Point(724, 9);
            this.btnDownloadOldSystemData.Name = "btnDownloadOldSystemData";
            this.btnDownloadOldSystemData.Size = new System.Drawing.Size(190, 23);
            this.btnDownloadOldSystemData.TabIndex = 26;
            this.btnDownloadOldSystemData.Text = "Download Data Of Old System";
            this.btnDownloadOldSystemData.UseVisualStyleBackColor = true;
            this.btnDownloadOldSystemData.Click += new System.EventHandler(this.btnDownloadOldSystemData_Click);
            // 
            // hpData
            // 
            this.hpData.Enabled = false;
            this.hpData.Location = new System.Drawing.Point(617, 53);
            this.hpData.Name = "hpData";
            this.hpData.Size = new System.Drawing.Size(75, 23);
            this.hpData.TabIndex = 25;
            this.hpData.Text = "HP Data Report";
            this.hpData.UseVisualStyleBackColor = true;
            this.hpData.Visible = false;
            this.hpData.Click += new System.EventHandler(this.hpData_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(617, 78);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "AdjustJRL HP Data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addEmp);
            this.groupBox1.Controls.Add(this.txtNewEmp);
            this.groupBox1.Location = new System.Drawing.Point(321, 435);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 48);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Employee";
            // 
            // addEmp
            // 
            this.addEmp.Location = new System.Drawing.Point(115, 16);
            this.addEmp.Name = "addEmp";
            this.addEmp.Size = new System.Drawing.Size(124, 23);
            this.addEmp.TabIndex = 22;
            this.addEmp.Text = "Add Employee";
            this.addEmp.UseVisualStyleBackColor = true;
            this.addEmp.Click += new System.EventHandler(this.addEmp_Click);
            // 
            // txtNewEmp
            // 
            this.txtNewEmp.Location = new System.Drawing.Point(9, 19);
            this.txtNewEmp.Name = "txtNewEmp";
            this.txtNewEmp.Size = new System.Drawing.Size(100, 20);
            this.txtNewEmp.TabIndex = 21;
            this.txtNewEmp.TextChanged += new System.EventHandler(this.txtNewEmp_TextChanged);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(732, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(182, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Display JRL Holiday Pay Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(617, 451);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Confirm And Process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(337, 79);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(62, 23);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClearHP
            // 
            this.btnClearHP.Location = new System.Drawing.Point(269, 79);
            this.btnClearHP.Name = "btnClearHP";
            this.btnClearHP.Size = new System.Drawing.Size(62, 23);
            this.btnClearHP.TabIndex = 17;
            this.btnClearHP.Text = "Clear";
            this.btnClearHP.UseVisualStyleBackColor = true;
            this.btnClearHP.Click += new System.EventHandler(this.btnClearHP_Click);
            // 
            // btnGetHP
            // 
            this.btnGetHP.Location = new System.Drawing.Point(201, 79);
            this.btnGetHP.Name = "btnGetHP";
            this.btnGetHP.Size = new System.Drawing.Size(62, 23);
            this.btnGetHP.TabIndex = 16;
            this.btnGetHP.Text = "Get ";
            this.btnGetHP.UseVisualStyleBackColor = true;
            this.btnGetHP.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(99, 452);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(18, 452);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Location = new System.Drawing.Point(18, 145);
            this.gvList.Name = "gvList";
            this.gvList.Size = new System.Drawing.Size(903, 284);
            this.gvList.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(59, 81);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 11;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // HolidayPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 506);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HolidayPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Holiday Pay Data";
            this.Load += new System.EventHandler(this.HolidayPay_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGetHP;
        private System.Windows.Forms.Button btnClearHP;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addEmp;
        private System.Windows.Forms.TextBox txtNewEmp;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button hpData;
        private System.Windows.Forms.Button btnDownloadOldSystemData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnUpdateData;
        private System.Windows.Forms.Button btnCancelConfirmation;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label1;
    }
}