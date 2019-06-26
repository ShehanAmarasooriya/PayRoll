using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeFund : Form
    {
        public EmployeeFund()
        {
            InitializeComponent();
        }
        FTSPayRollBL.EmployeeFund myEmpFunds = new FTSPayRollBL.EmployeeFund();
        private void EmployeeFund_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myEmpFunds.ListAllFunds();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFundCode.Text == "")
                {
                    MessageBox.Show("Fund Code can not be empty");
                    txtFundCode.Focus();
                }

                else if (txtFundName.Text == "")
                {
                    MessageBox.Show("Fund Name can not be empty");
                    txtFundName.Focus();
                }
                else
                {
                    myEmpFunds.StrCode = txtFundCode.Text;
                    myEmpFunds.StrName = txtFundName.Text;
                    myEmpFunds.InsertEmpFund();
                    MessageBox.Show("Fund Added successfully");
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..!");
            }

        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFundCode.Text == "")
                {
                    MessageBox.Show("Fund Code can not be empty");
                    txtFundCode.Focus();
                }

                else if (txtFundName.Text == "")
                {
                    MessageBox.Show("Fund Name can not be empty");
                    txtFundName.Focus();
                }
                else
                {
                    myEmpFunds.StrCode = txtFundCode.Text;
                    myEmpFunds.StrName = txtFundName.Text;
                    myEmpFunds.UpdateEmpFund();
                    MessageBox.Show("Fund Updated successfully");
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..!");
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtFundCode.Text == "")
                {
                    MessageBox.Show("Fund Code can not be empty");
                    txtFundCode.Focus();
                }
                else
                {
                    myEmpFunds.StrCode = txtFundCode.Text;
                    myEmpFunds.DeleteEmpFund();
                    MessageBox.Show("Fund Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtFundCode.Text = "";
            txtFundName.Text = "";
            gvlist.DataSource = myEmpFunds.ListAllFunds();
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
            txtFundCode.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtFundName.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }
    }
}