using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
   
    public partial class JobMaster : Form
    {
        FTSPayRollBL.Job myJobM =new FTSPayRollBL.Job();
        FTSPayRollBL.JobGroup myJobGroup = new FTSPayRollBL.JobGroup();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        public JobMaster()
        {
            InitializeComponent();
        }
        //private String getType()
        //{
        //    String JobType = "NotSelected";

        //    //if (rbtnAllowance.Checked)
        //    //{
        //    //    JobType = rbtnAllowance.Text;
        //    //}
        //    //else if (rbtnBenifit.Checked)
        //    //{
        //    //    JobType = rbtnBenifit.Text;
        //    //}
        //    //else if (rbtnJob.Checked)
        //    //{
        //    //    JobType = rbtnJob.Text;
        //    //}
        //    //else if (rbtnOther.Checked)
        //    //{
        //    //    JobType = rbtnOther.Text;
        //    //}
        //    //else
        //    //{
        //    //    JobType = "NotSelected";
        //    //}
        //    return JobType;
        //}
        //private void SetType(String type)
        //{
        //    //rbtnJob.Checked = false;
        //    //rbtnAllowance.Checked = false;
        //    //rbtnBenifit.Checked = false;
        //    //rbtnOther.Checked = false;
        //    //if (type.Equals("Job"))
        //    //{
        //    //    rbtnJob.Checked = true;
        //    //}
        //    //else if(type.Equals("Allowance"))
        //    //{
        //    //    rbtnAllowance.Checked=true;
        //    //}
        //    //else if (type.Equals("Benifit"))
        //    //{
        //    //    rbtnBenifit.Checked=true;
        //    //}
        //    //else if (type.Equals("Other"))
        //    //{
        //    //    rbtnOther.Checked=true;
        //    //}
           

 
        //}
        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        JobM.IntGroupID = Convert.ToInt32(cmbJobGroup.SelectedValue.ToString());
        //        JobM.StrJobDesc = txtDescription.Text;
        //        JobM.StrShortName = txtShortName.Text;
        //        JobM.StrJobType = getType();
        //        JobM.InsertJob();

        //        MessageBox.Show("Job Created successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void JobMaster_Load(object sender, EventArgs e)
        //{
        //    cmbJobGroup.DataSource = jGroup.ListGroups();
        //    cmbJobGroup.DisplayMember = "GroupName";
        //    cmbJobGroup.ValueMember = "GroupID";

        //    gvJobs.DataSource = JobM.ListJobs();
         

        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            JobM.IntJobID = Convert.ToInt32(lblRefNo.Text);
        //            JobM.DeleteJob();
        //            MessageBox.Show("Job Deleted successfully!");
        //            btnCancel.PerformClick();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void gvJobs_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{          
        //    lblRefNo.Text = gvJobs.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    cmbJobGroup.SelectedValue =int.Parse(gvJobs.Rows[e.RowIndex].Cells[1].Value.ToString());
        //    txtDescription.Text = gvJobs.Rows[e.RowIndex].Cells[2].Value.ToString();
        //    txtShortName.Text = gvJobs.Rows[e.RowIndex].Cells[3].Value.ToString();
        //    SetType( gvJobs.Rows[e.RowIndex].Cells[4].Value.ToString());

        //    btnAdd.Enabled = false;
        //    btnUpdate.Enabled = true;
        //    btnDelete.Enabled = true;

        //    cmbJobGroup.Focus();
        //}

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    //rbtnJob.Checked = false;
        //    //rbtnAllowance.Checked = false;
        //    //rbtnBenifit.Checked = false;
        //    //rbtnOther.Checked = false;

        //    txtDescription.Clear();
        //    txtShortName.Clear();

        //    gvJobs.DataSource = JobM.ListJobs();

        //    btnAdd.Enabled = true;
        //    btnUpdate.Enabled = false;
        //    btnDelete.Enabled = false;
        //}

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        JobM.IntJobID = Convert.ToInt32(lblRefNo.Text);
        //        JobM.IntGroupID = Convert.ToInt32(cmbJobGroup.SelectedValue.ToString());
        //        JobM.StrJobDesc = txtDescription.Text;
        //        JobM.StrShortName = txtShortName.Text;
        //        JobM.StrJobType = getType();
        //        JobM.UpdateJob();
        //        btnCancel.PerformClick();

        //        MessageBox.Show("Job Updated successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void JobMaster_Load(object sender, EventArgs e)
        {
            
            cmbJobGroup.DataSource = myJobGroup.ListGroupName();
            cmbJobGroup.DisplayMember = "GroupName";
            cmbJobGroup.ValueMember = "GroupID";

            cmbExpenditureType.SelectedIndex = 0;
            cmbJobType.SelectedIndex = 0;

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";
            
           // gvlist.DataSource = myJobM.ListJobs();
            cmbAnalyzeCode.DataSource = myJobM.ListAnalyzeCodes();
            cmbAnalyzeCode.DisplayMember = "Description";
            cmbAnalyzeCode.ValueMember = "ShortCode";
            gvlist.DataSource = myJobM.ListJobs();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtShortName.Text == "")
                {
                    MessageBox.Show("Short Name can not be empty");
                    txtShortName.Focus();
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Full Name can not be empty");
                    txtDescription.Focus();
                }
                else if (String.IsNullOrEmpty(cmbAnalyzeCode.Text))
                {
                    MessageBox.Show("Analyze Code Invalid");
                }
                else
                {
                    myJobM.IntGroupID = Convert.ToInt32(cmbJobGroup.SelectedValue.ToString());
                    myJobM.StrShortName = txtShortName.Text;
                    myJobM.StrJobDesc = txtDescription.Text;
                    myJobM.StrJobType = cmbJobType.Text;
                    myJobM.StrAccType = cmbExpenditureType.Text;
                    myJobM.StrAnalyzeShortCode = cmbAnalyzeCode.SelectedValue.ToString();
                    myJobM.IntCropType = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                    myJobM.InsertJob();
                    MessageBox.Show("Job Added successfully");
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
                if (txtShortName.Text == "")
                {
                    MessageBox.Show("Short Name can not be empty");
                    txtShortName.Focus();
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Full Name can not be empty");
                    txtDescription.Focus();
                }
                else if (String.IsNullOrEmpty(cmbAnalyzeCode.Text))
                {
                    MessageBox.Show("Analyze Code Invalid");
                }
                else
                {

                    myJobM.IntGroupID = Convert.ToInt32(cmbJobGroup.SelectedValue.ToString());


                    myJobM.StrShortName = txtShortName.Text;
                    myJobM.StrJobDesc = txtDescription.Text;
                    myJobM.StrJobType = cmbJobType.Text;
                    myJobM.StrAccType = cmbExpenditureType.Text;
                    myJobM.StrAnalyzeShortCode = cmbAnalyzeCode.SelectedValue.ToString();
                    myJobM.IntCropType = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                    myJobM.UpdateJob();
                    MessageBox.Show("Job Updated successfully");
                    cmdClear.PerformClick();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..!",ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtShortName.Text == "")
                {
                    MessageBox.Show("Short Name can not be empty");
                    txtShortName.Focus();
                }
                else
                {
                    myJobM.StrShortName = txtShortName.Text;
                    myJobM.DeleteJob();
                    MessageBox.Show("Job Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDescription.Text = "";
            txtShortName.Text = "";

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

            gvlist.DataSource = myJobM.ListJobs();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbAnalyzeCode.SelectedIndex = -1;
            txtDescription.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtShortName.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbJobType.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbExpenditureType.Text = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbAnalyzeCode.SelectedValue = gvlist.Rows[e.RowIndex].Cells[8].Value.ToString();
            myJobM.IntGroupID = Convert.ToInt32(gvlist.Rows[e.RowIndex].Cells[0].Value.ToString());
            cmbJobGroup.Text = myJobM.getGroupname().Rows[0][0].ToString();
            cmbCropType.SelectedValue = Convert.ToInt32(gvlist.Rows[e.RowIndex].Cells[9].Value.ToString());

            //cmbJobGroup.DisplayMember = "JobGroup";
            //cmbJobGroup.ValueMember = "JobGroupID";

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            JobAnalysisCodeMaster JobAnalysisCode = new JobAnalysisCodeMaster();
            JobAnalysisCode.Show();
        }

        private void cmbJobGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvlist.DataSource = myJobM.ListJobs(Convert.ToInt32(cmbJobGroup.SelectedValue.ToString()));
            }
            catch
            {
            }
        }

       
    }
}