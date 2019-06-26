using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FTSPayrollMain : Form
    {
        FTSPayRollBL.MenuLoader objMenuItem = new FTSPayRollBL.MenuLoader();
        FTSPayRollBL.ListingDetails objListing = new FTSPayRollBL.ListingDetails();
        FTSPayRollBL.EstateDivisionBlock myDiv = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        FTSPayRollBL.SystemSetting ChkSettings = new FTSPayRollBL.SystemSetting();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        FTSPayRollBL.YearMonth myYM = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.FTSCheckRollSettings ObjSettings = new FTSPayRollBL.FTSCheckRollSettings();

        public FTSPayrollMain()
        {
            InitializeComponent();
        }

        private void statutorySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeductionStatutorySettings DeductStatutorySett = new DeductionStatutorySettings();
            DeductStatutorySett.MdiParent = this;
            DeductStatutorySett.Show();
        }

        private void departmentwiseSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeductionDepartmentSettings DeductDeptSett = new DeductionDepartmentSettings();
            DeductDeptSett.MdiParent = this;
            DeductDeptSett.Show();
        }

        private void harvestModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HarvestMode HMode = new HarvestMode();
            HMode.MdiParent = this;
            HMode.Show();
        }

        private void FTSPayrollMain_Load(object sender, EventArgs e)
        {
            //click Once Configurations
            //OlaxToolsSet.SystemInformation SI = new OlaxToolsSet.SystemInformation();


            ////OlaxToolsSet.SystemInformation SI = new OlaxToolsSet.SystemInformation();
            ////SI.dbServer = @"10.10.7.100\SQLEXPRESS";
            ////SI.dbUserId = "sa";
            ////SI.dbPassWord = "pass1234";
            ////SI.module = "Agalawatte Checkroll";
            ////SI.latestVersion = "1.0.0.30";

           
            //SI.dbServer = @"10.10.19.100";
            //SI.dbUserId = "sa";
            //SI.dbPassWord = "pass1234";
            //SI.module = "Agalawatte Checkroll";
            //SI.latestVersion = "1.0.0.41";

            ////SI.dbServer = @"192.168.1.17";
            ////SI.dbUserId = "sa";
            ////SI.dbPassWord = "pass1234";
            ////SI.module = "Agalawatte Checkroll";
            ////SI.latestVersion = "1.0.0.31";



            //OlaxToolsSet.VersionUpdateManage.updateSystem(SI);
            //OlaxToolsSet.VersionUpdateManage.validateVersion(SI);
            //this.Text = OlaxToolsSet.VersionUpdateManage.getCurrentVersion(SI);
            //click Once Configurations end
            // lblVersion.Text = "1.0.0.19";

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                this.lblVersion.Text = ad.CurrentVersion.ToString();
            }

            

            //FTSPayRollBL.User.StrUserName = "";
            //FTSPayRollBL.User.StrUserPassword = "";
            String MainMenuName = "";
            String SubMenuName = "";
            int count = 0;
            int m, n = 0;
            lblLoginStatus.Text = "Welcome, " + FTSPayRollBL.User.StrUserName;
            lblCurrentYearMonth.Text = "Working Year:" + FTSPayRollBL.User.StrYear + " Month:" + FTSPayRollBL.User.StrMonth;

            if (FTSPayRollBL.Company.getCompanyCode().ToUpper().Equals("APL"))
            {
                //this.BackgroundImage = Image.FromFile(Path + @"\ChkRol_BgUi_BPL.jpg");
                this.BackgroundImage = Properties.Resources.ChkRol_BgUi_APL;
                //this.BackgroundImage = Properties.Resources.ChkRl_BgUi_MPL;
            }
            else if (FTSPayRollBL.Company.getCompanyCode().ToUpper().Equals("BPL"))
            {
                //this.BackgroundImage = Image.FromFile(Path + @"\ChkRol_BgUi_BPL.jpg");
                this.BackgroundImage = Properties.Resources.ChkRol_BgUi_BALANGODA;
                //this.BackgroundImage = Properties.Resources.ChkRl_BgUi_MPL;
            }
            else
                //this.BackgroundImage = Image.FromFile(Path + @"\ChkRol_BgUi_MPL.jpg");
                //this.BackgroundImage = Properties.Resources.CR_BgUi;ChkRl_BgUi_MPL
                this.BackgroundImage = Properties.Resources.ChkRl_BgUi_MPL;
                /*Reset Menu*/
                try
                {

                    objMenuItem.DeleteMenuItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, On Delete Menus. " + ex.Message);
                }

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {

                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();
                MainMenuName = item.Text;
                item.Enabled = false;

                try
                {
                    for (n = 0; n < item.DropDownItems.Count; n++)
                    {
                        SubMenuName = "";
                        try
                        {
                            ToolStripMenuItem subitem = new ToolStripMenuItem(item.DropDownItems[n].Text);
                            if (subitem.Text != "")
                            {
                                SubMenuName = item.DropDownItems[n].Text;
                                //subitem.Enabled = true;
                                item.DropDownItems[n].Enabled = false;
                                objMenuItem.InsertMenuItem(MainMenuName, SubMenuName);
                            }

                        }
                        catch
                        {
                        }
                    }

                }
                catch
                {
                    // continue;
                }
                count++;
            }
            /*End Reset Menu*/

            count = 0;
            m = 0;
            n = 0;
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();
                MainMenuName = item.Text;
                item.Enabled = false;

                try
                {
                    for (n = 0; n < item.DropDownItems.Count; n++)
                    {
                        SubMenuName = "";
                        try
                        {
                            ToolStripMenuItem subitem = new ToolStripMenuItem(item.DropDownItems[n].Text);
                            if (subitem.Text != "")
                            {
                                SubMenuName = item.DropDownItems[n].Text;
                                //subitem.Enabled = true;
                                item.DropDownItems[n].Enabled = false;
                            }

                        }
                        catch
                        {
                        }
                    }

                }
                catch
                {
                    // continue;
                }
                count++;
            }

            addMenus();
            FTSPayRollBL.User.BoolDayBlockAvailable = ChkSettings.IsOldDataEntryBlocked();

            /*disable inactivation*/
            if(ObjSettings.IsAvailableAutoStatusChange())
            {
            EmployeeStatusChange objEmpStatusUpdate = new EmployeeStatusChange(2);
            objEmpStatusUpdate.MdiParent = this;
            objEmpStatusUpdate.Show();
            objEmpStatusUpdate.Close();
            }

        }

        //private Version GetRunningVersion()
        //{
        //    try
        //    {
        //        return Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
        //    }
        //    catch
        //    {
        //        return Assembly.GetExecutingAssembly().GetName().Version;
        //    }
        //}

        private void dailyHarvestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DailyHarvest dailyHarvest = new DailyHarvest();
            dailyHarvest.MdiParent = this;
            dailyHarvest.Show();
        }

        private void monthlyWegesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessMonthlyWeges ProcessMWeges = new ProcessMonthlyWeges();
            ProcessMWeges.MdiParent = this;
            ProcessMWeges.Show();
        }

        private void monthlyWegesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reports.ReportMonthlyWeges repMonthlyWeges = new FTSPayroll.Reports.ReportMonthlyWeges();
            repMonthlyWeges.MdiParent = this;
            repMonthlyWeges.Show();
        }

        private void loanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Loan EmpLoan = new Loan();
            //EmpLoan.MdiParent = this;
            //EmpLoan.Show();
        }

        private void overtimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OverTime OT = new OverTime();
            OT.MdiParent = this;
            OT.Show();
        }

        private void deductionAssignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeDeduction EmpDeduct = new EmployeeDeduction();
            EmpDeduct.MdiParent = this;
            EmpDeduct.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login userLogin = new Login();
            userLogin.MdiParent = this;
            userLogin.Show();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeMaster EmpMaster = new EmployeeMaster();
            EmpMaster.MdiParent = this;
            EmpMaster.Show();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            ExtraRates exRates = new ExtraRates();
            exRates.MdiParent = this;
            exRates.Show();
        }

        private void oTFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OTParameters OTParams = new OTParameters();
            OTParams.MdiParent = this;
            OTParams.Show();
        }

        private void employeeGangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeGang EmpGang = new EmployeeGang();
            EmpGang.MdiParent = this;
            EmpGang.Show();
        }

        private void employeeUnionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeUnionMasters myUnion = new EmployeeUnionMasters();
            myUnion.MdiParent = this;
            myUnion.Show();
        }

        private void fundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeFund EmpFund = new EmployeeFund();
            EmpFund.MdiParent = this;
            EmpFund.Show();
        }

        private void fixedParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FixedParameters myFixedParameters = new FixedParameters();
            myFixedParameters.MdiParent = this;
            myFixedParameters.Show();
        }

        private void estateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void divisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Division myDivision = new Division();
            myDivision.MdiParent = this;
            myDivision.Show();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Field myfield = new Field();
            myfield.MdiParent = this;
            myfield.Show();
        }

        private void dailyWorkDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeWorkAttendanceForm myWorkAtt = new EmployeeWorkAttendanceForm();
            myWorkAtt.MdiParent = this;
            myWorkAtt.Show();
            //DailyWorkDistribution myDailyWorkDistribution = new DailyWorkDistribution();
            //myDailyWorkDistribution.MdiParent = this;
            //myDailyWorkDistribution.Show();
        }

        private void intakePerPluckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IntakePerPlucker myIntakePerPlucker = new IntakePerPlucker();
            myIntakePerPlucker.MdiParent = this;
            myIntakePerPlucker.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FixedDeductions myFixedDeductions = new FixedDeductions();
            myFixedDeductions.MdiParent = this;
            myFixedDeductions.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RiceFlourTeaDeductions RFTDeduct = new RiceFlourTeaDeductions();
            RFTDeduct.MdiParent = this;
            RFTDeduct.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SkipDeduction mySkipDeduction = new SkipDeduction();
            mySkipDeduction.MdiParent = this;
            mySkipDeduction.Show();
        }

        private void loanHoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoldLoan myHoldLoan = new HoldLoan();
            myHoldLoan.MdiParent = this;
            myHoldLoan.Show();
        }

        public void addMenus()
        {
            //DataTable myTable = objMenuItem.getMenus().Tables[0];
            DataTable myTable = objMenuItem.getRoleMenus().Tables[0];
            int count = 0;
            int m, n = 0;
            for (int i = 0; i < myTable.Rows.Count; i++)
            {
                foreach (ToolStripMenuItem item in menuStrip1.Items)
                {
                    List<string> list1 = new List<string>();
                    List<string> list2 = new List<string>();

                    if (myTable.Rows[i][1].ToString() == item.Text)
                    {
                        item.Enabled = true;
                    }

                    try
                    {
                        for (n = 0; n < item.DropDownItems.Count; n++)
                        {
                            try
                            {
                                ToolStripMenuItem subitem = new ToolStripMenuItem(item.DropDownItems[n].Text);
                                if (subitem.Text != "")
                                {
                                    //list1.Add(subitem.Text);
                                    if ((myTable.Rows[i][0].ToString() == subitem.Text) && (myTable.Rows[i][1].ToString() == item.Text))
                                    {
                                        //if (subitem.Text == "Payment Checkroll" || subitem.Text == "Net Pay Signature List")
                                        //if(item.Text=="Reports")
                                        //{
                                            //subitem.Enabled = true;
                                            item.DropDownItems[n].Enabled = true;
                                        //}
                                    }

                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch
                    {
                        // continue;
                    }
                    count++;
                }
            }

        }

        private void divisionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataSet dsDivisionReport = new DataSet();
            dsDivisionReport = objListing.getEstatesDivisions();
            dsDivisionReport.WriteXml("EstateDivisions.xml");

            EstateDivisionReport objReport = new EstateDivisionReport();
            objReport.SetDataSource(dsDivisionReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void estateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataSet dsEstatesReport = new DataSet();
            dsEstatesReport = objListing.getEstatesDetails();
            dsEstatesReport.WriteXml("EstatesDetails.xml");

            EstatesDetailsReport objReport = new EstatesDetailsReport();
            objReport.SetDataSource(dsEstatesReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void categoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataSet dsEmpCategoryReport = new DataSet();
            dsEmpCategoryReport = objListing.getEmployeeCategory();
            dsEmpCategoryReport.WriteXml("EmployeeCategory.xml");

            EmployeeCategoryReport objReport = new EmployeeCategoryReport();
            objReport.SetDataSource(dsEmpCategoryReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void overtimeParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsOvertimeParameterReport = new DataSet();
            dsOvertimeParameterReport = objListing.getOvertimeParameters();
            dsOvertimeParameterReport.WriteXml("OvertimeParameter.xml");

            OvertimeParameterReport objReport = new OvertimeParameterReport();
            objReport.SetDataSource(dsOvertimeParameterReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void fixedParametersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsParametersAndRatesListing objParams = new SettingsParametersAndRatesListing();
            objParams.MdiParent=this;
            objParams.Show();
            //DataSet dsFixedParameterReport = new DataSet();
            //dsFixedParameterReport = objListing.getFixedParameters();
            //dsFixedParameterReport.WriteXml("FixedParameter.xml");

            //FixedParameterReport objReport = new FixedParameterReport();
            //objReport.SetDataSource(dsFixedParameterReport);
            //ReportViewerForm objReportViewer = new ReportViewerForm();

            //objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            //objReportViewer.crystalReportViewer1.ReportSource = objReport;
            //objReportViewer.Show();
        }

        private void jobToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataSet dsJobReport = new DataSet();
            dsJobReport = objListing.getJob();
            dsJobReport.WriteXml("Job.xml");

            JobReport objReport = new JobReport();
            objReport.SetDataSource(dsJobReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void employeeGangsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsEmpGangsReport = new DataSet();
            dsEmpGangsReport = objListing.getEmployeeGangs();
            dsEmpGangsReport.WriteXml("EmpGangs.xml");

            EmpGangsReport objReport = new EmpGangsReport();
            objReport.SetDataSource(dsEmpGangsReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void employeeUnionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeUnions myEmpUnions = new EmployeeUnions();
            myEmpUnions.MdiParent = this;
            myEmpUnions.Show();
        }

        private void employeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //EmployeeList myEmpList = new EmployeeList();
            //myEmpList.MdiParent = this;
            //myEmpList.Show();
            EmployeeListReport myEmpList = new EmployeeListReport();
            myEmpList.MdiParent = this;
            myEmpList.Show();


        }

        private void loanToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataSet dsLoanReport = new DataSet();
            dsLoanReport = objListing.getLoanDetails();
            dsLoanReport.WriteXml("LoanDetails.xml");

            LoanReport objReport = new LoanReport();
            objReport.SetDataSource(dsLoanReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", FTSPayRollBL.User.StrEstate);
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void fundsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataSet dsFundsReport = new DataSet();
            dsFundsReport = objListing.getFundsDetails();
            dsFundsReport.WriteXml("FundsDetails.xml");

            FundsReport objReport = new FundsReport();
            objReport.SetDataSource(dsFundsReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", FTSPayRollBL.User.StrEstate);
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void deductionsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataSet dsDuctionsReport = new DataSet();
            dsDuctionsReport = objListing.getDeductionsDetails();
            dsDuctionsReport.WriteXml("DuctionsDetails.xml");

            DuctionsReport objReport = new DuctionsReport();
            objReport.SetDataSource(dsDuctionsReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", FTSPayRollBL.User.StrEstate);
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void fieldToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            DataSet dsFieldsReport = new DataSet();
            dsFieldsReport = objListing.getEstatesFields();
            dsFieldsReport.WriteXml("EstateFields.xml");

            EstateFieldReport objReport = new EstateFieldReport();
            objReport.SetDataSource(dsFieldsReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            //myteabookprint.SetParameterValue("FromDate", dtFrom.Value.Date);
            //myteabookprint.SetParameterValue("ToDate", dtFrom.Value.Date);
            objReport.SetParameterValue("Estate", FTSPayRollBL.User.StrEstate);
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();

        }

        private void additionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdditionsAndAllowances myaddition = new AdditionsAndAllowances();
            myaddition.MdiParent = this;
            myaddition.Show();
        }

        private void paymentCheckrollToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paymentCheckrollToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            PaymentCheckRoll myPayment = new PaymentCheckRoll();
            myPayment.MdiParent = this;
            myPayment.Show();
        }

        private void divisionWiseAmalgamationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DivisionWiseAmalmagation myReport = new DivisionWiseAmalmagation();
            //myReport.MdiParent = this;
            //myReport.Show();a
            AmalgamationRPT myAmalga = new AmalgamationRPT();
            myAmalga.MdiParent = this;
            myAmalga.Show();
        }

        private void dailyAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeAttendance myAttendance = new EmployeeAttendance();
            myAttendance.MdiParent = this;
            myAttendance.Show();
        }

        private void dailyWorkInDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DailyWorkDistributionDetail myDetail = new DailyWorkDistributionDetail();
            myDetail.MdiParent = this;
            myDetail.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            GreenLeafRegister myRegister = new GreenLeafRegister();
            myRegister.MdiParent = this;
            myRegister.Show();
        }

        private void paySlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaySilpPrinting mySlip = new PaySilpPrinting();
            mySlip.MdiParent = this;
            mySlip.Show();
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessPreviewMonthlyWages PreviewWages = new ProcessPreviewMonthlyWages();
            PreviewWages.MdiParent = this;
            PreviewWages.Show();
        }

        private void checkrollReconcilationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckRollReconiliation myForm = new CheckRollReconiliation();
            myForm.MdiParent = this;
            myForm.Show();
        }

        private void denominationCoinAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoinAnalysis myCoin = new CoinAnalysis();
            myCoin.MdiParent = this;
            myCoin.Show();
        }

        private void additionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdditionMaster myAddition = new AdditionMaster();
            myAddition.MdiParent = this;
            myAddition.Show();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            DivisionWiseEPFETFSummary mySummary = new DivisionWiseEPFETFSummary();
            mySummary.MdiParent = this;
            mySummary.Show();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Click YES For Bulk Addition Entry, NO For Individual Entry  ", "Selection Of Entry Type",
     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
     MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No))
            {

                Additions myAdditionstrans = new Additions();
                myAdditionstrans.MdiParent = this;
                myAdditionstrans.Show();
            }

            else
            {

                AdditionBulkFRM myBulkAdd = new AdditionBulkFRM();
                myBulkAdd.MdiParent = this;
                myBulkAdd.Show();
            }
        }

        private void FTSPayrollMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void belowNormAboveNormSummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NormSummary myNorm = new NormSummary();
            myNorm.MdiParent = this;
            myNorm.Show();
        }

        private void netPaySiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void netPaySiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            EmployeeNetPay mynetPay = new EmployeeNetPay();
            mynetPay.MdiParent = this;
            mynetPay.Show();
        }

        private void diductionSummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DivisionWiseDeductionRegister myregister = new DivisionWiseDeductionRegister();
            myregister.MdiParent = this;
            myregister.Show();
        }

        private void dailyDeductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            OutstandingRecoveries myRecoveris = new OutstandingRecoveries();
            myRecoveris.MdiParent = this;
            myRecoveris.Show();
        }

        private void employeeWiseAdditionSummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DivisionWiseAdditionRegister myAdditionReg = new DivisionWiseAdditionRegister();
            myAdditionReg.MdiParent = this;
            myAdditionReg.Show();
        }

        private void ePFETFMonthlyRemittanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPFMonthlyRemittence myEPF = new EPFMonthlyRemittence();
            myEPF.MdiParent = this;
            myEPF.Show();
        }

        private void harvestEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HarvestRegister myHarvest = new HarvestRegister();
            myHarvest.MdiParent = this;
            myHarvest.Show();
        }

        private void overtimeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OverTimeRegister myRegister = new OverTimeRegister();
            myRegister.MdiParent = this;
            myRegister.Show();
        }

        private void additionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AdditionsRegister myRegister = new AdditionsRegister();
            myRegister.MdiParent = this;
            myRegister.Show();
        }

        private void deductionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeductionRegister myRegister = new DeductionRegister();
            myRegister.MdiParent = this;
            myRegister.Show();
        }

        private void skippedDeductionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkipDeductionRegister mySkip = new SkipDeductionRegister();
            mySkip.MdiParent = this;
            mySkip.Show();
        }

        private void loanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoanRegister myRegister = new LoanRegister();
            myRegister.MdiParent = this;
            myRegister.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            EmployeeMovement myMovements = new EmployeeMovement();
            myMovements.MdiParent = this;
            myMovements.Show();
        }

        private void debtorsForCurrentMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebtorList myList = new DebtorList();
            myList.MdiParent = this;
            myList.Show();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            EmployeeGratuity myG = new EmployeeGratuity();
            myG.MdiParent = this;
            myG.Show();
        }

        private void ePFETFSixMonthReportEmployeewiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ETF6MonthsReport myReport = new ETF6MonthsReport();
            //myReport.MdiParent = this;
            //myReport.Show();
            EPFETF6MonthReport myEPFETF6 = new EPFETF6MonthReport();
            myEPFETF6.MdiParent = this;
            myEPFETF6.Show();
        }

        private void processToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcessMonthlyWeges ProcessWages = new ProcessMonthlyWeges();
            ProcessWages.MdiParent = this;
            ProcessWages.Show();
        }



        private void loanDirectPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanDirectPayment LDirectPayment = new LoanDirectPayment();
            LDirectPayment.MdiParent = this;
            LDirectPayment.Show();
        }

        private void employeeStatusUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EmployeeCurrentStatus EmpStatus = new EmployeeCurrentStatus();
            //EmpStatus.MdiParent = this;
            //EmpStatus.Show();
            EmployeeStatusChange objStatusChange = new EmployeeStatusChange(3);
            objStatusChange.MdiParent = this;
            objStatusChange.Show();
        }

        private void notOfferedEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotOfferedEntry NotOffered = new NotOfferedEntry();
            NotOffered.MdiParent = this;
            NotOffered.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            EmployeeWorkHistory myEmpWrkHis = new EmployeeWorkHistory();
            myEmpWrkHis.MdiParent = this;
            myEmpWrkHis.Show();


        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword mychangePass = new ChangePassword();
            mychangePass.MdiParent = this;
            mychangePass.Show();
        }

        private void userSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserProfile myUserProfile = new UserProfile();
            myUserProfile.MdiParent = this;
            myUserProfile.Show();
        }

        private void jobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JobGroup jbGroup = new JobGroup();
            jbGroup.MdiParent = this;
            jbGroup.Show();
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            JobMaster jbMaster = new JobMaster();
            jbMaster.MdiParent = this;
            jbMaster.Show();
        }

        private void deductionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeductionGroup DeductGroup = new DeductionGroup();
            DeductGroup.MdiParent = this;
            DeductGroup.Show();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            DeductionMaster DeductMaster = new DeductionMaster();
            DeductMaster.MdiParent = this;
            DeductMaster.Show();
        }

        private void deductionSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Deduction_Search myDeductSearch = new Deduction_Search();
            //myDeductSearch.MdiParent = this;
            //myDeductSearch.Show();

            DeductionSearchReport myDeductSearch = new DeductionSearchReport();
            myDeductSearch.MdiParent = this;
            myDeductSearch.Show();
        }

        private void employeeStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void monthlyHolidayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MonthlyHoliday myHoliday = new MonthlyHoliday();
            //myHoliday.MdiParent = this;
            //myHoliday.Show();

        }

        private void monthlyDivisionNormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DivisionWiseNorm DivisionNorm = new DivisionWiseNorm();
            //DivisionNorm.MdiParent = this;
            //DivisionNorm.Show();
            FieldWiseNorm FieldNorm = new FieldWiseNorm();
            FieldNorm.MdiParent = this;
            FieldNorm.Show();
        }

        private void monthlyHolidaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyHoliday myHoliday = new MonthlyHoliday();
            myHoliday.MdiParent = this;
            myHoliday.Show();

        }

        private void addExtraNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddExtraNames myAddExName = new AddExtraNames();
            myAddExName.MdiParent = this;
            myAddExName.Show();
        }

        private void postToGLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GLProcess glProc = new GLProcess();
            glProc.MdiParent = this;
            glProc.Show();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            Daily_Harvest_Register Dreg = new Daily_Harvest_Register();
            Dreg.MdiParent = this;
            Dreg.Show();
        }

        private void dailyHarvestCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DailyHarvestCW DHarvestCW = new DailyHarvestCW();
            DHarvestCW.MdiParent = this;
            DHarvestCW.Show();
        }

        private void rFTDeductionRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RFTDeductionRegister myRft = new RFTDeductionRegister();
            myRft.MdiParent = this;
            myRft.Show();
        }

        private void fixedDeductionRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FixedDeductionRegister myFixDeduct = new FixedDeductionRegister();
            myFixDeduct.MdiParent = this;
            myFixDeduct.Show();
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("here");
            DailyWorkDistribution1 myWorkDis1 = new DailyWorkDistribution1();
            myWorkDis1.MdiParent = this;
            myWorkDis1.Show();
            //WorkDistributionInManDays myWorkDistribution = new WorkDistributionInManDays();
            //myWorkDistribution.MdiParent = this;
            //myWorkDistribution.Show();
        }

        private void loanDeductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanDeductions myLoan = new LoanDeductions();
            myLoan.MdiParent = this;
            myLoan.Show();
        }

        private void loanDeductionRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanDeductionRegister myLoanReg = new LoanDeductionRegister();
            myLoanReg.MdiParent = this;
            myLoanReg.Show();
        }

        private void fixedAdditionRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FixedAdditionRegister myFixAdd = new FixedAdditionRegister();
            myFixAdd.MdiParent = this;
            myFixAdd.Show();
        }

        private void offeredDaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyWorkOfferedDays myWorkOffered = new MonthlyWorkOfferedDays();
            myWorkOffered.MdiParent = this;
            myWorkOffered.Show();
        }

        private void dailyNotOfferedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bulkNotOfferedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DailyNotOffered myDNon = new DailyNotOffered();
            myDNon.MdiParent = this;
            myDNon.Show();
        }

        private void systemUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemSqlDBUpdates myUpdates = new SystemSqlDBUpdates();
            myUpdates.MdiParent = this;
            myUpdates.Show();
        }

        private void cancelProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelMonthlyProcess myCancelProcess = new CancelMonthlyProcess();
            myCancelProcess.MdiParent = this;
            myCancelProcess.Show();
        }

        private void MonthlyAdvRpt_Click(object sender, EventArgs e)
        {
            MonthlyAdvanceReport myMOADV = new MonthlyAdvanceReport();
            myMOADV.MdiParent = this;
            myMOADV.Show();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            CheckrollWages myWagesRep = new CheckrollWages();
            myWagesRep.MdiParent = this;
            myWagesRep.Show();
        }

        private void changeEmployeeUnionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnionDeduction myUnionDeduct = new UnionDeduction();
            myUnionDeduct.MdiParent = this;
            myUnionDeduct.Show();
        }

        private void LoanList_Click(object sender, EventArgs e)
        {
            DeductionList myLoanDeductionList = new DeductionList();
            myLoanDeductionList.MdiParent = this;
            myLoanDeductionList.Show();
        }

        private void LentLabourSummery_Click(object sender, EventArgs e)
        {
            LentLabourCostRep myLentLabourRep = new LentLabourCostRep();
            myLentLabourRep.MdiParent = this;
            myLentLabourRep.Show();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            FieldWiseReport myFieldWise = new FieldWiseReport();
            myFieldWise.MdiParent = this;
            myFieldWise.Show();
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            GreenLeafSummery myLeafSumm = new GreenLeafSummery();
            myLeafSumm.MdiParent = this;
            myLeafSumm.Show();
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            EmployeeWiseEPFETFSummery myEmpEpfSummery = new EmployeeWiseEPFETFSummery();
            myEmpEpfSummery.MdiParent = this;
            myEmpEpfSummery.Show();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            HolidayPayData myHPayData = new HolidayPayData();
            myHPayData.MdiParent = this;
            myHPayData.Show();
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            EmployeeWiseEpfEtfSummery2 myepfSumm2 = new EmployeeWiseEpfEtfSummery2();
            myepfSumm2.MdiParent = this;
            myepfSumm2.Show();
        }

        private void toolStripMenuItem23_Click_1(object sender, EventArgs e)
        {
            BlockPluckingRegister myreg = new BlockPluckingRegister();
            myreg.MdiParent = this;
            myreg.Show();
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            BlockPluckingReport myBlkrep = new BlockPluckingReport();
            myBlkrep.MdiParent = this;
            myBlkrep.Show();
        }

        private void openeBlockedDatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBlockedDates myOpenBlocked = new OpenBlockedDates();
            myOpenBlocked.MdiParent = this;
            myOpenBlocked.Show();
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            NewSystemUpdate myupdate = new NewSystemUpdate();
            myupdate.MdiParent = this;
            myupdate.Show();
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            DailyHarvestContractCW myHarvestContract = new DailyHarvestContractCW();
            myHarvestContract.MdiParent = this;
            myHarvestContract.Show();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            ContractCashWorkRegister myCCReg = new ContractCashWorkRegister();
            myCCReg.MdiParent = this;
            myCCReg.Show();
        }

        private void menuItemEPFNoChange_Click(object sender, EventArgs e)
        {
            EmployeeDetailsList myEmpDetails = new EmployeeDetailsList();
            myEmpDetails.MdiParent = this;
            myEmpDetails.Show();
        }

        private void inactiveMadeUpCoinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InactiveMadeUpCoinsTransfer myMadeup = new InactiveMadeUpCoinsTransfer();
            myMadeup.MdiParent = this;
            myMadeup.Show();
        }

        private void holidayPayDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HolidayPay myHPay = new HolidayPay();
            myHPay.MdiParent = this;
            myHPay.Show();
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            ResetHolidaypay ReHPay = new ResetHolidaypay();
            ReHPay.Show();
        }

        private void holidaypayProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HolidaypayProcess HPProcess = new HolidaypayProcess();
            HPProcess.Show();
        }

        private void holidaypayDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HolidayPay myHPay = new HolidayPay();
            myHPay.MdiParent = this;
            myHPay.Show();
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            HolidaypayFinalReports HPFinalRpt = new HolidaypayFinalReports();
            HPFinalRpt.MdiParent = this;
            HPFinalRpt.Show();
        }

        private void toolStripMenuItemGuaranteeRecovery_Click(object sender, EventArgs e)
        {
            GuaranteeRecoveryReport myGuaranteeRecovery = new GuaranteeRecoveryReport();
            myGuaranteeRecovery.MdiParent = this;
            myGuaranteeRecovery.Show();
        }

        private void resetGuaranteeRecoveryDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGuaranteeRecoveryData myGuaranteeReset = new ResetGuaranteeRecoveryData();
            myGuaranteeReset.MdiParent = this;
            myGuaranteeReset.Show();
        }

        private void gratuitypayDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GratuityData myGratuity = new GratuityData();
            myGratuity.MdiParent = this;
            myGratuity.Show();

        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            GratuitypayFinalReport gp = new GratuitypayFinalReport();
            gp.MdiParent = this;
            gp.Show();
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            EmployeeChildrenRegister empchiReg = new EmployeeChildrenRegister();
            empchiReg.MdiParent = this;
            empchiReg.Show();
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            EmployeeChildrens empChi = new EmployeeChildrens();
            empChi.MdiParent = this;
            empChi.Show();
        }

        private void cashKiloRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashKiloRegister myKilosReg = new CashKiloRegister();
            myKilosReg.MdiParent = this;
            myKilosReg.Show();
        }

        private void areaCoveredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdjustAreaCoveredFieldWeight myAreaCover = new AdjustAreaCoveredFieldWeight();
            myAreaCover.MdiParent = this;
            myAreaCover.Show();
        }

        private void confirmationDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashboardDataConfirmationRegister myDataConfirm = new DashboardDataConfirmationRegister();
            myDataConfirm.MdiParent = this;
            myDataConfirm.Show();
        }

        private void menuBlkPlk2013Reg_Click(object sender, EventArgs e)
        {
            BlockPlk2013Register BlkPlkReg = new BlockPlk2013Register();
            BlkPlkReg.MdiParent = this;
            BlkPlkReg.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            MenWomenMandaysReport myManDays = new MenWomenMandaysReport();
            myManDays.MdiParent = this;
            myManDays.Show();
        }

        private void easyweighToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadData objDownload = new DownloadData();
            objDownload.MdiParent = this;
            objDownload.Show();
        }

        private void easyWeighDailyHarvestRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EasyweighDailyHarvestRegister objEasyweighRegister = new EasyweighDailyHarvestRegister();
            objEasyweighRegister.MdiParent = this;
            objEasyweighRegister.Show();
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            DailyHarvestCW1 myDHarvestCWOkg = new DailyHarvestCW1();
            myDHarvestCWOkg.MdiParent = this;
            myDHarvestCWOkg.Show();
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            DeductionSearchReportbyEmoRange dedbyRange = new DeductionSearchReportbyEmoRange();
            dedbyRange.MdiParent = this;
            dedbyRange.Show();
        }

        private void MenuPlkKiloReg_Click(object sender, EventArgs e)
        {

        }

        private void checkrollTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckrollTransferToHO DataTrn = new CheckrollTransferToHO();
            DataTrn.MdiParent = this;
            DataTrn.Show();
        }

        private void employerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EPFEmployerMaster myEmployer = new EPFEmployerMaster();
            myEmployer.MdiParent = this;
            myEmployer.Show();
        }

        private void addOtherEPFPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherEPFAdditions objOthEPF = new OtherEPFAdditions();
            objOthEPF.MdiParent = this;
            objOthEPF.Show();
        }

        private void DailyWorkAnalysis_Click(object sender, EventArgs e)
        {
            DailyWorkAnalysis DailyAnalysis = new DailyWorkAnalysis();
            DailyAnalysis.MdiParent = this;
            DailyAnalysis.Show();
        }

        private void CashNamePlkRegister_Click(object sender, EventArgs e)
        {
            CashNamePluckingRegister NamePlkReg = new CashNamePluckingRegister();
            NamePlkReg.MdiParent = this;
            NamePlkReg.Show();
        }

        private void EntryConfirmation_Click(object sender, EventArgs e)
        {
            EntryConfirmation EntryConfirm = new EntryConfirmation();
            EntryConfirm.MdiParent = this;
            EntryConfirm.Show();
        }

        private void backUpDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackUpDataBases BakDB = new BackUpDataBases();
            BakDB.MdiParent = this;
            BakDB.Show();
        }

        private void previousDebtsAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviousDebtsAdjust adjustDebt = new PreviousDebtsAdjust();
            adjustDebt.MdiParent = this;
            adjustDebt.Show();
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            if (FTSPayRollBL.User.StrUserName.ToUpper() == "ADMIN")
            {
                GrantCHKRollSystemSettings myObj = new GrantCHKRollSystemSettings();
                myObj.MdiParent = this;
                myObj.Show();
            }
            else
            {
                MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void checkrollSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FTSPayRollBL.User.StrUserName.ToUpper() == "ADMIN")
            {
                SystemSetting myObj = new SystemSetting();
                myObj.MdiParent = this;
                myObj.Show();
            }
            else
            {
                MessageBox.Show("Permission Denied!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void miDailyHarvestRubber_Click(object sender, EventArgs e)
        {
            DailyHarvestRubber FrmDharvestRub = new DailyHarvestRubber();
            FrmDharvestRub.MdiParent = this;
            FrmDharvestRub.Show();
        }

        private void MIDailyHarvestRegRubber_Click(object sender, EventArgs e)
        {
            DailyHarvestRgisterRubber frmDHRegRub = new DailyHarvestRgisterRubber();
            frmDHRegRub.MdiParent = this;
            frmDHRegRub.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backupOldDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void miDailyHarvestRubberCW_Click(object sender, EventArgs e)
        {
            DailyHarvestRubberCW myDHRubberCW = new DailyHarvestRubberCW();
            myDHRubberCW.MdiParent = this;
            myDHRubberCW.Show();
        }

        private void auditReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuditReports myAudit = new AuditReports();
            myAudit.MdiParent = this;
            myAudit.Show();
        }



        private void updateCashBlockPluckingRateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UpdateCashBlockPluckingRate myUpdateCashBlokPlkRate = new UpdateCashBlockPluckingRate();
            myUpdateCashBlokPlkRate.MdiParent = this;
            myUpdateCashBlokPlkRate.Show();
        }

        private void MI_MusterChit_Click(object sender, EventArgs e)
        {
            MusterChitEntry MChit = new MusterChitEntry();
            MChit.MdiParent = this;
            MChit.Show();
        }

        private void openingCoinsDebtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningCoinDebtBalance OPCoinsDebts = new OpeningCoinDebtBalance();
            OPCoinsDebts.MdiParent = this;
            OPCoinsDebts.Show();
        }

        private void downloadOpenedDaysToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dailyFieldSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FieldWiseSummaryEntry objFieldSummary = new FieldWiseSummaryEntry();
            objFieldSummary.MdiParent = this;
            objFieldSummary.Show();
        }

        private void MIAddFreeNames_Click(object sender, EventArgs e)
        {
            AddExtraNames objExtraNames = new AddExtraNames();
            objExtraNames.MdiParent = this;
            objExtraNames.Show();
        }

        private void MIAddPHFreeNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MIInterEstateBorrowed_Click(object sender, EventArgs e)
        {
            InterEstateBorrowedLabour objIE = new InterEstateBorrowedLabour();
            objIE.MdiParent = this;
            objIE.Show();
        }

        private void MIDeductions_Click(object sender, EventArgs e)
        {
            Deduction objDeduction = new Deduction();
            objDeduction.MdiParent = this;
            objDeduction.Show();
        }

        private void toolStripMenuItem1_Click_2(object sender, EventArgs e)
        {

        }

        private void registersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MIWorkDistributionMainCodeWise_Click(object sender, EventArgs e)
        {
            WorkDistributionMainCodeWise objWorkDistribution = new WorkDistributionMainCodeWise();
            objWorkDistribution.MdiParent = this;
            objWorkDistribution.Show();
        }

        private void sundryTaskListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsJobReport = new DataSet();
            dsJobReport = objListing.ListSundryTasks();
            dsJobReport.WriteXml("SundryTasksList.xml");

            SundryTaskList objReport = new SundryTaskList();
            objReport.SetDataSource(dsJobReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void MIDailyHarvestOilPalm_Click(object sender, EventArgs e)
        {
            DailyHarvestOilPalmEntry objDHOP = new DailyHarvestOilPalmEntry();
            objDHOP.MdiParent = this;
            objDHOP.Show();
        }

        private void MISummaryReports_Click(object sender, EventArgs e)
        {
            SummaryReports objSummaryRpt = new SummaryReports();
            objSummaryRpt.MdiParent = this;
            objSummaryRpt.Show();
        }

        private void MIFieldSettings_Click(object sender, EventArgs e)
        {
            FieldSettings objFieldSetting = new FieldSettings();
            objFieldSetting.MdiParent = this;
            objFieldSetting.Show();
        }

        private void MIAutoTermination_Click(object sender, EventArgs e)
        {

        }

        private void MiJobListWithCrop_Click(object sender, EventArgs e)
        {
            DataSet oDataSet = new DataSet();
            oDataSet = objListing.GetJobCropList();

            oDataSet.WriteXml("jobwithcroptype.xml");

            JobsWithCropsRPT oEmployeeMasterRPT = new JobsWithCropsRPT();
            oEmployeeMasterRPT.SetDataSource(oDataSet);

            ReportViewerForm oReportViewr = new ReportViewerForm();
            oReportViewr.crystalReportViewer1.ReportSource = oEmployeeMasterRPT;
            oReportViewr.Show();


        }

        private void MIDailyHarvestOtherCrop_Click(object sender, EventArgs e)
        {
            DailyHarvestOtherCrop ObjDHarvestOtherCrop = new DailyHarvestOtherCrop();
            ObjDHarvestOtherCrop.MdiParent = this;
            ObjDHarvestOtherCrop.Show();
        }

        private void MICostCenterList_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = objListing.viewAllFields();
            dataSetReport.WriteXml("AllFields.xml");
            FieldsRPT myaclist = new FieldsRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();

        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            DataSet dsJobReport = new DataSet();
            dsJobReport = objListing.getJob();
            dsJobReport.WriteXml("Job.xml");

            JobListReport objReport = new JobListReport();
            objReport.SetDataSource(dsJobReport);
            ReportViewerForm objReportViewer = new ReportViewerForm();

            objReport.SetParameterValue("Estate", myDiv.ListEstates().Rows[0][0].ToString());
            objReportViewer.crystalReportViewer1.ReportSource = objReport;
            objReportViewer.Show();
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator33_Click(object sender, EventArgs e)
        {

        }


    }
}