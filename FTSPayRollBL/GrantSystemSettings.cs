using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FTSPayRollBL;
using DataAccess;
using System.Data.SqlClient;

namespace FTSPayRollBL
{
    public class GrantSystemSettings
    {
        public DataTable viewSystemDeductionDetail()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Short Name"));
            dt.Columns.Add(new DataColumn("Deduction Name"));
            dt.Columns.Add(new DataColumn("Group"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Active Status"));
            dt.Columns.Add(new DataColumn("AccessGrant"));
            DataRow dtRow;
            SqlDataReader dataReader = null;
            dtRow = dt.NewRow();

            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName, dbo.CHKDeductionGroup.GroupName, dbo.CHKSysDeductions.DeductAmount,dbo.CHKSysDeductions.Active,dbo.CHKSysDeductions.AccessGrant FROM dbo.CHKDeduction INNER JOIN dbo.CHKDeductionGroup ON dbo.CHKDeduction.DeductionGroupCode = dbo.CHKDeductionGroup.DeductionGroupCode INNER JOIN dbo.CHKSysDeductions ON dbo.CHKSysDeductions.DeductionId=dbo.CHKDeduction.DeductionCode ORDER BY dbo.CHKDeduction.ShortName, dbo.CHKDeduction.DeductionName", CommandType.Text);
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
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetBoolean(5);
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable viewCheckRollRatesDetail()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("AccessGrant"));
            DataRow dtRow;
            SqlDataReader dataReader = null;
            dtRow = dt.NewRow();

            dataReader = SQLHelper.ExecuteReader("SELECT Type, Name, Description, Amount,AccessGrant FROM dbo.FTSCheckRollRates", CommandType.Text);
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

        public void updateDeductionAccessGrant(String shortName,String Group,Boolean status)
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.CHKSysDeductions SET AccessGrant='" + status + "' WHERE DeductionId=(SELECT DeductionCode FROM dbo.CHKDeduction WHERE ShortName='" + shortName + "' AND DeductionGroupCode=(SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE GroupName='" + Group + "'))", CommandType.Text);           
        }

        public void updateRateAccessGrant(String type,Boolean status)
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.FTSCheckRollRates SET AccessGrant='" + status + "' WHERE Type='"+type+"'", CommandType.Text);
        }
    }
}
