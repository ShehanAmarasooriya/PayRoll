using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AddExtraNames : Form
    {
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();
        FTSPayRollBL.EstateDivisionBlock EstDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();

        public AddExtraNames()
        {
            InitializeComponent();
        }

        private void AddExtraNames_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision.Text = FTSPayRollBL.User.StrDivision;

            chkPaidHoliday.Enabled = false;
            rbtnEmpAttended.Checked = true;

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            String strAddType = "";
            if (!chkPaidHoliday.Checked)
            {
                MessageBox.Show("Selected Date Must Be A Paid Holiday.");
                dateTimePicker1.Focus();
            }
            else if (cmbDivision.SelectedIndex < 0)
            {
                MessageBox.Show("Select A Division");
                cmbDivision.Focus();
            }
            else
            {
                if (rbtnEmpAttended.Checked)
                {
                    DHarvest.StrPHAddType = "ATTENDED";
                    strAddType = rbtnEmpAttended.Text;
                }
                else
                {
                    DHarvest.StrPHAddType = "ACTIVE";
                    strAddType = rbtnAllActive.Text;
                }
                DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                
                DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
               
                if (MessageBox.Show(strAddType, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (chkAllDivision.Checked)
                    {
                        foreach (DataRow drow1 in EstDiv.ListEstateDivisions().Rows)
                        {
                            MChit.StrACCode = "021";
                            MChit.StrGangNumber = drow1[0].ToString() + "PH";
                            MChit.StrChitNo = drow1[0].ToString() + "PH";
                            DHarvest.StrACCode = MChit.StrACCode;
                            DHarvest.StrMusterChitNumber = MChit.StrChitNo;
                            DHarvest.StrGangNo = MChit.StrGangNumber;
                            DHarvest.StrDivision = drow1[0].ToString();
                            try
                            {
                                String status = DHarvest.InsertExtraNameToDivision();

                                if (status.Equals("COMPLETED"))
                                {
                                    try
                                    {
                                        DHarvest.StrDivision = drow1[0].ToString();
                                        MChit.StrACCode = "021";
                                        MChit.StrGangNumber = drow1[0].ToString() + "PH";
                                        MChit.StrChitNo = drow1[0].ToString() + "PH";
                                        DHarvest.StrACCode = MChit.StrACCode;
                                        DHarvest.StrMusterChitNumber = MChit.StrChitNo;
                                        DHarvest.StrGangNo = MChit.StrGangNumber;
                                        DHarvest.StrDivision = drow1[0].ToString();
                                        MChit.DtDate = dateTimePicker1.Value.Date;
                                        MChit.StrDivision = drow1[0].ToString();
                                        MChit.StrField = "NA";
                                        //MChit.StrACCode = "021";
                                        MChit.IntEmpCount = MChit.getNoOfPHEmployees(dateTimePicker1.Value.Date, drow1[0].ToString());
                                        //MChit.StrGangNumber = drow1[0].ToString() + "PH";
                                        //MChit.StrChitNo = drow1[0].ToString() + "PH";
                                        MChit.StrUser = FTSPayRollBL.User.StrUserName;
                                        //Set Labour Type as General
                                            MChit.StrLabourType = "General";
                                            MChit.StrLabourEstate = "NA";
                                            MChit.StrLabourDivision = "NA";
                                            MChit.StrLabourField = "NA";
                                       

                                        MChit.InsertMusterChitEntry();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error On MusterChit Insert, "+ex.Message);
                                    }
                                    MessageBox.Show("Free Names Added Successfully To Division: " + drow1[0].ToString() + "! ");

                                }
                                else if (status.Equals("EXISTS"))
                                {
                                    MessageBox.Show("Already Exists Division: " + drow1[0].ToString()  );
                                }
                                else if (status.Equals("NOTPH"))
                                {
                                    MessageBox.Show("Fail to Add Free Names, Not A Paid Holiday");
                                }
                                else
                                {
                                    MessageBox.Show("Oops, something went wrong!");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error, " + ex.Message);
                            }

                            
                            

                        }
                        MessageBox.Show("All Divisions Completed.");
                    }
                    else
                    {
                        

                        try
                        {
                            DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                            MChit.StrACCode = "021";
                            MChit.StrGangNumber = cmbDivision.SelectedValue.ToString() + "PH";
                            MChit.StrChitNo = cmbDivision.SelectedValue.ToString() + "PH";
                            DHarvest.StrACCode = MChit.StrACCode;
                            DHarvest.StrMusterChitNumber = MChit.StrChitNo;
                            DHarvest.StrGangNo = MChit.StrGangNumber;
                            DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                            MChit.DtDate = dateTimePicker1.Value.Date;
                            MChit.StrDivision = cmbDivision.SelectedValue.ToString();
                            MChit.StrField = "NA";
                            //MChit.StrACCode = "021";
                            MChit.IntEmpCount = MChit.getNoOfPHEmployees(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                            String status = DHarvest.InsertExtraNameToDivision();

                            if (status.Equals("COMPLETED"))
                            {
                                try
                                {
                                    DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                                    MChit.DtDate = dateTimePicker1.Value.Date;
                                    MChit.StrDivision = cmbDivision.SelectedValue.ToString();
                                    MChit.StrField = "NA";
                                    MChit.StrACCode = "021";
                                    MChit.IntEmpCount = MChit.getNoOfPHEmployees(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                                    MChit.StrGangNumber = cmbDivision.SelectedValue.ToString() + "PH";
                                    MChit.StrChitNo = cmbDivision.SelectedValue.ToString() + "PH";
                                    MChit.StrUser = FTSPayRollBL.User.StrUserName;
                                    //Set Labour Type as General
                                    MChit.StrLabourType = "General";
                                    MChit.StrLabourEstate = "NA";
                                    MChit.StrLabourDivision = "NA";
                                    MChit.StrLabourField = "NA";
                                    MChit.InsertMusterChitEntry();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error On MusterChit Insert, " + ex.Message);
                                }
                                MessageBox.Show("Free Names Added Successfully To Division: " + cmbDivision.SelectedValue.ToString() + "! ");
                            }
                            else if (status.Equals("EXISTS"))
                            {
                                MessageBox.Show("Already Exists");
                            }
                            else if (status.Equals("NOTPH"))
                            {
                                MessageBox.Show("Fail to Add Free Names, Not A Paid Holiday");
                            }
                            else
                            {
                                MessageBox.Show("Oops, something went wrong!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled By User.");
                }

            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            cmbDivision.SelectedIndex = -1;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (myHoli.IsPaidHoliday(dateTimePicker1.Value.Date))
            {
                chkPaidHoliday.Checked = true;
            }
            else
            {
                chkPaidHoliday.Checked = false;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (!chkPaidHoliday.Checked)
            {
                MessageBox.Show("Selected Date Must Be A Paid Holiday.");
                dateTimePicker1.Focus();
            }
            else if (cmbDivision.SelectedIndex < 0)
            {
                MessageBox.Show("Select A Division");
                cmbDivision.Focus();
            }
            else
            {
                if (MessageBox.Show("Delete All FreeNames", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                    
                    DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;

                    try
                    {
                        if (chkAllDivision.Checked)
                        {
                            foreach (DataRow drow2 in EstDiv.ListEstateDivisions().Rows)
                            {
                                DHarvest.StrDivision = drow2[0].ToString();

                                String status = DHarvest.DeleteExtraNameToDivision();

                                if (status.Equals("COMPLETED"))
                                {
                                    MessageBox.Show(drow2[0].ToString() +"Division Free Names Deleted Successfully! ");
                                }
                                else if (status.Equals("NOTEXISTS"))
                                {
                                    MessageBox.Show(drow2[0].ToString()+" Division PH Not Exists");
                                }
                                else if (status.Equals("NOTPH"))
                                {
                                    MessageBox.Show("Failed, Not A Paid Holiday");
                                }
                                else
                                {
                                    MessageBox.Show("Oops, something went wrong!");
                                }
                                
                            }
                            MessageBox.Show("All Divisions Completed.");

                        }
                        else
                        {
                            DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                            String status = DHarvest.DeleteExtraNameToDivision();

                            if (status.Equals("COMPLETED"))
                            {
                                MessageBox.Show("Free Names Deleted Successfully! ");
                            }
                            else if (status.Equals("NOTEXISTS"))
                            {
                                MessageBox.Show("PH Not Exists");
                            }
                            else if (status.Equals("NOTPH"))
                            {
                                MessageBox.Show("Failed, Not A Paid Holiday");
                            }
                            else
                            {
                                MessageBox.Show("Oops, something went wrong!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled By User.");
                }
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            //if (myHoli.IsPaidHoliday(dateTimePicker1.Value.Date))
            //{
            //    chkPaidHoliday.Checked = true;
            //}
            //else
            //{
            //    chkPaidHoliday.Checked = false;
            //}
        }

        
    }
}