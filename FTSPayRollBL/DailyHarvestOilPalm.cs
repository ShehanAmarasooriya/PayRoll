using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class DailyHarvestOilPalm
    {
        #region VariableDeclaration
        private DateTime dtHarvestDate;

        public DateTime DtHarvestDate
        {
            get { return dtHarvestDate; }
            set { dtHarvestDate = value; }
        }
        private Boolean boolHolidayYesNo;

        public Boolean BoolHolidayYesNo
        {
            get { return boolHolidayYesNo; }
            set { boolHolidayYesNo = value; }
        }
        private String strHarvestDate;

        public String StrHarvestDate
        {
            get { return strHarvestDate; }
            set { strHarvestDate = value; }
        }
        private Int32 intWorkType;

        public Int32 IntWorkType
        {
            get { return intWorkType; }
            set { intWorkType = value; }
        }
        private String strWorkType;

        public String StrWorkType
        {
            get { return strWorkType; }
            set { strWorkType = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private String strField;

        public String StrField
        {
            get { return strField; }
            set { strField = value; }
        }
        private String strBlock;

        public String StrBlock
        {
            get { return strBlock; }
            set { strBlock = value; }
        }
        private String strCategory;

        public String StrCategory
        {
            get { return strCategory; }
            set { strCategory = value; }
        }
        private String strLabourType;

        public String StrLabourType
        {
            get { return strLabourType; }
            set { strLabourType = value; }
        }
        private String strLabourEstate;

        public String StrLabourEstate
        {
            get { return strLabourEstate; }
            set { strLabourEstate = value; }
        }
        private String strLabourDivision;

        public String StrLabourDivision
        {
            get { return strLabourDivision; }
            set { strLabourDivision = value; }
        }
        private String strLabourField;

        public String StrLabourField
        {
            get { return strLabourField; }
            set { strLabourField = value; }
        }
        private Int32 intCropType;

        public Int32 IntCropType
        {
            get { return intCropType; }
            set { intCropType = value; }
        }
        private String strCropType;

        public String StrCropType
        {
            get { return strCropType; }
            set { strCropType = value; }
        }
        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
        }
        private String strEmpName;

        public String StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
        }
        private String strJob;

        public String StrJob
        {
            get { return strJob; }
            set { strJob = value; }
        }
        private Boolean boolPRIYesNo;

        public Boolean BoolPRIYesNo
        {
            get { return boolPRIYesNo; }
            set { boolPRIYesNo = value; }
        }
        private Boolean boolTaskCompletedYesNo;

        public Boolean BoolTaskCompletedYesNo
        {
            get { return boolTaskCompletedYesNo; }
            set { boolTaskCompletedYesNo = value; }
        }

        private Int32 intFullHalf;

        public Int32 IntFullHalf
        {
            get { return intFullHalf; }
            set { intFullHalf = value; }
        }
        //private float flFullHalf;

        //public float FlFullHalf
        //{
        //    get { return flFullHalf; }
        //    set { flFullHalf = value; }
        //}

        private float flQty;

        public float FlQty
        {
            get { return flQty; }
            set { flQty = value; }
        }
        private float flOKgs;

        public float FlOKgs
        {
            get { return flOKgs; }
            set { flOKgs = value; }
        }
        private float flHours;

        public float FlHours
        {
            get { return flHours; }
            set { flHours = value; }
        }
        private float flManDays;

        public float FlManDays
        {
            get { return flManDays; }
            set { flManDays = value; }
        }
        private float flHoliManDays;

        public float FlHoliManDays
        {
            get { return flHoliManDays; }
            set { flHoliManDays = value; }
        }

        private Int32 intHatvestEntryId;

        public Int32 IntHatvestEntryId
        {
            get { return intHatvestEntryId; }
            set { intHatvestEntryId = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }

        private Boolean boolFormLoad;

        public Boolean BoolFormLoad
        {
            get { return boolFormLoad; }
            set { boolFormLoad = value; }
        }

        private Boolean boolPaidHolidayYesNo;

        public Boolean BoolPaidHolidayYesNo
        {
            get { return boolPaidHolidayYesNo; }
            set { boolPaidHolidayYesNo = value; }
        }
        private float flNorm;

        public float FlNorm
        {
            get { return flNorm; }
            set { flNorm = value; }
        }
        private float flScrapQty;

        public float FlScrapQty
        {
            get { return flScrapQty; }
            set { flScrapQty = value; }
        }

        private String strCWType;

        public String StrCWType
        {
            get { return strCWType; }
            set { strCWType = value; }
        }
        private Int32 intCWType;

        public Int32 IntCWType
        {
            get { return intCWType; }
            set { intCWType = value; }
        }

        private Decimal decAreaCovered;

        public Decimal DecAreaCovered
        {
            get { return decAreaCovered; }
            set { decAreaCovered = value; }
        }

        private Int32 fieldCropType;

        public Int32 FieldCropType
        {
            get { return fieldCropType; }
            set { fieldCropType = value; }
        }
        private Int32 intPRINorm;

        public Int32 IntPRINorm
        {
            get { return intPRINorm; }
            set { intPRINorm = value; }
        }
        private String strACCode;

        public String StrACCode
        {
            get { return strACCode; }
            set { strACCode = value; }
        }
        private String strMusterChitNumber;

        public String StrMusterChitNumber
        {
            get { return strMusterChitNumber; }
            set { strMusterChitNumber = value; }
        }
        private String strGangNo;

        public String StrGangNo
        {
            get { return strGangNo; }
            set { strGangNo = value; }
        }

        private Decimal decSundryTaskCompleted;

        public Decimal DecSundryTaskCompleted
        {
            get { return decSundryTaskCompleted; }
            set { decSundryTaskCompleted = value; }
        }
        private Int32 intTappingType;

        public Int32 IntTappingType
        {
            get { return intTappingType; }
            set { intTappingType = value; }
        }

        private Decimal decBunches;

        public Decimal DecBunches
        {
            get { return decBunches; }
            set { decBunches = value; }
        }
        #endregion

        public DataTable ListHarvestForDivision(DateTime HDate, String DivisionID, Int32 workType,Int32 intCrop)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));//0
            dt.Columns.Add(new DataColumn("EMPName"));//1
            dt.Columns.Add(new DataColumn("WorkCode"));//2
            dt.Columns.Add(new DataColumn("Quantity"));//3
            dt.Columns.Add(new DataColumn("Over kilos"));//4
            dt.Columns.Add(new DataColumn("LabourType"));//5
            dt.Columns.Add(new DataColumn("LentDivision"));//5
            dt.Columns.Add(new DataColumn("ManDays"));//7
            dt.Columns.Add(new DataColumn("Field"));//8            
            dt.Columns.Add(new DataColumn("Ref#"));//9
            dt.Columns.Add(new DataColumn("NormKilos"));//10
            dt.Columns.Add(new DataColumn("ScrapKgs"));//11
            dt.Columns.Add(new DataColumn("CashWorkType"));//12
            dt.Columns.Add(new DataColumn("MusterChitNumber"));//13
            dt.Columns.Add(new DataColumn("GangNumber"));//14
            dt.Columns.Add(new DataColumn("MainACCode"));//15 
            dt.Columns.Add(new DataColumn("Bunches"));//16                
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision AS LentDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID, CASE WHEN (dbo.DailyGroundTransactions.TaskCompleted = 1)  THEN 'Y' ELSE 'N' END AS TaskCompleted,  dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.ScrapKgs,  dbo.DailyGroundTransactions.CashWorkType,dbo.DailyGroundTransactions.MusterChitNumber,dbo.DailyGroundTransactions.GangNumber,dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.Bunches FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (CropType='"+intCrop+"') and  (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "')AND (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('ABS')) AND (WorkType='" + workType + "') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                //EmpName
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                //EmpNo
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                //WorkCode
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                //Quantity
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetDecimal(3);
                }
                //Overkilos
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }
                //Lent Division
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                //Lent Field
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                //Mandays
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDecimal(7);
                }
                //field
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                //Autokey
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[9] = dataReader.GetInt32(10);
                }
                //normKilos
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[10] = dataReader.GetDecimal(11);
                }
                //Scrap
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[11] = dataReader.GetDecimal(12);
                }
                //CashWorkType
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[12] = dataReader.GetInt32(13);
                }
                //ChitNumber
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[13] = dataReader.GetString(14).Trim();
                }
                //GangNo
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[14] = dataReader.GetString(15).Trim();
                }
                //MainAC
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[15] = dataReader.GetString(16).Trim();
                }
                //Bunches
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[16] = dataReader.GetDecimal(17);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListHarvestForDivision(DateTime HDate, String DivisionID, Int32 workType,String strSearchText,Int32 intCrop)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));//0
            dt.Columns.Add(new DataColumn("EMPName"));//1
            dt.Columns.Add(new DataColumn("WorkCode"));//2
            dt.Columns.Add(new DataColumn("Quantity"));//3
            dt.Columns.Add(new DataColumn("Over kilos"));//4
            dt.Columns.Add(new DataColumn("LabourType"));//5
            dt.Columns.Add(new DataColumn("LentDivision"));//5
            dt.Columns.Add(new DataColumn("ManDays"));//7
            dt.Columns.Add(new DataColumn("Field"));//8            
            dt.Columns.Add(new DataColumn("Ref#"));//9
            dt.Columns.Add(new DataColumn("NormKilos"));//10
            dt.Columns.Add(new DataColumn("ScrapKgs"));//11
            dt.Columns.Add(new DataColumn("CashWorkType"));//12
            dt.Columns.Add(new DataColumn("MusterChitNumber"));//13
            dt.Columns.Add(new DataColumn("GangNumber"));//14
            dt.Columns.Add(new DataColumn("MainACCode"));//15  
            dt.Columns.Add(new DataColumn("Bunches"));//16               
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision AS LentDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID, CASE WHEN (dbo.DailyGroundTransactions.TaskCompleted = 1)  THEN 'Y' ELSE 'N' END AS TaskCompleted,  dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.ScrapKgs,  dbo.DailyGroundTransactions.CashWorkType,dbo.DailyGroundTransactions.MusterChitNumber,dbo.DailyGroundTransactions.GangNumber,dbo.DailyGroundTransactions.SubCategoryCode FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (CropType='"+intCrop+"') and  (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "')AND (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('ABS')) AND (WorkType='" + workType + "') AND (dbo.DailyGroundTransactions.EmpNo like '" + strSearchText + "') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                //EmpName
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                //EmpNo
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                //WorkCode
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                //Quantity
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetDecimal(3);
                }
                //Overkilos
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }
                //Lent Division
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                //Lent Field
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                //Mandays
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDecimal(7);
                }
                //field
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                //Autokey
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[9] = dataReader.GetInt32(10);
                }
                //normKilos
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[10] = dataReader.GetDecimal(11);
                }
                //Scrap
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[11] = dataReader.GetDecimal(12);
                }
                //CashWorkType
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[12] = dataReader.GetInt32(13);
                }
                //ChitNumber
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[13] = dataReader.GetString(14).Trim();
                }
                //GangNo
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[14] = dataReader.GetString(15).Trim();
                }
                //MainAC
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[15] = dataReader.GetString(16).Trim();
                }
                //Bunches
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[16] = dataReader.GetDecimal(17);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        //------------
        /// <summary>
        /// Insert Daily Harvest Entry Of an Employee
        /// </summary>
        /// <returns>Status - ADDED, EXISTS</returns>
        public String InsertHarvetEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtHarvestDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@holidayYesNo", SqlDbType.Bit);
            param.Value = BoolHolidayYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@HoliManDays", SqlDbType.Float);
            param.Value = FlHoliManDays;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@cropType", SqlDbType.Int);
            param.Value = IntCropType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workType", SqlDbType.Int);
            param.Value = IntWorkType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fieldId", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@blockId", SqlDbType.VarChar, 50);
            param.Value = StrBlock;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@jobId", SqlDbType.VarChar, 50);
            param.Value = StrJob;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourType", SqlDbType.VarChar, 50);
            param.Value = StrLabourType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourEstate", SqlDbType.VarChar, 50);
            param.Value = StrLabourEstate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourDivision", SqlDbType.VarChar, 50);
            param.Value = StrLabourDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourField", SqlDbType.VarChar, 50);
            param.Value = StrLabourField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty", SqlDbType.Float);
            param.Value = FlQty;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@manDays", SqlDbType.Float);
            param.Value = FlManDays;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@taskCompleted", SqlDbType.Bit, 1);
            param.Value = BoolTaskCompletedYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fullHalf", SqlDbType.Int);
            param.Value = IntFullHalf;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@normValue", SqlDbType.Float);
            param.Value = FlNorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@paidHoliday", SqlDbType.Bit, 1);
            param.Value = BoolPaidHolidayYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@OKgs", SqlDbType.Float);
            param.Value = FlOKgs;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@scrapKgs", SqlDbType.Float);
            param.Value = FlScrapQty;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@cwType", SqlDbType.Int);
            param.Value = IntCWType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AreaCovered", SqlDbType.Decimal);
            param.Value = DecAreaCovered;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldCropType", SqlDbType.Int);
            param.Value = FieldCropType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PRINorm", SqlDbType.Int);
            param.Value = IntPRINorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AccountCode", SqlDbType.VarChar, 50);
            param.Value = StrACCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MusterChitNumber", SqlDbType.VarChar, 50);
            param.Value = StrMusterChitNumber;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@GangNo", SqlDbType.VarChar, 50);
            param.Value = StrGangNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@SundryTaskCompleted", SqlDbType.Decimal);
            param.Value = DecSundryTaskCompleted;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@TappingType", SqlDbType.Int);
            param.Value = IntTappingType;
            paramList.Add(param);

            /*hourly employees not available*/
            //param = SQLHelper.CreateParameter("@hours", SqlDbType.Float);
            //param.Value = FlHours;
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddDailyHarvestOilPalm", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public DataTable ListHarvestEntriesForDivision(DateTime HDate, String DivisionID, Int32 RefNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("Estate"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("Holiday"));
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("CropType"));
            dt.Columns.Add(new DataColumn("WorkType"));
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("TaskCompleted"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("LabourEstate"));
            dt.Columns.Add(new DataColumn("LabourDivision"));
            dt.Columns.Add(new DataColumn("LabourField"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("Norm"));
            dt.Columns.Add(new DataColumn("ScrapKgs"));
            dt.Columns.Add(new DataColumn("CashWorkType"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldCropType"));//22
            dt.Columns.Add(new DataColumn("SundryTask"));
            dt.Columns.Add(new DataColumn("Bunches"));//24


            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.EstateID, dbo.DailyGroundTransactions.EmpNo, dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.LabourEstate, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.ScrapKgs, dbo.DailyGroundTransactions.CashWorkType, dbo.DailyGroundTransactions.AreaCovered, 0,SundryTaskCompleted, dbo.DailyGroundTransactions.Bunches FROM dbo.DailyGroundTransactions  WHERE  (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102) AND (DivisionID = '" + DivisionID + "') AND (AutoKey = '" + RefNo + "'))", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                //ref#
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                //date entered
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetDateTime(1);
                }
                //empno
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                //holidayYesNo
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                //LabourType
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetBoolean(4);
                }
                //crop type
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                //worktype
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetInt32(6);
                }
                //divisionid
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                //fieldid
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                //work code id
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                //task completed
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10).Trim();
                }
                //full half
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetBoolean(11);
                }
                //workqty
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetInt32(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetString(14).Trim();
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15).Trim();
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetDecimal(17);
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetDecimal(18);
                }
                //Scrap
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetDecimal(19);
                }
                //CashWorkType
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetInt32(20);
                }
                //AreaCovered
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetDecimal(21);
                }
                //Field type
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetInt32(22);
                }
                if (!dataReader.IsDBNull(23))
                {
                    dtrow[23] = dataReader.GetDecimal(23);
                }
                //Bunches
                if (!dataReader.IsDBNull(24))
                {
                    dtrow[24] = dataReader.GetDecimal(24);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public String DeleteHarvetEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@harvestId", SqlDbType.Int);
            param.Value = IntHatvestEntryId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteDailyHarvestOilPalm", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public DataTable GetDaySummary(DateTime HDate, String DivisionID, Int32 workType, Int32 intIsContract, Boolean boolCWOkg,Int32 intCrop)
        {
            SqlDataReader reader;
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingNames"));
            dtnew.Columns.Add(new DataColumn("NotOffered"));
            dtnew.Columns.Add(new DataColumn("Sundry"));
            dtnew.Columns.Add(new DataColumn("Kilos"));
            dtnew.Columns.Add(new DataColumn("OverKilos"));

            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where (CropType='"+intCrop+"') and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('X%')) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[1] = reader.GetInt32(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();

            if (workType == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType='"+intCrop+"') and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('OHV')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            else if (workType == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType='"+intCrop+"') and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('OHV')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();




            if (workType == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType='"+intCrop+"') and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK','TAP','OHV')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            else if (workType == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType='"+intCrop+"') and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK','TAP','OHV')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[2] = reader.GetDecimal(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT     SUM(WorkQty) AS Kilos FROM dbo.DailyGroundTransactions WHERE (CropType='"+intCrop+"') and (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (WorkType = '" + workType + "') AND (DivisionID = '" + DivisionID + "') AND (IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND  (WorkCodeID = 'OHV') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT     SUM(OverKgs) AS Kilos FROM dbo.DailyGroundTransactions WHERE (CropType='"+intCrop+"') and (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (WorkType = '" + workType + "') AND (DivisionID = '" + DivisionID + "') AND (IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND  (WorkCodeID = 'OHV') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();
            dtnew.Rows.Add(dtRow1);
            return dtnew;
        }

    }
}
