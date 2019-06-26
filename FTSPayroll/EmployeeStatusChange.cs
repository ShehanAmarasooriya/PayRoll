using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FTSPayRollBL;

namespace FTSPayroll
{
    public partial class EmployeeStatusChange : Form
    {
        EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSCheckRollSettings FTSSettings = new FTSCheckRollSettings();
        EmployeeCurrentStatus EmpStatus = new EmployeeCurrentStatus();
        FTSPayRollBL.EmployeeCurrentStatus objempCurrentStatus = new FTSPayRollBL.EmployeeCurrentStatus();
        DataTable dtInactiveDuration = new DataTable();
        public int intValue = 0;

        public EmployeeStatusChange()
        {
            InitializeComponent();
        }
        public EmployeeStatusChange(int intVal)
        {
            intValue = intVal;
            InitializeComponent();
        }

        private void EmployeeStatusChange_Load(object sender, EventArgs e)
        {
            cmbEstate.DataSource = EstDivBlock.ListEstates();
            cmbEstate.DisplayMember = "EstateName";
            cmbEstate.ValueMember = "EstateID";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            //cmbNewStatus.DataSource = FTSSettings.ListDataFromSettings("EmpStatus");
            //cmbNewStatus.DisplayMember = "Name";
            //cmbNewStatus.ValueMember = "Name";

            cmbNewStatus.Text = txtCurrentStatus.Text;

            cmbBatchCurrentStatus.DataSource = FTSSettings.ListDataFromSettings("EmpStatus");
            cmbBatchCurrentStatus.DisplayMember = "Name";
            cmbBatchCurrentStatus.ValueMember = "Name";

            cmbBatchCurrentStatus_SelectedIndexChanged(null, null);

            
            dtInactiveDuration = FTSSettings.GetInactiveAbsentsDays("InactiveDuration");
            if (dtInactiveDuration.Rows.Count > 0)
            {
                lblInactiveDuration.Text = FTSSettings.GetInactiveAbsentsDays("InactiveDuration").Rows[0][0].ToString();
                lblTerminateDuration.Text = dtInactiveDuration.Rows[0][1].ToString();
            }
            else
            {
                lblInactiveDuration.Text = "NA";
                lblTerminateDuration.Text = "NA";
            }

            if (intValue == 1)
            {

                rbtnBatchWise.Checked = true;
                rbtnBatchWise_CheckedChanged(null, null);
                cmbBatchCurrentStatus_SelectedIndexChanged(null, null);
            }
            else if (intValue == 2)
            {
                this.ControlBox = false;
                this.btnChangeStatus.Enabled = false;
                rbtnBatchWise.Checked = true;
                rbtnBatchWise_CheckedChanged(null, null);
                cmbBatchCurrentStatus_SelectedIndexChanged(null, null);
                btnAutoProcess.Enabled = false;
                btnAutoProcess_Click(null, null);

                this.btnChangeStatus.Enabled = false;
                btnAutoProcess.Enabled = false;
                this.ControlBox = true;

            }
            else
            {
                rbtnEmpWise.Checked = true;
                rbtnEmpWise_CheckedChanged(null, null);
                if (FTSSettings.IsAvailableAutoStatusChange())
                {
                    btnChangeStatus.Enabled = true;
                    btnAutoProcess.Enabled = true;
                   
                }
                else
                {
                    btnChangeStatus.Enabled = false;
                    btnAutoProcess.Enabled = false;
                }
            }

            //groupBox1.Enabled = false;
            //btnChangeStatus.Enabled = false;

            //btnAutoProcess.PerformClick();

            //this.Close();


        }

        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        
        {
            if (e.KeyChar == 13)
            {
                if (txtEmpNo.Text.Equals("?"))
                {
                    EmployeeList empList = new EmployeeList(this, cmbDivision.SelectedValue.ToString());
                    empList.ShowDialog();

                }
                else
                {
                    txtEmpNo_Changed();
                }
            }
        }

        private void txtEmpNo_Changed()
        {
            try
            {
                if (String.IsNullOrEmpty(txtEmpNo.Text))
                {
                    MessageBox.Show("Please Enter a Employee Number");
                }
                else
                {
                    txtEmpNo.Text = txtEmpNo.Text.PadLeft(4, '0');
                    DataSet dsEmpDetails = EmpMaster.GetEmployeeDetailsByEmpNo(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                    DataSet dsLastWork = EmpMaster.getEmployeeLastWorkDetails(cmbDivision.SelectedValue.ToString(), txtEmpNo.Text);
                    if (dsEmpDetails.Tables[0].Rows.Count > 0)
                    {
                        lblEmpName.Text = dsEmpDetails.Tables[0].Rows[0][3].ToString();
                        txtCurrentStatus.Text = dsEmpDetails.Tables[0].Rows[0][4].ToString();
                        if (Convert.ToBoolean(dsEmpDetails.Tables[0].Rows[0][5].ToString()) == true)
                        {
                            rbtnActive.Checked = true;
                        }
                        else
                        {
                            rbtnInactive.Checked = true;
                        }
                        try
                        {
                            cmbNewStatus.Text = dsEmpDetails.Tables[0].Rows[0][4].ToString();
                            cmbNewStatus.Focus();
                            if (dsLastWork.Tables[0].Rows.Count > 0)
                            {
                                txtLastDate.Text = dsLastWork.Tables[0].Rows[0][0].ToString();
                                txtLastRate.Text = dsLastWork.Tables[0].Rows[0][1].ToString();
                            }
                            else
                            {
                                txtLastDate.Text = "NA";
                                txtLastRate.Text = "NA";
                            }
                        }
                        catch { }

                    }
                    else
                    {
                        MessageBox.Show("Employee Not Available");
                        txtEmpNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR !");
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshBatchWiseGrid();
            chkAllEmp.Checked = true;
            chkAllEmp_CheckedChanged(null, null);
            cmbBatchCurrentStatus_SelectedIndexChanged(null, null);
        }

        private void txtEmpNo_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmpNo.Text))
            {
                txtEmpNo_Changed();
            }
        }

        private void cmbEstate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            
            String strStatus = "";
            String EmpDetails = "";
            Boolean boolOk = true;
            if (String.IsNullOrEmpty(txtBatchWiseReason.Text) && rbtnBatchWise.Checked)
            {
                MessageBox.Show("Type A Valid Reason To Continue");
                txtReason.Focus();
            }
            if (String.IsNullOrEmpty(txtReason.Text) && rbtnEmpWise.Checked)
            {
                MessageBox.Show("Type A Valid Reason To Continue");
                txtReason.Focus();
            }
            else if (cmbBatchCurrentStatus.SelectedValue.ToString() == cmbBatchNewStatus.SelectedValue.ToString() && rbtnBatchWise.Checked)
            {
                MessageBox.Show("Current Status and New Status Is Same");
                cmbBatchNewStatus.Focus();
            }
            else if (txtCurrentStatus.Text == cmbNewStatus.SelectedValue.ToString() && rbtnEmpWise.Checked)
            {
                MessageBox.Show("Current Status and New Status Is Same");
                cmbBatchNewStatus.Focus();
            }
            else
            {
                if (intValue == 1)
                {
                    for (int i = 0; i < gvlist.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gvlist.Rows[i].Cells[0].Value.ToString()) == true)
                        {
                            try
                            {
                                objempCurrentStatus.DtTxDate = DateTime.Now.Date;
                                objempCurrentStatus.StrEmpEstate = cmbEstate.SelectedValue.ToString();
                                objempCurrentStatus.StrEmpDivision = cmbDivision.SelectedValue.ToString();
                                objempCurrentStatus.StrEmpNo = gvlist.Rows[i].Cells[2].Value.ToString();
                                objempCurrentStatus.StrEmpName = gvlist.Rows[i].Cells[3].Value.ToString();
                                objempCurrentStatus.StrCurrentStatus = gvlist.Rows[i].Cells[6].Value.ToString();
                                objempCurrentStatus.StrNewStatus = cmbBatchNewStatus.SelectedValue.ToString();
                                objempCurrentStatus.StrReason = txtBatchWiseReason.Text;
                                objempCurrentStatus.StrChangedMethod = "ManuallyUsingCheckroll";
                                objempCurrentStatus.StrUserID = User.StrUserName;
                                if (txtLastDate.Text.Equals("NA"))
                                {
                                    objempCurrentStatus.DtLastWorkedDate = Convert.ToDateTime("1900-1-1");
                                }
                                else
                                {
                                    objempCurrentStatus.DtLastWorkedDate = Convert.ToDateTime(txtLastDate.Text);
                                }
                                if (txtLastRate.Text.Equals("NA"))
                                {
                                    objempCurrentStatus.DecLastWorkRate = 0;
                                }
                                else
                                {
                                    objempCurrentStatus.DecLastWorkRate = Convert.ToDecimal(txtLastRate.Text);
                                }
                                strStatus = objempCurrentStatus.EmployeeStatusChange();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error On Status Change, " + ex.Message);
                                boolOk = false;
                            }
                        }

                    }
                }
                else
                {
                    try
                    {
                        objempCurrentStatus.DtTxDate = DateTime.Now.Date;
                        objempCurrentStatus.StrEmpEstate = cmbEstate.SelectedValue.ToString();
                        objempCurrentStatus.StrEmpDivision = cmbDivision.SelectedValue.ToString();
                        objempCurrentStatus.StrEmpNo = txtEmpNo.Text;
                        objempCurrentStatus.StrEmpName = lblEmpName.Text;
                        objempCurrentStatus.StrCurrentStatus = txtCurrentStatus.Text; ;
                        objempCurrentStatus.StrNewStatus = cmbNewStatus.SelectedValue.ToString();
                        objempCurrentStatus.StrReason = txtReason.Text;
                        objempCurrentStatus.StrChangedMethod = "ManuallyUsingCheckroll";
                        objempCurrentStatus.StrUserID = User.StrUserName;
                        if (txtLastDate.Text.Equals("NA"))
                        {
                            objempCurrentStatus.DtLastWorkedDate = Convert.ToDateTime("1900-1-1");
                        }
                        else
                        {
                            objempCurrentStatus.DtLastWorkedDate = Convert.ToDateTime(txtLastDate.Text);
                        }
                        if (txtLastRate.Text.Equals("NA"))
                        {
                            objempCurrentStatus.DecLastWorkRate = 0;
                        }
                        else
                        {
                            objempCurrentStatus.DecLastWorkRate = Convert.ToDecimal(txtLastRate.Text);
                        }
                        strStatus = objempCurrentStatus.EmployeeStatusChange();
                        EmpDetails += " " + cmbDivision.SelectedValue.ToString() + " : " + txtEmpNo.Text + " ";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error On Status Change, " + ex.Message);
                        boolOk = false;
                    }
                }
                if (boolOk)
                {
                    MessageBox.Show(EmpDetails+" "+" Status Changed From " + objempCurrentStatus.StrCurrentStatus + " To " + objempCurrentStatus.StrNewStatus + " Successfully.");
                }
                else
                {
                    MessageBox.Show(EmpDetails + " " + " Status Changed From " + objempCurrentStatus.StrCurrentStatus + " To " + objempCurrentStatus.StrNewStatus + " Completed Witn Errors.");
                }
                AfterAdd();
            }
            
        }

        private void AfterAdd()
        {

            if (intValue == 1)
            {
                refreshBatchWiseGrid();
            }
            else
            {
                txtEmpNo.Clear();
                lblEmpName.Text = "";
                txtCurrentStatus.Clear();
                txtReason.Clear();
            }
            
            chkAllEmp.Checked = true;
            chkAllEmp_CheckedChanged(null, null);
        }

        private void cmbNewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNewStatus.Text.ToUpper().Equals("ACTIVE"))
            {
                rbtnActive.Checked = true;
            }
            else
            {
                rbtnActive.Checked = true;
            }

            if (txtCurrentStatus.Text.ToUpper().Equals("PAIDOFF") || txtCurrentStatus.Text.ToUpper().Equals("BOOKED"))
            {
                if (rbtnActive.Checked == true)
                {
                    MessageBox.Show("Paid Off Or Booked employee Cannot Be Active");
                    rbtnInactive.Checked = true;
                }                
            }

            if (cmbNewStatus.Text.ToUpper().Equals("PAIDOFF") || cmbNewStatus.Text.ToUpper().Equals("BOOKED") || cmbNewStatus.Text.ToUpper().Equals("INACTIVE"))
            {
                if (rbtnActive.Checked == true)
                {
                    MessageBox.Show("Paid Off Or Booked employee Cannot Be Active");
                    rbtnInactive.Checked = true;
                }
            }
        }

        private void rbtnEmpWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnEmpWise.Checked)
            {
                gbEmpWise.Enabled = true;
                gbBatchWise.Enabled = false;
            }
            else
            {
                gbBatchWise.Enabled = true;
                gbEmpWise.Enabled = false;
            }
        }

        private void chkAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllEmp.Checked == true)
            {
                for (int i = 0; i < gvlist.Rows.Count; i++)
                {

                    gvlist.Rows[i].Cells[0].Value = true;

                }
            }
            if (chkAllEmp.Checked == false)
            {
                for (int i = 0; i < gvlist.Rows.Count; i++)
                {

                    gvlist.Rows[i].Cells[0].Value = false;

                }
            }
        }

        private void rbtnBatchWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBatchWise.Checked==true)
            {
                gbBatchWise.Enabled = true;
                gbEmpWise.Enabled = false;                
            }
            else
            {
                gbEmpWise.Enabled = true;
                gbBatchWise.Enabled = false;
            }
        }

        private void refreshBatchWiseGrid()
        {
            try
            {

                if (dtInactiveDuration.Rows.Count > 0)
                {
                    int intval = Convert.ToInt32(Convert.ToDecimal(dtInactiveDuration.Rows[0][0].ToString()));
                    gvlist.DataSource = objempCurrentStatus.getListOfEmployeesAboveToChangeStatus(cmbEstate.SelectedValue.ToString(), cmbDivision.SelectedValue.ToString(), cmbBatchCurrentStatus.SelectedValue.ToString(), intval,true);
                    gvlist.ReadOnly = true;
                }
            }
            catch(Exception ex) { }

        }

        private void cmbBatchCurrentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbBatchNewStatus.DataSource = objempCurrentStatus.GetPossibleNewStatusList(cmbBatchCurrentStatus.SelectedValue.ToString());
                cmbBatchNewStatus.DisplayMember = "Status";
                cmbBatchNewStatus.ValueMember = "Status";
                cmbBatchNewStatus.SelectedValue = cmbBatchCurrentStatus.SelectedValue.ToString();


            }
            catch { }

            refreshBatchWiseGrid();
            chkAllEmp.Checked = true;
            chkAllEmp_CheckedChanged(null, null);
        }

        private void cmbBatchNewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAutoProcess_Click(object sender, EventArgs e)
        {
            String strMessageText = "";
            String strStatus = "";
            Boolean boolOk = true;
            DataTable dtDivisions = EstDivBlock.ListEstateDivisions();
            DataTable dtEmployees;
            foreach (DataRow drow in dtDivisions.Rows)
            {
                //Active to Inactive
                dtEmployees = objempCurrentStatus.getListOfEmployeesAboveToChangeStatus(EstDivBlock.getEstateId(), drow[0].ToString(), "Active", Convert.ToInt32(dtInactiveDuration.Rows[0][0].ToString()),true);
                if (dtEmployees.Rows.Count > 0)
                {
                    foreach (DataRow drowEmp in dtEmployees.Rows)
                    {
                        try
                        {
                            objempCurrentStatus.DtTxDate = DateTime.Now.Date;
                            objempCurrentStatus.StrEmpEstate = EstDivBlock.getEstateId();
                            objempCurrentStatus.StrEmpDivision = drow[0].ToString();
                            objempCurrentStatus.StrEmpNo = drowEmp[2].ToString();
                            objempCurrentStatus.StrCurrentStatus = "Active";
                            objempCurrentStatus.StrNewStatus = "Inactive";
                            objempCurrentStatus.StrReason = "AbsentToWork";
                            objempCurrentStatus.StrChangedMethod = "ByAutoProcess";
                            objempCurrentStatus.StrUserID = User.StrUserName;
                            objempCurrentStatus.DtLastWorkedDate = Convert.ToDateTime(drowEmp[7].ToString());
                            objempCurrentStatus.DecLastWorkRate = Convert.ToDecimal(drowEmp[9].ToString());
                            strStatus = objempCurrentStatus.EmployeeStatusChange();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error On Status Change, " + ex.Message);
                            boolOk = false;
                        }
                    }
                    if (boolOk)
                    {
                        strMessageText += drow[1].ToString() + " Employees Status Changed From Active To Inactive Successfully.\r\n";
                    }
                    else
                    {
                        strMessageText += drow[1].ToString() + " Employees Status Changed From Active To Inactive Complted With Errors.\r\n";
                    }
                }
                
                

            }
            if(strMessageText.Length>0)
                MessageBox.Show(strMessageText);

            //Terminate employees
            foreach (DataRow drow in dtDivisions.Rows)
            {
                //Inactive to Terminate
                dtEmployees = objempCurrentStatus.getListOfEmployeesAboveToChangeStatus(EstDivBlock.getEstateId(), drow[0].ToString(), "Terminate", Convert.ToInt32(dtInactiveDuration.Rows[0][0].ToString()), false);
                if (dtEmployees.Rows.Count > 0)
                {
                    foreach (DataRow drowEmp in dtEmployees.Rows)
                    {
                        try
                        {
                            objempCurrentStatus.DtTxDate = DateTime.Now.Date;
                            objempCurrentStatus.StrEmpEstate = EstDivBlock.getEstateId();
                            objempCurrentStatus.StrEmpDivision = drow[0].ToString();
                            objempCurrentStatus.StrEmpNo = drowEmp[2].ToString();
                            objempCurrentStatus.StrCurrentStatus = "Inactive";
                            objempCurrentStatus.StrNewStatus = "Terminated";
                            objempCurrentStatus.StrReason = "AbsentToWork";
                            objempCurrentStatus.StrChangedMethod = "ByAutoProcess";
                            objempCurrentStatus.StrUserID = User.StrUserName;
                            objempCurrentStatus.DtLastWorkedDate = Convert.ToDateTime(drowEmp[7].ToString());
                            objempCurrentStatus.DecLastWorkRate = Convert.ToDecimal(drowEmp[9].ToString());
                            strStatus = objempCurrentStatus.EmployeeStatusChange();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error On Status Change, " + ex.Message);
                            boolOk = false;
                        }
                    }
                    if (boolOk)
                    {
                        strMessageText += drow[1].ToString() + " Employees Status Changed From Active To Inactive Successfully.\r\n";
                    }
                    else
                    {
                        strMessageText += drow[1].ToString() + " Employees Status Changed From Active To Inactive Complted With Errors.\r\n";
                    }
                }

            }
            if (strMessageText.Length > 0)
                MessageBox.Show(strMessageText);
            //-------------------------------------
            DataSet dsEmpGangsReport = new DataSet();
            dsEmpGangsReport = objempCurrentStatus.ListEmployeeStatusChangeLog("%",Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()));
            if (dsEmpGangsReport.Tables[0].Rows.Count > 0)
            {
                dsEmpGangsReport.WriteXml("EmployeeStatusChangeLog.xml");

                EmployeeStatusChangeLogRPT objReport = new EmployeeStatusChangeLogRPT();
                objReport.SetDataSource(dsEmpGangsReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (chkAllDiv.Checked)
            {
                DataSet dsEmpGangsReport = new DataSet();
                dsEmpGangsReport = objempCurrentStatus.ListEmployeeStatusChangeLog("%", Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()));
                dsEmpGangsReport.WriteXml("EmployeeStatusChangeLog.xml");

                EmployeeStatusChangeLogRPT objReport = new EmployeeStatusChangeLogRPT();
                objReport.SetDataSource(dsEmpGangsReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                DataSet dsEmpGangsReport = new DataSet();
                dsEmpGangsReport = objempCurrentStatus.ListEmployeeStatusChangeLog(cmbDivision.SelectedValue.ToString());
                dsEmpGangsReport.WriteXml("EmployeeStatusChangeLog.xml");

                EmployeeStatusChangeLogRPT objReport = new EmployeeStatusChangeLogRPT();
                objReport.SetDataSource(dsEmpGangsReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
        }

        private void txtCurrentStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCurrentStatus.Text))
                {
                    cmbNewStatus.DataSource = objempCurrentStatus.GetPossibleNewStatusList(txtCurrentStatus.Text);
                    cmbNewStatus.DisplayMember = "Status";
                    cmbNewStatus.ValueMember = "Status";
                    cmbNewStatus.SelectedValue = txtCurrentStatus.Text;
                }


            }
            catch { }
        }

        private void cmbNewStatus_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbNewStatus.Text.ToUpper().Equals("ACTIVE"))
            {
                rbtnActive.Checked = true;
            }
            else
            {
                rbtnInactive.Checked = true;
            }
        }

        private void btnAboveToChange_Click(object sender, EventArgs e)
        {
            if (chkAllDiv.Checked)
            {
                DataSet dsEmpGangsReport = new DataSet();
                dsEmpGangsReport.Tables.Add(objempCurrentStatus.getListOfEmployeesAboveToChangeStatus(EstDivBlock.getEstateId(), "%", "Active", Convert.ToInt32(dtInactiveDuration.Rows[0][0].ToString()), true));
                //dsEmpGangsReport = objempCurrentStatus.ListEmployeeStatusChangeLog("%", Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()));
                if (dsEmpGangsReport.Tables[0].Rows.Count > 0)
                {
                    dsEmpGangsReport.WriteXml("EmployeesAboveToChange.xml");

                    EmployeesAboveToInactiveRPT objReport = new EmployeesAboveToInactiveRPT();
                    objReport.SetDataSource(dsEmpGangsReport);
                    ReportViewerForm objReportViewer = new ReportViewerForm();

                    objReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                    objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                    objReportViewer.crystalReportViewer1.ReportSource = objReport;
                    objReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            else
            {

                DataSet dsEmpGangsReport = new DataSet();
                dsEmpGangsReport.Tables.Add(objempCurrentStatus.getListOfEmployeesAboveToChangeStatus(EstDivBlock.getEstateId(),cmbDivision.SelectedValue.ToString(), "Active", Convert.ToInt32(dtInactiveDuration.Rows[0][0].ToString()), true));
                //dsEmpGangsReport = objempCurrentStatus.ListEmployeeStatusChangeLog("%", Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()));

                if (dsEmpGangsReport.Tables[0].Rows.Count > 0)
                {
                    dsEmpGangsReport.WriteXml("EmployeesAboveToChange.xml");

                    EmployeesAboveToInactiveRPT objReport = new EmployeesAboveToInactiveRPT();
                    objReport.SetDataSource(dsEmpGangsReport);
                    ReportViewerForm objReportViewer = new ReportViewerForm();

                    objReport.SetParameterValue("Company", FTSPayRollBL.Company.getCompanyName());
                    objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                    objReportViewer.crystalReportViewer1.ReportSource = objReport;
                    objReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }

            }
        }
    }
}