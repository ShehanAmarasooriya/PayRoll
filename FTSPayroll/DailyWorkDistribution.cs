using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyWorkDistribution : Form
    {
        public DailyWorkDistribution()
        {
            InitializeComponent();
        }
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Reports myReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.EstateDivisionBlock myDivi = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();

        private void DailyWorkDistribution_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myYearMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = myYearMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = myDivi.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            rbNormal.Checked = true;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

           
            String strDivisionID = cmbDivision.SelectedValue.ToString();
            Int32 intworktyp = 1;

            if (rbNormal.Checked)
            {
                intworktyp = 1;
            }
            else
            {
                intworktyp = 2;
            }            

                dataSetReport = myReports.DailyWorkDistribution(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), (cmbDivision.SelectedValue.ToString()), intworktyp,true);
                dataSetReport.WriteXml("DailyWorkDistribution.xml");

            
            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                DailyWorkDistributionRPT myDailyWorkDistributionRPT = new DailyWorkDistributionRPT();
                myDailyWorkDistributionRPT.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myDailyWorkDistributionRPT.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                myDailyWorkDistributionRPT.SetParameterValue("DivisionName", myDivi.EstateDivision(strDivisionID).Tables[0].Rows[0][0].ToString());
                myDailyWorkDistributionRPT.SetParameterValue("Period","For the Month of :" + cmbMonth.Text + " / " + cmbYear.Text );
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
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}