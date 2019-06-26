using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FieldWiseNorm : Form
    {
        FTSPayRollBL.YearMonth YMonths = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        FTSPayRollBL.ProcessMonthlyWages ProMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        

        DataTable DivisionTbl;
        String strPRIStatus = "";
        public FieldWiseNorm()
        {
            InitializeComponent();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (chkAll.Checked == true)
            //    RefreshGrid(true);
            //else
                RefreshGrid(false);
        }

        private void RefreshGrid(bool boolAll)
        {
            try
            {
                gvList.DataSource = null;
                DataTable dt1=new DataTable();
                if (boolAll == true)
                    dt1 = DivNorm.ListFieldwiseNorm(cmbDivision.SelectedValue.ToString(),cmbCropType.Text);
                else
                    dt1 = DivNorm.ListFieldwiseNorm(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), cmbCropType.Text);
                if (dt1.Rows.Count > 0)
                {
                    gvList.DataSource = dt1;
                    gvList.Columns[0].ReadOnly = true;
                    gvList.Columns[1].ReadOnly = true;
                    gvList.Columns[2].ReadOnly = true;
                }
                //else
                    //gvList.DataSource = DivNorm.ListFieldwiseNormEmpty(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()));
            }
            catch { }
        }

        private void FieldWiseNorm_Load(object sender, EventArgs e)
        {
            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            cmbDivision.DataSource = EstDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            try
            {
                cmbDivision_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        

        private void btnApplyToAll_Click(object sender, EventArgs e)
        {
            for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
            {

                if (!String.IsNullOrEmpty(txtMPlkNorm.Text) && !String.IsNullOrEmpty(txtFemalePlkNorm.Text) && !String.IsNullOrEmpty(txtMalePRINorm.Text) && !String.IsNullOrEmpty(txtFemalPRINorm.Text) && !String.IsNullOrEmpty(txtGasTapNorm.Text))
                {
                    dateTimePicker1.Value.Date.ToShortDateString();
                    gvList.Rows[i].Cells[0].Value = dateTimePicker1.Value.Date.ToShortDateString();
                    gvList.Rows[i].Cells[3].Value = txtMPlkNorm.Text;
                    gvList.Rows[i].Cells[4].Value = txtFemalePlkNorm.Text;
                    gvList.Rows[i].Cells[5].Value = txtMalePRINorm.Text;
                    gvList.Rows[i].Cells[6].Value = txtFemalPRINorm.Text;
                    gvList.Rows[i].Cells[7].Value = txtGasTapNorm.Text;
                    //empMaster.UpdateEmployerNoToAllDivision(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), cmbEmployer.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Error, One Or More Norm Fields are empty...");
                    break;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                int i1 = 0;
            String strPRIStatus = "";
            try
            {
                if (!ProMWages.IsProcessed(cmbDivision.SelectedValue.ToString(), dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month))
                {                     

                    //RefreshGrid(false);
                    DivNorm.DeleteAllFieldNorm(cmbDivision.SelectedValue.ToString(), dateTimePicker1.Value.Date, cmbCropType.Text);
                    for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                    {
                        i1 = i;

                        //if (i == 46)
                        //    MessageBox.Show("46");

                        //if (!String.IsNullOrEmpty(txtMPlkNorm.Text) && !String.IsNullOrEmpty(txtFemalePlkNorm.Text))
                        //{
                        if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[0].Value.ToString()))
                        {
                            if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[1].Value.ToString()))
                            {
                                //gvList.Rows[i].Cells[2].Value = txtMPlkNorm.Text;
                                //gvList.Rows[i].Cells[3].Value = txtFemalePlkNorm.Text;
                                //gvList.Rows[i].Cells[4].Value = txtPINorm.Text;
                                //gvList.Rows[i].Cells[5].Value = dateTimePicker1.Value.Date.ToShortDateString();
                                DivNorm.InsertNormToAllField(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), gvList.Rows[i].Cells[1].Value.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 0, FTSPayRollBL.User.StrUserName, Convert.ToInt32(gvList.Rows[i].Cells[3].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[4].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[5].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[6].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[7].Value.ToString()));
                                //empMaster.UpdateEmployerNoToAllDivision(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), cmbEmployer.SelectedValue.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Field Not Found!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Division Not Found");
                        }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Field No not found");
                        //}
                    }
                    //if (MessageBox.Show("Adjust Available Entries? Date:" + dateTimePicker1.Value.Date.ToShortDateString() + " \r\n Division: " + cmbDivision.SelectedValue.ToString(), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    try
                    //    {
                    //        strPRIStatus = ProMWages.UpdatePluckingPRI(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), cmbDivision.SelectedValue.ToString(), cmbCropType.Text);
                    //        MessageBox.Show(dateTimePicker1.Value.Date.ToShortDateString() + "-" + cmbDivision.SelectedValue.ToString() + " PRI Adjusted");
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show(ProMWages.StrDivision + ", Error On Plucking PRI Update");
                    //    }
                    //}

                    MessageBox.Show("completed..");
                }
                else
                {
                    MessageBox.Show("Already Processed, Cannot Update.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error,"+gvList.Rows[i1].Cells[1].Value.ToString()+ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //if (chkAll.Checked == true)
            //    RefreshGrid(true);
            //else
                RefreshGrid(false);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dsDivisionReport = new DataTable();
            //dsDivisionReport = myEmployeeDeduction.GetMonthPRINorms(cmbDivision.SelectedValue.ToString(), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1)).Tables[0];
            dsDivisionReport = myEmployeeDeduction.GetMonthPRINormsForCrossTab(cmbDivision.SelectedValue.ToString(), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1),cmbCropType.Text).Tables[0];
            if (dsDivisionReport.Rows.Count > 0)
            {
                dsDivisionReport.WriteXml("MonthPRINorm.xml");

                FieldWiseDivisionNormRPT1 objReport = new FieldWiseDivisionNormRPT1();
                objReport.SetDataSource(dsDivisionReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDiv.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
                objReport.SetParameterValue("Period", new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).ToShortDateString() + " To " + new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview.");
            }
        }

        private void cmbCropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(false);
        }

        private void btnUpdateEntries_Click(object sender, EventArgs e)
        {
            DivisionTbl = null;
            //if (chkAll.Checked)
            //{
            //    DivisionTbl = EstDiv.ListEstateDivisions();
            //}
            //else
            //{
                DivisionTbl = EstDiv.ListEstateDivisions(cmbDivision.SelectedValue.ToString());
            //}

            foreach (DataRow drow1 in DivisionTbl.Rows)
            {

                ProMWages.StrDivision = drow1[0].ToString();
                //UPDATE PLUCKING pri
                try
                {
                    strPRIStatus = ProMWages.UpdatePluckingPRI(new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1), drow1[0].ToString(),cmbCropType.Text);
                    lblStatus.Text = drow1[0].ToString() + ", Updated Plucking PRI";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(drow1[0].ToString() + ", Error On Plucking PRI Update");
                }
            }
            MessageBox.Show("PRI For Plucking And Tapping Adjusted Successfully!");
        }

        private void btnPRIErrors_Click(object sender, EventArgs e)
        {
            DataTable dsDivisionReport = new DataTable();
            //dsDivisionReport = myEmployeeDeduction.GetMonthPRINorms(cmbDivision.SelectedValue.ToString(), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1)).Tables[0];
            dsDivisionReport = ProMWages.GetOkgPRI1(new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1), cmbDivision.SelectedValue.ToString(), cmbCropType.Text).Tables[0];
            if (dsDivisionReport.Rows.Count > 0)
            {
                dsDivisionReport.WriteXml("MonthPRIOkgErrors.xml");

                PRIOkgErrorsRPT objReport = new PRIOkgErrorsRPT();
                objReport.SetDataSource(dsDivisionReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDiv.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
                objReport.SetParameterValue("Period", new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).ToShortDateString() + " To " + new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview.");
            }
        }

        private void txtMPlkNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtFemalePlkNorm.Focus();
        }

        private void txtFemalePlkNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtMalePRINorm.Focus();
        }

        private void txtMalePRINorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtFemalPRINorm.Focus();
        }

        private void txtFemalPRINorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtGasTapNorm.Focus();
        }

        private void btnNormUpdateLog_Click(object sender, EventArgs e)
        {
            String strTextToSearch = "";
            if (String.IsNullOrEmpty(txtFieldIDToSearch.Text))
            {
                strTextToSearch = "";
            }
            else
                strTextToSearch = txtFieldIDToSearch.Text;
            DataTable dsDivisionReport = new DataTable();
            //dsDivisionReport = myEmployeeDeduction.GetMonthPRINorms(cmbDivision.SelectedValue.ToString(), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1)).Tables[0];
            dsDivisionReport = DivNorm.GetFieldNormUpdateLog(Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()), cmbDivision.SelectedValue.ToString(), cmbCropType.Text, strTextToSearch).Tables[0];
            if (dsDivisionReport.Rows.Count > 0)
            {
                dsDivisionReport.WriteXml("MonthPRINormUpdateLog.xml");

                FieldWiseNormUpdateLogRPT objReport = new FieldWiseNormUpdateLogRPT();
                objReport.SetDataSource(dsDivisionReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDiv.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
                objReport.SetParameterValue("Period", dateTimePicker1.Value.Date.ToShortDateString());
                objReport.SetParameterValue("UserId", FTSPayRollBL.User.StrUserName);
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview.");
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            String strDateOk = "";
            myEntries.IntEntryYear = dateTimePicker1.Value.Date.Year;
            myEntries.IntEntryMonth = dateTimePicker1.Value.Date.Month;
            /*Blocked for BPL*/
            if (FTSPayRollBL.User.BoolDayBlockAvailable)
            {
                strDateOk = myEntries.CheckMonthDifference();
            }
            else
            {
                strDateOk = "OK";
            }
            if ((strDateOk.Equals("OK")))
            {
                cmbCropType.Focus();
            }
            else if (strDateOk.Equals("BLOCK"))
            {
                MessageBox.Show("This Date Entries Are Blocked Now, Please Contact Head Office For Release.", "Blocked Entries");

                //MChit.DtDate = dtpDate.Value.Date;
                dateTimePicker1.Focus();
            }
            else if (strDateOk.Equals("POST_DATE_BLOCK"))
            {
                MessageBox.Show("Post Date Entry Blocked.", "Blocked Entries");

                //MChit.DtDate = cmbMonth.Value.Date;
                dateTimePicker1.Focus();
            }
            else if (strDateOk.Equals("CONFIRMED"))
            {
                MessageBox.Show("Already Confirmed.", "Entries Blocked");

                //MChit.DtDate = dtpDate.Value.Date;
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