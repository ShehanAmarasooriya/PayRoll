using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class WorkDistributionMainCodeWise : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();

        public WorkDistributionMainCodeWise()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WorkDistributionMainCodeWise_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            btnYear.Show();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                
                DateTime dtFromDate = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, 1);
                DateTime dtMonthEndDate = dtFromDate.AddMonths(1).AddDays(-1);
                DateTime dtYearStartDate = new DateTime(dtpFrom.Value.Date.Year, 1, 1);
                DateTime dtToDate = dtpFrom.Value.Date;
                
                DataSet dataSetReport = new DataSet();
                String strAllDivisions = "%";
                if (chkAll.Checked)
                    strAllDivisions="%";
                else
                    strAllDivisions=cmbDivision.SelectedValue.ToString();
                   
                    dataSetReport = myReports.getWorkDistributionMainCodeWise(dtpFrom.Value.Date, strAllDivisions);

                dataSetReport.WriteXml("WorkDistributionMainCodeWise.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    WorkDistributionMainCodeWiseRPT myaclist = new WorkDistributionMainCodeWiseRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Options", "To Date : " + dtpFrom.Value.Date );
                    myaclist.SetParameterValue("date", "  From  " + dtFromDate + "  To  " + dtMonthEndDate);
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                    MessageBox.Show("No Data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime dtFromDate = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, 1);
                DateTime dtMonthEndDate = dtFromDate.AddMonths(1).AddDays(-1);
                DateTime dtYearStartDate = new DateTime(dtpFrom.Value.Date.Year, 1, 1);
                DateTime dtToDate = dtpFrom.Value.Date;

                DataSet dataSetReport = new DataSet();
                String strAllDivisions = "%";
                if (chkAll.Checked)
                    strAllDivisions = "%";
                else
                    strAllDivisions = cmbDivision.SelectedValue.ToString();

                dataSetReport = myReports.GetWorkDistributionMainCodeWiseNew(dtpFrom.Value.Date, strAllDivisions);

                dataSetReport.WriteXml("WorkDistributionMainCodeWiseNew.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    WorkDistributionMainCodeWiseNewRPT myaclist = new WorkDistributionMainCodeWiseNewRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Options", "To Date : " + dtpFrom.Value.Date );
                    myaclist.SetParameterValue("date", "  From  " + dtYearStartDate + "  To " + dtMonthEndDate );
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                    MessageBox.Show("No Data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}