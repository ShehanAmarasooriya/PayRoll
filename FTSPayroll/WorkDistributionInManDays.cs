using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class WorkDistributionInManDays : Form
    {
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Reports myReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.EstateDivisionBlock myDivi = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();

        public WorkDistributionInManDays()
        {
            InitializeComponent();
        }

        private void WorkDistributionInManDays_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myYearMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = myYearMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            try
            {
                cmbYear.SelectedValue = myYearMonth.getLastYearID();
                cmbMonth.SelectedValue = myYearMonth.getLastMonthID();
            }
            catch
            {
            }

            cmbDivision.DataSource = myDivi.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            rbNormal.Checked = true;

            gbYearMonth.Enabled = true;
            gbDateRange.Enabled = false;
        }

        private void chkDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateRange.Checked)
            {
                gbDateRange.Enabled = true;
                gbYearMonth.Enabled = false;
            }
            else
            {
                gbDateRange.Enabled = false;
                gbYearMonth.Enabled = true;
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            DataSet dataSetReportSub = new DataSet();

            String strDivisionID = "%";
            Int32 intworktyp = 1;

            if (rbNormal.Checked)
            {
                intworktyp = 1;
            }
            else
            {
                intworktyp = 2;
            }
            if (chkAllDivisions.Checked)
            {
                strDivisionID = "%";
            }
            else
            {
                strDivisionID = cmbDivision.SelectedValue.ToString();
            }
            if (chkDateRange.Checked)
            {

                dataSetReport = myReports.DailyWorkDistributionByRange(dtpFrom.Value.Date, dtpTo.Value.Date, strDivisionID, intworktyp,true);
                dataSetReport.WriteXml("DailyWorkDistribution.xml");

                dataSetReportSub = myReports.DailyWorkDistributionNONWorkByRange(dtpFrom.Value.Date, dtpTo.Value.Date, strDivisionID, intworktyp);
                dataSetReportSub.WriteXml("DailyWorkDistributionSub.xml");
            }
            else
            {
                dataSetReport = myReports.DailyWorkDistribution(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivisionID, intworktyp,true);
                dataSetReport.WriteXml("DailyWorkDistribution.xml");

                dataSetReportSub = myReports.DailyWorkDistributionNONWork(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivisionID, intworktyp);
                dataSetReportSub.WriteXml("DailyWorkDistributionSub.xml");
            }
            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                DailyWorkDistribution1RPT myDailyWorkDistributionRPT = new DailyWorkDistribution1RPT();
                myDailyWorkDistributionRPT.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myDailyWorkDistributionRPT.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                myDailyWorkDistributionRPT.SetParameterValue("DivisionName", "ALL");
                if (chkDateRange.Checked)
                {
                    myDailyWorkDistributionRPT.SetParameterValue("Period", "From : " + dtpFrom.Value.Date.ToShortDateString() + " To : " + dtpTo.Value.Date.ToShortDateString());
                }
                else
                {
                    myDailyWorkDistributionRPT.SetParameterValue("Period", "Month : " + cmbMonth.Text + " / " + cmbYear.Text);
                }
                if (intworktyp == 1)
                {
                    myDailyWorkDistributionRPT.SetParameterValue("WorkType", "For Normal Work");

                }
                else
                {
                    myDailyWorkDistributionRPT.SetParameterValue("WorkType", "For Cash Work");
                }
                myReportViewer.crystalReportViewer1.ReportSource = myDailyWorkDistributionRPT;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            DataSet dataSetReportSub = new DataSet();

            String strDivisionID = "%";
            Int32 intworktyp = 1;

            if (rbNormal.Checked)
            {
                intworktyp = 1;
            }
            else
            {
                intworktyp = 2;
            }
            if (chkAllDivisions.Checked)
            {
                strDivisionID = "%";
            }
            else
            {
                strDivisionID = cmbDivision.SelectedValue.ToString();
            }
            if (chkDateRange.Checked)
            {

                dataSetReport = myReports.DailyWorkDistributionByRange(dtpFrom.Value.Date, dtpTo.Value.Date, strDivisionID, intworktyp,false);
                dataSetReport.WriteXml("DailyWorkDistribution.xml");

                dataSetReportSub = myReports.DailyWorkDistributionNONWorkByRange(dtpFrom.Value.Date, dtpTo.Value.Date, strDivisionID, intworktyp);
                dataSetReportSub.WriteXml("DailyWorkDistributionSub.xml");
            }
            else
            {
                dataSetReport = myReports.DailyWorkDistribution(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivisionID, intworktyp,false);
                dataSetReport.WriteXml("DailyWorkDistribution.xml");

                dataSetReportSub = myReports.DailyWorkDistributionNONWork(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivisionID, intworktyp);
                dataSetReportSub.WriteXml("DailyWorkDistributionSub.xml");
            }
            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                DailyWorkDistribution1RPT myDailyWorkDistributionRPT = new DailyWorkDistribution1RPT();
                myDailyWorkDistributionRPT.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myDailyWorkDistributionRPT.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                myDailyWorkDistributionRPT.SetParameterValue("DivisionName", "ALL");
                if (chkDateRange.Checked)
                {
                    myDailyWorkDistributionRPT.SetParameterValue("Period", "From : " + dtpFrom.Value.Date.ToShortDateString() + " To : " + dtpTo.Value.Date.ToShortDateString());
                }
                else
                {
                    myDailyWorkDistributionRPT.SetParameterValue("Period", "Month : " + cmbMonth.Text + " / " + cmbYear.Text);
                }
                if (intworktyp == 1)
                {
                    myDailyWorkDistributionRPT.SetParameterValue("WorkType", "For Normal Work");

                }
                else
                {
                    myDailyWorkDistributionRPT.SetParameterValue("WorkType", "For Cash Work");
                }
                myReportViewer.crystalReportViewer1.ReportSource = myDailyWorkDistributionRPT;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}