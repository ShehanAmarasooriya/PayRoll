using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{    
    public partial class DeductionGroup : Form
    {
        FTSPayRollBL.DeductionMaster myDeductMaster = new FTSPayRollBL.DeductionMaster();
        public DeductionGroup()
        {
            InitializeComponent();
        }

        private void DeductionGroup_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myDeductMaster.ListDeductionGroups();

            chkLoan.Enabled = true;
            chkFunds.Enabled = true;
            chkFunds.Checked = false;
            chkLoan.Checked = false;
            
        }

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    DeductMaster.StrGroupName = txtDeductGroupName.Text;
        //    DeductMaster.StrGroupShortName = txtGroupShortName.Text;           
        //    DeductMaster.IntPriority = int.Parse(txtPriority.Text);
        //    DeductMaster.StrGroupType = cmbDeductionGroupType.SelectedValue.ToString();
        //    String status = DeductMaster.InsertDiductionGroup();
        //    if (status.Equals("ADDED"))
        //    {
        //        MessageBox.Show("Deduction Group Added Successfully!");
        //    }
        //    if(status.Equals("EXISTS"))
        //    {
        //        MessageBox.Show("Deduction Group Already Exists!");
        //    }
        //    if (status.Equals("ERROR"))
        //    {
        //        MessageBox.Show("Error!");
        //    }
        //    btnCancel.PerformClick();
            
        //}

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(lblRefNo.Text))
        //    {
        //        DeductMaster.IntDeductGroupId = int.Parse(lblRefNo.Text);
        //        DeductMaster.StrGroupName = txtDeductGroupName.Text;
        //        DeductMaster.StrGroupShortName= txtGroupShortName.Text;               
        //        DeductMaster.IntPriority = int.Parse(txtPriority.Text);
        //        DeductMaster.StrGroupType = cmbDeductionGroupType.SelectedText.ToString();
        //        String status = DeductMaster.UpdateDiductionGroup();
        //        if (status.Equals("UPDATED"))
        //        {
        //            MessageBox.Show("Deduction Group Updated Successfully!");
        //        }
        //        if (status.Equals("NOTEXISTS"))
        //        {
        //            MessageBox.Show("Deduction Group Not Exists!");
        //        }
        //        if (status.Equals("ERROR"))
        //        {
        //            MessageBox.Show("Error!");
        //        }
        //    }
        //    else
        //        MessageBox.Show("Please Select a Row Before Update.");
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //    {
        //        if (!String.IsNullOrEmpty(lblRefNo.Text))
        //        {
        //            DeductMaster.IntDeductGroupId = int.Parse(lblRefNo.Text);
        //            String status = DeductMaster.DeleteDiductionGroup();
        //            if (status.Equals("DELETED"))
        //            {
        //                btnCancel.PerformClick();
        //                MessageBox.Show("Deduction Group Deleted Successfully!");
        //            }
        //            if (status.Equals("NOTEXISTS"))
        //            {
        //                MessageBox.Show("Deduction Group Not Exists!");
        //            }
        //            if (status.Equals("ERROR"))
        //            {
        //                MessageBox.Show("Error!");
        //            }
        //        }
        //        else
        //            MessageBox.Show("Please Select a Row Before Delete.");
        //    }
        //}

        //private void gvDeductionGroups_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    lblRefNo.Text = gvDeductionGroups.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    txtDeductGroupName.Text = gvDeductionGroups.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    txtGroupShortName.Text = gvDeductionGroups.Rows[e.RowIndex].Cells[2].Value.ToString();
        //    txtPriority.Text = gvDeductionGroups.Rows[e.RowIndex].Cells[4].Value.ToString();
        //    cmbDeductionGroupType.SelectedValue = gvDeductionGroups.Rows[e.RowIndex].Cells[3].Value.ToString();
        //    btnAdd.Enabled = false;
        //    btnUpdate.Enabled = true;
        //    btnDelete.Enabled = true;
        //}

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    txtDeductGroupName.Clear();
        //    txtGroupShortName.Clear();
        //    txtPriority.Clear();
        //    cmbDeductionGroupType.SelectedText = "";
        //    btnAdd.Enabled = true;
        //    btnUpdate.Enabled = false;
        //    btnDelete.Enabled = false;

        //    gvDeductionGroups.DataSource = DeductMaster.ListDeductionGroups();
        //}

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtDeductGroupName.Text == "")
            {
                MessageBox.Show("Group name can not be empty");
                txtDeductGroupName.Focus();
            }
            else if (txtGroupShortName.Text == "")
            {
                MessageBox.Show("Group short name can not be empty");
                txtGroupShortName.Focus();
            }
            else if (txtPriority.Text == "")
            {
                MessageBox.Show("Priority can not be empty");
                txtPriority.Focus();
            }
            else
            {
                myDeductMaster.StrGroupName = txtDeductGroupName.Text;
                myDeductMaster.StrGroupShortName = txtGroupShortName.Text;
                myDeductMaster.IntPriority = Convert.ToInt32(txtPriority.Text);
                if (chkLoan.Checked)
                {
                    myDeductMaster.BitLoan = true;
                }
                else
                {
                    myDeductMaster.BitLoan = false;
                }
                if (chkFunds.Checked)
                {
                    myDeductMaster.BitFund = true;
                }
                else
                {
                    myDeductMaster.BitFund = false;
                }
                if (this.chkFS.Checked)
                {
                    myDeductMaster.BitRFT = true;
                }
                else
                {
                    myDeductMaster.BitRFT = false;
                }
                myDeductMaster.InsertDeductionGroup();
                MessageBox.Show("Deduction Group Added successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtDeductGroupName.Text == "")
            {
                MessageBox.Show("Group name can not be empty");
                txtDeductGroupName.Focus();
            }
            else if (txtGroupShortName.Text == "")
            {
                MessageBox.Show("Group short name can not be empty");
                txtGroupShortName.Focus();
            }
            else if (txtPriority.Text == "")
            {
                MessageBox.Show("Priority can not be empty");
                txtPriority.Focus();
            }
            else
            {
                myDeductMaster.StrGroupName = txtDeductGroupName.Text;
                myDeductMaster.StrGroupShortName = txtGroupShortName.Text;
                myDeductMaster.IntPriority = Convert.ToInt32(txtPriority.Text);
                if (chkLoan.Checked)
                {
                    myDeductMaster.BitLoan = true;
                }
                else
                {
                    myDeductMaster.BitLoan = false;
                }
                if (this.chkFunds.Checked)
                {
                    myDeductMaster.BitFund = true;
                }
                else
                {
                    myDeductMaster.BitFund = false;
                }
                if (this.chkFS.Checked)
                {
                    myDeductMaster.BitRFT = true;
                }
                else
                {
                    myDeductMaster.BitRFT = false;
                }
                myDeductMaster.UpdateDeductionGroup();
                MessageBox.Show("Deduction Group Updated successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtDeductGroupName.Text == "")
                {
                    MessageBox.Show("Group name can not be empty");
                    txtDeductGroupName.Focus();
                }
                else if (txtGroupShortName.Text == "")
                {
                    MessageBox.Show("Group short name can not be empty");
                    txtGroupShortName.Focus();
                }
                else
                {
                    myDeductMaster.StrGroupName = txtDeductGroupName.Text;
                    myDeductMaster.StrGroupShortName = txtGroupShortName.Text;
                    myDeductMaster.DeleteDeductionGroup();
                    MessageBox.Show("Deduction Group Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDeductGroupName.Text = "";
            txtGroupShortName.Text = "";
            txtPriority.Text = "";
            chkLoan.Checked = false;
            chkFunds.Checked = false;
            chkFS.Checked = false;


            gvlist.DataSource = myDeductMaster.ListDeductionGroups();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDeductGroupName.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtGroupShortName.Text =  gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPriority.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[3].Value.ToString()) == true)
            {
                chkLoan.Checked = true;
            }
            else
            {
                chkLoan.Checked = false;
            }
            if (Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[4].Value.ToString()) == true)
            {
                chkFunds.Checked = true;
            }
            else
            {
                chkFunds.Checked = false;
            }
            if (Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[8].Value.ToString()) == true)
            {
                chkFS.Checked = true;
            }
            else
            {
                chkFS.Checked = false;
            }
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void chkLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoan.Checked)
            {
                chkFunds.Enabled = false;
                chkFunds.Checked = false;
            }
            else
            {
                chkFunds.Enabled = true;
            }
        }

        private void chkFunds_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFunds.Checked)
            {
                chkLoan.Enabled = false;
                chkLoan.Checked = false;
            }
            else
            {
                chkLoan.Enabled = true;
            }
        }

        
        
    }
}