namespace FTSPayroll
{
    partial class MonthlyAdvanceReport
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDisplay2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbDeductGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDeduct = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCoinSummary = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(326, 157);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(58, 23);
            this.cmdClose.TabIndex = 26;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdDisplay2
            // 
            this.cmdDisplay2.Location = new System.Drawing.Point(229, 157);
            this.cmdDisplay2.Name = "cmdDisplay2";
            this.cmdDisplay2.Size = new System.Drawing.Size(91, 23);
            this.cmdDisplay2.TabIndex = 27;
            this.cmdDisplay2.Text = "Payment List";
            this.cmdDisplay2.UseVisualStyleBackColor = true;
            this.cmdDisplay2.Click += new System.EventHandler(this.cmdDisplay2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDeductGroup);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbDeduct);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 148);
            this.panel1.TabIndex = 25;
            // 
            // cmbDeductGroup
            // 
            this.cmbDeductGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductGroup.FormattingEnabled = true;
            this.cmbDeductGroup.Location = new System.Drawing.Point(120, 90);
            this.cmbDeductGroup.Name = "cmbDeductGroup";
            this.cmbDeductGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbDeductGroup.TabIndex = 42;
            this.cmbDeductGroup.SelectedIndexChanged += new System.EventHandler(this.cmbDeductGroup_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Deduction Group";
            // 
            // cmbDeduct
            // 
            this.cmbDeduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeduct.FormattingEnabled = true;
            this.cmbDeduct.Location = new System.Drawing.Point(120, 115);
            this.cmbDeduct.Name = "cmbDeduct";
            this.cmbDeduct.Size = new System.Drawing.Size(121, 21);
            this.cmbDeduct.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Deduction ";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(120, 9);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Division";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(120, 63);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Month";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Items.AddRange(new object[] {
            "2011",
            "2012",
            "2013",
            "2014",
            "2015"});
            this.cmbYear.Location = new System.Drawing.Point(120, 36);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 35;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Year";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Coin Analysis";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCoinSummary
            // 
            this.btnCoinSummary.Location = new System.Drawing.Point(121, 157);
            this.btnCoinSummary.Name = "btnCoinSummary";
            this.btnCoinSummary.Size = new System.Drawing.Size(102, 23);
            this.btnCoinSummary.TabIndex = 29;
            this.btnCoinSummary.Text = "Coin Summary";
            this.btnCoinSummary.UseVisualStyleBackColor = true;
            this.btnCoinSummary.Click += new System.EventHandler(this.btnCoinSummary_Click);
            // 
            // MonthlyAdvanceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 183);
            this.Controls.Add(this.btnCoinSummary);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdDisplay2);
            this.Controls.Add(this.panel1);
            this.Name = "MonthlyAdvanceReport";
            this.Text = "MonthlyAdvanceReport";
            this.Load += new System.EventHandler(this.MonthlyAdvanceReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDisplay2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbDeductGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeduct;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCoinSummary;

    }
}