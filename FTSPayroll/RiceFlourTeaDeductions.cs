using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class RiceFlourTeaDeductions : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.RFTDeductions RFTDeduct = new FTSPayRollBL.RFTDeductions();
        FTSPayRollBL.EmployeeDeduction empDeduct = new FTSPayRollBL.EmployeeDeduction();
        public RiceFlourTeaDeductions()
        {
            InitializeComponent();
        }

        private void RiceFlourTeaDeductions_Load(object sender, EventArgs e)
        {
            try
            {
                cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
                cmbDivision.DisplayMember = "DivisionID";
                cmbDivision.ValueMember = "DivisionID";
                cmbDivision.Text = FTSPayRollBL.User.StrDivision;

                cmbDeductGroup.DataSource = DeductMaster.ListRFTDeductionGroups();
                cmbDeductGroup.DisplayMember = "DeductGroupName";
                cmbDeductGroup.ValueMember = "DeductionGroupId";
                cmbDeductGroup_SelectedIndexChanged(null, null);

                //cmbCategory.DataSource = EmpCat.ListCategories();
                //cmbCategory.DisplayMember = "CategoryName";
                //cmbCategory.ValueMember = "CategoryID";

                cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
                cmbYear.DisplayMember = "Year";
                cmbYear.ValueMember = "Year";
                cmbYear.SelectedValue = YMonth.getLastYearID();

                cmbMonth.DataSource = YMonth.ListMonths();
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";
                cmbMonth.SelectedValue = YMonth.getLastMonthID();

                cmbDivision.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions();
        }

        private void cmbDeductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeduct.DataSource = null;
                lblDeduction.Text = "";
                cmbDeduct.DataSource = DeductMaster.ListDeduction(int.Parse(cmbDeductGroup.SelectedValue.ToString()));
                cmbDeduct.DisplayMember = "DeductShortName";
                cmbDeduct.ValueMember = "DeductCode";


            }
            catch (Exception ex)
            { }
            
        }
       
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDeductGroup.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a deduction Group");
                    cmbDeductGroup.Focus();
                }
                else if (cmbDeduct.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a deduction");
                    cmbDeduct.Focus();
                }
                else if (txtRate1.Text.Trim() == "")
                {
                    MessageBox.Show("Rate can not be empty.!");
                }
                else if (txtQuantity.Text.Trim() == "")
                {
                    MessageBox.Show("Quantity can not be empty.!");
                }              
                else
                {
                    RFTDeduct.StrDivision = cmbDivision.SelectedValue.ToString();
                    RFTDeduct.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    RFTDeduct.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    RFTDeduct.IntDeductionGroup = Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString());
                    RFTDeduct.IntDeduction = Convert.ToInt32(cmbDeduct.SelectedValue.ToString());
                    RFTDeduct.StrEmpNo = txtEmpNo.Text.Trim();
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
            
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {

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
                else if (!String.IsNullOrEmpty(lblRefNo.Text))
                {
                    RFTDeduct.IntRFTDeductId = Convert.ToInt32(lblRefNo.Text);

                    RFTDeduct.StrDivision = cmbDivision.SelectedValue.ToString();
                    RFTDeduct.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    RFTDeduct.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    RFTDeduct.IntDeductionGroup = Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString());
                    RFTDeduct.IntDeduction = Convert.ToInt32(cmbDeduct.SelectedValue.ToString());
                    RFTDeduct.StrEmpNo = txtEmpNo.Text;
                    RFTDeduct.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    RFTDeduct.FlRate = float.Parse(txtRate1.Text.Trim());
                    RFTDeduct.FlQty = float.Parse(txtQuantity.Text.Trim());
                    RFTDeduct.FlDeductAmount = RFTDeduct.FlRate * RFTDeduct.FlQty;

                    try
                    {
                        String status = RFTDeduct.UpdateRFTDeductions();

                        if (status == "UPDATED")
                        {
                            MessageBox.Show("Deduction Updated Successfully");
                            cmdCancel.PerformClick();
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
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(lblRefNo.Text))
                {
                    RFTDeduct.IntRFTDeductId = Convert.ToInt32(lblRefNo.Text);


                    try
                    {
                        String status = RFTDeduct.DeleteRFTDeductions();

                        if (status == "DELETED")
                        {
                            MessageBox.Show("Deduction Delete Successfully");
                            cmdCancel.PerformClick();
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
        }

        public void SetType(String dType)
        {
            if (dType.Equals(rbtnRice.Text))
            {
                rbtnRice.Checked = true; 
            }
            else if (dType.Equals(rbtnFlour.Text))
            {
                rbtnFlour.Checked = true;
            }
            else if (dType.Equals(rbtnTea.Text))
            {
                rbtnTea.Checked = true;
            }
            else
            {
                MessageBox.Show("Type Not Selected.");
            }
        }

        private void gvRFTDeductList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRefNo.Text = gvRFTDeductList.Rows[e.RowIndex].Cells[9].Value.ToString();
            cmbDeductGroup.SelectedValue = Convert.ToInt32(gvRFTDeductList.Rows[e.RowIndex].Cells[6].Value.ToString());
            cmbDeduct.SelectedValue = Convert.ToInt32(gvRFTDeductList.Rows[e.RowIndex].Cells[7].Value.ToString());
            cmbDivision.SelectedValue = gvRFTDeductList.Rows[e.RowIndex].Cells[8].Value.ToString();
            //cmbCategory.SelectedValue = Convert.ToInt32(gvRFTDeductList.Rows[e.RowIndex].Cells[4].Value.ToString());
            cmbYear.SelectedValue = Convert.ToInt32(gvRFTDeductList.Rows[e.RowIndex].Cells[4].Value.ToString());
            cmbMonth.SelectedValue = Convert.ToInt32(gvRFTDeductList.Rows[e.RowIndex].Cells[5].Value.ToString());
            this.txtEmpNo.Text = gvRFTDeductList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
            txtDeductAmount.Text = gvRFTDeductList.Rows[e.RowIndex].Cells[1].Value.ToString();

            txtRate1.Text = gvRFTDeductList.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtQuantity.Text = gvRFTDeductList.Rows[e.RowIndex].Cells[11].Value.ToString();


            cmdAdd.Enabled = false;
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            
                
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetRFTDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
            //gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            cmbDeductGroup.SelectedValue = -1;
            cmbDeduct.SelectedValue = -1;
            //cmbCategory.SelectedValue = -1;
            cmbDivision.SelectedValue = -1;
            cmbYear.SelectedValue = -1;
            cmbMonth.SelectedValue = -1;
            txtEmpName.Text = "";
            txtEmpNo.Clear();
            txtDeductAmount.Text = "";
            txtQuantity.Clear();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void AfterAdd()
        {
            try
            {
                gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetRFTDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
            //gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            txtEmpName.Text = "";
            txtEmpNo.Clear();
            txtDeductAmount.Text = "";
            txtQuantity.Clear();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            txtEmpNo.Focus();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(cmbDivision.SelectedValue.ToString());
                //cmbEmpNo.DisplayMember = "EmpNo";
                //cmbEmpNo.ValueMember = "EmpNo";
                //cmbEmpNo_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            { }
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(), "RFT");
                empList.Show();
            }
            else
            {                
                if (e.KeyChar == 13)
                {
                    if (txtEmpNo.Text.Trim() != "")
                    {
                        txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                    }
                    txtEmpNo_LeaveChanged();
                }
            }
        }
        private void txtEmpNo_LeaveChanged()
        {
            if (txtEmpNo.Text.Trim() != "")
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
                        txtQuantity.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                        txtEmpNo.Text = "";
                        txtEmpNo.Focus();
                    }
                }
            }
        }

        private void rbtnRice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRate.Focus();
            }
        }

        private void rbtnFlour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRate.Focus();
            }
        }

        private void rbtnTea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRate.Focus();
            }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQty.Focus();
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void cmbDeductGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDeduct.Focus();
            }
        }

        private void cmbDeduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRate1.Focus();
            }
        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbYear.Focus();
            }
            //cmbCategory.Focus();
        }

        //private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    cmbDeductGroup.Focus();
        //}

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
                //gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                cmbDeductGroup.Focus();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmbDeduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblDeduction.Text = DeductMaster.GetDeductionNameByID(Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
                gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetRFTDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
        }

        private void txtDeductAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvRFTDeductList.DataSource = RFTDeduct.ListRFTDeductions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
                lblTotal.Text = empDeduct.GetRFTDeductionTotal(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()), Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), cmbDivision.Text).ToString();
            }
            catch { }
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
                    txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                    txtEmpNo_LeaveChanged();
                }
                
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calculate();
            }
            catch { }
        }

        public void calculate()
        {
            try
            {
                Decimal rate = Convert.ToDecimal(txtRate1.Text.Trim());
                Decimal Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                Decimal Amount = Math.Round(rate * Quantity,2);
                txtDeductAmount.Text = Amount.ToString();
            }
            catch {}
            
        }

        private void txtRate1_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void txtRate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtRate1.Text.Trim() != "")
                {
                    txtEmpNo.Focus();
                }
                else
                    txtRate1.Focus();
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtQuantity.Text.Trim() != "")
            {
                if (e.KeyChar == 13)
                {
                    cmdAdd.PerformClick();
                }
            }
            else
                txtQuantity.Focus();
        }
      
    }
}