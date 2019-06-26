namespace FTSPayroll
{
    partial class DownloadData
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
            this.btnDOWNLOAD = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.prBar = new System.Windows.Forms.ProgressBar();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnPathSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gvlist = new System.Windows.Forms.DataGridView();
            this.btnSAVE = new System.Windows.Forms.Button();
            this.btnCLOSE = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvlist)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDOWNLOAD);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.prBar);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.btnPathSelect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(726, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnDOWNLOAD
            // 
            this.btnDOWNLOAD.Location = new System.Drawing.Point(570, 103);
            this.btnDOWNLOAD.Name = "btnDOWNLOAD";
            this.btnDOWNLOAD.Size = new System.Drawing.Size(136, 23);
            this.btnDOWNLOAD.TabIndex = 6;
            this.btnDOWNLOAD.Text = "DOWNLOAD";
            this.btnDOWNLOAD.UseVisualStyleBackColor = true;
            this.btnDOWNLOAD.Click += new System.EventHandler(this.btnDOWNLOAD_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(81, 103);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 5;
            // 
            // prBar
            // 
            this.prBar.Location = new System.Drawing.Point(84, 68);
            this.prBar.Name = "prBar";
            this.prBar.Size = new System.Drawing.Size(622, 23);
            this.prBar.TabIndex = 4;
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(84, 29);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(541, 20);
            this.txtPath.TabIndex = 3;
            // 
            // btnPathSelect
            // 
            this.btnPathSelect.Location = new System.Drawing.Point(631, 29);
            this.btnPathSelect.Name = "btnPathSelect";
            this.btnPathSelect.Size = new System.Drawing.Size(75, 23);
            this.btnPathSelect.TabIndex = 2;
            this.btnPathSelect.Text = ".......";
            this.btnPathSelect.UseVisualStyleBackColor = true;
            this.btnPathSelect.Click += new System.EventHandler(this.btnPathSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Path";
            // 
            // gvlist
            // 
            this.gvlist.AllowUserToAddRows = false;
            this.gvlist.AllowUserToDeleteRows = false;
            this.gvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvlist.Location = new System.Drawing.Point(8, 146);
            this.gvlist.Name = "gvlist";
            this.gvlist.ReadOnly = true;
            this.gvlist.Size = new System.Drawing.Size(726, 423);
            this.gvlist.TabIndex = 1;
            // 
            // btnSAVE
            // 
            this.btnSAVE.Location = new System.Drawing.Point(456, 589);
            this.btnSAVE.Name = "btnSAVE";
            this.btnSAVE.Size = new System.Drawing.Size(136, 23);
            this.btnSAVE.TabIndex = 7;
            this.btnSAVE.Text = "SAVE";
            this.btnSAVE.UseVisualStyleBackColor = true;
            this.btnSAVE.Click += new System.EventHandler(this.btnSAVE_Click);
            // 
            // btnCLOSE
            // 
            this.btnCLOSE.Location = new System.Drawing.Point(598, 589);
            this.btnCLOSE.Name = "btnCLOSE";
            this.btnCLOSE.Size = new System.Drawing.Size(136, 23);
            this.btnCLOSE.TabIndex = 8;
            this.btnCLOSE.Text = "CLOSE";
            this.btnCLOSE.UseVisualStyleBackColor = true;
            this.btnCLOSE.Click += new System.EventHandler(this.btnCLOSE_Click);
            // 
            // DownloadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 624);
            this.Controls.Add(this.btnCLOSE);
            this.Controls.Add(this.btnSAVE);
            this.Controls.Add(this.gvlist);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download Easy Weigh Data";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDOWNLOAD;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar prBar;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnPathSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSAVE;
        private System.Windows.Forms.Button btnCLOSE;
        private System.Windows.Forms.DataGridView gvlist;
    }
}