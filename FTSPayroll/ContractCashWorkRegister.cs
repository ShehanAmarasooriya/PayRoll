using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class ContractCashWorkRegister : Form
    {
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myreport = new FTSPayRollBL.CheckRollReports();
        public ContractCashWorkRegister()
        {
            InitializeComponent();
        }

        private void ContractCashWorkRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbType.Enabled = false;
            }
            else
            {
                cmbType.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String EmpType = "All";
            if (chkAll.Checked)
            {
                cmbType.Enabled = false;
                EmpType = "All";
            }
            else
            {
                cmbType.Enabled = true;
                EmpType = cmbType.Text;
            }

            ds = myreport.getContractCWRegister((cmbDivision.SelectedValue.ToString()), (dtDate.Value.Date),2,EmpType);
            ds.WriteXml("ContractCWRegister.xml");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ContractCWRegister myDailyRep = new ContractCWRegister();
                myDailyRep.SetDataSource(ds);
                ReportViewer myReportViewer = new ReportViewer();

                myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myDailyRep.SetParameterValue("Date", "Date : " + dtDate.Value.Date.ToShortDateString());
                myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.SelectedValue.ToString());
                myDailyRep.SetParameterValue("CashWork", "Contract Cash Work - "+cmbType.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }
    }
}