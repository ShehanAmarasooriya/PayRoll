namespace FTSPayroll
{
    partial class ACSubCategoryList
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
            this.gvSubCategoryList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubCategory = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnAddToForm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubCategoryList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddToForm);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtSubCategory);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.gvSubCategoryList);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(729, 502);
            this.panel1.TabIndex = 0;
            // 
            // gvSubCategoryList
            // 
            this.gvSubCategoryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSubCategoryList.Location = new System.Drawing.Point(11, 68);
            this.gvSubCategoryList.Name = "gvSubCategoryList";
            this.gvSubCategoryList.Size = new System.Drawing.Size(715, 431);
            this.gvSubCategoryList.TabIndex = 0;
            this.gvSubCategoryList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSubCategoryList_CellContentDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sub Category Code";
            // 
            // txtSubCategory
            // 
            this.txtSubCategory.Location = new System.Drawing.Point(116, 19);
            this.txtSubCategory.Name = "txtSubCategory";
            this.txtSubCategory.Size = new System.Drawing.Size(100, 20);
            this.txtSubCategory.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(222, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(388, 20);
            this.txtName.TabIndex = 3;
            // 
            // btnAddToForm
            // 
            this.btnAddToForm.Location = new System.Drawing.Point(616, 19);
            this.btnAddToForm.Name = "btnAddToForm";
            this.btnAddToForm.Size = new System.Drawing.Size(103, 23);
            this.btnAddToForm.TabIndex = 4;
            this.btnAddToForm.Text = "Add To Form";
            this.btnAddToForm.UseVisualStyleBackColor = true;
            this.btnAddToForm.Click += new System.EventHandler(this.btnAddToForm_Click);
            // 
            // ACSubCategoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 504);
            this.Controls.Add(this.panel1);
            this.Name = "ACSubCategoryList";
            this.Text = "ACSubCategoryList";
            this.Load += new System.EventHandler(this.ACSubCategoryList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubCategoryList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvSubCategoryList;
        private System.Windows.Forms.TextBox txtSubCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAddToForm;
    }
}