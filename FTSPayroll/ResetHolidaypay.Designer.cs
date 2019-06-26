namespace FTSPayroll
{
    partial class ResetHolidaypay
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOverKgAmount = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkAttBonusAdjust = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.chkMandays = new System.Windows.Forms.CheckBox();
            this.chkNormalManDays = new System.Windows.Forms.CheckBox();
            this.chkHolidayHalf = new System.Windows.Forms.CheckBox();
            this.chkEarnings = new System.Windows.Forms.CheckBox();
            this.chkDailyBasic = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancelConfirm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnCancelConfirm);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 368);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOverKgAmount);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.chkAttBonusAdjust);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.chkMandays);
            this.groupBox1.Controls.Add(this.chkNormalManDays);
            this.groupBox1.Controls.Add(this.chkHolidayHalf);
            this.groupBox1.Controls.Add(this.chkEarnings);
            this.groupBox1.Controls.Add(this.chkDailyBasic);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(17, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 164);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editable Columns";
            // 
            // chkOverKgAmount
            // 
            this.chkOverKgAmount.AutoSize = true;
            this.chkOverKgAmount.Location = new System.Drawing.Point(98, 53);
            this.chkOverKgAmount.Name = "chkOverKgAmount";
            this.chkOverKgAmount.Size = new System.Drawing.Size(98, 17);
            this.chkOverKgAmount.TabIndex = 26;
            this.chkOverKgAmount.Text = "OverKgAmount";
            this.chkOverKgAmount.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(97, 131);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            // 
            // chkAttBonusAdjust
            // 
            this.chkAttBonusAdjust.AutoSize = true;
            this.chkAttBonusAdjust.Location = new System.Drawing.Point(12, 108);
            this.chkAttBonusAdjust.Name = "chkAttBonusAdjust";
            this.chkAttBonusAdjust.Size = new System.Drawing.Size(98, 17);
            this.chkAttBonusAdjust.TabIndex = 29;
            this.chkAttBonusAdjust.Text = "AttBonusAdjust";
            this.chkAttBonusAdjust.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(16, 131);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // chkMandays
            // 
            this.chkMandays.AutoSize = true;
            this.chkMandays.Location = new System.Drawing.Point(12, 30);
            this.chkMandays.Name = "chkMandays";
            this.chkMandays.Size = new System.Drawing.Size(71, 17);
            this.chkMandays.TabIndex = 23;
            this.chkMandays.Text = "ManDays";
            this.chkMandays.UseVisualStyleBackColor = true;
            // 
            // chkNormalManDays
            // 
            this.chkNormalManDays.AutoSize = true;
            this.chkNormalManDays.Location = new System.Drawing.Point(98, 76);
            this.chkNormalManDays.Name = "chkNormalManDays";
            this.chkNormalManDays.Size = new System.Drawing.Size(104, 17);
            this.chkNormalManDays.TabIndex = 28;
            this.chkNormalManDays.Text = "NormalManDays";
            this.chkNormalManDays.UseVisualStyleBackColor = true;
            // 
            // chkHolidayHalf
            // 
            this.chkHolidayHalf.AutoSize = true;
            this.chkHolidayHalf.Location = new System.Drawing.Point(98, 30);
            this.chkHolidayHalf.Name = "chkHolidayHalf";
            this.chkHolidayHalf.Size = new System.Drawing.Size(113, 17);
            this.chkHolidayHalf.TabIndex = 24;
            this.chkHolidayHalf.Text = "HolidayHalfNames";
            this.chkHolidayHalf.UseVisualStyleBackColor = true;
            // 
            // chkEarnings
            // 
            this.chkEarnings.AutoSize = true;
            this.chkEarnings.Location = new System.Drawing.Point(12, 76);
            this.chkEarnings.Name = "chkEarnings";
            this.chkEarnings.Size = new System.Drawing.Size(67, 17);
            this.chkEarnings.TabIndex = 27;
            this.chkEarnings.Text = "Earnings";
            this.chkEarnings.UseVisualStyleBackColor = true;
            // 
            // chkDailyBasic
            // 
            this.chkDailyBasic.AutoSize = true;
            this.chkDailyBasic.Location = new System.Drawing.Point(12, 53);
            this.chkDailyBasic.Name = "chkDailyBasic";
            this.chkDailyBasic.Size = new System.Drawing.Size(75, 17);
            this.chkDailyBasic.TabIndex = 25;
            this.chkDailyBasic.Text = "DailyBasic";
            this.chkDailyBasic.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "* Reset  - To reset all available Holidaypay Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(319, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "* Remove - To remove all available Holidaypay Data Confirmations";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(97, 109);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 15;
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(97, 82);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Year";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(219, 151);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(147, 151);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancelConfirm
            // 
            this.btnCancelConfirm.Location = new System.Drawing.Point(8, 151);
            this.btnCancelConfirm.Name = "btnCancelConfirm";
            this.btnCancelConfirm.Size = new System.Drawing.Size(133, 23);
            this.btnCancelConfirm.TabIndex = 1;
            this.btnCancelConfirm.Text = "Cancel Confirmation";
            this.btnCancelConfirm.UseVisualStyleBackColor = true;
            this.btnCancelConfirm.Click += new System.EventHandler(this.button1_Click);
            // 
            // ResetHolidaypay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 373);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ResetHolidaypay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Holidaypay";
            this.Load += new System.EventHandler(this.ResetHolidaypay_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancelConfirm;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbDivision;
        public System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox chkNormalManDays;
        private System.Windows.Forms.CheckBox chkEarnings;
        private System.Windows.Forms.CheckBox chkOverKgAmount;
        private System.Windows.Forms.CheckBox chkDailyBasic;
        private System.Windows.Forms.CheckBox chkHolidayHalf;
        private System.Windows.Forms.CheckBox chkMandays;
        private System.Windows.Forms.CheckBox chkAttBonusAdjust;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}