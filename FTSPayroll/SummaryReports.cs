using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class SummaryReports : Form
    {
        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.Reports clsReports = new FTSPayRollBL.Reports();

        public SummaryReports()
        {
            InitializeComponent();
        }

        private void btnActiveEmpRegister_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "ActiveEmployeeEPFRegister";
            ds = clsReports.GetEmployeeWiseEPFDetails(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("ActiveEmployeeEPFRegister.xml");
                ActiveEmployeeEPFRegisterRPT myReportViewe = new ActiveEmployeeEPFRegisterRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() );
                myReportViewe.SetParameterValue("Date", "Year: " + cmbYear.SelectedValue.ToString() + "/  Month: " + cmbMonth.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SummaryReports_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = myMonth.ListMonths(Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";
                cmbMonth.SelectedValue = myMonth.getLastMonthID();
            }
            catch { }
        }

        
    }
}