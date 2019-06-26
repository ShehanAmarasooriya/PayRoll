using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FTSPayRollBL;
using DataAccess;
using System.Data.SqlClient;


namespace FTSPayRollBL
{
    public class SystemSetting
    {
        public DataTable viewSystemDeductionDetail()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Short Name"));
            dt.Columns.Add(new DataColumn("Deduction Name"));
            dt.Columns.Add(new DataColumn("Group"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Active Status"));
            dt.Columns.Add(new DataColumn("Editor's Name"));
            dt.Columns.Add(new DataColumn("Reason to Change Amount"));
            DataRow dtRow;
            SqlDataReader dataReader = null;
            dtRow = dt.NewRow();

            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName, dbo.CHKDeductionGroup.GroupName, dbo.CHKSysDeductions.DeductAmount,dbo.CHKSysDeductions.Active FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKSysDeductions ON dbo.CHKSysDeductions.DeductionId=dbo.CHKDeduction.DeductionCode WHERE dbo.CHKSysDeductions.AccessGrant='TRUE' ORDER BY dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetDecimal(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetBoolean(4);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable viewCheckRollRates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Editor's Name"));
            dt.Columns.Add(new DataColumn("Reason to Change Amount"));
            DataRow dtRow;
            SqlDataReader dataReader = null;
            dtRow = dt.NewRow();

            dataReader = SQLHelper.ExecuteReader("SELECT Type, Name, Description, Amount FROM dbo.FTSCheckRollRates WHERE AccessGrant='TRUE'", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetDecimal(3);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }


        public void updateDeductions(String Name, String UserId, String Password, String Reason, Decimal Amount, DateTime CreateDateTime, String GroupName, String ShortName, String ChangedValue,Decimal currAmount)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO dbo.dbo.FTSCheckRollSettingsAdjustLog(Name ,UserId,Password,Reason,Amount,CreateDateTime,ChangedValue,PreviousAmount) VALUES('" + Name + "','" + UserId + "','" + Password + "','" + Reason + "','" + Amount + "','" + CreateDateTime + "','" + ChangedValue + "','" + currAmount + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE dbo.CHKSysDeductions SET DeductAmount='" + Amount + "' WHERE DeductionGroupId=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + GroupName + "')AND DeductionId=(SELECT DeductionCode FROM dbo.CHKDeduction WHERE ShortName='" + ShortName + "' AND DeductionGroupCode=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + GroupName + "'))", CommandType.Text);          
        }

        public Boolean isRateUsedForCurrentMonth(Decimal currAmout)
        {
            FTSPayRollBL.YearMonth ymObj = new YearMonth();
            Int32 lastMonthId = ymObj.getLastMonthID();
            Int32 lastYearId = ymObj.getLastYearID();
            DateTime currMonthStartDate = new DateTime(lastYearId, lastMonthId, 1);
            DateTime currMonthEndDate = currMonthStartDate.AddDays(DateTime.DaysInMonth(lastYearId, lastMonthId) - 1);
            SqlDataReader dtrd = SQLHelper.ExecuteReader("SELECT * FROM dbo.DailyGroundTransactions WHERE (DateEntered BETWEEN ('"+currMonthStartDate+"') AND ('"+currMonthEndDate+"'))", CommandType.Text);
            if (dtrd.HasRows)
                return true;
            else
                return false;
            dtrd.Close(); 
        }

        public Boolean isRateChange(Decimal curAmunt,String type)
        {
            DataTable dt = new DataTable();
            Decimal preAmunt=0;
            SqlDataReader dtrd = SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates where Type='"+type+"'", CommandType.Text);
            dt.Load(dtrd);
            if (dt.Rows.Count > 1)
            {   throw new Exception("FTSCheckRollRates table's primary key has changed");   }
            else if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    preAmunt = Convert.ToDecimal(dr[0]);
                }  
            }
            if (preAmunt == curAmunt)
                return false;
            else
                return true;
        }

        public Boolean isDeductionChange(Decimal curAmunt,String Group,String shortName)
        {
            DataTable dt = new DataTable();
            Decimal preAmunt = 0;
            //SqlDataReader dtrd = SQLHelper.ExecuteReader("SELECT dbo.CHKSysDeductions.DeductAmount  FROM dbo.CHKSysDeductions WHERE DeductionId=(SELECT DeductionCode FROM dbo.CHKDeduction WHERE ShortName='" + shortName + "' AND DeductionGroupCode=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + Group + "' AND ShortName='" + shortName + "')) AND DeductionGroupId=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + Group + "' AND ShortName='" + shortName + "')", CommandType.Text);
            SqlDataReader dtrd = SQLHelper.ExecuteReader("SELECT dbo.CHKSysDeductions.DeductAmount  FROM dbo.CHKSysDeductions WHERE DeductionId=(SELECT DeductionCode FROM dbo.CHKDeduction WHERE ShortName='" + shortName + "' AND DeductionGroupCode=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + Group + "'))", CommandType.Text);
            dt.Load(dtrd);
            if (dt.Rows.Count > 1)
            { throw new Exception("CHKSysDeductions table's keys has changed"); }
            else if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    preAmunt = Convert.ToDecimal(dr[0]);
                }
            }
            if (preAmunt == curAmunt)
                return false;
            else
                return true;
        }

        public Decimal getCurrentRate(String type)
        {
            DataTable dt = new DataTable();
            Decimal CurrAmunt = 0;
            SqlDataReader dtrd = SQLHelper.ExecuteReader("SELECT Amount FROM dbo.FTSCheckRollRates where Type='" + type + "'", CommandType.Text);
            dt.Load(dtrd);
            if (dt.Rows.Count > 1)
            { throw new Exception("FTSCheckRollRates table's primary key has changed"); }
            else if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CurrAmunt = Convert.ToDecimal(dr[0]);
                }
            }
            return CurrAmunt;
        }

        public Decimal getCurrentDeduction(String Group, String shortName)
        {
            DataTable dt = new DataTable();
            Decimal CurrAmunt = 0;
            SqlDataReader dtrd = SQLHelper.ExecuteReader("SELECT dbo.CHKSysDeductions.DeductAmount  FROM dbo.CHKSysDeductions WHERE DeductionId=(SELECT DeductionCode FROM dbo.CHKDeduction WHERE ShortName='" + shortName + "' AND DeductionGroupCode=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + Group + "'))", CommandType.Text);
            dt.Load(dtrd);
            if (dt.Rows.Count > 1)
            { throw new Exception("CHKSysDeductions table's keys has changed"); }
            else if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CurrAmunt = Convert.ToDecimal(dr[0]);
                }
            }
            return CurrAmunt;
        }

        public void updateRate(String Name, String UserId, String Password, String Reason, Decimal Amount, DateTime CreateDateTime, String type, String ChangedValue, Decimal currAmount)
        {
            if (isRateUsedForCurrentMonth(currAmount))
            {
                throw new Exception("Daily Entries Exists for current month.");
            }
            else
            {
                SQLHelper.ExecuteNonQuery("INSERT INTO dbo.dbo.FTSCheckRollSettingsAdjustLog(Name ,UserId,Password,Reason,Amount,CreateDateTime,ChangedValue,PreviousAmount) VALUES('" + Name + "','" + UserId + "','" + Password + "','" + Reason + "','" + Amount + "','" + CreateDateTime + "','" + ChangedValue + "','" + currAmount + "')", CommandType.Text);
                SQLHelper.ExecuteNonQuery("UPDATE dbo.FTSCheckRollRates SET Amount='" + Amount + "' WHERE Type='" + type + "'", CommandType.Text);
            }
        }

        public Boolean IsOldDataEntryBlocked()
        {
            Boolean boolYesNo = false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'PreviousEntryBlocking')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if(reader.GetString(0).ToString().ToUpper().Equals("AVAILABLE"))
                         boolYesNo=true;
                }
            }
            return boolYesNo;
        }

        public Boolean IsNetPayWithGrossEarnings()
        {
            Boolean boolYesNo = false;
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'NetPayWithGrossPay')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    if (reader.GetString(0).ToString().ToUpper().Equals("YES"))
                        boolYesNo = true;
                }
            }
            return boolYesNo;
        }


    }
}
