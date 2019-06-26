using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using FTSPayRollBL;


namespace FTSPayroll
{
    public partial class ContractorList : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();


        public ContractorList()
        {
            InitializeComponent();
        }

        private void EmployeeList_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);

        }

        private void gvEmpList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            txtEmpNo.Text = gvEmpList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
            cmbActive.Text = gvEmpList.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvEmpList.DataSource = EmpMaster.getContractorDetailsByDivision(cmbDivision.SelectedValue.ToString());
            }
            catch { }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtEmpNo.Clear();
            txtEmpName.Clear();
            cmbActive.SelectedIndex = -1;
            try
            {
                gvEmpList.DataSource = EmpMaster.getContractorDetails();
            }
            catch { }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbDivision.Text))
            {
                MessageBox.Show("Division Cannot be empty");
            }
            else if (String.IsNullOrEmpty(txtEmpNo.Text))
            {
                MessageBox.Show("Contractor Cannot be empty");
            }
            else
            {
                try
                {
                    EmpMaster.UpdateEmployeeActiveState(cmbDivision.Text, txtEmpNo.Text, Convert.ToBoolean(cmbActive.Text));
                    MessageBox.Show("Updated Contractor Active Status Successfully");
                    cmdClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }

        }

        private void txtEmpNo_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txtEmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Active Contractor Within the Division You Selected Above.");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
                else
                {
                    //cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                    this.cmbActive.Focus();
                }
            }

        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                EmployeeList empList = new EmployeeList();
                empList.Show();
            }
            else
            {
                if (e.KeyChar == 8)
                {
                    //txtJobShortName.Focus();
                }
                else
                {
                    if (e.KeyChar == 13)
                    {
                        txtEmpNo_LeaveChanged();
                    }
                }
            }
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
           
                txtEmpNo_LeaveChanged();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmpNo.Text))
            {
                MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                getTextEmployeeNoValue();
                this.Close();
            }
        }

        public string getTextEmployeeNoValue()
        {
            return txtEmpNo.Text;
        }

       
    }
}