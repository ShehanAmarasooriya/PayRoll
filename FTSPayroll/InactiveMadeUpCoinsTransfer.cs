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
    public partial class InactiveMadeUpCoinsTransfer : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.MadeUpCoins myCoins = new MadeUpCoins();
        public Int32 intRowIndex = -1;
        public String strDiv;

        public InactiveMadeUpCoinsTransfer()
        {
            InitializeComponent();
        }

        
        private void InactiveMadeUpCoinsTransfer_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbYear_SelectedIndexChanged(null, null);

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null,null);


        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = myCoins.ListInactiveEmpCoins(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;

            strDiv = cmbDivision.SelectedValue.ToString();
        }

        public void refreshGrid()
        {
            try
            {
                dataGridView1.DataSource = myCoins.ListInactiveEmpCoins(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;

                strDiv = cmbDivision.SelectedValue.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                EmployeeSearch empSearch = new EmployeeSearch(this,e.RowIndex,strDiv);
                empSearch.Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                for (Int32 i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    count = i;
                    if (count == 151)
                    {
                        MessageBox.Show("here");
                    }
                    if (Convert.ToBoolean(dataGridView1[4, i].Value.ToString()) == false)
                    {
                        if (string.IsNullOrEmpty(dataGridView1[2, i].Value.ToString()))
                        {
                            myCoins.UpdateUnpaidCoinsTransfer(cmbDivision.SelectedValue.ToString(), dataGridView1[0, i].Value.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), "NA", "NA");
                        }
                        else if (dataGridView1[2, i].Value.ToString().Equals("NA"))
                        {
                            myCoins.UpdateUnpaidCoinsTransfer(cmbDivision.SelectedValue.ToString(), dataGridView1[0, i].Value.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), "NA", "NA");
                        }
                        else
                        {
                            myCoins.UpdateUnpaidCoinsTransfer(cmbDivision.SelectedValue.ToString(), dataGridView1[0, i].Value.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), dataGridView1[2, i].Value.ToString(), dataGridView1[3, i].Value.ToString());
                        }
                    }
                }

                MessageBox.Show("Transfer To Employee Details Saved Successfully...!");
                refreshGrid();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+count.ToString());
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();
            refreshGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    for (Int32 i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            //    {
            //        myCoins.UpdateEmployeeDetails(cmbDivision.SelectedValue.ToString(), dataGridView1[0, i].Value.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), dataGridView1[2, i].Value.ToString(), dataGridView1[3, i].Value.ToString());
            //    }
            //    MessageBox.Show("Employee Details Saved Successfully...!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrid();
        }
    }
}