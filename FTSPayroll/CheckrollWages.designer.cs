namespace FTSPayroll
{
    partial class CheckrollWages
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.BFProcess = new System.Windows.Forms.Button();
            this.btnLent = new System.Windows.Forms.Button();
            this.cmbCropType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAllCrops = new System.Windows.Forms.CheckBox();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAllFields = new System.Windows.Forms.CheckBox();
            this.cmdFieldwise = new System.Windows.Forms.Button();
            this.btnCashBreakDown = new System.Windows.Forms.Button();
            this.btnJobWiseWages = new System.Windows.Forms.Button();
            this.btnCropDivisionJob = new System.Windows.Forms.Button();
            this.btnCropDivisionFieldJob = new System.Windows.Forms.Button();
            this.gbCropWise = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEstateJob = new System.Windows.Forms.Button();
            this.btnEstateDivisionJob = new System.Windows.Forms.Button();
            this.btnDivisionWiseIncludingLentBorrow = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDivisionWagesGeneralBorrowSummary = new System.Windows.Forms.Button();
            this.btnAdditions = new System.Windows.Forms.Button();
            this.btnDivisionSummary = new System.Windows.Forms.Button();
            this.btnDivisionWagesGeneralLent = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnWagesLentSummary = new System.Windows.Forms.Button();
            this.btnWagesEstateSummary = new System.Windows.Forms.Button();
            this.btnLentLabourDetails = new System.Windows.Forms.Button();
            this.gbCropWise.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Month";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(86, 72);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 21);
            this.cmbYear.TabIndex = 2;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(86, 99);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(121, 21);
            this.cmbMonth.TabIndex = 3;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Enabled = false;
            this.btnDisplay.Location = new System.Drawing.Point(583, 71);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(62, 23);
            this.btnDisplay.TabIndex = 4;
            this.btnDisplay.Text = "Summary";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(28, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(234, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(652, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 24);
            this.button1.TabIndex = 6;
            this.button1.Text = "Field Wise";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(86, 45);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(121, 21);
            this.cmbDivision.TabIndex = 1;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Division";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(213, 47);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(37, 17);
            this.chkAll.TabIndex = 14;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Enabled = false;
            this.btnPreview.Location = new System.Drawing.Point(640, 97);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(76, 23);
            this.btnPreview.TabIndex = 15;
            this.btnPreview.Text = "DISPLAY";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Visible = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // BFProcess
            // 
            this.BFProcess.Enabled = false;
            this.BFProcess.Location = new System.Drawing.Point(578, 280);
            this.BFProcess.Name = "BFProcess";
            this.BFProcess.Size = new System.Drawing.Size(143, 23);
            this.BFProcess.TabIndex = 5;
            this.BFProcess.Text = "Display Job Wise";
            this.BFProcess.UseVisualStyleBackColor = true;
            this.BFProcess.Visible = false;
            this.BFProcess.Click += new System.EventHandler(this.BFProcess_Click);
            // 
            // btnLent
            // 
            this.btnLent.Enabled = false;
            this.btnLent.Location = new System.Drawing.Point(588, 126);
            this.btnLent.Name = "btnLent";
            this.btnLent.Size = new System.Drawing.Size(133, 23);
            this.btnLent.TabIndex = 17;
            this.btnLent.Text = "Display Lent Report";
            this.btnLent.UseVisualStyleBackColor = true;
            this.btnLent.Visible = false;
            this.btnLent.Click += new System.EventHandler(this.btnLent_Click);
            // 
            // cmbCropType
            // 
            this.cmbCropType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCropType.FormattingEnabled = true;
            this.cmbCropType.Location = new System.Drawing.Point(86, 18);
            this.cmbCropType.Name = "cmbCropType";
            this.cmbCropType.Size = new System.Drawing.Size(121, 21);
            this.cmbCropType.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Crop Type";
            // 
            // chkAllCrops
            // 
            this.chkAllCrops.AutoSize = true;
            this.chkAllCrops.Checked = true;
            this.chkAllCrops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllCrops.Location = new System.Drawing.Point(213, 18);
            this.chkAllCrops.Name = "chkAllCrops";
            this.chkAllCrops.Size = new System.Drawing.Size(37, 17);
            this.chkAllCrops.TabIndex = 20;
            this.chkAllCrops.Text = "All";
            this.chkAllCrops.UseVisualStyleBackColor = true;
            this.chkAllCrops.CheckedChanged += new System.EventHandler(this.chkAllCrops_CheckedChanged);
            // 
            // cmbField
            // 
            this.cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbField.Enabled = false;
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(640, 44);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(36, 21);
            this.cmbField.TabIndex = 22;
            this.cmbField.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(605, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Field";
            this.label5.Visible = false;
            // 
            // chkAllFields
            // 
            this.chkAllFields.AutoSize = true;
            this.chkAllFields.Checked = true;
            this.chkAllFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllFields.Enabled = false;
            this.chkAllFields.Location = new System.Drawing.Point(682, 44);
            this.chkAllFields.Name = "chkAllFields";
            this.chkAllFields.Size = new System.Drawing.Size(37, 17);
            this.chkAllFields.TabIndex = 23;
            this.chkAllFields.Text = "All";
            this.chkAllFields.UseVisualStyleBackColor = true;
            this.chkAllFields.Visible = false;
            this.chkAllFields.CheckedChanged += new System.EventHandler(this.chkAllFields_CheckedChanged);
            // 
            // cmdFieldwise
            // 
            this.cmdFieldwise.Enabled = false;
            this.cmdFieldwise.Location = new System.Drawing.Point(588, 222);
            this.cmdFieldwise.Name = "cmdFieldwise";
            this.cmdFieldwise.Size = new System.Drawing.Size(133, 23);
            this.cmdFieldwise.TabIndex = 6;
            this.cmdFieldwise.Text = "Display Fieldwise Report";
            this.cmdFieldwise.UseVisualStyleBackColor = true;
            this.cmdFieldwise.Visible = false;
            this.cmdFieldwise.Click += new System.EventHandler(this.cmdFieldwise_Click);
            // 
            // btnCashBreakDown
            // 
            this.btnCashBreakDown.Enabled = false;
            this.btnCashBreakDown.Location = new System.Drawing.Point(640, 173);
            this.btnCashBreakDown.Name = "btnCashBreakDown";
            this.btnCashBreakDown.Size = new System.Drawing.Size(81, 23);
            this.btnCashBreakDown.TabIndex = 26;
            this.btnCashBreakDown.Text = "Cash Work Breakdown";
            this.btnCashBreakDown.UseVisualStyleBackColor = true;
            this.btnCashBreakDown.Visible = false;
            this.btnCashBreakDown.Click += new System.EventHandler(this.btnCashBreakDown_Click);
            // 
            // btnJobWiseWages
            // 
            this.btnJobWiseWages.Enabled = false;
            this.btnJobWiseWages.Location = new System.Drawing.Point(632, 251);
            this.btnJobWiseWages.Name = "btnJobWiseWages";
            this.btnJobWiseWages.Size = new System.Drawing.Size(89, 23);
            this.btnJobWiseWages.TabIndex = 27;
            this.btnJobWiseWages.Text = "Job Wise Wages Report";
            this.btnJobWiseWages.UseVisualStyleBackColor = true;
            this.btnJobWiseWages.Visible = false;
            this.btnJobWiseWages.Click += new System.EventHandler(this.btnJobWiseWages_Click);
            // 
            // btnCropDivisionJob
            // 
            this.btnCropDivisionJob.Location = new System.Drawing.Point(6, 19);
            this.btnCropDivisionJob.Name = "btnCropDivisionJob";
            this.btnCropDivisionJob.Size = new System.Drawing.Size(234, 23);
            this.btnCropDivisionJob.TabIndex = 28;
            this.btnCropDivisionJob.Text = "Checkroll Wages Report";
            this.btnCropDivisionJob.UseVisualStyleBackColor = true;
            this.btnCropDivisionJob.Click += new System.EventHandler(this.btnCropDivisionJob_Click);
            // 
            // btnCropDivisionFieldJob
            // 
            this.btnCropDivisionFieldJob.Location = new System.Drawing.Point(6, 48);
            this.btnCropDivisionFieldJob.Name = "btnCropDivisionFieldJob";
            this.btnCropDivisionFieldJob.Size = new System.Drawing.Size(234, 23);
            this.btnCropDivisionFieldJob.TabIndex = 29;
            this.btnCropDivisionFieldJob.Text = "Field Wise Wages Report";
            this.btnCropDivisionFieldJob.UseVisualStyleBackColor = true;
            this.btnCropDivisionFieldJob.Click += new System.EventHandler(this.btnCropDivisionFieldJob_Click_1);
            // 
            // gbCropWise
            // 
            this.gbCropWise.Controls.Add(this.btnCropDivisionJob);
            this.gbCropWise.Controls.Add(this.btnCropDivisionFieldJob);
            this.gbCropWise.Location = new System.Drawing.Point(6, 24);
            this.gbCropWise.Name = "gbCropWise";
            this.gbCropWise.Size = new System.Drawing.Size(253, 82);
            this.gbCropWise.TabIndex = 30;
            this.gbCropWise.TabStop = false;
            this.gbCropWise.Text = "Crop Wise";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEstateJob);
            this.groupBox1.Controls.Add(this.btnEstateDivisionJob);
            this.groupBox1.Location = new System.Drawing.Point(6, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 78);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            // 
            // btnEstateJob
            // 
            this.btnEstateJob.Location = new System.Drawing.Point(6, 15);
            this.btnEstateJob.Name = "btnEstateJob";
            this.btnEstateJob.Size = new System.Drawing.Size(234, 23);
            this.btnEstateJob.TabIndex = 30;
            this.btnEstateJob.Text = "Division Wise Report";
            this.btnEstateJob.UseVisualStyleBackColor = true;
            this.btnEstateJob.Click += new System.EventHandler(this.btnEstateJob_Click);
            // 
            // btnEstateDivisionJob
            // 
            this.btnEstateDivisionJob.Location = new System.Drawing.Point(6, 44);
            this.btnEstateDivisionJob.Name = "btnEstateDivisionJob";
            this.btnEstateDivisionJob.Size = new System.Drawing.Size(234, 23);
            this.btnEstateDivisionJob.TabIndex = 28;
            this.btnEstateDivisionJob.Text = "Estate Division Wise Report";
            this.btnEstateDivisionJob.UseVisualStyleBackColor = true;
            this.btnEstateDivisionJob.Click += new System.EventHandler(this.btnEstateDivisionJob_Click);
            // 
            // btnDivisionWiseIncludingLentBorrow
            // 
            this.btnDivisionWiseIncludingLentBorrow.Location = new System.Drawing.Point(7, 15);
            this.btnDivisionWiseIncludingLentBorrow.Name = "btnDivisionWiseIncludingLentBorrow";
            this.btnDivisionWiseIncludingLentBorrow.Size = new System.Drawing.Size(261, 23);
            this.btnDivisionWiseIncludingLentBorrow.TabIndex = 31;
            this.btnDivisionWiseIncludingLentBorrow.Text = "(1) Division Wages (General and Borrow) Detail";
            this.btnDivisionWiseIncludingLentBorrow.UseVisualStyleBackColor = true;
            this.btnDivisionWiseIncludingLentBorrow.Click += new System.EventHandler(this.btnDivisionWiseIncludingLentBorrow_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gbCropWise);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(16, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 192);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "As Per Amalgamation";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLentLabourDetails);
            this.groupBox3.Controls.Add(this.btnDivisionWagesGeneralBorrowSummary);
            this.groupBox3.Controls.Add(this.btnAdditions);
            this.groupBox3.Controls.Add(this.btnDivisionSummary);
            this.groupBox3.Controls.Add(this.btnDivisionWagesGeneralLent);
            this.groupBox3.Controls.Add(this.btnDivisionWiseIncludingLentBorrow);
            this.groupBox3.Location = new System.Drawing.Point(287, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 139);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Division Cost";
            // 
            // btnDivisionWagesGeneralBorrowSummary
            // 
            this.btnDivisionWagesGeneralBorrowSummary.Location = new System.Drawing.Point(7, 44);
            this.btnDivisionWagesGeneralBorrowSummary.Name = "btnDivisionWagesGeneralBorrowSummary";
            this.btnDivisionWagesGeneralBorrowSummary.Size = new System.Drawing.Size(261, 23);
            this.btnDivisionWagesGeneralBorrowSummary.TabIndex = 35;
            this.btnDivisionWagesGeneralBorrowSummary.Text = "(2) Division Wages (General and Borrow) Summary";
            this.btnDivisionWagesGeneralBorrowSummary.UseVisualStyleBackColor = true;
            this.btnDivisionWagesGeneralBorrowSummary.Click += new System.EventHandler(this.btnDivisionWagesGeneralBorrowSummary_Click);
            // 
            // btnAdditions
            // 
            this.btnAdditions.Location = new System.Drawing.Point(22, 138);
            this.btnAdditions.Name = "btnAdditions";
            this.btnAdditions.Size = new System.Drawing.Size(246, 37);
            this.btnAdditions.TabIndex = 34;
            this.btnAdditions.Text = "Division Other Additions";
            this.btnAdditions.UseVisualStyleBackColor = true;
            // 
            // btnDivisionSummary
            // 
            this.btnDivisionSummary.Enabled = false;
            this.btnDivisionSummary.Location = new System.Drawing.Point(22, 175);
            this.btnDivisionSummary.Name = "btnDivisionSummary";
            this.btnDivisionSummary.Size = new System.Drawing.Size(246, 23);
            this.btnDivisionSummary.TabIndex = 33;
            this.btnDivisionSummary.Text = "Division Summary Including Lent and Borrow";
            this.btnDivisionSummary.UseVisualStyleBackColor = true;
            this.btnDivisionSummary.Visible = false;
            this.btnDivisionSummary.Click += new System.EventHandler(this.btnDivisionSummary_Click);
            // 
            // btnDivisionWagesGeneralLent
            // 
            this.btnDivisionWagesGeneralLent.Location = new System.Drawing.Point(7, 73);
            this.btnDivisionWagesGeneralLent.Name = "btnDivisionWagesGeneralLent";
            this.btnDivisionWagesGeneralLent.Size = new System.Drawing.Size(261, 23);
            this.btnDivisionWagesGeneralLent.TabIndex = 32;
            this.btnDivisionWagesGeneralLent.Text = "(3) Division Wages (Amalgamation Breakdown)";
            this.btnDivisionWagesGeneralLent.UseVisualStyleBackColor = true;
            this.btnDivisionWagesGeneralLent.Click += new System.EventHandler(this.btnDivisionWagesGeneralLent_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnWagesLentSummary);
            this.groupBox4.Controls.Add(this.btnWagesEstateSummary);
            this.groupBox4.Location = new System.Drawing.Point(293, 271);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(274, 77);
            this.groupBox4.TabIndex = 34;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Estate Summary";
            // 
            // btnWagesLentSummary
            // 
            this.btnWagesLentSummary.Location = new System.Drawing.Point(6, 43);
            this.btnWagesLentSummary.Name = "btnWagesLentSummary";
            this.btnWagesLentSummary.Size = new System.Drawing.Size(262, 23);
            this.btnWagesLentSummary.TabIndex = 32;
            this.btnWagesLentSummary.Text = "Lent Labour Summary";
            this.btnWagesLentSummary.UseVisualStyleBackColor = true;
            this.btnWagesLentSummary.Click += new System.EventHandler(this.btnWagesLentSummary_Click);
            // 
            // btnWagesEstateSummary
            // 
            this.btnWagesEstateSummary.Location = new System.Drawing.Point(6, 14);
            this.btnWagesEstateSummary.Name = "btnWagesEstateSummary";
            this.btnWagesEstateSummary.Size = new System.Drawing.Size(262, 23);
            this.btnWagesEstateSummary.TabIndex = 31;
            this.btnWagesEstateSummary.Text = "Estate Summary Report";
            this.btnWagesEstateSummary.UseVisualStyleBackColor = true;
            this.btnWagesEstateSummary.Click += new System.EventHandler(this.btnWagesEstateSummary_Click);
            // 
            // btnLentLabourDetails
            // 
            this.btnLentLabourDetails.Location = new System.Drawing.Point(7, 102);
            this.btnLentLabourDetails.Name = "btnLentLabourDetails";
            this.btnLentLabourDetails.Size = new System.Drawing.Size(261, 23);
            this.btnLentLabourDetails.TabIndex = 36;
            this.btnLentLabourDetails.Text = "(4) Lent Labour Details";
            this.btnLentLabourDetails.UseVisualStyleBackColor = true;
            this.btnLentLabourDetails.Click += new System.EventHandler(this.btnLentLabourDetails_Click);
            // 
            // CheckrollWages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 355);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnJobWiseWages);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCashBreakDown);
            this.Controls.Add(this.cmdFieldwise);
            this.Controls.Add(this.chkAllFields);
            this.Controls.Add(this.cmbField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkAllCrops);
            this.Controls.Add(this.cmbCropType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLent);
            this.Controls.Add(this.BFProcess);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.cmbDivision);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckrollWages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkroll Wages Report";
            this.Load += new System.EventHandler(this.CheckrollWages_Load);
            this.gbCropWise.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button BFProcess;
        private System.Windows.Forms.Button btnLent;
        private System.Windows.Forms.ComboBox cmbCropType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAllCrops;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAllFields;
        private System.Windows.Forms.Button cmdFieldwise;
        private System.Windows.Forms.Button btnCashBreakDown;
        private System.Windows.Forms.Button btnJobWiseWages;
        private System.Windows.Forms.Button btnCropDivisionJob;
        private System.Windows.Forms.Button btnCropDivisionFieldJob;
        private System.Windows.Forms.GroupBox gbCropWise;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEstateDivisionJob;
        private System.Windows.Forms.Button btnEstateJob;
        private System.Windows.Forms.Button btnDivisionWiseIncludingLentBorrow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDivisionWagesGeneralLent;
        private System.Windows.Forms.Button btnDivisionSummary;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnWagesLentSummary;
        private System.Windows.Forms.Button btnWagesEstateSummary;
        private System.Windows.Forms.Button btnAdditions;
        private System.Windows.Forms.Button btnDivisionWagesGeneralBorrowSummary;
        private System.Windows.Forms.Button btnLentLabourDetails;
    }
}