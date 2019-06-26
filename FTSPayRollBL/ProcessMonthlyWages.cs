using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class ProcessMonthlyWages
    {
        private DateTime dtProcessFromDate;

        public DateTime DtProcessFromDate
        {
            get { return dtProcessFromDate; }
            set { dtProcessFromDate = value; }
        }
        private DateTime dtProcessToDate;

        public DateTime DtProcessToDate
        {
            get { return dtProcessToDate; }
            set { dtProcessToDate = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private Int32 intCategory;

        public Int32 IntCategory
        {
            get { return intCategory; }
            set { intCategory = value; }
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
        private Boolean boolProcess;

        public Boolean BoolProcess
        {
            get { return boolProcess; }
            set { boolProcess = value; }
        }
        private String strWorkCode;

        public String StrWorkCode
        {
            get { return strWorkCode; }
            set { strWorkCode = value; }
        }
        private String strUserId;

        public String StrUserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }


        public DataTable getWorkCodesAndFieldsFromDailyGroundTransactions(String strDiv, DateTime dtFrom, DateTime dtTo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FieldType"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //NIsaladataReader = SQLHelper.ExecuteReader("SELECT FieldID, WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (DivisionID = '" + strDiv + "') GROUP BY WorkCodeID, FieldID ORDER BY FieldID ASC", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.WorkCodeID, dbo.EstateField.Type FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.WorkCodeID, dbo.EstateField.Type ORDER BY dbo.DailyGroundTransactions.WorkCodeID, dbo.EstateField.Type", CommandType.Text);
            //dataReader = SQLHelper.ExecuteReader("SELECT JobShortName FROM dbo.JobMaster GROUP BY JobShortName", CommandType.Text);
            //dataReader = SQLHelper.ExecuteReader("SELECT FieldID, WorkCodeID FROM dbo.DailyGroundTransactions WHERE DivisionID = '"+ strDiv +"' AND YEAR(DateEntered) = '"+ Year +"' AND MONTH(DateEntered) = '"+ Month +"' GROUP BY FieldID, WorkCodeID", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        

        public String DeletePreviewMonthlyWegesData()
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value =Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessDeletePreviewData", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String processMonthlyWeges(String EmpNo)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProEmpNo", SqlDbType.VarChar, 50);
            param.Value = EmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spFinalProcessMonthlyWeges", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String processDeductionWeges(String EmpNo)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProEmpNo", SqlDbType.VarChar, 50);
            param.Value = EmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Process", SqlDbType.Bit);
            param.Value = BoolProcess;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessDeductMonthlyDeductions", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            //String Status = "";
            //SqlParameter param = new SqlParameter();
            //SqlParameter statusParam = new SqlParameter();
            //List<SqlParameter> paramList = new List<SqlParameter>();
            //param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            //param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            //paramList.Add(param);
            //param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            //param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            //paramList.Add(param);
            //param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            //param.Value = StrDivision;
            //paramList.Add(param);
            //param = SQLHelper.CreateParameter("@Process", SqlDbType.Bit);
            //param.Value = BoolProcess;
            //paramList.Add(param);
            //param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            //param.Value = User.StrUserName;
            //paramList.Add(param);
            //SqlCommand cmd = new SqlCommand();
            //cmd = SQLHelper.CreateCommand("spFinalProcessDeductMonthlyDeductions", CommandType.StoredProcedure, paramList);
            //statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            //statusParam.Direction = ParameterDirection.Output;
            //SQLHelper.ExecuteNonQuery(cmd);
            //Status = statusParam.Value.ToString();
            //cmd.Dispose();
            //return Status;
            // trnScope.Complete();
            //}
        }
        public String processFinalWeges(String empNo)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Process", SqlDbType.Bit);
            param.Value = BoolProcess;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProEmpNo", SqlDbType.VarChar, 50);
            param.Value = empNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessFinalMonthlyWeges", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String processGLEntries(String WorkCode)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workCode", SqlDbType.VarChar,50);
            param.Value = WorkCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessGLEntries", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            if (Status.Equals("COMPLETED"))
            {
                SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKProcessDetails] ([Division] ,[Category] ,[ProcessYear] ,[ProcessMonth] ,[PreviewYesNo] ,[ProcessedYesNo] ,[ProcessedDate] ,[CreateDateTime] ,[UserId]) VALUES ('" + StrDivision + "' ,1 ,'" + Convert.ToDateTime(DtProcessFromDate.ToShortDateString()).Year + "' ,'" + Convert.ToDateTime(DtProcessFromDate.ToShortDateString()).Month + "' ,1 ,1 ,GETDATE() ,GETDATE() ,'ADMIN' )", CommandType.Text);
            }
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public void CancelprocessGLEntries()
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@DeductYear", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeductMonth", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteProcessedGLEntries", CommandType.StoredProcedure, paramList);           
            SQLHelper.ExecuteNonQuery(cmd);
            
            cmd.Dispose();
            // trnScope.Complete();
            //}
        }

        public DataTable getWorkCodesFromDailyGroundTransactions(String strDiv,DateTime dtFrom,DateTime dtTo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("WorkCode"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT WorkCodeID FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) AND (DivisionID = '"+strDiv+"')  GROUP BY WorkCodeID", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }


        public String CancelMonthlyProcess(Int32 CancelType)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@reverseYesNo", SqlDbType.Bit, 50);
            param.Value = CancelType;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spDeletePreviewProcessData]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }


        public String CancelMonthlyProcessForEmpNO(String DivisionID,String EmpNo)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = EmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@reverseYesNo", SqlDbType.Bit, 50);
            param.Value = 0;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spDeletePreviewProcessDataForEmpNo]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public void BlockProcessEntry(String div,Int32 PYear, Int32 PMonth,String strUser,DateTime ProcessDate)
        {
            SQLHelper.ExecuteNonQuery("insert into dbo.CHKProcessDetails(Division, Category, ProcessYear, ProcessMonth, PreviewYesNo, ProcessedYesNo, ProcessedDate, CreateDateTime, UserId) values('"+div+"',11,'"+PYear+"','"+PMonth+"',1,1,'"+ProcessDate+"',getdate(),'"+strUser+"')", CommandType.Text);
        }

        public void UnBlockProcessEntryOnProcessCancellation(String div, Int32 PYear, Int32 PMonth)
        {
            SQLHelper.ExecuteNonQuery("delete from dbo.CHKProcessDetails where ProcessYear='" + PYear + "' and ProcessMonth='" + PMonth + "' and Division='" + div + "'", CommandType.Text);
        }

        public DataTable ListFixedDeductionsToCancel(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT DeductedYear, DeductedMonth, DeductedAmount, BalanceAmount, RecoveredInstallments, RecoveredAmount FROM dbo.CHKFixedDeductions where (DivisionId='"+strDiv+"') AND (DeductedYear = '"+intYear+"') AND (DeductedMonth = '"+intMonth+"') AND (DeductedAmount>0)", CommandType.Text);
            return ds.Tables[0];
        }
        public DataTable ListLoanDeductionsToCancel(String strDiv, Int32 intYear, Int32 intMonth)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT     DeductionGroup, DeductionCode, EmployeeNo FROM dbo.CHKLoan WHERE     (DivisionCode = '" + strDiv + "') AND (DeductedYear = '" + intYear + "') AND (DeductedMonth = '" + intMonth + "') AND (DeductedAmount > 0)", CommandType.Text);
            return ds.Tables[0];
        }


        public DataTable ListProcessEntries(String strDiv,Int32 year,int month)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("id"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ProcessidAuto FROM dbo.CHKProcessDetails WHERE (ProcessYear = '"+year+"') AND (ProcessMonth = '"+month+"') AND (ProcessedYesNo = 1) AND (Division = '"+strDiv+"')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void AddNewMonth( Int32 PYear, Int32 PMonth)
        {            
            DateTime dtCurrentMonthDate = new DateTime(PYear, PMonth, 1);
            DateTime dtNextMonthStartDate = dtCurrentMonthDate.AddMonths(1);
            DateTime dtNextMonthEndDate = dtCurrentMonthDate.AddMonths(2).AddDays(-1);
            Int32 intNextYear = dtCurrentMonthDate.AddMonths(1).Year;
            Int32 intNextMonth = dtCurrentMonthDate.AddMonths(1).Month;

            String Status = "";
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@nxYear", SqlDbType.Int);
            param.Value = intNextYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@nxMonth", SqlDbType.Int);
            param.Value = intNextMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@nxStDate", SqlDbType.DateTime);
            param.Value = dtNextMonthStartDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@nxEndDate", SqlDbType.DateTime);
            param.Value = dtNextMonthEndDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar,50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPAddNewMonth", CommandType.StoredProcedure, paramList);
           
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }

        public Boolean IsProcessed(String strDiv, Int32 PYear, Int32 PMonth)
        {
            Boolean ProcessYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Year");
            dt.Columns.Add("month");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ProcessYear,ProcessMonth FROM dbo.CHKProcessDetails WHERE (ProcessYear = '"+PYear+"') AND (ProcessMonth = '"+PMonth+"') AND (Division = '"+strDiv+"') AND (ProcessedYesNo = 1)", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetInt32(1);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ProcessYesNo = true;
            else
                ProcessYesNo = false;
            return ProcessYesNo;
        }

        public Boolean IsProcessedAllDivisions( Int32 PYear, Int32 PMonth)
        {
            Boolean ProcessYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Year");
            dt.Columns.Add("month");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ProcessYear,ProcessMonth FROM dbo.CHKProcessDetails WHERE (ProcessYear = '" + PYear + "') AND (ProcessMonth = '" + PMonth + "')  AND (ProcessedYesNo = 0)", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetInt32(1);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ProcessYesNo = true;
            else
                ProcessYesNo = false;
            return ProcessYesNo;
        }


        public String processFixedDeductions( String strEmpNo)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar,50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductYear", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Deductmonth", SqlDbType.Int);
            param.Value = intMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Employee", SqlDbType.VarChar, 50);
            param.Value = strEmpNo;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessFixedDeductions", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String CancelprocessFixedDeductions()
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductYear", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Deductmonth", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);
            //param = SQLHelper.CreateParameter("@Employee", SqlDbType.VarChar, 50);
            //param.Value = strEmpNo;
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spCancelProcessFixedDeductions", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String CancelprocessLoanDeductions()
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductYear", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Deductmonth", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);
            //param = SQLHelper.CreateParameter("@Employee", SqlDbType.VarChar, 50);
            //param.Value = strEmpNo;
            //paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spCancelProcessLoanDeductions", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String processLoanDeductions(String strEmp)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar,50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@deductYear", SqlDbType.Int);
            param.Value = IntYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Deductmonth", SqlDbType.Int);
            param.Value = IntMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Employee", SqlDbType.VarChar,50);
            param.Value = strEmp;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessLoanDeductions", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public void InsertProcessingDivision(String div, Int32 PYear, Int32 PMonth)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@ProYear", SqlDbType.Int);
            param.Value = PYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProMonth", SqlDbType.Int);
            param.Value = PMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProDivision", SqlDbType.VarChar, 50);
            param.Value = div;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPAddProcessingDivisions", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            // trnScope.Complete();
            //}
        }

        public void UpdateProcessedDivisionStatus(String div, Int32 PYear, Int32 PMonth)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@ProYear", SqlDbType.Int);
            param.Value = PYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProMonth", SqlDbType.Int);
            param.Value = PMonth;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProDivision", SqlDbType.VarChar, 50);
            param.Value = div;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("SPUpdateProcessedDivisionsStatus", CommandType.StoredProcedure, paramList);
            SQLHelper.ExecuteNonQuery(cmd);
            cmd.Dispose();
            // trnScope.Complete();
            //}
        }

        public Boolean IsAllDivisionsProcessUnsuccessful(Int32 PYear, Int32 PMonth)
        {
            Boolean NotSuccessfull = true;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Year");
            dt.Columns.Add("month");
            dt.Columns.Add("Processed");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ProcessYear, ProcessMonth,Processed  FROM dbo.CHKDivisionProcessedStatus WHERE     (ProcessYear = '" + PYear + "') AND (ProcessMonth = '" + PMonth + "') ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetBoolean(2);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dtrow1 in dt.Rows)
                {
                    if (Convert.ToBoolean(dtrow1[2].ToString())==false)
                    {
                        NotSuccessfull = false;
                    }
                }
            }
            else
                NotSuccessfull = false;
            return NotSuccessfull;
        }

        public void DeleteMonthEntry( Int32 PYear, Int32 PMonth)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM dbo.CHKMonths WHERE (Year = '"+PYear+"') AND (MId = '"+PMonth+"')", CommandType.Text);
        }

        public DataTable GetMonthToAllowCancel()
        {
            SqlDataReader dataReader;
            DataTable dt = new DataTable();
            dt.Columns.Add("PYear");
            dt.Columns.Add("PMonth");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) ProcessYear, ProcessMonth FROM dbo.CHKProcessDetails WHERE (ProcessedYesNo = 1) GROUP BY ProcessYear, ProcessMonth ORDER BY ProcessYear DESC, ProcessMonth DESC", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetInt32(1);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public void InsertProcessCancelAudit(String strUser, Int32 intYear, Int32 intMonth)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO  [dbo].[AuditProcessCancel] ([UserName] ,[CanceledDate] ,[CancelledYear] ,[CancelledMonth]) VALUES ('" + strUser + "' ,getdate() ,'" + intYear + "' ,'" + intMonth + "')", CommandType.Text);
        }

        public Boolean IsPreviewed(Int32 PYear, Int32 PMonth)
        {
            Boolean ProcessYesNo = false;
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Year");
            dt.Columns.Add("month");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT ProcessYear,ProcessMonth FROM dbo.CHKProcessDetails WHERE (ProcessYear = '" + PYear + "') AND (ProcessMonth = '" + PMonth + "')  AND (PreviewYesNo  = 1)", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetInt32(1);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            if (dt.Rows.Count > 0)
                ProcessYesNo = true;
            else
                ProcessYesNo = false;
            return ProcessYesNo;
        }

        public void FillAccountMapping(Int32 intYear, Int32 intMonth)
        {
            //SqlCommand cmd = new SqlCommand();
            //cmd = SQLHelper.CreateCommand("spAccountMapping", CommandType.StoredProcedure);
            //SQLHelper.ExecuteNonQuery(cmd);
            //cmd.Dispose();

            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@MapYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MapMonth", SqlDbType.Int);
            param.Value = intMonth;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spAccountMapping]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
        }
        public void FillAccountMappingOT(Int32 intYear, Int32 intMonth)
        {
            //SqlCommand cmd = new SqlCommand();
            //cmd = SQLHelper.CreateCommand("spAccountMappingOT", CommandType.StoredProcedure);
            //SQLHelper.ExecuteNonQuery(cmd);
            //cmd.Dispose();


            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@MapYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MapMonth", SqlDbType.Int);
            param.Value = intMonth;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spAccountMappingOT]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
        }
        public DataTable ListDailyEntriesMapping(DateTime dtFrom, DateTime dtTo, Boolean boolAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DateEntered");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("FieldID");
            dt.Columns.Add("Job");
            dt.Columns.Add("MainCode");
            //dt.Columns.Add("AccountCode");
            dt.Columns.Add("Status");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            if (boolAll == true)
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID, Job, MainCode, AccountCode, Status FROM dbo.AccountMappingTable WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102))", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID, Job, MainCode, Status FROM dbo.AccountMappingTable WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) ", CommandType.Text);
            }
            else
            {
                dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID, Job, MainCode, Status FROM dbo.AccountMappingTable WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) and  (Status = 0)", CommandType.Text);
            }
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    drow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetString(4).Trim();
                }
                //if (!dataReader.IsDBNull(5))
                //{
                //    drow[5] = dataReader.GetString(5).Trim();
                //}
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetBoolean(5);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListDailyEntriesMappingOT(DateTime dtFrom, DateTime dtTo, Boolean boolAll)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DateEntered");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("FieldID");
            dt.Columns.Add("Job");
            dt.Columns.Add("MainCode");
            //dt.Columns.Add("AccountCode");
            dt.Columns.Add("Status");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            if (boolAll==true)
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID, Job, MainCode, AccountCode, Status FROM dbo.AccountMappingTableOT WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102))", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID, Job, MainCode, Status FROM dbo.AccountMappingTableOT WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102))", CommandType.Text);
            }
            else
            {
                dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID, Job, MainCode, Status FROM dbo.AccountMappingTableOT WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) and  (Status = 0)", CommandType.Text);
            }
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    drow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetString(4).Trim();
                }
                //if (!dataReader.IsDBNull(5))
                //{
                //    drow[5] = dataReader.GetString(5).Trim();
                //}
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetBoolean(5);
                }

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        //public void UpdateDailyEntryMapping(DateTime dtDate, String strDiv, String strField, String strJob, String SubCatCode, String ACCode)
        //{
        //    SQLHelper.ExecuteNonQuery("UPDATE  dbo.DailyGroundTransactions SET SubCategoryCode='"+SubCatCode+"',JobAccountCode='"+ACCode+"' WHERE     (DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (DivisionID = '"+strDiv+"') AND (FieldID = '"+strField+"') AND (WorkCodeID = '"+strJob+"')",CommandType.Text);
        //    SQLHelper.ExecuteNonQuery("UPDATE  dbo.AccountMappingTable SET MainCode='" + SubCatCode + "',AccountCode='" + ACCode + "',Status=1 WHERE     (DateEntered = CONVERT(DATETIME,'" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strField + "') AND (Job = '" + strJob + "')", CommandType.Text);
        //}
        //public void UpdateDailyEntryMappingOT(DateTime dtDate, String strDiv, String strField, String strJob, String SubCatCode, String ACCode)
        //{
        //    SQLHelper.ExecuteNonQuery("UPDATE  dbo.CHKOvertime SET  SubCategoryCode='" + SubCatCode + "',JobAccountCode='" + ACCode + "' WHERE      (OtDate = CONVERT(DATETIME, '" + dtDate + "', 102) ) AND (DivisionCode = '" + strDiv + "') AND  (Field = '" + strField + "') AND (Job ='" + strJob + "')", CommandType.Text);
        //    SQLHelper.ExecuteNonQuery("UPDATE  dbo.AccountMappingTableot SET MainCode='" + SubCatCode + "',AccountCode='" + ACCode + "',Status=1 WHERE     (DateEntered = CONVERT(DATETIME,'" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strField + "') AND (Job = '" + strJob + "')", CommandType.Text);
        //}

        public void UpdateDailyEntryMapping(DateTime dtDate, String strDiv, String strField, String strJob, String SubCatCode)
        {
            SQLHelper.ExecuteNonQuery("UPDATE  dbo.DailyGroundTransactions SET SubCategoryCode='" + SubCatCode + "' WHERE     (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strField + "') AND (WorkCodeID = '" + strJob + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE  dbo.AccountMappingTable SET MainCode='" + SubCatCode + "',Status=1 WHERE     (DateEntered = CONVERT(DATETIME,'" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strField + "') AND (Job = '" + strJob + "')", CommandType.Text);
        }
        public void UpdateDailyEntryMappingOT(DateTime dtDate, String strDiv, String strField, String strJob, String SubCatCode)
        {
            SQLHelper.ExecuteNonQuery("UPDATE  dbo.CHKOvertime SET  SubCategoryCode='" + SubCatCode + "' WHERE      (OtDate = CONVERT(DATETIME, '" + dtDate + "', 102) ) AND (DivisionCode = '" + strDiv + "') AND  (Field = '" + strField + "') AND (Job ='" + strJob + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE  dbo.AccountMappingTableot SET MainCode='" + SubCatCode + "',Status=1 WHERE     (DateEntered = CONVERT(DATETIME,'" + dtDate + "', 102)) AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strField + "') AND (Job = '" + strJob + "')", CommandType.Text);
        }

        public String UpdatePluckingPRI(DateTime dtFromDate, DateTime dtToDate, String strPRIDivision,String strCrop)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strPRIDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropType", SqlDbType.VarChar, 50);
            param.Value = strCrop;
            paramList.Add(param);   
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[SPUpdate_PlkTap_PRI]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String UpdatePluckingPRI_General(DateTime dtFromDate, DateTime dtToDate, String strPRIDivision, String strCrop)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strPRIDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropType", SqlDbType.VarChar, 50);
            param.Value = strCrop;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[SPUpdate_PlkTap_PRI_General]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String UpdatePluckingPRI_Lent(DateTime dtFromDate, DateTime dtToDate, String strPRIDivision, String strCrop)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strPRIDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropType", SqlDbType.VarChar, 50);
            param.Value = strCrop;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[SPUpdate_PlkTap_PRI_Lent]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public DataSet GetOkgPRI(DateTime dtFromDate, DateTime dtToDate, String strPRIDivision, String strCrop)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            DataSet ds = new DataSet();
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtFromDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strPRIDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropType", SqlDbType.VarChar, 50);
            param.Value = strCrop;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            ds = SQLHelper.FillDataSet("[SP_Check_OKG_PRI]", CommandType.StoredProcedure, paramList);
            
            cmd.Dispose();
            return ds;
            // trnScope.Complete();
            //}
        }

        public DataSet GetOkgPRI1(DateTime dtFromDate, DateTime dtToDate, String strDivision, String strCrop)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("select * from ( SELECT        DateEntered, DivisionID, FieldID, WorkCodeID, WorkQty, (SELECT        ISNULL(MalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS MaleOkgNorm, (SELECT        ISNULL(FemalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_2 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS FemaleOkgNorm, EmpNo, (SELECT        Gender FROM            dbo.EmployeeMaster WHERE        (EmpNo = dbo.DailyGroundTransactions.EmpNo) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID)) AS Gender, (SELECT        ISNULL(MalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS MalePRINorm, (SELECT        ISNULL(FemalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS FemalePRINorm, LabourType, FullHalf, PRIAmount, OverKgs, OverKgAmount, CASE WHEN ((SELECT        Gender FROM            dbo.EmployeeMaster WHERE        (EmpNo = dbo.DailyGroundTransactions.EmpNo) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID)) = 'M') THEN (SELECT        ISNULL(MalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) ELSE (SELECT        ISNULL(FemalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) END AS PRINorm, CASE WHEN ((SELECT        Gender FROM            dbo.EmployeeMaster WHERE        (EmpNo = dbo.DailyGroundTransactions.EmpNo) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID)) = 'M') THEN (SELECT        ISNULL(MalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) ELSE (SELECT        ISNULL(FemalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) END AS OkgNorm,   'OKG_Different' as Type FROM            dbo.DailyGroundTransactions WHERE        (WorkType = 1) AND (WorkCodeID IN ('TAP', 'PLK')) AND (LabourType = 'general') AND (DateEntered BETWEEN CONVERT(DATETIME, '"+dtFromDate+"', 102) AND  CONVERT(DATETIME, '"+dtToDate+"', 102)) AND (DivisionID like '"+strDivision+"')) as T1 where T1.WorkQty> T1.OkgNorm and (T1.WorkQty-T1.OkgNorm)<>T1.OverKgs  union  select * from ( SELECT        DateEntered, DivisionID, FieldID, WorkCodeID, WorkQty, (SELECT        ISNULL(MalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS MaleOkgNorm, (SELECT        ISNULL(FemalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_2 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS FemaleOkgNorm, EmpNo, (SELECT        Gender FROM            dbo.EmployeeMaster WHERE        (EmpNo = dbo.DailyGroundTransactions.EmpNo) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID)) AS Gender, (SELECT        ISNULL(MalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS MalePRINorm, (SELECT        ISNULL(FemalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) AS FemalePRINorm, LabourType, FullHalf, PRIAmount, OverKgs, OverKgAmount, CASE WHEN ((SELECT        Gender FROM            dbo.EmployeeMaster WHERE        (EmpNo = dbo.DailyGroundTransactions.EmpNo) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID)) = 'M') THEN (SELECT        ISNULL(MalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) ELSE (SELECT        ISNULL(FemalePRINorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) END AS PRINorm, CASE WHEN ((SELECT        Gender FROM            dbo.EmployeeMaster WHERE        (EmpNo = dbo.DailyGroundTransactions.EmpNo) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID)) = 'M') THEN (SELECT        ISNULL(MalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) ELSE (SELECT        ISNULL(FemalePlkNorm, 0) AS Expr1 FROM            dbo.ChkFieldWiseNorm AS ChkFieldWiseNorm_1 WHERE        (NormDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionId = dbo.DailyGroundTransactions.DivisionID) AND  (FieldId = dbo.DailyGroundTransactions.FieldID)) END AS OkgNorm,  'PRI_Different' as Type FROM            dbo.DailyGroundTransactions WHERE        (WorkType = 1) AND (WorkCodeID IN ('TAP', 'PLK')) AND (LabourType = 'general') AND (DateEntered BETWEEN CONVERT(DATETIME, '"+dtFromDate+"', 102) AND  CONVERT(DATETIME, '"+dtToDate+"', 102)) AND (DivisionID like '"+strDivision+"')) as T1 where T1.WorkQty>= T1.PRINorm and (T1.PRIAmount<>140)    order by T1.Type", CommandType.Text);

            return ds;
            // trnScope.Complete();
            //}
        }

        //Get general dates and fields to update PRI and Okg Norm
        public DataSet GetGeneralDailyEntryDates(DateTime dtDate, String strDivision)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT        DateEntered, DivisionID, FieldID FROM  dbo.DailyGroundTransactions WHERE (WorkType = 1) AND  (WorkCodeID IN ('PLK', 'TAP')) AND (DivisionID = '" + strDivision + "') AND (DateEntered = CONVERT(DATETIME,'" + dtDate + "', 102) ) and (LabourType='General') GROUP BY DateEntered, DivisionID, FieldID", CommandType.Text);

            return ds;
            // trnScope.Complete();
            //}
        }

        //Get Lent dates and fields to update PRI and Okg Norm
        public DataSet GetLentDailyEntryDates(DateTime dtDate, String strDivision)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT        DateEntered, LabourDivision, LabourField FROM  dbo.DailyGroundTransactions WHERE (WorkType = 1) AND  (WorkCodeID IN ('PLK', 'TAP')) AND (LabourDivision = '" + strDivision + "') AND (DateEntered = CONVERT(DATETIME,'" + dtDate + "', 102) ) and (LabourType='Lent Labour') GROUP BY DateEntered, LabourDivision, LabourField", CommandType.Text);

            return ds;
            // trnScope.Complete();
            //}
        }
        public DataSet GetFieldWiseNorms(DateTime dtNormDate, String strDivision, String strField)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT        isnull(MalePlkNorm,0), isnull(FemalePlkNorm,0), isnull(MalePRINorm,0), isnull(FemalePRINorm,0),FieldId, isnull(GasTappingNorm,0) FROM            dbo.ChkFieldWiseNorm WHERE        (NormDate = CONVERT(DATETIME, '" + dtNormDate + "', 102)) AND (DivisionId = '" + strDivision + "') AND (FieldId = '" + strField + "')", CommandType.Text);

            return ds;
            // trnScope.Complete();
            //}
        }
        public Decimal GetFiledOkgRate(String strDivision, String strField)
        {
            SqlDataReader readerFieldRate;
            SqlDataReader readerFieldCrop;
            Decimal rateValue=0;
            readerFieldRate = SQLHelper.ExecuteReader("SELECT TOP (1) Rate FROM dbo.CHKFieldSettings WHERE        (Type = 'OKGRATE') AND (DivisionID = '" + strDivision + "') AND (FieldID = '" + strField + "')", CommandType.Text);
            while(readerFieldRate.Read())
            {
                if(!readerFieldRate.IsDBNull(0))
                {
                    rateValue=readerFieldRate.GetDecimal(0);
                }
            }
            readerFieldRate.Close();
            if(rateValue<=0)
            {
                readerFieldCrop=SQLHelper.ExecuteReader("SELECT  CropType FROM dbo.EstateField WHERE (DivisionID = '"+strDivision+"') AND (FieldID = '"+strField+"')",CommandType.Text);
                while(readerFieldCrop.Read())
                {                    
                    if(!readerFieldCrop.IsDBNull(0))
                    {
                        if(readerFieldCrop.GetString(0).ToUpper().Equals("TEA"))
                        {
                            readerFieldRate=SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'OverKgRate') AND (EmpType = 'All') ",CommandType.Text);                            
                        }
                        else if(readerFieldCrop.GetString(0).ToUpper().Equals("RUBBER"))
                        {
                            readerFieldRate=SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'ROverKgRate') AND (EmpType = 'All') ",CommandType.Text);
                        }
                        else
                            readerFieldRate=SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates WHERE (Type = 'OPOverKgRate') AND (EmpType = 'All')",CommandType.Text);
                        while(readerFieldRate.Read())
                        {
                            if(!readerFieldRate.IsDBNull(0))
                            {
                                rateValue=readerFieldRate.GetDecimal(0);
                            }
                            else
                                rateValue=0;
                        }
                        readerFieldRate.Close();
                    }                    
                }
                readerFieldCrop.Close();
            }
            return rateValue;
        }

        public String UpdatePluckingPRI_FieldWise(DateTime dtNormDate, String strPRIDivision, String strPRIField,Int32 intMaleOkgNorm,Int32 intFemaleOkgNorm,Int32 intMalePRINorm,Int32 intFemalePRINorm,Int32 intGasTappingNorm,Decimal decOkgRate)
        {
            // using (TransactionScope trnScope = new TransactionScope())
            //{
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@NormDate", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(dtNormDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar, 50);
            param.Value = strPRIDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldID", SqlDbType.VarChar, 50);
            param.Value = strPRIField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MaleOkgNorm", SqlDbType.Int);
            param.Value = intMaleOkgNorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FemaleOkgNorm", SqlDbType.Int);
            param.Value = intFemaleOkgNorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MalePRINorm", SqlDbType.Int);
            param.Value = intMalePRINorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FemalePRINorm", SqlDbType.Int);
            param.Value = intFemalePRINorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@GasTappingNorm", SqlDbType.Int);
            param.Value = intGasTappingNorm;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldOkgRate", SqlDbType.Decimal);
            param.Value = decOkgRate;
            paramList.Add(param);
            
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[SPUpdate_PlkTap_PRI_FieldWise]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public void ResetPRI(String strDivision,DateTime dtNormDate)
        {
            SQLHelper.ExecuteNonQuery("update DailyGroundTransactions set PRIAmount=0 where   (DateEntered='"+dtNormDate+"') and (DivisionID='"+strDivision+"') and (WorkType=1) and (WorkCodeID  in ('TAP','PLK'))", CommandType.Text);
        }




    }
}
