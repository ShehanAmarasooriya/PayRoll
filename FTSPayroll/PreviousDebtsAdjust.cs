using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;
using System.Text.RegularExpressions;

namespace FTSPayroll
{
    public partial class PreviousDebtsAdjust : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.Debtors MyDebts = new FTSPayRollBL.Debtors();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.ProcessMonthlyWages ProcessWages = new ProcessMonthlyWages();
        public PreviousDebtsAdjust()
        {
            InitializeComponent();
        }

        private void PreviousDebtsAdjust_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            DataTable dt = myMonth.GetPreviousMonth();
            DateTime dtCurrentMonthDate = new DateTime(Convert.ToInt32(dt.Rows[0][0].ToString()), Convert.ToInt32(dt.Rows[0][1].ToString()), 1);
            DateTime dtPreviousMonthDate = dtCurrentMonthDate.AddMonths(-1);

            if (MyDebts.IsProcessedMonth(dtCurrentMonthDate.Year, dtCurrentMonthDate.Month))
            {
                MessageBox.Show("Already Processed Month, Access Denied.");
                this.Close();
            }
            cmbDivision_SelectedIndexChanged(null, null);
            try
            {               

                gvList.DataSource = MyDebts.ListDebtsToAdjust(cmbDivision.SelectedValue.ToString(), dtPreviousMonthDate.Year, dtPreviousMonthDate.Month);
                if (gvList.RowCount > 0)
                {
                    gvList.Columns[0].ReadOnly = true;
                    gvList.Columns[1].ReadOnly = true;
                    gvList.Columns[2].ReadOnly = true;
                    gvList.Columns[3].ReadOnly = true;
                    gvList.Columns[0].Width = 75;
                    gvList.Columns[1].Width = 75;
                    gvList.Columns[2].Width = 100;
                    gvList.Columns[3].Width = 100;
                    gvList.Columns[4].Width = 100;
                }
            }
            catch (Exception ex)
            {
                               
            }

        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = myMonth.GetPreviousMonth();
            DateTime dtCurrentMonthDate = new DateTime(Convert.ToInt32(dt.Rows[0][0].ToString()), Convert.ToInt32(dt.Rows[0][1].ToString()), 1);
            DateTime dtPreviousMonthDate = dtCurrentMonthDate.AddMonths(-1);

            gvList.DataSource = MyDebts.ListDebtsToAdjust(cmbDivision.SelectedValue.ToString(), dtPreviousMonthDate.Year, dtPreviousMonthDate.Month);
            if (gvList.RowCount > 0)
            {
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[3].ReadOnly = true;
                gvList.Columns[0].Width = 75;
                gvList.Columns[1].Width = 75;
                gvList.Columns[2].Width = 100;
                gvList.Columns[3].Width = 100;
                gvList.Columns[4].Width = 100;
            }
            refreshGridData();
        }

        public void refreshGridData()
        {
            DataTable dt = myMonth.GetPreviousMonth();
            DateTime dtCurrentMonthDate = new DateTime(Convert.ToInt32(dt.Rows[0][0].ToString()), Convert.ToInt32(dt.Rows[0][1].ToString()), 1);
            DateTime dtPreviousMonthDate = dtCurrentMonthDate.AddMonths(-1);

            gvList.DataSource = MyDebts.ListDebtsToAdjust(cmbDivision.SelectedValue.ToString(), dtPreviousMonthDate.Year, dtPreviousMonthDate.Month);
            if (gvList.RowCount > 0)
            {
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[3].ReadOnly = true;
                gvList.Columns[0].Width = 75;
                gvList.Columns[1].Width = 75;
                gvList.Columns[2].Width = 100;
                gvList.Columns[3].Width = 100;
                gvList.Columns[4].Width = 100;
            }
            lblTotalAmt.Text = MyDebts.GetTotal(cmbDivision.SelectedValue.ToString(), dtPreviousMonthDate.Year, dtPreviousMonthDate.Month).ToString();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataTable dt = myMonth.GetPreviousMonth();
            DateTime dtCurrentMonthDate = new DateTime(Convert.ToInt32(dt.Rows[0][0].ToString()), Convert.ToInt32(dt.Rows[0][1].ToString()), 1);
            DateTime dtPreviousMonthDate = dtCurrentMonthDate.AddMonths(-1);

            if (ProcessWages.IsProcessed(cmbDivision.SelectedValue.ToString(), dtCurrentMonthDate.Year, dtCurrentMonthDate.Month))
            {
                MessageBox.Show("Cannot Change Debts Amounts, Already Processed the Checkroll!","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                    {
                        if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[4].Value.ToString()))
                        {
                            MyDebts.UpdateDebtsAdjustGrid(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), Convert.ToInt32(gvList.Rows[i].Cells[0].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[1].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()));
                        }

                    }
                    MessageBox.Show("Saved Successfully.....!");

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Data Failed...., " + ex.Message.ToString());
                }
            }
            refreshGridData();
        }


        public void refreshGrid()
        {
            try
            {
               // gvList.DataSource = MyDebts.ListDebtsAdject(cmbDivision.SelectedValue.ToString());
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[3].ReadOnly = true;
                gvList.Columns[0].Width = 75;
                gvList.Columns[1].Width = 75;
                gvList.Columns[2].Width = 100;
                gvList.Columns[3].Width = 100;
                gvList.Columns[4].Width = 150;              
            }
            catch
            {
            }
        }

        private void gvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddDebit_Click(object sender, EventArgs e)
        {
            DataTable dt=myMonth.GetPreviousMonth();
            DateTime dtThisMonthDate=new DateTime (Convert.ToInt32(dt.Rows[0][0].ToString()),Convert.ToInt32(dt.Rows[0][1].ToString()),1);
            DateTime dtPreMonthDate=dtThisMonthDate.AddMonths(-1);
            String status = "";
            try
            {
                status=MyDebts.InsertNewDebitAmount(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text.PadLeft(5, '0'), dtPreMonthDate.Year, dtPreMonthDate.Month, Convert.ToDecimal(txtAmount.Text));
                if (status.ToUpper().Equals("OK"))
                {
                    MessageBox.Show("Added Successfully");
                }
                else
                {
                    MessageBox.Show("Already Exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, "+ex.Message);
            }
            refreshGridData();
        }
    }
}