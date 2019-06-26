using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeGang
    {
        private String strEstateID;
        public String StrEstateID
        {
            get { return strEstateID; }
            set { strEstateID = value; }
        }

        private String strDivisionID;
        public String StrDivisionID
        {
            get { return strDivisionID; }
            set { strDivisionID = value; }
        }

        private String strGang;
        public String StrGang
        {
            get { return strGang; }
            set { strGang = value; }
        }

        public DataTable ListAllGang()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("GangName"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionId, gangName, UserId, CreateDateTime FROM  EmployeeGang", CommandType.Text);

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
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetDateTime(3);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void InsertEmpGang()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeGang (EstateId, DivisionId, gangName, UserId)  VALUES ('" + strEstateID + "','" + strDivisionID + "','" + strGang + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void UpdateEmpGang()
        {
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeGang SET DivisionId ='" + strDivisionID + "', UserId ='" + FTSPayRollBL.User.StrUserName + "' WHERE (gangName = '" + strGang + "')", CommandType.Text);
        }

        public void DeleteEmpGang()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM EmployeeGang WHERE(gangName = '" + strGang + "')", CommandType.Text);
        }

        public DataTable getGangID()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GangID"));
            dt.Columns.Add(new DataColumn("GangName"));
            
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT GangId, gangName FROM EmployeeGang", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
               
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

    }
}
