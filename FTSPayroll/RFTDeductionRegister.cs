using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class RFTDeductionRegister : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.RFTDeductions RFTDeduct = new FTSPayRollBL.RFTDeductions();
        public RFTDeductionRegister()
        {
            InitializeComponent();
        }

        private void RFTDeductionRegister_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.Text = DateTime.Now.Date.Year.ToString();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductGroup.DataSource = DeductMaster.ListRFTDeductionGroups();
            cmbDeductGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductGroup.ValueMember = "DeductionGroupId";
            cmbDeductGroup_SelectedIndexChanged(null, null);
        }       

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            if (chkAllEmp.Checked == true)
            {
                dataSetReport = myReports.getRFTDeductionsRegister(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));
            }
            else
            {
                String empNoFrom = txtEmpFrom.Text.Trim().ToString();
                String empNoTo = txtEmpTo.Text.Trim().ToString();

                dataSetReport = myReports.getRFTDeductionsRegisterByEmpRange(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()), empNoFrom,empNoTo);
            }
  
            dataSetReport.WriteXml("RFTDeductionsRegister.xml");
            RFTDeductionsRegisterRPT myaclist = new RFTDeductionsRegisterRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
            myaclist.SetParameterValue("Division", cmbDivision.Text);
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void chkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllEmp.Checked == true)
            {
                txtEmpFrom.Enabled = false;
                txtEmpTo.Enabled = false;

            }
            else
            {
                txtEmpFrom.Enabled = true;
                txtEmpTo.Enabled = true;
            }
        }
    }
}