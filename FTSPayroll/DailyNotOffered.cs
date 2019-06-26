using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyNotOffered : Form
    {
        FTSPayRollBL.DailyNotOfferedCls MyNotOffered = new FTSPayRollBL.DailyNotOfferedCls();
        FTSPayRollBL.EstateDivisionBlock EstDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster myemp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory myCategory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();

        public DailyNotOffered()
        {
            InitializeComponent();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            String strGender="All";
            String strDiv = "";
            if (chkAllDivisions.Checked)
            {
                strDiv = "All";
            }
            else
            {
                strDiv = cmbDivision.SelectedValue.ToString();
            }

            if (!chkAll.Checked)
            {
                if (cmbGender.Text.Equals("Male"))
                {
                    MyNotOffered.StrGender = "M";
                    strGender="Male";
                }
                else
                {
                    MyNotOffered.StrGender = "F";
                    strGender="Female";
                }
            }
            else
            {
                MyNotOffered.StrGender = "%";
                    strGender="All";
            }

            if (MessageBox.Show(" Add Not Offered Entries To " + strGender+ " of " + strDiv + " Division(s)?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                String strError = "";
                DataTable DivisionTbl;
                DataTable ActiveEmployeesTbl;
                cmdAdd.Enabled = false;
                cmdClear.Enabled = false;
                cmdClose.Enabled = false;
                if (String.IsNullOrEmpty(cmbGender.Text) && !chkAll.Checked)
                {
                    MessageBox.Show("Select Male/Female Or All");
                    cmbGender.Focus();
                }
                else if (String.IsNullOrEmpty(cmbNOCodes.Text))
                {
                    MessageBox.Show("Select a Not Offered Code");
                    cmbNOCodes.Focus();
                }
                else
                {
                    if (chkAllDivisions.Checked)
                    {
                        DivisionTbl = EstDiv.ListEstateDivisions();
                    }
                    else
                    {
                        DivisionTbl = EstDiv.ListEstateDivisions(cmbDivision.SelectedValue.ToString());
                    }
                    foreach (DataRow drow1 in DivisionTbl.Rows)
                    {
                        String status = "";
                        MyNotOffered.DtDate = dateTimePicker1.Value.Date;
                        MyNotOffered.StrDivision = drow1[0].ToString();
                        
                        MyNotOffered.StrNOCode = cmbNOCodes.Text;
                        MyNotOffered.StrUserId = FTSPayRollBL.User.StrUserName;
                        try
                        {

                            try
                            {
                                for (int i = 0; i < ListWorkCategories.Items.Count; i++)
                                {
                                    if (ListWorkCategories.GetItemChecked(i) == true)
                                    {
                                        ActiveEmployeesTbl = myemp.ListActiveEmployees(drow1[0].ToString(), MyNotOffered.StrGender, ListWorkCategories.Items[i].ToString());
                                        strError = "";
                                        foreach (DataRow drow2 in ActiveEmployeesTbl.Rows)
                                        {                                            
                                            status = MyNotOffered.InsertBulkNotOffered(drow2[1].ToString());
                                            if (status.Equals("ADDED"))
                                            {
                                                //MessageBox.Show("Not Offered Added to " + MyNotOffered.StrDivision + " Division Successfully!","",MessageBoxButtons.OK,MessageBoxIcon.None);
                                            }
                                            else if (status.Equals("EXISTS"))
                                            {
                                                strError += "EXISTS";


                                            }
                                            else if (status.Equals("HOLIDAY"))
                                            {
                                                strError += "HOLIDAY";
                                                MessageBox.Show("Selected Date Is A Holiday, Not Offered Not Allowed On General Holiday!", "Holiday", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                break;
                                            }
                                            else
                                            {
                                                strError += "Error";
                                                MessageBox.Show("Error!");
                                                break;
                                            }
                                        }
                                    }
                                }
                         
                                if(strError.Contains("Error"))
                                //if (strError != "")
                                {
                                    MessageBox.Show("Not Offered Added With Errors.");
                                }
                                else
                                {
                                    MessageBox.Show("Not Offered Added to " + MyNotOffered.StrDivision + " Division Successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error!," + ex.Message);
                            }
                            //cmdAdd.Enabled = true;
                            //cmdClear.Enabled = true;
                            //cmdClose.Enabled = true;


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, ", ex.Message);
                        }
                    }
                    cmdAdd.Enabled = true;
                    cmdClear.Enabled = true;
                    cmdClose.Enabled = true;

                }
            }
           
        }

        

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbGender.Enabled = false;
                MyNotOffered.StrGender = "All";
            }
            else
            {
                cmbGender.Enabled = true;
            }
        }

        private void DailyNotOffered_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            ListWorkCategories.Items.Clear();
            DataTable myTable = myCategory.ListWorkCategories();
            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    ListWorkCategories.Items.Add(myTable.Rows[i][0].ToString());
                }
            }
            else
                ListWorkCategories.Items.Clear();
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDivision.Focus();
            }
        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbGender.Focus();
            }
        }

        private void cmbGender_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                chkAll.Focus();
            }
        }

        private void chkAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbNOCodes.Focus();
            }
        }

        private void cmbNOCodes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String StrAllDiv = "All";
            String status = "";

            if (chkAllDivisions.Checked)
                MyNotOffered.StrDivision = "%";
            else
            {
                MyNotOffered.StrDivision = cmbDivision.SelectedValue.ToString();
                StrAllDiv = cmbDivision.SelectedValue.ToString();
            }


            if (MessageBox.Show("Do You Want To Delete "+StrAllDiv+" Not Offered On "+dateTimePicker1.Value.Date.ToShortDateString(),"Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    MyNotOffered.DtDate = dateTimePicker1.Value.Date;
                    MyNotOffered.StrNOCode = cmbNOCodes.Text;
                    MyNotOffered.StrUserId = FTSPayRollBL.User.StrUserName;
                    status = MyNotOffered.DeleteBulkNotOffered();
                    MessageBox.Show(StrAllDiv+" Not Offered On "+dateTimePicker1.Value.Date.ToShortDateString()+" Deleted Successfully.","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, "+ex.Message);
                }
            }
        }

        private void chkAllWorkCategoy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllWorkCategoy.Checked)
            {
                for (int i = 0; i < ListWorkCategories.Items.Count; i++)
                {
                    ListWorkCategories.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < ListWorkCategories.Items.Count; i++)
                {
                    ListWorkCategories.SetItemChecked(i, false);
                }
            }
        }

        private void cmbNOCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblName.Text = Job1.JobNameByShortName(this.cmbNOCodes.Text.ToUpper());
                
            }
            catch { }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            String strDateOk = "";
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            myEntries.IntEntryYear = dateTimePicker1.Value.Date.Year;
            myEntries.IntEntryMonth = dateTimePicker1.Value.Date.Month;
            /*Blocked for BPL*/
            if (FTSPayRollBL.User.BoolDayBlockAvailable)
            {
                strDateOk = myEntries.CheckMonthDifference();
            }
            else
            {
                strDateOk = "OK";
            }
            if ((strDateOk.Equals("OK")))
            {
                cmbDivision.Focus();
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                //MChit.DtDate = dateTimePicker1.Value.Date;
                dateTimePicker1.Focus();
            }
            else if (strDateOk.Equals("POST_DATE_BLOCK"))
            {
                MessageBox.Show("Post Date Entry Blocked.", "Blocked Entries");

                //MChit.DtDate = dateTimePicker1.Value.Date;
                dateTimePicker1.Focus();
            }
            else if (strDateOk.Equals("CONFIRMED"))
            {
                MessageBox.Show("Already Confirmed.", "Entries Blocked");

                //MChit.DtDate = dateTimePicker1.Value.Date;
                dateTimePicker1.Focus();
            }
            else
            {
                MessageBox.Show("This Date Data Entries Are Blocked Now, Please Contact Head Office For Date Release.");
                this.Close();
            }
        }
    }
}