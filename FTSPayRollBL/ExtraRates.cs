using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class ExtraRates
    {
        private String strWorkCode;
        public String StrWorkCode
        {
            get { return strWorkCode; }
            set { strWorkCode = value; }
        }

        private Decimal decExtraRate;
        public Decimal DecExtraRate
        {
            get { return decExtraRate; }
            set { decExtraRate = value; }
        }

        public DataTable ListJobs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));

            DataRow dtrow = dt.NewRow();
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT JobShortName FROM  JobMaster", CommandType.Text);

            while (reader.Read())
            {
                dtrow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtrow[0] = reader.GetString(0).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public DataTable ListAllExtraRates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("WorkCode"));
            dt.Columns.Add(new DataColumn("ExtraRate"));
            dt.Columns.Add(new DataColumn("JobName"));

            DataRow dtrow = dt.NewRow();
            SqlDataReader reader;
            //reader = SQLHelper.ExecuteReader("SELECT WorkCode, ExtraRate, UserId, CreateDateTime FROM FTSCheckRollExtraRates", CommandType.Text);
            reader = SQLHelper.ExecuteReader("SELECT     dbo.FTSCheckRollExtraRates.WorkCode, dbo.FTSCheckRollExtraRates.ExtraRate, dbo.JobMaster.JobName FROM dbo.FTSCheckRollExtraRates INNER JOIN dbo.JobMaster ON dbo.FTSCheckRollExtraRates.WorkCode = dbo.JobMaster.JobShortName", CommandType.Text);

            while (reader.Read())
            {
                dtrow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtrow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtrow[1] = reader.GetDecimal(1);
                }
                if (!reader.IsDBNull(2))
                {
                    dtrow[2] = reader.GetString(2).Trim();
                }
                
                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public void InsertExtraRate()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO FTSCheckRollExtraRates  (WorkCode, ExtraRate, UserId) VALUES ('" + StrWorkCode + "','" + decExtraRate + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void UpdateExtraRate()
        {
            SQLHelper.ExecuteNonQuery("UPDATE FTSCheckRollExtraRates SET  ExtraRate ='" + decExtraRate + "', UserId ='" + FTSPayRollBL.User.StrUserName + "'  WHERE (WorkCode = '" + StrWorkCode + "')", CommandType.Text);
        }

        public void DeleteExtraRate()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM FTSCheckRollExtraRates WHERE (WorkCode = '" + StrWorkCode + "')", CommandType.Text);
        }
    }
}
