using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class LoanDeductions : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction empDeduct = new FTSPayRollBL.EmployeeDeduction();
        public LoanDeductions()
        {
            InitializeComponent();
        }

        private void LoanDeductions_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductionGroup.DataSource = DeductMaster.getLoanDeductionGroup();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductGroupCode";
            cmbDeductionGroup_SelectedIndexChanged(null, null);

            txtLoanName.Enabled = false;
            gbTerminate.Enabled = false;
            cmbDivision.Text = FTSPayRollBL.User.StrDivision;
            try
            {
                gvlist.DataSource = LMaster.ListLoans(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetLoanDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
            
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cmbDeductions_SelectedIndexChanged(null, null);
                DeductMaster.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                cmbDeductions.DataSource = null;
                cmbDeductions.DataSource = DeductMaster.ListDeduction(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                cmbDeductions.DisplayMember = "DeductShortName";
                cmbDeductions.ValueMember = "DeductCode";

                cmbDeductions_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvlist.DataSource = LMaster.ListLoans(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetLoanDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvlist.DataSource = LMaster.ListLoans(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetLoanDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtLoanName.Text == "")
            {
                MessageBox.Show(" Loan name can not be empty");
                txtLoanName.Focus();
            }

            else if (txtNoOfInstallments.Text == "")
            {
                MessageBox.Show("No of installmant can not be empty");
                txtNoOfInstallments.Focus();
            }

            else if (txtInstallmentAmount.Text == "")
            {
                MessageBox.Show("Installment Amount can not be empty");
                txtInstallmentAmount.Focus();
            }

            
            else if (gvPayeeList.Rows.Count<1)
            {
                MessageBox.Show("No Payee Details Found.");
            }

            else
            {
                LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                LMaster.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                LMaster.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
                LMaster.StrEmpNo = txtEmpNo.Text;
                LMaster.IntCategoryCode = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.Text);
                LMaster.StrName = txtLoanName.Text;
                LMaster.FlNoOfInstallments = float.Parse(txtNoOfInstallments.Text);
                LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
                LMaster.FlPrincipalAmount = float.Parse(txtNoOfInstallments.Text) * float.Parse(txtInstallmentAmount.Text);
                
                LMaster.DtLoanDate = Convert.ToDateTime(cmbMonth.SelectedValue.ToString() + "/1/" + cmbYear.SelectedValue.ToString());
                try
                {
                    LMaster.InsertLoan();
                    LMaster.DeleteLoanPayeeDetails();
                    for (int j = 0; j < gvPayeeList.Rows.Count;j++ )
                    {
                        LMaster.StrPayeeACNo = gvPayeeList.Rows[j].Cells[0].Value.ToString();
                        LMaster.StrPayeeACName = gvPayeeList.Rows[j].Cells[1].Value.ToString();
                        LMaster.DecPayeeAmount = Convert.ToDecimal(gvPayeeList.Rows[j].Cells[2].Value.ToString());
                        try
                        {

                            LMaster.InsertLoanPayeeDetails();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Insert Payee Details Failed.., " + ex.Message);
                        }
                    }
                    MessageBox.Show("Loan Added Successfully");
                    AfterAdd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            EmployeeList empList = new EmployeeList();
            empList.Show();
        }

        private void txtEmpNo_LeaveChanged()
        {
            if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
            {
                MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                txtEmpNo.Text = "";
                txtEmpNo.Focus();
            }
            else
            {
                if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                {
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    txtPayeeAccNo.Focus();
                }
                else
                {
                    MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
            }

        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                EmployeeList empList = new EmployeeList();
                empList.Show();
            }
            else
            {
                if (txtEmpNo.Text.Trim() != "")
                {
                    //txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                    txtEmpNo_LeaveChanged();
                }
                
            }
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Trim() != String.Empty)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(), "Loan");
                    empList.Show();
                }
                else
                {
                    if (e.KeyChar == 8)
                    {
                        //txtJobShortName.Focus();
                    }
                    else
                    {
                        if (e.KeyChar == 13)
                        {
                            if (txtEmpNo.Text.Trim() != "")
                            {
                                txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                                txtEmpNo_LeaveChanged();
                            }
                            
                        }
                    }
                }
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtLoanName.Text == "")
            {
                MessageBox.Show(" Loan name can not be empty");
                txtLoanName.Focus();
            }

            //else if (txtPrincipalAmount.Text == "")
            //{
            //    MessageBox.Show("Principal amount can not be empty");
            //    txtPrincipalAmount.Focus();
            //}

            else if (txtNoOfInstallments.Text == "")
            {
                MessageBox.Show("No of installmant can not be empty");
                txtNoOfInstallments.Focus();
            }

            else if (txtInstallmentAmount.Text == "")
            {
                MessageBox.Show("Installment Amount can not be empty");
                txtInstallmentAmount.Focus();
            }

            

            else
            {
                LMaster.IntLoanCode = Convert.ToInt32(lblRefNo.Text);
                LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                LMaster.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                LMaster.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
                LMaster.StrEmpNo = txtEmpNo.Text;
                LMaster.StrName = txtLoanName.Text;
                LMaster.FlPrincipalAmount = float.Parse(txtNoOfInstallments.Text) * float.Parse(txtInstallmentAmount.Text);
                LMaster.FlNoOfInstallments = float.Parse(txtNoOfInstallments.Text);
                LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
                
                LMaster.DtLoanDate = Convert.ToDateTime(cmbMonth.SelectedValue.ToString() + "/1/" + cmbYear.SelectedValue.ToString());
                LMaster.StrUserId = FTSPayRollBL.User.StrUserName;
                try
                {
                    LMaster.UpdateLoanOnlyDeductAmount();
                    LMaster.DeleteLoanPayeeDetails();
                    for (int j = 0; j < gvPayeeList.Rows.Count; j++)
                    {
                        LMaster.StrPayeeACNo = gvPayeeList.Rows[j].Cells[0].Value.ToString();
                        LMaster.StrPayeeACName = gvPayeeList.Rows[j].Cells[1].Value.ToString();
                        LMaster.DecPayeeAmount = Convert.ToDecimal(gvPayeeList.Rows[j].Cells[2].Value.ToString());
                        try
                        {

                            LMaster.InsertLoanPayeeDetails();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Update Payee Details Failed.., " + ex.Message);
                        }
                    }
                    MessageBox.Show("Loan Updated Successfully");
                    cmdClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //if (!LMaster.IsProcessedEntry(Convert.ToInt32(lblRefNo.Text)))
                if(true)
                {
                    LMaster.IntLoanCode = Convert.ToInt32(lblRefNo.Text);
                    LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    LMaster.StrEmpNo = txtEmpNo.Text;
                    LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
                    LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    LMaster.StrUserId = FTSPayRollBL.User.StrUserName;

                    try
                    {
                        LMaster.DeleteLoanPayeeDetails();
                        LMaster.DeleteLoan();
                        MessageBox.Show("Loan Deleted successfully");
                        cmdClear.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Delete, This Entry Refers to Processed Data");
                }

            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtPayeeAccNo.Text = "";
            txtLoanName.Text = "";
            txtNoOfInstallments.Text = "";
            txtEmpName.Text = "";
            txtEmpNo.Clear();
            txtInstallmentAmount.Text = "";
            lblRefNo.Text = "";

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            gbTerminate.Enabled = false;

            try
            {
                gvlist.DataSource = LMaster.ListLoans(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetLoanDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
                gvPayeeList.DataSource = null;
            }
            catch { }
            this.cmbDivision.Focus();
        }

        private void AfterAdd()
        {
            txtPayeeAccNo.Text = "";
            //txtLoanName.Text = "";
            //txtNoOfInstallments.Text = "";
            txtEmpName.Text = "";
            txtEmpNo.Clear();
            txtInstallmentAmount.Text = "";
            lblRefNo.Text = "";

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            gbTerminate.Enabled = true;

            try
            {
                gvlist.DataSource = LMaster.ListLoans(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetLoanDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
                gvPayeeList.DataSource = null;
            }
            catch { }
            this.txtEmpNo.Focus();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblDeduction.Text = DeductMaster.GetDeductionNameByID(Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                txtLoanName.Text = cmbDeductions.Text;
                txtLoanName.Enabled = false;
                gvlist.DataSource = LMaster.ListLoans(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetLoanDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
        }

        private void gvlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDeductionGroup.Focus();
            }
        }

        private void cmbDeductionGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDeductions.Focus();
            }
        }

        private void cmbDeductions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbYear.Focus();
            }
        }

        private void cmbYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbMonth.Focus();
            }
        }

        private void cmbMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void txtAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPayeeName.Focus();
            }
        }

        private void txtLoanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtInstallmentAmount.Focus();
            }
        }

        private void txtPrincipalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNoOfInstallments.Focus();
            }
        }

        private void txtNoOfInstallments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void txtInstallmentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNoOfInstallments.Focus();
            }
        }

        private void gvlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRefNo.Text = gvlist.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtEmpNo.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
            cmbDeductionGroup.SelectedValue = gvlist.Rows[e.RowIndex].Cells[11].Value.ToString();
            cmbDeductions.SelectedValue = gvlist.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtInstallmentAmount.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            //cmbYear.SelectedValue = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            //cmbMonth.SelectedValue = gvlist.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtNoOfInstallments.Text = gvlist.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPayeeAccNo.Text = gvlist.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtLoanName.Text = gvlist.Rows[e.RowIndex].Cells[9].Value.ToString();
            cmbDivision.SelectedValue = gvlist.Rows[e.RowIndex].Cells[10].Value.ToString();

            gvPayeeList.DataSource = LMaster.ListLoanPayeeDetails(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
            gbTerminate.Enabled = true;
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            LMaster.IntLoanCode = Convert.ToInt32(lblRefNo.Text);
            LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
            LMaster.StrEmpNo = txtEmpNo.Text;
            LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
            LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
            LMaster.StrUserId = FTSPayRollBL.User.StrUserName;
            LMaster.StrTerminateReason = txtReason.Text;
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                if (!String.IsNullOrEmpty(txtReason.Text))
                {
                    if (MessageBox.Show("Are you sure you want to terminate Loan...!", "Terminate Loan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (MessageBox.Show("Confirm Terminate...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            LMaster.TerminateLoan();
                            MessageBox.Show("Loan Terminated successfully");
                            cmdClear.PerformClick();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You Must Provide a Valid Reason To Terminate");
                }
            }
            else
            {
                MessageBox.Show("Please select a loan before terminate");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddACAmounts_Click(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(txtPayeeAccNo.Text))
            //{
            //    MessageBox.Show("Payee Accout No Cannot Be empty");
            //    txtPayeeAccNo.Focus();
            //}
            //else if (String.IsNullOrEmpty(txtPayeeName.Text))
            //{
            //    MessageBox.Show("Payee Account Name can not be empty");
            //    txtPayeeName.Focus();
            //}
            //else if (String.IsNullOrEmpty(txtPayeeAmount.Text))
            //{
            //    MessageBox.Show("Payee Amount can not be empty");
            //    txtPayeeAmount.Focus();
            //}
            //else if (txtNoOfInstallments.Text == "")
            //{
            //    MessageBox.Show("No of installmant can not be empty");
            //    txtNoOfInstallments.Focus();
            //}

            //else if (txtInstallmentAmount.Text == "")
            //{
            //    MessageBox.Show("Installment Amount can not be empty");
            //    txtInstallmentAmount.Focus();
            //}

            

            //else
            //{
            //    LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            //    LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
            //    LMaster.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            //    LMaster.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            //    LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
            //    LMaster.StrEmpNo = txtEmpNo.Text;
            //    LMaster.StrPayeeACNo = txtPayeeAccNo.Text;
            //    LMaster.StrPayeeACName = txtPayeeName.Text;
            //    LMaster.StrPayeeAmount = txtPayeeAmount.Text;
                
                
            //    try
            //    {
            //        if (!LMaster.IsPayeeExists())
            //        {
            //            LMaster.InsertLoanPayeeDetails();
            //        }
            //        else
            //        {
            //            LMaster.UpdateLoanPayeeDetails();
            //        }
            //        RefreshPayeeGrid();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error, " + ex.Message);
            //    }
            //}
            if (txtPayeeAccNo.Text.Trim() == "")
            {
                MessageBox.Show("Enter Payee Account Number to Proceed...!");
            }
            else if (txtPayeeName.Text.Trim() == "")
            {
                MessageBox.Show("Enter Payee Account Name to Proceed...!");
            }
            else if (txtPayeeAmount.Text.Trim() == "")
            {
                MessageBox.Show("Enter Payee Amount to Proceed...!");
            }
            
            else
            {
                DataTable dt = new DataTable();
                DataRow drow;

                dt.Columns.Add(new DataColumn("PayeeAccountNo"));
                dt.Columns.Add(new DataColumn("PayeeName"));
                dt.Columns.Add(new DataColumn("PayeeAmount"));

                drow = dt.NewRow();
                drow[0] = txtPayeeAccNo.Text;
                drow[1] = txtPayeeName.Text;
                drow[2] = Convert.ToDecimal(txtPayeeAmount.Text);

                dt.Rows.Add(drow);

                for (int i = 0; i < gvPayeeList.Rows.Count; i++)
                {
                    drow = dt.NewRow();
                    drow[0] = gvPayeeList.Rows[i].Cells[0].Value.ToString();
                    drow[1] = gvPayeeList.Rows[i].Cells[1].Value.ToString();
                    drow[2] = Convert.ToDecimal(gvPayeeList.Rows[i].Cells[2].Value.ToString());
                    dt.Rows.Add(drow);
                }

                gvPayeeList.DataSource = dt;

                txtInstallmentAmount.Text = CalculateInstallmentAmount().ToString();
                txtPayeeAccNo.Clear();
                txtPayeeName.Clear();
                txtPayeeAmount.Clear();
                AfterPayeeDetailsAdd();
                
            }
        }

        private void AfterPayeeDetailsAdd()
        {
            if (MessageBox.Show("Add More Payee Details", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                txtPayeeAccNo.Focus();
            }
            else
            {
                txtNoOfInstallments.Focus();
            }
        }

        public void RefreshPayeeGrid()
        {
            try
            {
                //gvPayeeList.DataSource = LMaster.ListLoanPayeeDetails(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                
            }
            catch { }
        }

        private void gvPayeeList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRefNo.Text = gvlist.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtEmpNo.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPayeeName.Text = gvPayeeList.Rows[e.RowIndex].Cells[0].ToString();
            txtPayeeAccNo.Text = gvPayeeList.Rows[e.RowIndex].Cells[1].ToString();
            txtPayeeAmount.Text = gvPayeeList.Rows[e.RowIndex].Cells[2].ToString();            
        }

        private void btnDeletePayee_Click(object sender, EventArgs e)
        {

        }

        private void txtPayeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPayeeAmount.Focus();
            }
        }

        private void txtPayeeAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAddACAmounts.Focus();
            }
        }

        private Decimal CalculateInstallmentAmount()
        {
            Decimal decInstallment = 0;
            for (int i = 0; i < gvPayeeList.Rows.Count; i++)
            {
                decInstallment += Convert.ToDecimal(gvPayeeList.Rows[i].Cells[2].Value.ToString());
            }
            return decInstallment;
        }

        private void txtNoOfInstallments_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void txtPayeeAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPayeeName.Focus();
            }
        }

        private void txtPayeeName_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPayeeAmount.Focus();
            }
        }

        private void txtPayeeAmount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAddACAmounts.Focus();
            }
        }

        private void gvPayeeList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtInstallmentAmount.Text = CalculateInstallmentAmount().ToString();
        }

    }
}