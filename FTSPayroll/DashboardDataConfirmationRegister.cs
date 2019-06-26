using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DashboardDataConfirmationRegister : Form
    {
         FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myreport = new FTSPayRollBL.CheckRollReports();

        public DashboardDataConfirmationRegister()
        {
            InitializeComponent();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds=myreport.getConfirmationData(dtpFrom.Value.Date,dtpTo.Value.Date);
            ds.WriteXml("ConfirmationData.xml");
            if (ds.Tables[0].Rows.Count > 0)
            {        
                DataConfirmationRegisterRPT myRep = new DataConfirmationRegisterRPT();
                myRep.SetDataSource(ds);                
                ReportViewer myReportViewer = new ReportViewer();

                myRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myRep.SetParameterValue("Date", "From: " + dtpFrom.Value.Date+"  To: "+dtpTo.Value.Date);
                
                myReportViewer.crystalReportViewer1.ReportSource = myRep;
                myReportViewer.Show();
            }
            else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
           

            }
        
    }
}