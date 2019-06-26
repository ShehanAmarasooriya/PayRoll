using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DeductionRegister : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.RFTDeductions RFTDeduct = new FTSPayRollBL.RFTDeductions();

        public DeductionRegister()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void DeductionRegister_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.Text = DateTime.Now.Date.Year.ToString();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDeductGroup.DataSource = DeductMaster.ListRFTDeductionGroups();
            cmbDeductGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductGroup.ValueMember = "DeductionGroupId";

            cmbDeductGroup_SelectedIndexChanged(null, null);
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getDeductionsRegister(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            dataSetReport.WriteXml("DeductionsRegister.xml");
            DeductionsRegisterRPT myaclist = new DeductionsRegisterRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Period", cmbMonth.Text + " of " + cmbYear.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void cmbDeductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeduct.DataSource = null;
                cmbDeduct.DataSource = DeductMaster.ListDeduction(int.Parse(cmbDeductGroup.SelectedValue.ToString()));
                cmbDeduct.DisplayMember = "DeductShortName";
                cmbDeduct.ValueMember = "DeductCode";
            }
            catch (Exception ex)
            { }
        }
    }
}