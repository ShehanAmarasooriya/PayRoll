using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class HarvestRegister : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();

        public HarvestRegister()
        {
            InitializeComponent();
        }

        private void HarvestRegister_Load(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Under construction");
            //DataSet dataSetReport = new DataSet();

            //if (chkAll.Checked)
            //{
            //    dataSetReport = myReports.getHarvestRegisterAllDivision((Convert.ToInt32(cmbYear.Text)), (Convert.ToInt32(cmbMonth.SelectedValue.ToString())));

            //    if (dataSetReport.Tables[0].Rows.Count > 0)
            //    {

            //        dataSetReport.WriteXml("DailyHarvestRegister.xml");

            //        DailyHarvestRegister DHarvestReg = new DailyHarvestRegister();
            //        DHarvestReg.SetDataSource(dataSetReport);
            //        ReportViewer myReportViewer = new ReportViewer();
            //        //HarvestRegisterAllDivision myHRAD = new HarvestRegisterAllDivision();
            //        //myHRAD.SetDataSource(dataSetReport);                   
            //        //ReportViewer myReportViewer = new ReportViewer();

            //        DHarvestReg.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //        DHarvestReg.SetParameterValue("Period", cmbMonth.Text + " of " + cmbYear.Text);
            //        myReportViewer.crystalReportViewer1.ReportSource = DHarvestReg;
            //        myReportViewer.Show();
                    
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data to Preview");
            //    }
            //}
            //else
            //{
            //    //dataSetReport = myReports.getHarvestRegister((cmbDivision.SelectedValue.ToString()), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));


            //    dataSetReport = myReports.getHarvestRegisterDivision(cmbDivision.SelectedValue.ToString(),(Convert.ToInt32(cmbYear.Text)), (Convert.ToInt32(cmbMonth.SelectedValue.ToString())));

            //    if (dataSetReport.Tables[0].Rows.Count > 0)
            //    {

            //        dataSetReport.WriteXml("DailyHarvestRegister.xml");

            //        DailyHarvestRegister DHarvestReg = new DailyHarvestRegister();
            //        DHarvestReg.SetDataSource(dataSetReport);
            //        ReportViewer myReportViewer = new ReportViewer();
            //        //HarvestRegisterAllDivision myHRAD = new HarvestRegisterAllDivision();
            //        //myHRAD.SetDataSource(dataSetReport);                   
            //        //ReportViewer myReportViewer = new ReportViewer();

            //        DHarvestReg.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //        DHarvestReg.SetParameterValue("Period", cmbMonth.Text + " of " + cmbYear.Text);
            //        myReportViewer.crystalReportViewer1.ReportSource = DHarvestReg;
            //        myReportViewer.Show();

            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data to Preview");
            //    }

            //}
            
           
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void putvalues(DataSet dataSetReport)
        {
            DailyHarvestRegister DHarvestReg = new DailyHarvestRegister();
            DHarvestReg.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            DHarvestReg.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            DHarvestReg.SetParameterValue("Period", cmbMonth.Text + " of " + cmbYear.Text);
            myReportViewer.crystalReportViewer1.ReportSource = DHarvestReg;
            myReportViewer.Show();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
            {
                cmbDivision.Enabled = true;
            }
        }
    }
}