using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
    
{
    public partial class NotOfferedEntry : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.NotOffered NOffered = new FTSPayRollBL.NotOffered();
        FTSPayRollBL.Job JobMaster = new FTSPayRollBL.Job();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();

        public NotOfferedEntry()
        {
            InitializeComponent();
        }

        private void NotOfferedEntry_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbCategory.DataSource = EmpCat.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            cmbNotOfferedCode.DataSource = JobMaster.getNotOfferedJobs();
            cmbNotOfferedCode.DisplayMember = "JobName";
            cmbNotOfferedCode.ValueMember = "JobID";

            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                {
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
                }
                if (cmbDivision.SelectedIndex != -1 && cmbEmpNo.SelectedIndex != -1)
                {
                    if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()) && !String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                    {
                        gvNotOffered.DataSource = NOffered.ListNotOfferedByEmployee(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
                    }
                }
            }
            catch { }
        }
        public void AfterAdd()
        {

        }
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            String status = "";
            DateTime dtFrom=dtpFromDate.Value.Date;
            DateTime dtToDate=dtpToDate.Value.Date;
            
            while (dtFrom <= dtToDate)
            {
                DHarvest.DtHarvestDate = Convert.ToDateTime(dtFrom.ToShortDateString()); 
                DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                DHarvest.StrJob = cmbNotOfferedCode.Text;
                DHarvest.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
                DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
                try
                {
                    if (myHoli.IsGeneralHoliday(dtFrom))
                    {
                        MessageBox.Show("Date: " + dtFrom + " Is A Holiday, Not Offered Not Added.");
                    }
                    else
                    {
                        status = DHarvest.InsertHarvetEntryNotOffered();
                        if (status.Equals("ADDED"))
                        {
                            //MessageBox.Show("Daily Harvest Entry Added Successfully! ");
                            AfterAdd();
                        }

                        else if (status.Equals("EXISTS"))
                        {
                            MessageBox.Show("Already Exists");
                            AfterAdd();
                        }
                        else
                        {
                            MessageBox.Show("Oops, something went wrong!");
                            AfterAdd();
                        }
                    }
                    dtFrom = dtFrom.AddDays(1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }

            }
            if (chkEPF.Checked)
            {
                NOffered.BoolEPF = true;
            }
            else
            {
                NOffered.BoolEPF = false;
            }
            if (chkETF.Checked)
            {
                NOffered.BoolETF = true;
            }
            else 
            {
                NOffered.BoolETF = false;
            }
            if (chkDailyPay.Checked)
            {
                NOffered.BoolNamePay = true;
            }
            else
            {
                NOffered.BoolNamePay = false;
            }
            NOffered.StrUserId=FTSPayRollBL.User.StrUserName;
           try
           {
               status = DHarvest.InsertHarvetEntryNotOffered();
            //String status=NOffered.insertEmployeeNotOffered();
           
            if(status.Equals("ADDED"))
            {
                MessageBox.Show("Not Offered Entry Added Successfully!");
            }
            else if(status.Equals("EXISTS"))
            {
                MessageBox.Show("Already Added Not Offered Entries");
            }
            else
            {
                MessageBox.Show("Error, Not Offered Entry Failed!");
            }
            cmdClear.PerformClick();
           }            
            catch(Exception ex)
            {
                MessageBox.Show("Error, "+ex.Message);
            }
        
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            NOffered.IntNotOfferedId = Convert.ToInt32(lblRefNo.Text);
            NOffered.StrDivision = cmbDivision.SelectedValue.ToString();
            NOffered.IntEmpCategory = Convert.ToInt32(cmbCategory.SelectedValue.ToString());
            NOffered.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
            NOffered.DtFromDate = dtpFromDate.Value.Date;
            NOffered.DtToDate = dtpToDate.Value.Date;
            NOffered.IntNotOfferedCode = Convert.ToInt32(cmbNotOfferedCode.SelectedValue.ToString());
            if (chkEPF.Checked)
            {
                NOffered.BoolEPF = true;
            }
            else
            {
                NOffered.BoolEPF = false;
            }
            if (chkETF.Checked)
            {
                NOffered.BoolETF = true;
            }
            else
            {
                NOffered.BoolETF = false;
            }
            if (chkDailyPay.Checked)
            {
                NOffered.BoolNamePay = true;
            }
            else
            {
                NOffered.BoolNamePay = false;
            }
            NOffered.StrUserId = FTSPayRollBL.User.StrUserName;
            try
            {
                String status = NOffered.UpdateEmployeeNotOffered();

                if (status.Equals("UPDATED"))
                {
                    MessageBox.Show("Not Offered Entry Updated Successfully!");
                }
                else if (status.Equals("NOTEXISTS"))
                {
                    MessageBox.Show("Not an Existing Not Offered Entry");
                }
                else
                {
                    MessageBox.Show("Error, Update Not Offered Entry Failed!");
                }
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            
            try
            {
                String status = DHarvest.DeleteEmployeeNotOffered(dtpFromDate.Value.Date, dtpToDate.Value.Date);
                if(status.Equals("DELETED"))
                {
                    MessageBox.Show("Not Offered Entries Deleted Successfully!");
                }
                else if (status.Equals("NOTEXISTS"))
                {
                    MessageBox.Show("Not an Existing Not Offered Entry");
                }
                else
                {
                    MessageBox.Show("Error !");
                }
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }

        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
            {
                cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(cmbDivision.SelectedValue.ToString());
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";

                cmbEmpNo_SelectedIndexChanged(null, null);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            
            txtEmpName.Text = "";
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            chkEPF.Checked = false;
            chkETF.Checked = false;
            if (cmbDivision.SelectedIndex != -1 && cmbEmpNo.SelectedIndex != -1)
            {
                if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()) && !String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                {
                    gvNotOffered.DataSource = NOffered.ListNotOfferedByEmployee(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
                }
            }
            chkDailyPay.Checked = false;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            cmdAdd.Enabled = true;
        }

        private void gvNotOffered_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           lblRefNo.Text = gvNotOffered.Rows[e.RowIndex].Cells[0].Value.ToString();
           cmbDivision.SelectedValue = gvNotOffered.Rows[e.RowIndex].Cells[1].Value.ToString();
           cmbCategory.SelectedValue = Convert.ToInt32(gvNotOffered.Rows[e.RowIndex].Cells[2].Value.ToString());
           cmbEmpNo.SelectedValue = gvNotOffered.Rows[e.RowIndex].Cells[3].Value.ToString(); ;
           txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
           dtpFromDate.Value = Convert.ToDateTime(gvNotOffered.Rows[e.RowIndex].Cells[5].Value.ToString());
            dtpToDate.Value=Convert.ToDateTime(gvNotOffered.Rows[e.RowIndex].Cells[6].Value.ToString());
            cmbNotOfferedCode.SelectedValue=Convert.ToInt32(gvNotOffered.Rows[e.RowIndex].Cells[4].Value.ToString());
            chkEPF.Checked=Convert.ToBoolean(gvNotOffered.Rows[e.RowIndex].Cells[7].Value.ToString());
            chkETF.Checked=Convert.ToBoolean(gvNotOffered.Rows[e.RowIndex].Cells[8].Value.ToString());
            chkDailyPay.Checked=Convert.ToBoolean(gvNotOffered.Rows[e.RowIndex].Cells[9].Value.ToString());
            cmdAdd.Enabled = false;
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
       
    }
}