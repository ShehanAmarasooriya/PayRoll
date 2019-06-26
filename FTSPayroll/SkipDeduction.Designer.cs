namespace FTSPayroll
{
    partial class SkipDeduction
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
            this.cmbDeductions = new System.Windows.Forms.ComboBox();
            this.cmbDeductionGroup = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.xmdSkip = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSelectAllDed = new System.Windows.Forms.CheckBox();
            this.gvlist = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancelSkip = new System.Windows.Forms.Button();
            this.gvSkippedDeduction = new System.Windows.Forms.DataGridView();
            this.chkSelectAllSkipped = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvlist)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSkippedDeduction)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbDeductions);
            this.groupBox1.Controls.Add(this.cmbDeductionGroup);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbMonth);
            this.groupBox1.Controls.Add(this.cmbYear);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbDivision);
            this.groupBox1.Location = new System.Drawing.Point(7, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbDeductions
            // 
            this.cmbDeductions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductions.FormattingEnabled = true;
            this.cmbDeductions.Location = new System.Drawing.Point(96, 121);
            this.cmbDeductions.Name = "cmbDeductions";
            this.cmbDeductions.Size = new System.Drawing.Size(125, 21);
            this.cmbDeductions.TabIndex = 6;
            this.cmbDeductions.SelectedIndexChanged += new System.EventHandler(this.cmbDeductions_SelectedIndexChanged);
            // 
            // cmbDeductionGroup
            // 
            this.cmbDeductionGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductionGroup.FormattingEnabled = true;
            this.cmbDeductionGroup.Location = new System.Drawing.Point(96, 94);
            this.cmbDeductionGroup.Name = "cmbDeductionGroup";
            this.cmbDeductionGroup.Size = new System.Drawing.Size(125, 21);
            this.cmbDeductionGroup.TabIndex = 5;
            this.cmbDeductionGroup.SelectedIndexChanged += new System.EventHandler(this.cmbDeductionGroup_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Deduction ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Deduction Group";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Month";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(96, 67);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(90, 21);
            this.cmbMonth.TabIndex = 4;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(96, 40);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(90, 21);
            this.cmbYear.TabIndex = 3;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Year";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(96, 13);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(210, 21);
            this.cmbDivision.TabIndex = 0;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.xmdSkip);
            this.groupBox2.Location = new System.Drawing.Point(7, 379);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 46);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(412, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // xmdSkip
            // 
            this.xmdSkip.Location = new System.Drawing.Point(325, 13);
            this.xmdSkip.Name = "xmdSkip";
            this.xmdSkip.Size = new System.Drawing.Size(81, 23);
            this.xmdSkip.TabIndex = 0;
            this.xmdSkip.Text = "SKIP";
            this.xmdSkip.UseVisualStyleBackColor = true;
            this.xmdSkip.Click += new System.EventHandler(this.xmdSkip_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSelectAllDed);
            this.groupBox3.Controls.Add(this.gvlist);
            this.groupBox3.Location = new System.Drawing.Point(7, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 207);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // chkSelectAllDed
            // 
            this.chkSelectAllDed.AutoSize = true;
            this.chkSelectAllDed.Location = new System.Drawing.Point(9, 19);
            this.chkSelectAllDed.Name = "chkSelectAllDed";
            this.chkSelectAllDed.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAllDed.TabIndex = 5;
            this.chkSelectAllDed.Text = "Select All";
            this.chkSelectAllDed.UseVisualStyleBackColor = true;
            this.chkSelectAllDed.CheckedChanged += new System.EventHandler(this.chkSelectAllDed_CheckedChanged);
            // 
            // gvlist
            // 
            this.gvlist.AllowUserToAddRows = false;
            this.gvlist.AllowUserToDeleteRows = false;
            this.gvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvlist.Location = new System.Drawing.Point(5, 41);
            this.gvlist.Name = "gvlist";
            this.gvlist.Size = new System.Drawing.Size(482, 160);
            this.gvlist.TabIndex = 0;
            this.gvlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvlist_CellContentClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCancelSkip);
            this.groupBox4.Controls.Add(this.gvSkippedDeduction);
            this.groupBox4.Location = new System.Drawing.Point(505, 32);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(372, 393);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Skipped Deduction";
            // 
            // btnCancelSkip
            // 
            this.btnCancelSkip.Location = new System.Drawing.Point(252, 361);
            this.btnCancelSkip.Name = "btnCancelSkip";
            this.btnCancelSkip.Size = new System.Drawing.Size(114, 23);
            this.btnCancelSkip.TabIndex = 4;
            this.btnCancelSkip.Text = "Cancel Skipped";
            this.btnCancelSkip.UseVisualStyleBackColor = true;
            this.btnCancelSkip.Click += new System.EventHandler(this.btnCancelSkip_Click);
            // 
            // gvSkippedDeduction
            // 
            this.gvSkippedDeduction.AllowUserToAddRows = false;
            this.gvSkippedDeduction.AllowUserToDeleteRows = false;
            this.gvSkippedDeduction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSkippedDeduction.Location = new System.Drawing.Point(6, 19);
            this.gvSkippedDeduction.Name = "gvSkippedDeduction";
            this.gvSkippedDeduction.Size = new System.Drawing.Size(360, 316);
            this.gvSkippedDeduction.TabIndex = 0;
            this.gvSkippedDeduction.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSkippedDeduction_CellContentClick);
            // 
            // chkSelectAllSkipped
            // 
            this.chkSelectAllSkipped.AutoSize = true;
            this.chkSelectAllSkipped.Location = new System.Drawing.Point(807, 22);
            this.chkSelectAllSkipped.Name = "chkSelectAllSkipped";
            this.chkSelectAllSkipped.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAllSkipped.TabIndex = 5;
            this.chkSelectAllSkipped.Text = "Select All";
            this.chkSelectAllSkipped.UseVisualStyleBackColor = false;
            this.chkSelectAllSkipped.CheckedChanged += new System.EventHandler(this.chkSelectAllSkipped_CheckedChanged);
            // 
            // SkipDeduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 428);
            this.Controls.Add(this.chkSelectAllSkipped);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SkipDeduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skip Deduction";
            this.Load += new System.EventHandler(this.SkipDeduction_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvlist)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSkippedDeduction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbDeductions;
        private System.Windows.Forms.ComboBox cmbDeductionGroup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button xmdSkip;
        private System.Windows.Forms.DataGridView gvlist;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCancelSkip;
        private System.Windows.Forms.DataGridView gvSkippedDeduction;
        private System.Windows.Forms.CheckBox chkSelectAllDed;
        private System.Windows.Forms.CheckBox chkSelectAllSkipped;
    }
}