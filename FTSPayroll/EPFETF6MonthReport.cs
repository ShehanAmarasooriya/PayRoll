using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EPFETF6MonthReport : Form
    {
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock myEst = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myYM = new FTSPayRollBL.YearMonth();

        public EPFETF6MonthReport()
        {
            InitializeComponent();
        }

        private void EPFETF6MonthReport_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myYM.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYM.getLastYearID();

            cmbYear.Text = DateTime.Now.Year.ToString();

            cmbDivision.DataSource = myEst.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void chkAllDiv_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnETF_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(cmbMonth.Text))
                {
                    MessageBox.Show("Select A Time Period.");
                }
                else
                {
                    Int32 intChkAll = 1;
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
                        StrAllDiv = "%";
                    }
                    else
                    {
                        StrAllDiv = cmbDivision.SelectedValue.ToString();
                    }

                    DataSet dataSetReport = new DataSet();

                    dataSetReport = myReports.getETF6MonthReportData(Convert.ToInt32(cmbYear.Text), intPeriod, intChkAll, StrAllDiv);
                    dataSetReport.WriteXml("EPFETF6MonthReportData.xml");
                  
                        EPFETFSixMonthsRPT myEPFRep = new EPFETFSixMonthsRPT();
                        myEPFRep.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();
                        myEPFRep.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString() + " Estate ");
                        if (intPeriod == 1)
                        {
                            myEPFRep.SetParameterValue("Period", "Returns For The Period January  To June " + cmbYear.Text);
                        }
                        else
                        {
                            myEPFRep.SetParameterValue("Period", "Returns For The Period July  To December " + cmbYear.Text);
                        }
                        myEPFRep.SetParameterValue("Title", "The Employee's Trust Fund ");
                        myReportViewer.crystalReportViewer1.ReportSource = myEPFRep;
                        myReportViewer.Show();
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnEpf_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(cmbMonth.Text))
                {
                    MessageBox.Show("Select A Time Period.");
                }
                else
                {
                    Int32 intChkAll = 1;
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
                        StrAllDiv = "%";
                    }
                    else
                    {
                        StrAllDiv = cmbDivision.SelectedValue.ToString();
                    }

                    DataSet dataSetReport = new DataSet();

                    dataSetReport = myReports.getEPF6MonthReportData(Convert.ToInt32(cmbYear.Text), intPeriod, intChkAll, StrAllDiv);
                    dataSetReport.WriteXml("EPFETF6MonthReportData.xml");

                    EPFETFSixMonthsRPT myEPFRep = new EPFETFSixMonthsRPT();
                    myEPFRep.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();
                    myEPFRep.SetParameterValue("Estate", myEst.ListEstates().Rows[0][0].ToString() + " Estate ");
                    if (intPeriod == 1)
                    {
                        myEPFRep.SetParameterValue("Period", "Returns For The Period January  To June " + cmbYear.Text);
                    }
                    else
                    {
                        myEPFRep.SetParameterValue("Period", "Returns For The Period July  To December " + cmbYear.Text);
                    }
                    myEPFRep.SetParameterValue("Title", "The Employee's Provident Fund ACT No. 150F 1958");
                    myReportViewer.crystalReportViewer1.ReportSource = myEPFRep;
                    myReportViewer.Show();
                }
            }
            catch (Exception ex)
            { }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
    }
}