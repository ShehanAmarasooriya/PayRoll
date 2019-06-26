using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class FixedParameters
    {
        private Int32 intName;

        public Int32 IntName
        {
            get { return intName; }
            set { intName = value; }
        }

        private Decimal decAmount;

        public Decimal DecAmount
        {
            get { return decAmount; }
            set { decAmount = value; }
        }

        public DataTable getParameters()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("ID"));

            DataRow dtrow = dt.NewRow();
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Name,Id_Auto  FROM FTSCheckRollRates", CommandType.Text);

            while (reader.Read())
            {
                dtrow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtrow[0] = reader.GetString(0).Trim();
                }
                if (!reader.IsDBNull(1))
                {
                    dtrow[1] = reader.GetInt32(1);
                }
                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public DataTable ListAllParameters()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Amount"));

            DataRow dtrow = dt.NewRow();
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Id_Auto, Type, Name, Amount FROM FTSCheckRollRates", CommandType.Text);

            while (reader.Read())
            {
                dtrow = dt.NewRow();
                if (!reader.IsDBNull(0))
                {
                    dtrow[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    dtrow[1] = reader.GetString(1).Trim();
                }
                if (!reader.IsDBNull(2))
                {
                    dtrow[2] = reader.GetString(2).Trim();
                }
                if (!reader.IsDBNull(3))
                {
                    dtrow[3] = reader.GetDecimal(3);
                }
                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }
        public void UpdateParameters()
        {
            SQLHelper.ExecuteNonQuery("UPDATE FTSCheckRollRates SET Amount ='" + decAmount + "' WHERE (Id_Auto = '" + intName + "')", CommandType.Text);
        }
    }
}
