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
    public partial class DailyHarvestContractCW : Form
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
        BlockEntries myEntries = new BlockEntries();

        public DailyHarvestContractCW()
        {
            InitializeComponent();
        }

        private void DailyHarvestCW_Load(object sender, EventArgs e)
        {
            lblRefNo.Visible = false;
            DHarvest.BoolFormLoad = true;
            DHarvest.BoolCashOkgYesNo = false;

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

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
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

            DHarvest.BoolIsContract = true;

            chkBlockPlk.Focus();
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
                            gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
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
                        gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
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
                    gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
                }
                catch { }
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtContractor.Text))
            {
                MessageBox.Show("Contractor Name Cannot Be Empty\r\n If You Need To Enter Normal Cash Work\r\n Use \"Daily Harvest(Cash Work) Form\"");
                txtContractor.Focus();
            }
            else
            {
                DHarvest.BoolIsContract = true;
                CalcOverkilos();
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
                if (DHarvest.BoolIsContract == true)
                {
                    DHarvest.StrContractor = txtContractor.Text;
                }
                else
                {
                    DHarvest.BoolIsContract = false;
                    DHarvest.StrContractor = "NA";
                }
                DHarvest.StrEmpNo = txtEmpNo.Text;
                //DHarvest.StrEmpNo=cmbEmpNo.SelectedValue.ToString();
                DHarvest.StrEmpName = txtEmpName.Text;
                DHarvest.StrJob = txtJobShortName.Text;
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
                if (this.txtJobShortName.Text.ToUpper().Equals("PLK"))
                {
                    DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), txtJobShortName.Text.ToUpper()).ToString());
                }
                else
                {
                    DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), "Other").ToString());
                }
                //DHarvest.FlNorm = float.Parse(txtNorm.Text);
                //DHarvest.FlHours=txtHours.Text;
                if (DHarvest.StrJob.Equals("PLK"))
                {
                    DHarvest.DecContractorRate = Convert.ToDecimal(txtPerKiloContribution.Text);
                }
                else
                {
                    DHarvest.DecContractorRate = Convert.ToDecimal(txtPerDayContribution.Text);
                }
                DHarvest.BoolBlockPlk2013 = false;
                DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;
                try
                {
                    if (String.IsNullOrEmpty(DHarvest.StrContractor)||DHarvest.StrContractor.Equals("NA"))
                    {
                        MessageBox.Show("Employee Does Not Have a Contractor.");
                        txtEmpNo.Focus();
                    }
                    else
                    {
                        String status = DHarvest.InsertHarvetEntry();

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
                        else if (status.Equals("CCW EXISTS"))
                        {
                            MessageBox.Show("Contract Cash Work Already Exists");
                            AfterAdd();
                        }
                        else if (status.Equals("BPLK EXISTS"))
                        {
                            MessageBox.Show("Block Plk Already Exists");
                            AfterAdd();
                        }
                        else
                        {
                            MessageBox.Show("Oops, something went wrong!");
                            AfterAdd();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                    btnCancel.PerformClick();
                }                
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpName.Clear();
            txtEmpNo.Clear();
            txtContractor.Clear();
            txtJobShortName.Text = "";
            txtJobName.Text = "";
            txtQty.Clear();
            txtOverKilos.Clear();
            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";
            
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dateTimePicker1.Enabled = true;
            txtEmpNo.Enabled = true;
            cmbDivision.Enabled = true;
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
                gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
            }
            dateTimePicker1.Focus();
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
            //txtContractor.Focus();
            txtEmpNo.Focus();
            txtQty1.Text = "0";
            txtQty2.Text = "0";
            txtQty3.Text = "0";
            txtAreaCovered.Text = "0";
            txtFieldWeight.Text = "0";

            //cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType");
            //cmbWorkType.DisplayMember = "Name";
            //cmbWorkType.ValueMember = "Code";

            if (!String.IsNullOrEmpty(dateTimePicker1.Value.Date.ToString()))
            {
                //gvDailyHarvest.DataSource = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
            }
        }

        private void gvDailyHarvest_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
            cmbField.SelectedValue = gvDailyHarvest.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtQty1.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtQty2.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtQty3.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[11].Value.ToString();
            this.txtAreaCovered.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtFieldWeight.Text = gvDailyHarvest.Rows[e.RowIndex].Cells[13].Value.ToString();

            DataTable gridDt = DHarvest.ListHarvestEntriesForDivision(dateTimePicker1.Value.Date, cmbDivision.Text, Convert.ToInt32(gvDailyHarvest.Rows[e.RowIndex].Cells[8].Value.ToString()),txtEmpNo.Text.Trim(),2);

            if (gridDt.Rows.Count > 0)
            {
                dateTimePicker1.Value = Convert.ToDateTime(gridDt.Rows[0][1].ToString());
                cmbEstate.Text = gridDt.Rows[0][2].ToString();
                cmbDivision.Text = gridDt.Rows[0][8].ToString();
                cmbCropType.Text = gridDt.Rows[0][6].ToString();
                cmbWorkType.Text = gridDt.Rows[0][7].ToString();
                cmbHoliManDays.Text = gridDt.Rows[0][17].ToString();
                if (Convert.ToBoolean(gridDt.Rows[0][18].ToString()))
                {
                    if (gridDt.Rows[0][10].ToString().Equals("PLK"))
                    {
                        txtPerKiloContribution.Text = gridDt.Rows[0][20].ToString();
                    }
                    else
                    {
                        txtPerDayContribution.Text = gridDt.Rows[0][20].ToString();
                    }
                    txtContractor.Text = gridDt.Rows[0][19].ToString();
                }

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
                txtJobShortName.Text = gridDt.Rows[0][10].ToString();
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
            String strDateOk = "";
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            strDateOk =  myEntries.CheckDateDifference();
            if ((strDateOk.Equals("OK")))
            {
                if (!String.IsNullOrEmpty(lblRefNo.Text))
                {
                    DHarvest.IntHatvestEntryId = Convert.ToInt32(lblRefNo.Text);
                    DHarvest.DtHarvestDate = dateTimePicker1.Value.Date;
                    DHarvest.StrDivision = cmbDivision.SelectedValue.ToString();
                    DHarvest.StrField = cmbField.SelectedValue.ToString();
                    DHarvest.StrEmpNo = txtEmpNo.Text;
                    try
                    {
                        String status = DHarvest.DeleteHarvetContractCWEntry();

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
                    DHarvest.BoolIsContract = true;
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
                    if (DHarvest.BoolIsContract == true)
                    {
                        DHarvest.StrContractor = txtContractor.Text;
                    }
                    else
                    {
                        DHarvest.BoolIsContract = false;
                        DHarvest.StrContractor = "NA";
                    }
                    DHarvest.StrEmpNo = txtEmpNo.Text;
                    //DHarvest.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
                    DHarvest.StrEmpName = txtEmpName.Text;
                    DHarvest.StrJob = txtJobShortName.Text;
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
                    if (this.txtJobShortName.Text.ToUpper().Equals("PLK"))
                    {
                        DHarvest.FlNorm = float.Parse(DivNorm.getLatestRelavantNormOfDivision(cmbDivision.SelectedValue.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()), txtJobShortName.Text.ToUpper()).ToString());
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
                    
                    //DHarvest.FlHours=txtHours.Text;
                    if (DHarvest.StrJob.Equals("PLK"))
                    {
                        DHarvest.DecContractorRate = Convert.ToDecimal(txtPerKiloContribution.Text);
                    }
                    else
                    {
                        DHarvest.DecContractorRate = Convert.ToDecimal(txtPerDayContribution.Text);
                    }
                    DHarvest.BoolBlockPlk2013 = false;
                    DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;

                    /*Blocked for BPL*/
                    //try
                    //{
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
                            DHarvest.BoolCashOkgYesNo = false;
                            status = DHarvest.InsertHarvetEntry();

                            if (status == "ADDED")
                            {
                                MessageBox.Show("Updated successfully.!");
                            }
                            else
                                MessageBox.Show("Something went wrong.!, Select relavant employee first");
                        }
                        else
                            MessageBox.Show("Something went wrong.!, Select relavant employee first");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message);
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

        private void txtContractor_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txtContractor.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNoAndEmpType(txtEmpNo.Text, 1)))
                {
                    MessageBox.Show("Please Select A Valid Contractor");
                    txtContractor.Text = "";
                    txtContractor.Focus();
                }
                else
                {
                    txtContractorName.Text = EmpMaster.GetEmployeeNameByEmpNoAndCategory(txtEmpNo.Text, "C");
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
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
                else
                {
                    cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    txtContractor.Text = EmpMaster.GetContractorNoByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                    txtContractor.Enabled = false;
                    txtJobShortName.Focus();
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
            gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
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
            gvDailyHarvest.DataSource = DHarvest.ListContractHarvestForDivision((dateTimePicker1.Value.Date), (cmbDivision.SelectedValue.ToString()),Convert.ToInt32(cmbWorkType.SelectedValue.ToString()),1);
           
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
                //txtEmpNo.Focus();
                txtPerKiloContribution.Focus();
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
                txtPerKiloContribution.Focus();
            }
        }

        private void cmbLabourEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (rbtnInterEstate.Checked)
                {
                    //txtEmpNo.Focus();
                    txtPerKiloContribution.Focus();
                }
                else if (rbtnLentLabour.Checked)
                {
                    cmbLabourDivision.Focus();
                }

            }
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                //EmployeeList empList = new EmployeeList();
                //empList.Show();
                EmployeeList empList = new EmployeeList();
                empList.ShowDialog();
                txtEmpNo.Text = empList.getTextEmployeeNoValue();
                empList.Dispose();
            }
            else
            {
                if (e.KeyChar == 13)
                {                    
                    txtEmpNo_Leave(null, null);
                    if (e.KeyChar == 13)
                    {
                        txtEmpNo_LeaveChanged();
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
                rbtnGeneral.Focus();
            }
            else
            {
                MessageBox.Show("Please Select a Validate Field");
                cmbField.Focus();
            }
        }

        private void cmbField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtJobShortName.Text.ToUpper().Equals("?"))
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
                txtJobShortName_LeaveChanged();
            }
        }

        private void txtJobShortName_LeaveChanged()
        {
            //txtQty.Enabled = true;
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
                    if (this.txtJobShortName.Text.ToUpper().Equals("PLK") || this.txtJobShortName.Text.ToUpper().Equals("PLKN"))
                    {
                        txtQty1.Focus();
                    }
                    else
                    {
                        if (cmbFullHalf.Text.Equals("Full"))
                        {
                            chkTaskCompleted.Checked = true;
                            txtQty.Enabled = false;
                            txtAreaCovered.Focus();
                        }
                        else
                        {
                            txtAreaCovered.Focus();
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
                //if (dtPH.Rows.Count < 1)
                //{
                //    MessageBox.Show("Add Extra Names For Paid Holiday");
                //    this.Close();
                //    AddExtraNames AddExNames = new AddExtraNames();
                //    AddExNames.Show();
                //}
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
                txtQty.Focus();
            }

        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                //txt_IsNum(txtQty.Text);
                string context = this.txtQty.Text;
                if (float.Parse(context) > 10000.00)
                {
                    MessageBox.Show("Qty Value Should be less than 4 digits!");
                    txtQty.Clear();
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Qty Value Error");
                txtQty1.Focus();
            }
        }

        private void txtEmpNo_Leave_1(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                //EmployeeList empList = new EmployeeList();
                //empList.Show();
                EmployeeList empList = new EmployeeList();
                empList.ShowDialog();
                txtEmpNo.Text = empList.getTextEmployeeNoValue();
                empList.Dispose();
            }
            else
            {
                txtEmpNo_LeaveChanged();
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            String strDateOk = "";
            Boolean OpenedDate = false;
            strDateOk =  myEntries.CheckDateDifference();
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
                    dateTimePicker1.Focus();
                    DHarvest.BoolBlockPlk = true;
                }
                else
                {
                    chkBlockPlk.Checked = false;
                    dateTimePicker1.Focus();
                    DHarvest.BoolBlockPlk = false;
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
                                txtQty2.Focus();
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
                                txtAreaCovered.Focus();
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

        private void txtContractor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                ContractorList ConList = new ContractorList();
                ConList.Show();
            }
            else
            {
                if (e.KeyChar == 13)
                {

                    txtContractor_LeaveChanged();
                }
            }
           
        }

        private void txtPerKiloContribution_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPerDayContribution.Focus();
            }
        }

        private void txtPerDayContribution_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

    }
}