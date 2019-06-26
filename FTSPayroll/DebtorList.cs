using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DebtorList : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        

        public DebtorList()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DebtorList_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();
            cmbYear_SelectedIndexChanged(null, null);

            
            cmbMonth.Text = FTSPayRollBL.User.StrMonth;

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myReports.getDebtorList(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),cmbDivision.SelectedValue.ToString());

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                dataSetReport.WriteXml("DebtorList.xml");
                DebtorListRPT myaclist = new DebtorListRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                myaclist.SetParameterValue("Period", cmbMonth.Text + " of " + cmbYear.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            else
                MessageBox.Show("No Data to preview..!");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Boolean blYesNo = false;
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name == "DebtorsRecoveryList")
                {
                    blYesNo = true;
                    break;
                }                
            }

            if (blYesNo == false)
            {
                DebtorsRecoveryList myDebtorsRecoveryList = new DebtorsRecoveryList(cmbDivision.Text.Trim(), cmbYear.Text.Trim(), cmbMonth.SelectedValue.ToString(), "", "");
                myDebtorsRecoveryList.Show();
            }
            else
            {
                DebtorsRecoveryList myDebtorsRecoveryList = new DebtorsRecoveryList(cmbDivision.Text.Trim(), cmbYear.Text.Trim(), cmbMonth.SelectedValue.ToString(), "", "");
                myDebtorsRecoveryList.Close();
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbMonth.SelectedValue = myMonth.getLastMonthID();
        }
    }
}