using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeSearch : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        InactiveMadeUpCoinsTransfer myObj;
        DebtorsRecoveryList myDebtorList;
        FixedDeductions frmFixed;
        LoanDeductions frmLoan;
        RiceFlourTeaDeductions frmRFT;
        OverTime frmOT;
        Deduction frmDeduction;

        public  Int32 intMyRow;
        public String strDivision;
        public Boolean blYesNo = false;
        public String strFrmType = "";
        public Boolean boolActiveVal = false;

        public EmployeeSearch(InactiveMadeUpCoinsTransfer frmObj,Int32 intRow, String strDiv)
        {
            myObj = frmObj;
            intMyRow = intRow;
            strDivision = strDiv;
            InitializeComponent();
        }

        public EmployeeSearch(FixedDeductions frmObj, String strDiv,String frmtype)
        {
            frmFixed = frmObj;
            strDivision = strDiv;
            strFrmType = frmtype;
            InitializeComponent();
        }

        public EmployeeSearch(LoanDeductions frmObj, String strDiv, String frmtype)
        {
            frmLoan = frmObj;
            strDivision = strDiv;
            strFrmType = frmtype;
            InitializeComponent();
        }

        public EmployeeSearch(RiceFlourTeaDeductions frmObj, String strDiv, String frmtype)
        {
            frmRFT = frmObj;
            strDivision = strDiv;
            strFrmType = frmtype;
            InitializeComponent();
        }
        public EmployeeSearch(DebtorsRecoveryList frmObj, Int32 intRow, String strDiv,Boolean bl)
        {
            myDebtorList = frmObj;
            intMyRow = intRow;
            strDivision = strDiv;
            blYesNo = bl;
            InitializeComponent();
        }

        public EmployeeSearch(OverTime frmObj, String strDiv, String frmtype)
        {
            frmOT = frmObj;
            strDivision = strDiv;
            strFrmType = frmtype;
            InitializeComponent();
        }

        public EmployeeSearch(Deduction frmObj, String strDiv, String frmtype)
        {
            frmDeduction = frmObj;
            strDivision = strDiv;
            strFrmType = frmtype;
            InitializeComponent();
        }
        private void EmployeeSearch_Load(object sender, EventArgs e)
        {
            ChkOnlyActive.Checked = true;

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision.SelectedValue = strDivision;


            gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(strDivision,true);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString(),true);
            }
            catch { }
        }

        private void gvEmpList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            boolActiveVal = Convert.ToBoolean(gvEmpList.Rows[e.RowIndex].Cells[7].Value.ToString());
            txtEmpNo.Text = gvEmpList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
            cmbActive.Text = gvEmpList.Rows[e.RowIndex].Cells[7].Value.ToString();
            linkLabel1.Focus();
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
            boolActiveVal = false;
            try
            {
                gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString());
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
                MessageBox.Show("EmpNo Cannot be empty");
            }
            else
            {
                try
                {
                    EmpMaster.UpdateEmployeeActiveState(cmbDivision.Text, txtEmpNo.Text, boolActiveVal);
                    MessageBox.Show("Updated Employee Active Status Successfully");
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
                    MessageBox.Show("Please Select Active Employee Within the Division You Selected Above.");
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

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            txtEmpNo_LeaveChanged();
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                //EmployeeList empList = new EmployeeList();
                //empList.Show();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (strFrmType.Equals("Fixed"))
                {
                    if (Convert.ToBoolean(boolActiveVal) == true)
                    {
                        frmFixed.txtEmpNo.Text = txtEmpNo.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Active Employee.", "Invalid");
                        cmdClear.PerformClick();
                    }
                    
                }
                else if (strFrmType.Equals("Loan"))
                {
                    if(Convert.ToBoolean(boolActiveVal) == true)
                    {
                        frmLoan.txtEmpNo.Text = txtEmpNo.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Active Employee.", "Invalid");
                        cmdClear.PerformClick();
                    }
                }
                else if (strFrmType.Equals("RFT"))
                {
                    if (Convert.ToBoolean(boolActiveVal) == true)
                    {
                        frmRFT.txtEmpNo.Text = txtEmpNo.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Active Employee.", "Invalid");
                        cmdClear.PerformClick();
                    }
                }
                else if (strFrmType.Equals("OT"))
                {
                    if (Convert.ToBoolean(boolActiveVal) == true)
                    {
                        frmOT.txtEmpNo.Text = txtEmpNo.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Active Employee.", "Invalid");
                        cmdClear.PerformClick();
                    }
                }
                else if (strFrmType.Equals("Deduction"))
                {
                    if (Convert.ToBoolean(boolActiveVal) == true)
                    {
                        frmDeduction.txtEmpNo.Text = txtEmpNo.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Active Employee.", "Invalid");
                        cmdClear.PerformClick();
                    }
                }
                else
                {
                    if (blYesNo == false)
                    {
                        if (Convert.ToBoolean(boolActiveVal) == true)
                        {
                            myObj.dataGridView1[2, intMyRow].Value = txtEmpNo.Text;
                            myObj.dataGridView1[3, intMyRow].Value = cmbDivision.SelectedValue.ToString();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Please Select a Active Employee.", "Invalid");
                            cmdClear.PerformClick();
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(boolActiveVal) == true)
                        {
                            myDebtorList.gvlist[7, intMyRow].Value = txtEmpNo.Text;
                            myDebtorList.gvlist[8, intMyRow].Value = txtEmpName.Text;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Please Select a Active Employee.", "Invalid");
                            cmdClear.PerformClick();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public string getTextEmployeeNoValue()
        {
            return txtEmpNo.Text;
        }

        public void RefreshGrids()
        {
            try
            {
                if (ChkOnlyActive.Checked)
                {
                    if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString(), true);
                    }
                    else
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString(), true);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString());
                    }
                    else
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString());
                    }
                }
            }
            catch { }
        }

        private void ChkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrids();
        }
    }
}