using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class GreenLeafSummery : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();

        DataSet dataSetReport = new DataSet();

        public GreenLeafSummery()
        {
            InitializeComponent();
        }

        private void GreenLeafSummery_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            cmbEmployeeCategory.DisplayMember = "CategoryName";
            cmbEmployeeCategory.ValueMember = "CategoryID";
        }

        private void btnDivWiseSummery_Click(object sender, EventArgs e)
        {
            String strAllDiv = "";
            String strAllCategory = "1";
            if (chkAllCategory.Checked)
            {
                strAllCategory = "%";
            }
            else
            {
                strAllCategory = cmbEmployeeCategory.SelectedValue.ToString();
            }
            //if(chkAllDivisions.Checked)
            //{
            //    strAllDiv = "%";
            //}
            //else
            //{
            //    strAllDiv = cmbDivision.SelectedValue.ToString();
            //}
            strAllDiv = "%";

            try
            {
                dataSetReport = myReports.getFieldWiseGreenLeafSummery(strAllDiv, strAllCategory, dtpStartDate.Value, dtpEndDate.Value);
                dataSetReport.WriteXml("GreenLeafFieldWiseSummery.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    GreenLeafSummaryDivisionWise myGLSummFieldWise = new GreenLeafSummaryDivisionWise();
                    myGLSummFieldWise.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myGLSummFieldWise.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myGLSummFieldWise.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                    myGLSummFieldWise.SetParameterValue("Division", "ALL");
                    myGLSummFieldWise.SetParameterValue("DateRange", "From: " + dtpStartDate.Value.ToShortDateString() + " To :" + dtpEndDate.Value.ToShortDateString());
                    myReportViewer.crystalReportViewer1.ReportSource = myGLSummFieldWise;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No data to preview..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error On Get Data, "+ex.Message);
            }

        }

        private void btnFieldSummery_Click(object sender, EventArgs e)
        {
            String strAllDiv = "";
            String strAllCategory = "1";
            String paramDiv = "";
            if (chkAllCategory.Checked)
            {
                strAllCategory = "%";
            }
            else
            {
                strAllCategory = cmbEmployeeCategory.SelectedValue.ToString();               
            }
            if (chkAllDivisions.Checked)
            {
                strAllDiv = "%";
                paramDiv = "All";
            }
            else
            {
                strAllDiv = cmbDivision.SelectedValue.ToString();
                paramDiv = strAllDiv;
            }

            try
            {
                dataSetReport = myReports.getFieldWiseGreenLeafSummery(strAllDiv, strAllCategory, dtpStartDate.Value, dtpEndDate.Value);
                dataSetReport.WriteXml("GreenLeafFieldWiseSummery.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    GreenLeafSummeryFieldWise myGLSummFieldWise = new GreenLeafSummeryFieldWise();
                    myGLSummFieldWise.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myGLSummFieldWise.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myGLSummFieldWise.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                    myGLSummFieldWise.SetParameterValue("Division", paramDiv);
                    myGLSummFieldWise.SetParameterValue("DateRange", "From: " + dtpStartDate.Value.ToShortDateString() + " To :" + dtpEndDate.Value.ToShortDateString());
                    myReportViewer.crystalReportViewer1.ReportSource = myGLSummFieldWise;
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