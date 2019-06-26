using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FTSPayRollBL;
using System.Data.SqlClient;

namespace FTSPayroll
{
    public partial class DailyHarvestRubber : Form
    {
        EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        EmployeeCategory EmpCat = new EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        Job Job1 = new Job();
        FTSCheckRollSettings FTSSettings = new FTSCheckRollSettings();
        FTSPayRollBL.DailyHarvestRubber DHarvestRubber = new FTSPayRollBL.DailyHarvestRubber();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.EstateDivisionBlock myField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Division myDiv = new FTSPayRollBL.Division();
        FTSPayRollBL.Field myFieldType = new FTSPayRollBL.Field();
        BlockEntries myEntries = new BlockEntries();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();
        FTSPayRollBL.AccountInformation DHAccounts = new AccountInformation();
        DataTable dtDaySummary = new DataTable();
        Int16 intCropType = 2;
        DataSet dsSunTask;
        String status = "NA";
        DataSet dsNorm = new DataSet();

        public DailyHarvestRubber()
        {
            InitializeComponent();
        }

        private void DailyHarvestRubber_Load(object sender, EventArgs e)
        {
            lblRefNo.Visible = false;
            
            DHarvestRubber.BoolFormLoad = true;

            cmbHoliManDays.Enabled = false;
            cmbEstate.DataSource = EstDivBlock.ListEstates();
            cmbEstate.DisplayMember = "EstateName";
            cmbEstate.ValueMember = "EstateID";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbFullHalf.DataSource = FTSSettings.ListDataFromSettings("FullHalfType");
            cmbFullHalf.DisplayMember = "Name";
            cmbFullHalf.ValueMember = "Code";
            cmbFullHalf.SelectedIndex = 1;

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Normal");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType","Rubber");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";
            cmbCropType.Text = "Rubber";

            cmbCategory.DataSource = EmpCat.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            //cmbEmpNo.DataSource = EmpMaster.ListAllEmployees();
            //cmbEmpNo.DisplayMember = "EmpNo";
            //cmbEmpNo.ValueMember = "EmpNo";

            cmbJob.DataSource = Job1.ListJobs();
            cmbJob.DisplayMember = "JobShortName";
            cmbJob.ValueMember = "JobShortName";

            //changed in order to get divisionwise norm
            //txtNorm.Text = DHarvestRubber.getNorm().Tables[0].Rows[0][0].ToString();

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
            cmbChitNumber.Enabled = false;

            cmbDivision.SelectedValue = FTSPayRollBL.User.StrDivision;

            //cmbDivision_SelectedIndexChanged(null, null);

            DHarvestRubber.BoolFormLoad = false;

            chkHoliday.Enabled = false;
            chkPaidHoliday.Enabled = false;
            txtPriNorm.Text = "0";
            dateTimePicker1.Focus();

            //try
            //{
            //    dateTimePicker1_ValueChanged(null, null);
            //}
            //catch { }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!cmbDivision.Text.Equals(FTSPayRollBL.User.StrDivision))
            if(true)
            {
                if (DHarvestRubber.BoolFormLoad == false)
                {
                    if (!cmbDivision.Text.ToUpper().Equals(User.StrDivision))
                    {
                        if (MessageBox.Show("Do you want to proceed with " + cmbDivision.Text + " Division...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            try
                            {
                                if (!cmbDivision.SelectedItem.ToString().Equals(""))
                                {

                                }

                                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                                DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();
                                refreshSummaryDetails();
                                DateChanged();
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
                        //FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                        ////gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                        //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();
                        refreshSummaryDetails();
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
                    //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                    gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                }
                catch { }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Decimal decAvailNormalMandays = 0;
            try
            {
            /*Day Blocking*/
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            Boolean OpenedDate = false;
            String strDateOk = "";
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            /*Blocked for BPL*/
            //if (FTSPayRollBL.User.BoolDayBlockAvailable && !chkSLMFLabour.Checked)
            if (FTSPayRollBL.User.BoolDayBlockAvailable )
            {
                strDateOk = myEntries.CheckDateDifference();
            }
            else
            {
                strDateOk = "OK";
            }
            if ((strDateOk.Equals("OK")))
            {
                if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                {
                    DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                }
                else
                {
                    MessageBox.Show("Please Select a Date Within the Month You Logged In");
                    goto End;
                    dateTimePicker1.Focus();
                    //dateTimePicker1.Value = new DateTime(Convert.ToInt32(User.StrYear),YMonth.GetMonthIdByMonthName(User.StrMonth), 1);
                }
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.","Blocked Entries");
               
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                myEntries.AddBlockDates();
                goto End;
                dateTimePicker1.Focus();
            }
            else
            {
                MessageBox.Show("This Date Data Entries Are Blocked Now, Please Contact Head Office For Date Release.");
                this.Close();
            }
            /*day blocking end*/

                if (!EmpMaster.IsEPFEntitled(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                {
                    MessageBox.Show("Employee Not Entitled For EPF!");
                }
                else if (FTSSettings.IsEntryValidationAgainstMusterEmpCount() && MChit.IsEmpHeadCountExceed(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()), Convert.ToInt32(txtNoOfEmployees.Text)))
                {
                    MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:" + txtNoOfEmployees.Text.ToString() + "\r\n Already Entered Count:" + txtAvailableEmpCount.Text);
                }
                //else if( MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()))>=Convert.ToInt32(txtNoOfEmployees.Text))
                //{
                //    MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:"+txtNoOfEmployees.Text.ToString()+"\r\n Already Entered Count:"+txtAvailableEmpCount.Text);
                //}

                else{
                    //DateChanged();
                    //if (IsFieldandWorkcodeMatch() == true)
                    //{
                    //if (DHarvestRubber.CheckPreviousDayEntries(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString())).Equals("OK"))
                    //{
                    if (chkPHPoyaGeneralName.Checked)
                    {

                        #region PHGeneralName
                        if (FTSPayRollBL.User.StrUserName != "admin")
                        {
                            if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                            {
                                DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                            }
                            else
                            {
                                MessageBox.Show("Please Select a Date Within the Month You Logged In", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dateTimePicker1.Focus();
                                goto End;
                            }
                        }
                        else
                        {
                            DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                        }

                        if (dateTimePicker1.Value.Date == DateTime.Now.Date)
                        {
                            if (MessageBox.Show("Please confirm the selected date..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            {
                                dateTimePicker1.Focus();
                                goto End;
                            }
                        }
                        decAvailNormalMandays = DHarvest.GetRubberEmployeeAvailableManDays(DHarvestRubber.DtHarvestDate, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        if (decAvailNormalMandays >= 1)
                        {
                            if (MessageBox.Show("Employee:" + txtEmpNo.Text + " Already Has " + decAvailNormalMandays + " ManDays,\r\n\r\n Do You Want To Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (MessageBox.Show("You are going to enter more than " + decAvailNormalMandays + " manday(s) to Emp:" + txtEmpNo.Text, "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
                        if (chkHoliday.Checked)
                        {
                            DHarvestRubber.BoolHolidayYesNo = true;
                            DHarvestRubber.FlHoliManDays = float.Parse(cmbHoliManDays.Text);
                            DHarvestRubber.BoolPaidHolidayYesNo = false;
                        }
                        else if (chkPaidHoliday.Checked)
                        {
                            DHarvestRubber.BoolPaidHolidayYesNo = true;
                            DHarvestRubber.FlHoliManDays = float.Parse("1.5");
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
                            DHarvestRubber.BoolPaidHolidayYesNo = true;
                        }

                        DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();
                        DHarvestRubber.StrField = cmbField.SelectedValue.ToString();
                        DHarvestRubber.StrBlock = "NA";
                        DHarvestRubber.FieldCropType = Convert.ToInt32(cmbFieldCropType.SelectedValue.ToString());

                        if (rbtnGeneral.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnGeneral.Text.ToString();
                        }
                        if (rbtnLentLabour.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnLentLabour.Text.ToString();
                            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            DHarvestRubber.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                            DHarvestRubber.StrLabourField = cmbLabourField.SelectedValue.ToString();
                        }
                        if (rbtnInterEstate.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnInterEstate.Text.ToString();
                            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                        }
                        DHarvestRubber.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
                        DHarvestRubber.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());

                        if (txtEmpNo.Text == "")
                        {
                            MessageBox.Show("Employee No Can't be Empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto End;
                        }
                        else
                        {
                            DHarvestRubber.StrEmpNo = txtEmpNo.Text;
                        }
                        //DHarvestRubber.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
                        DHarvestRubber.StrEmpName = txtEmpName.Text;

                        if (cmbJobCode.SelectedValue.ToString() != "")
                        {
                            DHarvestRubber.StrJob = (cmbJobCode.SelectedValue.ToString()).ToUpper();
                        }
                        else
                        {
                            MessageBox.Show("Job Code can't be empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto End;
                        }


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
                            Decimal normVal = 0;
                            Decimal Qtyval = 0;
                            Decimal OverKgs = 0;
                            Decimal PRINorm = 0;

                            normVal = Convert.ToDecimal(txtTapNorm.Text);
                            Qtyval = Convert.ToDecimal(txtQty.Text);
                            PRINorm = Convert.ToDecimal(txtPriNorm.Text);

                            if (normVal == 0 && Qtyval > 0)
                            {
                                if (MessageBox.Show("'Norm value zero'\n Are you sure..?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    goto End;
                                }
                                else
                                {
                                    goto End;
                                }

                            }
                            else
                            {
                                CalculateOverKilos();
                            }


                            DHarvestRubber.FlQty = float.Parse(txtQty.Text.ToString());
                            DHarvestRubber.FlOKgs = float.Parse(txtOverKilos.Text.ToString());

                            DHarvestRubber.IntPRINorm = Convert.ToInt32((txtPriNorm.Text.ToString()));
                            if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                            {
                                DHarvestRubber.FlNorm = float.Parse(txtTapNorm.Text);
                            }


                            if (txtScrapQty.Text != "")
                            {
                                DHarvestRubber.FlScrapQty = float.Parse(txtScrapQty.Text);
                            }
                            else
                            {
                                DHarvestRubber.FlScrapQty = 0;
                            }

                            //if (Convert.ToDecimal(txtQty.Text) >= Convert.ToDecimal(txtTapNorm.Text))
                            //{
                            //    DHarvestRubber.BoolTaskCompletedYesNo = true;
                            //}
                            //else
                            //{
                            //    DHarvestRubber.BoolTaskCompletedYesNo = false;
                            //}
                            if (chkTaskCompleted.Checked)
                            {
                                DHarvest.BoolTaskCompletedYesNo = true;
                            }
                            else
                            {
                                DHarvest.BoolTaskCompletedYesNo = false;
                            }

                            //if (!String.IsNullOrEmpty(txtPriNorm.Text))
                            //{
                            //    if (Convert.ToInt32(txtPriNorm.Text) > 0)
                            //    {
                            //        if (Convert.ToDecimal(txtQty.Text) >= Convert.ToDecimal(txtPriNorm.Text))
                            //        {
                            //            DHarvestRubber.BoolTaskCompletedYesNo = true;
                            //        }
                            //        else
                            //        {
                            //            DHarvestRubber.BoolTaskCompletedYesNo = false;
                            //        }
                            //    }
                            //}
                            //if (chkTaskCompleted.Checked)
                            //    DHarvestRubber.BoolTaskCompletedYesNo = true;
                            //else
                            //    DHarvestRubber.BoolTaskCompletedYesNo = false;
                        }
                        else
                        {
                            DHarvestRubber.FlQty = 0;
                            DHarvestRubber.FlOKgs = 0;
                            DHarvestRubber.FlScrapQty = 0;
                            DHarvestRubber.IntPRINorm = 0;
                            if (chkTaskCompleted.Checked == true)
                            {
                                DHarvestRubber.BoolTaskCompletedYesNo = true;
                            }
                            else
                            {
                                DHarvestRubber.BoolTaskCompletedYesNo = false;
                            }
                        }
                        //if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                        //{
                        //    if (txtTapNorm.Text != "")
                        //    {
                        //        DHarvestRubber.FlNorm = float.Parse(txtTapNorm.Text);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Please add Norm to processed..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        goto End;
                        //    }
                        //}

                        if (txtAreaCovered.Text.Trim() != "")
                        {
                            DHarvestRubber.DecAreaCovered = Convert.ToDecimal(txtAreaCovered.Text);
                        }
                        else
                        {
                            DHarvestRubber.DecAreaCovered = 0;
                        }

                        DHarvestRubber.StrUserId = User.StrUserName;



                        if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                        {
                            MessageBox.Show("Please Enter a Correct Job Code..!");
                            //txtJobShortName.Text = "";
                            //txtJobShortName.Focus();
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

                            DHarvestRubber.IntPRINorm = Convert.ToInt32(txtPriNorm.Text);
                            DHarvestRubber.StrACCode = "00";
                            DHarvestRubber.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                            DHarvestRubber.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                            try
                            {
                                if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                                {
                                    status = DHarvestRubber.InsertHarvetEntry();

                                    //if ADD button clicked else UDPATE button clicked
                                    if (e != null)
                                    {
                                        if (status.Equals("ADDED"))
                                        {
                                            MessageBox.Show("Daily Harvest Entry Added Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            AfterAdd();
                                        }
                                        else if (status.Equals("EXISTS"))
                                        {
                                            MessageBox.Show("Already Exists", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            AfterAdd();
                                        }
                                        else if (status.Equals("UPDATED"))
                                        {
                                            MessageBox.Show("Daily Harvest Entry updated Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            AfterAdd();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Oops, something went wrong!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            AfterAdd();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Daily Harvest Entry Updated Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        AfterAdd();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Cannot Add PLK Entries Here! ");
                                    //txtJobShortName.Focus();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error, " + ex.Message);
                                btnCancel.PerformClick();
                            }
                            txtQty.Enabled = true;

                        }
                        #endregion

                    }
                    else
                    {
                        if (FTSPayRollBL.User.StrUserName != "admin")
                        {
                            if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                            {
                                DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                            }
                            else
                            {
                                MessageBox.Show("Please Select a Date Within the Month You Logged In", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dateTimePicker1.Focus();
                                goto End;
                            }
                        }
                        else
                        {
                            DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                        }
                        decAvailNormalMandays = DHarvest.GetRubberEmployeeAvailableManDays(DHarvestRubber.DtHarvestDate, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        if (decAvailNormalMandays >= 1)
                        {
                            if (MessageBox.Show("Employee:" + txtEmpNo.Text + " Already Has " + decAvailNormalMandays + " ManDays,\r\n\r\n Do You Want To Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (MessageBox.Show("You are going to enter more than " + decAvailNormalMandays + " manday(s) to Emp:" + txtEmpNo.Text, "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
                        ///2011-08-11
                        //DHarvestRubber.StrBlock = cmbBlock.SelectedValue.ToString();
                        DHarvestRubber.StrBlock = "NA";
                        //DHarvestRubber.FieldCropType = Convert.ToInt32(cmbFieldCropType.SelectedValue.ToString());

                        //DHarvestRubber.StrCategory = cmbCategory.SelectedItem.ToString();
                        if (rbtnGeneral.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnGeneral.Text.ToString();
                        }
                        if (rbtnLentLabour.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnLentLabour.Text.ToString();
                            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            DHarvestRubber.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                            DHarvestRubber.StrLabourField = cmbLabourField.SelectedValue.ToString();
                        }
                        if (rbtnInterEstate.Checked)
                        {
                            DHarvestRubber.StrLabourType = rbtnInterEstate.Text.ToString();
                            DHarvestRubber.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                        }
                        DHarvestRubber.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
                        DHarvestRubber.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());

                        if (txtEmpNo.Text == "")
                        {
                            MessageBox.Show("Employee No Can't be Empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto End;
                        }
                        else
                        {
                            DHarvestRubber.StrEmpNo = txtEmpNo.Text;
                        }
                        //DHarvestRubber.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
                        DHarvestRubber.StrEmpName = txtEmpName.Text;

                        if (cmbJobCode.SelectedValue.ToString() != "")
                        {
                            DHarvestRubber.StrJob = (cmbJobCode.SelectedValue.ToString()).ToUpper();
                        }
                        else
                        {
                            MessageBox.Show("Job Code can't be empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto End;
                        }

                        //if (chkTaskCompleted.Checked) DHarvestRubber.BoolTaskCompletedYesNo = true;
                        //else DHarvestRubber.BoolTaskCompletedYesNo = false;
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
                            Decimal normVal = 0;
                            Decimal Qtyval = 0;
                            Decimal OverKgs = 0;
                            Decimal PRINorm = 0;

                            normVal = Convert.ToDecimal(txtTapNorm.Text);
                            Qtyval = Convert.ToDecimal(txtQty.Text);
                            PRINorm = Convert.ToDecimal(txtPriNorm.Text);

                            if (normVal == 0 && Qtyval > 0)
                            {
                                if (MessageBox.Show("'Norm value zero'\n Are you sure..?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    goto End;
                                }
                                else
                                {
                                    goto End;
                                }

                            }
                            else
                            {
                                CalculateOverKilos();
                            }


                            DHarvestRubber.FlQty = float.Parse(txtQty.Text.ToString());
                            DHarvestRubber.FlOKgs = float.Parse(txtOverKilos.Text.ToString());

                            DHarvestRubber.IntPRINorm = Convert.ToInt32((txtPriNorm.Text.ToString()));
                            if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                            {
                                DHarvestRubber.FlNorm = float.Parse(txtTapNorm.Text);
                            }

                            if (txtScrapQty.Text != "")
                            {
                                DHarvestRubber.FlScrapQty = float.Parse(txtScrapQty.Text);
                            }
                            else
                            {
                                DHarvestRubber.FlScrapQty = 0;
                            }

                            //if (Convert.ToDecimal(txtQty.Text) >= Convert.ToDecimal(txtTapNorm.Text))
                            //{
                            //    DHarvestRubber.BoolTaskCompletedYesNo = true;
                            //}
                            //else
                            //{
                            //    DHarvestRubber.BoolTaskCompletedYesNo = false;
                            //}


                            //if (!String.IsNullOrEmpty(txtPriNorm.Text))
                            //{
                            //    if (Convert.ToInt32(txtPriNorm.Text) > 0)
                            //    {
                            //        if (Convert.ToDecimal(txtQty.Text) >= Convert.ToDecimal(txtPriNorm.Text))
                            //        {
                            //            DHarvestRubber.BoolTaskCompletedYesNo = true;
                            //        }
                            //        else
                            //        {
                            //            DHarvestRubber.BoolTaskCompletedYesNo = false;
                            //        }
                            //    }
                            //}


                        }
                        else
                        {
                            DHarvestRubber.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
                            DHarvestRubber.FlQty = 0;
                            DHarvestRubber.FlOKgs = 0;
                            DHarvestRubber.FlScrapQty = 0;
                            //if (DHarvestRubber.IntFullHalf == 2)
                            //{
                            //    DHarvestRubber.BoolTaskCompletedYesNo = true;
                            //}
                            //else
                            //{
                            //    DHarvestRubber.BoolTaskCompletedYesNo = false;
                            //}
                        }
                        if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                        {
                            DHarvestRubber.FlNorm = float.Parse(txtTapNorm.Text);
                        }


                        //if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                        //{
                        //    if (txtTapNorm.Text != "")
                        //    {
                        //        DHarvestRubber.FlNorm = float.Parse(txtTapNorm.Text);
                        //        if (!String.IsNullOrEmpty(txtPriNorm.Text))
                        //        {
                        //            DHarvestRubber.IntPRINorm = Convert.ToInt32(txtPriNorm.Text);
                        //        }
                        //        else
                        //        {
                        //            DHarvestRubber.IntPRINorm = 0;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Please add Norm to processed..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        goto End;
                        //    }
                        //}

                        if (txtAreaCovered.Text.Trim() != "")
                        {
                            DHarvestRubber.DecAreaCovered = Convert.ToDecimal(txtAreaCovered.Text);
                        }
                        else
                        {
                            DHarvestRubber.DecAreaCovered = 0;
                        }


                        DHarvestRubber.StrUserId = User.StrUserName;



                        if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                        {
                            MessageBox.Show("Please Enter a Correct Job Code..!");
                            //txtJobShortName.Text = "";
                            //txtJobShortName.Focus();
                            goto End;
                        }
                        else
                        {
                            try
                            {
                                DHarvestRubber.StrACCode = "00";
                                DHarvestRubber.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                                DHarvestRubber.StrGangNo = cmbGangNumber.SelectedValue.ToString();

                                if (!String.IsNullOrEmpty(txtSundryTask.Text))
                                {
                                    DHarvestRubber.DecSundryTaskCompleted = Convert.ToDecimal(txtSundryTask.Text);
                                }
                                else
                                {
                                    DHarvestRubber.DecSundryTaskCompleted = 0;
                                }
                                if (!DHarvestRubber.StrJob.ToUpper().Equals("TAP"))
                                {
                                    if (String.IsNullOrEmpty(lblTaskValue.Text) || lblTaskValue.Text.ToUpper().Equals("TASK NOT DEFINED"))
                                    {
                                        if (chkTaskCompleted.Checked)
                                        {
                                            chkTaskCompleted.Checked = true;
                                        }
                                        else
                                        {
                                            chkTaskCompleted.Checked = false;
                                        }
                                    }
                                    else
                                    {
                                        if (DHarvestRubber.DecSundryTaskCompleted >= Convert.ToDecimal(lblTaskValue.Text))
                                        {
                                            DHarvestRubber.BoolTaskCompletedYesNo = true;
                                            chkTaskCompleted.Checked = true;
                                        }
                                        else
                                        {
                                            DHarvestRubber.BoolTaskCompletedYesNo = false;
                                            chkTaskCompleted.Checked = false;
                                        }
                                    }

                                }
                                else
                                {
                                    if (chkTaskCompleted.Checked)
                                    {
                                        DHarvestRubber.BoolTaskCompletedYesNo = true;
                                    }
                                    else
                                    {
                                        DHarvestRubber.BoolTaskCompletedYesNo = false;
                                    }
                                }

                                if (cmbTapType.SelectedIndex > -1)
                                {
                                    DHarvestRubber.IntTappingType = Convert.ToInt32(cmbTapType.SelectedValue.ToString());
                                }
                                else
                                {
                                    DHarvestRubber.IntTappingType = 1;
                                }
                                if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                                {
                                    status = DHarvestRubber.InsertHarvetEntry();

                                    //if ADD button clicked else UDPATE button clicked
                                    if (e != null)
                                    {
                                        if (status.Equals("ADDED"))
                                        {
                                            MessageBox.Show("Daily Harvest Entry Added Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            AfterAdd();
                                        }
                                        else if (status.Equals("EXISTS"))
                                        {
                                            MessageBox.Show("Already Exists", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            AfterAdd();
                                        }
                                        else if (status.Equals("UPDATED"))
                                        {
                                            MessageBox.Show("Daily Harvest Entry updated Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            AfterAdd();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Oops, something went wrong!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            AfterAdd();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Daily Harvest Entry Updated Successfully! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        AfterAdd();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Cannot Add PLK Entries Here!");
                                    //txtJobShortName.Focus();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error, " + ex.Message);
                                btnCancel.PerformClick();
                            }
                            txtQty.Enabled = true;

                        }
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Have Not Entered Previous Day Entries!\r\nPlease Enter " + dateTimePicker1.Value.Date.AddDays(-1).ToShortDateString() + " Day     Entries To Proceed ", "Entries Blocked");
                    //    dateTimePicker1.Focus();
                    //}
                    //}
                    //else
                    //    goto End;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        End:
            Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            //txtJobShortName.Text = "";
            //cmbJobCode. = "";
            txtQty.Clear();
            txtScrapQty.Clear();
            txtOverKilos.Clear();
            txtAreaCovered.Clear();
            txtQty.Enabled = true;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Normal");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";


            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
            }
        }

        private void AfterAdd()
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            txtQty.Clear();
            txtScrapQty.Clear();
            txtOverKilos.Clear();
            txtAreaCovered.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
            txtSundryTask.Clear();
          //  txtEmpNo.Focus();

            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                refreshSummaryDetails();
            }
            txtEmpNo.Focus();
        }



        private void gvDailyHarvest_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            

            lblRefNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();

            DataTable gridDt = DHarvestRubber.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString()));
            cmbMusterChit.SelectedValue = MChit.getMusterChitNumber(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[13].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[14].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[15].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[2].Value.ToString());
            cmbMusterChit_SelectedIndexChanged(null, null);
            cmbJobCode.SelectedValue = gridDt.Rows[0][10].ToString();
            txtEmpNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEmpNo_LeaveChanged();
            txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtOverKilos.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbLabourField.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[6].Value.ToString();

            dateTimePicker1.Value = Convert.ToDateTime(gridDt.Rows[0][1].ToString());
            cmbEstate.Text = gridDt.Rows[0][2].ToString();
            cmbDivision.Text = gridDt.Rows[0][8].ToString();
            cmbField.Text = gridDt.Rows[0][9].ToString();
            cmbCropType.Text = gridDt.Rows[0][6].ToString();
            cmbWorkType.Text = gridDt.Rows[0][7].ToString();
            cmbHoliManDays.Text = gridDt.Rows[0][17].ToString();
            txtTapNorm.Text = gridDt.Rows[0][18].ToString();
            txtScrapQty.Text = gridDt.Rows[0][19].ToString();
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
            txtEmpNo_Leave(null,null);

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
            txtSundryTask.Text = gridDt.Rows[0][23].ToString();
            if (gridDt.Rows[0][10].ToString().ToUpper().Equals("TAP"))
            {
                txtQty_Leave(null, null);
            }

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                DHarvestRubber.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

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
            {
                MessageBox.Show("Please Select Data Before Delete");
            }

            if (e != null)
                btnCancel.PerformClick();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //call delete first and then add entry again
            //btnDelete_Click(null, null);
            DHarvestRubber.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

            try
            {
                if (!EmpMaster.IsEPFEntitled(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                {
                    MessageBox.Show("Employee Not Entitled For EPF!");
                }
                else
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }


            
        }

        private void cmbLabourDivision_SelectedIndexChanged(object sender, EventArgs e)
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

        private void rbtnInterEstate_CheckedChanged(object sender, EventArgs e)
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
            else if (rbtnInterEstate.Checked)
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

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmpNo.Text))
            {
                if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                {
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        SetNormDetailsByLabourType();
                    }
                }
                else
                {
                    MessageBox.Show("Inactive Employee");
                    txtEmpNo.Focus();
                }
            }
        }

        private void txtEmpNo_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txtEmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            txtSundryTask.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                        txtEmpNo.Text = "";
                        txtEmpNo.Focus();
                    }

                    //EmpMaster.StrGender = EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    //cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    //txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    //txtJobShortName.Focus();
                    //txtEmpNo_leave_With_Job();
                }
            }

        }

        public void SetNormDetailsByLabourType()
        {
            if (rbtnGeneral.Checked)
            {
                SetNormDetails(cmbDivision.SelectedValue.ToString(),cmbField.SelectedValue.ToString());
            }
            else if (rbtnLentLabour.Checked)
            {
                SetNormDetails(cmbLabourDivision.SelectedValue.ToString(), cmbLabourField.SelectedValue.ToString());
            }
            else
            {
                SetNormDetails(cmbLabourDivision.SelectedValue.ToString(), cmbLabourField.SelectedValue.ToString());
            }
        }

        public void SetNormDetails(String Division,String Field)
        {            
            txtMaleNorm.Text = "0";
            txtNorm.Text = "0";
            txtPriNorm.Text = "0";
            txtFemalePRINorm.Text = "0";
            txtTapNorm.Text = "0";
            txtPriNorm.Text = "0";
            txtGasTappingNorm.Text = "0";
            try
            {
                dsNorm = DivNorm.GetFieldwisePlkNorm(Division, Field, dateTimePicker1.Value.Date);
                if (dsNorm.Tables[0].Rows.Count > 0)
                {
                    txtMaleNorm.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                    txtNorm.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                    txtMalePRINorm.Text = dsNorm.Tables[0].Rows[0][2].ToString();
                    txtFemalePRINorm.Text = dsNorm.Tables[0].Rows[0][3].ToString();
                    if (!String.IsNullOrEmpty(dsNorm.Tables[0].Rows[0][3].ToString()))
                    {
                        txtGasTappingNorm.Text = dsNorm.Tables[0].Rows[0][4].ToString();
                    }

                    if (EmpMaster.StrGender.ToUpper().Equals("F"))
                    {
                        //if (cmbFullHalf.SelectedValue.ToString() == "1")
                        //{
                        //    txtTapNorm.Text = Math.Ceiling(Convert.ToDecimal(dsNorm.Tables[0].Rows[0][1].ToString()) / Convert.ToDecimal("2")).ToString();

                        //    //txtNormTempVal.Focus();
                        //    txtQty.Focus();
                        //}
                        //else
                        //{
                            txtTapNorm.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                            txtPriNorm.Text = dsNorm.Tables[0].Rows[0][3].ToString();
                        //}
                    }
                    else
                    {
                        //if (cmbFullHalf.SelectedValue.ToString() == "1")
                        //{
                        //    txtTapNorm.Text = Math.Ceiling(Convert.ToDecimal(dsNorm.Tables[0].Rows[0][0].ToString()) / Convert.ToDecimal("2")).ToString();
                        //    //txtNormTempVal.Focus();
                        //    txtQty.Focus();
                        //}
                        //else
                        //{
                            txtTapNorm.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                            txtPriNorm.Text = dsNorm.Tables[0].Rows[0][2].ToString();
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("Norm Entries Not Available");
                }
            }
            catch { }
            //norm   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cmdCloseEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure you Want to Close " + dateTimePicker1.Value.Date.ToShortDateString() + " Day Entries?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DHarvestRubber.DtHarvestDate = dateTimePicker1.Value.Date;
                    DHarvestRubber.StrUserId = User.StrUserName;
                    DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();

                    DataTable DivisionTbl;
                    DivisionTbl = EstDivBlock.ListEstateDivisions();

                    foreach (DataRow drow in DivisionTbl.Rows)
                    {
                        String state = DHarvestRubber.CloseDayEntries(drow[0].ToString(), dateTimePicker1.Value.Date);
                        if (state.Equals("CLOSED"))
                        {
                            //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestEntries(dateTimePicker1.Value.Date);
                            gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                            MessageBox.Show(drow[0].ToString() + "Day Harvest Entries Closed Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error in Close day Entries..!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbHoliManDays.Text = "0.00";
            chkHoliday.Checked = false;
            chkPaidHoliday.Checked = false;
            if (e.KeyChar == 13)
            {
                DateChanged();
                cmbField.Focus();
            }
            else
            {
                dateTimePicker1.Focus();
            }

            gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));

        }

        private void DateChanged()
        {
            //if a poyaday
            chkHoliday.Checked = false;
            chkPaidHoliday.Checked = false;
            if (dateTimePicker1.Value.Date.ToString("dddd").Equals("Sunday"))
            {
                chkHoliday.Checked = true;
                cmbHoliManDays.Text = "1.50";
            }

            if (myHoli.IsPoyaday(dateTimePicker1.Value.Date))
            {
                chkHoliday.Checked = true;
                cmbHoliManDays.Text = "1.50";
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
            //if (!chkPaidHoliday.Checked && chkHoliday.Checked)
            //{
            //    cmbHoliManDays.Text = "1.00";
            //}
            //if a sunday
            cmbMusterChit.DataSource = MChit.ListMusterChitForSelectedDate(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
            cmbMusterChit.DisplayMember = "MChitName";
            cmbMusterChit.ValueMember = "AutoMusterID";

            //cmbMusterChit_SelectedIndexChanged(null, null);

            //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
            //DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();

            gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
            DHarvestRubber.StrDivision = cmbDivision.SelectedValue.ToString();
            refreshSummaryDetails();
        }

        private void chkHoliday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                rbtnGeneral.Focus();
            }
        }

        private void cmbCropType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbEstate.Focus();
            }
        }

        private void cmbHoliManDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbCropType.Focus();
            }
        }

        private void cmbEstate_KeyPress(object sender, KeyPressEventArgs e)
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
                dateTimePicker1.Focus();
            }
        }

        private void cmbWorkType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbField.Focus();
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

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList(this, cmbDivision.SelectedValue.ToString());
                    empList.ShowDialog();

                }
                else
                {
                    if (txtEmpNo.Text.Trim() != "")
                    {
                        txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                        txtEmpNo_LeaveChanged();
                    }
                }

            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    Decimal normVal = 0;
                    Decimal Qtyval = 0;
                    Decimal OverKgs = 0;
                    //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.Text));
                    //normVal = Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(),EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), txtJobShortName.Text, cmbField.SelectedValue.ToString()));
                    normVal = Convert.ToDecimal(txtTapNorm.Text);
                    Qtyval = Convert.ToDecimal(txtQty.Text);

                    if (normVal == 0 && Qtyval > 0)
                    {
                        if (MessageBox.Show("'Norm value zero'\n Are you sure..?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            txtScrapQty.Focus();
                        }
                        else
                        {
                            txtQty.Focus();
                        }
                    }
                    else if ((Qtyval - normVal) > 0)
                    {
                        OverKgs = Qtyval - normVal;
                        txtOverKilos.Text = OverKgs.ToString();
                        txtScrapQty.Focus();
                    }
                    else
                    {
                        txtOverKilos.Text = "0";
                        txtScrapQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Wrong..!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBlock.DataSource = myDiv.ListBlocks(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
            cmbBlock.DisplayMember = "BlockID";
            cmbBlock.ValueMember = "BlockID";


            DataSet ds = new DataSet();

            //ds = myField.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
            //if (ds.Tables.Count != 0)
            //{
            //    txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
            //}
            txtFieldName.Text = "";
        }

        private void cmbLabourField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnInterEstate.Checked)
            {
                cmbField.Enabled = false;
            }
            else
            {
                cmbField.Enabled = true;
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
                //txtTapNorm.Focus();
                //if (cmbJob.SelectedValue.ToString().Equals("TAP"))
                //{
                //    txtTapNorm.Focus();
                //}
                //else
                //{
                //    txtTapNorm.Text = "0";
                //    txtEmpNo.Focus();
                //}
            }
            else
            {
                MessageBox.Show("Please Select a Validate Field");
                cmbField.Focus();
            }
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

        private void EmpNo_Leave()
        {
            
                try
                {
                    if (cmbJobCode.SelectedValue.ToString().Equals("?"))
                    {
                        JobList myJob = new JobList(this);
                        myJob.Show();
                    }
                    else
                    {
                        //txtJobShortName_LeaveChanged();
                        //if (txtJobShortName.Text != "")
                        //{
                        //    if (rbtnGeneral.Checked == true && rbtnInterEstate.Checked == false && rbtnLentLabour.Checked == false)
                        //    {
                        //        DataTable dt = myFieldType.ListAllFieldsTypes(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
                        //        DataTable dt1 = myFieldType.GetWorkCodeType(txtJobShortName.Text);


                        //        if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                        //        {
                        //            txtJobShortName.Text.ToUpper();
                        //            txtJobShortName_LeaveChanged();
                        //        }
                        //        else if (txtJobShortName.Text.Substring(0, 1).ToUpper() == "X")
                        //        {
                        //            txtJobShortName.Text.ToUpper();
                        //            txtJobShortName_LeaveChanged();
                        //        }
                        //        else
                        //        {
                        //            MessageBox.Show("Field Type - '" + dt.Rows[0][0].ToString() + "' and WorkCode type - '" + dt1.Rows[0][0].ToString() + "' should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //            txtJobShortName.Focus();
                        //            txtJobShortName.Clear();
                        //        }
                        //        dt.Dispose();
                        //        dt1.Dispose();
                        //    }
                        //    else if (rbtnGeneral.Checked == false && rbtnInterEstate.Checked == false && rbtnLentLabour.Checked == true)
                        //    {
                        //        DataTable dt = myFieldType.ListAllFieldsTypes(cmbLabourDivision.SelectedValue.ToString(), cmbLabourField.SelectedValue.ToString());
                        //        DataTable dt1 = myFieldType.GetWorkCodeType(txtJobShortName.Text);


                        //        if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                        //        {
                        //            txtJobShortName.Text.ToUpper();
                        //            txtJobShortName_LeaveChanged();
                        //        }
                        //        else if (txtJobShortName.Text.Substring(0, 1).ToUpper() == "X")
                        //        {
                        //            txtJobShortName.Text.ToUpper();
                        //            txtJobShortName_LeaveChanged();
                        //        }
                        //        else
                        //        {
                        //            MessageBox.Show("Field Type - '" + dt.Rows[0][0].ToString() + "' and WorkCode type - '" + dt1.Rows[0][0].ToString() + "' should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //            txtJobShortName.Focus();
                        //            txtJobShortName.Clear();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        txtJobShortName.Text.ToUpper();
                        //        txtJobShortName_LeaveChanged();
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("No Workcode Found..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    txtJobShortName.Clear();
                        //    txtJobShortName.Focus();
                        //}
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something Wrong in a Workcode..!" + ex.Message);
                    //txtJobShortName.Clear();
                }
           
        }

        private void txtEmpNo_leave_With_Job()
        {
            if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
            {
                MessageBox.Show("Please Enter a Correct Job Code.");
                //txtJobShortName.Text = "";
                //txtJobShortName.Focus();
            }
            else
            {
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    SetNormDetailsByLabourType();
                }
                txtJobName.Text = Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper());
                //cmbJobCode.SelectedValue.ToString() = cmbJobCode.SelectedValue.ToString().ToUpper();
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    if (!String.IsNullOrEmpty(txtTapNorm.Text))
                    {
                        if (Convert.ToDecimal(txtTapNorm.Text) < 1)
                        {
                            //MessageBox.Show("Norm Value Should Be Greater Than 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            //txtTapNorm.Focus();
                            txtQty.Focus();
                        }
                        else
                        {
                            txtQty.Focus();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Norm Value Cannot Be Empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //txtTapNorm.Focus();
                        txtQty.Focus();
                    }
                }
                else
                {
                    txtSundryTask.SelectAll();
                    txtSundryTask.Focus();
                }





            }


        }

        //private void txtJobShortName_LeaveChanged()
        //{
        //    //txtQty.Enabled = true;
        //    if (!String.IsNullOrEmpty(txtJobShortName.Text))
        //    {

        //        if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
        //        {
        //            MessageBox.Show("Please Enter a Correct Job Code.");
        //            txtJobShortName.Text = "";
        //            txtJobShortName.Focus();
        //        }
        //        else
        //        {
        //            txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
        //            txtJobShortName.Text = txtJobShortName.Text.ToUpper();
        //            if (txtJobShortName.Text.ToUpper().Equals("PH"))
        //            {
        //                chkTaskCompleted.Checked = true;

        //                txtQty.Clear();
        //                txtQty.Enabled = false;

        //                btnAdd.Focus();
        //            }
        //            else
        //            {
        //                if (this.txtJobShortName.Text.ToUpper().Equals("TAP"))
        //                {
        //                    //txtNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), txtJobShortName.Text.ToUpper(), cmbField.SelectedValue.ToString()).ToString();
        //                    txtQty.Focus();
        //                    txtScrapQty.Enabled = true;
        //                    txtQty.Enabled = true;
        //                }
        //                else
        //                {
        //                    chkTaskCompleted.Checked = true;

        //                        txtQty.Clear();
        //                        txtQty.Enabled = false;

        //                        txtScrapQty.Clear();
        //                        txtScrapQty.Enabled = false;
        //                        this.btnAdd.Focus();                            
        //                }
        //            }
        //        }
        //    }
        //}

        private void cmbLabourField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void btnJobSearch_Click(object sender, EventArgs e)
        {
            FTSPayroll.JobList myJobList = new FTSPayroll.JobList();
            myJobList.Show();
        }

        private void chkPaidHoliday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaidHoliday.Checked)
            {
                //MessageBox.Show("Please Add Entries From Daily Harvest - Cashwork Form.");
                //this.Close();
                //DHarvestRubber.BoolPaidHolidayYesNo = true;

                //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
                //cmbWorkType.DisplayMember = "Name";
                //cmbWorkType.ValueMember = "Code";
                DataTable dtPH;
                dtPH = DHarvestRubber.ListPHHarvestForDivision(dateTimePicker1.Value.Date, FTSPayRollBL.User.StrDivision);
                if (dtPH.Rows.Count < 1)
                {
                    //MessageBox.Show("Add Extra Names For Paid Holiday");
                    //this.Close();
                    //AddExtraNames AddExNames = new AddExtraNames();
                    //AddExNames.Show();
                }


            }
            else
            {
                DHarvestRubber.BoolPaidHolidayYesNo = false;
                cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Normal");
                cmbWorkType.DisplayMember = "Name";
                cmbWorkType.ValueMember = "Code";
            }
        }

        private void chkPaidHoliday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                rbtnGeneral.Focus();
            }
        }

        private void DailyHarvestRubber_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                //if (DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "TAP", cmbField.SelectedValue.ToString()) > 0 && DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "TAP", cmbField.SelectedValue.ToString()) > 0)
                //{
                //    txtMaleNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "TAP", cmbField.SelectedValue.ToString()).ToString();
                //    txtNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "TAP", cmbField.SelectedValue.ToString()).ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Please Add Norm Value For This Division And Proceed.");
                //    this.Close();
                //}

                //if (DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "TAP") > 0 && DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "TAP") > 0)
                //{
                //    txtMaleNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "TAP").ToString();
                //    txtNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "TAP").ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Please Add Norm Value For This Division And Proceed.");
                //    this.Close();
                //}
            }
        }

        private void rbtnInterEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourEstate.Focus();
            }
        }

        private void txt_IsNum(string context)
        {
            bool isnum = true;
            for (int i = 0; i < context.Length; i++)
            {
                if (!char.IsNumber(context[i]))
                {
                    isnum = false;
                    break;
                }
            }
            if (!isnum)
            {
                MessageBox.Show("Invalid Qty Value!");
                txtQty.Clear();
                txtQty.Focus();
            }

        }

        private void txtQty_Leave(object sender, EventArgs e)
        { 
            if (!String.IsNullOrEmpty(txtQty.Text))
            {
                string context = this.txtQty.Text;
                if (float.Parse(context) > 100.00)
                {
                    MessageBox.Show("Qty Value Should be less than 3 digits!");
                    txtQty.Clear();
                    txtQty.Focus();
                }
                else
                {
                    CalculateOverKilos();
                    txtScrapQty.Focus();
                }
            }
        }

        private void CalculateOverKilos()
        {
            Decimal normVal = 0;
            Decimal Qtyval = 0;
            Decimal OverKgs = 0;
            Decimal PRINorm = 0; 

            if (cmbTapType.SelectedValue.ToString().ToUpper().Equals("2"))
            {
                normVal = Convert.ToDecimal(txtGasTappingNorm.Text);
                PRINorm = Convert.ToDecimal(txtGasTappingNorm.Text);
                Qtyval = Convert.ToDecimal(txtQty.Text);
                txtTapNorm.Text = txtGasTappingNorm.Text;
                txtPriNorm.Text = txtGasTappingNorm.Text;
            }
            else
            {
                if (EmpMaster.StrGender.ToUpper().Equals("M"))
                {
                    normVal = Convert.ToDecimal(txtMaleNorm.Text);
                    PRINorm = Convert.ToDecimal(txtMalePRINorm.Text);
                    Qtyval = Convert.ToDecimal(txtQty.Text);
                    txtTapNorm.Text = txtMaleNorm.Text;
                    txtPriNorm.Text = txtMalePRINorm.Text;
                }
                else
                {
                    normVal = Convert.ToDecimal(txtNorm.Text);
                    PRINorm = Convert.ToDecimal(txtFemalePRINorm.Text);
                    Qtyval = Convert.ToDecimal(txtQty.Text);
                    txtTapNorm.Text = txtNorm.Text;
                    txtPriNorm.Text = txtFemalePRINorm.Text;
                }
            }
            //normVal = Convert.ToDecimal(txtTapNorm.Text);
            //Qtyval = Convert.ToDecimal(txtQty.Text);
            //PRINorm=Convert.ToDecimal(txtPriNorm.Text);
            if ((Qtyval - normVal) > 0)
            {
                OverKgs = Qtyval - normVal;
                txtOverKilos.Text = OverKgs.ToString();
                chkTaskCompleted.Checked = true;
            }
            else if (Qtyval == normVal)
            {
                chkTaskCompleted.Checked = true;
                txtOverKilos.Text = "0";
            }
            else
            {
                txtOverKilos.Text = "0";
                chkTaskCompleted.Checked = false;
                if (Qtyval >= PRINorm)
                {
                    chkTaskCompleted.Checked = true;
                }
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

                        cmbDivision_SelectedIndexChanged(null, null);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {            
            DateChanged();
        }

        private void txtScrapQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    txtSundryTask.Focus();
            //}
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
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
                    //txtTapNorm.Focus();

                }
                else
                {
                    MessageBox.Show("No Block Found..!");
                    cmbBlock.Focus();
                }
            }
        }

        private void txtTapNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void txtTapNorm_Leave(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(txtTapNorm.Text))
            //{
            //    txtTapNorm.Text = "0";
            //    MessageBox.Show("Norm value not found..!", "WARNING.!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    //txtTapNorm.Clear();
            //    txtTapNorm.Focus();
            //}
            //else
            //{
            //    txtEmpNo.Focus();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Sure you Want to Close " + dtpDateToClose.Value.Date.ToShortDateString() + " Day Entries?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DHarvestRubber.StrUserId = User.StrUserName;
                    DataTable DivisionTbl;
                    DivisionTbl = EstDivBlock.ListEstateDivisions();
                    foreach (DataRow drow in DivisionTbl.Rows)
                    {
                        String state = DHarvestRubber.CloseDayEntries(drow[0].ToString(), dtpDateToClose.Value.Date);
                        if (state.Equals("CLOSED"))
                        {
                            //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestEntries(dateTimePicker1.Value.Date);
                            //gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                            MessageBox.Show(drow[0].ToString() + " Division - Day Harvest Entries Closed Successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Error!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtAreaCovered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        private void txtJobShortName_Leave(object sender, EventArgs e)
        {

            //IsFieldandWorkcodeMatch();
        }

        //private void txtJobShortName_LeaveChanged()
        //{
        //    //txtQty.Enabled = true;
        //    if (!String.IsNullOrEmpty(txtJobShortName.Text))
        //    {

        //        if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
        //        {
        //            MessageBox.Show("Please Enter a Correct Job Code.");
        //            txtJobShortName.Text = "";
        //            txtJobShortName.Focus();
        //        }
        //        else
        //        {
        //            /*display norm  */

        //            //norm                    

        //            txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
        //            txtJobShortName.Text = txtJobShortName.Text.ToUpper();
        //            if (txtJobShortName.Text.ToUpper().Equals("PH"))
        //            {
        //                chkTaskCompleted.Checked = true;
        //                txtQty.Enabled = false;
        //                btnAdd.Focus();
        //            }
        //            else
        //            {
        //                if (this.txtJobShortName.Text.ToUpper().Equals("TAP"))
        //                {
        //                    //norm selection comes here
        //                    txtQty1.Focus();
        //                    //txtQty.Focus();
        //                }
        //                else
        //                {
        //                    if (cmbFullHalf.Text.Equals("Full"))
        //                    {
        //                        chkTaskCompleted.Checked = true;
        //                        txtQty.Enabled = false;
        //                        //this.txtAreaCovered.Focus();
        //                        //btnAdd.PerformClick();
        //                        btnAdd.Focus();
        //                    }
        //                    else
        //                    {
        //                        //txtAreaCovered.Focus();
        //                        //btnAdd.PerformClick();
        //                        btnAdd.Focus();
        //                    }
        //                }
        //            }
        //        }

        //    }
        //}

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
                            DataTable dt = myFieldType.ListAllFieldsTypes(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
                            DataTable dt1 = myFieldType.GetWorkCodeType(cmbJobCode.SelectedValue.ToString());


                            if (dt.Rows[0][0].ToString() == dt1.Rows[0][0].ToString())
                            {
                                //txtJobShortName.Text.ToUpper();
                                //txtJobShortName_LeaveChanged();
                                Status = true;
                            }
                            else if (cmbJobCode.SelectedValue.ToString().Substring(0, 1).ToUpper() == "X")
                            {
                                //txtJobShortName.Text.ToUpper();
                                //txtJobShortName_LeaveChanged();
                                Status = true;
                            }
                            else
                            {
                                MessageBox.Show("Field Type - '" + dt.Rows[0][0].ToString() + "' and WorkCode type - '" + dt1.Rows[0][0].ToString() + "' should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //txtJobShortName.Focus();
                                //txtJobShortName.Clear();
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
                                //txtJobShortName.Text.ToUpper();
                                //txtJobShortName_LeaveChanged();
                                Status = true;
                            }
                            else if (cmbJobCode.SelectedValue.ToString().Substring(0, 1).ToUpper() == "X")
                            {
                                //txtJobShortName.Text.ToUpper();
                                //txtJobShortName_LeaveChanged();
                                Status = true;
                            }
                            else
                            {
                                MessageBox.Show("Field Type - '" + dt.Rows[0][0].ToString() + "' and WorkCode type - '" + dt1.Rows[0][0].ToString() + "' should be matched..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //txtJobShortName.Focus();
                                //txtJobShortName.Clear();
                            }
                        }
                        else
                        {
                            //txtJobShortName.Text.ToUpper();
                            //txtJobShortName_LeaveChanged();
                            Status = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Workcode Found..!", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //txtJobShortName.Clear();
                        //txtJobShortName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Wrong in a Workcode..!" + ex.Message);
                //txtJobShortName.Clear();
                cmbJobCode.SelectedIndex = -1;
                
            }

            return Status;
        }

        private void cmbFieldCropType_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    txtTapNorm.Focus();
            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
                cmbJobCode.DisplayMember = "JobShortName";
                cmbJobCode.ValueMember = "JobShortName";
                cmbJobCode.SelectedValue = dtMusterData.Rows[0][10].ToString();
                dsSunTask = null;
                dsSunTask = Job1.GetSundryTask(dtMusterData.Rows[0][10].ToString(), intCropType);
                if (dsSunTask.Tables[0].Rows.Count > 0)
                {
                    lblTaskUnit.Text = dsSunTask.Tables[0].Rows[0][0].ToString();
                    lblTaskValue.Text = dsSunTask.Tables[0].Rows[0][1].ToString();
                    txtSundryTask.Text = dsSunTask.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    lblTaskUnit.Text = "...";
                    lblTaskValue.Text = "Task Not Defined";
                }

                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    //txtQty1.Focus();
                }
                else
                {

                    DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(), intCropType);
                    if (dsTask.Tables[0].Rows.Count > 0)
                    {
                        //if (!String.IsNullOrEmpty(txtSundryTask.Text))
                        //{
                        //    if (Convert.ToDecimal(txtSundryTask.Text) >= Convert.ToDecimal(dsTask.Tables[0].Rows[0][1].ToString()))
                        //    {
                        //        chkTaskCompleted.Checked = true;
                        //    }
                        //    else
                        //    {
                        //        chkTaskCompleted.Checked = false;
                        //    }

                        //    //btnAdd.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Task Cannot Be Zero");
                        //    txtSundryTask.Text = "0.0";
                        //    txtSundryTask.SelectAll();
                        //    txtSundryTask.Focus();
                        //}
                    }
                    else
                    {
                        chkTaskCompleted.Enabled = false;
                        //not available a task
                        if (cmbFullHalf.SelectedValue.ToString().Equals("2"))
                        {
                            chkTaskCompleted.Enabled = true;
                            chkTaskCompleted.Focus();
                        }
                        else
                        {
                            chkTaskCompleted.Checked = false;
                            btnAdd.Focus();
                        }

                    }
                }

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
                    cmbLabourDivision.SelectedValue = dtMusterData.Rows[0][8].ToString();
                    cmbLabourDivision_SelectedIndexChanged(null, null);
                    cmbLabourField.SelectedValue = dtMusterData.Rows[0][9].ToString();
                }
                gbLent.Enabled = false;

                fieldChanged();
                if (cmbMusterChit.DataSource != null)
                {
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                    {
                        //txtTapNorm.Focus();
                        txtEmpNo.Focus();
                    }
                    else
                    {
                        txtTapNorm.Text = "0";
                        txtEmpNo.Focus();
                    }
                }
                if (dtMusterData.Rows[0][10].ToString().ToUpper().Equals("TAP"))
                {
                    //SetNormDetails();
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
            if (FTSPayRollBL.User.BoolDayBlockAvailable )
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

        private void cmbFullHalf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        private void chkTaskCompleted_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        private void SetPRIFromSundryTask()
        {

           
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    //txtQty1.Focus();
                }
                else
                {

                    DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(), intCropType);
                    if (dsTask.Tables[0].Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(txtSundryTask.Text))
                        {
                            if (Convert.ToDecimal(txtSundryTask.Text) >= Convert.ToDecimal(dsTask.Tables[0].Rows[0][1].ToString()))
                            {
                                chkTaskCompleted.Checked = true;
                            }
                            else
                            {
                                chkTaskCompleted.Checked = false;
                            }

                            btnAdd.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Task Cannot Be Zero");
                            txtSundryTask.Text = "0.0";
                            txtSundryTask.SelectAll();
                            txtSundryTask.Focus();
                        }
                    }
                    else
                    {
                        chkTaskCompleted.Enabled = false;
                        //not available a task
                        if (cmbFullHalf.SelectedValue.ToString().Equals("2"))
                        {
                            chkTaskCompleted.Enabled = true;
                            chkTaskCompleted.Focus();
                        }
                        else
                        {
                            chkTaskCompleted.Checked = false;
                            btnAdd.Focus();
                        }

                    }
                }
                btnAdd.Focus();
            
        }

        private void txtSundryTask_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
                {
                    //txtQty1.Focus();
                }
                else
                {

                    DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(), intCropType);
                    if (dsTask.Tables[0].Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(txtSundryTask.Text))
                        {
                            if (Convert.ToDecimal(txtSundryTask.Text) >= Convert.ToDecimal(dsTask.Tables[0].Rows[0][1].ToString()))
                            {
                                chkTaskCompleted.Checked = true;
                            }
                            else
                            {
                                chkTaskCompleted.Checked = false;
                            }

                            btnAdd.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Task Cannot Be Zero");
                            txtSundryTask.SelectAll();
                            txtSundryTask.Focus();
                        }
                    }
                    else
                    {
                        chkTaskCompleted.Enabled = false;
                        //not available a task
                        if (cmbFullHalf.SelectedValue.ToString().Equals("2"))
                        {
                            chkTaskCompleted.Enabled = true;
                            chkTaskCompleted.Focus();
                        }
                        else
                        {
                            chkTaskCompleted.Checked = false;
                            btnAdd.Focus();
                        }

                    }
                }
                btnAdd.Focus();
            }

            //if (e.KeyChar == 13)
            //{
            //    btnAdd.Focus();
            //}
            //if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
            //{
            //    //txtQty1.Focus();
            //}
            //else
            //{

            //    DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(), intCropType);
            //    if (dsTask.Tables[0].Rows.Count > 0)
            //    {
            //        if (!String.IsNullOrEmpty(txtSundryTask.Text))
            //        {
            //            if (Convert.ToDecimal(txtSundryTask.Text) >= Convert.ToDecimal(dsTask.Tables[0].Rows[0][1].ToString()))
            //            {
            //                chkTaskCompleted.Checked = true;
            //            }
            //            else
            //            {
            //                chkTaskCompleted.Checked = false;
            //            }

            //            //btnAdd.Focus();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Task Cannot Be Zero");
            //            txtSundryTask.Text = "0.0";
            //            txtSundryTask.Focus();
            //        }
            //    }
            //    else
            //    {
            //        chkTaskCompleted.Enabled = false;
            //        //not available a task
            //        if (cmbFullHalf.SelectedValue.ToString().Equals("2"))
            //        {
            //            chkTaskCompleted.Enabled = true;
            //            chkTaskCompleted.Focus();
            //        }
            //        else
            //        {
            //            chkTaskCompleted.Checked = false;
            //            btnAdd.Focus();
            //        }

            //    }
            //}
        }

        private void txtSundryTask_TextChanged(object sender, EventArgs e)
        {
            //if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("TAP"))
            //{
            //    //txtQty1.Focus();
            //}
            //else
            //{

            //    DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(), intCropType);
            //    if (dsTask.Tables[0].Rows.Count > 0)
            //    {
            //        if (!String.IsNullOrEmpty(txtSundryTask.Text))
            //        {
            //            if (Convert.ToDecimal(txtSundryTask.Text) >= Convert.ToDecimal(dsTask.Tables[0].Rows[0][1].ToString()))
            //            {
            //                chkTaskCompleted.Checked = true;
            //            }
            //            else
            //            {
            //                chkTaskCompleted.Checked = false;
            //            }

            //            //btnAdd.Focus();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Task Cannot Be Zero");
            //            txtSundryTask.Text = "0.0";
            //            txtSundryTask.Focus();
            //        }
            //    }
            //    else
            //    {
            //        chkTaskCompleted.Enabled = false;
            //        //not available a task
            //        if (cmbFullHalf.SelectedValue.ToString().Equals("2"))
            //        {
            //            chkTaskCompleted.Enabled = true;
            //            chkTaskCompleted.Focus();
            //        }
            //        else
            //        {
            //            chkTaskCompleted.Checked = false;
            //            btnAdd.Focus();
            //        }

            //    }
            //}
        }

        private void txtSundryTask_Leave(object sender, EventArgs e)
        {
            
            //SetPRIFromSundryTask();
        }

        private void txtSearchEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbJobCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTapType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
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

        private void txtSearchEmpNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvDailyHarvest.DataSource = DHarvestRubber.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), txtSearchEmpNo.Text.PadLeft(5, '0'));
            }
            catch 
            {
            }
        }

        private void cmbTapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQty_Leave(null, null);
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

       



















    }
}