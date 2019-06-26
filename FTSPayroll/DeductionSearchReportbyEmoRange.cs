using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DeductionSearchReportbyEmoRange : Form
    {
        public DeductionSearchReportbyEmoRange()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock myEstateDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster myDeducMas = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Reports myDeducSearch = new FTSPayRollBL.Reports();

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
                ReportName = cmbDivision.SelectedValue.ToString() + " Division -";
            }
            else
            {
                ReportName = "All Division - ";

            }
            //if (!chkAllEmp.Checked)
            //{
            //    strAllEmp = cmbEmpNo.SelectedValue.ToString();
               
            //}
            //else
            //{
            //    ReportName += " All Employees - ";
            //}
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
            if (chkAllEmp.Checked == true)
            {
                ReportName += " - All Employee  ";
                DsDeductSearch = myDeducSearch.GetDeductionSearchData(strAllDiv, strAllDeductCode, strAllEmp, cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString());

            }
            else
            {
                String empNoFrom = txtEmpFrom.Text.Trim().ToString();
                String empNoTo= txtEmpTo.Text.Trim().ToString();
                ReportName += "EmpNo- From"+txtEmpFrom.Text.ToString() +" to " + txtEmpTo.Text.ToString();
                DsDeductSearch = myDeducSearch.GetDeductionSearchDataByEmpNoRange(strAllDiv, strAllDeductCode, strAllEmp, cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString(), empNoFrom,empNoTo);
            }
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

        private void DeductionSearchReportbyEmoRange_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstateDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductCode.DataSource = myDeducMas.ListDeducCode();
            cmbDeductCode.DisplayMember = "DeductShortName";
            cmbDeductCode.ValueMember = "DeductShortName";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();
        }

        private void chkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllEmp.Checked == true)
            {
                txtEmpFrom.Enabled = false;
                txtEmpTo.Enabled = false;
            }
            else
            {
                txtEmpFrom.Enabled = true;
                txtEmpTo.Enabled = true;
            }
        }
    }
}