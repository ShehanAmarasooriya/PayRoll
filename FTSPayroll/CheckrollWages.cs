using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CheckrollWages : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CHKWages myWage = new FTSPayRollBL.CHKWages();
        FTSPayRollBL.EstateDivisionBlock myEst = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.ProcessMonthlyWages proWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();

        public CheckrollWages()
        {
            InitializeComponent();
        }

        private void CheckrollWages_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEst.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            myMonth.getLastYearID();

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            cmbYear.SelectedValue = DateTime.Now.Year;


            cmbYear_SelectedIndexChanged(null, null);

            //cmbMonth.DataSource = myMonth.ListMonths();
            //cmbMonth.DisplayMember = "Month";
            //cmbMonth.ValueMember = "MId";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            String Division = "ALL";
            if (chkAll.Checked)
            {
                Division = "ALL";
            }
            else
            {
                Division = cmbDivision.SelectedValue.ToString();
            }
            DataTable dtDiv = new DataTable();
            Int32 intYear = Convert.ToInt32(cmbYear.Text);
            Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            DataSet ds1 = new DataSet();
            dtDiv = myWage.GetDivisionWiseCHKWages(intYear, intMonth, Division);
            ds1.Tables.Add(dtDiv);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ds1.WriteXml("DivisionWiseWages.xml");
                DivisionWiseWages myReport1 = new DivisionWiseWages();
                myReport1.SetDataSource(ds1);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String StringDivision = "";
            if (chkAll.Checked)
            {
                StringDivision = "ALL";
            }
            else
            {
                StringDivision = cmbDivision.SelectedValue.ToString();
                DataTable dtDiv = new DataTable();
                Int32 intYear = Convert.ToInt32(cmbYear.Text);
                Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                DataSet ds1 = new DataSet();
                dtDiv = myWage.GetFieldWiseCHKWages(intYear, intMonth, StringDivision);
                ds1.Tables.Add(dtDiv);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ds1.WriteXml("FieldWiseWages.xml");
                    FieldWiseWages myReport1 = new FieldWiseWages();
                    myReport1.SetDataSource(ds1);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to preview..!");
                }
            }


        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                chkAllFields.Checked = true;
                chkAllFields.Enabled = false;
                cmbField.Enabled = false;
                cmbDivision.Enabled = false;
            }
            else
            {
                chkAllFields.Checked = false;
                chkAllFields.Enabled = true;
                cmbField.Enabled = true;
                cmbDivision.Enabled = true;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = myMonth.ListMonths(Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";

                cmbMonth.SelectedValue = myMonth.getLastMonthID();
            }
            catch
            {
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            String Division = "%";
            if (chkAll.Checked)
            {
                Division = "%";
            }
            else
            {
                Division = cmbDivision.SelectedValue.ToString();
            }
            DataTable dtDiv = new DataTable();
            Int32 intYear = Convert.ToInt32(cmbYear.Text);
            Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            DataSet ds1 = new DataSet();
            ds1 = myWage.GetCheckrollWagesForMonth(intYear, intMonth, Division);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ds1.WriteXml("CheckrollWagesForMonth.xml");
                CheckrollWagesForMonthRPT myReport1 = new CheckrollWagesForMonthRPT();
                myReport1.SetDataSource(ds1);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + Division);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }
        }

        public void ProcessGLEntries(out bool blAllreadyExt)
        {
            DataTable divTable;
            DataTable WCFIDdt;
            blAllreadyExt = false;

            myWage.IntYear = Convert.ToInt32(cmbYear.Text);
            myWage.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            myWage.ProcessFromDate = new DateTime(myWage.IntYear, myWage.IntMonth, 1);
            myWage.ProcessToDate = new DateTime(myWage.IntYear, myWage.IntMonth, DateTime.DaysInMonth(myWage.IntYear, myWage.IntMonth));

            if (chkAll.Checked)
            {
                divTable = myEst.ListEstateDivisions();
            }
            else
            {
                myWage.StrDivID = cmbDivision.SelectedValue.ToString();
                divTable = myEst.ListEstateDivisions(myWage.StrDivID);
            }

            foreach (DataRow drow1 in divTable.Rows)
            {
                if (!myWage.isAvailable(drow1[0].ToString(), myWage.IntYear, myWage.IntMonth))
                {
                    myWage.StrDivID = drow1[0].ToString();
                    //WCFIDdt = proWages.getWorkCodesAndFieldsFromDailyGroundTransactions(drow1[0].ToString(), myWage.ProcessFromDate, myWage.ProcessToDate);
                    WCFIDdt = proWages.getWorkCodesAndFieldsFromDailyGroundTransactions(drow1[0].ToString(), myWage.ProcessFromDate, myWage.ProcessToDate);

                    String strGLProStatusNew = "";

                    if (WCFIDdt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in WCFIDdt.Rows)
                        {
                            try
                            {
                                strGLProStatusNew = myWage.processGLEntriesNEW(dr[0].ToString(), dr[1].ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "GL Error");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Records Exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else
                {
                    blAllreadyExt = true;
                    MessageBox.Show("Records Already Exist for" + " " + drow1[0].ToString() + "Division", "Information", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }

        }

        private void BFProcess_Click(object sender, EventArgs e)
        {

            if (cmbDivision.SelectedValue == null && chkAll.Checked == false)
            {
                MessageBox.Show("Select a Division", "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbYear.Text == null)
            {
                MessageBox.Show("Select a Year", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbYear.Focus();
            }
            else if (cmbMonth.SelectedValue == null)
            {
                MessageBox.Show("Select a Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbField.SelectedValue == null && chkAllFields.Checked == false)
            {
                MessageBox.Show("Select a Field", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String Division = "%";
                String Field = "%";
                String strAllCrops = "%";

                if (chkAll.Checked && chkAllFields.Checked)
                {
                    Division = "%";
                    Field = "%";
                }
                else if (chkAll.Checked == false && chkAllFields.Checked)
                {
                    Division = cmbDivision.SelectedValue.ToString();
                    Field = "%";
                }
                else if (chkAll.Checked && chkAllFields.Checked == false)
                {
                    Division = "%";
                    Field = "%";
                }
                else
                {
                    Division = cmbDivision.SelectedValue.ToString();
                    Field = cmbField.SelectedValue.ToString();
                }

                //bool blAllreadyExt = false;
                //ProcessGLEntries(out blAllreadyExt);

                //if (blAllreadyExt == true)
                //{
                //    MessageBox.Show("Records already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                //else
                //{
                //    MessageBox.Show("Records copied successfully!", "information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}

                if (chkAllCrops.Checked)
                {
                    strAllCrops = "%";
                }
                else
                {
                    strAllCrops = cmbCropType.SelectedValue.ToString();
                }

                //if (chkAll.Checked)
                //{
                //    Division = "%";
                //}
                //else
                //{
                //    Division = cmbDivision.SelectedValue.ToString();
                //}

                DataTable dtDiv = new DataTable();
                Int32 intYear = Convert.ToInt32(cmbYear.Text);
                Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                DataSet ds1 = new DataSet();

                ds1 = myWage.GetCheckrollWagesForMonthBFProcessJobWise(intYear, intMonth, Division, strAllCrops);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ds1.WriteXml("CheckrollWagesForMonthBFProcess.xml");
                    CheckrollWagesForMonthBFProcessRPT myReport1 = new CheckrollWagesForMonthBFProcessRPT();
                    myReport1.SetDataSource(ds1);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + Division);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to preview..!");
                }
            }
        }

        private void btnLent_Click(object sender, EventArgs e)
        {

            if (cmbDivision.SelectedValue == null && chkAll.Checked == false)
            {
                MessageBox.Show("Select a Division", "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbYear.Text == null)
            {
                MessageBox.Show("Select a Year", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbYear.Focus();
            }
            else if (cmbMonth.SelectedValue == null)
            {
                MessageBox.Show("Select a Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbField.SelectedValue == null && chkAllFields.Checked == false)
            {
                MessageBox.Show("Select a Field", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String Division = "%";
                String Field = "%";
                String strAllCrops = "%";

                if (chkAll.Checked && chkAllFields.Checked)
                {
                    Division = "%";
                    Field = "%";
                }
                else if (chkAll.Checked == false && chkAllFields.Checked)
                {
                    Division = cmbDivision.SelectedValue.ToString();
                    Field = "%";
                }
                else if (chkAll.Checked && chkAllFields.Checked == false)
                {
                    Division = "%";
                    Field = "%";
                }
                else
                {
                    Division = cmbDivision.SelectedValue.ToString();
                    Field = cmbField.SelectedValue.ToString();
                }

                if (chkAllCrops.Checked)
                {
                    strAllCrops = "%";
                }
                else
                {
                    strAllCrops = cmbCropType.SelectedValue.ToString();
                }

                DataTable dtDiv = new DataTable();
                Int32 intYear = Convert.ToInt32(cmbYear.Text);
                Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                DataSet ds1 = new DataSet();
                ds1 = myWage.GetCheckrollWagesForMonthBFProcessLentLabour(intYear, intMonth, Division);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ds1.WriteXml("CheckrollWagesForMonthBFProcessLent.xml");
                    CheckrollWagesForMonthBFProcessLentRPT myReport1 = new CheckrollWagesForMonthBFProcessLentRPT();
                    myReport1.SetDataSource(ds1);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + Division);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to preview..!");
                }
            }
        }

        private void chkAllCrops_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCrops.Checked)
            {
                cmbCropType.Enabled = false;
            }
            else
            {
                cmbCropType.Enabled = true;
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbField.DataSource = myWage.GetEstateFieldsbyDivision(cmbDivision.SelectedValue.ToString());
            cmbField.DisplayMember = "FieldID";
            cmbField.ValueMember = "FieldID";
        }

        private void chkAllFields_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllFields.Checked)
            {
                cmbField.Enabled = false;
            }
            else
            {
                cmbField.Enabled = true;
            }
        }

        private void cmdFieldwise_Click(object sender, EventArgs e)
        {
            if (cmbDivision.SelectedValue == null && chkAll.Checked == false)
            {
                MessageBox.Show("Select a Division", "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbYear.Text == null)
            {
                MessageBox.Show("Select a Year", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbYear.Focus();
            }
            else if (cmbMonth.SelectedValue == null)
            {
                MessageBox.Show("Select a Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbField.SelectedValue == null && chkAllFields.Checked == false)
            {
                MessageBox.Show("Select a Field", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String Division = "%";
                String Field = "%";
                String strAllCrops = "%";

                if (!chkAll.Checked)
                {
                    Division = cmbDivision.SelectedValue.ToString();
                }
                if (!chkAllFields.Checked)
                {
                    Field = cmbField.SelectedValue.ToString();
                }


                //bool blAllreadyExt = false;
                //ProcessGLEntries(out blAllreadyExt);

                //if (blAllreadyExt == true)
                //{
                //    MessageBox.Show("Records already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                //else
                //{
                //    MessageBox.Show("Records copied successfully!", "information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}

                if (chkAllCrops.Checked)
                {
                    strAllCrops = "%";
                }
                else
                {
                    strAllCrops = cmbCropType.SelectedValue.ToString();
                }

                //if (chkAll.Checked)
                //{
                //    Division = "%";
                //}
                //else
                //{
                //    Division = cmbDivision.SelectedValue.ToString();
                //}

                DataTable dtDiv = new DataTable();
                Int32 intYear = Convert.ToInt32(cmbYear.Text);
                Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                DataSet ds1 = new DataSet();

                ds1 = myWage.GetCheckrollWagesForMonthBFProcess(intYear, intMonth, Division, strAllCrops);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ds1.WriteXml("CheckrollWagesForMonthBFProcess.xml");
                    CheckrollWagesFieldWiseRPT myReport1 = new CheckrollWagesFieldWiseRPT();
                    myReport1.SetDataSource(ds1);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + Division);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to preview..!");
                }
            }
            #region MyRegion
            //if (cmbDivision.SelectedValue == null && chkAll.Checked == false)
            //{
            //    MessageBox.Show("Select a Division", "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (cmbYear.Text == null)
            //{
            //    MessageBox.Show("Select a Year", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cmbYear.Focus();
            //}
            //else if (cmbMonth.SelectedValue == null)
            //{
            //    MessageBox.Show("Select a Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (cmbField.SelectedValue == null && chkAllFields.Checked == false)
            //{
            //    MessageBox.Show("Select a Field", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    String Division = "%";
            //    String Field = "%";
            //    String strAllCrops = "%";

            //    if (chkAll.Checked && chkAllFields.Checked)
            //    {
            //        Division = "%";
            //        Field = "%";
            //    }
            //    else if (chkAll.Checked == false && chkAllFields.Checked)
            //    {
            //        Division = cmbDivision.SelectedValue.ToString();
            //        Field = "%";
            //    }
            //    else if (chkAll.Checked && chkAllFields.Checked == false)
            //    {
            //        Division = "%";
            //        Field = "%";
            //    }
            //    else
            //    {
            //        Division = cmbDivision.SelectedValue.ToString();
            //        Field = cmbField.SelectedValue.ToString();
            //    }

            //    bool blAllreadyExt = false;
            //    ProcessGLEntries(out blAllreadyExt);

            //    if (blAllreadyExt == true)
            //    {
            //        MessageBox.Show("Records already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Records copied successfully!", "information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }

            //    if (chkAllCrops.Checked)
            //    {
            //        strAllCrops = "%";
            //    }
            //    else
            //    {
            //        strAllCrops = cmbCropType.SelectedValue.ToString();
            //    }

            //    //if (chkAll.Checked)
            //    //{
            //    //    Division = "%";
            //    //}
            //    //else
            //    //{
            //    //    Division = cmbDivision.SelectedValue.ToString();
            //    //}

            //    DataTable dtDiv = new DataTable();
            //    Int32 intYear = Convert.ToInt32(cmbYear.Text);
            //    Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            //    DataSet ds1 = new DataSet();

            //    ds1 = myWage.GetCheckrollWagesForMonthBFProcessFieldwise(intYear, intMonth, Division, Field, strAllCrops);

            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        ds1.WriteXml("CheckrollWagesForMonthBFProcess.xml");
            //        CheckrollWagesForMonthBFProcessFiledwiseRPT myReport1 = new CheckrollWagesForMonthBFProcessFiledwiseRPT();
            //        myReport1.SetDataSource(ds1);
            //        ReportViewer myViewer = new ReportViewer();
            //        myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
            //        myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
            //        myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            //        if (chkAll.Checked)
            //        {
            //            myReport1.SetParameterValue("Division", "All Division");
            //        }
            //        else
            //        {
            //            myReport1.SetParameterValue("Division", "Division " + Division);
            //        }
            //        if (chkAllFields.Checked)
            //        {
            //            myReport1.SetParameterValue("Field", "All");
            //        }
            //        else
            //        {
            //            myReport1.SetParameterValue("Field", cmbField.Text);
            //        }
            //        if (chkAllCrops.Checked)
            //        {
            //            myReport1.SetParameterValue("Crop Type", "ALL");
            //        }
            //        else
            //        {
            //            myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
            //        }

            //        myViewer.crystalReportViewer1.ReportSource = myReport1;
            //        myViewer.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data to preview..!");
            //    }
            //} 
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbDivision.SelectedValue == null && chkAll.Checked == false)
            {
                MessageBox.Show("Select a Division", "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbYear.Text == null)
            {
                MessageBox.Show("Select a Year", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbYear.Focus();
            }
            else if (cmbMonth.SelectedValue == null)
            {
                MessageBox.Show("Select a Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbField.SelectedValue == null && chkAllFields.Checked == false)
            {
                MessageBox.Show("Select a Field", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String Division = "%";
                String Field = "%";
                String strAllCrops = "%";

                if (chkAll.Checked && chkAllFields.Checked)
                {
                    Division = "%";
                    Field = "%";
                }
                else if (chkAll.Checked == false && chkAllFields.Checked)
                {
                    Division = cmbDivision.SelectedValue.ToString();
                    Field = "%";
                }
                else if (chkAll.Checked && chkAllFields.Checked == false)
                {
                    Division = "%";
                    Field = "%";
                }
                else
                {
                    Division = cmbDivision.SelectedValue.ToString();
                    Field = cmbField.SelectedValue.ToString();
                }

                bool blAllreadyExt = false;
                ProcessGLEntries(out blAllreadyExt);

                if (blAllreadyExt == true)
                {
                    MessageBox.Show("Records already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Records copied successfully!", "information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (chkAllCrops.Checked)
                {
                    strAllCrops = "%";
                }
                else
                {
                    strAllCrops = cmbCropType.SelectedValue.ToString();
                }

                //if (chkAll.Checked)
                //{
                //    Division = "%";
                //}
                //else
                //{
                //    Division = cmbDivision.SelectedValue.ToString();
                //}

                DataTable dtDiv = new DataTable();
                Int32 intYear = Convert.ToInt32(cmbYear.Text);
                Int32 intMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                DataSet ds1 = new DataSet();

                ds1 = myWage.GetCheckrollWagesForMonthBFProcessFieldwise(intYear, intMonth, Division, Field, strAllCrops);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ds1.WriteXml("CheckrollWagesForMonthBFProcess.xml");
                    CheckrollWagesForMonthBFProcessFiledwiseRPT myReport1 = new CheckrollWagesForMonthBFProcessFiledwiseRPT();
                    myReport1.SetDataSource(ds1);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + Division);
                    }
                    if (chkAllFields.Checked)
                    {
                        myReport1.SetParameterValue("Field", "All");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Field", cmbField.Text);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to preview..!");
                }
            }
        }

        private void btnCashBreakDown_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = myWage.GetCashWorkBreakDown(strAllDiv, strCrop, dtFrom, dtTo);
            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollCashBreakDown.xml");
                WagesCashWorkBreakDownRPT myReport1 = new WagesCashWorkBreakDownRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
        }

        private void btnJobWiseWages_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = myWage.GetCropWiseWages(strAllDiv, strCrop, dtFrom, dtTo);
            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWages.xml");
                CheckrollWagesJobWiseRPT myReport1 = new CheckrollWagesJobWiseRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
        }

        private void btnCropDivisionJob_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWages(strAllDiv, strCrop, dtFrom, dtTo));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesXML.xml");
                CheckrollWagesRPT myReport1 = new CheckrollWagesRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
        }

        private void btnCropDivisionFieldJob_Click_1(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWages(strAllDiv, strCrop, dtFrom, dtTo));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesXML.xml");
                CheckrollWagesFiedWiseWagesRPT myReport1 = new CheckrollWagesFiedWiseWagesRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
        }

        private void btnEstateDivisionJob_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWages(strAllDiv, strCrop, dtFrom, dtTo));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesXML.xml");
                CheckrollWages_EstateDivisionJobRPT myReport1 = new CheckrollWages_EstateDivisionJobRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Display");
            }
        }

        private void btnEstateJob_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWages(strAllDiv, strCrop, dtFrom, dtTo));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesXML.xml");
                CheckrollWages_EstateJobRPT myReport1 = new CheckrollWages_EstateJobRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }





        }

        private void btnEstateDivisionFieldJob_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWages(strAllDiv, strCrop, dtFrom, dtTo));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesXML.xml");
                CheckrollWagesRPT myReport1 = new CheckrollWagesRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Display");
            }
        }

        private void btnDivisionWiseIncludingLentBorrow_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                MessageBox.Show("Select One Division, This Report Not Support To All Division");
            }
            else
            {
                String strCrop = "%";
                String strAllDiv = "%";
                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
                if (chkAllCrops.Checked)
                    strCrop = "%";
                else
                    strCrop = cmbCropType.SelectedValue.ToString();
                if (chkAll.Checked)
                    strAllDiv = "%";
                else
                    strAllDiv = cmbDivision.SelectedValue.ToString();
                DataSet dsWagesRPT = new DataSet();
                dsWagesRPT.Tables.Add(myWage.GetWagesIncludingBothLentBorrow(strAllDiv, strCrop, dtFrom, dtTo,1));

                if (dsWagesRPT.Tables[0].Rows.Count > 0)
                {
                    dsWagesRPT.WriteXml("CheckrollWagesIncludingLentBorrowXML.xml");
                    CheckrollWages_DivisionIncLentBorrowRPT myReport1 = new CheckrollWages_DivisionIncLentBorrowRPT();
                    myReport1.SetDataSource(dsWagesRPT);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport1.SetParameterValue("Title", "Checkroll Wages For Division (General And Borrowing) Detail -(1)");
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
            }
        }

        

        private void btnDivisionSummary_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWagesIncludingBothLentBorrow(strAllDiv, strCrop, dtFrom, dtTo,1));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesIncludingLentBorrowXML.xml");
                CheckrollWages_DivisionSummaryIncLentBorrowRPT myReport1 = new CheckrollWages_DivisionSummaryIncLentBorrowRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
        }

        private void btnWagesEstateSummary_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            if (chkAllCrops.Checked)
                strCrop = "%";
            else
                strCrop = cmbCropType.SelectedValue.ToString();
            if (chkAll.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = new DataSet();
            dsWagesRPT.Tables.Add(myWage.GetWages(strAllDiv, strCrop, dtFrom, dtTo));

            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollWagesXML.xml");
                CheckrollWages_EstateSummaryRPT myReport1 = new CheckrollWages_EstateSummaryRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkAll.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                if (chkAllCrops.Checked)
                {
                    myReport1.SetParameterValue("Crop Type", "ALL");
                }
                else
                {
                    myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                }

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Display");
            }
        }

        private void btnWagesLentSummary_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            String strAllDiv = "%";

            if (chkAll.Checked)
            {
                strAllDiv = "%";
            }
            else
            {
                strAllDiv = cmbDivision.SelectedValue.ToString();
            }

            dt = myWage.GetLentLabourData(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), strAllDiv);
            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("LentLabourDetail.xml");

                LentLabourCostRep1 myLent = new LentLabourCostRep1();
                myLent.SetDataSource(ds);

                ReportViewer myView = new ReportViewer();
                myLent.SetParameterValue("Estate", "Estate :" + myEst.ListEstates().Rows[0][0].ToString());
                myLent.SetParameterValue("Date", "Checkroll Wages Report for the Month of " + cmbMonth.Text + " " + cmbYear.Text);
                myLent.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());

                myView.crystalReportViewer1.ReportSource = myLent;
                myView.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }
        }

        private void btnDivisionWagesGeneralLent_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                MessageBox.Show("Select One Division, This Report Not Support To All Division");
            }
            else
            {
                String strCrop = "%";
                String strAllDiv = "%";
                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
                if (chkAllCrops.Checked)
                    strCrop = "%";
                else
                    strCrop = cmbCropType.SelectedValue.ToString();
                if (chkAll.Checked)
                    strAllDiv = "%";
                else
                    strAllDiv = cmbDivision.SelectedValue.ToString();
                DataSet dsWagesRPT = new DataSet();
                dsWagesRPT.Tables.Add(myWage.GetWagesIncludingBothLentBorrow(strAllDiv, strCrop, dtFrom, dtTo, 2));

                if (dsWagesRPT.Tables[0].Rows.Count > 0)
                {
                    dsWagesRPT.WriteXml("CheckrollWagesIncludingLentBorrowXML.xml");
                    CheckrollWages_DivisionIncLentBorrowRPT myReport1 = new CheckrollWages_DivisionIncLentBorrowRPT();
                    myReport1.SetDataSource(dsWagesRPT);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport1.SetParameterValue("Title", "Checkroll Wages (Amalgamation Break Down) -(3)");
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
            }
        }

        private void btnDivisionWagesGeneralBorrowSummary_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                MessageBox.Show("Select One Division, This Report Not Support To All Division");
            }
            else
            {
                String strCrop = "%";
                String strAllDiv = "%";
                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
                if (chkAllCrops.Checked)
                    strCrop = "%";
                else
                    strCrop = cmbCropType.SelectedValue.ToString();
                if (chkAll.Checked)
                    strAllDiv = "%";
                else
                    strAllDiv = cmbDivision.SelectedValue.ToString();
                DataSet dsWagesRPT = new DataSet();
                dsWagesRPT.Tables.Add(myWage.GetWagesIncludingBothLentBorrow(strAllDiv, strCrop, dtFrom, dtTo, 1));

                if (dsWagesRPT.Tables[0].Rows.Count > 0)
                {
                    dsWagesRPT.WriteXml("CheckrollWagesIncludingLentBorrowXML.xml");
                    CheckrollWages_DivisionIncLentBorrowSummaryRPT myReport1 = new CheckrollWages_DivisionIncLentBorrowSummaryRPT();
                    myReport1.SetDataSource(dsWagesRPT);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport1.SetParameterValue("Title", "Checkroll Wages For Division (General And Borrowing) Summary -(2)");
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
            }
        }

        private void btnLentLabourDetails_Click(object sender, EventArgs e)
        {
            //if (chkAll.Checked)
            //{
            //    MessageBox.Show("Select One Division, This Report Not Support To All Division");
            //}
            //else
            //{
                String strCrop = "%";
                String strAllDiv = "%";
                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
                if (chkAllCrops.Checked)
                    strCrop = "%";
                else
                    strCrop = cmbCropType.SelectedValue.ToString();
                if (chkAll.Checked)
                    strAllDiv = "%";
                else
                    strAllDiv = cmbDivision.SelectedValue.ToString();
                DataSet dsWagesRPT = new DataSet();
                dsWagesRPT.Tables.Add(myWage.GetWagesLentLabourSummary(strAllDiv, strCrop, dtFrom, dtTo, 1));

                if (dsWagesRPT.Tables[0].Rows.Count > 0)
                {
                    dsWagesRPT.WriteXml("CheckrollWagesLentLabourSummaryXML.xml");
                    CheckrollWages_LentLabourRPT myReport1 = new CheckrollWages_LentLabourRPT();
                    myReport1.SetDataSource(dsWagesRPT);
                    ReportViewer myViewer = new ReportViewer();
                    myReport1.SetParameterValue("Date", cmbYear.Text + " of " + cmbMonth.Text);
                    myReport1.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString());
                    myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport1.SetParameterValue("Title", "Checkroll Wages Lent Labour -(4)");
                    if (chkAll.Checked)
                    {
                        myReport1.SetParameterValue("Division", "All Division");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                    }
                    if (chkAllCrops.Checked)
                    {
                        myReport1.SetParameterValue("Crop Type", "ALL");
                    }
                    else
                    {
                        myReport1.SetParameterValue("Crop Type", cmbCropType.Text);
                    }

                    myViewer.crystalReportViewer1.ReportSource = myReport1;
                    myViewer.Show();
                }
            //}
        }

       
    }
}