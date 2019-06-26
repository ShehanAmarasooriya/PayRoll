using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class MonthlyWorkOfferedDays : Form
    {
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.OfferedDays myOffered = new FTSPayRollBL.OfferedDays();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        public MonthlyWorkOfferedDays()
        {
            InitializeComponent();
        }

        private void MonthlyWorkOfferedDays_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            try
            {
                gvList.DataSource = myOffered.ListAllOffered();
            }
            catch {}

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtFemaleOffered.Clear();
            txtMaleOffered.Clear();
            try
            {
                gvList.DataSource = myOffered.ListAllOffered();
            }
            catch { }

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaleOffered.Text))
            {
                MessageBox.Show("Male offered cannot be empty.");
                txtMaleOffered.Focus();
            }
            if (String.IsNullOrEmpty(txtFemaleOffered.Text))
            {
                MessageBox.Show("Female offered cannot be empty.");
                txtFemaleOffered.Focus();
            }
            else
            {
                myOffered.IntOfferedYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myOffered.IntOfferedMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myOffered.IntMaleOffered = Convert.ToInt32(txtMaleOffered.Text);
                myOffered.IntFemaleOffered = Convert.ToInt32(txtFemaleOffered.Text);
                myOffered.StrDivision = cmbDivision.SelectedValue.ToString();

                if (chkApplyAll.Checked)
                {
                    DataTable DivisionTbl;
                    DivisionTbl = EstDivBlock.ListEstateDivisions();
                    foreach (DataRow drow in DivisionTbl.Rows)
                    {
                        
                        myOffered.StrDivision = drow[0].ToString();
                        try
                        {
                            myOffered.InsertOfferedDays();
                            MessageBox.Show(myOffered.StrDivision+" Division Offered Days Added Successfully");
                            cmdClear.PerformClick();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, ", ex.Message);
                        }
                    }
                }
                else
                {
                    
                    myOffered.StrDivision = cmbDivision.SelectedValue.ToString();
                    try
                    {
                        myOffered.InsertOfferedDays();
                        MessageBox.Show("Added Successfully");
                        cmdClear.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, ", ex.Message);
                    }
                }
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
             if (String.IsNullOrEmpty(txtMaleOffered.Text))
            {
                MessageBox.Show("Male offered cannot be empty.");
                txtMaleOffered.Focus();
            }
            if (String.IsNullOrEmpty(txtFemaleOffered.Text))
            {
                MessageBox.Show("Female offered cannot be empty.");
                txtFemaleOffered.Focus();
            }
            else
            {
                myOffered.IntOfferedYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myOffered.IntOfferedMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myOffered.IntMaleOffered = Convert.ToInt32(txtMaleOffered.Text);
                myOffered.IntFemaleOffered = Convert.ToInt32(txtFemaleOffered.Text);
                myOffered.StrDivision = cmbDivision.SelectedValue.ToString();
                try
                {
                    if (!myOffered.GetMonthOfferedDayState(myOffered.IntOfferedYear, myOffered.IntOfferedMonth,myOffered.StrDivision))
                    {
                        myOffered.UpdateOfferedDays();
                        MessageBox.Show("Updated Successfully");
                        cmdClear.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Already Processed Month, This Value Cannot Change.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, ", ex.Message);
                }
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = myOffered.ListAllOffered();
            }
            catch { }
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbYear.SelectedValue = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbMonth.SelectedValue = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbDivision.SelectedValue = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMaleOffered.Text = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFemaleOffered.Text = gvList.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void txtMaleOffered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFemaleOffered.Focus();
            }
        }

        private void txtFemaleOffered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void cmbYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbMonth.Focus();
            }
        }

        private void cmbMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtMaleOffered.Focus();
            }
        }
    }
}