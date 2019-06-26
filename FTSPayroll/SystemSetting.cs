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
    public partial class SystemSetting : Form
    {
        FTSPayRollBL.SystemSetting myObj = new FTSPayRollBL.SystemSetting();
        DataTable rateList = null;
        DataTable DeductList = null;

        public SystemSetting()
        {
            InitializeComponent();
        }

        private void SystemSetting_Load(object sender, EventArgs e)
        {
            try
            {
                dgvRates.Columns.Add("Type", "Type");
                dgvRates.Columns.Add("Name", "Name");
                dgvRates.Columns.Add("Description", "Description");
                dgvRates.Columns.Add("Amount", "Amount");
                dgvRates.Columns.Add("Editor's Name", "Editor's Name");
                dgvRates.Columns.Add("Reason to Change Amount", "Reason to Change Amount");

                dgvDeduction.Columns.Add("Short Name", "Short Name");
                dgvDeduction.Columns.Add("Deduction Name", "Deduction Name");
                dgvDeduction.Columns.Add("Group", "Group");
                dgvDeduction.Columns.Add("Amount", "Amount");
                dgvDeduction.Columns.Add("Active Status", "Active Status");
                dgvDeduction.Columns.Add("Editor's Name", "Editor's Name");
                dgvDeduction.Columns.Add("Reason to Change Amount", "Reason to Change Amount");



                rateList = myObj.viewCheckRollRates();
                cmbRateList.DataSource = rateList;
                cmbRateList.DisplayMember = "Name";
                cmbRateList.ValueMember = "Type";
                DeductList = myObj.viewSystemDeductionDetail();
                cmbDeductionList.DataSource = DeductList;
                cmbDeductionList.DisplayMember = "Deduction Name";
                cmbDeductionList.ValueMember = "Short Name";
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Decimal rates = 0;
            Boolean rateFlag = false, deducFlag = false;
            try
            {
                foreach (DataGridViewRow dgvr in dgvRates.Rows)
                {
                    if (Decimal.TryParse(dgvr.Cells[3].Value.ToString(), out rates))
                    {
                        if (myObj.isRateChange(rates, dgvr.Cells[0].Value.ToString().Trim()))
                        {
                            if (!String.IsNullOrEmpty(dgvr.Cells[4].Value.ToString()) && !String.IsNullOrEmpty(dgvr.Cells[5].Value.ToString()))
                            {
                                Decimal currRateVal = myObj.getCurrentRate(dgvr.Cells[0].Value.ToString().Trim());
                                myObj.updateRate(dgvr.Cells[4].Value.ToString().Trim(), FTSPayRollBL.User.StrUserName.ToString(), FTSPayRollBL.User.StrUserPassword.ToString(), dgvr.Cells[5].Value.ToString().Trim(), rates, DateTime.Now, dgvr.Cells[0].Value.ToString().Trim(), dgvr.Cells[2].Value.ToString(), currRateVal);
                                rateFlag = true;
                            }
                            else
                            {
                                MessageBox.Show("If you change the Rate amount, you must fill both yourname and reason for the change..", "Update was not success");
                                cmbRateList_SelectedIndexChanged(null, null);
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please check amount");
                        break;
                    }
                }

                Decimal deducAmnt = 0;
                foreach (DataGridViewRow dgvr in dgvDeduction.Rows)
                {
                    if (Decimal.TryParse(dgvr.Cells[3].Value.ToString(), out deducAmnt))
                    {
                        if (myObj.isDeductionChange(deducAmnt, dgvr.Cells[2].Value.ToString(), dgvr.Cells[0].Value.ToString()))
                        {
                            if (!String.IsNullOrEmpty(dgvr.Cells[5].Value.ToString()) && !String.IsNullOrEmpty(dgvr.Cells[6].Value.ToString()))
                            {
                                Decimal currAmnt = myObj.getCurrentDeduction(dgvr.Cells[2].Value.ToString().Trim(), dgvr.Cells[0].Value.ToString().Trim());
                                myObj.updateDeductions(dgvr.Cells[5].Value.ToString().Trim(), FTSPayRollBL.User.StrUserName.ToString(), FTSPayRollBL.User.StrUserPassword.ToString(), dgvr.Cells[6].Value.ToString().Trim(), deducAmnt, DateTime.Now, dgvr.Cells[2].Value.ToString().Trim(), dgvr.Cells[0].Value.ToString().Trim(), dgvr.Cells[1].Value.ToString(), currAmnt);
                                deducFlag = true;
                            }
                            else
                            {
                                MessageBox.Show("If you change the Deduction amount, you must fill both yourname and reason for the change..", "Update was not success");
                                cmbDeductionList_SelectedIndexChanged(null, null);
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please check amount");
                        break;
                    }
                }
                if (deducFlag || rateFlag)
                    MessageBox.Show("Update successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmbDeductionList_SelectedIndexChanged(null, null);
                cmbRateList_SelectedIndexChanged(null, null);
            }
        }

        private void dgvRates_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Decimal rates = 0;
            if (e.ColumnIndex == 3)
            {
                if (Decimal.TryParse(e.FormattedValue.ToString(), out rates))
                {
                    dgvRates[e.ColumnIndex, e.RowIndex].ErrorText = null;
                }
                else
                {
                    dgvRates[e.ColumnIndex, e.RowIndex].ErrorText = "Invalid amount";
                }
            }
            else if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dgvRates[e.ColumnIndex, e.RowIndex].ErrorText = "You must be fill this field";
                }
                else
                {
                    dgvRates[e.ColumnIndex, e.RowIndex].ErrorText = null;
                }
            }
        }

        private void dgvDeduction_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Decimal rate = 0;
            if (e.ColumnIndex == 3)
            {
                if (Decimal.TryParse(e.FormattedValue.ToString(), out rate))
                {
                    dgvDeduction[e.ColumnIndex, e.RowIndex].ErrorText = null;
                }
                else
                {
                    dgvDeduction[e.ColumnIndex, e.RowIndex].ErrorText = "Invalid amount";
                }
            }
            else if(e.ColumnIndex==5||e.ColumnIndex==6)
            {
                if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dgvDeduction[e.ColumnIndex, e.RowIndex].ErrorText = "You must be fill this field";
                }
                else
                {
                    dgvDeduction[e.ColumnIndex, e.RowIndex].ErrorText = null;
                }
            }
        }

        private void cmbRateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvRates.Rows.Clear();
                dgvRates.Rows.Add();
                dgvRates.Rows[0].Cells[0].Value = rateList.Rows[cmbRateList.SelectedIndex][0];
                dgvRates.Rows[0].Cells[1].Value = rateList.Rows[cmbRateList.SelectedIndex][1];
                dgvRates.Rows[0].Cells[2].Value = rateList.Rows[cmbRateList.SelectedIndex][2];
                dgvRates.Rows[0].Cells[3].Value = rateList.Rows[cmbRateList.SelectedIndex][3];
                dgvRates.Rows[0].Cells[4].Value = rateList.Rows[cmbRateList.SelectedIndex][4];
                dgvRates.Rows[0].Cells[5].Value = rateList.Rows[cmbRateList.SelectedIndex][5];

                if (dgvRates.Rows.Count != 0)
                {
                    dgvRates.Columns[0].ReadOnly = true;
                    dgvRates.Columns[1].ReadOnly = true;
                    dgvRates.Columns[2].ReadOnly = true;
                    dgvRates.Columns[3].ReadOnly = false;
                    dgvRates.Columns[0].Width = 100;
                    dgvRates.Columns[1].Width = 150;
                    dgvRates.Columns[2].Width = 250;
                    dgvRates.Columns[3].Width = 100;
                    dgvRates.Columns[4].Width = 250;
                    dgvRates.Columns[5].Width = 700;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbDeductionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDeduction.Rows.Clear();
                dgvDeduction.Rows.Add();
                dgvDeduction.Rows[0].Cells[0].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][0];
                dgvDeduction.Rows[0].Cells[1].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][1];
                dgvDeduction.Rows[0].Cells[2].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][2];
                dgvDeduction.Rows[0].Cells[3].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][3];
                dgvDeduction.Rows[0].Cells[4].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][4];
                dgvDeduction.Rows[0].Cells[5].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][5];
                dgvDeduction.Rows[0].Cells[6].Value = DeductList.Rows[cmbDeductionList.SelectedIndex][6];

                if (dgvDeduction.Rows.Count != 0)
                {
                    dgvDeduction.Columns[0].ReadOnly = true;
                    dgvDeduction.Columns[1].ReadOnly = true;
                    dgvDeduction.Columns[2].ReadOnly = true;
                    dgvDeduction.Columns[3].ReadOnly = false;
                    dgvDeduction.Columns[4].ReadOnly = true;
                    dgvDeduction.Columns[0].Width = 100;
                    dgvDeduction.Columns[1].Width = 200;
                    dgvDeduction.Columns[2].Width = 200;
                    dgvDeduction.Columns[3].Width = 100;
                    dgvDeduction.Columns[4].Width = 100;
                    dgvDeduction.Columns[5].Width = 250;
                    dgvDeduction.Columns[6].Width = 700;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}