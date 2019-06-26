using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FieldWiseSummaryEntry : Form
    {
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock myDivi = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.DailyFieldSummary DFieldSummary = new FTSPayRollBL.DailyFieldSummary();
        FTSPayRollBL.Job Job1 = new FTSPayRollBL.Job();
        FTSPayRollBL.ClsMusterChit MChit = new FTSPayRollBL.ClsMusterChit();
        FTSPayRollBL.EstateDivisionBlock myField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.AccountInformation DHAccounts = new FTSPayRollBL.AccountInformation();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();


        public FieldWiseSummaryEntry()
        {
            InitializeComponent();
        }

        private void FieldWiseSummaryEntry_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivi.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettingsExceptGiven("CropType", "None");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";


            //txtJobShortName.Text = "PLK";

            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(cmbDivision.SelectedItem.ToString()))
            {
                //cmbField.DataSource = myDivi.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                //cmbField.DisplayMember = "FieldID";
                //cmbField.ValueMember = "FieldID";

                //cmbChitNumber.DataSource = MChit.ListChitNumbersForSelectedDate(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString());
                //cmbChitNumber.DisplayMember = "ChitNumber";
                //cmbChitNumber.ValueMember = "ChitNumber";

            }
            try
            {
                cmbChitNumber.DataSource = null;
                RefreshData();
            }
            catch (Exception ex){ }
        }

        public void RefreshData()
        {
            cmbMusterChit.DataSource = MChit.ListMusterChitForSelectedDate(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString(), 999);
            cmbMusterChit.DisplayMember = "MChitName";
            cmbMusterChit.ValueMember = "AutoMusterID";

            gvList.DataSource = DFieldSummary.ListDailyFieldSummary(cmbDivision.SelectedValue.ToString(), dateTimePicker1.Value.Date);
            txtFieldWeight.Clear();
            txtAreaCovered.Clear();
            txtManDays.Clear();
            //txtJobShortName.Clear();
            txtJobName.Clear();

            DataTable dtTotals = DFieldSummary.ListDailyFieldSummaryTotals(cmbDivision.SelectedValue.ToString(), dateTimePicker1.Value.Date);
            txtAreaCoveredTotal.Text = dtTotals.Rows[0][0].ToString();
            txtFieldWeightTotal.Text = dtTotals.Rows[0][1].ToString();
            txtManDaysTotal.Text = dtTotals.Rows[0][2].ToString();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public void RefreshData(Int32 intMchitRef)
        {
            gvList.DataSource = DFieldSummary.ListDailyFieldSummary(cmbDivision.SelectedValue.ToString(), dateTimePicker1.Value.Date,intMchitRef);
            txtFieldWeight.Clear();
            txtAreaCovered.Clear();
            txtManDays.Clear();
            //txtJobShortName.Clear();
            txtJobName.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtJobShortName_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtJobShortName.Text))
            {
                if (String.IsNullOrEmpty(Job1.JobNameByShortName(this.txtJobShortName.Text.ToUpper())))
                {
                    MessageBox.Show("Please Enter a Correct Job Code.");
                    txtJobShortName.Text = "";
                    txtJobShortName.Focus();
                }
            }
            else
            {
                MessageBox.Show("Job Cannot Be Empty");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbChitNumber.SelectedValue.ToString()))
            {
                MessageBox.Show("Chit Number Cannot Be Empty");
            }
            else if (String.IsNullOrEmpty(txtJobShortName.Text))
            {
                MessageBox.Show("Job Cannot Be Empty");
            }
            else
            {

                DFieldSummary.DtDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString());
                DFieldSummary.StrDivision = cmbDivision.SelectedValue.ToString();
                DFieldSummary.StrFiled = txtField.Text;
                DFieldSummary.StrChitNo = cmbChitNumber.SelectedValue.ToString();
                DFieldSummary.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                DFieldSummary.StrJob = txtJobShortName.Text;
                DFieldSummary.StrMainACCode = txtACCode.Text;
                DFieldSummary.IntMusterChitNumber = Convert.ToInt32(cmbMusterChit.SelectedValue.ToString());
                DFieldSummary.StrLabourType = txtLabourType.Text;
                
                if (String.IsNullOrEmpty(txtAreaCovered.Text))
                {
                    DFieldSummary.DecAreaCovered = 0;
                }
                else
                    DFieldSummary.DecAreaCovered = Convert.ToDecimal(txtAreaCovered.Text);
                if (String.IsNullOrEmpty(txtFieldWeight.Text))
                {
                    DFieldSummary.DecFieldWeight = 0;
                }
                else
                    DFieldSummary.DecFieldWeight = Convert.ToDecimal(txtFieldWeight.Text);
                if (String.IsNullOrEmpty(txtManDays.Text))
                {
                    DFieldSummary.DecManDays = 0;
                }
                else
                    DFieldSummary.DecManDays = Convert.ToDecimal(txtManDays.Text);
                try
                {
                    DFieldSummary.InsertDailyFieldSummary();
                    RefreshData();
                    MessageBox.Show("Field Summary Added Successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Insert, "+ex.Message);
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbChitNumber.SelectedValue.ToString()))
            {
                MessageBox.Show("Chit Number Cannot Be Empty");
            }
            else if (String.IsNullOrEmpty(txtJobShortName.Text))
            {
                MessageBox.Show("Job Cannot Be Empty");
            }
            else
            {

                DFieldSummary.DtDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString());
                DFieldSummary.StrDivision = cmbDivision.SelectedValue.ToString();
                DFieldSummary.StrFiled = txtField.Text;
                DFieldSummary.StrChitNo = cmbChitNumber.SelectedValue.ToString();
                DFieldSummary.StrJob = txtJobShortName.Text;
                DFieldSummary.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                DFieldSummary.StrMainACCode = txtACCode.Text;
                DFieldSummary.IntMusterChitNumber = Convert.ToInt32(cmbMusterChit.SelectedValue.ToString());

                if (String.IsNullOrEmpty(txtAreaCovered.Text))
                {
                    DFieldSummary.DecAreaCovered = 0;
                }
                else
                    DFieldSummary.DecAreaCovered = Convert.ToDecimal(txtAreaCovered.Text);
                if (String.IsNullOrEmpty(txtFieldWeight.Text))
                {
                    DFieldSummary.DecFieldWeight = 0;
                }
                else
                    DFieldSummary.DecFieldWeight = Convert.ToDecimal(txtFieldWeight.Text);
                if (String.IsNullOrEmpty(txtManDays.Text))
                {
                    DFieldSummary.DecManDays = 0;
                }
                else
                    DFieldSummary.DecManDays = Convert.ToDecimal(txtManDays.Text);
                try
                {
                    DFieldSummary.UpdateDailyFieldSummary();
                    RefreshData();
                    MessageBox.Show("Field Summary Updated Successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Insert, " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbChitNumber.SelectedValue.ToString()))
            {
                MessageBox.Show("Chit Number Cannot Be Empty");
            }
            else if (String.IsNullOrEmpty(txtJobShortName.Text))
            {
                MessageBox.Show("Job Cannot Be Empty");
            }
            else
            {

                DFieldSummary.DtDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString());
                DFieldSummary.StrDivision = cmbDivision.SelectedValue.ToString();
                DFieldSummary.StrFiled = txtField.Text;
                DFieldSummary.StrChitNo = cmbChitNumber.SelectedValue.ToString();
                DFieldSummary.StrJob = txtJobShortName.Text;
                DFieldSummary.StrGangNo = cmbGangNumber.SelectedValue.ToString();
                DFieldSummary.StrMainACCode = txtACCode.Text;
                DFieldSummary.IntMusterChitNumber = Convert.ToInt32(cmbMusterChit.SelectedValue.ToString());

                if (String.IsNullOrEmpty(txtAreaCovered.Text))
                {
                    DFieldSummary.DecAreaCovered = 0;
                }
                else
                    DFieldSummary.DecAreaCovered = Convert.ToDecimal(txtAreaCovered.Text);
                if (String.IsNullOrEmpty(txtFieldWeight.Text))
                {
                    DFieldSummary.DecFieldWeight = 0;
                }
                else
                    DFieldSummary.DecFieldWeight = Convert.ToDecimal(txtFieldWeight.Text);
                if (String.IsNullOrEmpty(txtManDays.Text))
                {
                    DFieldSummary.DecManDays = 0;
                }
                else
                    DFieldSummary.DecManDays = Convert.ToDecimal(txtManDays.Text);
                try
                {
                    DFieldSummary.DeleteDailyFieldSummary();
                    RefreshData();
                    MessageBox.Show("Field Summary Deleted Successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Insert, " + ex.Message);
                }
            }
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString());
            cmbMusterChit.SelectedValue = Convert.ToInt32(gvList.Rows[e.RowIndex].Cells[8].Value.ToString());
            cmbMusterChit_SelectedIndexChanged(null, null);
            txtField.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbChitNumber.SelectedValue = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtJobShortName.Text = gvList.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtAreaCovered.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtFieldWeight.Text = gvList.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtManDays.Text = gvList.Rows[e.RowIndex].Cells[7].Value.ToString();
            cmbGangNumber.SelectedValue = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtACCode.Text = gvList.Rows[e.RowIndex].Cells[9].Value.ToString();
           
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbChitNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMChit = MChit.ListGangNumbersForSelectedChitNumber(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), cmbDivision.SelectedValue.ToString(), cmbChitNumber.SelectedValue.ToString());
                if (dtMChit.Rows.Count > 0)
                {
                    cmbGangNumber.DataSource = dtMChit;
                    cmbGangNumber.DisplayMember = "GangNumber";
                    cmbGangNumber.ValueMember = "GangNumber";

                    //txtField.Text = dtMChit.Rows[0][1].ToString();
                    ////fieldChanged();
                    //txtACCode.Text = dtMChit.Rows[0][2].ToString();
                    txtFieldWeight.Focus();
                    //txtACCodeName.Text = DHAccounts.GETSubCategoryNameByCode(txtACCode.Text);
                    //if (txtACCodeName.Text.ToUpper().Equals("NA"))
                    //{
                    //    MessageBox.Show("Account Code Not Found!");
                    //}
                    //else
                    //{
                    //    if (String.IsNullOrEmpty(txtACCodeName.Text))
                    //    {
                    //        MessageBox.Show("Account Code Not Found");
                    //        txtACCode.Focus();
                    //    }
                    //    else
                    //    {
                    //        txtFieldWeight.Focus();
                    //    }
                    //}
                }
                else
                {
                    txtField.Clear();
                    txtACCode.Text = "";
                }


            }
            catch { }
        }

        public void fieldChanged()
        {
            DataSet ds = new DataSet();
            ds = myField.getFieldName(txtField.Text, cmbDivision.SelectedValue.ToString());
            if (ds.Tables.Count > 0)
            {
                //txtFieldName.Text = ds.Tables[0].Rows[0][0].ToString();
                txtACCode.Focus();
            }
            else
            {
                MessageBox.Show("Please Select a Valid Field");
                txtField.Focus();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //crop set to 999 to get all crop

                cmbMusterChit_SelectedIndexChanged(null, null);

                RefreshData();
                cmbMusterChit.SelectedIndex = -1;
            }
            catch { }
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

                //cmbField.DataSource = MChit.ListFieldIdForSelectedMuster(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()));
                //cmbField.DisplayMember = "FieldID";
                //cmbField.ValueMember = "FieldID";
                //cmbField.DataSource = myDivi.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                //cmbField.DisplayMember = "FieldID";
                //cmbField.ValueMember = "FieldID";

                DataTable dtMusterData = new DataTable();
                dtMusterData = MChit.GetMusterDetailsForSelectedMuster(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString()));
                cmbChitNumber.SelectedValue = dtMusterData.Rows[0][1].ToString();
                cmbGangNumber.SelectedValue = dtMusterData.Rows[0][2].ToString();
                txtLabourType.Text = dtMusterData.Rows[0][6].ToString();
                txtJobShortName.Text = dtMusterData.Rows[0][10].ToString();
                if (txtLabourType.Text.ToUpper().Equals("GENERAL"))
                {
                    txtField.Text = dtMusterData.Rows[0][3].ToString();
                }
                else
                {
                    txtField.Text = dtMusterData.Rows[0][9].ToString();
                }
                txtACCode.Text = dtMusterData.Rows[0][4].ToString();
                
                

                RefreshData(Convert.ToInt32(Convert.ToInt32(cmbMusterChit.SelectedValue.ToString())));

                txtFieldWeight.Focus();
            }
            catch { }
        }

        private void cmbMusterChit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtField.Focus();

            }
        }

        private void cmbField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFieldWeight.Focus();

            }
        }

        private void txtFieldWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtAreaCovered.Focus();

            }
        }

        private void txtAreaCovered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtManDays.Focus();

            }
        }

        private void txtManDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAdd.Focus();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                //dt = myRep.GetAmalgamation(cmbYear.Text, month);
                dt = DFieldSummary.ListDailyFieldSummary(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()));

                if (dt.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.WriteXml("DailyFieldSummaryRep.xml");

                    DailyFieldSummaryRPT myReport = new DailyFieldSummaryRPT();
                    myReport.SetDataSource(ds);
                    ReportViewer myViewer = new ReportViewer();

                    //myReport.SetParameterValue("Estate", "Estate :" + myDivi.ListEstates().Rows[0][0].ToString()+" Division:"+cmbDivision.SelectedValue.ToString());
                    //myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    //myReport.SetParameterValue("Date:", dateTimePicker1.Value.Date.ToShortDateString());
                    myReport.SetParameterValue("Estate", "Estate :" + myDivi.ListEstates().Rows[0][0].ToString() + "/ Division:" + cmbDivision.SelectedValue.ToString());
                    myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport.SetParameterValue("EntryDate", "Date :"+dateTimePicker1.Value.Date.ToShortDateString()); 
                    myViewer.crystalReportViewer1.ReportSource = myReport;
                    myViewer.Show();

                }
                else
                {
                    MessageBox.Show("No Data Preview..!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!" + ex.Message);
            }
        }

        private void cmbCropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}