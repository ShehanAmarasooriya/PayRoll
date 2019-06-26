using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class HolidaypayFinalReports : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EmployeeCategory EmpCat = new FTSPayRollBL.EmployeeCategory();
        FTSPayRollBL.YearMonth YMonth = new FTSPayRollBL.YearMonth();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        Boolean boolAll=false;

        public HolidaypayFinalReports()
        {
            InitializeComponent();
        }

        private void HolidaypayFinalReports_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            if (HoliPay.IsColumnEditable("AttBonusAdjust"))
            {
                lblAttBUpdate.Enabled = true;
            }
            else
            {
                lblAttBUpdate.Enabled = false;
            }
        }

        private void btnRPT1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                String strDivision = "%";
                if (chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }
                cmbYear_SelectedIndexChanged(null, null);
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidaypayMandaysEarningsNotOffered(dtpFrom.Value.Date,dtpTo.Value.Date, strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidaypayRPT1.xml");
                    HolidaypayRPT1 myHPData = new HolidaypayRPT1();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview\r\nMay Be Holidaypay Data Already Confirmed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnRPT4_Click(object sender, EventArgs e)
        {
            String strDivision = "%";
            if (chkAll.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }

            DataSet dataSetReport = new DataSet();
            try
            {
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidayPayData(Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidaypayRPT4.xml");
                    HolidaypayRPT4 myHPData = new HolidaypayRPT4();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnRPT2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                String strDivision = "%";
                if (chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }
                cmbYear_SelectedIndexChanged(null, null);
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidaypayMandaysEarningsNotOffered(dtpFrom.Value.Date,dtpTo.Value.Date, strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidaypayRPT2.xml");
                    HolidaypayRPT2 myHPData = new HolidaypayRPT2();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview\r\nMay Be Holidaypay Data Already Confirmed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnRPT3_Click(object sender, EventArgs e)
        {
            String strDivision = "%";
            if (chkAll.Checked)
            {
                strDivision = "%";
            }
            else
            {
                strDivision = cmbDivision.SelectedValue.ToString();
            }

            DataSet dataSetReport = new DataSet();
            try
            {
                cmbYear_SelectedIndexChanged(null, null);
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidaypayMandaysEarningsNotOffered(Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidaypayRPT3.xml");
                    HolidaypayRPT3 myHPData = new HolidaypayRPT3();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview\r\nMay Be Holidaypay Data Already Confirmed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                boolAll = true;
            }
            else
            {
                boolAll = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateAttendanceBonus myUpdateAtt = new UpdateAttendanceBonus();
            myUpdateAtt.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                String strDivision = "%";
                if (chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidayPayData(Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidaypaySignatureList.xml");
                    HolidaypaySignatureListRPT myHPData = new HolidaypaySignatureListRPT();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnattNetPayList_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                String strDivision = "%";
                if (chkAll.Checked)
                {
                    strDivision = "%";
                }
                else
                {
                    strDivision = cmbDivision.SelectedValue.ToString();
                }
                //dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                dataSetReport = HoliPay.ListHolidayPayData(Convert.ToInt32(cmbYear.SelectedValue.ToString()), strDivision);
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("AttBonusSignatureList.xml");
                    AttBonusSignaturePaylist myHPData = new AttBonusSignaturePaylist();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myHPData.SetParameterValue("Year", cmbYear.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myHPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet dsHPYear = HoliPay.GetHolidayPayDateRange(Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                dtpFrom.Value = Convert.ToDateTime(dsHPYear.Tables[0].Rows[0][1].ToString());
                dtpTo.Value = Convert.ToDateTime(dsHPYear.Tables[0].Rows[0][2].ToString());
            }
            catch { }
        }
    }
}