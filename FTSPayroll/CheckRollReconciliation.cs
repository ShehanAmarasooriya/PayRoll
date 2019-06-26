using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CheckRollReconciliation : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();

        public CheckRollReconciliation()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckRollReconciliation_Load(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = myDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            try
            {
                String strDivisionID = cmbDivision.SelectedValue.ToString();

                DataSet dataSetReport = new DataSet();
                dataSetReport = myReports.getCheckRollReconcilation(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), (cmbDivision.SelectedValue.ToString()));
                dataSetReport.WriteXml("CheckRollReconcilation.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {

                    CheckRollReconcilationRPT1 myaclist = new CheckRollReconcilationRPT1();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Division Name", myDiv.EstateDivision(strDivisionID).Tables[0].Rows[0][0].ToString());
                    myaclist.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            try
            {
                String strDivisionID = cmbDivision.SelectedValue.ToString();

                DataSet dataSetReport = new DataSet();
                dataSetReport = myReports.getCheckRollReconcilation2(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), (cmbDivision.SelectedValue.ToString()));
                dataSetReport.WriteXml("CheckRollReconcilation2.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    CheckRollReconcilationRPT2 myaclist = new CheckRollReconcilationRPT2();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Division Name", myDiv.EstateDivision(strDivisionID).Tables[0].Rows[0][0].ToString());
                    myaclist.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}