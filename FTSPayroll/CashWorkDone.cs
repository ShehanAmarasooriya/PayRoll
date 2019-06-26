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
    public partial class CashWorkDone : Form
    {
        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.YearMonth myMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.EmployeeCategory myCatagory = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();

        DataSet dataSetReport = new DataSet();

        public CashWorkDone()
        {
            InitializeComponent();
        }

        private void CashWorkDone_Load(object sender, EventArgs e)
        {
            cmbDivision.DataSource = myEstate.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionName";
            cmbDivision.ValueMember = "DivisionID";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strAllDivision="%";
            if(chkDivision.Checked)
            {
                strAllDivision="%";
            }
            else
            {
                strAllDivision=cmbDivision.SelectedValue.ToString();
            }
            dataSetReport = myReports.getEmployeeCashPluckingDetails(dtpFromDate.Value, dtpToDate.Value, 2, strAllDivision);
            dataSetReport.WriteXml("EmployeeCashPluckingDetail.xml");
            
            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                CashPluckingKilo rptObj = new CashPluckingKilo();
                rptObj.SetDataSource(dataSetReport);
                ReportViewer myReportViewer = new ReportViewer();

                rptObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                rptObj.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                rptObj.SetParameterValue("Options", "Division : "+cmbDivision.Text.ToString()+" / From:" + dtpFromDate.Value.ToShortDateString() + "   To:" +dtpToDate.Value.ToShortDateString());

                rptObj.SetParameterValue("WorkType", "Work Type : Cash Work");
                //if (intworktyp == 1)
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Normal Work");

                //}
                //else
                //{
                //    myaclist.SetParameterValue("WorkType", "Work Type : Cash Work");
                //}
                myReportViewer.crystalReportViewer1.ReportSource = rptObj;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            String strAllDivision = "%";
            if (chkDivision.Checked)
            {
                strAllDivision = "%";
            }
            else
            {
                strAllDivision = cmbDivision.SelectedValue.ToString();
            }
            dataSetReport = myReports.getEmployeeSundryManDaysDetails(dtpFromDate.Value, dtpToDate.Value, 2, strAllDivision);
            dataSetReport.WriteXml("EmployeeCashSundryManDaysDetail.xml");

            if (dataSetReport.Tables[0].Rows.Count > 0)
            {
                CashSundryManDays rptObj = new CashSundryManDays();
                rptObj.SetDataSource(dataSetReport);
                
                ReportViewer myReportViewer = new ReportViewer();

                rptObj.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                rptObj.SetParameterValue("Estate", myEstate.ListEstates().Rows[0][0].ToString());
                rptObj.SetParameterValue("Options", "Division : " + cmbDivision.Text.ToString() + " / From:" + dtpFromDate.Value.ToShortDateString() + "   To:" + dtpToDate.Value.ToShortDateString());

                rptObj.SetParameterValue("WorkType", "Work Type : Cash Work");

                myReportViewer.crystalReportViewer1.ReportSource = rptObj;
                myReportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Data to Preview..!");
            }
        }
    }
}