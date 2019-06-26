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
    public partial class ContractorSearch : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        BlockPlk2013Register myBlkPlkRegForm;
        DailyHarvest myDharvest;
        String strDivisionId = "";
        Int32 intFormId = 0; //1 - BlockPlkRegForm
    
        public ContractorSearch()
        {
            InitializeComponent();
        }
        public ContractorSearch(BlockPlk2013Register myForm,String strDiv)
        {
            myBlkPlkRegForm = myForm;
            strDivisionId = strDiv;
            InitializeComponent();
            intFormId = 1;

            cmbDivision_SelectedIndexChanged(null, null);
        }

        public ContractorSearch(DailyHarvest myForm, String strDiv)
        {
            myDharvest = myForm;
            strDivisionId = strDiv;
            InitializeComponent();
            intFormId = 2;

            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void ContractorSearch_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvEmpList.DataSource = EmpMaster.getEmpTypeContractorDetailsByDivision(cmbDivision.SelectedValue.ToString());
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch(intFormId)
            {
                case 1:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myBlkPlkRegForm.txtEmpNo.Text = this.txtEmpNo.Text;
                            myBlkPlkRegForm.txtEmpName.Text = this.txtEmpName.Text;
                            this.Close();
                        }
                        break;                    
                    }
                case 2:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myDharvest.txtContractor.Text = this.txtEmpNo.Text;
                            myDharvest.txtContractorName.Text = this.txtEmpName.Text;
                           
                            this.Close();
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Invalid request!");
                        break;
                    }

            }
        
        }

        private void gvEmpList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEmpNo.Text = gvEmpList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
            cmbActive.Text = gvEmpList.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}