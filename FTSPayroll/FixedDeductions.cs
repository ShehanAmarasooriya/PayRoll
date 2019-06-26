using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FixedDeductions : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();

        public FixedDeductions()
        {
            InitializeComponent();
        }

        private void FixedDeductions_Load(object sender, EventArgs e)
        {
            if (myUser.IsAdminUser(FTSPayRollBL.User.StrUserName))
            {
                chkDirectPay.Enabled = true;
            }
            lblTotal.Text = "0";
            groupBox6.Enabled = false;
            groupBox7.Enabled = false;

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductionGroup.DataSource = DeductMaster.ListFixedDeductionGroups();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductionGroupId";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();

            cmbDivision.Text = FTSPayRollBL.User.StrDivision;

            cmbDeductionGroup_SelectedIndexChanged(null, null);
            cmbDivision_SelectedIndexChanged(null, null);

            try
            {
                gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
            }
            catch { }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtDeductAmount.Text == "")
            {
                MessageBox.Show("txtDeduct Amount can not be empty");
                txtDeductAmount.Focus();
            }
            else if (txtNoOfMonths.Text == "")
            {
                MessageBox.Show("No Of Months can not be empty");
                txtNoOfMonths.Focus();
            }
            else
            {
                myEmployeeDeduction.IntFixedDeductId = Convert.ToInt32(lblRefNo.Text);
                myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                myEmployeeDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
                myEmployeeDeduction.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.Text);
                myEmployeeDeduction.StrEmpNO = txtEmpNo.Text;
                myEmployeeDeduction.IntNoOfMonths = Convert.ToInt32(txtNoOfMonths.Text);
                myEmployeeDeduction.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myEmployeeDeduction.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                if (chkFixed.Checked)
                {
                    myEmployeeDeduction.BoolFixed = true;
                }
                else
                {
                    myEmployeeDeduction.BoolFixed = false;
                }
                myEmployeeDeduction.UpdateFixedDeductions();
                MessageBox.Show("Fixed Deductions updated Successfully");
                AfterAdd();
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtDeductAmount.Text == "")
            {
                MessageBox.Show("txtDeduct Amount can not be empty");
                txtDeductAmount.Focus();
            }
            else if (txtNoOfMonths.Text == "" || Convert.ToInt32(txtNoOfMonths.Text) <1)
            {
                MessageBox.Show("No Of Months Can Not Be Empty Or Less Than 1");
                txtNoOfMonths.Focus();
            }
            else if (txtG1EmpNo.Text == "" && ChkGurantorRec.Checked==true)
            {
                if (ChkGurantorRec.Checked)
                {
                    if (txtG1EmpNo.Text == "")
                    {
                        MessageBox.Show("Guarantor1 No Can Not Be Empty.");
                        txtG1EmpNo.Focus();
                    }
                   
                }
            }
            else if (txtG2EmpNo.Text == "" && ChkGurantorRec.Checked == true)
            {
                if (ChkGurantorRec.Checked)
                {
                   
                    if (txtG2EmpNo.Text == "")
                    {
                        MessageBox.Show("Guarantor2 No Can Not Be Empty.");
                        txtG2EmpNo.Focus();
                    }
                }
            }

            else
            {
                try
                {
                    myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    myEmployeeDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
                    myEmployeeDeduction.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.Text);
                    myEmployeeDeduction.StrEmpNO = txtEmpNo.Text;
                    myEmployeeDeduction.IntNoOfMonths = Convert.ToInt32(txtNoOfMonths.Text);
                    myEmployeeDeduction.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    myEmployeeDeduction.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                    myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                    myEmployeeDeduction.DecPrincipalAmount = myEmployeeDeduction.DecDeductAmount * myEmployeeDeduction.IntNoOfMonths;
                    myEmployeeDeduction.DecBalanceAmount = myEmployeeDeduction.DecPrincipalAmount;
                    myEmployeeDeduction.DecRecoveredAmount = 0;
                    myEmployeeDeduction.DecRecoveredInstallments = 0;
                    myEmployeeDeduction.BoolCloseYesNo = false;
                    
                    if (groupBox6.Enabled == true)
                    {
                        myEmployeeDeduction.StrGur1Div = cmbG1Division.SelectedValue.ToString();
                        myEmployeeDeduction.Gurantor1 = txtG1EmpNo.Text;
                    }
                    else
                    {
                        myEmployeeDeduction.StrGur1Div = "NA";
                        myEmployeeDeduction.Gurantor1 = "NA";
                    }
                    if (groupBox7.Enabled == true)
                    {
                        myEmployeeDeduction.StrGur2Div = cmbG2Division.SelectedValue.ToString();
                        myEmployeeDeduction.Gurantor2 = txtG2EmpNo.Text;
                    }
                    else
                    {
                        myEmployeeDeduction.StrGur2Div = "NA";
                        myEmployeeDeduction.Gurantor2 = "NA";
                    }
                    if (chkFixed.Checked)
                    {
                        myEmployeeDeduction.BoolFixed = true;
                    }
                    else
                    {
                        myEmployeeDeduction.BoolFixed = false;
                    }

                    DataTable DtBalance = myEmployeeDeduction.GetBalanceAmounts(myEmployeeDeduction.StrDivision,myEmployeeDeduction.StrEmpNO,myEmployeeDeduction.IntDeductGroupId,myEmployeeDeduction.IntDeduction);
                    if (DtBalance.Rows.Count > 0)
                    {
                        MessageBox.Show("Cannot Add Fixed Deduction,\r\n\r\nEmployee " + txtEmpNo.Text + " Has a " + cmbDeductions.Text + " Deduction Balance On " + DtBalance.Rows[0][1].ToString() + "/" + DtBalance.Rows[0][2].ToString(), "Error");
                        //if (MessageBox.Show("Go To Deduction", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        //{
                        //    txtSearchEmpNo.Text = txtEmpNo.Text;
                        //    txtSearchEmpNo_TextChanged(null, null);
                        //}
                    }
                    else
                    {
                        myEmployeeDeduction.InsertFixedDeductions();
                        MessageBox.Show("Fixed Deductions Added Successfully");
                        AfterAdd();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!, Duplicate Entry.");
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Cancel...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                myEmployeeDeduction.IntFixedDeductId = Convert.ToInt32(lblRefNo.Text);
                myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                myEmployeeDeduction.StrEmpNO = txtEmpNo.Text;
                myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                myEmployeeDeduction.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myEmployeeDeduction.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                if (chkFixed.Checked)
                {
                    myEmployeeDeduction.BoolFixed = true;
                }
                else
                {
                    myEmployeeDeduction.BoolFixed = false;
                }
                    //tempory code until get id from empdeductions
                    if (myEmployeeDeduction.IsRecoveredDeduction(myEmployeeDeduction.IntFromYear, myEmployeeDeduction.IntFromMonth,Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()),Convert.ToInt32(cmbDeductions.SelectedValue.ToString()),cmbDivision.SelectedValue.ToString(),txtEmpNo.Text))
                    //if(true)
                    {
                        MessageBox.Show("Cannot Delete, Already Recovered From This Entry But You May Terminate");
                        
                    }
                    else
                    {
                        try
                        {
                            myEmployeeDeduction.DeleteFixedDeductions();
                            MessageBox.Show("Fixed Deductions Canceled Successfully");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }
                
                cmdClear.PerformClick();

            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            try
            {
                gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
            }
            catch { }
            txtDeductAmount.Text = "";
            lblRefNo.Text = "";
            txtNoOfMonths.Text = "";
            txtEmpName.Text = "";
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmpNo.Text))
            {
                if (!String.IsNullOrEmpty(txtEmpNo.Text))
                {
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Employee Not Available In the Selected Division.");
                    txtEmpNo.Text = "";
                }
            }
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {               
                DataTable dt = new DataTable();
                dt = DeductMaster.getFundDeductionGroup(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                if (Convert.ToBoolean(dt.Rows[0][3].ToString()) == true)
                {
                    txtNoOfMonths.Text = "";
                    txtNoOfMonths.Enabled = true;
                }
                else
                {
                    txtNoOfMonths.Text = "";
                    txtNoOfMonths.Enabled = true;
                }

                DeductMaster.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                cmbDeductions.DataSource = null;
                cmbDeductions.DataSource = DeductMaster.getDeduction();
                cmbDeductions.DisplayMember = "DeductShortName";
                cmbDeductions.ValueMember = "DeductCode";
                cmbDeductions.SelectedIndex = 0;

                //cmbDeductions_SelectedIndexChanged(null, null);
                if (Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 3 || Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 2)
                {
                    chkFixed.Checked = false;
                    chkFixed.Enabled = false;
                }
                else
                {
                    chkFixed.Enabled = true;
                    chkFixed.Checked = false;
                }
                //If Monthly advance-> only one month
                if (Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 3)
                {
                    txtNoOfMonths.Text = "1";
                    txtNoOfMonths.Enabled = false;
                }
                else
                {
                    txtNoOfMonths.Enabled = true;
                }


            }
            catch { }
        }

        private void cmbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblDeduction.Text = DeductMaster.GetDeductionNameByID(Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
            }
            catch { }
        }

        private void gvFixedDeductions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 3 || Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 2)
            {
                gbTerminate.Enabled = false;
                txtReason.Text = "";
            }
            else
            {
                gbTerminate.Enabled = true;
            }
            //If MA No of Months Cannot be Changed.
            if (Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 3)
            {
                txtNoOfMonths.Enabled = false;
            }
            else
            {
                txtNoOfMonths.Enabled = true;
            }

                cmbG1Division.SelectedIndex = -1;
                txtG1EmpNo.Text = "";
                txtG1EmpName.Text = "";
                cmbG2Division.SelectedIndex = -1;
                txtG2EmpNo.Text = "";
                txtG2EmpName.Text = "";
                lblRefNo.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[10].Value.ToString();
                cmbDeductionGroup.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[7].Value.ToString();
                //cmbDeductionGroup_SelectedIndexChanged(null, null);
                cmbDivision.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[9].Value.ToString();
                cmbDeductions.SelectedValue = Convert.ToInt32(gvFixedDeductions.Rows[e.RowIndex].Cells[8].Value.ToString());
                txtEmpNo.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                txtDeductAmount.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtNoOfMonths.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbMonth.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmbYear.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (!String.IsNullOrEmpty(gvFixedDeductions.Rows[e.RowIndex].Cells[11].Value.ToString()))
                {
                    if (!gvFixedDeductions.Rows[e.RowIndex].Cells[12].Value.ToString().Equals("NA"))
                    {
                        cmbG1Division.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[11].Value.ToString();
                        txtG1EmpNo.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[12].Value.ToString();
                    }
                    else
                    {
                        groupBox6.Enabled = false;
                    }
                }
                if (!String.IsNullOrEmpty(gvFixedDeductions.Rows[e.RowIndex].Cells[13].Value.ToString()))
                {
                    if (!gvFixedDeductions.Rows[e.RowIndex].Cells[14].Value.ToString().Equals("NA"))
                    {
                        cmbG2Division.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[13].Value.ToString();
                        txtG2EmpNo.Text = gvFixedDeductions.Rows[e.RowIndex].Cells[14].Value.ToString();
                    }
                    else
                    {
                        groupBox7.Enabled = false;
                    }
                }

                cmdUpdate.Enabled = true;
                cmdDelete.Enabled = true;
                cmdAdd.Enabled = false;
            
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();               
            }
            catch { }
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
                    txtDeductAmount.Focus();
                }
                else
                {
                    MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
            }

        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                //EmployeeList empList = new EmployeeList();
                //empList.Show();
                EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(),"Fixed");
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
                        txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                        txtEmpNo_LeaveChanged();
                    }
                }
            }


        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(), "Fixed");
            empList.Show();
        }

        private void cmbDeductions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbYear.Focus();
            }
        }

        private void AfterAdd()
        {
            try
            {
                lblTotal.Text = Convert.ToDecimal("0.0").ToString();
                gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
            }
            catch { }
            //txtDeductAmount.Text = "";
            //txtNoOfMonths.Text = "";
            txtEmpNo.Clear();
            txtEmpName.Text = "";
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            txtEmpNo.Focus();
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

        private void txtDeductAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtNoOfMonths.Text.Equals("999"))
                {
                    cmbYear.Focus();
                }
                else 
                {
                    if (Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()) == 3)
                    {
                        cmdAdd.Focus();
                    }
                    else
                    {
                        txtNoOfMonths.Focus();
                    }
                }
                
            }
        }

        private void txtNoOfMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
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
            if (chkFixed.Enabled == true)
            {
                if (e.KeyChar == 13)
                {
                    chkFixed.Focus();
                }
            }
            else
            {
                if (e.KeyChar == 13)
                {
                    txtEmpNo.Focus();
                }
            }
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(), "Fixed");
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

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
            }
            catch { }
        }

        private void cmbG1Division_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtG1EmpNo_Leave(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(txtG1EmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtG1EmpNo.Text, cmbG1Division.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Active Employee Within the Division You Selected Above.");
                    this.txtG1EmpName.Text = "";
                    txtG1EmpNo.Text = ""; 
                    txtG1EmpNo.Focus();
                }
                else
                {
                    //cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    this.txtG1EmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtG1EmpNo.Text, cmbG1Division.SelectedValue.ToString());
                    cmbG2Division.Focus();
                }
            }
        }

        private void txtG2EmpNo_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtG2EmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(this.txtG2EmpNo.Text, cmbG2Division.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Active Employee Within the Division You Selected Above.");
                    this.txtG2EmpName.Text = "";
                    this.txtG2EmpNo.Text = "";
                    this.txtG2EmpNo.Focus();
                }
                else
                {
                    //cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    this.txtG2EmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtG2EmpNo.Text, cmbG2Division.SelectedValue.ToString());
                    cmdAdd.Focus();
                }
            }
        }

        private void cmbG1Division_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtG1EmpNo.Focus();
            }
        }

        private void txtG1EmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbG2Division.Focus();
            }
        }

        private void cmbG2Division_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtG2EmpNo.Focus();
            }
        }

        private void txtG2EmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void ChkGurantorRec_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkGurantorRec.Checked)
            {
                groupBox6.Enabled = true;
                cmbG1Division.DataSource = EstDivBlock.ListEstateDivisions();
                cmbG1Division.DisplayMember = "DivisionID";
                cmbG1Division.ValueMember = "DivisionID";
                groupBox7.Enabled = true;
                cmbG2Division.DataSource = EstDivBlock.ListEstateDivisions();
                cmbG2Division.DisplayMember = "DivisionID";
                cmbG2Division.ValueMember = "DivisionID";
            }
            else
            {
                groupBox6.Enabled = false;
                groupBox7.Enabled = false;
            }
        }

        private void txtG1EmpNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChkGurantorRec_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    cmbG1Division.Focus();
            //}
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
                        myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                        myEmployeeDeduction.StrEmpNO = txtEmpNo.Text;
                        myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                        myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                        myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                        myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                        myEmployeeDeduction.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                        myEmployeeDeduction.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                        myEmployeeDeduction.StrReason = txtReason.Text;
                        try
                        {
                            myEmployeeDeduction.TerminateFixedDeductions();
                            MessageBox.Show("Fixed Deductions Terminated Successfully");
                            try
                            {
                                gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                                lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
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

        private void chkFixed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void chkFixed_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

       
        private void btnDirectPayment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm The Direct Payment Of \r\nEmpNO : '" + txtEmpNo.Text + "'!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (String.IsNullOrEmpty(lblRefNo.Text))
                    {
                        MessageBox.Show("Please Select a Deduction");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtDeductAmount.Text))
                        {
                            MessageBox.Show("Amount is not valid");
                            txtDeductAmount.Focus();
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(txtRefNo.Text))
                            {
                                MessageBox.Show("Please add valid Payment Voucher No");
                                txtRefNo.Focus();
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(txtPayReason.Text))
                                {
                                    MessageBox.Show("Please Type a Reason");
                                }
                                else
                                {
                                    myEmployeeDeduction.DirectPaymentFixedDeductions(dtpDP.Value.Date, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString(), txtEmpNo.Text.ToString(), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToDecimal(txtPayAmount.Text), txtRefNo.Text, txtPayReason.Text);
                                    MessageBox.Show("Direct Payment Completed Successfully!");
                                    try
                                    {
                                        gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                                        lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
                                    }
                                    catch { }
                                    txtPayAmount.Text = "";
                                    txtPayReason.Text = "";
                                    txtRefNo.Text = "";
                                    chkDirectPay.Checked = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
        }

        private void chkDirectPay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirectPay.Checked)
            {
                gbDirectPay.Enabled = true;
            }
            else
            {
                gbDirectPay.Enabled = false;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Deduction objDeductions = new Deduction();
            //objDeductions.MdiParent = this;
            objDeductions.Show();
        }
    }
}