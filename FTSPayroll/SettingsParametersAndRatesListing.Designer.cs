namespace FTSPayroll
{
    partial class SettingsParametersAndRatesListing
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
            this.btnFixedParam = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFieldSettings = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFixedParam);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnFieldSettings);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 116);
            this.panel1.TabIndex = 0;
            // 
            // btnFixedParam
            // 
            this.btnFixedParam.Location = new System.Drawing.Point(154, 56);
            this.btnFixedParam.Name = "btnFixedParam";
            this.btnFixedParam.Size = new System.Drawing.Size(100, 23);
            this.btnFixedParam.TabIndex = 5;
            this.btnFixedParam.Text = "Dispaly";
            this.btnFixedParam.UseVisualStyleBackColor = true;
            this.btnFixedParam.Click += new System.EventHandler(this.btnFixedParam_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fixed Parameters";
            // 
            // btnFieldSettings
            // 
            this.btnFieldSettings.Location = new System.Drawing.Point(154, 27);
            this.btnFieldSettings.Name = "btnFieldSettings";
            this.btnFieldSettings.Size = new System.Drawing.Size(100, 23);
            this.btnFieldSettings.TabIndex = 3;
            this.btnFieldSettings.Text = "Dispaly";
            this.btnFieldSettings.UseVisualStyleBackColor = true;
            this.btnFieldSettings.Click += new System.EventHandler(this.btnFieldSettings_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Field Settings";
            // 
            // SettingsParametersAndRatesListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 117);
            this.Controls.Add(this.panel1);
            this.Name = "SettingsParametersAndRatesListing";
            this.Text = "Settings Parameters And Rates Listing";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFixedParam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFieldSettings;
        private System.Windows.Forms.Label label2;
    }
}