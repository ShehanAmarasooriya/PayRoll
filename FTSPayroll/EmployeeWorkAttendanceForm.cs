using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeWorkAttendanceForm : Form
    {
        FTSPayRollBL.YearMonth myYear = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();

        public EmployeeWorkAttendanceForm()
        {
            InitializeComponent();
        }

        private void EmployeeWorkAttendanceForm_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstate.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember ="DivisionID";

            cmbYear.DataSource = myYear.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myYear.getLastYearID();

            cmbMonth.DataSource = myYear.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myYear.getLastMonthID();

            rbtNormal.Checked = true;
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
                cmbDivision.Enabled = true;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            Int32 IntWorkType;
            if (ChkAll.Checked)
            {
                if (rbtNormal.Checked)
                {
                    IntWorkType = 1;

                    ds = myReports.GetEmpWorkAttandance(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(IntWorkType));
                }
                else
                {
                    IntWorkType = 2;

                    ds = myReports.GetEmpWorkAttandance(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(IntWorkType));
                }

            }
            else
            {
                if (rbtNormal.Checked)
                {
                    IntWorkType = 1;

                    ds = myReports.GetEmpWorkAttandance(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(IntWorkType));
                }
                else
                {
                    IntWorkType = 2;

                    ds = myReports.GetEmpWorkAttandance(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(IntWorkType));
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("EmployeeWorkAttendance.xml");

                EmployeeWorkAttendanceRPT myWorkAtt = new EmployeeWorkAttendanceRPT();
                myWorkAtt.SetDataSource(ds);
                myWorkAtt.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myWorkAtt.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());

                if (ChkAll.Checked)
                {
                    myWorkAtt.SetParameterValue("Options", "Division : ALL / Month:" + cmbMonth.Text + "/" + cmbYear.Text);
                }
                else
                {
                    myWorkAtt.SetParameterValue("Options", "Division : " + cmbDivision.Text +" Month: " + cmbMonth.Text + "/" + cmbYear.Text);
                }
                if (IntWorkType == 1)
                {
                    myWorkAtt.SetParameterValue("WorkType", "Work Type : Normal Work");
                }
                else
                {
                    myWorkAtt.SetParameterValue("WorkType", "Work Type : Cash Work");
                }

                ReportViewer myReportViewer = new ReportViewer();
                myReportViewer.crystalReportViewer1.ReportSource = myWorkAtt;
                myReportViewer.Show();
               
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strDiv = "%";
            String strWorkType = "%";
            DataSet ds = new DataSet();

            if (ChkAll.Checked)
            {
                strDiv = "%";
            }
            else
            {
                strDiv = cmbDivision.SelectedValue.ToString();
            }
            if (rbtNormal.Checked)
            {
                strWorkType = "1";
                ds = myReports.GetEmpWorkDetailManDaysWorkCode(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), strWorkType, strDiv);
            }
            else
            {
                strWorkType = "2";
                ds = myReports.GetEmpWorkDetailManDaysWorkCode(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), strWorkType, strDiv);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("EmployeeWorkDetailMandaysWorkCode.xml");

                if (chkWithManDays.Checked)
                {
                    EmpWorkDetailsMandaysWorkCodeRPT myWorkAtt = new EmpWorkDetailsMandaysWorkCodeRPT();
                    myWorkAtt.SetDataSource(ds);
                    myWorkAtt.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myWorkAtt.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());

                    if (ChkAll.Checked)
                    {
                        myWorkAtt.SetParameterValue("Options", "Division : ALL / Month:" + cmbMonth.Text + "/" + cmbYear.Text);
                    }
                    else
                    {
                        myWorkAtt.SetParameterValue("Options", "Division : " + cmbDivision.Text + " Month: " + cmbMonth.Text + "/" + cmbYear.Text);
                    }
                    if (strWorkType == "1")
                    {
                        myWorkAtt.SetParameterValue("WorkType", "Work Type : Normal Work");
                    }
                    else
                    {
                        myWorkAtt.SetParameterValue("WorkType", "Work Type : Cash Work");
                    }

                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myWorkAtt;
                    myReportViewer.Show();
                }
                else
                {
                    EmpWorkDetailsWorkcodeRPT myWorkAtt = new EmpWorkDetailsWorkcodeRPT();
                    myWorkAtt.SetDataSource(ds);
                    myWorkAtt.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myWorkAtt.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());

                    if (ChkAll.Checked)
                    {
                        myWorkAtt.SetParameterValue("Options", "Division : ALL / Month:" + cmbMonth.Text + "/" + cmbYear.Text);
                    }
                    else
                    {
                        myWorkAtt.SetParameterValue("Options", "Division : " + cmbDivision.Text + " Month: " + cmbMonth.Text + "/" + cmbYear.Text);
                    }
                    if (strWorkType == "1")
                    {
                        myWorkAtt.SetParameterValue("WorkType", "Work Type : Normal Work");
                    }
                    else
                    {
                        myWorkAtt.SetParameterValue("WorkType", "Work Type : Cash Work");
                    }

                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myWorkAtt;
                    myReportViewer.Show();
                }
                

            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }

        }
    }
}