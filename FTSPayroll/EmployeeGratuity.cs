using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeGratuity : Form
    {
        public EmployeeGratuity()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            EmployeeGratuityRPT myaclist = new EmployeeGratuityRPT();
            ReportViewer myReportViewer = new ReportViewer();

            myaclist.SetParameterValue("Estate", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Period", "Report Up to" + dtDate.Value.Date.ToShortDateString());
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }
    }
}