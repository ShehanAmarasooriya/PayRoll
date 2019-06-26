using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class NormSummary : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();

        public NormSummary()
        {
            InitializeComponent();
        }

        private void NormSummary_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
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
                if (chkAll.Checked)
                    dataSetReport = myReports.getNormSummary(dtpFrom.Value.Date, dtpTo.Value.Date,"%"); 
                else
                    dataSetReport = myReports.getNormSummary(dtpFrom.Value.Date,dtpTo.Value.Date,cmbDivision.SelectedValue.ToString()); 

                dataSetReport.WriteXml("NormSummary.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    NormSummaryRPT myaclist = new NormSummaryRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Period", "From : "+dtpFrom.Value.Date+" To : "+dtpTo.Value.Date);
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                    MessageBox.Show("No Data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void btnBNormList_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSetReport = new DataSet();
                if (chkAll.Checked)
                    dataSetReport = myReports.getBelowNormList(dtpFrom.Value.Date, dtpTo.Value.Date, "%");
                else
                    dataSetReport = myReports.getBelowNormList(dtpFrom.Value.Date, dtpTo.Value.Date, cmbDivision.SelectedValue.ToString());

                dataSetReport.WriteXml("BelowNormList.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    BelowNormListRPT myaclist = new BelowNormListRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                    myaclist.SetParameterValue("Period", "From : " + dtpFrom.Value.Date + " To : " + dtpTo.Value.Date);
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                    MessageBox.Show("No Data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}