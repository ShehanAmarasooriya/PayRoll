using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyHarvestRubberCW : Form
    {

        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.DailyHarvestRubber DHarvestRubber = new FTSPayRollBL.DailyHarvestRubber();
        FTSPayRollBL.EstateDivisionBlock myField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Division myDiv = new FTSPayRollBL.Division();
        FTSPayRollBL.Field myFieldType = new FTSPayRollBL.Field();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();
        FTSPayRollBL.AccountInformation DHAccounts = new  FTSPayRollBL.AccountInformation();
        DataTable dtDaySummary = new DataTable();

        public DailyHarvestRubberCW()
        {
            InitializeComponent();
        }

        private void DateChanged()
        {
            //if a poyaday 
            if (dateTimePicker1.Value.Date.ToString("dddd").Equals("Sunday"))
            {
                chkHoliday.Checked = true;
                cmbHoliManDays.Text = "1.00";
            }

            if (myHoli.IsPoyaday(dateTimePicker1.Value.Date))
            {
                chkHoliday.Checked = true;
                cmbHoliManDays.Text = "1.00";
            }
            //if a paid holiday
            if (myHoli.IsPaidHoliday(dateTimePicker1.Value.Date))
            {
                chkPaidHoliday.Checked = true;
                cmbHoliManDays.Text = "1.00";
            }
            else
            {
                chkPaidHoliday.Checked = false;
            }
            if (!dateTimePicker1.Value.Date.ToString("dddd").Equals("Sunday") && !myHoli.IsPoyaday(dateTimePicker1.Value.Date) && !myHoli.IsPaidHoliday(dateTimePicker1.Value.Date))
            {
                cmbHoliManDays.Text = "1.00";
            }

            try
            {
            cmbMusterChit.DataSource = MChit.ListMusterChitForSelectedDate(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
            cmbMusterChit.DisplayMember = "MChitName";
            cmbMusterChit.ValueMember = "AutoMusterID";

            
                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                refreshSummaryDetails();
            }
            catch { }
       
        }
       
        public Boolean IsFieldandWorkcodeMatch()
        {
            Boolean Status = false;

            try
            {
                if (cmbJobCode.SelectedValue.ToString().Equals("?"))
                {
                    JobList myJob = new JobList();
                    myJob.Show();
                }
                else
                {
                    if (cmbJobCode.SelectedValue.ToString() != "")
                    {
                        if (rbtnGeneral.Checked == true && rbtnInterEstate.Checked == false && rbtnLentLabour.Checked == false)
                        {
                            DataTable dt1 = new DataTable();
                            DataTable dt = myFieldType.ListAllFieldsTypes(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                dt1 = myFieldType.GetWorkCodeType(cmbJobCode.SelectedValue.ToString());
                                if (dt1.Rows.Count <= 0)
                                    MessageBox.Show("Workcode Expenditure Type Not Available", "Warning", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show("Field Type Not Available", "Warning", MessageBoxButtons.OK);
                            }

                            try
                            {
                                if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                                {
                                    cmbJobCode.SelectedValue.ToString().ToUpper();
                                    txtJobShortName_LeaveChanged();
                                    Status = true;
                                }
                                else if (cmbJobCode.SelectedValue.ToString().Substring(0, 1).ToUpper() == "X")
                                {
                                    cmbJobCode.SelectedValue.ToString().ToUpper();
                                    txtJobShortName_LeaveChanged();
                                    Status = true;
                                }
                                else
                                {
                                    MessageBox.Show("Field Type - '" + dt.Rows[0][0].ToString() + "' and WorkCode type - '" + dt1.Rows[0][0].ToString() + "' should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cmbJobCode.Focus();
                                    //txtJobShortName.Clear();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Field Type And Job Expenditure Type Error", "Warning", MessageBoxButtons.OK);
                                cmbField.Focus();
                            }
                            dt.Dispose();
                            dt1.Dispose();
                        }
                        else if (rbtnGeneral.Checked == false && rbtnInterEstate.Checked == false && rbtnLentLabour.Checked == true)
                        {
                            DataTable dt = myFieldType.ListAllFieldsTypes(cmbLabourDivision.SelectedValue.ToString(), cmbLabourField.SelectedValue.ToString());
                            DataTable dt1 = myFieldType.GetWorkCodeType(cmbJobCode.SelectedValue.ToString());


                            if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                            {
                                cmbJobCode.SelectedValue.ToString().ToUpper();
                                txtJobShortName_LeaveChanged();
                                Status = true;
                            }
                            else if (cmbJobCode.SelectedValue.ToString().Substring(0, 1).ToUpper() == "X")
                            {
                                cmbJobCode.SelectedValue.ToString().ToUpper();
                                txtJobShortName_LeaveChanged();
                                Status = true;
                            }
                            else
                            {
                                MessageBox.Show("Field Type - '" + dt.Rows[0][0].ToString() + "' and WorkCode type - '" + dt1.Rows[0][0].ToString() + "' should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cmbJobCode.Focus();
                                //txtJobShortName.Clear();
                            }
                        }
                        else
                        {
                            cmbJobCode.SelectedValue.ToString().ToUpper();
                            txtJobShortName_LeaveChanged();
                            Status = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Workcode Found..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbCWWorkType_SelectedIndexChanged(null, null);
                        cmbCWWorkType.Focus();
                        ////txtJobShortName.Clear();
                        //txtJobShortName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Wrong in a Workcode..!" + ex.Message);
                //txtJobShortName.Clear();
            }

            return Status;
        }
        private void txtJobShortName_LeaveChanged()
        {
            //txtQty.Enabled = true;
            if (!String.IsNullOrEmpty(cmbJobCode.SelectedValue.ToString()))
            {
                if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    cmbJobCode.SelectedValue = "";
                    cmbJobCode.Focus();
                }
                else
                {
                    txtJobName.Text = Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper());
                    cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString().ToUpper();
                    if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        txtQty.Focus();
                    }
                    else
                    {
                        if (cmbFullHalf.Text.Equals("Full"))
                        {
                            chkTaskCompleted.Checked = true;
                            txtQty.Enabled = false;
                            txtScrapQty.Enabled = false;
                            this.btnAdd.Focus();
                        }
                        else
                        {
                            btnAdd.Focus();
                        }
                    }
                }
                if (cmbCWWorkType.SelectedValue.ToString() == "3")
                {
                    if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        MessageBox.Show("This WorkCode Not Valid..!", "Warning.!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtJobName.Clear();
                        cmbJobCode.Focus();
                    }
                }

            }

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            
            
            String status = "";
            Decimal decAvailCashMandays = 0;
            try
            {
                //DateChanged();


                //if (IsFieldandWorkcodeMatch() == true)
                if (true)
                {

                    //if (DHarvestRubber.CheckPreviousDayEntries(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString())).Equals("OK"))
                    if (true)
                    {
                        DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                        if (dateTimePicker1.Value.Date == DateTime.Now.Date)
                        {
                            if (MessageBox.Show("Please confirm the selected date..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            {
                                dateTimePicker1.Focus();
                                goto End;
                            }
                        }
                        if (FTSSettings.IsEntryValidationAgainstMusterEmpCount() && MChit.IsEmpHeadCountExceed(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()), Convert.ToInt32(txtNoOfEmployees.Text)))
                        {
                            MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:" + txtNoOfEmployees.Text.ToString() + "\r\n Already Entered Count:" + txtAvailableEmpCount.Text);
                            goto End;
                        }
                        decAvailCashMandays = DHarvest.GetRubberEmployeeAvailableCashManDays(DHarvestRubber.DtHarvestDate, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        if (decAvailCashMandays >= 1)
                        {
                            if (MessageBox.Show("Employee:" + txtEmpNo.Text + " Already Has " + decAvailCashMandays + " Cash ManDay(s),\r\n\r\n Do You Want To Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (MessageBox.Show("You are going to enter more than " + decAvailCashMandays + " Cash Manday(s) to Emp:" + txtEmpNo.Text, "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    //goto End;
                                    txtEmpNo.Focus();
                                    goto End;
                                }
                            }
                            else
                            {
                                //goto End;
                                txtEmpNo.Focus();
                                goto End;
                            }
                        }
                        DHarvestRubber.IntCWType = Convert.ToInt32(cmbCWWorkType.SelectedValue.ToString());
                        if (chkHoliday.Checked)
                        {
                            DHarvestRubber.BoolHolidayYesNo = true;
                            DHarvestRubber.FlHoliManDays = float.Parse(cmbHoliManDays.Text);
                            DHarvestRubber.BoolPaidHolidayYesNo = false;
                        }
                        else if (chkPaidHoliday.Checked)
                        {
                            DHarvestRubber.BoolPaidHolidayYesNo = true;
                            DHarvestRubber.BoolHolidayYesNo = false;
                        }
                        else
                        {
                            DHarvestRubber.BoolHolidayYesNo = false;
                            int fHoliManDays = 0;
                            DHarvestRubber.FlHoliManDays = (float)fHoliManDays;
                        }

                        if (chkPaidHoliday.Checked)
                        {
                            DHarvestRubber.BoolPaidHolidayYesNo = true;
                        }
                        else
                        {
                            DHarvestRubber.BoolPaidHolidayYesNo = false;

                        }

                        DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();
                        DHarvestRubber.StrField = cmbField.SelectedValue.ToString();

                        //DHarvest.StrBlock=cmbBlock.SelectedValue.ToString();
                        DHarvestRubber.StrBlock = "NA";
                        //DHarvestRubber.FieldCropType = Convert.ToInt32(cmbFieldCropType.SelectedValue.ToString());
                        DHarvestRubber.FieldCropType = 1;
                        //DHarvest.StrCategory = cmbCategory.SelectedItem.ToString();
                        if (rbtnGeneral.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnGeneral.Text.ToString();
                        }
                        if (rbtnLentLabour.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnLentLabour.Text.ToString();
                            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            DHarvestRubber.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();

                            //cmbLabourDivision_SelectedIndexChanged(null, null);
                            DHarvestRubber.StrLabourField = cmbLabourField.SelectedValue.ToString();
                        }
                        if (rbtnInterEstate.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnInterEstate.Text.ToString();
                            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                        }
                        DHarvestRubber.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
                        DHarvestRubber.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
                        DHarvestRubber.StrEmpNo = txtEmpNo.Text;
                        //DHarvest.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
                        DHarvestRubber.StrEmpName = txtEmpName.Text;

                        if (cmbJobCode.SelectedValue != "")
                        {
                            DHarvestRubber.StrJob = (cmbJobCode.SelectedValue.ToString()).ToUpper();
                        }
                        else
                        {
                            MessageBox.Show("Job Code can't be empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto End;
                        }
                        //no pri for cashworkers
                        DHarvestRubber.BoolTaskCompletedYesNo = false;
                        DHarvestRubber.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
                        //mandays for holidays need to be implemented.......
                        if (chkHoliday.Checked)
                        {
                            if (DHarvestRubber.IntFullHalf == 2)
                            {
                                DHarvestRubber.FlManDays = (float)(DHarvestRubber.FlHoliManDays);
                            }
                            else if (DHarvestRubber.IntFullHalf == 1)
                            {
                                double mValue = 0.5;
                                DHarvestRubber.FlManDays = (float)mValue;
                            }
                        }
                        else if (!chkHoliday.Checked)
                        {
                            DHarvestRubber.FlManDays = (float)(DHarvestRubber.IntFullHalf / 2.0);
                        }
                        if (DHarvestRubber.StrJob == "TAP")
                        {
                            if (txtScrapQty.Text == "")
                            {
                                txtScrapQty.Text = "0.00";
                            }
                            if (txtOverKilos.Text == "")
                            {
                                txtOverKilos.Text = "0";
                            }
                            DHarvestRubber.FlQty = float.Parse(txtQty.Text);
                            DHarvestRubber.FlOKgs = float.Parse(txtOverKilos.Text);
                            DHarvestRubber.FlScrapQty = float.Parse(txtScrapQty.Text);
                        }
                        else
                        {
                            DHarvestRubber.FlQty = 0;
                            DHarvestRubber.FlOKgs = 0;
                            DHarvestRubber.FlScrapQty = 0;
                        }
                        //if (this.txtJobShortName.Text.ToUpper().Equals("TAP"))
                        //{
                        //    //DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), txtJobShortName.Text.ToUpper(), cmbField.SelectedValue.ToString()).ToString());                
                        //}
                        //else
                        //{
                        //    //DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "Other", cmbField.SelectedValue.ToString()).ToString());
                        //}
                        if (cmbCWWorkType.SelectedValue.ToString() == "1")
                        {
                            if (this.cmbJobCode.SelectedValue.ToString().ToUpper() != "TAP")
                            {
                                MessageBox.Show("This WorkCode Not Valid..!", "Warning.!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cmbCWWorkType.Focus();
                                goto End;
                            }
                        }
                        if (cmbCWWorkType.SelectedValue.ToString() == "2")
                        {
                            if (!this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                            {
                                MessageBox.Show("This WorkCode Not Valid..!", "Warning.!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cmbCWWorkType.Focus();
                                goto End;
                            }
                        }
                        if (cmbCWWorkType.SelectedValue.ToString() == "3")
                        {
                            if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                            {
                                MessageBox.Show("This WorkCode Not Valid..!", "Warning.!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cmbJobCode.Focus();
                                goto End;
                            }
                        }
                        if (cmbCWWorkType.SelectedValue.ToString() == "4")
                        {
                            if (!this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                            {
                                MessageBox.Show("This WorkCode Not Valid..!", "Warning.!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cmbJobCode.Focus();
                                goto End;
                            }
                        }
                        if (cmbCWWorkType.SelectedValue.ToString() == "5")
                        {
                            //if (!this.txtJobShortName.Text.ToUpper().Equals("TAP"))
                            //{
                            //    MessageBox.Show("This WorkCode Not Valid..!", "Warning.!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtJobShortName.Focus();
                            //    goto End;
                            //}
                        }

                        if (cmbCWWorkType.Text == "Double Tapping")
                        {
                            cmbJobCode.Enabled = false;
                            cmbJobCode.SelectedValue = "TAP";
                        }
                        else if (cmbCWWorkType.Text == "Cash Tapping")
                        {
                            cmbJobCode.Enabled = false;
                            cmbJobCode.SelectedValue = "TAP";
                        }
                        else if (cmbCWWorkType.Text == "Cash Work")
                        {
                            //cmbJobCode.Enabled = false;
                            txtQty.Enabled = false;
                            txtScrapQty.Enabled = false;
                            txtScrapQty.Text = "0.00";
                            txtQty.Text = "0.00";
                            ////txtJobShortName.Clear();
                        }
                        else if (cmbCWWorkType.Text == "Contract Tapping")
                        {
                            cmbJobCode.Enabled = false;
                            cmbJobCode.SelectedValue = "TAP";
                        }
                        else if (cmbCWWorkType.Text == "Contract Name")
                        {
                            cmbJobCode.Enabled = false;
                        }

                        if (txtAreaCovered.Text.Trim() != "")
                        {
                            DHarvestRubber.DecAreaCovered = Convert.ToDecimal(txtAreaCovered.Text);
                        }
                        else
                        {
                            DHarvestRubber.DecAreaCovered = 0;
                        }

                        //DHarvestRubber.FlNorm = float.Parse(txtNorm.Text);
                        //DHarvestRubber.FlHours=txtHours.Text;
                        DHarvestRubber.StrUserId = FTSPayRollBL.User.StrUserName;
                        if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                        {
                            MessageBox.Show("Please Enter a Correct Job Code.");
                            cmbJobCode.SelectedValue = "";
                            cmbJobCode.Focus();
                            goto End;
                        }
                        else if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP") && String.IsNullOrEmpty(txtQty.Text))
                        {
                            MessageBox.Show("QTY Cannot Be Empty!");

                        }
                        else if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP") && Convert.ToDecimal(txtQty.Text) == 0)
                        {
                            MessageBox.Show("Qty Cannot Be 0");
                        }

                        else
                        {
                            if (cmbTapType.SelectedIndex > -1)
                            {
                                DHarvestRubber.IntTappingType = Convert.ToInt32(cmbTapType.SelectedValue.ToString());
                            }
                            else
                            {
                                DHarvestRubber.IntTappingType = 1;
                            }
                            DHarvestRubber.IntPRINorm = 0;
                            DHarvestRubber.StrACCode = "00";
                            DHarvestRubber.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                            DHarvestRubber.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                            try
                            {

                                status = DHarvestRubber.InsertHarvetEntry();

                                //if ADD button clicked else UDPATE button clicked
                                if (e != null)
                                {
                                    if (status.Equals("ADDED"))
                                    {
                                        MessageBox.Show("Daily Harvest Entry Added Successfully! ");
                                        AfterAdd();
                                    }

                                    else if (status.Equals("EXISTS"))
                                    {
                                        MessageBox.Show("Already Exists");
                                        AfterAdd();
                                    }
                                    else if (status.Equals("UPDATED"))
                                    {
                                        MessageBox.Show("Daily Harvest Entry updated Successfully! ");
                                        AfterAdd();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Oops, something went wrong!");
                                        AfterAdd();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Daily Harvest Entry Updated Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    AfterAdd();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error, " + ex.Message);
                                btnCancel.PerformClick();
                            }
                            txtQty.Enabled = true;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Enter Monthly Holidays to procceed DailyHarvest Entries...!","Message ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            //    this.Close();
                            //}            
                        }
                    }
                    else
                    {
                        MessageBox.Show("You Have Not Entered Previous Day Entries!\r\nPlease Enter " + dateTimePicker1.Value.Date.AddDays(-1).ToShortDateString() + " Day     Entries To Proceed ", "Entries Blocked");
                        dateTimePicker1.Focus();
                    }
                }
                else
                    goto End;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..! " + ex.Message);
            }

        End:
            Application.DoEvents();
        }


        private void AfterAdd()
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            txtQty.Clear();
            txtScrapQty.Clear();
            txtAreaCovered.Clear();
            txtOverKilos.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
            txtEmpNo.Focus();

            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                refreshSummaryDetails();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //call delete first and then add entry again
            //btnDelete_Click(null, null);

            DHarvestRubber.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
            //DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

            try
            {
                String status = DHarvestRubber.DeleteHarvetEntry();

                if (e != null)
                {
                    if (status.Equals("DELETED"))
                    {
                        //MessageBox.Show("Daily Harvest Entry Deleted Successfully! ");
                        btnAdd_Click(null, null);
                    }
                    else if (status.Equals("NOTEXISTS"))
                    {
                        MessageBox.Show("Not Exists");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }

            


            //commented on 2013/04/24
            //try
            //{
            //    DateChanged();

            //    if (!String.IsNullOrEmpty(lblRefNo.Text))
            //    {
            //        DHarvestRubber.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
            //        DHarvestRubber.IntCWType = Convert.ToInt32(cmbCWWorkType.SelectedValue.ToString());
            //        DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
            //        if (chkHoliday.Checked)
            //            DHarvestRubber.BoolHolidayYesNo = true;
            //        else DHarvestRubber.BoolHolidayYesNo = false;
            //        DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();
            //        DHarvestRubber.StrField = cmbField.SelectedValue.ToString();
            //        //DHarvestRubber.StrBlock=cmbBlock.SelectedItem.ToString();
            //        DHarvestRubber.StrBlock = "NA";
            //        DHarvestRubber.StrCategory = cmbCategory.SelectedItem.ToString();
            //        if (rbtnGeneral.Checked)
            //        {
            //            DHarvestRubber.StrLabourType = rbtnGeneral.Text.ToString();
            //        }
            //        if (rbtnLentLabour.Checked)
            //        {
            //            DHarvestRubber.StrLabourType = rbtnLentLabour.Text.ToString();
            //            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
            //            DHarvestRubber.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
            //            DHarvestRubber.StrLabourField = cmbLabourField.SelectedValue.ToString();
            //        }
            //        if (rbtnInterEstate.Checked)
            //        {
            //            DHarvestRubber.StrLabourType = rbtnInterEstate.Text.ToString();
            //        }
            //        DHarvest.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
            //        DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
            //        DHarvest.StrEmpNo = txtEmpNo.Text;
            //        //DHarvest.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
            //        DHarvest.StrEmpName = txtEmpName.Text;
            //        DHarvest.StrJob = txtJobShortName.Text;
            //        if (chkTaskCompleted.Checked) DHarvest.BoolTaskCompletedYesNo = true;
            //        else DHarvest.BoolTaskCompletedYesNo = false;
            //        DHarvest.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
            //        //mandays for holidays need to be implemented.......
            //        DHarvest.FlManDays = (float)(DHarvest.IntFullHalf / 2.0);
            //        if (DHarvest.StrJob == "TAP")
            //        {
            //            DHarvest.FlQty = float.Parse(txtQty.Text);
            //            DHarvest.FlOKgs = float.Parse(txtOverKilos.Text);
            //            DHarvest.FlScrapQty = float.Parse(txtScrapQty.Text);
            //        }
            //        else
            //        {
            //            DHarvest.FlQty = 0;
            //            DHarvest.FlOKgs = 0;
            //            DHarvest.FlScrapQty = 0;
            //        }
            //        if (this.txtJobShortName.Text.ToUpper().Equals("PLK"))
            //        {
            //            //DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), txtJobShortName.Text.ToUpper(), cmbField.SelectedValue.ToString()).ToString());
            //        }
            //        else
            //        {
            //            //DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "Other", cmbField.SelectedValue.ToString()).ToString());
            //        }
            //        //DHarvest.FlHours=txtHours.Text;
            //        DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
            //        try
            //        {
            //            String status = DHarvest.UpdateHarvetEntry();


            //            if (status.Equals("UPDATED"))
            //            {
            //                MessageBox.Show("Daily Harvest Entry Updated Successfully! ");
            //                btnCancel.PerformClick();
            //            }
            //            else if (status.Equals("NOTEXISTS"))
            //            {
            //                MessageBox.Show("Not Exists");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Error, ", ex.Message);
            //        }

            //    }
            //    else
            //        MessageBox.Show("Please Select Data Before Update");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error, " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void cmbLabourDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.cmbLabourDivision.SelectedItem.ToString().Equals(""))
                {
                    cmbLabourField.DataSource = EstDivBlock.ListDivisionFields(cmbLabourDivision.SelectedValue.ToString().ToString());
                    cmbLabourField.DisplayMember = "FieldID";
                    cmbLabourField.ValueMember = "FieldID";

                }
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                DHarvestRubber.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
                //DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

                try
                {
                    String status = DHarvestRubber.DeleteHarvetEntry();

                    if (e != null)
                    {
                        if (status.Equals("DELETED"))
                        {
                            MessageBox.Show("Daily Harvest Entry Deleted Successfully! ");
                        }
                        else if (status.Equals("NOTEXISTS"))
                        {
                            MessageBox.Show("Not Exists");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
            else
                MessageBox.Show("Please Select Data Before Delete");

            if (e != null)
                btnCancel.PerformClick();


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            cmbJobCode.SelectedValue = "";
            txtJobName.Text = "";
            txtQty.Clear();
            txtScrapQty.Clear();
            txtOverKilos.Clear();
            txtAreaCovered.Clear();
            txtQty.Enabled = true;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;


            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                txtEmpNo.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DailyHarverstRubberCashWorck_Load(object sender, EventArgs e)
        {
            lblRefNo.Visible = false;
            cmbChitNumber.Enabled = false;

            DHarvestRubber.BoolFormLoad = true;

            cmbHoliManDays.Enabled = false;
            cmbEstate.DataSource = EstDivBlock.ListEstates();
            cmbEstate.DisplayMember = "EstateName";
            cmbEstate.ValueMember = "EstateID";

            //cmbDivision.SelectedIndexChanged -= cmbDivision_SelectedIndexChanged;
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            //cmbDivision.SelectedIndexChanged += cmbDivision_SelectedIndexChanged;


            cmbFullHalf.DataSource = FTSSettings.ListDataFromSettings("FullHalfType");
            cmbFullHalf.DisplayMember = "Name";
            cmbFullHalf.ValueMember = "Code";
            cmbFullHalf.SelectedIndex = 1;

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType", "Rubber");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            cmbCategory.DataSource = EmpCat.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            cmbJob.DataSource = Job1.ListJobs();
            cmbJob.DisplayMember = "JobShortName";
            cmbJob.ValueMember = "JobShortName";

            cmbCWWorkType.DataSource = FTSSettings.ListDataFromSettings("Cash Work");
            cmbCWWorkType.DisplayMember = "Name";
            cmbCWWorkType.ValueMember = "Code";

            cmbFieldCropType.DataSource = FTSSettings.GetFieldCropType().Tables[0];
            cmbFieldCropType.DisplayMember = "Name";
            cmbFieldCropType.ValueMember = "Code";

            cmbTapType.DataSource = FTSSettings.ListDataFromSettings("TappingType");
            cmbTapType.DisplayMember = "Name";
            cmbTapType.ValueMember = "Code";

            chkTaskCompleted.Enabled = false;
            rbtnGeneral.Checked = true;
            dateTimePicker1.Focus();

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            cmbDivision.Text = FTSPayRollBL.User.StrDivision;

            cmbDivision_SelectedIndexChanged(null, null);

            DHarvestRubber.BoolFormLoad = false;

            chkHoliday.Enabled = false;
            chkPaidHoliday.Enabled = false;

            cmbCropType.SelectedValue = 2;
            try
            {
                dateTimePicker1_ValueChanged(null, null);
            }
            catch { }


        }

        private void cmdCloseEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure you Want to Close " + dateTimePicker1.Value.Date.ToShortDateString() + " Day Entries?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //        DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                    //        DHarvestRubber.StrUserId = FTSPayRollBL.User.StrUserName;
                    //        String state = DHarvestRubber.CloseDayEntries();
                    //        if (state.Equals("CLOSED"))
                    //        {
                    //            //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntries(dateTimePicker1.Value.Date);
                    //            gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                    //            MessageBox.Show("Day Harvest Entries Closed Successfully!");
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Error!");
                    //        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbWorkType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbCWWorkType.Text == "Double Tapping")
                {
                    cmbJobCode.Enabled = false;
                    cmbJobCode.SelectedValue = "TAP";
                    txtQty.Enabled = true;
                    txtScrapQty.Enabled = true;
                    cmbField.Focus();
                }
                else if (cmbCWWorkType.Text == "Cash Tapping")
                {
                    cmbJobCode.Enabled = false;
                    cmbJobCode.SelectedValue = "TAP";
                    txtQty.Enabled = true;
                    txtScrapQty.Enabled = true;
                    cmbField.Focus();
                }
                else if (cmbCWWorkType.Text == "Cash Work")
                {
                    cmbJobCode.Enabled = false;
                    txtQty.Enabled = false;
                    txtScrapQty.Enabled = false;
                    txtScrapQty.Text = "0.00";
                    txtQty.Text = "0.00";
                    cmbField.Focus();
                }
                else if (cmbCWWorkType.Text == "Contract Tapping")
                {
                    cmbJobCode.Enabled = false;
                    cmbJobCode.SelectedValue = "TAP";
                    txtQty.Enabled = true;
                    txtScrapQty.Enabled = true;
                    cmbField.Focus();
                }
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbDivision.Text.Equals(FTSPayRollBL.User.StrDivision))
            {
                if (DHarvestRubber.BoolFormLoad == false)
                {
                    if (MessageBox.Show("Do you want to proceed with " + cmbDivision.Text + " Division...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            if (cmbDivision.SelectedIndex>-1)
                            {
                                //cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                                //cmbField.DisplayMember = "FieldID";
                                //cmbField.ValueMember = "FieldID";

                                //cmbField_SelectedIndexChanged(null, null);

                            }
                            FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                            gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        }
                        catch { }
                    }
                    else
                    {
                        cmbDivision.Text = FTSPayRollBL.User.StrDivision;
                    }
                }
                else
                {
                    try
                    {
                        if (!cmbDivision.SelectedItem.ToString().Equals(""))
                        {
                            //cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                            //cmbField.DisplayMember = "FieldID";
                            //cmbField.ValueMember = "FieldID";

                            //cmbField_SelectedIndexChanged(null, null);
                        }
                        FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                        gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    if (!String.IsNullOrEmpty(cmbDivision.Text))
                    {
                        //cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                        //cmbField.DisplayMember = "FieldID";
                        //cmbField.ValueMember = "FieldID";

                        //cmbField_SelectedIndexChanged(null, null);
                    }
                    FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                    gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                }
                catch { }
            }
            dateTimePicker1_ValueChanged(null, null);
        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void chkPaidHoliday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaidHoliday.Checked)
            {
                //DataTable dtPH;
                //dtPH = DHarvestRubber.ListPHHarvestForDivision(dateTimePicker1.Value.Date, FTSPayRollBL.User.StrDivision);
                //if (dtPH.Rows.Count < 1)
                //{

                //    if (MessageBox.Show("Add Extra Names For Paid Holiday?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //    {
                //        this.Close();
                //        AddExtraNames AddExNames = new AddExtraNames();
                //        AddExNames.Show();
                //    }
                //    else
                //    {

                //    }
                //}
            }
        }

        private void chkHoliday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHoliday.Checked)
            {
                DHarvestRubber.BoolHolidayYesNo = true;
            }
            else
            {
                DHarvestRubber.BoolHolidayYesNo = false;
            }
        }

        private void cmbCWWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldName.Text = "";


            //cmbBlock.DataSource = myDiv.ListBlocks(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
            //cmbBlock.DisplayMember = "BlockID";
            //cmbBlock.ValueMember = "BlockID";
        }

        private void cmbFieldCropType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                rbtnGeneral.Focus();
            }
        }
        private void validateJobAndCWType()
        {
            try
            {
                if (cmbCWWorkType.Text == "Double Tapping")
                {
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        txtQty.Enabled = true;
                        txtScrapQty.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Muster Chit Job Is Invalid ");
                        cmbCWWorkType.Focus();
                    }

                }
                else if (cmbCWWorkType.Text == "Cash Tapping")
                {
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        txtQty.Enabled = true;
                        txtScrapQty.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Muster Chit Job Is Invalid ");
                        cmbCWWorkType.Focus();
                    }

                }
                else if (cmbCWWorkType.Text == "Cash Work")
                {
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        MessageBox.Show("Muster Chit Job Is Invalid ");
                        cmbCWWorkType.Focus();
                    }
                    else
                    {
                        txtQty.Enabled = false;
                        txtScrapQty.Enabled = false;
                        txtScrapQty.Text = "0.00";
                        txtQty.Text = "0.00";
                    }
                }
                else if (cmbCWWorkType.Text == "Contract Tapping")
                {
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        txtQty.Enabled = true;
                        txtScrapQty.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Muster Chit Job Is Invalid ");
                        cmbCWWorkType.Focus();
                    }
                }
                else if (cmbCWWorkType.Text == "Contract Name")
                {
                    txtQty.Enabled = true;
                    txtScrapQty.Enabled = true;
                }
            }
            catch { }
        }

        private void cmbBlock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataSet ds = new DataSet();
                ds = myDiv.GetBlockName(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString(), cmbBlock.SelectedValue.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtBlockName.Text = ds.Tables[0].Rows[0][0].ToString();
                    rbtnGeneral.Focus();

                }
                else
                {
                    MessageBox.Show("No Block Found..!");
                    cmbBlock.Focus();
                }
            }
        }

        private void rbtnGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGeneral.Checked)
            {
                cmbLabourField.Enabled = false;
                cmbLabourDivision.Enabled = false;
                cmbLabourEstate.Enabled = false;
                DHarvestRubber.StrLabourEstate = "NA";
                DHarvestRubber.StrLabourDivision = "NA";
                DHarvestRubber.StrLabourField = "NA";
            }
        }

        private void rbtnGeneral_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void rbtnLentLabour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourDivision.Focus();
            }
        }

        private void rbtnLentLabour_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLentLabour.Checked)
            {
                cmbField.Enabled = false;
                cmbLabourField.Enabled = true;
                cmbLabourDivision.Enabled = true;
                cmbLabourEstate.Enabled = true;

                cmbLabourEstate.DataSource = EstDivBlock.ListEstates();
                cmbLabourEstate.DisplayMember = "EstateName";
                cmbLabourEstate.ValueMember = "EstateID";

                cmbLabourDivision.DataSource = EstDivBlock.ListEstateDivisions();
                cmbLabourDivision.DisplayMember = "DivisionID";
                cmbLabourDivision.ValueMember = "DivisionID";

                cmbLabourEstate.Text = FTSPayRollBL.User.StrEstate;



                cmbLabourDivision_SelectedIndexChanged(null, null);

            }
            else
            {
                cmbField.Enabled = true;
                cmbLabourField.Enabled = false;
                cmbLabourDivision.Enabled = false;
                cmbLabourEstate.Enabled = false;
            }
        }

        private void rbtnInterEstate_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnInterEstate.Checked)
            //{
            //    cmbLabourField.Enabled = false;
            //    cmbLabourDivision.Enabled = false;
            //    cmbLabourEstate.Enabled = true;

            //    cmbLabourEstate.DataSource = EstDivBlock.ListOtherEstates();
            //    cmbLabourEstate.DisplayMember = "EstateName";
            //    cmbLabourEstate.ValueMember = "EstateID";

            //    //DHarvestRubber.StrLabourEstate = "NA";
            //    DHarvestRubber.StrLabourDivision = "NA";
            //    DHarvestRubber.StrLabourField = "NA";
            //}
            if (rbtnInterEstate.Checked)
            {
                /*LOAD LENT ESTATE DATA*/
                cmbLabourField.Enabled = true;
                cmbLabourDivision.Enabled = true;
                cmbLabourEstate.Enabled = true;

                cmbLabourEstate.DataSource = EstDivBlock.ListOtherEstates();
                cmbLabourEstate.DisplayMember = "EstateName";
                cmbLabourEstate.ValueMember = "EstateID";

                //cmbLabourEstate.SelectedIndex = -1;


            }
        }

        private void rbtnInterEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourEstate.Focus();
            }
        }

        private void cmbLabourEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rbtnInterEstate.Checked)
                {
                    txtEmpNo.Focus();
                }
                else if (rbtnLentLabour.Checked)
                {
                    cmbLabourDivision.Focus();
                }

            }
        }

        private void cmbLabourDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourField.Focus();
            }
        }

        private void cmbLabourDivision_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            try
            {
                if (rbtnInterEstate.Checked)
                {
                    cmbLabourField.DataSource = EstDivBlock.ListOtherDivisionFields(cmbLabourDivision.SelectedValue.ToString());
                    cmbLabourField.DisplayMember = "FieldId";
                    cmbLabourField.ValueMember = "FieldId";
                }
                else
                {
                    if (!this.cmbLabourDivision.SelectedItem.ToString().Equals(""))
                    {
                        cmbLabourField.DataSource = EstDivBlock.ListDivisionFields(cmbLabourDivision.SelectedValue.ToString());
                        cmbLabourField.DisplayMember = "FieldID";
                        cmbLabourField.ValueMember = "FieldID";

                    }
                }
            }
            catch { }
        }

        private void cmbLabourEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLabourEstate.SelectedIndex > -1)
            {
                if (rbtnInterEstate.Checked)
                {
                    if (EstDivBlock.ListActiveOtherDivisions(cmbLabourEstate.SelectedValue.ToString()).Rows.Count > 0)
                    {
                        //DownloadBorrowingEstateDivisionFields();

                        cmbLabourDivision.DataSource = EstDivBlock.ListActiveOtherDivisions(cmbLabourEstate.SelectedValue.ToString());
                        cmbLabourDivision.DisplayMember = "DivisionID";
                        cmbLabourDivision.ValueMember = "DivisionID";

                    }
                    else
                    {
                        MessageBox.Show("You Must Download Borrowing Estate Division Fields To Proceed", "No Divisions Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DownloadBorrowingEstateDivisionFields();
                    }
                }
            }
        }

        private void cmbLabourField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList(this, cmbDivision.SelectedValue.ToString());
                    empList.Show();
                }
                else
                {
                    if (txtEmpNo.Text.Trim() != "")
                    {
                        if (e.KeyChar == 13)
                        {
                            txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                            txtEmpNo_Leave(null, null);
                            if (e.KeyChar == 13)
                            {
                                txtEmpNo_LeaveChanged();
                            }
                        }
                    }
                }

            }


        }
        private void txtEmpNo_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txtEmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
                else
                {
                    if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                    {
                        EmpMaster.StrGender = EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                        {
                            txtQty.Focus();
                        }
                        else
                        {
                            btnAdd.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                        txtEmpNo.Text = "";
                        txtEmpNo.Focus();
                    }
                    cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    //cmbJobCode.Focus();
                    

                }
            }

        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Trim() != "")
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList();
                    empList.Show();
                }
                else
                {
                    txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                    txtEmpNo_LeaveChanged();
                }
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            EmployeeList empList = new EmployeeList();
            empList.Show();
        }

        private void txtJobShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (cmbJobCode.SelectedValue.ToString().Equals("?"))
                    {
                        JobList myJob = new JobList();
                        myJob.Show();
                    }
                    else
                    {
                        if (cmbJobCode.SelectedValue != "")
                        {
                            if (rbtnGeneral.Checked == true && rbtnInterEstate.Checked == false && rbtnLentLabour.Checked == false)
                            {
                                DataTable dt = myFieldType.ListAllFieldsTypes(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
                                DataTable dt1 = myFieldType.GetWorkCodeType(cmbJobCode.SelectedValue.ToString());


                                if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                                {
                                    cmbJobCode.SelectedValue.ToString().ToUpper();
                                    txtJobShortName_LeaveChanged();
                                }
                                else if (cmbJobCode.SelectedValue.ToString().Substring(0, 1).ToUpper() == "X")
                                {
                                    cmbJobCode.SelectedValue.ToString().ToUpper();
                                    txtJobShortName_LeaveChanged();
                                }
                                else
                                {
                                    MessageBox.Show("Oops... Field Type and WorkCode type should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cmbJobCode.Focus();
                                }
                                dt.Dispose();
                                dt1.Dispose();
                            }
                            else if (rbtnGeneral.Checked == false && rbtnInterEstate.Checked == false && rbtnLentLabour.Checked == true)
                            {
                                DataTable dt = myFieldType.ListAllFieldsTypes(cmbLabourDivision.SelectedValue.ToString(), cmbLabourField.SelectedValue.ToString());
                                DataTable dt1 = myFieldType.GetWorkCodeType(cmbJobCode.SelectedValue.ToString());


                                if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                                {
                                    cmbJobCode.SelectedValue.ToString().ToUpper();
                                    txtJobShortName_LeaveChanged();
                                }
                                else if (cmbJobCode.SelectedValue.ToString().Substring(0, 1).ToUpper() == "X")
                                {
                                    cmbJobCode.SelectedValue.ToString().ToUpper();
                                    txtJobShortName_LeaveChanged();
                                }
                                else
                                {
                                    MessageBox.Show("Oops... Field Type and WorkCode type should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cmbJobCode.Focus();
                                }
                            }
                            else
                            {
                                cmbJobCode.SelectedValue.ToString().ToUpper();
                                txtJobShortName_LeaveChanged();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Oops... No Workcode Found..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmbJobCode.Focus();

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something Wrong in a Workcode..!" + ex.Message);
                }
            }
        }

        private void txtJobShortName_Leave(object sender, EventArgs e)
        {
            IsFieldandWorkcodeMatch();
        }

        private void btnJobSearch_Click(object sender, EventArgs e)
        {
            FTSPayroll.JobList myJobList = new FTSPayroll.JobList();
            myJobList.Show();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtQty.Text.Trim() == "")
                {
                    MessageBox.Show("Quantity can't be empty..!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Decimal normVal = 0;
                    Decimal Qtyval = 0;
                    Decimal OverKgs = 0;
                    //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.SelectedItem.ToString()));

                    Qtyval = Convert.ToDecimal(txtQty.Text);
                    //if ((Qtyval - normVal) > 0)
                    //{
                    //    OverKgs = Qtyval - normVal;
                    //    txtOverKilos.Text = "0";
                    //}
                    //else
                    //{
                    //    txtOverKilos.Text = "0";
                    //}
                    txtScrapQty.Focus();                   
                    
                }
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            Decimal normVal = 0;
            Decimal Qtyval = 0;
            Decimal OverKgs = 0;
            //txt_IsNum(txtQty.Text);
            if (!String.IsNullOrEmpty(txtQty.Text))
            {
                string context = this.txtQty.Text;
                if (float.Parse(context) > 10000.00)
                {
                    MessageBox.Show("Qty Value Should be less than 4 digits!");
                    txtQty.Clear();
                    txtQty.Focus();
                }

                //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.SelectedItem.ToString()));
                //Qtyval = Convert.ToDecimal(txtQty.Text);
                //if ((Qtyval - normVal) > 0)
                //{
                //    OverKgs = Qtyval - normVal;
                //    txtOverKilos.Text = "0";
                //}
                //else
                //{
                //    txtOverKilos.Text = "0";
                //}
                //txtScrapQty.Focus();
                if (Convert.ToInt32(cmbCWWorkType.SelectedValue.ToString()) == 5)
                {
                    //txtNorm.Focus();
                }
                else
                {
                    //txtNorm.Text = "0";
                    txtScrapQty.Focus();
                }
            }
        }

        private void txtScrapQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        private void txtAreaCovered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        private void gvDailyHarvest_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEmpNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtOverKilos.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbLabourField.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[6].Value.ToString();

            lblRefNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();

            DataTable gridDt = DHarvestRubber.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString()));
            //dateTimePicker1.Value = Convert.ToDateTime(gridDt.Rows[0][1].ToString());
            //cmbEstate.Text = gridDt.Rows[0][2].ToString();
            //cmbDivision.Text = gridDt.Rows[0][8].ToString();
            cmbCropType.Text = gridDt.Rows[0][6].ToString();
            cmbWorkType.Text = gridDt.Rows[0][7].ToString();
            cmbHoliManDays.Text = gridDt.Rows[0][17].ToString();
            txtScrapQty.Text = gridDt.Rows[0][19].ToString();
            cmbCWWorkType.SelectedValue = gridDt.Rows[0][20].ToString();
            txtAreaCovered.Text = gridDt.Rows[0][21].ToString();
            cmbFieldCropType.SelectedValue = Convert.ToInt32(gridDt.Rows[0][22].ToString());

            if (gridDt.Rows[0][4].ToString() == "True")
            {
                chkHoliday.Checked = true;
            }
            else
            {
                chkHoliday.Checked = false;
            }
            if (gridDt.Rows[0][5].ToString() == "General")
            {
                rbtnGeneral.Checked = true;
            }
            else if (gridDt.Rows[0][5].ToString() == "Lent Labour")
            {
                rbtnLentLabour.Checked = true;
                cmbLabourEstate.SelectedValue = gridDt.Rows[0][14].ToString();
                cmbLabourDivision.SelectedValue = gridDt.Rows[0][15].ToString();
                cmbLabourDivision_SelectedIndexChanged(null, null);
                cmbLabourField.SelectedValue = gridDt.Rows[0][16].ToString();
            }
            else if (gridDt.Rows[0][5].ToString() == "Inter Estate Lent Labour")
            {
                rbtnInterEstate.Checked = true;
            }
            //cmbCategory.Text = gridDt.Rows[0][18].ToString();
            cmbJobCode.SelectedValue = gridDt.Rows[0][10].ToString();
            //txtJobShortName_LeaveChanged();

            DataSet ds = EstDivBlock.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
            txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();

            if (gridDt.Rows[0][11].ToString() == "True")
            {
                chkTaskCompleted.Checked = true;
            }
            else
            {
                chkTaskCompleted.Checked = false;
            }



            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void cmbEstate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtJobShortName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateChanged();
        }

        private void cmbField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("?"))
            {
                FTSPayroll.JobList myJobList = new FTSPayroll.JobList();
                myJobList.Show();
            }
            else
            {
                if (e.KeyChar == 13)
                {
                    fieldChanged();
                }
            }
        }
        public void fieldChanged()
        {
            DataSet ds = new DataSet();
            ds = myField.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
            if (ds.Tables.Count > 0)
            {
                txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                //cmbBlock.Focus();
                rbtnGeneral.Focus();
                //cmbFieldCropType.Focus();
            }
            else
            {
                MessageBox.Show("Please Select a Valid Field", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbField.Focus();
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbHoliManDays.Text = "0.00";
            chkHoliday.Checked = false;
            chkPaidHoliday.Checked = false;

            if (e.KeyChar == 13)
            {
                //DateChanged();
                cmbCWWorkType.Focus();
            }
            else
            {
                dateTimePicker1.Focus();
            }

            //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));

        }

        private void cmbCWWorkType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void txtEmpNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtScrapQty.Focus();
            }
        }

        private void txtNorm_Leave(object sender, EventArgs e)
        {
            Decimal Qtyval = 0;
            Decimal normVal = 0;
            Decimal OverKgs = 0;

            if (cmbCWWorkType.SelectedValue.ToString() == "5")
            {
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    Qtyval = Convert.ToDecimal(txtQty.Text);
                    normVal = 0;// Convert.ToDecimal(txtNorm.Text);
                    if ((Qtyval - normVal) > 0)
                    {
                        OverKgs = Qtyval - normVal;
                        txtOverKilos.Text = OverKgs.ToString();
                    }
                    else
                    {
                        txtOverKilos.Text = "0";
                    }
                }
                else
                {
                    txtOverKilos.Text = "0";
                }
            }
            else
            {
                //txtNorm.Text = "0";
                txtOverKilos.Text = "0";
            }


        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            DateChanged();

            //if (FTSPayRollBL.User.StrUserName != "admin")
            //{
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                Boolean OpenedDate = false;
                String strDateOk = "";
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                strDateOk = myEntries.CheckDateDifference();
                //strDateOk = "OK";
                String UnblockedpastDates = "OK";// myEntries.IsPastDatesClosed(dateTimePicker1.Value.Date.AddDays(-2), cmbDivision.SelectedValue.ToString());
                if (!UnblockedpastDates.Equals("OK"))
                {
                    MessageBox.Show("Following Dates Need To Close Before Transfer Data To Head Office - " + UnblockedpastDates);
                    //this.Close();
                }
                if ((strDateOk.Equals("OK")))
                {
                    if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                    {
                        DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;

                        //cmbDivision_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Date Within the Month You Logged In");
                        dateTimePicker1.Focus();
                        //dateTimePicker1.Value = new DateTime(Convert.ToInt32(User.StrYear),YMonth.GetMonthIdByMonthName(User.StrMonth), 1);
                    }
                }
                else if (strDateOk.Equals("BLOCK"))
                {
                    MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                    myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                    myEntries.AddBlockDates();
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
            //}
        }

        private void cmbMusterChit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cmbField.SelectedValue = dtMusterData.Rows[0][1].ToString();
                
                cmbChitNumber.DataSource = MChit.ListChitNumbersForSelectedMuster(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()));
                cmbChitNumber.DisplayMember = "ChitNumber";
                cmbChitNumber.ValueMember = "ChitNumber";

                cmbGangNumber.DataSource = MChit.ListGangNumbersForSelectedMuster(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()));
                cmbGangNumber.DisplayMember = "GangNumber";
                cmbGangNumber.ValueMember = "GangNumber";

                cmbField.DataSource = MChit.ListFieldIdForSelectedMuster(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()));
                cmbField.DisplayMember = "FieldID";
                cmbField.ValueMember = "FieldID";


                DataTable dtMusterData = new DataTable();
                dtMusterData = MChit.GetMusterDetailsForSelectedMuster(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()));
                cmbChitNumber.SelectedValue = dtMusterData.Rows[0][1].ToString();
                cmbGangNumber.SelectedValue = dtMusterData.Rows[0][2].ToString();
                cmbJobCode.DataSource = Job1.ListJobMaster(dtMusterData.Rows[0][10].ToString());
                txtNoOfEmployees.Text = dtMusterData.Rows[0][11].ToString();
                cmbJobCode.DisplayMember = "JobShortName";
                cmbJobCode.ValueMember = "JobShortName";
                cmbJobCode.SelectedValue = dtMusterData.Rows[0][10].ToString();
                txtNoOfEmployees.Text = dtMusterData.Rows[0][11].ToString();
                txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
                
                cmbField.SelectedValue = dtMusterData.Rows[0][3].ToString();
                if (dtMusterData.Rows[0][6].ToString().Equals("General"))
                {
                    rbtnGeneral.Checked = true;
                    cmbLabourEstate.SelectedIndex = -1;
                    cmbLabourDivision.SelectedIndex = -1;
                    cmbLabourField.SelectedIndex = -1;
                }
                else if (dtMusterData.Rows[0][6].ToString().Equals("Lent Labour"))
                {
                    rbtnLentLabour.Checked = true;
                    cmbLabourEstate.SelectedValue = dtMusterData.Rows[0][7].ToString();
                    cmbLabourDivision.SelectedValue = dtMusterData.Rows[0][8].ToString();
                    cmbLabourField.SelectedValue = dtMusterData.Rows[0][9].ToString();
                }
                else
                {
                    rbtnInterEstate.Checked = true;
                    cmbLabourEstate.SelectedValue = dtMusterData.Rows[0][7].ToString();
                    cmbLabourEstate_SelectedIndexChanged(null, null);
                    cmbLabourDivision.SelectedValue = dtMusterData.Rows[0][8].ToString();
                    cmbLabourDivision_SelectedIndexChanged(null, null);
                    cmbLabourField.SelectedValue = dtMusterData.Rows[0][9].ToString();
                }
                gbLent.Enabled = false;
                fieldChanged();
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    cmbCWWorkType.Focus();
                }
                else
                {
                    cmbCWWorkType.SelectedValue = 3;
                    txtEmpNo.Focus();
                }
                

            }
            catch { }


        }
       

        private void cmbMusterChit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbChitNumber.Focus();
            }
        }

        private void cmbMusterChit_Leave(object sender, EventArgs e)
        {
            //DateChanged();
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            Boolean OpenedDate = false;
            String strDateOk = "";
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            /*Blocked for BPL*/
            if (FTSPayRollBL.User.BoolDayBlockAvailable)
            {
                strDateOk = myEntries.CheckDateDifference();
            }
            else
            {
                strDateOk = "OK";
            }
            //String UnblockedpastDates =  myEntries.IsPastDatesClosed(dateTimePicker1.Value.Date.AddDays(-2), cmbDivision.SelectedValue.ToString());

            //String UnblockedpastDates =  myEntries.IsPastDatesClosed(dateTimePicker1.Value.Date.AddDays(-2), cmbDivision.SelectedValue.ToString());
            //if (!UnblockedpastDates.Equals("OK"))
            //{
            //    MessageBox.Show("Following Dates Need To Close Before Transfer Data To Head Office - "+UnblockedpastDates);
            //    //this.Close();
            //}
            if ((strDateOk.Equals("OK")))
            {
                if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                {
                    DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;

                    //cmbDivision_SelectedIndexChanged(null, null);

                    //if (DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "PLK", dateTimePicker1.Value.Date) > 0 && DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "PLK", dateTimePicker1.Value.Date) > 0)
                    //{
                    //    txtMaleNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "PLK", dateTimePicker1.Value.Date).ToString();
                    //    txtNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "PLK", dateTimePicker1.Value.Date).ToString();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please Add Norm Value For This Division And Proceed.");
                    //    this.Close();
                    //}
                }
                else
                {
                    MessageBox.Show("Please Select a Date Within the Month You Logged In");
                    dateTimePicker1.Focus();
                    //dateTimePicker1.Value = new DateTime(Convert.ToInt32(User.StrYear),YMonth.GetMonthIdByMonthName(User.StrMonth), 1);
                }
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                myEntries.AddBlockDates();
                dateTimePicker1.Focus();
            }
            else
            {
                MessageBox.Show("This Date Data Entries Are Blocked Now, Please Contact Head Office For Date Release.");
                this.Close();
            }
        }

        private void refreshSummaryDetails()
        {
            dtDaySummary = DHarvestRubber.GetDaySummary(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false);
            txtSumPlkNames.Text = dtDaySummary.Rows[0][0].ToString();
            txtSumSunNames.Text = dtDaySummary.Rows[0][2].ToString();
            txtSumKilos.Text = dtDaySummary.Rows[0][3].ToString();
            txtSumOverKgs.Text = dtDaySummary.Rows[0][4].ToString();
            txtSumScraps.Text = dtDaySummary.Rows[0][5].ToString();
        }

        private void cmbCWWorkType_Leave(object sender, EventArgs e)
        {
            validateJobAndCWType();
        }
        private void DownloadBorrowingEstateDivisionFields()
        {
            if (MessageBox.Show("Do you want to Download And Refresh, " + cmbLabourEstate.SelectedValue.ToString() + " Estate Divisions and Fields  ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (MessageBox.Show("Are You Sure That Your Internet Connection Is Connected", "Connection Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    MessageBox.Show("download code comes here");
                }
                else
                {
                    MessageBox.Show("Cannot Refresh " + cmbLabourEstate.SelectedValue.ToString() + " Estate Division Fields Without Connection", "Cannot Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cmbTapType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }
    }
}