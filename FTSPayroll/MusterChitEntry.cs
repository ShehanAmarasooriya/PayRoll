using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace FTSPayroll
{
    public partial class MusterChitEntry : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.EstateDivisionBlock myField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.SystemSetting ChkSettings = new FTSPayRollBL.SystemSetting();
        FTSPayRollBL.AccountInformation DHAccounts = new FTSPayRollBL.AccountInformation();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        FTSPayRollBL.DownloadData myDownload = new FTSPayRollBL.DownloadData();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        public Boolean boolDownloadOk = false;


        public MusterChitEntry()
        {
            InitializeComponent();
        }

        private void MusterChitEntry_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettingsExceptGiven("CropType","None");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            cmbDivision.SelectedValue = FTSPayRollBL.User.StrDivision;

            cmbDivision_SelectedIndexChanged(null, null);

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            try
            {
                gvList.DataSource = MChit.ListMusterChitEntry(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
            }
            catch
            {
            }
            cmbLabourEstate.DataSource = EstDivBlock.ListOtherEstates();
            cmbLabourEstate.DisplayMember = "EstateName";
            cmbLabourEstate.ValueMember = "EstateID";

            rbtnGeneral.Checked = true;

        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbDivision.SelectedItem.ToString()))
            {
                lblFieldName.Text = "";
                cmbField.DataSource = EstDivBlock.ListDivisionFieldsByCrop(cmbDivision.SelectedValue.ToString(),cmbCropType.Text);
                cmbField.DisplayMember = "FieldID";
                cmbField.ValueMember = "FieldID";

            }
            
            AfterAdd();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AfterAdd()
        {
            DataTable dtDayTotal;
            txtNoOfEmployees.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            try
            {
                gvList.DataSource = MChit.ListMusterChitEntry(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                dtDayTotal = MChit.ListMusterChitEntryDayTotal(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                txtManDaysTotal.Text = dtDayTotal.Rows[0][0].ToString();
                gvJobList.DataSource = null;
            }
            //catch{} 
            catch(Exception ex)
            {
                MessageBox.Show("Error After Add , " + ex.Message);
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
                    txtACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtACCode.Text);
                    if (txtACCodeName.Text.ToUpper().Equals("NA"))
                    {
                        MessageBox.Show("Account Code Not Found!");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtACCodeName.Text))
                        {
                            //MessageBox.Show("Account Code Not Found");
                            txtNoOfEmployees.Focus();
                        }
                        else
                        {
                            txtNoOfEmployees.Focus();
                        }
                    }
                }

            }
            else
            {
                txtNoOfEmployees.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
       
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

            if ((strDateOk.Equals("OK")))
            {
                if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                {
                    //If Ok
                    string status = "";
                    txtACCode.Text = "00";
                    if (String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
                    {
                        MessageBox.Show("Division is not selected.");
                    }
                    else if (rbtnGeneral.Checked && cmbField.Items.Count < 1)
                    {
                        MessageBox.Show("Fields Not Found");
                        cmbField.Focus();
                    }

                    else if (String.IsNullOrEmpty(cmbField.SelectedValue.ToString()))
                    {
                        MessageBox.Show("Field is not selected.");
                    }
                    else if (String.IsNullOrEmpty(txtACCode.Text))
                    {
                        //MessageBox.Show("Must Enter A Main Code");
                        txtACCode.Text = "00";
                    }
                    //else if (string.IsNullOrEmpty(txtJobShortName.Text))
                    //{
                    //    MessageBox.Show("Job Cannot be Empty");
                    //    txtJobShortName.Focus();

                    //}
                    //else if (String.IsNullOrEmpty(txtNoOfEmployees.Text))
                    //{
                    //    MessageBox.Show("Employee Count Cannot be Empty");
                    //}
                    else if (gvJobList.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Jobs Available");
                    }
                    else if (String.IsNullOrEmpty(txtGangNumber.Text))
                    {
                        MessageBox.Show("Gang number cannot be empty.");
                    }
                    else if (String.IsNullOrEmpty(txtChitNumber.Text))
                    {
                        MessageBox.Show("Chit Number Cannot Be Empty");
                    }
                    //else if (DHAccounts.GETSubCategoryNameByCode(txtACCode.Text).ToUpper().Equals("NA") || String.IsNullOrEmpty(txtACCodeName.Text))
                    //{
                    //    //MessageBox.Show("Account Code Not Found!");
                    //    txtNoOfEmployees.Focus();
                    //}

                    else
                    {

                        MChit.IntCropType = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                        try
                        {
                            if (rbtnGeneral.Checked)
                            {
                                MChit.StrLabourType = rbtnGeneral.Text.ToString();
                                MChit.StrLabourEstate = "NA";
                                MChit.StrLabourDivision = "NA";
                                MChit.StrLabourField = "NA";
                            }
                            if (rbtnLentLabour.Checked)
                            {
                                MChit.StrLabourType = rbtnLentLabour.Text.ToString();
                                MChit.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                                MChit.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                                MChit.StrLabourField = cmbLabourField.SelectedValue.ToString();
                            }
                            if (rbtnInterEstate.Checked)
                            {
                                MChit.StrLabourType = rbtnInterEstate.Text.ToString();
                                MChit.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                                MChit.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                                MChit.StrLabourField = cmbLabourField.SelectedValue.ToString();
                            }
                            MChit.DtDate = dateTimePicker1.Value.Date;
                            MChit.StrDivision = cmbDivision.SelectedValue.ToString();
                            MChit.StrField = cmbField.SelectedValue.ToString();

                            MChit.StrACCode = "00";
                            MChit.StrGangNumber = txtGangNumber.Text;
                            MChit.StrChitNo = txtChitNumber.Text;
                            MChit.StrUser = FTSPayRollBL.User.StrUserName;

                            //---------------
                            for (int j = 0; j < gvJobList.Rows.Count; j++)
                            {
                                String StrActualFieldCrop = "";
                                String StrActualFieldExType = "";
                                String strActualDivision = "";
                                String strActualField = "";
                                if (rbtnGeneral.Checked)
                                {
                                    StrActualFieldCrop = lblFieldCrop.Text;
                                    StrActualFieldExType = lblFieldExType.Text;
                                    strActualDivision = cmbDivision.SelectedValue.ToString();
                                    strActualField = cmbField.SelectedValue.ToString();
                                }
                                if (rbtnLentLabour.Checked)
                                {
                                    StrActualFieldCrop = lblLabourFieldCrop.Text;
                                    StrActualFieldExType = lblLabourFieldExType.Text;
                                    strActualDivision = cmbLabourDivision.SelectedValue.ToString();
                                    strActualField = cmbLabourField.SelectedValue.ToString();
                                }
                                if (rbtnInterEstate.Checked)
                                {
                                    //StrActualFieldCrop = lblLabourFieldCrop.Text;
                                    StrActualFieldCrop = cmbCropType.Text;
                                    StrActualFieldExType = lblLabourFieldExType.Text;
                                    txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
                                    txtJobShortName.Text = txtJobShortName.Text.ToUpper();
                                    txtNoOfEmployees.Focus();
                                    //-------------------------------
                                    DataSet dsJobDetail = new DataSet();

                                    dsJobDetail = Job1.InterEstateJobNameCropAndExTypeByShortName(gvJobList.Rows[j].Cells[0].Value.ToString(), StrActualFieldCrop);

                                    if (dsJobDetail.Tables[0].Rows.Count > 0)
                                    {

                                        MChit.StrJobShortName = gvJobList.Rows[j].Cells[0].Value.ToString();
                                        MChit.IntEmpCount = Convert.ToInt32(gvJobList.Rows[j].Cells[1].Value.ToString());
                                        try
                                        {
                                            status = MChit.InsertMusterChitEntry();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Insert Muster For " + MChit.StrJobShortName + " Failed.., " + ex.Message);
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show(gvJobList.Rows[j].Cells[0].Value.ToString() + " Job Is Not Available For " + lblFieldCrop.Text + " Crop Or " + lblFieldExType.Text + " Expenditure Type");
                                    }
                                    dsJobDetail.Dispose();
                                    //---------------------
                                }
                                else
                                {
                                    DataSet dsJobDetail = new DataSet();
                                    if (EstDivBlock.IsNonCropField(strActualDivision, strActualField))
                                    {
                                        dsJobDetail = Job1.JobNameCropAndExTypeByShortName(gvJobList.Rows[j].Cells[0].Value.ToString(), "NONE", StrActualFieldExType);
                                    }
                                    else
                                    {
                                        dsJobDetail = Job1.JobNameCropAndExTypeByShortName(gvJobList.Rows[j].Cells[0].Value.ToString(), StrActualFieldCrop, StrActualFieldExType);
                                    }
                                    if (dsJobDetail.Tables[0].Rows.Count > 0)
                                    {
                                        if (dsJobDetail.Tables[0].Rows[0][2].ToString().ToUpper().Equals(StrActualFieldExType.ToUpper()))
                                        {
                                            MChit.StrJobShortName = gvJobList.Rows[j].Cells[0].Value.ToString();
                                            MChit.IntEmpCount = Convert.ToInt32(gvJobList.Rows[j].Cells[1].Value.ToString());
                                            try
                                            {
                                                status = MChit.InsertMusterChitEntry();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Insert Muster For " + MChit.StrJobShortName + " Failed.., " + ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Field & Job Expenditure Types Not Matching...");
                                            txtJobShortName.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(gvJobList.Rows[j].Cells[0].Value.ToString() + " Job Is Not Available For " + lblFieldCrop.Text + " Crop Or " + lblFieldExType.Text + " Expenditure Type");
                                    }
                                    dsJobDetail.Dispose();
                                    //------------------------
                                    //if (!gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("PLK") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("TAP") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("XXX") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("XPR") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("XMT") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("PH"))
                                    //{
                                    //    MChit.StrJobShortName = gvJobList.Rows[j].Cells[0].Value.ToString();
                                    //}
                                    //else
                                    //{
                                    //    MChit.StrJobShortName = gvJobList.Rows[j].Cells[0].Value.ToString();
                                    //}                                
                                    //MChit.IntEmpCount = Convert.ToInt32(gvJobList.Rows[j].Cells[1].Value.ToString());
                                    //try
                                    //{
                                    //    status = MChit.InsertMusterChitEntry();
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    MessageBox.Show("Insert Muster For " + MChit.StrJobShortName + " Failed.., " + ex.Message);
                                    //}
                                }
                            }


                            //-----------------

                            if (status.Equals("ADDED"))
                            {
                                MessageBox.Show("Muster Chit Added Successfully.");
                                AfterAdd();
                                txtGangNumber.Focus();
                            }
                            else if (status.Equals("UPDATED"))
                            {
                                MessageBox.Show("Muster Chit Updated");
                                AfterAdd();
                            }
                            else if (status.Equals("ENTRYEXISTS"))
                            {
                                MessageBox.Show("Daily Entries Available For Given Muster Data");
                                AfterAdd();
                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error On Insert , " + ex.Message);
                        }
                    }
                    //If Ok end
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
            //-------------------



        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString());
            cmbDivision.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbDivision_SelectedIndexChanged(null, null);
            try
            {
                cmbField.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
            txtGangNumber.Text = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtChitNumber.Text = gvList.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtACCode.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtNoOfEmployees.Text = gvList.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[13].Value.ToString();
            cmbCropType.SelectedValue = Convert.ToInt32(gvList.Rows[e.RowIndex].Cells[14].Value.ToString());
            if (gvList.Rows[e.RowIndex].Cells[9].Value.ToString() == "General")
            {
                rbtnGeneral.Checked = true;
                cmbLabourEstate.SelectedIndex = -1;
                cmbLabourDivision.SelectedIndex = -1;
                cmbLabourField.SelectedIndex = -1;
            }
            else if (gvList.Rows[e.RowIndex].Cells[9].Value.ToString() == "Lent Labour")
            {
                rbtnLentLabour.Checked = true;
                cmbLabourEstate.SelectedValue = gvList.Rows[e.RowIndex].Cells[10].Value.ToString();
                cmbLabourDivision.SelectedValue = gvList.Rows[e.RowIndex].Cells[11].Value.ToString();
                cmbLabourDivision_SelectedIndexChanged(null, null);
                cmbLabourField.SelectedValue = gvList.Rows[e.RowIndex].Cells[12].Value.ToString();
            }
            else if (gvList.Rows[e.RowIndex].Cells[9].Value.ToString() == "Inter Estate Lent Labour")
            {
                rbtnInterEstate.Checked = true;
                cmbLabourEstate.SelectedValue = gvList.Rows[e.RowIndex].Cells[10].Value.ToString();
                cmbLabourEstate_SelectedIndexChanged(null, null);
                cmbLabourDivision.SelectedValue = gvList.Rows[e.RowIndex].Cells[11].Value.ToString();
                cmbLabourDivision_SelectedIndexChanged(null, null);
                cmbLabourField.SelectedValue = gvList.Rows[e.RowIndex].Cells[12].Value.ToString();

            }
            //gvJobList.DataSource = MChit.GetJobOfMuster(MChit.DtDate, MChit.StrDivision, MChit.StrField, MChit.StrLabourType, MChit.StrLabourEstate, MChit.StrLabourDivision, MChit.StrLabourField, MChit.StrJobShortName, MChit.StrChitNo, MChit.StrGangNumber).Tables[0];
            gvJobList.DataSource = MChit.GetJobOfMuster(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), gvList.Rows[e.RowIndex].Cells[2].Value.ToString(), gvList.Rows[e.RowIndex].Cells[9].Value.ToString(), gvList.Rows[e.RowIndex].Cells[10].Value.ToString(), gvList.Rows[e.RowIndex].Cells[11].Value.ToString(), gvList.Rows[e.RowIndex].Cells[12].Value.ToString(), txtJobShortName.Text, txtChitNumber.Text, txtGangNumber.Text).Tables[0];
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete and Add");
            //btnDelete_Click(null, null);
            //btnAdd_Click(null, null);
            //        myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            //        Boolean OpenedDate = false;
            //        String strDateOk = "";
            //        myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            //        /*Blocked for BPL*/
            //        if (FTSPayRollBL.User.BoolDayBlockAvailable && !chkSLMFLabour.Checked)
            //        {
            //            strDateOk = myEntries.CheckDateDifference();
            //        }
            //        else
            //        {
            //            strDateOk = "OK";
            //        }

            //        if ((strDateOk.Equals("OK")))
            //        {
            //            if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
            //            {
            //        //-----start--------------
            //        String status = "";
            //        try
            //        {
                        
            //            if (String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
            //            {
            //                MessageBox.Show("Division is not selected.");
            //            }
            //            else if (String.IsNullOrEmpty(cmbField.SelectedValue.ToString()))
            //            {
            //                MessageBox.Show("Field is not selected.");
            //            }
            //            //else if (String.IsNullOrEmpty(txtACCode.Text))
            //            //{
            //            //    MessageBox.Show("Must Enter A Main Code");
            //            //}
            //            //else if (string.IsNullOrEmpty(txtJobShortName.Text))
            //            //{
            //            //    MessageBox.Show("Job Cannot be Empty");
            //            //    txtJobShortName.Focus();

            //            //}
            //            //else if (String.IsNullOrEmpty(txtNoOfEmployees.Text))
            //            //{
            //            //    MessageBox.Show("Employee Count Cannot be Empty");
            //            //}
            //            //else if (Convert.ToInt32(txtNoOfEmployees.Text) < 1)
            //            //{
            //            //    MessageBox.Show("Employee Count Must Be Greater Than 0");
            //            //}
            //            else if (gvJobList.Rows.Count <= 0)
            //            {
            //                MessageBox.Show("No Jobs Available");
            //            }  
            //            else if (String.IsNullOrEmpty(txtGangNumber.Text))
            //            {
            //                MessageBox.Show("Gang number cannot be empty.");
            //            }
            //            else if (String.IsNullOrEmpty(txtChitNumber.Text))
            //            {
            //                MessageBox.Show("Chit Number Cannot Be Empty");
            //            }
            //            //else if (DHAccounts.GETSubCategoryNameByCode(txtACCode.Text).ToUpper().Equals("NA") || String.IsNullOrEmpty(txtACCodeName.Text))
            //            //{
            //            //    //MessageBox.Show("Account Code Not Found!");
            //            //    txtNoOfEmployees.Focus();
            //            //}
            //            else
            //            {
            //                if (rbtnGeneral.Checked)
            //                {
            //                    MChit.StrLabourType = rbtnGeneral.Text.ToString();
            //                    MChit.StrLabourEstate = "NA";
            //                    MChit.StrLabourDivision = "NA";
            //                    MChit.StrLabourField = "NA";
            //                }
            //                if (rbtnLentLabour.Checked)
            //                {
            //                    MChit.StrLabourType = rbtnLentLabour.Text.ToString();
            //                    MChit.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
            //                    MChit.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
            //                    MChit.StrLabourField = cmbLabourField.SelectedValue.ToString();
            //                }
            //                if (rbtnInterEstate.Checked)
            //                {
            //                    MChit.StrLabourType = rbtnInterEstate.Text.ToString();
            //                    MChit.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
            //                    MChit.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
            //                    MChit.StrLabourField = cmbLabourField.SelectedValue.ToString();
            //                }

            //                MChit.DtDate = dateTimePicker1.Value.Date;
            //                MChit.StrDivision = cmbDivision.SelectedValue.ToString();
            //                MChit.StrField = cmbField.SelectedValue.ToString();
            //                MChit.StrACCode = "00";
                            
            //                MChit.StrGangNumber = txtGangNumber.Text;
            //                MChit.StrChitNo = txtChitNumber.Text;
            //                MChit.StrUser = FTSPayRollBL.User.StrUserName;

            //                //-------------
            //                for (int j = 0; j < gvJobList.Rows.Count; j++)
            //                {
            //                    if (!gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("PLK") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("TAP") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("XXX") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("XPR") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("XMT") && !gvJobList.Rows[j].Cells[0].Value.ToString().ToUpper().Equals("PH"))
            //                    {
            //                        MChit.StrJobShortName = gvJobList.Rows[j].Cells[0].Value.ToString();
            //                    }
            //                    else
            //                    {
            //                        MChit.StrJobShortName = gvJobList.Rows[j].Cells[0].Value.ToString();
            //                    }
            //                    MChit.IntEmpCount = Convert.ToInt32(gvJobList.Rows[j].Cells[1].Value.ToString());
            //                    try
            //                    {
            //                        status = MChit.UpdateMusterChitEntry();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        MessageBox.Show("Insert Muster For " + MChit.StrJobShortName + " Failed.., " + ex.Message);
            //                    }
            //                }
            //                //--------------
            //                if (status.Equals("UPDATED"))
            //                {
            //                    MessageBox.Show("Muster Chit Updated Successfully.");
            //                }
            //                else if (status.Equals("MCNOTEXISTS"))
            //                {
            //                    MessageBox.Show("Muster Chit Not Exists.");
            //                }
            //                AfterAdd();



            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Error On Update , " + ex.Message);
            //        }
            //    //--end-------------
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please Select a Date Within the Month You Logged In");
            //        dateTimePicker1.Focus();
            //        //dateTimePicker1.Value = new DateTime(Convert.ToInt32(User.StrYear),YMonth.GetMonthIdByMonthName(User.StrMonth), 1);
            //    }
            //}
            //else if (strDateOk.Equals("BLOCK"))
            //{
            //    MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

            //    myEntries.DtCurrentDate = dateTimePicker1.Value.Date;
            //    myEntries.AddBlockDates();
            //    dateTimePicker1.Focus();
            //}
            //else
            //{
            //    MessageBox.Show("This Date Data Entries Are Blocked Now, Please Contact Head Office For Date Release.");
            //    this.Close();
            //}

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
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

            if ((strDateOk.Equals("OK")))
            {
                if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                {
            if (String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
            {
                MessageBox.Show("Division is not selected.");
            }
            else if (String.IsNullOrEmpty(cmbField.SelectedValue.ToString()))
            {
                MessageBox.Show("Field is not selected.");
            }
            //else if (String.IsNullOrEmpty(txtACCode.Text))
            //{
            //    MessageBox.Show("Must Enter A Main Code");
            //}
            //else if (string.IsNullOrEmpty(txtJobShortName.Text))
            //{
            //    MessageBox.Show("Job Cannot be Empty");
            //    txtJobShortName.Focus();

            //}
            else if (String.IsNullOrEmpty(txtGangNumber.Text))
            {
                MessageBox.Show("Gang number cannot be empty.");
            }
            else if (String.IsNullOrEmpty(txtChitNumber.Text))
            {
                MessageBox.Show("Chit Number Cannot Be Empty");
            }
            else
            {
                try
                {
                    String strStatus = "";
                    MChit.IntCropType = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                    MChit.DtDate = dateTimePicker1.Value.Date;
                    MChit.StrDivision = cmbDivision.SelectedValue.ToString();
                    MChit.StrField = cmbField.SelectedValue.ToString();
                    MChit.StrACCode = "00";
                    if (!txtJobShortName.Text.ToUpper().Equals("PLK") && !txtJobShortName.Text.ToUpper().Equals("TAP") && !txtJobShortName.Text.ToUpper().Equals("XXX") && !txtJobShortName.Text.ToUpper().Equals("XPR") && !txtJobShortName.Text.ToUpper().Equals("XMT") && !txtJobShortName.Text.ToUpper().Equals("PH"))
                    {
                        MChit.StrJobShortName = txtJobShortName.Text;
                    }
                    else
                    {
                        MChit.StrJobShortName = txtJobShortName.Text;
                    }
                    MChit.IntEmpCount = Convert.ToInt32(txtNoOfEmployees.Text);
                    MChit.StrGangNumber = txtGangNumber.Text;
                    MChit.StrChitNo = txtChitNumber.Text;
                    strStatus = MChit.DeleteMusterChitEntry();
                    if (strStatus.Equals("DELETED"))
                    {
                        MessageBox.Show("Muster Chit Deleted Successfully.");
                        AfterAdd();
                    }
                    else if (strStatus.Equals("MCEXISTS"))
                    {
                        MessageBox.Show("Cannot Delete, Daily Entries Available.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Delete , " + ex.Message);
                }
            }
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
                cmbDivision_SelectedIndexChanged(null, null);

                cmbCropType.Focus();
            }
            
        }

        private void cmbField_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                rbtnGeneral.Focus();
            }
        }

        private void txtNoOfEmployees_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                cmdAddList.Focus();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            AfterAdd();
        }

        private void txtACCode_Leave(object sender, EventArgs e)
        {
            //DateChanged();
            //if (txtACCode.Text.Equals("?"))
            //{
            //    ACSubCategoryList objAcSubList = new ACSubCategoryList(this);
            //    objAcSubList.Show();
            //}
            //else
            //{
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
            //            txtNoOfEmployees.Focus();
            //        }
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AfterAdd();
        }

        private void txtGangNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtChitNumber.Focus();
            }
        }

        private void txtChitNumber_KeyPress(object sender, KeyPressEventArgs e)
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
                //txtNoOfEmployees.Focus();
                txtJobShortName.Focus();
            }
        }

        private void cmbLabourField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtJobShortName.Focus();
                //txtNoOfEmployees.Focus();
            }
        }

        private void rbtnGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGeneral.Checked)
            {
                cmbLabourField.Enabled = false;
                cmbLabourDivision.Enabled = false;
                cmbLabourEstate.Enabled = false;
                MChit.StrLabourEstate = "NA";
                MChit.StrLabourDivision = "NA";
                MChit.StrLabourField = "NA";
            }
        }

        private void rbtnLentLabour_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLentLabour.Checked)
            {                
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

                if (cmbField.Items.Count<1)
                {
                    cmbField.DataSource=EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                    cmbField.DisplayMember = "FieldID";
                    cmbField.ValueMember = "FieldID";
                }

                cmbLabourDivision_SelectedIndexChanged(null, null);
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

            }
        }

        private void cmbLabourDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnInterEstate.Checked)
                {
                    cmbLabourField.DataSource = EstDivBlock.ListOtherDivisionFieldsByCrop(cmbLabourDivision.SelectedValue.ToString(),cmbLabourEstate.SelectedValue.ToString(),cmbCropType.Text);
                    cmbLabourField.DisplayMember = "FieldId";
                    cmbLabourField.ValueMember = "FieldId";
                }
                else
                {
                    if (!String.IsNullOrEmpty(cmbLabourDivision.SelectedItem.ToString()))
                    {
                        cmbLabourField.DataSource = EstDivBlock.ListDivisionFieldsByCrop(cmbLabourDivision.SelectedValue.ToString(),cmbCropType.Text);
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

        private void DownloadBorrowingEstateDivisionFields()
        {
            if (MessageBox.Show("Do you want to Download And Refresh, " + cmbLabourEstate.SelectedValue.ToString() + " Estate Divisions and Fields  ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (MessageBox.Show("Are You Sure That Your Internet Connection Is Connected", "Connection Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    /*Download Borrowing Estate Divisions And Fields*/
                    
                    Int32 intCount = 0;
                    String BorrowingEstId = cmbLabourEstate.SelectedValue.ToString();
                    //FrmPleaseWait frmWait = new FrmPleaseWait(this);
                    //frmWait.Show();
                    WebService.WebService myService = new WebService.WebService();
                    DataTable odata = myService.DownloadDivisions(BorrowingEstId);
                    //progressBar1.Maximum = odata.Rows.Count;
                    //progressBar1.Minimum = 0;

                    for (int i = 0; i < odata.Rows.Count; i++)
                    {
                        myDownload.InsertCheckRollDivision(odata.Rows[i][0].ToString(), odata.Rows[i][1].ToString(), odata.Rows[i][2].ToString());
                        //progressBar1.Value = i + 1;
                        Application.DoEvents();
                    }

                    odata = myService.DownloadFields(BorrowingEstId);                    

                    for (int i = 0; i < odata.Rows.Count; i++)
                    {

                        myDownload.InsertCheckRollDivisionField(BorrowingEstId, odata.Rows[i][2].ToString(), 1, odata.Rows[i][0].ToString(), odata.Rows[i][1].ToString(), odata.Rows[i][5].ToString(), odata.Rows[i][4].ToString(), odata.Rows[i][1].ToString(), odata.Rows[i][0].ToString(), FTSPayRollBL.User.StrUserName);
                        //progressBar1.Value = i + 1;
                        Application.DoEvents();

                        intCount++;
                    }
                    MessageBox.Show("Divisions and Fields Downloaded");
                    Application.DoEvents();
                    boolDownloadOk = true;
                    cmbLabourDivision.Focus();
                }
                    //}

                }
                else
                {
                    MessageBox.Show("Cannot Refresh " + cmbLabourEstate.SelectedValue.ToString() + " Estate Division Fields Without Connection", "Cannot Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadBorrowingEstateDivisionFields();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
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
            if ((strDateOk.Equals("OK")))
            {
                if (dateTimePicker1.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                {
                    MChit.DtDate = dateTimePicker1.Value.Date;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            dataSetReport.Tables.Add(MChit.ListMusterChitEntry(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString()));
            
            dataSetReport.WriteXml("MusterChitRegister.xml");
            MusterChitRegisterRPT myaclist = new MusterChitRegisterRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Period", "Date:" + dateTimePicker1.Value.Date.ToShortDateString());
            myaclist.SetParameterValue("Division", "Division: "+cmbDivision.SelectedValue.ToString());
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void txtACCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJobShortName_KeyPress(object sender, KeyPressEventArgs e)
        
        {
            
            String strActualFieldCrop="";
            String strActualExType = "";
            String strActualDivision="";
            String strActualField="";
            if (e.KeyChar == 13)
            {
                if (txtJobShortName.Text.Equals("?"))
                {
                    if (rbtnGeneral.Checked)
                    {
                        try
                        {
                            if (cmbField.Items.Count>0)
                            {
                                //strActualFieldCrop = lblFieldCrop.Text;
                                strActualFieldCrop = cmbCropType.Text;
                                strActualExType = lblFieldExType.Text;
                                strActualDivision = cmbDivision.SelectedValue.ToString();
                                strActualField = cmbField.SelectedValue.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Fields Not Found...");
                                cmbField.Focus();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, "+ex.Message);
                        }
                    }
                    else if (rbtnLentLabour.Checked)
                    {
                        if (cmbLabourField.Items.Count > 0)
                        {
                            //strActualFieldCrop = lblLabourFieldCrop.Text;
                            strActualFieldCrop = cmbCropType.Text;
                            strActualExType = lblLabourFieldExType.Text;
                            strActualDivision = cmbLabourDivision.SelectedValue.ToString();
                            strActualField = cmbLabourField.SelectedValue.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Fields Not Found");
                            cmbLabourField.Focus();
                        }
                    }
                    else
                    {
                        
                        strActualFieldCrop = "%";
                        strActualExType = "%";
                    }
                    if (rbtnInterEstate.Checked)
                    {
                        JobList objJobSearch = new JobList(this);
                        objJobSearch.Show();
                    }
                    else
                    {
                        if (EstDivBlock.IsNonCropField(strActualDivision, strActualField))
                        {
                            //JobList objJobSearch = new JobList(this,strActualExType);
                            //objJobSearch.Show(); 
                            JobList objJobSearch = new JobList(this, strActualFieldCrop, strActualExType);
                            objJobSearch.Show();
                        }
                        else
                        {
                            JobList objJobSearch = new JobList(this, strActualFieldCrop, strActualExType);
                            objJobSearch.Show();
                        }
                    }
                }
                else
                {

                    if (!txtJobShortName.Text.ToUpper().Equals("PLK") && !txtJobShortName.Text.ToUpper().Equals("TAP") && !txtJobShortName.Text.ToUpper().Equals("XXX") && !txtJobShortName.Text.ToUpper().Equals("XPR") && !txtJobShortName.Text.ToUpper().Equals("XMT") && !txtJobShortName.Text.ToUpper().Equals("PH"))
                    {
                        txtJobShortName.Text = txtJobShortName.Text;

                    }
                    txtJobShortName_LeaveChanged();
                }
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
                    DataSet dsJobDetail = new DataSet();
                    
                        String StrActualFieldCrop="";
                        String StrActualFieldExType="";
                        String strActualDivision = "";
                        String strActualField = "";

                        if (rbtnGeneral.Checked)
                        {
                            if (cmbField.Items.Count>0)
                            {
                                //StrActualFieldCrop = lblFieldCrop.Text;
                                StrActualFieldCrop = cmbCropType.Text;
                                StrActualFieldExType = lblFieldExType.Text;
                                strActualDivision = cmbDivision.SelectedValue.ToString();
                                strActualField = cmbField.SelectedValue.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Field Not Found");
                                cmbField.Focus();
                            }
                        }
                        if (rbtnLentLabour.Checked)
                        {
                            if (cmbLabourField.Items.Count > 0)
                            {
                                //StrActualFieldCrop = lblLabourFieldCrop.Text;
                                StrActualFieldCrop = cmbCropType.Text;
                                StrActualFieldExType = lblLabourFieldExType.Text;
                                strActualDivision = cmbLabourDivision.SelectedValue.ToString();
                                strActualField = cmbLabourField.SelectedValue.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Field Not Found");
                                cmbLabourField.Focus();
                            }
                        }
                        if (rbtnInterEstate.Checked)
                        {
                            //StrActualFieldCrop = lblLabourFieldCrop.Text;
                            StrActualFieldCrop = cmbCropType.Text;
                            StrActualFieldExType = lblLabourFieldExType.Text;
                            txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
                            txtJobShortName.Text = txtJobShortName.Text.ToUpper();
                            txtNoOfEmployees.Focus();
                        }
                        else
                        {
                            if (EstDivBlock.IsNonCropField(strActualDivision, strActualField))
                            {
                                //dsJobDetail = Job1.JobNameCropAndExTypeByShortName(this.txtJobShortName.Text.ToUpper(), "NONE", StrActualFieldExType);
                                dsJobDetail = Job1.JobNameCropAndExTypeByShortName(this.txtJobShortName.Text.ToUpper(), StrActualFieldCrop, StrActualFieldExType);
                            }
                            else
                            {
                                dsJobDetail = Job1.JobNameCropAndExTypeByShortName(this.txtJobShortName.Text.ToUpper(), StrActualFieldCrop, StrActualFieldExType);
                            }
                            //DataSet dsJobDetail=Job1.JobNameCropAndExTypeByShortName(this.txtJobShortName.Text.ToUpper(),StrActualFieldCrop,StrActualFieldExType);
                            if(dsJobDetail.Tables[0].Rows.Count>0)
                            {
                                if (dsJobDetail.Tables[0].Rows[0][2].ToString().ToUpper().Equals(StrActualFieldExType.ToUpper()))
                                {
                                    txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
                                    txtJobShortName.Text = txtJobShortName.Text.ToUpper();
                                    txtNoOfEmployees.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("Field & Job Expenditure Types Not Matching...");
                                    txtJobShortName.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show(txtJobShortName.Text + " Job Is Not Available For " + lblFieldCrop.Text + " Crop Or " + lblFieldExType.Text + " Expenditure Type");
                            }
                            dsJobDetail.Dispose();
                        }
                    
                           
                    
                    
                    
                }
                }
            
        }

        private void cmbLabourDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    txtJobShortName.Focus();
            //    //txtNoOfEmployees.Focus();
            //}
        }

        private void cmbLabourEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void cmdAddList_Click(object sender, EventArgs e)
        {
            if (gvJobList.Rows.Count < 0)
            {
                if (String.IsNullOrEmpty(txtJobShortName.Text))
                {
                    MessageBox.Show("Enter Job To Proceed...!");
                }
                else if (String.IsNullOrEmpty(txtNoOfEmployees.Text.Trim()))
                {
                    MessageBox.Show("Enter No Of Employees To Proceed...!");
                }
                else if (Convert.ToInt32(txtNoOfEmployees.Text.Trim()) <= 0)
                {
                    MessageBox.Show("No Of Employees Should Be Greater Than 0...!");
                }
            }
            else
            {
                DataTable dt = new DataTable();
                DataRow drow;

                dt.Columns.Add(new DataColumn("Job"));
                dt.Columns.Add(new DataColumn("NoOfEmployees"));

                drow = dt.NewRow();
                drow[0] = txtJobShortName.Text;
                drow[1] = txtNoOfEmployees.Text;

                dt.Rows.Add(drow);

                for (int i = 0; i < gvJobList.Rows.Count; i++)
                {
                    String StrActualFieldCrop="";
                    String StrActualFieldExType="";
                    if (rbtnGeneral.Checked)
                    {
                        //StrActualFieldCrop = lblFieldCrop.Text;
                        StrActualFieldCrop = cmbCropType.Text;
                        StrActualFieldExType = lblFieldExType.Text;
                    }
                    if (rbtnLentLabour.Checked)
                    {
                        //StrActualFieldCrop = lblLabourFieldCrop.Text;
                        StrActualFieldCrop = cmbCropType.Text;
                        StrActualFieldExType = lblLabourFieldExType.Text;
                    }
                    if (rbtnInterEstate.Checked)
                    {
                        //StrActualFieldCrop = lblLabourFieldCrop.Text;
                        StrActualFieldCrop = cmbCropType.Text;
                        StrActualFieldExType = lblLabourFieldExType.Text;
                        drow = dt.NewRow();
                        drow[0] = gvJobList.Rows[i].Cells[0].Value.ToString();
                        drow[1] = gvJobList.Rows[i].Cells[1].Value.ToString();
                        dt.Rows.Add(drow);
                    }
                    else
                    {
                        //DataSet dsJobDetail = Job1.JobNameCropAndExTypeByShortName(this.txtJobShortName.Text.ToUpper(),StrActualFieldCrop,StrActualFieldExType);
                        //if (dsJobDetail.Tables[0].Rows[0][2].ToString().ToUpper().Equals(StrActualFieldCrop.ToUpper()))
                        //{
                        //    if (dsJobDetail.Tables[0].Rows[0][2].ToString().ToUpper().Equals(StrActualFieldExType.ToUpper()))
                        //    {
                        //        drow = dt.NewRow();
                        //        drow[0] = gvJobList.Rows[i].Cells[0].Value.ToString();
                        //        drow[1] = gvJobList.Rows[i].Cells[1].Value.ToString();
                        //        dt.Rows.Add(drow);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show(gvJobList.Rows[i].Cells[0].Value.ToString() + " Job Failed, " + "Field & Job Expenditure Types Not Matching...");
                        //        txtJobShortName.Focus();
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show(gvJobList.Rows[i].Cells[0].Value.ToString() + " Job Failed, " + " Field & Job Crop Types Not Matching");
                        //    txtJobShortName.Focus();
                        //}
                        //dsJobDetail.Dispose();
                        //-------------
                        DataSet dsJobDetail = Job1.JobNameCropAndExTypeByShortName(this.txtJobShortName.Text.ToUpper(), StrActualFieldCrop, StrActualFieldExType);
                        if (dsJobDetail.Tables[0].Rows.Count > 0)
                        {
                            if (dsJobDetail.Tables[0].Rows[0][2].ToString().ToUpper().Equals(StrActualFieldExType.ToUpper()))
                            {
                                drow = dt.NewRow();
                                drow[0] = gvJobList.Rows[i].Cells[0].Value.ToString();
                                drow[1] = gvJobList.Rows[i].Cells[1].Value.ToString();
                                dt.Rows.Add(drow);
                            }
                            else
                            {
                                MessageBox.Show("Field & Job Expenditure Types Not Matching...");
                                txtJobShortName.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show(txtJobShortName.Text + " Job Is Not Available For " + lblFieldCrop.Text + " Crop Or " + lblFieldExType.Text + " Expenditure Type");
                        }
                        dsJobDetail.Dispose();
                    }
                    
                }

                gvJobList.DataSource = dt;
                txtJobShortName.Clear();
                txtNoOfEmployees.Clear();

                AfterAddJobs();

            }
        }

        private void AfterAddJobs()
        {
            if (MessageBox.Show("Add More Jobs", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                txtJobShortName.Focus();                
            }
            else
            {
                //btnAdd.Focus();
                btnAdd.PerformClick();
            }
        }

        private void cmdAddList_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnMusterSummary_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = myField.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
            if (ds.Tables.Count > 0)
            {
                lblFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0][1].ToString()))
                {
                    lblFieldCrop.Text = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                    lblFieldCrop.Text = "None";
                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0][2].ToString()))
                {
                    lblFieldExType.Text = ds.Tables[0].Rows[0][2].ToString();
                }
                else
                {
                    lblFieldExType.Text = "None";
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FieldList objFieldList = new FieldList(this, cmbDivision.SelectedValue.ToString());
            objFieldList.Show();
        }

        private void cmbLabourField_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds1 = new DataSet();
            
            if (rbtnInterEstate.Checked)
            {
                ds1 = myField.getOtherEstateFieldName(cmbLabourField.SelectedValue.ToString(), cmbLabourDivision.SelectedValue.ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lblLentFieldName.Text = ds1.Tables[0].Rows[0][0].ToString();
                    lblLabourFieldCrop.Text = ds1.Tables[0].Rows[0][1].ToString();
                    if (!String.IsNullOrEmpty(ds1.Tables[0].Rows[0][2].ToString()))
                    {
                        lblLabourFieldExType.Text = ds1.Tables[0].Rows[0][2].ToString();
                    }
                }
            }
            else
            {
                ds1 = myField.getFieldName(cmbLabourField.SelectedValue.ToString(), cmbLabourDivision.SelectedValue.ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lblLentFieldName.Text = ds1.Tables[0].Rows[0][0].ToString();
                    lblLabourFieldCrop.Text = ds1.Tables[0].Rows[0][1].ToString();
                    if (!String.IsNullOrEmpty(ds1.Tables[0].Rows[0][2].ToString()))
                    {
                        lblLabourFieldExType.Text = ds1.Tables[0].Rows[0][2].ToString();
                    }
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbCropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDivision_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void cmbCropType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtGangNumber.Focus();
        }

        private void cmbCropType_Leave(object sender, EventArgs e)
        {
            //if ( cmbField.Items.Count > 0)
            //{
            //}
            //else
            //{
                if (cmbDivision.Items.Count <1 && cmbCropType.Items.Count <1)
                {
                    MessageBox.Show("Divisions Or Crops Not Available");
                    cmbDivision.Focus();
                }
                else
                {
                    //MessageBox.Show("Fields Not Available");
                    //cmbCropType.Focus();
                }
            //}
        }

        private void rbtnGeneral_Leave(object sender, EventArgs e)
        {
            if (cmbField.Items.Count < 1)
            {
                MessageBox.Show("No Fields Found");
                cmbField.Focus();
            }
        }        
    }
  }