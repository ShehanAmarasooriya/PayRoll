using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class Company
    {
        public static String getCompanyName()
        {
            String CompanyName = "";

            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT CompanyName FROM dbo.CHKCompany", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    CompanyName = reader.GetString(0).Trim();
                }
            }
            return CompanyName;
        }
        public static String getCompanyAddress()
        {
            String CompanyName = "";

            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Address FROM dbo.CHKCompany", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    CompanyName = reader.GetString(0).Trim();
                }
            }
            return CompanyName;
        }

        public static String getCompanyCode()
        {
            String CompanyCode = "";

            SqlDataReader reader;
            reader = SQLHelper.ExecuteReader("SELECT Code FROM dbo.CHKCompany", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    CompanyCode = reader.GetString(0).Trim();
                }
            }
            return CompanyCode;
        }
    }
}
