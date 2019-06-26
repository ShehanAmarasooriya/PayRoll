using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{

    public partial class SpecialHalfNames : Form
    {
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        Job Job1 = new Job();

        DailyHarvest FrmDHarvest;
        String Division;
        String EmpNo;
        String FieldID;
        DateTime DateEntered;
        public SpecialHalfNames()
        {
            InitializeComponent();
        }

        public SpecialHalfNames(DailyHarvest DHarvest,DateTime dtDate, String strDiv, String strEmpNo, String strField)
        {
            FrmDHarvest = DHarvest;
            DateEntered = dtDate;
            Division = strDiv;
            EmpNo = strEmpNo;
            FieldID = strField;
            InitializeComponent();
        }

        private void SpecialHalfNames_Load(object sender, EventArgs e)
        {
            dtDate.Value = DateEntered;
            txtSHDivision.Text = Division;
            txtSHEmpNo.Text = EmpNo;
            txtSHField.Text = FieldID;
            txtSHWorkCode.Focus();

        }

        private void txtSHEmpNo_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSHEmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtSHEmpNo.Text, txtSHDivision.Text)))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txtSHEmpNo.Text = "";
                    txtSHEmpNo.Focus();
                }
                else
                {
                    if (EmpMaster.IsNotInactive(txtSHEmpNo.Text, txtSHDivision.Text))
                    {
                        //EmpMaster.StrGender = EmpMaster.GetEmployeeGenderByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        //cmbCategory.SelectedValue = EmpMaster.GetEmployeeCategoryByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        txtSHEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtSHEmpNo.Text, txtSHDivision.Text);
                        txtSHWorkCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive", "Invalid Entry");
                        txtSHEmpNo.Text = "";
                        txtSHEmpNo.Focus();
                    }
                }
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {

        }
    }
}