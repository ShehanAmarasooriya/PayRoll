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
    public partial class EmployeeMaster : Form
    {
        public EmployeeMaster()
        {
            InitializeComponent();
        }

        EstateDivisionBlock myDivision = new EstateDivisionBlock();
        FTSPayRollBL.EmployeeGang myGang = new FTSPayRollBL.EmployeeGang();
        FTSPayRollBL.EmployeeUnion myEmpUnion = new FTSPayRollBL.EmployeeUnion();
        Job myJob = new Job();
        FTSPayRollBL.EmployeeCategory myCategory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.EmployeeMaster myEmployee = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeFund myEmpFund = new FTSPayRollBL.EmployeeFund();
        FTSPayRollBL.ClsBankMaster bankBranch = new ClsBankMaster();


        private void EmployeeMaster_Load(object sender, EventArgs e)
        {
            cmbEstate.DataSource = myDivision.ListEstates();
            cmbEstate.DisplayMember = "EstateName";
            cmbEstate.ValueMember = "EstateID";

            cmbDivision.DataSource = myDivision.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbUnion.DataSource = myEmpUnion.ListAllUnion();
            cmbUnion.DisplayMember = "UnionCode";
            cmbUnion.ValueMember = "UnionCode";

            cmbGang.DataSource = myGang.getGangID();
            cmbGang.DisplayMember = "GangName";
            cmbGang.ValueMember = "GangID";

            cmbBasicJob.DataSource = myJob.getJobID();
            cmbBasicJob.DisplayMember = "JobName";
            cmbBasicJob.ValueMember = "JobID";

            cmbCategory.DataSource = myCategory.ListCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";

            if (bankBranch.IsBanksAvailable())
            {
                cmbBankBranch.DataSource = bankBranch.ListBankBranch().Tables[0];
                cmbBankBranch.DisplayMember = "BankBranch";
                cmbBankBranch.ValueMember = "BankCode";
            }

            //txtContractor.Enabled = false;

            cmbGender.SelectedIndex = 0;
            
            cmbMaritalStatus.SelectedIndex = 0;

            gvlist.DataSource = myEmployee.ListAllEmployess();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            String status = "";
            try
            {
                if (txtEmployeeNo.Text == "")
                {
                    MessageBox.Show("Employee no can not be empty");
                    txtEmployeeNo.Focus();
                }
                else if (!Regex.IsMatch(txtEmployeeNo.Text, @"^[0-9]{4}$"))
                {
                    MessageBox.Show("Employee No Must Be 4 Digits.");
                    txtEmployeeNo.Focus();
                }
                else if (!Regex.IsMatch(txtEpfNo.Text, @"^[0-9]{6}$")&& !String.IsNullOrEmpty(txtEpfNo.Text) )
                {                    
                    MessageBox.Show("EPFNo_Member No Must Be 6 Digits.");
                    txtEpfNo.Focus();
                }                    
                else  if (txtEmployeeNo.Text == "")
                {
                    MessageBox.Show("Employee no can not be empty");
                    txtEmployeeNo.Focus();
                }

                else if (txtFirstName.Text == "")
                {
                    MessageBox.Show("Employee name can not be empty");
                    txtFirstName.Focus();
                }
            
                else
                {
                    myEmployee.StrDivisionID = cmbDivision.SelectedValue.ToString();
                    myEmployee.StrEstateID = cmbEstate.SelectedValue.ToString();
                    myEmployee.StrContractorNo = "NA";
                    myEmployee.StrEmpno = txtEmployeeNo.Text;
                    myEmployee.StrEpfNo = txtEpfNo.Text;
                    myEmployee.StrEmpName = txtFirstName.Text;
                    myEmployee.StrNICNo = txtNICNo.Text;
                    myEmployee.StrStatus = txtStatus.Text;
                    myEmployee.StrGender = cmbGender.Text;
                    myEmployee.DatDateJoin = dtDateofJoin.Value.Date;
                    if (chkStatus.Checked)
                    {
                        myEmployee.ActiveEmp1 = true;
                    }
                    else
                    {
                        myEmployee.ActiveEmp1 = false;
                    }
                    myEmployee.IntEmpCategory = Convert.ToInt32(cmbCategory.SelectedValue.ToString());
                    
                    //myEmployee.IntUnionCode = Convert.ToInt32(cmbUnion.SelectedValue.ToString());
                    //myEmployee.IntGangcode = Convert.ToInt32(cmbGang.SelectedValue.ToString());
                    //myEmployee.IntBasicJob = Convert.ToInt32(cmbBasicJob.SelectedValue.ToString());
                    myEmployee.StrMaritalStatus = cmbMaritalStatus.Text;
                    myEmployee.ConfirmDate1 = dtConfirmDate.Value.Date;
                    //myEmployee.ResignedDate1 = dtpResignedDate.Value.Date;
                    myEmployee.DOB1 = dtDOB.Value.Date;
                    myEmployee.IntEmptype = 0;
                    if (bankBranch.IsBanksAvailable())
                    {
                        if (cmbBankBranch.SelectedIndex > -1)
                        {
                            myEmployee.StrBranchCode = cmbBankBranch.SelectedValue.ToString();
                        }
                        else
                        {
                            myEmployee.StrBranchCode = "NA";
                        }
                    }
                    else
                        myEmployee.StrBranchCode = "NA";

                    myEmployee.InsertEmployee();
                    //myEmpFund.StrEmployeeNo = txtEmployeeNo.Text;


                        //for (int i = 0; i < chkLtFunds.Items.Count; i++)
                        //{
                        //    myEmpFund.StrEmployeeNo = txtEmployeeNo.Text;
                        //    myEmpFund.StrName = chkLtFunds.Items[i].ToString();
                        //    myEmpFund.StrUserId = FTSPayRollBL.User.StrUserName;

                        //    if (chkLtFunds.GetItemChecked(i) == true)
                        //    {
                        //        myEmpFund.BlActiveFund = true;
                        //        status=myEmpFund.InsertEmployeeFunds();
                        //    }
                        //    else
                        //    {
                        //        myEmpFund.BlActiveFund = false;
                        //        status = myEmpFund.DeleteEmployeeFundsAssign();
                        //    }
                        //}
                        MessageBox.Show("Employee Added sucessfully");
                        cmdClear.PerformClick();
                }

            }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Occurred..!");
                }
            }
        

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            String status = "";
            try
            {
                if (txtEmployeeNo.Text == "")
                {
                    MessageBox.Show("Employee no can not be empty");
                    txtEmployeeNo.Focus();
                }

                else if (txtEpfNo.Text == "")
                {
                    MessageBox.Show("EpfNo no can not be empty");
                    txtEpfNo.Focus();
                }

                else if (txtFirstName.Text == "")
                {
                    MessageBox.Show("Employee name can not be empty");
                    txtFirstName.Focus();
                }
                else
                {
                    myEmployee.StrDivisionID = cmbDivision.SelectedValue.ToString();
                    myEmployee.StrEstateID = cmbEstate.SelectedValue.ToString();
                    myEmployee.StrContractorNo = "NA";
                    myEmployee.StrEmpno = txtEmployeeNo.Text;
                    myEmployee.StrEpfNo = txtEpfNo.Text;
                    myEmployee.StrEmpName = txtFirstName.Text;
                    myEmployee.StrNICNo = txtNICNo.Text;
                    myEmployee.StrStatus = txtStatus.Text;
                    myEmployee.StrGender = cmbGender.Text;
                    myEmployee.DatDateJoin = dtDateofJoin.Value.Date;
                    if (chkStatus.Checked)
                    {
                        myEmployee.ActiveEmp1 = true;
                    }
                    else
                    {
                        myEmployee.ActiveEmp1 = false;
                    }
                    myEmployee.IntEmpCategory = Convert.ToInt32(cmbCategory.SelectedValue.ToString());
                    myEmployee.StrMaritalStatus = cmbMaritalStatus.Text;
                    myEmployee.ConfirmDate1 = dtConfirmDate.Value.Date;
                    myEmployee.DOB1 = dtDOB.Value.Date;
                    myEmployee.IntEmptype = 0;
                    if (bankBranch.IsBanksAvailable())
                    {
                        if (cmbBankBranch.SelectedIndex > -1)
                        {
                            myEmployee.StrBranchCode = cmbBankBranch.SelectedValue.ToString();
                        }
                        else
                        {
                            myEmployee.StrBranchCode = "NA";
                        }
                    }
                    else
                        myEmployee.StrBranchCode = "NA";

                    myEmployee.UpdateEmployee();
                    MessageBox.Show("Employee Updated sucessfully");
                    cmdClear.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred..!");
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            String status = "";
            if (MessageBox.Show("Confirm Delete...!", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtEmployeeNo.Text == "")
                {
                    MessageBox.Show("Employee no can not be empty");
                    txtEmployeeNo.Focus();
                }
                else
                {
                    myEmployee.StrEmpno = txtEmployeeNo.Text;
                    myEmployee.StrDivisionID=cmbDivision.SelectedValue.ToString();
                    myEmployee.StrEmpName = txtFirstName.Text;
                    myEmployee.DeleteEmployee();
                    MessageBox.Show("Employee Deleted successfully");
                    cmdClear.PerformClick();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            gvlist.DataSource = myEmployee.ListAllEmployess(cmbDivision.SelectedText);
            txtEmployeeNo.Text="";
            txtEpfNo.Text = "";
            txtFirstName.Text = "";
            txtNICNo.Text = "";
            txtStatus.Text = "";

            //chkLtFunds.Items.Clear();
            //DataTable myTable = myEmpFund.ListEmpFunds();
            //if (myTable.Rows.Count > 0)
            //{
            //    for (int i = 0; i < myTable.Rows.Count; i++)
            //    {
            //        chkLtFunds.Items.Add(myTable.Rows[i][1].ToString());
            //    }
            //}
            //else
            //    chkLtFunds.Items.Clear();
            try
            {
                gvlist.DataSource = myEmployee.ListAllEmployess(cmbDivision.SelectedValue.ToString());
            }
            catch { }
            cmdAdd.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbUnion.SelectedIndex = -1;
            cmbGang.SelectedIndex = -1;
            cmbBasicJob.SelectedIndex = -1;
            cmbDivision.Text = gvlist.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbEstate.Text = gvlist.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmployeeNo.Text = gvlist.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEpfNo.Text = gvlist.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFirstName.Text = gvlist.Rows[e.RowIndex].Cells[4].Value.ToString();
            //begins kalana.
            txtLastName.Text = gvlist.Rows[e.RowIndex].Cells[19].Value.ToString();
            txtInitials.Text = gvlist.Rows[e.RowIndex].Cells[20].Value.ToString();
            cmbOCGrade.Text = gvlist.Rows[e.RowIndex].Cells[21].Value.ToString();
            cmbZoneCode.Text = gvlist.Rows[e.RowIndex].Cells[22].Value.ToString();
            cmbMemStatus.Text = gvlist.Rows[e.RowIndex].Cells[23].Value.ToString();
            txtEmployerNo.Text = gvlist.Rows[e.RowIndex].Cells[24].Value.ToString();
            //end kalana.

            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[6].Value.ToString()))
            {
                txtStatus.Text = gvlist.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[5].Value.ToString()))
            {
                if(gvlist.Rows[e.RowIndex].Cells[5].Value.ToString().Equals("M"))
                {
                    cmbGender.Text="M";
                }
                else if (gvlist.Rows[e.RowIndex].Cells[5].Value.ToString().Equals("F"))
                {
                    cmbGender.Text="F";
                }
                else{
                    cmbGender.SelectedIndex=-1;
                }
            }
            //cmbGender.Text = gvlist.Rows[e.RowIndex].Cells[5].Value.ToString();
            //cmbUnion.Text = gvlist.Rows[e.RowIndex].Cells[12].Value.ToString();
            //cmbGang.Text = gvlist.Rows[e.RowIndex].Cells[13].Value.ToString();
            //cmbBasicJob.Text = gvlist.Rows[e.RowIndex].Cells[14].Value.ToString();
            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[7].Value.ToString()))
            {
                dtDateofJoin.Value = Convert.ToDateTime(gvlist.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
            //else
            //{
            //    dtDateofJoin.Value.Date = null;
            //}


            if (Convert.ToBoolean(gvlist.Rows[e.RowIndex].Cells[8].Value.ToString())==true)
            {
                chkStatus.Checked = true;
            }
            else
            {
                chkStatus.Checked = false;
            }
          
            cmbCategory.SelectedValue = Convert.ToInt32(gvlist.Rows[e.RowIndex].Cells[10].Value.ToString());
            cmbCategory_SelectedIndexChanged(null,null);
           

            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[11].Value.ToString()))
            {
                cmbMaritalStatus.Text = gvlist.Rows[e.RowIndex].Cells[11].Value.ToString();
            }

            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[12].Value.ToString()))
            {
                dtConfirmDate.Value = Convert.ToDateTime(gvlist.Rows[e.RowIndex].Cells[12].Value.ToString());
            }

            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[13].Value.ToString()))
            {
                dtDOB.Value = Convert.ToDateTime(gvlist.Rows[e.RowIndex].Cells[13].Value.ToString());
            }

            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[15].Value.ToString()))
            {
                txtNICNo.Text = gvlist.Rows[e.RowIndex].Cells[15].Value.ToString();
            }
            if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[16].Value.ToString()))
            {
                cmbUnion.SelectedValue = gvlist.Rows[e.RowIndex].Cells[16].Value.ToString();
            }
            //if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[17].Value.ToString()))
            //{
            //    if (!gvlist.Rows[e.RowIndex].Cells[17].Value.ToString().Equals("NA"))
            //    {
            //        txtContractor.Text = gvlist.Rows[e.RowIndex].Cells[17].Value.ToString();
            //        lblContractorName.Text = myEmployee.GetEmployeeNameByEmpNo(txtContractor.Text, cmbDivision.SelectedValue.ToString());

            //    }
            //    else
            //    {
            //        txtContractor.Text = "";
            //        lblContractorName.Text = "";
            //    }
            //}
            //else
            //{
            //    txtContractor.Text = "";
            //    lblContractorName.Text = "";
            //}
            //if (!String.IsNullOrEmpty(gvlist.Rows[e.RowIndex].Cells[18].Value.ToString()))
            //{
            //    if (Convert.ToInt32(gvlist.Rows[e.RowIndex].Cells[18].Value.ToString()) == 1)
            //    {
            //        rbContractor.Checked = true;
            //    }
            //    else
            //    {
            //        rbOther.Checked = true;
            //    }
            //}
            //cmbUnion.Text = gvlist.Rows[e.RowIndex].Cells[12].Value.ToString();
            //cmdUpdate.Enabled = true;
            //cmdDelete.Enabled = true;
            cmdAdd.Enabled = false;
        }

       
        private void cmbCategoryChanged()
        {
            try
            {
                txtEmployeeNo.Focus();
                //if (myCategory.IsContractor(Convert.ToInt32(cmbCategory.SelectedValue.ToString())))
                //{
                //    txtContractor.Text = "NA";
                //    txtContractor.Enabled = false;
                //    txtEmployeeNo.Focus();
                //}
                //else if (myCategory.IsContractCashWorker(Convert.ToInt32(cmbCategory.SelectedValue.ToString())))
                //{
                   
                //    txtContractor.Enabled = true;
                //    txtContractor.Focus();
                //}
                //else
                //{                   
                //    txtContractor.Text = "NA";
                //    txtContractor.Enabled = true;
                //    txtEmployeeNo.Focus();
                //}

            }
            catch (Exception ex)
            {
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbCategoryChanged();
            }
        }

        private void txtContractor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtContractor.Text.Equals("?"))
            //{
            //    //EmployeeList empList = new EmployeeList();
            //    //empList.Show();
            //    ContractorList ContractorList = new ContractorList();
            //    ContractorList.ShowDialog();
            //    txtContractor.Text = ContractorList.getTextEmployeeNoValue();
            //    ContractorList.Dispose();
            //}
            //else
            //{
            //    if (e.KeyChar == 13)
            //    {
            //        txtEmployeeNo.Focus();
            //    }
            //}

        }

        private void txtEmployeeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEpfNo.Focus();
            }
        }

        private void txtEpfNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFirstName.Focus();
            }
        }

        private void txtContractor_Leave(object sender, EventArgs e)
        {
            txtContractor_LeaveChanged();
        }

        private void txtContractor_LeaveChanged()
        {
            //if (!String.IsNullOrEmpty(txtContractor.Text))
            //{
            //    if (String.IsNullOrEmpty(myEmployee.GetEmployeeNameByEmpNo(txtContractor.Text, cmbDivision.SelectedValue.ToString())))
            //    {
            //        MessageBox.Show("Please Select Contractor Within the Division You Selected Above.");
            //        txtContractor.Text = "";
            //        txtContractor.Focus();
            //    }
            //    else
            //    {
            //        lblContractorName.Text = myEmployee.GetEmployeeNameByEmpNo(txtContractor.Text, cmbDivision.SelectedValue.ToString());
            //    }
            //}

        }


        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNICNo.Focus();
            }
        }

        private void txtNICNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
               dtDOB.Focus();
            }
        }

        private void dtDOB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbGender.Focus();
            }
        }

        private void cmbGender_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbMaritalStatus.Focus();
            }
        }

        private void cmbMaritalStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtDateofJoin.Focus();
            }
        }

        private void dtDateofJoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtConfirmDate.Focus();
            }
           
        }

        private void dtConfirmDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                chkStatus.Focus();
            }
        }

        private void chkStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void txtStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmdAdd.Focus();
            }
        }

        private void cmbDivision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbCategory.Focus();
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvlist.DataSource = myEmployee.ListAllEmployess(cmbDivision.SelectedValue.ToString());
        }

        private void txtSearchEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSearchEmpNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchEmpNo.Text.Trim() != "")
                {
                    gvlist.DataSource = myEmployee.ListAllEmployess(cmbDivision.SelectedValue.ToString(),txtSearchEmpNo.Text);
                }
                else
                {
                    gvlist.DataSource = myEmployee.ListAllEmployess(cmbDivision.SelectedValue.ToString());
                }
                
            }
            catch { }
        }

        private void btnNortifications_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = myEmployee.ListEmployeesAboveToRetire();
            dataSetReport.WriteXml("EmployeesAboveToRetire.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                EmployesAboveToRetireRPT myaclist = new EmployesAboveToRetireRPT();
                myaclist.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                myaclist.SetParameterValue("Estate", myDivision.ListEstates().Rows[0][0].ToString()+ " Estate");
                myaclist.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                myReportViewer.crystalReportViewer1.ReportSource = myaclist;
                myReportViewer.Show();
            }
            else
                MessageBox.Show("No Data to preview..!");
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

       
    }
}