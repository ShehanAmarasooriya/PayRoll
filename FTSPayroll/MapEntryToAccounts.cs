using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class MapEntryToAccounts : Form
    {
        FTSPayRollBL.ProcessMonthlyWages ProMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.AccountInformation DHAccounts = new FTSPayRollBL.AccountInformation();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();

        DateTime dtFrom = new DateTime(2016, 2, 1);
        DateTime dtTo = new DateTime(2016, 2, 29);

        public MapEntryToAccounts()
        {
            InitializeComponent();
        }

        private void MapEntryToAccounts_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);
            
            rbtnDailyEntries.Checked = true;
            gvList.DataSource = ProMWages.ListDailyEntriesMapping(dtFrom, dtTo,false); 
            gvJobSubCategory.DataSource = null;

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (rbtnDailyEntries.Checked == true)
            {
                try
                {
                    ProMWages.FillAccountMapping(Convert.ToInt32(cmbYear.SelectedValue.ToString()),Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    refreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
            else
            {
                try
                {
                    ProMWages.FillAccountMappingOT(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    refreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dateTimePicker1.Value = Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtDivision.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtField.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();

                txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtJobName.Text=Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
                gvJobSubCategory.DataSource = DHAccounts.GetAvailableSubCategoriesForJob(txtJobShortName.Text);

                if(!String.IsNullOrEmpty(gvList.Rows[e.RowIndex].Cells[4].Value.ToString()))
                {
                    txtMainACCode.Text = gvList.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtMainACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtMainACCode.Text);
                    if (txtMainACCodeName.Text.ToUpper().Equals("NA"))
                    {
                        MessageBox.Show("Main Account Code Not Found!");
                        txtMainACCode.Focus();
                    }
                    else
                    {
                        if (DHAccounts.GetAvailableAccountsForJob(txtMainACCode.Text, txtJobShortName.Text).Rows.Count > 0)
                        {
                            //cmbAcCode.DataSource = DHAccounts.GetAvailableAccountsForJob(txtMainACCode.Text, txtJobShortName.Text);
                            //cmbAcCode.DisplayMember = "AccountCode";
                            //cmbAcCode.ValueMember = "AccountName";
                            //if (gvList.Rows[e.RowIndex].Cells[5].Value.ToString().Equals("NA") || String.IsNullOrEmpty(gvList.Rows[e.RowIndex].Cells[5].Value.ToString()))
                            //{
                            //    //cmbAcCode.SelectedIndex = -1;
                            //    txtAccountName.Text = "";
                            //}
                            //else
                            //{
                            //    cmbAcCode.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
                            //}
                        }
                        else
                        {
                            MessageBox.Show("No Accounts Availble For Job-" + txtJobShortName.Text + ", Maincode-" + txtMainACCode.Text);
                            cmbAcCode.DataSource = null;
                            txtAccountName.Text = "";
                        }
                        
                    }

                    //cmbAcCode.SelectedValue = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                txtStatus.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();

                if (String.IsNullOrEmpty(txtMainACCodeName.Text))
                {
                    MessageBox.Show("Main Account Code Not Found");
                    txtMainACCode.Focus();
                }
                else
                {
                    if (DHAccounts.GetAvailableAccountsForJob(txtMainACCode.Text, txtJobShortName.Text).Rows.Count > 0)
                    {
                        //cmbAcCode.DataSource = DHAccounts.GetAvailableAccountsForJob(txtMainACCode.Text, txtJobShortName.Text);
                        //cmbAcCode.DisplayMember = "AccountCode";
                        //cmbAcCode.ValueMember = "AccountName";
                        //cmbAcCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No Accounts Availble For Job-" + txtJobShortName.Text + ", Maincode-" + txtMainACCode.Text);
                        cmbAcCode.DataSource = null;
                        txtMainACCode.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, ",ex.Message);
            }
        }

        private void txtJobShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtJobShortName.Text))
            {
                if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    txtJobShortName.Text = "";
                    txtJobShortName.Focus();
                }
                else
                {
                    txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
                    txtJobShortName.Text = txtJobShortName.Text.ToUpper();                    
                }
            }
        }

        private void txtMainACCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtMainACCode.Text.Equals("?"))
                {
                    ACSubCategoryList objAcSubList = new ACSubCategoryList(this);
                    objAcSubList.Show();
                }
                else
                {
                    //DateChanged();
                    txtMainACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtMainACCode.Text);
                    if (txtMainACCodeName.Text.ToUpper().Equals("NA"))
                    {
                        MessageBox.Show("Main Account Code Not Found!");
                        txtMainACCode.Focus();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtMainACCodeName.Text))
                        {
                            MessageBox.Show("Main Account Code Not Found");
                            txtMainACCode.Focus();
                        }
                        else
                        {
                            cmbAcCode.Focus();
                        }
                    }
                }

            }
            else
            {
                txtMainACCode.Focus();
            }
        }

        private void txtMainACCode_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtJobShortName.Text))
            //{
            //    MessageBox.Show("Job Code Not Found!");
            //    txtJobShortName.Focus();
            //}

            //    //DateChanged();
            //    txtMainACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtMainACCode.Text);
            //    if (txtMainACCodeName.Text.ToUpper().Equals("NA"))
            //    {
            //        MessageBox.Show("Main Account Code Not Found!");
            //        txtMainACCode.Focus();
            //    }
            //    else
            //    {
            //        if (String.IsNullOrEmpty(txtMainACCodeName.Text))
            //        {
            //            MessageBox.Show("Main Account Code Not Found");
            //            txtMainACCode.Focus();
            //        }
            //        else
            //        {
            //            //if (DHAccounts.GetAvailableAccountsForJob(txtMainACCode.Text, txtJobShortName.Text).Rows.Count > 0)
            //            //{
            //            //    cmbAcCode.DataSource = DHAccounts.GetAvailableAccountsForJob(txtMainACCode.Text, txtJobShortName.Text);
            //            //    cmbAcCode.DisplayMember = "AccountCode";
            //            //    cmbAcCode.ValueMember = "AccountName";
            //            //    cmbAcCode.Focus();
            //            //}
            //            //else
            //            //{
            //            //    MessageBox.Show("No Accounts Availble For Job-"+txtJobShortName.Text+", Maincode-"+txtMainACCode.Text);
            //            //    cmbAcCode.DataSource = null;
            //            //    txtMainACCode.Focus();
            //            //}

            //        }
            //    }
            
        }

        private void cmbAcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtAccountName.Text = cmbAcCode.SelectedValue.ToString();
            }
            catch { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (rbtnDailyEntries.Checked)
            {
                try
                {
                    txtMainACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtMainACCode.Text);
                    if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
                    {
                        MessageBox.Show("Please Enter a Correct Job Code.");
                        txtJobShortName.Text = "";
                        txtJobShortName.Focus();
                    }
                    else if (txtMainACCodeName.Text.ToUpper().Equals("NA") || String.IsNullOrEmpty(txtMainACCode.Text))
                    {
                        MessageBox.Show("Main Account Code Not Found!");
                        txtMainACCode.Focus();
                    }
                    //else if (!DHAccounts.IsJobAccountAvaialbleInACMaster(txtJobShortName.Text, txtMainACCode.Text, cmbAcCode.Text))
                    //{
                    //    MessageBox.Show("Job Account Not Available In Account Master");
                    //    cmbAcCode.Focus();

                    //}
                    else
                    {
                        //ProMWages.UpdateDailyEntryMapping(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), txtDivision.Text, txtField.Text, txtJobShortName.Text, txtMainACCode.Text, cmbAcCode.Text);
                        ProMWages.UpdateDailyEntryMapping(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), txtDivision.Text, txtField.Text, txtJobShortName.Text, txtMainACCode.Text);
                        refreshGrid();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Update, " + ex.Message);
                }
            }
            else
            {
                try
                {
                    txtMainACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtMainACCode.Text);
                    if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
                    {
                        MessageBox.Show("Please Enter a Correct Job Code.");
                        txtJobShortName.Text = "";
                        txtJobShortName.Focus();
                    }
                    else if (txtMainACCodeName.Text.ToUpper().Equals("NA") || String.IsNullOrEmpty(txtMainACCode.Text))
                    {
                        MessageBox.Show("Main Account Code Not Found!");
                        txtMainACCode.Focus();
                    }
                    else if (!DHAccounts.IsJobAccountAvaialbleInACMaster(txtJobShortName.Text, txtMainACCode.Text, cmbAcCode.Text))
                    {
                        MessageBox.Show("Job Account Not Available In Account Master");
                        cmbAcCode.Focus();

                    }
                    else
                    {
                        //ProMWages.UpdateDailyEntryMappingOT(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), txtDivision.Text, txtField.Text, txtJobShortName.Text, txtMainACCode.Text, cmbAcCode.Text);
                        ProMWages.UpdateDailyEntryMappingOT(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), txtDivision.Text, txtField.Text, txtJobShortName.Text, txtMainACCode.Text);
                        refreshGrid();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Update, " + ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnDailyEntries_CheckedChanged(object sender, EventArgs e)
        {
            refreshGrid();
        }
        public void refreshGrid()
        {
            DateTime dtMonthStDate = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
            DateTime dtMonthEndDate = dtMonthStDate.AddMonths(1).AddDays(-1);
            if (rbtnDailyEntries.Checked)
            {
                if (chkAllCodes.Checked)
                {
                    gvList.DataSource = ProMWages.ListDailyEntriesMapping(dtMonthStDate, dtMonthEndDate, true);
                }
                else
                {
                    gvList.DataSource = ProMWages.ListDailyEntriesMapping(dtMonthStDate, dtMonthEndDate, false);
                }

            }
            else
            {
                if (chkAllCodes.Checked)
                {
                    gvList.DataSource = ProMWages.ListDailyEntriesMappingOT(dtMonthStDate, dtMonthEndDate, true);
                }
                else
                {
                    gvList.DataSource = ProMWages.ListDailyEntriesMappingOT(dtMonthStDate, dtMonthEndDate, false);
                }
            }
            gvJobSubCategory.DataSource = null;
            txtJobShortName.Clear();
            txtMainACCode.Clear();
            txtDivision.Clear();
            txtField.Clear();
            txtStatus.Clear();
        }

        private void chkAllCodes_CheckedChanged(object sender, EventArgs e)
        {
            refreshGrid();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = YMonth.ListMonths(Convert.ToInt32(this.cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";
                cmbMonth.SelectedValue = YMonth.getLastMonthID();
            }
            catch (Exception ex)
            {
            }
        }
    }
}