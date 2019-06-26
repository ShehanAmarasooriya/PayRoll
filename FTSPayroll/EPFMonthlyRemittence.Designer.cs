namespace FTSPayroll
{
    partial class EPFMonthlyRemittence
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
            this.btnETF = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDisplay1 = new System.Windows.Forms.Button();
            this.btnEVEMP = new System.Windows.Forms.Button();
            this.btnEVEMC = new System.Windows.Forms.Button();
            this.btnCheckErrors = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheckErrors);
            this.groupBox1.Controls.Add(this.btnETF);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cmbMonth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbYear);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(11, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 172);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // btnETF
            // 
            this.btnETF.Location = new System.Drawing.Point(21, 114);
            this.btnETF.Name = "btnETF";
            this.btnETF.Size = new System.Drawing.Size(236, 23);
            this.btnETF.TabIndex = 23;
            this.btnETF.Text = "Generate ETF";
            this.btnETF.UseVisualStyleBackColor = true;
            this.btnETF.Click += new System.EventHandler(this.btnETF_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Generate EPF - EVEMC & EVEMP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(136, 58);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Month";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(136, 31);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 7;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Year";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(197, 193);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(74, 23);
            this.cmdClose.TabIndex = 17;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdDisplay1
            // 
            this.cmdDisplay1.Enabled = false;
            this.cmdDisplay1.Location = new System.Drawing.Point(18, 193);
            this.cmdDisplay1.Name = "cmdDisplay1";
            this.cmdDisplay1.Size = new System.Drawing.Size(17, 23);
            this.cmdDisplay1.TabIndex = 18;
            this.cmdDisplay1.Text = "Display Report";
            this.cmdDisplay1.UseVisualStyleBackColor = true;
            this.cmdDisplay1.Visible = false;
            this.cmdDisplay1.Click += new System.EventHandler(this.cmdDisplay1_Click);
            // 
            // btnEVEMP
            // 
            this.btnEVEMP.Location = new System.Drawing.Point(121, 193);
            this.btnEVEMP.Name = "btnEVEMP";
            this.btnEVEMP.Size = new System.Drawing.Size(70, 23);
            this.btnEVEMP.TabIndex = 25;
            this.btnEVEMP.Text = "EVEMP";
            this.btnEVEMP.UseVisualStyleBackColor = true;
            this.btnEVEMP.Click += new System.EventHandler(this.btnEVEMP_Click);
            // 
            // btnEVEMC
            // 
            this.btnEVEMC.Location = new System.Drawing.Point(41, 193);
            this.btnEVEMC.Name = "btnEVEMC";
            this.btnEVEMC.Size = new System.Drawing.Size(74, 23);
            this.btnEVEMC.TabIndex = 24;
            this.btnEVEMC.Text = "EVEMC";
            this.btnEVEMC.UseVisualStyleBackColor = true;
            this.btnEVEMC.Click += new System.EventHandler(this.btnEVEMC_Click);
            // 
            // btnCheckErrors
            // 
            this.btnCheckErrors.Location = new System.Drawing.Point(21, 143);
            this.btnCheckErrors.Name = "btnCheckErrors";
            this.btnCheckErrors.Size = new System.Drawing.Size(236, 23);
            this.btnCheckErrors.TabIndex = 24;
            this.btnCheckErrors.Text = "EFORM Master Data Validate";
            this.btnCheckErrors.UseVisualStyleBackColor = true;
            this.btnCheckErrors.Click += new System.EventHandler(this.btnCheckErrors_Click);
            // 
            // EPFMonthlyRemittence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 225);
            this.Controls.Add(this.btnEVEMP);
            this.Controls.Add(this.btnEVEMC);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdDisplay1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EPFMonthlyRemittence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPF Monthly Remittence";
            this.Load += new System.EventHandler(this.EPFMonthlyRemittence_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDisplay1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEVEMP;
        private System.Windows.Forms.Button btnEVEMC;
        private System.Windows.Forms.Button btnETF;
        private System.Windows.Forms.Button btnCheckErrors;
    }
}