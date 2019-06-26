using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class PaymentCheckRoll : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        DataSet dataSetReport = new DataSet();

        public PaymentCheckRoll()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PaymentCheckRoll_Load(object sender, EventArgs e)
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

            cmbEmpCat.DataSource = myCatagory.ListCategories();
            cmbEmpCat.DisplayMember = "CategoryName";
            cmbEmpCat.ValueMember = "CategoryID";


            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            String AllCat = "%";
            if (chkAllCat.Checked)
            {
                AllCat = "%";
            }
            else
            {
                AllCat = cmbEmpCat.SelectedValue.ToString();
            }
            try
            {
                if (chkAllDivisions.Checked == true)
                {

                    dataSetReport = myReports.getPaymentCheckRoll(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),AllCat);
                    dataSetReport.WriteXml("PaymentCheckRoll.xml");

                    if (dataSetReport.Tables[0].Rows.Count > 0)
                    {
                        PaymentCheckRollRPT myaclist = new PaymentCheckRollRPT();
                        myaclist.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();

                        myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                        myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                        myaclist.SetParameterValue("paramDivision", "ALL");
                        myaclist.SetParameterValue("paramYearMonth", cmbMonth.Text + "  /  " + cmbYear.Text);
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

                    dataSetReport = myReports.getPaymentCheckRoll(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),AllCat);
                    dataSetReport.WriteXml("PaymentCheckRoll.xml");

                    if (dataSetReport.Tables[0].Rows.Count > 0)
                    {
                        PaymentCheckRollRPT myaclist = new PaymentCheckRollRPT();
                        myaclist.SetDataSource(dataSetReport);
                        ReportViewer myReportViewer = new ReportViewer();

                        myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                        myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                        myaclist.SetParameterValue("paramDivision", cmbDivision.Text);
                        myaclist.SetParameterValue("paramYearMonth", cmbMonth.Text + "  /  " + cmbYear.Text);
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

        private void chkAllDivisions_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDivisions.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
                cmbDivision.Enabled = true;
        }

        

        private void cmdDisplay2_Click(object sender, EventArgs e)
        {
            String AllCat = "%";
            if (chkAllCat.Checked)
            {
                AllCat = "%";
            }
            else
            {
                AllCat = cmbEmpCat.SelectedValue.ToString();
            }
            try
            {
                if (chkAllDivisions.Checked == true)
                {
                    
                        dataSetReport = myReports.getPaymentCheckRollII(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),AllCat);
                        dataSetReport.WriteXml("PaymentCheckRollII.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {
                            PaymentCheckrollRPTII myaclist = new PaymentCheckrollRPTII();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("paramDivision", "ALL");
                            myaclist.SetParameterValue("paramYearMonth", cmbMonth.Text + "  /  " + cmbYear.Text);
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
                    
                        dataSetReport = myReports.getPaymentCheckRollII(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()),AllCat);
                        dataSetReport.WriteXml("PaymentCheckRollII.xml");

                        if (dataSetReport.Tables[0].Rows.Count > 0)
                        {
                            PaymentCheckrollRPTII myaclist = new PaymentCheckrollRPTII();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            myaclist.SetParameterValue("paramDivision", cmbDivision.Text);
                            myaclist.SetParameterValue("paramYearMonth", cmbMonth.Text + "  /  " + cmbYear.Text);
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

        private void chkAllCat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat.Checked)
            {

            }
            else
            {
            }
        }

        private void btnPaymentDetails_Click(object sender, EventArgs e)
        {
            PaymentDetails objPayDetails = new PaymentDetails();
            objPayDetails.Show();
        }
    }
}