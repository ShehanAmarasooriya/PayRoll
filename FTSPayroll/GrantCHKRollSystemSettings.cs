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
    public partial class GrantCHKRollSystemSettings : Form
    {
        FTSPayRollBL.GrantSystemSettings myObj = new GrantSystemSettings();
        public GrantCHKRollSystemSettings()
        {
            InitializeComponent();
        }

        private void GrantCHKRollSystemSettings_Load(object sender, EventArgs e)
        {
            try
            {
                //Create Columns for the DatagridView
                DataTable dtDeduction=myObj.viewSystemDeductionDetail();
                dgvDeductions.Columns.Add("Short Name", "Short Name");
                dgvDeductions.Columns.Add("Deduction Name", "Deduction Name");
                dgvDeductions.Columns.Add("Group", "Group");
                dgvDeductions.Columns.Add("Amount", "Amount");
                dgvDeductions.Columns.Add("Active Status", "Active Status");
                DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
                chkColumn.HeaderText = "Access Priviladge";
                dgvDeductions.Columns.Add(chkColumn);
                //End Creating Columns for the DatagridView
                //Iterate through the datatable and add values to the DataGridView
                for (int i = 0; i <= dtDeduction.Rows.Count - 1; i++)
                {
                    dgvDeductions.Rows.Add();
                    dgvDeductions.Rows[i].Cells[0].Value = dtDeduction.Rows[i][0];
                    dgvDeductions.Rows[i].Cells[1].Value = dtDeduction.Rows[i][1];
                    dgvDeductions.Rows[i].Cells[2].Value = dtDeduction.Rows[i][2];
                    dgvDeductions.Rows[i].Cells[3].Value = dtDeduction.Rows[i][3];
                    dgvDeductions.Rows[i].Cells[4].Value = dtDeduction.Rows[i][4];
                    dgvDeductions.Rows[i].Cells[5].Value = dtDeduction.Rows[i][5];
                }
                //Lock Datagridview Columns
                dgvDeductions.Columns[0].ReadOnly = true;
                dgvDeductions.Columns[1].ReadOnly = true;
                dgvDeductions.Columns[2].ReadOnly = true;
                dgvDeductions.Columns[3].ReadOnly = true;
                dgvDeductions.Columns[4].ReadOnly = true;
                dgvDeductions.Columns[5].ReadOnly = false;
                dgvDeductions.Columns[2].Width = 115;
                dgvDeductions.Columns[3].Width = 115;

                DataTable dtRates = myObj.viewCheckRollRatesDetail();
                //Create Columns for the DatagridView
                dgvRates.Columns.Add("Type", "Type");
                dgvRates.Columns.Add("Name", "Name");
                dgvRates.Columns.Add("Description", "Description");
                dgvRates.Columns.Add("Amount", "Amount");
                DataGridViewCheckBoxColumn chkColumn2 = new DataGridViewCheckBoxColumn();
                chkColumn2.HeaderText = "Access Priviladge";
                dgvRates.Columns.Add(chkColumn2);
                //End Creating Columns for the DatagridView
                //Iterate through the datatable and add values to the DataGridView
                for (int j = 0; j <= dtRates.Rows.Count - 1; j++)
                {
                    dgvRates.Rows.Add();
                    dgvRates.Rows[j].Cells[0].Value = dtRates.Rows[j][0];
                    dgvRates.Rows[j].Cells[1].Value = dtRates.Rows[j][1];
                    dgvRates.Rows[j].Cells[2].Value = dtRates.Rows[j][2];
                    dgvRates.Rows[j].Cells[3].Value = dtRates.Rows[j][3];
                    dgvRates.Rows[j].Cells[4].Value = dtRates.Rows[j][4];
                }
                //Lock Datagridview Columns
                dgvRates.Columns[0].ReadOnly = true;
                dgvRates.Columns[1].ReadOnly = true;
                dgvRates.Columns[2].ReadOnly = true;
                dgvRates.Columns[3].ReadOnly = true;
                dgvRates.Columns[4].ReadOnly = false;
                dgvRates.Columns[2].Width = 230;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dgvRates.Rows)
                {
                    myObj.updateRateAccessGrant(r.Cells[0].Value.ToString().Trim(), Convert.ToBoolean(r.Cells[4].Value));
                }
                foreach (DataGridViewRow r in dgvDeductions.Rows)
                {
                    myObj.updateDeductionAccessGrant(r.Cells[0].Value.ToString().Trim(), r.Cells[2].Value.ToString().Trim(), Convert.ToBoolean(r.Cells[5].Value));
                }
                MessageBox.Show("Access modified successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}