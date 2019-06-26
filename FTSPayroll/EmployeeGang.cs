using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeGang : Form
    {
        public EmployeeGang()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeGang myEmployeegang = new FTSPayRollBL.EmployeeGang();

        private void EmployeeGang_Load(object sender, EventArgs e)
        {
            cmbEstate.DataSource = myEstate.ListEstates();
            cmbEstate.DisplayMember = "EstateName";
            cmbEstate.ValueMember = "EstateID";

            cmbDivision.DataSource = myEstate.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            gvlist.DataSource = myEmployeegang.ListAllGang();
            
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGangName.Text == "")
                {
                    MessageBox.Show("Gang name can not be empty");
                    txtGangName.Focus();
                }

                else
                {
                    myEmployeegang.StrEstateID = cmbEstate.SelectedValue.ToString();
                    myEmployeegang.StrDivisionID = cmbDivision.SelectedValue.ToString();
                    myEmployeegang.StrGang = txtGangName.Text;
                    myEmployeegang.InsertEmpGang();
                    MessageBox.Show("Employee Gang Added successfully");
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..!");
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGangName.Text == "")
                {
                    MessageBox.Show("Gang name can not be empty");
                    txtGangName.Focus();
                }

                else
                {
                    myEmployeegang.StrEstateID = cmbEstate.SelectedValue.ToString();
                    myEmployeegang.StrDivisionID = cmbDivision.SelectedValue.ToString();
                    myEmployeegang.StrGang = txtGangName.Text;
                    myEmployeegang.UpdateEmpGang();
                    MessageBox.Show("Employee Gang Updated successfully");
                    cmdClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured..!");
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtGangName.Text == "")
                {
                    MessageBox.Show("Gang name can not be empty");
                    txtGangName.Focus();
                }
                else
                {
                    myEmployeegang.StrGang = txtGangName.Text;
                    myEmployeegang.DeleteEmpGang();
                    MessageBox.Show("Employee Gang Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtGangName.Text = "";
            gvlist.DataSource = myEmployeegang.ListAllGang();
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDivision.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtGangName.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
           
            cmdUpdate.Enabled = true;
            cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
            
        }
    }
}