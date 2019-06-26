using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class LeafAndManDayAnalysis : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();

        public LeafAndManDayAnalysis()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LeafAndManDayAnalysis_Load(object sender, EventArgs e)
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
        }

        private void chkAllDivisions_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            //String strAllDiv = "%";
            //if (chkAllDivisions.Checked)
            //{
            //    strAllDiv = "%";
            //}
            //else
            //{
            //    strAllDiv = cmbDivision.SelectedValue.ToString();
            //}
            //dataSetReport = myReports.getPaymentCheckRoll(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), AllCat);
            //dataSetReport.WriteXml("PaymentCheckRoll.xml");

            //if (dataSetReport.Tables[0].Rows.Count > 0)
            //{
            //    PaymentCheckRollRPT myaclist = new PaymentCheckRollRPT();
            //    myaclist.SetDataSource(dataSetReport);
            //    ReportViewer myReportViewer = new ReportViewer();

            //    myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //    myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
            //    myaclist.SetParameterValue("paramDivision", "ALL");
            //    myaclist.SetParameterValue("paramYearMonth", cmbMonth.Text + "  /  " + cmbYear.Text);
            //    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            //    myReportViewer.Show();
            //}
            //else
            //{
            //    MessageBox.Show("No Data to Preview..!");
            //}
        }
    }
}