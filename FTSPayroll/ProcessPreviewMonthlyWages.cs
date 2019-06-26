using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Data.SqlClient;
namespace FTSPayroll
{
    public partial class ProcessPreviewMonthlyWages : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.PreviewMonthlyWages PreMWages = new FTSPayRollBL.PreviewMonthlyWages();
        FTSPayRollBL.ProcessMonthlyWages ProMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.FTSCheckRollSettings mySettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.UpdateManager myUpdates = new FTSPayRollBL.UpdateManager();
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckErrors chkErrors = new FTSPayRollBL.CheckErrors();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        FTSPayRollBL.User SysUser = new FTSPayRollBL.User();
        FTSPayRollBL.Validation clsValidation = new FTSPayRollBL.Validation();

        public ProcessPreviewMonthlyWages()
        {
            InitializeComponent();
        }

        private void ProcessPreviewMonthlyWages_Load(object sender, EventArgs e)
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

            cmbYear_SelectedIndexChanged(null, null);
            pboxLoad.Visible = false;
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

        //public void OneLabourPreview(String strEmpNo, String strDivision, Int32 intPYear, Int32 intPMonth)
        //{
        //    lblEarningsStatus.Text = "";
        //    lblDeductions.Text = "";
        //    lblFinalProcess.Text = "";

        //    String Status = "";
        //    String DeleteStatus = "";
        //    String DeductStatus = "";
        //    String FinalStatus = "";
        //    PreMWages.StrDivision = strDivision;
        //    DataTable dt = YMonth.getOpenCloseDates(intPMonth, intPYear);
        //    PreMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
        //    PreMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
        //    PreMWages.IntYear = intPYear;
        //    PreMWages.IntMonth = intPMonth;


        //            PreMWages.BoolProcess = false;
        //            pbProcessStatus.Maximum = 100;
        //            pbProcessStatus.Value = 1;
        //            lblEarningsStatus.Text = "Processing Earnings....";
        //            //delete all preview data
        //            //try
        //            //{ 
        //            ProMWages.DtProcessFromDate = PreMWages.DtProcessFromDate;
        //            ProMWages.DtProcessToDate = PreMWages.DtProcessToDate;
        //            ProMWages.StrUserId = FTSPayRollBL.User.StrUserName;
        //            ProMWages.StrDivision = PreMWages.StrDivision;
        //            try
        //            {
        //                using (TransactionScope deleteScope = new TransactionScope())
        //                {
        //                    DeleteStatus = ProMWages.CancelMonthlyProcess(2);//cancel preview
        //                    deleteScope.Complete();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Cancel Error, " + ex.Message);
        //            }
        //            if (DeleteStatus.Equals("DONE"))
        //            {
        //                MessageBox.Show("Process Canceled Successfully");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Error,Process Cannot Cancel");
        //            }

        //            //    Status = PreMWages.DeletePreviewMonthlyWegesData();
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    MessageBox.Show("Error., " + ex.Message);
        //            //}

                   
        //                try
        //                {
        //                    Status = PreMWages.processPreviewMonthlyWeges(strEmpNo);
        //                    Application.DoEvents();
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show("Preview Error., " + ex.Message);
        //                }

        //            if (Status.Equals("COMPLETED"))
        //            {
                       
        //                    try
        //                    {
        //                        DeductStatus = PreMWages.processDeductionWeges(strEmpNo);

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        MessageBox.Show("Deduction Error, " + ex.Message);
        //                    }

                           
        //                }
                       
        //                    try
        //                    {
        //                        FinalStatus = PreMWages.processFinalWeges(drow[0].ToString());
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        MessageBox.Show("Finalizing Error, " + ex.Message);
        //                    }
                   
               
        //}

       

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Year And Month To Preview \r\n\r\nYear :"+cmbYear.SelectedValue.ToString()+" \r\n Month : " + cmbMonth.Text , "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
            lblEarningsStatus.Text = "";
            Boolean BoolIsAdminiUser=false;

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
            Decimal DecPreviewPer=0;
            btnClose.Enabled = false;
            btnPreview.Enabled = false;
            chkAll.Enabled = false;
            DataTable DivisionTbl;
            if (FTSPayRollBL.User.StrUserName.ToUpper()== "ADMIN")
            {
                BoolIsAdminiUser = true;
            }

                //if (chkErrors.checkInactiveEmpEntries(PreMWages.DtProcessFromDate,PreMWages.DtProcessToDate).Tables[0].Rows.Count<1)
                if(true)
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
                            //lblEarningsStatus.Text = "Creating Checkroll Backup....";
                            //String dirPath = "C:\\ChkDbBackUps";
                            //String SDirectory = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + Convert.ToInt32(cmbYear.SelectedValue.ToString()) + "_" + cmbMonth.Text;
                            //String filePath = "";
                            //String fileName = "";
                            //// Create a reference to a directory.
                            //DirectoryInfo di = new DirectoryInfo(dirPath);
                            //DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);
                            //DirectoryInfo SubDi=new DirectoryInfo("C:\\ChkDbBackUps\\"+SDirectory);
                            //// Create the directory only if it does not already exist.
                            //if (di.Exists == false)
                            //{
                            //    di.Create();
                            //    DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
                            //}
                            //else  if (SubDi.Exists == false)
                            //{
                            //    SubDi.Create();                            
                            //}
                            //fileName = SDirectory + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2,'0') + DateTime.Now.Day.ToString().PadLeft(2,'0');
                            //filePath = dirPath + "\\" + SubDirectory.ToString() + "\\" + fileName + ".bak";
                            //if (File.Exists(filePath))
                            //{
                            //    MessageBox.Show(" Already Backed Up The Checkroll Data For Today,\r\n Press 'OK' To Proceed. ");
                            //}
                            //else
                            //{
                            //    try
                            //    {
                            //        myUpdates.BackUpDataBase(filePath);
                            //        myUpdates.Compress(filePath, fileName + ".ZIP");
                            //        //MessageBox.Show("Backup of Checkroll Data completed successfully. ");
                            //        lblEarningsStatus.Text = "Created Backup Successfully ....";
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        di.Delete(true);
                            //        MessageBox.Show("Back Up Error,Please Preview Again");
                            //    }
                            //}
                            /*End Database BackUp*/

                            /***Preview Process Cancelation*/
                            PreMWages.BoolProcess = false;
                            pbProcessStatus.Value = 0;
                            ProMWages.DtProcessFromDate = PreMWages.DtProcessFromDate;
                            ProMWages.DtProcessToDate = PreMWages.DtProcessToDate;
                            ProMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                            ProMWages.StrDivision = PreMWages.StrDivision;


                            if (chkAll.Checked)
                            {
                                DivisionTbl = EstDivBlock.ListEstateDivisions();
                            }
                            else
                            {
                                DivisionTbl = EstDivBlock.ListEstateDivisions(ProMWages.StrDivision);
                            }

                            try
                            {
                               
                                    foreach (DataRow drow1 in DivisionTbl.Rows)
                                    {

                                        ProMWages.StrDivision = drow1[0].ToString();
                                        DataSet dsEntryDatesNFields = new DataSet();
                                        DataSet dsFieldNorms = new DataSet();
                                        DateTime dtCurrentDate;
                                        String normUpdateStatus = "";
                                        Boolean boolComplete = true;
                                        Decimal DecFieldOkgRate = 0;

                                        //UPDATE PLUCKING pri
                                        if (!chkSkipNormOkgRework.Checked)
                                        {
                                            //if (MessageBox.Show("Do You Want To Skip Norm and Over Kg Recalculation \r\n\r\nYear :" + cmbYear.SelectedValue.ToString() + " \r\n Month : " + cmbMonth.Text, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            //{
                                                try
                                                {
                                                    dsEntryDatesNFields = null;
                                                    dsFieldNorms = null;
                                                    pbProcessStatus.Minimum = 0;
                                                    pbProcessStatus.Maximum = ProMWages.DtProcessToDate.Day;
                                                    pbProcessStatus.Value = 0;
                                                    for (int i = 1; i < ProMWages.DtProcessToDate.Day; i++)
                                                    {
                                                        dtCurrentDate = new DateTime(ProMWages.DtProcessFromDate.Year, ProMWages.DtProcessFromDate.Month, i);
                                                        //ProMWages.ResetPRI(ProMWages.StrDivision, dtCurrentDate);
                                                        dsEntryDatesNFields = ProMWages.GetGeneralDailyEntryDates(dtCurrentDate, ProMWages.StrDivision);
                                                        dsFieldNorms = null;
                                                        if (dsEntryDatesNFields.Tables[0].Rows.Count > 0)
                                                        {
                                                            for (int j = 0; j < dsEntryDatesNFields.Tables[0].Rows.Count; j++)
                                                            {
                                                                try
                                                                {
                                                                    DecFieldOkgRate = 0;
                                                                    DecFieldOkgRate = ProMWages.GetFiledOkgRate(ProMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                                    dsFieldNorms = ProMWages.GetFieldWiseNorms(dtCurrentDate, ProMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                                    if (dsFieldNorms.Tables[0].Rows.Count > 0)
                                                                    {
                                                                        normUpdateStatus = ProMWages.UpdatePluckingPRI_FieldWise(dtCurrentDate, ProMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString(), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][0].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][1].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][2].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][3].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][5].ToString()), DecFieldOkgRate);
                                                                    }

                                                                    if (normUpdateStatus.ToUpper().Equals("INCOMPLETE"))
                                                                    {
                                                                        MessageBox.Show("Error On " + dtCurrentDate + " Division:" + ProMWages.StrDivision + " Field:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update ");
                                                                        boolComplete = false;
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    MessageBox.Show("Error On Norm Update " + dtCurrentDate + " Division:" + ProMWages.StrDivision + " Field:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update " + ex.Message);
                                                                }
                                                            }
                                                        }

                                                        //Lent Labour
                                                        dsEntryDatesNFields = ProMWages.GetLentDailyEntryDates(dtCurrentDate, ProMWages.StrDivision);
                                                        dsFieldNorms = null;
                                                        if (dsEntryDatesNFields.Tables[0].Rows.Count > 0)
                                                        {
                                                            for (int j = 0; j < dsEntryDatesNFields.Tables[0].Rows.Count; j++)
                                                            {
                                                                try
                                                                {
                                                                    DecFieldOkgRate = 0;
                                                                    DecFieldOkgRate = ProMWages.GetFiledOkgRate(ProMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                                    dsFieldNorms = ProMWages.GetFieldWiseNorms(dtCurrentDate, ProMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString());
                                                                    if (dsFieldNorms.Tables[0].Rows.Count > 0)
                                                                    {
                                                                        normUpdateStatus = ProMWages.UpdatePluckingPRI_FieldWise(dtCurrentDate, ProMWages.StrDivision, dsEntryDatesNFields.Tables[0].Rows[j][2].ToString(), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][0].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][1].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][2].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][3].ToString()), Convert.ToInt32(dsFieldNorms.Tables[0].Rows[0][5].ToString()),DecFieldOkgRate);
                                                                    }
                                                                    if (normUpdateStatus.ToUpper().Equals("INCOMPLETE"))
                                                                    {
                                                                        MessageBox.Show("Error On  Norm Update on " + dtCurrentDate + " LabourDivision:" + ProMWages.StrDivision + " LabourField:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update ");
                                                                        boolComplete = false;
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    MessageBox.Show("Error On " + dtCurrentDate + " LabourDivision:" + ProMWages.StrDivision + " LabourField:" + dsEntryDatesNFields.Tables[0].Rows[j][2].ToString() + " Entry Update " + ex.Message);
                                                                }
                                                            }
                                                        }
                                                        pbProcessStatus.Value++;
                                                        lblEarningsStatus.Text = "Updated Norms - " + ProMWages.StrDivision;
                                                        Application.DoEvents();
                                                        gbProgress.Refresh();

                                                    }
                                                   
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ProMWages.StrDivision + ", Error On Plucking PRI Update- Field Wise Norms May Not Available" + ex.Message);
                                                }
                                            //}
                                            //try
                                            //{
                                            //    //strPRIStatus = ProMWages.UpdatePluckingPRI(ProMWages.DtProcessFromDate, ProMWages.DtProcessToDate, ProMWages.StrDivision, "Tea");
                                            //    strPRIStatus = ProMWages.UpdatePluckingPRI(ProMWages.DtProcessFromDate, ProMWages.DtProcessToDate, ProMWages.StrDivision, "Rubber");
                                            //    lblEarningsStatus.Text = ProMWages.StrDivision + ", Updated Tapping PRI";
                                            //    Application.DoEvents();
                                            //}
                                            //catch (Exception ex)
                                            //{
                                            //    MessageBox.Show(ProMWages.StrDivision + ", Error On Tapping PRI Update");
                                            //}
                                        }

                                        lblEarningsStatus.Text = "Cancel Preview ....";
                                        using (TransactionScope deleteScope = new TransactionScope())
                                        {
                                            DeleteStatus = ProMWages.CancelMonthlyProcess(2);//cancel preview
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
                                    gbProgress.Text = "0%";
                                    pboxLoad.Visible = true;

                                    EmployeeTbl = EmpMaster.getActiveEmployeeDetailsByDivision(PreMWages.StrDivision,PreMWages.DtProcessFromDate,PreMWages.DtProcessToDate);

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
                                            if (drow[0].ToString().Equals("0052"))
                                            {
                                                MessageBox.Show("0052");
                                            }
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
                                                    //Status = PreMWages.processPreviewMonthlyWeges(drow[0].ToString());
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
                                            gbProgress.Text = Convert.ToInt32(DecPreviewPer).ToString() + "%";
                                            gbProgress.Refresh();
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
                                    gbProgress.Text = DecPreviewPer.ToString() + "%";
                                    gbProgress.Refresh();
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
                                    gbProgress.Text = DecPreviewPer.ToString() + "%";
                                    gbProgress.Refresh();
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

                        //}
                        //else
                        //{
                        //    MessageBox.Show("All Daily Entries Are Not Confirmed,\r\nPlease confirm All Days And Preview Again.", "Warning", MessageBoxButtons.OK);
                        //    this.Close();
                        //    //--
                        //    DataSet dsConfirmations = new DataSet();
                        //    dsConfirmations = myEntries.GetConfirmationDetails(PreMWages.DtProcessFromDate);
                        //    dsConfirmations.WriteXml("EntryConfirmations.xml");

                        //    EntryConfirmationRpt objReport = new EntryConfirmationRpt();
                        //    objReport.SetDataSource(dsConfirmations);
                        //    ReportViewerForm objReportViewer = new ReportViewerForm();
                        //    objReport.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                        //    objReport.SetParameterValue("Date", PreMWages.DtProcessFromDate.Month);
                        //    objReport.SetParameterValue("Division", "All Divisions");
                        //    objReport.SetParameterValue("Year", PreMWages.DtProcessFromDate.Year);

                        //    objReportViewer.crystalReportViewer1.ReportSource = objReport;
                        //    objReportViewer.Show();
                        //}
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            //Updates for wage increment
            String DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\FTS\";
            try
            {
                DataTable myTable = myUpdates.GetUpdateFiles(DesktopPath);
                if (myTable.Rows.Count > 0)
                {
                    if (myTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < myTable.Rows.Count; i++)
                        {
                            myUpdates.ExecuteScript(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\FTS\" + myTable.Rows[i][0].ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Update Found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error On DB Changes, " + ex.Message);
            }
            String strBackUpLocation = myUpdates.GetBackUpLocation("BackUpLocation");
            lblEarningsStatus.Text = "Creating Checkroll Backup....";
            //String dirPath = "C:\\ChkDbBackUps";
            String dirPath = strBackUpLocation;//"D:\\OLAX\\ChkDbBackUps";
            String SDirectory = "CHK" + myDivision.ListEstate().Rows[0][0].ToString() + Convert.ToInt32(cmbYear.SelectedValue.ToString()) + "_" + cmbMonth.Text;
            String filePath = "";
            String fileName = "";
            // Create a reference to a directory.
            DirectoryInfo di = new DirectoryInfo(dirPath);
            DirectoryInfo SubDirectory = new DirectoryInfo(SDirectory);
            //DirectoryInfo SubDi = new DirectoryInfo("C:\\ChkDbBackUps\\" + SDirectory);
            DirectoryInfo SubDi = new DirectoryInfo(dirPath+"\\" + SDirectory);
            // Create the directory only if it does not already exist.
            if (di.Exists == false)
            {
                di.Create();
                DirectoryInfo dis = di.CreateSubdirectory(SDirectory);
            }
            else if (SubDi.Exists == false)
            {
                SubDi.Create();
            }
            fileName = SDirectory + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            filePath = dirPath + "\\" + SubDirectory.ToString() + "\\" + fileName + ".bak";
            if (File.Exists(filePath))
            {
                MessageBox.Show(" Already Backed Up The Checkroll Data For Today,\r\n Press 'OK' To Proceed. ");
                lblEarningsStatus.Text = "Ready to preview....";
                btnPreview.Enabled = true;
                btnBackUp.Enabled = false;
            }
            else
            {
                try
                {
                    myUpdates.BackUpDataBase(filePath);
                    myUpdates.Compress(filePath, fileName + ".ZIP");
                    //MessageBox.Show("Backup of Checkroll Data completed successfully. ");
                    lblEarningsStatus.Text = "Created Backup Successfully ....";
                    btnPreview.Enabled = true;
                    btnBackUp.Enabled = false;
                }
                catch (Exception ex)
                {
                    //di.Delete(true);
                    MessageBox.Show("Back Up Error,Please Preview Again"+ex.Message);
                    btnPreview.Enabled = true;
                    btnBackUp.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ProMWages.FillAccountMapping();
            MapEntryToAccounts objMapEntries = new MapEntryToAccounts();
            objMapEntries.Show();
        }
    }
}