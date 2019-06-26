using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace FTSPayroll
{
    
    public partial class AdditionBulkFRM : Form
    {

        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.Additions myAdditions = new FTSPayRollBL.Additions();

        public AdditionBulkFRM()
        {
            InitializeComponent();
            this.gvList.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.gvList.Columns["Amount"].DefaultCellStyle.BackColor = Color.Cyan;
        }

        private void AdditionBulkFRM_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            try
            {
                cmbYear.SelectedValue = YMonth.getLastYearID();
                cmbMonth.SelectedValue = YMonth.getLastMonthID();
            }
            catch { }

            cmbAddition.DataSource = myAdditions.getAddition();
            cmbAddition.DisplayMember = "AdditionName";
            cmbAddition.ValueMember = "AdditionId";
            this.setGrid();
        }




        private void setGrid()
        {

            try
            {
                gvList.DataSource = myAdditions.getAditionEmpListForBulk(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbAddition.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()).Tables[0];
            }
            catch { }


        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setGrid();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setGrid();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setGrid();
        }

        private void cmbAddition_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setGrid();
            try
            {
                lblAddition.Text = myAdditions.GetAdditionNameByID(Convert.ToInt32(cmbAddition.SelectedValue.ToString()));
            }
            catch { }
        }
        private void addAddition()
        {
            
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int _year = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                int _month = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                int _addtionCode = Convert.ToInt32(cmbAddition.SelectedValue.ToString());
                string _division = cmbDivision.SelectedValue.ToString();
                string _UserID = FTSPayRollBL.User.StrUserName;
                decimal _amount = 0;
                string _empNo = string.Empty;
               // myAdditions.DeleteAdditionsBulk(_year, _month, _addtionCode, _division);


                for (int i = 0; i < gvList.RowCount; i++)
                {
                    if (Convert.ToBoolean(gvList.Rows[i].Cells[0].Value) == true)
                    {
                        _amount = Convert.ToDecimal(gvList.Rows[i].Cells["Amount"].Value);
                        _empNo = gvList.Rows[i].Cells["EmpNo"].Value.ToString();

                        if (_amount == 0)
                        { continue; }

                        myAdditions.InsertAdditionsBulk(_year, _month, _addtionCode, _division, _amount, _empNo, _UserID);
                    }
                   
                    
                }

                this.setGrid();
                MessageBox.Show("Additions Saved Successfully!");
            }
            catch
            {
                MessageBox.Show("Additions Save Failed!");
            }

           
            
        }
       
        
    
          

        

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int _year = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            int _month = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
            int _addtionCode = Convert.ToInt32(cmbAddition.SelectedValue.ToString());
            string _division = cmbDivision.SelectedValue.ToString();
            myAdditions.DeleteAdditionsBulk(_year, _month, _addtionCode, _division);

            this.setGrid();
        }

        private void gvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void cheakAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvList.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
            }
        }

        private void gvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //string str;

            //str = "3";
            //for (int i = 0; i < gvList.Rows.Count; i++)
            //{
            //    if (Convert.ToBoolean(gvList.Rows[i].Cells[0].Value) == true)
            //    {
            //        gvList.Rows[i].Cells[3].Value = str;
            //    }
            //}
        }

        private void gvList_CurrentCellChanged(object sender, EventArgs e)
        {
            //decimal _amount = 0;
            //string _empNo = string.Empty;


            //for (int i = 0; i < gvList.Rows.Count; i++)
            //{

            //    _amount = Convert.ToDecimal(gvList.Rows[i].Cells["Amount"].Value);
            //    _empNo = gvList.Rows[i].Cells["EmpNo"].Value.ToString();
            //    if (Convert.ToBoolean(gvList.Rows[i].Cells[0].Value) == true)
            //    {
            //        gvList.Rows[i].Cells[3].Value = 500;
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal _amount;
            string val = Interaction.InputBox("Enter the Amount", "Enter Fixed Addition", "", 500, 500);
            if (val != "")
            {
                _amount = System.Convert.ToDecimal(val);
                try
                {
                    int _year = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    int _month = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                    int _addtionCode = Convert.ToInt32(cmbAddition.SelectedValue.ToString());
                    string _division = cmbDivision.SelectedValue.ToString();
                    string _UserID = FTSPayRollBL.User.StrUserName;
                    string _empNo = string.Empty;
                    // myAdditions.DeleteAdditionsBulk(_year, _month, _addtionCode, _division);


                    for (int i = 0; i < gvList.RowCount; i++)
                    {
                        if (Convert.ToBoolean(gvList.Rows[i].Cells[0].Value) == true)
                        {
                          
                            _empNo = gvList.Rows[i].Cells["EmpNo"].Value.ToString();

                            if (_amount == 0)
                            { continue; }

                            myAdditions.InsertAdditionsBulk(_year, _month, _addtionCode, _division, _amount, _empNo, _UserID);
                        }


                    }

                    this.setGrid();
                    cheakAll.Checked = false;
                    MessageBox.Show("Additions Saved Successfully!");
                }
                catch
                {
                    MessageBox.Show("Additions Save Failed!");
                }

            }
            else
                MessageBox.Show("Please Enter amout");

        }



    }
}

