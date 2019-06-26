using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;
using System.Text.RegularExpressions;



namespace FTSPayroll
{
    public partial class EPFEmployerMaster : Form
    {
        EstateDivisionBlock myDivision = new EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster myEmployee = new FTSPayRollBL.EmployeeMaster();
        EmployerMaster objEmployer = new EmployerMaster();

        public EPFEmployerMaster()
        {
            InitializeComponent();
        }

        private void EPFEmployerMaster_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            try
            {
                gvlist.DataSource = objEmployer.ListEmployers();
            }
            catch { }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(txtEmployerNo.Text, @"^[0-9]{6}$"))
                {
                    MessageBox.Show("Employer No Must Be 6 Digits", "Invalid", MessageBoxButtons.OK);
                    txtEmployerNo.Focus();
                }
                else if (!Regex.IsMatch(txtZoneCode.Text, @"^[A-Z]$"))
                {
                    MessageBox.Show("Zone Code Must Be 1 Character.");
                    txtZoneCode.Focus();
                }
                else if (!Regex.IsMatch(txtDistrictCode.Text, @"^[1-9]{2}$"))
                {
                    MessageBox.Show("District Office Code Must Be 2 Digits.");
                    txtDistrictCode.Focus();
                }
                else
                {
                    objEmployer.InsertEmployer(User.StrEstate, cmbDivision.SelectedValue.ToString(), txtEmployerNo.Text, txtZoneCode.Text, cmbPayMode.Text, "", txtDistrictCode.Text);
                    cmdClear.PerformClick();
                    MessageBox.Show("Saved Successfully!");
                }
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
                objEmployer.DeleteEmployer(txtEmployerNo.Text);
                cmdClear.PerformClick();
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(txtEmployerNo.Text, @"^[0-9]{6}$"))
                {
                    MessageBox.Show("Employer No Must Be 6 Digits", "Invalid", MessageBoxButtons.OK);
                    txtEmployerNo.Focus();
                }
                else if (!Regex.IsMatch(txtZoneCode.Text, @"^[A-Z]$"))
                {
                    MessageBox.Show("Zone Code Must Be 1 Character.");
                    txtZoneCode.Focus();
                }
                else if (!Regex.IsMatch(txtDistrictCode.Text, @"^[1-9]{2}$"))
                {
                    MessageBox.Show("District Office Code Must Be 2 Digits.");
                    txtDistrictCode.Focus();
                }
                else
                {
                    objEmployer.UpdateEmployer(cmbDivision.SelectedValue.ToString(), txtEmployerNo.Text, txtZoneCode.Text, cmbPayMode.Text, "", txtDistrictCode.Text);
                    cmdClear.PerformClick();
                    MessageBox.Show("Employer Saved!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            cmdAdd.Enabled = true;
           
            try
            {
                gvlist.DataSource = objEmployer.ListEmployers();
            }
            catch { }
        }

        private void gvlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDivision.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEmployerNo.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtZoneCode.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbPayMode.Text = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDistrictCode.Text = gvlist.Rows[e.RowIndex].Cells[6].Value.ToString();

            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }
    }
}