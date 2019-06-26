using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class BlockPlk2013Register : Form
    {
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myreport = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        String strAllContractors = "%";
        public BlockPlk2013Register()
        {
            InitializeComponent();
        }

        private void BlockPlk2013Register_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmpNo.Text.Equals("?"))
            {
                ContractorSearch ContractorList = new ContractorSearch(this, cmbDivision.SelectedValue.ToString());
                ContractorList.Show();
            }
            else
            {
                if (e.KeyChar == 13)
                {
                    //txtEmpNo_Leave(null, null);
                    if (e.KeyChar == 13)
                    {
                        txtEmpNo_LeaveChanged();
                    }
                }
            }
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            //if (txtEmpNo.Text.Equals("?"))
            //{
            //    ContractorSearch ContractorList = new ContractorSearch();
            //    ContractorList.Show();
            //}
            //else
            //{
            //    txtEmpNo_LeaveChanged();
            //}        
        }

        private void txtEmpNo_LeaveChanged()
        {
            if (!String.IsNullOrEmpty(txtEmpNo.Text))
            {
                if (String.IsNullOrEmpty(EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString())))
                {
                    MessageBox.Show("Please Select Employee Within the Division You Selected Above.");
                    txtEmpNo.Text = "";
                    txtEmpNo.Focus();
                }
                else
                {
                    if (EmpMaster.IsNotInactive(txtEmpNo.Text, cmbDivision.SelectedValue.ToString()))
                    {
                        txtEmpName.Text = EmpMaster.GetEmployeeNameByEmpNo(txtEmpNo.Text, cmbDivision.SelectedValue.ToString());
                        dtFromDate.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Employee Is Inactive");
                        txtEmpNo.Text = "";
                        txtEmpNo.Focus();
                    }
                }
            }

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            String strCon = "All";
            if (chkAllCon.Checked)
            {
                strAllContractors = "%";
            }
            else
            {
                strAllContractors = txtEmpNo.Text;
            }

            ds = myreport.getBlockPlucking2013Register((cmbDivision.SelectedValue.ToString()), (dtFromDate.Value.Date),strAllContractors);
            ds.WriteXml("BlockPlucking2013Register.xml");

            if (ds.Tables[0].Rows.Count > 0)
            {
                BlockPlk2013RegisterRPT myDailyRep = new BlockPlk2013RegisterRPT();
                myDailyRep.SetDataSource(ds);
                ReportViewer myReportViewer = new ReportViewer();
                if (strAllContractors == "%")
                {
                    strCon = "All";
                }
                else
                {
                    strCon = strAllContractors;
                }

                myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myDailyRep.SetParameterValue("Date", "Date : " + dtFromDate.Value.Date);
                myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.SelectedValue.ToString());
                myDailyRep.SetParameterValue("WorkType", "Block Plucking");
                myDailyRep.SetParameterValue("Contractor", "Contractor(s):" + strCon);
                myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void chkAllCon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCon.Checked)
            {
                txtEmpNo.Enabled = false;
                strAllContractors = "%";
                
            }
            else
            {
                txtEmpNo.Enabled = true;
                strAllContractors = txtEmpNo.Text;
            }
        }

        private void chkAllDiv_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}