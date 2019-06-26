using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class GLProcess
    {
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
        public DataTable GetGLAdditions(Int32 intYear, Int32 intM)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("A/C Code"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKGLEntries.WorkCodeId, dbo.CHKGLEntries.WorkCodeName, SUM(dbo.CHKGLEntries.TotalWithIncentive) AS Amount, CASE WHEN (dbo.JobMaster.AccountCode IS NULL) THEN '111-1-111' ELSE CASE WHEN (dbo.JobMaster.AccountCode = '') THEN '111-1-111' ELSE dbo.JobMaster.AccountCode END END AS ACCode FROM dbo.CHKGLEntries INNER JOIN  dbo.JobMaster ON dbo.CHKGLEntries.WorkCodeId = dbo.JobMaster.JobShortName GROUP BY dbo.CHKGLEntries.WorkCodeId, dbo.CHKGLEntries.WorkCodeName, dbo.JobMaster.AccountCode, dbo.CHKGLEntries.ProcessYear, dbo.CHKGLEntries.ProcessMonth HAVING (dbo.CHKGLEntries.ProcessYear = '"+intYear+"') AND (dbo.CHKGLEntries.ProcessMonth = '"+intM+"')", CommandType.Text);
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
                    dtrow[3] = dataReader.GetString(3).Trim();
                }              

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }
    }

   
}
