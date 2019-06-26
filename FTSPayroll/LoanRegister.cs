using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class LoanRegister : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();

        public LoanRegister()
        {
            InitializeComponent();
        }

        private void LoanRegister_Load(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSetReport = new DataSet();
                dataSetReport = myReports.getLoanRegister(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {

                    dataSetReport.WriteXml("LoanRegister.xml");
                    LoanRegisterRPT myaclist = new LoanRegisterRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }

                else
                {
                    MessageBox.Show("No data to preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}