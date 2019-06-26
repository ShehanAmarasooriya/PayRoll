using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class GreenLeafRegister : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
       

        DataSet dataSetReport = new DataSet();

        public GreenLeafRegister()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GreenLeafRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();


            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();

            cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            cmbEmployeeCategory.DisplayMember = "CategoryName";
            cmbEmployeeCategory.ValueMember = "CategoryID";

            cmbYear.SelectedIndex = 0;

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAllDivisions.Checked == true)
                {
                    if (chkAllCategory.Checked == true)
                    {
                        dataSetReport = myReports.getGreenLeafRegister(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                        dataSetReport.WriteXml("GreenLeafRegister.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {
                            GreenLeafRegisterRPT myaclist = new GreenLeafRegisterRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Options", "For All Divisions for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No data to preview..!");
                        }
                    }
                    else
                    {
                        dataSetReport = myReports.getGreenLeafRegister(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbEmployeeCategory.SelectedValue.ToString());
                        dataSetReport.WriteXml("GreenLeafRegister.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {

                            GreenLeafRegisterRPT myaclist = new GreenLeafRegisterRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Options", "For All Divisions and for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No data to preview..!");
                        }                       
                        
                    }
                }
                else
                {
                    if (chkAllCategory.Checked == true)
                    {
                        dataSetReport = myReports.getGreenLeafRegister(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                        dataSetReport.WriteXml("GreenLeafRegister.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {
                            GreenLeafRegisterRPT myaclist = new GreenLeafRegisterRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Options", "For Division : " + cmbDivision.Text + " for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No data to preview..!");
                        }

                    }
                    else
                    {
                        dataSetReport = myReports.getGreenLeafRegister(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbEmployeeCategory.SelectedValue.ToString());
                        dataSetReport.WriteXml("GreenLeafRegister.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {

                            GreenLeafRegisterRPT myaclist = new GreenLeafRegisterRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("Options", "For Division : " + cmbDivision.Text + "  for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No data to preview..!");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void chkAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCategory.Checked)
            {
                cmbEmployeeCategory.Enabled = false;
            }
            else
            {
                cmbEmployeeCategory.Enabled = true;
            }
        }
    }
}