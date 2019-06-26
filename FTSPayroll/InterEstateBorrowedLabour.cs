using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class InterEstateBorrowedLabour : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();

        public InterEstateBorrowedLabour()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getInterEstateBorrowedLabourRegister(Convert.ToDateTime(dtFrom.Value.Date), Convert.ToDateTime(dtTo.Value.Date));
            dataSetReport.WriteXml("InterEstateBorrowedLabour.xml");
            InterEstateBorrowedLabourRegister myOTRegRep = new InterEstateBorrowedLabourRegister();
            myOTRegRep.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myOTRegRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myOTRegRep.SetParameterValue("Date", "From: " +dtFrom.Value.Date.ToShortDateString()+" To:"+dtTo.Value.Date.ToShortDateString());
            myOTRegRep.SetParameterValue("Division", "Borrowed Estate : " + FTSPayRollBL.User.StrEstate);
            myReportViewer.crystalReportViewer1.ReportSource = myOTRegRep;
            myReportViewer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getInterEstateBorrowedLabourRegister(Convert.ToDateTime(dtFrom.Value.Date), Convert.ToDateTime(dtTo.Value.Date));
            dataSetReport.WriteXml("InterEstateBorrowedLabour.xml");
            InterEstateBorrowedFieldWise myOTRegRep = new InterEstateBorrowedFieldWise();
            myOTRegRep.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myOTRegRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myOTRegRep.SetParameterValue("Date", "From: " + dtFrom.Value.Date.ToShortDateString() + " To:" + dtTo.Value.Date.ToShortDateString());
            myOTRegRep.SetParameterValue("Division", "Borrowed Estate : " + FTSPayRollBL.User.StrEstate);
            myReportViewer.crystalReportViewer1.ReportSource = myOTRegRep;
            myReportViewer.Show();
        }
    }
}