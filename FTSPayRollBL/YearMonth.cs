using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class YearMonth
    {
        public DataTable ListMonths()
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("MId");
            dt.Columns.Add("Month");

            DataRow drow;
            drow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT [MId],[Month] FROM [dbo].[CHKMonths] GROUP BY MId, Month", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT DISTINCT MId, MAX(Month) AS Expr1 FROM  dbo.CHKMonths GROUP BY MId", CommandType.Text);
            while(dataReader.Read())
            {
                drow=dt.NewRow();
                if(!dataReader.IsDBNull(0))
                {
                    drow[0]=dataReader.GetInt32(0);
                }
                if(!dataReader.IsDBNull(1))
                {
                    drow[1]=dataReader.GetString(1);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public Int32 getLastYearID()
        {
            Int32 intYear = 1;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) PERCENT Year, MId FROM dbo.CHKMonths ORDER BY Year DESC, MId DESC", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intYear = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return intYear;
        }

        public Int32 getLastMonthID()
        {
            Int32 intMId = 1;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (1) PERCENT Year, MId FROM dbo.CHKMonths ORDER BY Year DESC, MId DESC", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(1))
                {
                    intMId = dataReader.GetInt32(1);
                }
            }
            dataReader.Close();
            return intMId;
        }

        public DataTable GetLastYearMonth()
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Yid");
            dt.Columns.Add("Month");

            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (1) PERCENT Year, MId FROM dbo.CHKMonths ORDER BY Year DESC, MId DESC", CommandType.Text);
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


         public DataTable ListYears()
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("YId");
            dt.Columns.Add("Year");

            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT YId, Year FROM   dbo.CHKYear", CommandType.Text);
            while(dataReader.Read())
            {
                drow=dt.NewRow();
                if(!dataReader.IsDBNull(0))
                {
                    drow[0]=dataReader.GetInt32(0);
                }
                if(!dataReader.IsDBNull(1))
                {
                    drow[1]=dataReader.GetInt32(1);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListYearsFromCHKMonths()
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Year");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT Year FROM dbo.CHKMonths GROUP BY Year", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListMonths(Int32 YearVal)
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("MId");
            dt.Columns.Add("Month");
            dt.Columns.Add("MonthKey");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT MId, Month, MonthKey FROM CHKMonths WHERE (Year = '" + YearVal + "') ORDER BY MId", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetInt32(2);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListJRLMonths(Int32 YearVal)
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("MId");
            dt.Columns.Add("Month");
            dt.Columns.Add("MonthKey");
            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT MId, Month, MonthKey FROM CHKMonths WHERE (Year = '" + YearVal + "') and (System='JRL') ORDER BY MId", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetInt32(2);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getOpenCloseDates(Int32 monthKey,Int32 yearKey)
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("YId");
            dt.Columns.Add("Year");
            dt.Columns.Add("MId");

            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT   OpenDate, CloseDate, MId FROM dbo.CHKMonths WHERE (MonthKey  = '" + monthKey + "') and (Year ='" + yearKey + "')", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetDateTime(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetInt32(2);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable GetOpenCloseDates(Int32 monthKey, Int32 yearKey)
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("YId");
            dt.Columns.Add("Year");
            dt.Columns.Add("MId");

            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT   OpenDate, CloseDate, MId FROM dbo.CHKMonths WHERE (MID  = '" + monthKey + "') and (Year ='" + yearKey + "')", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    drow[1] = dataReader.GetDateTime(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    drow[2] = dataReader.GetInt32(2);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListMonthsName()
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            //dt.Columns.Add("MId");
            dt.Columns.Add("Month");

            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT [Month] FROM [dbo].[CHKMonths]", CommandType.Text);
            while (dataReader.Read())
            {
                drow = dt.NewRow();                
                if (!dataReader.IsDBNull(0))
                {
                    drow[0] = dataReader.GetString(0);
                }
                dt.Rows.Add(drow);
            }
            dataReader.Close();
            return dt;
        }

        public Int32 GetMonthIdByMonthName(String MName)
        {
            Int32 intMId=0; 
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT MId FROM dbo.CHKMonths WHERE (Month = '" + MName + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    intMId = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return intMId;
        }

        public DataTable GetPreviousMonth()
        {
            DataTable dt = new DataTable();
            SqlDataReader dataReader;
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");

            DataRow drow;
            drow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (1) Year, MId FROM dbo.CHKMonths ORDER BY Year DESC, MId DESC", CommandType.Text);
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
      

    }
}
