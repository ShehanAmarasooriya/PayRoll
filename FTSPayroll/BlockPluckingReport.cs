using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class BlockPluckingReport : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        public BlockPluckingReport()
        {
            InitializeComponent();
        }

        private void BlockPluckingReport_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            dataSetReport = myReports.GetBlockPlkAdvanceReport(dtpStartDate.Value.Date, dtpEndDate.Value.Date, cmbDivision.Text);
            dataSetReport.WriteXml("BlockPluckingAdvanceReport.xml");
            BlockPluckingAdvanceRPT myBlkPlkAdvRep = new BlockPluckingAdvanceRPT();
            myBlkPlkAdvRep.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myBlkPlkAdvRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myBlkPlkAdvRep.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
            myBlkPlkAdvRep.SetParameterValue("Period", dtpStartDate.Value.Date.ToShortDateString() + " To: " + dtpEndDate.Value.Date.ToShortDateString());
            myReportViewer.crystalReportViewer1.ReportSource = myBlkPlkAdvRep;
            myReportViewer.Show();
        }
    }
}