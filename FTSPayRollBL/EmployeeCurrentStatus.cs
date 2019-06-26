using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeCurrentStatus
    {
        private String strEmpEstate;

        public String StrEmpEstate
        {
            get { return strEmpEstate; }
            set { strEmpEstate = value; }
        }
        

        private String strEmpDivision;

        public String StrEmpDivision
        {
            get { return strEmpDivision; }
            set { strEmpDivision = value; }
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

        private String strCurrentStatus;

        public String StrCurrentStatus
        {
            get { return strCurrentStatus; }
            set { strCurrentStatus = value; }
        }

        private String strNewStatus;

        public String StrNewStatus
        {
        get { return strNewStatus; }
        set { strNewStatus = value; }
        }

        private String strStatus;

        public String StrStatus
        {
            get { return strStatus; }
            set { strStatus = value; }
        }

        private DateTime strResignedDate;

        public DateTime StrResignedDate
        {
            get { return strResignedDate; }
            set { strResignedDate = value; }
        }

        private Boolean strResign;

        public Boolean StrResign
        {
            get { return strResign; }
            set { strResign = value; }
        }

        private Int32 strAutoKey;

        public Int32 StrAutoKey
        {
            get { return strAutoKey; }
            set { strAutoKey = value; }
        }
        private String strReason;

        public String StrReason
        {
            get { return strReason; }
            set { strReason = value; }
        }
        private String strCurrentStatusRef;

        public String StrCurrentStatusRef
        {
            get { return strCurrentStatusRef; }
            set { strCurrentStatusRef = value; }
        }

        private DateTime dtTxDate;

        public DateTime DtTxDate
        {
            get { return dtTxDate; }
            set { dtTxDate = value; }
        }

        private String strChangedMethod;

        public String StrChangedMethod
        {
            get { return strChangedMethod; }
            set { strChangedMethod = value; }
        }

        private String strUserID;

        public String StrUserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }

        private Int32 intInactiveAbsents;

        public Int32 IntInactiveAbsents
        {
            get { return intInactiveAbsents; }
            set { intInactiveAbsents = value; }
        }
        private Int32 intTerminateAbsents;

        public Int32 IntTerminateAbsents
        {
            get { return intTerminateAbsents; }
            set { intTerminateAbsents = value; }
        }

        private DateTime dtLastWorkedDate;

        public DateTime DtLastWorkedDate
        {
            get { return dtLastWorkedDate; }
            set { dtLastWorkedDate = value; }
        }
        private DateTime dtTerminatedDate;

        public DateTime DtTerminatedDate
        {
            get { return dtTerminatedDate; }
            set { dtTerminatedDate = value; }
        }
        private Decimal decLastWorkRate;

        public Decimal DecLastWorkRate
        {
            get { return decLastWorkRate; }
            set { decLastWorkRate = value; }
        }

        public DataTable ListAllEmpStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpDivision");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EmpName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpDivision, EmpNo, EmpName FROM  dbo.EmployeeCurrentStatus ORDER BY EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public void UpdateEmpCurrentStatus()
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.EmployeeCurrentStatus SET NewStatus ='" + strNewStatus + "' WHERE (EmpNo = '" + strEmpNo + "')", CommandType.Text);
            //SQLHelper.ExecuteNonQuery("UPDATE dbo.EmployeeMaster SET EmployeeStatus ='" + StrStatus + "' WHERE (EmpNo = '" + strEmpNo + "')", CommandType.Text);
          

        }
        public void InsertEmpCurrentStatus()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeCurrentStatus (EmpDivision, EmpNo, EmpName, CurrentStatus, NewStatus, UserID, ResignedDate, Resign) VALUES ('" + strEmpDivision + "','" + strEmpNo + "','" + strEmpName + "','" + strCurrentStatus + "','" + strNewStatus + "','" + FTSPayRollBL.User.StrUserName + "','" + strResignedDate + "','" + strResign + "')", CommandType.Text);
            //SQLHelper.ExecuteNonQuery("INSERT INTO dbo.EmployeeMaster(EmployeeStatus) VALUES ('" + strStatus + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE dbo.EmployeeMaster SET EmployeeStatus ='" + StrNewStatus + "', ResignedDate ='" + StrResignedDate + "' WHERE (EmpNo = '" + strEmpNo + "')", CommandType.Text);
          
        }

        public String GetEmpCurrentStatus(String strEmpNo)
        {      
            String employeeStatus = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT     EmpNo, EmployeeStatus FROM   dbo.EmployeeMaster WHERE     (EmpNo = '" + strEmpNo + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(1))
                {
                    employeeStatus = dataReader.GetString(1).Trim();
                }
            }
            dataReader.Close();
            return employeeStatus;
      
        }

        public DataTable EmpStatus()
        {
            DataTable dt = new DataTable();
        
            dt.Columns.Add(new DataColumn("EmpDivision"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("CurrentStatus"));
            dt.Columns.Add(new DataColumn("NewStatus"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("ResignedDate"));
            dt.Columns.Add(new DataColumn("Resign"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  EmpDivision, EmpNo, EmpName, CurrentStatus, NewStatus, UserID, CreateDateType, ResignedDate, Resign FROM  EmployeeCurrentStatus", CommandType.Text);

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
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6).ToLocalTime();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }

                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetBoolean(8);
                
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void InsertEmployeeStatusChange()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeCurrentStatus (EmpDivision, EmpNo, EmpName, CurrentStatus, NewStatus, UserID, ResignedDate, Resign) VALUES ('" + strEmpDivision + "','" + strEmpNo + "','" + strEmpName + "','" + strCurrentStatus + "','" + strNewStatus + "','" + FTSPayRollBL.User.StrUserName + "','" + strResignedDate + "','" + strResign + "')", CommandType.Text);
            //SQLHelper.ExecuteNonQuery("INSERT INTO dbo.EmployeeMaster(EmployeeStatus) VALUES ('" + strStatus + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE dbo.EmployeeMaster SET EmployeeStatus ='" + StrNewStatus + "', ResignedDate ='" + StrResignedDate + "' WHERE (EmpNo = '" + strEmpNo + "')", CommandType.Text);
        }

        public String EmployeeStatusChange()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@TxDate", SqlDbType.DateTime);
            param.Value = DtTxDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar, 50);
            param.Value = StrEmpEstate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar, 50);
            param.Value = StrEmpDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@AvailableStatus", SqlDbType.VarChar, 50);
            param.Value = StrCurrentStatus;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NewStatus", SqlDbType.VarChar, 50);
            param.Value = StrNewStatus;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Reason", SqlDbType.VarChar, 50);
            param.Value = StrReason;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ChangedMethod", SqlDbType.VarChar, 50);
            param.Value = StrChangedMethod;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = StrUserID;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@LastWorkedDate", SqlDbType.DateTime);
            param.Value = dtLastWorkedDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@LastWokedRate", SqlDbType.Decimal);
            param.Value = DecLastWorkRate;
            paramList.Add(param);

            //-------
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertEmployeeStatusChange", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            //-------


            //SqlCommand cmd = new SqlCommand();
            //cmd = SQLHelper.CreateCommand("spInsertEmployeeStatusChange", CommandType.StoredProcedure, paramList);
            ////identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            //statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            ////identityParam.Direction = ParameterDirection.ReturnValue;
            //statusParam.Direction = ParameterDirection.Output;
            //SQLHelper.ExecuteNonQuery(cmd);
            ////int trnScope = int.Parse(identityParam.Value.ToString());
            //Status = statusParam.Value.ToString();
            //cmd.Dispose();
            //return Status;
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKMusterChit] ([DateEntered] ,[DivisionID] ,[FieldID] ,[MainAccountCode] ,[NoOfEmployees] ,[CreateDateTime] ,[UserID],[ChitNo],GangNumber) VALUES ('" + DtDate + "' ,'" + StrDivision + "' ,'" + StrField + "' ,'" + StrACCode + "' ,'" + IntEmpCount + "' ,getdate() ,'" + StrUser + "','" + StrChitNo + "','"+StrGangNumber+"')", CommandType.Text);
        }

        public DataTable getListOfEmployeesAboveToChangeStatus(String strEst, String strDiv, String strStatus, Int32 intChangeDuration, Boolean boolActiveEmp)
        {
            DataTable dtListOfEmployees = new DataTable("ListOfEmployees");
            dtListOfEmployees.Columns.Add(new DataColumn("Change", typeof(bool)));//0
            dtListOfEmployees.Columns.Add(new DataColumn("DivisionID", typeof(String)));
            dtListOfEmployees.Columns.Add(new DataColumn("EmpNo", typeof(String)));
            dtListOfEmployees.Columns.Add(new DataColumn("EmpName", typeof(String)));
            dtListOfEmployees.Columns.Add(new DataColumn("Gender", typeof(String)));
            dtListOfEmployees.Columns.Add(new DataColumn("ActiveYesNo", typeof(String)));//5
            dtListOfEmployees.Columns.Add(new DataColumn("CurrentStatus", typeof(String)));
            dtListOfEmployees.Columns.Add(new DataColumn("LastWorkDate", typeof(DateTime)));//7
            dtListOfEmployees.Columns.Add(new DataColumn("NoOfAbsents", typeof(Int32)));
            dtListOfEmployees.Columns.Add(new DataColumn("LastWorkedRate", typeof(Decimal)));
            //SqlDataReader readerEmployee = SQLHelper.ExecuteReader("SELECT  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender,  dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, MAX(dbo.DailyGroundTransactions.DateEntered) AS LastWorkedDate, DATEDIFF(day,  MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) AS NoOfAbsentDays FROM dbo.EmployeeMaster INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID AND  dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo WHERE (dbo.EmployeeMaster.EstateID = '" + strEst + "') AND  (dbo.EmployeeMaster.DivisionID like '" + strDiv + "') AND (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "') GROUP BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmployeeStatus,  dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee HAVING        (DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) > '" + intChangeDuration + "')  ", CommandType.Text);
            //SqlDataReader readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, MAX(dbo.DailyGroundTransactions.DateEntered) AS LastWorkedDate, DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) AS NoOfAbsentDays FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.EmployeeCategory.AutoInactive = 1) GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeMaster.EMPName HAVING (DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) > '" + intChangeDuration + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') AND (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "') union "+
            //                                                        " SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, CONVERT(datetime, '2000-1-1', 102) AS LastWorkDate, DATEDIFF(day, CONVERT(datetime, '2000-1-1', 102), GETDATE()) AS NoOfAbsentDays FROM dbo.EmployeeMaster LEFT OUTER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID AND dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo WHERE (dbo.DailyGroundTransactions.EmpNo IS NULL)  AND (dbo.EmployeeMaster.EmployeeStatus = 'Active') union " +
            //                                                        " SELECT DivisionID, EmpNo, EMPName, Gender, ActiveEmployee, EmployeeStatus, CONVERT(datetime, '2000-1-1', 102) AS LastWorkedDate, DATEDIFF(day, CONVERT(datetime, '2000-1-1', 102), GETDATE()) AS NoOfAbsentDays FROM dbo.EmployeeMaster WHERE (dbo.EmployeeMaster.DivisionID like '" + strDiv + "') and ( (EmpNo NOT IN (SELECT EmpNo FROM dbo.DailyGroundTransactions WHERE (DivisionID LIKE dbo.EmployeeMaster.DivisionID) AND (EmpNo = dbo.EmployeeMaster.EmpNo) AND (WorkCodeID NOT LIKE 'X%') and (WorkCodeID not like 'ABS') and (WorkCodeID not like 'PH'))))", CommandType.Text);
            //SqlDataReader readerEmployee = SQLHelper.ExecuteReader(" SELECT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, MAX(dbo.DailyGroundTransactions.DateEntered) AS LastWorkedDate, DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) AS NoOfAbsentDays FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.EmployeeCategory.AutoInactive = 1) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'ABS')) AND  (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'PH')) GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeMaster.EMPName HAVING (DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) > '" + intChangeDuration + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') AND (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "') union " +
            //                                                        " SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, CONVERT(datetime, '2000-1-1', 102) AS LastWorkDate, DATEDIFF(day, CONVERT(datetime, '2000-1-1', 102), GETDATE()) AS NoOfAbsentDays FROM dbo.EmployeeMaster LEFT OUTER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID AND dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo WHERE (dbo.DailyGroundTransactions.EmpNo IS NULL)  AND (dbo.EmployeeMaster.EmployeeStatus = 'Active') union " +
            //                                                        " SELECT DivisionID, EmpNo, EMPName, Gender, ActiveEmployee, EmployeeStatus, CONVERT(datetime, '2000-1-1', 102) AS LastWorkedDate, DATEDIFF(day, CONVERT(datetime, '2000-1-1', 102), GETDATE()) AS NoOfAbsentDays FROM dbo.EmployeeMaster WHERE (dbo.EmployeeMaster.DivisionID like '" + strDiv + "') and ( (EmpNo NOT IN (SELECT EmpNo FROM dbo.DailyGroundTransactions WHERE (DivisionID LIKE dbo.EmployeeMaster.DivisionID) AND (EmpNo = dbo.EmployeeMaster.EmpNo) AND (WorkCodeID NOT LIKE 'X%') and (WorkCodeID not like 'ABS') and (WorkCodeID not like 'PH'))))", CommandType.Text);
            SqlDataReader readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, MAX(dbo.DailyGroundTransactions.DateEntered) AS LastWorkedDate, DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) AS NoOfAbsentDays,MAX(dbo.DailyGroundTransactions.DailyBasicAmount) AS LastWorkedRate FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.EmployeeCategory.AutoInactive = 1) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'ABS')) AND  (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'PH')) AND (dbo.EmployeeMaster.ActiveEmployee='" + boolActiveEmp + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeMaster.EMPName HAVING (DATEDIFF(day, MAX(dbo.DailyGroundTransactions.DateEntered), GETDATE()) > '" + intChangeDuration + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + strDiv + "') AND (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "') union " +
                                                                   "SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.ActiveEmployee, dbo.EmployeeMaster.EmployeeStatus, CONVERT(datetime, '2000-1-1', 102) AS LastWorkDate, DATEDIFF(day, CONVERT(datetime, '2000-1-1', 102), GETDATE()) AS NoOfAbsentDays,0 AS LastWorkedRate FROM dbo.EmployeeMaster LEFT OUTER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID AND dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo WHERE  (dbo.DailyGroundTransactions.DivisionID like '" + strDiv + "') and  (dbo.DailyGroundTransactions.EmpNo IS NULL)  AND (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "') union " +
                                                                    "SELECT DivisionID, EmpNo, EMPName, Gender, ActiveEmployee, EmployeeStatus, CONVERT(datetime, '2000-1-1', 102) AS LastWorkedDate, DATEDIFF(day, CONVERT(datetime, '2000-1-1', 102), GETDATE()) AS NoOfAbsentDays,0 AS LastWorkedRate FROM dbo.EmployeeMaster WHERE (dbo.EmployeeMaster.DivisionID like '" + strDiv + "') and  (EmpNo NOT IN (SELECT EmpNo FROM dbo.DailyGroundTransactions WHERE (DivisionID LIKE dbo.EmployeeMaster.DivisionID) AND (EmpNo = dbo.EmployeeMaster.EmpNo) AND (WorkCodeID NOT LIKE 'X%') and (WorkCodeID not like 'ABS') and (WorkCodeID not like 'PH'))) AND (dbo.EmployeeMaster.EmployeeStatus = '" + strStatus + "') AND (dbo.EmployeeMaster.ActiveEmployee = '" + boolActiveEmp + "')", CommandType.Text);
            DataRow drow;
            while (readerEmployee.Read())
            {
                drow = dtListOfEmployees.NewRow();
                if (!readerEmployee.IsDBNull(0))
                {
                    drow[0] = false;
                }
                if (!readerEmployee.IsDBNull(0))
                {
                    drow[1] = readerEmployee.GetString(0);
                }
                if (!readerEmployee.IsDBNull(1))
                {
                    drow[2] = readerEmployee.GetString(1);
                }
                if (!readerEmployee.IsDBNull(2))
                {
                    drow[3] = readerEmployee.GetString(2);
                }
                if (!readerEmployee.IsDBNull(3))
                {
                    drow[4] = readerEmployee.GetString(3);
                }
                if (!readerEmployee.IsDBNull(4))
                {
                    drow[5] = readerEmployee.GetBoolean(4);
                }
                if (!readerEmployee.IsDBNull(5))
                {
                    drow[6] = readerEmployee.GetString(5);
                }
                if (!readerEmployee.IsDBNull(6))
                {
                    drow[7] = readerEmployee.GetDateTime(6);
                }
                if (!readerEmployee.IsDBNull(7))
                {
                    drow[8] = readerEmployee.GetInt32(7);
                }
                if (!readerEmployee.IsDBNull(8))
                {
                    drow[9] = readerEmployee.GetDecimal(8);
                }
                dtListOfEmployees.Rows.Add(drow);

            }
            readerEmployee.Close();
            return dtListOfEmployees;

        }

        public DataTable GetPossibleNewStatusList(String strCurrentStatus)
        {
            DataTable dtListStatus = new DataTable();
            dtListStatus.Columns.Add(new DataColumn("Status"));
            DataRow drow;
            switch (strCurrentStatus.ToUpper())
            {
                case "ACTIVE":
                    drow = dtListStatus.NewRow();
                    drow[0] = "Active";
                    dtListStatus.Rows.Add(drow);
                    drow = dtListStatus.NewRow();
                    drow[0] = "Inactive";
                    dtListStatus.Rows.Add(drow);
                    drow = dtListStatus.NewRow();
                    drow[0] = "Terminate";
                    dtListStatus.Rows.Add(drow);
                    break;
                case "INACTIVE":
                    drow = dtListStatus.NewRow();
                    drow[0] = "Inactive";
                    dtListStatus.Rows.Add(drow);
                    drow = dtListStatus.NewRow();
                    drow[0] = "Active";
                    dtListStatus.Rows.Add(drow);
                    drow = dtListStatus.NewRow();
                    drow[0] = "Terminate";
                    dtListStatus.Rows.Add(drow);
                    break;
                case "TERMINATED":                    
                    drow = dtListStatus.NewRow();
                    drow[0] = "Terminated";
                    dtListStatus.Rows.Add(drow);
                    drow = dtListStatus.NewRow();
                    drow[0] = "Booked";
                    dtListStatus.Rows.Add(drow);
                    break;
                //case "TERMINATE":
                //    drow = dtListStatus.NewRow();
                //    drow[0] = "Terminate";
                //    dtListStatus.Rows.Add(drow);
                //    drow = dtListStatus.NewRow();
                //    drow[0] = "Inactive";
                //    dtListStatus.Rows.Add(drow);
                //    drow = dtListStatus.NewRow();
                //    drow[0] = "Booked";
                //    dtListStatus.Rows.Add(drow);
                //    break;
                //case "BOOKED":
                //    drow = dtListStatus.NewRow();
                //    drow[0] = "Booked";
                //    dtListStatus.Rows.Add(drow);
                //    drow = dtListStatus.NewRow();
                //    drow[0] = "PaidOff";
                //    dtListStatus.Rows.Add(drow);
                //    //drow = dtListStatus.NewRow();
                //    //drow[0] = "Terminate";
                //    //dtListStatus.Rows.Add(drow);
                //    break;
                //case "PAIDOFF":
                //    //drow = dtListStatus.NewRow();
                //    //drow[0] = "Inactive";
                //    //dtListStatus.Rows.Add(drow);
                //    //drow = dtListStatus.NewRow();
                //    //drow[0] = "Terminate";
                //    //dtListStatus.Rows.Add(drow);
                //    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }
            return dtListStatus;
        }

        public DataSet ListEmployeeStatusChangeLog(String strDiv)
        {
            DataSet dsLog = new DataSet();
            dsLog = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKEmployeeStatusChangeLog.TransactionDate, dbo.CHKEmployeeStatusChangeLog.EstateID,  dbo.CHKEmployeeStatusChangeLog.DivisionID, dbo.CHKEmployeeStatusChangeLog.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.CHKEmployeeStatusChangeLog.AvailableStatus, dbo.CHKEmployeeStatusChangeLog.NewStatus, dbo.CHKEmployeeStatusChangeLog.Reason,  dbo.CHKEmployeeStatusChangeLog.ChangedMethod, dbo.CHKEmployeeStatusChangeLog.Emp_LastWorkedDate FROM dbo.CHKEmployeeStatusChangeLog INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmployeeStatusChangeLog.EstateID = dbo.EmployeeMaster.EstateID AND  dbo.CHKEmployeeStatusChangeLog.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmployeeStatusChangeLog.EmpNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.CHKEmployeeStatusChangeLog.DivisionID like '" + strDiv + "') ORDER BY dbo.CHKEmployeeStatusChangeLog.TransactionDate DESC", CommandType.Text);
            return dsLog;
        }

        public DataSet ListEmployeeStatusChangeLog(String strDiv,DateTime dtDate)
        {
            DataSet dsLog = new DataSet();
            dsLog = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKEmployeeStatusChangeLog.TransactionDate, dbo.CHKEmployeeStatusChangeLog.EstateID,  dbo.CHKEmployeeStatusChangeLog.DivisionID, dbo.CHKEmployeeStatusChangeLog.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.CHKEmployeeStatusChangeLog.AvailableStatus, dbo.CHKEmployeeStatusChangeLog.NewStatus, dbo.CHKEmployeeStatusChangeLog.Reason,  dbo.CHKEmployeeStatusChangeLog.ChangedMethod, dbo.CHKEmployeeStatusChangeLog.Emp_LastWorkedDate FROM dbo.CHKEmployeeStatusChangeLog INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmployeeStatusChangeLog.EstateID = dbo.EmployeeMaster.EstateID AND  dbo.CHKEmployeeStatusChangeLog.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmployeeStatusChangeLog.EmpNo = dbo.EmployeeMaster.EmpNo WHERE      (dbo.CHKEmployeeStatusChangeLog.TransactionDate='"+dtDate+"') AND  (dbo.CHKEmployeeStatusChangeLog.DivisionID like '" + strDiv + "') ORDER BY dbo.CHKEmployeeStatusChangeLog.TransactionDate DESC", CommandType.Text);
            return dsLog;
        }

        public DataSet ListEmployeesAboveToInactive(String strDiv, DateTime dtDate)
        {
            DataSet dsLog = new DataSet();
            dsLog = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKEmployeeStatusChangeLog.TransactionDate, dbo.CHKEmployeeStatusChangeLog.EstateID,  dbo.CHKEmployeeStatusChangeLog.DivisionID, dbo.CHKEmployeeStatusChangeLog.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.CHKEmployeeStatusChangeLog.AvailableStatus, dbo.CHKEmployeeStatusChangeLog.NewStatus, dbo.CHKEmployeeStatusChangeLog.Reason,  dbo.CHKEmployeeStatusChangeLog.ChangedMethod, dbo.CHKEmployeeStatusChangeLog.Emp_LastWorkedDate FROM dbo.CHKEmployeeStatusChangeLog INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmployeeStatusChangeLog.EstateID = dbo.EmployeeMaster.EstateID AND  dbo.CHKEmployeeStatusChangeLog.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmployeeStatusChangeLog.EmpNo = dbo.EmployeeMaster.EmpNo WHERE      (dbo.CHKEmployeeStatusChangeLog.TransactionDate='" + dtDate + "') AND  (dbo.CHKEmployeeStatusChangeLog.DivisionID like '" + strDiv + "') ORDER BY dbo.CHKEmployeeStatusChangeLog.TransactionDate DESC", CommandType.Text);
            return dsLog;
        }

    }
}
