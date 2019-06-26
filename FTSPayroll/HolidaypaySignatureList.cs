using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class HolidaypaySignatureList : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        public HolidaypaySignatureList()
        {
            InitializeComponent();
        }

        private void HolidaypaySignatureList_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

        }

        private void btnHoliPay_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                String strDivision = "%";
                if (chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidaypayMandaysEarningsNotOffered(Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidaypaySignatureList.xml");
                    HolidaypaySignatureListRPT myHPData = new HolidaypaySignatureListRPT();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnAttBonus_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                String strDivision = "%";
                if (chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidaypayMandaysEarningsNotOffered(Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("AttBonusSignatureList.xml");
                    AttBonusSignaturePaylist myHPData = new AttBonusSignaturePaylist();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }
    }
}