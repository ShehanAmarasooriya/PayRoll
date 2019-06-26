using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DivisionWiseNorm : Form
    {
        FTSPayRollBL.YearMonth YMonths = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();

        public DivisionWiseNorm()
        {
            InitializeComponent();
        }

        private void DivisionWiseNorm_Load(object sender, EventArgs e)
        {
            //cmbDivision.DataSource = EstDiv.ListEstateDivisions(FTSPayRollBL.User.StrDivision);
            cmbDivision.DataSource = EstDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            gvList.DataSource = DivNorm.ListDivisionwiseNorm();
        }

        

       

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtMPlkNorm.Focus();
            }
        }

        private void txtNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DivNorm.DtNormDate = dateTimePicker1.Value.Date;
                DivNorm.StrDivisionId = cmbDivision.Text;
                DivNorm.IntYear = Convert.ToInt32(DivNorm.DtNormDate.Year);
                DivNorm.IntMonthId = Convert.ToInt32(DivNorm.DtNormDate.Month);
                DivNorm.IntMalePlkNorm = Convert.ToInt32(txtMPlkNorm.Text);
                DivNorm.IntMaleSunNorm = 0;
                DivNorm.IntFemalePlkNorm = Convert.ToInt32(txtFemalePlkNorm.Text);
                DivNorm.IntFemaleSunNorm = 0;
                DivNorm.InsertDivisionNorm();
                MessageBox.Show("Division Norm Added Successfully!");
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, "+ex.Message);
            }
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRef.Text = gvList.Rows[e.RowIndex].Cells[6].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[3].Value.ToString());
            cmbDivision.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMPlkNorm.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFemalePlkNorm.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DivNorm.DtNormDate = dateTimePicker1.Value.Date;
                DivNorm.IntDivisionNormId = Convert.ToInt32(lblRef.Text);
                DivNorm.StrDivisionId = cmbDivision.Text;
                DivNorm.IntYear = Convert.ToInt32(DivNorm.DtNormDate.Year);
                DivNorm.IntMonthId = Convert.ToInt32(DivNorm.DtNormDate.Month);
                DivNorm.IntMalePlkNorm = Convert.ToInt32(txtMPlkNorm.Text);
                DivNorm.IntFemalePlkNorm = Convert.ToInt32(txtFemalePlkNorm.Text);
                DivNorm.UpdateDivisionNorm();
                MessageBox.Show("Division Norm Updated Successfully!");
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DivNorm.IntDivisionNormId = Convert.ToInt32(lblRef.Text);
                DivNorm.StrDivisionId = cmbDivision.Text;
                DivNorm.IntYear = Convert.ToInt32(dateTimePicker1.Value.Date.Year);
                DivNorm.IntMonthId = Convert.ToInt32(dateTimePicker1.Value.Date.Month);
                DivNorm.DtNormDate = dateTimePicker1.Value.Date;
                DivNorm.DeleteDivisionNorm();
                MessageBox.Show("Division Norm Deleted Successfully!");
                cmdClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtMPlkNorm.Clear();
            txtFemalePlkNorm.Clear();
            dateTimePicker1.Focus();
            gvList.DataSource = DivNorm.ListDivisionwiseNorm();

            cmdAdd.Enabled = true;
            cmdClear.Enabled = true;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMPlkNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFemalePlkNorm.Focus();
            }
        }

        private void txtMaleSunNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFemalePlkNorm.Focus();
            }
        }
        
        private void txtFemaleSunNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void txtFemalePlkNorm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbDivision.Focus();
            }
        }

        private void btnFieldNorm_Click(object sender, EventArgs e)
        {
            FieldWiseNorm objFNorm = new FieldWiseNorm();
            objFNorm.Show();
        }



    }
}