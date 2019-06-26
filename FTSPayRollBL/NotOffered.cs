using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class NotOffered
    {
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private Int32 intEmpCategory;

        public Int32 IntEmpCategory
        {
            get { return intEmpCategory; }
            set { intEmpCategory = value; }
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
        private Int32 intNotOfferedCode;

        public Int32 IntNotOfferedCode
        {
            get { return intNotOfferedCode; }
            set { intNotOfferedCode = value; }
        }
        private DateTime dtFromDate;

        public DateTime DtFromDate
        {
            get { return dtFromDate; }
            set { dtFromDate = value; }
        }
        private DateTime dtToDate;

        public DateTime DtToDate
        {
            get { return dtToDate; }
            set { dtToDate = value; }
        }
        private Boolean boolEPF;

        public Boolean BoolEPF
        {
            get { return boolEPF; }
            set { boolEPF = value; }
        }
        private Boolean boolETF;

        public Boolean BoolETF
        {
            get { return boolETF; }
            set { boolETF = value; }
        }
        private Boolean boolNamePay;

        public Boolean BoolNamePay
        {
            get { return boolNamePay; }
            set { boolNamePay = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }
        private Int32 intNotOfferedId;

        public Int32 IntNotOfferedId
        {
            get { return intNotOfferedId; }
            set { intNotOfferedId = value; }
        }
        private Int32 intRefNo;

        public Int32 IntRefNo
        {
            get { return intRefNo; }
            set { intRefNo = value; }
        }

        public DataTable ListNotOfferedByEmployee(String Division,String EmpNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RefId");
            dt.Columns.Add("Division");
            dt.Columns.Add("EmpCategory");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("NotOfferedCode");
            dt.Columns.Add("FromDate");
            dt.Columns.Add("ToDate");
            dt.Columns.Add("EPF");
            dt.Columns.Add("ETF");
            dt.Columns.Add("NamePay");
            DataRow drow;
            SqlDataReader dataReader;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT NotOfferedId, Division, EmpCategory, EmpNo, NotOfferedCode, FromDate, ToDate, EPF, ETF, NamePay FROM dbo.CHKNotOffered WHERE     (Division = '"+Division+"') AND (EmpNo = '"+EmpNo+"')", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    drow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetDateTime(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    drow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    drow[7] = dataReader.GetBoolean(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    drow[8] = dataReader.GetBoolean(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    drow[9] = dataReader.GetBoolean(9);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        
        public String insertEmployeeNotOffered()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpCategory", SqlDbType.Int);
            param.Value = IntEmpCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NotOfferedCode", SqlDbType.Int);
            param.Value = IntNotOfferedCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = DtFromDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = DtToDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EPF", SqlDbType.Bit);
            param.Value = BoolEPF;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ETF", SqlDbType.Bit);
            param.Value = BoolETF;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NamePay", SqlDbType.Bit);
            param.Value = BoolNamePay;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spInsertNotOffered", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@State", SqlDbType.VarChar, 50);
            statusParam.Direction=ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status=statusParam.Value.ToString();
            return status;
        }

        public String UpdateEmployeeNotOffered()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@NotOfferedId", SqlDbType.Int,4);
            param.Value = IntNotOfferedId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpCategory", SqlDbType.Int);
            param.Value = IntEmpCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NotOfferedCode", SqlDbType.Int);
            param.Value = IntNotOfferedCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = DtFromDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = DtToDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EPF", SqlDbType.Bit);
            param.Value = BoolEPF;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ETF", SqlDbType.Bit);
            param.Value = BoolETF;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NamePay", SqlDbType.Bit);
            param.Value = BoolNamePay;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);

            SqlCommand cmd = SQLHelper.CreateCommand("spUpdateNotOffered", CommandType.StoredProcedure,paramList);
            statusParam = cmd.Parameters.Add("@State", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }

        




    }
}
