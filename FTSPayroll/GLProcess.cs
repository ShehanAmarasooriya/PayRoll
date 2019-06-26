using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GLBusinessLayer;
using System.Transactions;

namespace FTSPayroll
{
    public partial class GLProcess : Form
    {
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.GLProcess myGLProcess = new FTSPayRollBL.GLProcess();
        FTSPayRollBL.FTSCheckRollSettings mySettings = new FTSPayRollBL.FTSCheckRollSettings();

        public GLProcess()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                GLBusinessLayer.Transaction myGLTransfer = new GLBusinessLayer.Transaction();
                String strSalaryControlAC;
                String strVoucherNo;

                //voucher entry
                strVoucherNo = "";
                strSalaryControlAC = mySettings.GetAccountNo("Wages Control");
                myGLTransfer.DatTransactionDate = new DateTime(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), DateTime.DaysInMonth(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString())));//Convert.ToDateTime(Convert.ToDateTime(cmbMonth.Text.ToString() + "/1/" + cmbYear.Text.ToString()).AddMonths(1).AddDays(-1));
                //DateTime startdt = new DateTime(2011, 1, 1);
                //DateTime enddt = new DateTime(2011, 1, DateTime.DaysInMonth(2011, 1));
                myGLTransfer.StrUserID = FTSPayRollBL.User.StrUserName;
                strVoucherNo = "CR/" + "SA/" + myGLTransfer.DatTransactionDate.Month.ToString() + "/" + myGLTransfer.DatTransactionDate.Year.ToString();
                try
                {
                    myGLTransfer.InsertJournalVoucher(strVoucherNo, myGLTransfer.DatTransactionDate, myGLTransfer.StrUserID);
                    for (int i = 0; i < gvAdditions.Rows.Count; i++)
                    {
                        //gernal voucher credit entry
                        myGLTransfer.StrDescription = "Checkroll Entry For The Month Of " + myGLTransfer.DatTransactionDate.Month.ToString() + "/" + myGLTransfer.DatTransactionDate.Year.ToString();
                        myGLTransfer.InsertJournalVoucherCreditDetail(strVoucherNo, gvAdditions.Rows[i].Cells[3].Value.ToString(), myGLTransfer.StrDescription, Convert.ToDecimal(gvAdditions.Rows[i].Cells[2].Value.ToString()));
                        //gernal voucher debit entry
                        myGLTransfer.StrDescription = gvAdditions.Rows[i].Cells[0].Value.ToString() + " for the month of " + myGLTransfer.DatTransactionDate.Month.ToString() + "/" + myGLTransfer.DatTransactionDate.Year.ToString();
                        myGLTransfer.InsertJournalVoucherDebitDetail(strVoucherNo, strSalaryControlAC, myGLTransfer.StrDescription, Convert.ToDecimal(gvAdditions.Rows[i].Cells[2].Value.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                Scope.Complete();
            }
        }

        private void GLProcess_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbYear_SelectedIndexChanged(null, null);
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = YMonth.ListMonths();
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";

                cmbMonth_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvAdditions.DataSource = myGLProcess.GetGLAdditions(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            }
            catch {}
        }

       

    }
}