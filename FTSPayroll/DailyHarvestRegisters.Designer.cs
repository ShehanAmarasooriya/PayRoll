namespace FTSPayroll
{
    partial class DailyHarvestRegisters
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
            this.chkEmpRange = new System.Windows.Forms.CheckBox();
            this.gbEmpRange = new System.Windows.Forms.GroupBox();
            this.txtEmpNoFrom = new System.Windows.Forms.TextBox();
            this.txtEmpNoTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.gbEmpRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEmpRange);
            this.groupBox1.Controls.Add(this.gbEmpRange);
            this.groupBox1.Controls.Add(this.dtDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDivision);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 266);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // chkEmpRange
            // 
            this.chkEmpRange.AutoSize = true;
            this.chkEmpRange.Location = new System.Drawing.Point(20, 83);
            this.chkEmpRange.Name = "chkEmpRange";
            this.chkEmpRange.Size = new System.Drawing.Size(144, 17);
            this.chkEmpRange.TabIndex = 29;
            this.chkEmpRange.Text = "Employee Range Search";
            this.chkEmpRange.UseVisualStyleBackColor = true;
            this.chkEmpRange.CheckedChanged += new System.EventHandler(this.chkEmpRange_CheckedChanged);
            // 
            // gbEmpRange
            // 
            this.gbEmpRange.Controls.Add(this.txtEmpNoFrom);
            this.gbEmpRange.Controls.Add(this.txtEmpNoTo);
            this.gbEmpRange.Controls.Add(this.label3);
            this.gbEmpRange.Controls.Add(this.label4);
            this.gbEmpRange.Location = new System.Drawing.Point(18, 107);
            this.gbEmpRange.Name = "gbEmpRange";
            this.gbEmpRange.Size = new System.Drawing.Size(331, 51);
            this.gbEmpRange.TabIndex = 28;
            this.gbEmpRange.TabStop = false;
            this.gbEmpRange.Text = "Employee Range";
            // 
            // txtEmpNoFrom
            // 
            this.txtEmpNoFrom.Location = new System.Drawing.Point(42, 16);
            this.txtEmpNoFrom.Name = "txtEmpNoFrom";
            this.txtEmpNoFrom.Size = new System.Drawing.Size(100, 20);
            this.txtEmpNoFrom.TabIndex = 20;
            // 
            // txtEmpNoTo
            // 
            this.txtEmpNoTo.Location = new System.Drawing.Point(194, 16);
            this.txtEmpNoTo.Name = "txtEmpNoTo";
            this.txtEmpNoTo.Size = new System.Drawing.Size(100, 20);
            this.txtEmpNoTo.TabIndex = 21;
            this.txtEmpNoTo.TextChanged += new System.EventHandler(this.txtEmpNoTo_TextChanged);
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
            this.label4.Location = new System.Drawing.Point(168, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "To";
            this.label4.Click += new System.EventHandler(this.label4_Click);
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
            // DailyHarvestRegisters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 328);
            this.Controls.Add(this.groupBox1);
            this.Name = "DailyHarvestRegisters";
            this.Text = "DailyHarvestRegisters";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEmpRange.ResumeLayout(false);
            this.gbEmpRange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEmpRange;
        private System.Windows.Forms.GroupBox gbEmpRange;
        private System.Windows.Forms.TextBox txtEmpNoFrom;
        private System.Windows.Forms.TextBox txtEmpNoTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDivision;
    }
}