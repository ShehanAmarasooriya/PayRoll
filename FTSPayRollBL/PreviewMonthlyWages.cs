using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{

    public class PreviewMonthlyWages
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
        private String strMaleFemale;

        public String StrMaleFemale
        {
            get { return strMaleFemale; }
            set { strMaleFemale = value; }
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
            param.Value = Convert.ToDateTime(DtProcessToDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spPreviewDeletePreviewData", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String processPreviewMonthlyWeges(String EmpNo)
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
            cmd = SQLHelper.CreateCommand("spProcessPreviewMonthlyWeges", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
        }

        public String processPreviewMonthlyWeges(String EmpNo, String Division)
        {
            
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
            param.Value = Division;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProEmpNo", SqlDbType.VarChar, 50);
            param.Value = EmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spProcessPreviewMonthlyWeges", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            
        }

        public Boolean IsPriorityDeduction()
        {
            Boolean boolPriorityDeduct=false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'DeductPriority')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).Equals("Available"))
                    {
                        boolPriorityDeduct = true;
                    }                    
                }
            }
            return boolPriorityDeduct;
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
            cmd = SQLHelper.CreateCommand("spProcessDeductMonthlyDeductions_Priority", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            // trnScope.Complete();
            //}
       
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
            //param = SQLHelper.CreateParameter("@ProEmpNO", SqlDbType.VarChar, 50);
            //param.Value = EmpNo;
            //paramList.Add(param);
            //param = SQLHelper.CreateParameter("@Process", SqlDbType.Bit);
            //param.Value = BoolProcess;
            //paramList.Add(param);
            //param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            //param.Value = User.StrUserName;
            //paramList.Add(param);
            //SqlCommand cmd = new SqlCommand();
            //if (IsPriorityDeduction())
            //{
            //    cmd = SQLHelper.CreateCommand("spProcessDeductMonthlyDeductions_Priority", CommandType.StoredProcedure, paramList);
            //}
            //else
            //{
            //    cmd = SQLHelper.CreateCommand("spProcessDeductMonthlyDeductions", CommandType.StoredProcedure, paramList);
            //}
            //statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            //statusParam.Direction = ParameterDirection.Output;
            //SQLHelper.ExecuteNonQuery(cmd);
            //Status = statusParam.Value.ToString();
            //cmd.Dispose();
            //return Status;
        }

        public String processDeductionWeges(String EmpNo, String Division)
        {
            
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
            param.Value = Division;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProEmpNO", SqlDbType.VarChar, 50);
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
        }


        public String processFinalWeges(String EmpNo)
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
            param.Value = EmpNo;
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

        public String processFinalWeges(String EmpNo,String Division)
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
            param.Value = Division;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Process", SqlDbType.Bit);
            param.Value = BoolProcess;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ProEmpNo", SqlDbType.VarChar, 50);
            param.Value = EmpNo;
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

        public DataSet GetMoreThanOneEntriesAddedToSameType(DateTime dtFromDate, DateTime dtToDate, Int32 intWorkType, String strDiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     DateEntered, WorkType, DivisionID, EmpNo, COUNT(WorkCodeID) AS NoOfEntries, CASE WHEN (WorkType = 1) THEN SUM(ManDays) ELSE SUM(CashMandays)  END AS Expr1 FROM dbo.DailyGroundTransactions WHERE (ConfirmedYesNo = 0) GROUP BY DateEntered, WorkType, DivisionID, EmpNo HAVING      (DateEntered between CONVERT(DATETIME, '" + dtFromDate + "', 102) and CONVERT(DATETIME, '" + dtToDate + "', 102)) AND  (DivisionID like '" + strDiv + "') AND (WorkType = '" + intWorkType + "') AND  (COUNT(WorkCodeID) > 1)", CommandType.Text);
            da.Fill(ds, "MoreThanOneEntriesNotConfirmed");
            return ds;
        }

        public DataSet GetMoreThanOneEntriesAddedToSameTypeDetail(DateTime dtDate,  Int32 intWorkType, String strDiv,String strEmp)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     DateEntered, DivisionID, EmpNo, WorkType, WorkCodeID, ManDays, CashManDays, WorkQty, PluckingExpenditure, Expenditure, CashPlkOkgYesNo FROM dbo.DailyGroundTransactions WHERE     (DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (DivisionID = '"+strDiv+"') AND (EmpNo = '"+strEmp+"') AND (WorkType = '"+intWorkType+"')", CommandType.Text);
            da.Fill(ds, "MoreThanOneEntriesNotConfirmed");
            return ds;
        }

        public void UpdateMoreThanOneEntriesAsConfirmed(DateTime dtFromDate, DateTime dtToDate, Int32 intWorkType, String strDiv)
        {
            SQLHelper.ExecuteNonQuery("update dbo.DailyGroundTransactions set ConfirmedYesNo =1 WHERE (DateEntered between CONVERT(DATETIME, '"+dtFromDate+"', 102) and CONVERT(DATETIME, '"+dtToDate+"', 102)) AND (DivisionID = '"+strDiv+"') AND (WorkType = '"+intWorkType+"') ", CommandType.Text);
        }

       

    }
}
