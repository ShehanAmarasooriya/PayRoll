using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CashKiloRegister : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        public CashKiloRegister()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            String strDivision;
            String strWorkType;
            try
            {
                if (rbNormal.Checked)
                {
                    strWorkType = "Normal Work";
                }
                else
                {
                    strWorkType = "Cash Work";
                }
                if(chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }                

                DataSet dataSetReport = new DataSet();
                if (rbNormal.Checked)
                    dataSetReport = myReports.getPluckingKilos(dtpFrom.Value.Date, dtpTo.Value.Date, strDivision, 1);
                else
                    dataSetReport = myReports.getPluckingKilosCashWork(dtpFrom.Value.Date, dtpTo.Value.Date, strDivision, 2);

                dataSetReport.WriteXml("PlkKilosReg.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    PluckingKiloRegisterRPT RepObj = new PluckingKiloRegisterRPT();
                    RepObj.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    RepObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    if (chkAll.Checked)
                    {
                        RepObj.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " - All Divisions");
                    }
                    else
                    {
                        RepObj.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " - " + cmbDivision.SelectedValue.ToString());
                    }
                    RepObj.SetParameterValue("Period", "From : " + dtpFrom.Value.Date + " To : " + dtpTo.Value.Date);
                    RepObj.SetParameterValue("WorkType", "Work Type : " + strWorkType);
                    myReportViewer.crystalReportViewer1.ReportSource = RepObj;
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

        private void CashKiloRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }
    }
}