using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class Field : Form
    {
        FTSPayRollBL.Field myField = new FTSPayRollBL.Field();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.EstateDivisionBlock myEstateDivisionBlock = new FTSPayRollBL.EstateDivisionBlock();
       
        public Field()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Field_Load(object sender, EventArgs e)
        {
            cmbDivison.DataSource = myDivision.ListDivisions();
            cmbDivison.DisplayMember = "DivisionName";
            cmbDivison.ValueMember = "DivisionID";

            cmbDivison.SelectedIndex = 0;
            cmbCropType.SelectedIndex = 0 ;
            cmbType.SelectedIndex = 0;

            gvList.DataSource = myField.ListAllFields();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFieldName.Text == "")
                {
                    MessageBox.Show("Enter Field Name");
                }
                
                if (txtExtent.Text == "")
                {
                    MessageBox.Show("Enter Extent of the field");
                }
                
                if (cmbDivison.Text == "")
                {
                    MessageBox.Show("Select Division...!");
                }
                if (cmbType.Text == "")
                {
                     MessageBox.Show("Select Field Type");
                }
                else
                {
                       myField.DecExtent = Convert.ToDecimal(txtExtent.Text);
                       myField.StrDivisionID = cmbDivison.SelectedValue.ToString();
                       myField.StrEstateID = myEstateDivisionBlock.getEstateId();
                       myField.StrFieldID = txtfieldID.Text;
                       myField.StrFieldType = cmbType.Text;
                       myField.StrFieldName = txtFieldName.Text;
                       myField.StrCropType = cmbCropType.Text;
                       myField.DNorm = 0;
                       myField.DTree = Convert.ToDecimal(txtTree.Text);
                       myField.StrMapField = "NA";
                       myField.StrExpenditureType = cmbExpenditureType.Text;
                                
                       myField.InsertField();
                       cmdClear.PerformClick();

                       MessageBox.Show("Estate Field Added Successfully..!");
                       
                }
           }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtFieldName.Clear();
            txtExtent.Clear();
            txtfieldID.Clear();
            txtTree.Clear();

            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

            gvList.DataSource = myField.ListAllFields();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFieldName.Text == "")
                {
                    MessageBox.Show("Enter Field Name");
                }
                else
                {
                    if (txtExtent.Text == "")
                    {
                        MessageBox.Show("Enter Extent of the field");
                    }
                    else
                    {
                        if (cmbDivison.Text == "")
                        {
                            MessageBox.Show("Select Division...!");
                        }
                        else
                        {
                            if (cmbType.Text == "")
                            {
                                MessageBox.Show("Select Field Type");
                            }
                            else
                            {
                                myField.DecExtent = Convert.ToDecimal(txtExtent.Text);
                                myField.StrDivisionID = cmbDivison.SelectedValue.ToString();
                                myField.StrEstateID = myEstateDivisionBlock.getEstateId();
                                myField.StrFieldID = txtfieldID.Text;
                                myField.StrFieldType = cmbType.Text;
                                myField.StrFieldName = txtFieldName.Text;
                                myField.StrCropType = cmbCropType.Text;
                                myField.DNorm = 0;
                                myField.DTree = Convert.ToDecimal(txtTree.Text);
                                myField.StrMapField = "NA";
                                myField.StrExpenditureType = cmbExpenditureType.Text;
                                  

                                myField.UpdateField();
                                cmdClear.PerformClick();

                                MessageBox.Show("EstateField Updated successfully..!");
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFieldName.Text == "")
                {
                    MessageBox.Show("Enter Field Name");
                }
                else
                {
                    if (txtExtent.Text == "")
                    {
                        MessageBox.Show("Enter Extent of the field");
                    }
                    else
                    {
                        if (cmbDivison.Text == "")
                        {
                            MessageBox.Show("Select Division...!");
                        }
                        else
                        {
                            if (cmbType.Text == "")
                            {
                                MessageBox.Show("Select Field Type");
                            }
                            else
                            {
                                if (MessageBox.Show("Confirm Delete", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    
                                    myField.StrDivisionID = cmbDivison.SelectedValue.ToString();
                                    myField.StrFieldID = txtfieldID.Text;
                                    myField.DeleteField();
                                    cmdClear.PerformClick();
                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmdAdd.Enabled = false;
                cmdUpdate.Enabled = true;
                cmdDelete.Enabled = true;

                cmbDivison.Text = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtfieldID.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtFieldName.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbCropType.Text = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtTree.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtExtent.Text = gvList.Rows[e.RowIndex].Cells[6].Value.ToString();
                cmbType.Text = gvList.Rows[e.RowIndex].Cells[7].Value.ToString();
 
                /// Have to display newly added items
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        cmdAdd.Enabled = false;
        //        cmdUpdate.Enabled = true;
        //        cmdDelete.Enabled = true;

        //        cmbDivison.SelectedValue = gvList.Rows[e.RowIndex].Cells[0].Value.ToString();
        //        txtfieldID.Text = gvList.Rows[e.RowIndex].Cells[1].Value.ToString();
        //        txtFieldName.Text = gvList.Rows[e.RowIndex].Cells[2].Value.ToString();
        //        cmbCropType.Text = gvList.Rows[e.RowIndex].Cells[3].Value.ToString();
        //        txtNorm.Text = gvList.Rows[e.RowIndex].Cells[4].Value.ToString();
        //        txtTree.Text = gvList.Rows[e.RowIndex].Cells[5].Value.ToString();
        //        txtExtent.Text = gvList.Rows[e.RowIndex].Cells[6].Value.ToString();
        //        cmbType.Text = gvList.Rows[e.RowIndex].Cells[7].Value.ToString();                
                

        //    }
            
        }
    }
