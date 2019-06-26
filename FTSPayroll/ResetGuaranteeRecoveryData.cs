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
    public partial class ResetGuaranteeRecoveryData : Form
    {
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();        
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.MonthlyWeges myGuarantee = new FTSPayRollBL.MonthlyWeges();
        FTSPayRollBL.ProcessMonthlyWages ProMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.PreviewMonthlyWages PreMWages = new FTSPayRollBL.PreviewMonthlyWages();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();


        public ResetGuaranteeRecoveryData()
        {
            InitializeComponent();
        }

        private void ResetGuaranteeRecoveryData_Load(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.Text = DateTime.Now.Year.ToString();

            if (FTSPayRollBL.User.StrUserName == "hoadmin")
            {
                btnReset.Enabled = true;
            }
            else
                btnReset.Enabled = false;
        }

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            DataSet ds = myGuarantee.GetGuaranteeRecoveryList(cmbDivision.Text, cmbYear.Text, cmbMonth.SelectedValue.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("GuaranteeRecovery.xml");

                GuaranteeRecoveryRPT myRPT = new GuaranteeRecoveryRPT();
                myRPT.SetDataSource(ds);
                myRPT.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                myRPT.SetParameterValue("Division", "DivisionID : " + cmbDivision.Text);
                myRPT.SetParameterValue("Period", "Period : " + cmbYear.Text + " - " + cmbMonth.Text);

                ReportViewer myViewer = new ReportViewer();
                myViewer.crystalReportViewer1.ReportSource = myRPT;
                myViewer.crystalReportViewer1.ShowRefreshButton = false;
                myViewer.Show();
            }
            else
            {
                MessageBox.Show("No data to preview..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 Count = 0;
                //If Proccesed or not?
                if (ProMWages.IsProcessed(cmbDivision.Text, Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString())) == false)
                {
                    DataSet ds = myGuarantee.ListGuaranteeRecoveryData(cmbDivision.Text, cmbYear.Text, cmbMonth.SelectedValue.ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblStatus.Text = "PLEASE WAIT........";
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            //confirmed empno only
                            if (Convert.ToBoolean(dr.ItemArray[10].ToString()) == true)
                            {
                                //Delete relavant data here
                                myGuarantee.ResetGuaranteeRecoveryData(dr.ItemArray[0].ToString(), dr.ItemArray[2].ToString(), dr.ItemArray[3].ToString(), dr.ItemArray[1].ToString(), Convert.ToInt32(dr.ItemArray[4].ToString()), dr.ItemArray[6].ToString(), Convert.ToInt32(dr.ItemArray[13].ToString()), Convert.ToInt32(dr.ItemArray[14].ToString()));

                                //Debtor Employee re - preview happens here
                                EmployeePreviewPerform(cmbDivision.Text, dr.ItemArray[1].ToString(), cmbYear.Text, cmbMonth.SelectedValue.ToString());

                                //Guarantee Employee re - preview happens here
                                EmployeePreviewPerform(cmbDivision.Text, dr.ItemArray[6].ToString(), cmbYear.Text, cmbMonth.SelectedValue.ToString());

                                Count += 1;
                            }
                        }                        
                    }
                    else
                        lblStatus.Text = "";
                    MessageBox.Show("No data to reset..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "";
                    MessageBox.Show("This month already proccesed............!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (Count > 0)
                {
                    lblStatus.Text = "";
                    MessageBox.Show("Reset Successfully..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error...! " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EmployeePreviewPerform(String Division, String EmpNo, String Year, String Month)
        {
            if (ProMWages.IsProcessed(Division, Convert.ToInt32(Year), Convert.ToInt32(Month)) == false)
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
            else
            {
                MessageBox.Show("This month already proccesed............!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}