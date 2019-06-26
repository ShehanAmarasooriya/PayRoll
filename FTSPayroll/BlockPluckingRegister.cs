using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class BlockPluckingRegister : Form
    {
        FTSPayRollBL.Division mydivision = new FTSPayRollBL.Division();
        FTSPayRollBL.CheckRollReports myreport = new FTSPayRollBL.CheckRollReports();

        public BlockPluckingRegister()
        {
            InitializeComponent();
        }

        private void BlockPluckingRegister_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = mydivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            
            ds = myreport.getBlockPluckingRegister((cmbDivision.SelectedValue.ToString()), (dtDate.Value.Date));
            ds.WriteXml("BlockPluckingRegister.xml");

            if (ds.Tables[0].Rows.Count > 0)
            {
                BlockPluckingRegisterRPT myDailyRep = new BlockPluckingRegisterRPT();
                myDailyRep.SetDataSource(ds);
                ReportViewer myReportViewer = new ReportViewer();

                myDailyRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                myDailyRep.SetParameterValue("Date", "Date : " + dtDate.Value.Date);
                myDailyRep.SetParameterValue("Division", "Division : " + cmbDivision.SelectedValue.ToString());                
                myDailyRep.SetParameterValue("WorkType", "Block Plucking");
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