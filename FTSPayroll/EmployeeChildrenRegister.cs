using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeChildrenRegister : Form
    {
        FTSPayRollBL.EstateDivisionBlock myEstateDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();

        public EmployeeChildrenRegister()
        {
            InitializeComponent();
        }


        private void EmployeeChildrenRegister_Load(object sender, EventArgs e)
        {
            DivisionID.DataSource = myEstateDiv.ListEstateDivisions();
            DivisionID.DisplayMember = "DivisionName";
            DivisionID.ValueMember = "DivisionID";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (txtEmpNo.Text.Trim() == "")
            {
                DataSet ds = new DataSet();

                if (chkAll.Checked)
                {
                    ds = myMaster.GetChildrenDetails("%");
                }
                else
                {
                    ds = myMaster.GetChildrenDetails(DivisionID.SelectedValue.ToString());
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("EmployeeChildrendetails.xml");

                    EmployeeChildrenRegisterRPT myreport = new EmployeeChildrenRegisterRPT();
                    myreport.SetDataSource(ds);

                    myreport.SetParameterValue("CompanyName",FTSPayRollBL.Company.getCompanyName().Trim());

                    if (chkAll.Checked)
                    {
                        myreport.SetParameterValue("Division", "For All Divisions");
                    }
                    else
                    {
                        myreport.SetParameterValue("Division", "Division : " + DivisionID.Text.Trim());
                    }

                    ReportViewer myviewer = new ReportViewer();
                    myviewer.crystalReportViewer1.ReportSource = myreport;
                    myviewer.Show();
                }
                else
                {
                    MessageBox.Show("No data to preview..!");
                }
            }
            else if (txtEmpNo.Text.Trim() != "")
            {
                DataSet ds = new DataSet();
                
                ds = myMaster.GetChildrenDetails(DivisionID.SelectedValue.ToString(),txtEmpNo.Text.Trim());
                

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("EmployeeChildrendetails.xml");

                    EmployeeChildrenRegisterRPT myreport = new EmployeeChildrenRegisterRPT();
                    myreport.SetDataSource(ds);

                    myreport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName().Trim());
                    myreport.SetParameterValue("Division", "Division : " + DivisionID.Text.Trim());

                    ReportViewer myviewer = new ReportViewer();
                    myviewer.crystalReportViewer1.ReportSource = myreport;
                    myviewer.Show();
                }
                else
                {
                    MessageBox.Show("No data to preview..!");
                }
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                txtEmpNo.Clear();
                DivisionID.Enabled = false;
            }
            else
                DivisionID.Enabled = true;
        }
    }
}