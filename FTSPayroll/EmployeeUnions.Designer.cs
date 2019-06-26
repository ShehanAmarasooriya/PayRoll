namespace FTSPayroll
{
    partial class EmployeeUnions
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.cmbNewUnion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkAllUnion = new System.Windows.Forms.CheckBox();
            this.chkAllDiv = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Division";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(86, 13);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(81, 21);
            this.cmbDivision.TabIndex = 10;
            // 
            // cmbNewUnion
            // 
            this.cmbNewUnion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewUnion.FormattingEnabled = true;
            this.cmbNewUnion.Location = new System.Drawing.Point(86, 40);
            this.cmbNewUnion.Name = "cmbNewUnion";
            this.cmbNewUnion.Size = new System.Drawing.Size(121, 21);
            this.cmbNewUnion.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = " Union";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAllDiv);
            this.panel1.Controls.Add(this.chkAllUnion);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.cmbNewUnion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(1, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 81);
            this.panel1.TabIndex = 13;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(118, 93);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(75, 23);
            this.btnDisplay.TabIndex = 14;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(207, 92);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // chkAllUnion
            // 
            this.chkAllUnion.AutoSize = true;
            this.chkAllUnion.Location = new System.Drawing.Point(214, 40);
            this.chkAllUnion.Name = "chkAllUnion";
            this.chkAllUnion.Size = new System.Drawing.Size(37, 17);
            this.chkAllUnion.TabIndex = 13;
            this.chkAllUnion.Text = "All";
            this.chkAllUnion.UseVisualStyleBackColor = true;
            // 
            // chkAllDiv
            // 
            this.chkAllDiv.AutoSize = true;
            this.chkAllDiv.Location = new System.Drawing.Point(173, 12);
            this.chkAllDiv.Name = "chkAllDiv";
            this.chkAllDiv.Size = new System.Drawing.Size(37, 17);
            this.chkAllDiv.TabIndex = 14;
            this.chkAllDiv.Text = "All";
            this.chkAllDiv.UseVisualStyleBackColor = true;
            // 
            // EmployeeUnions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 122);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeUnions";
            this.Text = "Employee Unions Report";
            this.Load += new System.EventHandler(this.EmployeeUnions_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.ComboBox cmbNewUnion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkAllDiv;
        private System.Windows.Forms.CheckBox chkAllUnion;
    }
}