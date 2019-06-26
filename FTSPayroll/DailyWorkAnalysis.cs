using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyWorkAnalysis : Form
    {
        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.Reports clsReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.ClsMusterChit objMuster = new FTSPayRollBL.ClsMusterChit();        
        FTSPayRollBL.CHKWages myWage = new FTSPayRollBL.CHKWages();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();

        DataSet dataSetReport = new DataSet();

        public DailyWorkAnalysis()
        {
            InitializeComponent();
        }

        private void DailyWorkAnalysis_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstate.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettingsOtherCrops("CropType", 3);
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            cmbCropTypeOt.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropTypeOt.DisplayMember = "Name";
            cmbCropTypeOt.ValueMember = "Code";
        }

        private void btnCashKilos_Click(object sender, EventArgs e)
        {
            String strAllDivision = "%";
            if (chkDivision.Checked)
            {
                strAllDivision = "%";
            }
            else
            {
                strAllDivision = cmbDivision.SelectedValue.ToString();
            }
            dataSetReport = myReports.getEmployeeCashPluckingDetails(Convert.ToDateTime(dtpFromDate.Value.ToShortDateString()), Convert.ToDateTime(dtpToDate.Value.ToShortDateString()), 2, strAllDivision);
            dataSetReport.WriteXml("EmployeeCashPluckingDetail.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                CashPluckingKilo rptObj = new CashPluckingKilo();
                rptObj.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                rptObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                rptObj.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                rptObj.SetParameterValue("Options", "Division : " + cmbDivision.Text.ToString() + " / From:" + dtpFromDate.Value.ToShortDateString() + "   To:" + dtpToDate.Value.ToShortDateString());

                rptObj.SetParameterValue("WorkType", "Work Type : Cash Work");
                //if (intworktyp == 1)
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");

                //}
                //else
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                //}
                myReportViewer.crystalReportViewer1.ReportSource = rptObj;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void btnCashSundry_Click(object sender, EventArgs e)
        {
            String strAllDivision = "%";
            if (chkDivision.Checked)
            {
                strAllDivision = "%";
            }
            else
            {
                strAllDivision = cmbDivision.SelectedValue.ToString();
            }
            dataSetReport = myReports.getEmployeeSundryManDaysDetails(Convert.ToDateTime(dtpFromDate.Value.ToShortDateString()), Convert.ToDateTime(dtpToDate.Value.ToShortDateString()), 2, strAllDivision);
            dataSetReport.WriteXml("EmployeeCashSundryManDaysDetail.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                CashSundryManDays rptObj = new CashSundryManDays();
                rptObj.SetDataSource(dataSetReport);

                ReportViewer myReportViewer = new ReportViewer();

                rptObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                rptObj.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                rptObj.SetParameterValue("Options", "Division : " + cmbDivision.Text.ToString() + " / From:" + dtpFromDate.Value.ToShortDateString() + "   To:" + dtpToDate.Value.ToShortDateString());

                rptObj.SetParameterValue("WorkType", "Work Type : Cash Work");

                myReportViewer.crystalReportViewer1.ReportSource = rptObj;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnDivisionSummary_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetDivisionWiseSummary(strDivision, Fromdate, Todate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DivisionWiseSummary.xml");
                DivisionWiseSummaryRPT myReportViewe = new DivisionWiseSummaryRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:"+myEstate.ListEstates().Rows[0][0].ToString()+" / DivisionID:"+cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOverTime_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            String strCropTypeOt = "%";
            String strParamCrop = "All";
            if (chkAllCropsOt.Checked)
            {
                strCropTypeOt = "%";
                strParamCrop = "All";
            }
            else
            {
                strCropTypeOt = cmbCropTypeOt.SelectedValue.ToString();
                strParamCrop = FTSSettings.getNameByCode("CropType", Convert.ToInt32(cmbCropTypeOt.SelectedValue.ToString()));
            }
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date.ToShortDateString());
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date.ToShortDateString());
            ds = myReports.GetOvertimeDetails(strDivision, Fromdate, Todate,strCropTypeOt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("OvertimeDetails.xml");
                OvertimeDetailsRPT myReportViewe = new OvertimeDetailsRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myReportViewe.SetParameterValue("crop", strParamCrop);
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOTEmpSummary_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            String strCropTypeOt = "%";
            String strParamCrop = "All";

            if (chkAllCropsOt.Checked)
            {
                strCropTypeOt = "%";
                strParamCrop = "All";
            }
            else
            {
                strCropTypeOt = cmbCropTypeOt.SelectedValue.ToString();
                strParamCrop = FTSSettings.getNameByCode("CropType", Convert.ToInt32(cmbCropTypeOt.SelectedValue.ToString()));
            }

            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date.ToShortDateString());
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date.ToShortDateString());
            ds = myReports.GetOvertimeDetails(strDivision, Fromdate, Todate,strCropTypeOt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("OvertimeDetails1.xml");
                OvertimeDetailsEmpwiseSummary myReportViewe = new OvertimeDetailsEmpwiseSummary();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myReportViewe.SetParameterValue("crop", strParamCrop);
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAttQualifyData_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DateTime Fromdate;
            DateTime Todate;
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                MessageBox.Show("Please Select a One Division");
                cmbDivision.Focus();
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
                 Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date.ToShortDateString());
                 Todate = Convert.ToDateTime(dtpToDate.Value.Date.ToShortDateString());
                ds.Tables.Add(clsReports.ListAttendenceIncentiveQualificationData(Fromdate,Todate,strDivision));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("AttIncentiveQualificationData.xml");
                    DivisionWiseSummaryAttIncDataRPT myReportViewe = new DivisionWiseSummaryAttIncDataRPT();
                    myReportViewe.SetDataSource(ds);
                    myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                    myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                    myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private void btnBlkPlk_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DateTime Fromdate;
            DateTime Todate;
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                MessageBox.Show("Please Select a One Division");
                cmbDivision.Focus();
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
                Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date.ToShortDateString());
                Todate = Convert.ToDateTime(dtpToDate.Value.Date.ToShortDateString());
                ds=clsReports.ListBlockPluckingDetails(Fromdate, Todate, strDivision);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("BlockPlkDetails.xml");
                    DivisionWiseBlockPlkSummary myReportViewe = new DivisionWiseBlockPlkSummary();
                    myReportViewe.SetDataSource(ds);
                    myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                    myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                    myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCashNamePlk_Click(object sender, EventArgs e)
        {
            String strAllDivision = "%";
            if (chkDivision.Checked)
            {
                strAllDivision = "%";
            }
            else
            {
                strAllDivision = cmbDivision.SelectedValue.ToString();
            }
            dataSetReport = clsReports.ListCashNamePluckingDetails(Convert.ToDateTime(dtpFromDate.Value.ToShortDateString()), Convert.ToDateTime(dtpToDate.Value.ToShortDateString()),  strAllDivision);
            dataSetReport.WriteXml("EmployeeCashNamePluckingDetail.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                CashNamePluckingRPT rptObj = new CashNamePluckingRPT();
                rptObj.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                rptObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                rptObj.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                rptObj.SetParameterValue("Options", "Division : " + cmbDivision.Text.ToString() + " / From:" + dtpFromDate.Value.ToShortDateString() + "   To:" + dtpToDate.Value.ToShortDateString());

                rptObj.SetParameterValue("WorkType", "Work Type : Cash Name Plucking");
                //if (intworktyp == 1)
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");

                //}
                //else
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                //}
                myReportViewer.crystalReportViewer1.ReportSource = rptObj;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void btnDailyEntry_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetDailyEntryData(strDivision, Fromdate, Todate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyDataSummary.xml");
                DailyDataEntrySummaryRPT myReportViewe = new DailyDataEntrySummaryRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMusterSummary_Click(object sender, EventArgs e)
        {
            String strAllDivision = "%";
            if (chkDivision.Checked)
            {
                strAllDivision = "%";
            }
            else
            {
                strAllDivision = cmbDivision.SelectedValue.ToString();
            }
            dataSetReport = objMuster.GetMusterSummary(dtpFromDate.Value.Date, dtpToDate.Value.Date, strAllDivision);
            dataSetReport.WriteXml("MusterSummary.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                MusterChitSummaryRPT rptObj = new MusterChitSummaryRPT();
                rptObj.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();
                rptObj.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                rptObj.SetParameterValue("Period", "From:"+ dtpFromDate.Value.Date.ToShortDateString() + " To: " + dtpToDate.Value.Date.ToShortDateString());
                rptObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                rptObj.SetParameterValue("Division", "Division : " + cmbDivision.Text.ToString());

                
                //if (intworktyp == 1)
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");

                //}
                //else
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                //}
                myReportViewer.crystalReportViewer1.ReportSource = rptObj;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void btnEmpEntries_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetDailyEntryData(strDivision, Fromdate, Todate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyDataSummary.xml");
                DailyDataEntrySummaryEmpWiseRPT myReportViewe = new DailyDataEntrySummaryEmpWiseRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnJobwiseOT_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            String strCropTypeOt = "%";
            String strParamCrop = "All";
            if (chkAllCropsOt.Checked)
            {
                strCropTypeOt = "%";
                strParamCrop = "All Crops";
            }
            else
            {
                strCropTypeOt = cmbCropTypeOt.SelectedValue.ToString();
                strParamCrop = FTSSettings.getNameByCode("CropType", Convert.ToInt32(cmbCropTypeOt.SelectedValue.ToString()));
            }
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
                
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date.ToShortDateString());
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date.ToShortDateString());
            ds = myReports.GetOvertimeJobWiseDistribution(strDivision, Fromdate, Todate, strCropTypeOt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("OvertimeDetailsJobWise.xml");
                OvertimeDetailsJobWiseRPT myReportViewe = new OvertimeDetailsJobWiseRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myReportViewe.SetParameterValue("crop", strParamCrop);
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGLSummary_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPLSummary_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetDailyEntryData(strDivision, Fromdate, Todate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyPLKSummary.xml");
                DailyPlkSummaryRPT myReportViewe = new DailyPlkSummaryRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDaywiseDE_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetDailyEntryData(strDivision, Fromdate, Todate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyDataSummary.xml");
                DailyDataEntriesSummaryDayWise myReportViewe = new DailyDataEntriesSummaryDayWise();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRubberTapping_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetTapDetails( Fromdate, Todate,strDivision);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyTappingSummary.xml");
                DailyDataEntryTappingRPT myReportViewe = new DailyDataEntryTappingRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCashBreakDown_Click(object sender, EventArgs e)
        {
            String strCrop = "%";
            String strAllDiv = "%";
            DateTime dtFrom = dtpFromDate.Value.Date;
            DateTime dtTo = dtpToDate.Value.Date;
            //if (chkAllCrops.Checked)
            //    strCrop = "%";
            //else
            //    strCrop = cmbCropType.SelectedValue.ToString();
            if (chkDivision.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();
            DataSet dsWagesRPT = myWage.GetCashWorkBreakDown(strAllDiv, strCrop, dtFrom, dtTo);
            if (dsWagesRPT.Tables[0].Rows.Count > 0)
            {
                dsWagesRPT.WriteXml("CheckrollCashBreakDown.xml");
                WagesCashWorkBreakDownRPT myReport1 = new WagesCashWorkBreakDownRPT();
                myReport1.SetDataSource(dsWagesRPT);
                ReportViewer myViewer = new ReportViewer();
                myReport1.SetParameterValue("Date", "From:" + dtpFromDate.Value.Date.ToShortDateString() + " To:" + dtpToDate.Value.Date.ToShortDateString());
                myReport1.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                myReport1.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                if (chkDivision.Checked)
                {
                    myReport1.SetParameterValue("Division", "All Division");
                }
                else
                {
                    myReport1.SetParameterValue("Division", "Division " + strAllDiv);
                }
                
                    myReport1.SetParameterValue("Crop Type", "ALL");
                

                myViewer.crystalReportViewer1.ReportSource = myReport1;
                myViewer.Show();
            }
        }

        private void btnOilPalmEntries_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            Int32 intWorkType=1;
            if(rbGeneral.Checked)
            {
                intWorkType=1;
            }
             else
            {
                intWorkType=2;
            }
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetOilParmEntries(Fromdate, Todate, strDivision,intWorkType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyEntriesOilPalm.xml");
                DailyDataEntryOilPalmRPT myReportViewe = new DailyDataEntryOilPalmRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strDivision = cmbDivision.SelectedValue.ToString();
            Int32 intWorkType = 1;
            int intCropType = Convert.ToInt32(cmbCropType.SelectedValue.ToString());
            if (rbGeneral.Checked)
            {
                intWorkType = 1;
            }
            else
            {
                intWorkType = 2;
            }
            if (chkDivision.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }
            DateTime Fromdate = Convert.ToDateTime(dtpFromDate.Value.Date);
            DateTime Todate = Convert.ToDateTime(dtpToDate.Value.Date);
            ds = myReports.GetOtherCropEntries(Fromdate, Todate, strDivision, intWorkType, intCropType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DailyEntriesOilPalm.xml");
                DailyDataEntryOtherCropRPT myReportViewe = new DailyDataEntryOtherCropRPT();
                myReportViewe.SetDataSource(ds);
                myReportViewe.SetParameterValue("Estate", "Estate:" + myEstate.ListEstates().Rows[0][0].ToString() + " / DivisionID:" + cmbDivision.SelectedValue.ToString());
                myReportViewe.SetParameterValue("Date", "From: " + Fromdate.ToShortDateString() + "  To: " + Todate.ToShortDateString());
                myReportViewe.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myReportViewe;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Print", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}