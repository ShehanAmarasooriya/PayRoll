using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class ExtraRates : Form
    {
        public ExtraRates()
        {
            InitializeComponent();
        }

        FTSPayRollBL.ExtraRates myExtraRates = new FTSPayRollBL.ExtraRates();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();

        private void ExtraRates_Load(object sender, EventArgs e)
        {
            
            cmbWorkcode.DataSource = myExtraRates.ListJobs();
            cmbWorkcode.DisplayMember = "JobShortName";
            cmbWorkcode.ValueMember = "JobShortName";

            gvlist.DataSource = myExtraRates.ListAllExtraRates();

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExtrarate.Text == "")
                {
                    MessageBox.Show("Extra Rate can not be empty");
                    txtExtrarate.Focus();
                }
                else
                {
                    myExtraRates.StrWorkCode = cmbWorkcode.SelectedValue.ToString();
                    myExtraRates.DecExtraRate = Convert.ToDecimal(txtExtrarate.Text);
                    myExtraRates.InsertExtraRate();
                    MessageBox.Show("Extra Rate Added Successfully");
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
                if (txtExtrarate.Text == "")
                {
                    MessageBox.Show("Extra Rate can not be empty");
                    txtExtrarate.Focus();
                }
                else
                {
                    myExtraRates.StrWorkCode = cmbWorkcode.SelectedValue.ToString();
                    myExtraRates.DecExtraRate = Convert.ToDecimal(txtExtrarate.Text);
                    myExtraRates.UpdateExtraRate();
                    MessageBox.Show("Extra Rate Updated Successfully");
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
                if (txtExtrarate.Text == "")
                {
                    MessageBox.Show("Extra Rate can not be empty");
                    txtExtrarate.Focus();
                }
                else
                {
                    myExtraRates.StrWorkCode = cmbWorkcode.SelectedValue.ToString();
                    myExtraRates.DeleteExtraRate();
                    MessageBox.Show("Extra Rate Deleted Successfully");
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
            txtExtrarate.Text = "";
            gvlist.DataSource = myExtraRates.ListAllExtraRates();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbWorkcode.SelectedValue = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtExtrarate.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void cmbWorkcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblJobName.Text = "";
            lblJobName.Text = Job1.JobNameByShortName(cmbWorkcode.SelectedValue.ToString());
        }  
    }
}