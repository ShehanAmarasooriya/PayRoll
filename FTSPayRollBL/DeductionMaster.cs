using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class DeductionMaster
    {
        private Int32 intSearchResult;

        public Int32 IntSearchResult
        {
            get { return intSearchResult; }
            set { intSearchResult = value; }
        }
        private String strGroupName;

        public String StrGroupName
        {
            get { return strGroupName; }
            set { strGroupName = value; }
        }
        private String strGroupShortName;

        public String StrGroupShortName
        {
            get { return strGroupShortName; }
            set { strGroupShortName = value; }
        }
        private Int32 intParentGroupCode;

        public Int32 IntParentGroupCode
        {
            get { return intParentGroupCode; }
            set { intParentGroupCode = value; }
        }
        private Int32 intPriority;

        public Int32 IntPriority
        {
            get { return intPriority; }
            set { intPriority = value; }
        }
        private Int32 intDeductGroupId;

        public Int32 IntDeductGroupId
        {
            get { return intDeductGroupId; }
            set { intDeductGroupId = value; }
        }
        private String strGroupType;

        public String StrGroupType
        {
            get { return strGroupType; }
            set { strGroupType = value; }
        }
        private String strDeductionName;

        //deduction variables
        public String StrDeductionName
        {
            get { return strDeductionName; }
            set { strDeductionName = value; }
        }
        private String strDeductionShortName;

        public String StrDeductionShortName
        {
            get { return strDeductionShortName; }
            set { strDeductionShortName = value; }
        }
        private String strDeductGroup;

        public String StrDeductGroup
        {
            get { return strDeductGroup; }
            set { strDeductGroup = value; }
        }
        private Int32 intDeductPriority;

        public Int32 IntDeductPriority
        {
            get { return intDeductPriority; }
            set { intDeductPriority = value; }
        }
        private Int32 intDeductionId;

        public Int32 IntDeductionId
        {
            get { return intDeductionId; }
            set { intDeductionId = value; }
        }
        private Int32 intDeductCategory;

        public Int32 IntDeductCategory
        {
            get { return intDeductCategory; }
            set { intDeductCategory = value; }
        }   
        private String strAccountHead;

        public String StrAccountHead
        {
            get { return strAccountHead; }
            set { strAccountHead = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }

        private Boolean bitLoan;
        public Boolean BitLoan
        {
            get { return bitLoan; }
            set { bitLoan = value; }
        }
        private Boolean bitFund;

        public Boolean BitFund
        {
            get { return bitFund; }
            set { bitFund = value; }
        }
        private Boolean bitRFT;

        public Boolean BitRFT
        {
            get { return bitRFT; }
            set { bitRFT = value; }
        }
        private Boolean boolFixed;

        public Boolean BoolFixed
        {
            get { return boolFixed; }
            set { boolFixed = value; }
        }

       //public String InsertDiductionGroup()
       // {
       //     String status = "";
       //     SqlParameter param = new SqlParameter();
       //     SqlParameter identityParam = new SqlParameter();
       //     SqlParameter statusParam = new SqlParameter();
       //     List<SqlParameter> paramList = new List<SqlParameter>();
       //     param = SQLHelper.CreateParameter("@groupName", SqlDbType.VarChar, 50);
       //     param.Value = StrGroupName;
       //     paramList.Add(param);
       //     param = SQLHelper.CreateParameter("@shortName", SqlDbType.Char,10);
       //     param.Value = StrGroupShortName;
       //     paramList.Add(param);
       //     param = SQLHelper.CreateParameter("@parentGroupCode", SqlDbType.Int,4);
       //     param.Value = IntParentGroupCode;
       //     paramList.Add(param);
       //     param = SQLHelper.CreateParameter("@priority", SqlDbType.Int, 4);
       //     param.Value = IntPriority;
       //     paramList.Add(param);
       //     param = SQLHelper.CreateParameter("@groupType", SqlDbType.VarChar, 50);
       //     param.Value = StrGroupType;
       //     paramList.Add(param);
       //     param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
       //     param.Value = "isuru";
       //     paramList.Add(param);
       //     SqlCommand cmd = SQLHelper.CreateCommand("spInsertDeductionGroup", CommandType.StoredProcedure,paramList);
       //     statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
       //     statusParam.Direction=ParameterDirection.Output;
       //     SQLHelper.ExecuteNonQuery(cmd);
       //     status=statusParam.Value.ToString();
       //     return status;
       // }

        //public String UpdateDiductionGroup()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter identityParam = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@deductGroupId", SqlDbType.Int, 4);
        //    param.Value = IntDeductGroupId;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@groupName", SqlDbType.VarChar, 50);
        //    param.Value = StrGroupName;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@shortName", SqlDbType.Char, 10);
        //    param.Value = StrGroupShortName;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@parentGroupCode", SqlDbType.Int, 4);
        //    param.Value = IntParentGroupCode;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@priority", SqlDbType.Int, 4);
        //    param.Value = IntPriority;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@groupType", SqlDbType.VarChar, 50);
        //    param.Value = StrGroupType;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
        //    param.Value = "isuru";
        //    paramList.Add(param);
        //    SqlCommand cmd = SQLHelper.CreateCommand("spUpdateDeductionGroup", CommandType.StoredProcedure,paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    return status;
        //}
        //public String DeleteDiductionGroup()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter identityParam = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@deductGroupId", SqlDbType.VarChar, 150);
        //    param.Value = IntDeductGroupId;
        //    paramList.Add(param);
        //    SqlCommand cmd = SQLHelper.CreateCommand("spDeleteDeductionGroup", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    return status;
        //}
        public DataTable ListDeductionGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority,Loan,Fund, UserId, CreateDateTime, DeductionGroupCode,RFT FROM dbo.CHKDeductionGroup", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        public DataTable ListBLDeductionGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority,Loan,Fund, UserId, CreateDateTime, DeductionGroupCode,RFT FROM dbo.CHKDeductionGroup where ShortName like 'BL'", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        public DataTable ListBL_IPDeductionGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority, Loan, Fund, UserId, CreateDateTime, DeductionGroupCode, RFT FROM dbo.CHKDeductionGroup WHERE (ShortName IN ('BL', 'IP'))", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        public DataTable ListRFTDeductionGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority,Loan,Fund, UserId, CreateDateTime, DeductionGroupCode,RFT FROM dbo.CHKDeductionGroup where (RFT=1)", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable ListFixedDeductionGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority,Loan,Fund, UserId, CreateDateTime, DeductionGroupCode,RFT FROM dbo.CHKDeductionGroup where (RFT=0) and (Loan=0) and (GroupName<>'EPF') and (ShortName<>'US')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable ListFixedDeductionGroupsWithOutUS()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority,Loan,Fund, UserId, CreateDateTime, DeductionGroupCode,RFT FROM dbo.CHKDeductionGroup where (RFT=0) and (Loan=0) and (ShortName not in ('E.P.F','US'))", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        /*Deduction master*/
        //public String InsertDiduction()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter identityParam = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@deductName", SqlDbType.VarChar, 50);
        //    param.Value = StrDeductionName;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductShortName", SqlDbType.VarChar, 50);
        //    param.Value = StrDeductionShortName;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductGroup", SqlDbType.Int,4);
        //    param.Value = int.Parse(StrDeductGroup);
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductPriority", SqlDbType.Int, 4);
        //    param.Value = IntDeductPriority;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductCategory", SqlDbType.VarChar, 50);
        //    param.Value = IntDeductCategory;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@accountCode", SqlDbType.VarChar, 50);
        //    param.Value = StrAccountHead;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
        //    param.Value = "isuru";//StrUserId;
        //    paramList.Add(param);
        //    SqlCommand cmd = SQLHelper.CreateCommand("spInsertDeductions", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    return status;
        //}
        /// <summary>
        /// ///////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        //public String UpdateDiduction()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter identityParam = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@DeductId", SqlDbType.Int,4);
        //    param.Value = IntDeductionId;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductName", SqlDbType.VarChar, 50);
        //    param.Value = StrDeductionName;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductShortName", SqlDbType.VarChar, 50);
        //    param.Value = StrDeductionShortName;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductGroup", SqlDbType.Int, 4);
        //    param.Value = StrDeductGroup;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductPriority", SqlDbType.Int, 4);
        //    param.Value = IntDeductPriority;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@deductCategory", SqlDbType.VarChar, 50);
        //    param.Value = IntDeductCategory;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@accountCode", SqlDbType.VarChar, 50);
        //    param.Value = StrAccountHead;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
        //    param.Value = "isuru";//StrUserId;
        //    paramList.Add(param);
        //    SqlCommand cmd = SQLHelper.CreateCommand("spUpdateDeduction", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    return status;
        //}
        //public String DeleteDiduction()
        //{
        //    String status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter identityParam = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@DeductId", SqlDbType.Int, 4);
        //    param.Value = IntDeductionId;
        //    paramList.Add(param);
        //    SqlCommand cmd = SQLHelper.CreateCommand("spDeleteDeduction", CommandType.StoredProcedure);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    status = statusParam.Value.ToString();
        //    return status;
        //}
        public DataTable ListAllDeductionMasters()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DiductionName");
            dt.Columns.Add("ShortName");
            dt.Columns.Add("Group");
            dt.Columns.Add("Priority");
            dt.Columns.Add("AccountCode");
            dt.Columns.Add("UserID");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionId");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT DeductionName, ShortName,DeductionGroupCode, Priority, AccountCode, UserId, CreateDateTime,DeductionCode FROM CHKDeduction", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
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
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                } 
              dt.Rows.Add(dtRow);
            }
            return dt;
        }
        public DataTable ListAllDeductionMasters(int DedGroup)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DiductionName");
            dt.Columns.Add("ShortName");
            dt.Columns.Add("Group");
            dt.Columns.Add("Priority");
            dt.Columns.Add("AccountCode");
            dt.Columns.Add("UserID");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionId");
            dt.Columns.Add("DeductUntilStop");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT DeductionName, ShortName,DeductionGroupCode, Priority, AccountCode, UserId, CreateDateTime,DeductionCode,DeductUntilStop FROM CHKDeduction WHERE     (DeductionGroupCode = '" + DedGroup + "')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
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
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public String GetLoanName(String DeductId)
        {
            String LoanName = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT DeductionName FROM dbo.CHKDeduction WHERE (DeductionCode = '" + DeductId + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    LoanName = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return LoanName;
        }

        public void InsertDeductionGroup()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKDeductionGroup (GroupName, ShortName, Priority,Loan, UserId,Fund,RFT) VALUES ('" + StrGroupName + "','" + strGroupShortName + "','" + intPriority + "','" + bitLoan + "','" + FTSPayRollBL.User.StrUserName + "','"+BitFund+"','"+BitRFT+"')", CommandType.Text);
        }

        public void UpdateDeductionGroup()
        {
            SQLHelper.ExecuteNonQuery("UPDATE    CHKDeductionGroup SET Priority ='" + intPriority + "',Loan='" + BitLoan + "', UserId ='" + FTSPayRollBL.User.StrUserName + "',Fund='"+BitFund+"',RFT='"+BitRFT+"'  WHERE (GroupName = '" + StrGroupName + "') AND (ShortName = '" + strGroupShortName + "')", CommandType.Text);
        }

        public void DeleteDeductionGroup()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKDeductionGroup WHERE (GroupName = '" + StrGroupName + "') AND (ShortName = '" + strGroupShortName + "')", CommandType.Text);
        }

        public DataTable getDeductionGroup()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupCode");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, DeductionGroupCode,Loan,Fund FROM CHKDeductionGroup ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetBoolean(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                          
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable getDeductionGroupWithoutFAMAFSTCO()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupCode");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, DeductionGroupCode,Loan,Fund FROM CHKDeductionGroup WHERE     (NOT (ShortName IN ('FA', 'MA', 'FS', 'TCO', 'US', 'PD', 'E.P.F'))) AND (Loan = 0) AND (RFT = 0) ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetBoolean(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable getLoanDeductionGroup()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupCode");
            dt.Columns.Add("ShortName");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, DeductionGroupCode,ShortName FROM CHKDeductionGroup WHERE (Loan = 1)", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetString(2).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        public DataTable getFundDeductionGroup()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupCode");
            dt.Columns.Add("LoanYesNo");
            dt.Columns.Add("FundYesNo");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, DeductionGroupCode,Loan,Fund FROM CHKDeductionGroup WHERE (GroupName = 'Funds')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetBoolean(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }


                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable getFundDeductionGroup(int GropId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupCode");
            dt.Columns.Add("LoanYesNo");
            dt.Columns.Add("FundYesNo");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, DeductionGroupCode,Loan,Fund FROM CHKDeductionGroup WHERE (DeductionGroupCode = '"+GropId+"')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetBoolean(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }


                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public void InsertDeductionMaster()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKDeduction  (DeductionGroupCode, DeductionName, ShortName, Priority, AccountCode, UserId) VALUES ('" + IntDeductGroupId + "','" + strDeductionName + "','" + strDeductionShortName + "','" + intPriority + "','" + strAccountHead + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

       
        /*update deduction group also*/
        //public void UpdateDeductionMaster()
        //{
        //    SQLHelper.ExecuteNonQuery("UPDATE CHKDeduction SET ShortName ='" + strDeductionShortName + "', Priority ='" + intPriority + "', AccountCode ='" + strAccountHead + "', UserId ='" + FTSPayRollBL.User.StrUserName + "', DeductionGroupCode ='" + IntDeductGroupId + "' WHERE (DeductionCode = '" + IntDeductionId + "') ", CommandType.Text);
        //}
         
        /*Update with out deduction group*/
        public void UpdateDeductionMaster()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[UpdatedUser],[Narration5])  SELECT     GETDATE() AS Expr1, 0 AS Expr2, 'DeductMaster' AS Expr3, 'NA' AS Expr5, 'NA' AS Expr6, dbo.CHKDeductionGroup.ShortName,  dbo.CHKDeduction.ShortName AS Expr11, dbo.CHKDeduction.DeductionName, '" + FTSPayRollBL.User.StrUserName + "' AS Expr4,'Updated' FROM         dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode   WHERE (dbo.CHKDeduction.DeductionCode = '" + IntDeductionId + "') AND (dbo.CHKDeduction.DeductionGroupCode ='" + IntDeductGroupId + "') ", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("UPDATE CHKDeduction SET ShortName ='" + strDeductionShortName + "', Priority ='" + intPriority + "', AccountCode ='" + strAccountHead + "', UserId ='" + FTSPayRollBL.User.StrUserName + "'  WHERE (DeductionCode = '" + IntDeductionId + "') AND (DeductionGroupCode ='" + IntDeductGroupId + "') ", CommandType.Text);
        }

        public void DeleteDeductionMaster()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[UpdatedUser],[Narration5])  SELECT     GETDATE() AS Expr1, 0 AS Expr2, 'DeductMaster' AS Expr3, 'NA' AS Expr5, 'NA' AS Expr6, dbo.CHKDeductionGroup.ShortName,  dbo.CHKDeduction.ShortName AS Expr11, dbo.CHKDeduction.DeductionName, '" + FTSPayRollBL.User.StrUserName + "' AS Expr4,'Deleted' FROM         dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode   WHERE (dbo.CHKDeduction.DeductionCode = '" + IntDeductionId + "') AND (dbo.CHKDeduction.DeductionGroupCode ='" + IntDeductGroupId + "') ", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKDeduction where (DeductionCode = '" + IntDeductionId + "')", CommandType.Text);
        }

        public DataTable getDeduction()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductionName");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("DeductShortName");
            
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT DeductionName, DeductionCode,ShortName FROM CHKDeduction WHERE (DeductionGroupCode = '" + IntDeductGroupId + "')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetString(2).Trim();
                }

               
                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        

        public DataTable ListDeduction(Int32 groupId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("DeductShortName");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT DeductionCode, ShortName FROM dbo.CHKDeduction WHERE (DeductionGroupCode = '"+groupId+"')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetInt32(0);
                }               
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        ////////////////////////// 2011/04/26 ///////////////////////

        public DataTable ListDeducCode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("DeductShortName");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT DeductionCode, ShortName FROM dbo.CHKDeduction", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable ListUnionDeductionGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("DeductionGroupId");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE (GroupName='Union Subscription')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }
       

        //public DataTable getDeductionGroup()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("GroupName");
        //    dt.Columns.Add("GroupCode");

        //    DataRow dtRow;
        //    SqlDataReader reader;
        //    dtRow = dt.NewRow();

        //    reader = SQLHelper.ExecuteReader("SELECT GroupName, DeductionGroupCode FROM CHKDeductionGroup", CommandType.Text);
        //    while (reader.Read())
        //    {
        //        dtRow = dt.NewRow();
        //        if (!reader.IsDBNull(0))
        //        {
        //            dtRow[0] = reader.GetString(0).Trim();
        //        }
        //        if (!reader.IsDBNull(1))
        //        {
        //            dtRow[1] = reader.GetInt32(1);
        //        }

        //        dt.Rows.Add(dtRow);
        //    }
        //    return dt;
        //}

        public Boolean IsFixedEntriesAvailable(Int32 intDeductId)
        {
            Boolean boolIsAvail = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT  StartYear, StartMonth, DivisionId, EmpNo FROM dbo.CHKFixedDeductions WHERE (BalanceAmount > 0) AND (CloseYesNo = 0) AND (DeductionId = '" + intDeductId + "')",CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolIsAvail = true;
            }
            else
            {
                boolIsAvail = false;
            }
            return boolIsAvail;
        }

        public Boolean IsLoanEntriesAvailable(Int32 intDeductId)
        {
            Boolean boolIsAvail = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     DeductionCode, BalanceAmount, ClosedYesNo FROM dbo.CHKLoan WHERE (ClosedYesNo = 0) AND (BalanceAmount > 0) AND (DeductionCode = '"+intDeductId+"')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolIsAvail = true;
            }
            else
            {
                boolIsAvail = false;
            }
            return boolIsAvail;
        }

        public Boolean IsLoanDeductionGroup(Int32 groupid)
        {
            Boolean boolIsLoan = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT DeductionGroupCode, ShortName, Loan, Fund, RFT, Fixed FROM dbo.CHKDeductionGroup WHERE (Loan = 1) AND (DeductionGroupCode = '"+groupid+"')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolIsLoan = true;
            }
            else
            {
                boolIsLoan = false;
            }
            return boolIsLoan;
        }

        public String GetDeductionNameByID(Int32 intDeductId)
        {
            String DeductName = "";
            SqlDataReader reader = SQLHelper.ExecuteReader("SELECT DeductionName FROM dbo.CHKDeduction WHERE (DeductionCode = '" + intDeductId + "')", CommandType.Text);
            while (reader.Read())
            {
                DeductName = reader.GetString(0).Trim();
            }
            reader.Close();
            return DeductName;
        }

       

        public DataTable ListAllDeductionGroupsWithout_US_EPF_SD()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupShortName");
            dt.Columns.Add("Priority");
            dt.Columns.Add("Loan");
            dt.Columns.Add("Fund");
            dt.Columns.Add("UserId");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("DeductionGroupId");
            dt.Columns.Add("RFT");
            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT GroupName, ShortName, Priority,Loan,Fund, UserId, CreateDateTime, DeductionGroupCode,RFT FROM dbo.CHKDeductionGroup where  (GroupName<>'EPF') and (ShortName not in ('US','SD'))", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetInt32(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetBoolean(8);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable getAllDeductionGroupsExcept_EPF_US_SD(int GropId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductGroupName");
            dt.Columns.Add("DeductGroupCode");
            dt.Columns.Add("LoanYesNo");
            dt.Columns.Add("FundYesNo");
            dt.Columns.Add("RFT");
            dt.Columns.Add("Fixed");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT     GroupName, DeductionGroupCode, Loan, Fund, RFT, Fixed FROM dbo.CHKDeductionGroup WHERE (DeductionGroupCode = '" + GropId + "')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetBoolean(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetBoolean(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetBoolean(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetBoolean(5);
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable GetDeductionDetailsByID(Int32 intDeductId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeductName");
            dt.Columns.Add("DeductUntilStop");
            DataRow dtRow;
            dtRow = dt.NewRow();
            SqlDataReader reader = SQLHelper.ExecuteReader("SELECT     DeductionName, DeductUntilStop FROM         dbo.CHKDeduction WHERE (DeductionCode = '" + intDeductId + "')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                else
                    dtRow[0] = "NA";
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetBoolean(1);
                }
                else
                    dtRow[1] = false;
                dt.Rows.Add(dtRow);
            }
            reader.Close();
            return dt;
        }

        public DataSet GetFoodStuffRates()
        {
            DataSet dsRates = SQLHelper.FillDataSet("SELECT dbo.CHKDeduction.DeductionCode,dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS Expr1, dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.Rate FROM            dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode WHERE        (dbo.CHKDeductionGroup.ShortName IN ('FS', 'TCO'))", CommandType.Text);
            return dsRates;
        }
        public void SaveFoodStuffRates(Int32 intDeductCode, Decimal decRate)
        {
            SQLHelper.ExecuteNonQuery("UPDATE CHKDeduction SET Rate='" + decRate + "' WHERE (DeductionCode='" + intDeductCode + "')", CommandType.Text);
        }
        public Decimal GetFoodStuffCodeRate(Int32 intID)
        {
            DataSet dsRate = SQLHelper.FillDataSet("SELECT    top 1   isnull( Rate,0) FROM  dbo.CHKDeduction WHERE        (DeductionCode = '"+intID+"')", CommandType.Text);
            Decimal decRate = Convert.ToDecimal(dsRate.Tables[0].Rows[0][0].ToString());
            return decRate;
        }


    }
}
