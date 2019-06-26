using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DivisionWiseAmalmagation : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        
        DataSet dataSetReport = new DataSet();

        public DivisionWiseAmalmagation()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DivisionWiseAmalmagation_Load(object sender, EventArgs e)
        {
            //cmbDivision.DataSource = myDivision.ListDivisions();
            //cmbDivision.DisplayMember = "DivisionName";
            //cmbDivision.ValueMember = "DivisionID";

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            //cmbEmployeeCategory.DataSource = myCatagory.ListCategories();
            //cmbEmployeeCategory.DisplayMember = "CategoryName";
            //cmbEmployeeCategory.ValueMember = "CategoryID";

            groupBox2.Enabled = false;
            cmbYear.SelectedIndex = 0;

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {            
                try
                {
                    String strDivision = "";

                    if (chkActiveDiv.Checked)
                    {
                        strDivision = cmbDivision.SelectedValue.ToString();
                        dataSetReport = myReports.getDivisionAmalgamation(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), strDivision.ToString());
                        dataSetReport.WriteXml("DivisionAmalgamation.xml");
                    }
                    else
                    {
                        strDivision = "";
                        dataSetReport = myReports.getDivisionAmalgamation(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                        dataSetReport.WriteXml("DivisionAmalgamation.xml");
                    }

                    if (dataSetReport.Tables[0].Rows.Count > 0)
                    {
                            DivisionAmalgamationRPT myaclist = new DivisionAmalgamationRPT();
                            myaclist.SetDataSource(dataSetReport);
                            ReportViewer myReportViewer = new ReportViewer();

                            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                            myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                            if (chkActiveDiv.Checked)
                            {
                                myaclist.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                            }
                            else
                            {
                                myaclist.SetParameterValue("Division", strDivision.ToString());
                            }
                            myaclist.SetParameterValue("Options", "For the Month of : " + cmbMonth.Text + "  /  " + cmbYear.Text);
                            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                            myReportViewer.Show();
                        }
                        else
                        {
                            MessageBox.Show("No Data to Preview..!");
                        }
                    //}
                    //else
                    //{
                    //    dataSetReport = myReports.getDivisionAmalgamation(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbEmployeeCategory.SelectedValue.ToString()));
                    //    dataSetReport.WriteXml("DivisionAmalgamation.xml");

                    //    if (dataSetReport.Tables[0].Rows.Count > 0)
                    //    {
                    //        DivisionAmalgamationRPT myaclist = new DivisionAmalgamationRPT();
                    //        myaclist.SetDataSource(dataSetReport);
                    //        ReportViewer myReportViewer = new ReportViewer();

                    //        myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    //        myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
                    //        myaclist.SetParameterValue("Options", "For All Divisions and Category of : " + cmbEmployeeCategory.Text + " for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
                    //        myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                    //        myReportViewer.Show();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("No Data to Preview..!");
                    //    }
                    ////}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            
            //}
            //else
            //{
            //    if (chkAllCategory.Checked == true)
            //    {
            //        dataSetReport = myReports.getDivisionAmalgamation(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            //        dataSetReport.WriteXml("DivisionAmalgamation.xml");
            //        DivisionAmalgamationRPT myaclist = new DivisionAmalgamationRPT();
            //        myaclist.SetDataSource(dataSetReport);
            //        ReportViewer myReportViewer = new ReportViewer();

                    //        myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //        myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
            //        myaclist.SetParameterValue("Options", "For All Categories and Division : " + cmbDivision.Text + " for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
            //        myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            //        myReportViewer.Show();
            //    }
            //    else
            //    {
            //        dataSetReport = myReports.getDivisionAmalgamation(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbEmployeeCategory.SelectedValue.ToString());
            //        dataSetReport.WriteXml("DivisionAmalgamation.xml");
            //        DivisionAmalgamationRPT myaclist = new DivisionAmalgamationRPT();
            //        myaclist.SetDataSource(dataSetReport);
            //        ReportViewer myReportViewer = new ReportViewer();

                    //        myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            //        myaclist.SetParameterValue("Estate", myDivision.ListEstate().Rows[0][1].ToString());
            //        myaclist.SetParameterValue("Options", "For Division : " + cmbDivision.Text + " and Category : " + cmbEmployeeCategory.Text + " for the Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
            //        myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            //        myReportViewer.Show();
            //    }
            //}

        }

        private void chkActiveDiv_CheckedChanged(object sender, EventArgs e)
        {

            if (chkActiveDiv.Checked)
            {
                groupBox2.Enabled = true;

                cmbDivision.DataSource = myDivision.ListDivisions();
                cmbDivision.DisplayMember = "DivisionName";
                cmbDivision.ValueMember = "DivisionID";
            }
            else
                groupBox2.Enabled = false;
        }

        //private void chkAllCategory_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkAllCategory.Checked)
        //    {
        //        cmbEmployeeCategory.Enabled = false;
        //    }
        //    else
        //    {
        //        cmbEmployeeCategory.Enabled = true;
        //    }
        //}
    }
}