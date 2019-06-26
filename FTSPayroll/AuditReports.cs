using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AuditReports : Form
    {
        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.EstateDivisionBlock EstDiv = new FTSPayRollBL.EstateDivisionBlock();

        public AuditReports()
        {
            InitializeComponent();
        }

        private void btnUserAudit_Click(object sender, EventArgs e)
        {
            DataSet dsDivisionReport = new DataSet();
            dsDivisionReport = myReports.getUserAudit(dtpFromDate.Value.Date,dtpToDate.Value.Date);
            dsDivisionReport.WriteXml("UserAudit.xml");

            UserAuditRPT objReport = new UserAuditRPT();
            objReport.SetDataSource(dsDivisionReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
            objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void AuditReports_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstate.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";
        }

        private void btnDailyEntriesAudit_Click(object sender, EventArgs e)
        {
            DataSet dsDivisionReport = new DataSet();
            dsDivisionReport = myReports.getDailyEntriesAudit(dtpFromDate.Value.Date, dtpToDate.Value.Date);
            dsDivisionReport.WriteXml("DailyEntriesAudit.xml");

            DailyEntriesAuditRPT objReport = new DailyEntriesAuditRPT();
            objReport.SetDataSource(dsDivisionReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
            objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void btnMasterAudit_Click(object sender, EventArgs e)
        {
            DataSet dsDivisionReport = new DataSet();
            dsDivisionReport = myReports.getMasterFileAudit(dtpFromDate.Value.Date, dtpToDate.Value.Date);
            dsDivisionReport.WriteXml("MasterFileAudit.xml");

            MasterFileAuditRPT objReport = new MasterFileAuditRPT();
            objReport.SetDataSource(dsDivisionReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
            objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}