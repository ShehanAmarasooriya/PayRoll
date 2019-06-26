using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class MandaysSummaryReport : Form
    {
        FTSPayRollBL.Division myDivision = new FTSPayRollBL.Division();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        DataSet dataSetReport = new DataSet();

        public MandaysSummaryReport()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            DateTime dtFromDate;
            DateTime dtToDate;
            dtFromDate = new DateTime(dateTimePicker1.Value.Date.Year, dateTimePicker1.Value.Date.Month, 1);
            dtToDate = dateTimePicker1.Value.Date;
            DataSet DsMDaysSummary;
            DsMDaysSummary = myReports.GetManDaysSummary(cmbDivision.SelectedValue.ToString(),dtFromDate,dtToDate);

        }

        private void MandaysSummaryReport_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myDivision.ListDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
        }
    }
}