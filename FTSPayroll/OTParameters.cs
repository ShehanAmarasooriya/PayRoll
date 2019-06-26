using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OTParameters : Form
    {
        public OTParameters()
        {
            InitializeComponent();
        }

        FTSPayRollBL.OTParameters myOTParameters = new FTSPayRollBL.OTParameters();

        private void OTParameters_Load(object sender, EventArgs e)
        {
            
            gvlist.DataSource = myOTParameters.ListAllOTParameters();
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOTType.Text == "")
                {
                    MessageBox.Show("OT Type can not be empty");
                    txtOTType.Focus();
                }

                else if (txtOtFactor.Text == "")
                {
                    MessageBox.Show("OT Factor can not be empty");
                    txtOtFactor.Focus();
                }

                else
                {
                    myOTParameters.StrOTType = txtOTType.Text;
                    myOTParameters.DecOTFactor = Convert.ToDecimal(txtOtFactor.Text);
                    myOTParameters.InsertOTParameter();
                    MessageBox.Show("OT Factor Added Successfully");
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

                if (txtOTType.Text == "")
                {
                    MessageBox.Show("OT Type can not be empty");
                    txtOTType.Focus();
                }

                else if (txtOtFactor.Text == "")
                {
                    MessageBox.Show("OT Factor can not be empty");
                    txtOtFactor.Focus();
                }
                else
                {
                    myOTParameters.StrOTType = txtOTType.Text;
                    myOTParameters.DecOTFactor = Convert.ToDecimal(txtOtFactor.Text);
                    myOTParameters.UpdateOTParameter();
                    MessageBox.Show("OT Factor Updated Successfully");
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
                if (txtOTType.Text == "")
                {
                    MessageBox.Show("OT Type can not be empty");
                    txtOTType.Focus();
                }

                else
                {
                    myOTParameters.StrOTType = txtOTType.Text;
                    myOTParameters.DeleteOTParameter();
                    MessageBox.Show("OT Factor Deleted Successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtOtFactor.Text = "";
            txtOTType.Text = "";

            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            cmdAdd.Enabled = true;

            gvlist.DataSource = myOTParameters.ListAllOTParameters();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOTType.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtOtFactor.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
           
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}