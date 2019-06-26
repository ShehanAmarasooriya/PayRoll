using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class GratuitypayFinalReport : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        Boolean boolAll = false;
        String strDivision;
        public GratuitypayFinalReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                if (strDivision == "All")
                {
                    dataSetReport.Tables.Add(HoliPay.GetGratuityPayDataReportData("%", Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                }
                else
                {
                    dataSetReport.Tables.Add(HoliPay.GetGratuityPayDataReportData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                }
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("GratuityPayDataPreview.xml");
                    GratuitypayDataPreview myGPData = new GratuitypayDataPreview();
                    myGPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myGPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myGPData.SetParameterValue("Date", cmbYear.SelectedValue.ToString());
                    myGPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + strDivision);
                    myReportViewer.crystalReportViewer1.ReportSource = myGPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview Gratuitypay, \r\n May be Confirmed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void GratuitypayFinalReport_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                boolAll = true;
                strDivision = "All";

            }
            else
            {
                boolAll = false;
                strDivision = cmbDivision.SelectedValue.ToString();
            }
        }
    }
}