using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CheckRollReconiliation : Form
    {
        public CheckRollReconiliation()
        {
            InitializeComponent();
        }

        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.ChkReconciliation myRe = new FTSPayRollBL.ChkReconciliation();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();

        private void CheckRollReconiliation_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.DisplayMember = "Month";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                String strMonth = cmbMonth.Text;

                for (int month = 1; month <= 12; month++)
                {
                    if (strMonth == myMonth.ListMonths().Rows[month - 1][1].ToString())
                    {
                        DataTable dt = new DataTable();
                        dt = myRe.GetReconciliation(cmbYear.Text, month);                        

                        if (dt.Rows.Count > 0)
                        {
                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);
                            ds.WriteXml("ChkReconciliation.xml");

                            CheckRollReconcilationRPT1 myaclist = new CheckRollReconcilationRPT1();
                            myaclist.SetDataSource(ds);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", "Estate :" + myDiv.ListEstates().Rows[0][0].ToString());
                            myaclist.SetParameterValue("Period", "Month of " + cmbMonth.Text + "/" + cmbYear.Text);
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No Data to Preview..!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!" + ex);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}