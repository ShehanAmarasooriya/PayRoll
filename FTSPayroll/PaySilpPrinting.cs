using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class PaySilpPrinting : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        

        public PaySilpPrinting()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PaySilpPrinting_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();

            cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            cmbEmployeeCategory.DisplayMember = "CategoryName";
            cmbEmployeeCategory.ValueMember = "CategoryID";

            cmbYear.Text = DateTime.Now.Year.ToString();

            if (FTSSettings.GetPayslipCropType().ToUpper().Equals("TEA"))
                chkMultiCrop.Checked = false;
            else
                chkMultiCrop.Checked = true;
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Int32 progressBarCount = 0;
            Decimal DecPercentage = 0;
            Boolean boolDataAvailable = true;
            lblPer.Text = "0%";
            LblProgress.Text = "";
            if (chkPrePrinted.Checked)
            {
                if (!chkMultiCrop.Checked)
                {
                    #region OneCropTea
                    //try
                    //{
                    DataSet dataSetReport = new DataSet();
                    DataSet dsEmp = new DataSet();

                    DataTable dt = null;

                    dsEmp = myReports.GetAllEmployeeListForPayslip(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    progressBarCount = dsEmp.Tables[0].Rows.Count;

                    if (progressBarCount > 0)
                    {

                        progressBar1.Maximum = progressBarCount + 1;
                        progressBar1.Value = 1;
                        foreach (DataRow drow in dsEmp.Tables[0].Rows)
                        {
                            if (dt == null)
                            {
                                dt = myReports.getSalarySlipsPrePrintedBPLOLAX_APL_Tea(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), false, drow[0].ToString());
                            }
                            else
                            {
                                dt.Merge(myReports.getSalarySlipsPrePrintedBPLOLAX_APL_Tea(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), false, drow[0].ToString()));
                            }
                            progressBar1.Value++;
                            DecPercentage = (progressBar1.Value * 100) / progressBarCount;
                            lblPer.Text = DecPercentage + "%";
                            LblProgress.Text = "EmpNo:" + drow[0].ToString() + " Is Processed...";
                            Application.DoEvents();
                        }
                    }
                    else
                        boolDataAvailable = false;

                    //if (rbtnCashWork.Checked)
                    //{
                    //    //if (chkOlaxPayslip.Checked)
                    //    //    dt = myReports.getSalarySlipsPrePrintedCWOLAX(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()),true);

                    //}
                    //else
                    //{
                    //    if (chkOlaxPayslip.Checked)
                    //dt = myReports.getSalarySlipsPrePrintedBPLOLAX(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), false);

                    //}

                    progressBar1.Value = progressBarCount + 1;
                    dt.TableName = "PaySlips";
                    dataSetReport.Tables.Add(dt);
                    dataSetReport.WriteXml("PaySlips.xml");
                    lblPer.Text = "100%";
                    LblProgress.Text = "";

                    //PaySlipOLAXRPT myaclist = new PaySlipOLAXRPT();
                    if (chkPrePrinted.Checked)
                    {
                        if (rbtnA4.Checked)
                        {
                            PayslipOLAX_A4_RPT myaclist = new PayslipOLAX_A4_RPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            //myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                            myaclist.SetParameterValue("DivisionID", myDivision.GetDivisionName(cmbDivision.SelectedValue.ToString()).Tables[0].Rows[0][0].ToString() + " - " + " Month of " + cmbMonth.Text + " " + cmbYear.Text);
                            myaclist.SetParameterValue("Title", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();

                            dt.Dispose();
                            progressBar1.Value = 0;
                        }
                        else if (rbtnLetter.Checked)
                        {
                            PayslipOLAX_Letter_RPT myaclist = new PayslipOLAX_Letter_RPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                            myaclist.SetParameterValue("DivisionID", myDivision.GetDivisionName(cmbDivision.SelectedValue.ToString()).Tables[0].Rows[0][0].ToString() + " - " + " Month of " + cmbMonth.Text + " " + cmbYear.Text);
                            myaclist.SetParameterValue("Title", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();

                            dt.Dispose();
                            progressBar1.Value = 0;
                        }
                        else
                        {
                            PayslipOLAX_USFanfold_Preprinted_RPT myaclist = new PayslipOLAX_USFanfold_Preprinted_RPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                            myaclist.SetParameterValue("DivisionID", myDivision.GetDivisionName(cmbDivision.SelectedValue.ToString()).Tables[0].Rows[0][0].ToString() + " - " + " Month of " + cmbMonth.Text + " " + cmbYear.Text);
                            myaclist.SetParameterValue("Title", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();

                            dt.Dispose();
                            progressBar1.Value = 0;
                        }

                        /*End*/
                    } 
                    #endregion
                }
                else
                {
                    #region MultiCropPayslip
                    //try
                    //{
                    DataSet dataSetReport = new DataSet();
                    DataSet dsEmp = new DataSet();

                    DataTable dt = null;

                    dsEmp = myReports.GetAllEmployeeListForPayslip(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    progressBarCount = dsEmp.Tables[0].Rows.Count;

                    if (progressBarCount > 0)
                    {

                        progressBar1.Maximum = progressBarCount + 1;
                        progressBar1.Value = 1;
                        foreach (DataRow drow in dsEmp.Tables[0].Rows)
                        {
                            if (dt == null)
                            {
                                dt = myReports.getSalarySlipsPrePrintedBPLOLAX_APL_MultiCrop(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), false, drow[0].ToString());
                            }
                            else
                            {
                                dt.Merge(myReports.getSalarySlipsPrePrintedBPLOLAX_APL_MultiCrop(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), false, drow[0].ToString()));
                            }
                            progressBar1.Value++;
                            DecPercentage = (progressBar1.Value * 100) / progressBarCount;
                            lblPer.Text = DecPercentage + "%";
                            LblProgress.Text = "EmpNo:" + drow[0].ToString() + " Is Processed...";
                            Application.DoEvents();
                        }
                    }
                    else
                        boolDataAvailable = false;

                    //if (rbtnCashWork.Checked)
                    //{
                    //    //if (chkOlaxPayslip.Checked)
                    //    //    dt = myReports.getSalarySlipsPrePrintedCWOLAX(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()),true);

                    //}
                    //else
                    //{
                    //    if (chkOlaxPayslip.Checked)
                    //dt = myReports.getSalarySlipsPrePrintedBPLOLAX(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, cmbEmployeeCategory.Text, Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()), false);

                    //}

                    progressBar1.Value = progressBarCount + 1;
                    dt.TableName = "PaySlips";
                    dataSetReport.Tables.Add(dt);
                    dataSetReport.WriteXml("PaySlips.xml");
                    lblPer.Text = "100%";
                    LblProgress.Text = "";

                    //PaySlipOLAXRPT myaclist = new PaySlipOLAXRPT();
                    if (chkPrePrinted.Checked)
                    {
                        if (rbtnA4.Checked)
                        {
                            PayslipOLAX_A4_RPT myaclist = new PayslipOLAX_A4_RPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                            myaclist.SetParameterValue("DivisionID", myDivision.GetDivisionName(cmbDivision.SelectedValue.ToString()).Tables[0].Rows[0][0].ToString() + " - " + " Month of " + cmbMonth.Text + " " + cmbYear.Text);
                            myaclist.SetParameterValue("Title", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();

                            dt.Dispose();
                            progressBar1.Value = 0;
                        }
                        else if (rbtnLetter.Checked)
                        {
                            PayslipOLAX_Letter_RPT myaclist = new PayslipOLAX_Letter_RPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                            myaclist.SetParameterValue("DivisionID", myDivision.GetDivisionName(cmbDivision.SelectedValue.ToString()).Tables[0].Rows[0][0].ToString() + " - " + " Month of " + cmbMonth.Text + " " + cmbYear.Text);
                            myaclist.SetParameterValue("Title", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();

                            dt.Dispose();
                            progressBar1.Value = 0;
                        }
                        else
                        {
                            PayslipOLAX_USFanfold_Preprinted_MultiCrop_RTP myaclist = new PayslipOLAX_USFanfold_Preprinted_MultiCrop_RTP();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                            myaclist.SetParameterValue("DivisionID", myDivision.GetDivisionName(cmbDivision.SelectedValue.ToString()).Tables[0].Rows[0][0].ToString() + " - " + " Month of " + cmbMonth.Text + " " + cmbYear.Text);
                            myaclist.SetParameterValue("Title", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();

                            dt.Dispose();
                            progressBar1.Value = 0;
                        }

                        /*End*/
                        
                    #endregion
                    }
                }
            }
        }

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkPrePrinted.Checked)
        //    {
        //        //Print in Pre-Printed
        //        gbLanguages.Enabled = false;
        //    }
        //    else
        //    {
        //        gbLanguages.Enabled = true;
        //    }
        //}

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}