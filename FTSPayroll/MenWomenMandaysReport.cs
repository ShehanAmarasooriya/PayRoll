using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class MenWomenMandaysReport : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        public MenWomenMandaysReport()
        {
            InitializeComponent();
        }

        private void MenWomenMandaysReport_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSetReport = new DataSet();
                if (chkAll.Checked)
                    dataSetReport = myReports.getMenWomenMandays(dtpFrom.Value.Date.Year);
                else
                    dataSetReport = myReports.getMenWomenMandays(dtpFrom.Value.Date.Year);

                dataSetReport.WriteXml("MenWomenManDaysSum.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    MenWomenManDaysRPT myaclist = new MenWomenManDaysRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Period","Year : "+ dtpFrom.Value.Date.Year.ToString() );
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