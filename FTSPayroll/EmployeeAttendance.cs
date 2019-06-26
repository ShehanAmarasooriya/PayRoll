using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeAttendance : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        DataSet dataSetReport = new DataSet();

        public EmployeeAttendance()
        {
            InitializeComponent();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 intworktyp = 1;
                String strAllDivision = "%";
                DateTime dtFrom;
                DateTime dtTo;
                if (chkDateRange.Checked)
                {
                    dtFrom = dtpFrom.Value.Date;
                    dtTo = dtpTo.Value.Date;
                }
                else
                {
                    dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                    dtTo = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
                }
                if (chkAllDivisions.Checked)
                {
                    strAllDivision = "%";
                }
                else
                {
                    strAllDivision = cmbDivision.SelectedValue.ToString();
                }
                if (rbNormal.Checked)
                {
                    intworktyp = 1;
                }
                else
                {
                    intworktyp = 2;
                }
                if (chkAllDivisions.Checked == true)
                {
                    //if (chkAllCategory.Checked == true)
                    //{
                        
                        dataSetReport = myReports.getEmployeeAttendance(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),intworktyp);
                        dataSetReport.WriteXml("EmployeeAttendance.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {
                            EmployeeAttendanceRPT myaclist = new EmployeeAttendanceRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Options", "Division : ALL / Month:" + cmbMonth.Text + "/" + cmbYear.Text);
                            if (intworktyp == 1)
                            {
                                myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");

                            }
                            else
                            {
                                myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                            }
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No Data to Preview..!");
                        }
                  
                }
                else
                {
                    //if (chkAllCategory.Checked == true)
                    //{
                        dataSetReport = myReports.getEmployeeAttendance(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),intworktyp);
                        dataSetReport.WriteXml("EmployeeAttendance.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {
                            EmployeeAttendanceRPT myaclist = new EmployeeAttendanceRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Options", "Division : " + cmbDivision.Text + " / Month : " + cmbMonth.Text + "/" + cmbYear.Text);
                            if (intworktyp == 1)
                            {
                                myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");
                            }
                            else
                            {
                                myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                            }
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No Data to Preview..!");
                        }
                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmployeeAttendance_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";

            cmbCropType.DataSource = FTSSettings.ListDataFromSettings("CropType");
            cmbCropType.DisplayMember = "Name";
            cmbCropType.ValueMember = "Code";

            try
            {
                cmbYear.SelectedValue = myMonth.getLastYearID();
            }
            catch { }

            cmbYear_SelectedIndexChanged(null, null);        

            cmbYear.Text = DateTime.Now.Year.ToString();
            rbNormal.Checked = true;
        }

        private void chkAllDivisions_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDivisions.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
            {
                cmbDivision.Enabled = true;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbMonth.DataSource = myMonth.ListMonths();
                cmbMonth.DisplayMember = "Month";
                cmbMonth.ValueMember = "MId";

                cmbMonth.SelectedValue = myMonth.getLastMonthID();
            }
            catch
            {
            }
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            String strAllDiv = "%";
            String strAllCrop = "%";
            Int32 intworktype = 1;
            DateTime dtFrom;
            DateTime dtTo;
            if (chkAllDivisions.Checked)
                strAllDiv = "%";
            else
                strAllDiv = cmbDivision.SelectedValue.ToString();

            if (!chkAllCrop.Checked)
            {
                strAllCrop = cmbCropType.SelectedValue.ToString();
                //if (Convert.ToInt32(cmbCropType.SelectedValue.ToString()) == 1)
                //    strAllCrop = "1";
                //else
                //    strAllCrop = "2";
            }

            if (rbNormal.Checked)
                intworktype = 1;
            else
                intworktype = 2;

            if (chkDateRange.Checked)
            {
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
            }
            else
            {
                dtFrom = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1);
                dtTo = new DateTime(Convert.ToInt32(cmbYear.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            }

            
            dataSetReport = myReports.getEmployeeAttendanceByCrop(dtFrom, dtTo, intworktype,strAllDiv,strAllCrop);
            dataSetReport.WriteXml("EmployeeAttendance.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                EmployeeAttendanceRPT myaclist = new EmployeeAttendanceRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myaclist.SetParameterValue("Estate", EstDivBlock.GetClusterName(myDivision.ListEstate().Rows[0][0].ToString(), cmbDivision.SelectedValue.ToString()));
                if (chkAllDivisions.Checked)
                {
                    myaclist.SetParameterValue("Options", "Division : ALL / Month:" + cmbMonth.Text + "/" + cmbYear.Text);
                }
                else
                    myaclist.SetParameterValue("Options", "Division : "+cmbDivision.SelectedValue.ToString()+" / Month:" + cmbMonth.Text + "/" + cmbYear.Text);
                
                if (intworktype == 1)
                {
                    myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");

                }
                else
                {
                    myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                }
                if(chkAllCrop.Checked)
                    myaclist.SetParameterValue("CropType", "Crop Type : All Crop" );
                else                
                    myaclist.SetParameterValue("CropType", "Crop Type : "+cmbCropType.Text);
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void chkDateRange_Click(object sender, EventArgs e)
        {
            if (chkDateRange.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
                cmbYear.Enabled = false;
                cmbMonth.Enabled = false;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
                cmbYear.Enabled = true;
                cmbMonth.Enabled = true;
                
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

       

        
    }
}