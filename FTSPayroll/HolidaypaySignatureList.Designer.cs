namespace FTSPayroll
{
    partial class HolidaypaySignatureList
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
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHoliPay = new System.Windows.Forms.Button();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAttBonus = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(196, 37);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(77, 17);
            this.chkAll.TabIndex = 21;
            this.chkAll.Text = "All Division";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(69, 37);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAttBonus);
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnHoliPay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 121);
            this.panel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(264, 77);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnHoliPay
            // 
            this.btnHoliPay.Location = new System.Drawing.Point(102, 77);
            this.btnHoliPay.Name = "btnHoliPay";
            this.btnHoliPay.Size = new System.Drawing.Size(75, 23);
            this.btnHoliPay.TabIndex = 19;
            this.btnHoliPay.Text = "Holidaypay";
            this.btnHoliPay.UseVisualStyleBackColor = true;
            this.btnHoliPay.Click += new System.EventHandler(this.btnHoliPay_Click);
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(69, 10);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Year";
            // 
            // btnAttBonus
            // 
            this.btnAttBonus.Location = new System.Drawing.Point(183, 77);
            this.btnAttBonus.Name = "btnAttBonus";
            this.btnAttBonus.Size = new System.Drawing.Size(75, 23);
            this.btnAttBonus.TabIndex = 23;
            this.btnAttBonus.Text = "Att. Bonus";
            this.btnAttBonus.UseVisualStyleBackColor = true;
            this.btnAttBonus.Click += new System.EventHandler(this.btnAttBonus_Click);
            // 
            // HolidaypaySignatureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 124);
            this.Controls.Add(this.panel1);
            this.Name = "HolidaypaySignatureList";
            this.Text = "HolidaypaySignatureList";
            this.Load += new System.EventHandler(this.HolidaypaySignatureList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAttBonus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHoliPay;
        public System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label1;
    }
}