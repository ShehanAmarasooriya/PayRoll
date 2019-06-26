using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class Division : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.EstateDivisionBlock myEstateDivisionBlock = new FTSPayRollBL.EstateDivisionBlock();
        public Division()
        {
            InitializeComponent();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtName.Clear();

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

            gvList.DataSource = myDivision.ListDivisions();
            txtCode.Focus();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Division_Load(object sender, EventArgs e)
        {
            gvList.DataSource = myDivision.ListDivisions();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == "")
                {
                    MessageBox.Show("Enter Division Code");
                }
                else
                {
                    if (txtName.Text == "")
                    {
                        MessageBox.Show("Enter Division Name");
                    }
                    else
                    {
                        myDivision.StrDivisionID = txtCode.Text;
                        myDivision.StrDivisionName = txtName.Text;
                        myDivision.StrEstateID = myEstateDivisionBlock.getEstateId();
                        if (chkActive.Checked)
                        {
                            myDivision.BoolActiveDivision = true;
                        }
                        else
                        {
                            myDivision.BoolActiveDivision = false;
                        }

                        myDivision.InsertCategory();
                        gvList.DataSource = myDivision.ListDivisions();
                        cmdClear.PerformClick();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == "")
                {
                    MessageBox.Show("Enter Division Code");
                }
                else
                {
                    if (txtName.Text == "")
                    {
                        MessageBox.Show("Enter Division Name");
                    }
                    else
                    {
                        myDivision.StrDivisionID = txtCode.Text;
                        myDivision.StrDivisionName = txtName.Text;
                        if (chkActive.Checked)
                        {
                            myDivision.BoolActiveDivision = true;
                        }
                        else
                        {
                            myDivision.BoolActiveDivision = false;
                        }
                        myDivision.UpdateCategory();
                        gvList.DataSource = myDivision.ListDivisions();
                        cmdClear.PerformClick();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == "")
                {
                    MessageBox.Show("Enter Division Code");
                }
                else
                {
                    if (txtName.Text == "")
                    {
                        MessageBox.Show("Enter Division Name");
                    }
                    else
                    {
                        if (MessageBox.Show("Confirm Delete", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            myDivision.StrDivisionID = txtCode.Text;
                            myDivision.StrDivisionName = txtName.Text;
                            myDivision.DeleteCategory();
                            gvList.DataSource = myDivision.ListDivisions();
                            cmdClear.PerformClick();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void gvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdAdd.Enabled = false;
                cmdUpdate.Enabled = true;
                cmdDelete.Enabled = true;

                txtCode.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}