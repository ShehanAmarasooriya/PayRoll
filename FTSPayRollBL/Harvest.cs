using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class Harvest
    {
        private String strHarvestDate;

        public String StrHarvestDate
        {
            get { return strHarvestDate; }
            set { strHarvestDate = value; }
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
        private String strLabourType;

        public String StrLabourType
        {
            get { return strLabourType; }
            set { strLabourType = value; }
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
        private Double doubleQty;

        public Double DoubleQty
        {
            get { return doubleQty; }
            set { doubleQty = value; }
        }
        private Double doubleHours;

        public Double DoubleHours
        {
            get { return doubleHours; }
            set { doubleHours = value; }
        }
        private Int32 intHatvestEntryId;

        public Int32 IntHatvestEntryId
        {
            get { return intHatvestEntryId; }
            set { intHatvestEntryId = value; }
        }
        

        
        /// <summary>
        /// Insert Daily Harvest Entry Of an Employee
        /// </summary>
        /// <returns>Status - ADDED, EXISTS</returns>
        public String InsertHarvetEntry()
        {
            String Status="";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@harvestDate",SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(StrHarvestDate);
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workType", SqlDbType.VarChar,50);
            param.Value = StrWorkType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@division", SqlDbType.VarChar,50);
            param.Value = StrDivision ;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@field", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@block", SqlDbType.VarChar, 50);
            param.Value = StrBlock;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourType", SqlDbType.VarChar, 50);
            param.Value = StrLabourType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@cropType", SqlDbType.VarChar, 50);
            param.Value = StrCropType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empName", SqlDbType.VarChar, 50);
            param.Value = StrEmpName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@job", SqlDbType.VarChar, 50);
            param.Value = StrJob;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PRIYesNo", SqlDbType.Bit,1);
            param.Value = BoolPRIYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@taskCompleted", SqlDbType.Bit, 1);
            param.Value = BoolTaskCompletedYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fullHalf", SqlDbType.Int);
            param.Value = IntFullHalf;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@qty", SqlDbType.Decimal);
            param.Value = DoubleQty;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@hours", SqlDbType.Decimal);
            param.Value = DoubleHours;
            paramList.Add(param);

            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertDailyHarvetEntry", CommandType.StoredProcedure, paramList);
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

        /// <summary>
        /// Update Daily Harvest Entry of an Employee
        /// </summary>
        /// <returns>status - UPDATED, NOTEXISTS</returns>
        public String UpdateHarvestEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@harvestDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(StrHarvestDate);
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workType", SqlDbType.VarChar, 50);
            param.Value = StrWorkType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@field", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@block", SqlDbType.VarChar, 50);
            param.Value = StrBlock;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourType", SqlDbType.VarChar, 50);
            param.Value = StrLabourType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@cropType", SqlDbType.VarChar, 50);
            param.Value = StrCropType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@empName", SqlDbType.VarChar, 50);
            param.Value = StrEmpName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@job", SqlDbType.VarChar, 50);
            param.Value = StrJob;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@PRIYesNo", SqlDbType.Bit, 1);
            param.Value = BoolPRIYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@taskCompleted", SqlDbType.Bit, 1);
            param.Value = BoolTaskCompletedYesNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fullHalf", SqlDbType.Int);
            param.Value = IntFullHalf;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@qty", SqlDbType.Decimal);
            param.Value = DoubleQty;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@hours", SqlDbType.Decimal);
            param.Value = DoubleHours;
            paramList.Add(param);

            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spUpdateDailyHarvetEntry", CommandType.StoredProcedure, paramList);
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

        /// <summary>
        /// Delete Daily Harvest Entry of an Employee
        /// </summary>
        /// <returns>Status - DELETED, NOTEXISTS</returns>
        public String DeleteHarestEntry()
        {
            String Status="";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@EntryId", SqlDbType.Int);
            param.Value = IntHatvestEntryId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteDailyHarvetEntry", CommandType.StoredProcedure, paramList);
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
    }
}
