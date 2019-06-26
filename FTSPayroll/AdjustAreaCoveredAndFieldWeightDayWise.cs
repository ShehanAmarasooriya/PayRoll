using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AdjustAreaCoveredAndFieldWeightDayWise : Form
    {
        FTSPayRollBL.YearMonth myYearMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Reports myReports = new FTSPayRollBL.Reports();
        FTSPayRollBL.EstateDivisionBlock myDivi = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        AdjustAreaCoveredFieldWeight AreaCoveredObj;
        String strSelectedDivision = "";
        String strSelectedField = "";
        DateTime dtSelectedDate;

        public AdjustAreaCoveredAndFieldWeightDayWise(AdjustAreaCoveredFieldWeight objForm,DateTime dtDate,String strDiv,String strField)
        {
            AreaCoveredObj = objForm;
            dtSelectedDate = dtDate;
            strSelectedDivision = strDiv;
            strSelectedField = strField;
            InitializeComponent();
            
        }
        public AdjustAreaCoveredAndFieldWeightDayWise()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdjustAreaCoveredAndFieldWeightDayWise_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivi.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            dateTimePicker1.Value = dtSelectedDate;
            cmbDivision.SelectedValue = strSelectedDivision;

            try
            {
                gvList.DataSource = DHarvest.ListAreaCoveredAndFieldWeight1(strSelectedDivision, strSelectedField, dtSelectedDate);
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[3].ReadOnly = true;
                gvList.Columns[4].ReadOnly = true;
                gvList.Columns[5].ReadOnly = true;
                gvList.Columns[6].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[7].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            catch { }
        }

        private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
            {
                try
                {
                    DHarvest.UpdateAreaCoveredAndFieldWeight(Convert.ToInt32(gvList.Rows[i].Cells[0].Value.ToString()), Convert.ToDateTime(gvList.Rows[i].Cells[1].Value.ToString()), cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), gvList.Rows[i].Cells[5].Value.ToString(), Convert.ToInt32(gvList.Rows[i].Cells[2].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[6].Value.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error");
                }
            }
            AreaCoveredObj.gvList.DataSource = DHarvest.ListAreaCoveredAndFieldWeight(strSelectedDivision, dtSelectedDate);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}