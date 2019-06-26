namespace FTSPayroll
{
    partial class ProcessPreviewMonthlyWages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPreviewMonthlyWages));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.gbProgress = new System.Windows.Forms.GroupBox();
            this.pbProcessStatus = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pboxLoad = new System.Windows.Forms.PictureBox();
            this.lblEarningsStatus = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSkipNormOkgRework = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.gbProgress.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSkipNormOkgRework);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.gbProgress);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.cmbCategory);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbDivision);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 218);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(334, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Validate Main Accounts";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(216, 11);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(77, 17);
            this.chkAll.TabIndex = 19;
            this.chkAll.Text = "All Division";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // gbProgress
            // 
            this.gbProgress.Controls.Add(this.pbProcessStatus);
            this.gbProgress.Location = new System.Drawing.Point(12, 122);
            this.gbProgress.Name = "gbProgress";
            this.gbProgress.Size = new System.Drawing.Size(484, 46);
            this.gbProgress.TabIndex = 18;
            this.gbProgress.TabStop = false;
            // 
            // pbProcessStatus
            // 
            this.pbProcessStatus.Location = new System.Drawing.Point(8, 17);
            this.pbProcessStatus.Name = "pbProcessStatus";
            this.pbProcessStatus.Size = new System.Drawing.Size(470, 23);
            this.pbProcessStatus.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pboxLoad);
            this.groupBox2.Controls.Add(this.lblEarningsStatus);
            this.groupBox2.Location = new System.Drawing.Point(207, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 87);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // pboxLoad
            // 
            this.pboxLoad.Image = ((System.Drawing.Image)(resources.GetObject("pboxLoad.Image")));
            this.pboxLoad.Location = new System.Drawing.Point(9, 10);
            this.pboxLoad.Name = "pboxLoad";
            this.pboxLoad.Size = new System.Drawing.Size(33, 27);
            this.pboxLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxLoad.TabIndex = 17;
            this.pboxLoad.TabStop = false;
            // 
            // lblEarningsStatus
            // 
            this.lblEarningsStatus.AutoSize = true;
            this.lblEarningsStatus.Location = new System.Drawing.Point(6, 41);
            this.lblEarningsStatus.Name = "lblEarningsStatus";
            this.lblEarningsStatus.Size = new System.Drawing.Size(107, 13);
            this.lblEarningsStatus.TabIndex = 14;
            this.lblEarningsStatus.Text = "Ready To Preview....";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(80, 37);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Category";
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(80, 10);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Division";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBackUp);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnPreview);
            this.groupBox1.Location = new System.Drawing.Point(15, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnBackUp
            // 
            this.btnBackUp.Location = new System.Drawing.Point(11, 12);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(75, 23);
            this.btnBackUp.TabIndex = 2;
            this.btnBackUp.Text = "Back Up";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(173, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(92, 13);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 0;
            this.btnPreview.Text = "Calculate";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(80, 66);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 2;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Year";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(80, 93);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Month";
            // 
            // chkSkipNormOkgRework
            // 
            this.chkSkipNormOkgRework.AutoSize = true;
            this.chkSkipNormOkgRework.Location = new System.Drawing.Point(334, 184);
            this.chkSkipNormOkgRework.Name = "chkSkipNormOkgRework";
            this.chkSkipNormOkgRework.Size = new System.Drawing.Size(138, 17);
            this.chkSkipNormOkgRework.TabIndex = 21;
            this.chkSkipNormOkgRework.Text = "Skip Norm Okg Rework";
            this.chkSkipNormOkgRework.UseVisualStyleBackColor = true;
            // 
            // ProcessPreviewMonthlyWages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 222);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessPreviewMonthlyWages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculate Monthly Checkroll (Preview)";
            this.Load += new System.EventHandler(this.ProcessPreviewMonthlyWages_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbProgress.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pbProcessStatus;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEarningsStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pboxLoad;
        private System.Windows.Forms.GroupBox gbProgress;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkSkipNormOkgRework;
    }
}