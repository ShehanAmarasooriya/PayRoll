using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DivisionWiseDeductionRegister : Form
    {
        public DivisionWiseDeductionRegister()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();

        Boolean allemp = true;

       
        private void DivisionWiseDeductionRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            rdbAll_CheckedChanged(null,null);

            cmbDivision_SelectedIndexChanged(null, null);
         }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();
                cmbEmpNo.DataSource = null;
                cmbEmpNo.DataSource = EmpMaster.ListAllEmployees(DivisionID);
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
                if (cmbEmpNo.SelectedIndex != -1)
                {
                    if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                    {
                        //txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbEmpNo.SelectedValue.ToString());
                        //myHoldloan.StrDivisionId = cmbDivision.SelectedValue.ToString();
                        //myHoldloan.StrEmpNo1 = cmbEmpNo.SelectedValue.ToString();
                        //gvlist.DataSource = myHoldloan.ListHoldByEmpID();
                    }
                }
                else
                {
                    cmbEmpNo.SelectedValue = "N/A";
                    txtEmpName.Text = "N/A";
                }
            }

            catch { }
        }

        private void rdbAll_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void rdbByEmpno_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAll.Checked == true)
            {
                cmbEmpNo.Enabled = false;
                allemp = true;
            }
        }

        private void rdbByEmpno_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbByEmpno.Checked == true)
            {
                cmbEmpNo.Enabled = true;
                allemp = false;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            FTSPayRollBL.Reports myDeduction= new FTSPayRollBL.Reports();
            
            if (allemp == true)
            {
                DataSet ds = new DataSet();
                myDeduction.StrDivisionID = cmbDivision.SelectedValue.ToString();
                myDeduction.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myDeduction.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                ds = myDeduction.DivisionWiseDeductionRegisterAllEmp();
                ds.WriteXml("DivisionWiseDeductionRegisterAllEmp.xml");

                DivisionWiseDeductionRegisterAllEmp myDeductionAllEmp = new DivisionWiseDeductionRegisterAllEmp();
                myDeductionAllEmp.SetDataSource(ds);
                myDeductionAllEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myDeductionAllEmp.SetParameterValue("Estate", "Estate : " + mydiv.ListEstates().Rows[0][0].ToString());
                myDeductionAllEmp.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                myDeductionAllEmp.SetParameterValue("Period", "For the Month of : " + cmbMonth.Text + "  /  " + cmbYear.Text);

                ReportViewer myReportViewer = new ReportViewer();

                myReportViewer.crystalReportViewer1.ReportSource = myDeductionAllEmp;
                myReportViewer.Show();
            }

            else
            {
                DataSet ds = new DataSet();
                myDeduction.StrDivisionID = cmbDivision.SelectedValue.ToString();
                myDeduction.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myDeduction.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                ds = myDeduction.DivisionWiseDeductionRegisterAllEmp(cmbEmpNo.SelectedValue.ToString());
                ds.WriteXml("DivisionWiseDeductionRegisterAllEmp.xml");


                DivisionWiseDeductionRegisterAllEmp myDeductionAllEmp = new DivisionWiseDeductionRegisterAllEmp();
                myDeductionAllEmp.SetDataSource(ds);
                myDeductionAllEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myDeductionAllEmp.SetParameterValue("Estate", "Estate : " + mydiv.ListEstates().Rows[0][0].ToString());
                myDeductionAllEmp.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                myDeductionAllEmp.SetParameterValue("Period", "For the Month of : " + cmbMonth.Text + "  /  " + cmbYear.Text);

                ReportViewer myReportViewer = new ReportViewer();

                myReportViewer.crystalReportViewer1.ReportSource = myDeductionAllEmp;
                myReportViewer.Show();
            }
        }
    }
}