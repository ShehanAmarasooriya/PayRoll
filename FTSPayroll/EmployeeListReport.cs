using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeListReport : Form
    {
        FTSPayRollBL.ListingDetails objListing = new FTSPayRollBL.ListingDetails();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();

        public EmployeeListReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String AllDiv = "%";
            String AllCat = "%";
            String AllGender = "%";
            if (!chkAllDiv.Checked)
            {
                AllDiv = cmbDivision.SelectedValue.ToString();
            }
            if (!chkAllCat.Checked)
            {
                AllCat = cmbAllCat.SelectedValue.ToString();
            }
            if (!chkAllGender.Checked)
            {
                AllGender = cmbAllGender.SelectedValue.ToString();
            }
            ds = objListing.getEmployeeDetails(AllDiv, AllCat, AllGender);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("EmployeeDetails.xml");
                EmployeeDetailsRep myReport = new EmployeeDetailsRep();
                myReport.SetDataSource(ds);

                ReportViewer myReportViewer = new ReportViewer();
                myReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                myReport.SetParameterValue("Estate", "ESTATE :" + myUser.GetEstates().Tables[0].Rows[0][1].ToString());
                myReportViewer.crystalReportViewer1.ReportSource = myReport;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }


        }

        private void EmployeeListReport_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbAllCat.DataSource = EmpCat.ListCategories();
            cmbAllCat.DisplayMember = "CategoryName";
            cmbAllCat.ValueMember = "CategoryID";

            
            cmbAllGender.DataSource = FTSSettings.ListDataFromSettings("Gender");
            cmbAllGender.DisplayMember = "Name";
            cmbAllGender.ValueMember = "Name";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}