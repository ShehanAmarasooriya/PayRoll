using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class HoldLoan : Form
    {
        public HoldLoan()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.HoldLoan myHoldloan = new FTSPayRollBL.HoldLoan();
        FTSPayRollBL.LoanMaster myLoan = new FTSPayRollBL.LoanMaster();

        private void HoldLoan_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();

            cmbDeductionGroup.DataSource = DeductMaster.getLoanDeductionGroup();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductGroupCode";

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDeductionGroup_SelectedIndexChanged(null, null);
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

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEmpNo.SelectedIndex != -1)
                {
                    if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                    {
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
                        myHoldloan.StrDivisionId = cmbDivision.SelectedValue.ToString();
                        myHoldloan.StrEmpNo1 = cmbEmpNo.SelectedValue.ToString();
                        gvlist.DataSource = myHoldloan.ListHoldByEmpID();
                    }
                }
                else
                {
                    cmbEmpNo.SelectedValue = "N/A";
                    txtEmpName.Text = "N/A";
                }
            }

            catch { }
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DeductMaster.IntDeductGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                cmbDeductions.DataSource = null;
                cmbDeductions.DataSource = DeductMaster.getDeduction();
                cmbDeductions.DisplayMember = "DeductionName";
                cmbDeductions.ValueMember = "DeductCode";
                cmbDeductions_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            try
            {
                myHoldloan.StrDivisionId = cmbDivision.SelectedValue.ToString();
                myHoldloan.StrEmpNo1 = cmbEmpNo.SelectedValue.ToString();
                myHoldloan.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myHoldloan.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myHoldloan.IntDeductionGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                myHoldloan.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                myHoldloan.BitHoldLoan = true;
                try
                {
                    myHoldloan.InsertLoanHoldDeduction();
                    MessageBox.Show("Loan Hold Updated successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, "+ex.Message);
                }
                gvlist.DataSource = myHoldloan.ListHoldByEmpID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..", ex.Message.ToString());
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                myHoldloan.StrDivisionId = cmbDivision.SelectedValue.ToString();
                myHoldloan.StrEmpNo1 = cmbEmpNo.SelectedValue.ToString();
                myHoldloan.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myHoldloan.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myHoldloan.IntDeductionGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                myHoldloan.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                myHoldloan.BitHoldLoan = false;


                myHoldloan.UpdateLoanHoldDeduction();
                MessageBox.Show("Loan Hold Canceled successfully");
                gvlist.DataSource = myHoldloan.ListHoldByEmpID();
                cmdDelete.Enabled = true;
                btnHold.Enabled = false;
                btnClose.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..", ex.Message.ToString());
            }
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDivision.SelectedValue = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbEmpNo.SelectedValue = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbYear.SelectedValue = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbMonth.SelectedValue = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbDeductionGroup.SelectedValue = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[7].Value.ToString()) == true)
            {
                cmdDelete.Enabled = true;
                cmdClear.Enabled = true;
                btnClose.Enabled = true;
                btnHold.Enabled = false;

            }
            else
            {
                cmdDelete.Enabled = true;
                cmdClear.Enabled = true;
                btnClose.Enabled = true;
                btnHold.Enabled = false;
            }
            btnHold.Enabled = false;
            cmdClear.Enabled = true;
            btnClose.Enabled = true;
            cmdDelete.Enabled = true;

        }

        private void cmbDeductions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                myLoan.IntDeductionGroup = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                myLoan.IntDeductCode = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                myLoan.StrEmpNo = cmbEmpNo.SelectedValue.ToString();               
            }
            catch { }
        }

        private void txtEmpName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";
            cmdDelete.Enabled = true;
            btnHold.Enabled = true;
            btnClose.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                myHoldloan.StrDivisionId = cmbDivision.SelectedValue.ToString();
                myHoldloan.StrEmpNo1 = cmbEmpNo.SelectedValue.ToString();
                myHoldloan.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myHoldloan.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myHoldloan.IntDeductionGroupId = Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString());
                myHoldloan.IntDeduction = Convert.ToInt32(cmbDeductions.SelectedValue.ToString());
                try
                {
                    myHoldloan.DeleteLoanHoldDeduction();
                    MessageBox.Show("Loan Deleted successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
                gvlist.DataSource = myHoldloan.ListHoldByEmpID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..", ex.Message.ToString());
            }
        }

        private void gvlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDivision.SelectedValue = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbEmpNo.SelectedValue = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbYear.SelectedValue = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbMonth.SelectedValue = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbDeductionGroup.SelectedValue = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbDeductions.SelectedValue = gvlist.Rows[e.RowIndex].Cells[5].Value.ToString();
            if (Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[7].Value.ToString()) == true)
            {
                cmdDelete.Enabled = true;
                cmdClear.Enabled = true;
                btnClose.Enabled = true;
                btnHold.Enabled = false;
            }
            else
            {
                cmdDelete.Enabled = true;
                cmdClear.Enabled = true;
                btnClose.Enabled = true;
                btnHold.Enabled = false;
            }
            btnHold.Enabled = false;
            cmdClear.Enabled = true;
            btnClose.Enabled = true;
            cmdDelete.Enabled = true;
        }
    }
}