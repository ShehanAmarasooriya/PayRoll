using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class Additions : Form
    {
        public Additions()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Additions myAdditions = new FTSPayRollBL.Additions();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        

        private void Additions_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            try
            {
                cmbYear.SelectedValue = YMonth.getLastYearID();
                cmbMonth.SelectedValue = YMonth.getLastMonthID();
            }
            catch { }

            cmbAddition.DataSource = myAdditions.getAddition();
            cmbAddition.DisplayMember = "AdditionName";
            cmbAddition.ValueMember = "AdditionId";

            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            }
            catch { }


            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            }
            catch { }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtAddAmount.Text == "")
            {
                MessageBox.Show("Add amount can not be empty");
                txtAddAmount.Focus();
            }
            else
            {
                try
                {
                    myAdditions.StrEmpno = txtEmpNo.Text;
                    myAdditions.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    myAdditions.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    myAdditions.AdditionId = Convert.ToInt32(cmbAddition.SelectedValue.ToString());
                    myAdditions.DecAmount = Convert.ToDecimal(txtAddAmount.Text);
                    myAdditions.StrDivision = cmbDivision.SelectedValue.ToString();
                    myAdditions.StrUserId = FTSPayRollBL.User.StrUserName;
                    myAdditions.InsertAdditions();
                    cmdClear.PerformClick();
                    MessageBox.Show("Data Added successfully");
                }
                catch (Exception ex)
                { 
                    MessageBox.Show("Error, "+ex.Message); 
                }
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtAddAmount.Text == "")
            {
                MessageBox.Show("Add amount can not be empty");
                txtAddAmount.Focus();
            }
            else
            {
                if (!String.IsNullOrEmpty(lblRef.Text))
                {
                    try
                    {
                        myAdditions.IntAdditionRefId = Convert.ToInt32(lblRef.Text);
                        myAdditions.StrEmpno = txtEmpNo.Text;
                        myAdditions.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                        myAdditions.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                        myAdditions.AdditionId = Convert.ToInt32(cmbAddition.SelectedValue.ToString());
                        myAdditions.DecAmount = Convert.ToDecimal(txtAddAmount.Text);
                        myAdditions.StrDivision = cmbDivision.SelectedValue.ToString();
                        myAdditions.StrUserId = FTSPayRollBL.User.StrUserName;
                        myAdditions.UpdateAdditions();
                        cmdClear.PerformClick();
                        MessageBox.Show("Data Updated successfully");
                     }
                    catch (Exception ex)
                    { 
                        MessageBox.Show("Error, "+ex.Message); 
                    }

                }
                else
                {
                    MessageBox.Show("Please select Data Before Update Or Delete.");
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!String.IsNullOrEmpty(lblRef.Text))
                {
                    try
                    {
                        myAdditions.IntAdditionRefId = Convert.ToInt32(lblRef.Text);
                        myAdditions.DeleteAdditions();
                        cmdClear.PerformClick();
                        MessageBox.Show("Data Deleted successfully");
                    } 
                    catch (Exception ex)
                    { 
                        MessageBox.Show("Error, "+ex.Message); 
                    }
                }
                else 
                {
                    MessageBox.Show("Please select Data Before Update Or Delete.");                
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtAddAmount.Text = "";
            txtEmpNo.Clear();
            txtEmpName.Clear();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            }
            catch { }
            cmbAddition.Focus();
        }

        private void AfterAdd()
        {
            txtAddAmount.Clear();
            txtEmpNo.Clear();
            txtEmpName.Clear();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            }
            catch { }
            txtEmpNo.Focus();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //cmbDeductions.SelectedValue = gvFixedDeductions.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmpName.Text = "";
            lblRef.Text = gvList.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtEmpNo.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAddAmount.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbYear.SelectedValue = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbMonth.SelectedValue = gvList.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbDivision.SelectedValue = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
            cmbAddition.SelectedValue = gvList.Rows[e.RowIndex].Cells[6].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            }
            catch { }
        }

        private void cmbAddition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
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
                    txtAddAmount.Focus();
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
                txtEmpNo_LeaveChanged();
            }
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList(this,cmbDivision.SelectedValue.ToString());
                    empList.Show();
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

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
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
                cmbAddition.Focus();
            }
        }

        private void cmbAddition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }
        

        private void txtAddAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }

        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            EmployeeList empList = new EmployeeList();
            empList.Show();
        }

        private void cmbMonth_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myAdditions.ListEmpAdditions(cmbDivision.Text, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            }
            catch { }
        }

        private void cmbAddition_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                lblAddition.Text = myAdditions.GetAdditionNameByID(Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            }
            catch 
            {
            }
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
                cmbMonth_SelectedIndexChanged_1(null, null);
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