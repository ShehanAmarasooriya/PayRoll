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
    public partial class EmployeeList : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster DeductMaster = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.LoanMaster LMaster = new FTSPayRollBL.LoanMaster();
        FTSPayRollBL.EmployeeDeduction myEmployeeDeduction = new FTSPayRollBL.EmployeeDeduction();
        InactiveMadeUpCoinsTransfer myObj;
        DailyHarvest myDharvest;
        DailyHarvestCW myDHarvestCW;
        DailyHarvestCW1 myDHarvestCW1;
        DailyHarvestRubber myDHarvestRub;
        DailyHarvestRubberCW myDHarvestRubCW;
        OutstandingRecoveriesForm myOutstandingRec;
        DailyHarvestOilPalmEntry myDHarvestOP;
        DailyHarvestOtherCrop myDHarvestOC;
        Additions myAdditions;
        EmployeeStatusChange myEmpStatusChange;
        
        String strDivisionId = "";
        Int32 intFormId = 1; //1 - Daily Harvest, 2-DHarvest CW

        public static Int32 intMyRow = -1;
        public Boolean ActiveStatus;
        public String EmployeeNo;
        public String EmpName;

        public EmployeeList()
        {
            InitializeComponent();
        }

        public EmployeeList(DailyHarvest myForm, String strDiv)
        {
            myDharvest = myForm;
            strDivisionId = strDiv;
            intFormId = 1;

            InitializeComponent();
            
            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

       

        public EmployeeList(DailyHarvestCW myForm, String strDiv)
        {
            myDHarvestCW = myForm;
            strDivisionId = strDiv;
            intFormId = 2;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(DailyHarvestCW1 myForm, String strDiv)
        {
            myDHarvestCW1 = myForm;
            strDivisionId = strDiv;
            intFormId = 3;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(DailyHarvestRubber myForm, String strDiv)
        {
            myDHarvestRub = myForm;
            strDivisionId = strDiv;
            intFormId = 4;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(DailyHarvestRubberCW myForm, String strDiv)
        {
            myDHarvestRubCW = myForm;
            strDivisionId = strDiv;
            intFormId = 5;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(OutstandingRecoveriesForm myForm, String strDiv)
        {
            myOutstandingRec = myForm;
            strDivisionId = strDiv;
            intFormId = 6;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(Additions myForm, String strDiv)
        {
            myAdditions = myForm;
            strDivisionId = strDiv;
            intFormId = 7;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(DailyHarvestOilPalmEntry myForm, String strDiv)
        {
            myDHarvestOP = myForm;
            strDivisionId = strDiv;
            intFormId = 8;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(EmployeeStatusChange myForm, String strDiv)
        {
            myEmpStatusChange = myForm;
            strDivisionId = strDiv;
            intFormId = 9;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }

        public EmployeeList(DailyHarvestOtherCrop myForm, String strDiv)
        {
            myDHarvestOC = myForm;
            strDivisionId = strDiv;
            intFormId = 10;

            InitializeComponent();

            cmbDivision_SelectedIndexChanged(null, null);
            cmbDivision.SelectedValue = strDiv;
        }


        

        private void EmployeeList_Load(object sender, EventArgs e)
        {
            ChkOnlyActive.Checked = true;

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions(strDivisionId);
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void gvEmpList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            txtEmpNo.Text = gvEmpList.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
            cmbActive.Text = gvEmpList.Rows[e.RowIndex].Cells[7].Value.ToString();
            ActiveStatus = Convert.ToBoolean(gvEmpList.Rows[e.RowIndex].Cells[7].Value.ToString());
            EmployeeNo=gvEmpList.Rows[e.RowIndex].Cells[0].Value.ToString();
            EmpName=EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text,cmbDivision.SelectedValue.ToString());
            linkLabel1.Focus();
            
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrids();
            //try
            //{
            //    if (ChkOnlyActive.Checked)
            //    {
            //        if (!String.IsNullOrEmpty(strDivisionId))
            //        {
            //            gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(strDivisionId, true);
            //        }
            //        else
            //        {
            //            gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString(), true);
            //        }
            //    }
            //    else
            //    {
            //        if (!String.IsNullOrEmpty(strDivisionId))
            //        {
            //            gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(strDivisionId);
            //        }
            //        else
            //        {
            //            gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString());
            //        }
            //    }
            //}
            //catch { }
        }

        public void RefreshGrids()
        {
            try
            {
                if (ChkOnlyActive.Checked)
                {
                    if (!String.IsNullOrEmpty(strDivisionId))
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(strDivisionId, true);
                    }
                    else
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString(), true);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(strDivisionId))
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(strDivisionId);
                    }
                    else
                    {
                        gvEmpList.DataSource = EmpMaster.getEmployeeDetailsByDivision(cmbDivision.SelectedValue.ToString());
                    }
                }
            }
            catch { }
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
                    String strResult = EmpMaster.CheckAndUpdateEmployeeActiveState(cmbDivision.Text, txtEmpNo.Text, Convert.ToBoolean(cmbActive.Text));
                    if (strResult.ToUpper().Equals("OK"))
                    {
                        MessageBox.Show("Updated Employee Active Status Successfully");
                        cmdClear.PerformClick();
                    }
                    else if (strResult.Equals("Available"))
                    {
                        MessageBox.Show("Employee Has Daily Harvest Entries, Cannot Inactive Employee " + txtEmpNo.Text, "Failed");
                        cmdClear.PerformClick();

                    }
                    else
                    {
                        MessageBox.Show("Inactive Employee Failed.", "Failed");
                        cmdClear.PerformClick();
                    }
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

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {           
                txtEmpNo_LeaveChanged();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmpNo.Text))
            {
                MessageBox.Show("Please Select A Employee To Add.","",MessageBoxButtons.OK,MessageBoxIcon.Information);                
            }
            else
            {
                if (ActiveStatus == true)
                {
                    getTextEmployeeNoValue();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please Select a Active Employee.","Invalid");
                    cmdClear.PerformClick();
                }
            }
        }

        public string getTextEmployeeNoValue()
        {
            return txtEmpNo.Text;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            switch (intFormId)
            {
                case 1:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            myDharvest.txtEmpNo.Text = this.txtEmpNo.Text;
                            myDharvest.txtEmpName.Text = this.txtEmpName.Text;
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
                            myDHarvestCW.txtEmpNo.Text = this.txtEmpNo.Text;
                            myDHarvestCW.txtEmpName.Text = this.txtEmpName.Text;
                            this.Close();
                        }
                        break;
                    }
                case 3:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myDHarvestCW1.txtEmpNo.Text = this.txtEmpNo.Text;
                            myDHarvestCW1.txtEmpName.Text = this.txtEmpName.Text;
                            this.Close();
                        }
                        break;
                    }
                case 4:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myDHarvestRub.txtEmpNo.Text = this.txtEmpNo.Text;
                            myDHarvestRub.txtEmpName.Text = this.txtEmpName.Text;
                            this.Close();
                        }
                        break;
                    }
                case 5:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myDHarvestRubCW.txtEmpNo.Text = this.txtEmpNo.Text;
                            myDHarvestRubCW.txtEmpName.Text = this.txtEmpName.Text;
                            this.Close();
                        }
                        break;
                    }
                case 6:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myOutstandingRec.txt_employeeNo.Text = this.txtEmpNo.Text;
                            this.Close();
                        }
                        break;
                    }
                case 7:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myAdditions.txtEmpNo.Text = this.txtEmpNo.Text;
                            this.Close();
                        }
                        break;
                    }
                case 8:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myDHarvestOP.txtEmpNo.Text = this.txtEmpNo.Text;
                            this.Close();
                        }
                        break;
                    }
                case 9:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myEmpStatusChange.txtEmpNo.Text = this.txtEmpNo.Text;
                            this.Close();
                        }
                        break;
                    }
                case 10:
                    {
                        if (String.IsNullOrEmpty(txtEmpNo.Text))
                        {
                            MessageBox.Show("Please Select A Employee To Add.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            myDHarvestOC.txtEmpNo.Text = this.txtEmpNo.Text;
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
            //---------
        }

        private void ChkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrids();
        }

       
    }
}