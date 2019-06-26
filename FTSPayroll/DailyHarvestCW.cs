using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class DailyHarvestCW : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.EstateDivisionBlock myField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MonthlyHoliday myHoli = new FTSPayRollBL.MonthlyHoliday();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.SystemSetting ChkSettings = new FTSPayRollBL.SystemSetting();
        FTSPayRollBL.AccountInformation DHAccounts = new AccountInformation();
        BlockEntries myEntries = new BlockEntries();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();
        DataTable dtDaySummary = new DataTable();
        FTSPayRollBL.Validation clsValidation = new FTSPayRollBL.Validation();

        Boolean Clicked = false;

        public DailyHarvestCW()
        {
            InitializeComponent();
        }

        private void DailyHarvestCW_Load(object sender, EventArgs e)
        {
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

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";

            cmbNameValue.DataSource = FTSSettings.ListDataFromCheckrollRates("DailyBasic");
            cmbNameValue.DisplayMember = "Amount";
            cmbNameValue.ValueMember = "Amount";

            cmbCashPlkRate.DataSource = FTSSettings.ListCashKiloRatesFromCheckrollRates();
            cmbCashPlkRate.DisplayMember = "Amount";
            cmbCashPlkRate.ValueMember = "Amount";

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

            chkBlockPlk.Checked = false;
            dateTimePicker1.Focus();
            DHarvest.BoolBlockPlk = false;

            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";

            chkBlockPlk.Focus();
            cmbNameValue.SelectedValue = FTSSettings.ListDataFromCheckrollRatesDefault("DailyBasic");

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
                                //if (DivNorm.getLatestNormOfDivision(cmbDivision.Text) > 0)
                                //{
                                //    txtNorm.Text = DivNorm.getLatestNormOfDivision(cmbDivision.Text).ToString();
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Please Add Norm Value For This Division And Proceed.");
                                //    this.Close();
                                //}
                            }
                            FTSPayRollBL.EmployeeMaster.DHarvestDivision = cmbDivision.SelectedValue.ToString();
                            //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Decimal decAvailCashMandays = 0;
           
                txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                txtEmpNo_LeaveChanged();
                //txtJobShortName_LeaveChanged();
            
            if (clsValidation.ExpenditureJournalValidation(dateTimePicker1.Value.Date) == true)
            {
                MessageBox.Show("Expenditure Journal For " + dateTimePicker1.Value.Date.Year.ToString() + "/" + dateTimePicker1.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                goto End;
            }
            else if (FTSSettings.IsEntryValidationAgainstMusterEmpCount() && MChit.IsEmpHeadCountExceed(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()), Convert.ToInt32(txtNoOfEmployees.Text)))
            {
                MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:" + txtNoOfEmployees.Text.ToString() + "\r\n Already Entered Count:" + txtAvailableEmpCount.Text);
            }
            else if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text.PadLeft(5, '0'), cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txtEmpNo.Focus();
                }
                else if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    cmbJobCode.Focus();
                }
                else if (String.IsNullOrEmpty(cmbNameValue.SelectedValue.ToString()))
                {
                    MessageBox.Show("Select The Correct Daily Basic Amount");
                }
                //else if (String.IsNullOrEmpty(txtACCode.Text))
                //{
                //    MessageBox.Show("Account Code");
                //    txtACCode.Focus();
                //}
                // else if (DHAccounts.GETSubCategoryNameByCode(txtACCode.Text).ToUpper().Equals("NA"))
                //{
                //    MessageBox.Show("Account Code Not Found!");
                //    txtACCode.Focus();
                //}
                else if(cmbCashPlkRate.DataSource==null || cmbCashPlkRate.SelectedIndex<0)
                 {
                     MessageBox.Show("Cash Kg Rate Not Selected!");
                     cmbCashPlkRate.Focus();
                 }

             
                else
                {
                    DHarvest.DecSundryTaskCompleted = 0;

                    try
                    {
                        DHarvest.DecNameValue = Convert.ToDecimal(cmbNameValue.SelectedValue.ToString());
                        if (dateTimePicker1.Value.Date == DateTime.Now.Date)
                        {
                            if (MessageBox.Show("Please confirm the selected date..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            {
                                dateTimePicker1.Focus();
                                goto End;
                            }
                        }

                        //if (!DHAccounts.IsJobAvaialbleInACMaster(cmbJobCode.SelectedValue.ToString(), txtACCode.Text))
                        //{
                        //    MessageBox.Show("Job Code Not Avaialble For Main Code Given");
                        //    goto End;
                        //    //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        //    //{
                        //    //    goto End;
                        //    //}

                        //}

                        decAvailCashMandays = DHarvest.GetEmployeeAvailableCashManDays(DHarvest.DtHarvestDate, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
                        if (decAvailCashMandays >= 1)
                        {
                            if (MessageBox.Show("Employee:" + txtEmpNo.Text + " Already Has " + decAvailCashMandays + " ManDays,\r\n\r\n Do You Want To Proceed?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (MessageBox.Show("You are going to enter more than " + decAvailCashMandays + DHarvest.FlManDays + " manday(s) to Emp:" + DHarvest.StrEmpNo, "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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

                        CalcOverkilos();
                        DHarvest.BoolIsContract = false;
                        DHarvest.StrContractor = "NA";
                        DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
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
                        //no pri for cashworkers
                        DHarvest.BoolTaskCompletedYesNo = false;
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
                        if (DHarvest.StrJob == "PLK" || DHarvest.StrJob == "PLKN")
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
                        if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                        {
                            DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.SelectedValue.ToString().ToUpper()).ToString());
                        }
                        else
                        {
                            DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "Other").ToString());
                        }
                        DHarvest.BoolBlockPlk2013 = false;
                        //DHarvest.FlNorm = float.Parse(txtNorm.Text);
                        //DHarvest.FlHours=txtHours.Text;
                        DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
                        DHarvest.StrACCode = txtACCode.Text;
                        DHarvest.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                        DHarvest.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                        DHarvest.DecCashPlkRate = Convert.ToDecimal(cmbCashPlkRate.SelectedValue.ToString());
                        try
                        {
                            DHarvest.BoolIsContract = false;
                            DHarvest.StrContractor = "NA";
                            DHarvest.DecContractorRate = 0;
                            String status = "";
                            if (txtQty.Text.Length > 3)
                            {
                                MessageBox.Show("Cannot Enter More Than 4 Digits For Quantity");
                                status = "Not Added";

                            }
                            else
                            {
                                status = DHarvest.InsertHarvetEntry();
                            }

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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

        End:
            Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            txtQty.Clear();
            txtOverKilos.Clear();
            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
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

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
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
            txtOverKilos.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtEmpNo.Focus();
            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";
            txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();

            //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType");
            //cmbWorkType.DisplayMember = "Name";
            //cmbWorkType.ValueMember = "Code";

            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),0,false);
                refreshSummaryDetails();
            }
        }

        private void gvDailyHarvest_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clicked = true;
            txtEmpNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtOverKilos.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbField.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString();
            cmbNameValue.SelectedValue = Convert.ToDecimal(gvDailyHarvest.Rows[e.RowIndex].Cells[15].Value.ToString());
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

            DataTable gridDt = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString()),txtEmpNo.Text.Trim(),2);

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
                cmbJobCode.SelectedValue = gridDt.Rows[0][10].ToString();
                txtJobShortName_LeaveChanged();

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

                cmbFullHalf.SelectedValue = int.Parse(gridDt.Rows[0][12].ToString());
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
            //cmbFullHalf.SelectedValue = int.Parse(gvDailyHarvest.Rows[e.RowIndex].Cells[11].Value.ToString());          
            //txtQty.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[12].Value.ToString();

            dateTimePicker1.Enabled = false;
            txtEmpNo.Enabled = false;
            cmbDivision.Enabled = false;
            cmbWorkType.Enabled = false;

            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            #region Create Comment20130625
            //String strDateOk = "";
            //myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            //strDateOk =  myEntries.CheckDateDifference();
            //if ((strDateOk.Equals("OK")))
            //{
            //    if (!String.IsNullOrEmpty(lblRefNo.Text))
            //    {
            //        DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

            //        try
            //        {
            //            String status = DHarvest.DeleteHarvetEntry();

            //            if (status.Equals("DELETED"))
            //            {
            //                MessageBox.Show("Daily Harvest Entry Deleted Successfully! ");
            //            }
            //            else if (status.Equals("NOTEXISTS"))
            //            {
            //                MessageBox.Show("Not Exists");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Error, " + ex.Message);
            //        }
            //    }
            //    else
            //        MessageBox.Show("Please Select Data Before Delete");
            //    btnCancel.PerformClick();
            //}
            //else
            //{
            //    MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

            //}
            #endregion

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
                    MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                 if (clsValidation.ExpenditureJournalValidation(dateTimePicker1.Value.Date) == true)
                {
                    MessageBox.Show("Expenditure Journal For " + dateTimePicker1.Value.Date.Year.ToString() + "/" + dateTimePicker1.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    goto End;
                }
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txtEmpNo.Focus();
                }
                if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    cmbJobCode.Focus();
                }

                if (dateTimePicker1.Value.Date == DateTime.Now.Date)
                {
                    if (MessageBox.Show("Please confirm the selected date..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        dateTimePicker1.Focus();
                        goto End;
                    }
                }
                // if (!DHAccounts.IsJobAvaialbleInACMaster(cmbJobCode.SelectedValue.ToString(), txtACCode.Text))
                //{
                //    MessageBox.Show("JOb Not Found In Accounts");
                //    goto End;
                //    //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                //    //{
                //    //    goto End;
                //    //}

                //}

                /*Blocked for BPL*/
                String strDateOk = "OK";
                DHarvest.BoolIsContract = false;
                DHarvest.StrContractor = "NA";
                myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
                //strDateOk = myEntries.CheckDateDifference();

                if ((strDateOk.Equals("OK")))
                {
                    if (!String.IsNullOrEmpty(lblRefNo.Text))
                    {
                        CalcOverkilos();
                        DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
                        DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                        if (chkHoliday.Checked)
                            DHarvest.BoolHolidayYesNo = true;
                        else DHarvest.BoolHolidayYesNo = false;
                        DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                        DHarvest.StrField = cmbField.SelectedValue.ToString();
                        //DHarvest.StrBlock=cmbBlock.SelectedItem.ToString();
                        DHarvest.StrCategory = cmbCategory.SelectedItem.ToString();
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
                        }
                        DHarvest.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
                        DHarvest.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());
                        DHarvest.StrEmpNo = txtEmpNo.Text;
                        //DHarvest.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
                        DHarvest.StrEmpName = txtEmpName.Text;
                        DHarvest.StrJob = cmbJobCode.SelectedValue.ToString();
                        if (chkTaskCompleted.Checked) DHarvest.BoolTaskCompletedYesNo = true;
                        else DHarvest.BoolTaskCompletedYesNo = false;
                        DHarvest.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
                        //mandays for holidays need to be implemented.......
                        DHarvest.FlManDays = (float)(DHarvest.IntFullHalf / 2.0);
                        if (DHarvest.StrJob == "PLK" || DHarvest.StrJob == "PLKN")
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
                        if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                        {
                            DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), cmbJobCode.SelectedValue.ToString().ToUpper()).ToString());
                        }
                        else
                        {
                            DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "Other").ToString());
                        }
                        if (chkBlockPlk.Checked)
                        {
                            DHarvest.BoolBlockPlk = true;
                        }
                        else
                        {
                            DHarvest.BoolBlockPlk = false;
                        }
                        DHarvest.BoolBlockPlk2013 = false;
                        //DHarvest.FlHours=txtHours.Text;
                        DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
                        DHarvest.StrACCode = txtACCode.Text;
                        DHarvest.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                        DHarvest.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                        /*Blocked for BPL*/
                        //try
                        //{
                        //    DHarvest.BoolIsContract = false;
                        //    DHarvest.StrContractor = "NA";
                        //    DHarvest.DecContractorRate = 0;
                        //    String status = DHarvest.UpdateHarvetEntry();


                        //    if (status.Equals("UPDATED"))
                        //    {
                        //        MessageBox.Show("Daily Harvest Entry Updated Successfully! ");
                        //        btnCancel.PerformClick();
                        //    }
                        //    else if (status.Equals("NOTEXISTS"))
                        //    {
                        //        MessageBox.Show("Not Exists");
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Error, ", ex.Message);
                        //}

                        try
                        {
                            String status = "";

                            status = DHarvest.DeleteHarvetEntry();

                            if (status.Equals("DELETED"))
                            {
                                status = DHarvest.InsertHarvetEntry();

                                if (status == "ADDED")
                                {
                                    MessageBox.Show("Updated successfully.!");
                                    btnCancel.PerformClick();
                                }
                                else
                                    MessageBox.Show("Something went wrong.!, Select relavant employee first");
                            }
                            else
                                MessageBox.Show("Something went wrong.!, Select relavant employee first");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error..!\n" + ex.Message);
                        }

                    }
                    else
                        MessageBox.Show("Please Select Data Before Update");
                }
                else
                {
                    MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        End:
            Application.DoEvents();
        }

        private void cmbLabourDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.cmbLabourDivision.SelectedItem.ToString().Equals(""))
                {
                    cmbLabourField.DataSource = EstDivBlock.ListDivisionFields(cmbLabourDivision.SelectedValue.ToString());
                    cmbLabourField.DisplayMember = "FieldID";
                    cmbLabourField.ValueMember = "FieldID";

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
                cmbLabourField.Enabled = false;
                cmbLabourDivision.Enabled = false;
                cmbLabourEstate.Enabled = true;

                cmbLabourEstate.DataSource = EstDivBlock.ListOtherEstates();
                cmbLabourEstate.DisplayMember = "EstateName";
                cmbLabourEstate.ValueMember = "EstateID";

                //DHarvest.StrLabourEstate = "NA";
                DHarvest.StrLabourDivision = "NA";
                DHarvest.StrLabourField = "NA";
            }
        }

        public Boolean IsActiveEmployee(String emp, String div)
        {
            if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
            {
                return false;
            }
            else
            {
                return true;
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
                        cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        //cmbJobCode.Focus();
                        if (cmbJobCode.Text.ToUpper().Equals("PLK"))
                        {
                            txtQty1.Focus();
                        }
                        else
                        {
                            if (cmbFullHalf.Text.Equals("Full"))
                            {
                                chkTaskCompleted.Checked = true;
                                this.btnAdd.Focus();
                            }
                            else
                            {
                                btnAdd.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive");
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
            try
            {
                MessageBox.Show("Please Use DailyHarvest Form To Close Dates...");
                //if (MessageBox.Show("Are you Sure you Want to Close " + dateTimePicker1.Value.Date.ToShortDateString() + " Day Entries?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                //    DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
                //    DataTable DivisionTbl;
                //    DivisionTbl = EstDivBlock.ListActiveDivisions();
                //    foreach (DataRow drow in DivisionTbl.Rows)
                //    {
                //        String state = DHarvest.CloseDayEntries(drow[0].ToString(),dateTimePicker1.Value.Date);
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
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            EmployeeList empList = new EmployeeList();
            empList.Show();
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

            cmbMusterChit_SelectedIndexChanged(null, null);
           
                gvDailyHarvest.DataSource = DHarvest.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false);
            }
            catch { }
            refreshSummaryDetails();
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbHoliManDays.Text = "0.00";
            chkHoliday.Checked = false;
            chkPaidHoliday.Checked = false;
            if (e.KeyChar == 13)
            {
                DateChanged();
                //cmbField.Focus();
                cmbChitNumber.Focus();
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

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dateTimePicker1.Focus();
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

        private void cmbLabourDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourField.Focus();
            }
        }

        private void cmbLabourField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
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
                            txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                        }
                        if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                        {

                            if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                            {
                                MessageBox.Show("Please Enter a Correct Job Code.");
                                cmbJobCode.SelectedIndex = -1;
                                cmbJobCode.Focus();
                            }
                            else
                            {
                                txtJobName.Text = Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper());
                                cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString().ToUpper();
                                if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK") || this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLKN"))
                                {
                                    //txtQty.Focus();
                                    txtQty1.Focus();
                                }
                                else
                                {
                                    if (cmbFullHalf.Text.Equals("Full"))
                                    {
                                        txtQty.Enabled = false;
                                        btnAdd.Focus();
                                    }
                                    else
                                    {
                                        btnAdd.Focus();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Select A Valid Employee");
                        }
                   
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Decimal normVal = 0;
                Decimal Qtyval = 0;
                Decimal OverKgs = 0;
                normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.SelectedItem.ToString()));
                Qtyval = Convert.ToDecimal(txtQty.Text);
                if ((Qtyval - normVal) > 0)
                {
                    OverKgs = Qtyval - normVal;
                    txtOverKilos.Text = "0";
                }
                else
                {
                    txtOverKilos.Text = "0";
                }
                txtAreaCovered.Focus();
            }
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldName.Text = "";
        }

        public void fieldChanged()
        {
            DataSet ds = new DataSet();
            ds = myField.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
            if (ds.Tables.Count > 0)
            {
                txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                txtACCode.Focus();
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
            if (e.KeyChar == 13)
            {
                if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                {
                    cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString();
                }
                txtJobShortName_LeaveChanged();
            }
        }

        private void txtJobShortName_LeaveChanged()
        {
            //txtQty.Enabled = true;
            if (!String.IsNullOrEmpty(cmbJobCode.SelectedValue.ToString()))
            {
                if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    cmbJobCode.SelectedIndex = -1;
                    cmbJobCode.Focus();
                }
                else
                {
                    txtJobName.Text = Job1.JobNameByShortName(this.cmbJobCode.SelectedValue.ToString().ToUpper());
                    cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString().ToUpper();
                    if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK") || this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLKN"))
                    {
                        //txtQty.Focus();
                        txtQty1.Focus();
                    }
                    else
                    {
                        if (cmbFullHalf.Text.Equals("Full"))
                        {
                            chkTaskCompleted.Checked = true;
                            txtQty.Enabled = false;

                            
                                //btnAdd.PerformClick();                            
                        }
                        else
                        {
                                                        
                                //btnAdd.PerformClick();
                        }
                    }
                }
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
                //DHarvest.BoolPaidHolidayYesNo = true;

                //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
                //cmbWorkType.DisplayMember = "Name";
                //cmbWorkType.ValueMember = "Code";
                DataTable dtPH;
                dtPH = DHarvest.ListPHHarvestForDivision(dateTimePicker1.Value.Date, FTSPayRollBL.User.StrDivision);
                if (dtPH.Rows.Count < 1)
                {
                    MessageBox.Show("Extra Free Names Not Added For Paid Holiday");
                    this.Close();
                    ////AddExtraNames AddExNames = new AddExtraNames();
                    ////AddExNames.Show();
                }
            }
            else
            {
                DHarvest.BoolPaidHolidayYesNo = false;
                cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType", "Cash Work");
                cmbWorkType.DisplayMember = "Name";
                cmbWorkType.ValueMember = "Code";
            }
        }

        private void DailyHarvestCW_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
               
            }
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                EmployeeList empList = new EmployeeList();
                empList.Show();
            }
            else
            {
                txtEmpNo_LeaveChanged();
            }          
        }

        private void rbtnInterEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourEstate.Focus();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateChanged();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

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
                txtQty1.Focus();
            }

        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            //txt_IsNum(txtQty.Text);
            string context = this.txtQty.Text;
            if (float.Parse(context) > 10000.00)
            {
                MessageBox.Show("Qty Value Should be less than 4 digits!");
                txtQty.Clear();
                txtQty1.Focus();
            }
        }

        private void txtEmpNo_Leave_1(object sender, EventArgs e)
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

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            String strDateOk = "";
            Boolean OpenedDate = false;
            if (FTSPayRollBL.User.BoolDayBlockAvailable)
            {
                strDateOk =  myEntries.CheckDateDifference();
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
                    dateTimePicker1.Focus();
                    //dateTimePicker1.Value = new DateTime(Convert.ToInt32(User.StrYear),YMonth.GetMonthIdByMonthName(User.StrMonth), 1);
                }
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.","Blocked Entries");
               
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
        }

        private void chkBlockPlk_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBlockPlk.Checked)
            {
                if (MessageBox.Show("Do You Need To Add Block Plucking? ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    chkBlockPlk.Checked = true;
                    DHarvest.BoolBlockPlk = true;
                    dateTimePicker1.Focus();
                }
                else
                {
                    chkBlockPlk.Checked = false;
                    DHarvest.BoolBlockPlk = false;
                    dateTimePicker1.Focus();
                }
            }
            else
            {
                chkBlockPlk.Checked = false;
                dateTimePicker1.Focus();
                DHarvest.BoolBlockPlk = false;
            }
        }

        private void chkBlockPlk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDivision.Focus();
            }
        }

        private void txtQty1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                if (e.KeyChar == 13)
                {
                    if (!String.IsNullOrEmpty(txtQty1.Text))
                    {
                        string context = this.txtQty1.Text;
                        if (float.Parse(context) > 10000.00)
                        {
                            MessageBox.Show("Qty Value Should be less than 5 digits!");
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
                                txtQty2.Text = "0";
                                txtQty3.Text = "0";
                                Decimal decQty = 0;
                                decQty = Convert.ToDecimal(txtQty1.Text) + Convert.ToDecimal(txtQty2.Text) + Convert.ToDecimal(txtQty3.Text);
                                txtQty.Text = decQty.ToString();
                                //-------------------
                                CalcOverkilos();
                                //txtAreaCovered.Focus();
                                btnAdd.PerformClick();
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
                        if (float.Parse(context) > 10000.00)
                        {
                            MessageBox.Show("Qty Value Should be less than 5 digits!");
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
                        MessageBox.Show("Qty1 Cannot Be Empty.");
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
                        if (float.Parse(context) > 10000.00)
                        {
                            MessageBox.Show("Qty Value Should be less than 5 digits!");
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
                                CalcOverkilos();
                                //txtAreaCovered.Focus();
                                btnAdd.PerformClick();
                                //btnAdd.Focus();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("qty3 Cannot Be Empty.");
                        txtQty3.Focus();
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

            normVal = Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "PLK", dateTimePicker1.Value.Date));
            Qtyval = Convert.ToDecimal(txtQty.Text);
            txtOverKilos.Text = "0";
            //if ((Qtyval - normVal) > 0)
            //{
            //    OverKgs = Qtyval - normVal;
            //    txtOverKilos.Text = OverKgs.ToString();
            //}
            //else
            //{
            //    txtOverKilos.Text = "0";
            //}
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
                    MessageBox.Show("Area Covered Cannot Be Empty!");
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
                    if (float.Parse(context) > 100000.00)
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
                            txtFieldWeight.Focus();
                        }
                        else
                        {
                            btnAdd.Focus();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Field Weight Cannot Be Empty!");
                    txtFieldWeight.Focus();
                }
            }
            
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //MessageBox.Show("CloseUp");
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

        private void cmbNameValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDivision.Focus();
            }
        }

        private void txtEmpNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtACCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtACCode.Text.Equals("?"))
                {
                    ACSubCategoryList objAcSubList = new ACSubCategoryList(this);
                    objAcSubList.Show();
                }
                else
                {
                    //DateChanged();
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
                            rbtnGeneral.Focus();
                        }
                    }
                }

            }
            else
            {
                txtACCode.Focus();
            }
            
        }

        private void txtMusterChitNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbField.Focus();
            }
        }

        private void cmbChitNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtMChit = MChit.ListGangNumbersForSelectedChitNumber(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), cmbDivision.SelectedValue.ToString(), cmbChitNumber.SelectedValue.ToString());
                //if (dtMChit.Rows.Count > 0)
                //{
                //    cmbGangNumber.DataSource = dtMChit;
                //    cmbGangNumber.DisplayMember = "GangNumber";
                //    cmbGangNumber.ValueMember = "GangNumber";

                //    cmbField.DataSource = dtMChit;
                //    cmbField.DisplayMember = "FieldID";
                //    cmbField.ValueMember = "FieldID";

                //    cmbField.SelectedValue = dtMChit.Rows[0][1].ToString();
                //    fieldChanged();
                //    txtACCode.Text = dtMChit.Rows[0][2].ToString();
                //    txtACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtACCode.Text);
                //    if (txtACCodeName.Text.ToUpper().Equals("NA"))
                //    {
                //        MessageBox.Show("Account Code Not Found!");
                //    }
                //    else
                //    {
                //        if (String.IsNullOrEmpty(txtACCodeName.Text))
                //        {
                //            MessageBox.Show("Account Code Not Found");
                //            txtACCode.Focus();
                //        }
                //        else
                //        {
                //            rbtnGeneral.Focus();
                //        }
                //    }
                //}
                //else
                //{
                //    cmbField.SelectedIndex = -1;
                //    txtACCode.Text = "";
                //}


            }
            catch { }
        }

        private void cmbChitNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbGangNumber.Focus();
            }
        }

        private void cmbGangNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbField.Focus();
            }
        }

        private void txtACCode_TextChanged(object sender, EventArgs e)
        {
            //if (DHAccounts.GetAvailableJobsForSubCategory(txtACCode.Text).Rows.Count > 0)
            //{
            //    cmbJobCode.DataSource = DHAccounts.GetAvailableJobsForSubCategory(txtACCode.Text);
            //    cmbJobCode.DisplayMember = "JobCode";
            //    cmbJobCode.ValueMember = "JobCode";
            //}
            //else
            //{
            //    cmbJobCode.DataSource = null;
            //}
        }

        private void cmbJobCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XXX") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XPR") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("XMT") && !cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PH"))
            //    {
            //        cmbJobCode.SelectedValue = cmbJobCode.SelectedValue.ToString();
            //    }
            //    txtJobShortName_LeaveChanged();
            //}
        }

        private void cmbMusterChit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
                cmbField.SelectedValue = dtMusterData.Rows[0][3].ToString();
                txtNoOfEmployees.Text = dtMusterData.Rows[0][11].ToString();
                txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
                cmbJobCode.DataSource = Job1.ListJobMaster(dtMusterData.Rows[0][10].ToString());
                cmbJobCode.DisplayMember = "JobShortName";
                cmbJobCode.ValueMember = "JobShortName";
                cmbJobCode.SelectedValue = dtMusterData.Rows[0][10].ToString();

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
                txtACCode.Text = dtMusterData.Rows[0][4].ToString();
                txtEmpNo.Focus();
                //MainAC_Changed();
                //rbtnGeneral.Focus();
            }
            catch { }
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
                    rbtnGeneral.Focus();
                }
            }

        }

        private void refreshSummaryDetails()
        {
            try
            {
                txtSumPlkNames.Text = "0";
                txtSumSunNames.Text = "0";
                txtSumKilos.Text = "0";
                txtSumOverKgs.Text = "0";
                dtDaySummary = DHarvest.GetDaySummary(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false);
                txtSumPlkNames.Text = dtDaySummary.Rows[0][0].ToString();
                txtSumSunNames.Text = dtDaySummary.Rows[0][2].ToString();
                txtSumKilos.Text = dtDaySummary.Rows[0][3].ToString();
                txtSumOverKgs.Text = dtDaySummary.Rows[0][4].ToString();
            }
            catch { }
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


    }
}