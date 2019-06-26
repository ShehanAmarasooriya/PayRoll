using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{

    public class RFTDeductions
    {
        private Int32 intRFTDeductId;

        public Int32 IntRFTDeductId
        {
            get { return intRFTDeductId; }
            set { intRFTDeductId = value; }
        }
        private Int32 intDeductionGroup;

        public Int32 IntDeductionGroup
        {
            get { return intDeductionGroup; }
            set { intDeductionGroup = value; }
        }
        private Int32 intDeduction;

        public Int32 IntDeduction
        {
            get { return intDeduction; }
            set { intDeduction = value; }
        }
        private Int32 intCategory;

        public Int32 IntCategory
        {
            get { return intCategory; }
            set { intCategory = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private Int32 intYear;

        public Int32 IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }
        private Int32 intMonth;

        public Int32 IntMonth
        {
            get { return intMonth; }
            set { intMonth = value; }
        }
        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
        }
        private String strDeductType;

        public String StrDeductType
        {
            get { return strDeductType; }
            set { strDeductType = value; }
        }
        private float flRate;

        public float FlRate
        {
            get { return flRate; }
            set { flRate = value; }
        }
        private float flQty;

        public float FlQty
        {
            get { return flQty; }
            set { flQty = value; }
        }
        private float flDeductAmount;

        public float FlDeductAmount
        {
            get { return flDeductAmount; }
            set { flDeductAmount = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }

        public DataTable ListRFTDeductions(String empNo)
        {  //RFTDeductId, DeductGroupId, DeductId, Division, Category, EmpNo, RFTRate, RFTQty, RFTDeductAmount, UserId
            DataTable dt = new DataTable();
            dt.Columns.Add("RFTDeductId");
            dt.Columns.Add("DeductGroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Division");
            dt.Columns.Add("Category");
            dt.Columns.Add("DYear");
            dt.Columns.Add("DMonth");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("RFTType");
            dt.Columns.Add("RFTRate");
            dt.Columns.Add("RFTQty");
            dt.Columns.Add("RFTDeductAmount");
            dt.Columns.Add("CreateDateTime");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT RFTDeductId, DeductGroupId, DeductId, Division, Category, DYear, DMonth, EmpNo,RFTType, RFTRate, RFTQty, RFTDeductAmount,  CreateDateTime FROM  dbo.CHKRFTDeductions WHERE  (EmpNo = '" + empNo + "') ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetInt32(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8);
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetDecimal(9);
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetDecimal(10);
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDecimal(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDateTime(12);
                } 
              dt.Rows.Add(dtRow);
            }
            return dt;
        
        }
        public DataTable ListRFTDeductions(String empNo,String strDiv)
        {  //RFTDeductId, DeductGroupId, DeductId, Division, Category, EmpNo, RFTRate, RFTQty, RFTDeductAmount, UserId
            DataTable dt = new DataTable();
            
            dt.Columns.Add("DeductGroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("DYear");
            dt.Columns.Add("DMonth");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("RFTDeductAmount");
            dt.Columns.Add("Division");
            dt.Columns.Add("RFTDeductId");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT DeductGroupId, DeductId,   DYear, DMonth, EmpNo, RFTDeductAmount,Division, RFTDeductId FROM  dbo.CHKRFTDeductions WHERE (Division='" + strDiv + "') AND (EmpNo = '" + empNo + "') ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }  
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetInt32(3);
                }               
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4);
                }               
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetString(6).Trim();
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;

        }

        public DataTable ListRFTDeductions(String strDiv,Int32 intYearVal,Int32 intMonthVal)
        {  //RFTDeductId, DeductGroupId, DeductId, Division, Category, EmpNo, RFTRate, RFTQty, RFTDeductAmount, UserId
            DataTable dt = new DataTable();

            dt.Columns.Add("EmpNo");
            dt.Columns.Add("RFTDeductAmount");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("GroupCode");
            dt.Columns.Add("DYear");
            dt.Columns.Add("DMonth");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Division");
            dt.Columns.Add("RFTDeductId");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT  dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.RFTDeductAmount, dbo.CHKDeduction.ShortName, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKDeductionGroup.DeductionGroupCode, dbo.CHKDeduction.DeductionCode, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.RFTDeductId FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKRFTDeductions.DYear = '" + intYearVal + "') AND (dbo.CHKRFTDeductions.DMonth = '" + intMonthVal + "') ORDER BY dbo.CHKRFTDeductions.RFTDeductId ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetDecimal(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetString(3).Trim();
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetInt32(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetInt32(9);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;

        }

        public DataTable ListRFTDeductions(String strDiv, Int32 intYearVal, Int32 intMonthVal,Int32 intDGroup,Int32 intDdeduct)
        {  //RFTDeductId, DeductGroupId, DeductId, Division, Category, EmpNo, RFTRate, RFTQty, RFTDeductAmount, UserId
            DataTable dt = new DataTable();

            dt.Columns.Add("EmpNo");
            dt.Columns.Add("RFTDeductAmount");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("GroupCode");
            dt.Columns.Add("DYear");
            dt.Columns.Add("DMonth");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("DeductId");
            dt.Columns.Add("Division");
            dt.Columns.Add("RFTDeductId");
            dt.Columns.Add("RFTRate");
            dt.Columns.Add("RFTQty");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT  dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.RFTDeductAmount, dbo.CHKDeduction.ShortName, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKDeductionGroup.DeductionGroupCode, dbo.CHKDeduction.DeductionCode, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.RFTDeductId, dbo.CHKRFTDeductions.RFTRate, dbo.CHKRFTDeductions.RFTQty FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKRFTDeductions.DYear = '" + intYearVal + "') AND (dbo.CHKRFTDeductions.DMonth = '" + intMonthVal + "')AND (dbo.CHKDeductionGroup.DeductionGroupCode = '" + intDGroup + "') AND (dbo.CHKDeduction.DeductionCode = '" + intDdeduct + "') ORDER BY dbo.CHKRFTDeductions.RFTDeductId ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetDecimal(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetString(3).Trim();
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetInt32(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetInt32(9);
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetDecimal(10);
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDecimal(11);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;

        }

        public String  InsertRFTDeductions()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@DeductGroupId", SqlDbType.Int);
            param.Value = IntDeductionGroup;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductId", SqlDbType.Int);
            param.Value = IntDeduction;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value =StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Category", SqlDbType.Int);
            param.Value = IntCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Year", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Month", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);            
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@RFTDeductAmount", SqlDbType.Float);
            param.Value = FlDeductAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@RFTRate", SqlDbType.Float);
            param.Value = FlRate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@RFTQty", SqlDbType.Float);
            param.Value = FlQty;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertRFTDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status; 
        }

        public String UpdateRFTDeductions()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@RFTDeductId", SqlDbType.Int);
            param.Value = intRFTDeductId ;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductGroupId", SqlDbType.Int);
            param.Value = IntDeductionGroup;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductId", SqlDbType.Int);
            param.Value = IntDeduction;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Category", SqlDbType.Int);
            param.Value = IntCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Year", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Month", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@RFTDeductAmount", SqlDbType.Float);
            param.Value = FlDeductAmount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@RFTRate", SqlDbType.Float);
            param.Value = FlRate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@RFTQty", SqlDbType.Float);
            param.Value = FlQty;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spUpdateRFTDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status; 
        }

        public String DeleteRFTDeductions()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@RFTDeductId", SqlDbType.Int);
            param.Value = intRFTDeductId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteRFTDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status; 
        }

    }
}
