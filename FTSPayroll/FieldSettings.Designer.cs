namespace FTSPayroll
{
    partial class FieldSettings
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
            this.gbOverKgRate = new System.Windows.Forms.GroupBox();
            this.gvOverKgRate = new System.Windows.Forms.DataGridView();
            this.btnDeleteOkgRate = new System.Windows.Forms.Button();
            this.btnUpdateOkgRate = new System.Windows.Forms.Button();
            this.btnAddOkgRate = new System.Windows.Forms.Button();
            this.txtOkgRate = new System.Windows.Forms.TextBox();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.gbOverKgRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOverKgRate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.gbOverKgRate);
            this.panel1.Controls.Add(this.cmbField);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 346);
            this.panel1.TabIndex = 0;
            // 
            // gbOverKgRate
            // 
            this.gbOverKgRate.Controls.Add(this.button1);
            this.gbOverKgRate.Controls.Add(this.btnClose);
            this.gbOverKgRate.Controls.Add(this.label4);
            this.gbOverKgRate.Controls.Add(this.gvOverKgRate);
            this.gbOverKgRate.Controls.Add(this.btnDeleteOkgRate);
            this.gbOverKgRate.Controls.Add(this.btnUpdateOkgRate);
            this.gbOverKgRate.Controls.Add(this.btnAddOkgRate);
            this.gbOverKgRate.Controls.Add(this.txtOkgRate);
            this.gbOverKgRate.Location = new System.Drawing.Point(9, 98);
            this.gbOverKgRate.Name = "gbOverKgRate";
            this.gbOverKgRate.Size = new System.Drawing.Size(428, 245);
            this.gbOverKgRate.TabIndex = 7;
            this.gbOverKgRate.TabStop = false;
            this.gbOverKgRate.Text = "Field Over Kilo Rate";
            // 
            // gvOverKgRate
            // 
            this.gvOverKgRate.AllowUserToAddRows = false;
            this.gvOverKgRate.AllowUserToDeleteRows = false;
            this.gvOverKgRate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOverKgRate.Location = new System.Drawing.Point(6, 82);
            this.gvOverKgRate.Name = "gvOverKgRate";
            this.gvOverKgRate.ReadOnly = true;
            this.gvOverKgRate.Size = new System.Drawing.Size(301, 157);
            this.gvOverKgRate.TabIndex = 4;
            this.gvOverKgRate.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvOverKgRate_CellContentDoubleClick);
            // 
            // btnDeleteOkgRate
            // 
            this.btnDeleteOkgRate.Location = new System.Drawing.Point(163, 45);
            this.btnDeleteOkgRate.Name = "btnDeleteOkgRate";
            this.btnDeleteOkgRate.Size = new System.Drawing.Size(69, 23);
            this.btnDeleteOkgRate.TabIndex = 3;
            this.btnDeleteOkgRate.Text = "Delete";
            this.btnDeleteOkgRate.UseVisualStyleBackColor = true;
            this.btnDeleteOkgRate.Click += new System.EventHandler(this.btnDeleteOkgRate_Click);
            // 
            // btnUpdateOkgRate
            // 
            this.btnUpdateOkgRate.Location = new System.Drawing.Point(88, 45);
            this.btnUpdateOkgRate.Name = "btnUpdateOkgRate";
            this.btnUpdateOkgRate.Size = new System.Drawing.Size(69, 23);
            this.btnUpdateOkgRate.TabIndex = 2;
            this.btnUpdateOkgRate.Text = "Update";
            this.btnUpdateOkgRate.UseVisualStyleBackColor = true;
            this.btnUpdateOkgRate.Click += new System.EventHandler(this.btnUpdateOkgRate_Click);
            // 
            // btnAddOkgRate
            // 
            this.btnAddOkgRate.Location = new System.Drawing.Point(13, 45);
            this.btnAddOkgRate.Name = "btnAddOkgRate";
            this.btnAddOkgRate.Size = new System.Drawing.Size(69, 23);
            this.btnAddOkgRate.TabIndex = 1;
            this.btnAddOkgRate.Text = "Add";
            this.btnAddOkgRate.UseVisualStyleBackColor = true;
            this.btnAddOkgRate.Click += new System.EventHandler(this.btnAddOkgRate_Click);
            // 
            // txtOkgRate
            // 
            this.txtOkgRate.Location = new System.Drawing.Point(75, 19);
            this.txtOkgRate.Name = "txtOkgRate";
            this.txtOkgRate.Size = new System.Drawing.Size(127, 20);
            this.txtOkgRate.TabIndex = 0;
            // 
            // cmbField
            // 
            this.cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(84, 36);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(127, 21);
            this.cmbField.TabIndex = 6;
            this.cmbField.SelectedIndexChanged += new System.EventHandler(this.cmbField_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Field";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(84, 9);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(127, 21);
            this.cmbDivision.TabIndex = 4;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Division";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(84, 63);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(127, 21);
            this.cmbType.TabIndex = 9;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Rate";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(313, 45);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FieldSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 352);
            this.Controls.Add(this.panel1);
            this.Name = "FieldSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Field Settings";
            this.Load += new System.EventHandler(this.FieldSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbOverKgRate.ResumeLayout(false);
            this.gbOverKgRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOverKgRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbOverKgRate;
        private System.Windows.Forms.DataGridView gvOverKgRate;
        private System.Windows.Forms.Button btnDeleteOkgRate;
        private System.Windows.Forms.Button btnUpdateOkgRate;
        private System.Windows.Forms.Button btnAddOkgRate;
        private System.Windows.Forms.TextBox txtOkgRate;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}