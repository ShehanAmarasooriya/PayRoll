using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DailyHarvestRgisterRubber : Form
    {
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myreport = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();
        FTSPayRollBL.FTSCheckRollSettings settings = new FTSPayRollBL.FTSCheckRollSettings();

        public DailyHarvestRgisterRubber()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DailyHarvestRgisterRubber_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

            rbGeneral.Checked = true;

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            Int32 wrkType = 1;

            if (rbGeneral.Checked)
            {
                wrkType = 1;
            }
            else
            {
                wrkType = 2;
            }

            ds = myreport.getHarvestRegisterRubber(cmbDivision.SelectedValue.ToString(), Convert.ToDateTime(dtDate.Value.Date.ToShortDateString()), wrkType);
                    ds.WriteXml("DailyHarvestRegisterRubberRep.xml");
               
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {

                DailyHarvestRegisterRubberRPT myDailyRep = new DailyHarvestRegisterRubberRPT();
                    myDailyRep.SetDataSource(ds);
                    ReportViewer myReportViewer = new ReportViewer();

                    myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myDailyRep.SetParameterValue("Date", dtDate.Value.Date.ToShortDateString());
                    myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.Text);
                    if (wrkType == 1)
                    {
                        myDailyRep.SetParameterValue("General", "For Normal Work");
                    }
                    else
                    {
                        myDailyRep.SetParameterValue("General", "For Cash Work");
                    }
                    myDailyRep.SetParameterValue("CashWork", "");
                    try
                    {
                        if (myEntries.IsDayExistsInCHKDateConfirmations(dtDate.Value.Date))
                        {
                            if (Convert.ToBoolean(myEntries.IsDailyEntryConfirmed(dtDate.Value.Date).Rows[0][0].ToString()) == true)
                            {
                                myDailyRep.SetParameterValue("ConfirmYesNo", "Entries Confirmed");
                            }
                            else
                            {
                                myDailyRep.SetParameterValue("ConfirmYesNo", "Confirmation Pending");
                            }
                        }
                        else
                        {
                            myDailyRep.SetParameterValue("ConfirmYesNo", "Confirmation Pending");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    myReportViewer.crystalReportViewer1.ReportSource = myDailyRep;
                    myReportViewer.Show();
                


            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }
    }
}