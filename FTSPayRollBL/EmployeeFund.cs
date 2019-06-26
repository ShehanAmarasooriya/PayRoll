using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeFund
    {
        private String strCode;

        public String StrCode
        {
            get { return strCode; }
            set { strCode = value; }
        }

        private String strName;

        public String StrName
        {
            get { return strName; }
            set { strName = value; }
        }

        private String strEmployeeName;

        public String StrEmployeeName
        {
            get { return strEmployeeName; }
            set { strEmployeeName = value; }
        }

        private Boolean blActiveFund;

        public Boolean BlActiveFund
        {
            get { return blActiveFund; }
            set { blActiveFund = value; }
        }

        private String strEmployeeNo;

        public String StrEmployeeNo
        {
            get { return strEmployeeNo; }
            set { strEmployeeNo = value; }
        }
        private float flMonthlyAmt;

        public float FlMonthlyAmt
        {
            get { return flMonthlyAmt; }
            set { flMonthlyAmt = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }

        //private Int32 intEmployeeNo;

        //public Int32 IntEmployeeNo
        //{
        //    get { return intEmployeeNo; }
        //    set { intEmployeeNo = value; }
        //}

        public DataTable ListAllFunds()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("UserID");
            dt.Columns.Add("CreateDateTime");
            dt.Columns.Add("MonthlyAmt");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT FundCode, FundName, UserId, CreateDateTime,MonthlyAmount FROM EmployeeFunds", CommandType.Text);
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
                    dtRow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDateTime(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetDecimal(4);
                }
                dt.Rows.Add(dtRow);


            }
            return dt;
        }


        /// <summary>
        /// ///// 2011/04/22
        /// </summary>
        /// <returns></returns>

        public DataTable ListEmpFunds()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            //dt.Columns.Add("UserID");
            //dt.Columns.Add("CreateDateTime");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT FundId, FundCode FROM EmployeeFunds ", CommandType.Text);
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
        public String InsertEmployeeFunds()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar,50);
            param.Value = StrEmployeeNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FundName", SqlDbType.VarChar,50);
            param.Value = StrName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ActiveFund", SqlDbType.Bit);
            param.Value = BlActiveFund;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = FTSPayRollBL.User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spFundAssign", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@State", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public void UpdateEmployeeFunds()
        {
            SQLHelper.ExecuteNonQuery("UPDATE FundAssign SET EmpNo ='" + StrEmployeeNo + "', FundName ='" + StrName + "', ActiveFund ='" + BlActiveFund + "', UserID ='" + FTSPayRollBL.User.StrUserName + "' WHERE (EmpNo = '" + StrEmployeeNo + "')", CommandType.Text);
        }

        public String DeleteEmployeeFundsAssign()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar,50);
            param.Value = StrEmployeeNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FundName", SqlDbType.VarChar,50);
            param.Value = StrName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ActiveFund", SqlDbType.Bit);
            param.Value = BlActiveFund;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = FTSPayRollBL.User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteFundAssign", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@State", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            //SQLHelper.ExecuteNonQuery("DELETE FROM FundAssign WHERE (EmpNo = '" + StrEmployeeNo + "')", CommandType.Text);
        }

        public String DeleteEmployeeAllFundsAssign()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar,50);
            param.Value = StrEmployeeNo;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteAllFundAssign", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@State", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            //SQLHelper.ExecuteNonQuery("DELETE FROM FundAssign WHERE (EmpNo = '" + StrEmployeeNo + "')", CommandType.Text);
        }


        public void InsertEmpFund()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeFunds (FundCode, FundName,MonthlyAmount,UserId) VALUES('" + strCode + "','" + strName + "','" + FlMonthlyAmt + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void UpdateEmpFund()
        {
            SQLHelper.ExecuteNonQuery("UPDATE    EmployeeFunds SET FundName ='" + strName + "',MonthlyAmount='"+FlMonthlyAmt+"'  ,UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (FundCode = '" + strCode + "')", CommandType.Text);
        }

        public void DeleteEmpFund()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM EmployeeFunds WHERE (FundCode = '"+strCode+"')", CommandType.Text);
        }
    }
}
