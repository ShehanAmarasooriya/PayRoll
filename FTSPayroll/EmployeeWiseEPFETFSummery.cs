using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeWiseEPFETFSummery : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        DataSet dataSetReport = new DataSet();

        public EmployeeWiseEPFETFSummery()
        {
            InitializeComponent();
        }

        private void EmployeeWiseEPFETFSummery_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbStartMonth.Text))
            {
                MessageBox.Show("Select The Start Month");
            }
            else if (String.IsNullOrEmpty(cmbEndMonth.Text))
            {
                MessageBox.Show("Select The End Month");
            }
            else
            {
                String strAllDiv = "";
                
                if (chkAllDivisions.Checked)
                {
                    strAllDiv = "%";
                }
                else
                {
                    strAllDiv = cmbDivision.SelectedValue.ToString();
                }

                try
                {
                    dataSetReport = myReports.getEmployeeWiseEPFETFSummary(Convert.ToInt32(cmbStartMonth.Text), Convert.ToInt32(cmbEndMonth.Text), strAllDiv);
                    dataSetReport.WriteXml("EmployeeWiseEPFETFSummery.xml");

                    if (dataSetReport.Tables[0].Rows.Count > 0)
                    {
                        EmployeeWiseEPFETFSummeryRPT myEPFETFSummery = new EmployeeWiseEPFETFSummeryRPT();
                        myEPFETFSummery.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();

                        myEPFETFSummery.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                        myEPFETFSummery.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                        myEPFETFSummery.SetParameterValue("DateRange", "From: " + cmbStartMonth.Text + " To :" + cmbEndMonth.Text);
                        myReportViewer.crystalReportViewer1.ReportSource = myEPFETFSummery;
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

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}