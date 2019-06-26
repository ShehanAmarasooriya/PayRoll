using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class PaymentDetails : Form
    {
        FTSPayRollBL.Reports myReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();

        public PaymentDetails()
        {
            InitializeComponent();
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            try
            {
                String strParamDiv = "All Division";
                String strDivision = "%";
                if (!chkAllDivisions.Checked)
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                    strParamDiv = cmbDivision.SelectedValue.ToString();
                }
                DataSet ds = new DataSet();

                //dt = myRep.GetAmalgamation(cmbYear.Text, month);
                ds = myReports.PaymentDetailsSummary(strDivision, dtpFrom.Value.Date, dtpTo.Value.Date);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    ds.WriteXml("PaymentDetailsSummary.xml");

                    PaymentDetailsSummaryRPT myReport = new PaymentDetailsSummaryRPT();
                    myReport.SetDataSource(ds);
                    ReportViewer myViewer = new ReportViewer();

                    myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString()+" - Division:"+ strParamDiv);
                    myReport.SetParameterValue("Period", "From:" + dtpFrom.Value.Date.ToShortDateString() + " To:" + dtpTo.Value.Date.ToShortDateString());
                    myViewer.crystalReportViewer1.ReportSource = myReport;
                    myViewer.Show();

                }
                else
                {
                    MessageBox.Show("No Data Preview..!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!" + ex.Message);
            }
        }

        private void PaymentDetails_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);

            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbEmpNo.DataSource = null;
            cmbEmpNo.DataSource = myMaster.ListAllEmployees(cmbDivision.SelectedValue.ToString());
            cmbEmpNo.DisplayMember = "EmpNo";
            cmbEmpNo.ValueMember = "EmpNo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String strParamDiv = "All Division";
                String strParamEmp="All Employees";
                String strEmployee="%";
                String strDivision = "%";
                if (!chkAllDivisions.Checked)
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                    strParamDiv = cmbDivision.SelectedValue.ToString();
                }
                if(!chkAllEmp.Checked)
                {
                    strEmployee=cmbEmpNo.SelectedValue.ToString();
                    strParamEmp=cmbEmpNo.SelectedValue.ToString();
                }
                DataSet ds = new DataSet();

                //dt = myRep.GetAmalgamation(cmbYear.Text, month);
                ds=myReports.PaymentDetailsSummaryEmpwise(strDivision, dtpFrom.Value.Date, dtpTo.Value.Date,strEmployee);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.WriteXml("PaymentDetailsSummaryEmpwise.xml");

                    //PaymentDetailsSummaryEmpRPTrpt myReport = new PaymentDetailsSummaryEmpRPTrpt();
                    EmployeeWisePaymentDetailsRPT myReport = new EmployeeWisePaymentDetailsRPT();
                    myReport.SetDataSource(ds);
                    ReportViewer myViewer = new ReportViewer();

                    myReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    if (chkAllDivisions.Checked)
                    {
                        myReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString() + " - Division:" + strParamDiv);
                    }
                    else
                    {
                        myReport.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString() + " - Division:" + myDiv.GetDivisionNameByID(strParamDiv));
                    }
                    myReport.SetParameterValue("Period", "From:" + dtpFrom.Value.Date.ToShortDateString() + " To:" + dtpTo.Value.Date.ToShortDateString());
                    
                    myViewer.crystalReportViewer1.ReportSource = myReport;
                    myViewer.Show();

                }
                else
                {
                    MessageBox.Show("No Data Preview..!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!" + ex.Message);
            }
        }
    }
}