using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class FieldSettings : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.FieldSettings ClsFieldSettings = new FTSPayRollBL.FieldSettings();
        FTSPayRollBL.FTSCheckRollSettings ClsSettings = new FTSPayRollBL.FTSCheckRollSettings();

        public FieldSettings()
        {
            InitializeComponent();
        }

        private void FieldSettings_Load(object sender, EventArgs e)
        {
            cmbType.DataSource = ClsSettings.ListDataFromSettings("FieldSetting");
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "Name";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";


            cmbDivision_SelectedIndexChanged(null, null);

        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbDivision.SelectedItem.ToString().Equals(""))
            {
                cmbField.DataSource = EstDivBlock.ListDivisionFields(cmbDivision.SelectedValue.ToString());
                cmbField.DisplayMember = "FieldID";
                cmbField.ValueMember = "FieldID";

            }
            RefreshGrids();

        }

        private void btnAddOkgRate_Click(object sender, EventArgs e)
        {
            try
            {
                ClsFieldSettings.StrDivision = cmbDivision.SelectedValue.ToString();
                ClsFieldSettings.StrField = cmbField.SelectedValue.ToString();
                ClsFieldSettings.DecOkgRate = Convert.ToDecimal(txtOkgRate.Text);
                ClsFieldSettings.StrType = cmbType.SelectedValue.ToString();
                ClsFieldSettings.InsertFieldOkgRate();
                AfterAdd();
                MessageBox.Show(ClsFieldSettings.StrDivision+"-"+ClsFieldSettings.StrField+"-"+"Over Kilo Rate Added Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Over Kilo Rate Failed, "+ex.Message);
            }
        }

        private void btnUpdateOkgRate_Click(object sender, EventArgs e)
        {
            try
            {
                ClsFieldSettings.StrDivision = cmbDivision.SelectedValue.ToString();
                ClsFieldSettings.StrField = cmbField.SelectedValue.ToString();
                ClsFieldSettings.DecOkgRate = Convert.ToDecimal(txtOkgRate.Text);
                ClsFieldSettings.StrType = cmbType.SelectedValue.ToString();
                ClsFieldSettings.UpdateFieldOkgRate();
                AfterAdd();
                MessageBox.Show(ClsFieldSettings.StrDivision + "-" + ClsFieldSettings.StrField + "-" + "Over Kilo Rate Updated Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Over Kilo Rate Failed, " + ex.Message);
            }
        }

        private void btnDeleteOkgRate_Click(object sender, EventArgs e)
        {
            try
            {
                ClsFieldSettings.StrDivision = cmbDivision.SelectedValue.ToString();
                ClsFieldSettings.StrField = cmbField.SelectedValue.ToString();
                ClsFieldSettings.DecOkgRate = Convert.ToDecimal(txtOkgRate.Text);
                ClsFieldSettings.StrType = cmbType.SelectedValue.ToString();
                ClsFieldSettings.DeleteFieldOkgRate();
                AfterAdd();
                MessageBox.Show(ClsFieldSettings.StrDivision + "-" + ClsFieldSettings.StrField + "-" + "Over Kilo Rate Deleted Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Over Kilo Rate Failed, " + ex.Message);
            }
        }

        private void AfterAdd()
        {
            cmbDivision.Enabled = true;
            cmbField.Enabled = true;
            cmbType.Enabled = true;
            btnAddOkgRate.Enabled = true;
            btnUpdateOkgRate.Enabled = false;
            btnDeleteOkgRate.Enabled = false;
            try
            {
                gvOverKgRate.DataSource= ClsFieldSettings.ListFieldOverKgRates(cmbDivision.SelectedValue.ToString(), cmbType.SelectedValue.ToString());
            }
            catch { }
        }

        private void RefreshGrids()
        {
            try
            {
                gvOverKgRate.DataSource = ClsFieldSettings.ListFieldOverKgRates(cmbDivision.SelectedValue.ToString(), cmbType.SelectedValue.ToString());
            }
            catch { }
        }

        private void gvOverKgRate_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbDivision.Enabled = false;
            cmbField.Enabled = false;
            cmbType.Enabled = false;
            btnAddOkgRate.Enabled = false;
            btnUpdateOkgRate.Enabled = true;
            btnDeleteOkgRate.Enabled = true;
            try
            {
                cmbField.SelectedValue = gvOverKgRate.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtOkgRate.Text = gvOverKgRate.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dsDivisionReport = new DataTable();
            dsDivisionReport = ClsFieldSettings.GetFieldSettings().Tables[0];
            if (dsDivisionReport.Rows.Count > 0)
            {
                dsDivisionReport.WriteXml("FiledSettings.xml");

                FieldSettingsRPT objReport = new FieldSettingsRPT();
                objReport.SetDataSource(dsDivisionReport);
                ReportViewerForm objReportViewer = new ReportViewerForm();

                objReport.SetParameterValue("Estate", EstDivBlock.ListEstates().Rows[0][0].ToString());
                objReport.SetParameterValue("CompanyName", FTSPayRollBL.Company.getCompanyName());
                objReportViewer.crystalReportViewer1.ReportSource = objReport;
                objReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data To Preview.");
            }
        }

        


    }
}