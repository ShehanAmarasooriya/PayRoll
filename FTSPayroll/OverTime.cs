using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OverTime : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.OverTime OTime = new FTSPayRollBL.OverTime();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.AccountInformation DHAccounts = new FTSPayRollBL.AccountInformation();
        FTSPayRollBL.Validation clsValidation = new FTSPayRollBL.Validation();
        FTSPayRollBL.ProcessMonthlyWages ProMWage = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();

        public OverTime()
        {
            InitializeComponent();
        }

        private String getLabourType()
        {
            String strSelect = "";
            if (rbtnGeneral.Checked == true)
            {
                strSelect = rbtnGeneral.Text;
            }
            else if (rbtnLentLabour.Checked == true)
            {
                strSelect = rbtnLentLabour.Text;
            }
            else if (rbtnInterEstate.Checked == true)
            {
                strSelect = rbtnInterEstate.Text;
            }
            else
                strSelect = "NotSelected";
            return strSelect;
        }

        public void setLaboutType(String LType)
        {
            if (LType.Equals("General"))
            {
                this.rbtnGeneral.Checked = true;
            }
            else if (LType.Equals("Lent Labour"))
            {
                this.rbtnLentLabour.Checked = true;
            }
            else if (LType.Equals("Inter Estate Lent Labour"))
            {
                this.rbtnInterEstate.Checked = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*if amota then <namepay>/<shiftHour>*<Otfactor>*<hours>  otfactor=1.25*/
            /*if pmota then <namepay>/<shiftHour>*<Otfactor>*<hours>  otfactor=2 */
            
                String status = "";
                //if (clsValidation.ExpenditureJournalValidation(dtpDate.Value.Date) == true)
                //{
                //    MessageBox.Show("Expenditure Journal For " + dtpDate.Value.Date.Year.ToString() + "/" + dtpDate.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //else  
                if (String.IsNullOrEmpty(txtHours.Text))
                {
                    MessageBox.Show("No Of Hours Cannot Be Empty");
                    txtHours.Focus();
                }
                else if (rbtnGeneral.Checked && cmbField.Items.Count < 1)
                {
                    MessageBox.Show("Fields Not Found");
                }
                else if (rbtnLentLabour.Checked && cmbField.Items.Count < 1)
                {
                    MessageBox.Show("Fields Not Found");
                }
                //else if (!DHAccounts.IsJobAvaialbleInACMaster(txtJobShortName.Text, txtACCode.Text))
                //{
                //    //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                //    //{
                //    //    goto Finish;
                //    //}
                //    MessageBox.Show("Accounts Not Available for Selected Job And Main Code");
                //    txtACCode.Focus();

                //}
                else
                {
                    if ((!rbtnGeneral.Checked) && (!rbtnLentLabour.Checked) && (!rbtnInterEstate.Checked))
                    {
                        MessageBox.Show("Labour Type Not Selected!");
                        rbtnGeneral.Focus();
                    }
                    else
                    {

                        OTime.DtDate = dtpDate.Value.Date;
                        OTime.IntCrop = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                        if (chkHoliday.Checked)
                        {
                            OTime.BoolHolidayYesNO = true;
                        }
                        else
                        {
                            OTime.BoolHolidayYesNO = false;
                        }
                        OTime.StrDivision = cmbDivision.SelectedValue.ToString();
                        OTime.StrField = cmbField.SelectedValue.ToString();
                        if (rbtnGeneral.Checked)
                        {
                            OTime.StrLabourType = rbtnGeneral.Text.ToString();
                            OTime.StrLabourEstate = "NA";
                            OTime.StrLabourDivision = "NA";
                            OTime.StrLabourField = "NA";
                        }
                        if (rbtnLentLabour.Checked)
                        {
                            OTime.StrLabourType = rbtnLentLabour.Text.ToString();
                            OTime.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            OTime.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                            OTime.StrLabourField = cmbLabourField.SelectedValue.ToString();
                        }
                        if (rbtnInterEstate.Checked)
                        {
                            OTime.StrLabourType = rbtnInterEstate.Text.ToString();
                            OTime.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            OTime.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                            OTime.StrLabourField = cmbLabourField.SelectedValue.ToString();
                        }

                        //if (!DHAccounts.IsJobAvaialbleInACMaster(txtJobShortName.Text, txtACCode.Text))
                        //{
                        //    //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        //    //{
                        //    //    goto Finish;
                        //    //}
                        //    MessageBox.Show("Accounts Not Available for Selected Job And Main Code");
                        //    txtACCode.Focus();

                        //}


                        OTime.StrEmpNO = txtEmpNo.Text;
                        OTime.StrJobShortName = txtJobShortName.Text;
                        OTime.FlHours = float.Parse(txtHours.Text);
                        OTime.IntOTType = int.Parse(cmbOTType.SelectedValue.ToString());
                        OTime.IntWorkType = 1;
                        OTime.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.Text);
                        OTime.IntCrop = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                        OTime.StrMainCode = "00";



                        try
                        {
                            status = OTime.InsertOverTime();

                            //if (status.Equals("ADDED"))
                            //{
                            //    MessageBox.Show("OverTime Added Successfully!");
                            //    AfterAdd();
                            //}
                            //else if (status.Equals("EXISTS"))
                            //{
                            //    MessageBox.Show("OverTime Already Exists.");
                            //}
                            //else if (status.Equals("ERROR"))
                            //{
                            //    MessageBox.Show("Error.");
                            //}
                            //else
                            //    MessageBox.Show("Error.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error!, " + ex.Message);
                        }

                    }

                }
                
            Finish:
                if (status.Equals("ADDED"))
                {
                    MessageBox.Show("OverTime Added Successfully!");
                    AfterAdd();
                }
                else if (status.Equals("EXISTS"))
                {
                    MessageBox.Show("OverTime Already Exists.");
                }
                else if (status.Equals("ERROR"))
                {
                    MessageBox.Show("Error.");
                }
                else
                    MessageBox.Show("Overtime Entry Failed.");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                if (clsValidation.ExpenditureJournalValidation(dtpDate.Value.Date) == true)
                {
                    MessageBox.Show("Expenditure Journal For " + dtpDate.Value.Date.Year.ToString() + "/" + dtpDate.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    if ((!rbtnGeneral.Checked) && (!rbtnLentLabour.Checked) && (!rbtnInterEstate.Checked))
                    {
                        MessageBox.Show("Labour Type Not Selected!");
                        rbtnGeneral.Focus();
                    }
                    //if (!DHAccounts.IsJobAvaialbleInACMaster(txtJobShortName.Text, txtACCode.Text))
                    //{
                    //    //if (MessageBox.Show("JOb Not Found In Accounts, Do You Want To Continue..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    //    //{
                    //    //    goto Finish;
                    //    //}
                    //    MessageBox.Show("Accounts Not Available for Selected Job And Main Code");
                    //    txtACCode.Focus();

                    //}
                    //else
                    //{
                        String status = "";
                        OTime.IntOTId = Convert.ToInt32(lblRefNo.Text);
                        OTime.DtDate = dtpDate.Value.Date;
                        if (chkHoliday.Checked)
                        {
                            OTime.BoolHolidayYesNO = true;
                        }
                        else
                        {
                            OTime.BoolHolidayYesNO = false;
                        }

                        OTime.StrDivision = cmbDivision.SelectedValue.ToString();
                        OTime.StrField = cmbField.SelectedValue.ToString();
                        if (rbtnGeneral.Checked)
                        {
                            OTime.StrLabourType = rbtnGeneral.Text.ToString();
                        }
                        if (rbtnLentLabour.Checked)
                        {
                            OTime.StrLabourType = rbtnLentLabour.Text.ToString();
                            OTime.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                            OTime.StrLabourDivision = cmbLabourDivision.SelectedValue.ToString();
                            OTime.StrLabourField = cmbLabourField.SelectedValue.ToString();
                        }
                        if (rbtnInterEstate.Checked)
                        {
                            OTime.StrLabourType = rbtnInterEstate.Text.ToString();
                            OTime.StrLabourEstate = cmbLabourEstate.SelectedValue.ToString();
                        }
                        OTime.StrEmpNO = txtEmpNo.Text;
                        OTime.StrJobShortName = txtJobShortName.Text;
                        OTime.FlHours = float.Parse(txtHours.Text);
                        OTime.IntOTType = int.Parse(cmbOTType.SelectedValue.ToString());
                        OTime.IntWorkType = 1;
                        OTime.IntCategory = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.Text);
                        OTime.IntCrop = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
                        OTime.StrMainCode = "00";

                        try
                        {
                            status = OTime.UpdateOverTime();

                            if (status.Equals("UPDATED"))
                            {
                                MessageBox.Show("OverTime Updated Successfully!");
                                btnCancel.PerformClick();
                            }
                            else if (status.Equals("NOTEXISTS"))
                            {
                                MessageBox.Show("OverTime Not Exists.");
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error!, " + ex.Message);
                        }
                    //}
                }

            } 
            else
            MessageBox.Show("Please Select Data Before Update");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    String status = "";
                    OTime.IntOTId = Convert.ToInt32(lblRefNo.Text);
                    try
                    {
                        //if (clsValidation.ExpenditureJournalValidation(dtpDate.Value.Date) == true)
                        //{
                        //    MessageBox.Show("Expenditure Journal For " + dtpDate.Value.Date.Year.ToString() + "/" + dtpDate.Value.Date.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //}
                        //else
                        //{
                            status = OTime.DeleteOverTime();

                            if (status.Equals("DELETED"))
                            {
                                MessageBox.Show("OverTime Deleted Successfully!");
                                btnCancel.PerformClick();
                            }
                            else if (status.Equals("NOTEXISTS"))
                            {
                                MessageBox.Show("OverTime Not Available To Delete.");
                            }
                            else
                                MessageBox.Show("Something Went Wrong");
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error!," + ex.Message);
                    }
                }

            }
            else
            {
                MessageBox.Show("Please Select Data Before Delete");
            }

        
        }

        private void OverTime_Load(object sender, EventArgs e)
        {

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            cmbDivision.Text= FTSPayRollBL.User.StrDivision;

            cmbOTType.DataSource = OTime.ListOTtypes();
            cmbOTType.DisplayMember = "OTType";
            cmbOTType.ValueMember = "OTSettingId";

            rbtnGeneral.Checked = true;
            txtAmount.Enabled = false;

            cmbDivision_SelectedIndexChanged(null, null);

            AfterAdd();
            //gvOverTime.DataSource = OTime.ListOverTime(dtpDate.Value.Date,cmbDivision.Text);
            cmbDivision.Focus();


        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDivision.SelectedIndex != -1)
                {
                    lblFieldName.Text = "";
                    if (!String.IsNullOrEmpty(cmbDivision.SelectedItem.ToString()))
                    {
                        cmbField.DataSource = EstDivBlock.ListDivisionFieldsByCrop(cmbDivision.SelectedValue.ToString(), cmbCropType.Text);
                        cmbField.DisplayMember = "FieldID";
                        cmbField.ValueMember = "FieldID";

                    }
                }
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //lblRefNo.Text = Convert.ToString(0);
            txtHours.Text="";
            txtAmount.Text="";
            txtEmpNo.Clear();
            txtEmpName.Clear();
            txtJobShortName.Clear();
            txtJobName.Clear();
            //cmbOTType.SelectedIndex = 1;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            gvOverTime.DataSource = OTime.ListOverTime(dtpDate.Value.Date,cmbDivision.Text,Convert.ToInt32(cmbOTType.SelectedValue.ToString()));
        }

        private void AfterAdd()
        {
            txtHours.Text = "";
            txtAmount.Text = "";
            txtEmpNo.Clear();
            txtEmpName.Clear();
            //txtJobShortName.Clear();
            //txtJobName.Clear();
            txtEmpNo.Focus();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            DataTable dtSummary = OTime.GetOTSummary(Convert.ToDateTime(dtpDate.Value.Date.ToShortDateString()), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbOTType.SelectedValue.ToString()));
            txtTotOTHours.Text = dtSummary.Rows[0][0].ToString();
            txtTotOTAmount.Text = dtSummary.Rows[0][1].ToString();

            gvOverTime.DataSource = OTime.ListOverTime(dtpDate.Value.Date, cmbDivision.Text,Convert.ToInt32(cmbOTType.SelectedValue.ToString()));
        }

        private void gvOverTime_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbLabourEstate.SelectedIndex = -1;
            cmbLabourDivision.SelectedIndex = -1;
            cmbLabourField.SelectedIndex = -1;
            lblRefNo.Text = gvOverTime.Rows[e.RowIndex].Cells[13].Value.ToString();
            dtpDate.Value=Convert.ToDateTime(gvOverTime.Rows[e.RowIndex].Cells[0].Value.ToString());
            cmbDivision.SelectedValue = gvOverTime.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEmpNo.Text = gvOverTime.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbField.SelectedValue=gvOverTime.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtJobShortName.Text=gvOverTime.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtHours.Text=gvOverTime.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtAmount.Text=gvOverTime.Rows[e.RowIndex].Cells[7].Value.ToString();
            cmbOTType.SelectedValue=int.Parse(gvOverTime.Rows[e.RowIndex].Cells[8].Value.ToString());
            string str = gvOverTime.Rows[e.RowIndex].Cells[9].Value.ToString();
            if (gvOverTime.Rows[e.RowIndex].Cells[9].Value.ToString().Equals(rbtnGeneral.Text))
            {
                rbtnGeneral.Checked = true;
            }
            else if (gvOverTime.Rows[e.RowIndex].Cells[9].Value.ToString().Equals(rbtnLentLabour.Text))
            {
                rbtnLentLabour.Checked = true;
                cmbLabourEstate.SelectedValue = gvOverTime.Rows[e.RowIndex].Cells[10].Value.ToString();
                cmbLabourDivision.SelectedValue = gvOverTime.Rows[e.RowIndex].Cells[11].Value.ToString();
                cmbLabourDivision_SelectedIndexChanged(null, null);
                cmbLabourField.SelectedValue = gvOverTime.Rows[e.RowIndex].Cells[12].Value.ToString();
            }

            else if (gvOverTime.Rows[e.RowIndex].Cells[9].Value.ToString().Equals(rbtnInterEstate.Text))
            {
                rbtnInterEstate.Checked = true;
                cmbLabourEstate.SelectedValue = gvOverTime.Rows[e.RowIndex].Cells[10].Value.ToString();
            }
           
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtpDate.Focus();
            }
        }

        private void dtpDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    cmbField.Focus();
            //}
            if (e.KeyChar == 13)
            {
                cmbField.Focus();
            }
        }

        private void cmbField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //txtACCode.Focus();
                cmbCropType.Focus();
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
                       txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        txtJobShortName.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                        txtEmpNo.Text = "";
                        txtEmpNo.Focus();
                    }
                }
            }

        }


        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    //EmployeeList empList = new EmployeeList();
                    //empList.Show();

                    EmployeeSearch empList = new EmployeeSearch(this, cmbDivision.SelectedValue.ToString(), "OT");
                    empList.Show();
                }
                else
                { 
                    if (e.KeyChar == 13)
                    {
                        if (txtEmpNo.Text.Trim() != "")
                        {
                            txtEmpNo.Text = txtEmpNo.Text.PadLeft(5, '0');
                            txtEmpNo_LeaveChanged();
                        }
                    }
                }
            }
        }

        private void txtJobShortName_LeaveChanged()
        {
            //txtQty.Enabled = true;
            //if (!String.IsNullOrEmpty(txtJobShortName.Text))
            //{
            //    if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
            //    {
            //        MessageBox.Show("Please Enter a Correct Job Code.");
            //        txtJobShortName.Text = "";
            //        txtJobShortName.Focus();
            //    }
            //    else
            //    {
            //        txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
            //        txtJobShortName.Text = txtJobShortName.Text.ToUpper();
            //        cmbOTType.Focus();
                   
            //    }
            //}

            //-------------------------------
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

                    String StrActualFieldCrop = "";
                    String StrActualFieldExType = "";
                    String strActualDivision = "";
                    String strActualField = "";

                    if (rbtnGeneral.Checked)
                    {
                        //StrActualFieldCrop = lblFieldCrop.Text;
                        StrActualFieldCrop = cmbCropType.Text;
                        StrActualFieldExType = lblFieldExType.Text;
                        strActualDivision = cmbDivision.SelectedValue.ToString();
                        strActualField = cmbField.SelectedValue.ToString();
                    }
                    if (rbtnLentLabour.Checked)
                    {
                        //StrActualFieldCrop = lblLabourFieldCrop.Text;
                        StrActualFieldCrop = cmbCropType.Text;
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
                        if (dsJobDetail.Tables[0].Rows.Count > 0)
                        {
                            if (dsJobDetail.Tables[0].Rows[0][2].ToString().ToUpper().Equals(StrActualFieldExType.ToUpper()))
                            {
                                txtJobName.Text = Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper());
                                txtJobShortName.Text = txtJobShortName.Text.ToUpper();
                                cmbOTType.Focus();
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

        private void txtJobShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            String strActualFieldCrop = "";
            String strActualExType = "";
            String strActualDivision = "";
            String strActualField = "";
            if (e.KeyChar == 13)
            {
                if (txtJobShortName.Text.Equals("?"))
                {
                    if (rbtnGeneral.Checked)
                    {
                        if (cmbField.Items.Count > 0)
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
                            cmbDivision.Focus();
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
                            MessageBox.Show("Fields Not Found...");
                            cmbLabourDivision.Focus();
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

        private void cmbOTType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
            txtHours.Focus();
            }
        }

        private void txtHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
            btnAdd.Focus();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                //gvOverTime.DataSource = OTime.ListOverTime(dtpDate.Value.Date, cmbDivision.Text);
                AfterAdd();
            }
            catch (Exception ex)
            { }
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
                OTime.StrLabourDivision = "NA";
                OTime.StrLabourField = "NA";
            }
        }

        private void rbtnGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGeneral.Checked)
            {
                if (cmbField.Items.Count > 0)
                {
                    cmbLabourField.Enabled = false;
                    cmbLabourDivision.Enabled = false;
                    cmbLabourEstate.Enabled = false;
                    OTime.StrLabourEstate = "NA";
                    OTime.StrLabourDivision = "NA";
                    OTime.StrLabourField = "NA";
                }
                else
                {
                    MessageBox.Show("Field Not Found...");
                    cmbField.Focus();
                }
            }
        }

        private void rbtnLentLabour_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLentLabour.Checked)
            {
                if (cmbField.Items.Count < 1)
                {
                    cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                    cmbField.DisplayMember = "FieldID";
                    cmbField.ValueMember = "FieldID";
                }
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

        private void LoadBorrowingFields()
        {
            if (rbtnInterEstate.Checked)
            {
                cmbLabourField.DataSource = EstDivBlock.ListOtherDivisionFieldsByCrop(cmbLabourDivision.SelectedValue.ToString(), cmbLabourEstate.SelectedValue.ToString(), cmbCropType.Text);
                cmbLabourField.DisplayMember = "FieldId";
                cmbLabourField.ValueMember = "FieldId";
            }
            else
            {
                lblLentFieldName.Text = "";
                if (!String.IsNullOrEmpty(cmbLabourDivision.SelectedItem.ToString()))
                {
                    cmbLabourField.DataSource = EstDivBlock.ListDivisionFieldsByCrop(cmbLabourDivision.SelectedValue.ToString(), cmbCropType.Text);
                    cmbLabourField.DisplayMember = "FieldID";
                    cmbLabourField.ValueMember = "FieldID";

                }
            }
        }

        private void cmbLabourDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBorrowingFields();
                //if (!this.cmbLabourDivision.SelectedItem.ToString().Equals(""))
                //{
                //    cmbLabourField.DataSource = EstDivBlock.ListDivisionFields(cmbLabourDivision.SelectedValue.ToString());
                //    cmbLabourField.DisplayMember = "FieldID";
                //    cmbLabourField.ValueMember = "FieldID";
                //}
            }
            catch { }
        }

        private void ShowFieldDetails()
        {
            DataSet ds = new DataSet();
            ds = EstDivBlock.getFieldName(cmbField.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
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

        private void ShowBorrowingFieldDetails()
        {

            DataSet ds1 = new DataSet();
            if (rbtnInterEstate.Checked)
            {
                ds1 = EstDivBlock.getOtherEstateFieldName(cmbLabourField.SelectedValue.ToString(), cmbLabourDivision.SelectedValue.ToString());
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
                ds1 = EstDivBlock.getFieldName(cmbLabourField.SelectedValue.ToString(), cmbLabourDivision.SelectedValue.ToString());
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

        private void cmbLabourField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnInterEstate.Checked)
            {
                cmbField.Enabled = false;
            }
            else
            {
                cmbField.Enabled = true;
                ShowBorrowingFieldDetails();
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

        private void cmbLabourField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEmpNo.Focus();
            }
        }

        private void rbtnInterEstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLabourEstate.Focus();
            }
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowFieldDetails();
            if (cmbField.Text.Equals("FAC"))
            {
                cmbOTType.Text = "AMOTA";
            }
            else
            {
                cmbOTType.Text = "AMOTI";
            }

        }

        private void dtpDate_Leave(object sender, EventArgs e)
        {
            String strDateOk = "";
            myEntries.DtCurrentDate = dtpDate.Value.Date;
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
                if (ProMWage.IsProcessed(cmbDivision.SelectedValue.ToString(), dtpDate.Value.Date.Year, dtpDate.Value.Date.Month))
                {
                    MessageBox.Show("Access Denied!, Already Processed ");
                    dtpDate.Focus();
                }
                else
                {
                    if (dtpDate.Value.Date.Month == YMonth.GetMonthIdByMonthName(FTSPayRollBL.User.StrMonth))
                    {

                        OTime.DtDate = dtpDate.Value.Date;
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Date Within the Month You Logged In");
                        dtpDate.Focus();
                        //dtpDate.Value = new DateTime(Convert.ToInt32(User.StrYear),YMonth.GetMonthIdByMonthName(User.StrMonth), 1);
                    }
                }
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                //MChit.DtDate = dtpDate.Value.Date;
                dtpDate.Focus();
            }
            else if (strDateOk.Equals("POST_DATE_BLOCK"))
            {
                MessageBox.Show("Post Date Entry Blocked.", "Blocked Entries");

                //MChit.DtDate = dtpDate.Value.Date;
                dtpDate.Focus();
            }
            else if (strDateOk.Equals("CONFIRMED"))
            {
                MessageBox.Show("Already Confirmed.", "Entries Blocked");

                //MChit.DtDate = dtpDate.Value.Date;
                dtpDate.Focus();
            }
            else
            {
                MessageBox.Show("This Date Data Entries Are Blocked Now, Please Contact Head Office For Date Release.");
                this.Close();
            }
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {            
                txtEmpNo_LeaveChanged();            
        }

        private void cmbOTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOTType.SelectedIndex != -1)
            {
                try
                {
                    lblOTFactor.Text = OTime.GetOTFactorByText(cmbOTType.Text);
                }
                catch { }
            }
        }

        private void cmbCrop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //txtACCode.Focus();
                rbtnGeneral.Focus();
            }
        }

        private void cmbCropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                cmbDivision_SelectedIndexChanged(null, null);
                if (!rbtnGeneral.Checked)
                {
                    LoadBorrowingFields();
                }
            }
            catch { }
        }

       

       

       
        

        

        
    }
}