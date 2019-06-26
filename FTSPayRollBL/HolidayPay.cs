using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;


namespace FTSPayRollBL
{

    public class HolidayPay
    {
        FTSCheckRollSettings chkSettings = new FTSCheckRollSettings();

        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        private Int32 intYear;

        public Int32 IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }

        private DateTime dtFromDate;

        public DateTime DtFromDate
        {
            get { return dtFromDate; }
            set { dtFromDate = value; }
        }

        private DateTime dtToDate;

        public DateTime DtToDate
        {
            get { return dtToDate; }
            set { dtToDate = value; }
        }
        //get HolidayPayData from Original data
        public DataTable GetHolidayPayData(String strDiv, Int32 intYear)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));//0
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("ManDays"));//4
            dt.Columns.Add(new DataColumn("HolidayHalfNames"));
            dt.Columns.Add(new DataColumn("DailyBasic"));
            dt.Columns.Add(new DataColumn("OverKgAmount"));
            dt.Columns.Add(new DataColumn("ExtraRates"));//8
            dt.Columns.Add(new DataColumn("Earnings"));
            dt.Columns.Add(new DataColumn("NormalManDays"));
            dt.Columns.Add(new DataColumn("Average"));
            dt.Columns.Add(new DataColumn("EmpNo_"));//12
            dt.Columns.Add(new DataColumn("XXXMDays"));//12-13
            dt.Columns.Add(new DataColumn("XPRDays"));
            dt.Columns.Add(new DataColumn("XMTDays"));
            dt.Columns.Add(new DataColumn("XINDays"));
            dt.Columns.Add(new DataColumn("OtherNotOfferedDays"));//16-17
            dt.Columns.Add(new DataColumn("NotianalDays"));
            dt.Columns.Add(new DataColumn("TotalManDays"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("EstateOffered"));//20-21
            dt.Columns.Add(new DataColumn("PaidHolidays"));//21
            dt.Columns.Add(new DataColumn("PSS"));//22
            dt.Columns.Add(new DataColumn("ScrapAmount"));//23
            dt.Columns.Add(new DataColumn("EPFPayable"));//24-25
            //dt.Columns.Add(new DataColumn("AttQualifyDays"));
            //dt.Columns.Add(new DataColumn("WorkPercentage"));
            //dt.Columns.Add(new DataColumn("AttBonus"));
            //dt.Columns.Add(new DataColumn("HolidayPay"));
            //dt.Columns.Add(new DataColumn("EPF"));
            //dt.Columns.Add(new DataColumn("PSS"));
            //dt.Columns.Add(new DataColumn("ETF"));
            //dt.Columns.Add(new DataColumn("CreateDateTime"));
            //dt.Columns.Add(new DataColumn("UserID"));

            DataRow dtrow;
            SqlDataReader dataReader;
            SqlDataReader dataReaderNew;

            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.HolidayPayData.Year, dbo.HolidayPayData.EstateID, dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo, dbo.HolidayPayData.ManDays, dbo.HolidayPayData.HolidayHalfNames, dbo.HolidayPayData.DailyBasic, dbo.HolidayPayData.OverKgAmount, dbo.HolidayPayData.ExtraRates,  dbo.HolidayPayData.Earnings, dbo.HolidayPayData.NormalManDays, dbo.HolidayPayData.Average, dbo.HolidayPayData.EmpNo, dbo.HolidayPayData.XXXDays, dbo.HolidayPayData.XPRDays,  dbo.HolidayPayData.XMTDays, dbo.HolidayPayData.XINDays, dbo.HolidayPayData.OtherNotOfferedDays, dbo.HolidayPayData.NotianalDays,  dbo.HolidayPayData.TotalManDays, dbo.EmployeeMaster.EMPName,dbo.HolidayPayData.EstateOffered,dbo.HolidayPayData.PaidHolidays,dbo.HolidayPayData.PSSAmount,dbo.HolidayPayData.ScrapAmount,dbo.HolidayPayData.EPFPayableAmount FROM dbo.HolidayPayData INNER JOIN dbo.EmployeeMaster ON dbo.HolidayPayData.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.HolidayPayData.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.HolidayPayData.Year = '" + intYear + "') AND (dbo.HolidayPayData.DivisionID = '" + strDiv + "')  AND (dbo.HolidayPayData.ConfirmedYesNo = 0)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
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
                    dtrow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDecimal(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetDecimal(16);
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetDecimal(17);
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetDecimal(18);
                }
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetDecimal(19);
                }
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetString(20);
                }
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetDecimal(21);
                }
                else
                {
                    dtrow[21] = 0;
                }
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetDecimal(22);
                }
                else
                {
                    dtrow[22] = 0;
                }
                if (!dataReader.IsDBNull(23))
                {
                    dtrow[23] = dataReader.GetDecimal(23);
                }
                else
                {
                    dtrow[23] = 0;
                }
                if (!dataReader.IsDBNull(24))
                {
                    dtrow[24] = dataReader.GetDecimal(24);
                }
                else
                {
                    dtrow[24] = 0;
                }
                if (!dataReader.IsDBNull(25))
                {
                    dtrow[25] = dataReader.GetDecimal(25);
                }
                else
                {
                    dtrow[25] = 0;
                }
                //dataReaderNew = SQLHelper.ExecuteReader("SELECT isnull(SUM(OfferedDays),0) AS Expr1 FROM dbo.EmpMonthlyEarnings WHERE     (DivisionId = '" + strDiv + "') AND (EmpNO = '" + dataReader.GetString(3).Trim() + "') AND (Year = '" + intYear + "') AND (Month BETWEEN 1 AND 12)", CommandType.Text);
                //while (dataReaderNew.Read())
                //{
                //    if (!dataReaderNew.IsDBNull(0))
                //    {
                //        dtrow[20] = dataReaderNew.GetDecimal(0);
                //    }
                //}

                dt.Rows.Add(dtrow);
                //dataReaderNew.Close();
            }
            dataReader.Close();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);
           

            return dt;
        }

        /*Previous Year Holiday Pay Balance*/
        public DataTable GetPreviousYearHolidayPayLeaveBalance(String strDiv, Int32 intYear)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));//0
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("XPRDays"));

            DataRow dtrow;
            SqlDataReader dataReader;
            SqlDataReader dataReaderNew;

            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.HolidayPayData.Year, dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo, dbo.EmployeeMaster.EMPName, dbo.HolidayPayData.XPRDays FROM dbo.HolidayPayData INNER JOIN dbo.EmployeeMaster ON dbo.HolidayPayData.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.HolidayPayData.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.HolidayPayData.Year = '" + intYear + "') AND (dbo.HolidayPayData.DivisionID = '" + strDiv + "') ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
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
                    dtrow[4] = dataReader.GetDecimal(4);
                }
               
                dt.Rows.Add(dtrow);
                //dataReaderNew.Close();
            }
            dataReader.Close();

            return dt;
        }


        /*Holidaypay data not processed*/
        public DataTable GetNotProcessedHolidayPayData(String strDiv, Int32 intYear)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));//0
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("ManDays"));//4
            dt.Columns.Add(new DataColumn("HolidayHalfNames"));
            dt.Columns.Add(new DataColumn("DailyBasic"));
            dt.Columns.Add(new DataColumn("OverKgAmount"));
            dt.Columns.Add(new DataColumn("ExtraRates"));//8
            dt.Columns.Add(new DataColumn("Earnings"));
            dt.Columns.Add(new DataColumn("NormalManDays"));
            dt.Columns.Add(new DataColumn("Average"));
            dt.Columns.Add(new DataColumn("XXXMDays"));//12
            dt.Columns.Add(new DataColumn("XPRDays"));
            dt.Columns.Add(new DataColumn("XMTDays"));
            dt.Columns.Add(new DataColumn("XINDays"));
            dt.Columns.Add(new DataColumn("OtherNotOfferedDays"));//16
            dt.Columns.Add(new DataColumn("NotianalDays"));
            dt.Columns.Add(new DataColumn("TotalManDays"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("EstateOffered"));//20
            //dt.Columns.Add(new DataColumn("AttQualifyDays"));
            //dt.Columns.Add(new DataColumn("WorkPercentage"));
            //dt.Columns.Add(new DataColumn("AttBonus"));
            //dt.Columns.Add(new DataColumn("HolidayPay"));
            //dt.Columns.Add(new DataColumn("EPF"));
            //dt.Columns.Add(new DataColumn("PSS"));
            //dt.Columns.Add(new DataColumn("ETF"));
            //dt.Columns.Add(new DataColumn("CreateDateTime"));
            //dt.Columns.Add(new DataColumn("UserID"));

            DataRow dtrow;
            SqlDataReader dataReader;
            SqlDataReader dataReaderNew;

            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.HolidayPayData.Year, dbo.HolidayPayData.EstateID, dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo, dbo.HolidayPayData.ManDays, dbo.HolidayPayData.HolidayHalfNames, dbo.HolidayPayData.DailyBasic, dbo.HolidayPayData.OverKgAmount, dbo.HolidayPayData.ExtraRates,  dbo.HolidayPayData.Earnings, dbo.HolidayPayData.NormalManDays, dbo.HolidayPayData.Average, dbo.HolidayPayData.XXXDays, dbo.HolidayPayData.XPRDays,  dbo.HolidayPayData.XMTDays, dbo.HolidayPayData.XINDays, dbo.HolidayPayData.OtherNotOfferedDays, dbo.HolidayPayData.NotianalDays,  dbo.HolidayPayData.TotalManDays, dbo.EmployeeMaster.EMPName FROM dbo.HolidayPayData INNER JOIN dbo.EmployeeMaster ON dbo.HolidayPayData.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.HolidayPayData.EmpNo = dbo.EmployeeMaster.EmpNo WHERE (dbo.HolidayPayData.Year = '" + intYear + "') AND (dbo.HolidayPayData.DivisionID = '" + strDiv + "')  AND (dbo.HolidayPayData.ConfirmedYesNo = 0)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
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
                    dtrow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDecimal(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetDecimal(16);
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetDecimal(17);
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetDecimal(18);
                }
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19);
                }
                dataReaderNew = SQLHelper.ExecuteReader("SELECT isnull(SUM(OfferedDays),0) AS Expr1 FROM dbo.EmpMonthlyEarnings WHERE     (DivisionId = '" + strDiv + "') AND (EmpNO = '" + dataReader.GetString(3).Trim() + "') AND (Year = '" + intYear + "') AND (Month BETWEEN 1 AND 12)", CommandType.Text);
                while (dataReaderNew.Read())
                {
                    if (!dataReaderNew.IsDBNull(0))
                    {
                        dtrow[20] = dataReaderNew.GetDecimal(0);
                    }
                }

                dt.Rows.Add(dtrow);
                dataReaderNew.Close();
            }
            dataReader.Close();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);


            return dt;
        }


        /*get holiday Pay data from DailyGroundTransaction Table*/
        public DataTable ListHolidayPayData(String strDiv, Int32 intYear)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));//0
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("ManDays"));//4
            dt.Columns.Add(new DataColumn("HolidayHalfNames"));
            dt.Columns.Add(new DataColumn("DailyBasic"));
            dt.Columns.Add(new DataColumn("OverKgAmount"));
            dt.Columns.Add(new DataColumn("ExtraRates"));//8
            dt.Columns.Add(new DataColumn("Earnings"));
            dt.Columns.Add(new DataColumn("NormalManDays"));
            dt.Columns.Add(new DataColumn("Average"));
            dt.Columns.Add(new DataColumn("XXXMDays"));//12
            dt.Columns.Add(new DataColumn("XPRDays"));
            dt.Columns.Add(new DataColumn("XMTDays"));
            dt.Columns.Add(new DataColumn("XINDays"));
            dt.Columns.Add(new DataColumn("OtherNotOfferedDays"));//16
            dt.Columns.Add(new DataColumn("NotianalDays"));
            dt.Columns.Add(new DataColumn("TotalManDays"));
            //dt.Columns.Add(new DataColumn("EstateOffered"));
            //dt.Columns.Add(new DataColumn("AttQualifyDays"));//20
            //dt.Columns.Add(new DataColumn("WorkPercentage"));
            //dt.Columns.Add(new DataColumn("AttBonus"));
            //dt.Columns.Add(new DataColumn("HolidayPay"));
            //dt.Columns.Add(new DataColumn("EPF"));//24
            //dt.Columns.Add(new DataColumn("PSS"));
            //dt.Columns.Add(new DataColumn("ETF"));
            //dt.Columns.Add(new DataColumn("CreateDateTime"));
            //dt.Columns.Add(new DataColumn("UserID"));//28

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT YEAR('1/1/2012') AS Year, (SELECT TOP (1) EstateID FROM dbo.Estate) AS Estate, DivisionID, EmpNo, SUM(ManDays) AS ManDays, SUM(HolidayHalfNames) AS HolidayHalf, SUM(DailyBasic) AS DailyBasic,  SUM(OverKgAmount) AS OverKgAmount, SUM(ExtraRates) AS ExtraRates, SUM(DailyBasic + OverKgAmount + ExtraRates) AS Earnings,  SUM(ManDays - HolidayHalfNames) AS Mandays1, CAST(CASE WHEN (SUM(ManDays - HolidayHalfNames) > 0) THEN SUM(DailyBasic + OverKgAmount + ExtraRates) / SUM(ManDays - HolidayHalfNames) ELSE 0 END AS decimal(18, 2)) AS Average, Convert(Decimal(18,2),SUM(CASE WHEN (WorkCodeID IN ('XXX')) THEN 1 ELSE 0 END)) AS XXXDays,  Convert(Decimal(18,2),SUM(CASE WHEN (WorkCodeID IN ('XPR')) THEN 1 ELSE 0 END)) AS XPRDays, Convert(Decimal(18,2),SUM(CASE WHEN (WorkCodeID IN ('XMT')) THEN 1 ELSE 0 END)) AS XMTDays,  convert(decimal(18,2),Convert(Decimal(18,2),SUM(CASE WHEN (WorkCodeID IN ('XIN')) THEN 1 ELSE 0 END))) AS XINDays, convert(decimal(18,2),0) AS OtherNotianalDays, Convert(Decimal(18,2),SUM(CASE WHEN (WorkCodeID IN ('XPR', 'XIN', 'XMT', 'XXX'))  THEN 1 ELSE 0 END)) AS NotianalDays, SUM(ManDays - HolidayHalfNames) + SUM(CASE WHEN (WorkCodeID IN ('XPR', 'XIN', 'XMT', 'XXX')) THEN 1 ELSE 0 END)  AS TotalDays FROM dbo.DailyGroundTransactions WHERE     (DateEntered BETWEEN CONVERT(DATETIME, '2012-01-01 00:00:00', 102) AND CONVERT(DATETIME, '2012-12-31 00:00:00', 102)) AND (DivisionID = 'ELL') AND  (WorkType = 1) GROUP BY DivisionID, EmpNo ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
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
                    dtrow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDecimal(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetDecimal(16);
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetDecimal(17);
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetDecimal(18);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public String InsertHolidayPayData(Int32 intYear,String strDiv,Boolean ReEnterYesNO,String Emp,DateTime dtFrom,DateTime dtTo)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@HolidayPayYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = dtFrom;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = dtTo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strDiv;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ReEnter", SqlDbType.Bit);
            param.Value = ReEnterYesNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar,50);
            param.Value = Emp;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spInsertHolidayPayData]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public String UpdateHolidayPayData1(Int32 intYear, String strDiv, Boolean ReEnterYesNO, String Emp,DateTime dtFrom,DateTime dtTo)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@HolidayPayYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FromDate", SqlDbType.DateTime);
            param.Value = dtFrom;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ToDate", SqlDbType.DateTime);
            param.Value = dtTo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strDiv;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ReEnter", SqlDbType.Bit);
            param.Value = ReEnterYesNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = Emp;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spUpdateHolidayPayData]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public String DownloadHolidayPayData(Int32 intYear, String strDiv, Boolean ReEnterYesNO, String Emp,DateTime dtFrom,DateTime dtTo)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@HolidayPayYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strDiv;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ReEnter", SqlDbType.Bit);
            param.Value = ReEnterYesNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = Emp;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@fromDate", SqlDbType.DateTime);
            param.Value = dtFrom;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@toDate", SqlDbType.DateTime);
            param.Value = dtTo;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spInsertHolidayPayData_OldSystem]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public void ClearHolidaypayData(String strDiv, Int32 intYear)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM dbo.HolidayPayData WHERE (Year = '"+intYear+"') AND (DivisionID = '"+strDiv+"')", CommandType.Text);
        }

        public void ClearGratuitypayData(String strDiv, Int32 intYear)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM  dbo.GratuityPayData1 WHERE (Year = '"+intYear+"') AND (DivisionId = '"+strDiv+"')", CommandType.Text);
        }

        public void UpdateHolidaypayData( Int32 intYear,String strDiv,String strEmpNo,Decimal decManDays,Decimal decHolidayHalf,Decimal decDailyBasic, Decimal decOverKgAmount,Decimal decExtraRate,Decimal decEarnings,Decimal decNormalManDays,Decimal decAverage,Decimal decXXX,Decimal decXPR,Decimal decXMT, Decimal decXIN, Decimal decOtherNotOffered,Decimal decNOtianal,Decimal decEstOffered,Decimal mDays,Decimal decPaidHolidays,Decimal decPSS,Decimal decScrapAmount,Boolean boolPHDeduct )
        {
            Decimal DecDeductingPH = decPaidHolidays;
            if (boolPHDeduct)
            {
                DecDeductingPH = decPaidHolidays;
            }
            else
            {
                DecDeductingPH = 0;
            }
            Decimal decNotianalDays = decXXX + decXPR + decXMT + decXIN + decOtherNotOffered;
            Decimal decMDayWithOutPH = decManDays - decPaidHolidays;
            Decimal decTotalDays = decNotianalDays + (decManDays - (decHolidayHalf + DecDeductingPH + decXXX));
            Decimal decAttqualifyDays = decTotalDays - decXXX;
            Decimal decEarningsAmount = decDailyBasic + decOverKgAmount + decExtraRate+decPSS+decScrapAmount;
            Decimal decEpfPayableAmount = decDailyBasic + decOverKgAmount + decExtraRate  + decScrapAmount;
            Decimal decEpfPayableAverage = 0;
            Decimal decAverageAmount = 0;
            if ((decManDays - decHolidayHalf) > 0)
            {
                decAverageAmount = decEarningsAmount / (decManDays - (decHolidayHalf+DecDeductingPH));
            }
            else
            {
                decAverageAmount = 0;
            }
            if ((decManDays - decHolidayHalf) > 0)
            {
                decEpfPayableAverage = decEpfPayableAmount / (decManDays - (decHolidayHalf+DecDeductingPH));
            }
            else
            {
                decEpfPayableAverage = 0;
            }
            //SQLHelper.ExecuteNonQuery("UPDATE [dbo].[HolidayPayData] SET [ManDays] = '" + decManDays + "' ,[HolidayHalfNames] ='" + decHolidayHalf + "' ,[DailyBasic] ='" + decDailyBasic + "' ,[OverKgAmount] = '" + decOverKgAmount + "' ,[ExtraRates] = '" + decExtraRate + "' ,[Earnings] = '" + decEarnings + "' ,[NormalManDays] = '" + decNormalManDays + "' ,[Average] = '" + decAverage + "' ,[XXXDays] = '" + decXXX + "' ,[XPRDays] = '" + decXPR + "' ,[XMTDays] = '" + decXMT + "' ,[XINDays] = '" + decXIN + "' ,[OtherNotOfferedDays] = '" + decOtherNotOffered + "' ,[NotianalDays] = '" + decNotianalDays + "',[TotalManDays]='" + decTotalDays + "' WHERE ([Year]='" + intYear + "') AND ([DivisionID]='" + strDiv + "') AND ([EmpNo] ='" + strEmpNo + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[HolidayPayData] SET  [ManDays] = '" + decManDays + "' ,[HolidayHalfNames] ='" + decHolidayHalf + "' ,[DailyBasic] ='" + decDailyBasic + "' ,[OverKgAmount] = '" + decOverKgAmount + "' ,[ExtraRates] = '" + decExtraRate + "' ,[Earnings] = '" + decEarningsAmount + "' , [NormalManDays] = '" + decNormalManDays + "' ,[Average] = '" + decAverageAmount + "' ,[XXXDays] = '" + decXXX + "' ,[XPRDays] = '" + decXPR + "' ,[XMTDays] = '" + decXMT + "' ,[XINDays] = '" + decXIN + "' ,[OtherNotOfferedDays] = '" + decOtherNotOffered + "' ,[NotianalDays] = '" + decNotianalDays + "',[TotalManDays]='" + decTotalDays + "', EstateOffered='" + decEstOffered + "',EPFPayableAmount='" + decEpfPayableAmount + "',EPFPaybleAverage='" + decEpfPayableAverage + "',PaidHolidays='"+decPaidHolidays+"' WHERE ([Year]='" + intYear + "') AND ([DivisionID]='" + strDiv + "') AND ([EmpNo] ='" + strEmpNo + "') ", CommandType.Text);

        }

        public void UpdateGratuitypayData(Int32 intYear, String strDiv, String strEmpNo, DateTime LastDateOfWork, Decimal PreVestingPeriod, Decimal PostVestingPeriod, Decimal AmountBF, Decimal ProvisionYear, Decimal UnderProvAdjestment, Decimal OverProvAdjestment, Decimal Payment, Decimal Balance)
        {  
            SQLHelper.ExecuteNonQuery("UPDATE dbo.GratuityPayData1 SET [LastDateOfWork] = '" + LastDateOfWork + "' ,[PreVestingPeriod] ='" + PreVestingPeriod + "' ,PostVestingPeriod='" + PostVestingPeriod + "', AmountBF='" + AmountBF + "', ProvisionYear='" + ProvisionYear + "',UnderProvAdjustment='" + UnderProvAdjestment + "',OverProvAdjustment='" + OverProvAdjestment + "',Payment='" + Payment + "',Balance='" + Balance + "'  WHERE ([Year]='" + intYear + "') AND ([DivisionID]='" + strDiv + "') AND ([EmpNo] ='" + strEmpNo + "') ", CommandType.Text);
        }
        public void UpdateGratuitypayDataWithOutLastDate(Int32 intYear, String strDiv, String strEmpNo, Decimal PreVestingPeriod, Decimal PostVestingPeriod, Decimal AmountBF, Decimal ProvisionYear, Decimal UnderProvAdjestment, Decimal OverProvAdjestment, Decimal Payment, Decimal Balance)
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.GratuityPayData1 SET [PreVestingPeriod] ='" + PreVestingPeriod + "' ,PostVestingPeriod='" + PostVestingPeriod + "', AmountBF='" + AmountBF + "', ProvisionYear='" + ProvisionYear + "',UnderProvAdjustment='" + UnderProvAdjestment + "',OverProvAdjustment='" + OverProvAdjestment + "',Payment='" + Payment + "',Balance='" + Balance + "'  WHERE ([Year]='" + intYear + "') AND ([DivisionID]='" + strDiv + "') AND ([EmpNo] ='" + strEmpNo + "') ", CommandType.Text);
        }
        public Boolean boolConfirmedHolidaypay(Int32 intYear,String strDiv)
        {
            Boolean boolConfirmed=false;
            Int32 intEmpCount = 0;
            SqlDataReader datareader;
            datareader = SQLHelper.ExecuteReader("SELECT isnull(COUNT(EmpNo),0) AS empCount FROM dbo.HolidayPayData WHERE (Year = '"+intYear+"') AND (DivisionID = '"+strDiv+"') AND (ConfirmedYesNo = 1)", CommandType.Text);
            while (datareader.Read())
            {
                if (!datareader.IsDBNull(0))
                {
                    intEmpCount = datareader.GetInt32(0);
                }
            }
            if (intEmpCount > 0)
                boolConfirmed = true;

            return boolConfirmed;
        }

        public Boolean boolConfirmedGratuitypay(Int32 intYear, String strDiv)
        {
            Boolean boolConfirmed = false;
            Int32 intEmpCount = 0;
            SqlDataReader datareader;
            datareader = SQLHelper.ExecuteReader("SELECT     count( EmpNo) as empCount  FROM         dbo.GratuityPayData1 WHERE     (Year = '"+intYear+"') AND (DivisionId = '"+strDiv+"') AND (ConfirmYesNo = 1)", CommandType.Text);
            while (datareader.Read())
            {
                if (!datareader.IsDBNull(0))
                {
                    intEmpCount = datareader.GetInt32(0);
                }
            }
            if (intEmpCount > 0)
                boolConfirmed = true;

            return boolConfirmed;
        }

        public Boolean boolProcessedHolidaypay(Int32 intYear, String strDiv)
        {
            Boolean boolConfirmed = false;
            Int32 intEmpCount = 0;
            SqlDataReader datareader;
            datareader = SQLHelper.ExecuteReader("SELECT isnull(COUNT(EmpNo),0) AS empCount FROM dbo.HolidayPayData WHERE (Year = '" + intYear + "') AND (DivisionID = '" + strDiv + "') AND (ProcessedYesNo = 1)", CommandType.Text);
            while (datareader.Read())
            {
                if (!datareader.IsDBNull(0))
                {
                    intEmpCount = datareader.GetInt32(0);
                }
            }
            if (intEmpCount > 0)
                boolConfirmed = true;

            return boolConfirmed;
        }

        public void ResetHolidayPayData(String strDiv, Int32 intYear)
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM dbo.HolidayPayData WHERE (Year = '" + intYear + "') AND (DivisionID = '" + strDiv + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO UpdateLog(UpdatedDate,RefNo,ReferenceTable,Division,EmpNo,Narration1,Narration2,UpdatedUser	)  VALUES(getdate(),0,'HolidaypayData','" + strDiv + "','NA','" + intYear + "','Reset Holiday pay Data','" + User.StrUserName + "')", CommandType.Text);
        }

        public void CancelConfirmation(String strDiv, Int32 intYear)
        {
            SQLHelper.ExecuteNonQuery("Update HolidaypayData SET ConfirmedYesNo=0,ProcessedYesNo=0,HPQualifyDays=0,HolidayPay=0, EPF=0,ETF=0,PSS=0 ,AttQualifyDays=0,WorkPercentage=0, AttBonus=0 ,ProcessedBy='Canceled',ProcessedDate=getdate() WHERE [Year]='" + intYear + "' AND [DivisionID]='" + strDiv + "' AND ConfirmedYesNo=1", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO UpdateLog(UpdatedDate,RefNo,ReferenceTable,Division,EmpNo,Narration1,Narration2,UpdatedUser	)  VALUES(getdate(),0,'HolidaypayData','"+strDiv+"','NA','"+intYear+"','Cancel Confirmed Holiday pay Data','"+User.StrUserName+"')",CommandType.Text);
        }

        public void ConfirmHolidaypayData(Int32 intYear, String strDiv)
        {
            SQLHelper.ExecuteNonQuery("Update HolidaypayData SET ConfirmedYesNo=1,ConfirmedDate=getdate(),ConfirmedBy='"+User.StrUserName+"' WHERE [Year]='" + intYear + "' AND [DivisionID]='" + strDiv + "'", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO UpdateLog(UpdatedDate,RefNo,ReferenceTable,Division,EmpNo,Narration1,Narration2,UpdatedUser	)  VALUES(getdate(),0,'HolidaypayData','" + strDiv + "','NA','" + intYear + "','Confirmed Holiday pay Data','" + User.StrUserName + "')", CommandType.Text);
        }

        public void ConfirmGratuityPayData(Int32 intYear, String strDiv)
        {
            SQLHelper.ExecuteNonQuery("Update GratuityPayData1 SET ConfirmYesNo=1,ConfirmedDate=getdate(),ConfirmedBy='" + User.StrUserName + "' WHERE [Year]='" + intYear + "' AND [DivisionID]='" + strDiv + "'", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO UpdateLog(UpdatedDate,RefNo,ReferenceTable,Division,EmpNo,Narration1,Narration2,UpdatedUser	)  VALUES(getdate(),0,'GratuityPayData1','" + strDiv + "','NA','" + intYear + "','Confirmed gratuity pay Data','" + User.StrUserName + "')", CommandType.Text);
        }

        public String processHolidaypay(Int32 intYear, String strdiv)
        {
            //process holidaypay sp need to execute
            String state="";
            SqlParameter param = new SqlParameter();
            SqlParameter stateParam = new SqlParameter();
            List<SqlParameter> paramList=new List<SqlParameter>();
            param=SQLHelper.CreateParameter("@HPYear",SqlDbType.Int);
            param.Value=intYear;
            paramList.Add(param);
            param=SQLHelper.CreateParameter("@HPDivision",SqlDbType.VarChar,50);
            param.Value = strdiv;
            paramList.Add(param);
            param=SQLHelper.CreateParameter("@userID",SqlDbType.VarChar,50);
            param.Value=User.StrUserName;
            paramList.Add(param);
            SqlCommand cmd=new SqlCommand();
            cmd=SQLHelper.CreateCommand("[spProcessHolidaypay]",CommandType.StoredProcedure,paramList);
            stateParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            stateParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            state = stateParam.Value.ToString();
            cmd.Dispose();

            return state;
            
        }

        public DataSet ListHolidayPayData(Int32 intYear,String strdiv)
        {
            DataSet dt;
            //dt = SQLHelper.FillDataSet("SELECT dbo.HolidayPayData.Year, dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender,  dbo.HolidayPayData.NormalManDays, dbo.HolidayPayData.Earnings, dbo.HolidayPayData.Average, dbo.HolidayPayData.XXXDays, dbo.HolidayPayData.XPRDays,  dbo.HolidayPayData.XMTDays, dbo.HolidayPayData.XINDays, dbo.HolidayPayData.OtherNotOfferedDays, dbo.HolidayPayData.NotianalDays,  dbo.HolidayPayData.TotalManDays, dbo.HolidayPayData.HPQualifyDays, dbo.HolidayPayData.HolidayPay, dbo.HolidayPayData.AttQualifyDays,  dbo.HolidayPayData.EstateOffered, dbo.HolidayPayData.WorkPercentage, dbo.HolidayPayData.AttBonus, dbo.HolidayPayData.EPF, dbo.HolidayPayData.PSS FROM dbo.HolidayPayData INNER JOIN dbo.EmployeeMaster ON dbo.HolidayPayData.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.HolidayPayData.EmpNo = dbo.EmployeeMaster.EmpNo WHERE   (dbo.HolidayPayData.Year = '"+intYear+"') AND (dbo.HolidayPayData.DivisionID like '"+strdiv+"')", CommandType.Text);
            //dt = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.HolidayPayData.Year, dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, dbo.HolidayPayData.NormalManDays, dbo.HolidayPayData.Earnings, dbo.HolidayPayData.Average, dbo.HolidayPayData.XXXDays,  dbo.HolidayPayData.XPRDays, dbo.HolidayPayData.XMTDays, dbo.HolidayPayData.XINDays, dbo.HolidayPayData.OtherNotOfferedDays,  dbo.HolidayPayData.NotianalDays, dbo.HolidayPayData.TotalManDays, dbo.HolidayPayData.HPQualifyDays, dbo.HolidayPayData.HolidayPay,  dbo.HolidayPayData.AttQualifyDays, dbo.HolidayPayData.EstateOffered, dbo.HolidayPayData.WorkPercentage, dbo.HolidayPayData.AttBonus,  dbo.HolidayPayData.EPF, dbo.HolidayPayData.PSS, dbo.HolidayPayData.PaidHolidays, dbo.HolidayPayData.ScrapAmount, dbo.HolidayPayData.PSSAmount,  dbo.HolidayPayData.EPFPayableAmount, dbo.HolidayPayData.EPFPaybleAverage FROM         dbo.HolidayPayData INNER JOIN dbo.EmployeeMaster ON dbo.HolidayPayData.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.HolidayPayData.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.HolidayPayData.Year = '" + intYear + "') AND (dbo.HolidayPayData.DivisionID LIKE '" + strdiv + "') ORDER BY dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo", CommandType.Text);
            dt = SQLHelper.FillDataSet("SELECT        TOP (100) PERCENT dbo.HolidayPayData.Year, dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo, dbo.EmployeeMaster.EMPName,  dbo.EmployeeMaster.Gender, dbo.HolidayPayData.NormalManDays, dbo.HolidayPayData.Earnings, dbo.HolidayPayData.Average, dbo.HolidayPayData.XXXDays,  dbo.HolidayPayData.XPRDays, dbo.HolidayPayData.XMTDays, dbo.HolidayPayData.XINDays, dbo.HolidayPayData.OtherNotOfferedDays,  dbo.HolidayPayData.NotianalDays, dbo.HolidayPayData.TotalManDays, dbo.HolidayPayData.HPQualifyDays, dbo.HolidayPayData.HolidayPay,  dbo.HolidayPayData.AttQualifyDays, dbo.HolidayPayData.EstateOffered, dbo.HolidayPayData.WorkPercentage, dbo.HolidayPayData.AttBonus,  dbo.HolidayPayData.EPF, dbo.HolidayPayData.PSS, dbo.HolidayPayData.PaidHolidays, dbo.HolidayPayData.ScrapAmount, dbo.HolidayPayData.PSSAmount,  dbo.HolidayPayData.EPFPayableAmount, dbo.HolidayPayData.EPFPaybleAverage, dbo.EstateDivision.DivisionName, dbo.HolidayPayData.HPNetPay,  dbo.HolidayPayData.CoinsCF, dbo.HolidayPayData.HPCalcMethod FROM            dbo.HolidayPayData INNER JOIN dbo.EmployeeMaster ON dbo.HolidayPayData.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.HolidayPayData.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EstateDivision ON dbo.HolidayPayData.DivisionID = dbo.EstateDivision.DivisionID WHERE        (dbo.HolidayPayData.Year = '" + intYear + "') AND (dbo.HolidayPayData.DivisionID LIKE '" + strdiv + "') ORDER BY dbo.HolidayPayData.DivisionID, dbo.HolidayPayData.EmpNo", CommandType.Text);

            return dt;
        }

        public DataSet ListHolidaypayMandays(Int32 intYear, String strdiv)
        {
            DataSet ds;
            ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT YEAR(dbo.DailyGroundTransactions.DateEntered) AS Expr1, MONTH(dbo.DailyGroundTransactions.DateEntered) AS Expr2,  dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  SUM(dbo.DailyGroundTransactions.ManDays - dbo.DailyGroundTransactions.HolidayHalfNames) AS Mandays FROM         dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '1/1/2012', 102) AND CONVERT(DATETIME, DATEADD(dd, - 1, DATEADD(yy, 1,  '1/1/' + '"+intYear+"')), 102)) AND (dbo.DailyGroundTransactions.WorkType = 1) GROUP BY YEAR(dbo.DailyGroundTransactions.DateEntered), MONTH(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName HAVING (dbo.DailyGroundTransactions.DivisionID = '"+strdiv+"') ORDER BY Expr1, Expr2, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            return ds;
        }

        public DataSet ListHolidaypayEarnings(Int32 intYear, String strdiv)
        {
            DataSet ds;
            ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT YEAR(dbo.DailyGroundTransactions.DateEntered) AS Expr1, MONTH(dbo.DailyGroundTransactions.DateEntered) AS Expr2,  dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName,  SUM(dbo.DailyGroundTransactions.ManDays - dbo.DailyGroundTransactions.HolidayHalfNames) AS Mandays,  SUM(dbo.DailyGroundTransactions.DailyBasic + dbo.DailyGroundTransactions.OverKgAmount + dbo.DailyGroundTransactions.ExtraRates) AS Earnings FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '1/1/2012', 102) AND CONVERT(DATETIME, DATEADD(dd, - 1, DATEADD(yy, 1,  '1/1/' + '" + intYear + "')), 102)) AND (dbo.DailyGroundTransactions.WorkType = 1) GROUP BY YEAR(dbo.DailyGroundTransactions.DateEntered), MONTH(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EMPName HAVING      (dbo.DailyGroundTransactions.DivisionID = '" + strdiv + "') ORDER BY Expr1, Expr2, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            return ds;
        }

        public DataSet ListHolidaypayMandaysEarningsNotOffered(Int32 intYear, String strdiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT YEAR(dbo.DailyGroundTransactions.DateEntered) AS Expr1, MONTH(dbo.DailyGroundTransactions.DateEntered) AS Expr2,  dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.Gender,  dbo.EmployeeMaster.EMPName, SUM(dbo.DailyGroundTransactions.ManDays - dbo.DailyGroundTransactions.HolidayHalfNames) AS Mandays,  SUM(dbo.DailyGroundTransactions.DailyBasic + dbo.DailyGroundTransactions.OverKgAmount + dbo.DailyGroundTransactions.ExtraRates+dbo.DailyGroundTransactions.PSSAmount+dbo.DailyGroundTransactions.ScrapKgAmount) AS Earnings,  SUM(CASE WHEN (WorkCodeID IN ('XXX')) THEN 1 ELSE 0 END) AS XXXDays, SUM(CASE WHEN (WorkCodeID IN ('XPR')) THEN 1 ELSE 0 END) AS XPRDays,  SUM(CASE WHEN (WorkCodeID IN ('XMT')) THEN 1 ELSE 0 END) AS XMTDays, SUM(CASE WHEN (WorkCodeID IN ('XIN')) THEN 1 ELSE 0 END) AS XINDays,  0 AS OtherNotOfferedDays, SUM(CASE WHEN (WorkCodeID IN ('XPR', 'XIN', 'XMT', 'XXX')) THEN 1 ELSE 0 END) AS NotianalDays,(SELECT NotianalDays FROM dbo.HolidayPayData WHERE (Year = YEAR(dbo.DailyGroundTransactions.DateEntered)) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID) AND (EmpNo = dbo.DailyGroundTransactions.EmpNo)) As FinalNotianal FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '1/1/'+ '" + intYear + "', 102) AND CONVERT(DATETIME, DATEADD(dd, - 1, DATEADD(yy, 1,  '1/1/' + '" + intYear + "')), 102)) AND (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.EmployeeMaster.EmpCategory = 1) GROUP BY YEAR(dbo.DailyGroundTransactions.DateEntered), MONTH(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EMPName HAVING      (dbo.DailyGroundTransactions.DivisionID like '" + strdiv + "') ORDER BY Expr1, Expr2, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            da.Fill(ds, "ManDaysEarningsNotOffered");
            return ds;
        }
        public DataSet ListHolidaypayMandaysEarningsNotOffered(DateTime dtFrom,DateTime dtTo, String strdiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT     TOP (100) PERCENT YEAR(dbo.DailyGroundTransactions.DateEntered) AS Expr1, MONTH(dbo.DailyGroundTransactions.DateEntered) AS Expr2,  dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.Gender,  dbo.EmployeeMaster.EMPName, SUM(dbo.DailyGroundTransactions.ManDays - dbo.DailyGroundTransactions.HolidayHalfNames) AS Mandays,  SUM(dbo.DailyGroundTransactions.DailyBasic + dbo.DailyGroundTransactions.OverKgAmount + dbo.DailyGroundTransactions.ExtraRates+dbo.DailyGroundTransactions.PSSAmount+dbo.DailyGroundTransactions.ScrapKgAmount) AS Earnings,  SUM(CASE WHEN (WorkCodeID IN ('XXX')) THEN 1 ELSE 0 END) AS XXXDays, SUM(CASE WHEN (WorkCodeID IN ('XPR')) THEN 1 ELSE 0 END) AS XPRDays,  SUM(CASE WHEN (WorkCodeID IN ('XMT')) THEN 1 ELSE 0 END) AS XMTDays, SUM(CASE WHEN (WorkCodeID IN ('XIN')) THEN 1 ELSE 0 END) AS XINDays,  0 AS OtherNotOfferedDays, SUM(CASE WHEN (WorkCodeID IN ('XPR', 'XIN', 'XMT', 'XXX')) THEN 1 ELSE 0 END) AS NotianalDays,(SELECT NotianalDays FROM dbo.HolidayPayData WHERE (Year = YEAR(dbo.DailyGroundTransactions.DateEntered)) AND (DivisionID = dbo.DailyGroundTransactions.DivisionID) AND (EmpNo = dbo.DailyGroundTransactions.EmpNo)) As FinalNotianal FROM dbo.DailyGroundTransactions INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactions.DivisionID = dbo.EmployeeMaster.DivisionID AND  dbo.DailyGroundTransactions.EmpNo = dbo.EmployeeMaster.EmpNo WHERE     (dbo.DailyGroundTransactions.DateEntered BETWEEN CONVERT(DATETIME, '"+dtFrom+"', 102) AND CONVERT(DATETIME, '"+dtTo+"', 102)) AND (dbo.DailyGroundTransactions.WorkType = 1) AND (dbo.EmployeeMaster.EmpCategory = 1) GROUP BY YEAR(dbo.DailyGroundTransactions.DateEntered), MONTH(dbo.DailyGroundTransactions.DateEntered), dbo.DailyGroundTransactions.DivisionID,  dbo.DailyGroundTransactions.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EMPName HAVING      (dbo.DailyGroundTransactions.DivisionID like '" + strdiv + "') ORDER BY Expr1, Expr2, dbo.DailyGroundTransactions.DivisionID, dbo.DailyGroundTransactions.EmpNo", CommandType.Text);
            da.Fill(ds, "ManDaysEarningsNotOffered");
            return ds;
        }

        
        

        public DataSet GetHolidayPayDateRange(Int32 intYear)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT TOP (200) HPYear, FromDate, ToDate FROM HolidayPayYear WHERE (HPYear = '" + intYear + "')", CommandType.Text);
            return ds;
        }

        //public String InsertGratuityPayData(Int32 intYear, String strDiv, Boolean ReEnterYesNO)
        //{
        //    String Status = "";
        //    SqlParameter param = new SqlParameter();
        //    SqlParameter statusParam = new SqlParameter();
        //    List<SqlParameter> paramList = new List<SqlParameter>();
        //    param = SQLHelper.CreateParameter("@PayYear", SqlDbType.Int);
        //    param.Value = intYear;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
        //    param.Value = strDiv;
        //    paramList.Add(param);
        //    param = SQLHelper.CreateParameter("@ReEnter", SqlDbType.Bit);
        //    param.Value = ReEnterYesNO;
        //    paramList.Add(param);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = SQLHelper.CreateCommand("[spInsertHolidayPayData]", CommandType.StoredProcedure, paramList);
        //    statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
        //    statusParam.Direction = ParameterDirection.Output;
        //    SQLHelper.ExecuteNonQuery(cmd);
        //    Status = statusParam.Value.ToString();
        //    cmd.Dispose();
        //    return Status;
        //}

        public String InsertGratuityPayData(Int32 intYear, String strDiv, Boolean ReEnterYesNO, String Emp)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@GratuityYear", SqlDbType.Int);
            param.Value = intYear;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar, 50);
            param.Value = strDiv;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ReEnter", SqlDbType.Bit);
            param.Value = ReEnterYesNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar, 50);
            param.Value = Emp;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("[spInsertGratuityPayData]", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
        }

        public DataTable GetGratuityPayData(String strDiv, Int32 intYear)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));//0
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EpfNo"));
            dt.Columns.Add(new DataColumn("EmpName"));//4
            dt.Columns.Add(new DataColumn("DateOfBirth"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("LastDateOfWork"));
            dt.Columns.Add(new DataColumn("PrevestingPeriod"));//8
            dt.Columns.Add(new DataColumn("PostvestingPeriod"));
            dt.Columns.Add(new DataColumn("AmountBF"));
            dt.Columns.Add(new DataColumn("ProvisionYear"));//11
            dt.Columns.Add(new DataColumn("UnderProvAdjestment"));
            dt.Columns.Add(new DataColumn("OverProvAdjestment"));
            dt.Columns.Add(new DataColumn("Payment"));
            dt.Columns.Add(new DataColumn("Balance"));//15         
            
            DataRow dtrow;
            SqlDataReader dataReader;
            SqlDataReader dataReaderNew;

            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader(" Select Year, DivisionId, EmpNo, EPFNo, EmpName, DateOfBirth, DateJoined, case when(LastDateOfWork is null) then getdate() else LastDateOfWork end as LastDateOfWork, PreVestingPeriod, PostVestingPeriod, AmountBF,  ProvisionYear, UnderProvAdjustment, OverProvAdjustment, Payment, Balance FROM dbo.GratuityPayData1 WHERE     (Year = '" + intYear + "') AND (DivisionId = '" + strDiv + "') AND (ConfirmYesNo = 0)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
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
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDateTime(5).ToShortDateString();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6).ToShortDateString();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7).ToShortDateString();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDecimal(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }
                

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            return dt;
        }

        public DataTable GetGratuityPayDataReportData(String strDiv, Int32 intYear)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));//0
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EpfNo"));
            dt.Columns.Add(new DataColumn("EmpName"));//4
            dt.Columns.Add(new DataColumn("DateOfBirth"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("LastDateOfWork"));
            dt.Columns.Add(new DataColumn("PrevestingPeriod"));//8
            dt.Columns.Add(new DataColumn("PostvestingPeriod"));
            dt.Columns.Add(new DataColumn("AmountBF"));
            dt.Columns.Add(new DataColumn("ProvisionYear"));//11
            dt.Columns.Add(new DataColumn("UnderProvAdjestment"));
            dt.Columns.Add(new DataColumn("OverProvAdjestment"));
            dt.Columns.Add(new DataColumn("Payment"));
            dt.Columns.Add(new DataColumn("Balance"));//15         

            DataRow dtrow;
            SqlDataReader dataReader;
            SqlDataReader dataReaderNew;

            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT [Year] ,[EstateID] ,[DivisionID] ,[EmpNo] ,[ManDays] ,[HolidayHalfNames] ,[DailyBasic] ,[OverKgAmount] ,[ExtraRates] ,[Earnings] ,[NormalManDays] ,[Average] ,[XXXDays] ,[XPRDays] ,[XMTDays] ,[XINDays] ,[OtherNotOfferedDays] ,[NotianalDays] ,[TotalManDays]  FROM [dbo].[HolidayPayData] WHERE (Year = '"+intYear+"')  AND (DivisionID = '"+strDiv+"')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader(" Select Year, DivisionId, EmpNo, EPFNo, EmpName, DateOfBirth, DateJoined, case when(LastDateOfWork is null) then getdate() else LastDateOfWork end as LastDateOfWork, PreVestingPeriod, PostVestingPeriod, AmountBF,  ProvisionYear, UnderProvAdjustment, OverProvAdjustment, Payment, Balance FROM dbo.GratuityPayData1 WHERE     (Year = '" + intYear + "') AND (DivisionId like '" + strDiv + "') AND (ConfirmYesNo = 1)", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
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
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetDateTime(5).ToShortDateString();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6).ToShortDateString();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7).ToShortDateString();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetDecimal(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetDecimal(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDecimal(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDecimal(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDecimal(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetDecimal(15);
                }


                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            return dt;
        }

        public Boolean IsOfferedEditable()
        {
            Boolean boolEditable = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Code FROM dbo.FTSCheckRollSettings WHERE (Name = 'OfferedEdit')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    if (Convert.ToInt32(dataReader.GetInt32(0).ToString()) == 1)
                    {
                        boolEditable = true;
                    }
                }
            }
            dataReader.Close();
            return boolEditable;
        }
        public Boolean IsColumnEditable(String ColName)
        {
            Boolean boolEditable = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT Code FROM dbo.FTSCheckRollSettings WHERE (Name = '" + ColName + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    if (Convert.ToInt32(dataReader.GetInt32(0).ToString()) == 1)
                    {
                        boolEditable = true;
                    }
                }
            }
            dataReader.Close();
            return boolEditable;
        }

        public void UpdateAttBonus(Int32 intYear, String strDiv, String strEmp,Decimal Amount,Decimal AvailAmt )
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.HolidayPayData SET AttBonus='" + Amount + "' WHERE Year='" + intYear + "' AND DivisionID='" + strDiv + "' and EmpNo='" + strEmp + "'", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO UpdateLog(UpdatedDate,RefNo,ReferenceTable,Division,EmpNo,Narration1,Narration2,UpdatedUser,Narration3,Narration4	)  VALUES(getdate(),0,'UpdateAttBonus','" + strDiv + "','" + strEmp + "','" + intYear + "','Update Attendance Bonus','" + User.StrUserName + "','" + AvailAmt + "' ,'" + Amount + "')", CommandType.Text);
        }

        public Decimal getAvailableAttIncentiveAmount(Int32 intYear, String strDiv, String strEmp)
        {
            Decimal decAvail = 0;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT AttBonus  FROM dbo.HolidayPayData WHERE     (Year = '" + intYear + "') AND (DivisionID = '" + strDiv + "') AND (EmpNo = '" + strEmp + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {                   
                        decAvail = Convert.ToDecimal(dataReader.GetDecimal(0));
                }
            }
            dataReader.Close();
            return decAvail;
        }

        public DataSet getJRLHolidayPayData(Int32 intYear)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT     DivisionID, EmpNo, DataYear, right('00'+Convert(varchar(50),DataMonth),2), SUM(DailyBasic) AS DailyBasic, SUM(ExtraRate) AS ExtraRates, SUM(OkgAmount) AS OkgAmount, SUM(DailyBasic)  + SUM(ExtraRate) + SUM(OkgAmount) AS totalEarnings, SUM(ManDays) AS TotalManDays FROM dbo.ChkHolidaypayJRLProcessedData GROUP BY DivisionID, EmpNo, DataYear, DataMonth HAVING  (DataYear = 2013)  AND   (SUM(DailyBasic) + SUM(ExtraRate) + SUM(OkgAmount) > 0)", CommandType.Text);
            da.Fill(ds, "JRLHolidayPayData");
            return ds;
        }

        public DataSet getHPData(Int32 intYear,String strDiv)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand("SELECT TOP (100) PERCENT DivisionID, EmpNo, YEAR(DateEntered) AS Expr1, MONTH(DateEntered) AS Expr2, SUM(ManDays) AS Expr3, SUM(DailyBasic) AS Expr4,  SUM(OverKgAmount) AS Expr5, SUM(ExtraRates) AS Expr6 FROM dbo.DailyGroundTransactions WHERE     (YEAR(DateEntered) = '"+intYear+"') GROUP BY DivisionID, EmpNo, YEAR(DateEntered), MONTH(DateEntered) HAVING      (SUM(ManDays) > 0) AND (DivisionID = '"+strDiv+"')", CommandType.Text);
            da.Fill(ds, "hpData");
            return ds;
        }

        public void InsertEmployeeToHolidaypay(String strDiv, String strEmp,Int32 intYear,String strEst)
        {
            for (int i = 1; i < 13; i++)
            {
                if (!CheckAvailabilityOfEmp(strDiv, strEmp, intYear, i,strEst))
                {
                    SQLHelper.ExecuteNonQuery("INSERT INTO DailyGroundTransactions (EstateID,DateEntered,CropType,WorkType,DivisionID,FieldID,BlockID,WorkCodeID, EmpNo,EPFNo,WorkQty,WorkQty1,WorkQty2,WorkQty3,AreaCovered,FieldWeight,ManDays,TaskCompleted,LabourType,LabourEstate,LabourDivision,LabourField,DailyBasic,OverKgs,CashKgs,OverKgAmount,CashKgAmount,	CashSundryAmount,FullHalf,UserID,CreateDateTime,PRIAmount,ExtraRates,PluckingExpenditure,Expenditure,HolidayYesNo,EPF10, EPF12, ETF3,NormKilos,CashManDays,HolidayHalfNames,ExtraName,PaidHoliday,CashBlockYesNo,BlockPlkAmount,BlockPlkKgs,Contractor,IsContract,ContractorRate,ContractorPay,EmpRef,BlockPlk2013,EasyweighYesNo,SpeciallHalfYesNo) VALUES	('"+strEst+"',Convert(datetime,convert(varchar(50),'"+intYear+"')+'-'+convert(varchar(50),'"+i+"')+'-1'),1,1,'"+strDiv+"','na',	'na','na', '"+strEmp+"','na',	0,0,0,0,0,0,0,1,'General','NA','NA','NA',0,0,0,0,0,0, 2,'admin',getdate(),0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'NA',0,0,0,'Emp',0,0,0)", CommandType.Text);
                }
            }
        }

        public Boolean CheckAvailabilityOfEmp(String strDiv1, String strEmp1, Int32 intYear1, Int32 intMonth1,String strEst1)
        {
            DataSet  ds;
            ds = SQLHelper.FillDataSet("SELECT DateEntered, DivisionID, EmpNo, WorkCodeID FROM dbo.DailyGroundTransactions WHERE (YEAR(DateEntered) = '" + intYear1 + "') AND (MONTH(DateEntered) = '" + intMonth1 + "') AND (DivisionID = '" + strDiv1 + "') AND (EmpNo = '" + strEmp1 + "')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ListDailyHolidayPayData(String DivisionID, Int32 intYear, Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("EmpNO");
            dt.Columns.Add("ManDays");
            dt.Columns.Add("HolidayHalf");
            dt.Columns.Add("DailyBasic");
            dt.Columns.Add("OverKgAmount");
            dt.Columns.Add("ExtraRates");
            SqlDataReader dataReader;
            DataRow dtRow;

            dataReader = SQLHelper.ExecuteReader("SELECT     YEAR(DateEntered) AS Expr1, MONTH(DateEntered) AS Expr2, EmpNo, ManDays, HolidayHalfNames, DailyBasic, OverKgAmount, ExtraRates FROM dbo.DailyGroundTransactions WHERE (YEAR(DateEntered) = '"+intYear+"') AND (MONTH(DateEntered) = '"+intMonth+"') AND (DivisionID = '"+DivisionID+"')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[2] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetDecimal(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetDecimal(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetDecimal(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtRow[7] = dataReader.GetDecimal(7);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public void UpdateDailyHolidayPayData(String strDiv, String EmpNO, Int32 HpYear, Int32 HpMonth,Decimal decManDays,Decimal decHoliHalfDays,Decimal decDailyBasic,Decimal decOverKgAmount,Decimal decExtraRates)
        {
            // SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + EmpNO + "',  '" + Adjustedvalue + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("update dbo.DailyGroundTransactions set ManDays='"+decManDays+"',HolidayHalfNames='"+decHoliHalfDays+"',DailyBasic='"+decDailyBasic+"', OverKgAmount='"+decOverKgAmount+"', ExtraRates='"+decExtraRates+"' WHERE     (YEAR(DateEntered) = '"+HpYear+"') AND (MONTH(DateEntered) = '"+HpMonth+"') AND (DivisionID = '"+strDiv+"') AND (EmpNo = '"+EmpNO+"') ", CommandType.Text);
        }





  

    }
}
