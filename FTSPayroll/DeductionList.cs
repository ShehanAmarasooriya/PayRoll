using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DeductionList : Form
    {
        FTSPayRollBL.DeductionMaster myDeduction = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth myYear = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Division myDiv = new FTSPayRollBL.Division();
        FTSPayRollBL.Reports myReport = new FTSPayRollBL.Reports();

        public DeductionList()
        {
            InitializeComponent();
        }

        private void DeductionList_Load(object sender, EventArgs e)
        {
            cmbDeductionGroup.DataSource = myDeduction.ListBL_IPDeductionGroups();
            cmbDeductionGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductionGroup.ValueMember = "DeductionGroupId";

            cmbYear.DataSource = myYear.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYear.getLastYearID();

            cmbMonth.DataSource = myYear.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myYear.getLastMonthID();

            cmbDivision.DataSource = myDiv.ListDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            cmbDeductionGroup_SelectedIndexChanged(null, null);
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeduction.DataSource = myDeduction.ListDeduction(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                cmbDeduction.DisplayMember = "DeductShortName";
                cmbDeduction.ValueMember = "DeductCode";
            }
            catch { }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
            {
                cmbDivision.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ReportViewer myReportViewer = new ReportViewer();

            if (chkAll.Checked)
            {                
                ds = myReport.LoanDeductionList(cmbYear.Text, cmbMonth.SelectedValue.ToString(), cmbDeductionGroup.SelectedValue.ToString(), cmbDeduction.SelectedValue.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("DeductionList.xml");
                    DeductionAccNoList myDeducReport = new DeductionAccNoList();
                    myDeducReport.SetDataSource(ds);
                    
                    myDeducReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myDeducReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstate().Rows[0][1].ToString());
                    myDeducReport.SetParameterValue("Division", "Division : For all Divisions");
                    myDeducReport.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                    myDeducReport.SetParameterValue("Deduction", "Bank Loan Recovery - " +myDeduction.GetLoanName(cmbDeduction.SelectedValue.ToString()));

                    myReportViewer.crystalReportViewer1.ReportSource = myDeducReport;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }               

            }
            else
            {
                
                ds = myReport.LoanDeductionList(cmbDivision.Text, cmbYear.Text, cmbMonth.SelectedValue.ToString(), cmbDeduction.SelectedValue.ToString(), cmbDeductionGroup.SelectedValue.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("DeductionList.xml");
                    DeductionAccNoList myDeducReport = new DeductionAccNoList();
                    myDeducReport.SetDataSource(ds);

                    myDeducReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myDeducReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstate().Rows[0][1].ToString());
                    myDeducReport.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    myDeducReport.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                    myDeducReport.SetParameterValue("Deduction", "Bank Loan Recovery - " + myDeduction.GetLoanName(cmbDeduction.SelectedValue.ToString()));

                    myReportViewer.crystalReportViewer1.ReportSource = myDeducReport;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                } 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPayeeDetails_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ReportViewer myReportViewer = new ReportViewer();

            if (chkAll.Checked)
            {
                ds = myReport.payeeLoanDeductionList("%", cmbYear.Text, cmbMonth.SelectedValue.ToString(), cmbDeduction.SelectedValue.ToString(), cmbDeductionGroup.SelectedValue.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("PayeeDeductionList.xml");
                    PayeeDeductionAccNoList myDeducReport = new PayeeDeductionAccNoList();
                    myDeducReport.SetDataSource(ds);

                    myDeducReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myDeducReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstate().Rows[0][1].ToString());
                    myDeducReport.SetParameterValue("Division", "Division : For all Divisions");
                    myDeducReport.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                    myDeducReport.SetParameterValue("Deduction", "Payee Loan Recovery - " + myDeduction.GetLoanName(cmbDeduction.SelectedValue.ToString()));

                    myReportViewer.crystalReportViewer1.ReportSource = myDeducReport;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }

            }
            else
            {

                ds = myReport.payeeLoanDeductionList(cmbDivision.Text, cmbYear.Text, cmbMonth.SelectedValue.ToString(), cmbDeduction.SelectedValue.ToString(), cmbDeductionGroup.SelectedValue.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("PayeeDeductionList.xml");
                    PayeeDeductionAccNoList myDeducReport = new PayeeDeductionAccNoList();
                    myDeducReport.SetDataSource(ds);

                    myDeducReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myDeducReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstate().Rows[0][1].ToString());
                    myDeducReport.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    myDeducReport.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                    myDeducReport.SetParameterValue("Deduction", "Payee Loan Recovery - " + myDeduction.GetLoanName(cmbDeduction.SelectedValue.ToString()));

                    myReportViewer.crystalReportViewer1.ReportSource = myDeducReport;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            }
        }
    }
}