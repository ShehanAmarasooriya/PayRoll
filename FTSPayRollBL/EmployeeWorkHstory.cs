using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeWorkHstory
    {
        public DateTime GetStartDate(int intYear, int intMonth)
        {
                return new DateTime(intYear, intMonth, 1);
            
        }

        public DateTime GetEndDateOfMonth(DateTime dtDate)
        {
            if (dtDate != null)
            {
                return dtDate.AddMonths(1).AddDays(-1);
            }
        }

        public DataSet GetEmpWorkHis(int intYear, int intMonth, String strDivisionID,String strEmp)
        {
            DataSet ds = new DataSet();
            DateTime dtSdate;
            DateTime dtEdate;


            dtSdate = GetStartDate(intYear, intMonth);
            dtEdate = GetEndDateOfMonth(dtSdate);

            //ds = DataAccess.SQLHelper.FillDataSet("SELECT DailyGroundTransactions.DateEntered, EmployeeMaster.EmpNo, EmployeeMaster.EMPName, DailyGroundTransactions.DivisionID,  JobMaster.JobName, SUM(DailyGroundTransactions.WorkQty) AS workquantity, DailyGroundTransactions.TaskCompleted, CHKCompany.CompanyName  FROM DailyGroundTransactions INNER JOIN  JobMaster ON DailyGroundTransactions.WorkCodeID = JobMaster.JobShortName INNER JOIN  EmployeeMaster ON DailyGroundTransactions.EmpNo = EmployeeMaster.EmpNo LEFT OUTER JOIN  EstateDivision ON DailyGroundTransactions.DivisionID = EstateDivision.DivisionID CROSS JOIN CHKCompany WHERE (YEAR(DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND  (DailyGroundTransactions.DivisionID = '" + strDivisionID + "') GROUP BY DailyGroundTransactions.DateEntered, EmployeeMaster.EmpNo, EmployeeMaster.EMPName, DailyGroundTransactions.DivisionID, JobMaster.JobName, JobMaster.JobShortName, DailyGroundTransactions.TaskCompleted, CHKCompany.CompanyName", CommandType.Text);
            ds = DataAccess.SQLHelper.FillDataSet("SELECT     dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.FullHalf,  SUM(dbo.DailyGroundTransactions.WorkQty) AS workquantity, dbo.CHKCompany.CompanyName,  dbo.DailyGroundTransactions.CashBlockYesNo,dbo.JobMaster.JobName FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + strDivisionID + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.DivisionID,  dbo.JobMaster.JobShortName, dbo.DailyGroundTransactions.FullHalf, dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.WorkCodeID,dbo.JobMaster.JobName,  dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.CashBlockYesNo HAVING      (dbo.EmployeeMaster.EmpNo LIKE '" + strEmp + "')", CommandType.Text);
            return ds;
        }


    }
}
