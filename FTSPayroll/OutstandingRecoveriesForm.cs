using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OutstandingRecoveriesForm : Form
    {
        //FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        //FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        //FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        //FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        //FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        //FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        //FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        //FTSPayRollBL.User myUser = new FTSPayRollBL.User();

        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster myDeducMas = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();

        public OutstandingRecoveriesForm()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void OutstandingRecoveriesForm_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductionGroup.DataSource = myDeducMas.ListDeductionGroups();
            cmbDeductionGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductionGroup.ValueMember = "DeductionGroupId";

            cmbSelectedGroup.DataSource = myDeducMas.ListDeductionGroups();
            cmbSelectedGroup.DisplayMember = "DeductGroupShortName";
            cmbSelectedGroup.ValueMember = "DeductionGroupId";
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeductCode.DataSource = myDeducMas.ListDeduction(int.Parse(cmbDeductionGroup.SelectedValue.ToString()));
                cmbDeductCode.DisplayMember = "DeductShortName";
                cmbDeductCode.ValueMember = "DeductCode";

            }
            catch { }
        }

        private void txt_employeeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txt_employeeNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList(this, cmbDivision.SelectedValue.ToString());
                    empList.ShowDialog();

                }
                else
                {
                    if (txt_employeeNo.Text.Trim() != "")
                    {
                        txt_employeeNo.Text = txt_employeeNo.Text.PadLeft(5, '0');
                        txtEmpNo_LeaveChanged();
                    }
                }

            }
        }

        private void txtEmpNo_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txt_employeeNo.Text))
            {
                if (String.IsNullOrEmpty(myMaster.GetEmployeeNameByEmpNo(txt_employeeNo.Text, cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txt_employeeNo.Text = "";
                    txt_employeeNo.Focus();
                }
                else
                {
                    if (myMaster.IsNotInactive(txt_employeeNo.Text, cmbDivision.SelectedValue.ToString()))
                    {
                        //EmpMaster.StrGender = EmpMaster.GetEmployeeGenderByEmpNo(txt_employeeNo.Text, cmbDivision.SelectedValue.ToString());
                        //cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txt_employeeNo.Text, cmbDivision.SelectedValue.ToString());
                        //txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txt_employeeNo.Text, cmbDivision.SelectedValue.ToString());
                        
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                        txt_employeeNo.Text = "";
                        txt_employeeNo.Focus();
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myDeducMas.IsLoanDeductionGroup(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString())))
            {
                SearchOutstandingLoanDeductions();
            }
            else
                SearchOutstandingFixedDeductions();
        }

        private void SearchOutstandingFixedDeductions()
        {
            try
            {
                String strAllDivision = "%";
                String strAllDeduction = "%";
                String strAllEmployee = "%";
                if (!chkAllDivision.Checked)
                    strAllDivision = cmbDivision.SelectedValue.ToString().Trim();
                if (!chkAllEmployee.Checked)
                    strAllEmployee = txt_employeeNo.Text.ToString().Trim();
                if (!chkAllDeduction.Checked)
                    strAllDeduction = cmbDeductCode.SelectedValue.ToString().Trim();


                DataTable dt;
                dt = myReports.SearchOutstandingFixedDeductions(strAllDivision, strAllEmployee, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), strAllDeduction);
                gvFixedDeductions.DataSource = dt;
                //if (dataSetReport.Tables[0].Rows.Count > 0)
                //{
                //    OutstandingRecoveriesRPT1 myaclist = new OutstandingRecoveriesRPT1();
                //    myaclist.SetDataSource(dataSetReport);
                //    ReportViewer myReportViewer = new ReportViewer();

                //    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                //    if (strAllDivision == "%")
                //        myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + "All");
                //    else
                //        myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + strAllDivision);
                //    myaclist.SetParameterValue("Recovery", "Fixed Deductions");
                //    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                //    myReportViewer.Show();
                //}
                //else
                //    MessageBox.Show("No data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchOutstandingLoanDeductions()
        {
            try
            {
                String strAllDivision = "%";
                String strAllDeduction = "%";
                String strAllEmployee = "%";
                if (!chkAllDivision.Checked)
                    strAllDivision = cmbDivision.SelectedValue.ToString().Trim();
                if (!chkAllEmployee.Checked)
                    strAllEmployee = txt_employeeNo.Text.ToString().Trim();
                if (!chkAllDeduction.Checked)
                    strAllDeduction = cmbDeductCode.SelectedValue.ToString().Trim();


                DataTable dt;
                dt = myReports.SearchOutstandingLoanDeductions(strAllDivision, strAllEmployee, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), strAllDeduction);
                gvFixedDeductions.DataSource = dt;
                //if (dataSetReport.Tables[0].Rows.Count > 0)
                //{
                //    OutstandingRecoveriesRPT1 myaclist = new OutstandingRecoveriesRPT1();
                //    myaclist.SetDataSource(dataSetReport);
                //    ReportViewer myReportViewer = new ReportViewer();

                //    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                //    if (strAllDivision == "%")
                //        myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + "All");
                //    else
                //        myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + strAllDivision);
                //    myaclist.SetParameterValue("Recovery", "Fixed Deductions");
                //    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                //    myReportViewer.Show();
                //}
                //else
                //    MessageBox.Show("No data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvFixedDeductions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbSelectedGroup.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[10].Value.ToString();
            cmbSelectedCode.SelectedValue = Convert.ToInt32(gvFixedDeductions.Rows[e.RowIndex].Cells[11].Value.ToString());
            cmbDivision.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[0].Value.ToString();

            lblRefNo.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtYear.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMonth.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDeductAmount.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtBalanceAmount.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtNoofInstallments.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtSelectedEmp.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[5].Value.ToString();

        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                if (String.IsNullOrEmpty(txtReason.Text))
                {
                    MessageBox.Show("You Must Enter A Valid Reason To Terminate");
                    txtReason.Focus();
                }
                else
                {
                    if (MessageBox.Show("Confirm Terminate...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        myEmployeeDeduction.IntFixedDeductId = Convert.ToInt32(lblRefNo.Text);
                        myEmployeeDeduction.StrEmpNO = txtSelectedEmp.Text;
                        myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                        myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbSelectedGroup.SelectedValue.ToString());
                        myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbSelectedCode.SelectedValue.ToString());
                        myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                        myEmployeeDeduction.IntFromYear = Convert.ToInt32(txtYear.Text);
                        myEmployeeDeduction.IntFromMonth = Convert.ToInt32(txtMonth.Text);
                        myEmployeeDeduction.StrReason = txtReason.Text;
                        try
                        {
                            myEmployeeDeduction.TerminateFixedDeductions();
                            MessageBox.Show("Fixed Deductions Terminated Successfully");
                            try
                            {
                                refreshGridAfterUpdate();
                            }
                            catch { }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("No Fixed Deduction Is Selected To Terminate");
            }
        }

        public void refreshGridAfterUpdate()
        {
            cmbDeductionGroup.SelectedValue = Convert.ToInt32(cmbSelectedGroup.SelectedValue.ToString());
            cmbDeductCode.SelectedValue = Convert.ToInt32(cmbSelectedCode.SelectedValue.ToString());
            txt_employeeNo.Text = txtSelectedEmp.Text;

            String strAllDivision = "%";
            String strAllDeduction = "%";
            String strAllEmployee = "%";
            
            if (!chkAllDivision.Checked)
                strAllDivision = cmbDivision.SelectedValue.ToString().Trim();
            if (!chkAllEmployee.Checked)
                strAllEmployee = txt_employeeNo.Text.ToString().Trim();
            if (!chkAllDeduction.Checked)
                strAllDeduction = cmbDeductCode.SelectedValue.ToString().Trim();

            gvFixedDeductions.DataSource = myReports.SearchOutstandingLoanDeductions(strAllDivision, strAllEmployee, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), strAllDeduction); ;
        }
        public void refreshGrid()
        {
            String strAllDivision = "%";
            String strAllDeduction = "%";
            String strAllEmployee = "%";

            if (!chkAllDivision.Checked)
                strAllDivision = cmbDivision.SelectedValue.ToString().Trim();
            if (!chkAllEmployee.Checked)
                strAllEmployee = txt_employeeNo.Text.ToString().Trim();
            if (!chkAllDeduction.Checked)
                strAllDeduction = cmbDeductCode.SelectedValue.ToString().Trim();

            gvFixedDeductions.DataSource = myReports.SearchOutstandingLoanDeductions(strAllDivision, strAllEmployee, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), strAllDeduction); ;
        }

        private void cmbSelectedGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSelectedCode.DataSource = myDeducMas.ListDeduction(int.Parse(cmbSelectedGroup.SelectedValue.ToString()));
                cmbSelectedCode.DisplayMember = "DeductShortName";
                cmbSelectedCode.ValueMember = "DeductCode";

            }
            catch { }
        }

    }
}