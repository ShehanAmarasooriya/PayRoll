using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class DailyHarvest : Form
    {
        EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        EmployeeCategory EmpCat = new EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        Job Job1 = new Job();
        FTSCheckRollSettings FTSSettings = new FTSCheckRollSettings();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.EstateDivisionBlock myField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.SystemSetting ChkSettings = new FTSPayRollBL.SystemSetting();
        FTSPayRollBL.AccountInformation DHAccounts = new AccountInformation();
        BlockEntries myEntries = new BlockEntries();
        DataTable dtDaySummary = new DataTable();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();
        FTSPayRollBL.Validation clsValidation = new FTSPayRollBL.Validation();
        DataSet dsSunTask;
        DataSet dsNorm = new DataSet();
        Int16 intCropType = 1;
        public DailyHarvest()
        {
            InitializeComponent();
        }

        private void DailyHarvest_Load(object sender, EventArgs e)
        {
            chkBlockPlk.Enabled = false;
            DHarvest.BoolCashOkgYesNo = false;
            

            /*Blocked for BPL*/
            //if (MessageBox.Show("Click 'Yes' To Proceed With Block Plucking", "Block Plucking Selection", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    chkBlockPlk.Checked = true;                    
            //    DHarvest.BoolBlockPlk = true;
            //    cmbJobCode.Text = "PLK";
            //    cmbJobCode.Enabled = false;
            //    txtContractor.Enabled = true;
            //    dateTimePicker1.Focus();
            //    lblEntryType.Text = "Block Plucking";

            //}
            //else
            //{
                txtContractor.Text = "";
                txtContractor.Enabled = false;
                DHarvest.BoolCashOkgYesNo = false;

                chkBlockPlk.Checked = false;
                DHarvest.BoolBlockPlk = false;
                cmbJobCode.SelectedIndex=-1;
                //cmbJobCode.Enabled = true;
                dateTimePicker1.Focus();
                lblEntryType.Text = "Normal";
            //}
            lblRefNo.Visible = false;

            DHarvest.BoolFormLoad = true;

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

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType","Tea");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

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
            //txtNorm.Text = DHarvest.getNorm().Tables[0].Rows[0][0].ToString();
           
            

            chkTaskCompleted.Enabled = false;
            rbtnGeneral.Checked = true;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            cmbDivision.Text = FTSPayRollBL.User.StrDivision;
            
            cmbDivision_SelectedIndexChanged(null, null);

            DHarvest.BoolFormLoad = false;
            chkHoliday.Enabled = false;
            chkPaidHoliday.Enabled = false;

            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";

            DHarvest.BoolIsContract = false;

            cmbDivision.Focus();
            dateTimePicker1.Focus();

            cmbLabourEstate.DataSource = EstDivBlock.ListOtherEstates();
            cmbLabourEstate.DisplayMember = "EstateName";
            cmbLabourEstate.ValueMember = "EstateID";

            dateTimePicker1_ValueChanged(null, null);
        }        

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (!cmbDivision.Text.Equals(FTSPayRollBL.User.StrDivision))
            {
                if (DHarvest.BoolFormLoad == false)
                {
                    if (MessageBox.Show("Do you want to proceed with " + cmbDivision.Text + " Division...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            if (!cmbDivision.SelectedItem.ToString().Equals(""))
                            {
                                cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                                cmbField.DisplayMember = "FieldID";
                                cmbField.ValueMember = "FieldID";

                                cmbField_SelectedIndexChanged(null, null);
                              
                            }
                            FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                            gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
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
                            cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                            cmbField.DisplayMember = "FieldID";
                            cmbField.ValueMember = "FieldID";

                            cmbField_SelectedIndexChanged(null, null);
                        }
                        FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                        //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                        gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
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
                        cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                        cmbField.DisplayMember = "FieldID";
                        cmbField.ValueMember = "FieldID";

                        cmbField_SelectedIndexChanged(null, null);
                    }
                    FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                    //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                    gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
                }
                catch { }
            }

            dateTimePicker1_ValueChanged(null, null);
        }

        private void txtContractor_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txtContractor.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNoAndEmpType(txtContractor.Text, 1)))
                {
                    MessageBox.Show("Please Select A Valid Contractor");
                    txtContractor.Text = "";
                    txtContractor.Focus();
                }
                else
                {
                    txtContractorName.Text = EmpMaster.GetEmployeeNameByEmpNoAndEmpType(txtContractor.Text, 1);
                    txtEmpNo.Focus();
                }
            }

        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            //write here if relavant blocks need to be selected
        }

        //private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
        //    {
        //        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbEmpNo.SelectedValue.ToString());
        //    }
        //}        

        private void btnAdd_Click(object sender, EventArgs e)        
        {
            

            /*Blocked for BPL*/
            //if (DHarvest.CheckPreviousDayEntries(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString())).Equals("OK"))
            //{

            
            //if (DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()) > 0 && DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()) > 0)
            //{
            //    txtMaleNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()).ToString();
            //    txtNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()).ToString();
            //}

            Decimal decAvailNormalMandays = 0;
            DHarvest.BoolSpeciMedHalf = false;
            if (clsValidation.ExpenditureJournalValidation(dateTimePicker1.Value.Date) == true)
            {
                MessageBox.Show("Expenditure Journal For " + dateTimePicker1.Value.Date.Year.ToString() + "/" + dateTimePicker1.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                goto End;

            }
            else if (chkSLMFLabour.Checked && !EmpMaster.IsSLMFEmployee(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text))
            {
                MessageBox.Show(txtEmpNo.Text + " Is Not A SLMF Labour, Insert Failed");
                chkSLMFLabour.Checked = false;
            }
            else if (cmbJobCode.SelectedValue.ToString().Equals("PH") && !chkPaidHoliday.Checked)
            {
                MessageBox.Show("Paid Holiday Is Not Added To Monthly Holidays");
                dateTimePicker1.Focus();
            }
            //else if (String.IsNullOrEmpty(txtACCode.Text))
            //{
            //    MessageBox.Show("Account Code");
            //    txtACCode.Focus();
            //}
            //if (DHAccounts.GETSubCategoryNameByCode(txtACCode.Text).ToUpper().Equals("NA"))
            //{
            //    MessageBox.Show("Account Code Not Found!");
            //    txtACCode.Focus();
            //}
            //else
            if (String.IsNullOrEmpty(cmbChitNumber.SelectedValue.ToString()))
            {
                MessageBox.Show("Muster Chit Number Cannot Be Empty");
                cmbChitNumber.Focus();
            }
            else if (String.IsNullOrEmpty(cmbGangNumber.SelectedValue.ToString()))
            {
                MessageBox.Show("Gang Number Cannot Be Empty");
                cmbGangNumber.Focus();
            }


            else if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
            {
                MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                txtEmpNo.Focus();
            }
            else if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
            {
                MessageBox.Show("Please Enter a Correct Job Code.");
                cmbJobCode.Focus();
            }
            else if (FTSSettings.IsEntryValidationAgainstMusterEmpCount() && MChit.IsEmpHeadCountExceed(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()), Convert.ToInt32(txtNoOfEmployees.Text)))
            {
                MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:" + txtNoOfEmployees.Text.ToString() + "\r\n Already Entered Count:" + txtAvailableEmpCount.Text);
            }
            //else if (MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())) >= Convert.ToInt32(txtNoOfEmployees.Text))
            //{
            //    MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:" + txtNoOfEmployees.Text.ToString() + "\r\n Already Entered Count:" + txtAvailableEmpCount.Text);
            //}
            else
            {
                try
                {
                    DHarvest.DecNameValue = FTSSettings.ListDataFromCheckrollRatesDefault("DailyBasic");
                    if (dateTimePicker1.Value.Date == DateTime.Now.Date)
                    {
                        if (MessageBox.Show("Please confirm the selected date..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            dateTimePicker1.Focus();
                            goto End;
                        }
                    }
                    //if (!txtACCode.Text.Equals("0369"))
                    //{
                    //    if (!DHAccounts.IsJobAvaialbleInACMaster(cmbJobCode.SelectedValue.ToString(), txtACCode.Text))
                    //    {
                    //        MessageBox.Show("Job Code Not Avaialble For Main Code Given");
                    //        goto End;
                    //        //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    //        //{
                    //        //    goto End;
                    //        //}

                    //    }
                    //}
                    if (!String.IsNullOrEmpty(txtSundryTask.Text))
                    {
                        DHarvest.DecSundryTaskCompleted = Convert.ToDecimal(txtSundryTask.Text);
                    }
                    else
                    {
                        DHarvest.DecSundryTaskCompleted = 0;
                        if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                        {
                            MessageBox.Show("Sundry Task Cannot be Zero");
                            txtSundryTask.Focus();
                            goto End;
                        }
                    }

                    decAvailNormalMandays = DHarvest.GetEmployeeAvailableManDays(DHarvest.DtHarvestDate, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                    if (decAvailNormalMandays >= 1)
                    {
                        if (MessageBox.Show("Employee:" + txtEmpNo.Text + " Already Has " + decAvailNormalMandays + " ManDays,\r\n\r\n Do You Want To Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            if (MessageBox.Show("You are going to enter more than " + decAvailNormalMandays + DHarvest.FlManDays + " manday(s) to Emp:" + DHarvest.StrEmpNo, "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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



                    if (chkBlockPlk.Checked)
                    {
                        //this doesn't Available to Normal Work
                        #region BlockPlkInsert
                        CalcOverkilos();
                        DHarvest.BoolIsContract = false;
                        DHarvest.StrContractor = "NA";
                        //if (EmpMaster.GetEmployeeNameByEmpNoAndEmpType(txtContractor.Text, 1).Equals(""))
                        //{
                        //    //MessageBox.Show("Please Select A Valid Contractor");
                        //    //txtContractor.Focus();
                        //}
                        //else 
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("EmpNo Cannot Be Empty!");
                            txtEmpNo.Focus();
                        }
                        else if (String.IsNullOrEmpty(cmbJobCode.SelectedValue.ToString()))
                        {
                            MessageBox.Show("Job Cannot Be Empty!");
                            cmbJobCode.Focus();
                        }
                        else
                        {
                            DHarvest.BoolBlockPlk = false;
                            //if (chkBlockPlk.Checked)
                            //{
                            //    DHarvest.BoolBlockPlk = true;
                            //}
                            //else
                            //{
                            //    DHarvest.BoolBlockPlk = false;
                            //}
                            DHarvest.StrContractor = "NA";
                            //DataSet ds = new DataSet();
                            //ds = myHoli.GetMonthHolidays((Convert.ToInt32(dateTimePicker1.Value.Year)), (Convert.ToInt32(dateTimePicker1.Value.Month)));

                            //if (ds.Tables[0].Rows.Count > 0)
                            //{ 
                            if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                            {
                                DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                            }
                            else
                            {
                                MessageBox.Show("Please Select a Date Within the Month You Logged In");
                                dateTimePicker1.Focus();
                            }
                            if (chkHoliday.Checked)
                            {
                                DHarvest.BoolHolidayYesNo = true;
                                DHarvest.FlHoliManDays = float.Parse(cmbHoliManDays.Text);
                                DHarvest.BoolPaidHolidayYesNo = false;
                            }
                            else if (chkPaidHoliday.Checked)
                            {
                                DHarvest.BoolPaidHolidayYesNo = true;
                                DHarvest.BoolHolidayYesNo = false;
                            }
                            else
                            {
                                DHarvest.BoolHolidayYesNo = false;
                                int fHoliManDays = 0;
                                DHarvest.FlHoliManDays = (float)fHoliManDays;
                            }

                            if (chkPaidHoliday.Checked)
                            {
                                DHarvest.BoolPaidHolidayYesNo = true;
                            }
                            else
                            {
                                DHarvest.BoolPaidHolidayYesNo = false;

                            }

                            DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                            DHarvest.StrField = cmbField.SelectedValue.ToString();

                            //DHarvest.StrBlock=cmbBlock.SelectedItem.ToString();
                            //DHarvest.StrCategory = cmbCategory.SelectedItem.ToString();
                            if (rbtnGeneral.Checked)
                            {
                                DHarvest.StrLabourType = rbtnGeneral.Text.ToString();
                            }
                            if (rbtnLentLabour.Checked)
                            {
                                DHarvest.StrLabourType = rbtnLentLabour.Text.ToString();
                                DHarvest.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                                DHarvest.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                                DHarvest.StrLabourField = cmbLabourField.SelectedValue.ToString();
                            }
                            if (rbtnInterEstate.Checked)
                            {
                                DHarvest.StrLabourType = rbtnInterEstate.Text.ToString();
                                DHarvest.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            }
                            DHarvest.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
                            DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
                            DHarvest.StrEmpNo = txtEmpNo.Text;
                            //DHarvest.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
                            DHarvest.StrEmpName = txtEmpName.Text;
                            DHarvest.StrJob = cmbJobCode.SelectedValue.ToString();
                            if (chkTaskCompleted.Checked) DHarvest.BoolTaskCompletedYesNo = true;
                            else DHarvest.BoolTaskCompletedYesNo = false;
                            DHarvest.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
                            //mandays for holidays need to be implemented.......
                            if (chkHoliday.Checked)
                            {
                                if (DHarvest.IntFullHalf == 2)
                                {
                                    DHarvest.FlManDays = (float)(DHarvest.FlHoliManDays);
                                }
                                else if (DHarvest.IntFullHalf == 1)
                                {
                                    double mValue = 0.5;
                                    DHarvest.FlManDays = (float)mValue;
                                }
                            }
                            else if (!chkHoliday.Checked)
                            {
                                DHarvest.FlManDays = (float)(DHarvest.IntFullHalf / 2.0);
                            }
                            if (DHarvest.StrJob == "PLK")
                            {
                                DHarvest.FlQty = float.Parse(txtQty.Text);
                                DHarvest.FlOKgs = float.Parse(txtOverKilos.Text);
                                DHarvest.DecQty1 = float.Parse(txtQty1.Text);
                                DHarvest.DecQty2 = float.Parse(txtQty2.Text);
                                DHarvest.DecQty3 = float.Parse(txtQty3.Text);
                                DHarvest.DecAreaCovered = float.Parse(txtAreaCovered.Text);
                                DHarvest.DecFieldWeight = float.Parse(txtFieldWeight.Text);
                            }
                            else
                            {
                                DHarvest.FlQty = 0;
                                DHarvest.FlOKgs = 0;
                                DHarvest.DecQty1 = 0;
                                DHarvest.DecQty2 = 0;
                                DHarvest.DecQty3 = 0;
                                DHarvest.DecAreaCovered = float.Parse(txtAreaCovered.Text);
                                DHarvest.DecFieldWeight = float.Parse(txtFieldWeight.Text);
                            }

                            DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.SelectedValue.ToString().ToUpper(), dateTimePicker1.Value.Date).ToString());

                            //DHarvest.FlHours=txtHours.Text;
                            DHarvest.StrUserId = User.StrUserName;
                            if (DHarvest.StrJob == "PLK" && DHarvest.FlQty < 1)
                            {
                                MessageBox.Show("Qty cannot be 0");
                                txtQty1.Focus();
                            }
                            else
                            {
                                try
                                {
                                    DHarvest.BoolIsContract = false;
                                    if (chkBlockPlk.Checked)
                                    {
                                        DHarvest.StrContractor = txtContractor.Text;
                                    }
                                    else
                                    {
                                        DHarvest.StrContractor = "NA";
                                    }
                                    DHarvest.DecContractorRate = 0;
                                    DHarvest.StrACCode = txtACCode.Text;
                                    DHarvest.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();

                                    String status = DHarvest.InsertHarvetEntryBlockPlucking();
                                    if (status.Equals("ADDED"))
                                    {
                                        //MessageBox.Show("Daily Harvest Entry Added Successfully! ");
                                        AfterAdd();
                                    }
                                    else if (status.Equals("NADDED"))
                                    {
                                        //MessageBox.Show("Normal Entry Added Successfully");
                                        AfterAdd();
                                    }
                                    else if (status.Equals("1EXISTS"))
                                    {
                                        MessageBox.Show("Normal Entry Already Exists");
                                        AfterAdd();
                                    }
                                    else if (status.Equals("2EXISTS"))
                                    {
                                        MessageBox.Show("Cashwork Entry Already Exists");
                                        AfterAdd();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Oops, something went wrong!");
                                        AfterAdd();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error, " + ex.Message);
                                    btnCancel.PerformClick();
                                }
                            }
                            txtQty.Enabled = true;

                        }
                        #endregion
                    }
                    else
                    {
                        txtOverKilos.Text = "0"; 
                            //CalcOverkilos();
                        if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                        {
                            CalculateOverKilos();
                        }

                        SetPRIFromSundryTask();
                        DHarvest.BoolIsContract = false;
                        DHarvest.StrContractor = "NA";
                        if (chkBlockPlk.Checked)
                        {
                            DHarvest.BoolBlockPlk2013 = true;
                        }
                        else
                        {
                            DHarvest.BoolBlockPlk2013 = false;
                        }

                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("EmpNo Cannot Be Empty!");
                            txtEmpNo.Focus();
                        }
                        else if (String.IsNullOrEmpty(cmbJobCode.SelectedValue.ToString()))
                        {
                            MessageBox.Show("Job Cannot Be Empty!");
                            cmbJobCode.Focus();
                        }
                        else
                        {
                            DHarvest.BoolBlockPlk = false;
                            DHarvest.StrContractor = "NA";
                            //DataSet ds = new DataSet();
                            //ds = myHoli.GetMonthHolidays((Convert.ToInt32(dateTimePicker1.Value.Year)), (Convert.ToInt32(dateTimePicker1.Value.Month)));

                            //if (ds.Tables[0].Rows.Count > 0)
                            //{ 
                            if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                            {
                                DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                            }
                            else
                            {
                                MessageBox.Show("Please Select a Date Within the Month You Logged In");
                                dateTimePicker1.Focus();
                            }
                            if (chkHoliday.Checked)
                            {
                                DHarvest.BoolHolidayYesNo = true;
                                DHarvest.FlHoliManDays = float.Parse(cmbHoliManDays.Text);
                                DHarvest.BoolPaidHolidayYesNo = false;
                            }
                            else if (chkPaidHoliday.Checked)
                            {
                                DHarvest.BoolPaidHolidayYesNo = true;
                                DHarvest.BoolHolidayYesNo = false;
                            }
                            else
                            {
                                DHarvest.BoolHolidayYesNo = false;
                                int fHoliManDays = 0;
                                DHarvest.FlHoliManDays = (float)fHoliManDays;
                            }

                            if (chkPaidHoliday.Checked)
                            {
                                DHarvest.BoolPaidHolidayYesNo = true;
                            }
                            else
                            {
                                DHarvest.BoolPaidHolidayYesNo = false;

                            }

                            DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                            DHarvest.StrField = cmbField.SelectedValue.ToString();

                            //DHarvest.StrBlock=cmbBlock.SelectedItem.ToString();
                            //DHarvest.StrCategory = cmbCategory.SelectedItem.ToString();
                            if (rbtnGeneral.Checked)
                            {
                                DHarvest.StrLabourType = rbtnGeneral.Text.ToString();
                            }
                            if (rbtnLentLabour.Checked)
                            {
                                DHarvest.StrLabourType = rbtnLentLabour.Text.ToString();
                                DHarvest.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                                DHarvest.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                                DHarvest.StrLabourField = cmbLabourField.SelectedValue.ToString();
                            }
                            if (rbtnInterEstate.Checked)
                            {
                                DHarvest.StrLabourType = rbtnInterEstate.Text.ToString();
                                DHarvest.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                                DHarvest.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                                DHarvest.StrLabourField = cmbLabourField.SelectedValue.ToString();
                            }
                            DHarvest.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
                            DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
                            DHarvest.StrEmpNo = txtEmpNo.Text;
                            //DHarvest.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
                            DHarvest.StrEmpName = txtEmpName.Text;
                            DHarvest.StrJob = cmbJobCode.SelectedValue.ToString();
                            if (chkTaskCompleted.Checked) DHarvest.BoolTaskCompletedYesNo = true;
                            else DHarvest.BoolTaskCompletedYesNo = false;
                            DHarvest.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
                            //mandays for holidays need to be implemented.......
                            if (chkHoliday.Checked)
                            {
                                if (DHarvest.IntFullHalf == 2)
                                {
                                    DHarvest.FlManDays = (float)(DHarvest.FlHoliManDays);
                                }
                                else if (DHarvest.IntFullHalf == 1)
                                {
                                    double mValue = 0.5;
                                    DHarvest.FlManDays = (float)mValue;
                                }
                            }
                            else if (!chkHoliday.Checked)
                            {
                                DHarvest.FlManDays = (float)(DHarvest.IntFullHalf / 2.0);
                            }
                            if (DHarvest.StrJob == "PLK")
                            {
                                DHarvest.FlQty = float.Parse(txtQty.Text);
                                DHarvest.FlOKgs = float.Parse(txtOverKilos.Text);
                                DHarvest.DecQty1 = float.Parse(txtQty1.Text);
                                DHarvest.DecQty2 = float.Parse(txtQty2.Text);
                                DHarvest.DecQty3 = float.Parse(txtQty3.Text);
                                DHarvest.DecAreaCovered = float.Parse(txtAreaCovered.Text);
                                DHarvest.DecFieldWeight = float.Parse(txtFieldWeight.Text);
                            }
                            else
                            {
                                DHarvest.FlQty = 0;
                                DHarvest.FlOKgs = 0;
                                DHarvest.DecQty1 = 0;
                                DHarvest.DecQty2 = 0;
                                DHarvest.DecQty3 = 0;
                                DHarvest.DecAreaCovered = float.Parse(txtAreaCovered.Text);
                                DHarvest.DecFieldWeight = float.Parse(txtFieldWeight.Text);
                            }


                            if (cmbFullHalf.SelectedValue.ToString().Equals("1"))
                            {
                                DHarvest.FlNorm = float.Parse(txtNormTempVal.Text);
                                DHarvest.FlPRINorm = float.Parse(txtPRITempVal.Text);
                            }
                            else
                            {
                                //DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.Text.ToUpper(), dateTimePicker1.Value.Date).ToString());
                                DHarvest.FlNorm = float.Parse(txtNormTempVal.Text);
                                DHarvest.FlPRINorm = float.Parse(txtPRITempVal.Text);
                            }


                            //DHarvest.FlHours=txtHours.Text;
                            DHarvest.StrUserId = User.StrUserName;

                            if (DHarvest.StrJob == "PLK" && DHarvest.FlQty < 1)
                            {
                                MessageBox.Show("Qty cannot be 0");
                                txtQty1.Focus();
                            }
                            else if (chkSpecialMedicalHalf.Checked && DHarvest.IntFullHalf == 2)
                            {
                                MessageBox.Show("Please select Full Half to Half");
                            }
                            else
                            {
                                try
                                {
                                    if (chkSpecialMedicalHalf.Checked)
                                    {
                                        //if (Job1.IsSpecialHalfNameCode(cmbJobCode.Text))
                                        if (true)
                                        {
                                            DHarvest.BoolSpeciMedHalf = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("This Work Code Is Invalid To Enter Special Half Name !", "Invalid");
                                            cmbJobCode.SelectedIndex = -1;
                                            cmbJobCode.Focus();
                                            goto End;
                                        }
                                    }
                                    DHarvest.BoolIsContract = false;
                                    DHarvest.StrContractor = "NA";
                                    DHarvest.DecContractorRate = 0;
                                    DHarvest.BoolCashOkgYesNo = false;
                                    if (cmbJobCode.SelectedValue.ToString().Equals("PH"))
                                    {
                                        if (!chkPaidHoliday.Checked)
                                        {
                                            MessageBox.Show("Paid Holiday Is Not Added To Monthly Holidays");
                                            dateTimePicker1.Focus();
                                        }
                                    }

                                    DHarvest.StrACCode = "00";
                                    DHarvest.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                                    DHarvest.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                                    DHarvest.DecCashPlkRate = 0;
                                    if (!String.IsNullOrEmpty(txtSundryTask.Text))
                                    {
                                        DHarvest.DecSundryTaskCompleted = Convert.ToDecimal(txtSundryTask.Text);
                                    }
                                    else
                                    {
                                        DHarvest.DecSundryTaskCompleted = 0;
                                    }
                                    if (!DHarvest.StrJob.ToUpper().Equals("PLK"))
                                    {
                                        if (String.IsNullOrEmpty(lblTaskValue.Text)||lblTaskValue.Text.ToUpper().Equals("TASK NOT DEFINED"))
                                        {
                                            chkTaskCompleted.Checked = true;
                                            chkTaskCompleted.Enabled = true;
                                        }
                                        else
                                        {
                                            if (DHarvest.DecSundryTaskCompleted >= Convert.ToDecimal(lblTaskValue.Text))
                                            {
                                                DHarvest.BoolTaskCompletedYesNo = true;
                                            }
                                            else
                                            {
                                                DHarvest.BoolTaskCompletedYesNo = false;
                                            }
                                        }
                                        
                                    }
                                  
                                    
                                    String status = DHarvest.InsertHarvetEntry();
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
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error, " + ex.Message);
                                    btnCancel.PerformClick();
                                }
                            }
                            //txtQty.Enabled = true;

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //}
            //else
            //{
            //    MessageBox.Show("You Have Not Entered Previous Day Entries!\r\nPlease Enter " + dateTimePicker1.Value.Date.AddDays(-1).ToShortDateString() + " Day Entries To Proceed ", "Entries Blocked");
            //    dateTimePicker1.Focus();

            //}
            End:
                Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            
            if (chkBlockPlk.Checked)
            {
            }
            else
            {
            //    cmbJobCode.SelectedIndex=-1;
            //    txtJobName.Text = "";
            }
            txtQty.Clear();
            txtOverKilos.Clear();
            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtNormTempVal.Clear();
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";

            txtQty.Enabled = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dateTimePicker1.Enabled = true;
            cmbDivision.Enabled = true;
            txtEmpNo.Enabled = true;
            cmbWorkType.Enabled = true;

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType","Normal");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";
            //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
            //cmbWorkType.DisplayMember = "Name";
            //cmbWorkType.ValueMember = "Code";

            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
            }


        }

        private void AfterAdd()
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            //txtJobShortName.Clear();
            //txtJobName.Clear();
            txtQty.Clear();
            cmbFullHalf.Text = "Full";
            txtOverKilos.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtNormTempVal.Clear();
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";
            txtEmpNo.Focus();
            chkSpecialMedicalHalf.Checked = false;
            txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();

            //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType");
            //cmbWorkType.DisplayMember = "Name";
            //cmbWorkType.ValueMember = "Code";

            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
                //dtDaySummary = DHarvest.GetDaySummary(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false);
                //txtSumPlkNames.Text=dtDaySummary.Rows[0][0].ToString();
                //txtSumSunNames.Text = dtDaySummary.Rows[0][2].ToString();
                //txtSumKilos.Text = dtDaySummary.Rows[0][3].ToString();
                //txtSumOverKgs.Text = dtDaySummary.Rows[0][4].ToString();
                refreshSummaryDetails();
            }
        }

        private void refreshSummaryDetails()
        {
            try
            {
                dtDaySummary = DHarvest.GetDaySummary(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false);
                txtSumPlkNames.Text = dtDaySummary.Rows[0][0].ToString();
                txtSumSunNames.Text = dtDaySummary.Rows[0][2].ToString();
                txtSumKilos.Text = dtDaySummary.Rows[0][3].ToString();
                txtSumOverKgs.Text = dtDaySummary.Rows[0][4].ToString();
            }
            catch { }
        }

       
        private void gvDailyHarvest_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PH"))
            if(gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString().ToUpper().Equals("PH"))
            {
                #region PH
                txtOverKilos.Clear();
                txtEmpNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmpName.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtOverKilos.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();

                //cmbFullHalf.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();
                //if (gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString() == "True")
                //{
                //    chkTaskCompleted.Checked = true;
                //}
                //else
                //{
                //    chkTaskCompleted.Checked = false;
                //}
                lblRefNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtQty1.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtQty2.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtQty3.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[11].Value.ToString();
                this.txtAreaCovered.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtFieldWeight.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[13].Value.ToString();
                cmbMusterChit.SelectedValue = MChit.getMusterChitNumber(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[16].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[17].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[18].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[2].Value.ToString());
                //cmbChitNumber.SelectedValue= gvDailyHarvest.Rows[e.RowIndex].Cells[16].Value.ToString();
                //cmbMusterChit_SelectedIndexChanged(null, null);
                //cmbField.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString();
                //cmbGangNumber.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[17].Value.ToString();
                if (gvDailyHarvest.Rows[e.RowIndex].Cells[14].Value.ToString() == "True")
                {
                    chkBlockPlk.Checked = true;
                }
                else
                {
                    chkBlockPlk.Checked = false;
                }

                DataTable gridDt = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString()), txtEmpNo.Text.Trim(), 1);

                if (gridDt.Rows.Count > 0)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(gridDt.Rows[0][1].ToString());
                    cmbEstate.Text = gridDt.Rows[0][2].ToString();
                    //cmbDivision.Text = gridDt.Rows[0][8].ToString();
                    cmbCropType.Text = gridDt.Rows[0][6].ToString();
                    cmbWorkType.Text = gridDt.Rows[0][7].ToString();
                    cmbHoliManDays.Text = gridDt.Rows[0][17].ToString();

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
                    txtJobShortName_LeaveChanged();

                    //DataSet ds = EstDivBlock.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
                    txtFieldName.Text = "NA";

                    if (gridDt.Rows[0][11].ToString() == "True")
                    {
                        chkTaskCompleted.Checked = true;
                    }
                    else
                    {
                        chkTaskCompleted.Checked = false;
                    }

                    cmbFullHalf.SelectedValue = int.Parse(gridDt.Rows[0][12].ToString());

                    txtNormTempVal.Text = gridDt.Rows[0][21].ToString();
                } 
                #endregion
            }
            else
            {
                txtOverKilos.Clear();
                txtEmpNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmpName.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtOverKilos.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();
                
                //cmbFullHalf.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();
                //if (gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString() == "True")
                //{
                //    chkTaskCompleted.Checked = true;
                //}
                //else
                //{
                //    chkTaskCompleted.Checked = false;
                //}
                lblRefNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtQty1.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtQty2.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtQty3.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[11].Value.ToString();
                this.txtAreaCovered.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtFieldWeight.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[13].Value.ToString();
                cmbMusterChit.SelectedValue = MChit.getMusterChitNumber(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[16].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[17].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[18].Value.ToString(), gvDailyHarvest.Rows[e.RowIndex].Cells[2].Value.ToString());
                cmbMusterChit_SelectedIndexChanged(null, null);
                txtACCode_TextChanged(null, null);
                //cmbChitNumber.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[16].Value.ToString();
                //cmbChitNumber_SelectedIndexChanged(null, null);
                //cmbField.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString();
                //cmbGangNumber.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[17].Value.ToString();
                if (gvDailyHarvest.Rows[e.RowIndex].Cells[14].Value.ToString() == "True")
                {
                    chkBlockPlk.Checked = true;
                }
                else
                {
                    chkBlockPlk.Checked = false;
                }

                DataTable gridDt = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString()), txtEmpNo.Text.Trim(), 1);

                if (gridDt.Rows.Count > 0)
                {
                    //dateTimePicker1.Value = Convert.ToDateTime(gridDt.Rows[0][1].ToString());
                    //cmbEstate.Text = gridDt.Rows[0][2].ToString();
                    //cmbDivision.Text = gridDt.Rows[0][8].ToString();
                    //cmbCropType.Text = gridDt.Rows[0][6].ToString();
                    //cmbWorkType.Text = gridDt.Rows[0][7].ToString();
                    //cmbHoliManDays.Text = gridDt.Rows[0][17].ToString();

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
                    String tempjob = gridDt.Rows[0][10].ToString();
                    cmbJobCode.SelectedValue = gridDt.Rows[0][10].ToString();
                    cmbJobCode.SelectedValue = tempjob;
                    txtJobShortName_LeaveChanged();
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PH"))
                    {
                        txtFieldName.Text = "NA";
                    }
                    else
                    {
                        DataSet ds = EstDivBlock.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
                        txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                    }

                    if (gridDt.Rows[0][11].ToString() == "True")
                    {
                        chkTaskCompleted.Checked = true;
                    }
                    else
                    {
                        chkTaskCompleted.Checked = false;
                    }

                    cmbFullHalf.SelectedValue = int.Parse(gridDt.Rows[0][12].ToString());
                    txtSundryTask.Text = gridDt.Rows[0][22].ToString();
                    txtNormTempVal.Text = gridDt.Rows[0][21].ToString();
                }

                //lblRefNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[0].Value.ToString();
                //dateTimePicker1.Value = Convert.ToDateTime(gvDailyHarvest.Rows[e.RowIndex].Cells[1].Value.ToString());
                //txtEmpNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[2].Value.ToString();
                ////cmbEmpNo.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[2].Value.ToString();
                //chkHoliday.Checked = Convert.ToBoolean(gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString());
                //if(gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString()=="General")
                //{
                //    rbtnGeneral.Checked=true;
                //}
                //else if (gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString()=="Lent Labour")
                //{
                //    rbtnLentLabour.Checked=true;
                //    cmbLabourEstate.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[13].Value.ToString();
                //    cmbLabourDivision.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[14].Value.ToString();
                //    cmbLabourDivision_SelectedIndexChanged(null,null);
                //    cmbLabourField.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[15].Value.ToString();
                //}
                //else if (gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString()=="Inter Estate Lent Labour")
                //{
                //    rbtnInterEstate.Checked=true;
                //}
                //cmbCropType.SelectedValue = int.Parse(gvDailyHarvest.Rows[e.RowIndex].Cells[5].Value.ToString());
                //cmbWorkType.SelectedValue = int.Parse(gvDailyHarvest.Rows[e.RowIndex].Cells[6].Value.ToString());
                //cmbDivision.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString();
                //cmbField.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString();
                ////txtEmpName.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
                //cmbJob.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();
                //chkTaskCompleted.Checked = Convert.ToBoolean(gvDailyHarvest.Rows[e.RowIndex].Cells[10].Value.ToString());
                //cmbFullHalf.SelectedValue = int.Parse(gridDt.Rows[0][12].ToString());          
                //txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[12].Value.ToString();

            }
            dateTimePicker1.Enabled = false;
            cmbDivision.Enabled = false;
            txtEmpNo.Enabled = false;
            cmbWorkType.Enabled = false;

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                String strDateOk = "";
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;

                /*Blocked for BPL*/
                //strDateOk =  myEntries.CheckDateDifference();
                strDateOk = "OK";

                if ((strDateOk.Equals("OK")))
                {
                    if (!String.IsNullOrEmpty(lblRefNo.Text))
                    {
                        DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                        DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
                        strDateOk = "OK";
                        DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                        DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
                        DHarvest.StrEmpNo = txtEmpNo.Text;

                        try
                        {
                            if (clsValidation.ExpenditureJournalValidation(dateTimePicker1.Value.Date) == true)
                            {
                                MessageBox.Show("Expenditure Journal For " + dateTimePicker1.Value.Date.Year.ToString() + "/" + dateTimePicker1.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                String status = "";
                                if (chkBlockPlk.Checked)
                                {
                                    status = DHarvest.DeleteHarvetEntryBlockPlk();
                                }
                                else
                                {
                                    status = DHarvest.DeleteHarvetEntry();
                                }

                                if (status.Equals("DELETED"))
                                {
                                    MessageBox.Show("Daily Harvest Entry Deleted Successfully! ");
                                    btnCancel.PerformClick();
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
                    btnCancel.PerformClick();
                }
                else
                {
                    MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Entries Blocked");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String strDateOk = "";
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;

            /*Blocked for BPL*/
            //strDateOk =  myEntries.CheckDateDifference();
            strDateOk = "OK";

            if ((strDateOk.Equals("OK")))
            {
                if (!String.IsNullOrEmpty(lblRefNo.Text))
                {
                    DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                    DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
                    strDateOk = "OK";
                    DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                    DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
                    DHarvest.StrEmpNo = txtEmpNo.Text;

                    try
                    {
                        if (clsValidation.ExpenditureJournalValidation(dateTimePicker1.Value.Date) == true)
                        {
                            MessageBox.Show("Expenditure Journal For " + dateTimePicker1.Value.Date.Year.ToString() + "/" + dateTimePicker1.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            String status = "";
                            if (chkBlockPlk.Checked)
                            {
                                status = DHarvest.DeleteHarvetEntryBlockPlk();
                            }
                            else
                            {
                                status = DHarvest.DeleteHarvetEntry();
                            }

                            if (status.Equals("DELETED"))
                            {
                                //MessageBox.Show("Daily Harvest Entry Deleted Successfully! ");
                                //btnCancel.PerformClick();
                                btnAdd_Click(null, null);
                            }
                            else if (status.Equals("NOTEXISTS"))
                            {
                                MessageBox.Show("Not Exists");
                            }
                            btnCancel.PerformClick();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, " + ex.Message);
                    }

                }
                else
                    MessageBox.Show("Please Select Data Before Delete");
                btnCancel.PerformClick();
            }
            else
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Entries Blocked");
            }
            ////if (clsValidation.ExpenditureJournalValidation(dateTimePicker1.Value.Date) == true)
            ////{
            ////    MessageBox.Show("Expenditure Journal For " + dateTimePicker1.Value.Date.Year.ToString() + "/" + dateTimePicker1.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ////}
            ////else  if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
            ////{
            ////    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
            ////    txtEmpNo.Focus();
            ////}
            ////else if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
            ////{
            ////    MessageBox.Show("Please Enter a Correct Job Code.");
            ////    cmbJobCode.Focus();
            ////}
            //////else if (!DHAccounts.IsJobAvaialbleInACMaster(cmbJobCode.SelectedValue.ToString(), txtACCode.Text))
            //////{
            //////    MessageBox.Show("Job Code Not Avaialble For Main Code Given");
            //////    //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            //////    //{
            //////    //    goto End;
            //////    //}

            //////}
            ////else
            ////{
            ////    try
            ////    {
            ////        if (dateTimePicker1.Value.Date == DateTime.Now.Date)
            ////        {
            ////            if (MessageBox.Show("Please confirm the selected date..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            ////            {
            ////                dateTimePicker1.Focus();
            ////                goto End;
            ////            }
            ////        }


            ////        String strDateOk = "";
            ////        DHarvest.BoolIsContract = false;
            ////        DHarvest.StrContractor = "NA";
            ////        myEntries.DtCurrentDate = dateTimePicker1.Value.Date;

            ////        /*Blocked for BPL*/
            ////        //strDateOk = myEntries.CheckDateDifference();
            ////        strDateOk = "OK";

            ////        if ((strDateOk.Equals("OK")))
            ////        {
            ////            if (!String.IsNullOrEmpty(lblRefNo.Text))
            ////            {

            ////                //if (myEntries.IsDateOpened())
            ////                //{
            ////                txtOverKilos.Text = "0";
            ////                    //CalcOverkilos();
            ////                    CalculateOverKilos();
            ////                DHarvest.BoolBlockPlk = false;

            ////                DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
            ////                DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
            ////                if (chkHoliday.Checked)
            ////                {
            ////                    DHarvest.BoolHolidayYesNo = true;
            ////                    DHarvest.FlHoliManDays = float.Parse(cmbHoliManDays.Text);
            ////                    DHarvest.BoolPaidHolidayYesNo = false;
            ////                }
            ////                else if (chkPaidHoliday.Checked)
            ////                {
            ////                    DHarvest.BoolPaidHolidayYesNo = true;
            ////                    DHarvest.BoolHolidayYesNo = false;
            ////                }
            ////                else
            ////                {
            ////                    DHarvest.BoolHolidayYesNo = false;
            ////                    int fHoliManDays = 0;
            ////                    DHarvest.FlHoliManDays = (float)fHoliManDays;
            ////                }

            ////                if (chkPaidHoliday.Checked)
            ////                {
            ////                    DHarvest.BoolPaidHolidayYesNo = true;
            ////                }
            ////                else
            ////                {
            ////                    DHarvest.BoolPaidHolidayYesNo = false;

            ////                }

            ////                DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
            ////                DHarvest.StrField = cmbField.SelectedValue.ToString();

            ////                //DHarvest.StrBlock=cmbBlock.SelectedItem.ToString();
            ////                DHarvest.StrCategory = cmbCategory.SelectedValue.ToString();
            ////                if (rbtnGeneral.Checked)
            ////                {
            ////                    DHarvest.StrLabourType = rbtnGeneral.Text.ToString();
            ////                }
            ////                if (rbtnLentLabour.Checked)
            ////                {
            ////                    DHarvest.StrLabourType = rbtnLentLabour.Text.ToString();
            ////                    DHarvest.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
            ////                    DHarvest.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
            ////                    DHarvest.StrLabourField = cmbLabourField.SelectedValue.ToString();
            ////                }
            ////                if (rbtnInterEstate.Checked)
            ////                {
            ////                    DHarvest.StrLabourType = rbtnInterEstate.Text.ToString();
            ////                    DHarvest.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
            ////                }
            ////                DHarvest.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
            ////                DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
            ////                DHarvest.StrEmpNo = txtEmpNo.Text;
            ////                //DHarvest.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
            ////                DHarvest.StrEmpName = txtEmpName.Text;
            ////                DHarvest.StrJob = cmbJobCode.SelectedValue.ToString();
            ////                if (chkTaskCompleted.Checked) DHarvest.BoolTaskCompletedYesNo = true;
            ////                else DHarvest.BoolTaskCompletedYesNo = false;
            ////                DHarvest.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
            ////                //mandays for holidays need to be implemented.......
            ////                if (chkHoliday.Checked)
            ////                {
            ////                    if (DHarvest.IntFullHalf == 2)
            ////                    {
            ////                        DHarvest.FlManDays = (float)(DHarvest.FlHoliManDays);
            ////                    }
            ////                    else if (DHarvest.IntFullHalf == 1)
            ////                    {
            ////                        double mValue = 0.5;
            ////                        DHarvest.FlManDays = (float)mValue;
            ////                    }
            ////                }
            ////                else if (!chkHoliday.Checked)
            ////                {
            ////                    DHarvest.FlManDays = (float)(DHarvest.IntFullHalf / 2.0);
            ////                }
            ////                if (DHarvest.StrJob == "PLK")
            ////                {
            ////                    DHarvest.FlQty = float.Parse(txtQty.Text);
            ////                    DHarvest.FlOKgs = float.Parse(txtOverKilos.Text);
            ////                    DHarvest.DecQty1 = float.Parse(txtQty1.Text);
            ////                    DHarvest.DecQty2 = float.Parse(txtQty2.Text);
            ////                    DHarvest.DecQty3 = float.Parse(txtQty3.Text);
            ////                    DHarvest.DecAreaCovered = float.Parse(txtAreaCovered.Text);
            ////                    DHarvest.DecFieldWeight = float.Parse(txtFieldWeight.Text);
            ////                }
            ////                else
            ////                {
            ////                    DHarvest.FlQty = 0;
            ////                    DHarvest.FlOKgs = 0;
            ////                    DHarvest.DecQty1 = 0;
            ////                    DHarvest.DecQty2 = 0;
            ////                    DHarvest.DecQty3 = 0;
            ////                    DHarvest.DecAreaCovered = float.Parse(txtAreaCovered.Text);
            ////                    DHarvest.DecFieldWeight = float.Parse(txtFieldWeight.Text);
            ////                }
            ////                //DHarvest.FlNorm = float.Parse(txtNorm.Text);

            ////                if (cmbFullHalf.SelectedValue.ToString().Equals("1"))
            ////                    DHarvest.FlNorm = float.Parse(txtNormTempVal.Text);
            ////                else
            ////                    DHarvest.FlNorm = float.Parse(txtNormTempVal.Text); //float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.Text.ToUpper(), dateTimePicker1.Value.Date).ToString());

            ////                //DHarvest.FlHours=txtHours.Text;
            ////                DHarvest.StrUserId = User.StrUserName;
            ////                if (DHarvest.StrJob == "PLK" && DHarvest.FlQty < 1)
            ////                {
            ////                    MessageBox.Show("Qty cannot be 0");
            ////                    txtQty1.Focus();
            ////                }
            ////                else
            ////                {
            ////                    /*Blocked for BPL*/
            ////                    //try
            ////                    //{
            ////                    //    DHarvest.BoolIsContract = false;
            ////                    //    DHarvest.StrContractor = "NA";
            ////                    //    DHarvest.DecContractorRate = 0;
            ////                    //    String status = DHarvest.UpdateHarvetEntry();


            ////                    //    if (status.Equals("UPDATED"))
            ////                    //    {
            ////                    //        MessageBox.Show("Daily Harvest Entry Updated Successfully! ");
            ////                    //        btnCancel.PerformClick();
            ////                    //    }
            ////                    //    else if (status.Equals("NOTEXISTS"))
            ////                    //    {
            ////                    //        MessageBox.Show("Not Exists");
            ////                    //    }
            ////                    //}
            ////                    //catch (Exception ex)
            ////                    //{
            ////                    //    MessageBox.Show("Error, ", ex.Message);
            ////                    //}

            ////                    try
            ////                    {
            ////                        String status = "";

            ////                        status = DHarvest.DeleteHarvetEntry();

            ////                        if (status.Equals("DELETED"))
            ////                        {
            ////                            DHarvest.StrACCode = "00";
            ////                            DHarvest.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
            ////                            DHarvest.StrGangNo = cmbGangNumber.SelectedValue.ToString();
            ////                            DHarvest.BoolCashOkgYesNo = false;
            ////                            DHarvest.BoolSpeciMedHalf=false;
            ////                            status = DHarvest.InsertHarvetEntry();

            ////                            if (status == "ADDED")
            ////                            {
            ////                                MessageBox.Show("Updated successfully.!");
            ////                                btnCancel.PerformClick();
            ////                            }
            ////                            else
            ////                                MessageBox.Show("Something went wrong.!, Select relavant employee first");
            ////                        }
            ////                        else
            ////                            MessageBox.Show("Something went wrong.!, Select relavant employee first");
            ////                    }
            ////                    catch (Exception ex)
            ////                    {
            ////                        MessageBox.Show("Error..!\n" + ex.Message);
            ////                    }
            ////                }
            ////                //}                    
            ////                //else
            ////                //{
            ////                //    MessageBox.Show("Update Past Entries Blocked!, Please Contact System Admin.");
            ////                //}

            ////            }
            ////            else
            ////                MessageBox.Show("Please Select Data Before Update");
            ////        }
            ////        else
            ////        {
            ////            MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Entries Blocked");

            ////        }
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        MessageBox.Show(ex.Message);
            ////    }
            ////}

            ////End:
                Application.DoEvents();
          
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
                DHarvest.StrLabourEstate = "NA";
                DHarvest.StrLabourDivision = "NA";
                DHarvest.StrLabourField = "NA";
            }
        }

        private void rbtnInterEstate_CheckedChanged(object sender, EventArgs e)
        {
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

        private void cmbJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DHarvest.StrJob != "PLK")
            {
                DHarvest.FlQty = 0;
                chkTaskCompleted.Enabled = true;
            }
            /*for plk task completed when achieve norm it will be assigned in sp*/
            String job = cmbJob.Text;
            if (cmbJob.Text=="PLK")
            {
                chkTaskCompleted.Enabled = false;
            }
            else
            {
                chkTaskCompleted.Enabled = true;
                DHarvest.FlQty = 0;
            }
        }

       
        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Trim() != "")
            {
                txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                txtEmpNo_LeaveChanged();
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                {
                    //SetNormDetails();
                    SetNormDetailsByLabourType();
                }
                else
                {
                    InitializeNorms();
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
                    if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()) && EmpMaster.IsEPFEntitled(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                    {
                        EmpMaster.StrGender = EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        if (chkBlockPlk.Checked)
                        {
                            txtQty1.Focus();
                        }
                        else
                        {
                            cmbJobCode.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive Or Not EPF Entitled","Invalid Entry");
                        txtEmpNo.Text = "";
                        txtEmpNo.Focus();
                    }
                }
            }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmpNo_TextChanged(object sender, EventArgs e)
        {
            chkTaskCompleted.Enabled = true;
        }

        private void chkHoliday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHoliday.Checked)
            {
                DHarvest.BoolHolidayYesNo = true;
            }
            else
            {
                DHarvest.BoolHolidayYesNo = false;
            }
        }

        private void cmdCloseEntry_Click(object sender, EventArgs e)
        {
            /*Blocked for BPL*/
            //try
            //{
            //    if (MessageBox.Show("Are you Sure you Want to Close " + dateTimePicker1.Value.Date.ToShortDateString()+ " Day Entries?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
            //        DHarvest.StrUserId = User.StrUserName;
            //        DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
            //        DataTable DivisionTbl;
            //        DivisionTbl = EstDivBlock.ListActiveDivisions();
            //        foreach (DataRow drow in DivisionTbl.Rows)
            //        {
            //            String state = DHarvest.CloseDayEntries(drow[0].ToString(), dateTimePicker1.Value.Date);
            //            if (state.Equals("CLOSED"))
            //            {
            //                //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntries(dateTimePicker1.Value.Date);
            //                gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0);
            //                MessageBox.Show(drow[0].ToString()+" Division - Day Harvest Entries Closed Successfully!");
            //            }
            //            else
            //            {
            //                MessageBox.Show("Error!");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            EmployeeList empList = new EmployeeList();
            empList.Show();
        }

        private void cmbLabourField_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbHoliManDays.Text = "0.00";
            chkHoliday.Checked = false;
            chkPaidHoliday.Checked = false;
            DateChanged();
            if (e.KeyChar==13)
            {
                //DateChanged();
                //txtACCode.Focus();              
                cmbMusterChit.Focus();
            }
            else
            {
                dateTimePicker1.Focus();
            }

            ////DataSet ds = new DataSet();
            ////ds = myHoli.GetMonthHolidays((Convert.ToInt32(dateTimePicker1.Value.Year)), (Convert.ToInt32(dateTimePicker1.Value.Month)));

            ////if (ds.Tables[0].Rows.Count > 0)
            ////{
            ////    if (((Convert.ToString(dateTimePicker1.Value.Year)) == (myHoli.GetMonthHolidaysYear((dateTimePicker1.Value.Year)).Tables[0].Rows[0][1].ToString())) && ((Convert.ToString(dateTimePicker1.Value.Month) == (myHoli.GetMonthHolidaysMonth((dateTimePicker1.Value.Month)).Tables[0].Rows[0][2].ToString()))))
            ////    {
                    
            ////        ds = myHoli.GetPoyaNSunday(dateTimePicker1.Value.Date);

            ////        if (ds.Tables[0].Rows.Count > 0)
            ////        {
            //////            if ((Convert.ToString(dateTimePicker1.Value.Date)) == (myHoli.GetPoyaNSunday(dateTimePicker1.Value.Date).Tables[0].Rows[0][0].ToString()))
            //////            {

            ////                chkHoliday.Checked = true;
            ////                chkHoliday.Enabled = false;
            ////                cmbHoliManDays.Text = "1.5";

            //                gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
            //    //        }
            //    //    }
            //    //    else
            //    //    {
            //    //        chkHoliday.Enabled = true;
            //    //        chkHoliday.Checked = false;
            //    //    }
            //    //}                
            ////}

            ////else
            ////{
            ////    MessageBox.Show("Enter Monthly Holidays to procceed DailyHarvest Entries...!","Message ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            ////    this.Close();
            ////}
            //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
            gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()),Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
           
        }

        private void DateChanged()
        {
            String StrPreDayState="";

            /*Blocked for BPL*/
            //StrPreDayState=DHarvest.CheckPreviousDayEntries(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()));
            StrPreDayState = "OK";

            if (StrPreDayState.Equals("OK"))
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
                try
                {
                cmbMusterChit.DataSource = MChit.ListMusterChitForSelectedDate(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
                cmbMusterChit.DisplayMember = "MChitName";
                cmbMusterChit.ValueMember = "AutoMusterID";

                cmbMusterChit_SelectedIndexChanged(null, null);

                
                    gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false);
                }
                catch { }
                refreshSummaryDetails();
            }
            else 
            {
                /*Blocked for BPL*/
                //if (StrPreDayState.Equals("NOFW"))
                //{
                //    MessageBox.Show("You Have Not Entered Previous Day Field Weight!\r\nPlease Enter " + dateTimePicker1.Value.Date.AddDays(-1).ToShortDateString() + " Field Weight To Proceed ", "Entries Blocked");
                //    dateTimePicker1.Focus();
                //}
                //else if (StrPreDayState.Equals("NOAC"))
                //{
                //    MessageBox.Show("You Have Not Entered Previous Day Area Covered!\r\nPlease Enter " + dateTimePicker1.Value.Date.AddDays(-1).ToShortDateString() + " Area Covered To Proceed ", "Entries Blocked");
                //    dateTimePicker1.Focus();
                //}
                //else
                //{
                //    MessageBox.Show("You Have Not Entered Previous Day Entries!\r\nPlease Enter " + dateTimePicker1.Value.Date.AddDays(-1).ToShortDateString() + " Day Entries To Proceed ", "Entries Blocked");
                //    dateTimePicker1.Focus();
                //}

            }
        }

        private void chkHoliday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                rbtnGeneral.Focus();
            }
        }

        private void cmbCropType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                cmbEstate.Focus();
            }
        }

        private void cmbHoliManDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
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

       

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbWorkType.Focus();
            }
        }

        private void acmbJob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }       

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        
        {
            if (e.KeyChar==13)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList(this,cmbDivision.SelectedValue.ToString());
                    empList.ShowDialog();

                }
                else
                {
                    if (txtEmpNo.Text.Trim() != "")
                    {
                        txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                        //txtEmpNo_LeaveChanged();
                        txtJobShortName_LeaveChanged();
                    }
                }

            }
            

        }

        private void txtEmpName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                chkTaskCompleted.Focus();
            }
        }

        private void chkTaskCompleted_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbFullHalf.Focus();
            }
        }

        private void cmbFullHalf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQty.Focus();
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Decimal normVal = 0;
                Decimal Qtyval = 0;
                Decimal OverKgs=0;
                //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.Text));
                normVal = Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(),EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "PLK",dateTimePicker1.Value.Date));
                Qtyval = Convert.ToDecimal(txtQty.Text);
                if ((Qtyval - normVal) > 0)
                {
                    OverKgs = Qtyval - normVal;
                    txtOverKilos.Text = OverKgs.ToString();
                }
                else
                {
                    txtOverKilos.Text = "0";
                }
                btnAdd.PerformClick();
                //btnAdd.Focus();
            }
        }

        private void cmbField_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();

            //ds = myField.getFieldName(cmbField.SelectedValue.ToString(),cmbDivision.SelectedValue.ToString());
            //if (ds.Tables.Count != 0)
            //{
            //    txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();                
            //}
            txtFieldName.Text = "";
            
        }

        private void cmbLabourField_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (rbtnInterEstate.Checked)
            {
                cmbField.Enabled = false;
            }
            else
            {
                cmbField.Enabled = true;
            }
            //DataSet ds = new DataSet();
            /////check this 
            //ds = myField.getFieldName(cmbLabourField.SelectedValue.ToString(),cmbDivision.SelectedValue.ToString());
            //if (ds.Tables.Count != 0)
            //{
            //    txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
            //}
        }
        public void fieldChanged()
        {
            DataSet ds = new DataSet();
            ds = myField.getFieldName(cmbField.SelectedValue.ToString(),cmbDivision.SelectedValue.ToString());
            if (ds.Tables.Count > 0)
            {
                txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                //txtACCode.Focus();
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

        private void txtJobShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XXX") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XPR") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XMT") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PH"))
            //    {
            //        cmbJobCode.SelectedValue= cmbJobCode.SelectedValue.ToString();
            //    }
            //    txtJobShortName_LeaveChanged();
            //}
        }

        private void txtJobShortName_LeaveChanged()
        {
            //txtQty.Enabled = true;
            if (cmbJobCode.SelectedIndex>-1)
            {

                if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    cmbJobCode.SelectedIndex=-1;
                    cmbJobCode.Focus();
                }
                else
                {
                    //if (!DHAccounts.IsJobAvaialbleInACMaster(cmbJobCode.Text, txtACCode.Text))
                    //{
                    //    if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    //    {
                    //        txtJobShortName.Focus();
                    //    }

                    //}
                    #region MyRegion
                    /*display norm  */
                    //norm
                    ////dsNorm = DivNorm.GetFieldwisePlkNorm(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString(), dateTimePicker1.Value.Date);
                    ////if (dsNorm.Tables[0].Rows.Count > 0)
                    ////{
                    ////    txtMaleNorm.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                    ////    txtNorm.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                    ////    txtPRINorm.Text = dsNorm.Tables[0].Rows[0][2].ToString();
                    ////    txtFemalePRI.Text = dsNorm.Tables[0].Rows[0][3].ToString();

                    ////    if (EmpMaster.StrGender.ToUpper().Equals("F"))
                    ////    {
                    ////        if (cmbFullHalf.SelectedValue.ToString() == "1")
                    ////        {
                    ////            txtNormTempVal.Text = Math.Ceiling(Convert.ToDecimal(dsNorm.Tables[0].Rows[0][1].ToString()) / Convert.ToDecimal("2")).ToString();
                    ////            //txtNormTempVal.Focus();
                    ////            txtQty.Focus();
                    ////        }
                    ////        else
                    ////        {
                    ////            txtNormTempVal.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                    ////            txtQty1.Focus();
                    ////        }
                    ////    }
                    ////    else
                    ////    {
                    ////        if (cmbFullHalf.SelectedValue.ToString() == "1")
                    ////        {
                    ////            txtNormTempVal.Text = Math.Ceiling(Convert.ToDecimal(dsNorm.Tables[0].Rows[0][0].ToString()) / Convert.ToDecimal("2")).ToString();
                    ////            //txtNormTempVal.Focus();
                    ////            txtQty.Focus();
                    ////        }
                    ////        else
                    ////        {
                    ////            txtNormTempVal.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                    ////            txtQty1.Focus();
                    ////        }
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    MessageBox.Show("Norm Entries Not Available");
                    ////}
                    //norm   
                    #endregion                  
                    
                        txtJobName.Text = Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper());
                        cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString().ToUpper();
                        if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PH"))
                        {
                            chkTaskCompleted.Checked = true;
                            txtQty.Enabled=false;
                            btnAdd.Focus();
                        }
                        else
                        {
                            if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                            {
                                //norm selection comes here
                                //SetNormDetails();
                                SetNormDetailsByLabourType();
                                txtQty1.Focus();
                                //txtQty.Focus();
                            }
                            else
                            {
                                if (cmbFullHalf.Text.Equals("Full"))
                                {
                                    chkTaskCompleted.Checked = true;
                                    txtQty.Enabled = false;
                                    //this.txtAreaCovered.Focus();
                                    //btnAdd.PerformClick();
                                    txtSundryTask.Focus();
                                    //btnAdd.Focus();
                                }
                                else
                                {
                                    //txtAreaCovered.Focus();
                                    //btnAdd.PerformClick();
                                    btnAdd.Focus();
                                    //chkTaskCompleted.Focus();
                                }
                            }
                        }
                    }
                    
                }
        }

        public void SetNormDetailsByLabourType()
        {
            if (rbtnGeneral.Checked)
            {
                SetNormDetails(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString());
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

        public void InitializeNorms()
        {
            txtMaleNorm.Text = "0";
            txtFemaleNorm.Text = "0";
            txtMalePRINorm.Text = "0";
            txtFemalePRINorm.Text = "0";
            txtPRITempVal.Text = "0";
            txtNormTempVal.Text = "0";

        }

        public void SetNormDetails(String Division, String Field)
        {
            txtMaleNorm.Text = "0";
            txtFemaleNorm.Text = "0";
            txtMalePRINorm.Text = "0";
            txtFemalePRINorm.Text = "0";
            txtPRITempVal.Text = "0";
            txtNormTempVal.Text = "0";
           
            try
            {
                dsNorm = DivNorm.GetFieldwisePlkNorm(Division, Field, dateTimePicker1.Value.Date);
                if (dsNorm.Tables[0].Rows.Count > 0)
                {
                    txtMaleNorm.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                    txtFemaleNorm.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                    txtMalePRINorm.Text = dsNorm.Tables[0].Rows[0][2].ToString();
                    txtFemalePRINorm.Text = dsNorm.Tables[0].Rows[0][3].ToString();
                    

                    if (EmpMaster.StrGender.ToUpper().Equals("F"))
                    {
                        txtNormTempVal.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                        txtPRITempVal.Text = dsNorm.Tables[0].Rows[0][3].ToString();
                    }
                    else
                    {
                        txtNormTempVal.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                        txtPRITempVal.Text = dsNorm.Tables[0].Rows[0][2].ToString();                        
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


        public void SetNormDetails()
        {
            //txtMaleNorm.Text = "0";
            //txtNorm.Text = "0";
            //txtPRINorm.Text = "0";

            //txtFemalePRI.Text = "0";
            
            //try
            //{
                
            //    dsNorm = DivNorm.GetFieldwisePlkNorm(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString(), dateTimePicker1.Value.Date);
            //    if (dsNorm.Tables[0].Rows.Count > 0)
            //    {
            //        txtMaleNorm.Text = dsNorm.Tables[0].Rows[0][0].ToString();
            //        txtNorm.Text = dsNorm.Tables[0].Rows[0][1].ToString();
            //        txtPRINorm.Text = dsNorm.Tables[0].Rows[0][2].ToString();
            //        txtFemalePRI.Text = dsNorm.Tables[0].Rows[0][3].ToString();

            //        if (EmpMaster.StrGender.ToUpper().Equals("F"))
            //        {
            //            //if (cmbFullHalf.SelectedValue.ToString() == "1")
            //            //{
            //            //    txtTapNorm.Text = Math.Ceiling(Convert.ToDecimal(dsNorm.Tables[0].Rows[0][1].ToString()) / Convert.ToDecimal("2")).ToString();

            //            //    //txtNormTempVal.Focus();
            //            //    txtQty.Focus();
            //            //}
            //            //else
            //            //{
            //                txtNormTempVal.Text = dsNorm.Tables[0].Rows[0][1].ToString();
            //            //}
            //        }
            //        else
            //        {
            //            //if (cmbFullHalf.SelectedValue.ToString() == "1")
            //            //{
            //            //    txtTapNorm.Text = Math.Ceiling(Convert.ToDecimal(dsNorm.Tables[0].Rows[0][0].ToString()) / Convert.ToDecimal("2")).ToString();
            //            //    //txtNormTempVal.Focus();
            //            //    txtQty.Focus();
            //            //}
            //            //else
            //            //{
            //            txtNormTempVal.Text = dsNorm.Tables[0].Rows[0][0].ToString();
            //            //}
            //        }
               
                    
            //    }
            //    else
            //    {
            //        MessageBox.Show("Norm Entries Not Available");
            //    }
            //}
            //catch { }
            ////norm   
        }

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

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void chkPaidHoliday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaidHoliday.Checked)
            {
                //MessageBox.Show("Please Add Entries From Daily Harvest - Cashwork Form.");
                //this.Close();
                //DHarvest.BoolPaidHolidayYesNo = true;

                //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
                //cmbWorkType.DisplayMember = "Name";
                //cmbWorkType.ValueMember = "Code";
                DataTable dtPH;
                dtPH=DHarvest.ListPHHarvestForDivision(dateTimePicker1.Value.Date, FTSPayRollBL.User.StrDivision);
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
                DHarvest.BoolPaidHolidayYesNo = false;
                cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Normal");
                cmbWorkType.DisplayMember = "Name";
                cmbWorkType.ValueMember = "Code";
            }
        }

        private void cmbLabourEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbLabourEstate.SelectedIndex>-1 )
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
                        MessageBox.Show("You Must Download Borrowing Estate Division Fields To Proceed","No Divisions Found",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                    MessageBox.Show("Download Other Estate Details From Master Data Download");
                }
                else
                {
                    MessageBox.Show("Cannot Refresh " + cmbLabourEstate.SelectedValue.ToString() + " Estate Division Fields Without Connection", "Cannot Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void chkPaidHoliday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                rbtnGeneral.Focus();
            }
        }

        private void rbtnInterEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourEstate_SelectedIndexChanged(null, null);
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
            //txt_IsNum(txtQty.Text);
            string context = this.txtQty.Text;
            if (Convert.ToDecimal(context) > FTSSettings.GetCheckrollRateValueByType("QtyMax"))
            {
                MessageBox.Show("Qty Value Should be less than 3 digits!");
                txtQty.Clear();
                txtQty.Focus();
            }
            Decimal normVal = 0;
            Decimal Qtyval = 0;
            Decimal OverKgs = 0;
            //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.Text));
            normVal = Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "PLK", dateTimePicker1.Value.Date));

            //Get 3 Weightment Total
            if (txtQty.Text.Trim() != "" && txtQty1.Text.Trim() != "" && txtQty2.Text.Trim() != "" && txtQty3.Text.Trim() != "")
            {
                Qtyval = Convert.ToDecimal(txtQty1.Text.Trim());
                Qtyval += Convert.ToDecimal(txtQty2.Text.Trim());
                Qtyval += Convert.ToDecimal(txtQty3.Text.Trim());
            }

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

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            //DateChanged();
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            Boolean OpenedDate = false;
            String strDateOk = "";
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            /*Blocked for BPL*/
            if (FTSPayRollBL.User.BoolDayBlockAvailable && !chkSLMFLabour.Checked)
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

                    cmbDivision_SelectedIndexChanged(null, null);
                    //if (!FTSSettings.GetNormType().ToUpper().Equals("FIELD"))
                    //{
                    //    //if (DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()) > 0 && DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "PLK", dateTimePicker1.Value.Date, "%") > 0)
                    //    //{
                    //    //    txtMaleNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "M", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()).ToString();
                    //    //    txtNorm.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), "F", "PLK", dateTimePicker1.Value.Date, cmbField.SelectedValue.ToString()).ToString();
                    //    //}
                    //    //else
                    //    //{
                    //    //    MessageBox.Show("Please Add Norm Value For This Division And Proceed.");
                    //    //    //this.Close();
                    //    //}
                    //}
                    //else
                    //{
                    //    dsNorm = DivNorm.GetFieldwisePlkNorm(cmbDivision.SelectedValue.ToString(), cmbField.SelectedValue.ToString(),dateTimePicker1.Value.Date);
                    //    txtMaleNorm.Text = dsNorm.Tables[0].Rows[0][0].ToString();
                    //    txtNorm.Text = dsNorm.Tables[0].Rows[0][1].ToString();
                    //    txtPRINorm.Text = dsNorm.Tables[0].Rows[0][2].ToString();
                    //    txtFemalePRI.Text = dsNorm.Tables[0].Rows[0][3].ToString();
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
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.","Entries Blocked");
               
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                myEntries.AddBlockDates();
                dateTimePicker1.Focus();
            }
            else if (strDateOk.Equals("POST_DATE_BLOCK"))
            {
                MessageBox.Show("Post Date Entry Blocked.", "Entries Blocked");

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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
             //DateTime dt = dateTimePicker1.Value.Date;
             DateChanged();
        }

        private void txtQty1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                if (e.KeyChar == 13)
                {
                    if (!String.IsNullOrEmpty(txtQty1.Text))
                    {
                        string context = this.txtQty1.Text;
                        if (Convert.ToDecimal(context) > FTSSettings.GetCheckrollRateValueByType("QtyMax"))
                        {
                            MessageBox.Show("Qty Value Should be less than 3 digits!");
                            txtQty1.Clear();
                            txtQty1.Focus();
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(txtQty1.Text))
                            {
                                txtQty1.Focus();
                            }
                            else
                            {
                                //txtQty2.Focus();
                                //----
                                txtQty2.Text = "0";
                                txtQty3.Text = "0";
                                Decimal decQty = 0;
                                decQty = Convert.ToDecimal(txtQty1.Text) + Convert.ToDecimal(txtQty2.Text) + Convert.ToDecimal(txtQty3.Text);
                                txtQty.Text = decQty.ToString();
                                //-------------------
                                //CalcOverkilos();
                                CalculateOverKilos();
                                //txtAreaCovered.Focus();
                                btnAdd.PerformClick();
                                //----
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Qty1 Cannot Be Empty.");
                        txtQty1.Focus();
                    }
                }
            
        }

        private void txtQty2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
                if (e.KeyChar == 13)
                {
                    if (!String.IsNullOrEmpty(txtQty2.Text))
                    {
                        string context = this.txtQty2.Text;
                        if (Convert.ToDecimal(context) >FTSSettings.GetCheckrollRateValueByType("QtyMax"))
                        {
                            MessageBox.Show("Qty Value Should be less than 3 digits!");
                            txtQty2.Clear();
                            txtQty2.Focus();
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(txtQty1.Text))
                            {
                                txtQty2.Focus();
                            }
                            else
                            {
                                txtQty3.Focus();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Qty2 Cannot Be Empty.");
                        txtQty2.Focus();
                    }
                }
           
        }

        private void txtQty3_KeyPress(object sender, KeyPressEventArgs e)
        {           
                if (e.KeyChar == 13)
                {
                    if (!String.IsNullOrEmpty(txtQty3.Text))
                    {
                        string context = this.txtQty3.Text;
                        if (Convert.ToDecimal(context) > FTSSettings.GetCheckrollRateValueByType("QtyMax"))
                        {
                            MessageBox.Show("Qty Value Should be less than 3 digits!");
                            txtQty3.Clear();
                            txtQty3.Focus();
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(txtQty1.Text))
                            {
                                txtQty3.Focus();
                            }
                            else
                            {
                                Decimal decQty = 0;
                                decQty = Convert.ToDecimal(txtQty1.Text) + Convert.ToDecimal(txtQty2.Text) + Convert.ToDecimal(txtQty3.Text);
                                txtQty.Text = decQty.ToString();
                                //-------------------
                                //CalcOverkilos();
                                CalculateOverKilos();
                                //txtAreaCovered.Focus();
                                btnAdd.PerformClick();
                                //btnAdd.Focus();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Qty3 Cannot Be Empty.");
                        txtQty3.Focus();
                    }
                }
           
        }

        public void CalcTotQty()
        {

        }

        private void txtQty1_Leave(object sender, EventArgs e)
        {
            //Decimal decQt1 = 0;
            //decQt1=Convert.ToDecimal(txtQty1.ToString()) + Convert.ToDecimal(txtQty2.ToString()) + Convert.ToDecimal(txtQty3.ToString());
            //txtQty.Text = decQt1.ToString();
            //CalcOverkilos();
        }

        private void txtQty2_Leave(object sender, EventArgs e)
        {
            //Decimal decQt2 = 0;
            //decQt2 = Convert.ToDecimal(txtQty1.ToString()) + Convert.ToDecimal(txtQty2.ToString()) + Convert.ToDecimal(txtQty3.ToString());
            //txtQty.Text = decQt2.ToString();
            //CalcOverkilos();
        }

        private void txtQty3_Leave(object sender, EventArgs e)
        {
            //Decimal decQt3 = 0;
            //decQt3 = Convert.ToDecimal(txtQty1.ToString()) + Convert.ToDecimal(txtQty2.ToString()) + Convert.ToDecimal(txtQty3.ToString());
            //txtQty.Text = decQt3.ToString();
            //CalcOverkilos();
        }

        private void CalculateOverKilos()
        {
            Decimal normVal = 0;
            Decimal Qtyval = 0;
            Decimal OverKgs = 0;
            Decimal PRINorm = 0;

            
                if (EmpMaster.StrGender.ToUpper().Equals("M"))
                {
                    normVal = Convert.ToDecimal(txtMaleNorm.Text);
                    PRINorm = Convert.ToDecimal(txtMalePRINorm.Text);
                    Qtyval = Convert.ToDecimal(txtQty.Text);
                    txtNormTempVal.Text = txtMaleNorm.Text;
                    txtPRITempVal.Text = txtMalePRINorm.Text;
                }
                else
                {
                    normVal = Convert.ToDecimal(txtFemaleNorm.Text);
                    PRINorm = Convert.ToDecimal(txtFemalePRINorm.Text);
                    Qtyval = Convert.ToDecimal(txtQty.Text);
                    txtNormTempVal.Text = txtFemaleNorm.Text;
                    txtPRITempVal.Text = txtFemalePRINorm.Text;
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

        public void CalcOverkilos()
        {
            Decimal normVal = 0;
            Decimal Qtyval = 0;
            Decimal OverKgs = 0;
            //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.Text));
            if (String.IsNullOrEmpty(txtQty1.Text))
            {
                txtQty1.Text = "0";
            }
            if (String.IsNullOrEmpty(txtQty2.Text))
            {
                txtQty2.Text = "0";
            }
            if (String.IsNullOrEmpty(txtQty3.Text))
            {
                txtQty3.Text = "0";
            }
            Decimal decQt = 0;
            decQt = Convert.ToDecimal(txtQty1.Text) + Convert.ToDecimal(txtQty2.Text) + Convert.ToDecimal(txtQty3.Text);
            txtQty.Text = decQt.ToString();
            if (String.IsNullOrEmpty(txtNormTempVal.Text))
            {

                txtNormTempVal.Text = "0";
            }
            else
            {
                if (cmbFullHalf.SelectedValue.ToString().Equals("1"))
                {
                    //normVal = Math.Ceiling(Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "PLK", dateTimePicker1.Value.Date)) / Convert.ToDecimal("2"));
                    normVal = Convert.ToDecimal(txtNormTempVal.Text);
                }
                else
                {
                    //normVal = Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "PLK", dateTimePicker1.Value.Date));
                    normVal = Convert.ToDecimal(txtNormTempVal.Text);
                }
                Qtyval = Convert.ToDecimal(txtQty.Text);
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
        }

        private void txtAreaCovered_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                if (e.KeyChar == 13)
                {
                    if (!String.IsNullOrEmpty(txtAreaCovered.Text))
                    {
                        string context = txtAreaCovered.Text;
                        if (float.Parse(context) > 10000.00)
                        {
                            MessageBox.Show("Area Value Should be less than 4 digits!");
                            this.txtAreaCovered.Clear();
                            txtAreaCovered.Focus();
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(txtAreaCovered.Text))
                            {
                                MessageBox.Show("Area Covered Cannot Be Empty!");
                                txtAreaCovered.Focus();
                            }
                            else
                            {
                                txtFieldWeight.Focus();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Area Covered Cannot Be Empty");
                        txtAreaCovered.Focus();
                    }
                }
           
        }

        private void txtFieldWeight_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtFieldWeight.Text))
                {
                    string context = txtFieldWeight.Text;
                    if (float.Parse(context) > 10000.00)
                    {
                        MessageBox.Show("Field Weight Should be less than 5 digits!");
                        this.txtFieldWeight.Clear();
                        txtFieldWeight.Focus();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtFieldWeight.Text))
                        {
                            MessageBox.Show("Field Weight Cannot Be Empty!");
                            txtAreaCovered.Focus();
                        }
                        else
                        {
                            btnAdd.Focus();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Field Weight Cannot Be Empty");
                    txtAreaCovered.Focus();
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /*Blocked for BPL*/
                //if (MessageBox.Show("Are you Sure you Want to Close " + dtpDateToClose.Value.Date.ToShortDateString() + " Day Entries?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    DHarvest.StrUserId = User.StrUserName;
                //    DataTable DivisionTbl;
                //    DivisionTbl = EstDivBlock.ListActiveDivisions();
                //    foreach (DataRow drow in DivisionTbl.Rows)
                //    {
                //        String state = DHarvest.CloseDayEntries(drow[0].ToString(),dtpDateToClose.Value.Date);
                //        if (state.Equals("CLOSED"))
                //        {
                //            //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntries(dateTimePicker1.Value.Date);
                //            //gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                //            MessageBox.Show(drow[0].ToString() + " Division - Day Harvest Entries Closed Successfully!");
                //        }
                //        else
                //        {
                //            MessageBox.Show("Error!");
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkBlockPlk_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkBlockPlk.Checked)
            //{
            //    if (MessageBox.Show("Do You Need To Add Block Plucking? ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        chkBlockPlk.Checked = true;
            //        DHarvest.BoolBlockPlk = true;
            //        cmbJobCode.Text = "PLK";
            //        txtJobShortName.Enabled = false;
            //        txtContractor.Enabled = true;
            //        dateTimePicker1.Focus();
            //    }
            //    else
            //    {
            //        chkBlockPlk.Checked = false;
            //        DHarvest.BoolBlockPlk = false;
            //        cmbJobCode.Text = "";
            //        txtJobShortName.Enabled = true;
            //        dateTimePicker1.Focus();
            //    }
            //}
            //else
            //{
            //    chkBlockPlk.Checked = false;
            //    dateTimePicker1.Focus();
            //    DHarvest.BoolBlockPlk = false;
            //}
        }

        private void chkBlockPlk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDivision.Focus();
            }
        }

        private void txtContractor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtContractor.Text.Equals("?"))
            {
                ContractorSearch ContractorList = new ContractorSearch(this, cmbDivision.SelectedValue.ToString());
                ContractorList.Show();
            }
            else
            {
                if (e.KeyChar == 13)
                {

                    txtContractor_LeaveChanged();
                }
            }
        }

        private void cmbFullHalf_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            cmbFullHalf_Leave(null,null);
            //if (cmbJobCode.Text.Equals("PLK"))
            //{
            //    if (e.KeyChar == 13)
            //    {
            //        if (cmbFullHalf.SelectedValue.ToString() == "1")
            //        {
            //            txtNormTempVal.Text = Math.Ceiling(Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.Text.ToUpper(), dateTimePicker1.Value.Date)) / Convert.ToDecimal("2")).ToString();
            //            txtNormTempVal.Focus();
            //        }
            //        else
            //        {
            //            txtNormTempVal.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.Text.ToUpper(), dateTimePicker1.Value.Date).ToString();
            //            txtQty1.Focus();
            //        }

            //    }
            //}
        }

        private void txtNormTempVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbJobCode.SelectedValue.ToString().Equals("PLK"))
                {
                    txtQty1.Focus();
                }
                else
                    btnAdd.Focus();
                
                    
            }
        }

        private void cmbFullHalf_Leave(object sender, EventArgs e)
        {
            if (cmbJobCode.SelectedValue.ToString().Equals("PLK"))
            {
                
                    if (cmbFullHalf.SelectedValue.ToString() == "1")
                    {
                        //txtNormTempVal.Text = Math.Ceiling(Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.Text.ToUpper(), dateTimePicker1.Value.Date)) / Convert.ToDecimal("2")).ToString();
                        txtNormTempVal.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.SelectedValue.ToString().ToUpper(), dateTimePicker1.Value.Date).ToString();
                        txtNormTempVal.Focus();
                    }
                    else
                    {
                        txtNormTempVal.Text = DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.SelectedValue.ToString().ToUpper(), dateTimePicker1.Value.Date).ToString();
                        txtQty1.Focus();
                    
                }
            }
            else
            {
                btnAdd.Focus();
            }
                
        }

        private void txtSearchEmpNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchEmpNo.Text.Trim() != "")
                {
                    gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, txtSearchEmpNo.Text.Trim(),false);
                }
                else
                {
                    gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0,false);
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SpecialHalfNames mySpecialHalf = new SpecialHalfNames(this, dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, cmbField.SelectedValue.ToString());
            mySpecialHalf.ShowDialog();
        }

        private void btnChangeField_Click(object sender, EventArgs e)
        {
            cmbField.Focus();
        }

        private void btnJobSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEmpSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void chkSpecialMedicalHalf_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDeleteAllPH_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure You Want To Delete All Paid Holiday Entries Of " + cmbDivision.Text + " For " + dateTimePicker1.Value.Date.ToShortDateString() + "?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    DHarvest.DeleteAllPH(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, "+ex.Message);
                }
            }
        }

        private void txtACCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //DateChanged();
                if (txtACCode.Text.Equals("?"))
                {
                    ACSubCategoryList objAcSubList = new ACSubCategoryList(this);
                    objAcSubList.Show();
                }
                else
                {
                    MainAC_Changed();
                    ////txtACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtACCode.Text);
                    ////if (txtACCodeName.Text.ToUpper().Equals("NA"))
                    ////{
                    ////    MessageBox.Show("Account Code Not Found!");
                    ////}
                    ////else
                    ////{
                    ////    if (String.IsNullOrEmpty(txtACCodeName.Text))
                    ////    {
                    ////        MessageBox.Show("Account Code Not Found");
                    ////        txtACCode.Focus();
                    ////    }
                    ////    else
                    ////    {
                    ////        rbtnGeneral.Focus();
                    ////    }
                    ////}
                }
                
            }
            else
            {
                txtACCode.Focus();
            }
        }



        private void cmbFullHalf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtJobShortName_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtMusterChitNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbField.Focus();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FieldWiseSummaryEntry myFWS = new FieldWiseSummaryEntry();
            myFWS.MdiParent = this;
            myFWS.Show();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            FieldWiseSummaryEntry myFWS = new FieldWiseSummaryEntry();
            //myFWS.MdiParent = this;
            myFWS.Show();
        }

        private void txtContractorName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbGangNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbGangNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbField.Focus();
            }
        }

        private void cmbChitNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dtMChit=MChit.ListGangNumbersForSelectedChitNumber(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), cmbDivision.SelectedValue.ToString(), cmbChitNumber.SelectedValue.ToString());
            //    if (dtMChit.Rows.Count > 0)
            //    {
            //        cmbGangNumber.DataSource = dtMChit;
            //        cmbGangNumber.DisplayMember = "GangNumber";
            //        cmbGangNumber.ValueMember = "GangNumber";

            //        cmbField.DataSource = dtMChit;
            //        cmbField.DisplayMember = "FieldID";
            //        cmbField.ValueMember = "FieldID";


            //        cmbField.SelectedValue = dtMChit.Rows[0][1].ToString();
            //        fieldChanged();
            //        txtACCode.Text = dtMChit.Rows[0][2].ToString();
            //        txtACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtACCode.Text);
            //        if (txtACCodeName.Text.ToUpper().Equals("NA"))
            //        {
            //            MessageBox.Show("Account Code Not Found!");
            //        }
            //        else
            //        {
            //            if (String.IsNullOrEmpty(txtACCodeName.Text))
            //            {
            //                MessageBox.Show("Account Code Not Found");
            //                txtACCode.Focus();
            //            }
            //            else
            //            {
            //                rbtnGeneral.Focus();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        cmbField.SelectedIndex = -1;
            //        txtACCode.Text = "";
            //    }


            //}
            //catch { }


        }

        private void cmbChitNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbGangNumber.Focus();
            }
        }

        private void txtACCode_TextChanged(object sender, EventArgs e)
        {
            //if (!txtACCode.Text.Equals("0369"))
            //{
            //    if (DHAccounts.GetAvailableJobsForSubCategory(txtACCode.Text).Rows.Count > 0)
            //    {
            //        cmbJobCode.DataSource = DHAccounts.GetAvailableJobsForSubCategory(txtACCode.Text);
            //        cmbJobCode.DisplayMember = "JobCode";
            //        cmbJobCode.ValueMember = "JobCode";
            //    }
            //    else
            //    {
            //        cmbJobCode.DataSource = null;
            //    }
            //}
            //else
            //{
            //    if (DHAccounts.GetAvailableJobsForSubCategory("%").Rows.Count > 0)
            //    {
            //        cmbJobCode.DataSource = DHAccounts.GetAvailableJobsForSubCategory("%");
            //        cmbJobCode.DisplayMember = "JobCode";
            //        cmbJobCode.ValueMember = "JobCode";
            //    }
            //    else
            //    {
            //        cmbJobCode.DataSource = null;
            //    }
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
                cmbJobCode.DisplayMember = "JobShortName";
                cmbJobCode.ValueMember = "JobShortName";
                txtNoOfEmployees.Text = dtMusterData.Rows[0][11].ToString();
                dsSunTask = null;
                cmbJobCode.SelectedValue = dtMusterData.Rows[0][10].ToString();
                txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
                dsSunTask = Job1.GetSundryTask(dtMusterData.Rows[0][10].ToString(),intCropType);
                if (dsSunTask.Tables[0].Rows.Count > 0)
                {
                    lblTaskUnit.Text = dsSunTask.Tables[0].Rows[0][0].ToString();
                    lblTaskValue.Text = dsSunTask.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    lblTaskUnit.Text = "...";
                    lblTaskValue.Text = "Task Not Defined";
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

                txtEmpNo.Focus();
                //rbtnGeneral.Focus();
                //MainAC_Changed();

            }
            catch { }

        }

        private void cmbMusterChit_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cmbMusterChit_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar==13)
            {
                cmbChitNumber.Focus();
            }
        }

        private void MainAC_Changed()
        {
            
                txtACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtACCode.Text);
                if (txtACCodeName.Text.ToUpper().Equals("NA"))
                {
                    MessageBox.Show("Account Code Not Found!");
                }
                else
                {
                    if (String.IsNullOrEmpty(txtACCodeName.Text))
                    {
                        MessageBox.Show("Account Code Not Found");
                        txtACCode.Focus();
                    }
                    else
                    {
                        txtEmpNo.Focus();
                        //rbtnGeneral.Focus();
                    }
                }
            
        }

        private void chkTaskCompleted_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        private void cmbJobCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XXX") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XPR") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XMT") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PH"))
                {
                    cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString();
                }
                //txtJobShortName_LeaveChanged();
            }
            catch { }
        }

        private void txtSundryTask_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();
            }
        }

        public void SetPRIFromSundryTask()
        {
            if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
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

                        //btnAdd.Focus();
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
        }

        private void txtSundryTask_TextChanged(object sender, EventArgs e)
        {
            
            if(cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
            {
                //txtQty1.Focus();
            } 
            else
            {
                
                DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(),intCropType);
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

                        //btnAdd.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Task Cannot Be Zero");
                        //txtSundryTask.Text = "0.0";
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

        }

        
        

    }
}