using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    
    public class DailyFieldSummary
    {
        DateTime dtDate;

        public DateTime DtDate
        {
            get { return dtDate; }
            set { dtDate = value; }
        }
        String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        String strFiled;

        public String StrFiled
        {
            get { return strFiled; }
            set { strFiled = value; }
        }
        String strChitNo;

        public String StrChitNo
        {
            get { return strChitNo; }
            set { strChitNo = value; }
        }
        String strJob;

        public String StrJob
        {
            get { return strJob; }
            set { strJob = value; }
        }
        Decimal decFieldWeight;

        public Decimal DecFieldWeight
        {
            get { return decFieldWeight; }
            set { decFieldWeight = value; }
        }
        Decimal decAreaCovered;

        public Decimal DecAreaCovered
        {
            get { return decAreaCovered; }
            set { decAreaCovered = value; }
        }
        Decimal decManDays;

        public Decimal DecManDays
        {
            get { return decManDays; }
            set { decManDays = value; }
        }

        private String strGangNo;

        public String StrGangNo
        {
            get { return strGangNo; }
            set { strGangNo = value; }
        }
        private String strMainACCode;

        public String StrMainACCode
        {
            get { return strMainACCode; }
            set { strMainACCode = value; }
        }
        private Int32 intMusterChitNumber;

        public Int32 IntMusterChitNumber
        {
            get { return intMusterChitNumber; }
            set { intMusterChitNumber = value; }
        }

        private String strLabourDivision;

        public String StrLabourDivision
        {
            get { return strLabourDivision; }
            set { strLabourDivision = value; }
        }
        private String strLabourField;

        public String StrLabourField
        {
            get { return strLabourField; }
            set { strLabourField = value; }
        }
        private String strLabourType;

        public String StrLabourType
        {
            get { return strLabourType; }
            set { strLabourType = value; }
        }

        public void InsertDailyFieldSummary()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[DailyFieldSummary] ([DateEntered] ,[DivisionID] ,[ChitNumber] ,[Job] ,[FieldID] ,[AreaCovered] ,[FieldWeight] ,[ManDays] ,[CreatedDateTime] ,[UserID],[GangNumber],[MainACCode],MusterChitNumber) VALUES ('" + DtDate + "' ,'" + StrDivision + "' ,'" + StrChitNo + "' ,'" + StrJob + "' ,'" + StrFiled + "' ,'" + DecAreaCovered + "' ,'" + DecFieldWeight + "' ,'" + DecManDays + "',getdate(),'" + User.StrUserName + "','" + StrGangNo + "','" + StrMainACCode + "','"+IntMusterChitNumber+"')", CommandType.Text);
        }
        public void UpdateDailyFieldSummary()
        {
            SQLHelper.ExecuteNonQuery("UPDATE [dbo].[DailyFieldSummary] SET [AreaCovered]='" + DecAreaCovered + "',[FieldWeight]='" + DecFieldWeight + "',[ManDays]='" + DecManDays + "' WHERE ([DateEntered]='" + DtDate + "') AND ([DivisionID]='" + StrDivision + "') AND ([FieldID]='" + StrFiled + "') AND ([Job]='" + StrJob + "') AND ([ChitNumber]='" + StrChitNo + "') AND ([GangNumber]='" + StrGangNo + "') AND ([MainACCode]='" + StrMainACCode + "' ) AND (MusterChitNumber='" + IntMusterChitNumber + "')", CommandType.Text);
        }
        public void DeleteDailyFieldSummary()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM  [dbo].[DailyFieldSummary] WHERE ([DateEntered]='" + DtDate + "') AND ([DivisionID]='" + StrDivision + "') AND ([FieldID]='" + StrFiled + "') AND ([Job]='" + StrJob + "') AND ([ChitNumber]='" + StrChitNo + "') AND ([GangNumber]='" + StrGangNo + "') AND ([MainACCode]='" + StrMainACCode + "')  AND (MusterChitNumber='" + IntMusterChitNumber + "')", CommandType.Text);
        }
        public DataTable ListDailyFieldSummary(String DivID, DateTime dtDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DateEntered");
            dt.Columns.Add("FieldId");
            dt.Columns.Add("ChitNumber");
            dt.Columns.Add("GangNumber");
            dt.Columns.Add("Job");
            dt.Columns.Add("AreaCovered");
            dt.Columns.Add("FieldWeight");
            dt.Columns.Add("ManDays");
            dt.Columns.Add("MusterChitRef");
            dt.Columns.Add("MainACCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     DateEntered, FieldID, ChitNumber,GangNumber, Job, AreaCovered, FieldWeight, ManDays,MusterChitNumber,MainACCode FROM dbo.DailyFieldSummary WHERE     (DivisionID = '" + DivID + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) ", CommandType.Text);
            dtRow = dt.NewRow();
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
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
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtRow[9] = dataReader.GetString(9).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListDailyFieldSummary(String DivID, DateTime dtDate, Int32 intSelectedMChit)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DateEntered");
            dt.Columns.Add("FieldId");
            dt.Columns.Add("ChitNumber");
            dt.Columns.Add("GangNumber");
            dt.Columns.Add("Job");
            dt.Columns.Add("AreaCovered");
            dt.Columns.Add("FieldWeight");
            dt.Columns.Add("ManDays");
            dt.Columns.Add("MusterChitRef");
            dt.Columns.Add("MainACCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     DateEntered, FieldID, ChitNumber,GangNumber, Job, AreaCovered, FieldWeight, ManDays,MusterChitNumber,MainACCode FROM dbo.DailyFieldSummary WHERE     (DivisionID = '" + DivID + "') AND (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (MusterChitNumber = '" + intSelectedMChit + "')", CommandType.Text);
            dtRow = dt.NewRow();
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
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
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtRow[9] = dataReader.GetString(9).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListDailyFieldSummaryTotals(String DivID, DateTime dtDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AreaCovered");
            dt.Columns.Add("FieldWeight");
            dt.Columns.Add("ManDays");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     SUM(AreaCovered) AS Expr1, SUM(FieldWeight) AS Expr2, SUM(ManDays) AS Expr3 FROM dbo.DailyFieldSummary WHERE     (DivisionID = '"+DivID+"') AND (DateEntered = CONVERT(DATETIME, '"+dtDate+"', 102)) AND (Job = 'plk')", CommandType.Text);
            dtRow = dt.NewRow();
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
               
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetDecimal(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetDecimal(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetDecimal(2);
                }

                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;

        }


    }
}
