using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class DisplayData : Form
    {
        FTSPayRollBL.CheckErrors chkErrors = new CheckErrors();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        DataSet dsData;
        String strMessage="";
        DateTime dtFromDate;
        DateTime dtToDate;
        String strDivision = "";

        public DisplayData( DataSet ds,String strmsg,DateTime dtFrom,DateTime dtTo,String strDiv)
        {
            InitializeComponent();
            dsData = ds;
            dataGridView1.DataSource = ds;
            strMessage = strmsg;
            if (String.IsNullOrEmpty(strmsg))
            {
                label1.ForeColor = Color.Red;
            }
            label1.Text = strmsg;
            dtFromDate = dtFrom;
            dtToDate = dtTo;
            strDivision = strDiv;
        }

        private void DisplayData_Load(object sender, EventArgs e)
        {
            if (dsData.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = dsData.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("No Data To Preview.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                dataSetReport = dsData;
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("DisplaDataXml.xml");
                    DisplayDataRPT myDisplayData = new DisplayDataRPT();
                    myDisplayData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myDisplayData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString()+" / "+strDivision);
                    myDisplayData.SetParameterValue("YearMonth", dtFromDate.Year.ToString()+"/"+dtFromDate.Month.ToString());
                    myDisplayData.SetParameterValue("Message", strMessage);
                    myReportViewer.crystalReportViewer1.ReportSource = myDisplayData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }
    }
}