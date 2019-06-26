namespace FTSPayroll
{
    partial class JobList
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
            this.gvList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchKeyWord = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.gvList);
            this.panel1.Controls.Add(this.txtSearchKeyWord);
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(642, 407);
            this.panel1.TabIndex = 1;
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
            this.gvList.AllowUserToDeleteRows = false;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Location = new System.Drawing.Point(10, 55);
            this.gvList.Name = "gvList";
            this.gvList.Size = new System.Drawing.Size(622, 338);
            this.gvList.TabIndex = 0;
            this.gvList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellContentDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Search By";
            // 
            // txtSearchKeyWord
            // 
            this.txtSearchKeyWord.Location = new System.Drawing.Point(73, 17);
            this.txtSearchKeyWord.Name = "txtSearchKeyWord";
            this.txtSearchKeyWord.Size = new System.Drawing.Size(256, 20);
            this.txtSearchKeyWord.TabIndex = 1;
            this.txtSearchKeyWord.TextChanged += new System.EventHandler(this.txtSearchKeyWord_TextChanged);
            this.txtSearchKeyWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchKeyWord_KeyPress);
            // 
            // JobList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 414);
            this.Controls.Add(this.panel1);
            this.Name = "JobList";
            this.Text = "Job List";
            this.Load += new System.EventHandler(this.JobList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.TextBox txtSearchKeyWord;
        private System.Windows.Forms.Label label2;
    }
}