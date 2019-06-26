using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class BlockEntries
    {
        private DateTime dtCurrentDate;

        public DateTime DtCurrentDate
        {
            get { return dtCurrentDate; }
            set { dtCurrentDate = value; }
        }

        private DateTime dtCloseDate;

        public DateTime DtCloseDate
        {
            get { return dtCloseDate; }
            set { dtCloseDate = value; }
        }

        private Int32 intEntryYear;

        public Int32 IntEntryYear
        {
            get { return intEntryYear; }
            set { intEntryYear = value; }
        }
        private Int32 intEntryMonth;

        public Int32 IntEntryMonth
        {
            get { return intEntryMonth; }
            set { intEntryMonth = value; }
        }

        public Boolean IsBlocked(Int32 AutoKey, DateTime dtDate)
        {
            Boolean BlockedYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Blocked");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT BlockedYesNO FROM dbo.DailyGroundTransactions WHERE (BlockedYesNO = 1) AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (AutoKey = '" + AutoKey + "')", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetBoolean(0);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                BlockedYesNo = true;
            else
                BlockedYesNo = false;
            return BlockedYesNo;
        }

        public Boolean IsBlockedDate( DateTime dtDate)
        {
            Boolean BlockedDateYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Blocked");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKDates.BlockedYesNo FROM dbo.CHKDates WHERE (BlockedYesNo = 1) AND (ChkDate = CONVERT(DATETIME, '" + dtDate + "', 102))", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetBoolean(0);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                BlockedDateYesNo = true;
            else
                BlockedDateYesNo = false;
            return BlockedDateYesNo;
        }

        public Boolean IsOpenedDate(DateTime dtDate)
        {
            Boolean OpenedDateYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Blocked");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKDates.BlockedYesNo FROM dbo.CHKDates WHERE (BlockedYesNo = 1) AND (ChkDate = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (OpenedYesNo = 1)", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetBoolean(0);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                OpenedDateYesNo = true;
            else
                OpenedDateYesNo = false;
            return OpenedDateYesNo;
        }

        public String CheckDateDifference()
                {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();            
            
            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtCurrentDate.ToShortDateString());
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPDtBlock_DateDurations", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@status", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);            
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public String CheckMonthDifference()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@EntryYear", SqlDbType.Int);
            param.Value = IntEntryYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EntryMonth", SqlDbType.Int);
            param.Value = IntEntryMonth;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPDtBlock_DateDurations_ByMonth", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@status", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public void AddBlockDates()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtCurrentDate.ToShortDateString());
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPDtBlock_AddBlockedDate", CommandType.StoredProcedure, paramList);
            
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }

        public Boolean IsDateOpened()
        {
            Boolean Status = false;
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtCurrentDate.ToShortDateString());
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPDtBlock_IsDateOpened", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@status", SqlDbType.Bit);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = Convert.ToBoolean(statusParam.Value.ToString());
            cmd.Dispose();
            return Status;
        }

        public void OpenBlockedDate()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtCurrentDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CloseDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtCloseDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar,50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            //param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            //param.Value = Convert.ToDateTime(DtCurrentDate.ToShortDateString());
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPDtBlock_OpenBlockedDate", CommandType.StoredProcedure, paramList);

            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }

        public void BlockBlockedDate()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtCurrentDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);

            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPDtBlock_BlockBlockedDate", CommandType.StoredProcedure, paramList);

            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }

        public DataTable ListOpenedBlockedDates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("OpenYesNo"));
            dt.Columns.Add(new DataColumn("OpenedBy"));
            dt.Columns.Add(new DataColumn("OpenedDate"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT ChkDate, OpenedYesNo, OpenedBy, OpenedDate FROM dbo.CHKDates  ORDER BY OpenedDate DESC", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetBoolean(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetDateTime(3);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Boolean IsAllEntriesConfirmed(DateTime dtFrom,DateTime dtTo)
        
        {
            Boolean IsConfirmed = true;
            SqlDataReader dataReader;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ConfirmedYesNo"));
            dt.Columns.Add(new DataColumn("EntryDate"));
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ConfirmYesNo, EntryDate FROM dbo.CHKDateConfirmations WHERE (EntryDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetBoolean(0);
                    if (dataReader.GetBoolean(0) == false)
                    {
                        IsConfirmed = false;
                        break;
                    }
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetDateTime(1);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count == 0)
            {
                IsConfirmed = false;
            }
            
            
            
            return IsConfirmed;
        }

        public void DailyEntryConfirmation(DateTime dtDate,String UserName)
        {

            String Status = "";
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            param = SQLHelper.CreateParameter("@EntryDate", SqlDbType.DateTime);
            param.Value = dtDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = UserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPConfirmDailyEntries", CommandType.StoredProcedure, paramList);

            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKDateConfirmations] ([EntryDate] ,[ConfirmYesNo] ,[ConfirmedBy] ,[ConfirmedDate]) VALUES ('"+Convert.ToDateTime(dtDate.ToShortDateString())+"' ,1 ,'"+UserID+"' ,getdate())",CommandType.Text);
        }

        public DataSet GetConfirmationDetails(DateTime dtDate)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT EntryDate, ConfirmYesNo, ConfirmedBy, ConfirmedDate FROM dbo.CHKDateConfirmations WHERE (EntryDate BETWEEN '"+new DateTime (dtDate.Year,dtDate.Month,1)+"' AND '"+new DateTime (dtDate.Year,dtDate.Month,1).AddMonths(1).AddDays(-1)+"')", CommandType.Text);
            return ds;
        }

        public DataTable IsDailyEntryConfirmed(DateTime dtDate)
        {
            DataTable dt=new DataTable();
            dt.Columns.Add(new DataColumn("Confirmed"));
            dt.Columns.Add(new DataColumn("ConfirmedBy"));
            dt.Columns.Add(new DataColumn("ConfirmedDate"));
            Boolean boolConfirmed = false;
            SqlDataReader reader;
            DataRow drow;
            drow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT [ConfirmYesNo] ,[ConfirmedBy] ,[ConfirmedDate] FROM [dbo].[CHKDateConfirmations] WHERE ([EntryDate]='" + dtDate + "') ", CommandType.Text);
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetBoolean(0);
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetDateTime(2);
                }
                dt.Rows.Add(drow);
            }
            reader.Close();
            return dt;

        }

        public Boolean IsDayExistsInCHKDateConfirmations(DateTime dtDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Confirmed"));
            dt.Columns.Add(new DataColumn("ConfirmedBy"));
            dt.Columns.Add(new DataColumn("ConfirmedDate"));
            Boolean boolConfirmed = false;
            SqlDataReader reader;
            DataRow drow;
            drow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT [ConfirmYesNo] ,[ConfirmedBy] ,[ConfirmedDate] FROM [dbo].[CHKDateConfirmations] WHERE ([EntryDate]='" + dtDate + "') ", CommandType.Text);
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetBoolean(0);
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetDateTime(2);
                }
                dt.Rows.Add(drow);
            }
            reader.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            


        }
        public Boolean IsNotInactive(String empno, String strdiv)
        {
            Boolean boolActive = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT top 1 ActiveEmployee FROM dbo.EmployeeMaster WHERE  (DivisionID = '" + strdiv + "') AND (EmpNo = '" + empno + "') ORDER BY EPFNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    boolActive = dataReader.GetBoolean(0);
                }
            }
            dataReader.Close();
            return boolActive;
        }
        

    }
}
