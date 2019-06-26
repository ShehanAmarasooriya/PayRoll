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
    public partial class HolidayPay : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        FTSPayRollBL.CheckRollReports myReports = new FTSPayRollBL.CheckRollReports();
        FTSPayRollBL.FTSCheckRollSettings chkSettings = new FTSPayRollBL.FTSCheckRollSettings();
        Form ProMsg;
        DataTable dtData = null;

        public HolidayPay()
        {
            InitializeComponent();
        }

        private void HolidayPay_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbYear_SelectedIndexChanged_1(null, null);

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);

        }

        public void bindDataToGrid(String strDiv,Int32 intYear)
        {
            if (!HoliPay.boolProcessedHolidaypay(intYear, strDiv))
            {
                gvList.DataSource = HoliPay.GetHolidayPayData(strDiv, intYear);
                DisableUneidtableColumns();
            }
            else
            {
                gvList.DataSource = null;
                MessageBox.Show(strDiv+" Division - Holidaypay Data Already Processed.", "Access Denied");
            }
        }

        public void DisableUneidtableColumns()
        {
            try
            {
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[3].ReadOnly = true;
                //gvList.Columns[4].ReadOnly = true;
                //gvList.Columns[5].ReadOnly = true;
                //gvList.Columns[6].ReadOnly = true;
                //gvList.Columns[7].ReadOnly = true;
                //gvList.Columns[8].ReadOnly = true;
                //gvList.Columns[9].ReadOnly = true;
                //gvList.Columns[10].ReadOnly = true;
                if (HoliPay.IsColumnEditable("ManDays"))
                {
                    gvList.Columns[4].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[4].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[4].ReadOnly = true;
                    gvList.Columns[4].DefaultCellStyle.BackColor = Color.White;
                }
                if (HoliPay.IsColumnEditable("HolidayHalfNames"))
                {
                    gvList.Columns[5].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[5].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[5].ReadOnly = true;
                    gvList.Columns[5].DefaultCellStyle.BackColor = Color.White;
                }
                if (HoliPay.IsColumnEditable("DailyBasic"))
                {
                    gvList.Columns[6].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[6].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[6].ReadOnly = true;
                    gvList.Columns[6].DefaultCellStyle.BackColor = Color.White;
                }
                if (HoliPay.IsColumnEditable("OverKgAmount"))
                {
                    gvList.Columns[7].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[7].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[7].ReadOnly = true;
                    gvList.Columns[7].DefaultCellStyle.BackColor = Color.White;
                }
                if (HoliPay.IsColumnEditable("ExtraRates"))
                {
                    gvList.Columns[8].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[8].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[8].ReadOnly = true;
                    gvList.Columns[8].DefaultCellStyle.BackColor = Color.White;
                }
                if (HoliPay.IsColumnEditable("Earnings"))
                {
                    gvList.Columns[9].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[9].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[9].ReadOnly = true;
                    gvList.Columns[9].DefaultCellStyle.BackColor = Color.White;
                }
                if (HoliPay.IsColumnEditable("NormalManDays"))
                {
                    gvList.Columns[10].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    gvList.Columns[10].ReadOnly = false;
                }
                else
                {
                    gvList.Columns[10].ReadOnly = true;
                    gvList.Columns[10].DefaultCellStyle.BackColor = Color.White;
                }  
                
                gvList.Columns[11].ReadOnly = true;
                gvList.Columns[12].ReadOnly = true;
                gvList.Columns[13].DefaultCellStyle.BackColor = Color.LightSkyBlue; ;
                gvList.Columns[14].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[15].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[16].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[17].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[18].ReadOnly = true;
                gvList.Columns[19].ReadOnly = true;
                gvList.Columns[20].ReadOnly = true;
                if (HoliPay.IsOfferedEditable())
                {
                    gvList.Columns[21].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    gvList.Columns[21].ReadOnly = true;
                }
                gvList.Columns[22].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[23].ReadOnly = true;
                gvList.Columns[24].ReadOnly = true;
            }
            catch
            {
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cmbDivision.SelectedValue.ToString()))
                {
                    this.bindDataToGrid(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                }
            }
            catch
            {
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.bindDataToGrid(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!HoliPay.boolConfirmedHolidaypay(dateTimePicker2.Value.Date.Year, cmbDivision.SelectedValue.ToString()))
            {
                if (MessageBox.Show("Do you want to get Holidaypay Data ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
                    HoliPay.IntYear = dateTimePicker2.Value.Date.Year;
                    HoliPay.DtFromDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString());
                    HoliPay.DtToDate = Convert.ToDateTime(dateTimePicker2.Value.Date.ToShortDateString());
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(GetHolidayPayData);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    ProMsg = new Processing();

                    try
                    {
                         bw.RunWorkerAsync();//this will run all Transmitting protocol coding at background thread

                        //MessageBox.Show("Please wait. Uploading logo.", "Status");
                        ProMsg.ShowDialog();//use controlable form instead of poor MessageBox
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    gvList.DataSource = dtData;
                }
            }
            else
            {
                MessageBox.Show(cmbDivision.SelectedValue.ToString()+" Division - Holidaypay Data Already Confirmed.","Access Denied");
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //all background work has complete and we are going to close the waiting message
            ProMsg.Close();
        }

        public void GetHolidayPayData(object sender, DoWorkEventArgs e)
        {
            DataTable dt=null;
            String strStatus = "";
            //if (Convert.ToInt32(HoliPay.IntYear) == Convert.ToInt32(User.StrYear))
            //{

                if (!String.IsNullOrEmpty(HoliPay.StrDivision))
                {
                    if (true)
                    {                       

                        try
                        {
                            if (HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear).Rows.Count > 0)
                            {
                                dtData = HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear);
                                DisableUneidtableColumns();
                            }
                            else
                            {
                                DataTable EmployeeTbl;
                                EmployeeTbl = myEmp.ListEPFEntitledEmployeesDetails(HoliPay.StrDivision);
                                 
                                foreach (DataRow drow in EmployeeTbl.Rows)
                                {
                                    strStatus = HoliPay.InsertHolidayPayData(HoliPay.IntYear, HoliPay.StrDivision, false,drow[0].ToString(),dateTimePicker1.Value.Date,dateTimePicker2.Value.Date);
                                }
                               
                                dtData = HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear);
                                DisableUneidtableColumns();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }
                    //else
                    //{
                    //    MessageBox.Show("Please Select a Valid Division.");
                    //}
                }
                else
                    MessageBox.Show("Please Select a Valid Division.");

            //}
            //else
            //    MessageBox.Show("Please Select The Current Working Year.");
        }

        public void UpdateHolidayPayData(object sender, DoWorkEventArgs e)
        {
            DataTable dt = null;
            String strStatus = "";
            //if (Convert.ToInt32(HoliPay.IntYear) == Convert.ToInt32(User.StrYear))
            //{

            if (!String.IsNullOrEmpty(HoliPay.StrDivision))
            {
                if (true)
                {

                    try
                    {
                        //if (HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear).Rows.Count > 0)
                        //{
                        //    dtData = HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear);
                        //    DisableUneidtableColumns();
                        //}
                        //else
                        //{
                            DataTable EmployeeTbl;
                            EmployeeTbl = myEmp.ListEmployeesDetailsbyCat(HoliPay.StrDivision, 1);

                            foreach (DataRow drow in EmployeeTbl.Rows)
                            {
                                strStatus = HoliPay.UpdateHolidayPayData1(HoliPay.IntYear, HoliPay.StrDivision, false, drow[0].ToString(),dateTimePicker1.Value.Date,dateTimePicker2.Value.Date);
                            }

                            dtData = HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear);
                            DisableUneidtableColumns();
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, " + ex.Message);
                    }
                }
                //else
                //{
                //    MessageBox.Show("Please Select a Valid Division.");
                //}
            }
            else
                MessageBox.Show("Please Select a Valid Division.");

            //}
            //else
            //    MessageBox.Show("Please Select The Current Working Year.");
        }

        private void btnClearHP_Click(object sender, EventArgs e)
        {
            if (!HoliPay.boolConfirmedHolidaypay(Convert.ToInt32(cmbYear.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()))
            {
                if (MessageBox.Show("Do You Want To Clear Existing Holidaypay Data?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
                    HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(ClearHolidayPayData);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    ProMsg = new Processing();

                    try
                    {
                        bw.RunWorkerAsync();//this will run all Transmitting protocol coding at background thread

                        //MessageBox.Show("Please wait. Uploading logo.", "Status");
                        ProMsg.ShowDialog();//use controlable form instead of poor MessageBox
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    this.bindDataToGrid(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Holidaypay Data Already Confirmed.", "Access Denied");
            }

        }

        public void ClearHolidayPayData(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(HoliPay.StrDivision))
                {                    
                        HoliPay.ClearHolidaypayData(HoliPay.StrDivision, HoliPay.IntYear);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();
            try
            {
                dataSetReport.Tables.Add(HoliPay.GetHolidayPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("HolidayPayDataPreview.xml");
                    HolidayPayDataPreview myHPData = new HolidayPayDataPreview();
                    myHPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myHPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myHPData.SetParameterValue("Date", cmbYear.SelectedValue.ToString());
                    myHPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
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
                MessageBox.Show("Error, "+ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Boolean boolPHDeducting=false;
            //int count = 0;
            try
            {
                boolPHDeducting = chkSettings.IsHolidaypayPHDeducting();

                for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                {
                    //count = i;
                    HoliPay.UpdateHolidaypayData(Convert.ToInt32(gvList.Rows[i].Cells[0].Value.ToString()), gvList.Rows[i].Cells[2].Value.ToString(), gvList.Rows[i].Cells[3].Value.ToString(), Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[5].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[6].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[7].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[8].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[9].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[10].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[11].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[13].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[14].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[15].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[16].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[17].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[18].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[21].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[4].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[22].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[23].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[24].Value.ToString()),boolPHDeducting); 
                }
                MessageBox.Show("Employee Details Saved Successfully...!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(count+ex.Message);
                MessageBox.Show("Error On Save, "+ ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            HolidaypayConfirmation HPayConfirm = new HolidaypayConfirmation(this);
            HPayConfirm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = HoliPay.getJRLHolidayPayData(2013);
            dataSetReport.WriteXml("JRLHolidayPayData.xml");
            JRLHolidayPayDataRPT myaclist = new JRLHolidayPayDataRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            myaclist.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
            myaclist.SetParameterValue("Period",  "Year: 2013 ");
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void addEmp_Click(object sender, EventArgs e)
        {
            try
            {
                HoliPay.InsertEmployeeToHolidaypay(cmbDivision.SelectedValue.ToString(), txtNewEmp.Text.PadLeft(5, '0'), Convert.ToInt32(cmbYear.Text), FTSPayRollBL.User.StrEstate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, "+ex.Message);
            }
            btnClearHP.PerformClick();
        }

        private void txtNewEmp_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            UpdateHolidayPayData myUpdateHpData = new UpdateHolidayPayData();
            myUpdateHpData.Show();
        }

        private void hpData_Click(object sender, EventArgs e)
        {
            DataSet dataSetReport = new DataSet();

            dataSetReport = HoliPay.getHPData(2014,cmbDivision.SelectedValue.ToString());
            dataSetReport.WriteXml("HPData.xml");
            HPDataRPT myaclist = new HPDataRPT();
            myaclist.SetDataSource(dataSetReport);
            ReportViewer myReportViewer = new ReportViewer();

            
            myReportViewer.crystalReportViewer1.ReportSource = myaclist;
            myReportViewer.Show();
        }

        private void btnDownloadOldSystemData_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            String strStatus = "";
            //if (Convert.ToInt32(HoliPay.IntYear) == Convert.ToInt32(User.StrYear))
            //{
            HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
            HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
            if (!String.IsNullOrEmpty(HoliPay.StrDivision))
            {
                if (true)
                {

                    try
                    {
                        //if (HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear).Rows.Count > 0)
                        //{
                        //    dtData = HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear);
                        //    DisableUneidtableColumns();
                        //}
                        //else
                        //{
                            //DataTable EmployeeTbl;
                            //EmployeeTbl = myEmp.ListEmployeesDetailsbyCat(HoliPay.StrDivision, 1);

                            //foreach (DataRow drow in EmployeeTbl.Rows)
                            //{
                                strStatus = HoliPay.DownloadHolidayPayData(HoliPay.IntYear, HoliPay.StrDivision, false,"NA", Convert.ToDateTime(dateTimePicker1.Value.Date.ToShortDateString()),Convert.ToDateTime(dateTimePicker2.Value.Date.ToShortDateString()));
                            //}

                            dtData = HoliPay.GetHolidayPayData(HoliPay.StrDivision, HoliPay.IntYear);
                            DisableUneidtableColumns();
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, " + ex.Message);
                    }
                }
                //else
                //{
                //    MessageBox.Show("Please Select a Valid Division.");
                //}
                MessageBox.Show("Downloaded Successfully");
            }
            else
                MessageBox.Show("Please Select a Valid Division.");

            //}
            //else
            //    MessageBox.Show("Please Select The Current Working Year.");
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            if (!HoliPay.boolConfirmedHolidaypay(Convert.ToInt32(cmbYear.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()))
            {
                if (MessageBox.Show("Do you want to update Holidaypay Data ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
                        HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += new DoWorkEventHandler(UpdateHolidayPayData);
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                        ProMsg = new Processing();

                        try
                        {
                            bw.RunWorkerAsync();//this will run all Transmitting protocol coding at background thread

                            //MessageBox.Show("Please wait. Uploading logo.", "Status");
                            ProMsg.ShowDialog();//use controlable form instead of poor MessageBox
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        gvList.DataSource = dtData;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(cmbDivision.SelectedValue.ToString() + " Division - Holidaypay Data Already Confirmed.", "Access Denied");
            }
        }

        private void btnCancelConfirmation_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Cancle Confirmation ?", "Cancel Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetHolidaypay ReHPay = new ResetHolidaypay();
                ReHPay.Show();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            cmbYear.SelectedValue = dateTimePicker2.Value.Date.Year;
        }

        private void cmbYear_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataSet dsHPYear = HoliPay.GetHolidayPayDateRange(Convert.ToInt32(cmbYear.SelectedValue.ToString()));
                dateTimePicker1.Value = Convert.ToDateTime(dsHPYear.Tables[0].Rows[0][1].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dsHPYear.Tables[0].Rows[0][2].ToString());
            }
            catch { }
        }

       

        
       
    }
}