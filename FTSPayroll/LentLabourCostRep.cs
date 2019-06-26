using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class LentLabourCostRep : Form
    {
        FTSPayRollBL.YearMonth myYear = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CHKWages myWage = new FTSPayRollBL.CHKWages();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        


        public LentLabourCostRep()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LentLabourCostRep_Load(object sender, EventArgs e)
        {
            cmbDivisionID.DataSource = myDiv.ListEstateDivisions();
            cmbDivisionID.DisplayMember = "DivisionID";
            cmbDivisionID.ValueMember = "DivisionID";

            cmbMonth.DataSource = myYear.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbYear.DataSource = myYear.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
        }

        private void btnDispaly_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            String strAllDiv = "%";

            if (chkAll.Checked)
            {
                strAllDiv = "%";
            }
            else
            {
                strAllDiv = cmbDivisionID.SelectedValue.ToString();
            }

            dt = myWage.GetLentLabourData(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),strAllDiv);
            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("LentLabourDetail.xml");

                LentLabourCostRep1 myLent = new LentLabourCostRep1();
                myLent.SetDataSource(ds);

                ReportViewer myView = new ReportViewer();
                myLent.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                myLent.SetParameterValue("Date", "Checkroll Wages Report for the Month of " + cmbMonth.Text + " " + cmbYear.Text);
                myLent.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());

                myView.crystalReportViewer1.ReportSource = myLent;
                myView.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            String strAllDiv = "%";

            if (chkAll.Checked)
            {
                strAllDiv = "%";
            }
            else
            {
                strAllDiv = cmbDivisionID.SelectedValue.ToString();
            }

            dt = myWage.GetLentLabourDataDayWise(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), strAllDiv);
            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("LentLabourDetailDayWise.xml");

                LentLabourCostDayWiseRPT myLent = new LentLabourCostDayWiseRPT();
                myLent.SetDataSource(ds);

                ReportViewer myView = new ReportViewer();
                myLent.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                myLent.SetParameterValue("Date", "Checkroll Wages Report for the Month of " + cmbMonth.Text + " " + cmbYear.Text);
                myLent.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());

                myView.crystalReportViewer1.ReportSource = myLent;
                myView.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }
        }
    }
}