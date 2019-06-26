namespace FTSPayroll
{
    partial class DailyNotOffered
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
            this.lblName = new System.Windows.Forms.Label();
            this.chkAllWorkCategoy = new System.Windows.Forms.CheckBox();
            this.ListWorkCategories = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAllDivisions = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmbNOCodes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.chkAllWorkCategoy);
            this.panel1.Controls.Add(this.ListWorkCategories);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.chkAllDivisions);
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.cmbNOCodes);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbGender);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 288);
            this.panel1.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(206, 93);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(16, 13);
            this.lblName.TabIndex = 17;
            this.lblName.Text = "...";
            // 
            // chkAllWorkCategoy
            // 
            this.chkAllWorkCategoy.AutoSize = true;
            this.chkAllWorkCategoy.Location = new System.Drawing.Point(107, 126);
            this.chkAllWorkCategoy.Name = "chkAllWorkCategoy";
            this.chkAllWorkCategoy.Size = new System.Drawing.Size(70, 17);
            this.chkAllWorkCategoy.TabIndex = 16;
            this.chkAllWorkCategoy.Text = "Select All";
            this.chkAllWorkCategoy.UseVisualStyleBackColor = true;
            this.chkAllWorkCategoy.CheckedChanged += new System.EventHandler(this.chkAllWorkCategoy_CheckedChanged);
            // 
            // ListWorkCategories
            // 
            this.ListWorkCategories.FormattingEnabled = true;
            this.ListWorkCategories.Location = new System.Drawing.Point(107, 145);
            this.ListWorkCategories.Name = "ListWorkCategories";
            this.ListWorkCategories.Size = new System.Drawing.Size(224, 79);
            this.ListWorkCategories.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Work Category";
            // 
            // chkAllDivisions
            // 
            this.chkAllDivisions.AutoSize = true;
            this.chkAllDivisions.Location = new System.Drawing.Point(206, 38);
            this.chkAllDivisions.Name = "chkAllDivisions";
            this.chkAllDivisions.Size = new System.Drawing.Size(37, 17);
            this.chkAllDivisions.TabIndex = 13;
            this.chkAllDivisions.Text = "All";
            this.chkAllDivisions.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(206, 67);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(37, 17);
            this.chkAll.TabIndex = 3;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkAll_KeyPress);
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // cmbDivision
            // 
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(79, 36);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 1;
            this.cmbDivision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDivision_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Division";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.cmdClose);
            this.groupBox3.Controls.Add(this.cmdClear);
            this.groupBox3.Controls.Add(this.cmdAdd);
            this.groupBox3.Location = new System.Drawing.Point(11, 223);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 54);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(88, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(245, 20);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(167, 20);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(72, 23);
            this.cmdClear.TabIndex = 1;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(7, 20);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 0;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmbNOCodes
            // 
            this.cmbNOCodes.FormattingEnabled = true;
            this.cmbNOCodes.Items.AddRange(new object[] {
            "XXX"});
            this.cmbNOCodes.Location = new System.Drawing.Point(107, 90);
            this.cmbNOCodes.Name = "cmbNOCodes";
            this.cmbNOCodes.Size = new System.Drawing.Size(93, 21);
            this.cmbNOCodes.TabIndex = 4;
            this.cmbNOCodes.SelectedIndexChanged += new System.EventHandler(this.cmbNOCodes_SelectedIndexChanged);
            this.cmbNOCodes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbNOCodes_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Not Offered Code";
            // 
            // cmbGender
            // 
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(79, 63);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(121, 21);
            this.cmbGender.TabIndex = 2;
            this.cmbGender.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbGender_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Gender";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(79, 11);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            this.dateTimePicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date";
            // 
            // DailyNotOffered
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 301);
            this.Controls.Add(this.panel1);
            this.Name = "DailyNotOffered";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Not Offered";
            this.Load += new System.EventHandler(this.DailyNotOffered_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbNOCodes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkAllDivisions;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckBox chkAllWorkCategoy;
        private System.Windows.Forms.CheckedListBox ListWorkCategories;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblName;
    }
}