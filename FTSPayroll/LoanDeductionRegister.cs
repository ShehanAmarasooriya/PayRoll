using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class LoanDeductionRegister : Form
    {

        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        public LoanDeductionRegister()
        {
            InitializeComponent();
        }

        private void LoanDeductionRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            

            cmbDeductionGroup.DataSource = DeductMaster.getLoanDeductionGroup();
            cmbDeductionGroup.DisplayMember = "ShortName";
            cmbDeductionGroup.ValueMember = "DeductGroupCode";
            cmbDeductGroup_SelectedIndexChanged(null,null);

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();
            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();
        }

        private void cmbDeductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeductions.DataSource = null;
                cmbDeductions.DataSource = DeductMaster.ListDeduction(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                cmbDeductions.DisplayMember = "DeductShortName";
                cmbDeductions.ValueMember = "DeductCode";
                cmbDeductions.SelectedIndex = 0;
            }
            catch { }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            if (chkAllEmp.Checked == true)
            {
                if (chkPayeeDetails.Checked)
                {
                    dataSetReport = myReports.GetLoanDeductionRegisterWithPayees(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                }
                else
                {
                    dataSetReport = myReports.GetLoanDeductionRegister(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                }
            }
            else
            {
                String empNoFrom = txtEmpFrom.Text.ToString();
                String empNoTo = txtEmpTo.Text.ToString(); ;
                
                dataSetReport = myReports.GetLoanDeductionRegisterByEmpRange(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), empNoFrom, empNoTo);
            }

            if (chkPayeeDetails.Checked)
            {
                dataSetReport.WriteXml("LoanDeductionsRegisterWithPayees.xml");
                LoanDeductionRegisterWithPayeesRPT myaclist = new LoanDeductionRegisterWithPayeesRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();
                myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
                myaclist.SetParameterValue("Division", cmbDivision.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            else
            {
                dataSetReport.WriteXml("LoanDeductionsRegister.xml");
                LoanDeductionRegisterRPT myaclist = new LoanDeductionRegisterRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();
                myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
                myaclist.SetParameterValue("Division", cmbDivision.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            
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

        private void txtEmpFrom_TextChanged(object sender, EventArgs e)
        {
           
            
        }
    }
}