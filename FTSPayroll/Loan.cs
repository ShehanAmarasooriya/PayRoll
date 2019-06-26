using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class Loan : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        public Loan()
        {
            InitializeComponent();
        }

        private void Loan_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmpNo.DataSource = EmpMaster.ListAllEmployees();
            cmbEmpNo.DisplayMember = "EmpNo";
            cmbEmpNo.ValueMember = "EmpNo";

            cmbDeductionGroup.DataSource = DeductMaster.getLoanDeductionGroup();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductGroupCode";

            cmbCategory.DataSource = EmpCat.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            //cmbFromMonth.DataSource = YMonth.ListMonths();
            //cmbFromMonth.DisplayMember = "Month";
            //cmbFromMonth.ValueMember = "MId";

            //cmbToMonth.DataSource = YMonth.ListMonths();
            //cmbToMonth.DisplayMember = "Month";
            //cmbToMonth.ValueMember = "MId";

            gvlist.DataSource = LMaster.ListLoans();
            cmbDeductionGroup_SelectedIndexChanged(null, null);
            cmbEmpNo_SelectedIndexChanged(null, null);
            cmbDivision_SelectedIndexChanged(null, null);
            //cmbDeductionGroup_SelectedIndexChanged(null, null);
            //cmbDeductName_SelectedIndexChanged(null, null);
        }

        public void validateForm()
        {
 
        }
       

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpNo.SelectedIndex != -1)
            {
                if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                {
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
                }
            }
        }
        

        private void cmbDeductName_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    try{
        //        if(!String.IsNullOrEmpty(cmbDeductionGroup.SelectedValue.ToString()))
        //        {
        //            txtLoanName.Text=DeductMaster.GetLoanName(cmbDeductionGroup.SelectedValue.ToString());
        //        }
        //    }
        //    catch(Exception ex)
        //    {}
        }

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    //txtPrincipalAmount.Clear();
        //    //txtNoOfInstallments.Clear();
        //    //txtDirectPayments.Clear();
        //    //txtInstallmentAmount.Clear();
        //    //txtRecoveredInstallments.Clear();
        //    //txtRecoveredAmount.Clear();
        //    //txtBalanceAmount.Clear();
        //    //cmbDeductionGroup.SelectedIndex = -1;
        //    //txtLoanName.Clear();
        //    //cmbCategory.SelectedIndex = -1;
        //    //cmbEmpNo.SelectedIndex = -1;
        //    //txtEmpName.Clear();
        //    //cmbDivision.SelectedIndex = -1;
        //    //cmbFromMonth.SelectedIndex = -1;
        //    //cmbToMonth.SelectedIndex = -1;
        //    //txtFromYear.Clear();
        //    //txtToYear.Clear();

        //    //gvLoans.DataSource = LMaster.ListLoans();

        //    //btnAdd.Enabled = true;
        //    //btnUpdate.Enabled = false;
        //    //btnDelete.Enabled = false;



        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    String status = "";
        //    LMaster.IntDeductCode = int.Parse(cmbDeductionGroup.SelectedValue.ToString());
        //    LMaster.IntCategoryCode = int.Parse(cmbCategory.SelectedValue.ToString());
        //    LMaster.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
        //    LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
        //    LMaster.FlPrincipalAmount = float.Parse(txtPrincipalAmount.Text);
        //    LMaster.FlNoOfInstallments = float.Parse(txtNoOfInstallments.Text);
        //    LMaster.FlDirectAmount = float.Parse(txtDirectPayments.Text);
        //    LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
        //    LMaster.FlRecoveredInstallments = float.Parse(txtRecoveredInstallments.Text);
        //    LMaster.FlRecoveredAmount = float.Parse(txtRecoveredAmount.Text);
        //    LMaster.FlBalanceAmount = float.Parse(txtBalanceAmount.Text);
        //    LMaster.IntFromMonth = int.Parse(cmbFromMonth.SelectedValue.ToString());
        //    LMaster.IntToMonth=int.Parse(cmbToMonth.SelectedValue.ToString());
        //    LMaster.IntFromYear = int.Parse(txtFromYear.Text);
        //    LMaster.IntToYear = int.Parse(txtToYear.Text);
        //    LMaster.DtLoanDate=Convert.ToDateTime(dtpLoanDate.Value.Date.ToString());
        //    try
        //    {
        //        status = LMaster.InsertLoan();

        //        if (status.Equals("ADDED"))
        //        {
        //            MessageBox.Show("Loan Added Successfully!");
        //            btnCancel.PerformClick();
                  
        //        }
        //        else if (status.Equals("EXISTS"))
        //        {
        //            MessageBox.Show("Loan Already Exists.");
        //        }               
        //        else
        //            MessageBox.Show("Error.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error!, " + ex.Message);
        //    }
        //}

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    String status = "";
        //    if (!String.IsNullOrEmpty(lblRefNo.Text))
        //    {
        //        LMaster.IntLoanId = int.Parse(lblRefNo.Text);
        //        LMaster.IntDeductCode = int.Parse(cmbDeductionGroup.SelectedValue.ToString());
        //        LMaster.IntCategoryCode = int.Parse(cmbCategory.SelectedValue.ToString());
        //        LMaster.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
        //        LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
        //        LMaster.FlPrincipalAmount = float.Parse(txtPrincipalAmount.Text);
        //        LMaster.FlNoOfInstallments = float.Parse(txtNoOfInstallments.Text);
        //        LMaster.FlDirectAmount = float.Parse(txtDirectPayments.Text);
        //        LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
        //        LMaster.FlRecoveredInstallments = float.Parse(txtRecoveredInstallments.Text);
        //        LMaster.FlRecoveredAmount = float.Parse(txtRecoveredAmount.Text);
        //        LMaster.FlBalanceAmount = float.Parse(txtBalanceAmount.Text);
        //        LMaster.IntFromMonth = int.Parse(cmbFromMonth.SelectedValue.ToString());
        //        LMaster.IntToMonth = int.Parse(cmbToMonth.SelectedValue.ToString());
        //        LMaster.IntFromYear = int.Parse(txtFromYear.Text);
        //        LMaster.IntToYear = int.Parse(txtToYear.Text);
        //        LMaster.DtLoanDate = Convert.ToDateTime(dtpLoanDate.Value.Date.ToString());
        //        try
        //        {
        //            status = LMaster.UpdateLoan();

        //            if (status.Equals("UPDATED"))
        //            {
        //                MessageBox.Show("Loan Updated Successfully!");
        //                btnCancel.PerformClick();
        //            }
        //            else if (status.Equals("NOTEXISTS"))
        //            {
        //                MessageBox.Show("Loan Not Exists.");
        //            }
        //            else
        //                MessageBox.Show("Error.");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error!, " + ex.Message);
        //        }
        //    }
        //    else
        //        MessageBox.Show("Please Select Data Before Update");

        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{           
        //    if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //    {
        //        String status = "";
        //        if (!String.IsNullOrEmpty(lblRefNo.Text))
        //        {
        //            LMaster.IntLoanId = int.Parse(lblRefNo.Text);
        //            try
        //            {
        //                status = LMaster.DeleteLoan();

        //                if (status.Equals("DELETED"))
        //                {
        //                    MessageBox.Show("Loan Deleted Successfully!");
        //                    btnCancel.PerformClick();
        //                }
        //                else if (status.Equals("NOTEXISTS"))
        //                {
        //                    MessageBox.Show("Loan Not Exists.");
        //                }
        //                else
        //                    MessageBox.Show("Error.");
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error!, " + ex.Message);
        //            }
        //        }
        //        else
        //            MessageBox.Show("Please Select Data Before Delete");
        //    }

        //}

        //private void gvLoans_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    lblRefNo.Text = gvLoans.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    cmbDeductionGroup.SelectedValue = gvLoans.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    cmbDivision.SelectedValue = gvLoans.Rows[e.RowIndex].Cells[2].Value.ToString();
        //    cmbCategory.SelectedValue = gvLoans.Rows[e.RowIndex].Cells[3].Value.ToString();
        //    cmbEmpNo.SelectedValue = gvLoans.Rows[e.RowIndex].Cells[4].Value.ToString();
        //    txtPrincipalAmount.Text = gvLoans.Rows[e.RowIndex].Cells[6].Value.ToString();
        //    txtNoOfInstallments.Text = gvLoans.Rows[e.RowIndex].Cells[7].Value.ToString();
        //    dtpLoanDate.Value = Convert.ToDateTime(gvLoans.Rows[e.RowIndex].Cells[8].Value.ToString());
        //    txtRecoveredInstallments.Text = gvLoans.Rows[e.RowIndex].Cells[9].Value.ToString();
        //    txtRecoveredAmount.Text = gvLoans.Rows[e.RowIndex].Cells[10].Value.ToString();
        //    txtBalanceAmount.Text = gvLoans.Rows[e.RowIndex].Cells[11].Value.ToString();
        //    txtInstallmentAmount.Text = gvLoans.Rows[e.RowIndex].Cells[12].Value.ToString();
        //    txtDirectPayments.Text = gvLoans.Rows[e.RowIndex].Cells[13].Value.ToString();
        //    cmbFromMonth.SelectedValue = gvLoans.Rows[e.RowIndex].Cells[14].Value.ToString();
        //    txtFromYear.Text = gvLoans.Rows[e.RowIndex].Cells[15].Value.ToString();
        //    cmbToMonth.SelectedValue = gvLoans.Rows[e.RowIndex].Cells[16].Value.ToString();
        //    txtToYear.Text = gvLoans.Rows[e.RowIndex].Cells[17].Value.ToString();

        //    btnAdd.Enabled = false;
        //    btnUpdate.Enabled = true;
        //    btnDelete.Enabled = true;
           
        //}

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //cmbDeductions_SelectedIndexChanged(null, null);
                DeductMaster.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                cmbDeductions.DataSource = null;
                cmbDeductions.DataSource = DeductMaster.getDeduction();
                cmbDeductions.DisplayMember = "DeductionName";
                cmbDeductions.ValueMember = "DeductCode";
                cmbDeductions.SelectedIndex = 0;

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

            else if (txtPrincipalAmount.Text == "")
            {
                MessageBox.Show("Principal amount can not be empty");
                txtPrincipalAmount.Focus();
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

            else if (txtAccNo.Text == "")
            {
                MessageBox.Show("Installment Amount can not be empty");
                txtAccNo.Focus();
            }

            else
            {
                LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                LMaster.IntCategoryCode = Convert.ToInt32(cmbCategory.SelectedValue.ToString());
                LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
                LMaster.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
                LMaster.StrName = txtLoanName.Text;
                LMaster.FlPrincipalAmount = float.Parse(txtPrincipalAmount.Text);
                LMaster.FlNoOfInstallments = float.Parse(txtNoOfInstallments.Text);
                LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
                LMaster.AccountNo1 = txtAccNo.Text;
                LMaster.DtLoanDate = dtpLoanDate.Value.Date;
                LMaster.InsertLoan();
                MessageBox.Show("Loan Added Successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtLoanName.Text == "")
            {
                MessageBox.Show(" Loan name can not be empty");
                txtLoanName.Focus();
            }

            else if (txtPrincipalAmount.Text == "")
            {
                MessageBox.Show("Principal amount can not be empty");
                txtPrincipalAmount.Focus();
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

            else if (txtAccNo.Text == "")
            {
                MessageBox.Show("Installment Amount can not be empty");
                txtAccNo.Focus();
            }

            else
            {
                LMaster.IntLoanCode = Convert.ToInt32(lblRefNo.Text);
                LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                LMaster.IntCategoryCode = Convert.ToInt32(cmbCategory.SelectedValue.ToString());
                LMaster.StrDivision = cmbDivision.SelectedValue.ToString();
                LMaster.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
                LMaster.StrName = txtLoanName.Text;
                LMaster.FlPrincipalAmount = float.Parse(txtPrincipalAmount.Text);
                LMaster.FlNoOfInstallments = float.Parse(txtNoOfInstallments.Text);
                LMaster.FlInstallmentAmount = float.Parse(txtInstallmentAmount.Text);
                LMaster.AccountNo1 = txtAccNo.Text;
                LMaster.DtLoanDate = dtpLoanDate.Value.Date;
                LMaster.UpdateLoan();
                MessageBox.Show("Loan Updated Successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LMaster.IntLoanCode = Convert.ToInt32(lblRefNo.Text);
                LMaster.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                LMaster.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                LMaster.DeleteLoan();
                MessageBox.Show("Loan Deleted successfully");
                cmdClear.PerformClick();
            
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtAccNo.Text = "";
            txtLoanName.Text = "";
            txtNoOfInstallments.Text = "";
            txtPrincipalAmount.Text = "";
            txtEmpName.Text = "";
            txtInstallmentAmount.Text = "";

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            gvlist.DataSource = LMaster.ListLoans();
        }

        private void gvlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtFundCode.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            //txtFundName.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblRefNo.Text = gvlist.Rows[e.RowIndex].Cells[11].Value.ToString();
            cmbDeductionGroup.SelectedValue=gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbDeductions.SelectedValue=gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbCategory.SelectedValue=gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbDivision.SelectedValue=gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbEmpNo.SelectedValue=gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtLoanName.Text=gvlist.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtPrincipalAmount.Text=gvlist.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtNoOfInstallments.Text=gvlist.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtInstallmentAmount.Text=gvlist.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtAccNo.Text=gvlist.Rows[e.RowIndex].Cells[9].Value.ToString();
            dtpLoanDate.Value = Convert.ToDateTime(gvlist.Rows[e.RowIndex].Cells[10].Value.ToString());
            cmbEmpNo_SelectedIndexChanged(null, null);
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void cmbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DeductMaster.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
            //cmbDeductions.DataSource = null;
            //cmbDeductions.DataSource = DeductMaster.getDeduction();
            //cmbDeductions.DisplayMember = "DeductionName";
            //cmbDeductions.ValueMember = "DeductCode";
            //cmbDeductions.SelectedIndex = 0;
        }

        private void cmbEmpNo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();
                cmbEmpNo.DataSource = null;
                cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(DivisionID);
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
                cmbEmpNo_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        

       
    }
}