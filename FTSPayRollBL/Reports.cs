using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class Reports
    {
        private String strDivisionID;
        public String StrDivisionID
        {
            get { return strDivisionID; }
            set { strDivisionID = value; }
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

        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
        }

        private String strDeductionName;

        public String StrDeductionName
        {
            get { return strDeductionName; }
            set { strDeductionName = value; }
        }

        private String strMonthID;

        public String StrMonthID
        {
            get { return strMonthID; }
            set { strMonthID = value; }
        }

        private Boolean boolAll;

        public Boolean BoolAll
        {
            get { return boolAll; }
            set { boolAll = value; }
        }



        public DataSet DailyWorkDistribution(Int32 MonthId,Int32 YearID, String Div, Int32 intworktype,Boolean boolWithHalf)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                if (boolWithHalf == true)
                {
                    ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,isnull(sum(HolidayHalfNames),0) as HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                    //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
                }
                else
                {
                    ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(case when (dbo.DailyGroundTransactions.ManDays=1.5) then 1 else dbo.DailyGroundTransactions.ManDays end ) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,isnull(sum(HolidayHalfNames),0) as HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                }
            }
            else if (intworktype == 2)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,isnull(sum(HolidayHalfNames),0) as HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID", CommandType.Text);
            }            
            return ds;
        }

        public DataSet DailyWorkDistributionByRange(DateTime dtFrom, DateTime dtTo, String Div, Int32 intworktype,Boolean boolWithHalf)
        {
            DataSet ds = new DataSet();
            
                if (intworktype == 1)
                {
                    if (boolWithHalf == true)
                    {
                        //with 'X%' workcodes - ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,isnull(sum(HolidayHalfNames),0) as HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.DailyGroundTransactions.DateEntered between '" + dtFrom + "' AND  '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                        ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty)  AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.EstateID, ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN '" + dtFrom + "' AND '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + Div + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING        (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')))", CommandType.Text);
                        //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
                    }
                    else
                    {
                        ds = SQLHelper.FillDataSet("SELECT        DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty)  AS wrkqty, SUM(CASE WHEN (dbo.DailyGroundTransactions.ManDays = 1.5) THEN 1 ELSE dbo.DailyGroundTransactions.ManDays END) AS Mandays,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN '" + dtFrom + "' AND '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + Div + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING        (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')))", CommandType.Text);
                    }
                }
                else if (intworktype == 2)
                {
                    //with 'X%' workcodes -ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,isnull(sum(HolidayHalfNames),0) as HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.DailyGroundTransactions.DateEntered between '" + dtFrom + "' AND '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID", CommandType.Text);
                    ds = SQLHelper.FillDataSet("SELECT        DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty)  AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.EstateID, ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM            dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN '" + dtFrom + "' AND '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + Div + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID", CommandType.Text);
                }
            
            return ds;
        }

        public DataSet DailyWorkDistribution(Int32 MonthId, Int32 YearID, Int32 intworktype)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                //ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM  dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                //With %X Work Codes -ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty)  AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty)  AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) ", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
            }
            else if (intworktype == 2)
            {
                //With %X Work Codes -ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM  dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM  dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) ", CommandType.Text);
            }
            return ds;
        }

        public DataSet DailyWorkDistributionByRange(DateTime dtFrom, DateTime dtTo, Int32 intworktype)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                //ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM  dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty)  AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE     (dbo.DailyGroundTransactions.DateEntered = '" + dtFrom + "' AND '" + dtTo + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
            }
            else if (intworktype == 2)
            {
                ds = SQLHelper.FillDataSet("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID,  ISNULL(SUM(dbo.DailyGroundTransactions.HolidayHalfNames), 0) AS HalfNames FROM  dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE      (dbo.DailyGroundTransactions.DateEntered = '" + dtFrom + "' AND '" + dtTo + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING      (NOT (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS'))) AND (NOT (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%'))", CommandType.Text);
            }
            return ds;
        }


        public DataSet DailyWorkDistributionNONWork(Int32 MonthId, Int32 YearID, String Div, Int32 intworktype)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
            }
            else if (intworktype == 2)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
            }
            return ds;
        }

        public DataSet DailyWorkDistributionNONWorkByRange(DateTime dtFrom, DateTime dtTo, String Div, Int32 intworktype)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.DailyGroundTransactions.DateEntered between '" + dtFrom + "' AND  '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
            }
            else if (intworktype == 2)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.DailyGroundTransactions.DateEntered between '" + dtFrom + "' AND  '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID like '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
            }
            return ds;
        }

        public DataSet DailyWorkDistributionNONWork(Int32 MonthId, Int32 YearID, Int32 intworktype)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
            }
            else if (intworktype == 2)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
            }
            return ds;
        }
        public DataSet DailyWorkDistributionNONWorkByRange(DateTime dtFrom, DateTime dtTo, Int32 intworktype)
        {
            DataSet ds = new DataSet();
            if (intworktype == 1)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.DailyGroundTransactions.DateEntered between '" + dtFrom + "' AND  '" + dtTo + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + Div + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType", CommandType.Text);
            }
            else if (intworktype == 2)
            {
                ds = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS DAY, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.CashManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.EstateID FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.DailyGroundTransactions.DateEntered between '" + dtFrom + "' AND  '" + dtTo + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + intworktype + "')  GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.EstateID HAVING (dbo.DailyGroundTransactions.WorkCodeID IN ('ABS')) OR (dbo.DailyGroundTransactions.WorkCodeID LIKE 'X%')", CommandType.Text);
            }
            return ds;
        }

        public DataSet ViewIntakePerPlucker(Int32 MonthId, Int32 YearID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT CONVERT(VarChar(2), MONTH(dbo.DailyGroundTransactions.DateEntered)) + '/' + CONVERT(VarChar(2), DAY(dbo.DailyGroundTransactions.DateEntered)) + '/' + CONVERT(VarChar(4), YEAR(dbo.DailyGroundTransactions.DateEntered)) AS ShortDate, COUNT(dbo.DailyGroundTransactions.EPFNo) AS EmpCount, SUM(dbo.DailyGroundTransactions.WorkQty) AS wrkqty, SUM(dbo.DailyGroundTransactions.ManDays) AS Mandays, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID LEFT OUTER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + YearID + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + MonthId + "') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID, dbo.CHKCompany.CompanyName, dbo.JobGroup.GroupName", CommandType.Text);
            return ds;
        }

        public DataSet ExtraRatesListing()
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT FTSCheckRollExtraRates.WorkCode, FTSCheckRollExtraRates.ExtraRate, FTSCheckRollExtraRates.CreateDateTime, FTSCheckRollExtraRates.UserId,CHKCompany.CompanyName FROM FTSCheckRollExtraRates CROSS JOIN CHKCompany", CommandType.Text);
            return ds;
        }

        public DataSet DivisionWiseDeductionRegisterAllEmp()
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName, dbo.CHKCompany.CompanyName, dbo.CHKEmpDeductions.DeductYear, dbo.CHKMonths.Month, dbo.EstateDivision.DivisionName FROM dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (EmployeeMaster.DivisionID = '" + strDivisionID + "') AND (CHKEmpDeductions.DeductYear = '" + intYear + "') AND  (CHKMonths.MId = '"+IntMonth+"')", CommandType.Text);
            return ds;
        }

        public DataSet DivisionWiseDeductionRegisterAllEmp(String EmpNO)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName,  dbo.CHKCompany.CompanyName, dbo.CHKEmpDeductions.DeductYear, dbo.CHKMonths.Month, dbo.EstateDivision.DivisionName FROM dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (EmployeeMaster.DivisionID = '" + strDivisionID + "') AND (CHKEmpDeductions.DeductYear = '" + intYear + "') AND (CHKMonths.MId = '" + intMonth + "')AND (EmployeeMaster.EmpNo = '" + EmpNO + "')", CommandType.Text);
            return ds;
        }

        public DataSet EmployeeNetPay(String intCat)
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyFinalWeges.WagePay, dbo.CHKMonths.Month, dbo.EmpMonthlyFinalWeges.WageYear, dbo.CHKCompany.CompanyName FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyFinalWeges.WageMonth = dbo.CHKMonths.MId CROSS JOIN dbo.CHKCompany WHERE (dbo.EmpMonthlyFinalWeges.WageYear = '" + IntYear + "') AND (dbo.CHKMonths.MId = '" + IntMonth + "') AND (dbo.EstateDivision.DivisionID = '" + StrDivisionID + "') AND (dbo.EmployeeMaster.ActiveEmployee = 1) ORDER BY dbo.EmployeeMaster.EmpCategory, CONVERT(int, dbo.EmployeeMaster.EmpNo)", CommandType.Text);
            ds = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyFinalWeges.WagePay, dbo.CHKMonths.Month, dbo.EmpMonthlyFinalWeges.WageYear, dbo.CHKCompany.CompanyName,dbo.EmpMonthlyFinalWeges.TotalEarnigs FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyFinalWeges.WageMonth = dbo.CHKMonths.MId AND  dbo.EmpMonthlyFinalWeges.WageYear = dbo.CHKMonths.Year CROSS JOIN dbo.CHKCompany WHERE     (dbo.EmpMonthlyFinalWeges.WageYear = '" + IntYear + "') AND (dbo.CHKMonths.MId = '" + IntMonth + "') AND  (dbo.EstateDivision.DivisionID = '" + StrDivisionID + "')  AND (CONVERT(VARCHAR(10),dbo.EmployeeMaster.EmpCategory) LIKE '" + intCat + "') AND (dbo.EmpMonthlyFinalWeges.WagePay>0) ORDER BY dbo.EmployeeMaster.EmpCategory, CONVERT(int, dbo.EmployeeMaster.EmpNo)", CommandType.Text);
            return ds;
        }

        public DataSet CheckrollWages(DateTime DtFromDate,DateTime DtToDate,String StrDivision,String StrWorkCodeId,String StrUserId)
        {
             String Status = "";
            DataSet ds=new DataSet();
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@wegesFromDate", SqlDbType.VarChar, 50);
            param.Value = DtFromDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@wegesToDate", SqlDbType.DateTime);
            param.Value = DtToDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@workCode", SqlDbType.VarChar, 50);
            param.Value = StrWorkCodeId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userId", SqlDbType.VarChar, 50);
            param.Value = StrUserId;
            paramList.Add(param);         
            ds = SQLHelper.FillDataSet("spGetCheckrollWagesData",CommandType.StoredProcedure,paramList);
            return ds;
        }

        public DataSet DivisionWiseAdditionRegisterAllEmp(String EmpNO)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT EmployeeMaster.EMPName, CHKEmpAdditions.Amount, CHKEmpAdditions.AdditionYear, CHKAddition.AdditionShortName, CHKMonths.Month,EstateDivision.DivisionName, CHKCompany.CompanyName, CHKEmpAdditions.EmpNo FROM CHKEmpAdditions INNER JOIN EmployeeMaster ON CHKEmpAdditions.EmpNo = EmployeeMaster.EmpNo INNER JOIN   CHKAddition ON CHKEmpAdditions.AdditionId = CHKAddition.AdditionId INNER JOIN CHKMonths ON CHKEmpAdditions.AdditionMonth = CHKMonths.MId INNER JOIN EstateDivision ON EmployeeMaster.DivisionID = EstateDivision.DivisionID CROSS JOIN CHKCompany WHERE (CHKEmpAdditions.AdditionMonth = '" + intMonth + "') AND (CHKEmpAdditions.AdditionYear = '" + IntYear + "')AND (CHKEmpAdditions.EmpNo = '" + EmpNO + "')AND  (EstateDivision.DivisionID  = '" + strDivisionID + "')", CommandType.Text);
            return ds;
        }

        public DataSet DivisionWiseAdditionRegisterAllEmp()
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT EmployeeMaster.EMPName, CHKEmpAdditions.Amount, CHKEmpAdditions.AdditionYear, CHKAddition.AdditionShortName, CHKMonths.Month,EstateDivision.DivisionName, CHKCompany.CompanyName, CHKEmpAdditions.EmpNo FROM CHKEmpAdditions INNER JOIN EmployeeMaster ON CHKEmpAdditions.EmpNo = EmployeeMaster.EmpNo INNER JOIN   CHKAddition ON CHKEmpAdditions.AdditionId = CHKAddition.AdditionId INNER JOIN CHKMonths ON CHKEmpAdditions.AdditionMonth = CHKMonths.MId INNER JOIN EstateDivision ON EmployeeMaster.DivisionID = EstateDivision.DivisionID CROSS JOIN CHKCompany WHERE (CHKEmpAdditions.AdditionMonth = '" + intMonth + "') AND (CHKEmpAdditions.AdditionYear = '" + IntYear + "')AND (EstateDivision.DivisionID  = '" + strDivisionID + "')", CommandType.Text);
            ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EMPName, dbo.CHKEmpAdditions.Amount, dbo.CHKEmpAdditions.AdditionYear, dbo.CHKAddition.AdditionShortName, dbo.CHKMonths.Month,  dbo.EstateDivision.DivisionName, dbo.CHKCompany.CompanyName, dbo.CHKEmpAdditions.EmpNo FROM dbo.CHKEmpAdditions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpAdditions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.CHKEmpAdditions.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId INNER JOIN dbo.CHKMonths ON dbo.CHKEmpAdditions.AdditionMonth = dbo.CHKMonths.MId AND dbo.CHKEmpAdditions.AdditionYear = dbo.CHKMonths.Year INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (CHKEmpAdditions.AdditionMonth = '" + intMonth + "') AND (CHKEmpAdditions.AdditionYear = '" + IntYear + "')AND (EstateDivision.DivisionID  = '" + strDivisionID + "')", CommandType.Text);
            return ds;
        }
        /// <summary>
        /// ////2011/05/31 FINISHED//////
        /// </summary>
        /// <param name="Division"></param>
        /// <param name="DeducName"></param>
        /// <param name="MonthID"></param>
        /// <param name="EmpNo"></param>
        /// <returns></returns>

        public DataSet DeducSearch(String Division, String DeducName, String MonthID, String Year, String EmpNo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName FROM dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKDeduction.ShortName = '" + DeducName + "') AND (dbo.CHKMonths.Year = '" + Year + "') AND (dbo.EmployeeMaster.EmpNo = '" + EmpNo + "') AND (dbo.EmployeeMaster.DivisionID = '" + Division + "')", CommandType.Text);
            return ds;

        }

        public DataSet AllDeductEmp(String Division, String Year, String MonthID, String EmpNo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName FROM dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKMonths.Year = '" + Year + "') AND (dbo.EmployeeMaster.EmpNo = '" + EmpNo + "') AND  (dbo.EmployeeMaster.DivisionID = '" + Division + "')", CommandType.Text);
            return ds;
        }

        public DataSet OneDeducAllEmp(String Division, String Year, String MonthID, String DeducName)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName,dbo.CHKEmpDeductions.DivisionId FROM         dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKMonths.Year = '" + Year + "') AND (dbo.EmployeeMaster.DivisionID = '" + Division + "') AND  (dbo.CHKDeduction.ShortName = '" + DeducName + "')", CommandType.Text);
            return ds;
        }
        public DataSet OneDeducAllEmpAllDivision(String Division, String Year, String MonthID, String DeducName)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName,dbo.CHKEmpDeductions.DivisionId FROM dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKDeduction.ShortName = '" + DeducName + "') AND (dbo.CHKMonths.Year = '" + Year + "')", CommandType.Text);
            return ds;
        }
        public DataSet AllDeductAllEmp(String Division, String Year, String MonthID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName,  dbo.EstateDivision.DivisionName FROM         dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKMonths.Year = '" + Year + "') AND (dbo.EmployeeMaster.DivisionID = '" + Division + "') ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
            return ds;
        }

        public DataSet EmpRangeOneDeduc(String Division, String Year, String MonthID, String DeducName, String FromEmpNo, String ToEmpNo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName FROM         dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKMonths.Year = '" + Year + "') AND (dbo.EmployeeMaster.EmpNo BETWEEN '" + FromEmpNo + "' AND  '" + ToEmpNo + "') AND (dbo.CHKDeduction.ShortName = '" + DeducName + "') AND (dbo.EstateDivision.DivisionID = '" + Division + "') ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
            return ds;
        }

        public DataSet EmpRangeAllDeduc(String Division, String Year, String MonthID, String FromEmpNo, String ToEmpNo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKDeduction.ShortName,  dbo.EstateDivision.DivisionName FROM         dbo.EmployeeMaster INNER JOIN dbo.CHKEmpDeductions ON dbo.EmployeeMaster.EmpNo = dbo.CHKEmpDeductions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.CHKEmpDeductions.DivisionId INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKMonths ON dbo.CHKEmpDeductions.DeductMonth = dbo.CHKMonths.MId INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.CHKMonths.Month = '" + MonthID + "') AND (dbo.CHKMonths.Year = '" + Year + "') AND (dbo.EmployeeMaster.EmpNo BETWEEN '" + FromEmpNo + "' AND  '" + ToEmpNo + "') AND (dbo.EstateDivision.DivisionID = '" + Division + "')", CommandType.Text);
            return ds;
        }

        public DataSet LoanDeductionList(String strDiv, String Year, String MonthID, String strDeduction, String strDeductionGroup)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     dbo.CHKEmpDeductions.DeductYear, dbo.CHKEmpDeductions.DeductMonth, dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKEmpDeductions.DeductIdNo, dbo.CHKLoan.AccountNo FROM dbo.CHKEmpDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKLoan ON dbo.CHKEmpDeductions.DeductIdNo = dbo.CHKLoan.LoanCode WHERE     (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + strDiv + "') and  (dbo.CHKEmpDeductions.DeductGroupId = '" + strDeductionGroup + "') AND (dbo.CHKEmpDeductions.DeductId = '" + strDeduction + "')", CommandType.Text);
            return ds;
        }

        public DataSet LoanDeductionList(String Year, String MonthID, String strDeductionGroup, String strDeduction)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     dbo.CHKEmpDeductions.DeductYear, dbo.CHKEmpDeductions.DeductMonth, dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKEmpDeductions.Amount, dbo.CHKEmpDeductions.DeductIdNo, dbo.CHKLoan.AccountNo FROM dbo.CHKEmpDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKLoan ON dbo.CHKEmpDeductions.DeductIdNo = dbo.CHKLoan.LoanCode WHERE     (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "')  and  (dbo.CHKEmpDeductions.DeductGroupId = '" + strDeductionGroup + "') AND (dbo.CHKEmpDeductions.DeductId = '" + strDeduction + "')", CommandType.Text);
            
            return ds;
        }
        public DataSet payeeLoanDeductionList(String Year, String MonthID, String strDeductionGroup, String strDeduction)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT    DivisionID, EmpNo, PayeeName, PayeeAccount, PayeeAmount FROM  dbo.CHKLoanPayeeDetails WHERE   (StartYear = '"+Year+"') AND (StartMonth = '"+MonthID+"') AND (DeductionGroupId = '"+strDeductionGroup+"') AND (DeductId = '"+strDeduction+"')", CommandType.Text);

            return ds;
        }
        public DataSet payeeLoanDeductionList(String strDiv, String Year, String MonthID, String strDeduction, String strDeductionGroup)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.CHKLoan.DeductionGroup, dbo.CHKLoan.DeductionCode, dbo.CHKDeduction.ShortName,  dbo.CHKDeduction.DeductionName, dbo.CHKLoanPayeeDetails.PayeeAccount, dbo.CHKLoanPayeeDetails.PayeeName,  dbo.CHKLoanPayeeDetails.PayeeAmount FROM         dbo.CHKEmpDeductions INNER JOIN dbo.CHKLoan ON dbo.CHKEmpDeductions.DeductIdNo = dbo.CHKLoan.LoanCode INNER JOIN dbo.CHKLoanPayeeDetails ON dbo.CHKLoan.DivisionCode = dbo.CHKLoanPayeeDetails.DivisionID AND  dbo.CHKLoan.EmployeeNo = dbo.CHKLoanPayeeDetails.EmpNo AND dbo.CHKLoan.StartYear = dbo.CHKLoanPayeeDetails.StartYear AND  dbo.CHKLoan.StartMonth = dbo.CHKLoanPayeeDetails.StartMonth AND dbo.CHKLoan.DeductionGroup = dbo.CHKLoanPayeeDetails.DeductionGroupId AND  dbo.CHKLoan.DeductionCode = dbo.CHKLoanPayeeDetails.DeductId INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode AND  dbo.CHKLoan.DeductionGroup = dbo.CHKDeduction.DeductionGroupCode WHERE   (dbo.CHKEmpDeductions.DivisionId like '"+strDiv+"') AND  (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductGroupId = '" + strDeductionGroup + "') AND  (dbo.CHKEmpDeductions.DeductId = '" + strDeduction + "')", CommandType.Text);
            return ds;
        }
        //-----new Deduction Search Query....
        public DataSet GetDeductionSearchData(String Div,String DeductCode,String Emp, String Year, String MonthID)
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.EmpNo LIKE '" + Emp + "') AND (dbo.CHKEmpDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "') ORDER BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo", CommandType.Text);
            ds = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') GROUP BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName,  dbo.CHKEmpDeductions.Amount HAVING      (dbo.CHKEmpDeductions.EmpNo LIKE '" + Emp + "') AND (dbo.CHKEmpDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "') ORDER BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo", CommandType.Text);
            return ds;
        }

        public DataSet GetDeductionSearchDataIncludingSkippedDeductions(String Div, String DeductCode, String Emp, String Year, String MonthID)
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.EmpNo LIKE '" + Emp + "') AND (dbo.CHKEmpDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "') ORDER BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo", CommandType.Text);
            ds = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount, 'Deducted' as Type1 FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') GROUP BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName,  dbo.CHKEmpDeductions.Amount HAVING      (dbo.CHKEmpDeductions.EmpNo LIKE '" + Emp + "') AND (dbo.CHKEmpDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "')  union  SELECT     dbo.CHKSystemSkipedDeductions.DivisionId, dbo.CHKSystemSkipedDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName,  dbo.CHKSystemSkipedDeductions.DeductAmount, 'Skipped' as Type1 FROM  dbo.CHKSystemSkipedDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKSystemSkipedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKSystemSkipedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKDeduction ON dbo.CHKSystemSkipedDeductions.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKSystemSkipedDeductions.DeductionCode = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKSystemSkipedDeductions.Year = '" + Year + "') AND (dbo.CHKSystemSkipedDeductions.Month = '" + MonthID + "') GROUP BY dbo.CHKSystemSkipedDeductions.DivisionId, dbo.CHKSystemSkipedDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName,  dbo.CHKSystemSkipedDeductions.DeductAmount HAVING      (dbo.CHKSystemSkipedDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKSystemSkipedDeductions.EmpNo LIKE '" + Emp + "') AND  (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "') ", CommandType.Text);
            return ds;
        }


        public DataSet GetDeductionSearchDataByEmpNoRange(String Div, String DeductCode, String Emp, String Year, String MonthID, String empFrom, String empTo)
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.EmpNo LIKE '" + Emp + "') AND (dbo.CHKEmpDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "') ORDER BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo", CommandType.Text);
            ds = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName, dbo.CHKEmpDeductions.Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpDeductions.DeductMonth = '" + MonthID + "') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') GROUP BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKDeduction.ShortName,  dbo.CHKEmpDeductions.Amount HAVING        (dbo.CHKEmpDeductions.EmpNo BETWEEN '" + empFrom + "' AND '" + empTo + "') AND (dbo.CHKEmpDeductions.DivisionId LIKE '" + Div + "') AND (dbo.CHKDeduction.ShortName LIKE '" + DeductCode + "') ORDER BY dbo.CHKEmpDeductions.DivisionId, dbo.CHKEmpDeductions.EmpNo", CommandType.Text);
            return ds;
        }

        public DataSet GetEPF_EVEMC_Data(String ConPeriod)
        {
            Int32 intDataSubmissionNo = 0;
            Decimal decTotContribution = 0;
            Int32 intMemberCount = 0;
            DataSet dsEVEMC = new DataSet();


            DataTable EVEMP = new DataTable();
            EVEMP.Columns.Add(new DataColumn("ZoneCode"));//0
            EVEMP.Columns.Add(new DataColumn("NICEmployerNo"));
            EVEMP.Columns.Add(new DataColumn("ContributionPeriod"));//2
            EVEMP.Columns.Add(new DataColumn("SubmissionNo"));
            EVEMP.Columns.Add(new DataColumn("TotalContribution"));//4
            EVEMP.Columns.Add(new DataColumn("MemberCount"));
            EVEMP.Columns.Add(new DataColumn("PayMode"));//6
            EVEMP.Columns.Add(new DataColumn("PayRef"));
            EVEMP.Columns.Add(new DataColumn("PayDate"));//8
            EVEMP.Columns.Add(new DataColumn("DistrictCode"));
            EVEMP.Columns.Add(new DataColumn("EstateID"));

            DataTable EVEMC = new DataTable();
            EVEMC.Columns.Add(new DataColumn("EstateID"));//0
            EVEMC.Columns.Add(new DataColumn("NICNo"));
            EVEMC.Columns.Add(new DataColumn("Surname"));//2
            EVEMC.Columns.Add(new DataColumn("Initials"));
            EVEMC.Columns.Add(new DataColumn("MemberNo"));//4
            EVEMC.Columns.Add(new DataColumn("TotalContribution"));
            EVEMC.Columns.Add(new DataColumn("EmployerContribution"));//6
            EVEMC.Columns.Add(new DataColumn("EmployeeContribution"));
            EVEMC.Columns.Add(new DataColumn("TotalEarnings"));//8
            EVEMC.Columns.Add(new DataColumn("MemberStatus"));
            EVEMC.Columns.Add(new DataColumn("Zone"));//10
            EVEMC.Columns.Add(new DataColumn("EmployerNo"));//11
            EVEMC.Columns.Add(new DataColumn("ContributionPeriod"));
            EVEMC.Columns.Add(new DataColumn("DataSubmissionNo"));
            EVEMC.Columns.Add(new DataColumn("NoOfDaysWorked"));//14
            EVEMC.Columns.Add(new DataColumn("OCGrade"));
            EVEMC.Columns.Add(new DataColumn("DistrictCode"));//16

            SqlDataReader dataReader;
            SqlDataReader readerEVEMP;
            DataRow dtrow1;
            dtrow1 = EVEMP.NewRow();
            DataRow dtrow;
            dtrow = EVEMC.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT AutoKey, EstateID, EmployerNO, ZoneCode, PayMode, PaymentRef, DistrictOfficeCode FROM dbo.CHKEPFEmployer ORDER BY AutoKey", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow1 = EVEMP.NewRow();
                decTotContribution = 0;
                DataSet ds = new DataSet();
                //ds = SQLHelper.FillDataSet("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100)  > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') and (dbo.EmployeeMaster.ActiveEmployee=1)", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100)  > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') ", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100)  > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') ", CommandType.Text);
                //ds = SQLHelper.FillDataSet("SELECT        CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')  END AS NICNO, dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 + (OtherEPFPay * 22 / 100)  ELSE dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 END AS totalContribution, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPF12 + (OtherEPFPay * 12 / 100) ELSE dbo.EmpMonthlyEarnings.EPF12 END AS EmployerContribution,  CASE WHEN (isnull(OtherEPFPay, 0) > 0) THEN dbo.EmpMonthlyEarnings.EPF10 + (OtherEPFPay * 10 / 100)  ELSE dbo.EmpMonthlyEarnings.EPF10 END AS EmployeeContribution, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPFPaybleAmount + (OtherEPFPay) ELSE dbo.EmpMonthlyEarnings.EPFPaybleAmount END AS TotalEarnings,  ISNULL(dbo.EmployeeMaster.MemberStatus, 'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM            dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE        ((dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10) + ISNULL(dbo.EmpMonthlyEarnings.OtherEPFPay, 0) * 22 / 100 > 0) AND  (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') AND (dbo.EmployeeCategory.EPFEntitled = 1) ", CommandType.Text);
                ds = SQLHelper.FillDataSet("SELECT        CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')  END AS NICNO, dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 + (OtherEPFPay * 22 / 100)  ELSE dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 END AS totalContribution, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPF12 + (OtherEPFPay * 12 / 100) ELSE dbo.EmpMonthlyEarnings.EPF12 END AS EmployerContribution,  CASE WHEN (isnull(OtherEPFPay, 0) > 0) THEN dbo.EmpMonthlyEarnings.EPF10 + (OtherEPFPay * 10 / 100)  ELSE dbo.EmpMonthlyEarnings.EPF10 END AS EmployeeContribution, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPFPaybleAmount + (OtherEPFPay) ELSE dbo.EmpMonthlyEarnings.EPFPaybleAmount END AS TotalEarnings,  ISNULL(dbo.EmployeeMaster.MemberStatus, 'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM            dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE        ((dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10) + ISNULL(dbo.EmpMonthlyEarnings.OtherEPFPay, 0) * 22 / 100 > 0) AND  (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') AND (dbo.EmployeeCategory.EPFEntitled = 1) ", CommandType.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    intDataSubmissionNo++;
                }
                SqlDataReader readerEVEMC;
                //readerEVEMC = SQLHelper.ExecuteReader("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100) > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') and (dbo.EmployeeMaster.ActiveEmployee=1)", CommandType.Text);
                //readerEVEMC = SQLHelper.ExecuteReader("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100) > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') ", CommandType.Text);
                readerEVEMC = SQLHelper.ExecuteReader("SELECT        CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')  END AS NICNO, dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 + (OtherEPFPay * 22 / 100)  ELSE dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 END AS totalContribution, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPF12 + (OtherEPFPay * 12 / 100) ELSE dbo.EmpMonthlyEarnings.EPF12 END AS EmployerContribution,  CASE WHEN (isnull(OtherEPFPay, 0) > 0) THEN dbo.EmpMonthlyEarnings.EPF10 + (OtherEPFPay * 10 / 100)  ELSE dbo.EmpMonthlyEarnings.EPF10 END AS EmployeeContribution, CASE WHEN (isnull(OtherEPFPay, 0) > 0)  THEN dbo.EmpMonthlyEarnings.EPFPaybleAmount + (OtherEPFPay) ELSE dbo.EmpMonthlyEarnings.EPFPaybleAmount END AS TotalEarnings,  ISNULL(dbo.EmployeeMaster.MemberStatus, 'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM            dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE        ((dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10) + ISNULL(dbo.EmpMonthlyEarnings.OtherEPFPay, 0) * 22 / 100 > 0) AND  (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "') AND (dbo.EmployeeCategory.EPFEntitled = 1) ", CommandType.Text);

                while (readerEVEMC.Read())
                {
                    dtrow = EVEMC.NewRow();

                    if (!dataReader.IsDBNull(0))
                    {
                        dtrow[0] = dataReader.GetString(1);
                    }
                    if (!dataReader.IsDBNull(2))
                    {
                        dtrow[11] = dataReader.GetString(2);
                    }
                    if (!readerEVEMC.IsDBNull(0))
                    {
                        dtrow[1] = readerEVEMC.GetString(0);
                    }
                    if (!readerEVEMC.IsDBNull(1))
                    {
                        dtrow[2] = readerEVEMC.GetString(1);
                    }
                    if (!readerEVEMC.IsDBNull(2))
                    {
                        dtrow[3] = readerEVEMC.GetString(2);
                    }
                    if (!readerEVEMC.IsDBNull(3))
                    {
                        if (readerEVEMC.GetString(3).Length > 6)
                        {
                            dtrow[4] = readerEVEMC.GetString(3).Substring(readerEVEMC.GetString(3).Length - 6, 6);
                        }
                        else
                            dtrow[4] = readerEVEMC.GetString(3);
                    }
                    if (!readerEVEMC.IsDBNull(4))
                    {
                        dtrow[5] = readerEVEMC.GetDecimal(4);
                        decTotContribution = decTotContribution + readerEVEMC.GetDecimal(4);
                        intMemberCount = intMemberCount + 1;
                    }
                    if (!readerEVEMC.IsDBNull(5))
                    {
                        dtrow[6] = readerEVEMC.GetDecimal(5);
                    }
                    if (!readerEVEMC.IsDBNull(6))
                    {
                        dtrow[7] = readerEVEMC.GetDecimal(6);
                    }
                    if (!readerEVEMC.IsDBNull(7))
                    {
                        dtrow[8] = readerEVEMC.GetDecimal(7);
                    }
                    if (!readerEVEMC.IsDBNull(8))
                    {
                        dtrow[9] = readerEVEMC.GetString(8);
                    }
                    if (!readerEVEMC.IsDBNull(9))
                    {
                        dtrow[10] = readerEVEMC.GetString(9);
                    }
                    if (!readerEVEMC.IsDBNull(11))
                    {
                        dtrow[12] = readerEVEMC.GetString(11);
                    }
                    if (!readerEVEMC.IsDBNull(12))
                    {
                        //dtrow[13] = intDataSubmissionNo.ToString().PadLeft(2, '0');
                        dtrow[13] = "1".PadLeft(2, '0');
                    }
                    if (!readerEVEMC.IsDBNull(13))
                    {
                        dtrow[14] = readerEVEMC.GetDecimal(13);
                    }
                    if (!readerEVEMC.IsDBNull(14))
                    {
                        dtrow[15] = readerEVEMC.GetString(14);
                    }
                    if (!dataReader.IsDBNull(6))
                    {
                        dtrow[16] = dataReader.GetString(6);
                    }
                    EVEMC.Rows.Add(dtrow);
                }
                readerEVEMC.Close();

                if (!dataReader.IsDBNull(3))
                {
                    dtrow1[0] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow1[1] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow1[2] = ConPeriod;
                }
                if (!dataReader.IsDBNull(3))
                {
                    //dtrow1[3] = intDataSubmissionNo.ToString();
                    dtrow1[3] = "1".PadLeft(2, '0');
                }
                dtrow1[4] = decTotContribution;//total contribution
                dtrow1[5] = intMemberCount;//mem count
                if (!dataReader.IsDBNull(4))
                {
                    dtrow1[6] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow1[7] = dataReader.GetString(5);
                }
                dtrow1[8] = DateTime.Now.Year.ToString().PadLeft(4, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                if (!dataReader.IsDBNull(6))
                {
                    dtrow1[9] = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow1[10] = dataReader.GetString(1);
                }
                EVEMP.Rows.Add(dtrow1);
            }

            dataReader.Close();
            dsEVEMC.Tables.Add(EVEMC);
            dsEVEMC.Tables.Add(EVEMP);

            SQLHelper.ExecuteNonQuery("DELETE  FROM dbo.CHKEVEMC WHERE (ContributionPeriod = '" + ConPeriod + "')", CommandType.Text);
            foreach (DataRow dr in EVEMC.Rows)
            {
                try
                {
                    //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMC] ([NICNo] ,[Surname] ,[Initials] ,[MemberNo] ,[TotalContribution] ,[EmployerContribution] , [EmployeeContribution] ,[TotalEarnings] ,[MemberStatus] ,[Zone] ,[EmployerNo] ,[ContributionPeriod] , [DataSubmissionNo] ,[NoOfDaysWorked] ,[OCGrade],UserID,DistrictCode)  VALUES ('" + dr.ItemArray[1].ToString().PadRight(20, ' ') + "'  ,'" + dr.ItemArray[2].ToString().PadRight(40, ' ') + "' ,'" + dr.ItemArray[3].ToString().PadRight(20, ' ') + "'  ,'" + dr.ItemArray[4].ToString().PadLeft(6, '0') + "' ,'" + dr.ItemArray[5].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[6].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[7].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[8].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[9].ToString().PadLeft(1, 'E') + "'  ,'" + dr.ItemArray[10].ToString().PadLeft(1, ' ') + "'  ,'" + dr.ItemArray[11].ToString().PadLeft(6, '0') + "'  ,'" + dr.ItemArray[12].ToString().PadLeft(6, ' ') + "'  ,'" + dr.ItemArray[13].ToString().PadLeft(2, '0') + "'  ,'" + dr.ItemArray[14].ToString().PadLeft(7, '0') + "'  ,'" + dr.ItemArray[15].ToString().PadLeft(3, '0') + "','" + User.StrUserName + "','" + dr.ItemArray[16].ToString().PadLeft(2, '0') + "' ) ", CommandType.Text);
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMC] ([NICNo] ,[Surname] ,[Initials] ,[MemberNo] ,[TotalContribution] ,[EmployerContribution] , [EmployeeContribution] ,[TotalEarnings] ,[MemberStatus] ,[Zone] ,[EmployerNo] ,[ContributionPeriod] , [DataSubmissionNo] ,[NoOfDaysWorked] ,[OCGrade],UserID,DistrictCode)  VALUES ('" + dr.ItemArray[1].ToString() + "'  ,'" + dr.ItemArray[2].ToString() + "' ,'" + dr.ItemArray[3].ToString() + "'  ,'" + dr.ItemArray[4].ToString() + "' ,'" + dr.ItemArray[5].ToString() + "'  ,'" + dr.ItemArray[6].ToString() + "'  ,'" + dr.ItemArray[7].ToString()+ "'  ,'" + dr.ItemArray[8].ToString() + "'  ,'" + dr.ItemArray[9].ToString().PadLeft(1, 'E') + "'  ,'" + dr.ItemArray[10].ToString().PadLeft(1, ' ') + "'  ,'" + dr.ItemArray[11].ToString()+ "'  ,'" + dr.ItemArray[12].ToString() + "'  ,'" + dr.ItemArray[13].ToString() + "'  ,'" + dr.ItemArray[14].ToString() + "'  ,'" + dr.ItemArray[15].ToString()+ "','" + User.StrUserName + "','" + dr.ItemArray[16].ToString() + "' ) ", CommandType.Text);
                    //"VALUES ('" + dr[0].ToString().PadLeft(20,'0') + "'  ,'" + dr[1].ToString().PadLeft(40,' ') + "' ,'" + dr[2].ToString().PadLeft(20,' ') + "'  ,'" + dr[3].ToString().PadLeft(6,'0') + "' ,'" + dr[4].ToString().PadLeft(12,'0') + "'  ,'" + dr[5].ToString().PadLeft(12,'0')+ "'  ,'" + dr[6].ToString().PadLeft(12,'0') + "'  ,'" + dr[7].ToString().PadLeft(12,'0') + "'  ,'" + dr[8].ToString().PadLeft(1,'E') + "'  ,'" + dr[9].ToString().PadLeft(1,' ') + "'  ,'" + dr[10].ToString().PadLeft(6,'0') + "'  ,'" + dr[11].ToString().PadLeft(6,' ') + "'  ,'" + dr[12].ToString().PadLeft(2,'0') + "'  ,'" + dr[13].ToString().PadLeft(7,'0') + "'  ,'" + dr[13].ToString().PadLeft(3,'0') + "' ) ", CommandType.Text);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ErrorLog.txt", true))
                    {
                        file.WriteLine("Error" + ex.Message + " NIC : " + dr.ItemArray[1].ToString().PadLeft(20, '0') + " Member : " + dr.ItemArray[4].ToString().PadLeft(6, '0') + "'");
                    }
                }
            }
            SQLHelper.ExecuteNonQuery("DELETE  FROM dbo.CHKEVEMP WHERE (ContributionPeriod = '" + ConPeriod + "')", CommandType.Text);
            foreach (DataRow dr1 in EVEMP.Rows)
            {
                try
                {
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMP] ([ZoneCode] ,[EmployerNo] ,[ContributionPeriod] ,[SubmissionId] ,[TotalContribution] ,[MemberCount] ,[PayMode] ,[PayRef] ,[PayDate] ,[DistrictCode] ,[EstateID],UserID) VALUES ('" + dr1.ItemArray[0].ToString().PadLeft(1, ' ') + "' ,'" + dr1.ItemArray[1].ToString().PadLeft(6, '0') + "' ,'" + dr1.ItemArray[2].ToString().PadLeft(6, '0') + "' ,'" + dr1.ItemArray[3].ToString().PadLeft(2, '0') + "' ,'" + dr1.ItemArray[4].ToString().PadLeft(14, '0') + "' ,'" + dr1.ItemArray[5].ToString().PadLeft(5, '0') + "' ,'" + dr1.ItemArray[6].ToString().PadLeft(1, ' ') + "' ,'" + dr1.ItemArray[7].ToString().PadLeft(20, '0') + "' ,'" + dr1.ItemArray[8].ToString().PadLeft(10, ' ') + "' ,'" + dr1.ItemArray[9].ToString().PadLeft(2, '0') + "' ,'" + dr1.ItemArray[10].ToString().PadLeft(2, ' ') + "','" + User.StrUserName + "') ", CommandType.Text);
                    //"VALUES ('" + dr[0].ToString().PadLeft(20,'0') + "'  ,'" + dr[1].ToString().PadLeft(40,' ') + "' ,'" + dr[2].ToString().PadLeft(20,' ') + "'  ,'" + dr[3].ToString().PadLeft(6,'0') + "' ,'" + dr[4].ToString().PadLeft(12,'0') + "'  ,'" + dr[5].ToString().PadLeft(12,'0')+ "'  ,'" + dr[6].ToString().PadLeft(12,'0') + "'  ,'" + dr[7].ToString().PadLeft(12,'0') + "'  ,'" + dr[8].ToString().PadLeft(1,'E') + "'  ,'" + dr[9].ToString().PadLeft(1,' ') + "'  ,'" + dr[10].ToString().PadLeft(6,'0') + "'  ,'" + dr[11].ToString().PadLeft(6,' ') + "'  ,'" + dr[12].ToString().PadLeft(2,'0') + "'  ,'" + dr[13].ToString().PadLeft(7,'0') + "'  ,'" + dr[13].ToString().PadLeft(3,'0') + "' ) ", CommandType.Text);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ErrorLog.txt", true))
                    {
                        file.WriteLine("Error" + ex.Message + " employerNo : " + dr1.ItemArray[1].ToString() + " SubmissionId : " + dr1.ItemArray[3].ToString() + "'");
                    }
                }
                //WRITE INTO THE FILE

                //
            }

            return dsEVEMC;

        }

        public DataSet GetEPF_EVEMC_Data_ETF(String ConPeriod)
        {
            Int32 intDataSubmissionNo = 0;
            Decimal decTotContribution = 0;
            Int32 intMemberCount = 0;
            DataSet dsEVEMC = new DataSet();


            DataTable EVEMP = new DataTable();
            EVEMP.Columns.Add(new DataColumn("ZoneCode"));//0
            EVEMP.Columns.Add(new DataColumn("NICEmployerNo"));
            EVEMP.Columns.Add(new DataColumn("ContributionPeriod"));//2
            EVEMP.Columns.Add(new DataColumn("SubmissionNo"));
            EVEMP.Columns.Add(new DataColumn("TotalContribution"));//4
            EVEMP.Columns.Add(new DataColumn("MemberCount"));
            EVEMP.Columns.Add(new DataColumn("PayMode"));//6
            EVEMP.Columns.Add(new DataColumn("PayRef"));
            EVEMP.Columns.Add(new DataColumn("PayDate"));//8
            EVEMP.Columns.Add(new DataColumn("DistrictCode"));
            EVEMP.Columns.Add(new DataColumn("EstateID"));

            DataTable EVEMC = new DataTable();
            EVEMC.Columns.Add(new DataColumn("EstateID"));//0
            EVEMC.Columns.Add(new DataColumn("NICNo"));
            EVEMC.Columns.Add(new DataColumn("Surname"));//2
            EVEMC.Columns.Add(new DataColumn("Initials"));
            EVEMC.Columns.Add(new DataColumn("MemberNo"));//4
            EVEMC.Columns.Add(new DataColumn("TotalContribution"));
            EVEMC.Columns.Add(new DataColumn("EmployerContribution"));//6
            EVEMC.Columns.Add(new DataColumn("EmployeeContribution"));
            EVEMC.Columns.Add(new DataColumn("TotalEarnings"));//8
            EVEMC.Columns.Add(new DataColumn("MemberStatus"));
            EVEMC.Columns.Add(new DataColumn("Zone"));//10
            EVEMC.Columns.Add(new DataColumn("EmployerNo"));//11
            EVEMC.Columns.Add(new DataColumn("ContributionPeriod"));
            EVEMC.Columns.Add(new DataColumn("DataSubmissionNo"));
            EVEMC.Columns.Add(new DataColumn("NoOfDaysWorked"));//14
            EVEMC.Columns.Add(new DataColumn("OCGrade"));
            EVEMC.Columns.Add(new DataColumn("DistrictCode"));//16

            SqlDataReader dataReader;
            SqlDataReader readerEVEMP;
            DataRow dtrow1;
            dtrow1 = EVEMP.NewRow();
            DataRow dtrow;
            dtrow = EVEMC.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT AutoKey, EstateID, EmployerNO, ZoneCode, PayMode, PaymentRef, DistrictOfficeCode FROM dbo.CHKEPFEmployer ORDER BY AutoKey", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow1 = EVEMP.NewRow();
                decTotContribution = 0;
                DataSet ds = new DataSet();
                ds = SQLHelper.FillDataSet("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100)  > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "')", CommandType.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    intDataSubmissionNo++;
                }
                SqlDataReader readerEVEMC;
                readerEVEMC = SQLHelper.ExecuteReader("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100) > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "')", CommandType.Text);

                while (readerEVEMC.Read())
                {
                    dtrow = EVEMC.NewRow();

                    if (!dataReader.IsDBNull(0))
                    {
                        dtrow[0] = dataReader.GetString(1);
                    }
                    if (!dataReader.IsDBNull(2))
                    {
                        dtrow[11] = dataReader.GetString(2);
                    }
                    if (!readerEVEMC.IsDBNull(0))
                    {
                        dtrow[1] = readerEVEMC.GetString(0);
                    }
                    if (!readerEVEMC.IsDBNull(1))
                    {
                        dtrow[2] = readerEVEMC.GetString(1);
                    }
                    if (!readerEVEMC.IsDBNull(2))
                    {
                        dtrow[3] = readerEVEMC.GetString(2);
                    }
                    if (!readerEVEMC.IsDBNull(3))
                    {
                        if (readerEVEMC.GetString(3).Length > 6)
                        {
                            dtrow[4] = readerEVEMC.GetString(3).Substring(readerEVEMC.GetString(3).Length - 6, 6);
                        }
                        else
                            dtrow[4] = readerEVEMC.GetString(3);
                    }
                    if (!readerEVEMC.IsDBNull(4))
                    {
                        dtrow[5] = readerEVEMC.GetDecimal(4);
                        decTotContribution = decTotContribution + readerEVEMC.GetDecimal(4);
                        intMemberCount = intMemberCount + 1;
                    }
                    if (!readerEVEMC.IsDBNull(5))
                    {
                        dtrow[6] = readerEVEMC.GetDecimal(5);
                    }
                    if (!readerEVEMC.IsDBNull(6))
                    {
                        dtrow[7] = readerEVEMC.GetDecimal(6);
                    }
                    if (!readerEVEMC.IsDBNull(7))
                    {
                        dtrow[8] = readerEVEMC.GetDecimal(7);
                    }
                    if (!readerEVEMC.IsDBNull(8))
                    {
                        dtrow[9] = readerEVEMC.GetString(8);
                    }
                    if (!readerEVEMC.IsDBNull(9))
                    {
                        dtrow[10] = readerEVEMC.GetString(9);
                    }
                    if (!readerEVEMC.IsDBNull(11))
                    {
                        dtrow[12] = readerEVEMC.GetString(11);
                    }
                    if (!readerEVEMC.IsDBNull(12))
                    {
                        //dtrow[13] = intDataSubmissionNo.ToString().PadLeft(2, '0');
                        dtrow[13] = "1".PadLeft(2, '0');
                    }
                    if (!readerEVEMC.IsDBNull(13))
                    {
                        dtrow[14] = readerEVEMC.GetDecimal(13);
                    }
                    if (!readerEVEMC.IsDBNull(14))
                    {
                        dtrow[15] = readerEVEMC.GetString(14);
                    }
                    if (!dataReader.IsDBNull(6))
                    {
                        dtrow[16] = dataReader.GetString(6);
                    }
                    EVEMC.Rows.Add(dtrow);
                }
                readerEVEMC.Close();

                if (!dataReader.IsDBNull(3))
                {
                    dtrow1[0] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow1[1] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow1[2] = ConPeriod;
                }
                if (!dataReader.IsDBNull(3))
                {
                    //dtrow1[3] = intDataSubmissionNo.ToString();
                    dtrow1[3] = "1".PadLeft(2, '0');
                }
                dtrow1[4] = decTotContribution;//total contribution
                dtrow1[5] = intMemberCount;//mem count
                if (!dataReader.IsDBNull(4))
                {
                    dtrow1[6] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow1[7] = dataReader.GetString(5);
                }
                dtrow1[8] = DateTime.Now.Year.ToString().PadLeft(4, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                if (!dataReader.IsDBNull(6))
                {
                    dtrow1[9] = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow1[10] = dataReader.GetString(1);
                }
                EVEMP.Rows.Add(dtrow1);
            }

            dataReader.Close();
            dsEVEMC.Tables.Add(EVEMC);
            dsEVEMC.Tables.Add(EVEMP);

            SQLHelper.ExecuteNonQuery("DELETE  FROM dbo.CHKEVEMC WHERE (ContributionPeriod = '" + ConPeriod + "')", CommandType.Text);
            foreach (DataRow dr in EVEMC.Rows)
            {
                try
                {
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMC] ([NICNo] ,[Surname] ,[Initials] ,[MemberNo] ,[TotalContribution] ,[EmployerContribution] , [EmployeeContribution] ,[TotalEarnings] ,[MemberStatus] ,[Zone] ,[EmployerNo] ,[ContributionPeriod] , [DataSubmissionNo] ,[NoOfDaysWorked] ,[OCGrade],UserID,DistrictCode)  VALUES ('" + dr.ItemArray[1].ToString().PadRight(20, ' ') + "'  ,'" + dr.ItemArray[2].ToString().PadRight(40, ' ') + "' ,'" + dr.ItemArray[3].ToString().PadRight(20, ' ') + "'  ,'" + dr.ItemArray[4].ToString().PadLeft(6, '0') + "' ,'" + dr.ItemArray[5].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[6].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[7].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[8].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[9].ToString().PadLeft(1, 'E') + "'  ,'" + dr.ItemArray[10].ToString().PadLeft(1, ' ') + "'  ,'" + dr.ItemArray[11].ToString().PadLeft(6, '0') + "'  ,'" + dr.ItemArray[12].ToString().PadLeft(6, ' ') + "'  ,'" + dr.ItemArray[13].ToString().PadLeft(2, '0') + "'  ,'" + dr.ItemArray[14].ToString().PadLeft(7, '0') + "'  ,'" + dr.ItemArray[15].ToString().PadLeft(3, '0') + "','" + User.StrUserName + "','" + dr.ItemArray[16].ToString().PadLeft(2, '0') + "' ) ", CommandType.Text);
                    //"VALUES ('" + dr[0].ToString().PadLeft(20,'0') + "'  ,'" + dr[1].ToString().PadLeft(40,' ') + "' ,'" + dr[2].ToString().PadLeft(20,' ') + "'  ,'" + dr[3].ToString().PadLeft(6,'0') + "' ,'" + dr[4].ToString().PadLeft(12,'0') + "'  ,'" + dr[5].ToString().PadLeft(12,'0')+ "'  ,'" + dr[6].ToString().PadLeft(12,'0') + "'  ,'" + dr[7].ToString().PadLeft(12,'0') + "'  ,'" + dr[8].ToString().PadLeft(1,'E') + "'  ,'" + dr[9].ToString().PadLeft(1,' ') + "'  ,'" + dr[10].ToString().PadLeft(6,'0') + "'  ,'" + dr[11].ToString().PadLeft(6,' ') + "'  ,'" + dr[12].ToString().PadLeft(2,'0') + "'  ,'" + dr[13].ToString().PadLeft(7,'0') + "'  ,'" + dr[13].ToString().PadLeft(3,'0') + "' ) ", CommandType.Text);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ErrorLog.txt", true))
                    {
                        file.WriteLine("Error" + ex.Message + " NIC : " + dr.ItemArray[1].ToString().PadLeft(20, '0') + " Member : " + dr.ItemArray[4].ToString().PadLeft(6, '0') + "'");
                    }
                }
            }
            SQLHelper.ExecuteNonQuery("DELETE  FROM dbo.CHKEVEMP WHERE (ContributionPeriod = '" + ConPeriod + "')", CommandType.Text);
            foreach (DataRow dr1 in EVEMP.Rows)
            {
                try
                {
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMP] ([ZoneCode] ,[EmployerNo] ,[ContributionPeriod] ,[SubmissionId] ,[TotalContribution] ,[MemberCount] ,[PayMode] ,[PayRef] ,[PayDate] ,[DistrictCode] ,[EstateID],UserID) VALUES ('" + dr1.ItemArray[0].ToString().PadLeft(1, ' ') + "' ,'" + dr1.ItemArray[1].ToString().PadLeft(6, '0') + "' ,'" + dr1.ItemArray[2].ToString().PadLeft(6, '0') + "' ,'" + dr1.ItemArray[3].ToString().PadLeft(2, '0') + "' ,'" + dr1.ItemArray[4].ToString().PadLeft(14, '0') + "' ,'" + dr1.ItemArray[5].ToString().PadLeft(5, '0') + "' ,'" + dr1.ItemArray[6].ToString().PadLeft(1, ' ') + "' ,'" + dr1.ItemArray[7].ToString().PadLeft(20, '0') + "' ,'" + dr1.ItemArray[8].ToString().PadLeft(10, ' ') + "' ,'" + dr1.ItemArray[9].ToString().PadLeft(2, '0') + "' ,'" + dr1.ItemArray[10].ToString().PadLeft(2, ' ') + "','" + User.StrUserName + "') ", CommandType.Text);
                    //"VALUES ('" + dr[0].ToString().PadLeft(20,'0') + "'  ,'" + dr[1].ToString().PadLeft(40,' ') + "' ,'" + dr[2].ToString().PadLeft(20,' ') + "'  ,'" + dr[3].ToString().PadLeft(6,'0') + "' ,'" + dr[4].ToString().PadLeft(12,'0') + "'  ,'" + dr[5].ToString().PadLeft(12,'0')+ "'  ,'" + dr[6].ToString().PadLeft(12,'0') + "'  ,'" + dr[7].ToString().PadLeft(12,'0') + "'  ,'" + dr[8].ToString().PadLeft(1,'E') + "'  ,'" + dr[9].ToString().PadLeft(1,' ') + "'  ,'" + dr[10].ToString().PadLeft(6,'0') + "'  ,'" + dr[11].ToString().PadLeft(6,' ') + "'  ,'" + dr[12].ToString().PadLeft(2,'0') + "'  ,'" + dr[13].ToString().PadLeft(7,'0') + "'  ,'" + dr[13].ToString().PadLeft(3,'0') + "' ) ", CommandType.Text);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ErrorLog.txt", true))
                    {
                        file.WriteLine("Error" + ex.Message + " employerNo : " + dr1.ItemArray[1].ToString() + " SubmissionId : " + dr1.ItemArray[3].ToString() + "'");
                    }
                }
                //WRITE INTO THE FILE

                //
            }

            return dsEVEMC;

        }

        public DataSet GetEPF_EVEMC_Data_Payroll(String ConPeriod)
        {
            Int32 intDataSubmissionNo = 0;
            Decimal decTotContribution = 0;
            Int32 intMemberCount = 0;
            DataSet dsEVEMC = new DataSet();


            DataTable EVEMP = new DataTable();
            EVEMP.Columns.Add(new DataColumn("ZoneCode"));//0
            EVEMP.Columns.Add(new DataColumn("NICEmployerNo"));
            EVEMP.Columns.Add(new DataColumn("ContributionPeriod"));//2
            EVEMP.Columns.Add(new DataColumn("SubmissionNo"));
            EVEMP.Columns.Add(new DataColumn("TotalContribution"));//4
            EVEMP.Columns.Add(new DataColumn("MemberCount"));
            EVEMP.Columns.Add(new DataColumn("PayMode"));//6
            EVEMP.Columns.Add(new DataColumn("PayRef"));
            EVEMP.Columns.Add(new DataColumn("PayDate"));//8
            EVEMP.Columns.Add(new DataColumn("DistrictCode"));
            EVEMP.Columns.Add(new DataColumn("EstateID"));

            DataTable EVEMC = new DataTable();
            EVEMC.Columns.Add(new DataColumn("EstateID"));//0
            EVEMC.Columns.Add(new DataColumn("NICNo"));
            EVEMC.Columns.Add(new DataColumn("Surname"));//2
            EVEMC.Columns.Add(new DataColumn("Initials"));
            EVEMC.Columns.Add(new DataColumn("MemberNo"));//4
            EVEMC.Columns.Add(new DataColumn("TotalContribution"));
            EVEMC.Columns.Add(new DataColumn("EmployerContribution"));//6
            EVEMC.Columns.Add(new DataColumn("EmployeeContribution"));
            EVEMC.Columns.Add(new DataColumn("TotalEarnings"));//8
            EVEMC.Columns.Add(new DataColumn("MemberStatus"));
            EVEMC.Columns.Add(new DataColumn("Zone"));//10
            EVEMC.Columns.Add(new DataColumn("EmployerNo"));//11
            EVEMC.Columns.Add(new DataColumn("ContributionPeriod"));
            EVEMC.Columns.Add(new DataColumn("DataSubmissionNo"));
            EVEMC.Columns.Add(new DataColumn("NoOfDaysWorked"));//14
            EVEMC.Columns.Add(new DataColumn("OCGrade"));
            EVEMC.Columns.Add(new DataColumn("DistrictCode"));//16

            SqlDataReader dataReader;
            SqlDataReader readerEVEMP;
            DataRow dtrow1;
            dtrow1 = EVEMP.NewRow();
            DataRow dtrow;
            dtrow = EVEMC.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT AutoKey, EstateID, EmployerNO, ZoneCode, PayMode, PaymentRef, DistrictOfficeCode FROM dbo.CHKEPFEmployer ORDER BY AutoKey", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow1 = EVEMP.NewRow();
                decTotContribution = 0;
                DataSet ds = new DataSet();
                ds = SQLHelper.FillDataSet("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100)  > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "')", CommandType.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    intDataSubmissionNo++;
                }
                SqlDataReader readerEVEMC;
                readerEVEMC = SQLHelper.ExecuteReader("SELECT     CASE WHEN (len(ISNULL(dbo.EmployeeMaster.NICNo, '000000000V')) < 10) THEN '000000000V' ELSE ISNULL(dbo.EmployeeMaster.NICNo, '000000000V') END AS NICNO,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.EPFNo,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*22/100) else  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 end AS totalContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF12+(OtherEPFPay*12/100) else dbo.EmpMonthlyEarnings.EPF12 end  AS EmployerContribution,  case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPF10+(OtherEPFPay*10/100) else dbo.EmpMonthlyEarnings.EPF10 end AS EmployeeContribution, case when (isnull(OtherEPFPay,0)>0) then dbo.EmpMonthlyEarnings.EPFPaybleAmount+(OtherEPFPay) else dbo.EmpMonthlyEarnings.EPFPaybleAmount end AS TotalEarnings, ISNULL(dbo.EmployeeMaster.MemberStatus,  'E') AS MemberStatus, dbo.EmployeeMaster.ZoneCode, dbo.EmployeeMaster.EmployerNo, CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Year)  + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) AS ContributionPeriod, '1' AS DataSubmissionNo,  dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NoOfDaysWorked, ISNULL(dbo.EmployeeMaster.OCGrade, '092')  AS OCGrade, dbo.CHKEPFEmployer.AutoKey FROM         dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO INNER JOIN dbo.CHKEPFEmployer ON dbo.EmployeeMaster.EstateID = dbo.CHKEPFEmployer.EstateID AND  dbo.EmployeeMaster.EmployerNo = dbo.CHKEPFEmployer.EmployerNO WHERE     (dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+(isnull(OtherEPFPay,0)*22/100) > 0) AND (dbo.CHKEPFEmployer.EmployerNO = '" + dataReader.GetString(2) + "') AND (CONVERT(varchar(50),  dbo.EmpMonthlyEarnings.Year) + RIGHT('00' + CONVERT(varchar(50), dbo.EmpMonthlyEarnings.Month), 2) = '" + ConPeriod + "')", CommandType.Text);

                while (readerEVEMC.Read())
                {
                    dtrow = EVEMC.NewRow();

                    if (!dataReader.IsDBNull(0))
                    {
                        dtrow[0] = dataReader.GetString(1);
                    }
                    if (!dataReader.IsDBNull(2))
                    {
                        dtrow[11] = dataReader.GetString(2);
                    }
                    if (!readerEVEMC.IsDBNull(0))
                    {
                        dtrow[1] = readerEVEMC.GetString(0);
                    }
                    if (!readerEVEMC.IsDBNull(1))
                    {
                        dtrow[2] = readerEVEMC.GetString(1);
                    }
                    if (!readerEVEMC.IsDBNull(2))
                    {
                        dtrow[3] = readerEVEMC.GetString(2);
                    }
                    if (!readerEVEMC.IsDBNull(3))
                    {
                        if (readerEVEMC.GetString(3).Length > 6)
                        {
                            dtrow[4] = readerEVEMC.GetString(3).Substring(readerEVEMC.GetString(3).Length - 6, 6);
                        }
                        else
                            dtrow[4] = readerEVEMC.GetString(3);
                    }
                    if (!readerEVEMC.IsDBNull(4))
                    {
                        dtrow[5] = readerEVEMC.GetDecimal(4);
                        decTotContribution = decTotContribution + readerEVEMC.GetDecimal(4);
                        intMemberCount = intMemberCount + 1;
                    }
                    if (!readerEVEMC.IsDBNull(5))
                    {
                        dtrow[6] = readerEVEMC.GetDecimal(5);
                    }
                    if (!readerEVEMC.IsDBNull(6))
                    {
                        dtrow[7] = readerEVEMC.GetDecimal(6);
                    }
                    if (!readerEVEMC.IsDBNull(7))
                    {
                        dtrow[8] = readerEVEMC.GetDecimal(7);
                    }
                    if (!readerEVEMC.IsDBNull(8))
                    {
                        dtrow[9] = readerEVEMC.GetString(8);
                    }
                    if (!readerEVEMC.IsDBNull(9))
                    {
                        dtrow[10] = readerEVEMC.GetString(9);
                    }
                    if (!readerEVEMC.IsDBNull(11))
                    {
                        dtrow[12] = readerEVEMC.GetString(11);
                    }
                    if (!readerEVEMC.IsDBNull(12))
                    {
                        //dtrow[13] = intDataSubmissionNo.ToString().PadLeft(2, '0');
                        dtrow[13] = "1".PadLeft(2, '0');
                    }
                    if (!readerEVEMC.IsDBNull(13))
                    {
                        dtrow[14] = readerEVEMC.GetDecimal(13);
                    }
                    if (!readerEVEMC.IsDBNull(14))
                    {
                        dtrow[15] = readerEVEMC.GetString(14);
                    }
                    if (!dataReader.IsDBNull(6))
                    {
                        dtrow[16] = dataReader.GetString(6);
                    }
                    EVEMC.Rows.Add(dtrow);
                }
                readerEVEMC.Close();

                if (!dataReader.IsDBNull(3))
                {
                    dtrow1[0] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow1[1] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow1[2] = ConPeriod;
                }
                if (!dataReader.IsDBNull(3))
                {
                    //dtrow1[3] = intDataSubmissionNo.ToString();
                    dtrow1[3] = "1".PadLeft(2, '0');
                }
                dtrow1[4] = decTotContribution;//total contribution
                dtrow1[5] = intMemberCount;//mem count
                if (!dataReader.IsDBNull(4))
                {
                    dtrow1[6] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow1[7] = dataReader.GetString(5);
                }
                dtrow1[8] = DateTime.Now.Year.ToString().PadLeft(4, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                if (!dataReader.IsDBNull(6))
                {
                    dtrow1[9] = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow1[10] = dataReader.GetString(1);
                }
                EVEMP.Rows.Add(dtrow1);
            }

            dataReader.Close();
            dsEVEMC.Tables.Add(EVEMC);
            dsEVEMC.Tables.Add(EVEMP);

            SQLHelper.ExecuteNonQuery("DELETE  FROM dbo.CHKEVEMC WHERE (ContributionPeriod = '" + ConPeriod + "')", CommandType.Text);
            foreach (DataRow dr in EVEMC.Rows)
            {
                try
                {
                    //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMC] ([NICNo] ,[Surname] ,[Initials] ,[MemberNo] ,[TotalContribution] ,[EmployerContribution] , [EmployeeContribution] ,[TotalEarnings] ,[MemberStatus] ,[Zone] ,[EmployerNo] ,[ContributionPeriod] , [DataSubmissionNo] ,[NoOfDaysWorked] ,[OCGrade],UserID,DistrictCode)  VALUES ('" + dr.ItemArray[1].ToString().PadRight(20, ' ') + "'  ,'" + dr.ItemArray[2].ToString().PadRight(40, ' ') + "' ,'" + dr.ItemArray[3].ToString().PadRight(20, ' ') + "'  ,'" + dr.ItemArray[4].ToString().PadLeft(6, '0') + "' ,'" + dr.ItemArray[5].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[6].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[7].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[8].ToString().PadLeft(12, '0') + "'  ,'" + dr.ItemArray[9].ToString().PadLeft(1, 'E') + "'  ,'" + dr.ItemArray[10].ToString().PadLeft(1, ' ') + "'  ,'" + dr.ItemArray[11].ToString().PadLeft(6, '0') + "'  ,'" + dr.ItemArray[12].ToString().PadLeft(6, ' ') + "'  ,'" + dr.ItemArray[13].ToString().PadLeft(2, '0') + "'  ,'" + dr.ItemArray[14].ToString().PadLeft(7, '0') + "'  ,'" + dr.ItemArray[15].ToString().PadLeft(3, '0') + "','" + User.StrUserName + "','" + dr.ItemArray[16].ToString().PadLeft(2, '0') + "' ) ", CommandType.Text);
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMC] ([NICNo] ,[Surname] ,[Initials] ,[MemberNo] ,[TotalContribution] ,[EmployerContribution] , [EmployeeContribution] ,[TotalEarnings] ,[MemberStatus] ,[Zone] ,[EmployerNo] ,[ContributionPeriod] , [DataSubmissionNo] ,[NoOfDaysWorked] ,[OCGrade],UserID,DistrictCode)  VALUES ('" + dr.ItemArray[1].ToString() + "'  ,'" + dr.ItemArray[2].ToString() + "' ,'" + dr.ItemArray[3].ToString() + "'  ,'" + dr.ItemArray[4].ToString() + "' ,'" + dr.ItemArray[5].ToString() + "'  ,'" + dr.ItemArray[6].ToString() + "'  ,'" + dr.ItemArray[7].ToString() + "'  ,'" + dr.ItemArray[8].ToString() + "'  ,'" + dr.ItemArray[9].ToString().PadLeft(1, 'E') + "'  ,'" + dr.ItemArray[10].ToString().PadLeft(1, ' ') + "'  ,'" + dr.ItemArray[11].ToString() + "'  ,'" + dr.ItemArray[12].ToString() + "'  ,'" + dr.ItemArray[13].ToString() + "'  ,'" + dr.ItemArray[14].ToString() + "'  ,'" + dr.ItemArray[15].ToString() + "','" + User.StrUserName + "','" + dr.ItemArray[16].ToString() + "' ) ", CommandType.Text);
                    //"VALUES ('" + dr[0].ToString().PadLeft(20,'0') + "'  ,'" + dr[1].ToString().PadLeft(40,' ') + "' ,'" + dr[2].ToString().PadLeft(20,' ') + "'  ,'" + dr[3].ToString().PadLeft(6,'0') + "' ,'" + dr[4].ToString().PadLeft(12,'0') + "'  ,'" + dr[5].ToString().PadLeft(12,'0')+ "'  ,'" + dr[6].ToString().PadLeft(12,'0') + "'  ,'" + dr[7].ToString().PadLeft(12,'0') + "'  ,'" + dr[8].ToString().PadLeft(1,'E') + "'  ,'" + dr[9].ToString().PadLeft(1,' ') + "'  ,'" + dr[10].ToString().PadLeft(6,'0') + "'  ,'" + dr[11].ToString().PadLeft(6,' ') + "'  ,'" + dr[12].ToString().PadLeft(2,'0') + "'  ,'" + dr[13].ToString().PadLeft(7,'0') + "'  ,'" + dr[13].ToString().PadLeft(3,'0') + "' ) ", CommandType.Text);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ErrorLog.txt", true))
                    {
                        file.WriteLine("Error" + ex.Message + " NIC : " + dr.ItemArray[1].ToString().PadLeft(20, '0') + " Member : " + dr.ItemArray[4].ToString().PadLeft(6, '0') + "'");
                    }
                }
            }
            SQLHelper.ExecuteNonQuery("DELETE  FROM dbo.CHKEVEMP WHERE (ContributionPeriod = '" + ConPeriod + "')", CommandType.Text);
            foreach (DataRow dr1 in EVEMP.Rows)
            {
                try
                {
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKEVEMP] ([ZoneCode] ,[EmployerNo] ,[ContributionPeriod] ,[SubmissionId] ,[TotalContribution] ,[MemberCount] ,[PayMode] ,[PayRef] ,[PayDate] ,[DistrictCode] ,[EstateID],UserID) VALUES ('" + dr1.ItemArray[0].ToString().PadLeft(1, ' ') + "' ,'" + dr1.ItemArray[1].ToString().PadLeft(6, '0') + "' ,'" + dr1.ItemArray[2].ToString().PadLeft(6, '0') + "' ,'" + dr1.ItemArray[3].ToString().PadLeft(2, '0') + "' ,'" + dr1.ItemArray[4].ToString().PadLeft(14, '0') + "' ,'" + dr1.ItemArray[5].ToString().PadLeft(5, '0') + "' ,'" + dr1.ItemArray[6].ToString().PadLeft(1, ' ') + "' ,'" + dr1.ItemArray[7].ToString().PadLeft(20, '0') + "' ,'" + dr1.ItemArray[8].ToString().PadLeft(10, ' ') + "' ,'" + dr1.ItemArray[9].ToString().PadLeft(2, '0') + "' ,'" + dr1.ItemArray[10].ToString().PadLeft(2, ' ') + "','" + User.StrUserName + "') ", CommandType.Text);
                    //"VALUES ('" + dr[0].ToString().PadLeft(20,'0') + "'  ,'" + dr[1].ToString().PadLeft(40,' ') + "' ,'" + dr[2].ToString().PadLeft(20,' ') + "'  ,'" + dr[3].ToString().PadLeft(6,'0') + "' ,'" + dr[4].ToString().PadLeft(12,'0') + "'  ,'" + dr[5].ToString().PadLeft(12,'0')+ "'  ,'" + dr[6].ToString().PadLeft(12,'0') + "'  ,'" + dr[7].ToString().PadLeft(12,'0') + "'  ,'" + dr[8].ToString().PadLeft(1,'E') + "'  ,'" + dr[9].ToString().PadLeft(1,' ') + "'  ,'" + dr[10].ToString().PadLeft(6,'0') + "'  ,'" + dr[11].ToString().PadLeft(6,' ') + "'  ,'" + dr[12].ToString().PadLeft(2,'0') + "'  ,'" + dr[13].ToString().PadLeft(7,'0') + "'  ,'" + dr[13].ToString().PadLeft(3,'0') + "' ) ", CommandType.Text);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "ErrorLog.txt", true))
                    {
                        file.WriteLine("Error" + ex.Message + " employerNo : " + dr1.ItemArray[1].ToString() + " SubmissionId : " + dr1.ItemArray[3].ToString() + "'");
                    }
                }
                //WRITE INTO THE FILE

                //
            }

            return dsEVEMC;

        }

        public DataSet GetEVEMC(String strContributionPeriod)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     NICNo, Surname, Initials, MemberNo, TotalContribution, EmployerContribution, EmployeeContribution, TotalEarnings, MemberStatus, Zone, EmployerNo,  ContributionPeriod, DataSubmissionNo, NoOfDaysWorked, OCGrade, CreatedDateTime, UserID,DistrictCode FROM dbo.CHKEVEMC WHERE ContributionPeriod='" + strContributionPeriod + "'", CommandType.Text);
            return ds;
        }

        public DataSet GetEVEMP(String strContributionPeriod)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT ZoneCode, EmployerNo, ContributionPeriod, SubmissionId, TotalContribution, MemberCount, PayMode, PayRef, PayDate, DistrictCode, EstateID, CreatedDateTime,  UserID FROM dbo.CHKEVEMP WHERE (ContributionPeriod = '" + strContributionPeriod + "')", CommandType.Text);
            return ds;
        }

        public DataSet getEmployerWiseEVEMC(String strContributionPeriod, String Employer)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     NICNo, Surname, Initials, MemberNo, TotalContribution, EmployerContribution, EmployeeContribution, TotalEarnings, MemberStatus, Zone, EmployerNo,  ContributionPeriod, DataSubmissionNo, NoOfDaysWorked, OCGrade, DistrictCode  FROM dbo.CHKEVEMC WHERE ContributionPeriod='" + strContributionPeriod + "' AND (right('000000'+EmployerNo,6) = '" + Employer.PadLeft(6, '0') + "')", CommandType.Text);
            return ds;
        }

        public DataSet getEmployerWiseEVEMC_ETF(String strContributionPeriod, String Employer)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT     NICNo, Surname, Initials, MemberNo, TotalContribution, EmployerContribution, EmployeeContribution, TotalEarnings, MemberStatus, Zone, EmployerNo,  ContributionPeriod, DataSubmissionNo, NoOfDaysWorked, OCGrade, DistrictCode  FROM dbo.CHKEVEMC WHERE ContributionPeriod='" + strContributionPeriod + "' AND (EmployerNo = '" + Employer.PadLeft(6, '0') + "')", CommandType.Text);
            return ds;
        }
        public DataSet GetDuplicateEPFNos()
        {
            DataSet dsEPFNos = new DataSet();
            dsEPFNos = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmployerNo, dbo.EmployeeMaster.EPFNo FROM dbo.EmployeeMaster INNER JOIN ft_payroll.dbo.employee ON dbo.EmployeeMaster.EmployerNo = ft_payroll.dbo.employee.EmployerNo AND  dbo.EmployeeMaster.EPFNo = ft_payroll.dbo.employee.EPFMemberNo", CommandType.Text);
            return dsEPFNos;
        }
        public DataSet GetErrorEmployerNosInEmployeeMaster()
        {
            DataSet dsEmpNo = new DataSet();
            dsEmpNo = SQLHelper.FillDataSet("SELECT     EmployerNo, DivisionID, EmpNo, EPFNo FROM dbo.EmployeeMaster WHERE (NOT (EmployerNo IN (SELECT     TOP (100) PERCENT EmployerNO FROM dbo.CHKEPFEmployer ORDER BY EmployerNO))) AND (EmployerNo <> '')", CommandType.Text);
            return dsEmpNo;
        }

        public String GetDuplicateEPFNosPayroll()
        {
            DataSet dsEPFNos = new DataSet();
            DataSet dsDuplicateEmps = new DataSet();
            String strDuplicateEPFNos = "";
            dsEPFNos = SQLHelper.FillDataSet("SELECT     EmployerNo, EPFMemberNo, COUNT(code) AS Expr1  FROM ft_payroll.dbo.employee  WHERE (EPFMemberNo <> '')  AND (EPFMemberNo IS NOT NULL)  GROUP BY EmployerNo, EPFMemberNo HAVING (COUNT(code) > 1)", CommandType.Text);
            if (dsEPFNos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in dsEPFNos.Tables[0].Rows)
                {
                    dsDuplicateEmps = SQLHelper.FillDataSet("SELECT EmployerNo, EPFMemberNo, code FROM ft_payroll.dbo.employee WHERE (EmployerNo = '" + dr1[0].ToString() + "') AND (EPFMemberNo = '" + dr1[1].ToString() + "')", CommandType.Text);
                    if (dsDuplicateEmps.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dsDuplicateEmps.Tables[0].Rows)
                        {
                            strDuplicateEPFNos += "\n" + "Employee No:" + dr2[2] + " EPFNo:" + dr2[1];
                        }
                    }
                }
            }
            return strDuplicateEPFNos;
        }
        public String GetDuplicateEPFNosCheckroll()
        {
            DataSet dsEPFNos = new DataSet();
            DataSet dsDuplicateEmps = new DataSet();
            String strDuplicateEPFNos = "";
            dsEPFNos = SQLHelper.FillDataSet("SELECT EmployerNo, EPFNo, COUNT( DISTINCT NICNo) AS Expr1 FROM  dbo.EmployeeMaster WHERE  (ActiveEmployee = 1) AND  (EmpCategory = 1) AND  (EPFNo <> '') AND (NOT (EPFNo IS NULL)) GROUP BY EmployerNo,EPFNo HAVING  COUNT(DISTINCT NICNo)>1  ", CommandType.Text);
            if (dsEPFNos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in dsEPFNos.Tables[0].Rows)
                {
                    dsDuplicateEmps = SQLHelper.FillDataSet("SELECT EmployerNo, EPFNo, EmpNo,DivisionID AS Expr1 FROM dbo.EmployeeMaster WHERE (EmployerNo = '" + dr1[0].ToString() + "') AND (EPFNo = '" + dr1[1].ToString() + "')", CommandType.Text);
                    if (dsDuplicateEmps.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dsDuplicateEmps.Tables[0].Rows)
                        {
                            strDuplicateEPFNos += "\n" + "DivisionID:" + dr2[3] + " EmployeeNo:" + dr2[2] + " EPFNo:" + dr2[1];
                        }
                    }
                }
            }
            return strDuplicateEPFNos;
        }

        public String GetEmpEarningsWithoutEmployerNo(Int32 intYear, Int32 intMonth)
        {
            DataSet dsEPFNos = new DataSet();
            DataSet dsDuplicateEmps = new DataSet();
            String strErrorEmployer = "";
            dsEPFNos = SQLHelper.FillDataSet("SELECT     dbo.EmployeeMaster.EmployerNo, dbo.EmpMonthlyEarnings.DivisionId, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmpMonthlyEarnings.EPF10 FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month ='" + intMonth + "') AND (NOT (dbo.EmployeeMaster.EmployerNo IN (SELECT     EmployerNO FROM dbo.CHKEPFEmployer))) AND ((dbo.EmpMonthlyEarnings.EPF10+ dbo.EmpMonthlyEarnings.OtherEPFPay) > 0)", CommandType.Text);
            if (dsEPFNos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in dsEPFNos.Tables[0].Rows)
                {
                    strErrorEmployer += "\n" + "DivisionID:" + dr1[1] + " EmployeeNo:" + dr1[2] + " EmployerNo:" + dr1[0];
                }
            }
            return strErrorEmployer;
        }


        public DataTable ListAttendenceIncentiveQualificationData(DateTime dtDateFrom, DateTime dtDateTo, String strDiv)
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add(new DataColumn("Division"));//0
            dtData.Columns.Add(new DataColumn("EmpNo"));//1
            dtData.Columns.Add(new DataColumn("NoOfDaysInMonth"));//2
            dtData.Columns.Add(new DataColumn("GeneralHolidays"));//3
            dtData.Columns.Add(new DataColumn("NotOffered"));//4
            dtData.Columns.Add(new DataColumn("WorkOffered"));//5
            dtData.Columns.Add(new DataColumn("NoOfDaysWorked"));//6
            dtData.Columns.Add(new DataColumn("WorkPercentage"));//7
            dtData.Columns.Add(new DataColumn("QualifyDays"));//8
            //dtData.Columns.Add(new DataColumn("IncentiveAmount"));//9
            Int32 intWorkOffered = 0;
            SqlDataReader readerDaysInMonth;
            Int32 intNoOfDaysInMonth = 0;
            readerDaysInMonth = SQLHelper.ExecuteReader("select datediff( dd,dateadd(dd, -day('" + dtDateFrom + "')+1,'" + dtDateFrom + "'), dateadd(m,1,dateadd(dd, -day('" + dtDateFrom + "')+1, '" + dtDateFrom + "')))", CommandType.Text);
            while (readerDaysInMonth.Read())
            {
                if (!readerDaysInMonth.IsDBNull(0))
                {
                    intNoOfDaysInMonth = readerDaysInMonth.GetInt32(0);
                }
            }
            SqlDataReader readerGenHolidays;
            Int32 intGeneralHolidays = 0;
            readerGenHolidays = SQLHelper.ExecuteReader("SELECT COUNT(DISTINCT Date) AS NoOfHolidays FROM dbo.MonthlyHolidays WHERE (Year = '"+dtDateFrom.Year+"') AND (Month = '"+dtDateFrom.Month+"') AND (HolidayType IN ('Sunday', 'Poya Day', 'Paid Holiday'))", CommandType.Text);
            while (readerGenHolidays.Read())
            {
                if (!readerGenHolidays.IsDBNull(0))
                {
                    intGeneralHolidays = readerGenHolidays.GetInt32(0);
                }
            }
            SqlDataReader readerNotOffered;
            Int32 intNotOffered=0;
            SqlDataReader readerNoOfDaysWorked;
            Int32 intNoOfDaysWorked = 0;
            

            SqlDataReader dataReader;            
            DataRow dtrow1;
            dtrow1 = dtData.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME, '"+dtDateTo+"', 102)) GROUP BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo HAVING (dbo.EmployeeMaster.DivisionID = '"+strDiv+"') ORDER BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                dtrow1 = dtData.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow1[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow1[1] = dataReader.GetString(1).Trim();
                }
                dtrow1[2] = intNoOfDaysInMonth;
                dtrow1[3] = intGeneralHolidays;
                dtrow1[4] = 0;
                readerNotOffered=SQLHelper.ExecuteReader("SELECT     COUNT(DISTINCT DateEntered) AS Expr1 FROM dbo.DailyGroundTransactions WHERE (DivisionID = '"+dtrow1[0].ToString()+"') AND (EmpNo = '"+dtrow1[1].ToString()+"') AND (WorkCodeID IN ('XPR', 'XIN', 'XMT', 'XXX')) AND (DateEntered BETWEEN CONVERT(DATETIME,  '"+dtDateFrom+"', 102) AND CONVERT(DATETIME, '"+dtDateTo+"', 102))AND (NOT (DateEntered IN (SELECT     Date AS NoOfHolidays FROM  dbo.MonthlyHolidays WHERE (Year = '"+dtDateFrom.Year+"') AND (Month = '"+dtDateFrom.Month+"') AND (HolidayType IN ('Sunday', 'Poya Day', 'Paid Holiday')))))",CommandType.Text);
                while(readerNotOffered.Read())
                {
                    if(!readerNotOffered.IsDBNull(0))
                    {
                        dtrow1[4] = readerNotOffered.GetInt32(0);
                    }
                }
                readerNotOffered.Close();
                dtrow1[6] = 0;
                dtrow1[5] = Convert.ToInt32(dtrow1[2].ToString()) - (Convert.ToInt32(dtrow1[3].ToString()) + Convert.ToInt32(dtrow1[4].ToString()));
                readerNoOfDaysWorked = SQLHelper.ExecuteReader("SELECT isnull(SUM(ManDays),0) AS Expr3 FROM dbo.DailyGroundTransactions WHERE (WorkCodeID NOT LIKE 'X%')  AND (WorkCodeID NOT IN ('ABS','PH')) GROUP BY DivisionID,EmpNo,Year(DateEntered), MONTH(DateEntered), HolidayYesNo HAVING      (EmpNo = '" + dtrow1[1].ToString() + "') AND (MONTH(DateEntered) = '"+dtDateFrom.Month+"') AND  (Year(DateEntered)='"+dtDateFrom.Year+"') and (HolidayYesNo = 0)  AND (DivisionID = '" + dtrow1[0].ToString() + "')", CommandType.Text);
                while (readerNoOfDaysWorked.Read())
                {
                    if (!readerNoOfDaysWorked.IsDBNull(0))
                    {
                        dtrow1[6] = readerNoOfDaysWorked.GetDecimal(0);
                    }
                }
                readerNoOfDaysWorked.Close();
                dtrow1[7] = (Convert.ToDecimal(dtrow1[6].ToString()) / Convert.ToDecimal(dtrow1[5].ToString())) * 100;
                dtrow1[8] = Math.Floor(Convert.ToDecimal(dtrow1[5].ToString()) * 75 / 100);

                //if(Convert.ToDecimal(dtrow1[6].ToString())>0)
                dtData.Rows.Add(dtrow1);
            }
            dataReader.Close();
            return dtData;


        }

        public DataSet ListBlockPluckingDetails(DateTime dtDateFrom, DateTime dtDateTo, String strDiv)
        {
            DataSet dsBlockPlk = new DataSet("BlockPlkDetails");
            dsBlockPlk = SQLHelper.FillDataSet("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS Expr1, dbo.DailyGroundTransactions.EmpNo, dbo.DailyGroundTransactions.WorkQty,  dbo.EmployeeMaster.EMPName FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME, '"+dtDateTo+"', 102)) AND  (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND (dbo.DailyGroundTransactions.DivisionID = '"+strDiv+"') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')  AND (dbo.DailyGroundTransactions.WorkType = 2)", CommandType.Text);
            return dsBlockPlk;
        }

        public DataSet ListCashNamePluckingDetails(DateTime dtDateFrom, DateTime dtDateTo, String strDiv)
        {
            DataSet dsNamePlucking = new DataSet("NamePlkDetails");
            DataSet ds=new DataSet("tempDs");
            ds=SQLHelper.FillDataSet("SELECT DateEntered, DivisionID, EmpNo, WorkQty, CashKgAmount, CashKgs, NormKilos, CASE WHEN (CashKgs - NormKilos) > 0 THEN (CashKgs - NormKilos)  ELSE 0 END AS OverKilos, CASE WHEN (FullHalf = 1) THEN 'Half' ELSE 'Full' END AS FullHalf,'Half- (CashKgs-Norm)<0' as Type FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME,'"+dtDateTo+"', 102)) AND (WorkType = 2) AND  (DivisionID LIKE'"+strDiv+"') AND (WorkCodeID = 'plk') AND (CashPlkOkgYesNo = 1) AND (CashKgs < NormKilos) AND (FullHalf = 1)",CommandType.Text);
            dsNamePlucking.Merge(ds);
            ds = null;
            ds = SQLHelper.FillDataSet(" SELECT DateEntered, DivisionID, EmpNo, WorkQty, CashKgAmount, CashKgs, NormKilos, CASE WHEN (CashKgs - NormKilos) > 0 THEN (CashKgs - NormKilos)  ELSE 0 END AS OverKilos, CASE WHEN (FullHalf = 1) THEN 'Half' ELSE 'Full' END AS FullHalf,'Half- (CashKgs-Norm)=0' as Type FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME,'"+dtDateTo+"', 102)) AND (WorkType = 2) AND  (DivisionID LIKE'"+strDiv+"') AND (WorkCodeID = 'plk') AND (CashPlkOkgYesNo = 1) AND (CashKgs = NormKilos) AND (FullHalf = 1)", CommandType.Text);
            dsNamePlucking.Merge(ds);
            ds = null;
            ds = SQLHelper.FillDataSet(" SELECT DateEntered, DivisionID, EmpNo, WorkQty, CashKgAmount, CashKgs, NormKilos, CASE WHEN (CashKgs - NormKilos) > 0 THEN (CashKgs - NormKilos)  ELSE 0 END AS OverKilos, CASE WHEN (FullHalf = 1) THEN 'Half' ELSE 'Full' END AS FullHalf,'Half- (CashKgs-Norm)>0' as Type FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME,'"+dtDateTo+"', 102)) AND (WorkType = 2) AND  (DivisionID LIKE'"+strDiv+"') AND (WorkCodeID = 'plk') AND (CashPlkOkgYesNo = 1) AND (CashKgs > NormKilos) AND (FullHalf = 1)", CommandType.Text);
            dsNamePlucking.Merge(ds);
            ds = null;
            ds = SQLHelper.FillDataSet(" SELECT DateEntered, DivisionID, EmpNo, WorkQty, CashKgAmount, CashKgs, NormKilos, CASE WHEN (CashKgs - NormKilos) > 0 THEN (CashKgs - NormKilos)  ELSE 0 END AS OverKilos, CASE WHEN (FullHalf = 1) THEN 'Half' ELSE 'Full' END AS FullHalf,'full- (CashKgs-Norm)<0' as Type FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME,'"+dtDateTo+"', 102)) AND (WorkType = 2) AND  (DivisionID LIKE'"+strDiv+"') AND (WorkCodeID = 'plk') AND (CashPlkOkgYesNo = 1) AND (CashKgs < NormKilos) AND (FullHalf = 2)", CommandType.Text);
            dsNamePlucking.Merge(ds);
            ds = null;
            ds = SQLHelper.FillDataSet("  SELECT DateEntered, DivisionID, EmpNo, WorkQty, CashKgAmount, CashKgs, NormKilos, CASE WHEN (CashKgs - NormKilos) > 0 THEN (CashKgs - NormKilos)  ELSE 0 END AS OverKilos, CASE WHEN (FullHalf = 1) THEN 'Half' ELSE 'Full' END AS FullHalf,'full- (CashKgs-Norm)=0' as Type FROM  dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME,'"+dtDateTo+"', 102)) AND (WorkType = 2) AND  (DivisionID LIKE'"+strDiv+"') AND (WorkCodeID = 'plk') AND (CashPlkOkgYesNo = 1) AND (CashKgs = NormKilos) AND (FullHalf = 2)", CommandType.Text);
            dsNamePlucking.Merge(ds);
            ds = null;
            ds = SQLHelper.FillDataSet("  SELECT DateEntered, DivisionID, EmpNo, WorkQty, CashKgAmount, CashKgs, NormKilos, CASE WHEN (CashKgs - NormKilos) > 0 THEN (CashKgs - NormKilos)  ELSE 0 END AS OverKilos, CASE WHEN (FullHalf = 1) THEN 'Half' ELSE 'Full' END AS FullHalf,'full- (CashKgs-Norm)>0' as Type FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '"+dtDateFrom+"', 102) AND CONVERT(DATETIME,'"+dtDateTo+"', 102)) AND (WorkType = 2) AND  (DivisionID LIKE'"+strDiv+"') AND (WorkCodeID = 'plk') AND (CashPlkOkgYesNo = 1) AND (CashKgs > NormKilos) AND (FullHalf = 2)", CommandType.Text);
            dsNamePlucking.Merge(ds);
            ds = null;
            
            
            return dsNamePlucking;
        }

        public DataSet PaymentDetailsSummary(String strDiv, DateTime dtFrom, DateTime dtTo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT DateEntered, DivisionID, WorkType, SUM(ManDays) AS ManDays, SUM(OverKgs) AS Okgs, SUM(OverKgAmount) AS OkgAmount, SUM(PRIAmount) AS PRI,  SUM(PSSAmount) AS PSS, SUM(DailyBasic) AS DailyBasic, SUM(IncentiveAmount) AS attIncentive FROM         dbo.DailyGroundTransactions GROUP BY DateEntered, DivisionID, WorkType HAVING      (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (DivisionID LIKE '" + strDiv + "')", CommandType.Text);
            return ds;
        }

        public DataSet PaymentDetailsSummaryEmpwise(String strDiv, DateTime dtFrom, DateTime dtTo, String strEmp)
        {
            DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT        dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount,  dbo.DailyGroundTransactions.PSSAmount, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.IncentiveAmount,  dbo.DailyGroundTransactions.ExtraRates, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount,  dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount AS Scrap, dbo.DailyGroundTransactions.ScrapKgs,  ISNULL ((SELECT        SUM(Hours) AS OTHours FROM            dbo.CHKOvertime WHERE        (OtDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionCode = dbo.DailyGroundTransactions.DivisionID) AND  (EmployeeNo = dbo.DailyGroundTransactions.EmpNo)), 0) AS OTHours, ISNULL ((SELECT        SUM(Expenditure) AS OTAmount FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionCode = dbo.DailyGroundTransactions.DivisionID) AND  (EmployeeNo = dbo.DailyGroundTransactions.EmpNo)), 0) AS OTAmount, dbo.EstateDivision.DivisionName, dbo.Estate.EstateName,  dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.PRINorm FROM            dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.Estate ON dbo.EstateDivision.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.EmpNo LIKE '" + strEmp + "') order by dbo.DailyGroundTransactions.DateEntered", CommandType.Text);
            ds = SQLHelper.FillDataSet("SELECT        TOP (100) PERCENT dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  Convert(varchar(50),dbo.DailyGroundTransactions.WorkType) as WorkType ,dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.FullHalf,  dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.PSSAmount,  dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.IncentiveAmount, dbo.DailyGroundTransactions.ExtraRates,  dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount,  dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount AS Scrap, dbo.DailyGroundTransactions.ScrapKgs,  0 AS OTHours, 0 AS OTAmount, dbo.EstateDivision.DivisionName, dbo.Estate.EstateName,  dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.PRINorm FROM            dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.Estate ON dbo.EstateDivision.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo where         (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME,  '" + dtFrom + "', 102) AND CONVERT(DATETIME,  '" + dtTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.EmpNo LIKE '" + strEmp + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.FullHalf,  dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.PSSAmount,  dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.IncentiveAmount, dbo.DailyGroundTransactions.ExtraRates,  dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount,  dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount, dbo.DailyGroundTransactions.ScrapKgs,  dbo.EstateDivision.DivisionName, dbo.Estate.EstateName, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.PRINorm   union SELECT        TOP (100) PERCENT dbo.CHKOvertime.OtDate AS DateEntered, dbo.CHKOvertime.DivisionCode AS DivisionID, dbo.CHKOvertime.EmployeeNo AS EmpNo,  dbo.CHKOTParameters.OTType AS WorkType, dbo.CHKOvertime.LabourType, dbo.CHKOvertime.Job AS WorkCodeID, 2 AS FullHalf, 0 AS ManDays, 0 AS WorkQty,  0 AS NormKilos, 0 AS OverKgs, 0 AS OverKgAmount, 0 AS PRIAmount, 0 AS PSSAmount, 0 AS DailyBasic, 0 AS IncentiveAmount, 0 AS ExtraRates,  0 AS CashKgAmount, 0 AS CashSundryAmount, 0 AS Scrap, 0 AS ScrapKgs, SUM(dbo.CHKOvertime.Hours) AS OTHours, SUM(dbo.CHKOvertime.Expenditure)  AS OTAmount, dbo.EstateDivision.DivisionName, dbo.Estate.EstateName, dbo.EmployeeMaster.EMPName, 0 AS PRINorm FROM            dbo.EstateDivision INNER JOIN dbo.Estate ON dbo.EstateDivision.EstateID = dbo.Estate.EstateID INNER JOIN dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId ON  dbo.EstateDivision.DivisionID = dbo.CHKOvertime.DivisionCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKOvertime.DivisionCode = dbo.EmployeeMaster.DivisionID AND  dbo.CHKOvertime.EmployeeNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME,  '" + dtFrom + "', 102) AND CONVERT(DATETIME,  '" + dtTo + "', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '" + strDiv + "') AND (dbo.CHKOvertime.EmployeeNo LIKE '" + strEmp + "') GROUP BY dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo,dbo.CHKOTParameters.OTType, dbo.CHKOvertime.LabourType, dbo.CHKOvertime.Job,  dbo.EstateDivision.DivisionName, dbo.Estate.EstateName, dbo.EmployeeMaster.EMPName ORDER BY DateEntered", CommandType.Text);
            return ds;
        }

        public DataTable PaymentDetailsSummaryEmpwise1(String strDiv, DateTime dtFrom, DateTime dtTo, String strEmp)
        {
            //DataSet ds = new DataSet();
            //ds = SQLHelper.FillDataSet("SELECT        dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount,  dbo.DailyGroundTransactions.PSSAmount, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.IncentiveAmount,  dbo.DailyGroundTransactions.ExtraRates, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount,  dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount AS Scrap, dbo.DailyGroundTransactions.ScrapKgs,  ISNULL ((SELECT        SUM(Hours) AS OTHours FROM            dbo.CHKOvertime WHERE        (OtDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionCode = dbo.DailyGroundTransactions.DivisionID) AND  (EmployeeNo = dbo.DailyGroundTransactions.EmpNo)), 0) AS OTHours, ISNULL ((SELECT        SUM(Expenditure) AS OTAmount FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionCode = dbo.DailyGroundTransactions.DivisionID) AND  (EmployeeNo = dbo.DailyGroundTransactions.EmpNo)), 0) AS OTAmount, dbo.EstateDivision.DivisionName, dbo.Estate.EstateName,  dbo.EmployeeMaster.EMPName FROM            dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.Estate ON dbo.EstateDivision.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.EmpNo LIKE '" + strEmp + "') order by dbo.DailyGroundTransactions.DateEntered", CommandType.Text);
            
            DataTable dtPayment=new DataTable();
            DataRow drow;
            SqlDataReader readerPayment;
            dtPayment.Columns.Add(new DataColumn("DateEntered"));//0
            dtPayment.Columns.Add(new DataColumn("DivisionID"));
            dtPayment.Columns.Add(new DataColumn("EmpNo"));
            dtPayment.Columns.Add(new DataColumn("WorkType"));//3
            dtPayment.Columns.Add(new DataColumn("LabourType"));
            dtPayment.Columns.Add(new DataColumn("WorkCodeID"));
            dtPayment.Columns.Add(new DataColumn("FullHalf"));//6
            dtPayment.Columns.Add(new DataColumn("ManDays"));
            dtPayment.Columns.Add(new DataColumn("WorkQty"));
            dtPayment.Columns.Add(new DataColumn("NormKilos"));//9
            dtPayment.Columns.Add(new DataColumn("OverKgs"));//10
            dtPayment.Columns.Add(new DataColumn("OverKgAmount"));
            dtPayment.Columns.Add(new DataColumn("PRIAmount"));
            dtPayment.Columns.Add(new DataColumn("PSSAmount"));//13
            dtPayment.Columns.Add(new DataColumn("DailyBasic"));
            dtPayment.Columns.Add(new DataColumn("IncentiveAmount"));//15
            dtPayment.Columns.Add(new DataColumn("ExtraRates"));
            dtPayment.Columns.Add(new DataColumn("CashKgAmount"));//17
            dtPayment.Columns.Add(new DataColumn("CashSundryAmount"));
            dtPayment.Columns.Add(new DataColumn("Scrap"));
            dtPayment.Columns.Add(new DataColumn("ScrapKgs"));//20
            dtPayment.Columns.Add(new DataColumn("OTHours"));
            dtPayment.Columns.Add(new DataColumn("OTAmount"));
            dtPayment.Columns.Add(new DataColumn("DivisionName"));//23
            dtPayment.Columns.Add(new DataColumn("EstateName"));
            dtPayment.Columns.Add(new DataColumn("EMPName"));
            dtPayment.Columns.Add(new DataColumn("PRINorm"));//26

            DataSet ds = new DataSet();
            readerPayment = SQLHelper.ExecuteReader("SELECT        dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.FullHalf, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount,  dbo.DailyGroundTransactions.PSSAmount, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.IncentiveAmount,  dbo.DailyGroundTransactions.ExtraRates, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount,  dbo.DailyGroundTransactions.ScrapKgAmount + dbo.DailyGroundTransactions.CashScrapKgAmount AS Scrap, dbo.DailyGroundTransactions.ScrapKgs,  ISNULL ((SELECT        SUM(Hours) AS OTHours FROM            dbo.CHKOvertime WHERE        (OtDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionCode = dbo.DailyGroundTransactions.DivisionID) AND  (EmployeeNo = dbo.DailyGroundTransactions.EmpNo)), 0) AS OTHours, ISNULL ((SELECT        SUM(Expenditure) AS OTAmount FROM            dbo.CHKOvertime AS CHKOvertime_1 WHERE        (OtDate = CONVERT(DATETIME, dbo.DailyGroundTransactions.DateEntered, 102)) AND (DivisionCode = dbo.DailyGroundTransactions.DivisionID) AND  (EmployeeNo = dbo.DailyGroundTransactions.EmpNo)), 0) AS OTAmount, dbo.EstateDivision.DivisionName, dbo.Estate.EstateName,  dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.PRINorm FROM            dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.Estate ON dbo.EstateDivision.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE        (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.EmpNo LIKE '" + strEmp + "') order by dbo.DailyGroundTransactions.DateEntered", CommandType.Text);
            while(readerPayment.Read())
            {
                drow = dtPayment.NewRow();
                //1date
                if (!readerPayment.IsDBNull(0))
                {
                    drow[0] = readerPayment.GetDateTime(0);
                }
                //2Division
                if (!readerPayment.IsDBNull(1))
                {
                    drow[1] = readerPayment.GetString(1);
                }
                //3empNo
                if (!readerPayment.IsDBNull(2))
                {
                    drow[2] = readerPayment.GetString(2);
                }
                //4workType
                if (!readerPayment.IsDBNull(3))
                {
                    drow[3] = readerPayment.GetInt32(3);
                }
                //5LabourType
                if (!readerPayment.IsDBNull(4))
                {
                    drow[4] = readerPayment.GetString(4);
                }
                //6WorkCode
                if (!readerPayment.IsDBNull(5))
                {
                    drow[5] = readerPayment.GetString(5);
                }
                //7FullHalf
                if (!readerPayment.IsDBNull(6))
                {
                    drow[6] = readerPayment.GetInt32(6);
                }
                //8mandays
                if (!readerPayment.IsDBNull(7))
                {
                    drow[7] = readerPayment.GetDecimal(7);
                }
                //9workQty
                if (!readerPayment.IsDBNull(8))
                {
                    drow[8] = readerPayment.GetDecimal(8);
                }
                //10NormKilos
                if (!readerPayment.IsDBNull(9))
                {
                    drow[9] = readerPayment.GetDecimal(9);
                }
                //OverKg
                if (!readerPayment.IsDBNull(10))
                {
                    drow[10] = readerPayment.GetDecimal(10);
                }
                //OverKgAmount
                if (!readerPayment.IsDBNull(11))
                {
                    drow[11] = readerPayment.GetDecimal(11);
                }
                //PRI
                if (!readerPayment.IsDBNull(12))
                {
                    drow[12] = readerPayment.GetDecimal(12);
                }
                //PSS
                if (!readerPayment.IsDBNull(13))
                {
                    drow[13] = readerPayment.GetDecimal(13);
                }
                //DailyBasic
                if (!readerPayment.IsDBNull(14))
                {
                    drow[14] = readerPayment.GetDecimal(14);
                }
                //Incentive
                if (!readerPayment.IsDBNull(15))
                {
                    drow[15] = readerPayment.GetDecimal(15);
                }
                //ExtraRate
                if (!readerPayment.IsDBNull(16))
                {
                    drow[16] = readerPayment.GetDecimal(16);
                }
                //CashkgAmount
                if (!readerPayment.IsDBNull(17))
                {
                    drow[17] = readerPayment.GetDecimal(17);
                }
                //CashSundryAmount
                if (!readerPayment.IsDBNull(18))
                {
                    drow[18] = readerPayment.GetDecimal(18);
                }
                //ScrapAmount
                if (!readerPayment.IsDBNull(19))
                {
                    drow[19] = readerPayment.GetDecimal(19);
                }
                //Scrap Kgs
                if (!readerPayment.IsDBNull(20))
                {
                    drow[20] = readerPayment.GetDecimal(20);
                }
                //OT Hours
                if (!readerPayment.IsDBNull(21))
                {
                    drow[21] = readerPayment.GetDecimal(21);
                }
                //OT Amount
                if (!readerPayment.IsDBNull(22))
                {
                    drow[22] = readerPayment.GetDecimal(22);
                }
                //divisionName
                if (!readerPayment.IsDBNull(23))
                {
                    drow[23] = readerPayment.GetString(23);
                }
                //Estate Name
                if (!readerPayment.IsDBNull(24))
                {
                    drow[24] = readerPayment.GetString(24);
                }
                //EmpName
                if (!readerPayment.IsDBNull(25))
                {
                    drow[25] = readerPayment.GetString(25);
                }
                //PRINorm
                if (!readerPayment.IsDBNull(26))
                {
                    drow[26] = readerPayment.GetInt32(26);
                }
                dtPayment.Rows.Add(drow);

            }
            readerPayment.Dispose();
            return dtPayment;
        }

        public DataSet GetEmployeeWiseEPFDetails(int intYear, int intMonth)
        {
            DataSet dsEPFDetails = new DataSet();
            dsEPFDetails = SQLHelper.FillDataSet("SELECT ROW_NUMBER() OVER(ORDER BY dbo.EmployeeMaster.ZoneCode + dbo.EmployeeMaster.EmployerNo, dbo.EmployeeMaster.EPFNo) as SeqNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.ZoneCode + dbo.EmployeeMaster.EmployerNo AS EPFCompany, dbo.EmployeeMaster.EMPName as FullName,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, 'NA' AS Address, dbo.EmployeeMaster.NICNo, dbo.EmployeeMaster.Gender as Sex,  dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.dateOfBirth, CASE WHEN (DateJoined >= dateOfBirth) THEN datediff(yy, CONVERT(datetime, dateOfBirth,  102), CONVERT(datetime, getdate(), 102)) ELSE 0 END AS Age, replace(dbo.Estate.EstateName,'estate','') as EstateName, replace(dbo.EstateDivision.DivisionName,'division','') as DivisionName, dbo.EmployeeMaster.BasicJob,  dbo.EmpMonthlyEarnings.PluckingNamePay + dbo.EmpMonthlyEarnings.SundryNamePay + dbo.EmpMonthlyEarnings.ExtraRates + dbo.EmpMonthlyEarnings.OverKilosPay AS Salary, dbo.EmpMonthlyEarnings.EPF12, dbo.EmpMonthlyEarnings.EPF10,  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10 AS TotalContribution, dbo.EmployeeMaster.UnionNameCode FROM            dbo.EmployeeMaster INNER JOIN dbo.Estate ON dbo.EmployeeMaster.EstateID = dbo.Estate.EstateID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE        (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.EPF10>0) ORDER BY EPFCompany, dbo.EmployeeMaster.EPFNo", CommandType.Text);
            return dsEPFDetails;
        }
    }
}
