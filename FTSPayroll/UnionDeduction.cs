using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class UnionDeduction : Form
    {
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeUnion myEmpUnion = new FTSPayRollBL.EmployeeUnion();

        public UnionDeduction()
        {
            InitializeComponent();
        }

        private void btnCLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UnionDeduction_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            cmbDivision.SelectedIndex = 0;

            try
            {
                cmbDivision_SelectedIndexChanged(null, null);
            }
            catch { }

            btnADD.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled=true;

            groupBox2.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnADD.Enabled = true;
            btnUpdate.Enabled = false;
            groupBox2.Enabled = false;
            txtCurrentUnion.Text = "";
            gvList.DataSource = EmpMaster.ListEmployeeUnions(cmbDivision.SelectedValue.ToString());
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
            {
                cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(cmbDivision.SelectedValue.ToString());
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";

                cmbEmpNo_SelectedIndexChanged(null, null);

                gvList.DataSource = EmpMaster.ListEmployeeUnions(cmbDivision.SelectedValue.ToString());
            }
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                {
                    txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbEmpNo.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
                    txtCurrentUnion.Text = EmpMaster.GetEmployeeUnionByEmpNo(cmbEmpNo.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString());
                }
              
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            groupBox2.Enabled = true;
            btnUpdate.Enabled = true;
            btnADD.Enabled = true;

            cmbNewUnion.DataSource = myEmpUnion.ListAllUnion();
            cmbNewUnion.DisplayMember = "UnionCode";
            cmbNewUnion.ValueMember = "UnionCode";
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            myEmpUnion.StrDivision = cmbDivision.SelectedValue.ToString();
            myEmpUnion.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
            myEmpUnion.StrUnionID = cmbNewUnion.SelectedValue.ToString();
            try
            {
                myEmpUnion.InsertUnionToEmployee();
                MessageBox.Show("Union Assigned Successfully!");
                btnClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Union Assign Failed, " + ex.Message);
            }
            btnClear.PerformClick();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            myEmpUnion.StrDivision = cmbDivision.SelectedValue.ToString();
            myEmpUnion.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
            myEmpUnion.StrUnionID = cmbNewUnion.SelectedValue.ToString();
            try
            {
                myEmpUnion.UpdateUnionToEmployee();
                MessageBox.Show("Union Changed Successfully!");
                btnClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Union Change Failed, " + ex.Message);
            }
            btnClear.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            myEmpUnion.StrDivision = cmbDivision.SelectedValue.ToString();
            myEmpUnion.StrEmpNo = cmbEmpNo.SelectedValue.ToString();
            myEmpUnion.StrUnionID = cmbNewUnion.SelectedValue.ToString();
            try
            {
                myEmpUnion.DeleteUnionToEmployee();
                MessageBox.Show("Union Deleted Successfully!");
                btnClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Union Delete Failed, " + ex.Message);
            }
            btnClear.PerformClick();
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDivision.SelectedValue= gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbEmpNo.SelectedValue= gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbEmpNo_SelectedIndexChanged(null, null);
            txtCurrentUnion.Text=gvList.Rows[e.RowIndex].Cells[3].Value.ToString();            
        }

        
    }
}