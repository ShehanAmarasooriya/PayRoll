using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using System.IO;
using System.IO.Compression;
//using ICSharpCode.SharpZipLib.Zip;
using System.Net;
using System.Globalization;

namespace FTSPayRollBL
{
    public class CheckErrors
    {
        FTSPayRollBL.YearMonth myYearMonth = new YearMonth();
        public DataSet checkInactiveEmpEntries(DateTime dtFrom,DateTime dtTo)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT DATEADD(D, 0, DATEDIFF(D, 0, dbo.DailyGroundTransactions.DateEntered)) AS Date, CASE WHEN (dbo.EmployeeMaster.ActiveEmployee = 1) THEN 'Active' ELSE 'Inactive' END AS ActiveInactive,  CASE WHEN (dbo.DailyGroundTransactions.WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS WorkType, dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.EmpNo, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.ManDays,  dbo.DailyGroundTransactions.WorkQty FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) AND  (dbo.EmployeeMaster.ActiveEmployee = 0)", CommandType.Text);
            da.Fill(ds, "InactiveEmpEntries");
            return ds;        
        }

        public Boolean CheckDailyEntriesOfEmployee(String strEmpNo,String strDivision)
        {
            Boolean boolYesNo = false;
            Int32 intYear = myYearMonth.getLastYearID();
            Int32 intMonth = myYearMonth.getLastMonthID();
            DateTime dtFromDate = new DateTime(intYear, intMonth, 1);
            DateTime dtToDate = dtFromDate.AddMonths(1).AddDays(-1);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT DateEntered, DivisionID, EmpNo FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '"+dtToDate+"', 102)) AND (DivisionID = '"+strDivision+"') AND  (EmpNo = '"+strEmpNo+"')", CommandType.Text);
            da.Fill(ds, "empEntries");
            if (ds.Tables[0].Rows.Count > 0)
            {
                boolYesNo = true;
            }
            return boolYesNo;
        }
    }
}
