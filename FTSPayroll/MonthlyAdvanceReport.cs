using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class MonthlyAdvanceReport : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.RFTDeductions RFTDeduct = new FTSPayRollBL.RFTDeductions();
        public MonthlyAdvanceReport()
        {
            InitializeComponent();
        }

        private void MonthlyAdvanceReport_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);


            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductGroup.DataSource = DeductMaster.ListFixedDeductionGroups();
            cmbDeductGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductGroup.ValueMember = "DeductionGroupId";

            cmbDeductGroup.SelectedValue = "MA";

            cmbDeductGroup_SelectedIndexChanged(null, null);
        }

        private void cmbDeductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeduct.DataSource = null;
                cmbDeduct.DataSource = DeductMaster.ListDeduction(int.Parse(cmbDeductGroup.SelectedValue.ToString()));
                cmbDeduct.DisplayMember = "DeductShortName";
                cmbDeduct.ValueMember = "DeductCode";
            }
            catch (Exception ex)
            { }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = myMonth.ListMonths();
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";
                cmbMonth.SelectedValue = myMonth.getLastMonthID();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            dataSetReport = myReports.GetMonthlyAdvanceReport(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text,Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
            dataSetReport.WriteXml("MonthlyAdvanceReport.xml");
            MonthlyAdvanceRPT myaclist = new MonthlyAdvanceRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
            myaclist.SetParameterValue("Division", cmbDivision.Text);
            myaclist.SetParameterValue("Deduction", "Monthly Advance Code : "+cmbDeduct.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            dataSetReport = myReports.getCoinAnalysis(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()),cmbDivision.SelectedValue.ToString());
            //dataSetReport = myReports.GetMonthlyAdvanceReport(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
            dataSetReport.WriteXml("MACoinAnalysis.xml");
            MonthlyAdvanceCoinAnalysisRPT myaclist = new MonthlyAdvanceCoinAnalysisRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
            myaclist.SetParameterValue("Period", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
            myaclist.SetParameterValue("Deduction", "Advance Code : " + cmbDeduct.Text);

            //myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
            //myaclist.SetParameterValue("Division", cmbDivision.Text);
            //myaclist.SetParameterValue("Deduction", "Monthly Advance Code : " + cmbDeduct.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void btnCoinSummary_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            dataSetReport = myReports.getCoinAnalysis(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()), "%");
            //dataSetReport = myReports.GetMonthlyAdvanceReport(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
            dataSetReport.WriteXml("MACoinAnalysis.xml");
            MonthlyAdvanceCoinAnalysisDivisionWiseRPT myaclist = new MonthlyAdvanceCoinAnalysisDivisionWiseRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
            myaclist.SetParameterValue("Period", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
            myaclist.SetParameterValue("Deduction", "Advance Code : " + cmbDeduct.Text);

            //myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
            //myaclist.SetParameterValue("Division", cmbDivision.Text);
            //myaclist.SetParameterValue("Deduction", "Monthly Advance Code : " + cmbDeduct.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }
    }
}