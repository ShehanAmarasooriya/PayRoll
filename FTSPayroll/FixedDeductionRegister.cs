using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FixedDeductionRegister : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.RFTDeductions RFTDeduct = new FTSPayRollBL.RFTDeductions();
        public FixedDeductionRegister()
        {
            InitializeComponent();
        }

        private void FixedDeductionRegister_Load(object sender, EventArgs e)
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

            cmbDeductGroup.DataSource = DeductMaster.ListFixedDeductionGroups();
            cmbDeductGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductGroup.ValueMember = "DeductionGroupId";
            cmbDeductGroup_SelectedIndexChanged(null, null);

            chkAllEmp.Checked = true;
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

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSetReport = new DataSet();
              
                if (chkAllEmp.Checked == true)
                {
                    dataSetReport = myReports.GetFixedDeductionRegister(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()));

                }
                else
                {
                    String EmpNoFrom = txtEmpFrom.Text.ToString();
                    String EmpNoTo = txtEmpTo.Text.ToString();

                    dataSetReport = myReports.GetFixedDeductionRegisterByEmpRange(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.Text, Convert.ToInt32(cmbDeductGroup.SelectedValue.ToString()), Convert.ToInt32(cmbDeduct.SelectedValue.ToString()),EmpNoFrom,EmpNoTo);
                   

                }

                dataSetReport.WriteXml("FixedDeductionsRegister.xml");
                FixedDeductionRegisterRPT myaclist = new FixedDeductionRegisterRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();
                myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Month", cmbMonth.Text + " / " + cmbYear.Text);
                myaclist.SetParameterValue("Division", cmbDivision.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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