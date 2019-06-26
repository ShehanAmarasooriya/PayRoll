using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class SettingsParametersAndRatesListing : Form
    {
        FTSPayRollBL.ListingDetails objListing = new FTSPayRollBL.ListingDetails();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();

        public SettingsParametersAndRatesListing()
        {
            InitializeComponent();
        }

        private void btnFixedParam_Click(object sender, EventArgs e)
        {
            DataSet dsFixedParameterReport = new DataSet();
            dsFixedParameterReport = objListing.getFixedParameters();
            dsFixedParameterReport.WriteXml("FixedParameter.xml");

            FixedParameterReport objReport = new FixedParameterReport();
            objReport.SetDataSource(dsFixedParameterReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void btnFieldSettings_Click(object sender, EventArgs e)
        {
            DataSet dsFixedParameterReport = new DataSet();
            dsFixedParameterReport = objListing.getFieldSettings();
            dsFixedParameterReport.WriteXml("FieldSettingsListing.xml");

            FieldSettingsListingRPT objReport = new FieldSettingsListingRPT();
            objReport.SetDataSource(dsFixedParameterReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        
    }
}