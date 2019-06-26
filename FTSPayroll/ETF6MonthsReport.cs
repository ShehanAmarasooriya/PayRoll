using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class ETF6MonthsReport : Form
    {
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock myEst = new FTSPayRollBL.EstateDivisionBlock();

        public ETF6MonthsReport()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSetReport = new DataSet();

                dataSetReport = myReports.get6MonthETF(Convert.ToInt32(cmbYear.Text));
                dataSetReport.WriteXml("etf6months.xml");
                Rpt_ETF_6_monthly_report myaclist = new Rpt_ETF_6_monthly_report();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myaclist.SetParameterValue("Estate", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Period", cmbMonth.Text + "/" + cmbYear.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            catch (Exception ex)
            { }
        }

        private void ETF6MonthsReport_Load(object sender, EventArgs e)
        {
            cmbYear.Text = DateTime.Now.Year.ToString();

            cmbDivision.DataSource = myEst.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(cmbMonth.Text))
                {
                    MessageBox.Show("Select A Time Period.");
                }
                else
                {
                    Int32 intChkAll = 0;
                    Int32 intPeriod = 1;
                    String StrAllDiv = "%";
                    if (cmbMonth.Text.Equals("Jan-Jun"))
                    {
                        intPeriod = 1;
                    }
                    else
                    {
                        intPeriod = 2;
                    }
                    if (chkAll.Checked)
                    {
                        intChkAll = 1;
                    }
                    if (this.chkAllDiv.Checked)
                    {
                        StrAllDiv = "%";
                    }
                    else
                    {
                        StrAllDiv = cmbDivision.SelectedValue.ToString();
                    }

                    DataSet dataSetReport = new DataSet();
                    dataSetReport = myReports.getEPF6Month(Convert.ToInt32(cmbYear.Text), intPeriod, intChkAll,StrAllDiv);
                    dataSetReport.WriteXml("ePf6MonthReport.xml");
                    if (intChkAll == 1)
                    {
                        EPF6MonthReportAll myEPFRep = new EPF6MonthReportAll();
                        myEPFRep.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();
                        myEPFRep.SetParameterValue("Estate", FTSPayRollBL.Company.getCompanyName());
                        myEPFRep.SetParameterValue("Period", "January/2012" + " To " + "June/2012");
                        myReportViewer.crystalReportViewer1.ReportSource = myEPFRep;
                        myReportViewer.Show();
                    }
                    else
                    {
                        EPF6MonthReport myEPFRep = new EPF6MonthReport();
                        myEPFRep.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();
                        myEPFRep.SetParameterValue("Estate", FTSPayRollBL.Company.getCompanyName());
                        myEPFRep.SetParameterValue("Period", "January/2012" + " To " + "June/2012");
                        myReportViewer.crystalReportViewer1.ReportSource = myEPFRep;
                        myReportViewer.Show();
                    }
                    
                }
            }
            catch (Exception ex)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(cmbMonth.Text))
                {
                    MessageBox.Show("Select A Time Period.");
                }
                else
                {
                    Int32 intChkAll = 0;
                    Int32 intPeriod = 1;
                    String StrAllDiv = "%";
                    if (cmbMonth.Text.Equals("Jan-Jun"))
                    {
                        intPeriod = 1;
                    }
                    else
                    {
                        intPeriod = 2;
                    }

                    if (chkAll.Checked)
                    {
                        intChkAll = 1;
                    }
                    if (chkAllDiv.Checked)
                    {
                        StrAllDiv = "%";                        
                    }
                    else
                    {
                        StrAllDiv = cmbDivision.SelectedValue.ToString();
                    }

                    DataSet dataSetReport = new DataSet();

                    dataSetReport = myReports.getETF6Month(Convert.ToInt32(cmbYear.Text), intPeriod, intChkAll, StrAllDiv);
                    dataSetReport.WriteXml("ETF6MonthReport.xml");
                    if (intChkAll == 1)
                    {
                        ETF6MonthReportAll myEPFRep = new ETF6MonthReportAll();
                        myEPFRep.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();
                        myEPFRep.SetParameterValue("Estate", "Employer's Registration No : 269/S ");
                        myEPFRep.SetParameterValue("Period", "Return for the period January to June  2012");
                        myReportViewer.crystalReportViewer1.ReportSource = myEPFRep;
                        myReportViewer.Show();
                    }
                    else
                    {
                        ETF6MonthReport myEPFRep = new ETF6MonthReport();
                        myEPFRep.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();
                        myEPFRep.SetParameterValue("Estate", "Employer's Registration No : 269/S ");
                        myEPFRep.SetParameterValue("Period", "Return for the period January to June  2012");
                        myReportViewer.crystalReportViewer1.ReportSource = myEPFRep;
                        myReportViewer.Show();
                    }
                    
                }
            }
            catch (Exception ex)
            { }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            EPFETF6MonthReport myEpf = new EPFETF6MonthReport();
            myEpf.Show();
        }
    }
}