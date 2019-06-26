using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class HolidayPayData : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        DataSet dataSetReport = new DataSet();

        public HolidayPayData()
        {
            InitializeComponent();
        }

        private void HolidayPayData_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            cmbEmployeeCategory.DisplayMember = "CategoryName";
            cmbEmployeeCategory.ValueMember = "CategoryID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strAllDiv = "%";
            String strAllCat = "%";
            if (chkAllDivisions.Checked)
            {
                strAllDiv = "%";
            }
            else
            {
                strAllDiv = cmbDivision.SelectedValue.ToString();
            }
            if (chkAllCategory.Checked)
            {
                strAllCat = "%";
            }
            else
            {
                strAllCat = cmbEmployeeCategory.SelectedValue.ToString();
            }
            try
            {
                dataSetReport = myReports.getEmployeeHolidayPayData(dtpStartDate.Value, dtpEndDate.Value, strAllDiv, strAllCat);
                dataSetReport.WriteXml("EmployeeWiseHolidayPayData.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    HolidayPayDataRPT myHoliPayData = new HolidayPayDataRPT();
                    myHoliPayData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHoliPayData.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myHoliPayData.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                    myHoliPayData.SetParameterValue("DateRange", "From: " + dtpStartDate.Value.ToShortDateString() + " To :" + dtpEndDate.Value.ToShortDateString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHoliPayData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No data to preview..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error On Get Data, " + ex.Message);
            }
        }
    }
}