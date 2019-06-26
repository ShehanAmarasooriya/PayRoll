using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DeductionMaster : Form
    {
        FTSPayRollBL.DeductionMaster myDeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.AccountInformation AccInfo = new FTSPayRollBL.AccountInformation();
        public DeductionMaster()
        {
            InitializeComponent();
        }

        private void DeductionMaster_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myDeductMaster.ListAllDeductionMasters();

            cmbGroup.DataSource = myDeductMaster.getDeductionGroup();
            cmbGroup.DisplayMember = "DeductGroupName";
            cmbGroup.ValueMember = "DeductGroupCode";           
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="")
            {
                MessageBox.Show("Deduction name can not be empty");
                txtName.Focus();
            }
            else if (txtShortName.Text == "")
            {
                MessageBox.Show("Short name can not be empty");
                txtShortName.Focus();
            }
            else if (txtPriority.Text == "")
            {
                MessageBox.Show("Priority can not be empty");
                txtPriority.Focus();
            }
            else if (txtAccounyCode.Text == "")
            {
                MessageBox.Show("Account Code can not be empty");
                txtAccounyCode.Focus();
            }
            else
            {
                try
                {
                    myDeductMaster.IntDeductGroupId = Convert.ToInt32(cmbGroup.SelectedValue.ToString());
                    myDeductMaster.StrDeductionName = txtName.Text;
                    myDeductMaster.StrDeductionShortName = txtShortName.Text;
                    myDeductMaster.IntPriority = Convert.ToInt32(txtPriority.Text);
                    myDeductMaster.StrAccountHead = txtAccounyCode.Text;
                    if (chkFixed.Checked)
                    {
                        myDeductMaster.BoolFixed = true;
                    }
                    else
                    {
                        myDeductMaster.BoolFixed = false;
                    }
                    myDeductMaster.InsertDeductionMaster();
                    MessageBox.Show("Deduction Master Added successfully");
                    cmdClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Deduction Is Invalid.", "Error");
                }
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {

            if (txtName.Text == "")
            {
                MessageBox.Show("Deduction name can not be empty");
                txtName.Focus();
            }
            else if (txtShortName.Text == "")
            {
                MessageBox.Show("Short name can not be empty");
                txtShortName.Focus();
            }
            else if (txtPriority.Text == "")
            {
                MessageBox.Show("Priority can not be empty");
                txtPriority.Focus();
            }
            else if (txtAccounyCode.Text == "")
            {
                MessageBox.Show("Account Code can not be empty");
                txtAccounyCode.Focus();
            }
            else
            {
                myDeductMaster.IntDeductionId = Convert.ToInt32(lblRefNo.Text);
                myDeductMaster.IntDeductGroupId = Convert.ToInt32(cmbGroup.SelectedValue.ToString());
                myDeductMaster.StrDeductionName = txtName.Text;
                myDeductMaster.StrDeductionShortName = txtShortName.Text;
                myDeductMaster.IntPriority = Convert.ToInt32(txtPriority.Text);
                myDeductMaster.StrAccountHead = txtAccounyCode.Text;
                if (myDeductMaster.IsFixedEntriesAvailable(myDeductMaster.IntDeductionId))
                {
                    MessageBox.Show("Cannot Update,Related Fixed Entries Available");
                }
                else
                {
                    if (myDeductMaster.IsLoanEntriesAvailable(myDeductMaster.IntDeductionId))
                    {
                        MessageBox.Show("Cannot Update,Related Loan Entries Available");
                    }
                    else
                    {
                        myDeductMaster.UpdateDeductionMaster();
                        MessageBox.Show("Deduction Master Updated successfully");
                    }                    
                }                
                cmdClear.PerformClick();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Deduction name can not be empty");
                    txtName.Focus();
                }
                else
                {
                    myDeductMaster.IntDeductionId = Convert.ToInt32(lblRefNo.Text);
                    myDeductMaster.StrDeductionName = txtName.Text;
                    myDeductMaster.IntDeductGroupId = Convert.ToInt32(cmbGroup.SelectedValue.ToString());
                    myDeductMaster.StrGroupName = cmbGroup.Text;
                    if (myDeductMaster.IsFixedEntriesAvailable(myDeductMaster.IntDeductionId))
                    {
                        MessageBox.Show("Cannot Delete,Related Fixed Entries Available");
                    }
                    else
                    {
                        if (myDeductMaster.IsLoanEntriesAvailable(myDeductMaster.IntDeductionId))
                        {
                            MessageBox.Show("Cannot Delete,Related Loan Entries Available");
                        }
                        else
                        {
                            myDeductMaster.DeleteDeductionMaster();
                            MessageBox.Show("Deduction Master Deleted successfully");
                        }
                    }   
                    
                    
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPriority.Text = "";
            txtShortName.Text = "";
            txtAccounyCode.Text = "";

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

            try
            {
                gvlist.DataSource = myDeductMaster.ListAllDeductionMasters(Convert.ToInt32(cmbGroup.SelectedValue.ToString()));
            }
            catch (Exception)
            {
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRefNo.Text = gvlist.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtName.Text= gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtShortName.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbGroup.SelectedValue = Convert.ToInt32(gvlist.Rows[e.RowIndex].Cells[2].Value.ToString());
            txtPriority.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtAccounyCode.Text = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvlist.DataSource = myDeductMaster.ListAllDeductionMasters(Convert.ToInt32(cmbGroup.SelectedValue.ToString()));
            }
            catch (Exception)
            {
            }
        }

 }
    }
