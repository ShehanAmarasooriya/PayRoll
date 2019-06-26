using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;
using System.Transactions;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace FTSPayroll
{
    public partial class ProcessMonthlyWeges : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.PreviewMonthlyWages PreMWages = new FTSPayRollBL.PreviewMonthlyWages();
        FTSPayRollBL.ProcessMonthlyWages proMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.User myUserOb = new User();
        FTSPayRollBL.FTSCheckRollSettings mySett = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.UpdateManager myUpdates = new FTSPayRollBL.UpdateManager();
        FTSPayRollBL.Validation clsValidation = new FTSPayRollBL.Validation();
        FTSPayRollBL.CheckErrors chkErrors = new FTSPayRollBL.CheckErrors();
        public ProcessMonthlyWeges()
        {
            InitializeComponent();
        }
        private void ProcessMonthlyWeges_Load(object sender, EventArgs e)
        {
            cmbCategory.DataSource = EmpCat.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            pboxLoad.Visible = false;

            cmbYear_SelectedIndexChanged(null, null);

            btnBackUp.Enabled = true;
            btnProcess.Enabled = false;
            btnCancelProcess.Enabled = false;
            btnPreview.Enabled = false;

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = YMonth.ListMonths(Convert.ToInt32(this.cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MonthKey";
                cmbMonth.SelectedValue = YMonth.getLastMonthID();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Selected Month Checkroll Will be Re-Calculated Automatically Before Confirmation \r\n\r\n Please Note Your Available Checkroll May Changed If You Have Done Any Changes To Transaction Entries\r\n\r\n Year :" + cmbYear.SelectedValue.ToString() + " \r\n Month : " + cmbMonth.Text, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Boolean boolBackUpYesNo = false;
                Int32 progressBarCount = 0;
                Int32 progressWorkCode = 0;
                pboxLoad.Visible = false;
                btnProcess.Enabled = false;
                btnClose.Enabled = false;
                chkAll.Enabled = false;
                DataTable DivisionTbl;
                Boolean boolError = false;

                if (myUserOb.IsAdminUser(User.StrUserName))
                {
                    /*Backup DB Before Process*/
                    btnProcess.Enabled = false;
                    btnClose.Enabled = false;
                    chkAll.Enabled = false;
                    string ipAddress = "";
                    string myIP = "";
                    String myHost = System.Net.Dns.GetHostName();
                    if (Dns.GetHostAddresses(Dns.GetHostName()).Length > 0)
                    {
                        myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();
                    }
                    //if (myIP.Equals(mySett.GetIpAddress("IP")))
                    if (true)
                    {
                        pboxLoad.Visible = true;
                        //lblEarningsStatus.Text = "Creating Checkroll BackUp\r\nThis May Take Several Minutes...";
                        //Application.DoEvents();
                        //string n = string.Format("CHK" + myDivision.ListEstate().Rows[0][0].ToString() + "-{0:yyyy-MM-dd_hh-mm-ss-tt}.bin", DateTime.Now);
                        //File.WriteAllText(n, "aaa");
                        //String dirPath = "C:\\ChkDbBackUps";
                        //String SDirectory = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + Convert.ToInt32(cmbYear.SelectedValue.ToString()) + "_" + cmbMonth.Text + "BFProcess";
                        //String filePath = "";
                        //String fileName = "";
                        //// Create a reference to a directory.
                        //DirectoryInfo di = new DirectoryInfo(dirPath);
                        //DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);

                        //// Create the directory only if it does not already exist.
                        //if (di.Exists == false)
                        //{
                        //    di.Create();
                        //    DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
                        //}
                        //else
                        //{
                        //    if (SubDirectory.Exists == false)
                        //    {
                        //        DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
                        //    }
                        //}



                        ////fileName = SDirectory + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                        //fileName = "CHK" + myDivision.ListEstate().Rows[0][0].ToString()+DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString().PadLeft(2,'0')+DateTime.Now.Day.ToString().PadLeft(2,'0');
                        //filePath = dirPath + "\\" + SubDirectory.ToString() + "\\" + fileName + ".bak";
                        //if (File.Exists(filePath))
                        //{
                        //    MessageBox.Show(" Already Backed Up The Checkroll Before Process,\r\n Press 'OK' To Proceed. ");
                        //    boolBackUpYesNo = true;
                        //}
                        //else
                        //{
                        //    try
                        //    {
                        //        //temporarily commented
                        //        //myUpdates.BackUpDataBase(filePath);
                        //        //myUpdates.Compress(filePath, fileName + ".ZIP");
                        //        //FileInfo existingFile = new FileInfo(filePath);
                        //        //existingFile.Delete();
                        //        MessageBox.Show("Backup of Checkroll Data completed successfully. ");
                        //        boolBackUpYesNo = true;
                        //        lblEarningsStatus.Text = "Created Checkroll BackUp/r/n Successfully.";
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        //di.Delete(true);
                        //        MessageBox.Show("Back Up Error, " + ex.Message);
                        //        boolBackUpYesNo = false;
                        //    }
                        //}
                        btnProcess.Enabled = true;
                        btnClose.Enabled = true;
                        chkAll.Enabled = true;


                        //------------------------------------------------------------
                        //if (boolBackUpYesNo)
                        if (true)
                        {
                            btnProcess.Enabled = false;
                            btnClose.Enabled = false;
                            chkAll.Enabled = false;
                            String Status = "";
                            String DeductStatus = "";
                            String DeleteStatus = "";
                            String FinalStatus = "";

                            proMWages.StrDivision = cmbDivision.SelectedValue.ToString();
                            proMWages.IntCategory = Convert.ToInt32(cmbCategory.SelectedValue.ToString());
                            DataTable dt = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                            proMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                            proMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
                            proMWages.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                            proMWages.IntMonth = Convert.ToInt32(dt.Rows[0][2].ToString());

                            if (clsValidation.ExpenditureJournalValidation(proMWages.DtProcessFromDate) == true)
                            {
                                MessageBox.Show("Expenditure Journal For " + Convert.ToInt32(proMWages.DtProcessFromDate.Year.ToString()) + "/" + Convert.ToInt32(proMWages.DtProcessFromDate.Month.ToString()) + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                //call calculation
                                btnPreview.PerformClick();

                                proMWages.BoolProcess = true;
                                DataTable WCdt;
                                pbProcessStatus.Value = 1;

                                if (chkAll.Checked)
                                {
                                    DivisionTbl = EstDivBlock.ListEstateDivisions();
                                    foreach (DataRow dr in DivisionTbl.Rows)
                                    {
                                        proMWages.InsertProcessingDivision(dr[0].ToString(), proMWages.IntYear, proMWages.IntMonth);
                                    }
                                }
                                else
                                {
                                    DivisionTbl = EstDivBlock.ListEstateDivisions(proMWages.StrDivision);
                                }

                                foreach (DataRow drow1 in DivisionTbl.Rows)
                                {
                                    if (!proMWages.IsProcessed(drow1[0].ToString(), proMWages.IntYear, proMWages.IntMonth))
                                    {
                                        proMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                                        proMWages.StrDivision = drow1[0].ToString();

                                        DataTable EmployeeTbl;
                                        EmployeeTbl = EmpMaster.getActiveEmployeeDetailsByDivision(drow1[0].ToString(), proMWages.DtProcessFromDate, proMWages.DtProcessToDate);
                                        WCdt = proMWages.getWorkCodesFromDailyGroundTransactions(drow1[0].ToString(), proMWages.DtProcessFromDate, proMWages.DtProcessToDate);
                                        progressBarCount = EmployeeTbl.Rows.Count + WCdt.Rows.Count + 1;
                                        pbProcessStatus.Maximum = progressBarCount;
                                        pbProcessStatus.Value = 1;

                                        proMWages.BlockProcessEntry(drow1[0].ToString(), proMWages.IntYear, proMWages.IntMonth, User.StrUserName, DateTime.Now);
                                        foreach (DataRow drow in EmployeeTbl.Rows)
                                        {
                                            try
                                            {
                                                lblEarningsStatus.Text = "Division " + drow1[0].ToString() + "/ Emp " + drow[0].ToString() + " \r\n\r\nIs Processing ";

                                                Status = "COMPLETED"; // proMWages.processFinalWeges(drow[0].ToString());
                                                //if (drow[0].ToString().Equals("0267"))
                                                //{
                                                //    MessageBox.Show("here");
                                                //}
                                                //using (TransactionScope scope = new TransactionScope())
                                                //{
                                                proMWages.processFixedDeductions(drow[0].ToString());
                                                //scope.Complete();
                                                //}
                                                //using (TransactionScope scope = new TransactionScope())
                                                //{
                                                proMWages.processLoanDeductions(drow[0].ToString());
                                                //scope.Complete();
                                                //}
                                                //if (drow[0].ToString().Equals("0267"))
                                                //{
                                                //    MessageBox.Show("here");
                                                //}
                                                pbProcessStatus.Value++;
                                                gbProcess.Text = Convert.ToInt32((Decimal)pbProcessStatus.Value / (Decimal)progressBarCount * 100).ToString() + "%";
                                                gbProcess.Refresh();
                                                Application.DoEvents();

                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error., " + ex.Message);
                                                boolError = true;
                                            }
                                        }

                                        String strGLProStatus = "";
                                        foreach (DataRow dr in WCdt.Rows)
                                        {
                                            try
                                            {
                                                lblEarningsStatus.Text = "Processing GL Entries.... ";
                                                strGLProStatus = proMWages.processGLEntries(dr[0].ToString());
                                                pbProcessStatus.Value++;
                                                gbProcess.Text = Convert.ToInt32((Decimal)pbProcessStatus.Value / (Decimal)progressBarCount * 100).ToString() + "%";
                                                gbProcess.Refresh();
                                                Application.DoEvents();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message, "GL Error");
                                                boolError = true;
                                            }
                                        }


                                        if (pbProcessStatus.Value > progressBarCount - 1)
                                        {
                                            //MessageBox.Show("Process Completed Successfully!");

                                            proMWages.AddNewMonth(proMWages.IntYear, proMWages.IntMonth);

                                            lblEarningsStatus.Text = "Division " + proMWages.StrDivision + " Process\r\n\r\nCompleted Successfully ";
                                            proMWages.UpdateProcessedDivisionStatus(proMWages.StrDivision, proMWages.IntYear, proMWages.IntMonth);
                                            //proMWages.BlockProcessEntry(proMWages.StrDivision, proMWages.IntYear, proMWages.IntMonth, User.StrUserName, DateTime.Now);
                                        }
                                        else
                                        {
                                            MessageBox.Show(proMWages.StrDivision + " Division Process  Failed!");
                                            lblEarningsStatus.Text = "Division " + proMWages.StrDivision + "\r\nProcess Failed";
                                            boolError = true;
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("This Division Already Processed");
                                        pboxLoad.Visible = false;
                                        lblEarningsStatus.Text = "Division " + proMWages.StrDivision + "\r\nProcess Failed";
                                    }
                                }
                                pboxLoad.Visible = false;

                                if (boolError == true)
                                {
                                    lblEarningsStatus.Text = "Selected Division(s) Process\r\nCompleted With Errors.";
                                    btnProcess.Enabled = false;
                                }
                                else
                                {
                                    if (proMWages.IsAllDivisionsProcessUnsuccessful(proMWages.IntYear, proMWages.IntMonth))
                                    {
                                        lblEarningsStatus.Text = "Selected Division(s) Process\r\nCompleted Successfully.";
                                        MessageBox.Show("All Divisions Processed successfully.");
                                    }
                                    else
                                    {
                                        lblEarningsStatus.Text = "Selected Division(s) Process Failed\r\n.";
                                        MessageBox.Show("All Divisions Process Failed");
                                    }
                                    btnProcess.Enabled = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Backup Before Process Failed, Process From Main Computer", "Error");
                        pboxLoad.Visible = false;
                        lblEarningsStatus.Text = "Division " + proMWages.StrDivision + "\r\nProcess Failed";
                    }
                }
                else
                {
                    MessageBox.Show("You Have No Permission To Process, Please Contact Admin");
                    pboxLoad.Visible = false;
                    this.Close();
                }

                btnProcess.Enabled = true;
                btnClose.Enabled = true;
                chkAll.Enabled = true;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            

            lblEarningsStatus.Text = "Creating Checkroll BackUp\r\nThis May Take Several Minutes...";
            Application.DoEvents();
            string n = string.Format("CHK" + myDivision.ListEstate().Rows[0][0].ToString() + "-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            File.WriteAllText(n, "aaa");
            String dirPath = "C:\\ChkDbBackUps";
            String SDirectory = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + Convert.ToInt32(cmbYear.SelectedValue.ToString()) + "_" + cmbMonth.Text + "BFProcess";
            String filePath = "";
            String fileName = "";
            // Create a reference to a directory.
            DirectoryInfo di = new DirectoryInfo(dirPath);
            DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);

            // Create the directory only if it does not already exist.
            if (di.Exists == false)
            {
                di.Create();
                DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
            }
            else
            {
                if (SubDirectory.Exists == false)
                {
                    DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
                }
            }



            //fileName = SDirectory + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            //fileName = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')+n;
            fileName = "BF_Process_" + n;
            filePath = dirPath + "\\" + SubDirectory.ToString() + "\\" + fileName + ".bak";
            if (File.Exists(filePath))
            {
                MessageBox.Show(" Already Backed Up The Checkroll Before Process,\r\n Press 'OK' To Proceed. ");
                btnBackUp.Enabled = false;
                btnProcess.Enabled = false;
                btnPreview.Enabled = true;
            }
            else
            {
                try
                {
                    myUpdates.BackUpDataBase(filePath);
                    myUpdates.Compress(filePath, fileName + ".ZIP");
                    //FileInfo existingFile = new FileInfo(filePath);
                    //existingFile.Delete();
                    MessageBox.Show("Backup of Checkroll Data completed successfully. ");
                    lblEarningsStatus.Text = "Ready to preview";
                    btnBackUp.Enabled = false;
                    btnProcess.Enabled = true;
                    btnPreview.Enabled = true;
                    btnCancelProcess.Enabled = true;
                }
                catch (Exception ex)
                {
                    //di.Delete(true);
                    MessageBox.Show("Back Up Error, " + ex.Message);
                    btnBackUp.Enabled = false;
                    btnProcess.Enabled = false;
                    btnPreview.Enabled = false;
                    btnCancelProcess.Enabled = false;
                }
            }
        }

        private void btnCancelProcess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancel All Divisions Process  \r\n\r\nYear :" + cmbYear.SelectedValue.ToString() + " \r\n Month : " + cmbMonth.Text, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Boolean boolBackUpOk = false;
                DataTable dt1 = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                DataTable dtMonthToCancel = proMWages.GetMonthToAllowCancel();

                if (Convert.ToInt32(cmbYear.SelectedValue.ToString()) == Convert.ToInt32(dtMonthToCancel.Rows[0][0].ToString()) && Convert.ToInt32(dt1.Rows[0][2].ToString()) == Convert.ToInt32(dtMonthToCancel.Rows[0][1].ToString()) && !proMWages.IsPreviewed(Convert.ToDateTime(dt1.Rows[0][1].ToString()).AddMonths(1).Year, Convert.ToDateTime(dt1.Rows[0][1].ToString()).AddMonths(1).Month))
                {
                    try
                    {
                        /*BackUp Before Cancle*/
                        lblEarningsStatus.Text = "Creating Checkroll BackUp\r\nThis May Take Several Minutes...";
                        Application.DoEvents();
                        string n = string.Format("CHK" + myDivision.ListEstate().Rows[0][0].ToString() + "-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                        File.WriteAllText(n, "aaa");
                        String dirPath = "C:\\ChkDbBackUps";
                        String SDirectory = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + Convert.ToInt32(cmbYear.SelectedValue.ToString()) + "_" + cmbMonth.Text + "BFCancle";
                        String filePath = "";
                        String fileName = "";
                        // Create a reference to a directory.
                        DirectoryInfo di = new DirectoryInfo(dirPath);
                        DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);

                        // Create the directory only if it does not already exist.
                        if (di.Exists == false)
                        {
                            di.Create();
                            DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
                        }
                        else
                        {
                            if (SubDirectory.Exists == false)
                            {
                                DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
                            }
                        }

                        //fileName = SDirectory + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                        //fileName = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')+n;
                        fileName = "BF_Cancle_" + n;
                        filePath = dirPath + "\\" + SubDirectory.ToString() + "\\" + fileName + ".bak";
                        if (File.Exists(filePath))
                        {
                            MessageBox.Show(" Already Backed Up The Checkroll Before Process,\r\n Press 'OK' To Proceed. ");
                            btnBackUp.Enabled = false;
                            btnProcess.Enabled = true;
                        }
                        else
                        {
                            //try
                            //{
                            myUpdates.BackUpDataBase(filePath);
                            myUpdates.Compress(filePath, fileName + ".ZIP");
                            //FileInfo existingFile = new FileInfo(filePath);
                            //existingFile.Delete();
                            MessageBox.Show("Backup of Checkroll Data completed successfully. ");
                            lblEarningsStatus.Text = "Ready To Cancel";
                            boolBackUpOk = true;
                            //}
                            //catch (Exception ex)
                            //{
                            //    //di.Delete(true);
                            //    MessageBox.Show("Back Up Error, " + ex.Message);
                            //    boolBackUpOk = false;
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("", "Error", MessageBoxButtons.AbortRetryIgnore);
                        if (MessageBox.Show("Backup Failed!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (User.StrUserName.ToUpper().Equals("ADMIN"))
                            {
                                if (MessageBox.Show("Is Back Up Created Manually", "Only For Admin", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    boolBackUpOk = true;
                                }
                                else
                                {
                                    boolBackUpOk = false;
                                }
                            }
                            else
                            {
                                boolBackUpOk = false;
                            }
                        }
                    }
                    /*End Backup*/


                    //if (myUserOb.IsAdminUser(User.StrUserName))
                    //{
                    if (User.StrUserName.ToUpper().Equals("ADMIN"))
                    {
                        /*Cancel Process*/
                         DataTable dt = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));

                        //if (!GLCls.IsCheckrollJournalCreated(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(dt.Rows[0][2].ToString())))
                         if (true)
                         {
                             if (boolBackUpOk)
                             {
                                 try
                                 {
                                     proMWages.InsertProcessCancelAudit(User.StrUserName, Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(dt1.Rows[0][2].ToString()));
                                 }
                                 catch (Exception ex)
                                 {
                                     MessageBox.Show("Error, " + ex.Message);
                                 }

                                 DataTable DivisionTbl;
                                 Int32 progressBarCount = 0;
                                 btnProcess.Enabled = false;
                                 btnClose.Enabled = false;
                                 chkAll.Enabled = false;
                                 String Status = "";
                                 String DeductStatus = "";
                                 String DeleteStatus = "";
                                 String FinalStatus = "";
                                 Boolean boolError = false;

                                 proMWages.StrDivision = cmbDivision.SelectedValue.ToString();
                                 //DataTable dt2 = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                                 proMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                                 proMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
                                 proMWages.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                                 proMWages.IntMonth = Convert.ToInt32(dt.Rows[0][2].ToString());

                                 proMWages.BoolProcess = true;
                                 pbProcessStatus.Value = 1;

                                 if (chkAll.Checked)
                                 {
                                     DivisionTbl = EstDivBlock.ListEstateDivisions();

                                 }
                                 else
                                 {
                                     DivisionTbl = EstDivBlock.ListEstateDivisions(proMWages.StrDivision);
                                 }

                                 foreach (DataRow drow1 in DivisionTbl.Rows)
                                 {
                                     if (proMWages.IsProcessed(drow1[0].ToString(), proMWages.IntYear, proMWages.IntMonth))
                                     {
                                         proMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                                         proMWages.StrDivision = drow1[0].ToString();


                                         DataTable fixedDt = proMWages.ListFixedDeductionsToCancel(drow1[0].ToString(), proMWages.IntYear, proMWages.IntMonth);
                                         DataTable LoanDt = proMWages.ListLoanDeductionsToCancel(drow1[0].ToString(), proMWages.IntYear, proMWages.IntMonth);
                                         progressBarCount = fixedDt.Rows.Count + LoanDt.Rows.Count + 1;
                                         pbProcessStatus.Maximum = progressBarCount;
                                         pbProcessStatus.Value = 1;

                                         proMWages.UnBlockProcessEntryOnProcessCancellation(drow1[0].ToString(), proMWages.IntYear, proMWages.IntMonth);

                                         try
                                         {
                                             lblEarningsStatus.Text = "Division " + drow1[0].ToString() + " \r\n\r\nProcess Is Canceling ";

                                             Status = "COMPLETED"; // proMWages.processFinalWeges(drow[0].ToString());
                                             //if (drow[0].ToString().Equals("0267"))
                                             //{
                                             //    MessageBox.Show("here");
                                             //}
                                             //using (TransactionScope scope = new TransactionScope())
                                             //{
                                             proMWages.CancelprocessLoanDeductions();
                                             pbProcessStatus.Value = pbProcessStatus.Value + LoanDt.Rows.Count;
                                             //scope.Complete();
                                             //}
                                             //using (TransactionScope scope = new TransactionScope())
                                             //{
                                             proMWages.CancelprocessFixedDeductions();
                                             pbProcessStatus.Value = pbProcessStatus.Value + fixedDt.Rows.Count;
                                             //scope.Complete();
                                             //}
                                             //if (drow[0].ToString().Equals("0267"))
                                             //{
                                             //    MessageBox.Show("here");
                                             //}
                                             /*Preview Cancellation*/
                                             /***Preview Process Cancelation*/
                                             proMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                                             try
                                             {

                                                 proMWages.StrDivision = drow1[0].ToString();
                                                 lblEarningsStatus.Text = "Cancel Preview ....";
                                                 using (TransactionScope deleteScope = new TransactionScope())
                                                 {
                                                     DeleteStatus = proMWages.CancelMonthlyProcess(2);//cancel preview
                                                     deleteScope.Complete();
                                                 }
                                             }
                                             catch (Exception ex)
                                             {
                                                 MessageBox.Show("Preview Cancel Error, " + ex.Message);
                                             }
                                             if (DeleteStatus.Equals("DONE"))
                                             {
                                                 //MessageBox.Show("Process Canceled Successfully");
                                                 lblEarningsStatus.Text = "Preview Successfully Canceled ....";
                                             }
                                             else
                                             {
                                                 MessageBox.Show("Error,Preview Cannot Cancel");
                                             }
                                             /**/

                                             pbProcessStatus.Value = progressBarCount;
                                             //gbProcess.Text = Convert.ToInt32((Decimal)pbProcessStatus.Value / (Decimal)progressBarCount * 100).ToString() + "%";
                                             gbProcess.Refresh();
                                             Application.DoEvents();

                                         }
                                         catch (Exception ex)
                                         {
                                             MessageBox.Show("Error., " + ex.Message);
                                             boolError = true;
                                         }

                                         String strGLProStatus = "";
                                         try
                                         {
                                             lblEarningsStatus.Text = "Processing GL Entries.... ";
                                             proMWages.CancelprocessGLEntries();
                                             //gbProcess.Text = Convert.ToInt32((Decimal)pbProcessStatus.Value / (Decimal)progressBarCount * 100).ToString() + "%";
                                             //gbProcess.Refresh();
                                             Application.DoEvents();
                                         }
                                         catch (Exception ex)
                                         {
                                             MessageBox.Show(ex.Message, "GL Error");
                                             boolError = true;
                                         }
                                         DateTime dtThisMonthDate = new DateTime(proMWages.IntYear, proMWages.IntMonth, 1);
                                         DateTime dtNextMonth = dtThisMonthDate.AddMonths(1);
                                         try
                                         {
                                             proMWages.DeleteMonthEntry(dtNextMonth.Year, dtNextMonth.Month);
                                         }
                                         catch (Exception ex)
                                         {
                                             MessageBox.Show("Error, Deleting Next Month " + ex.Message);
                                         }
                                         lblEarningsStatus.Text = "Cancel Process of Division " + proMWages.StrDivision + " Completed Successfully ";
                                         //MessageBox.Show("Process Cancellation Successfully Completed.");
                                         //if (pbProcessStatus.Value > progressBarCount - 1)
                                         //{
                                         //    //MessageBox.Show("Process Completed Successfully!");

                                         //    proMWages.AddNewMonth(proMWages.IntYear, proMWages.IntMonth);

                                         //    lblEarningsStatus.Text = "Division " + proMWages.StrDivision + " Process\r\n\r\nCompleted Successfully ";
                                         //    proMWages.UpdateProcessedDivisionStatus(proMWages.StrDivision, proMWages.IntYear, proMWages.IntMonth);
                                         //    //proMWages.BlockProcessEntry(proMWages.StrDivision, proMWages.IntYear, proMWages.IntMonth, User.StrUserName, DateTime.Now);
                                         //}
                                         //else
                                         //{
                                         //    MessageBox.Show(proMWages.StrDivision + " Division Process  Failed!");
                                         //    lblEarningsStatus.Text = "Division " + proMWages.StrDivision + "\r\nProcess Failed";
                                         //    boolError = true;
                                         //}

                                     }
                                     else
                                     {
                                         MessageBox.Show("This Division Has Not Processed To Cancel");
                                         pboxLoad.Visible = false;
                                         lblEarningsStatus.Text = "Division " + proMWages.StrDivision + "\r\nProcess Failed";
                                     }
                                 }
                                 MessageBox.Show("Process Cancellation Completed.");
                                 pboxLoad.Visible = false;
                                 btnClose.Enabled = true;

                                 //if (boolError == true)
                                 //{
                                 //    lblEarningsStatus.Text = "Selected Division(s) Process Cancellation\r\nCompleted With Errors.";
                                 //    btnProcess.Enabled = false;
                                 //}
                                 //else
                                 //{
                                 //    if (proMWages.IsAllDivisionsProcessUnsuccessful(proMWages.IntYear, proMWages.IntMonth))
                                 //    {
                                 //        lblEarningsStatus.Text = "Selected Division(s) Process Failed\r\n.";
                                 //        MessageBox.Show("All Divisions Process Failed");
                                 //    }
                                 //    else
                                 //    {
                                 //        lblEarningsStatus.Text = "Selected Division(s) Process\r\nCompleted Successfully.";
                                 //    }
                                 //    btnProcess.Enabled = false;
                                 //}

                                 /*End Cancel Process*/
                             }
                             else
                             {
                                 MessageBox.Show("Back Up Failed!", "Error!", MessageBoxButtons.OK);
                             }
                         }
                         else
                         {
                             MessageBox.Show("Checkroll Journal Is Already Created!", "Access Denied!", MessageBoxButtons.OK);
                         }

                    }
                    else
                    {
                        MessageBox.Show("You Have No Permission To Cancel Process", "Restricted", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Cancle The Selected Month, \r\n May Be Current Working Month Previewed", "Access Denied!", MessageBoxButtons.OK);
                }


            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Year And Month To Preview \r\n\r\nYear :" + cmbYear.SelectedValue.ToString() + " \r\n Month : " + cmbMonth.Text, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblEarningsStatus.Text = "";
                Boolean BoolIsAdminiUser = false;

                String Status = "";
                String DeleteStatus = "";
                String DeductStatus = "";
                String FinalStatus = "";
                String strPRIStatus = "";
                PreMWages.StrDivision = cmbDivision.SelectedValue.ToString();
                DataTable dt = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                PreMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                PreMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
                PreMWages.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                PreMWages.IntMonth = Convert.ToInt32(dt.Rows[0][2].ToString());
                Int32 previewRowCount = 0;
                Decimal DecPreviewPer = 0;
                btnClose.Enabled = false;
                btnPreview.Enabled = false;
                chkAll.Enabled = false;
                DataTable DivisionTbl;
                if (FTSPayRollBL.User.StrUserName.ToUpper() == "ADMIN")
                {
                    BoolIsAdminiUser = true;
                }

                if (chkErrors.checkInactiveEmpEntries(PreMWages.DtProcessFromDate, PreMWages.DtProcessToDate).Tables[0].Rows.Count < 1)
                {
                    if (clsValidation.ExpenditureJournalValidation(PreMWages.DtProcessFromDate) == true)
                    {
                        MessageBox.Show("Expenditure Journal For " + PreMWages.DtProcessFromDate.Year.ToString() + "/" + PreMWages.DtProcessFromDate.Month.ToString() + " Already Created.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {

                        //if (myEntries.IsAllEntriesConfirmed(PreMWages.DtProcessFromDate, PreMWages.DtProcessToDate)|| BoolIsAdminiUser)
                        //{

                        /*Backup the existing checkroll database and compress..*/
                        string myIP = "";
                        String myHost = System.Net.Dns.GetHostName();
                        if (Dns.GetHostAddresses(Dns.GetHostName()).Length > 0)
                        {
                            myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();
                        }

                        /*** Database BackUp*/
                        //if (myIP.Equals(mySettings.GetIpAddress("IP")))
                        if (true)
                        {
                           

                            /***Preview Process Cancelation*/
                            PreMWages.BoolProcess = false;
                            pbProcessStatus.Value = 0;
                            proMWages.DtProcessFromDate = PreMWages.DtProcessFromDate;
                            proMWages.DtProcessToDate = PreMWages.DtProcessToDate;
                            proMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                            proMWages.StrDivision = PreMWages.StrDivision;


                            if (chkAll.Checked)
                            {
                                DivisionTbl = EstDivBlock.ListEstateDivisions();
                            }
                            else
                            {
                                DivisionTbl = EstDivBlock.ListEstateDivisions(proMWages.StrDivision);
                            }

                            try
                            {

                                foreach (DataRow drow1 in DivisionTbl.Rows)
                                {

                                    proMWages.StrDivision = drow1[0].ToString();
                                    DataSet dsEntryDatesNFields = new DataSet();
                                    DataSet dsFieldNorms = new DataSet();
                                    DateTime dtCurrentDate;
                                    String normUpdateStatus = "";
                                    Boolean boolComplete = true;
                                    Decimal DecFieldOkgRate = 0;

                                    //UPDATE PLUCKING pri
                                    
                                        //if (MessageBox.Show("Do You Want To Skip Norm and Over Kg Recalculation \r\n\r\nYear :" + cmbYear.SelectedValue.ToString() + " \r\n Month : " + cmbMonth.Text, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        //{
                                        try
                                        {
                                            dsEntryDatesNFields = null;
                                            dsFieldNorms = null;
                                            pbProcessStatus.Minimum = 0;
                                            pbProcessStatus.Maximum = proMWages.DtProcessToDate.Day;
                                            pbProcessStatus.Value = 0;
                                            for (int i = 1; i < proMWages.DtProcessToDate.Day; i++)
                                            {
                                                dtCurrentDate = new DateTime(proMWages.DtProcessFromDate.Year, proMWages.DtProcessFromDate.Month, i);
                                                //proMWages.ResetPRI(proMWages.StrDivision, dtCurrentDate);
                                                dsEntryDatesNFields = proMWages.GetGeneralDailyEntryDates(dtCurrentDate, proMWages.StrDivision);
                                                dsFieldNorms = null;
                                                if (dsEntryDatesNFields.Tables[0].Rows.Count > 0)
                                                {
                                                    for (int j = 0; j < dsEntryDatesNFields.Tables[0].Rows.Count; j++)
                                                    {
                                                        try
                                                        {
                                                            DecFieldOkgRate = 0;
                                                            DecFieldOkgRate = proMWages.GetFiledOkgRate(proMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                            dsFieldNorms = proMWages.GetFieldWiseNorms(dtCurrentDate, proMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                            if (dsFieldNorms.Tables[0].Rows.Count > 0)
                                                            {
                                                                normUpdateStatus = proMWages.UpdatePluckingPRI_FieldWise(dtCurrentDate, proMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString(), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][0].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][1].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][2].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][3].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][5].ToString()), DecFieldOkgRate);
                                                            }

                                                            if (normUpdateStatus.ToUpper().Equals("INCOMPLETE"))
                                                            {
                                                                MessageBox.Show("Error On " + dtCurrentDate + " Division:" + proMWages.StrDivision + " Field:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update ");
                                                                boolComplete = false;
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            MessageBox.Show("Error On Norm Update " + dtCurrentDate + " Division:" + proMWages.StrDivision + " Field:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update " + ex.Message);
                                                        }
                                                    }
                                                }

                                                //Lent Labour
                                                dsEntryDatesNFields = proMWages.GetLentDailyEntryDates(dtCurrentDate, proMWages.StrDivision);
                                                dsFieldNorms = null;
                                                if (dsEntryDatesNFields.Tables[0].Rows.Count > 0)
                                                {
                                                    for (int j = 0; j < dsEntryDatesNFields.Tables[0].Rows.Count; j++)
                                                    {
                                                        try
                                                        {
                                                            DecFieldOkgRate = 0;
                                                            DecFieldOkgRate = proMWages.GetFiledOkgRate(proMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                            dsFieldNorms = proMWages.GetFieldWiseNorms(dtCurrentDate, proMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                            if (dsFieldNorms.Tables[0].Rows.Count > 0)
                                                            {
                                                                normUpdateStatus = proMWages.UpdatePluckingPRI_FieldWise(dtCurrentDate, proMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString(), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][0].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][1].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][2].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][3].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][5].ToString()), DecFieldOkgRate);
                                                            }
                                                            if (normUpdateStatus.ToUpper().Equals("INCOMPLETE"))
                                                            {
                                                                MessageBox.Show("Error On  Norm Update on " + dtCurrentDate + " LabourDivision:" + proMWages.StrDivision + " LabourField:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update ");
                                                                boolComplete = false;
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            MessageBox.Show("Error On " + dtCurrentDate + " LabourDivision:" + proMWages.StrDivision + " LabourField:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update " + ex.Message);
                                                        }
                                                    }
                                                }
                                                pbProcessStatus.Value++;
                                                lblEarningsStatus.Text = "Updated Norms - " + proMWages.StrDivision;
                                                Application.DoEvents();
                                                gbProcess.Refresh();

                                            }
                                           
                                            //Application.DoEvents();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(proMWages.StrDivision + ", Error On Plucking PRI Update- Field Wise Norms May Not Available" + ex.Message);
                                        }
                                        
                                    

                                    lblEarningsStatus.Text = "Cancel Preview ....";
                                    using (TransactionScope deleteScope = new TransactionScope())
                                    {
                                        DeleteStatus = proMWages.CancelMonthlyProcess(2);//cancel preview
                                        deleteScope.Complete();
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Cancel Error, " + ex.Message);
                            }
                            if (DeleteStatus.Equals("DONE"))
                            {
                                //MessageBox.Show("Process Canceled Successfully");
                                lblEarningsStatus.Text = "Successfully Canceled ....";
                            }
                            else
                            {
                                MessageBox.Show("Error,Process Cannot Cancel");
                            }
                            /**End Preview Process Cancelation*/

                            ////////////////////////////////////
                            DataTable EmployeeTbl;
                            //---3
                            try
                            {
                                foreach (DataRow drow1 in DivisionTbl.Rows)
                                {
                                    //--------2
                                    //try
                                    //{
                                    pbProcessStatus.Value = 0;
                                    PreMWages.StrDivision = drow1[0].ToString();
                                    previewRowCount = 0;
                                    pbProcessStatus.Maximum = 1;
                                    gbProcess.Text = "0%";
                                    pboxLoad.Visible = true;

                                    EmployeeTbl = EmpMaster.getActiveEmployeeDetailsByDivision(PreMWages.StrDivision, PreMWages.DtProcessFromDate, PreMWages.DtProcessToDate);

                                    lblEarningsStatus.Text = "Started Preview ....";
                                    if (EmployeeTbl.Rows.Count > 0)
                                    {
                                        //------------1
                                        //try
                                        //{

                                        previewRowCount = EmployeeTbl.Rows.Count;
                                        pbProcessStatus.Maximum = previewRowCount;

                                        foreach (DataRow drow in EmployeeTbl.Rows)
                                        {
                                            lblEarningsStatus.Text = "Division " + PreMWages.StrDivision + "/ Emp " + drow[0].ToString() + " \r\n\r\nIs Processing For Preview";
                                            //if (drow[0].ToString().Equals("0052"))
                                            //{
                                            //    MessageBox.Show("0052");
                                            //}
                                            /*commented to terminate in a error*/
                                            //try
                                            //{

                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                Status = PreMWages.processPreviewMonthlyWeges(drow[0].ToString());
                                                scope.Complete();
                                            }
                                            Application.DoEvents();
                                            //}
                                            //catch (Exception ex)
                                            //{
                                            //    MessageBox.Show("Preview Error., " + ex.Message);
                                            //}
                                            if (!Status.Equals("ALREADYPROCESSED"))
                                            {
                                                /*commented to terminate in a error*/
                                                //try
                                                //{
                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    DeductStatus = PreMWages.processDeductionWeges(drow[0].ToString());
                                                    scope.Complete();
                                                }
                                                //}
                                                //catch (Exception ex)
                                                //{
                                                //    MessageBox.Show("Deduction Error, " + ex.Message);
                                                //}

                                                /*commented to terminate in a error*/
                                                //try
                                                //{
                                                using (TransactionScope scope = new TransactionScope())
                                                {
                                                    FinalStatus = PreMWages.processFinalWeges(drow[0].ToString());
                                                    scope.Complete();
                                                }
                                                //}
                                                //catch (Exception ex)
                                                //{
                                                //    MessageBox.Show("Finalizing Error, " + ex.Message);
                                                //}
                                            }
                                            else
                                            {
                                                MessageBox.Show(PreMWages.StrDivision + " - " + "ALREADY PROCESSED");
                                                break;
                                            }
                                            pbProcessStatus.Value++;
                                            DecPreviewPer = ((Decimal)pbProcessStatus.Value / (Decimal)previewRowCount) * 100;
                                            gbProcess.Text = Convert.ToInt32(DecPreviewPer).ToString() + "%";
                                            gbProcess.Refresh();
                                        }
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    MessageBox.Show("Preview Error,/r/nPreview Terminated Unexpectedly\r\n " + ex.Message);
                                        //}
                                        //-------------
                                    }
                                    else
                                    {
                                        Status = "COMPLETED";
                                        pbProcessStatus.Value = 1;
                                        previewRowCount = 1;
                                        btnProcess.Enabled = true;
                                        btnPreview.Enabled = false;
                                    }
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    MessageBox.Show("Preview Error!/r/nPreview Terminated Unexpectedly\r\n " + ex.Message);
                                    //}
                                    //-------2
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Preview Error!\r\nPreview Terminated Unexpectedly\r\n" + ex.Message);
                            }


                            //----3
                            if (chkAll.Checked)
                            {
                                // pbProcessStatus.Value = EmployeeTbl.Rows.Count;
                                if (Status.Equals("COMPLETED"))
                                {
                                    DecPreviewPer = ((Decimal)pbProcessStatus.Value / (Decimal)previewRowCount) * 100;
                                    gbProcess.Text = DecPreviewPer.ToString() + "%";
                                    gbProcess.Refresh();
                                    if (pbProcessStatus.Value > previewRowCount - 1)
                                    {
                                        pboxLoad.Visible = false;
                                        MessageBox.Show("All Division Preview Completed.");
                                        this.lblEarningsStatus.Text = "All Division Preview Completed.";
                                    }
                                }
                                else if (Status.Equals("ALREADYPROCESSED"))
                                {
                                    this.lblEarningsStatus.Text = "Already Processed.";
                                    pbProcessStatus.Value = 0;
                                    MessageBox.Show("Already Processed");
                                    pboxLoad.Visible = false;
                                }
                                else
                                {
                                    this.lblEarningsStatus.Text = "Process Preview Faild!";
                                }

                            }
                            else
                            {

                                //pbProcessStatus.Value = EmployeeTbl.Rows.Count;
                                if (Status.Equals("COMPLETED"))
                                {
                                    DecPreviewPer = ((Decimal)pbProcessStatus.Value / (Decimal)previewRowCount) * 100;
                                    gbProcess.Text = DecPreviewPer.ToString() + "%";
                                    gbProcess.Refresh();
                                    if (pbProcessStatus.Value > previewRowCount - 1)
                                    {
                                        pboxLoad.Visible = false;
                                        MessageBox.Show("Division " + PreMWages.StrDivision + "\r\nPreview Process Completed.");
                                        this.lblEarningsStatus.Text = "Division " + PreMWages.StrDivision + "\r\nPreview Process Completed.";
                                    }
                                }
                                else if (Status.Equals("ALREADYPROCESSED"))
                                {
                                    this.lblEarningsStatus.Text = "Division " + PreMWages.StrDivision + "\r\nAlready Processed.";
                                    pbProcessStatus.Value = 0;
                                    MessageBox.Show("Division " + PreMWages.StrDivision + "\r\nAlready Processed");
                                    pboxLoad.Visible = false;
                                }
                                else
                                {
                                    this.lblEarningsStatus.Text = "Process Preview Faild!";
                                }
                            }
                            //completed Process successfully
                        }
                        else
                        {
                            MessageBox.Show("Please Preview from main computer");
                        }

                        
                    }
                }
                else
                {
                    this.Close();
                    DisplayData chkErrorData = new DisplayData(chkErrors.checkInactiveEmpEntries(PreMWages.DtProcessFromDate, PreMWages.DtProcessToDate), "There are Some Entries For Inactive Labours\r\nPlease Correct Them And Preview Again", PreMWages.DtProcessFromDate, PreMWages.DtProcessToDate, cmbDivision.SelectedValue.ToString());
                    chkErrorData.ShowDialog();

                }
                btnPreview.Enabled = true;
                btnClose.Enabled = true;
                chkAll.Enabled = true;
            }
        }
    }
}