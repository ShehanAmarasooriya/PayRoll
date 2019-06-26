namespace FTSPayroll
{
    partial class Daily_Harvest_Register
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
            this.btnDisplay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.chkEmpRange = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gbEmpRange = new System.Windows.Forms.GroupBox();
            this.txtEmpNoFrom = new System.Windows.Forms.TextBox();
            this.txtEmpNoTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbGeneral = new System.Windows.Forms.RadioButton();
            this.rbCashwork = new System.Windows.Forms.RadioButton();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnBlockPlk = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.gbEmpRange.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(199, 234);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(85, 23);
            this.btnDisplay.TabIndex = 17;
            this.btnDisplay.Text = "Display Report";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(96, 25);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(253, 21);
            this.cmbDivision.TabIndex = 14;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(290, 234);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.chkEmpRange);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.gbEmpRange);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.dtDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDivision);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 266);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(132, 234);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(61, 23);
            this.btnConfirm.TabIndex = 19;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chkEmpRange
            // 
            this.chkEmpRange.AutoSize = true;
            this.chkEmpRange.Location = new System.Drawing.Point(18, 126);
            this.chkEmpRange.Name = "chkEmpRange";
            this.chkEmpRange.Size = new System.Drawing.Size(144, 17);
            this.chkEmpRange.TabIndex = 29;
            this.chkEmpRange.Text = "Employee Range Search";
            this.chkEmpRange.UseVisualStyleBackColor = true;
            this.chkEmpRange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkEmpRange_KeyPress);
            this.chkEmpRange.CheckedChanged += new System.EventHandler(this.chkEmpRange_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Confirmation Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbEmpRange
            // 
            this.gbEmpRange.Controls.Add(this.txtEmpNoFrom);
            this.gbEmpRange.Controls.Add(this.txtEmpNoTo);
            this.gbEmpRange.Controls.Add(this.label3);
            this.gbEmpRange.Controls.Add(this.label4);
            this.gbEmpRange.Location = new System.Drawing.Point(18, 149);
            this.gbEmpRange.Name = "gbEmpRange";
            this.gbEmpRange.Size = new System.Drawing.Size(200, 79);
            this.gbEmpRange.TabIndex = 28;
            this.gbEmpRange.TabStop = false;
            this.gbEmpRange.Text = "Employee Range";
            // 
            // txtEmpNoFrom
            // 
            this.txtEmpNoFrom.Location = new System.Drawing.Point(44, 19);
            this.txtEmpNoFrom.Name = "txtEmpNoFrom";
            this.txtEmpNoFrom.Size = new System.Drawing.Size(100, 20);
            this.txtEmpNoFrom.TabIndex = 20;
            this.txtEmpNoFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpNoFrom_KeyPress);
            // 
            // txtEmpNoTo
            // 
            this.txtEmpNoTo.Location = new System.Drawing.Point(43, 45);
            this.txtEmpNoTo.Name = "txtEmpNoTo";
            this.txtEmpNoTo.Size = new System.Drawing.Size(100, 20);
            this.txtEmpNoTo.TabIndex = 21;
            this.txtEmpNoTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpNoTo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "To";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnBlockPlk);
            this.groupBox2.Controls.Add(this.rbGeneral);
            this.groupBox2.Controls.Add(this.rbCashwork);
            this.groupBox2.Location = new System.Drawing.Point(17, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 38);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Work Type";
            // 
            // rbGeneral
            // 
            this.rbGeneral.AutoSize = true;
            this.rbGeneral.Location = new System.Drawing.Point(24, 16);
            this.rbGeneral.Name = "rbGeneral";
            this.rbGeneral.Size = new System.Drawing.Size(58, 17);
            this.rbGeneral.TabIndex = 19;
            this.rbGeneral.TabStop = true;
            this.rbGeneral.Text = "Normal";
            this.rbGeneral.UseVisualStyleBackColor = true;
            // 
            // rbCashwork
            // 
            this.rbCashwork.AutoSize = true;
            this.rbCashwork.Location = new System.Drawing.Point(115, 16);
            this.rbCashwork.Name = "rbCashwork";
            this.rbCashwork.Size = new System.Drawing.Size(78, 17);
            this.rbCashwork.TabIndex = 20;
            this.rbCashwork.TabStop = true;
            this.rbCashwork.Text = "Cash Work";
            this.rbCashwork.UseVisualStyleBackColor = true;
            // 
            // dtDate
            // 
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDate.Location = new System.Drawing.Point(95, 55);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(123, 20);
            this.dtDate.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Date";
            // 
            // rbtnBlockPlk
            // 
            this.rbtnBlockPlk.AutoSize = true;
            this.rbtnBlockPlk.Location = new System.Drawing.Point(209, 15);
            this.rbtnBlockPlk.Name = "rbtnBlockPlk";
            this.rbtnBlockPlk.Size = new System.Drawing.Size(96, 17);
            this.rbtnBlockPlk.TabIndex = 21;
            this.rbtnBlockPlk.TabStop = true;
            this.rbtnBlockPlk.Text = "Block Plucking";
            this.rbtnBlockPlk.UseVisualStyleBackColor = true;
            // 
            // Daily_Harvest_Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 273);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Daily_Harvest_Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Harvest Register";
            this.Load += new System.EventHandler(this.Daily_Harvest_Register_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEmpRange.ResumeLayout(false);
            this.gbEmpRange.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbCashwork;
        private System.Windows.Forms.RadioButton rbGeneral;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkEmpRange;
        private System.Windows.Forms.GroupBox gbEmpRange;
        private System.Windows.Forms.TextBox txtEmpNoFrom;
        private System.Windows.Forms.TextBox txtEmpNoTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtnBlockPlk;
    }
}