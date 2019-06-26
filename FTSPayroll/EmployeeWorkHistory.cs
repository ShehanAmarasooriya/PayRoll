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
    public partial class EmployeeWorkHistory : Form
    {
        public EmployeeWorkHistory()
        {
            InitializeComponent();
        }

        EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeMaster myEmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeWorkHstory myEmpwork = new FTSPayRollBL.EmployeeWorkHstory();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();



        private void EmployeeWorkHistory_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = myMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = myMonth.getLastYearID();

            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";
            cmbMonth.SelectedValue = myMonth.getLastMonthID();

            cmbDivisionID.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivisionID.DisplayMember = "DivisionID";
            cmbDivisionID.ValueMember = "DivisionID";

            cmbDivisionID_SelectedIndexChanged(null, null);
          

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            DateTime dtStartDate, dtEndDate;
            String strAllEmp="%";

            dtStartDate = myEmpwork.GetStartDate(int.Parse(cmbYear.Text.ToString()), int.Parse(cmbMonth.SelectedValue.ToString()));
            dtEndDate = myEmpwork.GetEndDateOfMonth(dtStartDate);
            if (chkAll.Checked)
            {
                strAllEmp = "%";
            }
            else
            {
                strAllEmp = cmbEmpNo.SelectedValue.ToString();
            }
            dataSetReport = myEmpwork.GetEmpWorkHis(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivisionID.SelectedValue.ToString(), strAllEmp);

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                dataSetReport.WriteXml("EmpWorkHistory.xml");

                EmpWorkHistory EmpWrkHis = new EmpWorkHistory();
                EmpWrkHis.SetDataSource(dataSetReport);

                EmpWrkHis.SetParameterValue("Estate","Estate :" + mydiv.ListEstates().Rows[0][0].ToString());
                EmpWrkHis.SetParameterValue("Period","For the month :" + cmbMonth.Text + "  /  " + cmbYear.Text);
                ReportViewer myRepView = new ReportViewer();
                myRepView.crystalReportViewer1.ReportSource = EmpWrkHis;
                myRepView.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview");
            }
                
                

        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDivisionID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbDivisionID.SelectedValue.ToString()))
            {
                cmbEmpNo.DataSource = null;
                cmbEmpNo.DataSource = myEmpMaster.ListAllEmployees(cmbDivisionID.SelectedValue.ToString());
                cmbEmpNo.DisplayMember = "EmpNo";
                cmbEmpNo.ValueMember = "EmpNo";
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}