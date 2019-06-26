using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace FTSPayRollBL
{
    public class 
        
        MonthlyHoliday
    {
        private DateTime dtday;

        public DateTime Dtday
        {
        get { return dtday; }
        set { dtday = value; }
        }

        private Int32 intYear;

        public Int32 IntYear
        {
            get { return intYear; }
            set { intYear = value; }
        }

        private String strMonth;

        public String StrMonth
        {
            get { return strMonth; }
            set { strMonth = value; }
        }

        private String strHoliType;

        public String StrHoliType
        {
            get { return strHoliType; }
            set { strHoliType = value; }
        }

        private String strHoliName;

        public String StrHoliName
        {
            get { return strHoliName; }
            set { strHoliName = value; }
        }

        private Int32 intMID;

        public Int32 IntMID
        {
            get { return intMID; }
            set { intMID = value; }
        }

        public void InsertMHoliday()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO MonthlyHolidays (Date, Year, Month, HolidayType, HolidayName, UserID) VALUES('" + Dtday + "','" + IntYear + "','" + IntMID + "','" + StrHoliType + "','" + StrHoliName + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void DeleteMHoliday()
        {
            SQLHelper.ExecuteNonQuery("DELETE MonthlyHolidays Where (Date = '" + Dtday + "') AND (HolidayType = '" + StrHoliType + "') ", CommandType.Text);
        }


        public DataTable ListMHoliday(Int32 intYear,Int32 intMonth)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");            
            dt.Columns.Add("HolidayType");
            dt.Columns.Add("HolidayName");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("UserID");
            //dt.Columns.Add("CreatedDate");

            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Date, HolidayType, HolidayName, Year, Month, UserID FROM dbo.MonthlyHolidays WHERE (Year = '" + intYear + "') AND (Month = '" + intMonth + "') ORDER BY Date DESC", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetDateTime(0);
                }                
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListMHoliday(Int32 intYear)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("HolidayType");
            dt.Columns.Add("HolidayName");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("UserID");
            //dt.Columns.Add("CreatedDate");

            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT Date, HolidayType, HolidayName, Year, Month, UserID FROM dbo.MonthlyHolidays WHERE (Year = '" + intYear + "')  ORDER BY Date DESC", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetString(5).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet GetMonthHolidaysYear(Int32 intYear)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT Date, Year, Month, HolidayType, HolidayName FROM  dbo.MonthlyHolidays WHERE (Year = '" + intYear + "') Order By Year, Month", CommandType.Text);
            return ds;
        }

        public DataSet GetMonthHolidaysMonth(Int32 intMonth)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT Date, Year, Month, HolidayType, HolidayName FROM  dbo.MonthlyHolidays WHERE (Month = '" + intMonth + "') Order By Year, Month", CommandType.Text);
            return ds;
        }

        public DataSet GetMonthHolidays(Int32 intYear, Int32 intMonth)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT TOP (100) PERCENT Date, Year, Month, HolidayType, HolidayName FROM  dbo.MonthlyHolidays WHERE (Year = '" + intYear + "') AND (Month = '" + intMonth + "') Order By Year, Month", CommandType.Text);
            return ds;
        }

        public DataSet GetPoyaNSunday(DateTime dt)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT Date, HolidayType FROM dbo.MonthlyHolidays WHERE (Date = '" + dt + "') AND ((HolidayType = 'Sunday') OR (HolidayType = 'Poya Day'))", CommandType.Text);
            return ds;
        }

        public DataTable ListHolidayType()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            

            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT Code, Name, Type FROM dbo.FTSCheckRollSettings WHERE (Type = 'Holiday Type')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
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

        public DataTable ListHolidayType(String Holiday)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");


            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT Code, Name, Type FROM dbo.FTSCheckRollSettings WHERE (Type = 'Holiday Type') AND (Name='" + Holiday + "')", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
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

        public DataSet GetPaidHoli(DateTime dt)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT Date, HolidayType FROM dbo.MonthlyHolidays WHERE (Date = '" + dt + "') AND (HolidayType = 'Paid Holiday')", CommandType.Text);
            return ds;
        }

        public Boolean IsPaidHoliday(DateTime enteredDate)
        {
            Boolean PaidHolidayYesNo = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM dbo.MonthlyHolidays WHERE (Date = '" + enteredDate + "') AND (HolidayType = 'Paid Holiday')",CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PaidHolidayYesNo = true;
            }
            else
            {
                PaidHolidayYesNo = false;
            }
            return PaidHolidayYesNo;
        }
        public Boolean IsPoyaday(DateTime enteredDate)
        {
            Boolean PoyaDayYesNo = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM dbo.MonthlyHolidays WHERE (Date = '" + enteredDate + "') AND (HolidayType = 'Poya Day')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PoyaDayYesNo = true;
            }
            else
            {
                PoyaDayYesNo = false;
            }
            return PoyaDayYesNo;
        }

        public Boolean IsSunday(DateTime enteredDate)
        {
            Boolean PoyaDayYesNo = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM dbo.MonthlyHolidays WHERE (Date = '" + enteredDate + "') AND (HolidayType = 'Sunday')", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PoyaDayYesNo = true;
            }
            else
            {
                PoyaDayYesNo = false;
            }
            return PoyaDayYesNo;
        }

        public Boolean IsGeneralHoliday(DateTime enteredDate)
        {
            Boolean HolidayYesNo = false;
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM dbo.MonthlyHolidays WHERE (Date = '" + enteredDate + "') ", CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                HolidayYesNo = true;
            }

            return HolidayYesNo;
        }
    }
}
