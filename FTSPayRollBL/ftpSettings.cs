using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
namespace FTSPayRollBL
{
    public class ftpSettings
    {
        public DataTable ListSettings()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ftpservername"));
            dt.Columns.Add(new DataColumn("username"));
            dt.Columns.Add(new DataColumn("password"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EstDataCapture.dbo.ftpSettings.FTPServerName, EstDataCapture.dbo.ftpSettings.UserName, EstDataCapture.dbo.ftpSettings.Password FROM EstDataCapture.dbo.ftpSettings", CommandType.Text);

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
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }
    }
}
