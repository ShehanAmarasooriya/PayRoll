using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class CancelMonthlyProcess : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.PreviewMonthlyWages PreMWages = new FTSPayRollBL.PreviewMonthlyWages();
        FTSPayRollBL.ProcessMonthlyWages ProMWages = new FTSPayRollBL.ProcessMonthlyWages();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();

        public CancelMonthlyProcess()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            String status = "";
            if (checkBox1.Checked)
            {
                DataTable DivisionTbl;
                DivisionTbl = EstDivBlock.ListEstateDivisions();
                foreach (DataRow drow in DivisionTbl.Rows)
                {
                    try
                    {
                        ProMWages.StrDivision = drow[0].ToString();
                        if (!String.IsNullOrEmpty(cmbMonth.Text))
                        {
                            DataTable dt = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                            ProMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                            ProMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
                            ProMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                            try
                            {
                                status = ProMWages.CancelMonthlyProcess(2);//cancel Process
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error, " + ex.Message);
                            }
                            if (status.Equals("DONE"))
                            {
                                MessageBox.Show(ProMWages.StrDivision + " Process Canceled Successfully");
                            }
                            else
                            {
                                MessageBox.Show("Error,Process Cannot Cancel");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Month Value Invalid");
                            cmbYear.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error., " + ex.Message);
                    }

                }
            }
            else
            {
                
                ProMWages.StrDivision = cmbDivision.SelectedValue.ToString();
                if (!String.IsNullOrEmpty(cmbMonth.Text))
                {
                    status = "";
                    DataTable dt = YMonth.getOpenCloseDates(Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                    ProMWages.DtProcessFromDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                    ProMWages.DtProcessToDate = Convert.ToDateTime(dt.Rows[0][1].ToString());
                    ProMWages.StrUserId = FTSPayRollBL.User.StrUserName;
                    try
                    {
                        status = ProMWages.CancelMonthlyProcess(2);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, " + ex.Message);
                    }
                    if (status.Equals("DONE"))
                    {
                        MessageBox.Show("Process Canceled Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Error,Process Cannot Cancel");
                    }
                }
                else
                {
                    MessageBox.Show("Month Value Invalid");
                    cmbYear.Focus();
                }
            }
        }

        private void CancelMonthlyProcess_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = YMonth.ListMonths(Convert.ToInt32(this.cmbYear.SelectedValue.ToString()));
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MonthKey";
            }
            catch (Exception ex)
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}