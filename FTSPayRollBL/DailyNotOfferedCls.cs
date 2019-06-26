using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class DailyNotOfferedCls
    {
        private DateTime dtDate;

        public DateTime DtDate
        {
            get { return dtDate; }
            set { dtDate = value; }
        }

        private String strNOCode;

        public String StrNOCode
        {
            get { return strNOCode; }
            set { strNOCode = value; }
        }
        private String strGender;

        public String StrGender
        {
            get { return strGender; }
            set { strGender = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }

        public String InsertBulkNotOffered()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Gender", SqlDbType.VarChar,50);
            param.Value = StrGender;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NOCode", SqlDbType.VarChar, 50);
            param.Value = StrNOCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertDailyNotOffered", CommandType.StoredProcedure, paramList);
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

        public String InsertBulkNotOffered(String strEmp)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NOCode", SqlDbType.VarChar, 50);
            param.Value = StrNOCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@emp", SqlDbType.VarChar, 50);
            param.Value = strEmp;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertDailyNotOfferedEmployeeWise", CommandType.StoredProcedure, paramList);
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

        public String DeleteNotOffered()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Gender", SqlDbType.VarChar, 50);
            param.Value = StrGender;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NOCode", SqlDbType.Float);
            param.Value = StrNOCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteDailyNotOffered", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public String DeleteBulkNotOffered()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NOCode", SqlDbType.VarChar,50);
            param.Value = StrNOCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteBulkNotOfferedDivisionWise", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }
    }

    
}
