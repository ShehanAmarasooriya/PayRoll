using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace FTSPayRollBL
{
    public class MenuLoader
    {
        public DataSet getMenus()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT Menu.MenuItem, Menu.MenuName FROM UserPermission INNER JOIN Menu ON UserPermission.MenuItemID = Menu.MenuItemID WHERE (UserPermission.UserID = '" + FTSPayRollBL.User.StrUserName + "') ORDER BY Menu.MenuName", CommandType.Text);
            return ds;
        }
        public DataSet getRoleMenus()
        {
            DataSet roleDs = SQLHelper.FillDataSet("SELECT top 1 UserRole FROM dbo.[User] WHERE (UserID = '"+FTSPayRollBL.User.StrUserName+"')", CommandType.Text);
            DataSet ds = SQLHelper.FillDataSet("SELECT Menu.MenuItem, Menu.MenuName FROM   dbo.UserPermission INNER JOIN dbo.Menu ON dbo.UserPermission.MenuName = dbo.Menu.MenuItem WHERE   (dbo.UserPermission.UserRole = '"+roleDs.Tables[0].Rows[0][0].ToString()+"')  ORDER BY Menu.MenuName", CommandType.Text);
            return ds;
        }

        public DataSet getUserPemitedMenus()
        {
            DataSet roleDs = SQLHelper.FillDataSet("SELECT top 1 UserRole FROM dbo.[User] WHERE (UserID = '" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
            DataSet ds = SQLHelper.FillDataSet("SELECT  MenuName FROM dbo.UserPermission WHERE (UserRole = '" + roleDs.Tables[0].Rows[0][0].ToString() + "')", CommandType.Text);
            return ds;
        }

        public void InsertMenuItem(String MainMenu, String SubMenu)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[Menu] ( [MenuItem] ,[MenuName],SubMenuItem) VALUES ('" + SubMenu + "' ,'" + MainMenu + "','" + SubMenu + "')", CommandType.Text);
        }

        public void DeleteMenuItems()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM Menu",CommandType.Text);
            SQLHelper.ExecuteNonQuery("DBCC CHECKIDENT('dbo.Menu', RESEED, 0)", CommandType.Text);
        }

        public DataTable ListAllMenuItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MenuID"));
            dt.Columns.Add(new DataColumn("MenuName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT MenuName, MenuItem FROM dbo.Menu order by MenuItemID", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }

                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }


                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }
    }
}
