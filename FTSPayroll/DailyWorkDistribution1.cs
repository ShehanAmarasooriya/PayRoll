using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyWorkDistribution1 : Form
    {
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Reports myReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.EstateDivisionBlock myDivi = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        public DailyWorkDistribution1()
        {
            InitializeComponent();
        }

        private void DailyWorkDistribution1_Load(object sender, EventArgs e)
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            DataSet dataSetReportSub = new DataSet();

            String strDivisionID = "%";
            String strDivisionName = "ALL";
            Int32 intworktyp = 1;
            DateTime dtFrom;
            DateTime dtTo;

            if (rbNormal.Checked)
            {
                intworktyp = 1;
            }
            else
            {
                intworktyp = 2;
            }
            if(chkAllDivisions.Checked)
            {
                strDivisionID = "%";
            }
            else
            {
                strDivisionID=cmbDivision.SelectedValue.ToString();
                strDivisionName = cmbDivision.Text;
            }
            if (chkDateRange.Checked)
            {
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
            }
            else
            {
                dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                dtTo = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            }
           

                dataSetReport = myReports.DailyWorkDistributionByRange(dtFrom, dtTo, strDivisionID, intworktyp,true);
                dataSetReport.WriteXml("DailyWorkDistribution.xml");

                dataSetReportSub = myReports.DailyWorkDistributionNONWorkByRange(dtFrom, dtTo, strDivisionID, intworktyp);
                dataSetReportSub.WriteXml("DailyWorkDistributionSub.xml");
            
            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                DailyWorkDistribution1RPT myDailyWorkDistributionRPT = new DailyWorkDistribution1RPT();
                myDailyWorkDistributionRPT.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myDailyWorkDistributionRPT.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                myDailyWorkDistributionRPT.SetParameterValue("DivisionName", strDivisionName);
                
                    myDailyWorkDistributionRPT.SetParameterValue("Period", "From : " + dtFrom.ToShortDateString() + " To : " + dtTo.ToShortDateString());
                
                if (intworktyp == 1)
                {
                    myDailyWorkDistributionRPT.SetParameterValue("WorkType", "For Normal Work");

                }
                else
                {
                    myDailyWorkDistributionRPT.SetParameterValue("WorkType", "For Cash Work");
                }
                myDailyWorkDistributionRPT.SetParameterValue("HalfNames", "(Including Half Names)");
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
            this.Close();
        }

        private void chkAllDivisions_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDivisions.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
                cmbDivision.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            DataSet dataSetReportSub = new DataSet();
            String strDivisionID="%";
            String strDivisionName = "ALL";
            DateTime dtFrom;
            DateTime dtTo;
            if(chkAllDivisions.Checked)
            {
                strDivisionID="%";
            }
            else
            {
                strDivisionID=cmbDivision.SelectedValue.ToString();
                strDivisionName = cmbDivision.Text;
            }
            Int32 intworktyp = 1;

            if (rbNormal.Checked)
            {
                intworktyp = 1;
            }
            else
            {
                intworktyp = 2;
            }
            if (chkDateRange.Checked)
            {
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
            }
            else
            {
                dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                dtTo = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            }
            
                
                    dataSetReport = myReports.DailyWorkDistributionByRange(dtFrom,dtTo,strDivisionID, intworktyp,false);
                    dataSetReport.WriteXml("DailyWorkDistribution.xml");

                    dataSetReportSub = myReports.DailyWorkDistributionNONWorkByRange(dtFrom,dtTo,strDivisionID, intworktyp);
                    dataSetReportSub.WriteXml("DailyWorkDistributionSub.xml");
                

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    //DailyWorkDistributionHalfNames myDailyWorkDistributionRPT = new DailyWorkDistributionHalfNames();
                    DailyWorkDistributionWithoutHalfNames myDailyWorkDistributionRPT = new DailyWorkDistributionWithoutHalfNames();
                    myDailyWorkDistributionRPT.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myDailyWorkDistributionRPT.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                    myDailyWorkDistributionRPT.SetParameterValue("DivisionName", strDivisionName);
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
                    myDailyWorkDistributionRPT.SetParameterValue("HalfNames", "(With Out Holiday Half Names)");
                    myReportViewer.crystalReportViewer1.ReportSource = myDailyWorkDistributionRPT;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
    }
}