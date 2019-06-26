using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class JobGroup
    {
        private Int32 intGroupID;
        public Int32 IntGroupID
        {
            get { return intGroupID; }
            set { intGroupID = value; }
        }

        private String strGroupName;
        public String StrGroupName
        {
            get { return strGroupName; }
            set { strGroupName = value; }
        }

        private String strDescription;
        public String StrDescription
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        //private String strGroupParent;
        //public String StrGroupParent
        //{
        //    get { return strGroupParent; }
        //    set { strGroupParent = value; }
        //}

        //private String strShortName;
        //public String StrShortName
        //{
        //    get { return strShortName; }
        //    set { strShortName = value; }
        //}

        public void InsertGroup()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO JobGroup  (GroupName, Description, UserID) VALUES ('" + StrGroupName + "','" + strDescription + "','"+FTSPayRollBL.User.StrUserName+"')", CommandType.Text);
        }
        public void DeleteGroup()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM JobGroup WHERE     (GroupName = '"+StrGroupName+"')", CommandType.Text);
        }
        public void UpdateGroup()
        {
            SQLHelper.ExecuteNonQuery("UPDATE  JobGroup SET  Description ='"+strDescription+"', UserID ='" + FTSPayRollBL.User.StrUserName + "'  WHERE     (GroupName = '" + StrGroupName + "')", CommandType.Text);
        }
        public DataTable ListGroups()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GroupName"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));

            DataRow dtrow;
            SqlDataReader reader;
            dtrow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT  GroupName, Description, UserID, CreateDateTime FROM  JobGroup", CommandType.Text);

            while (reader.Read())
            {
                dtrow = dt.NewRow();

                if (!reader.IsDBNull(0))
                {
                    dtrow[0] = reader.GetString(0).Trim();
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
                    dtrow[3] = reader.GetDateTime(3);
                }

                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public DataTable ListGroupName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GroupName"));
            dt.Columns.Add(new DataColumn("GroupID"));
            
            DataRow dtrow;
            SqlDataReader reader;
            dtrow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT GroupName, GroupID FROM JobGroup", CommandType.Text);

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
        
    }
}
