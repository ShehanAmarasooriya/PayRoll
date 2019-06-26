using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeChildrens : Form
    {

        FTSPayRollBL.EstateDivisionBlock myEstateDiv = new FTSPayRollBL.EstateDivisionBlock(); 
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();

        public EmployeeChildrens()
        {
            InitializeComponent();
        }

        private void EmployeeChildrens_Load(object sender, EventArgs e)
        {
            try
            {
                DivisionID.DataSource = myEstateDiv.ListEstateDivisions();
                DivisionID.DisplayMember = "DivisionName";
                DivisionID.ValueMember = "DivisionID";
                DivisionID_SelectedIndexChanged(null, null);

                cmbGender.SelectedIndex = 0;

                btnClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message);
            }
        }

        private void DivisionID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionName = DivisionID.SelectedValue.ToString();

                cmbEmpNo.DataSource = myMaster.ListAllEmployees(DivisionName);
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
                lblEmpName.Text = myMaster.GetEmployeeNameByEmpNo(DivisionID.SelectedValue.ToString(), cmbEmpNo.Text);
            }
            catch { }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtChildName.Text.Trim() == "")
                {
                    MessageBox.Show("Child Name can't be empty..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    myMaster.InsertEmployeeChildDetails(DivisionID.SelectedValue.ToString().Trim(), cmbEmpNo.Text.Trim(), txtChildName.Text.Trim(), DOB.Value.Date, cmbGender.Text);

                    MessageBox.Show("Child details added successfully..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..!\n"+ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtChildName.Text.Trim() == "")
                {
                    MessageBox.Show("Child Name can't be empty..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    myMaster.InsertEmployeeChildDetails(DivisionID.SelectedValue.ToString().Trim(), cmbEmpNo.Text.Trim(), txtChildName.Text.Trim(), DOB.Value.Date, cmbGender.Text);

                    MessageBox.Show("Child details added successfully..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..!\n" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtChildName.Clear();

                gvlist.DataSource = myMaster.GetChildrenDetails(DivisionID.SelectedValue.ToString().Trim()).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message);
            }
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DivisionID.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbEmpNo.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtChildName.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (gvlist.Rows[e.RowIndex].Cells[3].Value.ToString() != "")
                {
                    DOB.Value = Convert.ToDateTime(gvlist.Rows[e.RowIndex].Cells[3].Value.ToString());
                }
                cmbGender.Text = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();

                btnDelete.Enabled = true;
                btnAdd.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message);
            }
        }
    }
}