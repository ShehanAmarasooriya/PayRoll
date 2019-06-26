using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class UpdateHolidayPayData : Form
    {
        FTSPayRollBL.Debtors MyDebts = new FTSPayRollBL.Debtors();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.HolidayPay myHolidaypay = new FTSPayRollBL.HolidayPay();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        public UpdateHolidayPayData()
        {
            InitializeComponent();
        }

        private void UpdateHolidayPayData_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = myMonth.ListJRLMonths(Convert.ToInt32(this.cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MID";
            }
            catch { }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            refreshGrid();
        }

        public void refreshGrid()
        {
            try
            {
                gvList.DataSource = myHolidaypay.ListDailyHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[0].Width = 75;
                gvList.Columns[1].Width = 75;
                gvList.Columns[2].Width = 100;
                gvList.Columns[3].Width = 100;
                gvList.Columns[4].Width = 100;
                gvList.Columns[5].Width = 100;
                gvList.Columns[6].Width = 100;
                gvList.Columns[7].Width = 100;
            }
            catch
            {
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
           
                try
                {
                    for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                    {
                        if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[4].Value.ToString()))
                        {
                            myHolidaypay.UpdateDailyHolidayPayData(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), Convert.ToInt32(gvList.Rows[i].Cells[0].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[1].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[3].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[5].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[6].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[7].Value.ToString()));
                        }
                    }
                    MessageBox.Show("Saved Successfully.....!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Data Failed...., " + ex.Message.ToString());
                }

                refreshGrid();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}