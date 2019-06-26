using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeUnionMasters : Form
    {
        public EmployeeUnionMasters()
        {
            InitializeComponent();
        }
        FTSPayRollBL.EmployeeUnion myUnion = new FTSPayRollBL.EmployeeUnion();
        FTSPayRollBL.DeductionMaster myDeduction = new FTSPayRollBL.DeductionMaster();

        private void EmployeeUnionMasters_Load(object sender, EventArgs e)
        {
            cmbDeductionGroup.DataSource = myDeduction.ListUnionDeductionGroups();
            cmbDeductionGroup.DisplayMember = "DeductGroupName";
            cmbDeductionGroup.ValueMember = "DeductionGroupId";

            cmbDeductionGroup_SelectedIndexChanged(null, null);

            dtGrid.DataSource = myUnion.ListAllUnionMaster();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            String strStatus = "";
            if (txtAmount.Text == "")
            {
                MessageBox.Show("Union Amount can't be empty..!");
            }
            else
            {
                myUnion.StrDeductionGroup = cmbDeductionGroup.Text;
                myUnion.StrDeduction = Deduction.Text;
                myUnion.FlMonthlyAmt = float.Parse(txtAmount.Text);

                strStatus = myUnion.InsertUnionMaster();
                if(strStatus.ToUpper().Equals("OK"))
                     MessageBox.Show("Union Amount added Successfully..!");
                 else if (strStatus.ToUpper().Equals("EXISTS"))
                     MessageBox.Show("Already Exists..!");
                else
                     MessageBox.Show("Something Went wrong..!");
                    
                cmdClear.PerformClick();
            }

        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtAmount.Clear();
            dtGrid.DataSource = myUnion.ListAllUnionMaster();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void Deduction_SelectedIndexChanged(object sender, EventArgs e)
        {
          //
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                MessageBox.Show("Union Amount can't be empty..!");
            }
            else
            {
                myUnion.StrDeductionGroup = cmbDeductionGroup.Text;
                myUnion.StrDeduction = Deduction.Text;
                myUnion.FlMonthlyAmt = float.Parse(txtAmount.Text);

                myUnion.UpdateUnionMaster();
                MessageBox.Show("Updated Successfully..!");
                cmdClear.PerformClick();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (cmbDeductionGroup.Text == "")
                {
                    MessageBox.Show("Deduction Group Code can not be empty");
                    cmbDeductionGroup.Focus();
                }
                else
                {
                    myUnion.StrDeductionGroup = cmbDeductionGroup.Text;
                    myUnion.StrDeduction = Deduction.Text;
                    myUnion.DeleteUnionMaster();
                    MessageBox.Show("Employee Union Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Deduction.DataSource = myDeduction.ListDeduction(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                Deduction.DisplayMember = "DeductShortName";
                Deduction.ValueMember = "DeductShortName";
            }
            catch{}

        }

        private void dtGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDeductionGroup.Text = dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            Deduction.Text = dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAmount.Text = dtGrid.Rows[e.RowIndex].Cells[2].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }
    }
}