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
    public partial class HolidaypayConfirmation : Form
    {
        FTSPayRollBL.EmployeeMaster myEmp = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.YearMonth YMonth = new YearMonth();
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new EstateDivisionBlock();
        FTSPayRollBL.HolidayPay HoliPay = new FTSPayRollBL.HolidayPay();
        String strDivision;
        Int32 intYear;
        Form ProMsg;

        public HolidaypayConfirmation(HolidayPay HPay)
        {
            strDivision = HPay.cmbDivision.SelectedValue.ToString();
            intYear = Convert.ToInt32(HPay.cmbYear.SelectedValue.ToString());
            InitializeComponent();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Do You Want To Confirm ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(ConfirmHolidaypay);
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
                    MessageBox.Show(strDivision + " Division - Holidaypay Confirm Failed!\r\n"+ex.Message, "Failed!");
                }
             }
             
        }

        public void ConfirmHolidaypay(object sender, DoWorkEventArgs e)
        {
            try
            {
                HoliPay.ConfirmHolidaypayData(intYear, strDivision);
                //MessageBox.Show(strDivision +" Division - Confirmed Successfully!\r\n\r\n Holidaypay Report Will Be Displayed Now.", "Confirmation");
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strDivision + " Division - Confirm Failed.\r\n" + ex.Message, "Error");
            }
            String state = "";
            //try
            //{
                //HoliPay.IntYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());
                //HoliPay.StrDivision = cmbDivision.SelectedValue.ToString();
            try
            {
                state = HoliPay.processHolidaypay(intYear, strDivision);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error On Process,"+ex.Message);
            }
                if (state.Equals("COMPLETED"))
                {
                    MessageBox.Show(strDivision + " Division - Holidaypay Confirmed And Processed Successfully!");
                }
                else
                {
                    MessageBox.Show(strDivision + " Division - Holidaypay Confirm Failed!\r\nPlease Inform To OLAX Team", "Failed!");
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Process Failed, " + ex.Message, "Error!");
            //}
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //all background work has complete and we are going to close the waiting message
            ProMsg.Close();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HolidaypayConfirmation_Load(object sender, EventArgs e)
        {

        }
    }
}