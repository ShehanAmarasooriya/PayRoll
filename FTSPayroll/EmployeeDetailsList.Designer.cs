namespace FTSPayroll
{
    partial class EmployeeDetailsList
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
            this.btnAddOCToAll = new System.Windows.Forms.Button();
            this.cmbEmployer = new System.Windows.Forms.ComboBox();
            this.btnAddZoneToAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOCGrade = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtZoneCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.btnEmployerToAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddOCToAll);
            this.panel1.Controls.Add(this.cmbEmployer);
            this.panel1.Controls.Add(this.btnAddZoneToAll);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOCGrade);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtZoneCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.btnEmployerToAll);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.gvList);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 472);
            this.panel1.TabIndex = 0;
            // 
            // btnAddOCToAll
            // 
            this.btnAddOCToAll.Location = new System.Drawing.Point(860, 65);
            this.btnAddOCToAll.Name = "btnAddOCToAll";
            this.btnAddOCToAll.Size = new System.Drawing.Size(156, 23);
            this.btnAddOCToAll.TabIndex = 38;
            this.btnAddOCToAll.Text = "Add To All Employees";
            this.btnAddOCToAll.UseVisualStyleBackColor = true;
            this.btnAddOCToAll.Click += new System.EventHandler(this.btnAddOCToAll_Click);
            // 
            // cmbEmployer
            // 
            this.cmbEmployer.FormattingEnabled = true;
            this.cmbEmployer.Location = new System.Drawing.Point(748, 9);
            this.cmbEmployer.Name = "cmbEmployer";
            this.cmbEmployer.Size = new System.Drawing.Size(106, 21);
            this.cmbEmployer.TabIndex = 37;
            // 
            // btnAddZoneToAll
            // 
            this.btnAddZoneToAll.Location = new System.Drawing.Point(860, 36);
            this.btnAddZoneToAll.Name = "btnAddZoneToAll";
            this.btnAddZoneToAll.Size = new System.Drawing.Size(156, 23);
            this.btnAddZoneToAll.TabIndex = 36;
            this.btnAddZoneToAll.Text = "Add To All Employees";
            this.btnAddZoneToAll.UseVisualStyleBackColor = true;
            this.btnAddZoneToAll.Click += new System.EventHandler(this.btnAddZoneToAll_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "OC Grade";
            // 
            // txtOCGrade
            // 
            this.txtOCGrade.Location = new System.Drawing.Point(781, 68);
            this.txtOCGrade.Name = "txtOCGrade";
            this.txtOCGrade.Size = new System.Drawing.Size(73, 20);
            this.txtOCGrade.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(675, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Zone Code";
            // 
            // txtZoneCode
            // 
            this.txtZoneCode.Location = new System.Drawing.Point(781, 38);
            this.txtZoneCode.Name = "txtZoneCode";
            this.txtZoneCode.Size = new System.Drawing.Size(73, 20);
            this.txtZoneCode.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(675, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "EmployerNO";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(74, 9);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(93, 21);
            this.cmbDivision.TabIndex = 21;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // btnEmployerToAll
            // 
            this.btnEmployerToAll.Location = new System.Drawing.Point(860, 7);
            this.btnEmployerToAll.Name = "btnEmployerToAll";
            this.btnEmployerToAll.Size = new System.Drawing.Size(157, 23);
            this.btnEmployerToAll.TabIndex = 6;
            this.btnEmployerToAll.Text = "Add To All Employees";
            this.btnEmployerToAll.UseVisualStyleBackColor = true;
            this.btnEmployerToAll.Click += new System.EventHandler(this.btnEmployerToAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Division";
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Location = new System.Drawing.Point(5, 94);
            this.gvList.Name = "gvList";
            this.gvList.Size = new System.Drawing.Size(1013, 369);
            this.gvList.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(781, 479);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(943, 479);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(862, 479);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // EmployeeDetailsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 509);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EmployeeDetailsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Details";
            this.Load += new System.EventHandler(this.EmployeeDetailsList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEmployerToAll;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnAddOCToAll;
        private System.Windows.Forms.ComboBox cmbEmployer;
        private System.Windows.Forms.Button btnAddZoneToAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOCGrade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtZoneCode;
        private System.Windows.Forms.Label label2;
    }
}