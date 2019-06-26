namespace FTSPayroll
{
    partial class SystemSetting
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
            this.cmbRateList = new System.Windows.Forms.ComboBox();
            this.dgvRates = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDeductionList = new System.Windows.Forms.ComboBox();
            this.dgvDeduction = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeduction)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbRateList);
            this.groupBox1.Controls.Add(this.dgvRates);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(907, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CheckRoll Rates";
            // 
            // cmbRateList
            // 
            this.cmbRateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRateList.FormattingEnabled = true;
            this.cmbRateList.Location = new System.Drawing.Point(314, 24);
            this.cmbRateList.Name = "cmbRateList";
            this.cmbRateList.Size = new System.Drawing.Size(163, 21);
            this.cmbRateList.TabIndex = 1;
            this.cmbRateList.SelectedIndexChanged += new System.EventHandler(this.cmbRateList_SelectedIndexChanged);
            // 
            // dgvRates
            // 
            this.dgvRates.AllowUserToAddRows = false;
            this.dgvRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRates.Location = new System.Drawing.Point(11, 60);
            this.dgvRates.Name = "dgvRates";
            this.dgvRates.Size = new System.Drawing.Size(884, 166);
            this.dgvRates.TabIndex = 0;
            this.dgvRates.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRates_CellValidating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbDeductionList);
            this.groupBox2.Controls.Add(this.dgvDeduction);
            this.groupBox2.Location = new System.Drawing.Point(12, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(907, 201);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CheckRoll Deduction";
            // 
            // cmbDeductionList
            // 
            this.cmbDeductionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeductionList.FormattingEnabled = true;
            this.cmbDeductionList.Location = new System.Drawing.Point(314, 20);
            this.cmbDeductionList.Name = "cmbDeductionList";
            this.cmbDeductionList.Size = new System.Drawing.Size(163, 21);
            this.cmbDeductionList.TabIndex = 1;
            this.cmbDeductionList.SelectedIndexChanged += new System.EventHandler(this.cmbDeductionList_SelectedIndexChanged);
            // 
            // dgvDeduction
            // 
            this.dgvDeduction.AllowUserToAddRows = false;
            this.dgvDeduction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeduction.Location = new System.Drawing.Point(11, 54);
            this.dgvDeduction.Name = "dgvDeduction";
            this.dgvDeduction.Size = new System.Drawing.Size(884, 125);
            this.dgvDeduction.TabIndex = 0;
            this.dgvDeduction.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDeduction_CellValidating);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(765, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(846, 472);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SystemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 503);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Settings";
            this.Load += new System.EventHandler(this.SystemSetting_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRates)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeduction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvRates;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDeduction;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbRateList;
        private System.Windows.Forms.ComboBox cmbDeductionList;
    }
}