using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
   
    public class DailyHarvest
    {
        
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

        private float flPRINorm;

        public float FlPRINorm
        {
            get { return flPRINorm; }
            set { flPRINorm = value; }
        }
        private Boolean boolBlockPlk;

        public Boolean BoolBlockPlk
        {
            get { return boolBlockPlk; }
            set { boolBlockPlk = value; }
        }
        private float decQty1;

        public float DecQty1
        {
            get { return decQty1; }
            set { decQty1 = value; }
        }
        private float decQty2;

        public float DecQty2
        {
            get { return decQty2; }
            set { decQty2 = value; }
        }
        private float decQty3;

        public float DecQty3
        {
            get { return decQty3; }
            set { decQty3 = value; }
        }
        private float decAreaCovered;

        public float DecAreaCovered
        {
            get { return decAreaCovered; }
            set { decAreaCovered = value; }
        }
        private float decFieldWeight;

        public float DecFieldWeight
        {
            get { return decFieldWeight; }
            set { decFieldWeight = value; }
        }

        private String strContractor;

        public String StrContractor
        {
            get { return strContractor; }
            set { strContractor = value; }
        }
        private Boolean boolIsContract;

        public Boolean BoolIsContract
        {
            get { return boolIsContract; }
            set { boolIsContract = value; }
        }

        private Decimal decPerKiloConContribution;

        public Decimal DecPerKiloConContribution
        {
            get { return decPerKiloConContribution; }
            set { decPerKiloConContribution = value; }
        }
        private Decimal decPerDayConContribution;

        public Decimal DecPerDayConContribution
        {
            get { return decPerDayConContribution; }
            set { decPerDayConContribution = value; }
        }

        private Decimal decContractContribution;

        public Decimal DecContractContribution
        {
            get { return decContractContribution; }
            set { decContractContribution = value; }
        }

        private Decimal decContractorRate;

        public Decimal DecContractorRate
        {
            get { return decContractorRate; }
            set { decContractorRate = value; }
        }

        private Boolean boolBlockPlk2013;

        public Boolean BoolBlockPlk2013
        {
            get { return boolBlockPlk2013; }
            set { boolBlockPlk2013 = value; }
        }

        private Boolean blEasyWeighYesNo;

        public Boolean BlEasyWeighYesNo
        {
            get { return blEasyWeighYesNo; }
            set { blEasyWeighYesNo = value; }
        }

        
        private Boolean boolCashOkgYesNo;

        public Boolean BoolCashOkgYesNo
        {
            get { return boolCashOkgYesNo; }
            set { boolCashOkgYesNo = value; }
        }

        private Boolean boolSpeciMedHalf;

        public Boolean BoolSpeciMedHalf
        {
          get { return boolSpeciMedHalf; }
          set { boolSpeciMedHalf = value; }
        }

        private Decimal decNameValue;

        public Decimal DecNameValue
        {
            get { return decNameValue; }
            set { decNameValue = value; }
        }
        private String strACCode;

        public String StrACCode
        {
            get { return strACCode; }
            set { strACCode = value; }
        }
        private int intYear;

        public int IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }
        private int intMonth;

        public int IntMonth
        {
            get { return intMonth; }
            set { intMonth = value; }
        }

        private Decimal decBockPluckRate;

        public Decimal DecBockPluckRate
        {
            get { return decBockPluckRate; }
            set { decBockPluckRate = value; }
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

        private String strPHAddType;

        public String StrPHAddType
        {
            get { return strPHAddType; }
            set { strPHAddType = value; }
        }

        private Decimal decCashPlkRate;

        public Decimal DecCashPlkRate
        {
            get { return decCashPlkRate; }
            set { decCashPlkRate = value; }
        }
        private Decimal decSundryTaskCompleted;

        public Decimal DecSundryTaskCompleted
        {
            get { return decSundryTaskCompleted; }
            set { decSundryTaskCompleted = value; }
        }
        

        public void updateBlockPluckingRate()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@Year", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Month", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar);
            param.Value = strDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BockPlucRate", SqlDbType.Float);
            param.Value = DecBockPluckRate;
            paramList.Add(param);

            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spUpdateHarvestBlockPlk", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);

            cmd.Dispose();

        }

                
        
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
            param = SQLHelper.CreateParameter("@BlockPlkYesNo", SqlDbType.Bit);
            param.Value = BoolBlockPlk;
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
            //pass value 1 till the block is defined
            param.Value = "1";
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
            param = SQLHelper.CreateParameter("@workQty1", SqlDbType.Float);
            param.Value = DecQty1;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty2", SqlDbType.Float);
            param.Value = DecQty2;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty3", SqlDbType.Float);
            param.Value = DecQty3;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AreaCovered", SqlDbType.Float);
            param.Value = DecAreaCovered;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldWeight", SqlDbType.Float);
            param.Value = DecFieldWeight;
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
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar,50);
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
            param = SQLHelper.CreateParameter("@Contractor", SqlDbType.VarChar,50);
            param.Value = StrContractor;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@IsContract", SqlDbType.Float);
            param.Value = BoolIsContract;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ContractorRate", SqlDbType.Float);
            param.Value = DecContractorRate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlk2013", SqlDbType.Bit);
            param.Value = BoolBlockPlk2013;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EasyWeighYesNo", SqlDbType.Bit);
            param.Value = BlEasyWeighYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@SpecialMedicalHalfYesNo", SqlDbType.Bit);
            param.Value = BoolSpeciMedHalf;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DailyNameValue", SqlDbType.Decimal);
            param.Value = DecNameValue;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AccountCode", SqlDbType.VarChar,50);
            param.Value = StrACCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MusterChitNumber", SqlDbType.VarChar, 50);
            param.Value = StrMusterChitNumber;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@GangNo", SqlDbType.VarChar, 50);
            param.Value = StrGangNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CashKgRate", SqlDbType.Decimal);
            param.Value = DecCashPlkRate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@SundryTaskCompleted", SqlDbType.Decimal);
            param.Value = DecSundryTaskCompleted;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PRINorm", SqlDbType.Decimal);
            param.Value = FlPRINorm;
            paramList.Add(param);
            /*hourly employees not available*/
            //param = SQLHelper.CreateParameter("@hours", SqlDbType.Float);
            //param.Value = FlHours;
            //paramList.Add(param);
            
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddDailyHarvest", CommandType.StoredProcedure, paramList);
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

        public String InsertHarvetEntryNotOffered()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = DtHarvestDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlkYesNo", SqlDbType.Bit);
            param.Value = false;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@holidayYesNo", SqlDbType.Bit);
            param.Value = false; ;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@HoliManDays", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@cropType", SqlDbType.Int);
            param.Value = 1;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workType", SqlDbType.Int);
            param.Value = 1;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fieldId", SqlDbType.VarChar, 50);
            param.Value = "NA";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@blockId", SqlDbType.VarChar, 50);
            //pass value 1 till the block is defined
            param.Value = "1";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@jobId", SqlDbType.VarChar, 50);
            param.Value = StrJob;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourType", SqlDbType.VarChar, 50);
            param.Value = "General";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourEstate", SqlDbType.VarChar, 50);
            param.Value = "NA";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourDivision", SqlDbType.VarChar, 50);
            param.Value = "NA";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourField", SqlDbType.VarChar, 50);
            param.Value = "NA";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty1", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty2", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty3", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AreaCovered", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldWeight", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@manDays", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@taskCompleted", SqlDbType.Bit, 1);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fullHalf", SqlDbType.Int);
            param.Value = 2;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@normValue", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@paidHoliday", SqlDbType.Bit, 1);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@OKgs", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Contractor", SqlDbType.VarChar, 50);
            param.Value = "NA";
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@IsContract", SqlDbType.Float);
            param.Value = false;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ContractorRate", SqlDbType.Float);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlk2013", SqlDbType.Bit);
            param.Value = false;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EasyWeighYesNo", SqlDbType.Bit);
            param.Value = false;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@SpecialMedicalHalfYesNo", SqlDbType.Bit);
            param.Value = false;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DailyNameValue", SqlDbType.Decimal);
            param.Value = 0;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AccountCode", SqlDbType.VarChar, 50);
            param.Value = "NA";
            paramList.Add(param);
            //param = SQLHelper.CreateParameter("@CashOverKgYesNo", SqlDbType.Bit);
            //param.Value = BoolCashOkgYesNo;
            //paramList.Add(param);
            /*hourly employees not available*/
            //param = SQLHelper.CreateParameter("@hours", SqlDbType.Float);
            //param.Value = FlHours;
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddDailyHarvest", CommandType.StoredProcedure, paramList);
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
        

        public String InsertHarvetEntryCWOkg()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtHarvestDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlkYesNo", SqlDbType.Bit);
            param.Value = BoolBlockPlk;
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
            //pass value 1 till the block is defined
            param.Value = "1";
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
            param = SQLHelper.CreateParameter("@workQty1", SqlDbType.Float);
            param.Value = DecQty1;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty2", SqlDbType.Float);
            param.Value = DecQty2;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty3", SqlDbType.Float);
            param.Value = DecQty3;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AreaCovered", SqlDbType.Float);
            param.Value = DecAreaCovered;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldWeight", SqlDbType.Float);
            param.Value = DecFieldWeight;
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
            param = SQLHelper.CreateParameter("@Contractor", SqlDbType.VarChar, 50);
            param.Value = StrContractor;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@IsContract", SqlDbType.Float);
            param.Value = BoolIsContract;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ContractorRate", SqlDbType.Float);
            param.Value = DecContractorRate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlk2013", SqlDbType.Bit);
            param.Value = BoolBlockPlk2013;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EasyWeighYesNo", SqlDbType.Bit);
            param.Value = BlEasyWeighYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CashOverKgYesNo", SqlDbType.Bit);
            param.Value = BoolCashOkgYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DailyNameValue", SqlDbType.Decimal);
            param.Value = DecNameValue;
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
            
            /*hourly employees not available*/
            //param = SQLHelper.CreateParameter("@hours", SqlDbType.Float);
            //param.Value = FlHours;
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddDailyHarvestCWOkg", CommandType.StoredProcedure, paramList);
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

        public String InsertHarvetEntryBlockPlucking()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtHarvestDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlkYesNo", SqlDbType.Bit);
            param.Value = BoolBlockPlk;
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
            //pass value 1 till the block is defined
            param.Value = "1";
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
            param = SQLHelper.CreateParameter("@workQty1", SqlDbType.Float);
            param.Value = DecQty1;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty2", SqlDbType.Float);
            param.Value = DecQty2;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty3", SqlDbType.Float);
            param.Value = DecQty3;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AreaCovered", SqlDbType.Float);
            param.Value = DecAreaCovered;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldWeight", SqlDbType.Float);
            param.Value = DecFieldWeight;
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
            param = SQLHelper.CreateParameter("@Contractor", SqlDbType.VarChar, 50);
            param.Value = StrContractor;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@IsContract", SqlDbType.Float);
            param.Value = BoolIsContract;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ContractorRate", SqlDbType.Float);
            param.Value = DecContractorRate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ContractorRate", SqlDbType.Float);
            param.Value = DecContractorRate;
            paramList.Add(param);


            /*hourly employees not available*/
            //param = SQLHelper.CreateParameter("@hours", SqlDbType.Float);
            //param.Value = FlHours;
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddDailyHarvestBlockPlk", CommandType.StoredProcedure, paramList);
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

        public String UpdateHarvetEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@harvestId", SqlDbType.Int);
            param.Value = IntHatvestEntryId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@BlockPlkYesNo", SqlDbType.Bit);
            param.Value = BoolBlockPlk;
            paramList.Add(param);
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
            //pass value 1 till the block is defined
            param.Value = "1";
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
            param = SQLHelper.CreateParameter("@workQty1", SqlDbType.Float);
            param.Value = DecQty1;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty2", SqlDbType.Float);
            param.Value = DecQty2;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workQty3", SqlDbType.Float);
            param.Value = DecQty3;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AreaCovered", SqlDbType.Float);
            param.Value = DecAreaCovered;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldWeight", SqlDbType.Float);
            param.Value = DecFieldWeight;
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
            param = SQLHelper.CreateParameter("@Contractor", SqlDbType.VarChar, 50);
            param.Value = StrContractor;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@IsContract", SqlDbType.Float);
            param.Value = BoolIsContract;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ContractorRate", SqlDbType.Float);
            param.Value = DecContractorRate;
            paramList.Add(param);
            /*hourly employees not available*/
            //param = SQLHelper.CreateParameter("@hours", SqlDbType.Float);
            //param.Value = FlHours;
            //paramList.Add(param);
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser])  SELECT  GETDATE() AS Expr1, AutoKey, 'DailyHarvest' AS Expr2, DivisionID, EmpNo, DateEntered, WorkType, WorkCodeID, WorkQty, CashBlockYesNo,  '" + FTSPayRollBL.User.StrUserName + "' AS Expr3 FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + DtHarvestDate.ToShortDateString() + "', 102)) AND (WorkType = '"+IntWorkType.ToString()+"') AND (EmpNo = '"+StrEmpNo+"') AND (DivisionID = '"+StrDivision+"') AND  (CashBlockYesNo = '"+BoolBlockPlk+"')", CommandType.Text);            
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spUpdateDailyHarvestReplace", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser])  SELECT  GETDATE() AS Expr1, AutoKey, 'DailyHarvest' AS Expr2, DivisionID, EmpNo, DateEntered, WorkType, WorkCodeID, WorkQty, CashBlockYesNo,  '" + FTSPayRollBL.User.StrUserName + "' AS Expr3 FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + DtHarvestDate.ToShortDateString() + "', 102)) AND (WorkType = '" + IntWorkType.ToString() + "') AND (EmpNo = '" + StrEmpNo + "') AND (DivisionID = '" + StrDivision + "') AND  (CashBlockYesNo = '" + BoolBlockPlk + "')", CommandType.Text);            
            return Status;
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

            param = SQLHelper.CreateParameter("@DateEntered", SqlDbType.DateTime);
            param.Value = DtHarvestDate;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar);
            param.Value = StrEmpNo;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
            param.Value = StrDivision;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkType", SqlDbType.Int);
            param.Value = IntWorkType;
            paramList.Add(param);


            SqlCommand cmd = new SqlCommand();
            SQLHelper.ExecuteNonQuery("insert into dbo.DeleteLog (  DeletedDate, RefNo, ReferenceTable, EmpNo, Narration1, Naration2, Naration3, DeletedBy) SELECT GETDATE() AS Expr1, AutoKey, 'DailyHarvest' AS Expr2, DivisionID, EmpNo, DateEntered, WorkCodeID, '"+FTSPayRollBL.User.StrUserName+"' AS Expr3 FROM dbo.DailyGroundTransactions WHERE (AutoKey = '" + IntHatvestEntryId + "')", CommandType.Text);
            cmd = SQLHelper.CreateCommand("spDeleteDailyHarvest", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public String DeleteHarvetEntryBlockPlk()
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
            SQLHelper.ExecuteNonQuery("insert into dbo.DeleteLog (  DeletedDate, RefNo, ReferenceTable, EmpNo, Narration1, Naration2, Naration3, DeletedBy) SELECT GETDATE() AS Expr1, AutoKey, 'DailyHarvest' AS Expr2, DivisionID, EmpNo, DateEntered, WorkCodeID, '" + FTSPayRollBL.User.StrUserName + "' AS Expr3 FROM dbo.DailyGroundTransactions WHERE (AutoKey = '" + IntHatvestEntryId + "')", CommandType.Text);
            cmd = SQLHelper.CreateCommand("spDeleteDailyHarvestBlockPlk", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }
        public String DeleteHarvetContractCWEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@harvestId", SqlDbType.Int);
            param.Value = IntHatvestEntryId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = DtHarvestDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fieldId", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            SQLHelper.ExecuteNonQuery("insert into dbo.DeleteLog (  DeletedDate, RefNo, ReferenceTable, EmpNo, Narration1, Naration2, Naration3, DeletedBy) SELECT GETDATE() AS Expr1, AutoKey, 'DailyHarvest' AS Expr2, DivisionID, EmpNo, DateEntered, WorkCodeID, '" + FTSPayRollBL.User.StrUserName + "' AS Expr3 FROM dbo.DailyGroundTransactions WHERE (AutoKey = '" + IntHatvestEntryId + "')", CommandType.Text);
            cmd = SQLHelper.CreateCommand("spDeleteDailyHarvestContactCW", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public DataTable ListHarvestEntries(DateTime HDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Date"));
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

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT AutoKey, DateEntered, EmpNo, HolidayYesNo, LabourType, CropType, WorkType, DivisionID, FieldID, WorkCodeID, TaskCompleted, FullHalf, WorkQty,LabourEstate,LabourDivision,LabourField FROM DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '"+HDate+"', 102))", CommandType.Text);

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
                    dtrow[3] = dataReader.GetBoolean(3);
                }
                //LabourType
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                //crop type
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetInt32(5);
                }
                //worktype
                 if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetInt32(6);
                }
                //divisionid
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
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
                    dtrow[10] = dataReader.GetBoolean(10);
                }
                //full half
                 if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetInt32(11);
                }
                //workqty
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetString(13).Trim();
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetString(14).Trim();
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15).Trim();
                }
                
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public String CloseDayEntries(String StrDiv,DateTime dtDate)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@HarvestDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtDate.ToShortDateString()); 
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDiv;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spCloseDailyHarvestEntries", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            
        }

        public DataTable ListHarvestEntriesForDivision(DateTime HDate, String DivisionID, Int32 RefNo, String EmpNo, Int32 WorkType)
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
            dt.Columns.Add(new DataColumn("WorkCode"));//10
            dt.Columns.Add(new DataColumn("TaskCompleted"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("LabourEstate"));
            dt.Columns.Add(new DataColumn("LabourDivision"));
            dt.Columns.Add(new DataColumn("LabourField"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("IsContractor"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("ContractorRate"));//20
            dt.Columns.Add(new DataColumn("NormKilos"));
            dt.Columns.Add(new DataColumn("SundryTaskCompleted"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.EstateID, dbo.DailyGroundTransactions.EmpNo, dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.LabourEstate, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.IsContract, dbo.DailyGroundTransactions.Contractor, dbo.DailyGroundTransactions.ContractorRate FROM dbo.DailyGroundTransactions  WHERE (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102) AND (DivisionID = '" + DivisionID + "') AND (AutoKey = '" + RefNo + "'))", CommandType.Text);

            /*Changed for BPL*/
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.EstateID, dbo.DailyGroundTransactions.EmpNo, dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.LabourEstate, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.IsContract, dbo.DailyGroundTransactions.Contractor, dbo.DailyGroundTransactions.ContractorRate,NormKilos,SundryTaskCompleted FROM dbo.DailyGroundTransactions  WHERE (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + EmpNo + "') AND (WorkType='" + WorkType + "') AND (dbo.DailyGroundTransactions.AutoKey='" + RefNo + "'))", CommandType.Text);

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
                    dtrow[18] = dataReader.GetBoolean(18);
                }
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19);
                }
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetDecimal(20);
                }
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetDecimal(21);
                }
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetDecimal(22);
                }
                             

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet getNorm()
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'Norm') AND (EmpType = 'All')", CommandType.Text);
            return ds;
        }

        public DataSet getDivisionLatestNorm()
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'Norm') AND (EmpType = 'All')", CommandType.Text);
            return ds;
        }

        //
        public DataTable ListHarvestForDivision(DateTime HDate, String DivisionID,Int32 workType,Int32 intIsContract,Boolean boolCWOkg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Over kilos"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Qty1#"));
            dt.Columns.Add(new DataColumn("Qty2"));
            dt.Columns.Add(new DataColumn("Qty3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("BlockPlk2013"));
            dt.Columns.Add(new DataColumn("DailyBasicAmt"));
            dt.Columns.Add(new DataColumn("MusterChitNumber"));
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("MainACCode"));
            
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision AS LentDivision, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID,  dbo.DailyGroundTransactions.AutoKey,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight,dbo.DailyGroundTransactions.BlockPlk2013,dbo.DailyGroundTransactions.DailyBasicAmount,dbo.DailyGroundTransactions.MusterChitNumber,dbo.DailyGroundTransactions.GangNumber,dbo.DailyGroundTransactions.SubCategoryCode FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (CropType=1) AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "')AND (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('ABS')) AND (WorkType='" + workType + "') AND  (dbo.DailyGroundTransactions.IsContract = '" + intIsContract + "') AND   (CashPlkOkgYesNo = '" + boolCWOkg + "') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

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
                //Mandays
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                //field
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }               
                //Ref
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetBoolean(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetString(17).Trim();
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetString(18).Trim();
                }
                
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListHarvestForDivision(DateTime HDate, String DivisionID, Int32 workType, Int32 intIsContract, String EmpNo,Boolean boolCWOkg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Over kilos"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Qty1#"));
            dt.Columns.Add(new DataColumn("Qty2"));
            dt.Columns.Add(new DataColumn("Qty3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("BlockPlk2013"));
            dt.Columns.Add(new DataColumn("DailyBasicAmt"));
            dt.Columns.Add(new DataColumn("MusterChitNumber"));
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("MainACCode"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision AS LentDivision, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID,  dbo.DailyGroundTransactions.AutoKey,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight,dbo.DailyGroundTransactions.BlockPlk2013,dbo.DailyGroundTransactions.DailyBasicAmount,dbo.DailyGroundTransactions.MusterChitNumber,dbo.DailyGroundTransactions.GangNumber,dbo.DailyGroundTransactions.SubCategoryCode FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (CropType=1) and (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "')AND (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('ABS')) AND (WorkType='" + workType + "') AND  (dbo.DailyGroundTransactions.IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND (dbo.DailyGroundTransactions.EmpNo LIKE '" + EmpNo + "') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

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
                //Mandays
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                //field
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }
                //Ref
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetBoolean(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetString(17).Trim();
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetString(18).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        //Area Covered and Field Weight
        public DataTable ListAreaCoveredAndFieldWeight(String strDiv , DateTime dtDate)
        {            
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("RefNo"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("WorkType"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, FieldID, SUM(AreaCovered) AS AreaCovered, SUM(FieldWeight) AS FieldWeight FROM dbo.DailyGroundTransactions where     (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102))  AND (DivisionID = '" + strDiv + "') AND (WorkCodeID NOT IN ('PLK','TAP','OHV')) GROUP BY DateEntered, DivisionID, FieldID  ORDER BY DateEntered, FieldID", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT DateEntered, AutoKey, FieldID, WorkCodeID,WorkType, EmpNo, SUM(AreaCovered) AS AreaCovered FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (WorkCodeID NOT IN ('PLK', 'TAP', 'OHV')) GROUP BY DateEntered, AutoKey, FieldID, WorkCodeID, WorkType, EmpNo ORDER BY DateEntered, FieldID, WorkCodeID", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        //Area Covered and Field Weight Day wise
        public DataTable ListAreaCoveredAndFieldWeight1(String strDiv,String strfield,DateTime dtDate)
        {
           
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("WorkType"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("FieldId"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT AutoKey AS Ref, DateEntered, WorkType, EmpNo, FieldID, WorkCodeID, AreaCovered, FieldWeight FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strfield + "') AND (WorkCodeID <> 'PLK') ORDER BY DateEntered, FieldID", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetDateTime(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDecimal(7);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void UpdateAreaCoveredAndFieldWeight(Int32 intAutoKey, DateTime dtDate,String strDiv, String empNo, String WorkCode, Int32 intWorkType,Decimal decArea)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), '" + intAutoKey + "' , 'DailyGroundTransactions' ,'" + strDiv + "', '" + empNo + "',  '" + WorkCode + "', '" + decArea.ToString() + "', 0,  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE dbo.DailyGroundTransactions set AreaCovered='" + decArea + "' WHERE (AutoKey = '" + intAutoKey + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkType = '" + intWorkType + "') AND (EmpNo = '" + empNo + "') AND (WorkCodeID = '" + WorkCode + "') AND (DivisionID = '" + strDiv + "')", CommandType.Text);
        }

        

        //contractor Daily Harvest
        public DataTable ListContractHarvestForDivision(DateTime HDate, String DivisionID, Int32 workType,Int32 intIsContract)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Over kilos"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Qty1#"));
            dt.Columns.Add(new DataColumn("Qty2"));
            dt.Columns.Add(new DataColumn("Qty3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("IsContractor"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("ContractorRate"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision AS LentDivision,  dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.WorkQty1,  dbo.DailyGroundTransactions.WorkQty2, dbo.DailyGroundTransactions.WorkQty3, dbo.DailyGroundTransactions.AreaCovered,  dbo.DailyGroundTransactions.FieldWeight, dbo.DailyGroundTransactions.IsContract, dbo.DailyGroundTransactions.Contractor, dbo.DailyGroundTransactions.ContractorRate FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "') AND  (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('ABS')) AND (dbo.DailyGroundTransactions.WorkType = '" + workType + "') AND  (dbo.DailyGroundTransactions.IsContract = '" + intIsContract + "')  AND (dbo.DailyGroundTransactions.Contractor <> 'Contractor') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

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
                //Mandays
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                //field
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }
                //Ref
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetBoolean(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetDecimal(16);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        //cash work
        public DataTable ListCashWorkHarvestForDivision(DateTime HDate, String DivisionID, Int32 workType,Int32 intIsContract)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Over kilos"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("Field"));
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Qty1#"));
            dt.Columns.Add(new DataColumn("Qty2"));
            dt.Columns.Add(new DataColumn("Qty3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("IsContractor"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("ContractorRate"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision AS LentDivision,  dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.AutoKey, dbo.DailyGroundTransactions.WorkQty1,  dbo.DailyGroundTransactions.WorkQty2, dbo.DailyGroundTransactions.WorkQty3, dbo.DailyGroundTransactions.AreaCovered,  dbo.DailyGroundTransactions.FieldWeight, dbo.DailyGroundTransactions.IsContract, dbo.DailyGroundTransactions.Contractor, dbo.DailyGroundTransactions.ContractorRate FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "') AND  (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('ABS')) AND (dbo.DailyGroundTransactions.WorkType = '" + workType + "') AND  (dbo.DailyGroundTransactions.IsContract = '" + intIsContract + "') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

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
                //Mandays
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                //field
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7).Trim();
                }
                //Ref
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetBoolean(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetDecimal(16);
                }



                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListPHHarvestForDivision(DateTime HDate, String DivisionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EMPName"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Over kilos"));
            dt.Columns.Add(new DataColumn("Full/Half"));
            dt.Columns.Add(new DataColumn("Task completed"));
            dt.Columns.Add(new DataColumn("Ref#"));
            dt.Columns.Add(new DataColumn("Qty1#"));
            dt.Columns.Add(new DataColumn("Qty2"));
            dt.Columns.Add(new DataColumn("Qty3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.AutoKey FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "') AND (WorkType=1) AND (WorkCodeID='PH') ORDER BY dbo.DailyGroundTransactions.AutoKey DESC", CommandType.Text);

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
                //Quantity
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                //Overkilos
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetDecimal(3);
                }
                //Full/Half
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                //Taskcompleted
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetBoolean(5);
                }
                //Ref
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetInt32(6);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        //public DataTable ListHarvestEntriesForDivision1(DateTime HDate, String DivisionID, Int32 RefNo)
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add(new DataColumn("EstateID"));
        //    dt.Columns.Add(new DataColumn("Date"));
        //    dt.Columns.Add(new DataColumn("CropType"));
        //    dt.Columns.Add(new DataColumn("WorkType"));
        //    dt.Columns.Add(new DataColumn("Division"));
        //    dt.Columns.Add(new DataColumn("Field"));
        //    dt.Columns.Add(new DataColumn("ManDays"));
        //    dt.Columns.Add(new DataColumn("HolidayYesNo"));
        //    dt.Columns.Add(new DataColumn("EmpCategory"));
        //    dt.Columns.Add(new DataColumn("LabourEstate"));
        //    dt.Columns.Add(new DataColumn("LabourDivision"));
        //    dt.Columns.Add(new DataColumn("LabourField"));

        //    DataRow dtrow;
        //    SqlDataReader dataReader;
        //    dtrow = dt.NewRow();
        //    dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EstateID, dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.CropType, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.LabourEstate, dbo.DailyGroundTransactions.LabourDivision, dbo.DailyGroundTransactions.LabourField, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.HolidayYesNo FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "') AND (dbo.DailyGroundTransactions.AutoKey = '" + RefNo + "')", CommandType.Text);

        //    while (dataReader.Read())
        //    {
        //        dtrow = dt.NewRow();
        //        //ref#
        //        if (!dataReader.IsDBNull(0))
        //        {
        //            dtrow[0] = dataReader.GetInt32(0);
        //        }
        //        //date entered
        //        if (!dataReader.IsDBNull(1))
        //        {
        //            dtrow[1] = dataReader.GetDateTime(1);
        //        }
        //        //empno
        //        if (!dataReader.IsDBNull(2))
        //        {
        //            dtrow[2] = dataReader.GetString(2).Trim();
        //        }
        //        //holidayYesNo
        //        if (!dataReader.IsDBNull(3))
        //        {
        //            dtrow[3] = dataReader.GetBoolean(3);
        //        }
        //        //LabourType
        //        if (!dataReader.IsDBNull(4))
        //        {
        //            dtrow[4] = dataReader.GetString(4).Trim();
        //        }
        //        //crop type
        //        if (!dataReader.IsDBNull(5))
        //        {
        //            dtrow[5] = dataReader.GetInt32(5);
        //        }
        //        //worktype
        //        if (!dataReader.IsDBNull(6))
        //        {
        //            dtrow[6] = dataReader.GetInt32(6);
        //        }
        //        //divisionid
        //        if (!dataReader.IsDBNull(7))
        //        {
        //            dtrow[7] = dataReader.GetString(7).Trim();
        //        }
        //        //fieldid
        //        if (!dataReader.IsDBNull(8))
        //        {
        //            dtrow[8] = dataReader.GetString(8).Trim();
        //        }
        //        //work code id
        //        if (!dataReader.IsDBNull(9))
        //        {
        //            dtrow[9] = dataReader.GetString(9).Trim();
        //        }
        //        //task completed
        //        if (!dataReader.IsDBNull(10))
        //        {
        //            dtrow[10] = dataReader.GetBoolean(10);
        //        }
        //        //full half
        //        if (!dataReader.IsDBNull(11))
        //        {
        //            dtrow[11] = dataReader.GetInt32(11);
        //        }
        //        //workqty
        //        if (!dataReader.IsDBNull(12))
        //        {
        //            dtrow[12] = dataReader.GetDecimal(12);
        //        }
        //        if (!dataReader.IsDBNull(13))
        //        {
        //            dtrow[13] = dataReader.GetString(13).Trim();
        //        }
        //        if (!dataReader.IsDBNull(14))
        //        {
        //            dtrow[14] = dataReader.GetString(14).Trim();
        //        }
        //        if (!dataReader.IsDBNull(15))
        //        {
        //            dtrow[15] = dataReader.GetString(15).Trim();
        //        }

        //        dt.Rows.Add(dtrow);
        //    }
        //    dataReader.Close();
        //    return dt;
        //}
        /// <summary>
        /// Insert Extra Names to All Employees
        /// </summary>
        /// <returns>Status - ADDED, EXISTS</returns>
        public String InsertExtraNameToDivision()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtHarvestDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AddType", SqlDbType.VarChar, 50);
            param.Value = StrPHAddType;
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

            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddExtraNames", CommandType.StoredProcedure, paramList);
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

        public String DeleteExtraNameToDivision()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtHarvestDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteExtraNames", CommandType.StoredProcedure, paramList);
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

        

        public String CheckPreviousDayEntries(DateTime EntryDate)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@entryDate", SqlDbType.DateTime);
            param.Value =EntryDate;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPCheckPreviousDayData", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public Decimal GetEmployeeAvailableManDays(DateTime dtDate,String strDiv,String Stremp,Int32 intWorkType)
        {
            Decimal decManDays = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT SUM(ManDays) AS Expr1 FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (DivisionID = '"+strDiv+"') AND (WorkType = '"+intWorkType+"') AND (EmpNo = '"+Stremp+"')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decManDays = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decManDays;
        }

        public Decimal GetEmployeeAvailableCashManDays(DateTime dtDate, String strDiv, String Stremp, Int32 intWorkType)
        {
            Decimal decManDays = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT  SUM(CASE WHEN (WorkCodeID <> 'plk') THEN CashManDays ELSE CASE WHEN (FullHalf = 2) THEN 1 ELSE 0.5 END END) AS Mandays FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (WorkType = '" + intWorkType + "') AND (EmpNo = '" + Stremp + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decManDays = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decManDays;
        }

        public DataTable GetDaySummary(DateTime HDate, String DivisionID, Int32 workType, Int32 intIsContract, Boolean boolCWOkg)
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
            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where (CropType=1) and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('X%')) ", CommandType.Text);

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
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=1) and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('PLK')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            else if (workType == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=1) and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('PLK')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
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
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=1) and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK','TAP')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            else if (workType == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=1) and (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK','TAP')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
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

            reader = SQLHelper.ExecuteReader("SELECT     SUM(WorkQty) AS Kilos FROM dbo.DailyGroundTransactions WHERE (CropType=1) and (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (WorkType = '" + workType + "') AND (DivisionID = '" + DivisionID + "') AND (IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND  (WorkCodeID = 'PLK') ", CommandType.Text);

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

            reader = SQLHelper.ExecuteReader("SELECT     SUM(OverKgs) AS Kilos FROM dbo.DailyGroundTransactions WHERE (CropType=1) and (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (WorkType = '" + workType + "') AND (DivisionID = '" + DivisionID + "') AND (IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND  (WorkCodeID = 'PLK') ", CommandType.Text);

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

        public DataTable GetDaySummary_Rubber(DateTime HDate, String DivisionID, Int32 workType, Int32 intIsContract, Boolean boolCWOkg)
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
            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('X%') and (CropType = 2)) ", CommandType.Text);

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
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS TAP FROM dbo.DailyGroundTransactions where (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('TAP')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            else if (workType == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS TAP FROM dbo.DailyGroundTransactions where (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID  like ('TAP')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
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
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK','TAP')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
            }
            else if (workType == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (DivisionID = '" + strDivision + "') AND (WorkType = '" + workType + "') AND (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK','TAP')) AND (CashPlkOkgYesNo = '" + boolCWOkg + "') ", CommandType.Text);
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

            reader = SQLHelper.ExecuteReader("SELECT     SUM(WorkQty) AS Kilos FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (WorkType = '" + workType + "') AND (DivisionID = '" + DivisionID + "') AND (IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND  (WorkCodeID = 'TAP') ", CommandType.Text);

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

            reader = SQLHelper.ExecuteReader("SELECT     SUM(OverKgs) AS Kilos FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + HDate + "', 102)) AND (WorkType = '" + workType + "') AND (DivisionID = '" + DivisionID + "') AND (IsContract = '" + intIsContract + "') AND (CashPlkOkgYesNo = '" + boolCWOkg + "') AND  (WorkCodeID = 'TAP') ", CommandType.Text);

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

        public void DeleteAllPH(DateTime dtDate, String strDiv)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM dbo.DailyGroundTransactions WHERE     (DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (DivisionID = '"+strDiv+"') AND (WorkCodeID = 'PH')", CommandType.Text);
        
        }


        public Decimal GetRubberEmployeeAvailableManDays(DateTime dtDate, String strDiv, String Stremp, Int32 intWorkType)
        {
            Decimal decManDays = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT SUM(ManDays) AS Expr1 FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (WorkType = '" + intWorkType + "') AND (EmpNo = '" + Stremp + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decManDays = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decManDays;
        }

        public Decimal GetRubberEmployeeAvailableCashManDays(DateTime dtDate, String strDiv, String Stremp, Int32 intWorkType)
        {
            Decimal decManDays = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT  SUM(CASE WHEN (WorkCodeID <> 'TAP') THEN CashManDays ELSE CASE WHEN (FullHalf = 2) THEN 1 ELSE 0.5 END END) AS Mandays FROM dbo.DailyGroundTransactionsRubber WHERE (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (WorkType = '" + intWorkType + "') AND (EmpNo = '" + Stremp + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    decManDays = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();
            return decManDays;
        }

        public String DeleteEmployeeNotOffered(DateTime dtFrom,DateTime dtTo)
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@DtFrom", SqlDbType.DateTime);
            param.Value = dtFrom;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DtTo", SqlDbType.DateTime);
            param.Value = dtTo;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spDeleteNotOffered", CommandType.StoredProcedure,paramList);
            statusParam = cmd.Parameters.Add("@State", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }

        public DataTable ListThisMonthPaidHolidays(Int32 YearId,Int32 MonthId)
        {
            DataTable dtPH = new DataTable();
            SqlDataReader PHreader;
            dtPH.Columns.Add(new DataColumn("PHDate"));
            dtPH.Columns.Add(new DataColumn("PHName"));
            dtPH.Columns.Add(new DataColumn("HolidayType"));
            DataRow drowPH;
            drowPH = dtPH.NewRow();
            PHreader = SQLHelper.ExecuteReader("SELECT Date, HolidayName, HolidayType FROM dbo.MonthlyHolidays WHERE     (Year = '" + YearId + "') AND (Month = '" + MonthId + "')", CommandType.Text);
            while (PHreader.Read())
            {
                drowPH = dtPH.NewRow();                
                if (PHreader.IsDBNull(0))
                {
                    drowPH[0] = PHreader.GetDateTime(0);
                }
                if (PHreader.IsDBNull(1))
                {
                    drowPH[1] = PHreader.GetString(1).Trim();
                }
                if (PHreader.IsDBNull(2))
                {
                    drowPH[2] = PHreader.GetString(2).Trim();
                }
                dtPH.Rows.Add(drowPH);
            }
            PHreader.Close();
            return dtPH;

        }

        public DataSet  GetSundryAreaCovered(DateTime dtDate,String strDiv)
        {
            DataSet dsAreaCovered = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT DateEntered, FieldID, WorkCodeID, CASE WHEN (WorkType = 1) THEN 'Normal Work' ELSE 'Cash Work' END AS WorkType, SUM(AreaCovered) AS AreaCovered, SUM(FieldWeight) AS FieldWeight FROM dbo.DailyGroundTransactions WHERE (DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (DivisionID like  '"+strDiv+"') AND (WorkCodeID NOT IN ('PLK', 'TAP', 'OHV')) GROUP BY DateEntered, FieldID, WorkCodeID, WorkType ORDER BY DateEntered, FieldID, WorkCodeID", CommandType.Text);
            return dsAreaCovered;
        }

       

    }


}
