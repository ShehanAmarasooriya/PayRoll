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
    public partial class GratuityData : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        Form ProMsg;
        DataTable dtData = null;

        public GratuityData()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetGP_Click(object sender, EventArgs e)
        {
            if (!HoliPay.boolConfirmedGratuitypay(Convert.ToInt32(cmbYear.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()))
            {
                if (MessageBox.Show("Do you want to get Gratuity Data ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
                    HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(GetGratuityPayData);
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
                MessageBox.Show(cmbDivision.SelectedValue.ToString() + " Division - Holidaypay Data Already Confirmed.", "Access Denied");
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //all background work has complete and we are going to close the waiting message
            ProMsg.Close();
        }

        public void GetGratuityPayData(object sender, DoWorkEventArgs e)
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
                        if (HoliPay.GetGratuityPayData(HoliPay.StrDivision, HoliPay.IntYear).Rows.Count > 0)
                        {
                            dtData = HoliPay.GetGratuityPayData(HoliPay.StrDivision, HoliPay.IntYear);
                            DisableUneidtableColumns();
                        }
                        else
                        {
                            DataTable EmployeeTbl;
                            EmployeeTbl = myEmp.ListAllEmployees(HoliPay.StrDivision);

                            foreach (DataRow drow in EmployeeTbl.Rows)
                            {
                                strStatus = HoliPay.InsertGratuityPayData(HoliPay.IntYear, HoliPay.StrDivision, false, drow[0].ToString());
                            }

                            dtData = HoliPay.GetGratuityPayData(HoliPay.StrDivision, HoliPay.IntYear);  
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

        public void DisableUneidtableColumns()
        {
            try
            {
                gvList.Columns[0].ReadOnly = true;
                gvList.Columns[1].ReadOnly = true;
                gvList.Columns[2].ReadOnly = true;
                gvList.Columns[3].ReadOnly = true;
                gvList.Columns[4].ReadOnly = true;
                gvList.Columns[5].ReadOnly = true;
                gvList.Columns[6].ReadOnly = true;               
                gvList.Columns[7].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[8].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[9].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[10].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[11].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[12].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[13].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[14].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                gvList.Columns[15].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            catch
            {
            }
        }

        public void bindDataToGrid(String strDiv, Int32 intYear)
        {
            if (!HoliPay.boolConfirmedGratuitypay(intYear, strDiv))
            {
                gvList.DataSource = HoliPay.GetGratuityPayData(strDiv, intYear);
                DisableUneidtableColumns();
            }
            else
            {
                gvList.DataSource = null;
                MessageBox.Show(strDiv + " Division - Gratuitypay Data Already Confirmed.", "Access Denied");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!HoliPay.boolConfirmedGratuitypay(Convert.ToInt32(cmbYear.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString()))
            {
                if (MessageBox.Show("Do You Want To Clear Existing Gratuitypay Data?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
                    HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(ClearGratuityPayData);
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
        public void ClearGratuityPayData(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(HoliPay.StrDivision))
                {
                    HoliPay.ClearGratuitypayData(HoliPay.StrDivision, HoliPay.IntYear);
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
                dataSetReport.Tables.Add(HoliPay.GetGratuityPayData(cmbDivision.SelectedValue.ToString(), Convert.ToInt32(cmbYear.SelectedValue.ToString())));
                if (dataSetReport.Tables[0].Rows.Count > 0)
                {
                    dataSetReport.WriteXml("GratuityPayDataPreview.xml");
                    GratuitypayDataPreview myGPData = new GratuitypayDataPreview();
                    myGPData.SetDataSource(dataSetReport);
                    ReportViewer myReportViewer = new ReportViewer();

                    myGPData.SetParameterValue("Company Name", FTSPayRollBL.Company.getCompanyName());
                    myGPData.SetParameterValue("Date", cmbYear.SelectedValue.ToString());
                    myGPData.SetParameterValue("Division", EstDivBlock.ListEstates().Rows[0][0].ToString() + " / " + cmbDivision.SelectedValue.ToString());
                    myReportViewer.crystalReportViewer1.ReportSource = myGPData;
                    myReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("No Data To Preview Gratuitypay, \r\n May be Confirmed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (Int32 i = 0; i <= gvList.Rows.Count - 1; i++)
                {
                    //count = i;
                    if (String.IsNullOrEmpty(gvList.Rows[i].Cells[7].Value.ToString()) || gvList.Rows[i].Cells[7].Value.ToString().Equals("0"))
                    {
                        HoliPay.UpdateGratuitypayDataWithOutLastDate(Convert.ToInt32(gvList.Rows[i].Cells[0].Value.ToString()), gvList.Rows[i].Cells[1].Value.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), Convert.ToDecimal(gvList.Rows[i].Cells[8].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[9].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[10].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[11].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[12].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[13].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[14].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[15].Value.ToString()));
                    }
                    else
                    {
                        DateTime dtLastDate = Convert.ToDateTime(gvList.Rows[i].Cells[7].Value.ToString());
                        HoliPay.UpdateGratuitypayData(Convert.ToInt32(gvList.Rows[i].Cells[0].Value.ToString()), gvList.Rows[i].Cells[1].Value.ToString(), gvList.Rows[i].Cells[2].Value.ToString(), Convert.ToDateTime(gvList.Rows[i].Cells[7].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[8].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[9].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[10].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[11].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[12].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[13].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[14].Value.ToString()), Convert.ToDecimal(gvList.Rows[i].Cells[15].Value.ToString()));
                    }
                    
                }
                MessageBox.Show("Employee Details Saved Successfully...!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(count+ex.Message);
                MessageBox.Show("Error, " + ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Confirm ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(ConfirmGratuitypay);
                this.Close();
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                ProMsg = new Processing();

                try
                {
                    bw.RunWorkerAsync();//this will run all Transmitting protocol coding at background thread

                    ProMsg.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show( " Division - Holidaypay Confirm Failed!\r\n"+ex.Message, "Failed!");
                }
             }
             
        }

        public void ConfirmGratuitypay(object sender, DoWorkEventArgs e)
        {
            //try
            //{
                HoliPay.ConfirmGratuityPayData(Convert.ToInt32(cmbYear.SelectedValue.ToString()), cmbDivision.SelectedValue.ToString());
                MessageBox.Show("Confirmed Gratuity Pay of " + cmbDivision.SelectedValue.ToString() + " Division Successfully.");
            String state = "";
           
        
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

        private void GratuityData_Load(object sender, EventArgs e)
        {
            cmbYear.DataSource = YMonth.ListYearsFromCHKMonths();
            cmbYear.DisplayMember = "Year";
            cmbYear.ValueMember = "Year";
            cmbYear.SelectedValue = YMonth.getLastYearID();

            cmbDivision.DataSource = EstDivBlock.ListEstateDivisions();
            cmbDivision.DisplayMember = "DivisionID";
            cmbDivision.ValueMember = "DivisionID";

            cmbDivision_SelectedIndexChanged(null, null);
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
    }
}