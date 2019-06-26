using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class CheckRollReports
    {
        private String strCategory;

        public String StrCategory
        {
            get { return strCategory; }
            set { strCategory = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        public DataSet GetMonthlyWeges(String Category, String Division, int intYear, int intMonth)
        {
            String strfilterCat = "";
            String strfilterDiv = "";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            if (Category.Equals("ALL"))
            {
                strfilterCat = " ";
            }
            else
            {
                strfilterCat = "AND (dbo.EmployeeMaster.Category IN (" + Category + ")) ";
            }
            if (Division.Equals("ALL"))
            {
                strfilterDiv = " ";
            }
            else
            {
                strfilterDiv = " AND (dbo.EmployeeMaster.DivisionID IN (" + Division + ")) ";
            }
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.EmpMonthlyEarnings.PeriodFrom, dbo.EmpMonthlyEarnings.PeriodTo, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EMPName, " +
                                                                  " dbo.EmpMonthlyEarnings.PluckingManDays, dbo.EmpMonthlyEarnings.SundryManDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays,  " +
                                                                  " dbo.EmpMonthlyEarnings.HolidaySundryManDays, dbo.EmpMonthlyEarnings.OverKilos, dbo.EmpMonthlyEarnings.AttIncentive,  " +
                                                                  " dbo.EmpMonthlyEarnings.PluckingNamePay, dbo.EmpMonthlyEarnings.SundryNamePay, dbo.EmpMonthlyEarnings.ExtraRates,  " +
                                                                  " dbo.EmpMonthlyEarnings.PRIAmount " +
                                                                  " FROM         dbo.EmpMonthlyEarnings INNER JOIN " +
                                                                  " dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo ", CommandType.Text);
            //" WHERE   (dbo.EmpMonthlyEarnings.Year='"+intYear+"') and (dbo.EmpMonthlyEarnings.Month = '"+intMonth+"')'"+strfilterCat +"'" , CommandType.Text);
            //+ strfilterDiv + strfilterCat

            da.Fill(ds, "MonthlyWeges");
            return ds;

        }

        public DataSet getPaymentCheckRoll(int intYear, int intMonth,String intCat)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EmpMonthlyEarnings.EmpNO, dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.Month, dbo.EmpMonthlyEarnings.Year, dbo.EmpMonthlyEarnings.ExtraRates, dbo.EmpMonthlyEarnings.OverKilos, dbo.EmpMonthlyEarnings.CashPlucking, dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.PRIAmount, dbo.EmpMonthlyEarnings.AttIncentive, dbo.EmpMonthlyEarnings.PluckingManDays, dbo.EmpMonthlyEarnings.SundryManDays, dbo.EmpMonthlyEarnings.HolidaySundryManDays, dbo.EmpMonthlyEarnings.PluckingNamePay,  dbo.EmpMonthlyEarnings.SundryNamePay, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.HolidayPLKManDays, dbo.EmpMonthlyEarnings.PluckingKilos, case when (dbo.EmpMonthlyEarnings.QualifyDays=0) then dbo.EmpMonthlyEarnings.WorkedPercentage else dbo.EmpMonthlyEarnings.QualifyDays end as WorkedPercentage, dbo.EmpMonthlyEarnings.OverKilosPay, dbo.EmpMonthlyEarnings.PaidHolidays, dbo.EmpMonthlyEarnings.OtherEPFAdditions, dbo.EmpMonthlyEarnings.EPFPaybleAmount AS TotalPayEPF, dbo.EmpMonthlyEarnings.OverTime, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyEarnings.TotalEarnings, dbo.EmpMonthlyEarnings.PreviousMadeUpCoins, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.HolidayHalfNames, dbo.EmpMonthlyEarnings.PSSAmount, dbo.EmpMonthlyEarnings.MonthlyScrapKgs+dbo.EmpMonthlyEarnings.MonthlyCashScrapKgs ,  dbo.EmpMonthlyEarnings.MonthlyScrapKgAmount+dbo.EmpMonthlyEarnings.monthlyCashScrapKgAmount as MonthlyScrapKgAmount,dbo.EmpMonthlyEarnings.monthlyCashScrapKgAmount as MonthlyCashScrapKgAmount FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo AND  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (CONVERT(VARCHAR(10),Category) LIKE '" + intCat + "') ORDER BY dbo.EmpMonthlyEarnings.Category,convert(int, dbo.EmpMonthlyEarnings.EmpNO)", CommandType.Text);
            da.Fill(ds, "PaymentCheckRoll");
            return ds;
        }
        public DataSet getPaymentCheckRoll(String DivisionID, int intYear, int intMonth, String intCat)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EmpMonthlyEarnings.EmpNO, dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.Month, dbo.EmpMonthlyEarnings.Year, dbo.EmpMonthlyEarnings.ExtraRates, dbo.EmpMonthlyEarnings.OverKilos, dbo.EmpMonthlyEarnings.CashPlucking,  dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.PRIAmount, dbo.EmpMonthlyEarnings.AttIncentive, dbo.EmpMonthlyEarnings.PluckingManDays,  dbo.EmpMonthlyEarnings.SundryManDays, dbo.EmpMonthlyEarnings.HolidaySundryManDays, dbo.EmpMonthlyEarnings.PluckingNamePay,dbo.EmpMonthlyEarnings.SundryNamePay, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.HolidayPLKManDays, dbo.EmpMonthlyEarnings.PluckingKilos, case when (dbo.EmpMonthlyEarnings.QualifyDays=0) then dbo.EmpMonthlyEarnings.WorkedPercentage else dbo.EmpMonthlyEarnings.QualifyDays end as WorkedPercentage, dbo.EmpMonthlyEarnings.OverKilosPay, dbo.EmpMonthlyEarnings.PaidHolidays, dbo.EmpMonthlyEarnings.OtherEPFAdditions, dbo.EmpMonthlyEarnings.EPFPaybleAmount AS TotalPayEPF, dbo.EmpMonthlyEarnings.OverTime, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyEarnings.TotalEarnings, dbo.EmpMonthlyEarnings.PreviousMadeUpCoins, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.HolidayHalfNames, dbo.EmpMonthlyEarnings.PSSAmount, dbo.EmpMonthlyEarnings.MonthlyScrapKgs,  dbo.EmpMonthlyEarnings.MonthlyScrapKgAmount+dbo.EmpMonthlyEarnings.monthlyCashScrapKgAmount as MonthlyScrapKgAmount,dbo.EmpMonthlyEarnings.monthlyCashScrapKgAmount as MonthlyCashScrapKgAmount FROM  dbo.EmpMonthlyEarnings INNER JOIN  dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo AND  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (CONVERT(VARCHAR(10),Category) LIKE '" + intCat + "') ORDER BY dbo.EmpMonthlyEarnings.Category, convert(int,dbo.EmpMonthlyEarnings.EmpNO)", CommandType.Text);
            da.Fill(ds, "PaymentCheckRoll");
            return ds;
        }
        public DataSet getPaymentCheckRollII(int intYear, int intMonth, String intCat)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.EMPName, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyDeductions.EPFAmount, dbo.EmpMonthlyDeductions.MonthlyAdvance, dbo.EmpMonthlyDeductions.FestivalAdvance, dbo.EmpMonthlyDeductions.UnionSubscription, dbo.EmpMonthlyDeductions.Cooperative,dbo.EmpMonthlyDeductions.ReligiousActivities, dbo.EmpMonthlyDeductions.TeaCoconutOther, dbo.EmpMonthlyDeductions.FoodStuff,dbo.EmpMonthlyDeductions.Welfare, dbo.EmpMonthlyDeductions.Dhoby, dbo.EmpMonthlyDeductions.Barber, dbo.EmpMonthlyDeductions.Insuarance,dbo.EmpMonthlyDeductions.PreviousDebits, dbo.EmpMonthlyDeductions.PayDetailSlip, dbo.EmpMonthlyDeductions.BankLoan, dbo.EmpMonthlyDeductions.Others, dbo.EmpMonthlyDeductions.TotalDeductions, dbo.EmpMonthlyFinalWeges.SalaryAmount, dbo.EmpMonthlyFinalWeges.MadeUpBalance, dbo.EmpMonthlyFinalWeges.WagePay, dbo.EmpMonthlyFinalWeges.DebitsBF, dbo.EmpMonthlyEarnings.DivisionId, dbo.EmpMonthlyEarnings.EmpNO,dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.EPF12, dbo.EmpMonthlyEarnings.EPFPaybleAmount * 22 / 100 AS EPF22, dbo.EmpMonthlyEarnings.ETF3, dbo.EmpMonthlyEarnings.CashPlucking, dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.TotalEarnings,dbo.EmpMonthlyEarnings.monthlyCashScrapKgAmount as MonthlyCashScrapKgAmount FROM dbo.EmpMonthlyEarnings INNER JOIN  dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo AND dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmpMonthlyDeductions ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyDeductions.DivisionId AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyDeductions.EmpNo INNER JOIN dbo.EmpMonthlyFinalWeges ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyFinalWeges.DivisionId AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyFinalWeges.EmpNo WHERE (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (CONVERT(VARCHAR(10),Category) LIKE '" + intCat + "') ORDER BY dbo.EmpMonthlyEarnings.Category, convert(int,dbo.EmpMonthlyEarnings.EmpNO)", CommandType.Text);
            da.Fill(ds, "PaymentCheckRollII");
            return ds;
        }
        public DataSet getPaymentCheckRollII(String DivisionID, int intYear, int intMonth, String intCat)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.EMPName, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyDeductions.EPFAmount, dbo.EmpMonthlyDeductions.MonthlyAdvance, dbo.EmpMonthlyDeductions.FestivalAdvance, dbo.EmpMonthlyDeductions.UnionSubscription, dbo.EmpMonthlyDeductions.Cooperative, dbo.EmpMonthlyDeductions.ReligiousActivities, dbo.EmpMonthlyDeductions.TeaCoconutOther,  dbo.EmpMonthlyDeductions.FoodStuff, dbo.EmpMonthlyDeductions.Welfare, dbo.EmpMonthlyDeductions.Dhoby, dbo.EmpMonthlyDeductions.Barber, dbo.EmpMonthlyDeductions.Insuarance, dbo.EmpMonthlyDeductions.PreviousDebits, dbo.EmpMonthlyDeductions.PayDetailSlip, dbo.EmpMonthlyDeductions.BankLoan, dbo.EmpMonthlyDeductions.Others, dbo.EmpMonthlyDeductions.TotalDeductions, dbo.EmpMonthlyFinalWeges.SalaryAmount,dbo.EmpMonthlyFinalWeges.MadeUpBalance, dbo.EmpMonthlyFinalWeges.WagePay, dbo.EmpMonthlyFinalWeges.DebitsBF, dbo.EmpMonthlyEarnings.DivisionId, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.EPF12, dbo.EmpMonthlyEarnings.EPFPaybleAmount * 22 / 100 AS EPF22, dbo.EmpMonthlyEarnings.ETF3, dbo.EmpMonthlyEarnings.CashPlucking, dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.TotalEarnings,dbo.EmpMonthlyEarnings.monthlyCashScrapKgAmount as MonthlyCashScrapKgAmount FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmpMonthlyFinalWeges ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyFinalWeges.DivisionId AND dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyFinalWeges.EmpNo AND dbo.EmpMonthlyEarnings.Year = dbo.EmpMonthlyFinalWeges.WageYear AND dbo.EmpMonthlyEarnings.Month = dbo.EmpMonthlyFinalWeges.WageMonth INNER JOIN dbo.EmpMonthlyDeductions ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmpMonthlyDeductions.DivisionId AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmpMonthlyDeductions.EmpNo AND dbo.EmpMonthlyFinalWeges.WageYear = dbo.EmpMonthlyDeductions.Year AND dbo.EmpMonthlyFinalWeges.WageMonth = dbo.EmpMonthlyDeductions.Month INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (CONVERT(VARCHAR(10),Category) LIKE '" + intCat + "') ORDER BY dbo.EmpMonthlyEarnings.Category, convert(int,dbo.EmpMonthlyEarnings.EmpNO)", CommandType.Text);
            
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.EMPName, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyDeductions.EPFAmount, dbo.EmpMonthlyDeductions.MonthlyAdvance, dbo.EmpMonthlyDeductions.FestivalAdvance, dbo.EmpMonthlyDeductions.UnionSubscription, dbo.EmpMonthlyDeductions.Cooperative,dbo.EmpMonthlyDeductions.ReligiousActivities, dbo.EmpMonthlyDeductions.TeaCoconutOther, dbo.EmpMonthlyDeductions.FoodStuff,dbo.EmpMonthlyDeductions.Welfare, dbo.EmpMonthlyDeductions.Dhoby, dbo.EmpMonthlyDeductions.Barber, dbo.EmpMonthlyDeductions.Insuarance,dbo.EmpMonthlyDeductions.PreviousDebits, dbo.EmpMonthlyDeductions.PayDetailSlip, dbo.EmpMonthlyDeductions.BankLoan, dbo.EmpMonthlyDeductions.Others, dbo.EmpMonthlyDeductions.TotalDeductions, dbo.EmpMonthlyFinalWeges.SalaryAmount, dbo.EmpMonthlyFinalWeges.MadeUpBalance, dbo.EmpMonthlyFinalWeges.WagePay, dbo.EmpMonthlyFinalWeges.DebitsBF, dbo.EmpMonthlyEarnings.DivisionId, dbo.EmpMonthlyEarnings.EmpNO,dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.EPF12, dbo.EmpMonthlyEarnings.EPFPaybleAmount * 22 / 100 AS EPF22, dbo.EmpMonthlyEarnings.ETF3, dbo.EmpMonthlyEarnings.CashPlucking, dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.TotalEarnings  FROM dbo.EmpMonthlyEarnings INNER JOIN  dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo AND dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmpMonthlyDeductions ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyDeductions.DivisionId AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyDeductions.EmpNo INNER JOIN dbo.EmpMonthlyFinalWeges ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmpMonthlyFinalWeges.DivisionId AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmpMonthlyFinalWeges.EmpNo WHERE (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') ORDER BY dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.EmpNO", CommandType.Text);
            da.Fill(ds, "PaymentCheckRollII");
            return ds;
        }
        public DataSet getDivisionAmalgamation(int intYear, int intMonth, String strDiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.CashManDays, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ExtraRates, dbo.CHKOvertime.Expenditure, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.EPF12, dbo.DailyGroundTransactions.ETF3, dbo.DailyGroundTransactions.IncentiveAmount, dbo.EstateDivision.DivisionName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID LEFT OUTER JOIN dbo.CHKOvertime ON dbo.DailyGroundTransactions.DateEntered = dbo.CHKOvertime.OtDate AND dbo.DailyGroundTransactions.EmpNo = dbo.CHKOvertime.EmployeeNo AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKOvertime.DivisionCode WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "')", CommandType.Text);
            da.Fill(ds, "DivisionAmalgamation");
            return ds;
        }
        //public DataSet getDivisionAmalgamation(int intYear, int intMonth, Int32 EmployeeCategory)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.CashManDays, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ExtraRates, dbo.CHKOvertime.Expenditure, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.EPF12, dbo.DailyGroundTransactions.ETF3, dbo.DailyGroundTransactions.IncentiveAmount, dbo.EstateDivision.DivisionName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo LEFT OUTER JOIN dbo.CHKOvertime ON dbo.DailyGroundTransactions.DateEntered = dbo.CHKOvertime.OtDate AND dbo.DailyGroundTransactions.EmpNo = dbo.CHKOvertime.EmployeeNo AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKOvertime.DivisionCode WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (dbo.EmployeeMaster.EmpCategory = '" + EmployeeCategory + "')", CommandType.Text);
        //    da.Fill(ds, "DivisionAmalgamation");
        //    return ds;
        //}
        public DataSet getDivisionAmalgamation(int intYear, int intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.CashManDays, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ExtraRates, dbo.CHKOvertime.Expenditure, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.EPF12, dbo.DailyGroundTransactions.ETF3, dbo.DailyGroundTransactions.IncentiveAmount, dbo.EstateDivision.DivisionName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID LEFT OUTER JOIN dbo.CHKOvertime ON dbo.DailyGroundTransactions.DateEntered = dbo.CHKOvertime.OtDate AND dbo.DailyGroundTransactions.EmpNo = dbo.CHKOvertime.EmployeeNo AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKOvertime.DivisionCode WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.Fill(ds, "DivisionAmalgamation");
            return ds;
        }
        //public DataSet getDivisionAmalgamation(String DivisionID, int intYear, int intMonth, String EmployeeCategoryCode)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.CashManDays, dbo.DailyGroundTransactions.DailyBasic, dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ExtraRates, dbo.CHKOvertime.Expenditure, dbo.DailyGroundTransactions.OverKgAmount, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.EPF12, dbo.DailyGroundTransactions.ETF3, dbo.DailyGroundTransactions.IncentiveAmount, dbo.EstateDivision.DivisionName FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo LEFT OUTER JOIN dbo.CHKOvertime ON dbo.DailyGroundTransactions.DateEntered = dbo.CHKOvertime.OtDate AND dbo.DailyGroundTransactions.EmpNo = dbo.CHKOvertime.EmployeeNo AND dbo.DailyGroundTransactions.DivisionID = dbo.CHKOvertime.DivisionCode WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (dbo.EmployeeMaster.EmpCategory = '" + EmployeeCategoryCode + "') AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "')", CommandType.Text);
        //    da.Fill(ds, "DivisionAmalgamation");
        //    return ds;
        //}
        public DataSet getEmployeeAttendance(int intYear, int intMonth,int WType)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            if (WType == 1)
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays FROM  dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (WorkType='" + WType + "')   GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo ", CommandType.Text);
            }
            else 
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, COUNT(dbo.DailyGroundTransactions.WorkCodeID) AS ManDays FROM  dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (WorkType='" + WType + "')    GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo ", CommandType.Text);
            }
            //{ fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName
            da.Fill(ds, "EmployeeAttendance");
            return ds;
        }
        public DataSet getEmployeeAttendance(String DivisionID, int intYear, int intMonth, int WType)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            if (WType == 1)
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(dbo.DailyGroundTransactions.ManDays) AS ManDays FROM  dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (dbo.EstateDivision.DivisionID = '" + DivisionID + "') AND  (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (WorkType='" + WType + "')     GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo ", CommandType.Text);
            }
            else
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, COUNT(dbo.DailyGroundTransactions.WorkCodeID) AS ManDays FROM  dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (dbo.EstateDivision.DivisionID = '" + DivisionID + "') AND  (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (WorkType='" + WType + "')     GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }

                //{ fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName
            da.Fill(ds, "EmployeeAttendance");
            return ds;
        }
        public DataSet getEmployeeAttendance(int intYear, int intMonth, String EmployeeCategory)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo,dbo.EmployeeMaster.EmpName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.ManDays FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo  WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (dbo.DailyGroundTransactions.CropType = 1)", CommandType.Text);
            da.Fill(ds, "EmployeeAttendance");
            return ds;
        }
        public DataSet getEmployeeAttendance(String DivisionID, int intYear, int intMonth, String EmployeeCategoryCode)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName,dbo.DailyGroundTransactions.EmpNo,dbo.EmployeeMaster.EmpName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.ManDays FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo  WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (dbo.DailyGroundTransactions.CropType = 1) AND (dbo.EstateDivision.DivisionID = '" + DivisionID + "') ", CommandType.Text);
            da.Fill(ds, "EmployeeAttendance");
            return ds;
        }
        public DataSet getWorkDistributionDetail(int intYear, int intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.OverKgs FROM dbo.EmployeeCategory INNER JOIN dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID ON dbo.EmployeeCategory.CategoryID = dbo.EmployeeMaster.EmpCategory INNER JOIN dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "')", CommandType.Text);
            da.Fill(ds, "WorkDistributionDetail");
            return ds;
        }
        public DataSet getWorkDistributionDetail(int intYear, int intMonth, String EmployeeCategory)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.OverKgs FROM dbo.EmployeeCategory INNER JOIN dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID ON dbo.EmployeeCategory.CategoryID = dbo.EmployeeMaster.EmpCategory INNER JOIN dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "')  AND (dbo.EmployeeCategory.CategoryID = '" + EmployeeCategory + "')", CommandType.Text);
            da.Fill(ds, "WorkDistributionDetail");
            return ds;
        }
        public DataSet getWorkDistributionDetail(String DivisionID, int intYear, int intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.OverKgs FROM dbo.EmployeeCategory INNER JOIN dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID ON dbo.EmployeeCategory.CategoryID = dbo.EmployeeMaster.EmpCategory INNER JOIN dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.EstateDivision.DivisionID = '" + DivisionID + "')", CommandType.Text);
            da.Fill(ds, "WorkDistributionDetail");
            return ds;
        }

        public DataSet getWorkDistributionDetail(String DivisionID, int intYear, int intMonth, String EmployeeCategoryCode)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.JobMaster.JobName, dbo.JobGroup.GroupName, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.OverKgs FROM dbo.EmployeeCategory INNER JOIN dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID ON dbo.EmployeeCategory.CategoryID = dbo.EmployeeMaster.EmpCategory INNER JOIN dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.EmployeeCategory.CategoryID = '" + EmployeeCategoryCode + "') AND (dbo.EstateDivision.DivisionID = '" + DivisionID + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '       ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.JobMaster.JobShortName as JobName,  dbo.JobGroup.GroupName, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, dbo.DailyGroundTransactions.WorkQty,  dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.OverKgs FROM         dbo.EmployeeCategory INNER JOIN dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID ON  dbo.EmployeeCategory.CategoryID = dbo.EmployeeMaster.EmpCategory INNER JOIN dbo.JobGroup INNER JOIN dbo.JobMaster ON dbo.JobGroup.GroupID = dbo.JobMaster.JobGroup ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND  (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.EstateDivision.DivisionID LIKE '" + DivisionID + "') AND (CONVERT(varchar(50),  dbo.EmployeeCategory.CategoryID) LIKE '" + EmployeeCategoryCode + "')", CommandType.Text);
            da.Fill(ds, "WorkDistributionDetail");
            return ds;
        }
        public DataSet getGreenLeafRegister(int intYear, int intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkQty, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '        ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.EmployeeCategory.CategoryName, dbo.EmployeeCategory.CategoryID, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkQty, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '        ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.EmployeeCategory.CategoryName, dbo.EmployeeCategory.CategoryID, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')", CommandType.Text);
            da.Fill(ds, "GreenLeafRegister");
            return ds;
        }
        public DataSet getGreenLeafRegister(int intYear, int intMonth, String EmployeeCategory)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkType,  dbo.DailyGroundTransactions.WorkQty, CONVERT(VARCHAR(50), dbo.DailyGroundTransactions.EmpNo) + '   ' + CONVERT(VARCHAR(50),  dbo.EmployeeMaster.EMPName) + '   (' + CONVERT(VARCHAR(50), dbo.EmployeeMaster.DivisionID) + ')' AS EmpNoNEmpName,  dbo.EmployeeCategory.CategoryName, dbo.EmployeeCategory.CategoryID, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs FROM         dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE     (dbo.DailyGroundTransactions.CropType = 1) AND (dbo.EmployeeCategory.CategoryID = '" + EmployeeCategory + "') AND  (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND  (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')", CommandType.Text);
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkQty,  { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '        ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.EmployeeCategory.CategoryName, dbo.EmployeeCategory.CategoryID, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK')", CommandType.Text);
            da.Fill(ds, "GreenLeafRegister");
            return ds;
           
        }
        public DataSet getGreenLeafRegister(String DivisionID, int intYear, int intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkQty, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '        ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.EmployeeCategory.CategoryName, dbo.EmployeeCategory.CategoryID, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE     (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered)  = '" + intYear + "') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "')", CommandType.Text);
            da.Fill(ds, "GreenLeafRegister");
            return ds;
        }
        public DataSet getGreenLeafRegister(String DivisionID, int intYear, int intMonth, String EmployeeCategoryCode)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkType, dbo.DailyGroundTransactions.WorkQty, { fn CONCAT({ fn CONCAT(dbo.DailyGroundTransactions.EmpNo, '      ') }, dbo.EmployeeMaster.EMPName) } AS EmpNoNEmpName, dbo.EmployeeCategory.CategoryName, dbo.EmployeeCategory.CategoryID, dbo.DailyGroundTransactions.NormKilos, dbo.DailyGroundTransactions.OverKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.DailyGroundTransactions.CropType = 1) AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (dbo.DailyGroundTransactions.DivisionID = '" + DivisionID + "') AND (dbo.EmployeeCategory.CategoryID = '" + EmployeeCategoryCode + "')", CommandType.Text);
            da.Fill(ds, "GreenLeafRegister");
            return ds;
        }
        public DataSet getDivisionWiseGreenLeafSummery( String EmployeeCategoryCode, DateTime StartDate, DateTime EndDate)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.DailyGroundTransactions.DivisionID, SUM(dbo.DailyGroundTransactions.WorkQty) AS WorkQty,  SUM(dbo.DailyGroundTransactions.OverKgs) AS OverKgs, SUM(dbo.DailyGroundTransactions.CashKgs) AS CashKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.WorkCodeID = 'plk') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+StartDate+"', 102) AND CONVERT(DATETIME, '"+EndDate+"', 102)) AND (CONVERT(varchar(50), dbo.EmployeeMaster.EmpCategory)  LIKE '"+EmployeeCategoryCode+"') GROUP BY dbo.DailyGroundTransactions.DivisionID ORDER BY dbo.DailyGroundTransactions.DivisionID", CommandType.Text);
            da.Fill(ds, "GreenLeafSummery");
            return ds;
        }

        public DataSet getFieldWiseGreenLeafSummery(String DivisionCode,String EmployeeCategoryCode, DateTime StartDate, DateTime EndDate)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, SUM(dbo.DailyGroundTransactions.WorkQty)  AS WorkQty, SUM(dbo.DailyGroundTransactions.OverKgs) AS OverKgs, SUM(dbo.DailyGroundTransactions.CashKgs) AS CashKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.WorkCodeID = 'plk') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+StartDate+"', 102) AND CONVERT(DATETIME, '"+EndDate+"', 102)) AND (CONVERT(varchar(50), dbo.EmployeeMaster.EmpCategory)  LIKE '"+EmployeeCategoryCode+"') AND (dbo.DailyGroundTransactions.DivisionID LIKE '"+DivisionCode+"') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID ORDER BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT        TOP (100) PERCENT CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'Lent Labour')  THEN dbo.DailyGroundTransactions.LabourDivision ELSE dbo.DailyGroundTransactions.LabourEstate END END AS DivisionID,  CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'Lent Labour')  THEN dbo.DailyGroundTransactions.LabourField ELSE dbo.DailyGroundTransactions.LabourEstate END END AS FieldID, WorkQty, OverKgs, CashKgs FROM            dbo.DailyGroundTransactions WHERE        (WorkCodeID = 'plk') AND (DateEntered BETWEEN CONVERT(DATETIME, '" + StartDate + "', 102) AND CONVERT(DATETIME, '" + EndDate + "', 102)) AND  (CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'General')  THEN dbo.DailyGroundTransactions.DivisionID ELSE CASE WHEN (dbo.DailyGroundTransactions.LabourType = 'Lent Labour')  THEN dbo.DailyGroundTransactions.LabourDivision ELSE dbo.DailyGroundTransactions.LabourEstate END END LIKE '" + DivisionCode + "') ORDER BY DivisionID, FieldID", CommandType.Text);
            da.Fill(ds, "GreenLeafFieldSummery");
            return ds;
        }

        ////public DataTable getSalarySlips(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName)
        ////{
        ////    DataTable dt = new DataTable();
        ////    dt.Columns.Add("DivisionName");
        ////    dt.Columns.Add("CatagoryName");
        ////    dt.Columns.Add("Type");
        ////    dt.Columns.Add("SalaryItemName");
        ////    dt.Columns.Add("EmployeeName");
        ////    dt.Columns.Add("EMPNo");
        ////    dt.Columns.Add("EPFNo");
        ////    dt.Columns.Add("Qty");
        ////    dt.Columns.Add("Amount");
        ////    dt.Columns.Add("Balance");
        ////    dt.Columns.Add("Debits");
        ////    dt.Columns.Add("Coins B/F");

        ////    ///2011-08-02
        ////    dt.Columns.Add("TotalDays");
        ////    dt.Columns.Add("NormalDays");
        ////    dt.Columns.Add("HoliDays");
        ////    dt.Columns.Add("DailyBasic");
        ////    dt.Columns.Add("DivisionID");
        ////    dt.Columns.Add("NICNo");

        ////    DataRow dtRow;
        ////    SqlDataReader reader;
        ////    SqlDataReader readerEmployee;
        ////    SqlDataReader readerEmployeeEarnings;
        ////    SqlDataReader readerEmployeeDeductions;
        ////    SqlDataReader readerEmployeeFinal;
        ////    SqlDataReader readerDays;
        ////    SqlDataReader readerDetails;

        ////    dtRow = dt.NewRow();
        ////    //Additions
        ////    reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings') AND (SalryItemName <> 'Previous Made Up Coins') ORDER BY SeqId", CommandType.Text);
        ////    while (reader.Read())
        ////    {
        ////        //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
        ////        readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

        ////        while (readerEmployee.Read())
        ////        {
        ////            dtRow = dt.NewRow();
        ////            dtRow[0] = DivisionName;
        ////            dtRow[1] = CategoryName;
        ////            dtRow[2] = "Additions";

        ////            if (!reader.IsDBNull(0))
        ////            {
        ////                dtRow[3] = reader.GetString(0).Trim();
        ////            }

        ////            if (!readerEmployee.IsDBNull(2))
        ////            {
        ////                dtRow[4] = readerEmployee.GetString(2).Trim();
        ////            }
        ////            if (!readerEmployee.IsDBNull(0))
        ////            {
        ////                dtRow[5] = readerEmployee.GetString(0).Trim();
        ////            }
        ////            if (!readerEmployee.IsDBNull(1))
        ////            {
        ////                dtRow[6] = readerEmployee.GetString(1).Trim();
        ////            }

        ////            //Days
        ////            readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
        ////            while (readerDays.Read())
        ////            {
        ////                if (!readerDays.IsDBNull(0))
        ////                {
        ////                    dtRow[12] = readerDays.GetDecimal(0);
        ////                }
        ////                else
        ////                    dtRow[12] = 0.0;
        ////                if (!readerDays.IsDBNull(1))
        ////                {
        ////                    dtRow[13] = readerDays.GetDecimal(1);
        ////                }
        ////                else
        ////                    dtRow[13] = 0.0;
        ////                if (!readerDays.IsDBNull(1))
        ////                {
        ////                    dtRow[14] = readerDays.GetDecimal(2);
        ////                }
        ////                else
        ////                    dtRow[14] = 0.0;
        ////                if (!readerDays.IsDBNull(1))
        ////                {
        ////                    dtRow[15] = readerDays.GetDecimal(3);
        ////                }
        ////                else
        ////                    dtRow[15] = 0.0;
        ////            }
        ////            readerDays.Close();

        ////            //OtherDetails
        ////            readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
        ////            while (readerDetails.Read())
        ////            {
        ////                if (!readerDetails.IsDBNull(0))
        ////                {
        ////                    dtRow[16] = readerDetails.GetString(0).Trim();
        ////                }
        ////                else
        ////                    dtRow[16] = "N/A";
        ////                if (!readerDetails.IsDBNull(1))
        ////                {
        ////                    dtRow[17] = readerDetails.GetString(1).Trim();
        ////                }
        ////                else
        ////                {
        ////                    dtRow[17] = "NT";
        ////                }
        ////            }
        ////            readerDetails.Close();

        ////            //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
        ////            readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions + PreviousMadeUpCoins AS OtherAdditions FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

        ////            while (readerEmployeeEarnings.Read())
        ////            {
        ////                if (reader.GetString(0).Trim() == "Plucking")
        ////                {
        ////                    if (!readerEmployeeEarnings.IsDBNull(1))
        ////                        dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
        ////                    else
        ////                        dtRow[7] = 0;
        ////                    if (!readerEmployeeEarnings.IsDBNull(0))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Tea")
        ////                {
        ////                    dtRow[7] = 0;
        ////                    dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Tapping")
        ////                {
        ////                    dtRow[7] = 0;
        ////                    dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "RubberSundry")
        ////                {
        ////                    dtRow[7] = 0;
        ////                    dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Extra Rates")
        ////                {
        ////                    dtRow[7] = 0;

        ////                    if (!readerEmployeeEarnings.IsDBNull(2))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Over Kilos")
        ////                {
        ////                    if (!readerEmployeeEarnings.IsDBNull(3))
        ////                        dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

        ////                    else
        ////                        dtRow[7] = 0;
        ////                    if (!readerEmployeeEarnings.IsDBNull(4))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Overtime")
        ////                {
        ////                    dtRow[7] = 0;

        ////                    if (!readerEmployeeEarnings.IsDBNull(5))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Cash work")
        ////                {
        ////                    dtRow[7] = 0;

        ////                    if (!readerEmployeeEarnings.IsDBNull(6))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Attendance Incentive")
        ////                {
        ////                    dtRow[7] = 0;

        ////                    if (!readerEmployeeEarnings.IsDBNull(7))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "PRI")
        ////                {
        ////                    dtRow[7] = 0;

        ////                    if (!readerEmployeeEarnings.IsDBNull(8))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                if (reader.GetString(0).Trim() == "Other")
        ////                {
        ////                    dtRow[7] = 0;

        ////                    if (!readerEmployeeEarnings.IsDBNull(9))
        ////                        dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
        ////                    else
        ////                        dtRow[8] = 0;
        ////                }
        ////                //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
        ////                //{
        ////                //    dtRow[7] = 0;

        ////                //    if (!readerEmployeeEarnings.IsDBNull(10))
        ////                //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
        ////                //    else
        ////                //        dtRow[8] = 0;
        ////                //}
        ////            }
        ////            readerEmployeeEarnings.Close();
        ////            //--------------
        ////            readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
        ////            while (readerEmployeeFinal.Read())
        ////            {

        ////                if (!readerEmployeeFinal.IsDBNull(0))
        ////                    dtRow[9] = readerEmployeeFinal.GetDecimal(0);
        ////                else
        ////                    dtRow[9] = 0;
        ////                if (!readerEmployeeFinal.IsDBNull(1))
        ////                    dtRow[10] = readerEmployeeFinal.GetDecimal(1);
        ////                else
        ////                    dtRow[10] = 0;
        ////                if (!readerEmployeeFinal.IsDBNull(2))
        ////                    dtRow[11] = readerEmployeeFinal.GetDecimal(2);
        ////                else
        ////                    dtRow[11] = 0;
        ////            }
        ////            readerEmployeeFinal.Close();
        ////            //--------

        ////            dt.Rows.Add(dtRow);
        ////        }
        ////        readerEmployee.Close();
        ////    }
        ////    reader.Close();

        ////    //Deductions
        ////    reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
        ////    while (reader.Read())
        ////    {
        ////        //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
        ////        readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0)", CommandType.Text);
        ////        while (readerEmployee.Read())
        ////        {
        ////            dtRow = dt.NewRow();
        ////            dtRow[0] = DivisionName;
        ////            dtRow[1] = CategoryName;
        ////            dtRow[2] = "Deductions";

        ////            if (!reader.IsDBNull(0))
        ////            {
        ////                dtRow[3] = reader.GetString(0).Trim();
        ////            }
        ////            if (!readerEmployee.IsDBNull(2))
        ////            {
        ////                dtRow[4] = readerEmployee.GetString(2).Trim();
        ////            }
        ////            if (!readerEmployee.IsDBNull(0))
        ////            {
        ////                dtRow[5] = readerEmployee.GetString(0).Trim();
        ////            }
        ////            if (!readerEmployee.IsDBNull(1))
        ////            {
        ////                dtRow[6] = readerEmployee.GetString(1).Trim();
        ////            }

        ////            //Days
        ////            readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
        ////            while (readerDays.Read())
        ////            {
        ////                if (!readerDays.IsDBNull(0))
        ////                {
        ////                    dtRow[12] = readerDays.GetDecimal(0);
        ////                }
        ////                else
        ////                    dtRow[12] = 0.0;
        ////                if (!readerDays.IsDBNull(1))
        ////                {
        ////                    dtRow[13] = readerDays.GetDecimal(1);
        ////                }
        ////                else
        ////                    dtRow[13] = 0.0;
        ////                if (!readerDays.IsDBNull(1))
        ////                {
        ////                    dtRow[14] = readerDays.GetDecimal(2);
        ////                }
        ////                else
        ////                    dtRow[14] = 0.0;
        ////                if (!readerDays.IsDBNull(1))
        ////                {
        ////                    dtRow[15] = readerDays.GetDecimal(3);
        ////                }
        ////                else
        ////                    dtRow[15] = 0.0;
        ////            }
        ////            readerDays.Close();

        ////            //OtherDetails
        ////            readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
        ////            while (readerDetails.Read())
        ////            {
        ////                if (!readerDetails.IsDBNull(0))
        ////                {
        ////                    dtRow[16] = readerDetails.GetString(0).Trim();
        ////                }
        ////                else
        ////                    dtRow[16] = "N/A";
        ////                if (!readerDetails.IsDBNull(1))
        ////                {
        ////                    dtRow[17] = readerDetails.GetString(1).Trim();
        ////                }
        ////                else
        ////                {
        ////                    dtRow[17] = "NT";
        ////                }
        ////            }
        ////            readerDetails.Close();

        ////            readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
        ////            while (readerEmployeeDeductions.Read())
        ////            {
        ////                dtRow[7] = 0;

        ////                if (!readerEmployeeDeductions.IsDBNull(0))
        ////                    dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
        ////                else
        ////                    dtRow[8] = 0;
        ////            }
        ////            readerEmployeeDeductions.Close();

        ////            dt.Rows.Add(dtRow);
        ////            //--------------
        ////            readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
        ////            while (readerEmployeeFinal.Read())
        ////            {

        ////                if (!readerEmployeeFinal.IsDBNull(0))
        ////                    dtRow[9] = readerEmployeeFinal.GetDecimal(0);
        ////                else
        ////                    dtRow[9] = 0;
        ////                if (!readerEmployeeFinal.IsDBNull(1))
        ////                    dtRow[10] = readerEmployeeFinal.GetDecimal(1);
        ////                else
        ////                    dtRow[10] = 0;
        ////                if (!readerEmployeeFinal.IsDBNull(2))
        ////                    dtRow[11] = readerEmployeeFinal.GetDecimal(2);
        ////                else
        ////                    dtRow[11] = 0;
        ////            }
        ////            readerEmployeeFinal.Close();
        ////            //--------
        ////        }
        ////        readerEmployee.Close();
        ////    }
        ////    reader.Close();
        ////    return dt;
        ////}

        public DataTable getSalarySlips(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo,SinhalaItemName FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings') AND (SalryItemName <> 'Previous Made Up Coins') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(1).Trim();  
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions + PreviousMadeUpCoins AS OtherAdditions FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tapping")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "RubberSundry")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0)", CommandType.Text);
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(1).Trim();  
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        //Sinhala and Tamil payslip
        public DataTable getSalarySlipsSinTamil(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("SinhalaTerm");//18

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo,SinhalaItemName FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings') AND (SalryItemName <> 'Previous Made Up Coins') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(1).Trim();
                    }
                    if (!reader.IsDBNull(3))
                    {
                        dtRow[18] = reader.GetString(3).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                       

                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions + PreviousMadeUpCoins AS OtherAdditions FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tapping")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "RubberSundry")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo,SinhalaItemName FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0)", CommandType.Text);
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(1).Trim();
                    }
                    if (!reader.IsDBNull(3))
                    {
                        dtRow[18] = reader.GetString(3).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        //pre printed payslip
        public DataTable getSalarySlipsPrePrinted(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings') AND (SalryItemName <> 'Previous Made Up Coins') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (CashWorkPayslipAvailable())
                {
                    //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '"+inCat+"') ", CommandType.Text);
                    /*totalearnigs>0 deleted as requested by Bogawana*/
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "')  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')  ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '"+inCat+"')", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions + PreviousMadeUpCoins AS OtherAdditions FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }                       
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();
                            

                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (CashWorkPayslipAvailable())
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')  ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') +" "+reader.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }


                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    StrOtherDeductions1 = "";
                    StrOtherDeductionsTemp = "";

                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";
                        
                    }
                    if (StrOtherDeductions.Length > 42)
                    {
                        StrOtherDeductionsTemp = StrOtherDeductions;
                        StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                        StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                        StrOtherDeductionsTemp = "";
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    dtRow[24] = StrOtherDeductions1;

                    readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrLoanDeductions = "";
                    dtRow[22] = "-";
                    DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    DSotHours.Dispose();
                    while (readerEmpLoanDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpLoanDeduction.IsDBNull(1))
                            StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpLoanDeduction.IsDBNull(0))
                            StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                        if (StrLoanDeductions.Length > 42)
                        {
                            StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                        }
                    }
                    readerEmpLoanDeduction.Close();
                    dtRow[22] = StrLoanDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }
        //pre Printed BPL payslip
        public DataTable getSalarySlipsPrePrintedBPLOLAX(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat, Boolean boolCWPayslip, String strEmp)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");
            dt.Columns.Add("PRIPSS");
            dt.Columns.Add("ItemNameSinhala");
            dt.Columns.Add("NamePlk");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            SqlDataReader readerNamePlk;
            SqlDataReader readerDailyBasic;
            DataSet DSDailyBasic = new DataSet();
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";
            Decimal decWagePay = 0;
            Decimal decDebitsBF = 0;
            Decimal decMadeUpBalance = 0;
            String tempDivision = "";
            String tempNIC = "";
            String OTHours = "";
            String stritem = "";
            String strNamePlk = "";

            dtRow = dt.NewRow();
            readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays, 'PRI:'+convert(varchar(50),dbo.EmpMonthlyEarnings.PRIAmount)+'-PSS:' + convert(varchar(50),dbo.EmpMonthlyEarnings.PSSAmount) as PRIPSS FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.TotalEarnings > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') AND (dbo.EmployeeMaster.EmpNo='" + strEmp + "')", CommandType.Text);

            while (readerEmployee.Read())
            {
                StrLoanDeductions = "";
                StrOtherDeductions = "";
                OTHours = "";
                StrOtherDeductions1 = "";
                StrLoanDeductions = "";
                dtRow[27] = "PRI:0/PSS:0";
                dtRow[29] = "0";

                readerNamePlk = SQLHelper.ExecuteReader("SELECT SUM(CashManDays) AS Expr1, SUM(CashManDays * 500) AS CashNamePay, SUM(OverKgs) AS Okgs, SUM(OverKgs * 25) AS OkgAmounts, SUM(CashKgAmount)  AS CashKgAmt FROM            dbo.DailyGroundTransactions WHERE        (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "') AND (CashPlkOkgYesNo = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "')  ", CommandType.Text);
                dtRow[29] = "NamePlk:0";
                strNamePlk = "";
                while (readerNamePlk.Read())
                {
                    
                    if (!readerNamePlk.IsDBNull(0))
                        strNamePlk += "Names:" + readerNamePlk.GetDecimal(0).ToString() + " ";
                    if (!readerNamePlk.IsDBNull(1))
                        strNamePlk += "NamePay:" + readerNamePlk.GetDecimal(1).ToString("N2") + ", ";
                    if (!readerNamePlk.IsDBNull(2))
                        strNamePlk += "Okg:" + readerNamePlk.GetDecimal(2).ToString() + "\n ";
                    if (!readerNamePlk.IsDBNull(3))
                        strNamePlk += "Okg Amt:" + readerNamePlk.GetDecimal(3) + ", ";
                    if(!readerNamePlk.IsDBNull(4))
                        strNamePlk += "Total:" + readerNamePlk.GetDecimal(4).ToString("N2") + ", ";
                   
                }
                if (!String.IsNullOrEmpty(strNamePlk))
                    dtRow[29] = strNamePlk;
                else
                    dtRow[29] = "NamePlk:0";
                readerNamePlk.Close();


                readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                while (readerDetails.Read())
                {
                    if (!readerDetails.IsDBNull(0))
                    {
                        tempDivision = readerDetails.GetString(0).Trim();
                    }
                    else
                        tempDivision = "N/A";
                    if (!readerDetails.IsDBNull(1))
                    {
                        tempNIC = readerDetails.GetString(1).Trim();
                    }
                    else
                    {
                        tempNIC = "NT";
                    }
                }
                readerDetails.Close();

                readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                StrOtherDeductions = "";
                StrOtherDeductions1 = "";
                StrOtherDeductionsTemp = "";

                StrOtherDeductions = "-";
                while (readerEmpOtherDeduction.Read())
                {
                    //dtRow[19] = "-";
                    if (!readerEmpOtherDeduction.IsDBNull(1))
                        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    if (!readerEmpOtherDeduction.IsDBNull(0))
                        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                }
                if (StrOtherDeductions.Length > 42)
                {
                    StrOtherDeductionsTemp = StrOtherDeductions;
                    StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                    StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                    StrOtherDeductionsTemp = "";
                }
                readerEmpOtherDeduction.Close();
                //dtRow[19] = StrOtherDeductions;
                //dtRow[24] = StrOtherDeductions1;

                readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                StrLoanDeductions = "";
                dtRow[22] = "-";
                DataSet DSotHours1 = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                //dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                OTHours = DSotHours1.Tables[0].Rows[0][0].ToString();
                DSotHours1.Dispose();
                while (readerEmpLoanDeduction.Read())
                {
                    //dtRow[19] = "-";
                    if (!readerEmpLoanDeduction.IsDBNull(1))
                        StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                    if (!readerEmpLoanDeduction.IsDBNull(0))
                        StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                    if (StrLoanDeductions.Length > 42)
                    {
                        StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                    }
                }
                readerEmpLoanDeduction.Close();

                

                //dtRow[22] = StrLoanDeductions;

                //dtRow[19] = StrOtherDeductions;

                //dtRow[23] = OTHours;
                //dtRow[24] = StrOtherDeductions1;
                //dtRow[22] = StrLoanDeductions;

                decWagePay = 0;
                decDebitsBF = 0;
                decMadeUpBalance = 0;
                readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);

                readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                while (readerEmployeeFinal.Read())
                {

                    if (!readerEmployeeFinal.IsDBNull(0))
                        decWagePay = readerEmployeeFinal.GetDecimal(0);
                    else
                        decWagePay = 0;
                    if (!readerEmployeeFinal.IsDBNull(1))
                        decDebitsBF = readerEmployeeFinal.GetDecimal(1);
                    else
                        decDebitsBF = 0;
                    if (!readerEmployeeFinal.IsDBNull(2))
                        decMadeUpBalance = readerEmployeeFinal.GetDecimal(2);
                    else
                        decMadeUpBalance = 0;
                }

                readerEmployeeFinal.Close();

                //Additions
                reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo, SinhalaItemName FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings')  ORDER BY SeqId", CommandType.Text);
                while (reader.Read())
                {


                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                        if (reader.GetString(0).Trim().Equals("PRI"))
                        {
                            stritem = dtRow[3].ToString();
                        }
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                        dtRow[28] = reader.GetString(3).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }
                    if (!readerEmployee.IsDBNull(5))
                    {
                        dtRow[22] = readerEmployee.GetString(5);
                    }

                    //Days

                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }


                    ////OtherDetails
                    dtRow[16] = tempDivision;
                    dtRow[17] = tempNIC;
                    //readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    //while (readerDetails.Read())
                    //{
                    //    if (!readerDetails.IsDBNull(0))
                    //    {
                    //        dtRow[16] = readerDetails.GetString(0).Trim();
                    //    }
                    //    else
                    //        dtRow[16] = "N/A";
                    //    if (!readerDetails.IsDBNull(1))
                    //    {
                    //        dtRow[17] = readerDetails.GetString(1).Trim();
                    //    }
                    //    else
                    //    {
                    //        dtRow[17] = "NT";
                    //    }
                    //}
                    //readerDetails.Close();


                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos  AS NormalOkgs, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount+PSSAmount, OtherAdditions AS OtherAdditions ,PreviousMadeUpCoins,PRIAmount,PSSAmount  FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins, PSSAmount", CommandType.Text);
                    DSDailyBasic = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS ManDays, SUM(DailyBasic) AS TotPay, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN DailyBasic ELSE 0 END) AS PLKPay,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN DailyBasic ELSE 0 END) AS TAPPay, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS TeaSundry, SUM(CASE WHEN (CropType = 2) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS RubberSundry, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PLKManDays,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ManDays ELSE 0 END) AS TAPManDays, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN ManDays ELSE 0 END ELSE 0 END) AS TeaSundryManDays, SUM(CASE WHEN (CropType = 2) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN ManDays ELSE 0 END ELSE 0 END) AS RubberSundryManDays, SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgs ELSE 0 END) AS ScrapKgs,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgAmount ELSE 0 END) AS ScrapAmount FROM dbo.DailyGroundTransactions WHERE (WorkType = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')", CommandType.Text);
                 
                    
                   
                    while (readerEmployeeEarnings.Read())
                    {
                        
                            if (reader.GetString(0).Trim() == "Plucking")
                            {
                                //if (!readerEmployeeEarnings.IsDBNull(1))
                                //    dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                                //else
                                //    dtRow[7] = 0;
                                //if (!readerEmployeeEarnings.IsDBNull(0))
                                //    dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                                //else
                                //    dtRow[8] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][6].ToString()))
                                    dtRow[7] = DSDailyBasic.Tables[0].Rows[0][6].ToString();
                                else
                                    dtRow[7] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][2].ToString()))
                                    dtRow[8] = DSDailyBasic.Tables[0].Rows[0][2].ToString();
                                else
                                    dtRow[8] = 0;
                            }
                            if (reader.GetString(0).Trim() == "Tea")
                            {
                                //dtRow[7] = 0;
                                //dtRow[8] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][8].ToString()))
                                    dtRow[7] = DSDailyBasic.Tables[0].Rows[0][8].ToString();
                                else
                                    dtRow[7] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][4].ToString()))
                                    dtRow[8] = DSDailyBasic.Tables[0].Rows[0][4].ToString();
                                else
                                    dtRow[8] = 0;
                            }
                            if (reader.GetString(0).Trim() == "Tapping")
                            {
                                //dtRow[7] = 0;
                                //dtRow[8] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][7].ToString()))
                                    dtRow[7] = DSDailyBasic.Tables[0].Rows[0][7].ToString();
                                else
                                    dtRow[7] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][3].ToString()))
                                    dtRow[8] = DSDailyBasic.Tables[0].Rows[0][3].ToString();
                                else
                                    dtRow[8] = 0;
                            }
                            if (reader.GetString(0).Trim() == "Rubber")
                            {
                                //dtRow[7] = 0;
                                //dtRow[8] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][9].ToString()))
                                    dtRow[7] = DSDailyBasic.Tables[0].Rows[0][9].ToString();
                                else
                                    dtRow[7] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][5].ToString()))
                                    dtRow[8] = DSDailyBasic.Tables[0].Rows[0][5].ToString();
                                else
                                    dtRow[8] = 0;
                            }

                            if (reader.GetString(0).Trim() == "Scrap")
                            {
                                //dtRow[7] = 0;
                                //dtRow[8] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][10].ToString()))
                                    dtRow[7] = DSDailyBasic.Tables[0].Rows[0][10].ToString();
                                else
                                    dtRow[7] = 0;
                                if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][11].ToString()))
                                    dtRow[8] = DSDailyBasic.Tables[0].Rows[0][11].ToString();
                                else
                                    dtRow[8] = 0;
                            }
                        
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();


                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                                if (readerEmployeeEarnings.GetDecimal(8) > 0)
                                {
                                    dtRow[27] = "";
                                    if (!readerEmployeeEarnings.IsDBNull(11))
                                    {
                                        dtRow[27] = "PRI:" + readerEmployeeEarnings.GetDecimal(11).ToString();
                                    }
                                    if (!readerEmployeeEarnings.IsDBNull(12))
                                    {
                                        dtRow[27] = dtRow[27] + "/PSS:" + readerEmployeeEarnings.GetDecimal(12).ToString();
                                    }
                                }
                            }
                            else
                                dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(10))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                            else
                                dtRow[8] = 0;
                        }
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    dtRow[9] = decWagePay;
                    dtRow[10] = decDebitsBF;
                    dtRow[11] = decMadeUpBalance;
                    dtRow[29] = strNamePlk;

                    //--------

                    dt.Rows.Add(dtRow);

                }
                reader.Close();



                //Deductions

                //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
                //reader = SQLHelper.ExecuteReader(" select T1.SalryItemName,T1.ItemNO, sum(T1.Amount) as amount from (   SELECT     TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber') then 'Dhoby' else dbo.CHKWageItemSequenceOLAX.SalryItemName end as SalryItemName, CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber') then 11 else dbo.CHKWageItemSequenceOLAX.ItemNO end as ItemNO , SUM(dbo.CHKEmpDeductions.Amount)  AS Amount FROM         dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE     (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO, dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) t1 group by T1.SalryItemName,T1.ItemNO", CommandType.Text);
                reader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT SalryItemName as SalName, ItemNO,isnull( (SELECT     SUM(Amount) AS amount FROM          (SELECT     TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber')  THEN 'Dhoby' ELSE dbo.CHKWageItemSequenceOLAX.SalryItemName END AS SalryItemName,  CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber')  THEN 11 ELSE dbo.CHKWageItemSequenceOLAX.ItemNO END AS ItemNO, SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM          dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE      (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO,  dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) AS t1 WHERE      (T1.SalryItemName = CHKWageItemSequenceOLAX_1.SalryItemName) GROUP BY SalryItemName, ItemNO),0) AS Expr1,TamilItemName,SinhalaItemName FROM         dbo.CHKWageItemSequenceOLAX AS CHKWageItemSequenceOLAX_1 WHERE     (SalaryItemType = 'Deductions') ", CommandType.Text);
                while (reader.Read())
                {

                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(1);
                    dtRow[26] = "NA";
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(3).Trim();
                        dtRow[28] = reader.GetString(4).Trim();
                        dtRow[26] = reader.GetString(3).Trim();
                    }
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(1).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim() + " " + "NA";
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days

                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }


                    ////OtherDetails
                    dtRow[16] = tempDivision;
                    dtRow[17] = tempNIC;
                    //readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    //while (readerDetails.Read())
                    //{
                    //    if (!readerDetails.IsDBNull(0))
                    //    {
                    //        dtRow[16] = readerDetails.GetString(0).Trim();
                    //    }
                    //    else
                    //        dtRow[16] = "N/A";
                    //    if (!readerDetails.IsDBNull(1))
                    //    {
                    //        dtRow[17] = readerDetails.GetString(1).Trim();
                    //    }
                    //    else
                    //    {
                    //        dtRow[17] = "NT";
                    //    }
                    //}
                    //readerDetails.Close();

                    String str = reader.GetString(0).Trim();
                    String st = "";
                    //if (readerEmployee.GetString(0).Trim() == "1450")
                    //{
                    //    if (readerEmployee.GetString(0).Trim().ToUpper().Equals("1450") && (str.Equals("Stamp Duty")))
                    //    {
                    //        st = "here, ";
                    //    }
                    //    str = reader.GetString(0).Trim();
                    //    if (str == "Dhoby")
                    //    {
                    //        str = "Dhoby";
                    //    }
                    //}

                    //new 2016-01-06

                    dtRow[7] = 0;

                    if (!reader.IsDBNull(2))
                        dtRow[8] = reader.GetDecimal(2);
                    else
                        dtRow[8] = 0;
                    //
                    //if (reader.GetString(0).Trim().Equals("Dhoby"))
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //else
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //while (readerEmployeeDeductions.Read())
                    //{
                    //    dtRow[7] = 0;

                    //    if (!readerEmployeeDeductions.IsDBNull(0))
                    //        dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                    //    else
                    //        dtRow[8] = 0;
                    //}
                    //readerEmployeeDeductions.Close();


                    dtRow[19] = StrOtherDeductions;

                    dtRow[23] = OTHours;
                    dtRow[24] = StrOtherDeductions1;
                    dtRow[22] = StrLoanDeductions;
                    //readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrOtherDeductions = "";
                    //StrOtherDeductions1 = "";
                    //StrOtherDeductionsTemp = "";

                    //dtRow[19] = "-";
                    //while (readerEmpOtherDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(1))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(0))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    //}
                    //if (StrOtherDeductions.Length > 42)
                    //{
                    //    StrOtherDeductionsTemp = StrOtherDeductions;
                    //    StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                    //    StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                    //    StrOtherDeductionsTemp = "";
                    //}
                    //readerEmpOtherDeduction.Close();
                    //dtRow[19] = StrOtherDeductions;
                    //dtRow[24] = StrOtherDeductions1;

                    //readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrLoanDeductions = "";
                    //dtRow[22] = "-";
                    //DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    //dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    //DSotHours.Dispose();
                    //while (readerEmpLoanDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpLoanDeduction.IsDBNull(1))
                    //        StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpLoanDeduction.IsDBNull(0))
                    //        StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                    //    if (StrLoanDeductions.Length > 42)
                    //    {
                    //        StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                    //    }
                    //}
                    //readerEmpLoanDeduction.Close();
                    //dtRow[22] = StrLoanDeductions;

                    dtRow[9] = decWagePay;
                    dtRow[10] = decDebitsBF;
                    dtRow[11] = decMadeUpBalance;
                    dtRow[29] = strNamePlk;
                    dt.Rows.Add(dtRow);
                    //--------------



                    //--------

                }
                reader.Close();
                readerDays.Close();
            }
            readerEmployee.Close();
            return dt;
        }

        //Agalawatte Payslip
        //pre Printed BPL payslip
        public DataTable getSalarySlipsPrePrintedBPLOLAX_APL_Tea(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat, Boolean boolCWPayslip, String strEmp)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");
            dt.Columns.Add("PRIPSS");
            dt.Columns.Add("ItemNameSinhala");
            dt.Columns.Add("NamePlk");
            dt.Columns.Add("PRIPSS_summary");//30

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            SqlDataReader readerNamePlk;
            SqlDataReader readerDailyBasic;
            DataSet DSDailyBasic = new DataSet();
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";
            Decimal decWagePay = 0;
            Decimal decDebitsBF = 0;
            Decimal decMadeUpBalance = 0;
            String tempDivision = "";
            String tempNIC = "";
            String OTHours = "";
            String stritem = "";
            String strNamePlk = "";
            String strempno = "";
            dtRow = dt.NewRow();
            readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.LastName+' '+dbo.EmployeeMaster.Initials, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays, 'PRI:'+convert(varchar(50),dbo.EmpMonthlyEarnings.PRIAmount)+'-PSS:' + convert(varchar(50),dbo.EmpMonthlyEarnings.PSSAmount) as PRIPSS FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.TotalEarnings > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') AND (dbo.EmployeeMaster.EmpNo='" + strEmp + "')", CommandType.Text);
            if (strEmp == "01867")
                strempno = "01867";
            while (readerEmployee.Read())
            {
                StrLoanDeductions = "";
                StrOtherDeductions = "";
                OTHours = "";
                StrOtherDeductions1 = "";
                StrLoanDeductions = "";
                dtRow[27] = "PRI:0/PSS:0";
                dtRow[29] = "0";

                readerNamePlk = SQLHelper.ExecuteReader("SELECT SUM(CashManDays) AS Expr1, SUM(CashManDays * 500) AS CashNamePay, SUM(OverKgs) AS Okgs, SUM(OverKgs * 25) AS OkgAmounts, SUM(CashKgAmount)  AS CashKgAmt FROM            dbo.DailyGroundTransactions WHERE        (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "') AND (CashPlkOkgYesNo = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "')  ", CommandType.Text);
                dtRow[29] = "NamePlk:0";
                strNamePlk = "";
                while (readerNamePlk.Read())
                {

                    if (!readerNamePlk.IsDBNull(0))
                        strNamePlk += "Names:" + readerNamePlk.GetDecimal(0).ToString() + " ";
                    if (!readerNamePlk.IsDBNull(1))
                        strNamePlk += "NamePay:" + readerNamePlk.GetDecimal(1).ToString("N2") + ", ";
                    if (!readerNamePlk.IsDBNull(2))
                        strNamePlk += "Okg:" + readerNamePlk.GetDecimal(2).ToString() + "\n ";
                    if (!readerNamePlk.IsDBNull(3))
                        strNamePlk += "Okg Amt:" + readerNamePlk.GetDecimal(3) + ", ";
                    if (!readerNamePlk.IsDBNull(4))
                        strNamePlk += "Total:" + readerNamePlk.GetDecimal(4).ToString("N2") + ", ";

                }
                if (!String.IsNullOrEmpty(strNamePlk))
                    dtRow[29] = strNamePlk;
                else
                    dtRow[29] = "NamePlk:0";
                readerNamePlk.Close();


                readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                while (readerDetails.Read())
                {
                    if (!readerDetails.IsDBNull(0))
                    {
                        tempDivision = readerDetails.GetString(0).Trim();
                    }
                    else
                        tempDivision = "N/A";
                    if (!readerDetails.IsDBNull(1))
                    {
                        tempNIC = readerDetails.GetString(1).Trim();
                    }
                    else
                    {
                        tempNIC = "NT";
                    }
                }
                readerDetails.Close();

                readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                StrOtherDeductions = "";
                StrOtherDeductions1 = "";
                StrOtherDeductionsTemp = "";

                StrOtherDeductions = "-";
                while (readerEmpOtherDeduction.Read())
                {
                    //dtRow[19] = "-";
                    if (!readerEmpOtherDeduction.IsDBNull(1))
                        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    if (!readerEmpOtherDeduction.IsDBNull(0))
                        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                }
                if (StrOtherDeductions.Length > 42)
                {
                    StrOtherDeductionsTemp = StrOtherDeductions;
                    StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                    StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                    StrOtherDeductionsTemp = "";
                }
                readerEmpOtherDeduction.Close();
                //dtRow[19] = StrOtherDeductions;
                //dtRow[24] = StrOtherDeductions1;

                readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                StrLoanDeductions = "";
                dtRow[22] = "-";
                DataSet DSotHours1 = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                //dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                OTHours = DSotHours1.Tables[0].Rows[0][0].ToString();
                DSotHours1.Dispose();
                while (readerEmpLoanDeduction.Read())
                {
                    //dtRow[19] = "-";
                    if (!readerEmpLoanDeduction.IsDBNull(1))
                        StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                    if (!readerEmpLoanDeduction.IsDBNull(0))
                        StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                    if (StrLoanDeductions.Length > 42)
                    {
                        StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                    }
                }
                readerEmpLoanDeduction.Close();



                //dtRow[22] = StrLoanDeductions;

                //dtRow[19] = StrOtherDeductions;

                //dtRow[23] = OTHours;
                //dtRow[24] = StrOtherDeductions1;
                //dtRow[22] = StrLoanDeductions;

                decWagePay = 0;
                decDebitsBF = 0;
                decMadeUpBalance = 0;
                readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);

                readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                while (readerEmployeeFinal.Read())
                {

                    if (!readerEmployeeFinal.IsDBNull(0))
                        decWagePay = readerEmployeeFinal.GetDecimal(0);
                    else
                        decWagePay = 0;
                    if (!readerEmployeeFinal.IsDBNull(1))
                        decDebitsBF = readerEmployeeFinal.GetDecimal(1);
                    else
                        decDebitsBF = 0;
                    if (!readerEmployeeFinal.IsDBNull(2))
                        decMadeUpBalance = readerEmployeeFinal.GetDecimal(2);
                    else
                        decMadeUpBalance = 0;
                }

                readerEmployeeFinal.Close();

                //Additions
                reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo, SinhalaItemName FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings')  ORDER BY SeqId", CommandType.Text);
                while (reader.Read())
                {


                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                        if (reader.GetString(0).Trim().Equals("PRI"))
                        {
                            stritem = dtRow[3].ToString();
                        }
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                        dtRow[28] = reader.GetString(3).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }
                    if (!readerEmployee.IsDBNull(5))
                    {
                        dtRow[30] = readerEmployee.GetString(5);
                    }

                    //Days

                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }


                    ////OtherDetails
                    dtRow[16] = tempDivision;
                    dtRow[17] = tempNIC;
                    //readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    //while (readerDetails.Read())
                    //{
                    //    if (!readerDetails.IsDBNull(0))
                    //    {
                    //        dtRow[16] = readerDetails.GetString(0).Trim();
                    //    }
                    //    else
                    //        dtRow[16] = "N/A";
                    //    if (!readerDetails.IsDBNull(1))
                    //    {
                    //        dtRow[17] = readerDetails.GetString(1).Trim();
                    //    }
                    //    else
                    //    {
                    //        dtRow[17] = "NT";
                    //    }
                    //}
                    //readerDetails.Close();


                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos  AS NormalOkgs, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount+PSSAmount, OtherAdditions+PreviousMadeUpCoins AS OtherAdditions ,PreviousMadeUpCoins,PRIAmount,PSSAmount  FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins, PSSAmount", CommandType.Text);
                    DSDailyBasic = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS ManDays, SUM(DailyBasic) AS TotPay, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN DailyBasic ELSE 0 END) AS PLKPay,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN DailyBasic ELSE 0 END) AS TAPPay, SUM( CASE WHEN (WorkCodeID <> 'PLK')  THEN DailyBasic ELSE 0 END ) AS TeaSundry, SUM(CASE WHEN (CropType = 2) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS RubberSundry, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PLKManDays,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ManDays ELSE 0 END) AS TAPManDays, SUM( CASE WHEN (WorkCodeID <> 'PLK')  THEN ManDays ELSE 0 END ) AS TeaSundryManDays, SUM(CASE WHEN (CropType = 2) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN ManDays ELSE 0 END ELSE 0 END) AS RubberSundryManDays, SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgs ELSE 0 END) AS ScrapKgs,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgAmount+CashScrapKgAmount ELSE 0 END) AS ScrapAmount FROM dbo.DailyGroundTransactions WHERE (WorkType = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')", CommandType.Text);



                    while (readerEmployeeEarnings.Read())
                    {

                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            //if (!readerEmployeeEarnings.IsDBNull(1))
                            //    dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            //else
                            //    dtRow[7] = 0;
                            //if (!readerEmployeeEarnings.IsDBNull(0))
                            //    dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            //else
                            //    dtRow[8] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][6].ToString()))
                                dtRow[7] = DSDailyBasic.Tables[0].Rows[0][6].ToString();
                            else
                                dtRow[7] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][2].ToString()))
                                dtRow[8] = DSDailyBasic.Tables[0].Rows[0][2].ToString();
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            //dtRow[7] = 0;
                            //dtRow[8] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][8].ToString()))
                                dtRow[7] = DSDailyBasic.Tables[0].Rows[0][8].ToString();
                            else
                                dtRow[7] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][4].ToString()))
                                dtRow[8] = DSDailyBasic.Tables[0].Rows[0][4].ToString();
                            else
                                dtRow[8] = 0;
                        }
                        //if (reader.GetString(0).Trim() == "Tapping")
                        //{
                        //    //dtRow[7] = 0;
                        //    //dtRow[8] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][7].ToString()))
                        //        dtRow[7] = DSDailyBasic.Tables[0].Rows[0][7].ToString();
                        //    else
                        //        dtRow[7] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][3].ToString()))
                        //        dtRow[8] = DSDailyBasic.Tables[0].Rows[0][3].ToString();
                        //    else
                        //        dtRow[8] = 0;
                        //}
                        //if (reader.GetString(0).Trim() == "Rubber")
                        //{
                        //    //dtRow[7] = 0;
                        //    //dtRow[8] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][9].ToString()))
                        //        dtRow[7] = DSDailyBasic.Tables[0].Rows[0][9].ToString();
                        //    else
                        //        dtRow[7] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][5].ToString()))
                        //        dtRow[8] = DSDailyBasic.Tables[0].Rows[0][5].ToString();
                        //    else
                        //        dtRow[8] = 0;
                        //}

                        //if (reader.GetString(0).Trim() == "Scrap")
                        //{
                        //    //dtRow[7] = 0;
                        //    //dtRow[8] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][10].ToString()))
                        //        dtRow[7] = DSDailyBasic.Tables[0].Rows[0][10].ToString();
                        //    else
                        //        dtRow[7] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][11].ToString()))
                        //        dtRow[8] = DSDailyBasic.Tables[0].Rows[0][11].ToString();
                        //    else
                        //        dtRow[8] = 0;
                        //}

                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();


                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                                if (readerEmployeeEarnings.GetDecimal(8) > 0)
                                {
                                    dtRow[27] = "";
                                    if (!readerEmployeeEarnings.IsDBNull(11))
                                    {
                                        dtRow[27] = "PRI:" + readerEmployeeEarnings.GetDecimal(11).ToString();
                                    }
                                    if (!readerEmployeeEarnings.IsDBNull(12))
                                    {
                                        dtRow[27] = dtRow[27] + "/PSS:" + readerEmployeeEarnings.GetDecimal(12).ToString();
                                    }
                                }
                            }
                            else
                                dtRow[8] = 0;
                        }

                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    dtRow[9] = decWagePay;
                    dtRow[10] = decDebitsBF;
                    dtRow[11] = decMadeUpBalance;
                    dtRow[29] = strNamePlk;

                    //--------

                    dt.Rows.Add(dtRow);

                }
                reader.Close();



                //Deductions
                if (readerEmployee.GetString(0).Trim() == "00782")
                {
                    string sttext;
                    sttext = "00782";
                }
                //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
                //reader = SQLHelper.ExecuteReader(" select T1.SalryItemName,T1.ItemNO, sum(T1.Amount) as amount from (   SELECT     TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber') then 'Dhoby' else dbo.CHKWageItemSequenceOLAX.SalryItemName end as SalryItemName, CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber') then 11 else dbo.CHKWageItemSequenceOLAX.ItemNO end as ItemNO , SUM(dbo.CHKEmpDeductions.Amount)  AS Amount FROM         dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE     (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO, dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) t1 group by T1.SalryItemName,T1.ItemNO", CommandType.Text);
                //reader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT SalryItemName as SalName, ItemNO,isnull( (SELECT     SUM(Amount) AS amount FROM          (SELECT     TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 'Others' ELSE dbo.CHKWageItemSequenceOLAX.SalryItemName END AS SalryItemName,  CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 17 ELSE dbo.CHKWageItemSequenceOLAX.ItemNO END AS ItemNO, SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM          dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE      (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') AND (dbo.CHKWageItemSequenceOLAX.SalryItemName <> 'Stamp Duty') AND (dbo.CHKWageItemSequenceOLAX.SeqId <> 28) GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO,  dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) AS t1 WHERE      (T1.SalryItemName = CHKWageItemSequenceOLAX_1.SalryItemName) GROUP BY SalryItemName, ItemNO),0) AS Expr1,TamilItemName,SinhalaItemName FROM         dbo.CHKWageItemSequenceOLAX AS CHKWageItemSequenceOLAX_1 WHERE     (SalaryItemType = 'Deductions') AND (SalryItemName <> 'Stamp Duty')", CommandType.Text);
                reader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT SalryItemName AS SalName, ItemNO, ISNULL ((SELECT        SUM(Amount) AS amount FROM            (SELECT        TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 'Others' ELSE dbo.CHKWageItemSequenceOLAX.SalryItemName END AS SalryItemName,  CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 18 ELSE dbo.CHKWageItemSequenceOLAX.ItemNO END AS ItemNO, SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM            dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE        (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo =  '" + readerEmployee.GetString(0).Trim() + "') AND  (dbo.CHKEmpDeductions.DivisionId =  '" + DivisionID + "') GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO,  dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) AS t1 WHERE        (SalryItemName = CHKWageItemSequenceOLAX_1.SalryItemName) GROUP BY SalryItemName, ItemNO), 0) AS Expr1, TamilItemName, SinhalaItemName FROM            dbo.CHKWageItemSequenceOLAX AS CHKWageItemSequenceOLAX_1 WHERE        (SalaryItemType = 'Deductions') AND (SalryItemName <> 'Stamp Duty')", CommandType.Text);
                while (reader.Read())
                {

                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(1);
                    dtRow[26] = "NA";
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(3).Trim();
                        dtRow[28] = reader.GetString(4).Trim();
                        dtRow[26] = reader.GetString(3).Trim();
                    }
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(1).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim() + " " + "NA";
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }
                    if (!readerEmployee.IsDBNull(5))
                    {
                        dtRow[30] = readerEmployee.GetString(5);
                    }

                    //Days

                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }


                    ////OtherDetails
                    dtRow[16] = tempDivision;
                    dtRow[17] = tempNIC;
                    //readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    //while (readerDetails.Read())
                    //{
                    //    if (!readerDetails.IsDBNull(0))
                    //    {
                    //        dtRow[16] = readerDetails.GetString(0).Trim();
                    //    }
                    //    else
                    //        dtRow[16] = "N/A";
                    //    if (!readerDetails.IsDBNull(1))
                    //    {
                    //        dtRow[17] = readerDetails.GetString(1).Trim();
                    //    }
                    //    else
                    //    {
                    //        dtRow[17] = "NT";
                    //    }
                    //}
                    //readerDetails.Close();

                    String str = reader.GetString(0).Trim();
                    String st = "";
                    //if (readerEmployee.GetString(0).Trim() == "1450")
                    //{
                    //    if (readerEmployee.GetString(0).Trim().ToUpper().Equals("1450") && (str.Equals("Stamp Duty")))
                    //    {
                    //        st = "here, ";
                    //    }
                    //    str = reader.GetString(0).Trim();
                    //    if (str == "Dhoby")
                    //    {
                    //        str = "Dhoby";
                    //    }
                    //}

                    //new 2016-01-06

                    dtRow[7] = 0;

                    if (!reader.IsDBNull(2))
                        dtRow[8] = reader.GetDecimal(2);
                    else
                        dtRow[8] = 0;
                    //
                    //if (reader.GetString(0).Trim().Equals("Dhoby"))
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //else
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //while (readerEmployeeDeductions.Read())
                    //{
                    //    dtRow[7] = 0;

                    //    if (!readerEmployeeDeductions.IsDBNull(0))
                    //        dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                    //    else
                    //        dtRow[8] = 0;
                    //}
                    //readerEmployeeDeductions.Close();


                    dtRow[19] = StrOtherDeductions;

                    dtRow[23] = OTHours;
                    dtRow[24] = StrOtherDeductions1;
                    dtRow[22] = StrLoanDeductions;
                    //readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrOtherDeductions = "";
                    //StrOtherDeductions1 = "";
                    //StrOtherDeductionsTemp = "";

                    //dtRow[19] = "-";
                    //while (readerEmpOtherDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(1))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(0))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    //}
                    //if (StrOtherDeductions.Length > 42)
                    //{
                    //    StrOtherDeductionsTemp = StrOtherDeductions;
                    //    StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                    //    StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                    //    StrOtherDeductionsTemp = "";
                    //}
                    //readerEmpOtherDeduction.Close();
                    //dtRow[19] = StrOtherDeductions;
                    //dtRow[24] = StrOtherDeductions1;

                    //readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrLoanDeductions = "";
                    //dtRow[22] = "-";
                    //DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    //dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    //DSotHours.Dispose();
                    //while (readerEmpLoanDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpLoanDeduction.IsDBNull(1))
                    //        StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpLoanDeduction.IsDBNull(0))
                    //        StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                    //    if (StrLoanDeductions.Length > 42)
                    //    {
                    //        StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                    //    }
                    //}
                    //readerEmpLoanDeduction.Close();
                    //dtRow[22] = StrLoanDeductions;

                    dtRow[9] = decWagePay;
                    dtRow[10] = decDebitsBF;
                    dtRow[11] = decMadeUpBalance;
                    dtRow[29] = strNamePlk;
                    dt.Rows.Add(dtRow);
                    //--------------



                    //--------

                }
                reader.Close();
                readerDays.Close();
            }
            readerEmployee.Close();
            return dt;
        }
        //End Agalawatte payslip

        //Agalawatte Multi-Crop
        //Agalawatte Payslip
        //pre Printed BPL payslip
        public DataTable getSalarySlipsPrePrintedBPLOLAX_APL_MultiCrop(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat, Boolean boolCWPayslip, String strEmp)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");
            dt.Columns.Add("PRIPSS");
            dt.Columns.Add("ItemNameSinhala");
            dt.Columns.Add("NamePlk");
            dt.Columns.Add("PRIPSS_summary");//30

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            SqlDataReader readerNamePlk;
            SqlDataReader readerDailyBasic;
            DataSet DSDailyBasic = new DataSet();
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";
            Decimal decWagePay = 0;
            Decimal decDebitsBF = 0;
            Decimal decMadeUpBalance = 0;
            String tempDivision = "";
            String tempNIC = "";
            String OTHours = "";
            String stritem = "";
            String strNamePlk = "";

            dtRow = dt.NewRow();
            readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.LastName+' '+dbo.EmployeeMaster.Initials, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays, 'PRI:'+convert(varchar(50),dbo.EmpMonthlyEarnings.PRIAmount)+'-PSS:' + convert(varchar(50),dbo.EmpMonthlyEarnings.PSSAmount) as PRIPSS FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.TotalEarnings > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') AND (dbo.EmployeeMaster.EmpNo='" + strEmp + "')", CommandType.Text);

            while (readerEmployee.Read())
            {
                StrLoanDeductions = "";
                StrOtherDeductions = "";
                OTHours = "";
                StrOtherDeductions1 = "";
                StrLoanDeductions = "";
                dtRow[27] = "PRI:0/PSS:0";
                dtRow[29] = "0";

                readerNamePlk = SQLHelper.ExecuteReader("SELECT SUM(CashManDays) AS Expr1, SUM(CashManDays * 500) AS CashNamePay, SUM(OverKgs) AS Okgs, SUM(OverKgs * 25) AS OkgAmounts, SUM(CashKgAmount)  AS CashKgAmt FROM            dbo.DailyGroundTransactions WHERE        (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "') AND (CashPlkOkgYesNo = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "')  ", CommandType.Text);
                dtRow[29] = "NamePlk:0";
                strNamePlk = "";
                while (readerNamePlk.Read())
                {

                    if (!readerNamePlk.IsDBNull(0))
                        strNamePlk += "Names:" + readerNamePlk.GetDecimal(0).ToString() + " ";
                    if (!readerNamePlk.IsDBNull(1))
                        strNamePlk += "NamePay:" + readerNamePlk.GetDecimal(1).ToString("N2") + ", ";
                    if (!readerNamePlk.IsDBNull(2))
                        strNamePlk += "Okg:" + readerNamePlk.GetDecimal(2).ToString() + "\n ";
                    if (!readerNamePlk.IsDBNull(3))
                        strNamePlk += "Okg Amt:" + readerNamePlk.GetDecimal(3) + ", ";
                    if (!readerNamePlk.IsDBNull(4))
                        strNamePlk += "Total:" + readerNamePlk.GetDecimal(4).ToString("N2") + ", ";

                }
                if (!String.IsNullOrEmpty(strNamePlk))
                    dtRow[29] = strNamePlk;
                else
                    dtRow[29] = "NamePlk:0";
                readerNamePlk.Close();


                readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                while (readerDetails.Read())
                {
                    if (!readerDetails.IsDBNull(0))
                    {
                        tempDivision = readerDetails.GetString(0).Trim();
                    }
                    else
                        tempDivision = "N/A";
                    if (!readerDetails.IsDBNull(1))
                    {
                        tempNIC = readerDetails.GetString(1).Trim();
                    }
                    else
                    {
                        tempNIC = "NT";
                    }
                }
                readerDetails.Close();

                readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                StrOtherDeductions = "";
                StrOtherDeductions1 = "";
                StrOtherDeductionsTemp = "";

                StrOtherDeductions = "-";
                while (readerEmpOtherDeduction.Read())
                {
                    //dtRow[19] = "-";
                    if (!readerEmpOtherDeduction.IsDBNull(1))
                        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    if (!readerEmpOtherDeduction.IsDBNull(0))
                        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                }
                if (StrOtherDeductions.Length > 42)
                {
                    StrOtherDeductionsTemp = StrOtherDeductions;
                    StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                    StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                    StrOtherDeductionsTemp = "";
                }
                readerEmpOtherDeduction.Close();
                //dtRow[19] = StrOtherDeductions;
                //dtRow[24] = StrOtherDeductions1;

                readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                StrLoanDeductions = "";
                dtRow[22] = "-";
                DataSet DSotHours1 = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                //dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                OTHours = DSotHours1.Tables[0].Rows[0][0].ToString();
                DSotHours1.Dispose();
                while (readerEmpLoanDeduction.Read())
                {
                    //dtRow[19] = "-";
                    if (!readerEmpLoanDeduction.IsDBNull(1))
                        StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                    if (!readerEmpLoanDeduction.IsDBNull(0))
                        StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                    if (StrLoanDeductions.Length > 42)
                    {
                        StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                    }
                }
                readerEmpLoanDeduction.Close();



                //dtRow[22] = StrLoanDeductions;

                //dtRow[19] = StrOtherDeductions;

                //dtRow[23] = OTHours;
                //dtRow[24] = StrOtherDeductions1;
                //dtRow[22] = StrLoanDeductions;

                decWagePay = 0;
                decDebitsBF = 0;
                decMadeUpBalance = 0;
                readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);

                readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                while (readerEmployeeFinal.Read())
                {

                    if (!readerEmployeeFinal.IsDBNull(0))
                        decWagePay = readerEmployeeFinal.GetDecimal(0);
                    else
                        decWagePay = 0;
                    if (!readerEmployeeFinal.IsDBNull(1))
                        decDebitsBF = readerEmployeeFinal.GetDecimal(1);
                    else
                        decDebitsBF = 0;
                    if (!readerEmployeeFinal.IsDBNull(2))
                        decMadeUpBalance = readerEmployeeFinal.GetDecimal(2);
                    else
                        decMadeUpBalance = 0;
                }

                readerEmployeeFinal.Close();

                //Additions
                reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo, SinhalaItemName FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings')  ORDER BY SeqId", CommandType.Text);
                while (reader.Read())
                {


                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                        if (reader.GetString(0).Trim().Equals("PRI"))
                        {
                            stritem = dtRow[3].ToString();
                        }
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                        dtRow[28] = reader.GetString(3).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }
                    if (!readerEmployee.IsDBNull(5))
                    {
                        dtRow[30] = readerEmployee.GetString(5);
                    }

                    //Days

                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }


                    ////OtherDetails
                    dtRow[16] = tempDivision;
                    dtRow[17] = tempNIC;
                    //readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    //while (readerDetails.Read())
                    //{
                    //    if (!readerDetails.IsDBNull(0))
                    //    {
                    //        dtRow[16] = readerDetails.GetString(0).Trim();
                    //    }
                    //    else
                    //        dtRow[16] = "N/A";
                    //    if (!readerDetails.IsDBNull(1))
                    //    {
                    //        dtRow[17] = readerDetails.GetString(1).Trim();
                    //    }
                    //    else
                    //    {
                    //        dtRow[17] = "NT";
                    //    }
                    //}
                    //readerDetails.Close();


                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos  AS NormalOkgs, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount+PSSAmount, OtherAdditions+PreviousMadeUpCoins AS OtherAdditions ,PreviousMadeUpCoins,PRIAmount,PSSAmount FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins, PSSAmount", CommandType.Text);
                    //DSDailyBasic = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS ManDays, SUM(DailyBasic) AS TotPay, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN DailyBasic ELSE 0 END) AS PLKPay,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN DailyBasic ELSE 0 END) AS TAPPay, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS TeaSundry, SUM(CASE WHEN (CropType = 2) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS RubberSundry, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PLKManDays,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ManDays ELSE 0 END) AS TAPManDays, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN ManDays ELSE 0 END ELSE 0 END) AS TeaSundryManDays, SUM(CASE WHEN (CropType = 2) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN ManDays ELSE 0 END ELSE 0 END) AS RubberSundryManDays, SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgs ELSE 0 END) AS ScrapKgs,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgAmount+CashScrapKgAmount ELSE 0 END) AS ScrapAmount FROM dbo.DailyGroundTransactions WHERE (WorkType = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')", CommandType.Text);
                    //DSDailyBasic = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS ManDays, SUM(DailyBasic) AS TotPay, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN DailyBasic ELSE 0 END) AS PLKPay,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN DailyBasic ELSE 0 END) AS TAPPay, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS TeaSundry, SUM(CASE WHEN (CropType <>1) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS RubberSundry, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PLKManDays,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ManDays ELSE 0 END) AS TAPManDays, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN ManDays ELSE 0 END ELSE 0 END) AS TeaSundryManDays, SUM(CASE WHEN (CropType <>1) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN ManDays ELSE 0 END ELSE 0 END) AS RubberSundryManDays, SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgs ELSE 0 END) AS ScrapKgs,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgAmount+CashScrapKgAmount ELSE 0 END) AS ScrapAmount FROM dbo.DailyGroundTransactions WHERE (WorkType = 1) AND (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')", CommandType.Text);
                    //removed worktype filter
                    DSDailyBasic = SQLHelper.FillDataSet("SELECT SUM(ManDays) AS ManDays, SUM(DailyBasic) AS TotPay, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN DailyBasic ELSE 0 END) AS PLKPay,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN DailyBasic ELSE 0 END) AS TAPPay, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS TeaSundry, SUM(CASE WHEN (CropType <>1) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN DailyBasic ELSE 0 END ELSE 0 END) AS RubberSundry, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PLKManDays,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ManDays ELSE 0 END) AS TAPManDays, SUM(CASE WHEN (CropType = 1) THEN CASE WHEN (WorkCodeID <> 'PLK')  THEN ManDays ELSE 0 END ELSE 0 END) AS TeaSundryManDays, SUM(CASE WHEN (CropType <>1) THEN CASE WHEN (WorkCodeID <> 'TAP')  THEN ManDays ELSE 0 END ELSE 0 END) AS RubberSundryManDays, SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgs ELSE 0 END) AS ScrapKgs,  SUM(CASE WHEN (WorkCodeID = 'TAP') THEN ScrapKgAmount+CashScrapKgAmount ELSE 0 END) AS ScrapAmount FROM dbo.DailyGroundTransactions WHERE  (DivisionID = '" + DivisionID + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "')", CommandType.Text);



                    while (readerEmployeeEarnings.Read())
                    {

                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            //if (!readerEmployeeEarnings.IsDBNull(1))
                            //    dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            //else
                            //    dtRow[7] = 0;
                            //if (!readerEmployeeEarnings.IsDBNull(0))
                            //    dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            //else
                            //    dtRow[8] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][6].ToString()))
                                dtRow[7] = DSDailyBasic.Tables[0].Rows[0][6].ToString();
                            else
                                dtRow[7] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][2].ToString()))
                                dtRow[8] = DSDailyBasic.Tables[0].Rows[0][2].ToString();
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            //dtRow[7] = 0;
                            //dtRow[8] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][8].ToString()))
                                dtRow[7] = DSDailyBasic.Tables[0].Rows[0][8].ToString();
                            else
                                dtRow[7] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][4].ToString()))
                                dtRow[8] = DSDailyBasic.Tables[0].Rows[0][4].ToString();
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tapping")
                        {
                            //dtRow[7] = 0;
                            //dtRow[8] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][7].ToString()))
                                dtRow[7] = DSDailyBasic.Tables[0].Rows[0][7].ToString();
                            else
                                dtRow[7] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][3].ToString()))
                                dtRow[8] = DSDailyBasic.Tables[0].Rows[0][3].ToString();
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Rubber")
                        {
                            //dtRow[7] = 0;
                            //dtRow[8] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][9].ToString()))
                                dtRow[7] = DSDailyBasic.Tables[0].Rows[0][9].ToString();
                            else
                                dtRow[7] = 0;
                            if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][5].ToString()))
                                dtRow[8] = DSDailyBasic.Tables[0].Rows[0][5].ToString();
                            else
                                dtRow[8] = 0;
                        }

                        //if (reader.GetString(0).Trim() == "Scrap")
                        //{
                        //    //dtRow[7] = 0;
                        //    //dtRow[8] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][10].ToString()))
                        //        dtRow[7] = DSDailyBasic.Tables[0].Rows[0][10].ToString();
                        //    else
                        //        dtRow[7] = 0;
                        //    if (!String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][11].ToString()))
                        //        dtRow[8] = DSDailyBasic.Tables[0].Rows[0][11].ToString();
                        //    else
                        //        dtRow[8] = 0;
                        //}

                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();


                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9) && !String.IsNullOrEmpty(DSDailyBasic.Tables[0].Rows[0][6].ToString()))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9) + Convert.ToDecimal(DSDailyBasic.Tables[0].Rows[0][11].ToString());
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                                if (readerEmployeeEarnings.GetDecimal(8) > 0)
                                {
                                    dtRow[27] = "";
                                    if (!readerEmployeeEarnings.IsDBNull(11))
                                    {
                                        dtRow[27] = "PRI:" + readerEmployeeEarnings.GetDecimal(11).ToString();
                                    }
                                    if (!readerEmployeeEarnings.IsDBNull(12))
                                    {
                                        dtRow[27] = dtRow[27] + "/PSS:" + readerEmployeeEarnings.GetDecimal(12).ToString();
                                    }
                                }
                            }
                            else
                                dtRow[8] = 0;
                        }

                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    dtRow[9] = decWagePay;
                    dtRow[10] = decDebitsBF;
                    dtRow[11] = decMadeUpBalance;
                    dtRow[29] = strNamePlk;

                    //--------

                    dt.Rows.Add(dtRow);

                }
                reader.Close();



                //Deductions
                if (readerEmployee.GetString(0).Trim() == "00782")
                {
                    string sttext;
                    sttext = "00782";
                }
                //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
                //reader = SQLHelper.ExecuteReader(" select T1.SalryItemName,T1.ItemNO, sum(T1.Amount) as amount from (   SELECT     TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber') then 'Dhoby' else dbo.CHKWageItemSequenceOLAX.SalryItemName end as SalryItemName, CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Barber') then 11 else dbo.CHKWageItemSequenceOLAX.ItemNO end as ItemNO , SUM(dbo.CHKEmpDeductions.Amount)  AS Amount FROM         dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE     (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO, dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) t1 group by T1.SalryItemName,T1.ItemNO", CommandType.Text);
                //reader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT SalryItemName as SalName, ItemNO,isnull( (SELECT     SUM(Amount) AS amount FROM          (SELECT     TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 'Others' ELSE dbo.CHKWageItemSequenceOLAX.SalryItemName END AS SalryItemName,  CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 17 ELSE dbo.CHKWageItemSequenceOLAX.ItemNO END AS ItemNO, SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM          dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE      (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') AND (dbo.CHKWageItemSequenceOLAX.SalryItemName <> 'Stamp Duty') AND (dbo.CHKWageItemSequenceOLAX.SeqId <> 28) GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO,  dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) AS t1 WHERE      (T1.SalryItemName = CHKWageItemSequenceOLAX_1.SalryItemName) GROUP BY SalryItemName, ItemNO),0) AS Expr1,TamilItemName,SinhalaItemName FROM         dbo.CHKWageItemSequenceOLAX AS CHKWageItemSequenceOLAX_1 WHERE     (SalaryItemType = 'Deductions') AND (SalryItemName <> 'Stamp Duty')", CommandType.Text);
                reader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT SalryItemName AS SalName, ItemNO, ISNULL ((SELECT        SUM(Amount) AS amount FROM            (SELECT        TOP (100) PERCENT CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 'Others' ELSE dbo.CHKWageItemSequenceOLAX.SalryItemName END AS SalryItemName,  CASE WHEN (dbo.CHKWageItemSequenceOLAX.SalryItemName = 'Stamp Duty')  THEN 18 ELSE dbo.CHKWageItemSequenceOLAX.ItemNO END AS ItemNO, SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM            dbo.CHKDeductionGroup INNER JOIN dbo.CHKEmpDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKEmpDeductions.DeductGroupId INNER JOIN dbo.CHKWageItemSequenceOLAX ON dbo.CHKDeductionGroup.GroupName = dbo.CHKWageItemSequenceOLAX.SalryItemName WHERE        (dbo.CHKWageItemSequenceOLAX.SalaryItemType = 'Deductions') AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND  (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo =  '" + readerEmployee.GetString(0).Trim() + "') AND  (dbo.CHKEmpDeductions.DivisionId =  '" + DivisionID + "') GROUP BY dbo.CHKWageItemSequenceOLAX.SalryItemName, dbo.CHKWageItemSequenceOLAX.ItemNO,  dbo.CHKWageItemSequenceOLAX.SeqId ORDER BY dbo.CHKWageItemSequenceOLAX.SeqId) AS t1 WHERE        (SalryItemName = CHKWageItemSequenceOLAX_1.SalryItemName) GROUP BY SalryItemName, ItemNO), 0) AS Expr1, TamilItemName, SinhalaItemName FROM            dbo.CHKWageItemSequenceOLAX AS CHKWageItemSequenceOLAX_1 WHERE        (SalaryItemType = 'Deductions') AND (SalryItemName <> 'Stamp Duty')", CommandType.Text);
                while (reader.Read())
                {

                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(1);
                    dtRow[26] = "NA";
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(3).Trim();
                        dtRow[28] = reader.GetString(4).Trim();
                        dtRow[26] = reader.GetString(3).Trim();
                    }
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(1).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim() + " " + "NA";
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }
                    if (!readerEmployee.IsDBNull(5))
                    {
                        dtRow[30] = readerEmployee.GetString(5);
                    }

                    //Days

                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }


                    ////OtherDetails
                    dtRow[16] = tempDivision;
                    dtRow[17] = tempNIC;
                    //readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    //while (readerDetails.Read())
                    //{
                    //    if (!readerDetails.IsDBNull(0))
                    //    {
                    //        dtRow[16] = readerDetails.GetString(0).Trim();
                    //    }
                    //    else
                    //        dtRow[16] = "N/A";
                    //    if (!readerDetails.IsDBNull(1))
                    //    {
                    //        dtRow[17] = readerDetails.GetString(1).Trim();
                    //    }
                    //    else
                    //    {
                    //        dtRow[17] = "NT";
                    //    }
                    //}
                    //readerDetails.Close();

                    String str = reader.GetString(0).Trim();
                    String st = "";
                    //if (readerEmployee.GetString(0).Trim() == "1450")
                    //{
                    //    if (readerEmployee.GetString(0).Trim().ToUpper().Equals("1450") && (str.Equals("Stamp Duty")))
                    //    {
                    //        st = "here, ";
                    //    }
                    //    str = reader.GetString(0).Trim();
                    //    if (str == "Dhoby")
                    //    {
                    //        str = "Dhoby";
                    //    }
                    //}

                    //new 2016-01-06

                    dtRow[7] = 0;

                    if (!reader.IsDBNull(2))
                        dtRow[8] = reader.GetDecimal(2);
                    else
                        dtRow[8] = 0;
                    //
                    //if (reader.GetString(0).Trim().Equals("Dhoby"))
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //else
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //while (readerEmployeeDeductions.Read())
                    //{
                    //    dtRow[7] = 0;

                    //    if (!readerEmployeeDeductions.IsDBNull(0))
                    //        dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                    //    else
                    //        dtRow[8] = 0;
                    //}
                    //readerEmployeeDeductions.Close();


                    dtRow[19] = StrOtherDeductions;

                    dtRow[23] = OTHours;
                    dtRow[24] = StrOtherDeductions1;
                    dtRow[22] = StrLoanDeductions;
                    //readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrOtherDeductions = "";
                    //StrOtherDeductions1 = "";
                    //StrOtherDeductionsTemp = "";

                    //dtRow[19] = "-";
                    //while (readerEmpOtherDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(1))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(0))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    //}
                    //if (StrOtherDeductions.Length > 42)
                    //{
                    //    StrOtherDeductionsTemp = StrOtherDeductions;
                    //    StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                    //    StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                    //    StrOtherDeductionsTemp = "";
                    //}
                    //readerEmpOtherDeduction.Close();
                    //dtRow[19] = StrOtherDeductions;
                    //dtRow[24] = StrOtherDeductions1;

                    //readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrLoanDeductions = "";
                    //dtRow[22] = "-";
                    //DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    //dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    //DSotHours.Dispose();
                    //while (readerEmpLoanDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpLoanDeduction.IsDBNull(1))
                    //        StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpLoanDeduction.IsDBNull(0))
                    //        StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                    //    if (StrLoanDeductions.Length > 42)
                    //    {
                    //        StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                    //    }
                    //}
                    //readerEmpLoanDeduction.Close();
                    //dtRow[22] = StrLoanDeductions;

                    dtRow[9] = decWagePay;
                    dtRow[10] = decDebitsBF;
                    dtRow[11] = decMadeUpBalance;
                    dtRow[29] = strNamePlk;
                    dt.Rows.Add(dtRow);
                    //--------------



                    //--------

                }
                reader.Close();
                readerDays.Close();
            }
            readerEmployee.Close();
            return dt;
        }
        //End Agalawatte payslip
        //Agalawatte Multi-crop End

        /*bpl payslip*/

        //pre printed payslip
        public DataTable getSalarySlipsPrePrintedOLAX(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat,Boolean boolCWPayslip)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");
            dt.Columns.Add("PRIPSS");
            dt.Columns.Add("BlockPlkKilos");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            SqlDataReader readerBlockKilos;
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";

            dtRow = dt.NewRow();
            if (boolCWPayslip)
            {
                readerEmployee = null;
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '"+inCat+"') ", CommandType.Text);
                /*totalearnigs>0 deleted as requested by Bogawana*/
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "')  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "')  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
            }
            else
            {
                readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
            }
            //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
            //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
            //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

            while (readerEmployee.Read())
            {
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings')  ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();

                    dtRow[27] = "PRI:0/PSS:0";
                    dtRow[28] = "0";
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount+PSSAmount, OtherAdditions AS OtherAdditions ,PreviousMadeUpCoins, PRIAmount,PSSAmount  FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins,PSSAmount", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tapping")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Rubber")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();


                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                                readerBlockKilos = SQLHelper.ExecuteReader("SELECT  SUM(WorkQty)  FROM dbo.DailyGroundTransactions WHERE (CashBlockYesNo = 1) AND (WorkCodeID = 'plk') AND (YEAR(DateEntered) = '" + Year + "') AND (MONTH(DateEntered) = '" + Month + "') AND (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionID = '"+DivisionID+"')", CommandType.Text);
                                while (readerBlockKilos.Read())
                                {
                                    if (!readerBlockKilos.IsDBNull(0))
                                    {
                                        dtRow[28] = "Block Plk Kilos:"+readerBlockKilos.GetDecimal(0).ToString();
                                    }
                                }
                                readerBlockKilos.Close();
                            }

                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(8))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                                if (readerEmployeeEarnings.GetDecimal(8) > 0)
                                {
                                    dtRow[27] = "";
                                    if (!readerEmployeeEarnings.IsDBNull(11))
                                    {
                                        dtRow[27] = "PRI:" + readerEmployeeEarnings.GetDecimal(11).ToString();
                                    }
                                    if (!readerEmployeeEarnings.IsDBNull(12))
                                    {
                                        dtRow[27] = dtRow[27] + "/PSS:" + readerEmployeeEarnings.GetDecimal(12).ToString();
                                    }
                                }


                            }
                            else
                                dtRow[8] = 0;

                            //if (!readerEmployeeEarnings.IsDBNull(8))
                            //    dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            //else
                            //    dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(10))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                            else
                                dtRow[8] = 0;
                        }
                    }
                    readerEmployeeEarnings.Close();
                    ////--------------
                    //readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    //while (readerEmployeeFinal.Read())
                    //{

                    //    if (!readerEmployeeFinal.IsDBNull(0))
                    //        dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                    //    else
                    //        dtRow[9] = 0;
                    //    if (!readerEmployeeFinal.IsDBNull(1))
                    //        dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                    //    else
                    //        dtRow[10] = 0;
                    //    if (!readerEmployeeFinal.IsDBNull(2))
                    //        dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                    //    else
                    //        dtRow[11] = 0;
                    //}
                    //readerEmployeeFinal.Close();
                    ////--------

                    //dt.Rows.Add(dtRow);
               
            }
            reader.Close();
            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim() + " " + reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();
                    String str = reader.GetString(0).Trim();

                    if (readerEmployee.GetString(0).Trim() == "0461")
                    {
                        if (str == "Dhoby")
                        {
                            str = "Dhoby";
                        }
                    }
                    if (reader.GetString(0).Trim().Equals("Dhoby"))
                    {
                        readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    }
                    else
                    {
                        readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    }
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    StrOtherDeductions1 = "";
                    StrOtherDeductionsTemp = "";

                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    }
                    if (StrOtherDeductions.Length > 42)
                    {
                        StrOtherDeductionsTemp = StrOtherDeductions;
                        StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                        StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                        StrOtherDeductionsTemp = "";
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    dtRow[24] = StrOtherDeductions1;

                    readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrLoanDeductions = "";
                    dtRow[22] = "-";
                    DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    DSotHours.Dispose();
                    while (readerEmpLoanDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpLoanDeduction.IsDBNull(1))
                            StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpLoanDeduction.IsDBNull(0))
                            StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                        if (StrLoanDeductions.Length > 42)
                        {
                            StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                        }
                    }
                    readerEmpLoanDeduction.Close();
                    dtRow[22] = StrLoanDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                
            }
            reader.Close();
        }
        readerEmployee.Close();

            
            return dt;
        }
        

        //pre printed payslip Cash Work
        public DataTable getSalarySlipsPrePrintedCW(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName,Int32 inCat)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            //SqlDataReader readerEmpOtherDeduction;
            //String StrOtherDeductions = "";

            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings') AND (SalryItemName <> 'Previous Made Up Coins') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (CashWorkPayslipAvailable())
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions + PreviousMadeUpCoins AS OtherAdditions FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();

                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0)", CommandType.Text);
                readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                        
                    }
                    readerDetails.Close();

                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    //readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrOtherDeductions = "";
                    //dtRow[19] = "-";
                    //while (readerEmpOtherDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(1))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(0))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";
                    //    if (StrOtherDeductions.Length > 42)
                    //    {
                    //        StrOtherDeductions = StrOtherDeductions.Substring(0, 40) + "\r\n" + StrOtherDeductions.Substring(41, StrOtherDeductions.Length - 41);
                    //    }
                    //}
                    //readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    StrOtherDeductions1 = "";
                    StrOtherDeductionsTemp = "";
                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";
                        
                    }
                    if (StrOtherDeductions.Length > 42)
                    {
                        StrOtherDeductionsTemp = StrOtherDeductions;
                        StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                        StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                        StrOtherDeductionsTemp = "";
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    dtRow[24] = StrOtherDeductions1;
                    readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrLoanDeductions = "";
                    dtRow[22] = "-";
                    DataSet DSotHours=SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '"+Year+"') AND (MONTH(OtDate) = '"+Month+"') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')",CommandType.Text);
                    dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    DSotHours.Dispose();
                    while (readerEmpLoanDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpLoanDeduction.IsDBNull(1))
                            StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpLoanDeduction.IsDBNull(0))
                            StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                        if (StrLoanDeductions.Length > 42)
                        {
                            StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                        }
                    }
                    readerEmpLoanDeduction.Close();
                    dtRow[22] = StrLoanDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        //pre printed OLAX payslip Cash Work
        public DataTable getSalarySlipsPrePrintedCWOLAX(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat, Boolean boolCWPayslip)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (boolCWPayslip)
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions  AS OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tapping")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Rubber")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();

                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(10))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                            else
                                dtRow[8] = 0;
                        }
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0)", CommandType.Text);
                if (boolCWPayslip)
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = null;
                }
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }

                    }
                    readerDetails.Close();

                    //readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    if (reader.GetString(0).Trim().Equals("Dhoby"))
                    {
                        readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    }
                    else
                    {
                        readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    }

                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    //readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrOtherDeductions = "";
                    //dtRow[19] = "-";
                    //while (readerEmpOtherDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(1))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(0))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";
                    //    if (StrOtherDeductions.Length > 42)
                    //    {
                    //        StrOtherDeductions = StrOtherDeductions.Substring(0, 40) + "\r\n" + StrOtherDeductions.Substring(41, StrOtherDeductions.Length - 41);
                    //    }
                    //}
                    //readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    StrOtherDeductions1 = "";
                    StrOtherDeductionsTemp = "";
                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    }
                    if (StrOtherDeductions.Length > 42)
                    {
                        StrOtherDeductionsTemp = StrOtherDeductions;
                        StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                        StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                        StrOtherDeductionsTemp = "";
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    dtRow[24] = StrOtherDeductions1;
                    readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrLoanDeductions = "";
                    dtRow[22] = "-";
                    DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    DSotHours.Dispose();
                    while (readerEmpLoanDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpLoanDeduction.IsDBNull(1))
                            StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpLoanDeduction.IsDBNull(0))
                            StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                        if (StrLoanDeductions.Length > 42)
                        {
                            StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                        }
                    }
                    readerEmpLoanDeduction.Close();
                    dtRow[22] = StrLoanDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        public Boolean CashWorkPayslipAvailable()
        {
            Boolean boolCWPaySlip = false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CashWorkPaySlip')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).Equals("Available"))
                    {
                        boolCWPaySlip = true;
                    }
                }
            }
            return boolCWPaySlip;
        }

        public String GetPaySlipType()
        {
            String strPaySlipType = "OTHER";
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'PAYSLIP')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    strPaySlipType = reader.GetString(0).ToString();
                }
            }
            return strPaySlipType; 
        }

        public DataSet getCoinAnalysis(Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("DivisionName");
            dt.Columns.Add("Amount");
            dt.Columns.Add(new DataColumn("5000"));
            dt.Columns.Add(new DataColumn("2000"));
            dt.Columns.Add(new DataColumn("1000"));
            dt.Columns.Add(new DataColumn("500"));
            dt.Columns.Add(new DataColumn("100"));
            dt.Columns.Add(new DataColumn("50"));
            dt.Columns.Add(new DataColumn("20"));
            dt.Columns.Add(new DataColumn("10"));//10
            dt.Columns.Add(new DataColumn("Balance"));//11
            dt.Columns.Add(new DataColumn("EmployeeName"));//12
            dt.Columns.Add(new DataColumn("EmployeeNo"));
            dt.Columns.Add(new DataColumn("NetPay"));//14
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyFinalWeges.WagePay) AS WagePay, dbo.EmployeeMaster.EMPName,dbo.EmpMonthlyFinalWeges.EmpNo,SUM(dbo.EmpMonthlyFinalWeges.NetWages) as Wages FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyFinalWeges.WageYear = '" + intYear + "') AND (dbo.EmpMonthlyFinalWeges.WageMonth = '" + intMonth + "') GROUP BY dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EMPName,dbo.EmpMonthlyFinalWeges.EmpNo", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (dataReader.GetString(4).Equals("3055"))
                {
                    string st = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(5);
                }
                //if (!dataReader.IsDBNull(5))
                //{
                //    dtrow[5] = dataReader.GetDecimal(5);
                //}
                Decimal TotalValue = dataReader.GetDecimal(2);
                Int32 my5000 = Convert.ToInt32(System.Math.Floor(TotalValue / 5000));

                dtrow[3] = my5000;
                Decimal Balance = TotalValue - my5000 * 5000;

                //Decimal TotalValue = dataReader.GetDecimal(2);
                Int32 my2000 = Convert.ToInt32(System.Math.Floor(Balance / 2000));

                dtrow[4] = my2000;

                Balance = Balance - my2000 * 2000;

                Int32 my1000 = Convert.ToInt32(System.Math.Floor(Balance / 1000));

                dtrow[5] = my1000;

                Balance = Balance - my1000 * 1000;

                Int32 my500 = Convert.ToInt32(System.Math.Floor(Balance / 500));
                dtrow[6] = my500;

                Balance = Balance - (my500 * 500);

                Int32 my100 = Convert.ToInt32(System.Math.Floor(Balance / 100));
                dtrow[7] = my100;

                Balance = Balance - (my100 * 100);

                Int32 my50 = Convert.ToInt32(System.Math.Floor(Balance / 50));
                dtrow[8] = my50;

                Balance = Balance - (my50 * 50);

                Int32 my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                dtrow[9] = my20;

                Balance = Balance - (my20 * 20);

                Int32 my10 = Convert.ToInt32(System.Math.Floor(Balance / 10));
                dtrow[10] = my10;

                Balance = Balance - (my10 * 10);
                dtrow[11] = Balance;

                if (!dataReader.IsDBNull(3))
                {
                    dtrow[12] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[13] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[14] = dataReader.GetDecimal(5);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            DataSet dataSetReport = new DataSet();
            dt.TableName = "CoinAnalysis";
            dataSetReport.Tables.Add(dt);

            //summery
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("DivisionCode");
            dt1.Columns.Add("DivisionName");
            dt1.Columns.Add("Amount");
            dt1.Columns.Add(new DataColumn("5000"));
            dt1.Columns.Add(new DataColumn("2000"));
            dt1.Columns.Add(new DataColumn("1000"));
            dt1.Columns.Add(new DataColumn("500"));
            dt1.Columns.Add(new DataColumn("100"));
            dt1.Columns.Add(new DataColumn("50"));
            dt1.Columns.Add(new DataColumn("20"));
            dt1.Columns.Add(new DataColumn("10"));
            dt1.Columns.Add(new DataColumn("Balance"));
            DataRow dtrow1;
            SqlDataReader dataReader1;
            dtrow1 = dt1.NewRow();
            dataReader1 = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyFinalWeges.NetWages) AS NetWages FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyFinalWeges.WageYear = '" + intYear + "') AND (dbo.EmpMonthlyFinalWeges.WageMonth = '" + intMonth + "') GROUP BY dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName,dbo.EmpMonthlyFinalWeges.EmpNo, dbo.EmployeeMaster.EMPName", CommandType.Text);

            while (dataReader1.Read())
            {
                dtrow1 = dt1.NewRow();

                if (!dataReader1.IsDBNull(0))
                {
                    dtrow1[0] = dataReader1.GetString(0);
                }
                if (!dataReader1.IsDBNull(1))
                {
                    dtrow1[1] = dataReader1.GetString(1);
                }
                if (!dataReader1.IsDBNull(2))
                {
                    dtrow1[2] = dataReader1.GetDecimal(2);
                }

                Decimal TotalValue = dataReader1.GetDecimal(2);
                Int32 my5000 = Convert.ToInt32(System.Math.Floor(TotalValue / 5000));

                dtrow1[3] = my5000;

                Decimal Balance = TotalValue - my5000 * 5000;

                //Decimal TotalValue = dataReader1.GetDecimal(2);
                Int32 my2000 = Convert.ToInt32(System.Math.Floor(Balance / 2000));

                dtrow1[4] = my2000;

                Balance = Balance - my2000 * 2000;

                Int32 my1000 = Convert.ToInt32(System.Math.Floor(Balance / 1000));

                dtrow1[5] = my1000;

                Balance = Balance - my1000 * 1000;

                Int32 my500 = Convert.ToInt32(System.Math.Floor(Balance / 500));
                dtrow1[6] = my500;

                Balance = Balance - (my500 * 500);

                Int32 my100 = Convert.ToInt32(System.Math.Floor(Balance / 100));
                dtrow1[7] = my100;

                Balance = Balance - (my100 * 100);

                Int32 my50 = Convert.ToInt32(System.Math.Floor(Balance / 50));
                dtrow1[8] = my50;

                Balance = Balance - (my50 * 50);

                Int32 my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                dtrow1[9] = my20;

                Balance = Balance - (my20 * 20);

                Int32 my10 = Convert.ToInt32(System.Math.Floor(Balance / 10));
                dtrow1[10] = my10;

                Balance = Balance - (my10 * 10);
                dtrow1[11] = Balance;

                dt1.Rows.Add(dtrow1);
            }
            dataReader1.Close();
            dt1.TableName = "CoinAnalysisSummery";
            dataSetReport.Tables.Add(dt1);
            //-----------

            return dataSetReport;
        }

        //cash advance coin analysis
        public DataSet getCoinAnalysis(Int32 intYear, Int32 intMonth,int intDeductCode,String strDivision)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("DivisionName");
            dt.Columns.Add("Amount");
            dt.Columns.Add(new DataColumn("5000"));
            dt.Columns.Add(new DataColumn("2000"));
            dt.Columns.Add(new DataColumn("1000"));
            dt.Columns.Add(new DataColumn("500"));
            dt.Columns.Add(new DataColumn("100"));
            dt.Columns.Add(new DataColumn("50"));
            dt.Columns.Add(new DataColumn("20"));
            dt.Columns.Add(new DataColumn("10"));//10
            dt.Columns.Add(new DataColumn("Balance"));//11
            dt.Columns.Add(new DataColumn("EmployeeName"));//12
            dt.Columns.Add(new DataColumn("EmployeeNo"));
            dt.Columns.Add(new DataColumn("NetPay"));//14
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            DataSet ds1 = GetMonthlyAdvanceReport(intYear, intMonth, strDivision, intDeductCode);        
            //dataReader = GetMonthlyAdvanceReport(intYear, intMonth, strDivision, intDeductCode);           
            //dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyFinalWeges.WagePay) AS WagePay, dbo.EmployeeMaster.EMPName,dbo.EmpMonthlyFinalWeges.EmpNo,SUM(dbo.EmpMonthlyFinalWeges.NetWages) as Wages FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyFinalWeges.WageYear = '" + intYear + "') AND (dbo.EmpMonthlyFinalWeges.WageMonth = '" + intMonth + "') GROUP BY dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EMPName,dbo.EmpMonthlyFinalWeges.EmpNo", CommandType.Text);

            foreach(DataRow dr1 in ds1.Tables[0].Rows)
            {
                dtrow = dt.NewRow();

                    dtrow[0] = dr1[0].ToString();
                
               
                    dtrow[1] = dr1[0].ToString();
               
                
                    dtrow[2] = Convert.ToDecimal(dr1[5].ToString());
                
                //if (!dataReader.IsDBNull(5))
                //{
                //    dtrow[5] = dataReader.GetDecimal(5);
                //}
                Decimal TotalValue = Convert.ToDecimal(dr1[5].ToString());
                Int32 my5000 = Convert.ToInt32(System.Math.Floor(TotalValue / 5000));

                dtrow[3] = my5000;
                Decimal Balance = TotalValue - my5000 * 5000;

                //Decimal TotalValue = dataReader.GetDecimal(2);
                Int32 my2000 = Convert.ToInt32(System.Math.Floor(Balance / 2000));

                dtrow[4] = my2000;

                Balance = Balance - my2000 * 2000;

                Int32 my1000 = Convert.ToInt32(System.Math.Floor(Balance / 1000));

                dtrow[5] = my1000;

                Balance = Balance - my1000 * 1000;

                Int32 my500 = Convert.ToInt32(System.Math.Floor(Balance / 500));
                dtrow[6] = my500;

                Balance = Balance - (my500 * 500);

                Int32 my100 = Convert.ToInt32(System.Math.Floor(Balance / 100));
                dtrow[7] = my100;

                Balance = Balance - (my100 * 100);

                Int32 my50 = Convert.ToInt32(System.Math.Floor(Balance / 50));
                dtrow[8] = my50;

                Balance = Balance - (my50 * 50);

                Int32 my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                dtrow[9] = my20;

                Balance = Balance - (my20 * 20);

                Int32 my10 = Convert.ToInt32(System.Math.Floor(Balance / 10));
                dtrow[10] = my10;

                Balance = Balance - (my10 * 10);
                dtrow[11] = Balance;
                
                    dtrow[12] = dr1[4].ToString();                
                
                    dtrow[13] = dr1[3].ToString();                
               
                    dtrow[14] = Convert.ToDecimal(dr1[5].ToString());                

                dt.Rows.Add(dtrow);
            }

            DataSet dataSetReport = new DataSet();
            dt.TableName = "CoinAnalysis";
            dataSetReport.Tables.Add(dt);

            //summery
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("DivisionCode");
            dt1.Columns.Add("DivisionName");
            dt1.Columns.Add("Amount");
            dt1.Columns.Add(new DataColumn("5000"));
            dt1.Columns.Add(new DataColumn("2000"));
            dt1.Columns.Add(new DataColumn("1000"));
            dt1.Columns.Add(new DataColumn("500"));
            dt1.Columns.Add(new DataColumn("100"));
            dt1.Columns.Add(new DataColumn("50"));
            dt1.Columns.Add(new DataColumn("20"));
            dt1.Columns.Add(new DataColumn("10"));
            dt1.Columns.Add(new DataColumn("Balance"));
            DataRow dtrow1;
            SqlDataReader dataReader1;
            dtrow1 = dt1.NewRow();
            DataSet ds2 = GetDivisionWiseMonthlyAdvanceReport(intYear, intMonth, strDivision, intDeductCode);
            //dataReader1 = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyFinalWeges.NetWages) AS NetWages FROM dbo.EmpMonthlyFinalWeges INNER JOIN dbo.EstateDivision ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyFinalWeges.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.EmpMonthlyFinalWeges.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyFinalWeges.WageYear = '" + intYear + "') AND (dbo.EmpMonthlyFinalWeges.WageMonth = '" + intMonth + "') GROUP BY dbo.EmpMonthlyFinalWeges.DivisionId, dbo.EstateDivision.DivisionName,dbo.EmpMonthlyFinalWeges.EmpNo, dbo.EmployeeMaster.EMPName", CommandType.Text);

            foreach(DataRow dr2 in ds2.Tables[0].Rows)
            {
                dtrow1 = dt1.NewRow();

               
                    dtrow1[0] = dr2[0].ToString();


                    dtrow1[1] = dr2[1].ToString();
               
               
                    dtrow1[2] = Convert.ToDecimal(dr2[2].ToString());
                

                Decimal TotalValue = Convert.ToDecimal(dr2[2].ToString());
                Int32 my5000 = Convert.ToInt32(System.Math.Floor(TotalValue / 5000));

                dtrow1[3] = my5000;

                Decimal Balance = TotalValue - my5000 * 5000;

                //Decimal TotalValue = dataReader1.GetDecimal(2);
                Int32 my2000 = Convert.ToInt32(System.Math.Floor(Balance / 2000));

                dtrow1[4] = my2000;

                Balance = Balance - my2000 * 2000;

                Int32 my1000 = Convert.ToInt32(System.Math.Floor(Balance / 1000));

                dtrow1[5] = my1000;

                Balance = Balance - my1000 * 1000;

                Int32 my500 = Convert.ToInt32(System.Math.Floor(Balance / 500));
                dtrow1[6] = my500;

                Balance = Balance - (my500 * 500);

                Int32 my100 = Convert.ToInt32(System.Math.Floor(Balance / 100));
                dtrow1[7] = my100;

                Balance = Balance - (my100 * 100);

                Int32 my50 = Convert.ToInt32(System.Math.Floor(Balance / 50));
                dtrow1[8] = my50;

                Balance = Balance - (my50 * 50);

                Int32 my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                dtrow1[9] = my20;

                Balance = Balance - (my20 * 20);

                Int32 my10 = Convert.ToInt32(System.Math.Floor(Balance / 10));
                dtrow1[10] = my10;

                Balance = Balance - (my10 * 10);
                dtrow1[11] = Balance;

                dt1.Rows.Add(dtrow1);
            }
            dt1.TableName = "CoinAnalysisSummery";
            dataSetReport.Tables.Add(dt1);
            //-----------

            return dataSetReport;
        }

        public DataSet getDivisionWiseCoinAnalysis(Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Amount");
            dt.Columns.Add(new DataColumn("5000"));
            dt.Columns.Add(new DataColumn("2000"));
            dt.Columns.Add(new DataColumn("1000"));
            dt.Columns.Add(new DataColumn("500"));
            dt.Columns.Add(new DataColumn("100"));
            dt.Columns.Add(new DataColumn("50"));
            dt.Columns.Add(new DataColumn("20"));
            dt.Columns.Add(new DataColumn("10"));
            dt.Columns.Add(new DataColumn("Balance"));
            dt.Columns.Add(new DataColumn("NetPay"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     DivisionId, EmpNo, WagePay, NetWages, SalaryAmount - NetWages AS BalanceBF FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + intYear + "') AND (WageMonth = '" + intMonth + "') AND (SalaryAmount > 0)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (dataReader.GetString(1).Equals("3055"))
                {
                    string st = "3055";
                }
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                //if (!dataReader.IsDBNull(5))
                //{
                //    dtrow[5] = dataReader.GetDecimal(5);
                //}
                Decimal TotalValue = dataReader.GetDecimal(3);
                Int32 my5000 = Convert.ToInt32(System.Math.Floor(TotalValue / 5000));

                dtrow[3] = my5000;
                Decimal Balance = TotalValue - my5000 * 5000;

                //Decimal TotalValue = dataReader.GetDecimal(2);
                Int32 my2000 = Convert.ToInt32(System.Math.Floor(Balance / 2000));

                dtrow[4] = my2000;

                Balance = Balance - my2000 * 2000;

                Int32 my1000 = Convert.ToInt32(System.Math.Floor(Balance / 1000));

                dtrow[5] = my1000;

                Balance = Balance - my1000 * 1000;

                Int32 my500 = Convert.ToInt32(System.Math.Floor(Balance / 500));
                dtrow[6] = my500;

                Balance = Balance - (my500 * 500);

                Int32 my100 = Convert.ToInt32(System.Math.Floor(Balance / 100));
                dtrow[7] = my100;

                Balance = Balance - (my100 * 100);

                Int32 my50 = Convert.ToInt32(System.Math.Floor(Balance / 50));
                dtrow[8] = my50;

                Balance = Balance - (my50 * 50);

                Int32 my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                dtrow[9] = my20;

                Balance = Balance - (my20 * 20);

                Int32 my10 = Convert.ToInt32(System.Math.Floor(Balance / 10));
                dtrow[10] = my10;

                Balance = Balance - (my10 * 10);
                dtrow[11] = Balance;

                //if (Balance > 0)
                //{
                //    dtrow[8] = 0;
                //    Balance = Balance + my50 * 50 + my20*20;
                //    my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                //    dtrow[9] = my20;
                //    Balance = Balance - (my20 * 20);
                //    dtrow[10] = dataReader.GetDecimal(4);
                //}

                if (!dataReader.IsDBNull(3))
                {
                    dtrow[12] = dataReader.GetDecimal(3);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            DataSet dataSetReport = new DataSet();
            dt.TableName = "CoinAnalysis";
            dataSetReport.Tables.Add(dt);

            return dataSetReport;
        }

        public DataSet getDivisionWiseCoinAnalysisWithBalance(Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Amount");
            dt.Columns.Add(new DataColumn("5000"));
            dt.Columns.Add(new DataColumn("2000"));
            dt.Columns.Add(new DataColumn("1000"));
            dt.Columns.Add(new DataColumn("500"));
            dt.Columns.Add(new DataColumn("100"));
            dt.Columns.Add(new DataColumn("50"));
            dt.Columns.Add(new DataColumn("20"));
            dt.Columns.Add(new DataColumn("Balance"));
            dt.Columns.Add(new DataColumn("NetPay"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     DivisionId, EmpNo, SalaryAmount, NetWages, SalaryAmount - NetWages AS BalanceBF FROM dbo.EmpMonthlyFinalWeges WHERE (WageYear = '" + intYear + "') AND (WageMonth = '" + intMonth + "') AND (SalaryAmount > 0)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                //if (!dataReader.IsDBNull(5))
                //{
                //    dtrow[5] = dataReader.GetDecimal(5);
                //}
                Decimal TotalValue = dataReader.GetDecimal(3);
                Int32 my5000 = Convert.ToInt32(System.Math.Floor(TotalValue / 5000));

                dtrow[3] = my5000;
                Decimal Balance = TotalValue - my5000 * 5000;

                //Decimal TotalValue = dataReader.GetDecimal(2);
                Int32 my2000 = Convert.ToInt32(System.Math.Floor(Balance / 2000));

                dtrow[4] = my2000;

                Balance = Balance - my2000 * 2000;

                Int32 my1000 = Convert.ToInt32(System.Math.Floor(Balance / 1000));

                dtrow[5] = my1000;

                Balance = Balance - my1000 * 1000;

                Int32 my500 = Convert.ToInt32(System.Math.Floor(Balance / 500));
                dtrow[6] = my500;

                Balance = Balance - (my500 * 500);

                Int32 my100 = Convert.ToInt32(System.Math.Floor(Balance / 100));
                dtrow[7] = my100;

                Balance = Balance - (my100 * 100);

                Int32 my50 = Convert.ToInt32(System.Math.Floor(Balance / 50));
                dtrow[8] = my50;

                Balance = Balance - (my50 * 50);

                Int32 my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                dtrow[9] = my20;

                Balance = Balance - (my20 * 20);

                //Int32 my10 = Convert.ToInt32(System.Math.Floor(Balance / 10));
                //dtrow[10] = my10;

                //Balance = Balance - (my10 * 10);
                dtrow[10] = Balance;

                //if (Balance > 0)
                //{
                //    dtrow[8] = 0;
                //    Balance = Balance + my50 * 50 + my20 * 20;
                //    my20 = Convert.ToInt32(System.Math.Floor(Balance / 20));
                //    dtrow[9] = my20;
                //    Balance = Balance - (my20 * 20);
                //    dtrow[10] = dataReader.GetDecimal(4);
                //}

                if (!dataReader.IsDBNull(3))
                {
                    dtrow[11] = dataReader.GetDecimal(3);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            DataSet dataSetReport = new DataSet();
            dt.TableName = "CoinAnalysis";
            dataSetReport.Tables.Add(dt);

            return dataSetReport;
        }
        public DataSet getCheckRollReconcilation(Int32 Year, Int32 Month, String strDivID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("ShortCode");
            dt.Columns.Add("AccountCode");
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("Amount");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerOtherAdditions;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            dtRow = dt.NewRow();

            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,ShortCode,AccountCode FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings')ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                dtRow[0] = "1. Total Earnings";

                if (!reader.IsDBNull(0))
                {
                    dtRow[1] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[2] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[3] = reader.GetString(2).Trim();
                }

                readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT SUM(dbo.EmpMonthlyEarnings.PluckingNamePay + dbo.EmpMonthlyEarnings.SundryNamePay) AS NamePay, SUM(dbo.EmpMonthlyEarnings.PluckingKilos) AS PluckingKilos, SUM(dbo.EmpMonthlyEarnings.ExtraRates) AS ExtraRates, SUM(dbo.EmpMonthlyEarnings.OverKilos) AS OverKilos, SUM(dbo.EmpMonthlyEarnings.OverKilosPay) AS OverKilosPay, SUM(dbo.EmpMonthlyEarnings.OverTime) AS OverTime, SUM(dbo.EmpMonthlyEarnings.CashPlucking + dbo.EmpMonthlyEarnings.CashSundry) AS CashWork, SUM(dbo.EmpMonthlyEarnings.AttIncentive) AS AttIncentive, SUM(dbo.EmpMonthlyEarnings.PRIAmount) AS PRIAmount, SUM(dbo.EmpMonthlyEarnings.OtherAdditions) AS OtherAdditions, dbo.EmployeeMaster.DivisionID FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.DivisionID = '" + strDivID + "') GROUP BY dbo.EmployeeMaster.DivisionID", CommandType.Text);
                while (readerEmployeeEarnings.Read())
                {
                    if (!readerEmployeeEarnings.IsDBNull(10))
                    {
                        dtRow[4] = readerEmployeeEarnings.GetString(10).Trim();
                    }

                    if (reader.GetString(0).Trim() == "Plucking")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(0))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(0);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Tea")
                    {
                        dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Extra Rates")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(2))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(2);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Over Kilos")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(4))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(4);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Overtime")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(5))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(5);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Cash work")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(6))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(6);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Attendance Incentive")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(7))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(7);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "PRI")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(8))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(8);
                        else
                            dtRow[5] = 0;
                    }
                    if (reader.GetString(0).Trim() == "Other")
                    {
                        if (!readerEmployeeEarnings.IsDBNull(9))
                            dtRow[5] = readerEmployeeEarnings.GetDecimal(9);
                        else
                            dtRow[5] = 0;
                    }
                    dt.Rows.Add(dtRow);
                }
                readerEmployeeEarnings.Close();
            }
            reader.Close();

            //Other Additions
            readerOtherAdditions = SQLHelper.ExecuteReader("SELECT dbo.CHKAddition.AdditionName, dbo.CHKAddition.AdditionShortName, dbo.CHKAddition.AccountCode, dbo.EmployeeMaster.DivisionID, SUM(dbo.CHKEmpAdditions.Amount) AS Amount FROM dbo.CHKEmpAdditions INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpAdditions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKEmpAdditions.CancelYesNo = 0) AND (dbo.CHKEmpAdditions.AdditionYear = '" + Year + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + Month + "') AND (dbo.CHKEmpAdditions.DivisionID = '" + strDivID + "') GROUP BY dbo.CHKAddition.AdditionName, dbo.CHKAddition.AdditionShortName, dbo.CHKAddition.AccountCode, dbo.EmployeeMaster.DivisionID", CommandType.Text);
            while (readerOtherAdditions.Read())
            {
                dtRow = dt.NewRow();
                dtRow[0] = "2. Other Additions";
                if (!readerOtherAdditions.IsDBNull(0))
                {
                    dtRow[1] = readerOtherAdditions.GetString(0).Trim();
                }
                if (!readerOtherAdditions.IsDBNull(1))
                {
                    dtRow[2] = readerOtherAdditions.GetString(1).Trim();
                }
                if (!readerOtherAdditions.IsDBNull(2))
                {
                    dtRow[3] = readerOtherAdditions.GetString(2).Trim();
                }
                if (!readerOtherAdditions.IsDBNull(3))
                {
                    dtRow[4] = readerOtherAdditions.GetString(3).Trim();
                }
                if (!readerOtherAdditions.IsDBNull(4))
                {
                    dtRow[5] = readerOtherAdditions.GetDecimal(4);
                }
                dt.Rows.Add(dtRow);
            }
            readerOtherAdditions.Close();

            //Deductions
            readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKDeduction.AccountCode, SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.EmployeeMaster.DivisionID FROM dbo.CHKEmpDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKEmpDeductions.DeductYear = 2011) AND (dbo.CHKEmpDeductions.DeductMonth = 1) AND (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DivisionID = '" + strDivID + "') GROUP BY dbo.CHKDeduction.DeductionName, dbo.CHKDeduction.ShortName, dbo.CHKDeduction.AccountCode, dbo.EmployeeMaster.DivisionID", CommandType.Text);
            while (readerEmployeeDeductions.Read())
            {
                dtRow = dt.NewRow();
                dtRow[0] = "3. Other Deductions & Payments";

                if (!readerEmployeeDeductions.IsDBNull(0))
                {
                    dtRow[1] = readerEmployeeDeductions.GetString(0).Trim();
                }
                if (!readerEmployeeDeductions.IsDBNull(1))
                {
                    dtRow[2] = readerEmployeeDeductions.GetString(1).Trim();
                }
                if (!readerEmployeeDeductions.IsDBNull(2))
                {
                    dtRow[3] = readerEmployeeDeductions.GetString(2).Trim();
                }
                if (!readerEmployeeDeductions.IsDBNull(4))
                {
                    dtRow[4] = readerEmployeeDeductions.GetString(4).Trim();
                }

                if (!readerEmployeeDeductions.IsDBNull(3))
                    dtRow[5] = readerEmployeeDeductions.GetDecimal(3);
                else
                    dtRow[5] = 0;

                dt.Rows.Add(dtRow);

            }
            readerEmployeeDeductions.Close();

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return ds;
        }
        public DataSet getCheckRollReconcilation2(Int32 Year, Int32 Month, String strDivID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeCatagory");
            dt.Columns.Add("DivisionName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("HolidayPLKManDays");
            dt.Columns.Add("PluckingManDays");
            dt.Columns.Add("SundryManDays");
            dt.Columns.Add("HolidaySundryManDays");
            dt.Columns.Add("PluckingKilos");
            dt.Columns.Add("OverKilos");

            DataRow dtRow;
            SqlDataReader reader;

            reader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeCategory.CategoryName, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.Gender, dbo.EmpMonthlyEarnings.HolidayPLKManDays, dbo.EmpMonthlyEarnings.PluckingManDays, dbo.EmpMonthlyEarnings.SundryManDays, dbo.EmpMonthlyEarnings.HolidaySundryManDays, dbo.EmpMonthlyEarnings.PluckingKilos, dbo.EmpMonthlyEarnings.OverKilos FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.DivisionID = '" + strDivID + "')", CommandType.Text);
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
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetDecimal(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetDecimal(8);
                }
                dt.Rows.Add(dtRow);
            }
            reader.Close();

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return ds;
        }
        public DataSet getDivisionWiseEPFETFSummary(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyEarnings.TotalEarnings) AS TotalEarnings, SUM(dbo.EmpMonthlyEarnings.EPF10) AS EPF10, SUM(dbo.EmpMonthlyEarnings.EPF12) AS EPF12, SUM(dbo.EmpMonthlyEarnings.ETF3) AS ETF3 FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') GROUP BY dbo.EstateDivision.DivisionName", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT EstateDivision.DivisionName, SUM(EmpMonthlyEarnings.EPFPaybleAmount) AS TotalEarnings, SUM(EmpMonthlyEarnings.EPF10) AS EPF10, SUM(EmpMonthlyEarnings.EPF12) AS EPF12, SUM(EmpMonthlyEarnings.ETF3) AS ETF3 FROM EmpMonthlyEarnings INNER JOIN EmployeeMaster ON EmpMonthlyEarnings.EmpNO = EmployeeMaster.EmpNo AND EmpMonthlyEarnings.DivisionId = EmployeeMaster.DivisionID INNER JOIN EstateDivision ON EmployeeMaster.DivisionID = EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') GROUP BY dbo.EstateDivision.DivisionName", CommandType.Text);
            da.Fill(ds, "EPFETFSummary");
            return ds;
        }
        public DataSet getEmployeeWiseEPFETFSummary(DateTime fromDate, DateTime toDate,String div,String empCat)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyEarnings.TotalEarnings) AS TotalEarnings, SUM(dbo.EmpMonthlyEarnings.EPF10) AS EPF10, SUM(dbo.EmpMonthlyEarnings.EPF12) AS EPF12, SUM(dbo.EmpMonthlyEarnings.ETF3) AS ETF3 FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') GROUP BY dbo.EstateDivision.DivisionName", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT  MONTH(dbo.DailyGroundTransactions.DateEntered) as Expr1, dbo.DailyGroundTransactions.DivisionID,  dbo.EmployeeMaster.EPFNo, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, SUM(dbo.DailyGroundTransactions.EPF10)  AS EPF10, SUM(dbo.DailyGroundTransactions.EPF12) AS EPF12, SUM(dbo.DailyGroundTransactions.EPF10)  + SUM(dbo.DailyGroundTransactions.EPF12) AS EPF22, SUM(dbo.DailyGroundTransactions.ETF3) AS ETF3 FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + fromDate + "', 102) AND CONVERT(DATETIME,  '" + toDate + "', 102)) AND (dbo.EmployeeMaster.EmpCategory like '" + empCat + "') GROUP BY  MONTH(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName HAVING      (dbo.DailyGroundTransactions.DivisionID LIKE '" + div + "') ORDER BY MONTH(dbo.DailyGroundTransactions.DateEntered),dbo.EmployeeMaster.EPFNo ", CommandType.Text);
            da.Fill(ds, "EmpWiseEPFETFSummary");
            return ds;
        }
        public DataSet getEmployeeWiseEPFETFSummary(int StartMonth, int EndMonth, String div)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyEarnings.TotalEarnings) AS TotalEarnings, SUM(dbo.EmpMonthlyEarnings.EPF10) AS EPF10, SUM(dbo.EmpMonthlyEarnings.EPF12) AS EPF12, SUM(dbo.EmpMonthlyEarnings.ETF3) AS ETF3 FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') GROUP BY dbo.EstateDivision.DivisionName", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EmpMonthlyEarnings.Month, dbo.EmpMonthlyEarnings.DivisionId, dbo.EmployeeMaster.EPFNo, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EMPName,0 as EPF10,0 as EPF12, SUM(dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10) AS EPF22, SUM(dbo.EmpMonthlyEarnings.ETF3)  AS ETF3 FROM         dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo   GROUP BY dbo.EmpMonthlyEarnings.Month, dbo.EmpMonthlyEarnings.DivisionId, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo,  dbo.EmployeeMaster.EMPName HAVING      (dbo.EmpMonthlyEarnings.Month BETWEEN '"+StartMonth+"' AND '"+EndMonth+"') and (dbo.EmpMonthlyEarnings.DivisionId LIKE '"+div+"') ORDER BY dbo.EmpMonthlyEarnings.Month ", CommandType.Text);
            da.Fill(ds, "EmpWiseEPFETFSummary");
            return ds;
        }
        public DataSet getEmployeeHolidayPayData(DateTime fromDate, DateTime toDate, String div, String empCat)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT DivisionID, EmpNo, SUM(ManDays) AS TotalManDays, SUM(HolidayHalfNames) AS HalfNames, SUM(DailyBasic) AS DailyBasic, SUM(OverKgs) AS OverKgs, SUM(ExtraRates) AS ExtraRates, SUM(DailyBasic) + SUM(OverKgs) + SUM(ExtraRates) AS EPFPaybleAmount, MONTH(DateEntered) AS MONTH FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + fromDate + "', 102) AND CONVERT(DATETIME, '" + toDate + "', 102)) AND (DivisionID LIKE '" + div + "') AND (WorkType = '" + empCat + "') GROUP BY DivisionID, EmpNo, MONTH(DateEntered) ORDER BY DivisionID, EmpNo, MONTH", CommandType.Text);
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  TOP (100) PERCENT DivisionID, EmpNo, SUM(ManDays) AS TotalManDays, SUM(HolidayHalfNames) AS HalfNames, SUM(DailyBasic) AS DailyBasic, SUM(OverKgs) AS OverKgs, SUM(ExtraRates) AS ExtraRates, SUM(DailyBasic) + SUM(OverKgs) + SUM(ExtraRates) AS EPFPaybleAmount FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + fromDate + "', 102) AND CONVERT(DATETIME, '" + toDate + "', 102)) AND  (DivisionID LIKE '" + div + "') AND (WorkType = '" + empCat + "') GROUP BY DivisionID, EmpNo ORDER BY DivisionID, EmpNo ", CommandType.Text);
            da.Fill(ds, "EmpWiseHolidayPaySumm");
            return ds;
        }
       
        //public DataSet getDivisionWiseEPFETFSummary(Int32 intYear, Int32 intMonth)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionName, SUM(dbo.EmpMonthlyEarnings.TotalEarnings) AS TotalEarnings, SUM(dbo.EmpMonthlyEarnings.EPF10) AS EPF10, SUM(dbo.EmpMonthlyEarnings.EPF12) AS EPF12, SUM(dbo.EmpMonthlyEarnings.ETF3) AS ETF3 FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') GROUP BY dbo.EstateDivision.DivisionName", CommandType.Text);
        //    da.Fill(ds, "EPFETFSummary");
        //    return ds;
        //}
        public DataSet getNormSummary(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkQty - dbo.DailyGroundTransactions.ManDays * dbo.DailyGroundTransactions.NormKilos AS OverKilos, dbo.DailyGroundTransactions.DateEntered AS Date FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     DAY(DateEntered) AS Day, DivisionID, COUNT(EmpNo) AS Expr1 FROM dbo.DailyGroundTransactions WHERE (Year(DateEntered)='" + intYear + "') AND (Month(DateEntered)='" + intMonth + "') AND (WorkType = 1)  AND (WorkCodeID = 'PLK') AND  (WorkQty < NormKilos) GROUP BY DAY(DateEntered), DivisionID", CommandType.Text);
            da.Fill(ds, "NormSummary");
            return ds;
        }

        public DataSet getNormSummary(DateTime dtFrom, DateTime dtTo, String strDiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkQty - dbo.DailyGroundTransactions.ManDays * dbo.DailyGroundTransactions.NormKilos AS OverKilos, dbo.DailyGroundTransactions.DateEntered AS Date FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT DAY(DateEntered) AS Day, DivisionID, COUNT(EmpNo) AS Expr1 FROM dbo.DailyGroundTransactions WHERE (WorkType = 1) AND (WorkCodeID = 'PLK') AND (WorkQty < NormKilos) AND (DateEntered BETWEEN '"+dtFrom+"' AND '"+dtTo+"') AND (DivisionID like '"+strDiv+"') GROUP BY DAY(DateEntered), DivisionID", CommandType.Text);
            da.Fill(ds, "NormSummary");
            return ds;
        }

        public DataSet getBelowNormList(DateTime dtFrom, DateTime dtTo, String strDiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkQty - dbo.DailyGroundTransactions.ManDays * dbo.DailyGroundTransactions.NormKilos AS OverKilos, dbo.DailyGroundTransactions.DateEntered AS Date FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo,  dbo.DailyGroundTransactions.WorkQty, dbo.EmployeeMaster.EMPName FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND  (dbo.DailyGroundTransactions.WorkQty < dbo.DailyGroundTransactions.NormKilos) AND (dbo.DailyGroundTransactions.DateEntered BETWEEN '" + dtFrom + "' AND '" + dtTo + "') AND (dbo.DailyGroundTransactions.DivisionID like '"+strDiv+"')", CommandType.Text);
            da.Fill(ds, "BelowNormList");
            return ds;
        }

        public DataSet getMenWomenMandays(Int32 intYear)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkQty - dbo.DailyGroundTransactions.ManDays * dbo.DailyGroundTransactions.NormKilos AS OverKilos, dbo.DailyGroundTransactions.DateEntered AS Date FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  TOP (100) PERCENT  Year, Month, DivisionId, SUM(PluckingManDays + HolidayPLKManDays) AS ManDays, CASE WHEN (Sex = 'M')  THEN 'Men' ELSE 'Women' END AS Gender, 'Plucking' AS Type FROM dbo.EmpMonthlyEarnings GROUP BY Year, Month, Sex, DivisionId HAVING (Year = '" + intYear + "')  UNION SELECT     TOP (100) PERCENT  Year, Month, DivisionId, SUM(SundryManDays + HolidaySundryManDays) AS ManDays, CASE WHEN (Sex = 'M')  THEN 'Men' ELSE 'Women' END AS Gender, 'Sundry' AS Type FROM dbo.EmpMonthlyEarnings AS CheckRollEarnings_1 GROUP BY Year, Month, Sex, DivisionId HAVING (Year = '" + intYear + "')  ORDER BY DivisionId", CommandType.Text);
            da.Fill(ds, "MenWomenManDays");
            return ds;
        }
        public DataSet getPluckingKilosCashWork(DateTime dtFrom, DateTime dtTo, String strDiv,Int32 intWorkType)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkQty - dbo.DailyGroundTransactions.ManDays * dbo.DailyGroundTransactions.NormKilos AS OverKilos, dbo.DailyGroundTransactions.DateEntered AS Date FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.WorkType, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, SUM(dbo.DailyGroundTransactions.WorkQty)  AS Expr1, CASE WHEN (dbo.DailyGroundTransactions.CashBlockYesNo = 1) THEN 'BlockPlk' ELSE (CASE WHEN (dbo.DailyGroundTransactions.IsContract = 1)  THEN 'ContractPlk' ELSE 'Normal_CW' END) END AS PlkType FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND  (dbo.DailyGroundTransactions.WorkCodeID = 'plk') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkType = '"+intWorkType+"') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.WorkType,  dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.CashBlockYesNo, dbo.DailyGroundTransactions.IsContract,  dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName", CommandType.Text);
            da.Fill(ds, "PlkKilosReg");
            return ds;
        }

        public DataSet getPluckingKilos(DateTime dtFrom, DateTime dtTo, String strDiv, Int32 intWorkType)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.NormKilos, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.WorkQty - dbo.DailyGroundTransactions.ManDays * dbo.DailyGroundTransactions.NormKilos AS OverKilos, dbo.DailyGroundTransactions.DateEntered AS Date FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.WorkType, dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName, SUM(dbo.DailyGroundTransactions.WorkQty)  AS Expr1, CASE WHEN (dbo.DailyGroundTransactions.CashBlockYesNo = 1) THEN 'BlockPlk' ELSE (CASE WHEN (dbo.DailyGroundTransactions.IsContract = 1)  THEN 'ContractPlk' ELSE 'Normal' END) END AS PlkType FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND  (dbo.DailyGroundTransactions.WorkCodeID = 'plk') AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkType = '" + intWorkType + "') GROUP BY dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.WorkType,  dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.CashBlockYesNo, dbo.DailyGroundTransactions.IsContract,  dbo.EmployeeCategory.CategoryID, dbo.EmployeeCategory.CategoryName", CommandType.Text);
            da.Fill(ds, "PlkKilosReg");
            return ds;
        }

       


        public DataSet getOutstandingRecoveries()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.CHKDeductionGroup.GroupName, dbo.CHKDeduction.DeductionName, dbo.EmployeeMaster.EMPName, dbo.EstateDivision.DivisionName, dbo.CHKLoan.DivisionCode, dbo.CHKLoan.LoanName, dbo.CHKLoan.LoanDate, dbo.CHKLoan.Principalamount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.RecovredInstallments, dbo.CHKLoan.RecoveredAmount FROM dbo.CHKLoan INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKLoan.EmployeeNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID", CommandType.Text);
            da.Fill(ds, "OutstandingRecoveries");
            return ds;
        }
        public DataSet getOutstandingFixedDeductions()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, dbo.CHKFixedDeductions.DivisionId AS Division, dbo.CHKDeductionGroup.ShortName AS DeductGroup, dbo.CHKDeduction.ShortName AS DeductCode, dbo.CHKFixedDeductions.DeductAmount AS InstallmentAmount,  dbo.CHKFixedDeductions.NoOfMonths AS NoofInstallments, dbo.CHKFixedDeductions.BalanceAmount FROM         dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKFixedDeductions.DeductionId WHERE     (dbo.CHKFixedDeductions.StartMonth > 10) AND (dbo.CHKFixedDeductions.BalanceAmount > 0) ORDER BY dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, Division, DeductGroup, DeductCode", CommandType.Text);
            da.Fill(ds, "OutstandingRecoveries");
            return ds;
        }

       
        public DataSet getOutstandingFixedDeductions(String Division, String Employee, String Deduction, Int32 option, Int32 intGroup)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            if (option == 1)
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo AS EmployeeNo,dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, dbo.CHKFixedDeductions.DivisionId AS Division,dbo.CHKDeductionGroup.ShortName AS DeductGroup, dbo.CHKDeduction.ShortName AS DeductCode, dbo.CHKFixedDeductions.DeductAmount AS InstallmentAmount,dbo.CHKFixedDeductions.NoOfMonths AS NoofInstallments, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.RecoveredInstallments, dbo.CHKFixedDeductions.RecoveredAmount FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKFixedDeductions.DeductionId WHERE     (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.DivisionId LIKE '" + Division + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = '" + intGroup + "') AND (dbo.CHKFixedDeductions.DeductionId LIKE '" + Deduction + "') AND (dbo.CHKFixedDeductions.EmpNo LIKE '" + Employee + "') ORDER BY dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, Division, DeductGroup, DeductCode", CommandType.Text);
            da.Fill(ds, "OutstandingRecoveries");
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{

            //}

            return ds;
        }
        
        //End 

        public DataTable SearchOutstandingFixedDeductions(String Div, String Emp, Int32 intDeductGroup, String intDeductId)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;

            dt.Columns.Add("Division");
            dt.Columns.Add("DeductGroup");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("StartYear");
            dt.Columns.Add("StartMonth");//4
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("DeductAmount");
            dt.Columns.Add("NoOfMonths");
            dt.Columns.Add("BalanceAmt");
            dt.Columns.Add("CloseYesNo");
            dt.Columns.Add("GroupID");
            dt.Columns.Add("DeductID");
            dt.Columns.Add("RefNo");

            reader = SQLHelper.ExecuteReader("SELECT dbo.CHKFixedDeductions.DivisionId, dbo.CHKDeductionGroup.ShortName AS DeductGroup, dbo.CHKDeduction.ShortName AS DeductCode,  dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, dbo.CHKFixedDeductions.EmpNo, dbo.CHKFixedDeductions.DeductAmount,  dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.CloseYesNo,  dbo.CHKFixedDeductions.DeductionGroupId, dbo.CHKFixedDeductions.DeductionId, dbo.CHKFixedDeductions.FixedDeductionId FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId AND  dbo.CHKDeduction.DeductionCode = dbo.CHKFixedDeductions.DeductionId WHERE (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.DeductionGroupId = '"+intDeductGroup+"') AND (dbo.CHKFixedDeductions.DeductionId like '"+intDeductId+"') AND  (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.EmpNo like '"+Emp+"') AND (dbo.CHKFixedDeductions.DivisionId like '"+Div+"')", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetDecimal(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetBoolean(9);
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetInt32(10);
                }
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetInt32(11);
                }
                if (!reader.IsDBNull(12))
                {
                    drow[12] = reader.GetInt32(12);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

        public DataTable SearchOutstandingLoanDeductions(String Div, String Emp, Int32 intDeductGroup, String intDeductId)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            DataRow drow;

            dt.Columns.Add("Division");
            dt.Columns.Add("DeductGroup");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("StartYear");
            dt.Columns.Add("StartMonth");//4
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("DeductAmount");
            dt.Columns.Add("NoOfMonths");
            dt.Columns.Add("BalanceAmt");
            dt.Columns.Add("CloseYesNo");
            dt.Columns.Add("GroupID");
            dt.Columns.Add("DeductID");
            dt.Columns.Add("RefNo");

            reader = SQLHelper.ExecuteReader("SELECT     dbo.CHKLoan.DivisionCode, dbo.CHKDeductionGroup.ShortName AS GroupCode, dbo.CHKDeduction.ShortName AS DeductCode, dbo.CHKLoan.StartYear,  dbo.CHKLoan.StartMonth, dbo.CHKLoan.EmployeeNo, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.BalanceAmount,  dbo.CHKLoan.ClosedYesNo, dbo.CHKLoan.DeductionGroup, dbo.CHKLoan.DeductionCode, dbo.CHKLoan.LoanCode FROM dbo.CHKLoan INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode AND  dbo.CHKLoan.DeductionGroup = dbo.CHKDeduction.DeductionGroupCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.CHKLoan.DivisionCode like '" + Div + "') AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.BalanceAmount > 0) AND (dbo.CHKLoan.DeductionGroup = '" + intDeductGroup + "') AND  (dbo.CHKLoan.DeductionCode like '" + intDeductId + "') AND (dbo.CHKLoan.EmployeeNo like '"+Emp+"')", CommandType.Text);
            drow = dt.NewRow();
            while (reader.Read())
            {
                drow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    drow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    drow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    drow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    drow[3] = reader.GetInt32(3);
                }
                if (!reader.IsDBNull(4))
                {
                    drow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    drow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    drow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    drow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    drow[8] = reader.GetDecimal(8);
                }
                if (!reader.IsDBNull(9))
                {
                    drow[9] = reader.GetBoolean(9);
                }
                if (!reader.IsDBNull(10))
                {
                    drow[10] = reader.GetInt32(10);
                }
                if (!reader.IsDBNull(11))
                {
                    drow[11] = reader.GetInt32(11);
                }
                if (!reader.IsDBNull(12))
                {
                    drow[12] = reader.GetInt32(12);
                }

                dt.Rows.Add(drow);
            }
            return dt;
        }

        public DataSet getOutstandingFixedDeductions(String strDiv, String strDeductGroup,String strDeductCode)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, dbo.CHKFixedDeductions.DivisionId AS Division,  dbo.CHKDeductionGroup.ShortName AS DeductGroup, dbo.CHKDeduction.ShortName AS DeductCode, dbo.CHKFixedDeductions.DeductAmount AS InstallmentAmount,  dbo.CHKFixedDeductions.NoOfMonths AS NoofInstallments, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.OldEntryYesNo FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeduction.DeductionCode = dbo.CHKFixedDeductions.DeductionId WHERE (dbo.CHKDeductionGroup.ShortName = '"+strDeductGroup+"') AND (dbo.CHKDeduction.ShortName = '"+strDeductCode+"') AND (dbo.CHKFixedDeductions.OldEntryYesNo = 0) AND  (dbo.CHKFixedDeductions.BalanceAmount > 0) ORDER BY dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, Division, DeductGroup, DeductCode", CommandType.Text);
            da.Fill(ds, "OutstandingRecoveries");
            return ds;
        }
        public DataSet getOutstandingLoans()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.DivisionCode AS Division, dbo.CHKDeductionGroup.ShortName AS DeductGroup, dbo.CHKDeduction.ShortName AS DeductCode, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.BalanceAmount FROM         dbo.CHKLoan INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.CHKLoan.StartMonth > 10) AND (dbo.CHKLoan.BalanceAmount > 0) ORDER BY dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, Division, DeductGroup, DeductCode", CommandType.Text);
            da.Fill(ds, "OutstandingRecoveries");
            return ds;
        }

        public DataSet getOutstandingLoans(String Division, String Deduction, String Employee,Int32 intDeductGroup)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT  dbo.CHKLoan.EmployeeNo,dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.DivisionCode AS Division, dbo.CHKDeductionGroup.ShortName AS DeductGroup,dbo.CHKDeduction.ShortName AS DeductCode, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.BalanceAmount FROM dbo.CHKLoan INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKLoan.BalanceAmount > 0) AND (dbo.CHKLoan.ClosedYesNo = 0) AND (dbo.CHKLoan.DivisionCode LIKE '" + Division.Trim() + "')  AND (dbo.CHKLoan.DeductionGroup = '"+intDeductGroup+"') AND (dbo.CHKLoan.DeductionCode LIKE '" + Deduction.Trim() + "') AND (dbo.CHKLoan.EmployeeNo LIKE '" + Employee.Trim() + "')ORDER BY dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, Division, DeductGroup, DeductCode", CommandType.Text);
            da.Fill(ds, "OutstandingRecoveries");
            return ds;
        }

        public DataSet getEPFETFForRemmittence(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.EmpNO, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) AS EPFPaybleAmount, SUM(dbo.EmpMonthlyEarnings.EPF10) AS EPF10, SUM(dbo.EmpMonthlyEarnings.EPF12) AS EPF12 FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') GROUP BY dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.EmpNO", CommandType.Text);
            da.Fill(ds, "EPFETFForRemmittence");
            return ds;
        }
        //public DataSet getHarvestRegister(String strDivision, Int32 intYear, Int32 intMonth)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.PaidHoliday, " +
        //              " dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.EstateField.FieldName, dbo.DailyGroundTransactions.EmpNo,  " +
        //              " dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  " +
        //              " dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.ExtraRates,  " +
        //              " dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.ManDays, " +
        //              " dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.PaidHoliday " +
        //              " FROM         dbo.DailyGroundTransactions INNER JOIN " +
        //              " dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN " +
        //              " dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  " +
        //              " dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN " +
        //              " dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN " +
        //              " dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN " +
        //              "  dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID " +
        //              " WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
        //    da.Fill(ds, "HarvestRegister");
        //    return ds;
        //}
        public DataSet getHarvestRegisterAllDivision(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.PaidHoliday, " +
                      " dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.EstateField.FieldName, dbo.DailyGroundTransactions.EmpNo,  " +
                      " dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  " +
                      " dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.ExtraRates,  " +
                      " dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.ManDays, " +
                      " dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.PaidHoliday " +
                      " FROM         dbo.DailyGroundTransactions INNER JOIN " +
                      " dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN " +
                      " dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  " +
                      " dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN " +
                      " dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN " +
                      " dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN " +
                      "  dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID " +
                      " WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
            da.Fill(ds, "HarvestRegisterAllDivision");
            return ds;
        }
        //public DataSet getHarvestRegisterDivision(String strDiv,Int32 intYear, Int32 intMonth)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.DateEntered, dbo.DailyGroundTransactions.HolidayYesNo, dbo.DailyGroundTransactions.PaidHoliday, " +
        //              " dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.FieldID, dbo.EstateField.FieldName, dbo.DailyGroundTransactions.EmpNo,  " +
        //              " dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkCodeID,  " +
        //              " dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.ExtraRates,  " +
        //              " dbo.DailyGroundTransactions.TaskCompleted, dbo.DailyGroundTransactions.PRIAmount, dbo.DailyGroundTransactions.ManDays, " +
        //              " dbo.DailyGroundTransactions.LabourType, dbo.DailyGroundTransactions.PaidHoliday " +
        //              " FROM dbo.DailyGroundTransactions INNER JOIN " +
        //              " dbo.EstateDivision ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN " +
        //              " dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  " +
        //              " dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN " +
        //              " dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN " +
        //              " dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName INNER JOIN " +
        //              "  dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID " +
        //              " WHERE  (dbo.DailyGroundTransactions.DivisionID='" + strDiv + "') AND  (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "')", CommandType.Text);
        //    da.Fill(ds, "HarvestRegisterAllDivision");
        //    return ds;
        //}

        //2011/06/08
        public DataSet getHarvestRegister(String strDivision, DateTime dtDate, Int32 wrkty,Boolean boolBlkPlk)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("WorkQTY"));
            dt.Columns.Add(new DataColumn("OverKg"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("Holiday"));
            dt.Columns.Add(new DataColumn("Mandays"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate"));;
            dt.Columns.Add(new DataColumn("QTY1"));
            dt.Columns.Add(new DataColumn("QTY2"));
            dt.Columns.Add(new DataColumn("QTY3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday,  dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General') THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,   CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime ,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND   dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND   dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID  WHERE (dbo.DailyGroundTransactions.CropType=1) and (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "')  AND (dbo.DailyGroundTransactions.CashBlockYesNo = '" + boolBlkPlk + "') AND (dbo.DailyGroundTransactions.IsContract = 0) ORDER BY dbo.DailyGroundTransactions.AutoKey  ", CommandType.Text);

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
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7).Trim();
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetString(9).Trim();
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetString(10).Trim();
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDateTime(11);
                }
                 if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                 if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                 if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }
                 if (!reader.IsDBNull(15))
                {
                    dtRow[15] = reader.GetDecimal(15);
                }
                 if (!reader.IsDBNull(16))
                {
                    dtRow[16] = reader.GetDecimal(16);
                }

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingNames"));
            dtnew.Columns.Add(new DataColumn("AbsentCount"));
            dtnew.Columns.Add(new DataColumn("NotOffered"));
            dtnew.Columns.Add(new DataColumn("Sundry"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));
            dtnew.Columns.Add(new DataColumn("PlkAreaCovered"));
            dtnew.Columns.Add(new DataColumn("SundryAreaCovered"));
            dtnew.Columns.Add(new DataColumn("CashPluckerCount"));


            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('PLK')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('PLK')) ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS ABS FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('ABS')) ", CommandType.Text);


            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[1] = reader.GetInt32(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('X%')) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[2] = reader.GetInt32(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK')) ", CommandType.Text);
            }

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();
            //-----------------------

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);
           
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[5] = reader.GetDecimal(0);
                    else
                        dtRow1[5] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID  like ('PLK'))", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[6] = reader.GetDecimal(0);
                    else
                        dtRow1[6] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID <> 'PLK')", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[7] = reader.GetDecimal(0);
                    else
                        dtRow1[7] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT count(EmpNo) AS CashPlkCount FROM            dbo.DailyGroundTransactions WHERE        (CropType = 1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (WorkCodeID = 'PLK') AND (CashPlkOkgYesNo = 0)", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[8] = reader.GetInt32(0);
                    else
                        dtRow1[8] = "0";
                }
            }
            reader.Dispose();



            dtnew.Rows.Add(dtRow1);

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }

        //-------------
        public DataSet getHarvestRegister(String strDivision, DateTime dtDate, Int32 wrkty,String empFrom,String empTo,Boolean boolBlkPlk)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("WorkQTY"));
            dt.Columns.Add(new DataColumn("OverKg"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("Holiday"));
            dt.Columns.Add(new DataColumn("Mandays"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate")); ;
            dt.Columns.Add(new DataColumn("QTY1"));
            dt.Columns.Add(new DataColumn("QTY2"));
            dt.Columns.Add(new DataColumn("QTY3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));

            SqlDataReader reader;
            DataRow dtRow;

            //reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday,  dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General') THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,   CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime ,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND   dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND   dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID  WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 0) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '"+empFrom+"') AND CONVERT(int, '"+empTo+"')) ORDER BY dbo.DailyGroundTransactions.AutoKey  ", CommandType.Text);
            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday,  dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General') THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,   CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime ,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND   dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND   dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID  WHERE (dbo.DailyGroundTransactions.CropType=1) and (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "')  AND (dbo.DailyGroundTransactions.CashBlockYesNo = '" + boolBlkPlk + "') AND (dbo.DailyGroundTransactions.IsContract = 0) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "')) ORDER BY dbo.DailyGroundTransactions.EmpNo  ", CommandType.Text);

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
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7).Trim();
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetString(9).Trim();
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetString(10).Trim();
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDateTime(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }
                if (!reader.IsDBNull(15))
                {
                    dtRow[15] = reader.GetDecimal(15);
                }
                if (!reader.IsDBNull(16))
                {
                    dtRow[16] = reader.GetDecimal(16);
                }

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingNames"));
            dtnew.Columns.Add(new DataColumn("AbsentCount"));
            dtnew.Columns.Add(new DataColumn("NotOffered"));
            dtnew.Columns.Add(new DataColumn("Sundry"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));
            dtnew.Columns.Add(new DataColumn("PlkAreaCovered"));
            dtnew.Columns.Add(new DataColumn("SundryAreaCovered"));


            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('PLK')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '"+empFrom+"') AND CONVERT(int, '"+empTo+"')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('PLK')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "')) ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS ABS FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('ABS')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "')) ", CommandType.Text);


            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[1] = reader.GetInt32(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('X%')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "')) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[2] = reader.GetInt32(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "'))", CommandType.Text);
            }

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();
            //-----------------------

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "'))", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "'))", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[5] = reader.GetDecimal(0);
                    else
                        dtRow1[5] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID  like ('PLK')) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "'))", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[6] = reader.GetDecimal(0);
                    else
                        dtRow1[6] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where  (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID <> 'PLK') AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '" + empFrom + "') AND CONVERT(int, '" + empTo + "'))", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[7] = reader.GetDecimal(0);
                    else
                        dtRow1[7] = "0";
                }
            }
            reader.Dispose();


            dtnew.Rows.Add(dtRow1);

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }
        //---------------

        public DataSet getHarvestRegisterCashOkg(String strDivision, DateTime dtDate, Int32 wrkty)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("WorkQTY"));
            dt.Columns.Add(new DataColumn("OverKg"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("Holiday"));
            dt.Columns.Add(new DataColumn("Mandays"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate")); ;
            dt.Columns.Add(new DataColumn("QTY1"));
            dt.Columns.Add(new DataColumn("QTY2"));
            dt.Columns.Add(new DataColumn("QTY3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("OkgAmount"));
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("CashBlockYesNo"));

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday,  dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General') THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,   CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime ,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight, dbo.DailyGroundTransactions.OverKgAmount,dbo.DailyGroundTransactions.CashKgAmount,CashBlockYesNo FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND   dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND   dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID  WHERE (dbo.DailyGroundTransactions.CropType=1) and (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 0) ORDER BY dbo.DailyGroundTransactions.AutoKey  ", CommandType.Text);

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
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7).Trim();
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetString(9).Trim();
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetString(10).Trim();
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDateTime(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }
                if (!reader.IsDBNull(15))
                {
                    dtRow[15] = reader.GetDecimal(15);
                }
                if (!reader.IsDBNull(16))
                {
                    dtRow[16] = reader.GetDecimal(16);
                }
                if (!reader.IsDBNull(17))
                {
                    dtRow[17] = reader.GetDecimal(17);
                }
                if (!reader.IsDBNull(16))
                {
                    dtRow[18] = reader.GetDecimal(18);
                }
                if (!reader.IsDBNull(19))
                {
                    dtRow[19] = reader.GetBoolean(19);
                }

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingNames"));
            dtnew.Columns.Add(new DataColumn("AbsentCount"));
            dtnew.Columns.Add(new DataColumn("NotOffered"));
            dtnew.Columns.Add(new DataColumn("Sundry"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));
            dtnew.Columns.Add(new DataColumn("PlkAreaCovered"));
            dtnew.Columns.Add(new DataColumn("SundryAreaCovered"));


            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=1) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('PLK')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('PLK')) ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS ABS FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('ABS')) ", CommandType.Text);


            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[1] = reader.GetInt32(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('X%')) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[2] = reader.GetInt32(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','PLK')) ", CommandType.Text);
            }

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();
            //-----------------------

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight  FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[5] = reader.GetDecimal(0);
                    else
                        dtRow1[5] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID  like ('PLK'))", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[6] = reader.GetDecimal(0);
                    else
                        dtRow1[6] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where (CropType=1) AND  (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID <> 'PLK')", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[7] = reader.GetDecimal(0);
                    else
                        dtRow1[7] = "0";
                }
            }
            reader.Dispose();


            dtnew.Rows.Add(dtRow1);

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }

          public DataSet getBlockPluckingRegister(String strDivision, DateTime dtDate)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("BlockPlkQTY"));
            dt.Columns.Add(new DataColumn("BlockPlkAmount"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate"));

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.BlockPlkKgs, dbo.DailyGroundTransactions.BlockPlkAmount, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime AS CreateDateTime FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DivisionID = '"+strDivision+"') AND (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND  (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102))", CommandType.Text);

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
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetDecimal(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }              

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            //------summery-------//
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("BlockPlkKilos"));
            dtnew.Columns.Add(new DataColumn("BlockPlkAmount"));
            dtnew.Columns.Add(new DataColumn("EmpCount"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));

            DataRow dtRow1;
            dtRow1 = dtnew.NewRow();
            //Block Plucking Kilos
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.BlockPlkKgs) AS BlockPlkKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DivisionID = '"+strDivision+"') AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1)", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();
            //Block Plucking Amount
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.BlockPlkAmount) AS BlockPlkAmount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DivisionID = '"+strDivision+"') AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1)", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[1] = reader.GetDecimal(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();
            //Block Plucking Amount
            reader = SQLHelper.ExecuteReader("SELECT COUNT(dbo.DailyGroundTransactions.EmpNo) AS EmpCount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1)", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[2] = reader.GetInt32(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight  FROM dbo.DailyGroundTransactions WHERE     (DivisionID = '" + strDivision + "') AND (WorkType = 2) AND (CashBlockYesNo = 1) AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions WHERE     (DivisionID = '" + strDivision + "') AND (WorkType = 2) AND (CashBlockYesNo = 1) AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102))  ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();
            dtnew.Rows.Add(dtRow1);

            //--------------------//

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);
            return ds;
        }

        public DataSet getBlockPlucking2013Register(String strDivision, DateTime dtDate,String strContractor)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("BlockPlkQTY"));
            dt.Columns.Add(new DataColumn("BlockPlkAmount"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate"));
            dt.Columns.Add(new DataColumn("Contractor"));

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.BlockPlkKgs, dbo.DailyGroundTransactions.BlockPlkAmount, dbo.DailyGroundTransactions.UserID, dbo.DailyGroundTransactions.CreateDateTime,  dbo.DailyGroundTransactions.Contractor FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.WorkType = 2) AND  (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.Contractor like '"+strContractor+"')", CommandType.Text);

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
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetDecimal(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            //------summery-------//
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("BlockPlkKilos"));
            dtnew.Columns.Add(new DataColumn("BlockPlkAmount"));
            dtnew.Columns.Add(new DataColumn("EmpCount"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));

            DataRow dtRow1;
            dtRow1 = dtnew.NewRow();
            //Block Plucking Kilos
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.BlockPlkKgs) AS BlockPlkKgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND  (dbo.DailyGroundTransactions.Contractor = '" + strContractor + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();
            //Block Plucking Amount
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.BlockPlkAmount) AS BlockPlkAmount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND  (dbo.DailyGroundTransactions.Contractor = '" + strContractor + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[1] = reader.GetDecimal(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();
            //Block Plucking Amount
            reader = SQLHelper.ExecuteReader("SELECT COUNT(dbo.DailyGroundTransactions.EmpNo) AS EmpCount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND  (dbo.DailyGroundTransactions.Contractor = '" + strContractor + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[2] = reader.GetInt32(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight  FROM dbo.DailyGroundTransactions WHERE     (DivisionID = '" + strDivision + "') AND (WorkType = 2) AND (CashBlockYesNo = 1) AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.Contractor = '" + strContractor + "') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions WHERE     (DivisionID = '" + strDivision + "') AND (WorkType = 2) AND (CashBlockYesNo = 1) AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (dbo.DailyGroundTransactions.Contractor = '" + strContractor + "')  ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();
            dtnew.Rows.Add(dtRow1);

            //--------------------//

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);
            return ds;
        }
        public DataSet getContractCWRegister(String strDivision, DateTime dtDate, Int32 wrkty,String empType)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("WorkQTY"));
            dt.Columns.Add(new DataColumn("OverKg"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("Holiday"));
            dt.Columns.Add(new DataColumn("Mandays"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate")); ;
            dt.Columns.Add(new DataColumn("QTY1"));
            dt.Columns.Add(new DataColumn("QTY2"));
            dt.Columns.Add(new DataColumn("QTY3"));
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("FieldWeight"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("CashSundryAmount"));
            dt.Columns.Add(new DataColumn("ContractorRate"));

            SqlDataReader reader;
            DataRow dtRow;
            if (empType.Equals("Contractors"))
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1)  THEN 'Y' ELSE 'N' END AS Holiday, dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime, dbo.DailyGroundTransactions.WorkQty1, dbo.DailyGroundTransactions.WorkQty2,  dbo.DailyGroundTransactions.WorkQty3, dbo.DailyGroundTransactions.AreaCovered, dbo.DailyGroundTransactions.FieldWeight,  CASE WHEN (DailyGroundTransactions.Contractor = 'Contractor') THEN EmpRef ELSE DailyGroundTransactions.Contractor END AS Contractor,  dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ContractorRate FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE     (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1) AND  (dbo.DailyGroundTransactions.Contractor = 'Contractor') ORDER BY dbo.DailyGroundTransactions.AutoKey ", CommandType.Text); 
            }
            else if (empType.Equals("Labours"))
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1)  THEN 'Y' ELSE 'N' END AS Holiday, dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime, dbo.DailyGroundTransactions.WorkQty1, dbo.DailyGroundTransactions.WorkQty2,  dbo.DailyGroundTransactions.WorkQty3, dbo.DailyGroundTransactions.AreaCovered, dbo.DailyGroundTransactions.FieldWeight,  CASE WHEN (DailyGroundTransactions.Contractor = 'Contractor') THEN 'NA' ELSE DailyGroundTransactions.Contractor END AS Contractor,  dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ContractorRate FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE     (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1) AND  (dbo.DailyGroundTransactions.Contractor <> 'Contractor') ORDER BY dbo.DailyGroundTransactions.AutoKey  ", CommandType.Text);
            }
            else
            {
                reader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1)  THEN 'Y' ELSE 'N' END AS Holiday, dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General')  THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime, dbo.DailyGroundTransactions.WorkQty1, dbo.DailyGroundTransactions.WorkQty2,  dbo.DailyGroundTransactions.WorkQty3, dbo.DailyGroundTransactions.AreaCovered, dbo.DailyGroundTransactions.FieldWeight,  CASE WHEN (DailyGroundTransactions.Contractor = 'Contractor') THEN 'NA' ELSE DailyGroundTransactions.Contractor END AS Contractor,  dbo.DailyGroundTransactions.CashKgAmount, dbo.DailyGroundTransactions.CashSundryAmount, dbo.DailyGroundTransactions.ContractorRate FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE     (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1)  ORDER BY dbo.DailyGroundTransactions.AutoKey  ", CommandType.Text);
            }

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
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7).Trim();
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetString(9).Trim();
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetString(10).Trim();
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDateTime(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }
                if (!reader.IsDBNull(15))
                {
                    dtRow[15] = reader.GetDecimal(15);
                }
                if (!reader.IsDBNull(16))
                {
                    dtRow[16] = reader.GetDecimal(16);
                }
                if (!reader.IsDBNull(17))
                {
                    dtRow[17] = reader.GetString(17).Trim();
                }
                if (!reader.IsDBNull(18))
                {
                    dtRow[18] = reader.GetDecimal(18);
                }
                if (!reader.IsDBNull(19))
                {
                    dtRow[19] = reader.GetDecimal(19);
                }
                if (!reader.IsDBNull(20))
                {
                    dtRow[20] = reader.GetDecimal(20);
                }
                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingKilos"));
            dtnew.Columns.Add(new DataColumn("SundryManDays"));
            dtnew.Columns.Add(new DataColumn("ContractorPluckingPay"));
            dtnew.Columns.Add(new DataColumn("ContractorSundryPay"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));

            DataRow dtRow1;
            dtRow1 = dtnew.NewRow();
            //Plucking Kilos
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.WorkQty) AS Kgs FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1) ", CommandType.Text);           
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();

            //Sundry Mandays
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.CashManDays) AS CashManDays FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1)", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[1] = reader.GetDecimal(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();

            //Contractor Plucking Pay
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.CashKgAmount) AS CashKgAmount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1)  AND  (dbo.DailyGroundTransactions.Contractor = 'Contractor') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[2] = reader.GetDecimal(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            //Contractor Sundry Pay
            reader = SQLHelper.ExecuteReader("SELECT SUM(dbo.DailyGroundTransactions.CashSundryAmount) AS CashSundryAmount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 1)  AND  (dbo.DailyGroundTransactions.Contractor = 'Contractor')", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();
            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight FROM  dbo.DailyGroundTransactions WHERE (DivisionID = '" + strDivision + "') AND (WorkType = 2)  AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND   (IsContract = 1) AND (Contractor <> 'Contractor')", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[4] = reader.GetDecimal(0);
                    else
                        dtRow1[4] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered FROM dbo.DailyGroundTransactions WHERE (DivisionID = '" + strDivision + "') AND (WorkType = 2) AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (IsContract = 1) AND  (Contractor <> 'Contractor') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[5] = reader.GetDecimal(0);
                    else
                        dtRow1[5] = "0";
                }
            }
            reader.Dispose();
            dtnew.Rows.Add(dtRow1);
            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }
        //public DataSet getHarvestRegisterTotal(String strDivision, DateTime dtDate)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'PLK') THEN 1 ELSE 0 END AS PLKNames, "+
        //              " CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'ABS') THEN 1 ELSE 0 END AS ABSENTNames, "+
        //              " CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID = 'X%') THEN 1 ELSE 0 END AS NOTOFFEREDNames, "+
        //              " CASE WHEN (dbo.DailyGroundTransactions.WorkCodeID NOT IN ('PLK', 'ABS', 'X%')) THEN 1 ELSE 0 END AS SundryNames, "+
        //              " dbo.DailyGroundTransactions.WorkCodeID "+
        //              " FROM         dbo.DailyGroundTransactions INNER JOIN "+
        //              " dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND "+
        //              " dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN "+
        //              " dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND "+
        //              " dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID "+
        //              " WHERE     (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "') AND (dbo.DailyGroundTransactions.WorkType = '1') "+
        //              " ORDER BY dbo.DailyGroundTransactions.WorkCodeID, dbo.DailyGroundTransactions.AutoKey", CommandType.Text);
        //    da.Fill(ds, "HarvestRegisterNormalW_SUM");
        //    return ds;
        //}
        //END


        //2011/06/09
        public DataSet getHarvestRegisterCash(String strDivision, DateTime dtDate)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgAmount, " +
                      " dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday, " +
                      " dbo.DailyGroundTransactions.ManDays, dbo.DailyGroundTransactions.FieldID, dbo.DailyGroundTransactions.WorkCodeID, " +
                      " CASE WHEN (dbo.DailyGroundTransactions.TaskCompleted = 1) THEN 'Y' ELSE 'N' END AS PRI, dbo.DailyGroundTransactions.UserID, " +
                      " dbo.DailyGroundTransactions.CreateDateTime " +
                      " FROM  dbo.DailyGroundTransactions INNER JOIN " +
                      " dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND  " +
                      " dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN " +
                      " dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND  " +
                      " dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID " +
                      " WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '2') ORDER BY dbo.DailyGroundTransactions.AutoKey ", CommandType.Text);
            da.Fill(ds, "HarvestRegister");
            return ds;
        }
        public DataSet getOverTimeRegister(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT convert(varchar(50),dbo.CHKOvertime.OtDate,102) as otDate, dbo.CHKOvertime.WorkType, dbo.EmployeeCategory.CategoryName, dbo.CHKOvertime.EmployeeNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.CHKOvertime.Field, dbo.JobMaster.JobName, dbo.CHKOvertime.Hours, dbo.CHKOvertime.Expenditure FROM dbo.CHKOvertime INNER JOIN dbo.EmployeeCategory ON dbo.CHKOvertime.CategoryCode = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EmployeeMaster ON dbo.CHKOvertime.EmployeeNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobID WHERE (YEAR(dbo.CHKOvertime.OtDate) = '" + intYear + "') AND (MONTH(dbo.CHKOvertime.OtDate) = '" + intMonth + "')", CommandType.Text);
            da.Fill(ds, "OverTimeRegister");
            return ds;
        }
        public DataSet getOverTimeRegister(String strDiv, DateTime dtDate)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.CHKOvertime.OtDate, dbo.CHKOvertime.EmployeeNo, dbo.EmployeeMaster.EMPName, dbo.CHKOvertime.Field, dbo.CHKOvertime.Job, " +
                     " dbo.CHKOvertime.Hours, dbo.CHKOTParameters.OTType, dbo.CHKOvertime.Expenditure AS OTAmount, dbo.CHKOvertime.CreateDateTime,  " +
                     " dbo.CHKOvertime.UserId FROM dbo.CHKOvertime INNER JOIN  dbo.EmployeeMaster ON dbo.CHKOvertime.EmployeeNo = dbo.EmployeeMaster.EmpNo AND " +
                     " dbo.CHKOvertime.DivisionCode = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId " +
                     " WHERE (dbo.CHKOvertime.OtDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (dbo.CHKOvertime.DivisionCode = '" + strDiv + "') ORDER BY dbo.CHKOvertime.OverTimeId", CommandType.Text);
            da.Fill(ds, "OverTimeRegister");
            return ds;
        }
        public DataSet getAdditionsRegister(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.CHKEmpAdditions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.CHKAddition.AdditionName, dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.Description, dbo.CHKEmpAdditions.Amount FROM dbo.CHKEmpAdditions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpAdditions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId WHERE (dbo.CHKEmpAdditions.CancelYesNo = 0) AND (dbo.CHKEmpAdditions.AdditionYear = '" + intYear + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonth + "')", CommandType.Text);
            da.Fill(ds, "AdditionsRegister");
            return ds;
        }
        public DataSet getDeductionsRegister(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.CHKEmpDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.CHKDeductionGroup.GroupName, dbo.CHKDeduction.DeductionName, dbo.CHKEmpDeductions.Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKEmpDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKEmpDeductions.DeductYear = '" + intYear + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + intMonth + "') AND (dbo.CHKEmpDeductions.CancelYesNo = 0)", CommandType.Text);
            da.Fill(ds, "DeductionsRegister");
            return ds;
        }
        public DataSet getRFTDeductionsRegister(Int32 intYear, Int32 intMonth, String strDiv, Int32 intDGroup, Int32 intDdeduct)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.CHKRFTDeductions.RFTDeductId, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKRFTDeductions.DeductGroupId, dbo.CHKDeductionGroup.ShortName, dbo.CHKRFTDeductions.DeductId, dbo.CHKDeduction.ShortName AS Expr1, " +
                      " dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.RFTDeductAmount, dbo.CHKRFTDeductions.UserId, dbo.CHKRFTDeductions.CreateDateTime, dbo.CHKRFTDeductions.RFTRate, dbo.CHKRFTDeductions.RFTQty FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN " +
                      " dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKRFTDeductions.DYear = '" + intYear + "') AND (dbo.CHKRFTDeductions.DMonth = '" + intMonth + "') AND (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKDeductionGroup.DeductionGroupCode = '" + intDGroup + "') AND (dbo.CHKDeduction.DeductionCode = '" + intDdeduct + "') ORDER BY dbo.CHKRFTDeductions.RFTDeductId ", CommandType.Text);
            da.Fill(ds, "RFTDeductRegister");
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.CHKRFTDeductions.RFTDeductId, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKRFTDeductions.DeductGroupId, dbo.CHKDeductionGroup.ShortName, dbo.CHKRFTDeductions.DeductId, dbo.CHKDeduction.ShortName AS Expr1, " +
            //          " dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.RFTDeductAmount, dbo.CHKRFTDeductions.UserId, dbo.CHKRFTDeductions.CreateDateTime, dbo.CHKRFTDeductions.RFTRate, dbo.CHKRFTDeductions.RFTQty FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN " +
            //          " dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE  (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKDeductionGroup.DeductionGroupCode = '" + intDGroup + "') AND (dbo.CHKDeduction.DeductionCode = '" + intDdeduct + "') ORDER BY dbo.CHKRFTDeductions.RFTDeductId ", CommandType.Text);
            //da.Fill(ds, "RFTDeductRegister");
            return ds;
        }
        public DataSet GetFixedDeductionRegister(Int32 intYear, Int32 intMonth, String strDiv, Int32 intDGroup, Int32 intDdeduct)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("GroupCode"));
            dt.Columns.Add(new DataColumn("DeductCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("NoOfMonths"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate"));
            dt.Columns.Add(new DataColumn("BalanceAmount"));
            dt.Columns.Add(new DataColumn("StartYear"));
            dt.Columns.Add(new DataColumn("StartMonth"));
            SqlDataReader reader;
            DataRow dtRow;
            //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.UserId, dbo.CHKFixedDeductions.CreateDateTime, dbo.CHKFixedDeductions.BalanceAmount FROM  dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN  dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode   WHERE (dbo.CHKFixedDeductions.StartYear = '" + intYear + "') AND (dbo.CHKFixedDeductions.StartMonth = '" + intMonth + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDdeduct + "') AND (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "')  ORDER BY dbo.CHKFixedDeductions.FixedDeductionId", CommandType.Text);
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.UserId, dbo.CHKFixedDeductions.CreateDateTime, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth FROM  dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN  dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode   WHERE  (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDdeduct + "') AND (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "')  AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.CloseYesNo = 0)  ORDER BY dbo.CHKFixedDeductions.FixedDeductionId", CommandType.Text);
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
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetInt32(9);
                }
                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet GetMonthlyAdvanceReport(Int32 intYear, Int32 intMonth, String strDiv,Int32 intDeductId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("Year"));
            dt.Columns.Add(new DataColumn("Month"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Amount"));
            SqlDataReader reader;
            DataRow dtRow;
            reader = SQLHelper.ExecuteReader("SELECT     dbo.CHKFixedDeductions.DivisionId, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth, dbo.CHKFixedDeductions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKFixedDeductions.DeductionGroupId FROM dbo.CHKFixedDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKFixedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.CHKFixedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE  (dbo.CHKFixedDeductions.DivisionId like '" + strDiv + "') AND (dbo.CHKFixedDeductions.StartYear = '" + intYear + "') AND (dbo.CHKFixedDeductions.StartMonth = '" + intMonth + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = (SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup  WHERE (ShortName = 'MA')))  AND (dbo.CHKFixedDeductions.DeductionId = '"+intDeductId+"')", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetInt32(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetString(3).Trim();
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetDecimal(5);
                }
                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            ds.Tables.Add(dt);
            return ds;
        }

        //public DataSet GetDivisionWiseMonthlyAdvanceReport(Int32 intYear, Int32 intMonth, String strDiv, Int32 intDeductId)
        //{
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("Division"));
        //    dt.Columns.Add(new DataColumn("DivisionName"));
        //    dt.Columns.Add(new DataColumn("Amount"));
        //    SqlDataReader reader;
        //    DataRow dtRow;
        //    reader = SQLHelper.ExecuteReader("SELECT        dbo.CHKFixedDeductions.DivisionId, dbo.EstateDivision.DivisionName, dbo.CHKFixedDeductions.DeductAmount FROM            dbo.CHKFixedDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKFixedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKFixedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.CHKFixedDeductions.DivisionId = dbo.EstateDivision.DivisionID WHERE        (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.StartYear = '" + intYear + "') AND  (dbo.CHKFixedDeductions.StartMonth = '" + intMonth + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = (SELECT        DeductionGroupCode FROM            dbo.CHKDeductionGroup WHERE        (ShortName = 'MA'))) AND (dbo.CHKFixedDeductions.DeductionId = '" + intDeductId + "')", CommandType.Text);
        //    while (reader.Read())
        //    {
        //        dtRow = dt.NewRow();

        //        if (!reader.IsDBNull(0))
        //        {
        //            dtRow[0] = reader.GetString(0).Trim();
        //        }
        //        if (!reader.IsDBNull(1))
        //        {
        //            dtRow[1] = reader.GetString(1).Trim();
        //        }                
        //        if (!reader.IsDBNull(5))
        //        {
        //            dtRow[5] = reader.GetDecimal(5);
        //        }
        //        dt.Rows.Add(dtRow);
        //    }
        //    reader.Dispose();
        //    ds.Tables.Add(dt);
        //    return ds;
        //}
        public DataSet GetDivisionWiseMonthlyAdvanceReport(Int32 intYear, Int32 intMonth, String strDiv, Int32 intDeductId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("DivisionName"));
            dt.Columns.Add(new DataColumn("Amount"));
            SqlDataReader reader;
            DataRow dtRow;
            reader = SQLHelper.ExecuteReader("SELECT        dbo.CHKFixedDeductions.DivisionId, dbo.EstateDivision.DivisionName, dbo.CHKFixedDeductions.DeductAmount FROM            dbo.CHKFixedDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKFixedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKFixedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.CHKFixedDeductions.DivisionId = dbo.EstateDivision.DivisionID WHERE        (dbo.CHKFixedDeductions.DivisionId like '" + strDiv + "') AND (dbo.CHKFixedDeductions.StartYear = '" + intYear + "') AND  (dbo.CHKFixedDeductions.StartMonth = '" + intMonth + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = (SELECT        DeductionGroupCode FROM            dbo.CHKDeductionGroup WHERE        (ShortName = 'MA'))) AND (dbo.CHKFixedDeductions.DeductionId = '" + intDeductId + "')", CommandType.Text);
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
                    dtRow[2] = reader.GetDecimal(2);
                }
                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet GetBlockPlkAdvanceReport(DateTime StartDate, DateTime EndDate, String strDiv)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Kgs"));
            dt.Columns.Add(new DataColumn("Amount"));
            SqlDataReader reader;
            DataRow dtRow;
            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, SUM(dbo.DailyGroundTransactions.BlockPlkKgs) AS Kgs, SUM(dbo.DailyGroundTransactions.BlockPlkAmount) AS Amount FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') AND (dbo.DailyGroundTransactions.WorkType = 2) AND (dbo.DailyGroundTransactions.CashBlockYesNo = 1) AND  (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + StartDate + "', 102) AND CONVERT(DATETIME, '" + EndDate + "', 102)) AND   (dbo.DailyGroundTransactions.BlockPlkKgs > 0) GROUP BY dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName", CommandType.Text);
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
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
               
                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet GetFixedAdditionRegister(String strDivval, Int32 intYearval, Int32 intMonthval, Int32 intAddVal)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("AdditionCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("AdditionYear"));
            dt.Columns.Add(new DataColumn("AdditionMonth"));
            dt.Columns.Add(new DataColumn("DivsionId"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreateDate"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKEmpAdditions.EmpNo, dbo.CHKAddition.AdditionShortName, dbo.CHKEmpAdditions.Amount, dbo.CHKEmpAdditions.AdditionYear, dbo.CHKEmpAdditions.AdditionMonth, dbo.CHKEmpAdditions.DivisionID, dbo.CHKEmpAdditions.UserId, dbo.CHKEmpAdditions.CreateDateTime FROM dbo.CHKEmpAdditions INNER JOIN  dbo.CHKAddition ON dbo.CHKEmpAdditions.AdditionId = dbo.CHKAddition.AdditionId " +
                                                " WHERE (dbo.CHKEmpAdditions.DivisionID = '" + strDivval + "') AND (dbo.CHKEmpAdditions.AdditionYear = '" + intYearval + "') AND (dbo.CHKEmpAdditions.AdditionMonth = '" + intMonthval + "') AND (dbo.CHKEmpAdditions.AdditionId = '" + intAddVal + "')", CommandType.Text);

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
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            ds.Tables.Add(dt);
            return ds;
        }
        public DataSet GetLoanDeductionRegister(String strDiv, Int32 intYear, Int32 intMonth, Int32 intGroup, Int32 intDeduct)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("InstallmentAmount");
            dt.Columns.Add("StartYear");
            dt.Columns.Add("StartMonth");
            dt.Columns.Add("NoofInstallments");
            dt.Columns.Add("Principalamount");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("LoanName");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT DeductionGroup, DeductionCode, CategoryCode, DivisionCode, EmployeeNo, LoanName, Principalamount, NoofInstallments, InstallmentAmount,AccountNo, LoanDate,LoanCode FROM CHKLoan", CommandType.Text);
            //dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount, "+
            //        "  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode "+
            //        " WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (StartYear='" + intYear + "')  AND (StartMonth='" + intMonth + "') ", CommandType.Text);
            ////dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount, " +
            ////        "  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode " +
            ////        " WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (StartYear='" + intYear + "')  AND (StartMonth='" + intMonth + "') ", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount, " +
                    "  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode " +
                    " WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.BalanceAmount > 0) AND (dbo.CHKLoan.ClosedYesNo = 0) ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetString(0).Trim();
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
                    drow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetInt32(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    drow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    drow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    drow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    drow[9] = dataReader.GetString(9).Trim();
                }
                

                dt.Rows.Add(drow);
            }
            dataReader.Close();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet GetLoanDeductionRegisterWithPayees(String strDiv, Int32 intYear, Int32 intMonth, Int32 intGroup, Int32 intDeduct)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("InstallmentAmount");
            dt.Columns.Add("StartYear");
            dt.Columns.Add("StartMonth");
            dt.Columns.Add("NoofInstallments");
            dt.Columns.Add("Principalamount");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("LoanName");
            dt.Columns.Add("PayeeName");
            dt.Columns.Add("PayeeAccount");
            dt.Columns.Add("PayeeAmount");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT DeductionGroup, DeductionCode, CategoryCode, DivisionCode, EmployeeNo, LoanName, Principalamount, NoofInstallments, InstallmentAmount,AccountNo, LoanDate,LoanCode FROM CHKLoan", CommandType.Text);
            //dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName,  dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount,  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName, dbo.CHKLoanPayeeDetails.PayeeName, dbo.CHKLoanPayeeDetails.PayeeAccount,  dbo.CHKLoanPayeeDetails.PayeeAmount FROM         dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKLoanPayeeDetails ON dbo.CHKLoan.StartYear = dbo.CHKLoanPayeeDetails.StartYear AND  dbo.CHKLoan.StartMonth = dbo.CHKLoanPayeeDetails.StartMonth AND dbo.CHKLoan.DivisionCode = dbo.CHKLoanPayeeDetails.DivisionID AND  dbo.CHKLoan.EmployeeNo = dbo.CHKLoanPayeeDetails.EmpNo AND dbo.CHKLoan.DeductionGroup = dbo.CHKLoanPayeeDetails.DeductionGroupId AND  dbo.CHKLoan.DeductionCode = dbo.CHKLoanPayeeDetails.DeductId WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND  (dbo.CHKLoan.StartYear = '" + intYear + "') AND (dbo.CHKLoan.StartMonth = '" + intMonth + "')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName,  dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount,  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName, dbo.CHKLoanPayeeDetails.PayeeName, dbo.CHKLoanPayeeDetails.PayeeAccount,  dbo.CHKLoanPayeeDetails.PayeeAmount FROM         dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKLoanPayeeDetails ON dbo.CHKLoan.StartYear = dbo.CHKLoanPayeeDetails.StartYear AND  dbo.CHKLoan.StartMonth = dbo.CHKLoanPayeeDetails.StartMonth AND dbo.CHKLoan.DivisionCode = dbo.CHKLoanPayeeDetails.DivisionID AND  dbo.CHKLoan.EmployeeNo = dbo.CHKLoanPayeeDetails.EmpNo AND dbo.CHKLoan.DeductionGroup = dbo.CHKLoanPayeeDetails.DeductionGroupId AND  dbo.CHKLoan.DeductionCode = dbo.CHKLoanPayeeDetails.DeductId WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (dbo.CHKLoan.BalanceAmount > 0) AND (dbo.CHKLoan.ClosedYesNo = 0) ", CommandType.Text);
           while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetString(0).Trim();
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
                    drow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetInt32(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    drow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    drow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    drow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    drow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    drow[10] = dataReader.GetString(10).Trim();
                }
                if (!dataReader.IsDBNull(11))
                {
                    drow[11] = dataReader.GetString(11).Trim();
                }
                if (!dataReader.IsDBNull(12))
                {
                    drow[12] = dataReader.GetDecimal(12);
                }


                dt.Rows.Add(drow);
            }
            dataReader.Close();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet getSkippedDeductionsRegister(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.CHKSystemSkipedDeductions.DivisionId, dbo.CHKSystemSkipedDeductions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.CHKDeductionGroup.ShortName as GroupShortName, dbo.CHKDeductionGroup.GroupName, dbo.CHKDeduction.ShortName AS DeductShortName, dbo.CHKDeduction.DeductionName,  dbo.CHKSystemSkipedDeductions.DeductAmount FROM dbo.CHKSystemSkipedDeductions INNER JOIN dbo.CHKDeduction ON dbo.CHKSystemSkipedDeductions.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKSystemSkipedDeductions.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKSystemSkipedDeductions.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.EmployeeMaster ON dbo.CHKSystemSkipedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.CHKSystemSkipedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKSystemSkipedDeductions.Year = '" + intYear + "') AND (dbo.CHKSystemSkipedDeductions.Month =  '" + intMonth + "') ORDER BY dbo.CHKSystemSkipedDeductions.DivisionId, dbo.CHKSystemSkipedDeductions.EmpNo, dbo.CHKDeductionGroup.GroupName, DeductShortName ", CommandType.Text);
            da.Fill(ds, "SkippedDeductions");
            return ds;
        }
        public DataSet getLoanRegister(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.CHKDeductionGroup.GroupName, dbo.CHKDeduction.DeductionName, dbo.EstateDivision.DivisionName, dbo.CHKLoan.EmployeeNo, dbo.CHKLoan.LoanName, dbo.CHKLoan.LoanDate, dbo.CHKLoan.Principalamount, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.AccountNo, dbo.CHKLoan.RecovredInstallments, dbo.CHKLoan.RecoveredAmount, dbo.CHKLoan.BalanceAmount, dbo.EmployeeMaster.EMPName FROM dbo.CHKLoan INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.EstateDivision ON dbo.CHKLoan.DivisionCode = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.CHKLoan.EmployeeNo = dbo.EmployeeMaster.EmpNo WHERE (YEAR(dbo.CHKLoan.LoanDate) = '" + intYear + "') AND (MONTH(dbo.CHKLoan.LoanDate) = '" + intMonth + "')", CommandType.Text);
            da.Fill(ds, "LoanRegister");
            return ds;
        }
        public DataSet getDebtorList(Int32 intYear, Int32 intMonth,String strDiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  dbo.EstateDivision.DivisionName, dbo.ChkDebtors.EmpNO, dbo.EmployeeMaster.EMPName, dbo.ChkDebtors.DebtAmount,  dbo.CHKDeductionGroup.GroupName FROM         dbo.ChkDebtors INNER JOIN dbo.EstateDivision ON dbo.ChkDebtors.DivisionId = dbo.EstateDivision.DivisionID INNER JOIN dbo.EmployeeMaster ON dbo.ChkDebtors.EmpNO = dbo.EmployeeMaster.EmpNo AND dbo.ChkDebtors.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.CHKDeductionGroup ON dbo.ChkDebtors.DeductionGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE     (dbo.ChkDebtors.DebtYear = '" + intYear + "') AND (dbo.ChkDebtors.DebtMonth = '" + intMonth + "') AND (dbo.ChkDebtors.DivisionId = '"+strDiv+"')", CommandType.Text);
            da.Fill(ds, "HarvestRegister");
            return ds;
        }
        public DataSet getEmployeeMovements(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EstateDivision.DivisionName, dbo.TempEmpMovements.LastMonth, dbo.TempEmpMovements.New, dbo.TempEmpMovements.Out, dbo.TempEmpMovements.Balance FROM dbo.TempEmpMovements INNER JOIN dbo.EstateDivision ON dbo.TempEmpMovements.DivisionID = dbo.EstateDivision.DivisionID WHERE (dbo.TempEmpMovements.Year = '" + intYear + "') AND (dbo.TempEmpMovements.Month = '" + intMonth + "')", CommandType.Text);
            da.Fill(ds, "EmployeeMovements");
            return ds;
        }
        public DataSet get6MonthETF(Int32 intYear)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT SUM(dbo.EmpMonthlyEarnings.ETF3) AS ETF, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) AS TotalEarnings, dbo.EmployeeMaster.NICNo, dbo.EmployeeMaster.EPFNo FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6)  GROUP BY dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.NICNo, dbo.EmployeeMaster.EPFNo", CommandType.Text);
            da.Fill(ds, "etf6months");
            return ds;
        }

        public DataSet getEPF6Month(Int32 intYear,Int32 intPeriod,Int32 intAll,String strDiv)
        {
            if (intPeriod == 1)
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Contribution' AS GROUPName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '"+intYear+"') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')   AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month  UNION SELECT  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(EPFPaybleAmount) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Earnings' AS GROUPName  FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID  AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '"+intYear+"') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')   AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month", CommandType.Text);
                da.Fill(ds, "EPF6Months");
                return ds;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Contribution' AS GROUPName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')   AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month  UNION SELECT  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(EPFPaybleAmount) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Earnings' AS GROUPName  FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID  AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')   AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month", CommandType.Text);
                da.Fill(ds, "EPF6Months");
                return ds;
            }
        }

        public DataSet getETF6Month(Int32 intYear, Int32 intPeriod,Int32 intAll,String strDiv)
        {
            if (intPeriod == 1)
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(dbo.EmpMonthlyEarnings.ETF3) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Contribution' AS GROUPName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')   AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month  UNION SELECT  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(EPFPaybleAmount) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Earnings' AS GROUPName  FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID  AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '"+intYear+"') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6)  AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')  AND (dbo.EmployeeMaster.ActiveEmployee = 1) AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month", CommandType.Text);
                da.Fill(ds, "ETF6Months");
                return ds;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(dbo.EmpMonthlyEarnings.ETF3) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Contribution' AS GROUPName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')  AND (dbo.EmployeeMaster.ActiveEmployee = 1)  AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month  UNION SELECT  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  SUM(EPFPaybleAmount) AS Amount, dbo.EmpMonthlyEarnings.Month,  'Earnings' AS GROUPName  FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID  AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo  WHERE (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12)  AND ( dbo.EmployeeMaster.DivisionID like '" + strDiv + "')   AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName = 'RG'))) GROUP BY  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EmpNo,dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmpMonthlyEarnings.Month", CommandType.Text);
                da.Fill(ds, "ETF6Months");
                return ds;
            }
        }

        public DataSet getEPF6MonthReportData(Int32 intYear, Int32 intPeriod, Int32 intAll, String strDiv)
        {
            DataSet dsData = new DataSet();
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("DivisionId"));
            dtMain.Columns.Add(new DataColumn("Year"));
            dtMain.Columns.Add(new DataColumn("Month"));
            dtMain.Columns.Add(new DataColumn("MID"));
            dtMain.Columns.Add(new DataColumn("EmpNo"));
            dtMain.Columns.Add(new DataColumn("EPFNo"));
            dtMain.Columns.Add(new DataColumn("EmpName"));
            dtMain.Columns.Add(new DataColumn("Amount"));
            dtMain.Columns.Add(new DataColumn("NIC"));
            dtMain.Columns.Add(new DataColumn("GroupName"));
            if (intPeriod == 1)
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)*22/100  AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPF12+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0) > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da.Fill(ds, "EPFETF6Months");
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "')  AND (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0) > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da1.Fill(ds1, "EPFETF6Months1");
                DataSet ds2 = new DataSet();
                SqlDataAdapter da2 = new SqlDataAdapter();
                da2.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Earnings' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount++isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE      (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da2.Fill(ds2, "EPFETF6Months2");
                DataSet ds3 = new DataSet();
                SqlDataAdapter da3= new SqlDataAdapter();
                da3.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Contribution' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPF12+ dbo.EmpMonthlyEarnings.EPF10+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)*22/100) AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE      (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPF12+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da3.Fill(ds3, "EPFETF6Months3");

                Int32 colCount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            DataRow drm = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount].ToString();
                                    drm[1] = dr.ItemArray[colCount + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount + 2].ToString();
                                    drm[3] = dr.ItemArray[colCount + 3].ToString();
                                    drm[4] = dr.ItemArray[colCount + 4].ToString();
                                    drm[5] = dr.ItemArray[colCount + 5].ToString();
                                    drm[6] = dr.ItemArray[colCount + 6].ToString();
                                    drm[7] = dr.ItemArray[colCount + 7].ToString();
                                    drm[8] = dr.ItemArray[colCount + 8].ToString();
                                    drm[9] = dr.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc1 in ds1.Tables[0].Columns)
                        {
                            DataRow drm1 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm1[0] = dr1.ItemArray[colCount].ToString();
                                    drm1[1] = dr1.ItemArray[colCount + 1].ToString();
                                    drm1[2] = dr1.ItemArray[colCount + 2].ToString();
                                    drm1[3] = dr1.ItemArray[colCount + 3].ToString();
                                    drm1[4] = dr1.ItemArray[colCount + 4].ToString();
                                    drm1[5] = dr1.ItemArray[colCount + 5].ToString();
                                    drm1[6] = dr1.ItemArray[colCount + 6].ToString();
                                    drm1[7] = dr1.ItemArray[colCount + 7].ToString();
                                    drm1[8] = dr1.ItemArray[colCount + 8].ToString();
                                    drm1[9] = dr1.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm1);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc2 in ds2.Tables[0].Columns)
                        {
                            DataRow drm2 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm2[0] = dr2.ItemArray[colCount].ToString();
                                    drm2[1] = dr2.ItemArray[colCount + 1].ToString();
                                    drm2[2] = dr2.ItemArray[colCount + 2].ToString();
                                    drm2[3] = dr2.ItemArray[colCount + 3].ToString();
                                    drm2[4] = dr2.ItemArray[colCount + 4].ToString();
                                    drm2[5] = dr2.ItemArray[colCount + 5].ToString();
                                    drm2[6] = dr2.ItemArray[colCount + 6].ToString();
                                    drm2[7] = dr2.ItemArray[colCount + 7].ToString();
                                    drm2[8] = dr2.ItemArray[colCount + 8].ToString();
                                    drm2[9] = dr2.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm2);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc3 in ds3.Tables[0].Columns)
                        {
                            DataRow drm3 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm3[0] = dr3.ItemArray[colCount].ToString();
                                    drm3[1] = dr3.ItemArray[colCount + 1].ToString();
                                    drm3[2] = dr3.ItemArray[colCount + 2].ToString();
                                    drm3[3] = dr3.ItemArray[colCount + 3].ToString();
                                    drm3[4] = dr3.ItemArray[colCount + 4].ToString();
                                    drm3[5] = dr3.ItemArray[colCount + 5].ToString();
                                    drm3[6] = dr3.ItemArray[colCount + 6].ToString();
                                    drm3[7] = dr3.ItemArray[colCount + 7].ToString();
                                    drm3[8] = dr3.ItemArray[colCount + 8].ToString();
                                    drm3[9] = dr3.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm3);
                                }
                            }
                            colCount++;
                        }
                    }
                }
                dsData.Tables.Add(dtMain);
                return dsData;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.EPF12+dbo.EmpMonthlyEarnings.EPF10 AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPF12 > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da.Fill(ds, "EPFETF6Months");
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.EPFPaybleAmount AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "')  AND (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPFPaybleAmount > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da1.Fill(ds1, "EPFETF6Months1");
                DataSet ds2 = new DataSet();
                SqlDataAdapter da2 = new SqlDataAdapter();
                da2.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Earnings' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da2.Fill(ds2, "EPFETF6Months2");
                DataSet ds3 = new DataSet();
                SqlDataAdapter da3 = new SqlDataAdapter();
                da3.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Contribution' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPF10+dbo.EmpMonthlyEarnings.EPF12) AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPF12) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da3.Fill(ds3, "EPFETF6Months3");

                Int32 colCount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            DataRow drm = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount].ToString();
                                    drm[1] = dr.ItemArray[colCount + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount + 2].ToString();
                                    drm[3] = dr.ItemArray[colCount + 3].ToString();
                                    drm[4] = dr.ItemArray[colCount + 4].ToString();
                                    drm[5] = dr.ItemArray[colCount + 5].ToString();
                                    drm[6] = dr.ItemArray[colCount + 6].ToString();
                                    drm[7] = dr.ItemArray[colCount + 7].ToString();
                                    drm[8] = dr.ItemArray[colCount + 8].ToString();
                                    drm[9] = dr.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc1 in ds1.Tables[0].Columns)
                        {
                            DataRow drm1 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm1[0] = dr1.ItemArray[colCount].ToString();
                                    drm1[1] = dr1.ItemArray[colCount + 1].ToString();
                                    drm1[2] = dr1.ItemArray[colCount + 2].ToString();
                                    drm1[3] = dr1.ItemArray[colCount + 3].ToString();
                                    drm1[4] = dr1.ItemArray[colCount + 4].ToString();
                                    drm1[5] = dr1.ItemArray[colCount + 5].ToString();
                                    drm1[6] = dr1.ItemArray[colCount + 6].ToString();
                                    drm1[7] = dr1.ItemArray[colCount + 7].ToString();
                                    drm1[8] = dr1.ItemArray[colCount + 8].ToString();
                                    drm1[9] = dr1.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm1);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc2 in ds2.Tables[0].Columns)
                        {
                            DataRow drm2 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm2[0] = dr2.ItemArray[colCount].ToString();
                                    drm2[1] = dr2.ItemArray[colCount + 1].ToString();
                                    drm2[2] = dr2.ItemArray[colCount + 2].ToString();
                                    drm2[3] = dr2.ItemArray[colCount + 3].ToString();
                                    drm2[4] = dr2.ItemArray[colCount + 4].ToString();
                                    drm2[5] = dr2.ItemArray[colCount + 5].ToString();
                                    drm2[6] = dr2.ItemArray[colCount + 6].ToString();
                                    drm2[7] = dr2.ItemArray[colCount + 7].ToString();
                                    drm2[8] = dr2.ItemArray[colCount + 8].ToString();
                                    drm2[9] = dr2.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm2);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc3 in ds3.Tables[0].Columns)
                        {
                            DataRow drm3 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm3[0] = dr3.ItemArray[colCount].ToString();
                                    drm3[1] = dr3.ItemArray[colCount + 1].ToString();
                                    drm3[2] = dr3.ItemArray[colCount + 2].ToString();
                                    drm3[3] = dr3.ItemArray[colCount + 3].ToString();
                                    drm3[4] = dr3.ItemArray[colCount + 4].ToString();
                                    drm3[5] = dr3.ItemArray[colCount + 5].ToString();
                                    drm3[6] = dr3.ItemArray[colCount + 6].ToString();
                                    drm3[7] = dr3.ItemArray[colCount + 7].ToString();
                                    drm3[8] = dr3.ItemArray[colCount + 8].ToString();
                                    drm3[9] = dr3.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm3);
                                }
                            }
                            colCount++;
                        }
                    }
                }
                dsData.Tables.Add(dtMain);
                return dsData;
            }
        }

        public DataSet getEPFOneMonthReportData(Int32 intYear, Int32 intMonth, Int32 intAll, String strDiv)
        {
            DataSet dsData = new DataSet();
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("DivisionId"));
            dtMain.Columns.Add(new DataColumn("Year"));
            dtMain.Columns.Add(new DataColumn("Month"));
            dtMain.Columns.Add(new DataColumn("MID"));
            dtMain.Columns.Add(new DataColumn("EmpNo"));
            dtMain.Columns.Add(new DataColumn("EPFNo"));
            dtMain.Columns.Add(new DataColumn("EmpName"));
            dtMain.Columns.Add(new DataColumn("Amount"));
            dtMain.Columns.Add(new DataColumn("NIC"));
            dtMain.Columns.Add(new DataColumn("GroupName"));
            
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmpMonthlyEarnings.EPF12 + dbo.EmpMonthlyEarnings.EPF10  AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month ='"+intMonth+"') AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPF12 > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da.Fill(ds, "EPFETF6Months");
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.EPFPaybleAmount AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month ='"+intMonth+"') AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "')  AND (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPFPaybleAmount > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da1.Fill(ds1, "EPFETF6Months1");
                //DataSet ds2 = new DataSet();
                //SqlDataAdapter da2 = new SqlDataAdapter();
                //da2.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Earnings' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month ='"+intMonth+"') AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE      (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                //da2.Fill(ds2, "EPFETF6Months2");
                //DataSet ds3 = new DataSet();
                //SqlDataAdapter da3 = new SqlDataAdapter();
                //da3.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Contribution' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPF12+ dbo.EmpMonthlyEarnings.EPF10) AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month ='"+intMonth+"') AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE      (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPF12) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                //da3.Fill(ds3, "EPFETF6Months3");

                Int32 colCount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            DataRow drm = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount].ToString();
                                    drm[1] = dr.ItemArray[colCount + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount + 2].ToString();
                                    drm[3] = dr.ItemArray[colCount + 3].ToString();
                                    drm[4] = dr.ItemArray[colCount + 4].ToString();
                                    drm[5] = dr.ItemArray[colCount + 5].ToString();
                                    drm[6] = dr.ItemArray[colCount + 6].ToString();
                                    drm[7] = dr.ItemArray[colCount + 7].ToString();
                                    drm[8] = dr.ItemArray[colCount + 8].ToString();
                                    drm[9] = dr.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc1 in ds1.Tables[0].Columns)
                        {
                            DataRow drm1 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm1[0] = dr1.ItemArray[colCount].ToString();
                                    drm1[1] = dr1.ItemArray[colCount + 1].ToString();
                                    drm1[2] = dr1.ItemArray[colCount + 2].ToString();
                                    drm1[3] = dr1.ItemArray[colCount + 3].ToString();
                                    drm1[4] = dr1.ItemArray[colCount + 4].ToString();
                                    drm1[5] = dr1.ItemArray[colCount + 5].ToString();
                                    drm1[6] = dr1.ItemArray[colCount + 6].ToString();
                                    drm1[7] = dr1.ItemArray[colCount + 7].ToString();
                                    drm1[8] = dr1.ItemArray[colCount + 8].ToString();
                                    drm1[9] = dr1.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm1);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                //if (ds2.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                //    {
                //        colCount = 0;
                //        foreach (DataColumn dc2 in ds2.Tables[0].Columns)
                //        {
                //            DataRow drm2 = dtMain.NewRow();
                //            if (colCount <= 11)
                //            {
                //                if (colCount % 10 == 0)
                //                {
                //                    drm2[0] = dr2.ItemArray[colCount].ToString();
                //                    drm2[1] = dr2.ItemArray[colCount + 1].ToString();
                //                    drm2[2] = dr2.ItemArray[colCount + 2].ToString();
                //                    drm2[3] = dr2.ItemArray[colCount + 3].ToString();
                //                    drm2[4] = dr2.ItemArray[colCount + 4].ToString();
                //                    drm2[5] = dr2.ItemArray[colCount + 5].ToString();
                //                    drm2[6] = dr2.ItemArray[colCount + 6].ToString();
                //                    drm2[7] = dr2.ItemArray[colCount + 7].ToString();
                //                    drm2[8] = dr2.ItemArray[colCount + 8].ToString();
                //                    drm2[9] = dr2.ItemArray[colCount + 9].ToString();
                //                    dtMain.Rows.Add(drm2);
                //                }
                //            }
                //            colCount++;
                //        }
                //    }
                //}

                //if (ds3.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                //    {
                //        colCount = 0;
                //        foreach (DataColumn dc3 in ds3.Tables[0].Columns)
                //        {
                //            DataRow drm3 = dtMain.NewRow();
                //            if (colCount <= 11)
                //            {
                //                if (colCount % 10 == 0)
                //                {
                //                    drm3[0] = dr3.ItemArray[colCount].ToString();
                //                    drm3[1] = dr3.ItemArray[colCount + 1].ToString();
                //                    drm3[2] = dr3.ItemArray[colCount + 2].ToString();
                //                    drm3[3] = dr3.ItemArray[colCount + 3].ToString();
                //                    drm3[4] = dr3.ItemArray[colCount + 4].ToString();
                //                    drm3[5] = dr3.ItemArray[colCount + 5].ToString();
                //                    drm3[6] = dr3.ItemArray[colCount + 6].ToString();
                //                    drm3[7] = dr3.ItemArray[colCount + 7].ToString();
                //                    drm3[8] = dr3.ItemArray[colCount + 8].ToString();
                //                    drm3[9] = dr3.ItemArray[colCount + 9].ToString();
                //                    dtMain.Rows.Add(drm3);
                //                }
                //            }
                //            colCount++;
                //        }
                //    }
                //}
                dsData.Tables.Add(dtMain);
                return dsData;
            
        }

        public DataSet getETF6MonthReportData(Int32 intYear, Int32 intPeriod, Int32 intAll, String strDiv)
        {
            DataSet dsData = new DataSet();
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add(new DataColumn("DivisionId"));
            dtMain.Columns.Add(new DataColumn("Year"));
            dtMain.Columns.Add(new DataColumn("Month"));
            dtMain.Columns.Add(new DataColumn("MID"));
            dtMain.Columns.Add(new DataColumn("EmpNo"));
            dtMain.Columns.Add(new DataColumn("EPFNo"));
            dtMain.Columns.Add(new DataColumn("EmpName"));
            dtMain.Columns.Add(new DataColumn("Amount"));
            dtMain.Columns.Add(new DataColumn("NIC"));
            dtMain.Columns.Add(new DataColumn("GroupName"));
            if (intPeriod == 1)
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.ETF3+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)*3/100 AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPF12+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)*3/100 > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da.Fill(ds, "EPFETF6Months");
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "')  AND (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0) > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da1.Fill(ds1, "EPFETF6Months1");
                DataSet ds2 = new DataSet();
                SqlDataAdapter da2 = new SqlDataAdapter();
                da2.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Earnings' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE      (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE  '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da2.Fill(ds2, "EPFETF6Months2");
                DataSet ds3 = new DataSet();
                SqlDataAdapter da3 = new SqlDataAdapter();
                da3.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Contribution' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.ETF3+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)*3/100) AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 1 AND 6) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE      (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE  '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPF12+isnull(dbo.EmpMonthlyEarnings.OtherEPFPay,0)*3/100) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da3.Fill(ds3, "EPFETF6Months3");

                Int32 colCount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            DataRow drm = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount].ToString();
                                    drm[1] = dr.ItemArray[colCount + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount + 2].ToString();
                                    drm[3] = dr.ItemArray[colCount + 3].ToString();
                                    drm[4] = dr.ItemArray[colCount + 4].ToString();
                                    drm[5] = dr.ItemArray[colCount + 5].ToString();
                                    drm[6] = dr.ItemArray[colCount + 6].ToString();
                                    drm[7] = dr.ItemArray[colCount + 7].ToString();
                                    drm[8] = dr.ItemArray[colCount + 8].ToString();
                                    drm[9] = dr.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc1 in ds1.Tables[0].Columns)
                        {
                            DataRow drm1 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm1[0] = dr1.ItemArray[colCount].ToString();
                                    drm1[1] = dr1.ItemArray[colCount + 1].ToString();
                                    drm1[2] = dr1.ItemArray[colCount + 2].ToString();
                                    drm1[3] = dr1.ItemArray[colCount + 3].ToString();
                                    drm1[4] = dr1.ItemArray[colCount + 4].ToString();
                                    drm1[5] = dr1.ItemArray[colCount + 5].ToString();
                                    drm1[6] = dr1.ItemArray[colCount + 6].ToString();
                                    drm1[7] = dr1.ItemArray[colCount + 7].ToString();
                                    drm1[8] = dr1.ItemArray[colCount + 8].ToString();
                                    drm1[9] = dr1.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm1);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc2 in ds2.Tables[0].Columns)
                        {
                            DataRow drm2 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm2[0] = dr2.ItemArray[colCount].ToString();
                                    drm2[1] = dr2.ItemArray[colCount + 1].ToString();
                                    drm2[2] = dr2.ItemArray[colCount + 2].ToString();
                                    drm2[3] = dr2.ItemArray[colCount + 3].ToString();
                                    drm2[4] = dr2.ItemArray[colCount + 4].ToString();
                                    drm2[5] = dr2.ItemArray[colCount + 5].ToString();
                                    drm2[6] = dr2.ItemArray[colCount + 6].ToString();
                                    drm2[7] = dr2.ItemArray[colCount + 7].ToString();
                                    drm2[8] = dr2.ItemArray[colCount + 8].ToString();
                                    drm2[9] = dr2.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm2);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc3 in ds3.Tables[0].Columns)
                        {
                            DataRow drm3 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm3[0] = dr3.ItemArray[colCount].ToString();
                                    drm3[1] = dr3.ItemArray[colCount + 1].ToString();
                                    drm3[2] = dr3.ItemArray[colCount + 2].ToString();
                                    drm3[3] = dr3.ItemArray[colCount + 3].ToString();
                                    drm3[4] = dr3.ItemArray[colCount + 4].ToString();
                                    drm3[5] = dr3.ItemArray[colCount + 5].ToString();
                                    drm3[6] = dr3.ItemArray[colCount + 6].ToString();
                                    drm3[7] = dr3.ItemArray[colCount + 7].ToString();
                                    drm3[8] = dr3.ItemArray[colCount + 8].ToString();
                                    drm3[9] = dr3.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm3);
                                }
                            }
                            colCount++;
                        }
                    }
                }
                dsData.Tables.Add(dtMain);
                return dsData;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.ETF3 AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND  (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPF12 > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da.Fill(ds, "EPFETF6Months");
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.CHKMonths.Month, dbo.CHKMonths.MId, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.EPFPaybleAmount AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKMonths.Year = '" + intYear + "') AND (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "')  AND (dbo.EmployeeMaster.EmpCategory IN (SELECT CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.EPFPaybleAmount > 0) ORDER BY dbo.CHKMonths.Year, dbo.CHKMonths.MId", CommandType.Text);
                da1.Fill(ds1, "EPFETF6Months1");
                DataSet ds2 = new DataSet();
                SqlDataAdapter da2 = new SqlDataAdapter();
                da2.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Earnings' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) AS Amount, dbo.EmployeeMaster.NICNo,  'Earnings' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPFPaybleAmount) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da2.Fill(ds2, "EPFETF6Months2");
                DataSet ds3 = new DataSet();
                SqlDataAdapter da3 = new SqlDataAdapter();
                da3.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, 'Tot Contribution' AS Month, 0 AS MID, dbo.EmpMonthlyEarnings.EmpNO,  dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, SUM(dbo.EmpMonthlyEarnings.ETF3) AS Amount, dbo.EmployeeMaster.NICNo,  'Contribution' AS GroupName FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.CHKMonths ON dbo.EmpMonthlyEarnings.Year = dbo.CHKMonths.Year AND dbo.EmpMonthlyEarnings.Month = dbo.CHKMonths.MId INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID AND  dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo WHERE     (dbo.EmpMonthlyEarnings.Month BETWEEN 7 AND 12) AND (dbo.EmployeeMaster.EmpCategory IN (SELECT     CategoryID FROM dbo.EmployeeCategory WHERE (CategoryShortName in ('RG','C')))) AND (dbo.EmpMonthlyEarnings.DivisionId LIKE '" + strDiv + "') AND (dbo.CHKMonths.Year = '" + intYear + "') GROUP BY dbo.EmpMonthlyEarnings.DivisionId, dbo.CHKMonths.Year, dbo.EmpMonthlyEarnings.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.NICNo HAVING      (SUM(dbo.EmpMonthlyEarnings.EPF12) > 0) ORDER BY dbo.CHKMonths.Year", CommandType.Text);
                da3.Fill(ds3, "EPFETF6Months3");

                Int32 colCount = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            DataRow drm = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm[0] = dr.ItemArray[colCount].ToString();
                                    drm[1] = dr.ItemArray[colCount + 1].ToString();
                                    drm[2] = dr.ItemArray[colCount + 2].ToString();
                                    drm[3] = dr.ItemArray[colCount + 3].ToString();
                                    drm[4] = dr.ItemArray[colCount + 4].ToString();
                                    drm[5] = dr.ItemArray[colCount + 5].ToString();
                                    drm[6] = dr.ItemArray[colCount + 6].ToString();
                                    drm[7] = dr.ItemArray[colCount + 7].ToString();
                                    drm[8] = dr.ItemArray[colCount + 8].ToString();
                                    drm[9] = dr.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc1 in ds1.Tables[0].Columns)
                        {
                            DataRow drm1 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm1[0] = dr1.ItemArray[colCount].ToString();
                                    drm1[1] = dr1.ItemArray[colCount + 1].ToString();
                                    drm1[2] = dr1.ItemArray[colCount + 2].ToString();
                                    drm1[3] = dr1.ItemArray[colCount + 3].ToString();
                                    drm1[4] = dr1.ItemArray[colCount + 4].ToString();
                                    drm1[5] = dr1.ItemArray[colCount + 5].ToString();
                                    drm1[6] = dr1.ItemArray[colCount + 6].ToString();
                                    drm1[7] = dr1.ItemArray[colCount + 7].ToString();
                                    drm1[8] = dr1.ItemArray[colCount + 8].ToString();
                                    drm1[9] = dr1.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm1);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc2 in ds2.Tables[0].Columns)
                        {
                            DataRow drm2 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm2[0] = dr2.ItemArray[colCount].ToString();
                                    drm2[1] = dr2.ItemArray[colCount + 1].ToString();
                                    drm2[2] = dr2.ItemArray[colCount + 2].ToString();
                                    drm2[3] = dr2.ItemArray[colCount + 3].ToString();
                                    drm2[4] = dr2.ItemArray[colCount + 4].ToString();
                                    drm2[5] = dr2.ItemArray[colCount + 5].ToString();
                                    drm2[6] = dr2.ItemArray[colCount + 6].ToString();
                                    drm2[7] = dr2.ItemArray[colCount + 7].ToString();
                                    drm2[8] = dr2.ItemArray[colCount + 8].ToString();
                                    drm2[9] = dr2.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm2);
                                }
                            }
                            colCount++;
                        }
                    }
                }

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        colCount = 0;
                        foreach (DataColumn dc3 in ds3.Tables[0].Columns)
                        {
                            DataRow drm3 = dtMain.NewRow();
                            if (colCount <= 11)
                            {
                                if (colCount % 10 == 0)
                                {
                                    drm3[0] = dr3.ItemArray[colCount].ToString();
                                    drm3[1] = dr3.ItemArray[colCount + 1].ToString();
                                    drm3[2] = dr3.ItemArray[colCount + 2].ToString();
                                    drm3[3] = dr3.ItemArray[colCount + 3].ToString();
                                    drm3[4] = dr3.ItemArray[colCount + 4].ToString();
                                    drm3[5] = dr3.ItemArray[colCount + 5].ToString();
                                    drm3[6] = dr3.ItemArray[colCount + 6].ToString();
                                    drm3[7] = dr3.ItemArray[colCount + 7].ToString();
                                    drm3[8] = dr3.ItemArray[colCount + 8].ToString();
                                    drm3[9] = dr3.ItemArray[colCount + 9].ToString();
                                    dtMain.Rows.Add(drm3);
                                }
                            }
                            colCount++;
                        }
                    }
                }
                dsData.Tables.Add(dtMain);
                return dsData;
            }
        }

        public DataSet GetEmpWorkAttandance(String StrDivisionID, Int32 IntYear, Int32 IntMonth, Int32 IntWrkType)
        {
            DataSet ds = new DataSet();

            if (IntWrkType == 1)
            {
                ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (dbo.EstateDivision.DivisionID = '" + StrDivisionID + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND (dbo.DailyGroundTransactions.WorkType = '" + IntWrkType + "')  GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }
            else
            {
                ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (dbo.EstateDivision.DivisionID = '" + StrDivisionID + "') AND (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND (dbo.DailyGroundTransactions.WorkType = '" + IntWrkType + "')  GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }
            return ds;
        }
        public DataSet GetEmpWorkAttandance(Int32 IntYear, Int32 IntMonth, Int32 IntWrkType)
        {
            DataSet ds = new DataSet();
            if (IntWrkType == 1)
            {
                ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND (dbo.DailyGroundTransactions.WorkType = '" + IntWrkType + "')  GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }
            else
            {
                ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND (dbo.DailyGroundTransactions.WorkType = '" + IntWrkType + "')  GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }
            //if (IntWrkType == 1)
            //{
            //    ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND (dbo.DailyGroundTransactions.WorkType = '" + IntWrkType + "') AND (dbo.EmployeeMaster.ActiveEmployee = 1) GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            //}
            //else
            //{
            //    ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND (dbo.DailyGroundTransactions.WorkType = '" + IntWrkType + "') AND (dbo.EmployeeMaster.ActiveEmployee = 1) GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            //}
            return ds;
        }

        public DataSet GetEmpWorkDetailManDaysWorkCode(Int32 IntYear, Int32 IntMonth, String IntWrkType,String strDiv)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, dbo.DailyGroundTransactions.WorkCodeID,  CASE WHEN (dbo.DailyGroundTransactions.WorkType = 1) THEN SUM(dbo.DailyGroundTransactions.ManDays)  ELSE SUM(dbo.DailyGroundTransactions.CashManDays) END AS CashOrNormalMandays FROM         dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + IntYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + IntMonth + "') AND  (dbo.EstateDivision.DivisionID like '" + strDiv + "') AND  (dbo.DailyGroundTransactions.WorkType like '" + IntWrkType + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.WorkType,  dbo.DailyGroundTransactions.WorkCodeID ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            
            return ds;
        }

        public DataSet getConfirmationData( DateTime dtFrom,DateTime dtTo)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT DateEntered, SUM(CASE WHEN (WorkType = 1) AND (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PlkManDays,  SUM(CASE WHEN (WorkType = 1) AND (WorkCodeID <> 'PLK') THEN ManDays ELSE 0 END) AS SundryManDays, SUM(CASE WHEN (WorkType = 1)  THEN WorkQty ELSE 0 END) AS NormalKilos, SUM(CASE WHEN (WorkType = 2) THEN WorkQty ELSE 0 END) AS CashWorkKilos, SUM(CASE WHEN (WorkType = 2)  THEN CashManDays ELSE 0 END) AS CashMandays, SUM(FieldWeight) AS FieldWeight, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN AreaCovered ELSE 0 END)  AS PlkAreaCovered, SUM(CASE WHEN (WorkCodeID <> 'PLK') THEN AreaCovered ELSE 0 END) AS SundryAreaCovered FROM dbo.DailyGroundTransactions WHERE  (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) GROUP BY EstateID, DateEntered ORDER BY DateEntered", CommandType.Text);
            da.Fill(ds, "confirmData");
            return ds;
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("DateEntered"));
            //dt.Columns.Add(new DataColumn("PlkManDays"));
            //dt.Columns.Add(new DataColumn("SundryManDays"));
            //dt.Columns.Add(new DataColumn("NormKilos"));
            //dt.Columns.Add(new DataColumn("CashWorkKilos"));
            //dt.Columns.Add(new DataColumn("CashManDays"));
            //dt.Columns.Add(new DataColumn("FieldWeight"));
            //dt.Columns.Add(new DataColumn("PlkAreaCovered"));
            //dt.Columns.Add(new DataColumn("SundryAreaCovered"));

            //SqlDataReader reader;
            //DataRow dtRow;

            //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT DateEntered, SUM(CASE WHEN (WorkType = 1) AND (WorkCodeID = 'PLK') THEN ManDays ELSE 0 END) AS PlkManDays,  SUM(CASE WHEN (WorkType = 1) AND (WorkCodeID <> 'PLK') THEN ManDays ELSE 0 END) AS SundryManDays, SUM(CASE WHEN (WorkType = 1)  THEN WorkQty ELSE 0 END) AS NormalKilos, SUM(CASE WHEN (WorkType = 2) THEN WorkQty ELSE 0 END) AS CashWorkKilos, SUM(CASE WHEN (WorkType = 2)  THEN CashManDays ELSE 0 END) AS CashMandays, SUM(FieldWeight) AS FieldWeight, SUM(CASE WHEN (WorkCodeID = 'PLK') THEN AreaCovered ELSE 0 END)  AS PlkAreaCovered, SUM(CASE WHEN (WorkCodeID <> 'PLK') THEN AreaCovered ELSE 0 END) AS SundryAreaCovered FROM dbo.DailyGroundTransactions WHERE  (DateEntered BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) GROUP BY EstateID, DateEntered ORDER BY DateEntered ", CommandType.Text);

            //while (reader.Read())
            //{
            //    dtRow = dt.NewRow();

            //    if (!reader.IsDBNull(0))
            //    {
            //        dtRow[0] = reader.GetDateTime(0);
            //    }
            //    if (!reader.IsDBNull(1))
            //    {
            //        dtRow[1] = reader.GetDecimal(1);
            //    }
            //    if (!reader.IsDBNull(2))
            //    {
            //        dtRow[2] = reader.GetDecimal(2);
            //    }
            //    if (!reader.IsDBNull(3))
            //    {
            //        dtRow[3] = reader.GetDecimal(3);
            //    }
            //    if (!reader.IsDBNull(4))
            //    {
            //        dtRow[4] = reader.GetDecimal(4);
            //    }
            //    if (!reader.IsDBNull(5))
            //    {
            //        dtRow[5] = reader.GetDecimal(5);
            //    }
            //    if (!reader.IsDBNull(6))
            //    {
            //        dtRow[6] = reader.GetDecimal(6);
            //    }
            //    if (!reader.IsDBNull(7))
            //    {
            //        dtRow[7] = reader.GetDecimal(7);
            //    }
            //    if (!reader.IsDBNull(8))
            //    {
            //        dtRow[8] = reader.GetDecimal(8);
            //    }

            //    dt.Rows.Add(dtRow);
            //}
            //reader.Dispose();
            //return dt;
        }

        //public DataSet getLeafAndManDays(int intYear, int intMonth, String strDiv)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EmpMonthlyEarnings.EmpNO, dbo.EmpMonthlyEarnings.Sex, dbo.EmpMonthlyEarnings.Category, dbo.EmpMonthlyEarnings.Month, dbo.EmpMonthlyEarnings.Year, dbo.EmpMonthlyEarnings.ExtraRates, dbo.EmpMonthlyEarnings.OverKilos, dbo.EmpMonthlyEarnings.CashPlucking, dbo.EmpMonthlyEarnings.CashSundry, dbo.EmpMonthlyEarnings.PRIAmount, dbo.EmpMonthlyEarnings.AttIncentive, dbo.EmpMonthlyEarnings.PluckingManDays, dbo.EmpMonthlyEarnings.SundryManDays, dbo.EmpMonthlyEarnings.HolidaySundryManDays, dbo.EmpMonthlyEarnings.PluckingNamePay,  dbo.EmpMonthlyEarnings.SundryNamePay, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.HolidayPLKManDays, dbo.EmpMonthlyEarnings.PluckingKilos, case when (dbo.EmpMonthlyEarnings.QualifyDays=0) then dbo.EmpMonthlyEarnings.WorkedPercentage else dbo.EmpMonthlyEarnings.QualifyDays end as WorkedPercentage, dbo.EmpMonthlyEarnings.OverKilosPay, dbo.EmpMonthlyEarnings.PaidHolidays, dbo.EmpMonthlyEarnings.OtherEPFAdditions, dbo.EmpMonthlyEarnings.EPFPaybleAmount AS TotalPayEPF, dbo.EmpMonthlyEarnings.OverTime, dbo.EstateDivision.DivisionName, dbo.EmpMonthlyEarnings.TotalEarnings, dbo.EmpMonthlyEarnings.PreviousMadeUpCoins, dbo.EmpMonthlyEarnings.OtherAdditions, dbo.EmpMonthlyEarnings.HolidayHalfNames FROM dbo.EmpMonthlyEarnings INNER JOIN dbo.EmployeeMaster ON dbo.EmpMonthlyEarnings.EmpNO = dbo.EmployeeMaster.EmpNo AND  dbo.EmpMonthlyEarnings.DivisionId = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID WHERE     (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.Year = '" + intYear + "') AND (CONVERT(VARCHAR(10),Category) LIKE '" + intCat + "') ORDER BY dbo.EmpMonthlyEarnings.Category,convert(int, dbo.EmpMonthlyEarnings.EmpNO)", CommandType.Text);
        //    da.Fill(ds, "PaymentCheckRoll");
        //    return ds;
        //}

        public DataSet GetFixedDeductionRegisterByEmpRange(Int32 intYear, Int32 intMonth, String strDiv, Int32 intDGroup, Int32 intDdeduct, String StrEmpFrom, String StrEmpTo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("GroupCode"));
            dt.Columns.Add(new DataColumn("DeductCode"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("NoOfMonths"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate"));
            dt.Columns.Add(new DataColumn("BalanceAmount"));
            dt.Columns.Add(new DataColumn("StartYear"));
            dt.Columns.Add(new DataColumn("StartMonth"));
            SqlDataReader reader;
            DataRow dtRow;
            //reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.UserId, dbo.CHKFixedDeductions.CreateDateTime, dbo.CHKFixedDeductions.BalanceAmount FROM  dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN  dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode   WHERE (dbo.CHKFixedDeductions.StartYear = '" + intYear + "') AND (dbo.CHKFixedDeductions.StartMonth = '" + intMonth + "') AND (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDdeduct + "') AND (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.EmpNo BETWEEN '" + StrEmpFrom + "' AND '" + StrEmpTo + "') ORDER BY dbo.CHKFixedDeductions.FixedDeductionId", CommandType.Text);
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKFixedDeductions.EmpNo, dbo.CHKDeductionGroup.ShortName AS GroupShortName, dbo.CHKDeduction.ShortName, dbo.CHKFixedDeductions.DeductAmount, dbo.CHKFixedDeductions.NoOfMonths, dbo.CHKFixedDeductions.UserId, dbo.CHKFixedDeductions.CreateDateTime, dbo.CHKFixedDeductions.BalanceAmount, dbo.CHKFixedDeductions.StartYear, dbo.CHKFixedDeductions.StartMonth FROM  dbo.CHKDeductionGroup INNER JOIN dbo.CHKFixedDeductions ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKFixedDeductions.DeductionGroupId INNER JOIN  dbo.CHKDeduction ON dbo.CHKFixedDeductions.DeductionId = dbo.CHKDeduction.DeductionCode   WHERE (dbo.CHKFixedDeductions.DeductionGroupId = '" + intDGroup + "') AND (dbo.CHKFixedDeductions.DeductionId = '" + intDdeduct + "') AND (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "') AND (dbo.CHKFixedDeductions.EmpNo BETWEEN '" + StrEmpFrom + "' AND '" + StrEmpTo + "')  AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.CloseYesNo = 0) ORDER BY dbo.CHKFixedDeductions.FixedDeductionId", CommandType.Text);
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
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetInt32(4);
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDateTime(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetInt32(8);
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetInt32(9);
                }
                dt.Rows.Add(dtRow);
            }
            reader.Dispose();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet getRFTDeductionsRegisterByEmpRange(Int32 intYear, Int32 intMonth, String strDiv, Int32 intDGroup, Int32 intDdeduct, String empNoFrom, String empNoTo)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.CHKRFTDeductions.RFTDeductId, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKRFTDeductions.DeductGroupId, dbo.CHKDeductionGroup.ShortName, dbo.CHKRFTDeductions.DeductId, dbo.CHKDeduction.ShortName AS Expr1,  dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.RFTDeductAmount, dbo.CHKRFTDeductions.UserId, dbo.CHKRFTDeductions.CreateDateTime, dbo.CHKRFTDeductions.RFTRate, dbo.CHKRFTDeductions.RFTQty FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN   dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE (dbo.CHKRFTDeductions.DYear = '" + intYear + "') AND (dbo.CHKRFTDeductions.DMonth = '" + intMonth + "') AND (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKDeductionGroup.DeductionGroupCode = '" + intDGroup + "') AND (dbo.CHKDeduction.DeductionCode = '" + intDdeduct + "') AND (dbo.CHKRFTDeductions.EmpNo BETWEEN '" + empNoFrom + "' AND '" + empNoTo + "') ORDER BY dbo.CHKRFTDeductions.RFTDeductId ", CommandType.Text);
            da.Fill(ds, "RFTDeductRegister");
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.CHKRFTDeductions.RFTDeductId, dbo.CHKRFTDeductions.Division, dbo.CHKRFTDeductions.DYear, dbo.CHKRFTDeductions.DMonth, dbo.CHKRFTDeductions.DeductGroupId, dbo.CHKDeductionGroup.ShortName, dbo.CHKRFTDeductions.DeductId, dbo.CHKDeduction.ShortName AS Expr1, " +
            //          " dbo.CHKRFTDeductions.EmpNo, dbo.CHKRFTDeductions.RFTDeductAmount, dbo.CHKRFTDeductions.UserId, dbo.CHKRFTDeductions.CreateDateTime, dbo.CHKRFTDeductions.RFTRate, dbo.CHKRFTDeductions.RFTQty FROM dbo.CHKRFTDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKRFTDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN " +
            //          " dbo.CHKDeduction ON dbo.CHKRFTDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE  (dbo.CHKRFTDeductions.Division = '" + strDiv + "') AND (dbo.CHKDeductionGroup.DeductionGroupCode = '" + intDGroup + "') AND (dbo.CHKDeduction.DeductionCode = '" + intDdeduct + "') AND (dbo.CHKRFTDeductions.EmpNo BETWEEN '" + empNoFrom + "' AND '" + empNoTo + "') ORDER BY dbo.CHKRFTDeductions.RFTDeductId ", CommandType.Text);
            //da.Fill(ds, "RFTDeductRegister");
            return ds;
        }

        public DataSet GetLoanDeductionRegisterByEmpRange(String strDiv, Int32 intYear, Int32 intMonth, Int32 intGroup, Int32 intDeduct, String empFrom, String empTo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Group");
            dt.Columns.Add("DeductCode");
            dt.Columns.Add("InstallmentAmount");
            dt.Columns.Add("StartYear");
            dt.Columns.Add("StartMonth");
            dt.Columns.Add("NoofInstallments");
            dt.Columns.Add("Principalamount");
            dt.Columns.Add("AccountNo");
            dt.Columns.Add("LoanName");
            DataRow drow;
            drow = dt.NewRow();
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT DeductionGroup, DeductionCode, CategoryCode, DivisionCode, EmployeeNo, LoanName, Principalamount, NoofInstallments, InstallmentAmount,AccountNo, LoanDate,LoanCode FROM CHKLoan", CommandType.Text);
            //dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount, " +
            //        "  dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode " +
            //        " WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "') AND (StartYear='" + intYear + "')  AND (StartMonth='" + intMonth + "') AND (dbo.CHKLoan.EmployeeNo BETWEEN '" + empFrom + "' AND '" + empTo + "') ", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKLoan.EmployeeNo, dbo.CHKDeductionGroup.ShortName, dbo.CHKDeduction.ShortName AS DeductShortName, dbo.CHKLoan.InstallmentAmount, dbo.CHKLoan.StartYear, dbo.CHKLoan.StartMonth, dbo.CHKLoan.NoofInstallments, dbo.CHKLoan.Principalamount, dbo.CHKLoan.AccountNo, dbo.CHKLoan.LoanName FROM dbo.CHKLoan INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKLoan.DeductionGroup = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKLoan.DeductionCode = dbo.CHKDeduction.DeductionCode  WHERE     (dbo.CHKLoan.DivisionCode = '" + strDiv + "') AND (dbo.CHKLoan.DeductionGroup = '" + intGroup + "') AND (dbo.CHKLoan.DeductionCode = '" + intDeduct + "')  AND (dbo.CHKLoan.EmployeeNo BETWEEN '" + empFrom + "' AND '" + empTo + "') AND (dbo.CHKLoan.BalanceAmount > 0) AND (dbo.CHKLoan.ClosedYesNo = 0) ", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetString(0).Trim();
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
                    drow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    drow[4] = dataReader.GetInt32(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    drow[5] = dataReader.GetInt32(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    drow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    drow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    drow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    drow[9] = dataReader.GetString(9).Trim();
                }


                dt.Rows.Add(drow);
            }
            dataReader.Close();
            ds.Tables.Add(dt);
            return ds;
        }

        /*Individual Payment*/
        public DataTable getSalarySlipsPrePrintedByEmployee(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat,String strEmp)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("Amount");

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            String StrOtherDeductions = "";

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Earnings') AND (SalryItemName <> 'Previous Made Up Coins') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (CashWorkPayslipAvailable())
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions + PreviousMadeUpCoins AS OtherAdditions FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Tea")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        //if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        //{
                        //    dtRow[7] = 0;

                        //    if (!readerEmployeeEarnings.IsDBNull(10))
                        //        dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                        //    else
                        //        dtRow[8] = 0;
                        //}
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequence WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (CashWorkPayslipAvailable())
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')  ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";
                        if (StrOtherDeductions.Length > 42)
                        {
                            StrOtherDeductions = StrOtherDeductions.Substring(0, 40) + "\r\n" + StrOtherDeductions.Substring(41, StrOtherDeductions.Length - 41);
                        }
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        //Created by kalana.
        public DataSet getEmployeeCashPluckingDetails(DateTime dtFrom, DateTime dtTo, int WType, String division)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT EmpNo, DAY(DateEntered) AS Day, SUM(WorkQty) AS Expr1, CashPlkOkgYesNo,DivisionID FROM dbo.DailyGroundTransactions WHERE(WorkType = 2) AND (DateEntered BETWEEN '" + dtFrom + "' AND '" + dtTo + "') AND (WorkCodeID = 'PLK')AND(DivisionID like '" + division + "') GROUP BY DivisionID, EmpNo, DAY(DateEntered), CashPlkOkgYesNo ORDER BY DivisionID,EmpNo", CommandType.Text);


            da.Fill(ds, "EmpCashPLKDetails");
            return ds;
        }


        //Created by kalana.
        public DataSet getEmployeeSundryManDaysDetails(DateTime dtFrom, DateTime dtTo, int WType, String division)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT EmpNo, DAY(DateEntered) AS 'Days', sum(CashManDays) as CashManDays, DivisionID FROM dbo.DailyGroundTransactions WHERE     (WorkType = 2) AND (WorkCodeID <> 'PLK') AND (DivisionID LIKE '" + division + "') AND (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102)  AND CONVERT(DATETIME, '" + dtTo + "', 102)) GROUP BY DivisionID, EmpNo, DAY(DateEntered)", CommandType.Text);
            da.Fill(ds, "EmpCashSundryDetails");
            return ds;
        }

        //Created by gobi
        public DataSet ListDayReports1(String division, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.SQLHelper.FillDataSet("SELECT DivisionID, OverKgs, CashKgs, ManDays, CashSundryAmount, DateEntered, OverKgAmount, NormKilos, PluckingExpenditure, CashManDays FROM  dbo.DailyGroundTransactions WHERE   (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND   (DivisionID LIKE '" + division + "') ", CommandType.Text);
            return ds;
        }
        //public DataSet GetDivisionWiseSummary(String division, DateTime FromDate, DateTime ToDate)
        //{
        //    DataSet ds = new DataSet();
        //    ds = DataAccess.SQLHelper.FillDataSet("SELECT DateEntered,DivisionID, SUM(ManDays) AS ManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 1) THEN WorkQty ELSE 0 END) AS NormalKilos,  SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 1) THEN WorkQty ELSE 0 END) AS CashKilos, SUM(OverKgs) AS OverKilos, SUM(CashManDays)  AS CashManDays FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY DateEntered,DivisionID, WorkType HAVING  (DivisionID LIKE '" + division + "') ", CommandType.Text);
        //    return ds;
        //}

        public DataSet GetDivisionWiseSummary(String division, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("ManDays"));
            dt.Columns.Add(new DataColumn("NormalKilos"));
            dt.Columns.Add(new DataColumn("CashKilos"));
            dt.Columns.Add(new DataColumn("OverKilos"));
            dt.Columns.Add(new DataColumn("CashManDays"));
            dt.Columns.Add(new DataColumn("OTHours"));
            dt.Columns.Add(new DataColumn("OTAmount"));
            dt.Columns.Add(new DataColumn("Total Kilos"));
            dt.Columns.Add(new DataColumn("CropType"));
            Decimal decOvertime = 0;
            DataRow dtrow;
            SqlDataReader dataReader;
            SqlDataReader otReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DateEntered,DivisionID, SUM(ManDays) AS ManDays, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 1) THEN WorkQty ELSE 0 END) AS NormalKilos,  SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN WorkQty ELSE 0 END) AS CashKilos, SUM(OverKgs) AS OverKilos, SUM(CashManDays)  AS CashManDays, SUM(WorkQty) AS TotalKilos, CropType FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) GROUP BY DateEntered,DivisionID, WorkType, CropType HAVING  (DivisionID LIKE '" + division + "') ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetDecimal(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDecimal(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDecimal(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[9] = dataReader.GetDecimal(7);
                }
                 if (!dataReader.IsDBNull(8))
                {
                    dtrow[10] = dataReader.GetInt32(8);
                }
                otReader = SQLHelper.ExecuteReader("SELECT     SUM(isnull(Hours,0)) AS OTHours, SUM(isnull(Expenditure,0)) AS OTAmount FROM dbo.CHKOvertime WHERE (OtDate = CONVERT(DATETIME, '" + Convert.ToDateTime(dataReader.GetDateTime(0).ToShortDateString()) + "', 102)) AND (DivisionCode = '" + dataReader.GetString(1).Trim() + "')", CommandType.Text);
                while (otReader.Read())
                {
                    if (!otReader.IsDBNull(0))
                    {
                        dtrow[7] = otReader.GetDecimal(0);
                    }
                    else
                    {
                        dtrow[7] = 0;
                    }
                    if (!otReader.IsDBNull(1))
                    {
                        dtrow[8] = otReader.GetDecimal(1);
                    }
                    else
                    {
                        dtrow[8] = 0;
                    }
                }
                otReader.Close();
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet GetOvertimeDetails(String division, DateTime FromDate, DateTime ToDate,String strCrop)
        {
            DataSet ds = new DataSet();
            //ds = DataAccess.SQLHelper.FillDataSet("SELECT     OtDate, DivisionCode, EmployeeNo, SUM(Hours) AS Expr1, SUM(Expenditure) AS Expr2 FROM dbo.CHKOvertime WHERE     (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode LIKE '"+division+"') GROUP BY OtDate, DivisionCode, EmployeeNo", CommandType.Text);
            //ds = DataAccess.SQLHelper.FillDataSet("SELECT     dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, SUM(dbo.CHKOvertime.Hours) AS Expr1,  SUM(dbo.CHKOvertime.Expenditure) AS Expr2, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor FROM dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId WHERE     (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '"+division+"') GROUP BY dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor", CommandType.Text);
            //ds = DataAccess.SQLHelper.FillDataSet("SELECT dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, SUM(dbo.CHKOvertime.Hours) AS Expr1,  SUM(dbo.CHKOvertime.Expenditure) AS Expr2, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor, dbo.EmployeeMaster.EMPName FROM         dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId INNER JOIN dbo.EmployeeMaster ON dbo.CHKOvertime.DivisionCode = dbo.EmployeeMaster.DivisionID AND  dbo.CHKOvertime.EmployeeNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '"+division+"') GROUP BY dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor,  dbo.EmployeeMaster.EMPName", CommandType.Text);
            ds = DataAccess.SQLHelper.FillDataSet("SELECT dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, SUM(dbo.CHKOvertime.Hours) AS Expr1, SUM(dbo.CHKOvertime.Expenditure) AS Expr2, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor, dbo.EmployeeMaster.EMPName, dbo.CHKOvertime.CropCode, (SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CropType') AND (Code = dbo.CHKOvertime.CropCode)) AS CropName FROM dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId INNER JOIN dbo.EmployeeMaster ON dbo.CHKOvertime.DivisionCode = dbo.EmployeeMaster.DivisionID AND dbo.CHKOvertime.EmployeeNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (CONVERT(varchar(50),  dbo.CHKOvertime.CropCode) LIKE '"+strCrop+"') AND (dbo.CHKOvertime.DivisionCode LIKE '" + division + "') GROUP BY dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor, dbo.EmployeeMaster.EMPName, dbo.CHKOvertime.CropCode", CommandType.Text);
            return ds;
        }

        public DataSet GetOvertimeJobWiseDistribution(String division, DateTime FromDate, DateTime ToDate,String strCrop)
        {
            DataSet ds = new DataSet();
            //ds = DataAccess.SQLHelper.FillDataSet("SELECT     OtDate, DivisionCode, EmployeeNo, SUM(Hours) AS Expr1, SUM(Expenditure) AS Expr2 FROM dbo.CHKOvertime WHERE     (OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND (DivisionCode LIKE '"+division+"') GROUP BY OtDate, DivisionCode, EmployeeNo", CommandType.Text);
            //ds = DataAccess.SQLHelper.FillDataSet("SELECT     dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, SUM(dbo.CHKOvertime.Hours) AS Expr1,  SUM(dbo.CHKOvertime.Expenditure) AS Expr2, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor FROM dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId WHERE     (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102)) AND  (dbo.CHKOvertime.DivisionCode LIKE '"+division+"') GROUP BY dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOvertime.EmployeeNo, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor", CommandType.Text);
            ds = DataAccess.SQLHelper.FillDataSet("SELECT dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, SUM(dbo.CHKOvertime.Hours) AS Expr1, SUM(dbo.CHKOvertime.Expenditure) AS Expr2, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor, dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.CHKOvertime.CropCode, (SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CropType') AND (Code = dbo.CHKOvertime.CropCode)) AS CropName FROM dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId INNER JOIN dbo.JobMaster ON dbo.CHKOvertime.Job = dbo.JobMaster.JobShortName WHERE (dbo.CHKOvertime.OtDate BETWEEN CONVERT(DATETIME, '" + FromDate + "', 102) AND CONVERT(DATETIME, '" + ToDate + "', 102)) AND (dbo.CHKOvertime.DivisionCode LIKE '" + division + "') AND (CONVERT(varchar(50), dbo.CHKOvertime.CropCode) LIKE '" + strCrop + "') GROUP BY dbo.CHKOvertime.OtDate, dbo.CHKOvertime.DivisionCode, dbo.CHKOTParameters.OTType, dbo.CHKOTParameters.OTFactor, dbo.CHKOvertime.Job, dbo.JobMaster.JobName, dbo.CHKOvertime.CropCode", CommandType.Text);
            return ds;
        }

        public DataSet getHarvestNamePlkRegister(String strDivision, DateTime dtFrom,DateTime dtTo)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("Kilos"));
            dt.Columns.Add(new DataColumn("Norm"));
            dt.Columns.Add(new DataColumn("OverKgs"));
            dt.Columns.Add(new DataColumn("OverKgPay"));
            dt.Columns.Add(new DataColumn("NamePay")); ;
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("NamePayValue"));
            dt.Columns.Add(new DataColumn("CashNames"));

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT     DivisionID, FieldID, EmpNo, LabourType, WorkCodeID, WorkQty, FullHalf, CashKgs, NormKilos AS Norm, OverKgs, OverKgs * (SELECT     Amount AS Expr2 FROM dbo.FTSCheckRollRates WHERE (Type = 'CashKgRate')) AS OverKgPay, CashKgAmount - OverKgs * (SELECT     Amount AS Expr2 FROM dbo.FTSCheckRollRates AS FTSCheckRollRates_1 WHERE (Type = 'CashKgRate')) AS NamePay, CashKgAmount, CASE WHEN (WorkQty >= NormKilos) THEN DailyBasicAmount ELSE 0 END AS NamePayValue,CashManDays FROM dbo.DailyGroundTransactions WHERE (CashPlkOkgYesNo = 1) AND (DateEntered between CONVERT(DATETIME, '" + dtFrom + "', 102) and CONVERT(DATETIME, '" + dtTo + "', 102)) AND (DivisionID = '" + strDivision + "')", CommandType.Text);

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
                    dtRow[3] = reader.GetString(3).Trim();
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetDecimal(8);
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetDecimal(9);
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetDecimal(10);
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDecimal(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingNames"));
            dtnew.Columns.Add(new DataColumn("TotalKilos"));
            dtnew.Columns.Add(new DataColumn("OverKilos"));
            dtnew.Columns.Add(new DataColumn("OverKilosAmount"));
            dtnew.Columns.Add(new DataColumn("TotalAmount"));


            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT     SUM(WorkQty) AS Kilos, SUM(OverKgs) AS OverKilos, SUM(OverKgs) * (SELECT     Amount AS Expr2 FROM dbo.FTSCheckRollRates WHERE (Type = 'CashKgRate')) AS OverKgPay, SUM(CashManDays) AS CashNames, SUM(CashKgAmount) AS Payment FROM dbo.DailyGroundTransactions WHERE (CashPlkOkgYesNo = 1) AND (DateEntered BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) AND  (DivisionID = '"+strDivision+"') ", CommandType.Text);
           
            while (reader.Read())
            {
                if (!reader.IsDBNull(3))
                {
                    if (Convert.ToDecimal(reader.GetDecimal(3).ToString())>0)
                        dtRow1[0] = reader.GetDecimal(3);
                    else
                        dtRow1[0] = "0";
                }
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetDecimal(0) > 0)
                    {
                        dtRow1[1] = reader.GetDecimal(0);
                    }
                    else
                        dtRow1[1] = 0;
                }
                if (!reader.IsDBNull(1))
                {
                    if (reader.GetDecimal(1) > 0)
                    {
                        dtRow1[2] = reader.GetDecimal(1);
                    }
                    else
                        dtRow1[2] = 0;
                }
                if (!reader.IsDBNull(1))
                {
                    if (reader.GetDecimal(1) > 0)
                    {
                        dtRow1[2] = reader.GetDecimal(1);
                    }
                    else
                        dtRow1[2] = 0;
                }
                if (!reader.IsDBNull(2))
                {
                    if (reader.GetDecimal(2) > 0)
                    {
                        dtRow1[3] = reader.GetDecimal(2);
                    }
                    else
                        dtRow1[3] = 0;
                }
                if (!reader.IsDBNull(4))
                {
                    if (reader.GetDecimal(4) > 0)
                    {
                        dtRow1[4] = reader.GetDecimal(4);
                    }
                    else
                        dtRow1[4] = 0;
                }
                dtnew.Rows.Add(dtRow1);
            }
            reader.Dispose();

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }

        public DataSet getHarvestNamePlkRegister(String strDivision, DateTime dtFrom, DateTime dtTo,String strEmpFrom,String strEmpTo)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("Kilos"));
            dt.Columns.Add(new DataColumn("Norm"));
            dt.Columns.Add(new DataColumn("OverKgs"));
            dt.Columns.Add(new DataColumn("OverKgPay"));
            dt.Columns.Add(new DataColumn("NamePay")); ;
            dt.Columns.Add(new DataColumn("CashKgAmount"));
            dt.Columns.Add(new DataColumn("NamePayValue"));
            dt.Columns.Add(new DataColumn("CashNames"));

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT     DivisionID, FieldID, EmpNo, LabourType, WorkCodeID, WorkQty, FullHalf, CashKgs, NormKilos AS Norm, OverKgs, OverKgs * (SELECT     Amount AS Expr2 FROM dbo.FTSCheckRollRates WHERE (Type = 'CashKgRate')) AS OverKgPay, CashKgAmount - OverKgs * (SELECT     Amount AS Expr2 FROM dbo.FTSCheckRollRates AS FTSCheckRollRates_1 WHERE (Type = 'CashKgRate')) AS NamePay, CashKgAmount, CASE WHEN (WorkQty >= NormKilos) THEN DailyBasicAmount ELSE 0 END AS NamePayValue,CashManDays FROM dbo.DailyGroundTransactions WHERE (CashPlkOkgYesNo = 1) AND (DateEntered between CONVERT(DATETIME, '" + dtFrom + "', 102) and CONVERT(DATETIME, '" + dtTo + "', 102)) AND (DivisionID = '" + strDivision + "') AND (CONVERT(int, EmpNo) BETWEEN  '"+Convert.ToInt32(strEmpFrom)+"' AND '"+Convert.ToInt32(strEmpTo)+"')", CommandType.Text);

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
                    dtRow[3] = reader.GetString(3).Trim();
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetDecimal(5);
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetInt32(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetDecimal(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetDecimal(8);
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetDecimal(9);
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetDecimal(10);
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDecimal(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("PlukingNames"));
            dtnew.Columns.Add(new DataColumn("TotalKilos"));
            dtnew.Columns.Add(new DataColumn("OverKilos"));
            dtnew.Columns.Add(new DataColumn("OverKilosAmount"));
            dtnew.Columns.Add(new DataColumn("TotalAmount"));


            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT     SUM(WorkQty) AS Kilos, SUM(OverKgs) AS OverKilos, SUM(OverKgs) * (SELECT     Amount AS Expr2 FROM dbo.FTSCheckRollRates WHERE (Type = 'CashKgRate')) AS OverKgPay, SUM(CashManDays) AS CashNames, SUM(CashKgAmount) AS Payment FROM dbo.DailyGroundTransactions WHERE (CashPlkOkgYesNo = 1) AND (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND  (DivisionID = '" + strDivision + "') AND (CONVERT(int, EmpNo) BETWEEN  '" + Convert.ToInt32(strEmpFrom) + "' AND '" + Convert.ToInt32(strEmpTo) + "') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(3))
                {
                    if (Convert.ToDecimal(reader.GetDecimal(3).ToString()) > 0)
                        dtRow1[0] = reader.GetDecimal(3);
                    else
                        dtRow1[0] = "0";
                }
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetDecimal(0) > 0)
                    {
                        dtRow1[1] = reader.GetDecimal(0);
                    }
                    else
                        dtRow1[1] = 0;
                }
                if (!reader.IsDBNull(1))
                {
                    if (reader.GetDecimal(1) > 0)
                    {
                        dtRow1[2] = reader.GetDecimal(1);
                    }
                    else
                        dtRow1[2] = 0;
                }
                if (!reader.IsDBNull(1))
                {
                    if (reader.GetDecimal(1) > 0)
                    {
                        dtRow1[2] = reader.GetDecimal(1);
                    }
                    else
                        dtRow1[2] = 0;
                }
                if (!reader.IsDBNull(2))
                {
                    if (reader.GetDecimal(2) > 0)
                    {
                        dtRow1[3] = reader.GetDecimal(2);
                    }
                    else
                        dtRow1[3] = 0;
                }
                if (!reader.IsDBNull(4))
                {
                    if (reader.GetDecimal(4) > 0)
                    {
                        dtRow1[4] = reader.GetDecimal(4);
                    }
                    else
                        dtRow1[4] = 0;
                }
                dtnew.Rows.Add(dtRow1);
            }
            reader.Dispose();

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }

        //---------------
        public DataSet getHarvestRegisterRubber(String strDivision, DateTime dtDate, Int32 wrkty)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("WorkQTY"));
            dt.Columns.Add(new DataColumn("OverKg"));
            dt.Columns.Add(new DataColumn("LentDivision"));
            dt.Columns.Add(new DataColumn("Holiday"));
            dt.Columns.Add(new DataColumn("Mandays"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("WorkCodeID"));
            dt.Columns.Add(new DataColumn("FullHalf"));
            dt.Columns.Add(new DataColumn("User"));
            dt.Columns.Add(new DataColumn("CreatedDate")); ;
            dt.Columns.Add(new DataColumn("AreaCovered"));
            dt.Columns.Add(new DataColumn("ExtraRates"));
            dt.Columns.Add(new DataColumn("Scrap"));

            SqlDataReader reader;
            DataRow dtRow;

            //reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty, dbo.DailyGroundTransactions.OverKgs,  dbo.DailyGroundTransactions.LabourDivision, CASE WHEN (dbo.DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday,  dbo.DailyGroundTransactions.ManDays, CASE WHEN (dbo.DailyGroundTransactions.Labourtype = 'General') THEN dbo.DailyGroundTransactions.FieldID ELSE dbo.DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,   CASE WHEN (dbo.DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime ,dbo.DailyGroundTransactions.WorkQty1,dbo.DailyGroundTransactions.WorkQty2,dbo.DailyGroundTransactions.WorkQty3,dbo.DailyGroundTransactions.AreaCovered,dbo.DailyGroundTransactions.FieldWeight FROM  dbo.DailyGroundTransactions INNER JOIN  dbo.EstateField ON dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID AND   dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID INNER JOIN  dbo.EmployeeMaster ON dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo AND   dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID  WHERE (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "') AND (dbo.DailyGroundTransactions.DateEntered = '" + dtDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "') AND (dbo.DailyGroundTransactions.IsContract = 0) AND (CONVERT(int,  dbo.DailyGroundTransactions.EmpNo) BETWEEN CONVERT(int, '"+empFrom+"') AND CONVERT(int, '"+empTo+"')) ORDER BY dbo.DailyGroundTransactions.AutoKey  ", CommandType.Text);
            reader = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactions.WorkQty,  dbo.DailyGroundTransactions.OverKgs, dbo.DailyGroundTransactions.LabourDivision,  CASE WHEN (DailyGroundTransactions.HolidayYesNo = 1) THEN 'Y' ELSE 'N' END AS Holiday, dbo.DailyGroundTransactions.ManDays,  CASE WHEN (DailyGroundTransactions.Labourtype = 'General')  THEN DailyGroundTransactions.FieldID ELSE DailyGroundTransactions.LabourField END AS FieldID, dbo.DailyGroundTransactions.WorkCodeID,  CASE WHEN (DailyGroundTransactions.FullHalf = 2) THEN 'Full' ELSE 'Half' END AS FullHalf, dbo.DailyGroundTransactions.UserID,  dbo.DailyGroundTransactions.CreateDateTime, dbo.DailyGroundTransactions.AreaCovered, dbo.DailyGroundTransactions.ExtraRates,dbo.DailyGroundTransactions.ScrapKgs FROM         dbo.DailyGroundTransactions INNER JOIN dbo.EstateField ON dbo.DailyGroundTransactions.DivisionID = dbo.EstateField.DivisionID AND  dbo.DailyGroundTransactions.FieldID = dbo.EstateField.FieldID INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.DailyGroundTransactions.CropType=2) and  (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + strDivision + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + wrkty + "')  ", CommandType.Text);

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
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetDecimal(3);
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetDecimal(6);
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetString(7).Trim();
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetString(8).Trim();
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetString(9).Trim();
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetString(10).Trim();
                }
                if (!reader.IsDBNull(11))
                {
                    dtRow[11] = reader.GetDateTime(11);
                }
                if (!reader.IsDBNull(12))
                {
                    dtRow[12] = reader.GetDecimal(12);
                }
                if (!reader.IsDBNull(13))
                {
                    dtRow[13] = reader.GetDecimal(13);
                }
                if (!reader.IsDBNull(14))
                {
                    dtRow[14] = reader.GetDecimal(14);
                }


                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("TappingNames"));
            dtnew.Columns.Add(new DataColumn("AbsentCount"));
            dtnew.Columns.Add(new DataColumn("NotOffered"));
            dtnew.Columns.Add(new DataColumn("Sundry"));
            dtnew.Columns.Add(new DataColumn("FielWeight"));
            dtnew.Columns.Add(new DataColumn("AreaCovered"));
            dtnew.Columns.Add(new DataColumn("TappingAreaCovered"));
            dtnew.Columns.Add(new DataColumn("SundryAreaCovered"));
            dtnew.Columns.Add(new DataColumn("Scrap"));
            dtnew.Columns.Add(new DataColumn("TappingNamesExcludingHolidayHalf"));
            dtnew.Columns.Add(new DataColumn("SundryNamesExcludingHolidayHalf"));
            dtnew.Columns.Add(new DataColumn("ScrapKgAmount"));


            DataRow dtRow1;

            dtRow1 = dtnew.NewRow();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(ManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('TAP'))  ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('TAP'))  ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[0] = reader.GetDecimal(0);
                    else
                        dtRow1[0] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS ABS FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('ABS'))  ", CommandType.Text);


            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[1] = reader.GetInt32(0);
                    else
                        dtRow1[1] = "0";
                }
            }
            reader.Dispose();


            reader = SQLHelper.ExecuteReader("SELECT   count(isnull(EmpNo,0) )  AS NotOff FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('X%'))  ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetInt32(0).ToString()))
                        dtRow1[2] = reader.GetInt32(0);
                    else
                        dtRow1[2] = "0";
                }
            }
            reader.Dispose();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(ManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','TAP')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','TAP')) ", CommandType.Text);
            }

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[3] = reader.GetDecimal(0);
                    else
                        dtRow1[3] = "0";
                }
            }
            reader.Dispose();
            //-----------------------
            dtRow1[4] = "0";
            //reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(FieldWeight, 0)) AS FieldWeight  FROM dbo.DailyGroundTransactionsRubber where (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);

            //while (reader.Read())
            //{
            //    if (!reader.IsDBNull(0))
            //    {
            //        if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
            //            dtRow1[4] = reader.GetDecimal(0);
            //        else
            //            dtRow1[4] = "0";
            //    }
            //}
            //reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[5] = reader.GetDecimal(0);
                    else
                        dtRow1[5] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID  like ('TAP')) ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[6] = reader.GetDecimal(0);
                    else
                        dtRow1[6] = "0";
                }
            }
            reader.Dispose();

            reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(AreaCovered, 0)) AS AreaCovered  FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (WorkCodeID <> 'TAP') ", CommandType.Text);

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[7] = reader.GetDecimal(0);
                    else
                        dtRow1[7] = "0";
                }
            }
            reader.Dispose();
            //scrap
            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT  sum(ScrapKgs) as ScrapKgs,sum(CashScrapKgAmount) as ScrapKgAmount FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('TAP'))  ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT  sum(ScrapKgs) as ScrapKgs,sum(CashScrapKgAmount) as ScrapKgAmount  FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('TAP'))  ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[8] = reader.GetDecimal(0);
                    else
                        dtRow1[8] = "0";
                }
                if (!reader.IsDBNull(1))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(1).ToString()))
                        dtRow1[11] = reader.GetDecimal(1);
                    else
                        dtRow1[11] = "0";
                }
            }
            reader.Dispose();

            //mandays excluding Half Names
            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(case when (ManDays=1.5) then 1 else ManDays end , 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('TAP'))  ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT SUM(ISNULL(CashManDays, 0)) AS PLK FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID  like ('TAP'))  ", CommandType.Text);
            }
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[9] = reader.GetDecimal(0);
                    else
                        dtRow1[9] = "0";
                }
            }
            reader.Dispose();

            if (wrkty == 1)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(case when (ManDays=1.5) then 1 else ManDays end ,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','TAP')) ", CommandType.Text);
            }
            else if (wrkty == 2)
            {
                reader = SQLHelper.ExecuteReader("SELECT   SUM(isnull(CashManDays,0) )  AS SUNDRY FROM dbo.DailyGroundTransactions where (CropType=2) AND (DivisionID = '" + strDivision + "') AND (WorkType = '" + wrkty + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) and (WorkCodeID not like ('X%')) and (WorkCodeID not in ('ABS','TAP')) ", CommandType.Text);
            }

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (!String.IsNullOrEmpty(reader.GetDecimal(0).ToString()))
                        dtRow1[10] = reader.GetDecimal(0);
                    else
                        dtRow1[10] = "0";
                }
            }
            reader.Dispose();


            dtnew.Rows.Add(dtRow1);

            ds.Tables.Add(dt);
            ds.Tables.Add(dtnew);

            return ds;
        }

        public DataSet getUserAudit(DateTime dtFrom,DateTime dtTo)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT TransactionType, UserName, TransactionDate, UserID FROM dbo.UserAudit WHERE (TransactionDate BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) ORDER BY TransactionDate DESC", CommandType.Text);
            da.Fill(ds, "UserAudit");
            return ds;
        }

        public DataSet getDailyEntriesAudit(DateTime dtFrom, DateTime dtTo)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT ReferenceTable AS Type, DeletedDate, Naration2 AS Date, EmpNo, Narration1 AS emp, Naration3 AS WorkCode, DeletedBy, 'Deleted' as TransactionType FROM dbo.DeleteLog WHERE (ReferenceTable = 'DailyHarvest') AND (DeletedDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "',  102)) Union  SELECT  ReferenceTable, UpdatedDate, Narration1, Division, EmpNo, Narration3, UpdatedUser,'Updated' as TransactionType FROM dbo.UpdateLog WHERE     (ReferenceTable = 'DailyHarvest') AND (UpdatedDate BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "',  102))", CommandType.Text);
            da.Fill(ds, "DailyEntriesAudit");
            return ds;
        }

        public DataSet getMasterFileAudit(DateTime dtFrom, DateTime dtTo)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT ReferenceTable AS Expr1, UpdatedDate AS Expr2, Narration5, RefNo, Narration1, Narration2, Narration3, UpdatedUser FROM dbo.UpdateLog WHERE (UpdatedDate BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) AND  (ReferenceTable IN ('DeductMaster', 'AdditionMaster', 'JobMaster', 'EmployeeMaster'))ORDER BY Expr2 DESC", CommandType.Text);
            da.Fill(ds, "MasterFileAudit");
            return ds;
        }

        public DataSet getEmployeeAttendanceByCrop(int intYear, int intMonth, int WType,String strDiv,String strCrop)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            if (strCrop.Equals("1"))
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactions.WorkType=2) then 1 else dbo.DailyGroundTransactions.ManDays end) AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + WType + "')  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }
            else if (strCrop.Equals("2"))
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactionsRubber.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactionsRubber.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactionsRubber.WorkType=2) then 1 else dbo.DailyGroundTransactionsRubber.ManDays end)  AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactionsRubber ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactionsRubber.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactionsRubber.DivisionID WHERE      (YEAR(dbo.DailyGroundTransactionsRubber.DateEntered) = '" + intYear + "') AND  (MONTH(dbo.DailyGroundTransactionsRubber.DateEntered) = '" + intMonth + "') AND (dbo.DailyGroundTransactionsRubber.WorkType = '" + WType + "') AND  (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactionsRubber.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactionsRubber.DateEntered) ORDER BY dbo.DailyGroundTransactionsRubber.EmpNo", CommandType.Text);
            }
            else
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo as EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactionsRubber.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactionsRubber.WorkType=2) then 1 else dbo.DailyGroundTransactionsRubber.ManDays end)  AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactionsRubber ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactionsRubber.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactionsRubber.DivisionID WHERE      (YEAR(dbo.DailyGroundTransactionsRubber.DateEntered) = '" + intYear + "') AND  (MONTH(dbo.DailyGroundTransactionsRubber.DateEntered) = '" + intMonth + "') AND (dbo.DailyGroundTransactionsRubber.WorkType = '" + WType + "') AND  (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactionsRubber.DateEntered) union ALL " +
                                                                        "SELECT     TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo as EmpNO, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactions.WorkType=2) then 1 else dbo.DailyGroundTransactions.ManDays end) AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE     (YEAR(dbo.DailyGroundTransactions.DateEntered) = '" + intYear + "') AND (MONTH(dbo.DailyGroundTransactions.DateEntered) = '" + intMonth + "') AND  (dbo.DailyGroundTransactions.WorkType = '" + WType + "')  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered)  ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
            }
            da.Fill(ds, "EmployeeAttendance");
            return ds;
        }

        public DataSet getEmployeeAttendanceByCrop(DateTime dtFromDate, DateTime dtToDate, int WType, String strDiv, String strCrop)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            if (strCrop.Equals("0"))
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT  TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactions.WorkType=2) then 1 else dbo.DailyGroundTransactions.ManDays end) AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE   (dbo.DailyGroundTransactions.CropType=1) and   (dbo.DailyGroundTransactions.DateEntered between '" + dtFromDate + "' and '" + dtToDate + "')  AND  (dbo.DailyGroundTransactions.WorkType = '" + WType + "')  AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            }
            else if (strCrop.Equals("0"))
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactions.WorkType=2) then 1 else dbo.DailyGroundTransactions.ManDays end)  AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (dbo.DailyGroundTransactions.CropType=2)  AND (dbo.DailyGroundTransactions.DateEntered between '" + dtFromDate + "' and '" + dtToDate + "')  AND (dbo.DailyGroundTransactions.WorkType = '" + WType + "') AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);

            }
            else
            {
                da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT        TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender,  DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(CASE WHEN (dbo.DailyGroundTransactions.WorkType = 2) THEN 1 ELSE dbo.DailyGroundTransactions.ManDays END) AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE (dbo.DailyGroundTransactions.CropType like '"+strCrop+"') AND (dbo.DailyGroundTransactions.DateEntered BETWEEN '" + dtFromDate + "' AND '" + dtToDate + "') AND (dbo.DailyGroundTransactions.WorkType = '" + WType + "') AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) ORDER BY dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
                //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo as EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactionsRubber.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactionsRubber.WorkType=2) then 1 else dbo.DailyGroundTransactionsRubber.ManDays end)  AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactionsRubber ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactionsRubber.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactionsRubber.DivisionID WHERE     (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.DailyGroundTransactions.DateEntered between '" + dtFromDate + "' and '" + dtToDate + "')  AND (dbo.DailyGroundTransactionsRubber.WorkType = '" + WType + "') AND  (dbo.DailyGroundTransactionsRubber.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactionsRubber.DateEntered) union ALL " +
                //                                                        "SELECT     TOP (100) PERCENT dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo as EmpNO, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered) AS Day, SUM(case when (dbo.DailyGroundTransactions.WorkType=2) then 1 else dbo.DailyGroundTransactions.ManDays end) AS ManDays FROM dbo.EstateDivision INNER JOIN dbo.EmployeeMaster ON dbo.EstateDivision.DivisionID = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo AND  dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID WHERE     (dbo.DailyGroundTransactions.DateEntered between '" + dtFromDate + "' and '" + dtToDate + "')  AND  (dbo.DailyGroundTransactions.WorkType = '" + WType + "') AND (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.EstateDivision.DivisionID, dbo.EstateDivision.DivisionName, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, DAY(dbo.DailyGroundTransactions.DateEntered)  ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
            }
            da.Fill(ds, "EmployeeAttendance");
            return ds;
        }

        public DataSet getInterEstateBorrowedLabourRegister(DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     Date, WorkType, EstateID AS FromEstate, DivisionID, EmpNo, LabourEstate AS ToEstate, LabourDivision AS ToDivision, LabourField AS ToField, WorkCodeID,  ManDays, WorkQty, OverKilos FROM dbo.IECheckRollAttendance WHERE (Date BETWEEN CONVERT(DATETIME, '"+FromDate+"', 102) AND CONVERT(DATETIME, '"+ToDate+"', 102))", CommandType.Text);
            da.Fill(ds, "IEBorrowedLabour");
            return ds;
        }

        public DataSet GetManDaysSummary(String strDiv, DateTime dtFromDate, DateTime dtToDate)
        {
            DataSet ds = new DataSet("MandaysSummary");
            ds = SQLHelper.FillDataSet("SELECT ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName, SUM(dbo.DailyGroundTransactions.ManDays) AS Expr1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE ((dbo.DailyGroundTransactions.DivisionID LIKE '"+strDiv+"')) AND (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtToDate + "', 102)) AND  (dbo.DailyGroundTransactions.WorkType = 1) GROUP BY dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID, dbo.JobMaster.JobName", CommandType.Text);
            return ds;
        }

        public DataSet getWorkDistributionMainCodeWise(DateTime dtFrom, String strDiv)
        {
            DateTime dtFromDate = new DateTime(dtFrom.Year, dtFrom.Month, 1);
            DateTime dtMonthEndDate = dtFromDate.AddMonths(1).AddDays(-1);
            //DateTime dtToDate = dtFrom;
            DateTime dtYearStartDate = new DateTime(dtFrom.Year, 1, 1);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'Month' AS Type1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') union SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'ToDate' AS Type1 FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') ", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'Day' AS Type1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtFrom + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "')union  SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'MonthToDate' AS Type1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtFrom + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID like '" + strDiv + "')", CommandType.Text);
            da.Fill(ds, "WorkDistributionMainCodeWise");
            return ds;
        }
        public DataSet getWorkDistributionMainCodeWiseYear(DateTime dtFrom, String strDiv)
        {
            DateTime dtFromDate = new DateTime(dtFrom.Year, dtFrom.Month, 1);
            DateTime dtMonthEndDate = dtFromDate.AddMonths(1).AddDays(-1);
            //DateTime dtToDate = dtFrom;
            DateTime dtYearStartDate = new DateTime(dtFrom.Year, 1, 1);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'Month' AS Type1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') union SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'ToDate' AS Type1 FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtYearStartDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID LIKE'" + strDiv + "') ", CommandType.Text);
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'Month' AS Type1 FROM dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "')union  SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2)  THEN 1 ELSE 0.5 END END) AS ManDays, SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'ToDate' AS Type1 FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtYearStartDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') union SELECT     dbo.DailyGroundTransactions.DivisionID, ISNULL(dbo.DailyGroundTransactions.SubCategoryCode, 'NA') AS MainCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.JobMaster.JobName, dbo.DailyGroundTransactions.WorkType, (SELECT    ISNULL( MadulsimaEstateGL.dbo.ExpenditureBudget.Qty,'0')FROM         MadulsimaEstateGL.dbo.ExpenditureBudget WHERE     (AccountCode = (SELECT     MadulsimaEstateGL.dbo.AccountMaster.AccountCode FROM  MadulsimaEstateGL.dbo.AccountMaster                             WHERE      ( MadulsimaEstateGL.dbo.AccountMaster.AccountType = 'Labour') AND ( MadulsimaEstateGL.dbo.AccountMaster.SubCategoryCode = '005') AND ( MadulsimaEstateGL.dbo.AccountMaster.JobCode = '0094'))) AND (  MadulsimaEstateGL.dbo.ExpenditureBudget.SeasonKey =(SELECT     MadulsimaEstateGL.dbo.FinancialYear.PeriodKey   FROM      MadulsimaEstateGL.dbo.FinancialYear  WHERE      (YearName = '" + dtFromDate.Year + "'))) AND (MonthID = '" + dtFromDate.Month + "')) AS ManDays,  SUM(dbo.DailyGroundTransactions.WorkQty) AS workQty,(SELECT     TOP (1) MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryName FROM         MadulsimaEstateGL.dbo.AccountSubCategory WHERE     (MadulsimaEstateGL.dbo.AccountSubCategory.SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) as MainCodeName, 'Budget' AS Type1 FROM         dbo.DailyGroundTransactions INNER JOIN dbo.JobMaster ON dbo.DailyGroundTransactions.WorkCodeID = dbo.JobMaster.JobShortName WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '" + dtFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND  (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "') GROUP BY dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.SubCategoryCode, dbo.DailyGroundTransactions.WorkCodeID,  dbo.DailyGroundTransactions.WorkType, dbo.JobMaster.JobName HAVING      (dbo.DailyGroundTransactions.DivisionID LIKE '" + strDiv + "')", CommandType.Text);
            da.Fill(ds, "WorkDistributionMainCodeWise");
            return ds;
        }
        public DataSet GetWorkDistributionMainCodeWiseNew(DateTime dtToDate, String strDiv)
        {
            DateTime dtMonthFromDate = new DateTime(dtToDate.Year, dtToDate.Month, 1);
            //DateTime dtMonthEndDate = dtToDate.AddMonths(1).AddDays(-1);
            DateTime dtMonthEndDate = dtMonthFromDate.AddMonths(1).AddDays(-1);
            DateTime dtTodatefromDate = new DateTime(dtToDate.Year, 1, 1);
            DataSet ds = new DataSet("MainCodeWiseworkDistribution");
            SqlDataReader reader1;
            DataTable dt = new DataTable();
            dt.Columns.Add("MainCode");
            dt.Columns.Add("ManDays");
            dt.Columns.Add("type1");
            dt.Columns.Add("Type2");
            dt.Columns.Add("MainCodeName");
            dt.Columns.Add("Division");
            DataRow dtRow;
            dtRow = dt.NewRow();
            //Month
            reader1 = SQLHelper.ExecuteReader("SELECT     SubCategoryCode, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2) THEN 1 ELSE 0.5 END END) AS ManDays, 'Month' AS Type1,  CASE WHEN (WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS type2, (SELECT     SubCategoryName FROM          MadulsimaEstateGL.dbo.AccountSubCategory WHERE      (SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) AS CodeName, DivisionID FROM         dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '" + dtMonthFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND (DivisionID like  '"+strDiv+"') GROUP BY SubCategoryCode, WorkType, DivisionID", CommandType.Text);
             while (reader1.Read())
             {
                 dtRow = dt.NewRow();
                 if (!reader1.IsDBNull(0))
                 {
                     dtRow[0] = reader1.GetString(0).Trim();
                 }
                 else
                 {
                     dtRow[0] = "NA";
                 }
                 if (!reader1.IsDBNull(1))
                 {
                     dtRow[1] = reader1.GetDecimal(1);
                 }
                 if (!reader1.IsDBNull(2))
                 {
                     dtRow[2] = reader1.GetString(2).Trim();
                 }
                 if (!reader1.IsDBNull(3))
                 {
                     dtRow[3] = reader1.GetString(3).Trim();
                 }
                 if (!reader1.IsDBNull(4))
                 {
                     dtRow[4] = reader1.GetString(4).Trim();
                 }
                 else
                 {
                     dtRow[4] = "NA";
                 }
                 if (!reader1.IsDBNull(5))
                 {
                     dtRow[5] = reader1.GetString(5).Trim();
                 }                 
                 dt.Rows.Add(dtRow);                   

             }
             reader1.Close();
            //Todate
             reader1 = SQLHelper.ExecuteReader("SELECT     SubCategoryCode, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2) THEN 1 ELSE 0.5 END END) AS ManDays, 'Todate' AS Type1,  CASE WHEN (WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS type2, (SELECT     SubCategoryName FROM          MadulsimaEstateGL.dbo.AccountSubCategory WHERE      (SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) AS CodeName, DivisionID FROM         dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '" + dtTodatefromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND (DivisionID like  '" + strDiv + "') GROUP BY SubCategoryCode, WorkType, DivisionID", CommandType.Text);
             while (reader1.Read())
             {
                 dtRow = dt.NewRow();
                 if (!reader1.IsDBNull(0))
                 {
                     dtRow[0] = reader1.GetString(0).Trim();
                 }
                 else
                 {
                     dtRow[0] = "NA";
                 }
                 if (!reader1.IsDBNull(1))
                 {
                     dtRow[1] = reader1.GetDecimal(1);
                 }
                 if (!reader1.IsDBNull(2))
                 {
                     dtRow[2] = reader1.GetString(2).Trim();
                 }
                 if (!reader1.IsDBNull(3))
                 {
                     dtRow[3] = reader1.GetString(3).Trim();
                 }
                 if (!reader1.IsDBNull(4))
                 {
                     dtRow[4] = reader1.GetString(4).Trim();
                 }
                 else
                 {
                     dtRow[4] = "NA";
                 }
                 if (!reader1.IsDBNull(5))
                 {
                     dtRow[5] = reader1.GetString(5).Trim();
                 }
                 dt.Rows.Add(dtRow);

             }
             reader1.Close();
            //total Month
             reader1 = SQLHelper.ExecuteReader("SELECT     SubCategoryCode, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2) THEN 1 ELSE 0.5 END END) AS ManDays, 'Month' AS Type1, 'Total' AS type2, (SELECT     SubCategoryName FROM          MadulsimaEstateGL.dbo.AccountSubCategory WHERE      (SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) AS CodeName, DivisionID FROM         dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '" + dtMonthFromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND (DivisionID like  '" + strDiv + "') GROUP BY SubCategoryCode, DivisionID", CommandType.Text);
             while (reader1.Read())
             {
                 dtRow = dt.NewRow();
                 if (!reader1.IsDBNull(0))
                 {
                     dtRow[0] = reader1.GetString(0).Trim();
                 }
                 else
                 {
                     dtRow[0] = "NA";
                 }
                 if (!reader1.IsDBNull(1))
                 {
                     dtRow[1] = reader1.GetDecimal(1);
                 }
                 if (!reader1.IsDBNull(2))
                 {
                     dtRow[2] = reader1.GetString(2).Trim();
                 }
                 if (!reader1.IsDBNull(3))
                 {
                     dtRow[3] = reader1.GetString(3).Trim();
                 }
                 if (!reader1.IsDBNull(4))
                 {
                     dtRow[4] = reader1.GetString(4).Trim();
                 }
                 else
                 {
                     dtRow[4] = "NA";
                 }
                 if (!reader1.IsDBNull(5))
                 {
                     dtRow[5] = reader1.GetString(5).Trim();
                 }
                 dt.Rows.Add(dtRow);

             }
             reader1.Close();
             //Total Todate
             reader1 = SQLHelper.ExecuteReader("SELECT     SubCategoryCode, SUM(CASE WHEN (WorkType = 1) THEN ManDays ELSE CASE WHEN (FullHalf = 2) THEN 1 ELSE 0.5 END END) AS ManDays, 'Todate' AS Type1,  'Total' AS type2, (SELECT     SubCategoryName FROM          MadulsimaEstateGL.dbo.AccountSubCategory WHERE      (SubCategoryCode = dbo.DailyGroundTransactions.SubCategoryCode)) AS CodeName, DivisionID FROM         dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '" + dtTodatefromDate + "', 102) AND CONVERT(DATETIME, '" + dtMonthEndDate + "', 102)) AND (DivisionID like  '" + strDiv + "') GROUP BY SubCategoryCode, DivisionID", CommandType.Text);
             while (reader1.Read())
             {
                 dtRow = dt.NewRow();
                 if (!reader1.IsDBNull(0))
                 {
                     dtRow[0] = reader1.GetString(0).Trim();
                 }
                 else
                 {
                     dtRow[0] = "NA";
                 }
                 if (!reader1.IsDBNull(1))
                 {
                     dtRow[1] = reader1.GetDecimal(1);
                 }
                 if (!reader1.IsDBNull(2))
                 {
                     dtRow[2] = reader1.GetString(2).Trim();
                 }
                 if (!reader1.IsDBNull(3))
                 {
                     dtRow[3] = reader1.GetString(3).Trim();
                 }
                 if (!reader1.IsDBNull(4))
                 {
                     dtRow[4] = reader1.GetString(4).Trim();
                 }
                 else
                 {
                     dtRow[4] = "NA";
                 }
                 if (!reader1.IsDBNull(5))
                 {
                     dtRow[5] = reader1.GetString(5).Trim();
                 }
                 dt.Rows.Add(dtRow);

             }
             reader1.Close();
             
             ds.Tables.Add(dt);
            return ds;
        }


        //pre printed payslip MPL
        public DataTable getSalarySlipsPrePrintedMPL(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat, Boolean boolCWPayslip)
        {
            Decimal decEPFPayable = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");
            dt.Columns.Add("PRIPSS");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings')  ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (boolCWPayslip)
                {
                    readerEmployee = null;
                    //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '"+inCat+"') ", CommandType.Text);
                    /*totalearnigs>0 deleted as requested by Bogawana*/
                    //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "')  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                    //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "')  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {

                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    dtRow[27] = "PRI:0/PSS:0";
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount+PSSAmount, OtherAdditions AS OtherAdditions ,PreviousMadeUpCoins, PRIAmount,PSSAmount  FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins,PSSAmount", CommandType.Text);
                    
                    while (readerEmployeeEarnings.Read())
                    {
                        
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            }
                            else
                                dtRow[8] = 0;
                        }
                       
                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            }
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);                                
                            }
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Sick Leave")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Compensation")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Holiday Pay")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other1")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                                if (readerEmployeeEarnings.GetDecimal(8) > 0)
                                {
                                    dtRow[27] = "";
                                    if (!readerEmployeeEarnings.IsDBNull(11))
                                    {
                                        dtRow[27] = "PRI:" + readerEmployeeEarnings.GetDecimal(11).ToString();
                                    }
                                    if (!readerEmployeeEarnings.IsDBNull(12))
                                    {
                                        dtRow[27] = dtRow[27] + "/PSS:" + readerEmployeeEarnings.GetDecimal(12).ToString();
                                    }
                                }
                                
                                
                            }
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "EPF Payable Total")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = readerEmployeeEarnings.GetDecimal(0) + readerEmployeeEarnings.GetDecimal(2) + readerEmployeeEarnings.GetDecimal(4);
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();


                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Food")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(10))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

           

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (boolCWPayslip)
                {
                    readerEmployee = null;
                }
                else
                {
                    //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "')", CommandType.Text);
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim() + " " + reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();
                    String str = reader.GetString(0).Trim();

                    if (readerEmployee.GetString(0).Trim() == "0461")
                    {
                        if (str == "Dhoby")
                        {
                            str = "Dhoby";
                        }
                    }
                    //if (reader.GetString(0).Trim().Equals("Dhoby"))
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //else
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    dtRow[7] = 0;
                    dtRow[8] = 0;
                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    StrOtherDeductions1 = "";
                    StrOtherDeductionsTemp = "";

                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    }
                    if (StrOtherDeductions.Length > 42)
                    {
                        StrOtherDeductionsTemp = StrOtherDeductions;
                        StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                        StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                        StrOtherDeductionsTemp = "";
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    dtRow[24] = StrOtherDeductions1;

                    readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrLoanDeductions = "";
                    dtRow[22] = "-";
                    DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    DSotHours.Dispose();
                    while (readerEmpLoanDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpLoanDeduction.IsDBNull(1))
                            StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpLoanDeduction.IsDBNull(0))
                            StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                        if (StrLoanDeductions.Length > 42)
                        {
                            StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                        }
                    }
                    readerEmpLoanDeduction.Close();
                    dtRow[22] = StrLoanDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        //pre printed OLAX payslip Cash Work
        public DataTable getSalarySlipsPrePrintedCWMPL(String DivisionID, Int32 Year, Int32 Month, String DivisionName, String CategoryName, Int32 inCat, Boolean boolCWPayslip)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionName");//0
            dt.Columns.Add("CatagoryName");
            dt.Columns.Add("Type");
            dt.Columns.Add("SalaryItemName");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EMPNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Debits");
            dt.Columns.Add("Coins B/F");//11

            ///2011-08-02
            dt.Columns.Add("TotalDays");//12
            dt.Columns.Add("NormalDays");
            dt.Columns.Add("HoliDays");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("NICNo");
            dt.Columns.Add("LanguageTerm");//18
            dt.Columns.Add("OtherDeduct");
            dt.Columns.Add("OfferedDays");
            dt.Columns.Add("QualifyDays");
            dt.Columns.Add("LoanDeductions");
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OtherDeduct1");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("ItemNameTamil");
            dt.Columns.Add("PRIPSS");

            DataRow dtRow;
            SqlDataReader reader;
            SqlDataReader readerEmployee;
            SqlDataReader readerEmployeeEarnings;
            SqlDataReader readerEmployeeDeductions;
            SqlDataReader readerEmployeeFinal;
            SqlDataReader readerDays;
            SqlDataReader readerDetails;
            SqlDataReader readerEmpOtherDeduction;
            SqlDataReader readerEmpLoanDeduction;
            String StrOtherDeductions = "";
            String StrOtherDeductions1 = "";
            String StrOtherDeductionsTemp = "";
            String StrLoanDeductions = "";
            String strQulaifyEquation = "";

            dtRow = dt.NewRow();
            //Additions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName, TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Earnings') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                if (boolCWPayslip)
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                }
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1)  and (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT     dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + Year + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ", CommandType.Text);

                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "Cjpak; ";//addition in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    dtRow[27] = "PRI:0/PSS:0";
                    strQulaifyEquation = readerEmployee.GetDecimal(3).ToString() + "/*75/%" + "/=" + readerEmployee.GetDecimal(4).ToString();
                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + reader.GetString(0).Trim();
                    }
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }
                    }
                    readerDetails.Close();

                    //readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, PluckingKilos, CAST(OverKilos AS varchar(5)) + '  Kg' AS OverKilos, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount, OtherAdditions, PreviousMadeUpCoins FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "')", CommandType.Text);
                    readerEmployeeEarnings = SQLHelper.ExecuteReader("SELECT PluckingNamePay + SundryNamePay AS NamePay, SUM(PluckingManDays) + SUM(SundryManDays) + SUM(HolidayPLKManDays) + SUM(HolidaySundryManDays) AS PluckingKilos, ExtraRates, OverKilos, OverKilosPay, OverTime, CashPlucking + CashSundry AS CashWork, AttIncentive, PRIAmount+PSSAmount, OtherAdditions  AS OtherAdditions, PreviousMadeUpCoins, PRIAmount,PSSAmount FROM dbo.EmpMonthlyEarnings WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') GROUP BY ExtraRates, OverKilos, OverKilosPay, OverTime, AttIncentive, PRIAmount, PluckingNamePay, SundryNamePay, CashPlucking, CashSundry, OtherAdditions, PreviousMadeUpCoins,PSSAmount", CommandType.Text);

                    while (readerEmployeeEarnings.Read())
                    {
                        if (reader.GetString(0).Trim() == "Plucking")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(1))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(1);
                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(0))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(0);
                            }
                            else
                                dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Over Kilos")
                        {
                            if (!readerEmployeeEarnings.IsDBNull(3))
                                dtRow[7] = readerEmployeeEarnings.GetDecimal(3);

                            else
                                dtRow[7] = 0;
                            if (!readerEmployeeEarnings.IsDBNull(4))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(4);
                            }
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Extra Rates")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(2))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(2);
                            }
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Cash")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Sick Leave")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Compensation")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Holiday Pay")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Other1")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "PRI")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(8))
                            {
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(8);
                                if (readerEmployeeEarnings.GetDecimal(8) > 0)
                                {
                                    dtRow[27] = "";
                                    if (!readerEmployeeEarnings.IsDBNull(11))
                                    {
                                        dtRow[27] = "PRI:" + readerEmployeeEarnings.GetDecimal(11).ToString();
                                    }
                                    if (!readerEmployeeEarnings.IsDBNull(12))
                                    {
                                        dtRow[27] = dtRow[27] + "/PSS:" + readerEmployeeEarnings.GetDecimal(12).ToString();
                                    }
                                }
                            }
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "EPF Payable Total")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = readerEmployeeEarnings.GetDecimal(0) + readerEmployeeEarnings.GetDecimal(2) + readerEmployeeEarnings.GetDecimal(4);
                        }
                        if (reader.GetString(0).Trim() == "Cash work")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(6))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(6);
                            else
                                dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Overtime")
                        {
                            DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                            if (String.IsNullOrEmpty(DSotHours.Tables[0].Rows[0][0].ToString()))
                            {
                                dtRow[7] = 0;
                            }
                            else
                            {
                                dtRow[7] = DSotHours.Tables[0].Rows[0][0].ToString();
                            }
                            DSotHours.Dispose();


                            if (!readerEmployeeEarnings.IsDBNull(5))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(5);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Food")
                        {
                            dtRow[7] = 0;
                            dtRow[8] = 0;
                        }

                        if (reader.GetString(0).Trim() == "Other")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(9))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(9);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Previous Made Up Coins")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(10))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(10);
                            else
                                dtRow[8] = 0;
                        }
                        if (reader.GetString(0).Trim() == "Attendance Incentive")
                        {
                            dtRow[7] = 0;

                            if (!readerEmployeeEarnings.IsDBNull(7))
                                dtRow[8] = readerEmployeeEarnings.GetDecimal(7);
                            else
                                dtRow[8] = 0;
                        }
                    }
                    readerEmployeeEarnings.Close();
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------

                    dt.Rows.Add(dtRow);
                }
                readerEmployee.Close();
            }
            reader.Close();

            //Deductions
            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT SalryItemName,TamilItemName,ItemNo FROM dbo.CHKWageItemSequenceOLAX WHERE (SalaryItemType = 'Deductions') ORDER BY SeqId", CommandType.Text);
            while (reader.Read())
            {
                //readerEmployee = SQLHelper.ExecuteReader("SELECT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (ActiveEmployee = 1) AND (DivisionID = '" + DivisionID + "')", CommandType.Text);
                //readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.EPF10 > 0)", CommandType.Text);
                if (boolCWPayslip)
                {
                    readerEmployee = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmpMonthlyEarnings.OfferedDays,  dbo.EmpMonthlyEarnings.QualifyDays FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '" + Year + "') AND  (dbo.EmpMonthlyEarnings.Month = '" + Month + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND (dbo.EmpMonthlyEarnings.Category = '" + inCat + "') ", CommandType.Text);
                }
                else
                {
                    readerEmployee = null;
                }
                while (readerEmployee.Read())
                {
                    dtRow = dt.NewRow();
                    dtRow[0] = DivisionName;
                    dtRow[1] = CategoryName;
                    dtRow[2] = "fopTfs;";//Deductions in tamil
                    dtRow[25] = reader.GetInt32(2);
                    dtRow[26] = reader.GetString(1).Trim();
                    if (!reader.IsDBNull(1))
                    {
                        dtRow[18] = reader.GetString(1).Trim();
                    }

                    if (!reader.IsDBNull(0))
                    {
                        dtRow[3] = reader.GetInt32(2).ToString().PadLeft(2, '0') + " " + reader.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(2))
                    {
                        dtRow[4] = readerEmployee.GetString(2).Trim();
                    }
                    if (!readerEmployee.IsDBNull(0))
                    {
                        dtRow[5] = readerEmployee.GetString(0).Trim();
                    }
                    if (!readerEmployee.IsDBNull(1))
                    {
                        dtRow[6] = readerEmployee.GetString(1).Trim();
                    }
                    if (!readerEmployee.IsDBNull(3))
                    {
                        dtRow[20] = readerEmployee.GetDecimal(3).ToString();
                    }
                    if (!readerEmployee.IsDBNull(4))
                    {
                        dtRow[21] = readerEmployee.GetDecimal(4).ToString();
                    }

                    //Days
                    readerDays = SQLHelper.ExecuteReader("SELECT dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays + dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS TotalDays, dbo.EmpMonthlyEarnings.PluckingManDays + dbo.EmpMonthlyEarnings.SundryManDays AS NormalDays, dbo.EmpMonthlyEarnings.HolidayPLKManDays + dbo.EmpMonthlyEarnings.HolidaySundryManDays AS Holidays, dbo.FTSCheckRollRates.Amount FROM dbo.EmpMonthlyEarnings CROSS JOIN dbo.FTSCheckRollRates WHERE (Year = '" + Year + "') AND (Month = '" + Month + "') AND (EmpNO = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (dbo.FTSCheckRollRates.Type = 'DailyBasic')", CommandType.Text);
                    while (readerDays.Read())
                    {
                        if (!readerDays.IsDBNull(0))
                        {
                            dtRow[12] = readerDays.GetDecimal(0);
                        }
                        else
                            dtRow[12] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[13] = readerDays.GetDecimal(1);
                        }
                        else
                            dtRow[13] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[14] = readerDays.GetDecimal(2);
                        }
                        else
                            dtRow[14] = 0.0;
                        if (!readerDays.IsDBNull(1))
                        {
                            dtRow[15] = readerDays.GetDecimal(3);
                        }
                        else
                            dtRow[15] = 0.0;
                    }
                    readerDays.Close();

                    //OtherDetails
                    readerDetails = SQLHelper.ExecuteReader("SELECT DivisionID, NICNo FROM dbo.EmployeeMaster WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') GROUP BY DivisionID, NICNo HAVING (DivisionID = '" + DivisionID + "')", CommandType.Text);
                    while (readerDetails.Read())
                    {
                        if (!readerDetails.IsDBNull(0))
                        {
                            dtRow[16] = readerDetails.GetString(0).Trim();
                        }
                        else
                            dtRow[16] = "N/A";
                        if (!readerDetails.IsDBNull(1))
                        {
                            dtRow[17] = readerDetails.GetString(1).Trim();
                        }
                        else
                        {
                            dtRow[17] = "NT";
                        }

                    }
                    readerDetails.Close();

                    //readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //if (reader.GetString(0).Trim().Equals("Dhoby"))
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName in ('Dhoby','Barber')) AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    //else
                    //{
                    //    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);
                    //}
                    readerEmployeeDeductions = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode WHERE (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "')", CommandType.Text);

                    while (readerEmployeeDeductions.Read())
                    {
                        dtRow[7] = 0;

                        if (!readerEmployeeDeductions.IsDBNull(0))
                            dtRow[8] = readerEmployeeDeductions.GetDecimal(0);
                        else
                            dtRow[8] = 0;
                    }
                    readerEmployeeDeductions.Close();
                    //readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.GroupName = '" + reader.GetString(0).Trim() + "') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    //StrOtherDeductions = "";
                    //dtRow[19] = "-";
                    //while (readerEmpOtherDeduction.Read())
                    //{
                    //    //dtRow[19] = "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(1))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                    //    if (!readerEmpOtherDeduction.IsDBNull(0))
                    //        StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";
                    //    if (StrOtherDeductions.Length > 42)
                    //    {
                    //        StrOtherDeductions = StrOtherDeductions.Substring(0, 40) + "\r\n" + StrOtherDeductions.Substring(41, StrOtherDeductions.Length - 41);
                    //    }
                    //}
                    //readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    readerEmpOtherDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'O') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrOtherDeductions = "";
                    StrOtherDeductions1 = "";
                    StrOtherDeductionsTemp = "";
                    dtRow[19] = "-";
                    while (readerEmpOtherDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpOtherDeduction.IsDBNull(1))
                            StrOtherDeductions += readerEmpOtherDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpOtherDeduction.IsDBNull(0))
                            StrOtherDeductions += readerEmpOtherDeduction.GetDecimal(0) + ", ";

                    }
                    if (StrOtherDeductions.Length > 42)
                    {
                        StrOtherDeductionsTemp = StrOtherDeductions;
                        StrOtherDeductions = StrOtherDeductionsTemp.Substring(0, 40);// +"\r\n" +
                        StrOtherDeductions1 = StrOtherDeductionsTemp.Substring(41, StrOtherDeductionsTemp.Length - 41);
                        StrOtherDeductionsTemp = "";
                    }
                    readerEmpOtherDeduction.Close();
                    dtRow[19] = StrOtherDeductions;
                    dtRow[24] = StrOtherDeductions1;
                    readerEmpLoanDeduction = SQLHelper.ExecuteReader("SELECT SUM(dbo.CHKEmpDeductions.Amount) AS Amount, dbo.CHKDeduction.ShortName FROM dbo.CHKEmpDeductions INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKEmpDeductions.DeductGroupId = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKDeduction ON dbo.CHKDeductionGroup.DeductionGroupCode = dbo.CHKDeduction.DeductionGroupCode AND  dbo.CHKEmpDeductions.DeductId = dbo.CHKDeduction.DeductionCode WHERE     (dbo.CHKEmpDeductions.CancelYesNo = 0) AND (dbo.CHKEmpDeductions.DeductYear = '" + Year + "') AND (dbo.CHKEmpDeductions.DeductMonth = '" + Month + "') AND  (dbo.CHKEmpDeductions.EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (dbo.CHKDeductionGroup.ShortName = 'BL') AND (dbo.CHKEmpDeductions.DivisionId = '" + DivisionID + "') GROUP BY dbo.CHKDeduction.ShortName", CommandType.Text);
                    StrLoanDeductions = "";
                    dtRow[22] = "-";
                    DataSet DSotHours = SQLHelper.FillDataSet("SELECT sum(Hours) FROM dbo.CHKOvertime WHERE (YEAR(OtDate) = '" + Year + "') AND (MONTH(OtDate) = '" + Month + "') AND (DivisionCode = '" + DivisionID + "') AND (EmployeeNo = '" + readerEmployee.GetString(0).Trim() + "')", CommandType.Text);
                    dtRow[23] = DSotHours.Tables[0].Rows[0][0].ToString();
                    DSotHours.Dispose();
                    while (readerEmpLoanDeduction.Read())
                    {
                        //dtRow[19] = "-";
                        if (!readerEmpLoanDeduction.IsDBNull(1))
                            StrLoanDeductions += readerEmpLoanDeduction.GetString(1).Trim() + "-";
                        if (!readerEmpLoanDeduction.IsDBNull(0))
                            StrLoanDeductions += readerEmpLoanDeduction.GetDecimal(0) + ", ";
                        if (StrLoanDeductions.Length > 42)
                        {
                            StrLoanDeductions = StrLoanDeductions.Substring(0, 40) + "\r\n" + StrLoanDeductions.Substring(41, StrLoanDeductions.Length - 41);
                        }
                    }
                    readerEmpLoanDeduction.Close();
                    dtRow[22] = StrLoanDeductions;

                    dt.Rows.Add(dtRow);
                    //--------------
                    readerEmployeeFinal = SQLHelper.ExecuteReader("SELECT WagePay, DebitsBF, MadeUpBalance FROM dbo.EmpMonthlyFinalWeges WHERE (EmpNo = '" + readerEmployee.GetString(0).Trim() + "') AND (DivisionId = '" + DivisionID + "') AND (WageYear = '" + Year + "') AND (WageMonth = '" + Month + "')", CommandType.Text);
                    while (readerEmployeeFinal.Read())
                    {

                        if (!readerEmployeeFinal.IsDBNull(0))
                            dtRow[9] = readerEmployeeFinal.GetDecimal(0);
                        else
                            dtRow[9] = 0;
                        if (!readerEmployeeFinal.IsDBNull(1))
                            dtRow[10] = readerEmployeeFinal.GetDecimal(1);
                        else
                            dtRow[10] = 0;
                        if (!readerEmployeeFinal.IsDBNull(2))
                            dtRow[11] = readerEmployeeFinal.GetDecimal(2);
                        else
                            dtRow[11] = 0;
                    }
                    readerEmployeeFinal.Close();
                    //--------
                }
                readerEmployee.Close();
            }
            reader.Close();
            return dt;
        }

        public DataSet GetCWEmployeeListForPayslip(String strDiv, Int32 intEmpCat, Int32 intYear, Int32 intMonth)
        {
            DataSet empDS = new DataSet();
            empDS = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE  (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND (dbo.EmpMonthlyEarnings.TotalEarnings > 0) AND ((dbo.EmpMonthlyEarnings.CashPlucking + dbo.EmpMonthlyEarnings.CashSundry) > 0) AND  (dbo.EmpMonthlyEarnings.Category = '" + intEmpCat + "') ", CommandType.Text);
            return empDS;
        }

        public DataSet GetNormalEmployeeListForPayslip(String strDiv, Int32 intEmpCat, Int32 intYear, Int32 intMonth)
        {
            DataSet empDS = new DataSet();
            //empDS = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + intEmpCat + "')", CommandType.Text);
            empDS = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE  (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND  (dbo.EmpMonthlyEarnings.PluckingNamePay+dbo.EmpMonthlyEarnings.SundryNamePay+ dbo.EmpMonthlyEarnings.OtherEPFAdditions > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + intEmpCat + "')", CommandType.Text);
            return empDS;
        }
        public DataSet GetAllEmployeeListForPayslip(String strDiv, Int32 intEmpCat, Int32 intYear, Int32 intMonth)
        {
            DataSet empDS = new DataSet();
            //empDS = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + intEmpCat + "')", CommandType.Text);
            //empDS = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE (dbo.EmployeeMaster.ActiveEmployee = 1) AND (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND  (dbo.EmpMonthlyEarnings.TotalEarnings  > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + intEmpCat + "')", CommandType.Text);
            /*ActiveEmployee check filter removed*/
            empDS = SQLHelper.FillDataSet("SELECT dbo.EmployeeMaster.EmpNo FROM dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE  (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (dbo.EmpMonthlyEarnings.Year = '" + intYear + "')  AND (dbo.EmpMonthlyEarnings.Month = '" + intMonth + "') AND  (dbo.EmpMonthlyEarnings.TotalEarnings  > 0)  AND  (dbo.EmpMonthlyEarnings.Category = '" + intEmpCat + "')", CommandType.Text);
            return empDS;
        }

        public DataSet GetDailyEntryData(String strDiv, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsDailyEntry = SQLHelper.FillDataSet("SELECT   DateEntered, WorkType, DivisionID, EmpNo, CASE WHEN (WorkCodeID = 'PLK') THEN 'PLK' ELSE CASE WHEN (WorkCodeID = 'TAP') THEN 'TAP' ELSE WorkCodeID END END AS Job,  ManDays, WorkQty, OverKgs, ScrapKgs,CropType, PRIAmount, PSSAmount FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (DivisionID like '" + strDiv + "')", CommandType.Text);
            return dsDailyEntry;
        }

        public DataSet GetTapDetails(DateTime dtFrom, DateTime dtTo, String strDiv)
        {
            DataSet dsDailyEntry = SQLHelper.FillDataSet("SELECT DateEntered, CASE WHEN (WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS WorkType, CASE WHEN (WorkType = 1)  THEN 'Normal' ELSE CASE WHEN (CashWorkType = 1) THEN 'Double Tapping' ELSE CASE WHEN (CashWorkType = 2)  THEN 'Cash Tapping' ELSE CASE WHEN (CashWorkType = 3) THEN 'Cash Work' ELSE CASE WHEN (CashWorkType = 4)  THEN 'Contract Tapping' ELSE CASE WHEN (CashWorkType = 5) THEN 'Contract Name' ELSE 'Other' END END END END END END AS CWType,  CASE WHEN (WorkType = 1) THEN WorkQty ELSE CashKgs END AS WorkQty, CashWorkType, CASE WHEN (LabourType = 'General')  THEN DivisionID ELSE CASE WHEN (LabourType = 'Lent Labour') THEN LabourDivision ELSE LabourEstate END END AS Division, CASE WHEN (WorkType = 1)  THEN ManDays ELSE CashManDays END AS ManDays, OverKgs, ScrapKgs FROM            dbo.DailyGroundTransactions WHERE        (DateEntered BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) AND (WorkCodeID = 'TAP') AND  (DivisionID LIKE '"+strDiv+"')", CommandType.Text);
            return dsDailyEntry;            
        }

        public DataSet GetOilParmEntries(DateTime dtFrom, DateTime dtTo, String strDiv, Int32 intWorkType)
        {
            DataSet dsDailyEntry = SQLHelper.FillDataSet("SELECT        DateEntered, CropType, CASE WHEN (WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS Expr1, DivisionID, FieldID, EmpNo, LabourType, LabourDivision,  LabourField, WorkCodeID, WorkQty, ManDays, TaskCompleted, PRIAmount, ExtraRates, OverKgAmount, CashKgAmount, CashSundryAmount, OverKgs,  WorkType FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (CropType = 3) AND  (DivisionID = '" + strDiv + "') AND (WorkType = '" + intWorkType + "')", CommandType.Text);
            return dsDailyEntry;
        }

        public DataSet GetOtherCropEntries(DateTime dtFrom, DateTime dtTo, String strDiv, Int32 intWorkType,Int32 intCrop)
        {
            DataSet dsDailyEntry = SQLHelper.FillDataSet("SELECT        DateEntered, CropType, CASE WHEN (WorkType = 1) THEN 'Normal' ELSE 'Cash Work' END AS Expr1, DivisionID, FieldID, EmpNo, LabourType, LabourDivision,  LabourField, WorkCodeID, WorkQty, ManDays, TaskCompleted, PRIAmount, ExtraRates, OverKgAmount, CashKgAmount, CashSundryAmount, OverKgs,  WorkType FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (CropType >3) AND  (DivisionID = '" + strDiv + "') AND (WorkType = '" + intWorkType + "') AND ( CropType='"+intCrop+"')", CommandType.Text);
            return dsDailyEntry;
        }


        



       
    }
}
