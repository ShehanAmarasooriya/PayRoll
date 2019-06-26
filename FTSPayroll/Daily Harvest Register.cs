using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class Daily_Harvest_Register : Form
    {
        public Daily_Harvest_Register()
        {
            InitializeComponent();
        }

        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myreport = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        FTSPayRollBL.FTSCheckRollSettings settings = new FTSPayRollBL.FTSCheckRollSettings();

        private void Daily_Harvest_Register_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            rbGeneral.Checked = true;

            chkEmpRange.Checked = false;
            gbEmpRange.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
                DataSet ds = new DataSet();

            Int32 wrkType = 1;
            Boolean boolBlockPlk = false;
            
            if (rbGeneral.Checked)
            {
                wrkType = 1;
                boolBlockPlk = false;

            }
            else if (rbCashwork.Checked)
            {
                wrkType = 2;
                boolBlockPlk = false;
            }
            else
            {
                wrkType = 2;
                boolBlockPlk = true;
            }
            if (settings.IsCashOverKgsAvailable() && wrkType==2)
            {
                ds = myreport.getHarvestRegisterCashOkg((cmbDivision.SelectedValue.ToString()), (dtDate.Value.Date), wrkType);
                ds.WriteXml("DailyHarvestRegisterCashOkgRep.xml");
            }
            else
            {
                if (chkEmpRange.Checked)
                {
                    ds = myreport.getHarvestRegister((cmbDivision.SelectedValue.ToString()), (dtDate.Value.Date), wrkType,txtEmpNoFrom.Text,txtEmpNoTo.Text,boolBlockPlk);
                    ds.WriteXml("DailyHarvestRegisterRep.xml");
                }
                else
                {
                    ds = myreport.getHarvestRegister((cmbDivision.SelectedValue.ToString()), (dtDate.Value.Date), wrkType,boolBlockPlk);
                    ds.WriteXml("DailyHarvestRegisterRep.xml");
                }
            }
                

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (settings.IsCashOverKgsAvailable() && wrkType == 2)
                {
                    DailyHarvestRegisterCashOKgRPT myDailyRep = new DailyHarvestRegisterCashOKgRPT();
                    myDailyRep.SetDataSource(ds);
                    ReportViewer myReportViewer = new ReportViewer();

                    myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myDailyRep.SetParameterValue("Date", dtDate.Value.Date.ToShortDateString());
                    myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    if (wrkType == 1)
                    {
                        myDailyRep.SetParameterValue("General", "For Normal Work");
                    }
                    else
                    {
                        myDailyRep.SetParameterValue("General", "For Cash Work");
                    }
                    myDailyRep.SetParameterValue("CashWork", "");
                    try
                    {
                        if (myEntries.IsDayExistsInCHKDateConfirmations(dtDate.Value.Date))
                        {
                            if (Convert.ToBoolean(myEntries.IsDailyEntryConfirmed(dtDate.Value.Date).Rows[0][0].ToString()) == true)
                            {
                                myDailyRep.SetParameterValue("ConfirmYesNo", "Entries Confirmed");
                            }
                            else
                            {
                                myDailyRep.SetParameterValue("ConfirmYesNo", "Confirmation Pending");
                            }
                        }
                        else
                        {
                            myDailyRep.SetParameterValue("ConfirmYesNo", "Confirmation Pending");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
                    myReportViewer.Show();
                }
                else
                {
                    DailyHarvestRegisterRep myDailyRep = new DailyHarvestRegisterRep();
                    myDailyRep.SetDataSource(ds);
                    ReportViewer myReportViewer = new ReportViewer();

                    myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myDailyRep.SetParameterValue("Date", dtDate.Value.Date.ToShortDateString());
                    myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    if (wrkType == 1)
                    {
                        myDailyRep.SetParameterValue("General", "For Normal Work");
                    }
                    else
                    {
                        if (boolBlockPlk == false)
                        {
                            myDailyRep.SetParameterValue("General", "For Cash Work");
                        }
                        else
                        {
                            myDailyRep.SetParameterValue("General", "For Cash Work-Block Plucking");
                        }
                    }
                    myDailyRep.SetParameterValue("CashWork", "");
                    try
                    {
                        if (myEntries.IsDayExistsInCHKDateConfirmations(dtDate.Value.Date))
                        {
                            if (Convert.ToBoolean(myEntries.IsDailyEntryConfirmed(dtDate.Value.Date).Rows[0][0].ToString()) == true)
                            {
                                myDailyRep.SetParameterValue("ConfirmYesNo", "Entries Confirmed");
                            }
                            else
                            {
                                myDailyRep.SetParameterValue("ConfirmYesNo", "Confirmation Pending");
                            }
                        }
                        else
                        {
                            myDailyRep.SetParameterValue("ConfirmYesNo", "Confirmation Pending");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
                    myReportViewer.Show();
                }
                               
                
            }
            else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
           

            }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Daily Entries Of " + dtDate.Value.Date.ToShortDateString() + " ", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    myEntries.DailyEntryConfirmation(dtDate.Value.Date, FTSPayRollBL.User.StrUserName);
                    MessageBox.Show("Daily Entries Of " + dtDate.Value.Date.ToShortDateString()+" Confirmed");
                    dtDate.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Confirmation Failed"+ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dsConfirmations = new DataSet();
            dsConfirmations = myEntries.GetConfirmationDetails(dtDate.Value.Date);
            dsConfirmations.WriteXml("EntryConfirmations.xml");

            EntryConfirmationRpt objReport = new EntryConfirmationRpt();
            objReport.SetDataSource(dsConfirmations);
            ReportViewerForm objReportViewer = new ReportViewerForm();
            objReport.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            objReport.SetParameterValue("Date", dtDate.Value.Date.Month);
            objReport.SetParameterValue("Division", "Division : " + cmbDivision.Text);
            objReport.SetParameterValue("Year", dtDate.Value.Date.Year);
            
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void chkEmpRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmpRange.Checked)
            {
                gbEmpRange.Enabled = true;
            }
            else
            {
                gbEmpRange.Enabled = false;
            }
        }

        private void chkEmpRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkEmpRange.Checked)
            {
                txtEmpNoFrom.Focus();
            }
        }

        private void txtEmpNoFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtEmpNoFrom.Text))
                {
                    txtEmpNoFrom.Clear();
                    txtEmpNoFrom.Focus();
                }
                else
                {
                    txtEmpNoFrom.Text = txtEmpNoFrom.Text.PadLeft(5, '0');
                    txtEmpNoTo.Focus();
                }
            }
        }

        private void txtEmpNoTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtEmpNoTo.Text))
                {
                    txtEmpNoTo.Clear();
                    txtEmpNoTo.Focus();
                }
                else
                {
                    txtEmpNoTo.Text = txtEmpNoTo.Text.PadLeft(5, '0');
                    btnDisplay.Focus();
                }
            }
        }

            
        }
    }
