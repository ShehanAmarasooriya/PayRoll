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
    public partial class EmployeeMovement : Form

    {
        EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.EmployeeMovements MyMovRep = new FTSPayRollBL.EmployeeMovements();
        FTSPayRollBL.EstateDivisionBlock mydiv = new FTSPayRollBL.EstateDivisionBlock();

        public EmployeeMovement()
        {
            InitializeComponent();
        }

        private void EmployeeMovement_Load(object sender, EventArgs e)
        {
            cmbMonth.DataSource = myMonth.ListMonths();
            cmbMonth.DisplayMember = "Month";
            cmbMonth.ValueMember = "MId";

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";
            //cmbDivision.Items.Add("All");

            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay1_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {

                DataSet dataSetReport = new DataSet();
                DateTime dtStartDate, dtEndDate;


                dtStartDate = MyMovRep.GetStartDate(int.Parse(cmbYear.Text.ToString()), int.Parse(cmbMonth.SelectedValue.ToString()));
                dtEndDate = MyMovRep.GetEndDateOfMonth(dtStartDate);

                dataSetReport = MyMovRep.GetEmployeeMovements1(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {

                    dataSetReport.WriteXml("EmployeeMovementsRep.xml");
                    
                    EmployeeMovementReport myMovementRep = new EmployeeMovementReport();
                    myMovementRep.SetDataSource(dataSetReport);
                    myMovementRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myMovementRep.SetParameterValue("Estate", "Estate :" + mydiv.ListEstates().Rows[0][0].ToString());                    
                    myMovementRep.SetParameterValue("Period","For the Month of " + cmbMonth.SelectedValue.ToString() + " / " + cmbYear.Text);
                    
                    ReportViewer myReportViewer = new ReportViewer();
                    myReportViewer.crystalReportViewer1.ReportSource = myMovementRep;
                    myReportViewer.Show();                                   

                    
                }
                else
                {
                    MessageBox.Show(" No Data to Preview ");
                }
                
               
            }
            else
            {
                DataSet dataSetReport = new DataSet();
                DateTime dtStartDate, dtEndDate;

                dtStartDate = MyMovRep.GetStartDate(int.Parse(cmbYear.Text.ToString()), int.Parse(cmbMonth.SelectedValue.ToString()));
                dtEndDate = MyMovRep.GetEndDateOfMonth(dtStartDate);

                dataSetReport = MyMovRep.GetEmployeeMovements(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString());

                if (dataSetReport.Tables[0].Rows.Count > 0)
                {

                    dataSetReport.WriteXml("EmployeeMovementsRep.xml");
                    

                    EmployeeMovementReport myMovementRep = new EmployeeMovementReport();
                    myMovementRep.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();
                    myMovementRep.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myMovementRep.SetParameterValue("Estate", "Estate :" + mydiv.ListEstates().Rows[0][0].ToString());
                    myMovementRep.SetParameterValue("Period", "For the Month of " + cmbMonth.SelectedValue.ToString() + " / " + cmbYear.Text);

                    myReportViewer.crystalReportViewer1.ReportSource = myMovementRep;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data to Preview");
                }

            }
            //dataSetReport = myReports.getEmployeeMovements(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            //dataSetReport.WriteXml("EmployeeMovements.xml");
            //EmployeeMovementRPT myaclist = new EmployeeMovementRPT();
            //myaclist.SetDataSource(dataSetReport);
            //ReportViewer myReportViewer = new ReportViewer();

            //myaclist.SetParameterValue("Estate", FTSPayRollBL.Company.getCompanyName());
            //myaclist.SetParameterValue("Period", "Month of : " + cmbMonth.Text + "/" + cmbYear.Text);
            //myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            //myReportViewer.Show();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbDivision.Enabled = false;
            }
            else
            {
                cmbDivision.Enabled = true;
            }
        }
    }
}