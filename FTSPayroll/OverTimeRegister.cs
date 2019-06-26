using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OverTimeRegister : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();

        public OverTimeRegister()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OverTimeRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getOverTimeRegister(cmbDivision.SelectedValue.ToString(), dtDate.Value.Date);
            dataSetReport.WriteXml("OverTimeRegister.xml");
            OverTimeRegisterRPT myOTRegRep = new OverTimeRegisterRPT();
            myOTRegRep.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myOTRegRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myOTRegRep.SetParameterValue("Date", "Date : " + dtDate.Value.Date.ToShortDateString());
            myOTRegRep.SetParameterValue("Division", "Division : " + cmbDivision.SelectedValue.ToString());
            myReportViewer.crystalReportViewer1.ReportSource = myOTRegRep;
            myReportViewer.Show();
        }
    }
}