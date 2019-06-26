using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class JobGroup : Form
    {
        FTSPayRollBL.JobGroup myJobGroup = new FTSPayRollBL.JobGroup();

        public JobGroup()
        {
            InitializeComponent();
        }

        //private void cmdCancel_Click(object sender, EventArgs e)
        //{
        //    txtDescription.Clear();
        //    txtGroupName.Clear();

        //    gvList.DataSource = myJobGroup.ListGroups();

        //    lblUser.Text = "0";

        //    cmdAdd.Enabled = true;
        //    cmdUpdate.Enabled = false;
        //    cmdDelete.Enabled = false;
        //}

        //private void cmdAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        myJobGroup.StrGroupName = txtDescription.Text;
        //        myJobGroup.StrShortName = txtGroupName.Text;
        //        myJobGroup.StrGroupParent = cmbParentGroup.Text;
        //        myJobGroup.InsertGroup();

        //        MessageBox.Show("Job Group Created Successfully");

        //        cmdCancel.PerformClick();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void cmdUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        myJobGroup.IntGroupID = Convert.ToInt32(lblUser.Text);
        //        myJobGroup.StrGroupName = txtDescription.Text;
        //        myJobGroup.StrShortName = txtGroupName.Text;
        //        myJobGroup.StrGroupParent = cmbParentGroup.Text;
        //        myJobGroup.UpdateGroup();

        //        MessageBox.Show("Job Group Updated Successfully");

        //        cmdCancel.PerformClick();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void cmdDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            myJobGroup.IntGroupID = Convert.ToInt32(lblUser.Text);
        //            myJobGroup.DeleteGroup();

        //            MessageBox.Show("Group Deleted");
        //            gvList.DataSource = myJobGroup.ListGroups();
        //            cmdCancel.PerformClick();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        //private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    lblUser.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    txtDescription.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
        //    txtGroupName.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
        //    cmbParentGroup.Text = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();


        //    cmdAdd.Enabled = false;
        //    cmdUpdate.Enabled = true;
        //    cmdDelete.Enabled = true;

        //    txtDescription.Focus();
        //}

        private void JobGroup_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myJobGroup.ListGroups();
            
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtGroupName.Text = "";
            txtDescription.Text = "";
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

            gvlist.DataSource = myJobGroup.ListGroups();
            
        }

        private void cmdAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupName.Text == "")
                {
                    MessageBox.Show("Group name can not be empty");
                    txtGroupName.Focus();
                }
                myJobGroup.StrGroupName = txtGroupName.Text;
                myJobGroup.StrDescription = txtDescription.Text;
                myJobGroup.InsertGroup();
                MessageBox.Show("Group Added Successfully");
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, "+ex.Message);
            }
        }

        private void cmdUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupName.Text == "")
                {
                    MessageBox.Show("Group name can not be empty");
                    txtGroupName.Focus();
                }

                myJobGroup.StrGroupName = txtGroupName.Text;
                myJobGroup.StrDescription = txtDescription.Text;
                myJobGroup.UpdateGroup();
                MessageBox.Show("Group Updated Successfully");
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message); 
            }
        }

        private void cmdDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    myJobGroup.StrGroupName = txtGroupName.Text;
                    myJobGroup.DeleteGroup();
                    MessageBox.Show("Group Deleted Successfully");
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            txtDescription.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtGroupName.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;

           
        }
       
    }
}