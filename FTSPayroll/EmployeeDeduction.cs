using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeDeduction : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeDeduction EmpDeduct = new FTSPayRollBL.EmployeeDeduction();
        public EmployeeDeduction()
        {
            InitializeComponent();
        }
                
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmpDeduct.StrDivision = cmbDivision.SelectedValue.ToString();
            EmpDeduct.IntCategory=int.Parse(cmbCategory.SelectedValue.ToString());
            if (chkPeriodYesNo.Checked)
            {
                EmpDeduct.DtPeriodFrom = Convert.ToDateTime(dtpPeriodFrom.Value.Date);
                EmpDeduct.DtPeriodTo = Convert.ToDateTime(dtpPeriodTo.Value.Date);
                EmpDeduct.IntMonth = EmpDeduct.DtPeriodFrom.Month;
                EmpDeduct.IntYear = EmpDeduct.DtPeriodTo.Year;

            }
            else
            {
                EmpDeduct.IntMonth = int.Parse(cmbMonth.SelectedValue.ToString());
                EmpDeduct.IntYear = int.Parse(txtYear.Text);
                EmpDeduct.DtPeriodFrom = new DateTime(EmpDeduct.IntYear, EmpDeduct.IntMonth, 1);
                EmpDeduct.DtPeriodTo = new DateTime(EmpDeduct.IntYear, EmpDeduct.IntMonth, 1).AddMonths(1).AddDays(-1);
            }            
            EmpDeduct.IntDeduction = int.Parse(cmbDeduction.SelectedValue.ToString());
            EmpDeduct.FlAmount = float.Parse(txtAmount.Text);
           
            if (chkAll.Checked)
            {
                EmpDeduct.BoolAllCat = true;
                EmpDeduct.StrEmpNO = "ALL";
            }
            else
            {
                EmpDeduct.BoolAllCat = false;
                EmpDeduct.StrEmpNO = cmbEmpNO.SelectedValue.ToString();
            }
            try
            {
                String status=EmpDeduct.InsertEmployeeDeduction();
                if (status.Equals("ADDED"))
                {
                    MessageBox.Show("Deduction Added Successfully!");
                    btnCancel.PerformClick();
                }
                else if (status.Equals("EXISTS"))
                {
                    MessageBox.Show("Deduction Already Exists.");
                }
                else
                    MessageBox.Show("Error.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbDivision.SelectedValue = -1;
            cmbDivision.SelectedValue = -1;
            cmbCategory.SelectedValue = -1;

            chkPeriodYesNo.Checked = false;
            gbPeriod.Enabled = true;
            gbPeriod.Enabled = true;
            cmbMonth.SelectedValue = -1;
            txtYear.Text = "";
            cmbDeduction.SelectedValue = -1;
            txtAmount.Text = "";
            chkAll.Checked = false;
            cmbEmpNO.Enabled = true;
            cmbEmpNO.SelectedValue = -1;

            lblRefNo.Text = "";
            gvEmployeeDeduction.DataSource = EmpDeduct.ListEmpDeductions();

            txtName.Enabled = true;
            txtName.Text = "";
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void chkPeriodYesNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPeriodYesNo.Checked)
            {
                gbPeriod.Enabled = true;
                cmbMonth.Enabled = false;
                txtYear.Enabled = false;
            }
            else
            {
                gbPeriod.Enabled = false;
                cmbMonth.Enabled = true;
                txtYear.Enabled = true;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbEmpNO.Enabled = false;
                txtName.Enabled = false;
            }
            else
            {
                cmbEmpNO.Enabled = true; ;
                txtName.Enabled = true;
            }

        }

        private void EmployeeDeduction_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmpNO.DataSource = EmpMaster.ListAllEmployees();
            cmbEmpNO.DisplayMember = "EmpNo";
            cmbEmpNO.ValueMember = "EmpNo";

            //cmbDeduction.DataSource = DeductMaster.ListDeductions();
            //cmbDeduction.DisplayMember = "DiductionName";
            //cmbDeduction.ValueMember = "Ref#";

            cmbCategory.DataSource = EmpCat.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            
            gvEmployeeDeduction.DataSource = EmpDeduct.ListEmpDeductions();
            chkPeriodYesNo_CheckedChanged(null, null);
            chkAll_CheckedChanged(null,null);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                EmpDeduct.IntEmpDeductId = int.Parse(lblRefNo.Text);
                EmpDeduct.StrDivision = cmbDivision.SelectedValue.ToString();
                EmpDeduct.IntCategory = int.Parse(cmbCategory.SelectedValue.ToString());
                if (chkPeriodYesNo.Checked)
                {
                    EmpDeduct.DtPeriodFrom = Convert.ToDateTime(dtpPeriodFrom.Value.Date);
                    EmpDeduct.DtPeriodTo = Convert.ToDateTime(dtpPeriodTo.Value.Date);
                    EmpDeduct.IntMonth = EmpDeduct.DtPeriodFrom.Month;
                    EmpDeduct.IntYear = EmpDeduct.DtPeriodTo.Year;

                }
                else
                {
                    EmpDeduct.IntMonth = int.Parse(cmbMonth.SelectedValue.ToString());
                    EmpDeduct.IntYear = int.Parse(txtYear.Text);
                    EmpDeduct.DtPeriodFrom = new DateTime(EmpDeduct.IntYear, EmpDeduct.IntMonth, 1);
                    EmpDeduct.DtPeriodTo = new DateTime(EmpDeduct.IntYear, EmpDeduct.IntMonth, 1).AddMonths(1).AddDays(-1);
                }
                EmpDeduct.IntDeduction = int.Parse(cmbDeduction.SelectedValue.ToString());
                EmpDeduct.FlAmount = float.Parse(txtAmount.Text);

                if (chkAll.Checked)
                {
                    EmpDeduct.BoolAllCat = true;
                    EmpDeduct.StrEmpNO = "ALL";
                }
                else
                {
                    EmpDeduct.BoolAllCat = false;
                    EmpDeduct.StrEmpNO = cmbEmpNO.SelectedValue.ToString();
                }
                try
                {
                    String status = EmpDeduct.UpdateEmployeeDeduction();
                    if (status.Equals("UPDATED"))
                    {
                        MessageBox.Show("Deduction Updated Successfully!");
                        btnCancel.PerformClick();
                    }
                    else if (status.Equals("NOTEXISTS"))
                    {
                        MessageBox.Show("Deduction Not Exists.");
                    }
                    else
                        MessageBox.Show("Error.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error"+ex.Message);
                }

            }
            else
                MessageBox.Show("Please Select Data Before Update.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(lblRefNo.Text))
            {
                if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    EmpDeduct.IntEmpDeductId = int.Parse(lblRefNo.Text);
                    try
                    {
                        String status = EmpDeduct.DeleteEmployeeDeduction();
                        if (status.Equals("DELETED"))
                        {
                            MessageBox.Show("Deduction Deleted Successfully!");
                            btnCancel.PerformClick();
                        }
                        else if (status.Equals("NOTEXISTS"))
                        {
                            MessageBox.Show("Deduction Not Exists.");
                        }
                        else
                            MessageBox.Show("Error.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("Please Select Data Before Delete.");
        }

        private void gvEmployeeDeduction_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            lblRefNo.Text = gvEmployeeDeduction.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbDivision.SelectedValue = gvEmployeeDeduction.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbCategory.SelectedValue = int.Parse(gvEmployeeDeduction.Rows[e.RowIndex].Cells[2].Value.ToString());

            chkPeriodYesNo.Checked = Convert.ToBoolean(gvEmployeeDeduction.Rows[e.RowIndex].Cells[5].Value.ToString());
            if (chkPeriodYesNo.Checked)
            {
                gbPeriod.Enabled = true;
                dtpPeriodFrom.Value = Convert.ToDateTime(gvEmployeeDeduction.Rows[e.RowIndex].Cells[6].Value.ToString());
                dtpPeriodTo.Value = Convert.ToDateTime(gvEmployeeDeduction.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
            else
            {
                gbPeriod.Enabled = false;
                cmbMonth.SelectedValue = int.Parse(gvEmployeeDeduction.Rows[e.RowIndex].Cells[3].Value.ToString());
                txtYear.Text = gvEmployeeDeduction.Rows[e.RowIndex].Cells[4].Value.ToString();
            }

            cmbDeduction.SelectedValue = int.Parse(gvEmployeeDeduction.Rows[e.RowIndex].Cells[8].Value.ToString());
            txtAmount.Text = gvEmployeeDeduction.Rows[e.RowIndex].Cells[9].Value.ToString();
            chkAll.Checked = Convert.ToBoolean(gvEmployeeDeduction.Rows[e.RowIndex].Cells[10].Value.ToString());
            if (chkAll.Checked)
            {
                cmbEmpNO.Enabled = false;
                txtName.Enabled = false;
            }
            else
            {
                cmbEmpNO.SelectedValue = gvEmployeeDeduction.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;



        }

        private void cmbEmpNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpNO.SelectedIndex != -1)
            {
                if (!String.IsNullOrEmpty(cmbEmpNO.SelectedValue.ToString()))
                {
                    this.txtName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), cmbEmpNO.SelectedValue.ToString());
                }
            }
        }
    }
}