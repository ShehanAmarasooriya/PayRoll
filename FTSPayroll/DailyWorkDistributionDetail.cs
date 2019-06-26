using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyWorkDistributionDetail : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        DataSet dataSetReport = new DataSet();

        public DailyWorkDistributionDetail()
        {
            InitializeComponent();
        }

        private void DailyWorkDistributionDetail_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();

            cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            cmbEmployeeCategory.DisplayMember = "CategoryName";
            cmbEmployeeCategory.ValueMember = "CategoryID";

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            String strDiv="%";
            String strCat = "%";
            try
            {
                //changes
                if (chkAllDivisions.Checked)
                {
                    strDiv = "%";
                }
                else
                {
                    strDiv = cmbDivision.SelectedValue.ToString();
                }
                if (chkAllCategory.Checked)
                {
                    strCat = "%";
                }
                else
                {
                    strCat = cmbEmployeeCategory.SelectedValue.ToString();
                }
                //---------
                dataSetReport = myReports.getWorkDistributionDetail(strDiv, Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), strCat);
                dataSetReport.WriteXml("WorkDistributionDetail.xml");
                WorkDistributionDetailRPT myaclist = new WorkDistributionDetailRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Estate", "Estate : " + myDivision.ListEstate().Rows[0][1].ToString());
                myaclist.SetParameterValue("Options", "For Division : " + cmbDivision.Text + " and Category : " + cmbEmployeeCategory.Text + " for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
                //----------
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
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

        private void chkAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCategory.Checked)
            {
                cmbEmployeeCategory.Enabled = false;
            }
            else
                cmbEmployeeCategory.Enabled = true;
        }
    }
}