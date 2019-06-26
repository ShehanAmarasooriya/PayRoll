using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class IntakePerPlucker : Form
    {
        public IntakePerPlucker()
        {
            InitializeComponent();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            //BusinessLayer.MISReport myIntakePerPlucker = new BusinessLayer.MISReport();
            BusinessLayer.MISReport myIntakePerPlucker = new BusinessLayer.MISReport();
            DataSet dataSetReport = new DataSet();
            dataSetReport.Tables.Add(myIntakePerPlucker.ViewIntakePerPlucker(dtFromDate.Value.Date, dtToDate.Value.Date));
            dataSetReport.WriteXml("IntakePerPlucker.xml");


            IntakePerPluckerRPT myIntakeperPluckerRpt = new IntakePerPluckerRPT();
            myIntakeperPluckerRpt.SetDataSource(dataSetReport);

            //BusinessLayer.GetEstateID myGetEstId = new BusinessLayer.GetEstateID();
            FTSPayRollBL.Division myGetEstId = new FTSPayRollBL.Division();
            myIntakeperPluckerRpt.SetParameterValue("companyName", myGetEstId.ListEstate().Columns[1].ToString());
            myIntakeperPluckerRpt.SetParameterValue("FromDate", dtFromDate.Value.Date);
            myIntakeperPluckerRpt.SetParameterValue("ToDate", dtToDate.Value.Date);

            ReportViewer myReportViewer = new ReportViewer();
            myReportViewer.crystalReportViewer1.ReportSource = myIntakeperPluckerRpt;
            myReportViewer.Show();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void IntakePerPlucker_Load(object sender, EventArgs e)
        {

        }
    }
}