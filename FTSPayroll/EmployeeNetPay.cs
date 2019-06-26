using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeNetPay : Form
    {
        public EmployeeNetPay()
        {
            InitializeComponent();
        }
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Reports myRepotrs = new FTSPayRollBL.Reports();
        FTSPayRollBL.Division mydiv = new FTSPayRollBL.Division();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.SystemSetting ChkSettings = new FTSPayRollBL.SystemSetting();
        

        private void EmployeeNetPay_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myYearMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYearMonth.getLastYearID();

            cmbMonth.DataSource = myYearMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myYearMonth.getLastMonthID();

           

            cmbEmpCat.DataSource = myCatagory.ListCategories();
            cmbEmpCat.DisplayMember = "CategoryName";
            cmbEmpCat.ValueMember = "CategoryID";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            String AllCat = "%";
            if (chkAllCat.Checked)
            {
                AllCat = "%";
            }
            else
            {
                AllCat = cmbEmpCat.SelectedValue.ToString();
            }
            DataSet dataSetReport = new DataSet();

            myRepotrs.StrDivisionID = cmbDivision.SelectedValue.ToString();
            myRepotrs.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            myRepotrs.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());

            dataSetReport = myRepotrs.EmployeeNetPay(AllCat);
            dataSetReport.WriteXml("EmployeeNetpayRPT.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                if (ChkSettings.IsNetPayWithGrossEarnings())
                {
                    EmployeeNetPayWithGrossPayRPT myEmployeeNetpayRPT = new EmployeeNetPayWithGrossPayRPT();
                    myEmployeeNetpayRPT.SetDataSource(dataSetReport);
                    myEmployeeNetpayRPT.SetParameterValue("Estate", "Estate :" + EstDivBlock.GetClusterName(mydiv.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myEmployeeNetpayRPT;
                    myReportViewer.Show();
                }
                else
                {
                    EmployeeNetpayRPT myEmployeeNetpayRPT = new EmployeeNetpayRPT();
                    myEmployeeNetpayRPT.SetDataSource(dataSetReport);
                    myEmployeeNetpayRPT.SetParameterValue("Estate", "Estate :" + EstDivBlock.GetClusterName(mydiv.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myEmployeeNetpayRPT;
                    myReportViewer.Show();
                }
               
            }
            else
                MessageBox.Show("No Data to Preview..!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String AllCat = "%";
            if (chkAllCat.Checked)
            {
                AllCat = "%";
            }
            else
            {
                AllCat = cmbEmpCat.SelectedValue.ToString();
            }
            DataSet dataSetReport = new DataSet();

            myRepotrs.StrDivisionID = cmbDivision.SelectedValue.ToString();
            myRepotrs.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            myRepotrs.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());

            dataSetReport = myRepotrs.EmployeeNetPay(AllCat);
            dataSetReport.WriteXml("EmployeeNetpayRPT.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                if (ChkSettings.IsNetPayWithGrossEarnings())
                {
                    EmployeeNetPayWithGrossPayRPTEnvelop myEmployeeNetpayRPT = new EmployeeNetPayWithGrossPayRPTEnvelop();
                    myEmployeeNetpayRPT.SetDataSource(dataSetReport);
                    myEmployeeNetpayRPT.SetParameterValue("Estate", "Estate :" + EstDivBlock.GetClusterName(mydiv.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myEmployeeNetpayRPT;
                    myReportViewer.Show();
                }
                else
                {
                    EmployeeNetpayRPTEnvelop myEmployeeNetpayRPT = new EmployeeNetpayRPTEnvelop();
                    myEmployeeNetpayRPT.SetDataSource(dataSetReport);
                    myEmployeeNetpayRPT.SetParameterValue("Estate", "Estate :" + EstDivBlock.GetClusterName(mydiv.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myEmployeeNetpayRPT;
                    myReportViewer.Show();
                }

            }
            else
                MessageBox.Show("No Data to Preview..!");
        }

       
    }
}