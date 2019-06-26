using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class Deduction_Search : Form
    {
        public Deduction_Search()
        {
            InitializeComponent();
        }

        FTSPayRollBL.EstateDivisionBlock myEstateDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.DeductionMaster myDeducMas = new FTSPayRollBL.DeductionMaster();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeMaster myMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.Reports myDeducSearch = new FTSPayRollBL.Reports();
        
        Boolean allDeduc = true;


        private void Deduction_Search_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstateDiv.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
            cmbDivion_SelectedIndexChanged(null, null);

            cmbDeductCode.DataSource = myDeducMas.ListDeducCode();
            cmbDeductCode.DisplayMember = "DeductShortName";
            cmbDeductCode.ValueMember = "DeductShortName";

            cmbMonth.DataSource = myMonth.ListMonthsName();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "Month";

            cmbYear.SelectedIndex = 0;

            label5.Enabled = false;
            cmbFromEmpNo.Enabled = false;
            label6.Enabled = false;
            cmbToEmpNo.Enabled = false;


        }

        private void chkSelfromRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelfromRange.Checked)
            {
                label4.Enabled = false;
                cmbEmpNo.Enabled = false;
                label5.Enabled = true;
                cmbFromEmpNo.Enabled = true;
                label6.Enabled = true;
                cmbToEmpNo.Enabled = true;
                chkAllEmp.Enabled = false;
            }
            else
            {
                label4.Enabled = true;
                cmbEmpNo.Enabled = true;
                label5.Enabled = false;
                cmbFromEmpNo.Enabled = false;
                label6.Enabled = false;
                cmbToEmpNo.Enabled = false;
                chkAllEmp.Enabled = true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDivion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String DivisionID = cmbDivision.SelectedValue.ToString();
                cmbEmpNo.DataSource = null;
                cmbEmpNo.DataSource = myMaster.ListAllEmployees(DivisionID);
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
               
                //cmbEmpNo.SelectedIndexChanged = 1;

                cmbFromEmpNo.DataSource = null;
                cmbFromEmpNo.DataSource = myMaster.ListAllEmployees(DivisionID);
                cmbFromEmpNo.DisplayMember = "EmpNo";
                cmbFromEmpNo.ValueMember = "EmpNo";

                cmbToEmpNo.DataSource = null;
                cmbToEmpNo.DataSource = myMaster.ListAllEmployees(DivisionID);
                cmbToEmpNo.DisplayMember = "EmpNo";
                cmbToEmpNo.ValueMember = "EmpNo";

            }
            catch { }
        }

        private void chkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllEmp.Checked)
            {
                label4.Enabled = false;
                cmbEmpNo.Enabled = false;
                label5.Enabled = false;
                cmbFromEmpNo.Enabled = false;
                label6.Enabled = false;
                cmbToEmpNo.Enabled = false;
                chkSelfromRange.Enabled = false;
            }
            else
            {
                label4.Enabled = true;
                cmbEmpNo.Enabled = true;
                label5.Enabled = false;
                cmbFromEmpNo.Enabled = false;
                label6.Enabled = false;
                cmbToEmpNo.Enabled = false;
                chkSelfromRange.Enabled = true;
            }

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //FTSPayRollBL.Reports myDeducSearch = new FTSPayRollBL.Reports();
            ReportViewer myReportViewer = new ReportViewer();             
            

            if ((chkAllDeduction.Checked) && (chkAllEmp.Checked))
            {

                DataSet AllDeducnAllEmp = new DataSet();

                AllDeducnAllEmp = myDeducSearch.AllDeductAllEmp((cmbDivision.SelectedValue.ToString()), (cmbYear.Text),(cmbMonth.SelectedValue.ToString()));

               
                if (AllDeducnAllEmp.Tables[0].Rows.Count > 0)
                {
                    AllDeducnAllEmp.WriteXml("AllDeductAllEmp.xml");

                    AllDeductAllEmp myAllDeducAllEmp = new AllDeductAllEmp();
                    myAllDeducAllEmp.SetDataSource(AllDeducnAllEmp);

                    myAllDeducAllEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myAllDeducAllEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                    myAllDeducAllEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                    myAllDeducAllEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);

                    myReportViewer.crystalReportViewer1.ReportSource = myAllDeducAllEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }

            }
            else if ((chkAllDeduction.Checked) && (chkSelfromRange.Checked))
            {
                DataSet myRangeAllDeduc = new DataSet();

                myRangeAllDeduc = myDeducSearch.EmpRangeAllDeduc((cmbDivision.SelectedValue.ToString()), (cmbYear.Text), (cmbMonth.SelectedValue.ToString()), (cmbFromEmpNo.SelectedValue.ToString()), (cmbToEmpNo.SelectedValue.ToString()));

                
                if (myRangeAllDeduc.Tables[0].Rows.Count > 0)
                {
                    myRangeAllDeduc.WriteXml("AllDeducRangeEmp.xml");

                    AllDeducRangeEmp myAllDeducRangeEmp = new AllDeducRangeEmp();
                    myAllDeducRangeEmp.SetDataSource(myRangeAllDeduc);

                    myAllDeducRangeEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myAllDeducRangeEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                    myAllDeducRangeEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                    myAllDeducRangeEmp.SetParameterValue("RPTTitle", "All Deductions For Employee From : " + cmbFromEmpNo.Text + "  To : " + cmbToEmpNo.Text);
                    myAllDeducRangeEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);

                    myReportViewer.crystalReportViewer1.ReportSource = myAllDeducRangeEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }
            }

            else if (chkAllEmp.Checked)
            {
                DataSet oneDeducAllEmp = new DataSet();
                if (myDeducSearch.BoolAll == true)
                {
                    oneDeducAllEmp = myDeducSearch.OneDeducAllEmpAllDivision((cmbDivision.SelectedValue.ToString()), (cmbYear.Text), (cmbMonth.SelectedValue.ToString()), (cmbDeductCode.SelectedValue.ToString()));
                }
                else
                {
                    oneDeducAllEmp = myDeducSearch.OneDeducAllEmp((cmbDivision.SelectedValue.ToString()), (cmbYear.Text), (cmbMonth.SelectedValue.ToString()), (cmbDeductCode.SelectedValue.ToString()));
                }
                if (oneDeducAllEmp.Tables[0].Rows.Count > 0)
                {
                    oneDeducAllEmp.WriteXml("oneDeducAllEmp.xml");

                    OneDeducAllEmp myOneDeducAllEmp = new OneDeducAllEmp();
                    myOneDeducAllEmp.SetDataSource(oneDeducAllEmp);

                    myOneDeducAllEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myOneDeducAllEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                    myOneDeducAllEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                    myOneDeducAllEmp.SetParameterValue("RPTTitle", cmbDeductCode.Text + " For All Employees");
                    myOneDeducAllEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);

                    myReportViewer.crystalReportViewer1.ReportSource = myOneDeducAllEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }

            }

            else if (chkAllDeduction.Checked)
            {
                DataSet myAllDeduc = new DataSet();

                myAllDeduc = myDeducSearch.AllDeductEmp((cmbDivision.SelectedValue.ToString()), (cmbYear.Text), (cmbMonth.SelectedValue.ToString()), (cmbEmpNo.SelectedValue.ToString()));

                if (myAllDeduc.Tables[0].Rows.Count > 0)
                {
                    myAllDeduc.WriteXml("EmpAllDeductEmp.xml");

                    AllDeductEmp myAllDeductEmp = new AllDeductEmp();
                    myAllDeductEmp.SetDataSource(myAllDeduc);

                    myAllDeductEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myAllDeductEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                    myAllDeductEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                    myAllDeductEmp.SetParameterValue("RPTTitle", "Deductions for Employee No :" + cmbEmpNo.Text);
                    myAllDeductEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);

                    myReportViewer.crystalReportViewer1.ReportSource = myAllDeductEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }

            }

            else if (chkSelfromRange.Checked)
            {
                DataSet myRangeOneDeduc = new DataSet();

                myRangeOneDeduc = myDeducSearch.EmpRangeOneDeduc((cmbDivision.SelectedValue.ToString()), (cmbYear.Text), (cmbMonth.SelectedValue.ToString()), (cmbDeductCode.SelectedValue.ToString()), (cmbFromEmpNo.SelectedValue.ToString()), (cmbToEmpNo.SelectedValue.ToString()));

                if (myRangeOneDeduc.Tables[0].Rows.Count > 0)
                {
                    myRangeOneDeduc.WriteXml("OneDeducRangeEmp.xml");

                    OneDeducRangeEmp myOneDeducRangeEmp = new OneDeducRangeEmp();
                    myOneDeducRangeEmp.SetDataSource(myRangeOneDeduc);

                    myOneDeducRangeEmp.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                    myOneDeducRangeEmp.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                    myOneDeducRangeEmp.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                    myOneDeducRangeEmp.SetParameterValue("RPTTitle", cmbDeductCode.Text + " For Employee No From :" + cmbFromEmpNo.Text + " To : " + cmbToEmpNo.Text);
                    myOneDeducRangeEmp.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);

                    myReportViewer.crystalReportViewer1.ReportSource = myOneDeducRangeEmp;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview..!");
                }

           }

           else
           {
               DataSet dataSetReport = new DataSet();

               dataSetReport = myDeducSearch.DeducSearch((cmbDivision.SelectedValue.ToString()), (cmbDeductCode.SelectedValue.ToString()), (cmbMonth.SelectedValue.ToString()), (cmbYear.Text), (cmbEmpNo.SelectedValue.ToString()));

               if (dataSetReport.Tables[0].Rows.Count > 0)
               {
                   dataSetReport.WriteXml("EmpDeductionAmount.xml");

                   EmpDeductionAmount myEmpDeduc = new EmpDeductionAmount();
                   myEmpDeduc.SetDataSource(dataSetReport);

                   myEmpDeduc.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                   myEmpDeduc.SetParameterValue("Estate", "Estate :" + myEstateDiv.ListEstates().Rows[0][0].ToString());
                   myEmpDeduc.SetParameterValue("Division", "Division :" + cmbDivision.Text);
                   myEmpDeduc.SetParameterValue("RPTTitle", cmbDeductCode.Text + " For Employee No :" + cmbEmpNo.Text);
                   myEmpDeduc.SetParameterValue("Period", "For the Month of :" + cmbMonth.Text + "  /  " + cmbYear.Text);

                   myReportViewer.crystalReportViewer1.ReportSource = myEmpDeduc;
                   myReportViewer.Show();
               }
               else
               {
                   MessageBox.Show("No Data to Preview..!");
               }
           }       
        }


        private void chkAllDeduction_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDeduction.Checked)
            {
                label2.Enabled = false;
                cmbDeductCode.Enabled = false;
            }
            else
            {
                label2.Enabled = true;
                cmbDeductCode.Enabled = true;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                myDeducSearch.BoolAll = true;
            }
            else
            {
                myDeducSearch.BoolAll = false;
            }
        }

       
    }
}
