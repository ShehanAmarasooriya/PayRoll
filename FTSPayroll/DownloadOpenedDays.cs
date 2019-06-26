using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class DownloadOpenedDays : Form
    {
        FTSPayRollBL.EstateDivisionBlock EstDivBlock = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.BlockEntries myEntries = new FTSPayRollBL.BlockEntries();

        public DownloadOpenedDays()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to download this date for adjustments...? Make sure you have valid internet connection to proceed.", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //String EstateID = EstDivBlock.getEstateId();
                    
                    //WebService.WebService myService = new FTSPayroll.WebService.WebService();
                    
                    //myService.Credentials = System.Net.CredentialCache.DefaultCredentials;

                    //DataTable dt = myService.DownloadOpenDate(dtDate.Value.Date, EstateID);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    //    {
                    //        DateTime MyDate = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    //        String Section = dt.Rows[i][1].ToString();
                    //        if (!String.IsNullOrEmpty(Section))
                    //        {
                    //            myEntries.DtCurrentDate = MyDate;
                    //            myEntries.DtCloseDate = DateTime.Now.Date.AddDays(2);
                    //            myEntries.OpenBlockedDate();
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Invalid Data To Download");
                    //        }
                    //    }
                    //    myService.UpdateAsDownloaded(dtDate.Value.Date, EstateID);
                    //    MessageBox.Show("Date Openings Downloaded Successfully...!");
                    //}
                    //else
                    //    MessageBox.Show("No Records Found for this date...!");

                    //dt.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DownloadOpenedDays_Load(object sender, EventArgs e)
        {

        }
    }
}