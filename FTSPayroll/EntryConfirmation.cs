using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EntryConfirmation : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.PreviewMonthlyWages preWages = new FTSPayRollBL.PreviewMonthlyWages();
        FTSPayRollBL.FTSCheckRollSettings FTSSettings = new FTSPayRollBL.FTSCheckRollSettings();
        FTSPayRollBL.User myUser = new FTSPayRollBL.User();

        public EntryConfirmation()
        {
            InitializeComponent();
        }

        private void EntryConfirmation_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbWorkType.DataSource = FTSSettings.ListDataFromSettings("WorkType");
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Code";
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        private void RefreshGrid()
        {
            String strDiv="%";
            if(chkAll.Checked)
            {
                strDiv="%";
            }
            else
                strDiv=cmbDivision.SelectedValue.ToString();

            try
            {
                gvList.DataSource = preWages.GetMoreThanOneEntriesAddedToSameType(dtpFrom.Value.Date, dtpTo.Value.Date, Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), strDiv).Tables[0];
            }
            catch
            {
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dtpFrom_Leave(object sender, EventArgs e)
        {
            //RefreshGrid();
        }

        private void dtpTo_Leave(object sender, EventArgs e)
        {
            //RefreshGrid();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void cmbWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
            
        }

        private void gvList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            ds = preWages.GetMoreThanOneEntriesAddedToSameTypeDetail(Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString()), Convert.ToInt32(gvList.Rows[e.RowIndex].Cells[1].Value.ToString()), gvList.Rows[e.RowIndex].Cells[2].Value.ToString(), gvList.Rows[e.RowIndex].Cells[3].Value.ToString());
            ds.WriteXml("EntryConfirmationRegDetail.xml");
            EntryConfirmationCheckDetailRPT myDailyRep = new EntryConfirmationCheckDetailRPT();
            myDailyRep.SetDataSource(ds);
            ReportViewer myReportViewer = new ReportViewer();

            myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myDailyRep.SetParameterValue("Date", Convert.ToDateTime(gvList.Rows[e.RowIndex].Cells[0].Value.ToString()).ToShortDateString());
            myDailyRep.SetParameterValue("Division", "Division : " + gvList.Rows[e.RowIndex].Cells[2].Value.ToString());
            if (Convert.ToInt32(gvList.Rows[e.RowIndex].Cells[3].Value.ToString()) == 1)
            {
                myDailyRep.SetParameterValue("General", " Normal Work");
            }
            else
            {
                myDailyRep.SetParameterValue("General", " Cash Work");
            }
                    
            myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
            myReportViewer.Show();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (myUser.IsAdminUser(FTSPayRollBL.User.StrUserName))
            {
                String strDiv = "%";
                if (chkAll.Checked)
                {
                    strDiv = "%";
                }
                else
                {
                    strDiv = cmbDivision.SelectedValue.ToString();
                }
                try
                {
                    if (MessageBox.Show("Confirm, "+ cmbWorkType.Text+" Data From " + dtpFrom.Value.Date.ToShortDateString() + " To:" + dtpTo.Value.Date.ToShortDateString() + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        preWages.UpdateMoreThanOneEntriesAsConfirmed(Convert.ToDateTime(dtpFrom.Value.Date.ToShortDateString()), Convert.ToDateTime(dtpTo.Value.Date.ToShortDateString()), Convert.ToInt32(cmbWorkType.SelectedValue.ToString()), strDiv);
                        MessageBox.Show("Date From " + dtpFrom.Value.Date.ToShortDateString() + " To:" + dtpTo.Value.Date.ToShortDateString() + "-" + cmbWorkType.Text + " Data Confirmed Successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, " + ex.Message);
                }
            }
            else
                MessageBox.Show("You Have No Permission To Confirm.","No Permission",MessageBoxButtons.OK,MessageBoxIcon.Stop);

        }
    }
}