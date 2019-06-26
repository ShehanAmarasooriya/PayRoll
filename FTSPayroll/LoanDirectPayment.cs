using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class LoanDirectPayment : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.LoanMaster myLoan = new FTSPayRollBL.LoanMaster();
        public LoanDirectPayment()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void LoanDirectPayment_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(cmbDivision.SelectedValue.ToString());
            cmbEmpNo.DisplayMember = "EmpNo";
            cmbEmpNo.ValueMember = "EmpNo";

           
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbLoan.DataSource = myLoan.getLoan(cmbEmpNo.SelectedValue.ToString());
                cmbLoan.DisplayMember = "LoanName";
                cmbLoan.ValueMember = "LoanCode";

                cmbLoan_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            { }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(cmbDivision.SelectedValue.ToString());
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
            }
            catch (Exception ex)
            { }
        }

        private void cmbLoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                dt=myLoan.getLoanDetails(Convert.ToInt32(cmbLoan.SelectedValue.ToString()));
                txtLoanName.Text=dt.Rows[0][1].ToString();
                txtPrincipleValue.Text=dt.Rows[0][2].ToString();
                txtTotalPaid.Text=dt.Rows[0][3].ToString();
                txtOutstanding.Text=dt.Rows[0][4].ToString();
            }
            catch (Exception ex)
            { }
        }
    }
}