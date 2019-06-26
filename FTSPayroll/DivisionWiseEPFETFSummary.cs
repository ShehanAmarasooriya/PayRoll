using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DivisionWiseEPFETFSummary : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();

        public DivisionWiseEPFETFSummary()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DivisionWiseEPFETFSummary_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getDivisionWiseEPFETFSummary(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            
            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                dataSetReport.WriteXml("EPFETFSummary.xml");
                DivisionWiseEPFETFSummaryRPT myaclist = new DivisionWiseEPFETFSummaryRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                myaclist.SetParameterValue("Period", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            else
                MessageBox.Show("No Data to preview..!");
        }
    }
}