using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class GuaranteeRecoveryReport : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();        
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MonthlyWeges myGuarantee = new FTSPayRollBL.MonthlyWeges();
        

        public GuaranteeRecoveryReport()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GuaranteeRecoveryReport_Load(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet ds = myGuarantee.GetGuaranteeRecoveryList(cmbDivision.Text, cmbYear.Text, cmbMonth.SelectedValue.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("GuaranteeRecovery.xml");

                GuaranteeRecoveryRPT myRPT = new GuaranteeRecoveryRPT();
                myRPT.SetDataSource(ds);
                myRPT.SetParameterValue("CompanyName",FTSPayRollBL.Company.getCompanyName());
                myRPT.SetParameterValue("Division","DivisionID : " + cmbDivision.Text);
                myRPT.SetParameterValue("Period", "Period : " + cmbYear.Text + " - " + cmbMonth.Text);

                ReportViewer myViewer = new ReportViewer();
                myViewer.crystalReportViewer1.ReportSource = myRPT;
                myViewer.crystalReportViewer1.ShowRefreshButton = false;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No data to preview..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}