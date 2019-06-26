using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CashNamePluckingRegister : Form
    {
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        public CashNamePluckingRegister()
        {
            InitializeComponent();
        }

        private void chkEmpRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmpRange.Checked)
                gbEmpRange.Enabled = true;
            else
                gbEmpRange.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                if (chkEmpRange.Checked)
                {
                    ds = myReports.getHarvestNamePlkRegister(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dtpFrom.Value.Date.ToShortDateString()), Convert.ToDateTime(dtpTo.Value.Date.ToShortDateString()),txtEmpNoFrom.Text,txtEmpNoTo.Text);
                }
                else
                {
                    ds = myReports.getHarvestNamePlkRegister(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dtpFrom.Value.Date.ToShortDateString()), Convert.ToDateTime(dtpTo.Value.Date.ToShortDateString()));
                }

                ds.WriteXml("CashNamePlkRegister.xml");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CashNamePluckingRegisterRPT myDailyRep = new CashNamePluckingRegisterRPT();
                    myDailyRep.SetDataSource(ds);
                    ReportViewer myReportViewer = new ReportViewer();
                    myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myDailyRep.SetParameterValue("Date", "From:" + dtpFrom.Value.Date.ToShortDateString() + " To:" + dtpTo.Value.Date.ToShortDateString());
                    myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
                    myReportViewer.Show();
                }
                else
                    MessageBox.Show("No Data To Preview", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CashNamePluckingRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            gbEmpRange.Enabled = false;

        }
    }
}