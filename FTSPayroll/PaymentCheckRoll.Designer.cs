namespace FTSPayroll
{
    partial class PaymentCheckRoll
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
            this.chkAllCat = new System.Windows.Forms.CheckBox();
            this.cmbEmpCat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAllDivisions = new System.Windows.Forms.CheckBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDisplay = new System.Windows.Forms.Button();
            this.cmdDisplay2 = new System.Windows.Forms.Button();
            this.btnPaymentDetails = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAllCat);
            this.groupBox1.Controls.Add(this.cmbEmpCat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbMonth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbYear);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkAllDivisions);
            this.groupBox1.Controls.Add(this.cmbDivision);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkAllCat
            // 
            this.chkAllCat.AutoSize = true;
            this.chkAllCat.Location = new System.Drawing.Point(258, 112);
            this.chkAllCat.Name = "chkAllCat";
            this.chkAllCat.Size = new System.Drawing.Size(37, 17);
            this.chkAllCat.TabIndex = 12;
            this.chkAllCat.Text = "All";
            this.chkAllCat.UseVisualStyleBackColor = true;
            this.chkAllCat.CheckedChanged += new System.EventHandler(this.chkAllCat_CheckedChanged);
            // 
            // cmbEmpCat
            // 
            this.cmbEmpCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpCat.FormattingEnabled = true;
            this.cmbEmpCat.Location = new System.Drawing.Point(131, 110);
            this.cmbEmpCat.Name = "cmbEmpCat";
            this.cmbEmpCat.Size = new System.Drawing.Size(121, 21);
            this.cmbEmpCat.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Emp Category";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(131, 83);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Month";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(131, 56);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Year";
            // 
            // chkAllDivisions
            // 
            this.chkAllDivisions.AutoSize = true;
            this.chkAllDivisions.Location = new System.Drawing.Point(346, 31);
            this.chkAllDivisions.Name = "chkAllDivisions";
            this.chkAllDivisions.Size = new System.Drawing.Size(82, 17);
            this.chkAllDivisions.TabIndex = 2;
            this.chkAllDivisions.Text = "All Divisions";
            this.chkAllDivisions.UseVisualStyleBackColor = true;
            this.chkAllDivisions.CheckedChanged += new System.EventHandler(this.chkAllDivisions_CheckedChanged);
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(131, 29);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(209, 21);
            this.cmbDivision.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estate Division";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(280, 150);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdDisplay
            // 
            this.cmdDisplay.Location = new System.Drawing.Point(118, 149);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Size = new System.Drawing.Size(75, 23);
            this.cmdDisplay.TabIndex = 2;
            this.cmdDisplay.Text = "Report I";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // cmdDisplay2
            // 
            this.cmdDisplay2.Location = new System.Drawing.Point(199, 149);
            this.cmdDisplay2.Name = "cmdDisplay2";
            this.cmdDisplay2.Size = new System.Drawing.Size(75, 23);
            this.cmdDisplay2.TabIndex = 3;
            this.cmdDisplay2.Text = "Report II";
            this.cmdDisplay2.UseVisualStyleBackColor = true;
            this.cmdDisplay2.Click += new System.EventHandler(this.cmdDisplay2_Click);
            // 
            // btnPaymentDetails
            // 
            this.btnPaymentDetails.Location = new System.Drawing.Point(118, 181);
            this.btnPaymentDetails.Name = "btnPaymentDetails";
            this.btnPaymentDetails.Size = new System.Drawing.Size(237, 23);
            this.btnPaymentDetails.TabIndex = 6;
            this.btnPaymentDetails.Text = "Payment Details";
            this.btnPaymentDetails.UseVisualStyleBackColor = true;
            this.btnPaymentDetails.Click += new System.EventHandler(this.btnPaymentDetails_Click);
            // 
            // PaymentCheckRoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 216);
            this.Controls.Add(this.btnPaymentDetails);
            this.Controls.Add(this.cmdDisplay2);
            this.Controls.Add(this.cmdDisplay);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentCheckRoll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment CheckRoll Report";
            this.Load += new System.EventHandler(this.PaymentCheckRoll_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAllDivisions;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDisplay;
        private System.Windows.Forms.Button cmdDisplay2;
        private System.Windows.Forms.ComboBox cmbEmpCat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAllCat;
        private System.Windows.Forms.Button btnPaymentDetails;
    }
}