using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class OutstandingRecoveries : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster myDeducMas = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();
        DataTable empNoList = new DataTable();

        public OutstandingRecoveries()
        {
            InitializeComponent();
        }

        

        private void cmdDisplay1_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSetReport = new DataSet();
                dataSetReport = myReports.getOutstandingRecoveries();
                dataSetReport.WriteXml("OutstandingRecoveries.xml");

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    OutstandingRecoveriesRPT myaclist = new OutstandingRecoveriesRPT();
                    myaclist.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    myReportViewer.Show();
                }
                else
                    MessageBox.Show("No data to preview..!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFixDisplay_Click(object sender, EventArgs e)
        {
                try
                {
                    //Add by kalana
                    String strAllDivision = "%";
                    String strAllDeduction = "%";
                    String strAllEmployee = "%";
                    if (!chkAllDivision.Checked)
                        strAllDivision = cmbDivision.SelectedValue.ToString().Trim();
                    if (!chkAllEmployee.Checked)
                        strAllEmployee = txt_employeeNo.Text.ToString().Trim();
                    if (!chkAllDeduction.Checked)
                        strAllDeduction = cmbDeductCode.SelectedValue.ToString().Trim();
                    //Add end 


                    DataSet dataSetReport = new DataSet();
                    dataSetReport = myReports.getOutstandingFixedDeductions(strAllDivision.Trim(), strAllEmployee.Trim(), strAllDeduction.Trim(), 1,Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                    dataSetReport.WriteXml("OutstandingRecoveries1.xml");

                    if (dataSetReport.Tables[0].Rows.Count > 0)
                    {
                        OutstandingRecoveriesRPT1 myaclist = new OutstandingRecoveriesRPT1();
                        myaclist.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();

                        myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                        if (strAllDivision == "%")
                            myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + "All");
                        else
                            myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + strAllDivision);
                        myaclist.SetParameterValue("Recovery", "Fixed Deductions");
                        myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                        myReportViewer.Show();
                    }
                    else
                        MessageBox.Show("No data to preview..!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            


        }

        private void btnLoanDisplay_Click(object sender, EventArgs e)
        {
            
                try
                {
                    //Add by kalana

                    String strAllDivision = "%";
                    String strAllDeduction = "%";
                    String strAllEmployee = "%";
                    if (!chkAllDivision.Checked)
                        strAllDivision = cmbDivision.SelectedValue.ToString().Trim();
                    if (!chkAllEmployee.Checked)
                        strAllEmployee = txt_employeeNo.Text.ToString().Trim();
                    if (!chkAllDeduction.Checked)
                        strAllDeduction = cmbDeductCode.SelectedValue.ToString().Trim();
                    //Add end 

                    DataSet dataSetReport = new DataSet();
                    dataSetReport = myReports.getOutstandingLoans(strAllDivision, strAllDeduction, strAllEmployee,Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString()));
                    dataSetReport.WriteXml("OutstandingRecoveries1.xml");

                    if (dataSetReport.Tables[0].Rows.Count > 0)
                    {
                        OutstandingRecoveriesRPT1 myaclist = new OutstandingRecoveriesRPT1();
                        myaclist.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();

                        myaclist.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                        if (strAllDivision == "%")
                            myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + "All");
                        else
                            myaclist.SetParameterValue("Estate", mydiv.ListEstates().Rows[0][0].ToString() + " / Division: " + strAllDivision);
                        myaclist.SetParameterValue("Recovery", "Loans");
                        myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                        myReportViewer.Show();
                    }
                    else
                        MessageBox.Show("No data to preview..!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();
                empNoList = myMaster.ListAllEmployees(DivisionID);
            }
            catch { }
        }

        private void OutstandingRecoveries_Load(object sender, EventArgs e)
        {
            
            cmbDivision.DataSource = mydiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDeductionGroup.DataSource = myDeducMas.ListDeductionGroups();
            cmbDeductionGroup.DisplayMember = "DeductGroupShortName";
            cmbDeductionGroup.ValueMember = "DeductionGroupId";
        }

        private void txt_employeeNo_Validating(object sender, CancelEventArgs e)
        {
            int empno = 0;
            lbl_empName.Text = "";
            try
            {
                if (Int32.TryParse(txt_employeeNo.Text, out empno))
                {
                    if (txt_employeeNo.Text.Length == 4)
                    {
                        if (chekDivisionEmpNo() == null)
                        {
                            MessageBox.Show("Please check whether the selected division is correct or not");
                            txt_employeeNo.Focus();
                        }
                        else
                            lbl_empName.Text = chekDivisionEmpNo();
                    }
                    else if (txt_employeeNo.Text.Length <= 4)
                    {
                        txt_employeeNo.Text = txt_employeeNo.Text.ToString().PadLeft(5, '0');
                        if (chekDivisionEmpNo() == null)
                        {
                            MessageBox.Show("Please check whether the selected division is correct or not");
                            txt_employeeNo.Focus();
                        }
                        else
                            lbl_empName.Text = chekDivisionEmpNo();
                    }
                    else
                    {
                        MessageBox.Show("Invalid EmpNo please check");
                        txt_employeeNo.Focus();
                    }
                }
                else if (String.IsNullOrEmpty(txt_employeeNo.Text))
                {

                }
                else
                {
                    MessageBox.Show("Invalid EmpNo please check");
                    txt_employeeNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Created by kalana.
        private String chekDivisionEmpNo()
        {
            String empName = null;
            foreach (DataRow dr in empNoList.Rows)
            {
                if (dr[0].ToString().Trim() == txt_employeeNo.Text.ToString().Trim())
                {
                    empName = dr[2].ToString().Trim();
                    break;
                }
            }
            return empName;
        }
        //End kalana.

        private void cmbDeductionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDeductCode.DataSource = myDeducMas.ListDeduction(int.Parse(cmbDeductionGroup.SelectedValue.ToString()));
                cmbDeductCode.DisplayMember = "DeductShortName";
                cmbDeductCode.ValueMember = "DeductCode";

            }
            catch { }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (myDeducMas.IsLoanDeductionGroup(Convert.ToInt32(cmbDeductionGroup.SelectedValue.ToString())))
            {
                btnLoanDisplay_Click(null, null);
            }
            else
                btnFixDisplay_Click(null, null);
        }

        private void btnDisplayrec_Click(object sender, EventArgs e)
        {
            OutstandingRecoveriesForm objORF = new OutstandingRecoveriesForm();
            objORF.Show();
        }
    }
}