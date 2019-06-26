using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FixedAdditionRegister : Form
    {
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Additions myAdditions = new FTSPayRollBL.Additions();
        public FixedAdditionRegister()
        {
            InitializeComponent();
        }

        private void FixedAdditionRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbAddition.DataSource = myAdditions.getAddition();
            cmbAddition.DisplayMember = "ShortName";
            cmbAddition.ValueMember = "AdditionId";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            dataSetReport = myReports.GetFixedAdditionRegister(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            dataSetReport.WriteXml("FixedAdditionRegister.xml");
            FixedAdditionRegisterRPT myaclist = new FixedAdditionRegisterRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
            myaclist.SetParameterValue("Division", cmbDivision.Text);
            myaclist.SetParameterValue("Addition", cmbAddition.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        

        
    }
}