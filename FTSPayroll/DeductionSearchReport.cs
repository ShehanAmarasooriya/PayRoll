using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DeductionSearchReport : Form
    {
        FTSPayRollBL.EstateDivisionBlock myEstateDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster myDeducMas = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Reports myDeducSearch = new FTSPayRollBL.Reports();

        public DeductionSearchReport()
        {
            InitializeComponent();
        }

        private void DeductionSearchReport_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstateDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductionGroup.DataSource = myDeducMas.ListDeductionGroups();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductionGroupId";

            cmbDeductionGroup_SelectedIndexChanged(null, null);

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();

        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();
                cmbEmpNo.DataSource = null;
                cmbEmpNo.DataSource = myMaster.ListAllEmployees(DivisionID);
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
            }
            catch { }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            String strAllDiv = "%";
            String strAllEmp = "%";
            String strAllDeductCode = "%";
            String ReportName = "";
            ReportViewer myReportViewer = new ReportViewer();
            if (!chkAll.Checked)
            {
                strAllDiv = cmbDivision.SelectedValue.ToString();
                ReportName = cmbDivision.SelectedValue.ToString()+" Division -";
            }
            else
            {
                ReportName = "All Division - ";

            }
            if (!chkAllEmp.Checked)
            {
                strAllEmp = cmbEmpNo.SelectedValue.ToString();
                ReportName += cmbEmpNo.SelectedValue.ToString() + " Employee - ";
            }
            else
            {
                ReportName +=  " All Employees - ";
            }

            if (!chkAllDeduction.Checked)
            {
                strAllDeductCode = cmbDeductCode.SelectedValue.ToString();
                ReportName += cmbDeductCode.SelectedValue.ToString() + " Deduction  ";
            }
            else
            {
                ReportName += " All Deductions  ";
            }
            DataSet DsDeductSearch = new DataSet();
            DsDeductSearch = myDeducSearch.GetDeductionSearchData(strAllDiv, strAllDeductCode, strAllEmp, cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString());

            if (DsDeductSearch.Tables[0].Rows.Count > 0)
            {
                DsDeductSearch.WriteXml("DeductionSearchData.xml");

                DeductionSearchReportRPT myAllDeducAllEmp = new DeductionSearchReportRPT();
                myAllDeducAllEmp.SetDataSource(DsDeductSearch);

                myAllDeducAllEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myAllDeducAllEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                myAllDeducAllEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                myAllDeducAllEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                myAllDeducAllEmp.SetParameterValue("RepName", ReportName);
                myReportViewer.crystalReportViewer1.ReportSource = myAllDeducAllEmp;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                myDeducMas.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                cmbDeductCode.DataSource = myDeducMas.getDeduction();
                cmbDeductCode.DisplayMember = "DeductionName";
                cmbDeductCode.ValueMember = "DeductShortName";
            }
            catch { }
        }

        private void cmbDeductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblDeduction.Text = cmbDeductCode.SelectedValue.ToString();
                
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strAllDiv = "%";
            String strAllEmp = "%";
            String strAllDeductCode = "%";
            String ReportName = "";
            ReportViewer myReportViewer = new ReportViewer();
            if (!chkAll.Checked)
            {
                strAllDiv = cmbDivision.SelectedValue.ToString();
                ReportName = cmbDivision.SelectedValue.ToString() + " Division -";
            }
            else
            {
                ReportName = "All Division - ";

            }
            if (!chkAllEmp.Checked)
            {
                strAllEmp = cmbEmpNo.SelectedValue.ToString();
                ReportName += cmbEmpNo.SelectedValue.ToString() + " Employee - ";
            }
            else
            {
                ReportName += " All Employees - ";
            }

            if (!chkAllDeduction.Checked)
            {
                strAllDeductCode = cmbDeductCode.SelectedValue.ToString();
                ReportName += cmbDeductCode.SelectedValue.ToString() + " Deduction  ";
            }
            else
            {
                ReportName += " All Deductions  ";
            }
            DataSet DsDeductSearch = new DataSet();
            DsDeductSearch = myDeducSearch.GetDeductionSearchDataIncludingSkippedDeductions(strAllDiv, strAllDeductCode, strAllEmp, cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString());

            if (DsDeductSearch.Tables[0].Rows.Count > 0)
            {
                DsDeductSearch.WriteXml("DeductionSearchDataIncludeSkipped.xml");

                DeductionSearchIncludingSkippedRPT myAllDeducAllEmp = new DeductionSearchIncludingSkippedRPT();
                myAllDeducAllEmp.SetDataSource(DsDeductSearch);

                myAllDeducAllEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myAllDeducAllEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                myAllDeducAllEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                myAllDeducAllEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                myAllDeducAllEmp.SetParameterValue("RepName", ReportName);
                myReportViewer.crystalReportViewer1.ReportSource = myAllDeducAllEmp;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }
    }
}