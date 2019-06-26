namespace FTSPayroll
{
    partial class AutoTermination
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
            this.btnAutoTermination = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAutoTermination);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 76);
            this.panel1.TabIndex = 0;
            // 
            // btnAutoTermination
            // 
            this.btnAutoTermination.Location = new System.Drawing.Point(8, 21);
            this.btnAutoTermination.Name = "btnAutoTermination";
            this.btnAutoTermination.Size = new System.Drawing.Size(277, 23);
            this.btnAutoTermination.TabIndex = 0;
            this.btnAutoTermination.Text = "Process";
            this.btnAutoTermination.UseVisualStyleBackColor = true;
            this.btnAutoTermination.Click += new System.EventHandler(this.btnAutoTermination_Click);
            // 
            // AutoTermination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 83);
            this.Controls.Add(this.panel1);
            this.Name = "AutoTermination";
            this.Text = "Auto Termination";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAutoTermination;
    }
}