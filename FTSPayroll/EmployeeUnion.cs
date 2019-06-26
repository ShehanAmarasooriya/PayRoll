using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeUnion : Form
    {
        public EmployeeUnion()
        {
            InitializeComponent();
        }
        FTSPayRollBL.EmployeeUnion myEmpUnion = new FTSPayRollBL.EmployeeUnion();

        private void EmployeeUnion_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myEmpUnion.ListAllUnion();
            
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtUnionCode.Text == "")
            {
                MessageBox.Show("Union Code can not be empty");
                txtUnionCode.Focus();
            }
            else if (txtUnionName.Text == "")
            {
                MessageBox.Show("Union Name can not be empty");
                txtUnionName.Focus();
            }

            else
            {
                try
                {
                    myEmpUnion.StrUnionID = txtUnionCode.Text;
                    myEmpUnion.StrUnionName = txtUnionName.Text;
                    myEmpUnion.FlMonthlyAmt = float.Parse(txtMonthlyAmt.Text);
                    myEmpUnion.InsertUnion();
                    MessageBox.Show("Employee Union Added successfully");
                    cmdClear.PerformClick();
                }
                catch (Exception ex)
                { }
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUnionCode.Text == "")
                {
                    MessageBox.Show("Union Code can not be empty");
                    txtUnionCode.Focus();
                }
                else if (txtUnionName.Text == "")
                {
                    MessageBox.Show("Union Name can not be empty");
                    txtUnionName.Focus();
                }

                else
                {
                    myEmpUnion.StrUnionID = txtUnionCode.Text;
                    myEmpUnion.StrUnionName = txtUnionName.Text;
                    myEmpUnion.FlMonthlyAmt = float.Parse(txtMonthlyAmt.Text);
                    myEmpUnion.UpdateUnion();
                    MessageBox.Show("Employee Union Updated successfully");
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
                if (txtUnionCode.Text == "")
                {
                    MessageBox.Show("Union Code can not be empty");
                    txtUnionCode.Focus();
                }
                else
                {
                    myEmpUnion.StrUnionID = txtUnionCode.Text;
                    myEmpUnion.DeleteUnion();
                    MessageBox.Show("Employee Union Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            gvlist.DataSource = myEmpUnion.ListAllUnion();
            txtUnionName.Text = "";
            txtUnionCode.Text = "";
            txtMonthlyAmt.Text = "";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUnionCode.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtUnionName.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMonthlyAmt.Text = gvlist.Rows[e.RowIndex].Cells[5].Value.ToString();
            
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }
    }
}