using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OpeningCoinDebtBalance : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MadeUpCoins myCoins = new FTSPayRollBL.MadeUpCoins();
        public Int32 intRowIndex = -1;
        public String strDiv;

        public OpeningCoinDebtBalance()
        {
            InitializeComponent();
        }

        private void OpeningCoinDebtBalance_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);

            rbtnDebts.Checked = true;

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();
            refreshGrid();
        }

        public void refreshGrid()
        {
            try
            {
                if (rbtnDebts.Checked)
                {
                    dataGridView1.DataSource = myCoins.ListOpeningDebts(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = false; 
                }
                else
                {
                    dataGridView1.DataSource = myCoins.ListOpeningCoins(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = false;
                }

                strDiv = cmbDivision.SelectedValue.ToString();
            }
            catch (Exception ex){ }
            txtSubTotal.Text = getGridTotal().ToString("N2");
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrid();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                for (Int32 i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    
                        if (rbtnDebts.Checked)
                        {
                            if (myCoins.IsEntryAvailable(dataGridView1[0, i].Value.ToString(), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), "Debt"))
                            {
                                myCoins.UpdateOpeningDebts(dataGridView1[0, i].Value.ToString(), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToDecimal(dataGridView1[3, i].Value.ToString()));
                            }
                            else
                            {
                                myCoins.InsertOpeningDebts(dataGridView1[0, i].Value.ToString(), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToDecimal(dataGridView1[3, i].Value.ToString()));
                            }
                        }
                        else
                        {
                            if (myCoins.IsEntryAvailable(dataGridView1[0, i].Value.ToString(), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), "Coins"))
                            {
                                myCoins.UpdateOpeningCoins(dataGridView1[0, i].Value.ToString(), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToDecimal(dataGridView1[3, i].Value.ToString()));
                            }
                            else
                            {
                                myCoins.InsertOpeningCoins(dataGridView1[0, i].Value.ToString(), cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToDecimal(dataGridView1[3, i].Value.ToString()));
                            }
                        }
                    
                }

                MessageBox.Show("Opening Debts Saved Successfully...!");
                refreshGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + count.ToString());
            }
        }

        private Decimal getGridTotal()
        {
            Decimal decTotal=0;
            try
            {
                for (Int32 i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    decTotal += Convert.ToDecimal(dataGridView1[3, i].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
                decTotal = 0;
            }
            return decTotal;
        }

        private void rbtnDebts_CheckedChanged(object sender, EventArgs e)
        {
            refreshGrid();
        }
    }
}