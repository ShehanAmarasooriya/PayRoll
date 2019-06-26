using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FixedParameters : Form
    {
        public FixedParameters()
        {
            InitializeComponent();
        }
        FTSPayRollBL.FixedParameters myParameters = new FTSPayRollBL.FixedParameters();
        private void FixedParameters_Load(object sender, EventArgs e)
        {
           gvlist.DataSource = myParameters.ListAllParameters();
           cmbName.DataSource = myParameters.getParameters();
           cmbName.DisplayMember = "Name";
           cmbName.ValueMember = "ID";
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to update this ...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (txtAmount.Text == "")
                    {
                        MessageBox.Show("Please enter amount to update");
                        txtAmount.Focus();
                    }

                    else
                    {
                        myParameters.IntName = Convert.ToInt32(cmbName.SelectedValue.ToString());
                        myParameters.DecAmount = Convert.ToDecimal(txtAmount.Text);
                        myParameters.UpdateParameters();
                        MessageBox.Show("Amount updated successfully");
                        cmdClear.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..!");
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            gvlist.DataSource = myParameters.ListAllParameters();

        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbName.SelectedValue = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAmount.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

       
    }
}