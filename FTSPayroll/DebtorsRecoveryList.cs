using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Transactions;

namespace FTSPayroll
{
    public partial class DebtorsRecoveryList : Form
    {
        FTSPayRollBL.MonthlyWeges myDebtorDetails = new FTSPayRollBL.MonthlyWeges();
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
        FTSPayRollBL.DeductionDebtsSummery deductionDebts = new FTSPayRollBL.DeductionDebtsSummery();

        public String Division = "";
        public String Year = "";
        public String Month = "";
        public String EmpNo = "";
        public String GuarenteeEmpNo = "";

        public DebtorsRecoveryList(String ParentDivision, String ParentYear, String MonthID, String ParentEmpNo, String PassGuarenteeEmpNo)
        {
            Division = ParentDivision;
            Year = ParentYear;
            Month = MonthID;
            EmpNo = ParentEmpNo;
            GuarenteeEmpNo = PassGuarenteeEmpNo;

            InitializeComponent();
        }

        private void DebtorsRecoveryList_Load(object sender, EventArgs e)
        {
            lblDivision.Text = "DivisionID : " + Division;
            lblYear.Text = "Year : " + Year;
            lblMonth.Text = "MonthID : " + Month;

            gvlist.DataSource = myDebtorDetails.GetDebtorsGuaranteeRecoveryList(Division, Year, Month);
            this.setGuarntee();
            if (gvlist.DataSource != null)
            {
                foreach (DataGridViewColumn dc in gvlist.Columns)
                {
                    gvlist.Columns[dc.Index].ReadOnly = true;
                    gvlist.Columns[dc.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
                }


                gvlist.Columns[4].Visible = false;
                gvlist.Columns[5].Visible = false;

                gvlist.Columns[0].Width = 40;
                gvlist.Columns[1].Width = 140;

                DataGridViewCheckBoxColumn mychkBox = new DataGridViewCheckBoxColumn();
                mychkBox.HeaderText = "Confirmation";
                gvlist.Columns.Add(mychkBox);

                gvlist.Columns[9].Width = 70;


            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Boolean Status = false;
            lblStatus.Text = "PLEASE WAIT.......";
            Application.DoEvents();

            //If Proccesed or not?
            if (ProMWages.IsProcessed(Division, Convert.ToInt32(Year), Convert.ToInt32(Month)) == false)
            {
                try
                {
                    if (gvlist.Rows.Count > 0)
                    {
                        using (TransactionScope Scope = new TransactionScope())
                        {
                            foreach (DataGridViewRow dr in gvlist.Rows)
                            {
                                if (dr.Cells[0].Value != null)
                                {
                                    if (dr.Cells[0].Value.ToString().Trim() != "")
                                    {
                                        if (dr.Cells[7].Value != null)
                                        {
                                            if (dr.Cells[7].Value.ToString().Trim() != "" && dr.Cells[7].Value.ToString().Trim() != "NA")
                                            {
                                                if (dr.Cells[6].Value != null)
                                                {
                                                    if (dr.Cells[6].Value.ToString().Trim() != "")
                                                    {
                                                        if (lblDivision.Text != "" && lblYear.Text.Trim() != "" && lblMonth.Text.Trim() != "")
                                                        {
                                                            //Earnings available or not
                                                            if (myDebtorDetails.IsGuarantorEarningsAvailable(Division, dr.Cells[7].Value.ToString().Trim(), Year, Month, Convert.ToDecimal(dr.Cells[6].Value.ToString().Trim())) == true)
                                                            {
                                                                //Debt available or not
                                                                if (!myDebtorDetails.IsDebtorInDebtList(Division, dr.Cells[1].Value.ToString(), Convert.ToInt32(Year), Convert.ToInt32(Month)) == true)
                                                                {
                                                                    if (dr.Cells[9].Value != null && dr.Cells[9].Value.ToString().Trim() != "")
                                                                    {
                                                                        //confirmed empno only
                                                                        if (Convert.ToBoolean(dr.Cells[9].Value.ToString().Trim()) == true)
                                                                        {
                                                                            myDebtorDetails.InsertDebtorsDetails(Division, dr.Cells[0].Value.ToString(), Year, Month, Convert.ToInt32(dr.Cells[4].Value.ToString()), Convert.ToInt32(dr.Cells[5].Value.ToString()), Convert.ToDecimal(dr.Cells[6].Value.ToString()), dr.Cells[7].Value.ToString(), Convert.ToBoolean(dr.Cells[9].Value.ToString()));

                                                                            //Debtor Employee re - preview happens here
                                                                            EmployeePreviewPerform(Division, dr.Cells[0].Value.ToString().Trim(), Year, Month);

                                                                            //Guarantee Employee re - preview happens here
                                                                            EmployeePreviewPerform(Division, dr.Cells[7].Value.ToString().Trim(), Year, Month);

                                                                            Status = true;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("No Debts available for EmpNo : " + dr.Cells[1].Value.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("No earnings available for gurantee Empno: " + dr.Cells[1].Value.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Something went wrong..\ncontact system administrator.!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                                MessageBox.Show("Please select relavant gurantee employee No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                            }

                            if (Status == true)
                            {
                                lblStatus.Text = ".";
                                MessageBox.Show("Employee guarantee recovery data saved successfully...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Scope.Complete();
                                goto SUCCESS;
                            }
                            else
                                lblStatus.Text = "";
                            MessageBox.Show("No details saved...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto End;
                        }


                    }
                    else
                    {
                        lblStatus.Text = "";
                        MessageBox.Show("No details found for save...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        goto End;
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "";
                    MessageBox.Show("Error occurred..! " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto End;
                }

            SUCCESS:
                gvlist.DataSource = myDebtorDetails.GetDebtorsGuaranteeRecoveryList(Division, Year, Month);
                lblStatus.Text = ".";
                this.Close();

            End:
                Application.DoEvents();
                lblStatus.Text = ".";
            }
            else
            {
                lblStatus.Text = "";
                MessageBox.Show("This month already proccesed............!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void gvlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                EmployeeSearch empSearch = new EmployeeSearch(this, e.RowIndex, Division, true);
                empSearch.Show();
            }
        }

        private void EmployeePreviewPerform(String Division, String EmpNo, String Year, String Month)
        {
            if (ProMWages.IsProcessed(Division, Convert.ToInt32(Year), Convert.ToInt32(Month)) == false)
            {
                if (lblDivision.Text.Trim() != "" && lblYear.Text.Trim() != "" && lblMonth.Text.Trim() != "")
                {
                    String Status = "";
                    String DeleteStatus = "";
                    String DeductStatus = "";
                    String FinalStatus = "";

                    PreMWages.StrDivision = Division;

                    DataTable dt = YMonth.GetOpenCloseDates(Convert.ToInt32(Month), Convert.ToInt32(Year));
                    PreMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                    PreMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
                    PreMWages.IntYear = Convert.ToInt32(Year);
                    PreMWages.IntMonth = Convert.ToInt32(Month);

                    //if (chkErrors.checkInactiveEmpEntries(PreMWages.DtProcessFromDate, PreMWages.DtProcessToDate).Tables[0].Rows.Count < 1)
                    //{


                    PreMWages.BoolProcess = false;

                    ProMWages.DtProcessFromDate = PreMWages.DtProcessFromDate;
                    ProMWages.DtProcessToDate = PreMWages.DtProcessToDate;
                    ProMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                    ProMWages.StrDivision = Division;

                    try
                    {
                        using (TransactionScope deleteScope = new TransactionScope())
                        {
                            //cancel preview data
                            DeleteStatus = ProMWages.CancelMonthlyProcessForEmpNO(Division, EmpNo);
                            deleteScope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cancel Error, " + ex.Message);
                    }

                    try
                    {
                        //Employee Earnings calculate here
                        Status = PreMWages.processPreviewMonthlyWeges(EmpNo, Division);
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Preview Error., " + ex.Message);
                    }


                    try
                    {
                        //Employee deduction calculate here
                        DeductStatus = PreMWages.processDeductionWeges(EmpNo, Division);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Deduction Error, " + ex.Message);
                    }


                    try
                    {
                        //Employee final wages calculate here
                        FinalStatus = PreMWages.processFinalWeges(EmpNo, Division);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Finalizing Error, " + ex.Message);
                    }

                }
            }
            else
            {
                MessageBox.Show("This month already proccesed............!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewSave_Click(object sender, EventArgs e)
        {

            //if (!deductionDebts.CheckEmpNo(Year, Month, Division, EmpNo1, DeductionGrup, DeductionCd, "DE"))
            //{}


            String SuccessMessage = "Can not Save";
            String strErrorMessage = "";


            if (0 < gvlist.Rows.Count)
            {
                //If Proccesed or not?
                if (!ProMWages.IsProcessed(Division, Convert.ToInt32(Year), Convert.ToInt32(Month)))
                {
                    try
                    {
                        deductionDebts.DeleteTable(Year, Month, Division); //delete Records
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Available Entry Delete Error, "+ex.Message);
                    }


                    foreach (DataGridViewRow row in gvlist.Rows)
                    {


                        #region Variable Assigning

                        String EmpNo1 = row.Cells[0].Value.ToString();
                        String EmpName1 = row.Cells[1].Value.ToString();
                        String DeductionGrup = row.Cells[2].Value.ToString();
                        String DeductionCd = row.Cells[3].Value.ToString();
                        Decimal amount = Convert.ToDecimal(row.Cells[6].Value.ToString());
                        String EmpNo2 = row.Cells[7].Value.ToString();
                        String EmpName2 = row.Cells[8].Value.ToString();
                        String Type;
                        String UserID = "ADMIN";

                        #endregion

                        if (!(EmpNo2.Equals("NA")))
                        {

                                #region CheckIsNull
                                if (!String.IsNullOrEmpty(EmpNo1))
                                {
                                    if (!String.IsNullOrEmpty(EmpName1))
                                    {
                                        if (!String.IsNullOrEmpty(DeductionCd))
                                        {
                                            if (!String.IsNullOrEmpty(EmpNo2) && !EmpNo2.Equals("NA"))
                                            {
                                                if (!String.IsNullOrEmpty(EmpName2) && !EmpName2.Equals("NA"))
                                                {


                                                    try
                                                    {
                                                            Type = "DE";//Deuction
                                                            deductionDebts.insertToDeductionDetails(Year, Month, Division, EmpNo1, EmpName1, DeductionGrup, DeductionCd, amount, EmpNo2, EmpName2, Type,UserID);
                                                            Type = "AD";//Addion
                                                            deductionDebts.insertToDeductionDetails(Year, Month, Division, EmpNo2, EmpName2, DeductionGrup, DeductionCd, amount, EmpNo1, EmpName1, Type, UserID);
                                                            SuccessMessage = "Saved Successfully";
                                                    }
                                                    catch (Exception ex)
                                                    {

                                                        strErrorMessage += ex.Message+"\r\n";
                                                        SuccessMessage = ex.Message.ToString();
                                                        break;
                                                    }



                                                }
                                                else { strErrorMessage += "Emp no : " + EmpNo1 + " Debtor can not be Empty" + "\r\n"; MessageBox.Show("Emp no : " + EmpNo1 + " Guarantee can not be Empty"); }
                                            }
                                            else { strErrorMessage += "Emp no : " + EmpNo1 + " Debtor can not be Empty" + "\r\n"; MessageBox.Show("Emp no : " + EmpNo1 + " Debtor can not be Empty"); }
                                        }
                                        else { strErrorMessage += "Emp no : " + EmpNo1 + " Deduction Code can not be Empty" + "\r\n"; MessageBox.Show("Emp no : " + EmpNo1 + " Deduction Code can not be Empty"); }
                                    }
                                    else { strErrorMessage += "Emp no : " + EmpNo1 + " Emp Name can not be Empty" + "\r\n"; MessageBox.Show("Emp no : " + EmpNo1 + " Emp Name can not be Empty"); }
                                }
                                else { MessageBox.Show(" EmpNo can not be Empty"); }
                                #endregion
                            
                        }
                    }
                }



            }



            #region SuccessFull Message
            if (String.IsNullOrEmpty(strErrorMessage))
            {
                try
                {
                    deductionDebts.makeDebtsSummery();
                    MessageBox.Show(SuccessMessage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Error, "+strErrorMessage);
            }
            #endregion

            DataTable dtGuarantorEntries = deductionDebts.ListGuarantorRecoverySummaryFromTable(Convert.ToInt32(Year), Convert.ToInt32(Month), Division);
            foreach (DataRow drow1 in dtGuarantorEntries.Rows)
            {
                if (drow1[6].ToString().ToString().Equals("AD"))
                    myDebtorDetails.InsertDebtorsAdditionDetails(drow1[3].ToString(), drow1[0].ToString(), Convert.ToInt32(drow1[1].ToString()), Convert.ToInt32(drow1[2].ToString()), drow1[4].ToString(), Convert.ToDecimal(drow1[5].ToString()));
                else
                    myDebtorDetails.InsertDebtorsDeductionDetails(drow1[3].ToString(), Convert.ToInt32(drow1[1].ToString()), Convert.ToInt32(drow1[2].ToString()), drow1[4].ToString(), Convert.ToDecimal(drow1[5].ToString()),drow1[0].ToString());
                //add additions and deductions
            }
        }


        private void setGuarntee()
        {
            DataTable dt;
            foreach (DataGridViewRow row in gvlist.Rows)
            {


                #region Variable Assigning

                String EmpNo1 = row.Cells[0].Value.ToString();
                String EmpName1 = row.Cells[1].Value.ToString();
                String DeductionGrup = row.Cells[2].Value.ToString();
                String DeductionCd = row.Cells[3].Value.ToString();
                Decimal amount = Convert.ToDecimal(row.Cells[6].Value.ToString());


                #endregion

                dt = deductionDebts.setGuarantees(Year, Month, Division, EmpNo1, DeductionGrup, DeductionCd, amount, "DE");
                if (dt.Rows.Count > 0)
                {
                    row.Cells[7].Value = dt.Rows[0][0]; ;
                    row.Cells[8].Value= dt.Rows[0][1];

                }

            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = deductionDebts.getDetailsDeductionDebitorSummery(Year, Month, Division);
            DataSet ds=new DataSet();
            ds.Tables.Add(dt);
                
                



            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DeductionDebts.xml");

                deductinDebtsRPT myRPT = new deductinDebtsRPT();
                myRPT.SetDataSource(ds);
               myRPT.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myRPT.SetParameterValue("Division", "DivisionID : " + Division);
                myRPT.SetParameterValue("Year", "Year : "+Year);
                myRPT.SetParameterValue("Month", "Month : " + Month);

                ReportViewer myViewer = new ReportViewer();
                myViewer.crystalReportViewer1.ReportSource = myRPT;
                myViewer.crystalReportViewer1.ShowRefreshButton = false;
                myViewer.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = deductionDebts.getDetailsDeductionDebitordeatails(Year, Month, Division);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);





            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("DeductionDebtsDetails.xml");

                RecoveryDebtsDatailsRPT myRPT = new RecoveryDebtsDatailsRPT();
                myRPT.SetDataSource(ds);
                myRPT.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myRPT.SetParameterValue("Division", "DivisionID : " + Division);
                myRPT.SetParameterValue("Year", "Year : " + Year);
                myRPT.SetParameterValue("Month", "Month : " + Month);

                ReportViewer myViewer = new ReportViewer();
                myViewer.crystalReportViewer1.ReportSource = myRPT;
                myViewer.crystalReportViewer1.ShowRefreshButton = false;
                myViewer.Show();
            }
        }
    }
}