using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AdditionMaster : Form
    {
        public AdditionMaster()
        {
            InitializeComponent();
        }

        FTSPayRollBL.AdditionMaster myAddition = new FTSPayRollBL.AdditionMaster();

        private void AdditionMaster_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myAddition.ListAllAdditions();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (!myAddition.IsEPFPaybleAdditionAllowed() && chkEPFETFPayble.Checked)
            {
                MessageBox.Show("EPF Payble Additions Not Allowed!", "Warning", MessageBoxButtons.OK);
                chkEPFETFPayble.Checked = false;
                chkEPFETFPayble.Focus();
            }
            else if (txtName.Text == "")
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
                myAddition.StrAddition = txtName.Text;
                myAddition.StrShortName = txtShortName.Text;
                myAddition.IntPriority = Convert.ToInt32(txtPriority.Text);
                myAddition.StrAccCode = txtAccounyCode.Text;
                if (chkEPFETFPayble.Checked)
                {
                    myAddition.BoolEpfPayble = true;
                }
                else
                {
                    myAddition.BoolEpfPayble = false;
                }
                myAddition.InsertAddition();
                MessageBox.Show("Addition Master Added successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (!myAddition.IsEPFPaybleAdditionAllowed() && chkEPFETFPayble.Checked)
            {
                
                    MessageBox.Show("EPF Payble Additions Not Allowed!", "Warning", MessageBoxButtons.OK);
                    chkEPFETFPayble.Checked = false;
                    chkEPFETFPayble.Focus();
            }
            else if (txtName.Text == "")
            {
                MessageBox.Show("Name can not be empty");
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
                if (chkEPFETFPayble.Checked)
                {
                    myAddition.BoolEpfPayble = true;                    
                }
                else
                {
                    myAddition.BoolEpfPayble = false;
                }
                myAddition.StrAddition = txtName.Text;
                myAddition.StrShortName = txtShortName.Text;
                myAddition.IntPriority = Convert.ToInt32(txtPriority.Text);
                myAddition.StrAccCode = txtAccounyCode.Text;
                myAddition.UpdateAddition();
                cmdClear.PerformClick();
                MessageBox.Show("Addition Master Updated successfully");
                
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Name can not be empty");
                    txtName.Focus();
                }
                else
                {
                    myAddition.StrAddition = txtName.Text;
                    myAddition.DeleteAddition();
                    MessageBox.Show("Addition Master Deleted successfully");
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

            gvlist.DataSource = myAddition.ListAllAdditions();

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtShortName.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPriority.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtAccounyCode.Text = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            if(Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[7].Value.ToString())==true)
            {
                chkEPFETFPayble.Checked = true;
            }
            else
            {
                chkEPFETFPayble.Checked = false;
            }

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }
    }
}