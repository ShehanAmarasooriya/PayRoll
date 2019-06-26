using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AmalgamationRPT : Form
    {
        FTSPayRollBL.YearMonth myYM = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.ChkReconciliation myRep = new FTSPayRollBL.ChkReconciliation();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.ProcessMonthlyWages myProMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        
        public AmalgamationRPT()
        {
            InitializeComponent();
        }

        private void AmalgamationRPT_Load(object sender, EventArgs e)
        {
           // cmbYear.SelectedIndex = 0;
            cmbYear.DataSource = myYM.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYM.getLastYearID();

            cmbMonth.DataSource = myYM.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myYM.getLastMonthID();

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                String strMonth = cmbMonth.Text;

                
                    DataTable dt3 = myYM.ListMonths();
                    

                        //dt = myRep.GetAmalgamation(cmbYear.Text, month);
                        dt = myRep.GetAmalgamation(cmbYear.Text, Convert.ToInt32(cmbMonth.SelectedValue.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);
                            ds.WriteXml("AmalgamaionRep.xml");

                            AmalgamationReport myReport = new AmalgamationReport();
                            myReport.SetDataSource(ds);
                            ReportViewer myViewer = new ReportViewer();

                            myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                            myReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                            myReport.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                            if (myProMWages.IsAllDivisionsProcessUnsuccessful(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString())))
                            {
                                myReport.SetParameterValue("ProcessedStatus", "");
                            }
                            else
                            {
                                myReport.SetParameterValue("ProcessedStatus", "NOT FINALIZED");
                            }
                            myViewer.crystalReportViewer1.ReportSource = myReport;
                            myViewer.Show();

                        }
                        else
                        {
                            MessageBox.Show("No Data Preview..!");
                        }
                   
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!"+ ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOldDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                String strMonth = cmbMonth.Text;

                for (Int32 month = 1; month <= 12; month++)
                {
                    if (strMonth == myYM.ListMonths().Rows[month - 1][1].ToString())
                    {

                        //dt = myRep.GetAmalgamation(cmbYear.Text, month);
                        dt = myRep.GetAmalgamationOld(cmbYear.Text, month);

                        if (dt.Rows.Count > 0)
                        {
                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);
                            ds.WriteXml("AmalgamaionRep.xml");

                            AmalgamationReport myReport = new AmalgamationReport();
                            myReport.SetDataSource(ds);
                            ReportViewer myViewer = new ReportViewer();

                            myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                            myReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                            myReport.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                            myReport.SetParameterValue("ProcessedStatus", "OLD REPORT");
                            myViewer.crystalReportViewer1.ReportSource = myReport;
                            myViewer.Show();

                        }
                        else
                        {
                            MessageBox.Show("No Data Preview..!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!" + ex.Message);
            }
        }

        private void LinkLbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataSet ds = new DataSet();
            String strMonth = cmbMonth.Text;

            DateTime dtDate = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtPreviousDate = dtDate.AddDays(-1);

            ds = myRep.GetInactiveEmployeesCoins(cmbYear.Text, dtPreviousDate.Month);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("InactiveEmpCoins.xml");

                InactiveEmpCoins myCoins = new InactiveEmpCoins();
                myCoins.SetDataSource(ds);
                ReportViewer myViewer = new ReportViewer();

                myCoins.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myCoins.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                myCoins.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                myViewer.crystalReportViewer1.ReportSource = myCoins;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data Preview..!");
            }
        }

       
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataSet ds = new DataSet();
            String strMonth = cmbMonth.Text;

            DateTime dtDate = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtPreviousDate = dtDate.AddDays(-1);

            ds = myRep.GetInactiveEmployeesDebts(cmbYear.Text, dtPreviousDate.Month);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("InactiveEmpDebts.xml");

                InactiveEmployeeDebts myDebts = new InactiveEmployeeDebts();
                myDebts.SetDataSource(ds);
                ReportViewer myViewer = new ReportViewer();

                myDebts.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myDebts.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                myDebts.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                myViewer.crystalReportViewer1.ReportSource = myDebts;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data Preview..!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                String strMonth = cmbMonth.Text;


                DataTable dt3 = myYM.ListMonths();


                //dt = myRep.GetAmalgamation(cmbYear.Text, month);
                dt = myRep.GetAmalgamationDashBoard(cmbYear.Text, Convert.ToInt32(cmbMonth.SelectedValue.ToString()));

                if (dt.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.WriteXml("AmalgamaionRep.xml");

                    AmalgamationReport myReport = new AmalgamationReport();
                    myReport.SetDataSource(ds);
                    ReportViewer myViewer = new ReportViewer();

                    myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                    myReport.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                    myViewer.crystalReportViewer1.ReportSource = myReport;
                    myViewer.Show();

                }
                else
                {
                    MessageBox.Show("No Data Preview..!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!" + ex.Message);
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}