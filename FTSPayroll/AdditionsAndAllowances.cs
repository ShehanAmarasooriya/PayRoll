using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class AdditionsAndAllowances : Form
    {
        public AdditionsAndAllowances()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Additions myAdditions = new FTSPayRollBL.Additions();

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdditionsAndAllowances_Load(object sender, EventArgs e)
        {
            gvlist.DataSource = myAdditions.ListAllAdditions();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();
                cmbEmpNo.DataSource = null;
                cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(DivisionID);
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
                cmbEmpNo_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEmpNo.SelectedIndex != -1)
                {

                    if (cmbEmpNo.SelectedIndex != -1)
                    {
                        if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                        {
                            txtName.Text = EmpMaster.GetEmployeeNameByEmpNo( cmbEmpNo.SelectedValue.ToString(),cmbDivision.SelectedValue.ToString());
                        }
                    }
                   
                    
                        myAdditions.StrDivision = cmbDivision.SelectedValue.ToString();
                        myAdditions.StrEmpno = cmbEmpNo.SelectedValue.ToString();
                        myAdditions.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                        myAdditions.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                        txtAmount.Text = myAdditions.getAmount().ToString();
                    
                }
            }

            catch { }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                MessageBox.Show("There is no amount to proceed");
            }
            else
            {
                myAdditions.StrDivision = cmbDivision.SelectedValue.ToString();
                myAdditions.StrEmpno = cmbEmpNo.SelectedValue.ToString();
                myAdditions.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myAdditions.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myAdditions.DecAmount = Convert.ToDecimal(txtAmount.Text);
                myAdditions.UpdatePaidAdditionYesNoAsTrue();
                MessageBox.Show("Amount Updated successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                MessageBox.Show("There is no amount to proceed");
            }
            else
            {
                myAdditions.StrDivision = cmbDivision.SelectedValue.ToString();
                myAdditions.StrEmpno = cmbEmpNo.SelectedValue.ToString();
                myAdditions.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                myAdditions.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myAdditions.DecAmount = Convert.ToDecimal(txtAmount.Text);
                myAdditions.UpdatePaidAdditionYesNoAsFalse();
                MessageBox.Show("Amount Canceld successfully");
                cmdClear.PerformClick();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            gvlist.DataSource = myAdditions.ListAllAdditions();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtCode.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbDivision.SelectedValue = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbYear.SelectedValue = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbMonth.SelectedValue = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbEmpNo.SelectedValue = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtAmount.Text = gvlist.Rows[e.RowIndex].Cells[5].Value.ToString();
            if (gvlist.Rows[e.RowIndex].Cells[8].Value.ToString() == "Debtors")
            {
                radioButton1.Checked = true;
            }
            else if (gvlist.Rows[e.RowIndex].Cells[8].Value.ToString() == "From Fund")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton3.Checked = true;
            }


            cmbEmpNo_SelectedIndexChanged(null, null);

        }
    }
}