using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;
using System.Text.RegularExpressions;

namespace FTSPayroll
{
    public partial class EmployeeDetailsList : Form
    {
        FTSPayRollBL.EmployeeMaster empMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.ListingDetails objListing = new ListingDetails();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();
        

        public EmployeeDetailsList()
        {
            InitializeComponent();
        }

        private void EmployeeDetailsList_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbEmployer.DataSource = empMaster.ListEmployers();
            cmbEmployer.DisplayMember = "EmployerNO";
            cmbEmployer.ValueMember = "EmployerNO";

            cmbDivision_SelectedIndexChanged(null, null);
            try
            {
                gvList.DataSource = empMaster.ListEmployeesDetails(cmbDivision.SelectedValue.ToString());
                if (gvList.RowCount > 0)
                {
                    gvList.Columns[1].Width = 250;
                    gvList.Columns[0].ReadOnly = true;
                    gvList.Columns[1].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                //myEntry.DatabaseChange1();
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvList.DataSource = empMaster.ListEmployeesDetails(cmbDivision.SelectedValue.ToString());
            if (gvList.RowCount > 0)
            {
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[0].Width = 50;
                gvList.Columns[1].Width = 150;
                gvList.Columns[2].Width = 100;
                gvList.Columns[3].Width = 100;
                gvList.Columns[4].Width = 150;
                gvList.Columns[5].Width = 50;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            String empList = "";
            String strError = "OK";
            try
            {
                for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                {
                    if (String.IsNullOrEmpty(gvList.Rows[i].Cells[7].Value.ToString()) || !Regex.IsMatch(gvList.Rows[i].Cells[7].Value.ToString(), @"^[A-Z]$"))
                    {
                        strError = "ERRROR";
                        MessageBox.Show("Zone Code Of " + gvList.Rows[i].Cells[0].Value.ToString() + " Is Invalid!", "Invalid", MessageBoxButtons.OK);
                        break;
                    }
                    else if (!Regex.IsMatch(gvList.Rows[i].Cells[6].Value.ToString(), @"^[0-9]{3}$"))
                    {
                        strError = "ERRROR";
                        MessageBox.Show("OC Grade Of " + gvList.Rows[i].Cells[0].Value.ToString() + " Is Invalid!", "Invalid", MessageBoxButtons.OK);
                        break;
                    }
                    else if (!Regex.IsMatch(gvList.Rows[i].Cells[2].Value.ToString(), @"^[0-9]{0,6}$"))
                    {
                        MessageBox.Show(cmbDivision.SelectedValue.ToString() + "-" + gvList.Rows[i].Cells[0].Value.ToString() + "\r\n EPF No Must Be 1 to 6 Digits");
                        break;
                    }
                    else
                    {
                        if (!Regex.IsMatch(gvList.Rows[i].Cells[3].Value.ToString(), @"^\d{9}(X|V)$") && !Regex.IsMatch(gvList.Rows[i].Cells[3].Value.ToString(), @"^\d{12}$"))
                        {
                            if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[3].Value.ToString()))
                            {
                                MessageBox.Show("Invalid NIC Number, " + cmbDivision.SelectedValue.ToString() + "-" + gvList.Rows[i].Cells[0].Value.ToString() + "\r\n Please Correct NIC And Continue.");
                                strError = "ERRROR";
                                break;
                            }
                            else
                            {
                                empMaster.UpdateEmployeeDetailsGrid(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), gvList.Rows[i].Cells[4].Value.ToString(), gvList.Rows[i].Cells[5].Value.ToString(), gvList.Rows[i].Cells[6].Value.ToString(), gvList.Rows[i].Cells[7].Value.ToString(), gvList.Rows[i].Cells[8].Value.ToString(), gvList.Rows[i].Cells[9].Value.ToString(), gvList.Rows[i].Cells[10].Value.ToString());
                            }
                        }
                        else
                        {
                            empMaster.UpdateEmployeeDetailsGrid(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), gvList.Rows[i].Cells[4].Value.ToString(), gvList.Rows[i].Cells[5].Value.ToString(), gvList.Rows[i].Cells[6].Value.ToString(), gvList.Rows[i].Cells[7].Value.ToString(), gvList.Rows[i].Cells[8].Value.ToString(), gvList.Rows[i].Cells[9].Value.ToString(), gvList.Rows[i].Cells[10].Value.ToString());
                        }
                    }

                }

                if (strError.Equals("ERRROR"))
                {
                    MessageBox.Show("Employee Details Saved With Errors...!");
                    //refreshGrid();
                }
                else
                {
                    MessageBox.Show("Employee Details Saved Successfully...!");
                    refreshGrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error,\n" + ex.Message);
            }

        }
        //private void validate()
        //{
        //    try
        //    {
        //        for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
        //        {
        //            empMaster.UpdateEmployeeDetails(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), gvList.Rows[i].Cells[4].Value.ToString(), gvList.Rows[i].Cells[5].Value.ToString(), Convert.ToDateTime(gvList.Rows[i].Cells[6].Value.ToString()), Convert.ToDateTime(gvList.Rows[i].Cells[7].Value.ToString()), Convert.ToDateTime(gvList.Rows[i].Cells[8].Value.ToString()), gvList.Rows[i].Cells[9].Value.ToString(), gvList.Rows[i].Cells[10].Value.ToString(), gvList.Rows[i].Cells[11].Value.ToString(), gvList.Rows[i].Cells[12].Value.ToString(), gvList.Rows[i].Cells[13].Value.ToString(), gvList.Rows[i].Cells[14].Value.ToString());
        //        }
        //        MessageBox.Show("Employee Details Saved Successfully...!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnEmployerToAll_Click(object sender, EventArgs e)
        {
            String empList = "";
            try
            {
                for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                {

                    if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[0].Value.ToString()) && !String.IsNullOrEmpty(cmbEmployer.SelectedValue.ToString()))
                    {
                        empMaster.UpdateEmployerNoToAllDivision(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), cmbEmployer.SelectedValue.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Employee No not found");
                    }
                }
                MessageBox.Show("Employer No Added Successfully To All Divisional Employees...!");
                refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error,\n" + ex.Message);
            }          
        }

        private void txtEmployerNo_TextChanged(object sender, EventArgs e)
        {

        }
        private void refreshGrid()
        {
            try
            {
                gvList.DataSource = empMaster.ListEmployeesDetails(cmbDivision.SelectedValue.ToString());
                if (gvList.RowCount > 0)
                {
                    gvList.Columns[1].Width = 250;
                    gvList.Columns[0].ReadOnly = true;
                    gvList.Columns[1].ReadOnly = true;
                }
            }
            catch 
            {
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = objListing.getEPFEmployeeDetails(cmbDivision.SelectedValue.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.WriteXml("EmployeeEPFDetails.xml");
                EmployeeEPFDetailsRPT myReport = new EmployeeEPFDetailsRPT();
                myReport.SetDataSource(ds);

                ReportViewer myReportViewer = new ReportViewer();
                myReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                myReport.SetParameterValue("Estate", "ESTATE :" + myUser.GetEstates().Tables[0].Rows[0][1].ToString());
                myReportViewer.crystalReportViewer1.ReportSource = myReport;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to preview..!");
            }
        }

        private void btnAddZoneToAll_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtZoneCode.Text, @"^[A-Z]$"))
            {
                String empList = "";
                try
                {
                    for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                    {

                        if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[0].Value.ToString()) && !String.IsNullOrEmpty(txtZoneCode.Text))
                        {
                            empMaster.UpdateZoneCodeToAllDivision(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), txtZoneCode.Text);
                        }
                        else
                        {
                            MessageBox.Show("Employee No not found");
                        }
                    }
                    MessageBox.Show("Zone Code Added Successfully To All Divisional Employees...!");
                    gvList.DataSource = empMaster.ListEmployeesDetails(cmbDivision.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error,\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Zone Code Must Be 1 Capital Character", "Invalid", MessageBoxButtons.OK);
                txtZoneCode.Clear();
            }
        }

        private void btnAddOCToAll_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtOCGrade.Text, @"^[0-9]{3}$"))
            {
                String empList = "";
                try
                {
                    for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                    {

                        if (!String.IsNullOrEmpty(gvList.Rows[i].Cells[0].Value.ToString()) && !String.IsNullOrEmpty(txtOCGrade.Text))
                        {
                            empMaster.UpdateOCGradeToAllDivision(cmbDivision.SelectedValue.ToString(), gvList.Rows[i].Cells[0].Value.ToString(), txtOCGrade.Text);
                        }
                        else
                        {
                            MessageBox.Show("Employee No not found");
                        }
                    }
                    MessageBox.Show("Zone Code Added Successfully To All Divisional Employees...!");
                    gvList.DataSource = empMaster.ListEmployeesDetails(cmbDivision.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error,\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Occupational Grade Must Be 3 Digits", "Invalid", MessageBoxButtons.OK);
                txtOCGrade.Clear();
            }
        }
    }
}