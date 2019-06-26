using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FTSPayroll
{
    public partial class Deduction : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        FTSPayRollBL.RFTDeductions RFTDeduct = new FTSPayRollBL.RFTDeductions();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();

        public Deduction()
        {
            InitializeComponent();
        }

        private void Deduction_Load(object sender, EventArgs e)
        {
            if (myUser.IsAdminUser(FTSPayRollBL.User.StrUserName))
            {
                chkDirectPay.Enabled = true;
            }

            rbtnFixed.Checked = true;
            //GBLoanAccount.Enabled = false;
            
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductionGroup.DataSource = DeductMaster.ListAllDeductionGroupsWithout_US_EPF_SD();
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
                //gvFixedDeductions.DataSource = myEmployeeDeduction.ListAllFixedDeductions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                //lblTotal.Text = myEmployeeDeduction.GetFixedDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString("N2");
            }
            catch { }
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DeductMaster.getAllDeductionGroupsExcept_EPF_US_SD(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                if (Convert.ToBoolean(dt.Rows[0][2].ToString()) == true)
                {
                    rbtnLoan.Checked = true;
                    //btnBlkDeduction.Enabled = false;
                }
                else if (Convert.ToBoolean(dt.Rows[0][4].ToString()) == true)
                {
                    rbtnRFT.Checked = true;
                    //btnBlkDeduction.Enabled = false;
                }
                else
                {
                    rbtnFixed.Checked = true;
                    //btnBlkDeduction.Enabled = true;
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
                    if(!rbtnRFT.Checked)
                    txtNoOfMonths.Enabled = true;
                }


            }
            catch { }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbtnFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFixed.Checked)
            {
                GBLoanAccount.Enabled = false;
                gb_rft.Enabled = false;
                txtNoOfMonths.Enabled = true;
                txtDeductAmount.Enabled = true;
            }
        }

        private void rbtnLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoan.Checked)
            {
                //GBLoanAccount.Enabled = false;
                GBLoanAccount.Enabled = true;
                gb_rft.Enabled = false;
                txtNoOfMonths.Enabled = true;
                //txtDeductAmount.Enabled = true;
                txtDeductAmount.Enabled = false;
            }
        }

        private void rbtnRFT_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnRFT.Checked)
            {
                GBLoanAccount.Enabled = false;
                gb_rft.Enabled = true;
                txtNoOfMonths.Text = "0";
                txtNoOfMonths.Enabled = false;
                txtDeductAmount.Enabled = false;
            }


        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                 DataTable dtDetail = DeductMaster.GetDeductionDetailsByID(Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));
                 lblDeduction.Text = dtDetail.Rows[0][0].ToString();
                if (Convert.ToBoolean(dtDetail.Rows[0][1].ToString()) == true)
                {
                    chkFixed.Checked = true;
                }
                else
                    chkFixed.Checked = false;

                AfterAdd();
                //if (rbtnFixed.Checked)
                //{
                //    try
                //    {
                //        gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingFixedDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%");
                //    }
                //    catch
                //    {
                //    }
                //}
                //else if (rbtnLoan.Checked)
                //{
                //    try
                //    {
                //        gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingLoanDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()),"%");
                //    }
                //    catch
                //    {
                //    }
                //}
                //else
                //{
                //    try
                //    {
                //        gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingRFTDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()),"%");
                //    }
                //    catch
                //    {
                //    }
                //}
            }
            catch
            {               
            }
        }

        private void btnDirectPayment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm The Direct Payment Of \r\nEmpNO : '" + txtEmpNo.Text + "'!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (String.IsNullOrEmpty(lblRefNo1.Text))
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
                                    if (rbtnFixed.Checked)
                                    {
                                        myEmployeeDeduction.DirectPaymentFixedDeductions(dtpDP.Value.Date, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString(), txtEmpNo.Text.ToString(), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToDecimal(txtPayAmount.Text), txtRefNo.Text, txtPayReason.Text);
                                        MessageBox.Show("Direct Payment Completed Successfully!");
                                        try
                                        {
                                            AfterAdd();
                                        }
                                        catch { }
                                        txtPayAmount.Text = "";
                                        txtPayReason.Text = "";
                                        txtRefNo.Text = "";
                                        chkDirectPay.Checked = false;
                                    }
                                    else 
                                    {
                                        MessageBox.Show("This Option Is Available Only For Fixed Deductions");
                                    }
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

        private void txtDeductAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvListDeductions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rbtnRFT.Checked)
            {
                gbTerminate.Enabled = false;
            }
            else
            {
                gbTerminate.Enabled = true;
            }
            if(gvListDeductions.Rows[e.RowIndex].Cells[12].Value.ToString().Equals("LOAN"))
            {
                rbtnLoan.Checked = true;
                lblRefNo1.Text = gvListDeductions.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtEmpNo.Text = gvListDeductions.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                cmbDeductionGroup.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[8].Value.ToString();
                cmbDeductions.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtDeductAmount.Text = gvListDeductions.Rows[e.RowIndex].Cells[5].Value.ToString();
                Decimal intNoOfMonths = Convert.ToDecimal(gvListDeductions.Rows[e.RowIndex].Cells[6].Value.ToString());
                txtNoOfMonths.Text = Decimal.ToInt32(intNoOfMonths).ToString();
                cmbDivision.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[11].Value.ToString();
                cmbYear.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[1].Value.ToString());
                cmbMonth.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[2].Value.ToString());
                txtBalance.Text = gvListDeductions.Rows[e.RowIndex].Cells[7].Value.ToString();

                gvPayeeList.DataSource = LMaster.ListLoanPayeeDetails(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()));

            }
            else if (gvListDeductions.Rows[e.RowIndex].Cells[12].Value.ToString().Equals("FIXED"))
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
                rbtnFixed.Checked = true;
                lblRefNo1.Text = gvListDeductions.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtEmpNo.Text = gvListDeductions.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                cmbDeductionGroup.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[8].Value.ToString();
                cmbDeductions.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtDeductAmount.Text = gvListDeductions.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtNoOfMonths.Text = gvListDeductions.Rows[e.RowIndex].Cells[6].Value.ToString();
                cmbDivision.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[11].Value.ToString();
                cmbYear.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[1].Value.ToString());
                cmbMonth.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[2].Value.ToString());
                txtBalance.Text = gvListDeductions.Rows[e.RowIndex].Cells[7].Value.ToString();

            }
            else
            {
                rbtnRFT.Checked = true;
                lblRefNo1.Text = gvListDeductions.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtEmpNo.Text = gvListDeductions.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                cmbDeductionGroup.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[8].Value.ToString());
                cmbDeductions.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[9].Value.ToString());
                txtDeductAmount.Text = gvListDeductions.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtNoOfMonths.Text = gvListDeductions.Rows[e.RowIndex].Cells[6].Value.ToString();
                cmbDivision.SelectedValue = gvListDeductions.Rows[e.RowIndex].Cells[11].Value.ToString();
                cmbYear.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[1].Value.ToString());
                cmbMonth.SelectedValue = Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[2].Value.ToString());
                txtBalance.Text = gvListDeductions.Rows[e.RowIndex].Cells[7].Value.ToString();
                DataTable dtRFT = myEmployeeDeduction.ListOutstandingRFTDeductionsDetails(gvListDeductions.Rows[e.RowIndex].Cells[11].Value.ToString(), Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[8].Value.ToString()), Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[9].Value.ToString()), Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[1].Value.ToString()), Convert.ToInt32(gvListDeductions.Rows[e.RowIndex].Cells[2].Value.ToString()), gvListDeductions.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtRate1.Text = dtRFT.Rows[0][0].ToString();
                txtQuantity.Text = dtRFT.Rows[0][1].ToString();

            }

            /*Disable fields*/
            txtEmpNo.Enabled = false;
            cmbDeductionGroup.Enabled = false;
            cmbDeductions.Enabled = false;
            cmbDivision.Enabled = false;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void btnAddACAmounts_Click(object sender, EventArgs e)
        {
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
            else if (Convert.ToDecimal(txtPayeeAmount.Text)<=0)
            {
                MessageBox.Show("Payee Amount Cannot Be Zero");
                txtPayAmount.Focus();
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

                txtDeductAmount.Text = CalculateInstallmentAmount().ToString();
                txtPayeeAccNo.Clear();
                txtPayeeName.Clear();
                txtPayeeAmount.Clear();
                AfterPayeeDetailsAdd();

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

        private void AfterPayeeDetailsAdd()
        {
            if (MessageBox.Show("Add More Payee Details", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                txtPayeeAccNo.Focus();
            }
            else
            {
                txtEmpNo.Focus();
            }
        }

        private void gvPayeeList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtDeductAmount.Text = CalculateInstallmentAmount().ToString();
        }

        private void cmbDeductions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rbtnFixed.Checked)
                    txtEmpNo.Focus();
                else if (rbtnLoan.Checked)
                    txtPayeeAccNo.Focus();
                else
                    txtRate1.Focus();
            
            }
        }

        private void txtSearchEmpNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchEmpNo.Text.Trim() != "")
                {
                    if (rbtnFixed.Checked)
                    {
                        try
                        {
                            gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingFixedDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), txtSearchEmpNo.Text);
                            lblTotal.Text = myEmployeeDeduction.GetOutstandingFixedDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), txtSearchEmpNo.Text).ToString("N2");
                        }
                        catch
                        {
                        }
                    }
                    else if (rbtnLoan.Checked)
                    {
                        try
                        {
                            gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingLoanDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), txtSearchEmpNo.Text);
                            lblTotal.Text = myEmployeeDeduction.GetOutstandingLoanDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), txtSearchEmpNo.Text).ToString("N2");
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingRFTDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), txtSearchEmpNo.Text,Convert.ToInt32(cmbYear.SelectedValue.ToString()),Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                            lblTotal.Text = myEmployeeDeduction.GetOutstandingRFTDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), txtSearchEmpNo.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString())).ToString("N2");
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    if (rbtnFixed.Checked)
                    {
                        try
                        {
                            gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingFixedDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%");
                            lblTotal.Text = myEmployeeDeduction.GetOutstandingFixedDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%").ToString("N2");
                        }
                        catch
                        {
                        }
                    }
                    else if (rbtnLoan.Checked)
                    {
                        try
                        {
                            gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingLoanDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%");
                            lblTotal.Text = myEmployeeDeduction.GetOutstandingLoanDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%").ToString("N2");
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingFixedDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%");
                            lblTotal.Text = myEmployeeDeduction.GetOutstandingRFTDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%",Convert.ToInt32(cmbYear.SelectedValue.ToString()),Convert.ToInt32(cmbMonth.SelectedValue.ToString())).ToString("N2");
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch { }
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                //EmployeeList empList = new EmployeeList();
                //empList.Show();
                EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(), "Deduction");
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

        private void txtDeductAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rbtnFixed.Checked)
                {
                    if (txtNoOfMonths.Enabled == false)
                    {
                        cmdAdd.Focus();
                    }
                    else
                    txtNoOfMonths.Focus();
                }
                else if (rbtnLoan.Checked)
                    txtNoOfMonths.Focus();
                else
                {
                    txtNoOfMonths.Text = "";
                    cmdAdd.Focus();
                }
            }
        }

        private void txtRate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQuantity.Focus();
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        public void calculate()
        {
            try
            {
                Decimal rate = Convert.ToDecimal(txtRate1.Text.Trim());
                Decimal Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                Decimal Amount = Math.Round(rate * Quantity, 2);
                txtDeductAmount.Text = Amount.ToString();
            }
            catch { }
        }

        private void txtNoOfMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void chkDirectPay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirectPay.Checked)
            {
                gbDirectPay.Enabled = true;
                dtpDP.Focus();
            }
            else
            {
                gbDirectPay.Enabled = false;
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (rbtnFixed.Checked)
            {
                #region FixedDeductionUpdate
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
                    myEmployeeDeduction.IntFixedDeductId = Convert.ToInt32(lblRefNo1.Text);
                    myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    myEmployeeDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
                    myEmployeeDeduction.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.Text);
                    myEmployeeDeduction.StrEmpNO = txtEmpNo.Text.PadLeft(4,'0');
                    myEmployeeDeduction.IntNoOfMonths = Convert.ToInt32(txtNoOfMonths.Text);
                    myEmployeeDeduction.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    myEmployeeDeduction.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                    myEmployeeDeduction.DecPrincipalAmount = myEmployeeDeduction.DecDeductAmount * myEmployeeDeduction.IntNoOfMonths;
                    myEmployeeDeduction.DecBalanceAmount = myEmployeeDeduction.DecPrincipalAmount;
                    myEmployeeDeduction.DecRecoveredAmount = 0;
                    myEmployeeDeduction.DecRecoveredInstallments = 0;
                    myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                    if (chkFixed.Checked)
                    {
                        myEmployeeDeduction.BoolFixed = true;
                    }
                    else
                    {
                        myEmployeeDeduction.BoolFixed = false;
                    }
                    if (myEmployeeDeduction.IsRecoveredDeduction(myEmployeeDeduction.IntFromYear, myEmployeeDeduction.IntFromMonth, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString(), txtEmpNo.Text))
                    //if(true)
                    {
                        if (MessageBox.Show("Confirm To Update Only Monthly Deduct Amount -Balance Amount Of Deduction Will Not Change...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            myEmployeeDeduction.UpdateFixedDeductions();
                            MessageBox.Show("Fixed Deductions updated Successfully");
                            AfterAdd();
                        }
                    }
                    else
                    {
                        myEmployeeDeduction.UpdateFixedDeductions(true);
                        MessageBox.Show("Fixed Deductions updated Successfully");
                        AfterAdd();
                    }
                } 
                #endregion
            }
            else if (rbtnLoan.Checked)
            {
                #region LoanDeductionUpdate
                if (String.IsNullOrEmpty(lblDeduction.Text))
                {
                    MessageBox.Show(" Deduction Name Cannot Be Empty");
                    cmbDeductions.Focus();
                }

                else if (txtNoOfMonths.Text == "")
                {
                    MessageBox.Show("No of installmant can not be empty");
                    txtNoOfMonths.Focus();
                }

                else if (txtDeductAmount.Text == "")
                {
                    MessageBox.Show("Installment Amount can not be empty");
                    txtDeductAmount.Focus();
                }


                else if (gvPayeeList.Rows.Count < 1)
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
                    LMaster.StrEmpNo = txtEmpNo.Text.PadLeft(4,'0');
                    LMaster.StrName = lblDeduction.Text;
                    LMaster.FlNoOfInstallments = float.Parse(txtNoOfMonths.Text);
                    LMaster.FlInstallmentAmount = float.Parse(txtDeductAmount.Text);
                    LMaster.FlPrincipalAmount = float.Parse(txtNoOfMonths.Text) * float.Parse(txtDeductAmount.Text);
                    LMaster.IntLoanCode = Convert.ToInt32(lblRefNo1.Text);

                    LMaster.DtLoanDate = Convert.ToDateTime(cmbMonth.SelectedValue.ToString() + "/1/" + cmbYear.SelectedValue.ToString());

                    if (myEmployeeDeduction.IsRecoveredLoanDeduction(myEmployeeDeduction.IntFromYear, myEmployeeDeduction.IntFromMonth, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString(), txtEmpNo.Text))
                    //if(true)
                    {
                        if (MessageBox.Show("Confirm To Update Only Monthly Deduct Amount -Balance Amount Of Deduction Will Not Change...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            try
                            {
                                LMaster.UpdateLoanOnlyDeductAmount();
                                LMaster.DeleteLoanPayeeDetails();
                                for (int j = 0; j < gvPayeeList.Rows.Count; j++)
                                {
                                //LMaster.StrPayeeACNo = txtEmpNo.Text;
                                //LMaster.StrPayeeACName = txtEmpName.Text;
                                //LMaster.DecPayeeAmount = Convert.ToDecimal(txtDeductAmount.Text);
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
                                AfterAdd();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error, " + ex.Message);
                            }
                        }


                    }
                    else
                    {
                        try
                        {
                            LMaster.UpdateLoanOnlyDeductAmount(false);
                            LMaster.DeleteLoanPayeeDetails();
                            //for (int j = 0; j < gvPayeeList.Rows.Count; j++)
                            //{
                                //LMaster.StrPayeeACNo = gvPayeeList.Rows[j].Cells[0].Value.ToString();
                                //LMaster.StrPayeeACName = gvPayeeList.Rows[j].Cells[1].Value.ToString();
                                //LMaster.DecPayeeAmount = Convert.ToDecimal(gvPayeeList.Rows[j].Cells[2].Value.ToString());
                            LMaster.StrPayeeACNo = txtEmpNo.Text;
                            LMaster.StrPayeeACName = txtEmpName.Text;
                            LMaster.DecPayeeAmount = Convert.ToDecimal(txtDeductAmount.Text);
                                try
                                {

                                    LMaster.InsertLoanPayeeDetails();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Update Payee Details Failed.., " + ex.Message);
                                }
                            //}
                            MessageBox.Show("Loan Updated Successfully");
                            AfterAdd();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }
                }  
                #endregion
            }
            else
            {
                #region RFTDeductionUpdate
                try
                {
                    if (txtEmpNo.Text == "")
                    {
                        MessageBox.Show("EmpNo cannot be empty");
                        txtEmpNo.Focus();
                    }

                    else if (txtRate1.Text.Trim() == "")
                    {
                        MessageBox.Show("Rate can not be empty.!");
                    }
                    else if (txtQuantity.Text.Trim() == "")
                    {
                        MessageBox.Show("Quantity can not be empty.!");
                    }
                    else if (!String.IsNullOrEmpty(lblRefNo1.Text))
                    {
                        RFTDeduct.IntRFTDeductId = Convert.ToInt32(lblRefNo1.Text);

                        RFTDeduct.StrDivision = cmbDivision.SelectedValue.ToString();
                        RFTDeduct.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                        RFTDeduct.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                        RFTDeduct.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                        RFTDeduct.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                        RFTDeduct.StrEmpNo = txtEmpNo.Text.PadLeft(4,'0');
                        RFTDeduct.FlRate = float.Parse(txtRate1.Text.Trim());
                        RFTDeduct.FlQty = float.Parse(txtQuantity.Text.Trim());
                        RFTDeduct.FlDeductAmount = RFTDeduct.FlRate * RFTDeduct.FlQty;

                        try
                        {
                            String status = RFTDeduct.UpdateRFTDeductions();

                            if (status == "UPDATED")
                            {
                                MessageBox.Show("Deduction Updated Successfully");
                                AfterAdd();
                            }
                            else if (status == "NOTEXISTS")
                            {
                                MessageBox.Show("Deduction Not Exists");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error!, " + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("Please Select Data Before Update");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
                #endregion
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmpNo.Text))
            {
                MessageBox.Show("EmpNo can not be empty");
                txtEmpNo.Focus();
            }
            else
            {
                
                if (rbtnFixed.Checked)
                {

                    #region FixedDeductionInsert
                    if (txtDeductAmount.Text == "")
                    {
                        MessageBox.Show("txtDeduct Amount can not be empty");
                        txtDeductAmount.Focus();
                    }
                    else if (txtNoOfMonths.Text == "" || Convert.ToInt32(txtNoOfMonths.Text) < 1)
                    {
                        MessageBox.Show("No Of Months Can Not Be Empty Or Less Than 1");
                        txtNoOfMonths.Focus();
                    }
                    else if ((float.Parse(txtNoOfMonths.Text) * float.Parse(txtDeductAmount.Text)) <= 0)
                    {
                        MessageBox.Show("No Of Installments Or Monthly Deduct Amount Cannot be Zero");
                    }

                    else
                    {
                        try
                        {
                            myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                            myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                            myEmployeeDeduction.StrDivision = cmbDivision.SelectedValue.ToString();
                            myEmployeeDeduction.StrEmpNO = txtEmpNo.Text.PadLeft(4,'0');
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
                            myEmployeeDeduction.StrGur1Div = "NA";
                            myEmployeeDeduction.Gurantor1 = "NA";
                            myEmployeeDeduction.StrGur2Div = "NA";
                            myEmployeeDeduction.Gurantor2 = "NA";

                            myEmployeeDeduction.BoolFixed = false;

                            DataTable DtBalance = myEmployeeDeduction.GetBalanceAmounts(myEmployeeDeduction.StrDivision, myEmployeeDeduction.StrEmpNO, myEmployeeDeduction.IntDeductGroupId, myEmployeeDeduction.IntDeduction);
                            if (DtBalance.Rows.Count > 0)
                            {
                                MessageBox.Show("Cannot Add Fixed Deduction,\r\n\r\nEmployee " + txtEmpNo.Text + " Has a " + cmbDeductions.Text + " Deduction Balance On " + DtBalance.Rows[0][1].ToString() + "/" + DtBalance.Rows[0][2].ToString(), "Error");
                                txtEmpNo.Focus();
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
                    #endregion
                }
                else if (rbtnLoan.Checked)
                {
                    #region LoanDeductionInsert
                    if (String.IsNullOrEmpty(lblDeduction.Text))
                    {
                        MessageBox.Show(" Deduction Name Cannot Be Empty");
                        cmbDeductions.Focus();
                    }

                    else if (txtNoOfMonths.Text == "")
                    {
                        MessageBox.Show("No of installmant can not be empty");
                        txtNoOfMonths.Focus();
                    }

                    else if (txtDeductAmount.Text == "")
                    {
                        MessageBox.Show("Installment Amount can not be empty");
                        txtDeductAmount.Focus();
                    }
                    else if ((float.Parse(txtNoOfMonths.Text) * float.Parse(txtDeductAmount.Text)) <= 0)
                    {
                        MessageBox.Show("No Of Installments Or Monthly Deduct Amount Cannot be Zero");
                    }


                    //else if (gvPayeeList.Rows.Count < 1)
                    //{
                    //    MessageBox.Show("No Payee Details Found.");
                    //}

                    else
                    {
                        LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                        LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                        LMaster.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                        LMaster.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                        LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
                        LMaster.StrEmpNo = txtEmpNo.Text.PadLeft(4, '0');
                        LMaster.StrName = lblDeduction.Text;
                        LMaster.FlNoOfInstallments = float.Parse(txtNoOfMonths.Text);
                        LMaster.FlInstallmentAmount = float.Parse(txtDeductAmount.Text);
                        LMaster.FlPrincipalAmount = float.Parse(txtNoOfMonths.Text) * float.Parse(txtDeductAmount.Text);

                        LMaster.DtLoanDate = Convert.ToDateTime(cmbMonth.SelectedValue.ToString() + "/1/" + cmbYear.SelectedValue.ToString());
                        try
                        {
                            //---------
                            DataTable DtBalance = myEmployeeDeduction.GetLoanBalanceAmounts(LMaster.StrDivision, LMaster.StrEmpNo, LMaster.IntDeductionGroup, LMaster.IntDeductCode);
                            if (DtBalance.Rows.Count > 0)
                            {
                                MessageBox.Show("Cannot Add Loan Deduction,\r\n\r\nEmployee " + txtEmpNo.Text + " Has a " + cmbDeductions.Text + " Deduction Balance On " + DtBalance.Rows[0][1].ToString() + "/" + DtBalance.Rows[0][2].ToString(), "Error");
                                txtEmpNo.Focus();
                                //if (MessageBox.Show("Go To Deduction", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                //{
                                //    txtSearchEmpNo.Text = txtEmpNo.Text;
                                //    txtSearchEmpNo_TextChanged(null, null);
                                //}
                            }
                            else
                            {
                                LMaster.InsertLoan();
                                LMaster.DeleteLoanPayeeDetails();
                                for (int j = 0; j < gvPayeeList.Rows.Count; j++)
                                {
                                    LMaster.StrPayeeACNo = gvPayeeList.Rows[j].Cells[0].Value.ToString();
                                    LMaster.StrPayeeACName = gvPayeeList.Rows[j].Cells[1].Value.ToString();
                                    LMaster.DecPayeeAmount = Convert.ToDecimal(gvPayeeList.Rows[j].Cells[2].Value.ToString());
                                    //LMaster.StrPayeeACNo = LMaster.StrEmpNo;
                                    //LMaster.StrPayeeACName = txtEmpName.Text;
                                    //LMaster.DecPayeeAmount = Convert.ToDecimal(LMaster.FlInstallmentAmount);

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
                            //-------------

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    } 
                    #endregion
                }
                else
                {
                    #region RFTDeductionInsert
                    try
                    {
                        if (cmbDeductionGroup.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a deduction Group");
                            cmbDeductionGroup.Focus();
                        }
                        else if (cmbDeductions.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a deduction");
                            cmbDeductions.Focus();
                        }
                        else if (txtRate1.Text.Trim() == "")
                        {
                            MessageBox.Show("Rate can not be empty.!");
                        }
                        else if (txtQuantity.Text.Trim() == "")
                        {
                            MessageBox.Show("Quantity can not be empty.!");
                        }
                        if (Convert.ToDecimal(txtRate1.Text) * Convert.ToDecimal(txtQuantity.Text) <= 0)
                        {
                            MessageBox.Show("Rate Or qty Cannot Be Zero");
                        }
                        else
                        {
                            RFTDeduct.StrDivision = cmbDivision.SelectedValue.ToString();
                            RFTDeduct.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                            RFTDeduct.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                            RFTDeduct.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                            RFTDeduct.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                            RFTDeduct.StrEmpNo = txtEmpNo.Text.Trim().PadLeft(4, '0');
                            RFTDeduct.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                            RFTDeduct.FlRate = float.Parse(txtRate1.Text.Trim());
                            RFTDeduct.FlQty = float.Parse(txtQuantity.Text.Trim());
                            RFTDeduct.FlDeductAmount = RFTDeduct.FlRate * RFTDeduct.FlQty;

                            try
                            {
                                String status = RFTDeduct.InsertRFTDeductions();

                                if (status == "ADDED")
                                {
                                    MessageBox.Show("Deduction Added Successfully");
                                    //cmdCancel.PerformClick();
                                    AfterAdd();
                                }
                                else if (status == "EXISTS")
                                {
                                    MessageBox.Show("Deduction Already Exists");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error!, " + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    } 
                    #endregion
                }
            }
        }

        private void AfterAdd()
        {
            if (rbtnFixed.Checked)
            {
                try
                {
                    //lblTotal.Text = Convert.ToDecimal("0.0").ToString();
                    gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingFixedDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()),Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%");
                    gvPayeeList.DataSource = null;
                    lblTotal.Text = myEmployeeDeduction.GetOutstandingFixedDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%").ToString("N2");
                    txtEmpNo.Focus();
                }
                catch { }
            }
            else if (rbtnLoan.Checked)
            {
                try
                {
                    gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingLoanDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%");
                    gvPayeeList.DataSource = null;
                    lblTotal.Text = myEmployeeDeduction.GetOutstandingLoanDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%").ToString("N2");
                    txtPayeeAccNo.Focus();
                    //txtEmpNo.Focus();
                }
                catch {}
            }
            else
            {
                try
                {
                    gvListDeductions.DataSource = myEmployeeDeduction.ListOutstandingRFTDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%",Convert.ToInt32(cmbYear.SelectedValue.ToString()),Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    gvPayeeList.DataSource = null;
                    lblTotal.Text = myEmployeeDeduction.GetOutstandingRFTDeductionTotal(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), "%",Convert.ToInt32(cmbYear.SelectedValue.ToString()),Convert.ToInt32(cmbMonth.SelectedValue.ToString())).ToString("N2");
                    txtEmpNo.Focus();
                }
                catch
                {
                }
            }
            //txtDeductAmount.Text = "";
            //txtNoOfMonths.Text = "";

            /*Enable disbled fields*/
            txtEmpNo.Enabled = true;
            cmbDeductionGroup.Enabled = true;
            cmbDeductions.Enabled = true;
            cmbDivision.Enabled = true;
            cmbYear.Enabled = true;
            cmbMonth.Enabled = true;

            txtEmpNo.Clear();
            txtEmpName.Text = "";
            txtBalance.Clear();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (rbtnFixed.Checked)
            {
                #region FixedDeductionDelete
                if (MessageBox.Show("Confirm Cancel...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    myEmployeeDeduction.IntFixedDeductId = Convert.ToInt32(lblRefNo1.Text);
                    myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    myEmployeeDeduction.StrEmpNO = txtEmpNo.Text.PadLeft(4,'0');
                    myEmployeeDeduction.DecDeductAmount = Convert.ToDecimal(txtDeductAmount.Text);
                    myEmployeeDeduction.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    myEmployeeDeduction.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    myEmployeeDeduction.StrUserId = FTSPayRollBL.User.StrUserName;
                    myEmployeeDeduction.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    myEmployeeDeduction.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());


                    //tempory code until get id from empdeductions
                    if (myEmployeeDeduction.IsRecoveredDeduction(myEmployeeDeduction.IntFromYear, myEmployeeDeduction.IntFromMonth, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString(), txtEmpNo.Text))
                    //if(true)
                    {
                        MessageBox.Show("Cannot Delete, Already Recovered From This Entry But You May Terminate");

                    }
                    else
                    {
                        try
                        {
                            myEmployeeDeduction.DeleteFixedDeductions();
                            MessageBox.Show("Fixed Deductions Deleted Successfully");
                            AfterAdd();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }



                } 
                #endregion
            }
            else if (rbtnLoan.Checked)
            {
                #region LoanDeductionDelete
                if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    LMaster.IntFromMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    LMaster.IntFromYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
                    LMaster.StrEmpNo = txtEmpNo.Text.PadLeft(4,'0');
                    LMaster.StrName = lblDeduction.Text;
                    LMaster.FlNoOfInstallments = float.Parse(txtNoOfMonths.Text);
                    LMaster.FlInstallmentAmount = float.Parse(txtDeductAmount.Text);
                    LMaster.IntLoanCode = Convert.ToInt32(lblRefNo1.Text);
                    LMaster.StrUserId = FTSPayRollBL.User.StrUserName;
                    //if (!LMaster.IsProcessedEntry(Convert.ToInt32(lblRefNo.Text)))

                    if (myEmployeeDeduction.IsRecoveredLoanDeduction(LMaster.IntFromYear, LMaster.IntFromMonth, Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeductions.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString(), txtEmpNo.Text))
                    {

                        MessageBox.Show("Cannot Delete, This Entry Refers to Processed Data");
                    }
                    else
                    {
                        try
                        {
                            LMaster.DeleteLoanPayeeDetails();
                            LMaster.DeleteLoan();
                            MessageBox.Show("Loan Deleted successfully");
                            AfterAdd();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }

                } 
                #endregion
            }
            else
            {
                #region RFTDeductionDelete
                try
                {
                    if (!String.IsNullOrEmpty(lblRefNo1.Text))
                    {
                        RFTDeduct.IntRFTDeductId = Convert.ToInt32(lblRefNo1.Text);


                        try
                        {
                            String status = RFTDeduct.DeleteRFTDeductions();

                            if (status == "DELETED")
                            {
                                MessageBox.Show("Deduction Delete Successfully");
                                AfterAdd();
                            }
                            else if (status == "NOTEXISTS")
                            {
                                MessageBox.Show("Deduction Not Exists");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error!, " + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("Please Select Data Before Update");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
                #endregion
            }
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
                    if (rbtnLoan.Checked)
                    {
                        txtNoOfMonths.Focus();
                        
                        //txtDeductAmount.Focus();
                        //txtDeductAmount.SelectAll();
                    }
                    else if (rbtnRFT.Checked)
                    {
                        //this.txtDeductAmount.SelectionStart = 0;
                        //this.txtDeductAmount.SelectionStart = this.txtDeductAmount.Text.Length;
                        txtRate1.Text = DeductMaster.GetFoodStuffCodeRate(Convert.ToInt32(cmbDeductions.SelectedValue.ToString())).ToString();
                        //txtRate1.Focus();
                        txtQuantity.Focus();
                        txtQuantity.SelectAll();
                        txtRate1.Enabled = false;
                    }
                    else
                    {
                        txtDeductAmount.Focus();
                        txtDeductAmount.SelectAll();
                        //this.txtDeductAmount.SelectionStart = 0;
                        //this.txtDeductAmount.SelectionStart = this.txtDeductAmount.Text.Length;
                        
                    }
                    
                }
                else
                {
                    MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
            }

        }

        private void txtPayeeAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPayeeName.Focus();
            }
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

        private void cmbDeductionGroup_Leave(object sender, EventArgs e)
        {            
              
        }

        private void txtPayeeAmount_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtPayeeAmount.Text, @"^[0-9]{0,6}(\.[0-9]{1,2})?$"))
            {
                MessageBox.Show("Invalid Amount");
                txtPayeeAmount.Focus();
            }
        }

        private void cmbDeductionGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDeductions.Focus();
            }
        }

        private void txtRate1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBalance_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            if (rbtnFixed.Checked)
            {
                if (!String.IsNullOrEmpty(lblRefNo1.Text))
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
                            myEmployeeDeduction.IntFixedDeductId = Convert.ToInt32(lblRefNo1.Text);
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
                                    AfterAdd();
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
            else if (rbtnLoan.Checked)
            {
                try
                {
                    LMaster.IntLoanCode = Convert.ToInt32(lblRefNo1.Text);
                    LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    LMaster.StrEmpNo = txtEmpNo.Text;
                    LMaster.FlInstallmentAmount = float.Parse(txtNoOfMonths.Text);
                    LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                    LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                    LMaster.StrUserId = FTSPayRollBL.User.StrUserName;
                    LMaster.StrTerminateReason = txtReason.Text;
                    if (!String.IsNullOrEmpty(lblRefNo1.Text))
                    {
                        if (!String.IsNullOrEmpty(txtReason.Text))
                        {
                            if (MessageBox.Show("Are you sure you want to terminate Loan...!", "Terminate Loan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (MessageBox.Show("Confirm Terminate...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    LMaster.TerminateLoan();
                                    MessageBox.Show("Loan Terminated successfully");
                                    AfterAdd();
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
                catch
                {
                }
            }
            else
            {
                try
                {
                    
                }
                catch
                {
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            AfterAdd();
        }

        private void gvPayeeList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtDeductAmount.Text = CalculateInstallmentAmount().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnEmpReg_Click(object sender, EventArgs e)
        {
            //ds = myEmployeeDeduction.ListEmployeewiseOutstandingDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            
                DataTable dsDivisionReport = new DataTable();
                dsDivisionReport = myEmployeeDeduction.ListEmployeewiseOutstandingDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString())).Tables[0];
                if (dsDivisionReport.Rows.Count > 0)
                {
                    dsDivisionReport.WriteXml("EmpDeductionRegister.xml");

                    DeductionsRegisterEmpWise_RPT1 objReport = new DeductionsRegisterEmpWise_RPT1();
                    objReport.SetDataSource(dsDivisionReport);
                    ReportViewerForm objReportViewer = new ReportViewerForm();

                    objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                    objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    objReport.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
                    objReport.SetParameterValue("Period", cmbYear.SelectedValue.ToString().PadLeft(4,'0')+"/"+cmbMonth.SelectedValue.ToString().PadLeft(2,'0'));
                    objReportViewer.crystalReportViewer1.ReportSource = objReport;
                    objReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview.");
                }

        }

        private void btnDedMaster_Click(object sender, EventArgs e)
        {
            DeductionMaster objDedMaster = new DeductionMaster();
            objDedMaster.Show();
        }

        private void btnReg2_Click(object sender, EventArgs e)
        {
            DataTable dsDivisionReport = new DataTable();
            dsDivisionReport = myEmployeeDeduction.ListEmployeewiseOutstandingDeductions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString())).Tables[0];
            if (dsDivisionReport.Rows.Count > 0)
            {
                dsDivisionReport.WriteXml("EmpDeductionRegister.xml");

                DeductionsRegisterEmpWise objReport = new DeductionsRegisterEmpWise();
                objReport.SetDataSource(dsDivisionReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
                objReport.SetParameterValue("Period", cmbYear.SelectedValue.ToString().PadLeft(4, '0') + "/" + cmbMonth.SelectedValue.ToString().PadLeft(2, '0'));
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview.");
            }
        }

        private void btnBlkDeduction_Click(object sender, EventArgs e)
        {
            //BulkDeductionFRM blk = new BulkDeductionFRM();
            //BulkDeductionFRM._Division = cmbDivision.Text; ;
            //BulkDeductionFRM._DivisionId = cmbDivision.SelectedValue.ToString();
            //BulkDeductionFRM.DeductionGrp = cmbDeductionGroup.Text;
            //BulkDeductionFRM.DeductionGrpID = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            //BulkDeductionFRM.DeductionID = Convert.ToInt32(cmbDeductions.SelectedValue.ToString()); ;
            //BulkDeductionFRM.Deduction = cmbDeductions.Text;
            //BulkDeductionFRM._MonthID = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            //BulkDeductionFRM._Month = cmbMonth.SelectedValue.ToString();
            //BulkDeductionFRM._year = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            //if (chkFixed.Checked)
            //{
            //    myEmployeeDeduction.BoolFixed = true;
            //}
            //else
            //{
            //    myEmployeeDeduction.BoolFixed = false;
            //}




            //blk.Show();
        }

        private void btnUpdateRates_Click(object sender, EventArgs e)
        {
            RFTRateMaster objRateMaster = new RFTRateMaster();
            objRateMaster.Show();


        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMonth_Leave(object sender, EventArgs e)
        {
            String strDateOk = "";
            myEntries.IntEntryYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            myEntries.IntEntryMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            /*Blocked for BPL*/
            if (FTSPayRollBL.User.BoolDayBlockAvailable)
            {
                strDateOk = myEntries.CheckMonthDifference();
            }
            else
            {
                strDateOk = "OK";
            }
            if ((strDateOk.Equals("OK")))
            {
                cmbMonth_SelectedIndexChanged(null, null);
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                //MChit.DtDate = dtpDate.Value.Date;
                cmbMonth.Focus();
            }
            else if (strDateOk.Equals("POST_DATE_BLOCK"))
            {
                MessageBox.Show("Post Date Entry Blocked.", "Blocked Entries");

                //MChit.DtDate = cmbMonth.Value.Date;
                cmbMonth.Focus();
            }
            else if (strDateOk.Equals("CONFIRMED"))
            {
                MessageBox.Show("Already Confirmed.", "Entries Blocked");

                //MChit.DtDate = dtpDate.Value.Date;
                cmbMonth.Focus();
            }
            else
            {
                MessageBox.Show("This Date Data Entries Are Blocked Now, Please Contact Head Office For Date Release.");
                this.Close();
            }
        }
    }
}