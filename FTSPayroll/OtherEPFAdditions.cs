using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OtherEPFAdditions : Form
    {
        FTSPayRollBL.EstateDivisionBlock myDivision = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster myEmployee = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Additions myAdd = new FTSPayRollBL.Additions();
        FTSPayRollBL.YearMonth myYM = new FTSPayRollBL.YearMonth();

        public OtherEPFAdditions()
        {
            InitializeComponent();
        }

        private void OtherEPFAdditions_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myYM.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYM.getLastYearID();

            cmbDivision.DataSource = myDivision.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";


            

        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvList.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < gvList.Rows.Count - 1; i++)
                    {

                        if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[4].Value.ToString()) && Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()) >= 0)
                        {
                            myAdd.UpdateOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), Convert.ToInt32(gvList.Rows[i].Cells[1].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[2].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()));
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                if (gvList2.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < gvList2.Rows.Count - 1; i++)
                    {

                        if (!String.IsNullOrEmpty(gvList2.Rows[i].Cells[4].Value.ToString()) && Convert.ToDecimal(gvList2.Rows[i].Cells[4].Value.ToString()) > 0)
                        {
                            //myAdd.UpdateOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), Convert.ToInt32(gvList.Rows[i].Cells[1].Value.ToString()), Convert.ToInt32(gvList.Rows[i].Cells[2].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()));
                            myAdd.InsertInactiveOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), gvList2.Rows[i].Cells[3].Value.ToString(), Convert.ToInt32(gvList2.Rows[i].Cells[1].Value.ToString()), Convert.ToInt32(gvList2.Rows[i].Cells[2].Value.ToString()), Convert.ToDecimal(gvList2.Rows[i].Cells[4].Value.ToString()));

                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                refreshGrids();
                MessageBox.Show("EPF Other Additions Saved Successfully!");
                try
                {
                    gvList.DataSource = myAdd.ListEmployeeOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    refreshGrids();
                    DisableColumns();
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error,\n" + ex.Message);
            }            
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource=myAdd.ListEmployeeOtherEPFAdditions(cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbYear.Text),Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                gvList2.DataSource=myAdd.ListInactiveEmployeeOtherEPFAdditions(cmbDivision.SelectedValue.ToString(),Convert.ToInt32(cmbYear.Text),Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                DisableColumns();
                lblDivisionTotal.Text = "Total EPF Other Addition :" + myAdd.GetEPFOtherAdditionTotal(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void refreshGrids()
        {
            try
            {
                gvList.DataSource = myAdd.ListEmployeeOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                gvList2.DataSource = myAdd.ListInactiveEmployeeOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                DisableColumns();
                lblDivisionTotal.Text = "Total EPF Other Addition :"+myAdd.GetEPFOtherAdditionTotal(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrids();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = myYM.ListMonths(Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";
                cmbMonth.SelectedValue = myYM.getLastMonthID();
            }
            catch { }

            refreshGrids();
        }

        private void DisableColumns()
        {
            gvList.Columns[0].ReadOnly = true;
            gvList.Columns[1].ReadOnly = true;
            gvList.Columns[2].ReadOnly = true;
            gvList.Columns[3].ReadOnly = true;

            gvList2.Columns[0].ReadOnly = true;
            gvList2.Columns[1].ReadOnly = true;
            gvList2.Columns[2].ReadOnly = true;
            gvList2.Columns[3].ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean boolIsThisYear = false;
                if (chkThisYear.Checked)
                    boolIsThisYear = true;
                else
                    boolIsThisYear = false;

                myAdd.AddLastYearHolidayPayEPFPayable(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), boolIsThisYear);
                refreshGrids();
                MessageBox.Show("Last Year Holiday Pay EPF Additions Saved Successfully!");
                try
                {
                    gvList.DataSource = myAdd.ListEmployeeOtherEPFAdditions(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    refreshGrids();
                    DisableColumns();
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error,\n" + ex.Message);
            }          
        }
    }
}