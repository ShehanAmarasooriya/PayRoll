using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{

    public class OfferedDays
    {
        private Int32 intOfferedYear;

        public Int32 IntOfferedYear
        {
            get { return intOfferedYear; }
            set { intOfferedYear = value; }
        }
        private Int32 intOfferedMonth;

        public Int32 IntOfferedMonth
        {
            get { return intOfferedMonth; }
            set { intOfferedMonth = value; }
        }
        private Int32 intOfferedDays;

        public Int32 IntOfferedDays
        {
            get { return intOfferedDays; }
            set { intOfferedDays = value; }
        }
        private Int32 intMaleOffered;

        public Int32 IntMaleOffered
        {
            get { return intMaleOffered; }
            set { intMaleOffered = value; }
        }
        private Int32 intFemaleOffered;

        public Int32 IntFemaleOffered
        {
            get { return intFemaleOffered; }
            set { intFemaleOffered = value; }
        }

        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }

        public void InsertOfferedDays()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKMonthlyOfferedDays]([Year],[Month],[MaleOfferedDays],[FemaleOfferedDays],[UserId],DivisionId) VALUES ('" + IntOfferedYear + "','" + IntOfferedMonth + "','" + IntMaleOffered + "','" + IntFemaleOffered + "','" + FTSPayRollBL.User.StrUserName + "','" + StrDivision + "')", CommandType.Text);
        }
        public void UpdateOfferedDays()
        {
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[CHKMonthlyOfferedDays] SET [MaleOfferedDays]='"+IntMaleOffered+"',[FemaleOfferedDays]='"+IntFemaleOffered+"',[UpdateDateTime]='"+DateTime.Now.Date.ToShortDateString()+"' WHERE ( Year= '" + IntOfferedYear + "' and Month='" + IntOfferedMonth + "' and DivisionId='"+StrDivision+"')", CommandType.Text);
        }
        public void DeleteOfferedDays()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKMonthlyOfferedDays WHERE(( Year= '" + IntOfferedYear + "' and Month='" + IntOfferedMonth + "' and DivisionId='"+StrDivision+"')", CommandType.Text);
        }
        public DataTable ListAllOffered()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Year"));
            dt.Columns.Add(new DataColumn("Month"));
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("MaleOffered"));
            dt.Columns.Add(new DataColumn("FemaleOffered"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT [Year],[Month],DivisionId,[MaleOfferedDays],[FemaleOfferedDays] FROM [dbo].[CHKMonthlyOfferedDays]", CommandType.Text);
            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetInt32(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetInt32(4);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }
        public Boolean GetMonthOfferedDayState(Int32 intY,Int32 intM,String strDiv)
        {
            Boolean ProcessedYN = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT ProcessedYesNO FROM dbo.CHKMonthlyOfferedDays WHERE (Year = '"+intY+"') AND (Month = '"+intM+"') AND (DivisionId='"+strDiv+"')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    ProcessedYN = dataReader.GetBoolean(0);
                }
            }
            dataReader.Close();
            return ProcessedYN;
        }

       

    }
}
