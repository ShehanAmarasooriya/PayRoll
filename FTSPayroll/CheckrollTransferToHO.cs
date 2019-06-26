using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using System.IO.Compression;
using System.IO;
using System.Net;
using System.Configuration;
using System.Collections;
//using ICSharpCode.SharpZipLib.Zip;
using System.Net.Mail;

namespace FTSPayroll
{
    public partial class CheckrollTransferToHO : Form
    {
        
        FTSPayRollBL.CheckRollTransfer myTransfer = new FTSPayRollBL.CheckRollTransfer();
        FTSPayRollBL.ftpSettings mySettings = new FTSPayRollBL.ftpSettings();
        FTSPayRollBL.EstateDivisionBlock myEstate = new FTSPayRollBL.EstateDivisionBlock();

        public CheckrollTransferToHO()
        {
            InitializeComponent();
        }

        private void CheckrollTransferToHO_Load(object sender, EventArgs e)
        {

        }

        private void btnNewTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                String FileName = "";
                String StrCheckRoll = "";
                String StrTeaBook = "";
                String StrFieldDiary = "";
                String StrBinBalances = "";
                String StrEstateSales = "";
                String StrGratis = "";
                String StrSiftedTeas = "";
                String StrInterEstateCropIn = "";
                String StrInterEstateCropOut = "";
                String StrYieldDiary = "";
                String StrBoughtLeaf = "";
                String StrBoughtLeafPayments = "";
                String CashWork = "";
                String StrDailyBinBalance = "";
                String StrCropAdjustment = "";
                String StrRefusedTea = "";
                String StrInvoiceDispatch = "";
                String StrTeaReturn = "";
                String StrEmpMaster = "";

                if (MessageBox.Show("Do you want to send your data to BPL Head Office? Make sure you have valid internet connection to proceed.", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Boolean UpdateSuccess = true;
                    Boolean ChkDataAvailable = false;
                    Boolean chkCashDataAvailable = false;
                    Boolean chkAmalgamationDataAvailable = false;
                    String strEmailBody = "<ul>";
                    lblStatus.Visible = true;

#region Checkroll
                    if (UpdateSuccess == true)
                    {
                        if (true)
                        {
                            if (chkCheckroll.Checked)
                            {
                                //if (myTransfer.IsConfirmationsOK(dtDate.Value.Date, dtToDate.Value.Date))
                                //{
                                    lblStatus.Text = "Sending Check Roll Data...";
                                    //lblConnection.Visible = true;
                                    Application.DoEvents();

                                    myTransfer.DatDateEntered = dtDate.Value.Date;
                                    myTransfer.DatToDate = dtToDate.Value.Date;


                                    //DataTable dt = myTransfer.ListPendingCheckRollData();
                                    DataTable dt = myTransfer.ListPendingCheckRollDataFromOLAX();
                                    if (dt.Rows.Count > 0)
                                    {
                                        dt.TableName = "checkroll";
                                        dt.WriteXml("checkroll.xml");
                                        FileName = "checkroll" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                        Compress("checkroll.xml", FileName);

                                        UpdateSuccess = Upload(FileName);
                                        StrCheckRoll = "Check Roll Transffered from " + dtDate.Value.Date.ToShortDateString() + " To " + dtToDate.Value.Date.ToShortDateString();
                                        ChkDataAvailable = true;
                                        UpdateSuccess = true;
                                        lblStatus.Text = "Checkroll Normal Work Data Transfered";
                                        Application.DoEvents();
                                        strEmailBody += "<li> Checkroll-NormalWork " + "From:" + dtDate.Value.ToShortDateString() + " To:" + dtToDate.Value.ToShortDateString() + "</li>";
                                    }

                                    else
                                    {
                                        MessageBox.Show("No Checkroll Normal Data For The Selected Date Range");
                                        UpdateSuccess = false;
                                        lblStatus.Text = "Normal Work Data Transfer Failed";
                                        Application.DoEvents();
                                    }


                                    dt.Dispose();
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Transfer Failed, Some Date Confirmations Pending..");
                                //}
                            }
                            /**/


                        }
                    }
	#endregion                    

#region CashWork
		            if (UpdateSuccess == true)
                    {
                        if (true)
                        {
                            if (chkCashWork.Checked)
                            {
                                lblStatus.Text = "Sending Cash Work Data...";
                                Application.DoEvents();
                                DataTable dt;
                                myTransfer.DatDateEntered = dtDate.Value.Date;
                                myTransfer.DatToDate = dtToDate.Value.Date;

                                /*get checkroll from OLAX or CHECKROLL*/
                                //if (myTransfer.IsCheckrollFromOLAXCheckroll())
                                //{
                                dt = myTransfer.getCashWorkFromOLAX();
                                //}
                                //else
                                //{
                                //    dt = myTransfer.getCashWork();
                                //}
                                if (dt.Rows.Count > 0)
                                {
                                    dt.TableName = "CashWork";
                                    dt.WriteXml("CashWork.xml");
                                    FileName = "CashWork" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                    Compress("CashWork.xml", FileName);

                                    UpdateSuccess = Upload(FileName);
                                    CashWork = "Cash Work Transffered from " + dtDate.Value.Date.ToShortDateString() + " To " + dtToDate.Value.Date.ToShortDateString();
                                    chkCashDataAvailable = true;
                                    lblStatus.Text = "Cash Work Data Transfered";
                                    Application.DoEvents();
                                    strEmailBody += "<li>Checkroll-CashWork " + "From:" + dtDate.Value.ToShortDateString() + " To:" + dtToDate.Value.ToShortDateString() + "</li>";
                                }
                                else
                                {
                                    MessageBox.Show("No Checkroll Cash Work Data For The Selected Date Range");
                                    UpdateSuccess = false;
                                    lblStatus.Text = "Cash Work Data Transfer Failed";
                                    Application.DoEvents();
                                }

                                dt.Dispose();
                            }
                        }
                    }
	#endregion
                    
#region Amalgamation
		        if (UpdateSuccess == true)
                    {
                        if (chkAmalgamation.Checked)
                        {
                            DataTable dt1 = myTransfer.ListPendingAmalgamationSummary(dtDate.Value.Date.Year,dtDate.Value.Date.Month).Tables[0];
                            if (dt1.Rows.Count > 0)
                            {
                                dt1.TableName = "amalgamation";
                                dt1.WriteXml("amalgamation.xml");
                                FileName = "amalgamation" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                Compress("amalgamation.xml", FileName);

                                UpdateSuccess = Upload(FileName);
                                StrCheckRoll = "Amalgamation Transfer";
                                chkCashDataAvailable = true;
                                lblStatus.Text = "Amalgamation Data Transfered";
                                Application.DoEvents();
                                strEmailBody += "<li> Amalgamation  " + "Year:" + dtDate.Value.Year.ToString() + " Month:" + dtDate.Value.Month.ToString() + "</li>";
                            }
                            else
                            {
                                MessageBox.Show("No Amalgamation Data For The Selected Date Range");
                                UpdateSuccess = false;
                                lblStatus.Text = "Amalgamation Data Transfer Failed";
                                Application.DoEvents();
                            }

                            dt1.Dispose();
                        }
                    }
	#endregion
                    
#region EmployeeMaster
		        /*Transfer Employee Master*/
                    if (UpdateSuccess == true)
                    {
                        if (true)
                        {
                            if (chkEmpMaster.Checked)
                            {
                                lblStatus.Text = "Sending Employee Master Data...";
                                //lblConnection.Visible = true;
                                Application.DoEvents();

                                myTransfer.DatDateEntered = dtDate.Value.Date;
                                myTransfer.DatToDate = dtToDate.Value.Date;


                                //DataTable dt = myTransfer.ListPendingCheckRollData();
                                DataTable dt = myTransfer.ListEmpMasterData();
                                if (dt.Rows.Count > 0)
                                {
                                    dt.TableName = "OLAXEmp";
                                    dt.WriteXml("OLAXEmp.xml");
                                    FileName = "OLAXEmp" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                    Compress("OLAXEmp.xml", FileName);

                                    UpdateSuccess = Upload(FileName);
                                    StrCheckRoll = "Employee Master Data Transffered ";
                                    ChkDataAvailable = true;
                                    UpdateSuccess = true;
                                    lblStatus.Text = "Employee Master Data Transffered";
                                    Application.DoEvents();
                                    strEmailBody += "<li> Employee Master Data  " + "To Date" + DateTime.Now.Date.ToShortDateString() + "</li>";

                                }
                                else
                                {
                                    MessageBox.Show("No Employee Master Data To Transfer");
                                    UpdateSuccess = false;
                                    lblStatus.Text = "Normal Work Data Transfer Failed";
                                    Application.DoEvents();
                                }

                                dt.Dispose();
                            }

                            /**/


                        }
                    }
	#endregion
                    

#region Checkroll Summary
		/*Summary*/
                    if (UpdateSuccess == true)
                    {
                        if (true)
                        {
                            if (chkMonthlySummary.Checked)
                            {
                                lblStatus.Text = "Sending Check Roll Monthly Summary....";
                                //lblConnection.Visible = true;
                                Application.DoEvents();

                                myTransfer.DatDateEntered = dtDate.Value.Date;
                                myTransfer.DatToDate = dtToDate.Value.Date;

                                //if (UpdateSuccess == true)
                                //{
                                //    DataSet ds = myTransfer.ListCheckrollEstateFields();

                                //    ds.WriteXml("ChkFields.xml");
                                //    FileName = "ChkFields" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                //    CompressToFirstFolder("ChkFields.xml", FileName);
                                //    UpdateSuccess = Upload(FileName);
                                //    ds.Dispose();
                                //}
                                //send only if processed the checkroll
                                if (myTransfer.IsCheckrollProcessed(dtToDate.Value.Year, dtToDate.Value.Month))
                                {
                                    #region EmpEarnings
		                            if (UpdateSuccess == true)
                                    {
                                        DataSet ds = myTransfer.ListCheckRollMonthlyEarnings(dtDate.Value.Year, dtDate.Value.Month);

                                        ds.WriteXml("EMEarnings.xml");
                                        FileName = "EMEarnings" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                        Compress("EMEarnings.xml", FileName);
                                        UpdateSuccess = Upload(FileName);
                                        lblStatus.Text = "Emp Monthly Earnings Transfered";
                                        Application.DoEvents();
                                        strEmailBody += "<li> Emp Monthly Earnings  " + "Year:" + dtDate.Value.Year.ToString() + " Month:" + dtDate.Value.Month.ToString() + "</li>";
                                        ds.Dispose();
                                    } 
	#endregion

                                    #region CHKEmpDeductions_CRRecoveries
		                            DataTable dt = myTransfer.ListCheckRollRecoveries(dtDate.Value.Year, dtDate.Value.Month);
                                    if (dt.Rows.Count > 0)
                                    {
                                        dt.TableName = "CRRecoveries";
                                        dt.WriteXml("CRRecoveries.xml");
                                        FileName = "CRRecoveries" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                        Compress("CRRecoveries.xml", FileName);
                                        UpdateSuccess = Upload(FileName);
                                        lblStatus.Text = "Checkroll Recoveries Transfered";
                                        Application.DoEvents();
                                        strEmailBody += "<li> Checkroll Recoveries  " + "Year:" + dtDate.Value.Year.ToString() + " Month:" + dtDate.Value.Month.ToString() + "</li>";
                                    }
                                    dt.Dispose(); 
	#endregion

                                    #region EmpDeductions
		                            if (UpdateSuccess == true)
                                    {
                                        DataSet ds = myTransfer.ListCheckRollMonthlyDeductions(dtDate.Value.Year, dtToDate.Value.Month);

                                        ds.WriteXml("EMDeductions.xml");
                                        FileName = "EMDeductions" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                        Compress("EMDeductions.xml", FileName);
                                        UpdateSuccess = Upload(FileName);
                                        lblStatus.Text = "Emp Monthly Deductions Transfered";
                                        Application.DoEvents();
                                        strEmailBody += "<li> Emp Monthly Deductions  " + "Year:" + dtDate.Value.Year.ToString() + " Month:" + dtDate.Value.Month.ToString() + "</li>";
                                        ds.Dispose();
                                    } 
	                                #endregion

                                    #region EmpFinlaPay_CRFinal
		                            if (UpdateSuccess == true)
                                    {
                                        DataSet ds = myTransfer.ListCheckRollFinalPay(dtDate.Value.Year, dtToDate.Value.Month);

                                        ds.WriteXml("CRFinalPay.xml");
                                        FileName = "CRFinalPay" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                        Compress("CRFinalPay.xml", FileName);
                                        UpdateSuccess = Upload(FileName);
                                        lblStatus.Text = "Emp Monthly Final Payment Transfered";
                                        Application.DoEvents();
                                        strEmailBody += "<li> Emp Monthly Final Payment  " + "Year:" + dtDate.Value.Year.ToString() + " Month:" + dtDate.Value.Month.ToString() + "</li>";
                                        ds.Dispose();
                                    } 
	                                #endregion
                                }
                                else
                                {
                                    MessageBox.Show("Month Checkroll Must Process  To Transfer Summary ");
                                }
                               
                            }
                        }
                    }

                                
                    /**/ 
	#endregion

 #region RFTDeductions
		                        if (UpdateSuccess == true)
                                {
                                    DataSet ds = myTransfer.ListCheckRollRFTDeductions(dtDate.Value.Year, dtToDate.Value.Month);

                                    ds.WriteXml("RFTDeductions.xml");
                                    FileName = "RFTDeductions" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                    Compress("RFTDeductions.xml", FileName);
                                    UpdateSuccess = Upload(FileName);
                                    lblStatus.Text = "Food Stuf Deductions Transfered";
                                    Application.DoEvents();
                                    strEmailBody += "<li> Food Stuff Deductions  " + "Year:" + dtDate.Value.Year.ToString() + " Month:" + dtDate.Value.Month.ToString() + "</li>";
                                    ds.Dispose();
                                } 
	                            #endregion
 #region OverTime
		
                                if (UpdateSuccess == true)
                                {
                                    DataTable dt = myTransfer.ListCheckRollMonthlyOverTime(dtDate.Value.Date, dtToDate.Value.Date);
                                    if (dt.Rows.Count > 0)
                                    {
                                        dt.TableName = "CROvertime";
                                        dt.WriteXml("CROvertime.xml");
                                        FileName = "CROvertime" + dtDate.Value.Day.ToString() + dtDate.Value.Month.ToString() + dtDate.Value.Year.ToString() + ".zip";
                                        Compress("CROvertime.xml", FileName);
                                        UpdateSuccess = Upload(FileName);
                                        lblStatus.Text = "Overtime Entries Transfered";
                                        Application.DoEvents();
                                        strEmailBody += "<li> Overtime   " + "From:" + dtDate.Value.ToShortDateString() + " To:" + dtDate.Value.ToShortDateString() + "</li>";
                                    }
                                } 
	                            #endregion


                    Application.DoEvents();

                    //try
                    //{
                    //    if ((StrCheckRoll != "") )
                    //    {
                    //        String EstateName = myEstate.ListEstates().Rows[0][0].ToString();
                    //        String EmailAddress = myEstate.getManagerEmailAddress();
                    //        MailMessage message = new MailMessage();
                    //        message.From = new MailAddress("olax@bpl.lk");

                    //        message.To.Add(new MailAddress("waruni@bpl.lk"));
                    //        message.Bcc.Add(new MailAddress("chathuranga@fteenet.com"));
                    //        //message.Bcc.Add(new MailAddress("asanka@fteenet.com"));

                    //        message.Subject = "Data Upload Status report - " + EstateName;
                    //        message.Body = "Do not reply to this mail, as this is generated automatically from the data server - OLAX Studio \r\n" + "Data Received from " + EstateName + " Estate Successfully " + "at " + dtpnew.Value.ToShortDateString() + " " + dtpnew.Value.ToShortTimeString() + "\r\n" +
                    //        " " + StrBinBalances + "\r\n" + StrBoughtLeaf + "\r\n" + StrBoughtLeafPayments + "\r\n" + StrCheckRoll + "\r\n" + StrEstateSales + "\r\n" + StrFieldDiary + "\r\n" + StrGratis + "\r\n" + StrInterEstateCropIn + "\r\n" + StrInterEstateCropOut + "\r\n" + StrSiftedTeas + "\r\n" + StrTeaBook + "\r\n" + StrYieldDiary + "\r\n" + CashWork + "\r\n" + StrClimaticConditions + "\r\n" + StrDailyBinBalance + "\r\n" + StrCropAdjustment + "\r\n" + StrRefusedTea + "\r\n" + StrInvoiceDispatch + "\r\n" + StrAmalgamation + "\r\n" + StrTeaReturn + "\r\n" + StrEmpMaster;
                    //        message.IsBodyHtml = true;

                    //        SmtpClient client = new SmtpClient("smtp.gmail.com");
                    //        client.EnableSsl = true;
                    //        client.UseDefaultCredentials = false;
                    //        client.Credentials = new System.Net.NetworkCredential("olaxdashboard@gmail.com", "pass1234");
                    //        try
                    //        {
                    //            client.Send(message);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show("Your Mail has Failed...!");
                    //        }
                    //    }

                    //    if ((StrEstateFieldExtent != "") || (StrItemCategory != "") || (StrAssetName != "") || (StrJobCategory != "") || (StrProductionBudget != "") || (StrPerformanceBudget != "") || (StrManDaysBudget != "") || (StrFieldAndCultivationBudget != "") || (StrNitrogenRatioBudget != "") || (StrCostingBudget != "") || (StrRainFallDecennialAverage != "") || (StrLabourCount != "") || (StrFactoryStockBook != "") || (StrTeaNurseryDetails != "") || (StrAssetMaintenance != "") || (StrPayments != "") || (StrNitrogenRatio != "") || (StrSundryWorkEntryActual != "") || (StrOverTimeDetails != "") || (StrFertilizer != "") || (StrItemCategory != "") || (StrOfferedDays != ""))
                    //    {
                    //        SendProgressReportDataMail();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{

                    //}

                    if (UpdateSuccess == true)
                    {
                        MessageBox.Show("Data Uploaded successfully!");
                        try
                        {
                            sendSuccessEmail(strEmailBody);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Email failed.",ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Data Uploading Process Failed. Please Re Submit Data...!");
                    }

                    lblStatus.Visible = false;
                    //lblConnection.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void sendSuccessEmail(String strBody)
        {
            String EstateName = myEstate.getEstateId();
            String[] EmailAddress = new String[2];
            EmailAddress=myEstate.getEmailAddresses();
            MailMessage message = new MailMessage();
            message.From = new MailAddress("olax@bpl.lk");
            message.To.Add(new MailAddress(EmailAddress[1]));
            //message.To.Add(new MailAddress("isuru@ftservices.net"));
            //message.To.Add(new MailAddress("waruni@bpl.lk"));
            message.Bcc.Add(new MailAddress("isuru@ftservices.net"));
            //message.Bcc.Add(new MailAddress("asanka@fteenet.com"));
            if (String.IsNullOrEmpty(strBody))
            {
                strBody = "No Data Transfered";
            }
            else
            {
                strBody = "<h3>Following Data Received From " + EstateName + " Estate</h3>" + strBody;
            }

            message.Subject = "Checkroll Data Upload - " + EstateName;
            message.Body = "Do not reply to this mail, as this is generated automatically from the data server - OLAX Studio <br>" + strBody;
                //+"<h2>Data Received From "+ EstateName +" </h2><br>"+ " <h3>Checkroll From:" + dtDate.Value.ToShortDateString() + " To:" + dtToDate.Value.ToShortDateString() +"</h3> ";
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("olaxdashboard@gmail.com", "pass1234");
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your Mail has Failed...!");
            } 
        }

        public static void Compress(string strPath, String NewFileName)
        {
            DateTime current;
            string dstFile = "";
            FileStream fsIn = null;
            FileStream fsOut = null;
            GZipStream gzip = null;
            byte[] buffer;
            int count = 0;
            try
            {
                current = DateTime.Now;
                dstFile = Application.StartupPath + "\\" + NewFileName;
                fsOut = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsOut, CompressionMode.Compress, true);
                fsIn = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[fsIn.Length];
                count = fsIn.Read(buffer, 0, buffer.Length);
                fsIn.Close();
                fsIn = null;
                gzip.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.ToString());
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }

                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }

                if (fsIn != null)
                {
                    fsIn.Close();
                    fsIn = null;
                }
            }
        }

        private Boolean Upload(string filename)
        {
            String ftpServerIP = "";
            String ftpUserID = "";
            String ftpPassword = "";

            DataTable dt = mySettings.ListSettings();
            if (dt.Rows.Count > 0)
            {
                ftpServerIP = dt.Rows[0][0].ToString();
                ftpUserID = dt.Rows[0][1].ToString();
                ftpPassword = dt.Rows[0][2].ToString();
            }


            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            reqFTP = (FtpWebRequest)FtpWebRequest.Create
                     (new Uri(ftpServerIP + "/" + fileInf.Name));

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            reqFTP.KeepAlive = false;

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.UseBinary = true;

            reqFTP.ContentLength = fileInf.Length;

            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            FileStream fs = fileInf.OpenRead();

            try
            {
                Stream strm = reqFTP.GetRequestStream();

                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            ////return true;
        }

    }
}