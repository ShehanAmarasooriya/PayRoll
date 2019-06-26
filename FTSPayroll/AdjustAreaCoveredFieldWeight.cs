using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AdjustAreaCoveredFieldWeight : Form
    {
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Reports myReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.EstateDivisionBlock myDivi = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();

        public AdjustAreaCoveredFieldWeight()
        {
            InitializeComponent();
        }

        private void AdjustAreaCoveredFieldWeight_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivi.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";


        }

       

        

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = DHarvest.ListAreaCoveredAndFieldWeight(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()));
            }
            catch { }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            gvList.DataSource = DHarvest.ListAreaCoveredAndFieldWeight(cmbDivision.SelectedValue.ToString(),Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()));
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DateTime dtnew = Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString());
                AdjustAreaCoveredAndFieldWeightDayWise myForm = new AdjustAreaCoveredAndFieldWeightDayWise(this, Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString()), cmbDivision.SelectedValue.ToString(), gvList.Rows[e.RowIndex].Cells[1].Value.ToString());
                myForm.Show();
            }
            catch { }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            gvList.DataSource = DHarvest.ListAreaCoveredAndFieldWeight(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()));
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                try
                {
                    DHarvest.UpdateAreaCoveredAndFieldWeight(Convert.ToInt32(gvList.Rows[i].Cells[1].Value.ToString()), Convert.ToDateTime(gvList.Rows[i].Cells[0].Value.ToString()), cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[5].Value.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), Convert.ToInt32(gvList.Rows[i].Cells[4].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[6].Value.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error On Update Area Covered, " + gvList.Rows[i].Cells[1].Value.ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            String strDivision = "%";
            
            DataTable dsDivisionReport = new DataTable();
            //dsDivisionReport = myEmployeeDeduction.GetMonthPRINorms(cmbDivision.SelectedValue.ToString(), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1), new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1).AddMonths(1).AddDays(-1)).Tables[0];
            dsDivisionReport = DHarvest.GetSundryAreaCovered(dateTimePicker1.Value.Date, cmbDivision.SelectedValue.ToString()).Tables[0];
            if (dsDivisionReport.Rows.Count > 0)
            {
                dsDivisionReport.WriteXml("SundryAreaCovered.xml");

                SundryAreaCoveredRPT objReport = new SundryAreaCoveredRPT();
                objReport.SetDataSource(dsDivisionReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", myDivi.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Division", cmbDivision.SelectedValue.ToString());
                objReport.SetParameterValue("Period", dateTimePicker1.Value.Date.ToShortDateString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview.");
            }
        }
    }
}