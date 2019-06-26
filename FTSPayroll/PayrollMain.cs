using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OLAXPayrollBL;


namespace OLAXPayroll
{
    public partial class OLAXPayrollMain : Form
    {
        Additions objAdd = new Additions();
        OLAXPayrollBL.PRDeduction objDeduct = new OLAXPayrollBL.PRDeduction();
        OLAXPayrollBL.Company objComp = new OLAXPayrollBL.Company();
        User objEstate = new User();
        OLAXPayrollBL.Department objDept = new OLAXPayrollBL.Department();
        ClsEmployeeMaster objEmp = new ClsEmployeeMaster();
        OLAXPayrollBL.Designation Desi = new OLAXPayrollBL.Designation();
        ReportViewerForm myReportViewer = new ReportViewerForm();

        public OLAXPayrollMain()
        {
            InitializeComponent();
        }

        private void MIAdditionGroupMaster_Click(object sender, EventArgs e)
        {
            PRAdditionGroup objAdditionGroup = new PRAdditionGroup();
            objAdditionGroup.MdiParent = this;
            objAdditionGroup.Show();
        }

        private void MIAdditionsMaster_Click(object sender, EventArgs e)
        {
            PRAddition objAddition = new PRAddition();
            objAddition.MdiParent = this;
            objAddition.Show();
        }

        private void MIOTParameters_Click(object sender, EventArgs e)
        {
            Frm_ot_parameters objOTParam = new Frm_ot_parameters();
            objOTParam.MdiParent = this;
            objOTParam.Show();
        }

        private void MIOvertime_Click(object sender, EventArgs e)
        {
            Frm_over_time objOT = new Frm_over_time();
            objOT.MdiParent = this;
            objOT.Show();
        }

        private void MIcompanyMaster_Click(object sender, EventArgs e)
        {
            Company objCompany = new Company();
            objCompany.MdiParent = this;
            objCompany.Show();
        }
          
        

        private void MIDepartmentMaster_Click(object sender, EventArgs e)
        {
            Department objDept = new Department();
            objDept.MdiParent = this;
            objDept.Show();
        }

        private void MIDesignationMaster_Click(object sender, EventArgs e)
        {
            Designation objDesignation = new Designation();
            objDesignation.MdiParent = this;
            objDesignation.Show();
        }

        private void MIEmployeeMaster_Click(object sender, EventArgs e)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            objEmp.MdiParent = this;
            objEmp.Show();
        }

        private void MIDeductionGroupMaster_Click(object sender, EventArgs e)
        {
            PRDeductionGroupMaster objDeductGroup = new PRDeductionGroupMaster();
            objDeductGroup.MdiParent = this;
            objDeductGroup.Show();
        }

        private void MIDeductionsMaster_Click(object sender, EventArgs e)
        {
            PRDeductionMaster objDeduct = new PRDeductionMaster();
            objDeduct.MdiParent = this;
            objDeduct.Show();
        }

        private void MIBank_Click(object sender, EventArgs e)
        {
            //-----Edited By Janitha--------
            frmBank objBank = new frmBank();
            objBank.MdiParent = this;
            objBank.Show();
        }

        private void MIBankBranch_Click(object sender, EventArgs e)
        {
            BankBranch objBankBranch = new BankBranch();
            objBankBranch.MdiParent = this;
            objBankBranch.Show();
        }

        private void medicalOpningBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedicalOpeningBalances MediOP = new MedicalOpeningBalances();
            MediOP.MdiParent = this;
            MediOP.Show();
        }

        private void noPayDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoPayDetails objNoPayDetails = new NoPayDetails();
            objNoPayDetails.MdiParent = this;
            objNoPayDetails.Show();
        }

        private void MIEarnings_Click(object sender, EventArgs e)
        {
            Frm_emp_additions objEmpAdditions = new Frm_emp_additions();
            objEmpAdditions.MdiParent = this;
            objEmpAdditions.Show();
        }

        private void medicalClaimsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedicalClaims Medi = new MedicalClaims();
            Medi.MdiParent = this;
            Medi.Show();
        }

        private void additionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OLAXPayrollBL.Company cmp = new OLAXPayrollBL.Company();
            ReportViewerForm myReportViewer = new ReportViewerForm();
            DataTable addition = new DataTable();
            addition = objAdd.ALLAdditions();

            DataSet ds = new DataSet();
            ds.Tables.Add(addition);

            if (ds.Tables[0].Rows.Count > 0)
            {
                addition.WriteXml("additionList.xml");

                PRAdditionRPT myAlladdition = new PRAdditionRPT();
                myAlladdition.SetDataSource(ds);
                myReportViewer.Show();
                myAlladdition.SetParameterValue("CompanyName", objComp.getCompanyName());
                myAlladdition.SetParameterValue("Division", User.StrDivision);
                
                myReportViewer.cryRPTViewer.ReportSource = myAlladdition;
                myReportViewer.Show();
            }
        }

        private void deductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet Deduction = new DataSet();
            Deduction = objDeduct.getAllDeduction();

            if (Deduction.Tables[0].Rows.Count > 0)
            {
                Deduction.WriteXml("Deduction.xml");
                PRDeductionRPT DeductionRPT = new PRDeductionRPT();
                DeductionRPT.SetDataSource(Deduction);

                DeductionRPT.SetParameterValue("CompanyName", objComp.getCompanyName());
                DeductionRPT.SetParameterValue("Division", User.StrDivision);
                myReportViewer.cryRPTViewer.ReportSource = DeductionRPT;
                myReportViewer.Show();

            }
            else
            {
                MessageBox.Show("No Data To Preview");
            }
        }

        private void openingCoinsAndDebtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningCoinDebtBalance OPCoinsDebts = new OpeningCoinDebtBalance();
            OPCoinsDebts.MdiParent = this;
            OPCoinsDebts.Show();
        }

        private void additionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_addition_report AddiReport = new Frm_addition_report();
            AddiReport.MdiParent = this;
            AddiReport.Show();

        }

        private void overToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_overtime_report AddiReport = new Frm_overtime_report();
            AddiReport.MdiParent = this;
            AddiReport.Show();
        }

       

        private void salarySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Salary_Summary salSummary = new Frm_Salary_Summary();
            salSummary.MdiParent = this;
            salSummary.Show();
        }

        private void indivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_payslip salSummary = new Frm_payslip();
            salSummary.MdiParent = this;
            salSummary.Show();
        }

        private void MIPreview_Click(object sender, EventArgs e)
        {
            OLAXPayroll.CalculateMonthPayroll CalMonth = new OLAXPayroll.CalculateMonthPayroll();
            CalMonth.MdiParent = this;
            CalMonth.Show();
        }

        private void MIDeductions_Click(object sender, EventArgs e)
        {
            EMPDeductions objEmpDeductions = new EMPDeductions();
            objEmpDeductions.MdiParent = this;
            objEmpDeductions.Show();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsCompanyReport = new DataSet();
            dsCompanyReport = objComp.ListRPTCompany();
            if (dsCompanyReport.Tables[0].Rows.Count > 0)
            {
                dsCompanyReport.WriteXml("Company.xml");

                PRCompanyRPT CompanyRPT = new PRCompanyRPT();
                CompanyRPT.SetDataSource(dsCompanyReport);


                CompanyRPT.SetParameterValue("CompanyName", objComp.getCompanyName());
                CompanyRPT.SetParameterValue("Estate", "Estate :" + objEstate.ListEstates().Rows[0][0].ToString());
                ReportViewerForm objReportViewer = new ReportViewerForm();
                objReportViewer.cryRPTViewer.ReportSource = CompanyRPT;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No data Preview");
            }
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = objDept.ListDepartment();
            dt.TableName = "Department";

            if (dt.Rows.Count > 0)
            {
                dt.WriteXml("DepartmentRPT.xml");
                PRDepartmentRPT DepartmentRPT = new PRDepartmentRPT();
                DepartmentRPT.SetDataSource(dt);


                DepartmentRPT.SetParameterValue("CompanyName", objComp.getCompanyName());
                ReportViewerForm objReportView = new ReportViewerForm();
                objReportView.cryRPTViewer.ReportSource = DepartmentRPT;
                objReportView.Show();
            }
            else
            {
                MessageBox.Show("No data Preview");
            }
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = objEmp.getAllEmployeeDetail();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("EmployeeMasterDetail.xml");
                EmployeeMasterListRPT myReport = new EmployeeMasterListRPT();
                myReport.SetDataSource(ds);
                ReportViewerForm reportViewer = new ReportViewerForm();
                myReport.SetParameterValue("Division", User.StrDivision);
                myReport.SetParameterValue("CompanyName", new OLAXPayrollBL.Company().getCompanyName());
                reportViewer.cryRPTViewer.ReportSource = myReport;
                reportViewer.Show();
            }
            else
                MessageBox.Show("Not data in report!!!");
        }

        private void deductionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_deduction_report DeducReport = new Frm_deduction_report();
            DeducReport.MdiParent = this;
            DeducReport.Show();
        }

        private void noPayDaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frm_noPayDays_report noPayDays = new Frm_noPayDays_report();
            //noPayDays.MdiParent = this;
            //noPayDays.Show();
        }

        private void noPayDaysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_noPayDays_report_Report noPayDays = new Frm_noPayDays_report_Report();
            noPayDays.MdiParent = this;
            noPayDays.Show();
        }

        private void medicalOpeningBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void medicalClaimsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MedicalClaimsReport myMedicalClaims = new MedicalClaimsReport();
            myMedicalClaims.MdiParent = this;
            myMedicalClaims.Show();
        }

        private void medicalBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedicalSummaryDetails myMedicalSummaryDetail = new MedicalSummaryDetails();
            myMedicalSummaryDetail.MdiParent = this;
            myMedicalSummaryDetail.Show();
        }

        private void designationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = Desi.ListDesignation();
            dt.TableName = "Designation";

            if (dt.Rows.Count > 0)
            {
                dt.WriteXml("DesignationRPT.xml");
                PRDesignationRPT DesignationRPT = new PRDesignationRPT();
                DesignationRPT.SetDataSource(dt);


                DesignationRPT.SetParameterValue("CompanyName", objComp.getCompanyName());
                ReportViewerForm reportViewer = new ReportViewerForm();
                reportViewer.cryRPTViewer.ReportSource = DesignationRPT;
                reportViewer.Show();
            }
            else
            {
                MessageBox.Show("No data Preview");
            }
        }

        private void salarySummaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_Salary_Summary salSummary = new Frm_Salary_Summary();
            salSummary.MdiParent = this;
            salSummary.Show();
        }

        private void payslipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_payslip salSummary = new Frm_payslip();
            salSummary.MdiParent = this;
            salSummary.Show();
        }

        private void medicalClaimsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MedicalClaimsReport myMedicalClaims = new MedicalClaimsReport();
            myMedicalClaims.MdiParent = this;
            myMedicalClaims.Show();
        }

        private void medicalSummaryDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedicalSummaryDetails myMedicalSummaryDetail = new MedicalSummaryDetails();
            myMedicalSummaryDetail.MdiParent = this;
            myMedicalSummaryDetail.Show();
        }

        private void payrollReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPayrollSummary objPayrollReportFrm = new FrmPayrollSummary();
            objPayrollReportFrm.MdiParent = this;
            objPayrollReportFrm.Show();
        }

        private void bankRemitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_BankRemittance_RPT bankRemi = new Frm_BankRemittance_RPT();
            bankRemi.MdiParent = this;
            bankRemi.Show();
        }

        private void mnuPayrollWithReimbursement_Click(object sender, EventArgs e)
        {
            rptPayrollWithReimbursment rptReimbst = new rptPayrollWithReimbursment();
            rptReimbst.MdiParent = this;
            rptReimbst.Show();
        }

        private void reports2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paySlipToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmPayslip Payslip = new FrmPayslip();
            Payslip.MdiParent = this;
            Payslip.Show();
        }

        private void ePFReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_EPFMonthlyReport_RPT epfMonthR = new Frm_EPFMonthlyReport_RPT();
            epfMonthR.MdiParent = this;
            epfMonthR.Show();
        }

        private void OLAXPayrollMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ePFETFCPPReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MIProcess_Click(object sender, EventArgs e)
        {

        }

        private void MIProcess_Click_1(object sender, EventArgs e)
        {

        }

        private void eSPSReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ESPSMonthlyReport_RPT epfMonthR = new Frm_ESPSMonthlyReport_RPT();
            epfMonthR.MdiParent = this;
            epfMonthR.Show();

        }

        private void cPPSReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CPPSMonthlyReport_RPT epfMonthR = new Frm_CPPSMonthlyReport_RPT();
            epfMonthR.MdiParent = this;
            epfMonthR.Show();
        }

        private void eTFReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ETFMonthlyReport_RPT etfMonthR = new Frm_ETFMonthlyReport_RPT();
            etfMonthR.MdiParent = this;
            etfMonthR.Show();
        }

        private void ePF6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_EPF6MonthReport_RPT epfMonthR = new Frm_EPF6MonthReport_RPT();
            epfMonthR.MdiParent = this;
            epfMonthR.Show();

        }
            
            
            
        

        private void eTF6MonthReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ETF6MonthReport_RPT epfMonthR = new Frm_ETF6MonthReport_RPT();
            epfMonthR.MdiParent = this;
            epfMonthR.Show();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void OLAXPayrollMain_Load(object sender, EventArgs e)
        {

        }

        private void rptOverTimeReport_Click(object sender, EventArgs e)
        {
            rptOverTime rptObj = new rptOverTime();
            rptObj.MdiParent = this;
            rptObj.Show();
        }

        private void mnuStaffMedicalFund_Click(object sender, EventArgs e)
        {
            rptStaffMedicalFund stfMed = new rptStaffMedicalFund();
            stfMed.MdiParent = this;
            stfMed.Show();
        }

        private void mnuTaxReportSummary_Click(object sender, EventArgs e)
        {
            rptTaxSummaryReport txSum = new rptTaxSummaryReport();
            txSum.MdiParent = this;
            txSum.Show();
        }

        private void mnuAdditionSummaryReport_Click(object sender, EventArgs e)
        {
            rptAdditionSummary adSuma = new rptAdditionSummary();
            adSuma.MdiParent = this;
            adSuma.Show();
        }

        private void mnuDeductionSummaryReport_Click(object sender, EventArgs e)
        {
            rptDeductionSummary deSuma = new rptDeductionSummary();
            deSuma.MdiParent = this;
            deSuma.Show();
        }

        //private void mnuCoinageAnalysis_Click(object sender, EventArgs e)
        //{
        //    rptCoinageAnalysis coni = new rptCoinageAnalysis();
        //    coni.MdiParent = this;
        //    coni.Show();
        //}

        private void mnuCoinageNetSalary_Click(object sender, EventArgs e)
        {
            rptCoinageAnalysis coni = new rptCoinageAnalysis();
            coni.MdiParent = this;
            coni.Show();
        }

        private void mnuCoinageDeduction_Click(object sender, EventArgs e)
        {
            rptCoinageAnalysisDeduction coinDedct = new rptCoinageAnalysisDeduction();
            coinDedct.MdiParent = this;
            coinDedct.Show();
        }

        private void mnuDeductionBreakdown_Click(object sender, EventArgs e)
        {
            rptDeductionBreakdown dedbreak = new rptDeductionBreakdown();
            dedbreak.MdiParent = this;
            dedbreak.Show();
        }

        private void mnuDeductionSignatureList_Click(object sender, EventArgs e)
        {
            rptDeductionSignatureList deLIst = new rptDeductionSignatureList();
            deLIst.MdiParent = this;
            deLIst.Show();
        }

        private void ePFETFEFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
        

        
    }
}