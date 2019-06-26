using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DivisionWiseAdditionRegister : Form
    {
        public DivisionWiseAdditionRegister()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();

        Boolean allemp = true;

        private void DivisionWiseAdditionRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbMonth.DataSource = YMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = YMonth.getLastMonthID();

            rdbAll_CheckedChanged(null, null);

            cmbDivision_SelectedIndexChanged(null, null);
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEmpNo.SelectedIndex != -1)
                {
                    if (!String.IsNullOrEmpty(cmbEmpNo.SelectedValue.ToString()))
                    {
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(cmbDivision.SelectedValue.ToString(), cmbEmpNo.SelectedValue.ToString());
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

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAll.Checked == true)
            {
                cmbEmpNo.Enabled = false;
                allemp = true;
            }
            else
            {
                cmbEmpNo.Enabled = true;
                allemp = false;
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
            FTSPayRollBL.Reports myAddition = new FTSPayRollBL.Reports();

            if (allemp == true)
            {
                DataSet ds = new DataSet();
                myAddition.StrDivisionID = cmbDivision.SelectedValue.ToString();
                myAddition.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myAddition.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                ds = myAddition.DivisionWiseAdditionRegisterAllEmp();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("DivisionwiseAdditionRegister.xml");

                    DivisionwiseAdditionRegisterRPT myAdditionAllEmp = new DivisionwiseAdditionRegisterRPT();
                    myAdditionAllEmp.SetDataSource(ds);
                    myAdditionAllEmp.SetParameterValue("Estate", "Estate : " + mydiv.ListEstates().Rows[0][0].ToString());
                    myAdditionAllEmp.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    //myAdditionAllEmp.SetParameterValue("Period","For the Month of : " + cmbMonth.Text + "  /  " + cmbYear.Text);
                    ReportViewer myReportViewer = new ReportViewer();                    
                    myReportViewer.crystalReportViewer1.ReportSource = myAdditionAllEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            }

            else
            {
                DataSet ds = new DataSet();
                myAddition.StrDivisionID = cmbDivision.SelectedValue.ToString();
                myAddition.IntMonth = Convert.ToInt32(cmbMonth.SelectedValue.ToString());
                myAddition.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                ds = myAddition.DivisionWiseAdditionRegisterAllEmp(cmbEmpNo.SelectedValue.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.WriteXml("DivisionwiseAdditionRegister.xml");

                    DivisionwiseAdditionRegisterRPT myAdditionAllEmp = new DivisionwiseAdditionRegisterRPT();
                    myAdditionAllEmp.SetDataSource(ds);
                    myAdditionAllEmp.SetParameterValue("Estate", "Estate : " + mydiv.ListEstates().Rows[0][0].ToString());
                    myAdditionAllEmp.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    //myAdditionAllEmp.SetParameterValue("Period", "For the Month of : " + cmbMonth.Text + "  /  " + cmbYear.Text);
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myAdditionAllEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            }
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
    }
}