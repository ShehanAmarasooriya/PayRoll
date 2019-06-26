using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{
    public class Class1
    {

        public DataTable getStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");

            DataRow dtRow;
            SqlDataReader reader;
            dtRow = dt.NewRow();

            reader = SQLHelper.ExecuteReader("SELECT  Name,Type FROM FTSCheckRollSettings WHERE (Type = 'EmpStatus') ", CommandType.Text);
            while (reader.Read())
            {
                dtRow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtRow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtRow[1] = reader.GetString(1).Trim();
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

    }
}
