using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;
using System.Data.SqlClient;

namespace FTSPayroll
{
    public partial class DailyHarvestOilPalmEntry : Form
    {
        EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        EmployeeCategory EmpCat = new EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        Job Job1 = new Job();
        FTSCheckRollSettings FTSSettings = new FTSCheckRollSettings();
        FTSPayRollBL.DailyHarvestOilPalm DHarvestOP = new FTSPayRollBL.DailyHarvestOilPalm();
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
        Int16 intCropType = 3;
        DataSet dsSunTask;
        String status = "NA";
        String strQty = "KILOS";
        DataSet dsNorm = new DataSet();

        public DailyHarvestOilPalmEntry()
        {
            InitializeComponent();
        }

        private void DailyHarvestOilPalmEntry_Load(object sender, EventArgs e)
        {
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

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType","Oil Palm");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";
            cmbCropType.Text = "Oil Palm";
            cmbCropType.Enabled = false; ;

            chkTaskCompleted.Enabled = false;
            rbtnGeneral.Checked = true;
            dateTimePicker1.Focus();

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            cmbChitNumber.Enabled = false;

            cmbDivision.SelectedValue = FTSPayRollBL.User.StrDivision;

            //cmbDivision_SelectedIndexChanged(null, null);

            DHarvestOP.BoolFormLoad = false;

            if (FTSSettings.GetOilPalmEntryUOM().ToUpper().Equals("KILOS"))
                strQty = "KILOS";
            else
                strQty = "BUNCHES";

            txtOverKilos.Enabled = false;
            

            chkHoliday.Enabled = false;
            chkPaidHoliday.Enabled = false;
            txtPRITempVal.Text = "0";
            dateTimePicker1.Focus();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateChanged();
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

            cmbMusterChit.DataSource = MChit.ListMusterChitForSelectedDate(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
            cmbMusterChit.DisplayMember = "MChitName";
            cmbMusterChit.ValueMember = "AutoMusterID";

            gvDailyHarvest.DataSource = DHarvestOP.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
            DHarvestOP.StrDivision = cmbDivision.SelectedValue.ToString();
            refreshSummaryDetails();
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
                
                dsSunTask = null;
                txtSundryTask.Text = "0";
                dsSunTask = Job1.GetSundryTask(dtMusterData.Rows[0][10].ToString(), intCropType);
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

                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
                {
                    //txtQty1.Focus();
                }
                else
                {

                    DataSet dsTask = Job1.GetSundryTask(cmbJobCode.SelectedValue.ToString(), intCropType);
                    if (dsTask.Tables[0].Rows.Count > 0)
                    {
                        
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
                    if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
                    {
                        //txtOHVNorm.Focus();
                        txtNormTempVal.Text = "0";
                        txtSundryTask.Text = "0";

                        txtSundryTask.Enabled = false;
                        txtBunches.Enabled = true;
                        txtQty.Enabled = true;
                        txtEmpNo.Focus();                        
                    }
                    else
                    {
                        txtNormTempVal.Text = "0";
                        txtBunches.Text = "0";
                        txtQty.Text = "0";
                        txtOverKilos.Text = "0";
                        txtBunches.Enabled = false;
                        txtQty.Enabled = false;
                        txtSundryTask.Enabled = true;
                        txtEmpNo.Focus();
                    }
                }
                if (dtMusterData.Rows[0][10].ToString().ToUpper().Equals("OPH"))
                {
                    //SetNormDetails();
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

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                cmbDivision.Focus();
        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                cmbMusterChit.Focus();
        }

        private void cmbMusterChit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtEmpNo.Focus();
        }

        public void fieldChanged()
        {
            DataSet ds = new DataSet();
            ds = myField.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
            if (ds.Tables.Count > 0)
            {
                txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();               
            }
            else
            {
                MessageBox.Show("Please Select a Validate Field");
                cmbField.Focus();
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
                    if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()) && EmpMaster.IsEPFEntitled(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                    {
                        EmpMaster.StrGender = EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        lblGender.Text = EmpMaster.StrGender;
                        EmpMaster.IntEmpCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
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
                        MessageBox.Show("Employee Is Inactive Or EPF Not Entitled", "Invalid Entry");
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
                        if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
                        {
                            if (strQty.Equals("KILOS"))
                            {
                                txtBunches.Text = "1";
                                txtQty.Focus();
                            }
                            else
                            {
                                txtBunches.Focus();
                            }
                        }
                        else
                        {
                            txtSundryTask.Focus();
                        }
                        
                    }
                }

            }
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
            {
                SetNormDetailsByLabourType();
            }
            txtEmpNo_LeaveChanged();
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

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateChanged();
            }
            catch { }
        }

        private void txtBunches_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtBunches.Text))
                {
                    txtBunches.Text = "1";
                }
                else
                {
                    txtQty.Focus();
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
                    if (!String.IsNullOrEmpty(txtQty.Text))
                    {
                        if (btnAdd.Enabled == true)
                        {
                            btnAdd.Focus();
                        }
                        else
                        {
                            btnUpdate.Focus();
                        }
                    }
                

            }
            
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

            //normVal = Convert.ToDecimal(txtNormTempVal.Text);
            //Qtyval = Convert.ToDecimal(txtQty.Text);
            //PRINorm=Convert.ToDecimal(txtPriNorm.Text);
            if (Convert.ToInt32(cmbWorkType.SelectedValue.ToString()) == 2)
            {
                txtOverKilos.Text = "0";
                chkTaskCompleted.Checked = false;
            }
            else
            {

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
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtQty.Text))
            {
                CalculateOverKilos();
                btnAdd.Focus();
            }
            else
            {
                MessageBox.Show("Invalid Qty");
                txtQty.Focus();
            }
        }

        private void txtSundryTask_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //SundryTask_Changed();
                btnAdd.Focus();
            }

           
        }

        private void SundryTask_Changed()
        {
            
                if (cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
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
                        chkTaskCompleted.Enabled = true;
                        //not available a task
                        if (cmbFullHalf.SelectedValue.ToString().Equals("2"))
                        {
                            chkTaskCompleted.Enabled = true;
                            chkTaskCompleted.Checked = true;
                        }
                        else
                        {
                            chkTaskCompleted.Checked = false;
                           
                        }
                        btnAdd.Focus();

                    }
                }
                //btnAdd.Focus();
            
        }

        private void txtSundryTask_Leave(object sender, EventArgs e)
        {
            SundryTask_Changed();
            //btnAdd.Focus();
        }

        private void cmbFullHalf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnAdd.Focus();
        }

        private void AfterAdd()
        {
            txtBunches.Text = "0";
            txtQty.Text = "0";
            txtOverKilos.Text = "0";
            txtSundryTask.Text = "0";
            txtEmpNo.Clear();
            gvDailyHarvest.DataSource = DHarvestOP.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
            lblGender.Text = "";
            txtAvailableEmpCount.Text = MChit.intGetDailyEntryEmployeeCountForMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())).ToString();
            txtEmpNo.Focus();
            refreshSummaryDetails();


        }
        private void refreshSummaryDetails()
        {
            try
            {
                dtDaySummary = DHarvestOP.GetDaySummary(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), 0, false, Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
                txtSumPlkNames.Text = dtDaySummary.Rows[0][0].ToString();
                txtSumSunNames.Text = dtDaySummary.Rows[0][2].ToString();
                txtSumKilos.Text = dtDaySummary.Rows[0][3].ToString();
                txtSumOverKgs.Text = dtDaySummary.Rows[0][4].ToString();
            }
            catch { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Decimal decAvailNormalMandays = 0;
            if (FTSPayRollBL.User.StrUserName != "admin")
            {
                if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                {
                    DHarvestOP.DtHarvestDate = dateTimePicker1.Value.Date;
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
                DHarvestOP.DtHarvestDate = dateTimePicker1.Value.Date;
            }
            if (FTSSettings.IsEntryValidationAgainstMusterEmpCount() && MChit.IsEmpHeadCountExceed(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()), Convert.ToInt32(txtNoOfEmployees.Text)))
            {
                MessageBox.Show("Cannot Exceed Employee Count Of Muster,\r\n Muster Employee Count:" + txtNoOfEmployees.Text.ToString() + "\r\n Already Entered Count:" + txtAvailableEmpCount.Text);
                goto End;
            }
            decAvailNormalMandays = DHarvest.GetRubberEmployeeAvailableManDays(DHarvestOP.DtHarvestDate, cmbDivision.SelectedValue.ToString(), txtEmpNo.Text, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()));
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
                DHarvestOP.BoolHolidayYesNo = true;
                DHarvestOP.FlHoliManDays = float.Parse(cmbHoliManDays.Text);
                DHarvestOP.BoolPaidHolidayYesNo = false;
            }
            else if (chkPaidHoliday.Checked)
            {
                DHarvestOP.BoolPaidHolidayYesNo = true;
                DHarvestOP.BoolHolidayYesNo = false;
            }
            else
            {
                DHarvestOP.BoolHolidayYesNo = false;
                int fHoliManDays = 0;
                DHarvestOP.FlHoliManDays = (float)fHoliManDays;
            }

            if (chkPaidHoliday.Checked)
            {
                DHarvestOP.BoolPaidHolidayYesNo = true;
            }
            else
            {
                DHarvestOP.BoolPaidHolidayYesNo = false;
            }

            DHarvestOP.StrDivision = cmbDivision.SelectedValue.ToString();
            DHarvestOP.StrField = cmbField.SelectedValue.ToString();
            ///2011-08-11
            //DHarvestOP.StrBlock = cmbBlock.SelectedValue.ToString();
            DHarvestOP.StrBlock = "NA";
            //DHarvestOP.FieldCropType = Convert.ToInt32(cmbFieldCropType.SelectedValue.ToString());

            //DHarvestOP.StrCategory = cmbCategory.SelectedItem.ToString();
            if (rbtnGeneral.Checked)
            {
                DHarvestOP.StrLabourType = rbtnGeneral.Text.ToString();
            }
            if (rbtnLentLabour.Checked)
            {
                DHarvestOP.StrLabourType = rbtnLentLabour.Text.ToString();
                DHarvestOP.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                DHarvestOP.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                DHarvestOP.StrLabourField = cmbLabourField.SelectedValue.ToString();
            }
            if (rbtnInterEstate.Checked)
            {
                DHarvestOP.StrLabourType = rbtnInterEstate.Text.ToString();
                DHarvestOP.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
            }
            DHarvestOP.IntCropType = int.Parse(cmbCropType.SelectedValue.ToString());
            DHarvestOP.IntWorkType = int.Parse(cmbWorkType.SelectedValue.ToString());

            if (txtEmpNo.Text == "")
            {
                MessageBox.Show("Employee No Can't be Empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto End;
            }
            else
            {
                DHarvestOP.StrEmpNo = txtEmpNo.Text;
            }
            //DHarvestOP.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
            DHarvestOP.StrEmpName = txtEmpName.Text;

            if (cmbJobCode.SelectedValue.ToString() != "")
            {
                DHarvestOP.StrJob = (cmbJobCode.SelectedValue.ToString()).ToUpper();
            }
            else
            {
                MessageBox.Show("Job Code can't be empty..!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                goto End;
            }

            //if (chkTaskCompleted.Checked) DHarvestOP.BoolTaskCompletedYesNo = true;
            //else DHarvestOP.BoolTaskCompletedYesNo = false;
            DHarvestOP.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
            //mandays for holidays need to be implemented.......
            if (chkHoliday.Checked)
            {
                if (DHarvestOP.IntFullHalf == 2)
                {
                    DHarvestOP.FlManDays = (float)(DHarvestOP.FlHoliManDays);
                }
                else if (DHarvestOP.IntFullHalf == 1)
                {
                    double mValue = 0.5;
                    DHarvestOP.FlManDays = (float)mValue;
                }
            }
            else if (!chkHoliday.Checked)
            {
                DHarvestOP.FlManDays = (float)(DHarvestOP.IntFullHalf / 2.0);
            }

            if (DHarvestOP.StrJob == "OHV")
            {
                Decimal normVal = 0;
                Decimal Qtyval = 0;
                Decimal OverKgs = 0;
                Decimal PRINorm = 0;

                normVal = Convert.ToDecimal(txtNormTempVal.Text);
                Qtyval = Convert.ToDecimal(txtQty.Text);
                PRINorm = Convert.ToDecimal(txtPRITempVal.Text);

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


                DHarvestOP.FlQty = float.Parse(txtQty.Text.ToString());
                DHarvestOP.FlOKgs = float.Parse(txtOverKilos.Text.ToString());

                DHarvestOP.IntPRINorm = Convert.ToInt32((txtPRITempVal.Text.ToString()));
                if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
                {
                    DHarvestOP.FlNorm = float.Parse(txtNormTempVal.Text);
                }

                if (txtScrapQty.Text != "")
                {
                    DHarvestOP.FlScrapQty = float.Parse(txtScrapQty.Text);
                }
                else
                {
                    DHarvestOP.FlScrapQty = 0;
                }

                DHarvestOP.DecBunches = Convert.ToDecimal(txtBunches.Text);

                


            }
            else
            {
                DHarvestOP.IntFullHalf = int.Parse(cmbFullHalf.SelectedValue.ToString());
                DHarvestOP.FlQty = 0;
                DHarvestOP.FlOKgs = 0;
                DHarvestOP.FlScrapQty = 0;
                DHarvestOP.DecBunches = Convert.ToDecimal(txtBunches.Text);
                //if (DHarvestOP.IntFullHalf == 2)
                //{
                //    DHarvestOP.BoolTaskCompletedYesNo = true;
                //}
                //else
                //{
                //    DHarvestOP.BoolTaskCompletedYesNo = false;
                //}
            }
            if (this.cmbJobCode.SelectedValue.ToString().ToUpper().Equals("OHV"))
            {
                DHarvestOP.FlNorm = float.Parse(txtNormTempVal.Text);
            }

            
            
                DHarvestOP.DecAreaCovered = 0;


            DHarvestOP.StrUserId = User.StrUserName;



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
                    DHarvestOP.StrACCode = "00";
                    DHarvestOP.StrMusterChitNumber = cmbChitNumber.SelectedValue.ToString();
                    DHarvestOP.StrGangNo = cmbGangNumber.SelectedValue.ToString();

                    if (!String.IsNullOrEmpty(txtSundryTask.Text))
                    {
                        DHarvestOP.DecSundryTaskCompleted = Convert.ToDecimal(txtSundryTask.Text);
                    }
                    else
                    {
                        DHarvestOP.DecSundryTaskCompleted = 0;
                    }
                    if (!DHarvestOP.StrJob.ToUpper().Equals("OHV"))
                    {
                        if (Convert.ToInt32(cmbWorkType.SelectedValue.ToString()) == 1)
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
                                if (DHarvestOP.DecSundryTaskCompleted >= Convert.ToDecimal(lblTaskValue.Text))
                                {
                                    DHarvestOP.BoolTaskCompletedYesNo = true;
                                    chkTaskCompleted.Checked = true;
                                }
                                else
                                {
                                    DHarvestOP.BoolTaskCompletedYesNo = false;
                                    chkTaskCompleted.Checked = false;
                                }
                            }
                        }
                        else
                        {
                            DHarvestOP.BoolTaskCompletedYesNo = false;
                            chkTaskCompleted.Checked = false;
                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(cmbWorkType.SelectedValue.ToString()) == 1)
                        {
                            if (chkTaskCompleted.Checked)
                            {
                                DHarvestOP.BoolTaskCompletedYesNo = true;
                            }
                            else
                            {
                                DHarvestOP.BoolTaskCompletedYesNo = false;
                            }
                        }
                        else
                        {
                            DHarvestOP.BoolTaskCompletedYesNo = false;
                        }
                    }

                    
                        DHarvestOP.IntTappingType = 0;
                    if (!cmbJobCode.SelectedValue.ToString().ToUpper().Equals("PLK"))
                    {
                        status = DHarvestOP.InsertHarvetEntry();

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
            End:
            Application.DoEvents();
        }

        private void rbtnGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGeneral.Checked)
            {
                cmbLabourField.Enabled = false;
                cmbLabourDivision.Enabled = false;
                cmbLabourEstate.Enabled = false;
                DHarvestOP.StrLabourEstate = "NA";
                DHarvestOP.StrLabourDivision = "NA";
                DHarvestOP.StrLabourField = "NA";
            }
        }

        private void txtSearchEmpNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvDailyHarvest.DataSource = DHarvestOP.ListHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),"%"+txtSearchEmpNo.Text+"%",Convert.ToInt32(cmbCropType.SelectedValue.ToString()));
            }
            catch (Exception ex){ }
            
        }

        private void gvDailyHarvest_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            lblRefNo.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();

            DataTable gridDt = DHarvestOP.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString()));
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
            txtNormTempVal.Text = gridDt.Rows[0][18].ToString();
            txtBunches.Text = gridDt.Rows[0][24].ToString();



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
            txtEmpNo_Leave(null, null);

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
            if (gridDt.Rows[0][10].ToString().ToUpper().Equals("OHV"))
            {
                txtQty_Leave(null, null);
            }
            txtBunches.Focus();
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                DHarvestOP.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

                try
                {
                    String status = DHarvestOP.DeleteHarvetEntry();
                    if (e != null)
                    {
                        if (status.Equals("DELETED"))
                        {
                            MessageBox.Show("Daily Harvest Entry Deleted Successfully! ");
                            btnCancel.PerformClick();
                            AfterAdd();
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
            DHarvestOP.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);

            try
            {
                String status = DHarvestOP.DeleteHarvetEntry();
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

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtQty.Clear();
            txtBunches.Text = "1";
            txtOverKilos.Text = "0";
            chkTaskCompleted.Checked = false;
            txtSundryTask.Text = "0";
            txtEmpNo.Clear();
            txtEmpName.Clear();

            txtEmpNo.Focus();
        }

        private void cmbWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbWorkType.SelectedValue.ToString()) == 1)
                {
                    DateChanged();
                    txtEmpNo.Focus();
                }
                else
                {
                    DateChanged();
                    chkTaskCompleted.Checked = false;
                    txtOverKilos.Text = "0";
                    txtEmpNo.Focus();
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

        private void rbtnInterEstate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGeneral.Checked)
            {
                cmbLabourField.Enabled = false;
                cmbLabourDivision.Enabled = false;
                cmbLabourEstate.Enabled = false;
                DHarvestOP.StrLabourEstate = "NA";
                DHarvestOP.StrLabourDivision = "NA";
                DHarvestOP.StrLabourField = "NA";
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

        private void txtSundryTask_TextChanged(object sender, EventArgs e)
        {
            SundryTask_Changed();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
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
                    DHarvestOP.DtHarvestDate = dateTimePicker1.Value.Date;

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
        }


    }
}