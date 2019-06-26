using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
   public class OTParameters
    {

        private String strOTType;
        public String StrOTType
        {
            get { return strOTType; }
            set { strOTType = value; }
        }

        private Decimal decOTFactor;
        public Decimal DecOTFactor
        {
            get { return decOTFactor; }
            set { decOTFactor = value; }
        }


        public DataTable ListAllOTParameters()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("OTType"));
            dt.Columns.Add(new DataColumn("OTFactor"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));

            DataRow dtrow = dt.NewRow();
            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT  OTType, OTFactor, UserId, CreateDateTime  FROM  CHKOTParameters", CommandType.Text);
            while(reader.Read())
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
                if (!reader.IsDBNull(3))
                {
                    dtrow[3] = reader.GetDateTime(3);
                }
                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public void InsertOTParameter()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO CHKOTParameters  (OTType, OTFactor, UserId)   VALUES ('" + StrOTType + "','" + decOTFactor + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void UpdateOTParameter()
        {
            SQLHelper.ExecuteNonQuery("UPDATE  CHKOTParameters  SET OTFactor ='" + decOTFactor + "', UserId ='" + FTSPayRollBL.User.StrUserName + "'  WHERE (OTType = '" + StrOTType + "')", CommandType.Text);
        }

        public void DeleteOTParameter()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM CHKOTParameters WHERE (OTType = '" + StrOTType + "')", CommandType.Text);
        }
    }
}
