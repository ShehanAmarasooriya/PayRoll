using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeUnions : Form
    {
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeUnion myEmpUnion = new FTSPayRollBL.EmployeeUnion();
        FTSPayRollBL.ListingDetails objListing = new FTSPayRollBL.ListingDetails();
        public EmployeeUnions()
        {
            InitializeComponent();
        }

        private void EmployeeUnions_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbNewUnion.DataSource = myEmpUnion.ListAllUnion();
            cmbNewUnion.DisplayMember = "UnionCode";
            cmbNewUnion.ValueMember = "UnionCode";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            String StrAllDiv = "";
            String StrAllUnion = "";
            if (chkAllDiv.Checked)
            {
                StrAllDiv = "%";
            }
            else
            {
                StrAllDiv = cmbDivision.SelectedValue.ToString();
            }
            if (chkAllUnion.Checked)
            {
                StrAllUnion = "%";
            }
            else 
            {
                StrAllUnion = cmbNewUnion.SelectedValue.ToString();
            }
            DataSet dsEmpUnionReport = new DataSet();
            dsEmpUnionReport = objListing.getEmployeeUnions(StrAllDiv,StrAllUnion);
            dsEmpUnionReport.WriteXml("EmployeeUnionList.xml");

            EmpUnionReport objReport = new EmpUnionReport();
            objReport.SetDataSource(dsEmpUnionReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();
            objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }
    }
}