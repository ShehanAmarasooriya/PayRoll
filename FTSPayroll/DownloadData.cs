using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace FTSPayroll
{
    public partial class DownloadData : Form
    {
        FTSPayRollBL.DownloadData objDownloadData = new FTSPayRollBL.DownloadData();
        public DateTime DateEntered = DateTime.Now.Date;
        FTSPayRollBL.DailyHarvest DHarvest = new FTSPayRollBL.DailyHarvest();
        FTSPayRollBL.MonthlyHoliday objMonthlyHoliday = new FTSPayRollBL.MonthlyHoliday();
        FTSPayRollBL.EmployeeMaster objEmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.DivisionWiseNorm DivNorm = new FTSPayRollBL.DivisionWiseNorm();
        FTSPayRollBL.EmployeeMaster EmpMaster = new FTSPayRollBL.EmployeeMaster();
        FTSPayRollBL.EstateDivisionBlock EstDivField = new FTSPayRollBL.EstateDivisionBlock();
        FTSPayRollBL.Job JobMaster=new FTSPayRollBL.Job();

        public DownloadData()
        {
            InitializeComponent();
        }

        private void btnCLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPathSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            
            //Not Allows multiple files to be selected 
            FileDialog.Multiselect = false;

            //Call the ShowDialog method to show the dialog box
            bool? userClickedOK = Convert.ToBoolean(FileDialog.ShowDialog());

            //Process input if the user clicked OK 
            if (userClickedOK == true)
            {
                txtPath.Text = FileDialog.FileName;
            }     
        }

        private void btnDOWNLOAD_Click(object sender, EventArgs e)
        {
            try
            {
                objDownloadData.deleteDailyHarvestTemp();
                String Status ="";

                if (txtPath.Text.Trim() != "")
                {
                    using (StreamReader StReader = new StreamReader(txtPath.Text.Trim()))
                    {
                        prBar.Maximum = 10000;
                        string[] lines = System.IO.File.ReadAllLines(txtPath.Text.Trim());

                        foreach (String line in lines)
                        {
                            if (line != null)
                            {
                                prBar.Value += 1;
                                Application.DoEvents();

                                String EstateID = FTSPayRollBL.User.StrEstate;
                                //String EstateID = line.Substring(0, 2);
                                String DivisionID = line.Substring(3, 3);
                                String Empno = line.Substring(24, 5).Trim().PadLeft(4,'0');
                                Decimal WeighQty1 = 0;
                                Decimal WeighQty2 = 0;
                                Decimal WeighQty3 = 0; 

                                String WeighQty1_Div = "NA";
                                String WeighQty1_FieldID = "NA";

                                String WeighQty2_Div = "NA";
                                String WeighQty2_FieldID = "NA";

                                String WeighQty3_Div = "NA";
                                String WeighQty3_FieldID = "NA";

                                Int32 intWorkType=0;
                                Int32 intFullHalf = 2;
                                     
                                CultureInfo ukCulture = new CultureInfo("en-GB");
                                DateEntered = DateTime.Parse(line.Substring(13, 10), ukCulture.DateTimeFormat);

                                String crop = line.Substring(30, 1);
                                Int32 CropType = 0;

                                if (crop == "T")
                                {
                                    CropType = 1;
                                }
                               

                                String strWorkType = line.Substring(161, 1).Trim();
                                if (!String.IsNullOrEmpty(strWorkType))
                                {
                                    if (strWorkType.Equals("8"))
                                    {
                                        intWorkType = 1;
                                        intFullHalf=2;
                                    }
                                    else if (strWorkType.Equals("0"))
                                    {
                                        intWorkType = 2;
                                        intFullHalf=2;
                                    }
                                    else if (strWorkType.Equals("4"))
                                    {
                                        intWorkType = 1;
                                        intFullHalf = 1;

                                    }
                                    else if (strWorkType.Equals("12"))
                                    {
                                        intWorkType = 1;
                                        intFullHalf = 2;
                                    }
                                    else
                                    {
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString() + "ErrorLog.txt", true))
                                        {
                                            file.WriteLine(" Invalid Work Type " + strWorkType + " IntWorkType : " + intWorkType );
                                        }
                                        MessageBox.Show("Error Invalid WorkType :"+strWorkType);
                                        break;
                                    }
                                }
                                //1st weighment
                                String wie = line.Substring(42, 5).Trim();
                                if (wie != "")
                                {
                                    WeighQty1 = Decimal.Parse(wie);
                                    //if (Empno=="2359")
                                    //    MessageBox.Show("here");
                                    if(!String.IsNullOrEmpty(line.Substring(48, 3).Trim()))
                                    {
                                        WeighQty1_Div = line.Substring(48, 3).Trim();
                                    }
                                    //if (line.Substring(52, 2).Trim() != "")
                                    if (!String.IsNullOrEmpty(line.Substring(52, 4).Trim()))
                                    {
                                        WeighQty1_FieldID = line.Substring(52, 4).Trim();
                                    }
                                }
                                String Qty1_Time = line.Substring(65, 5);
                                String qtytime = DateEntered.ToShortDateString() + " " + line.Substring(65, 5).Trim();
                                DateTime WeighQty1_Time = DateTime.Parse(qtytime);

                                //2nd weighment
                                wie = line.Substring(71, 5).Trim();
                                if (wie != "")
                                {
                                    WeighQty2 = Decimal.Parse(wie);
                                    if (line.Substring(77, 3).Trim() != "")
                                    {
                                        WeighQty2_Div = line.Substring(77, 3);
                                    }
                                    if (line.Substring(81, 4).Trim() != "")
                                    {
                                        WeighQty2_FieldID = line.Substring(81, 4);
                                    }
                                }                                
                                qtytime = DateEntered.ToShortDateString() + " " + line.Substring(94, 5).Trim();
                                DateTime WeighQty2_Time = DateTime.Parse(qtytime);

                                //3rd weighment
                                wie = line.Substring(100, 5).Trim();
                                if (wie != "")
                                {
                                    WeighQty3 = Decimal.Parse(wie);
                                    if (line.Substring(106, 3).Trim() != "")
                                    {
                                        WeighQty3_Div = line.Substring(106, 3);
                                    }
                                    if (line.Substring(110, 4).Trim() != "")
                                    {
                                        WeighQty3_FieldID = line.Substring(110, 4);
                                    }
                                }                                
                                qtytime = DateEntered.ToShortDateString() + " " + line.Substring(123, 5).Trim();
                                DateTime WeighQty3_Time = DateTime.Parse(qtytime);


                                if (DivisionID.Trim() == "")
                                {
                                    //set Division
                                    if (WeighQty1_Div != "" && WeighQty1_Div != "NA")
                                        DivisionID = WeighQty2_Div;
                                    else if (WeighQty2_Div != "" && WeighQty2_Div != "NA")
                                        DivisionID = WeighQty2_Div;
                                    else if (WeighQty3_Div != "" && WeighQty3_Div != "NA")
                                        DivisionID = WeighQty3_Div;
                                }
                                //String strWorkType = line.Substring(161, 1).Trim();
                                //if (String.IsNullOrEmpty(strWorkType))
                                //{
                                //    intWorkType = Convert.ToInt32(strWorkType);
                                //}

                                //String WorkCodeID = "PLK";
                                String WorkCodeID = line.Substring(38, 3).Trim();
                                if (WeighQty3 + WeighQty2 + WeighQty1 > 0)
                                {
                                    WorkCodeID = "PLK";
                                }
                                else
                                {
                                   
                                    if (WorkCodeID.ToUpper().Equals("PLK"))
                                    {
                                        WorkCodeID = "PLK";
                                    }
                                    else
                                    {
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString() + "ErrorLog.txt", true))
                                        {
                                            file.WriteLine(" Other WorkCode " + WorkCodeID + " Div : " + DivisionID + " Emp :" + Empno);
                                        }
                                        if(!JobMaster.IsJobAvailable(WorkCodeID))
                                        {
                                            MessageBox.Show("WorkCode "+ WorkCodeID+" Is Not Available In The Job master ");
                                            break;
                                        }                                        

                                    }
                                }

                                if (DivisionID.Trim() != "" && DivisionID != "NA")
                                {
                                    Status = objDownloadData.InsertDailyHarvestEntry(EstateID, DivisionID, DateEntered, Empno, CropType, WorkCodeID, WeighQty1, WeighQty1_Div, WeighQty1_FieldID, WeighQty1_Time, WeighQty2, WeighQty2_Div, WeighQty2_FieldID, WeighQty2_Time, WeighQty3, WeighQty3_Div, WeighQty3_FieldID, WeighQty3_Time,intWorkType,intFullHalf);
                                }
                                
                                
                            }
                            
                        }

                        if (Status == "ADDED")
                        {
                            prBar.Value = prBar.Maximum;
                            MessageBox.Show("Data downloaded successfully.!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            prBar.Value = 0;

                            //Data bind into grid view start from here
                            if (objDownloadData.GetDailyGroundTransactionTemp(DateEntered).Rows.Count > 0)
                            {
                                gvlist.DataSource = objDownloadData.GetDailyGroundTransactionTemp(DateEntered);

                                gvlist.Columns[0].Visible = false;
                                gvlist.Columns[1].Width = 60;
                                gvlist.Columns[2].Width = 65;
                                gvlist.Columns[3].Width = 50;
                                gvlist.Columns[4].Visible = false;
                                gvlist.Columns[5].Width = 50;
                                gvlist.Columns[6].Width = 30;
                                gvlist.Columns[7].Visible = false;
                                gvlist.Columns[8].Width = 50;
                                gvlist.Columns[9].Width = 60;
                                gvlist.Columns[10].Width = 60;
                                gvlist.Columns[11].Visible = false;
                                gvlist.Columns[12].Width = 50;
                                gvlist.Columns[13].Width = 60;
                                gvlist.Columns[14].Width = 60;
                                gvlist.Columns[15].Visible = false;
                                gvlist.Columns[16].Width = 50;
                                gvlist.Columns[17].Width = 60;
                                gvlist.Columns[18].Visible = false;
                                gvlist.Columns[19].Visible = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No data downloaded.!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            prBar.Value = 0;
                        }
                    }
                }
                else
                    MessageBox.Show("File path can't be empty.!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Decimal CalcOverkilos(Decimal Qty1, Decimal Qty2, Decimal Qty3, String DivisionID, String EmpNo, DateTime dtDate)
        {
            Decimal normVal = 0;            
            Decimal OverKgs = 0;
            //normVal = Convert.ToDecimal(DivNorm.getLatestNormOfDivision(this.cmbDivision.Text));
           
            Decimal decQt = 0;
            decQt = Qty1 + Qty2 + Qty3;            

            normVal = Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(DivisionID, EmpMaster.GetEmployeeGenderByEmpNo(EmpNo, DivisionID), "PLK", dtDate));

            if ((decQt - normVal) > 0)
            {
                OverKgs = decQt - normVal;                
            }
            else
            {
                OverKgs = 0;
            }

            return OverKgs;
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Int32 Count = 0;

                    for (int i = 0; i < gvlist.Rows.Count; i++)
                    {

                        //DHarvest.FlOKgs = float.Parse(CalcOverkilos(Convert.ToDecimal(gvlist.Rows[i].Cells[6].Value.ToString()), Convert.ToDecimal(gvlist.Rows[i].Cells[10].Value.ToString()), Convert.ToDecimal(gvlist.Rows[i].Cells[14].Value.ToString()), gvlist.Rows[i].Cells[1].Value.ToString(), gvlist.Rows[i].Cells[3].Value.ToString(), Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString())).ToString());
                        DHarvest.BoolIsContract = false;
                        DHarvest.StrContractor = "NA";

                        DHarvest.BoolBlockPlk2013 = false;


                        DHarvest.BoolBlockPlk = false;
                        DHarvest.StrContractor = "NA";

                        //DHarvest.IntFullHalf = int.Parse("1");
                        DHarvest.IntFullHalf = Convert.ToInt32(gvlist.Rows[i].Cells[21].Value.ToString());
                        DHarvest.FlManDays = (float)(DHarvest.IntFullHalf / 2.0);

                        DHarvest.DtHarvestDate = Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString());

                        if (objMonthlyHoliday.IsPoyaday(Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString())) == true && objMonthlyHoliday.IsSunday(Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString())) == true)
                        {
                            if (DHarvest.IntFullHalf == 1)
                            {
                                DHarvest.BoolHolidayYesNo = true;
                                DHarvest.FlHoliManDays = float.Parse("0.5");
                                DHarvest.BoolPaidHolidayYesNo = false;
                            }
                            else
                            {
                                DHarvest.BoolHolidayYesNo = true;
                                DHarvest.FlHoliManDays = float.Parse("1.5");
                                DHarvest.BoolPaidHolidayYesNo = false;
                            }
                            
                        }
                        else if (objMonthlyHoliday.IsPaidHoliday(Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString())) == true)
                        {
                            DHarvest.BoolPaidHolidayYesNo = true;
                            DHarvest.BoolHolidayYesNo = false;
                        }
                        else
                        {
                            DHarvest.BoolHolidayYesNo = false;
                            int fHoliManDays = 0;
                            DHarvest.FlHoliManDays = (float)fHoliManDays;
                            DHarvest.BoolPaidHolidayYesNo = false;
                        }
                        //if (gvlist.Rows[i].Cells[3].Value.ToString().PadLeft(4, '0') == "0497")
                        //{
                        //    MessageBox.Show("HERE");
                        //}

                        DHarvest.StrDivision = gvlist.Rows[i].Cells[1].Value.ToString();

                        if (gvlist.Rows[i].Cells[8].Value.ToString() != "NA" && !String.IsNullOrEmpty(gvlist.Rows[i].Cells[8].Value.ToString()))
                        {
                            DHarvest.StrField = gvlist.Rows[i].Cells[8].Value.ToString();
                        }
                        else if (gvlist.Rows[i].Cells[12].Value.ToString() != "NA" && !String.IsNullOrEmpty(gvlist.Rows[i].Cells[12].Value.ToString()))
                        {
                            DHarvest.StrField = gvlist.Rows[i].Cells[12].Value.ToString();
                        }
                        else if (gvlist.Rows[i].Cells[16].Value.ToString() != "NA" && !String.IsNullOrEmpty(gvlist.Rows[i].Cells[16].Value.ToString()))
                        {
                            DHarvest.StrField = gvlist.Rows[i].Cells[16].Value.ToString();
                        }
                        if (EstDivField.IsFieldAvailable(gvlist.Rows[i].Cells[8].Value.ToString(), DHarvest.StrDivision))
                        {
                            DHarvest.StrField = gvlist.Rows[i].Cells[8].Value.ToString();
                        }
                        else if (EstDivField.IsFieldAvailable(gvlist.Rows[i].Cells[12].Value.ToString(), DHarvest.StrDivision))
                        {
                            DHarvest.StrField = gvlist.Rows[i].Cells[12].Value.ToString();
                        }
                        else if (EstDivField.IsFieldAvailable(gvlist.Rows[i].Cells[16].Value.ToString(), DHarvest.StrDivision))
                        {
                            DHarvest.StrField = gvlist.Rows[i].Cells[16].Value.ToString();
                        }
                        else
                        {
                            DHarvest.StrField = "NA";
                        }
                        

                        DHarvest.StrLabourType = "General";
                        DHarvest.StrLabourEstate = "NA";
                        DHarvest.StrLabourDivision = "NA";
                        DHarvest.StrLabourField = "NA";

                        DHarvest.IntCropType = int.Parse("1");
                        //DHarvest.IntWorkType = int.Parse("1");
                        DHarvest.IntWorkType = int.Parse(gvlist.Rows[i].Cells[20].Value.ToString().PadLeft(1, '0'));
                        if (DHarvest.IntWorkType == 2)
                        {
                            DHarvest.FlOKgs = 0;
                        }
                        else
                        {
                            DHarvest.FlOKgs = float.Parse(CalcOverkilos(Convert.ToDecimal(gvlist.Rows[i].Cells[6].Value.ToString()), Convert.ToDecimal(gvlist.Rows[i].Cells[10].Value.ToString()), Convert.ToDecimal(gvlist.Rows[i].Cells[14].Value.ToString()), gvlist.Rows[i].Cells[1].Value.ToString(), gvlist.Rows[i].Cells[3].Value.ToString(), Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString())).ToString());
                        }
                        DHarvest.StrEmpNo = gvlist.Rows[i].Cells[3].Value.ToString().PadLeft(5, '0');
                        //if (DHarvest.StrEmpNo == "2607")
                        //{
                        //    MessageBox.Show("here");
                        //}
                        DHarvest.StrEmpName = objEmpMaster.GetEmployeeNameByEmpNo(gvlist.Rows[i].Cells[3].Value.ToString().PadLeft(5, '0'),gvlist.Rows[i].Cells[1].Value.ToString());
                        if (String.IsNullOrEmpty(DHarvest.StrEmpName))
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString() + "ErrorLog.txt", true))
                            {
                                file.WriteLine("Employee" + gvlist.Rows[i].Cells[3].Value.ToString() + " Div : " + gvlist.Rows[i].Cells[1].Value.ToString().PadLeft(5, '0') + " Does Not Exist In EmplyeeMaster");
                            }
                            MessageBox.Show("Employee " + gvlist.Rows[i].Cells[3].Value.ToString().PadLeft(5, '0') + " not exists in the division " + gvlist.Rows[i].Cells[1].Value.ToString());
                            break;
                        }
                        //DHarvest.StrJob = "PLK";
                        DHarvest.StrJob = gvlist.Rows[i].Cells[5].Value.ToString().Trim();
                        
                        DHarvest.BoolTaskCompletedYesNo = true;
                        

                        Decimal qty = Convert.ToDecimal(gvlist.Rows[i].Cells[6].Value.ToString()) + Convert.ToDecimal(gvlist.Rows[i].Cells[10].Value.ToString()) + Convert.ToDecimal(gvlist.Rows[i].Cells[14].Value.ToString());

                        DHarvest.FlQty = float.Parse(qty.ToString());
                        DHarvest.DecQty1 = float.Parse(gvlist.Rows[i].Cells[6].Value.ToString());
                        DHarvest.DecQty2 = float.Parse(gvlist.Rows[i].Cells[10].Value.ToString());
                        DHarvest.DecQty3 = float.Parse(gvlist.Rows[i].Cells[14].Value.ToString());
                        DHarvest.DecAreaCovered = float.Parse("0");
                        DHarvest.DecFieldWeight = float.Parse("0");


                        DHarvest.FlNorm = float.Parse((Convert.ToDecimal(DivNorm.getLatestRelavantNormOfDivision(gvlist.Rows[i].Cells[1].Value.ToString(), EmpMaster.GetEmployeeGenderByEmpNo(gvlist.Rows[i].Cells[3].Value.ToString(), gvlist.Rows[i].Cells[1].Value.ToString()), "PLK", Convert.ToDateTime(gvlist.Rows[i].Cells[2].Value.ToString())))).ToString());

                        //DHarvest.FlHours=txtHours.Text;
                        DHarvest.StrUserId = FTSPayRollBL.User.StrUserName;


                        try
                        {
                            DHarvest.BoolIsContract = false;
                            DHarvest.StrContractor = "NA";
                            DHarvest.DecContractorRate = 0;
                            DHarvest.BlEasyWeighYesNo = true;
                            DHarvest.BoolSpeciMedHalf = false;
                            String status = "Pending";
                            if (JobMaster.IsJobAvailable(DHarvest.StrJob))
                            {
                                status = DHarvest.InsertHarvetEntry();
                            }
                            else
                            {
                                MessageBox.Show("WorkCode "+DHarvest.StrJob+" Is Not Available In The Job Master");
                            }
                            if (status.Equals("ADDED"))
                            {
                                Count += 1;
                            }
                            else if (status.Equals("EXISTS"))
                            {
                                if (MessageBox.Show("Already exists..!", "Message", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information) == DialogResult.Abort)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Oops, something went wrong!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error, " + ex.Message);
                        }
                    }
                    gvlist.DataSource = null;
                    if (Count > 0)
                    {
                        MessageBox.Show("Entries added successfully.!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gvlist.DataSource = null;
                        //objDownloadData.deleteDailyHarvestTemp();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
    }
}