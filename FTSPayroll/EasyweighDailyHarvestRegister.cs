using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EasyweighDailyHarvestRegister : Form
    {
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.DownloadData objDownloadData = new FTSPayRollBL.DownloadData();

        public EasyweighDailyHarvestRegister()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = objDownloadData.getEasyWeighHarvestRegister(cmbDivision.SelectedValue.ToString(), dtDate.Value.Date);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ds.WriteXml("EasyWeighDailyHarvestRegister.xml");

                        EasyWeighDailyHarvestRegisterRPT objReport = new EasyWeighDailyHarvestRegisterRPT();
                        objReport.SetDataSource(ds);

                        objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                        objReport.SetParameterValue("Date", "Date : " + dtDate.Value.Date);
                        objReport.SetParameterValue("Division", "Division : " + cmbDivision.SelectedValue.ToString());

                        ReportViewer objReportViewer = new ReportViewer();
                        objReportViewer.crystalReportViewer1.ReportSource = objReport;
                        objReportViewer.Show();
                    }
                    else
                        MessageBox.Show("No data to preview.!");
                }
                else
                    MessageBox.Show("No data to preview.!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EasyweighDailyHarvestRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
        }
    }
}