using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeListForm : Form
    {
        public EmployeeListForm()
        {
            InitializeComponent();
        }

        FTSPayRollBL.Class1 myStatus = new FTSPayRollBL.Class1();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.ListingDetails objListing = new FTSPayRollBL.ListingDetails();

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
                cmbDivision.Enabled = false;
            else
                cmbDivision.Enabled = true;
        }

        private void EmployeeListForm_Load(object sender, EventArgs e)
        {

            cmbStatus.DataSource = myStatus.getStatus();
           cmbStatus.DisplayMember = "Name";
           cmbStatus.ValueMember = "Name";
           cmbStatus.SelectedIndex = 0;

           cmbDivision.DataSource = myDiv.ListEstateDivisions();
           cmbDivision.DisplayMember = "DivisionID";
           cmbDivision.ValueMember = "DivisionID";
           cmbDivision.SelectedIndex = 0;

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            String strDivisionID = cmbDivision.SelectedValue.ToString();
            String strStatus = cmbStatus.SelectedValue.ToString();
            DataSet dsEmployeeReport = new DataSet();

            if (chkAll.Checked)
            {
                
                dsEmployeeReport = objListing.getEmployeeDetails(strStatus.ToString());
                dsEmployeeReport.WriteXml("EmployeeDetails.xml");

            }
            else
            {
               
                dsEmployeeReport = objListing.getEmployeeDetails(strDivisionID.ToString(),strStatus.ToString());
                dsEmployeeReport.WriteXml("EmployeeDetails.xml");

            }
            if (dsEmployeeReport.Tables[0].Rows.Count > 0)
            {
                EmployeeReport objReport = new EmployeeReport();
                objReport.SetDataSource(dsEmployeeReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", FTSPayRollBL.User.StrEstate);
                if (chkAll.Checked)
                {
                    objReport.SetParameterValue("Division", "");
                }
                else
                {
                    objReport.SetParameterValue("Division", "DivisionID : " + strDivisionID.ToString());
                }
                objReport.SetParameterValue("Status", "Employee Status : " + strStatus.ToString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
                MessageBox.Show("No Data to Preview..!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}