using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class JobAnalysisCodeMaster : Form
    {
        FTSPayRollBL.Job myJobM = new FTSPayRollBL.Job();
        public JobAnalysisCodeMaster()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtShortCode.Text))
                    MessageBox.Show("Short Code Cannot Be Empty");
                else if (String.IsNullOrEmpty(txtDescription.Text))
                    MessageBox.Show("Description Cannot Be Empty");
                else if (String.IsNullOrEmpty(txtSequenceNo.Text))
                    MessageBox.Show("Sequence No Cannot Be Empty");
                else
                {
                    myJobM.StrAnalyzeShortCode = txtShortCode.Text;
                    myJobM.StrDescription = txtDescription.Text;
                    myJobM.IntSequence = Convert.ToInt32(txtSequenceNo.Text);
                    myJobM.InsertAnalysisCode();
                    gvList.DataSource = myJobM.ListAnalyzeCodes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Failed, " + ex.Message);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtShortCode.Clear();
            txtDescription.Clear();
            txtSequenceNo.Clear();
            txtShortCode.Focus();
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            cmdAdd.Enabled = true;
            gvList.DataSource = myJobM.ListAnalyzeCodes();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRef.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtSequenceNo.Text=gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtShortCode.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblRef.Text))
                {
                    MessageBox.Show("Please Select A Analyze Code To Update");
                }
                else
                {
                    if (String.IsNullOrEmpty(txtShortCode.Text))
                        MessageBox.Show("Short Code Cannot Be Empty");
                    else if (String.IsNullOrEmpty(txtDescription.Text))
                        MessageBox.Show("Description Cannot Be Empty");
                    else if (String.IsNullOrEmpty(txtSequenceNo.Text))
                        MessageBox.Show("Sequence No Cannot Be Empty");
                    else
                    {
                        myJobM.IntAnalyseAutoKey = Convert.ToInt32(lblRef.Text);
                        myJobM.StrAnalyzeShortCode = txtShortCode.Text;
                        myJobM.StrDescription = txtDescription.Text;
                        myJobM.IntSequence = Convert.ToInt32(txtSequenceNo.Text);
                        myJobM.UpdateAnalysisCode();
                        cmdClear.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Failed, " + ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (String.IsNullOrEmpty(lblRef.Text))
                    {
                        MessageBox.Show("Please Select A Analyze Code To Delete");
                    }
                    else
                    {
                        myJobM.IntAnalyseAutoKey = Convert.ToInt32(lblRef.Text);
                        myJobM.DeleteAnalysisCode();
                        cmdClear.PerformClick();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Failed, " + ex.Message);
                }
            }
        }

        private void JobAnalysisCodeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myJobM.ListAnalyzeCodes();
            }
            catch { }
        }
    }
}