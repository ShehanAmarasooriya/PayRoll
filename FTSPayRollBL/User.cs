using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace FTSPayRollBL
{
    public class User
    {
        private static Boolean boolDayBlockAvailable;

        public static Boolean BoolDayBlockAvailable
        {
            get { return User.boolDayBlockAvailable; }
            set { User.boolDayBlockAvailable = value; }
        }
        private static Int32 usesID;

        public static Int32 UsesID
        {
            get { return User.usesID; }
            set { User.usesID = value; }
        }

        private static String strUserName;

        public static String StrUserName
        {
            get { return User.strUserName; }
            set { User.strUserName = value; }
        }
        private static String strUserPassword;

        public static String StrUserPassword
        {
            get { return User.strUserPassword; }
            set { User.strUserPassword = value; }
        }
        private static String strUserRole;

        public static String StrUserRole
        {
            get { return User.strUserRole; }
            set { User.strUserRole = value; }
        }
        private static String strEstate;

        public static String StrEstate
        {
            get { return User.strEstate; }
            set { User.strEstate = value; }
        }
        private static String strDivision;

        public static String StrDivision
        {
            get { return User.strDivision; }
            set { User.strDivision = value; }
        }
        private static String strYear;

        public static String StrYear
        {
            get { return User.strYear; }
            set { User.strYear = value; }
        }
        private static String strMonth;

        public static String StrMonth
        {
            get { return User.strMonth; }
            set { User.strMonth = value; }
        }
        private String strMenuID;

        public String StrMenuID
        {
            get { return strMenuID; }
            set { strMenuID = value; }
        }

        private String strMenuName;

        public String StrMenuName
        {
            get { return strMenuName; }
            set { strMenuName = value; }
        }

        private Int32 intCompanyKey;

        public Int32 IntCompanyKey
        {
            get { return intCompanyKey; }
            set { intCompanyKey = value; }
        }
        private String strDepartmentCode;

        public String StrDepartmentCode
        {
            get { return strDepartmentCode; }
            set { strDepartmentCode = value; }
        }
        private String strCurrencyCode;

        public String StrCurrencyCode
        {
            get { return strCurrencyCode; }
            set { strCurrencyCode = value; }
        }
        private String sUserName;

        public String SUserName
        {
            get { return sUserName; }
            set { sUserName = value; }
        }
        private String sUserPassword;

        public String SUserPassword
        {
            get { return sUserPassword; }
            set { sUserPassword = value; }
        }
        private String sEstate;

        public String SEstate
        {
            get { return sEstate; }
            set { sEstate = value; }
        }
        private String sDivision;

        public String SDivision
        {
            get { return sDivision; }
            set { sDivision = value; }
        }
        private Int32 intMenuId;

        public Int32 IntMenuId
        {
            get { return intMenuId; }
            set { intMenuId = value; }
        }
        private String strRole;

        public String StrRole
        {
            get { return strRole; }
            set { strRole = value; }
        }

        public DataSet GetEstates()
        {
            DataSet dsEstates = SQLHelper.FillDataSet("SELECT  EstateID, EstateName FROM Estate", CommandType.Text);
            return dsEstates;
        }

        public DataTable ListEstates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateName");
            dt.Columns.Add("EstateID");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT EstateName, EstateID FROM  dbo.Estate ORDER BY EstateName", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet GetEstateDivisions()
        {
            DataSet dsDivisions = SQLHelper.FillDataSet("SELECT  DivisionID, DivisionName  FROM  EstateDivision", CommandType.Text);
            return dsDivisions;
        }

        public DataTable ListEstateDivisions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("DivisionName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT DivisionID, DivisionName FROM  dbo.EstateDivision where ActiveDivision=1 ORDER BY DivisionID", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet GetYear()
        {
            DataSet dsYear = SQLHelper.FillDataSet("SELECT Year FROM dbo.CHKMonths GROUP BY Year", CommandType.Text);
            return dsYear;
        }
        public DataSet GetMonth(Int32 Year)
        {
            DataSet dsMonths = SQLHelper.FillDataSet("SELECT MId, Month FROM dbo.CHKMonths WHERE (Year = '"+Year+"') GROUP BY Month, MId", CommandType.Text);
            return dsMonths;
        }



        //==================

        public Boolean checkValidUser()
        {
            Boolean validUser = false;
            try
            {
                SqlDataReader dataReader;

                dataReader = SQLHelper.ExecuteReader("SELECT UserID FROM dbo.[User] WHERE (UserID = '" + StrUserName + "') AND (Password = '" + StrUserPassword + "') AND (EstateID = '" + StrEstate + "')AND (DivisionID = '" + StrDivision + "')", CommandType.Text);

                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        validUser = true;
                    }
                }
                dataReader.Close();
                return validUser;
            }
            catch (Exception exUser)
            {
                return validUser;
            }
        }

        public Boolean checkValidMonth()
        {
            Boolean validMonth = false;
            try
            {
                SqlDataReader dataReader;

                dataReader = SQLHelper.ExecuteReader("SELECT * FROM  CHKMonths  WHERE (Month = '" + StrMonth + "') AND  (DATEPART(year, CloseDate) = '" + StrYear + "') AND (Status = 0)", CommandType.Text);

                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        validMonth = true;
                    }
                }
                dataReader.Close();
                return validMonth;
            }
            catch (Exception exMonth)
            {
                return validMonth;
            }
        }

        public void PasswordChange(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = User.StrUserName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Password", SqlDbType.VarChar);
            param.Value = User.StrUserPassword;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("update [User] set Password ='" + User.StrUserPassword + "' where UserID='" + User.StrUserName + "'", CommandType.Text, paramList);
            SQLHelper.ExecuteNonQuery("INSERT INTO [UserAudit] (UserName, Password,EstateID,DivisionID,UserID,TransactionDate,TransactionType) VALUES ('"+User.StrUserName+"','NA','NA','NA','" + FTSPayRollBL.User.strUserName + "',getdate(),'PASSWORD CHANGED')", CommandType.Text);
        }

        public DataTable ListUserPermissionbyUserIDPassword(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = myUser.SUserName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Password", SqlDbType.VarChar);
            param.Value = myUser.SUserPassword;
            paramList.Add(param);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MenuName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.Menu.MenuID FROM dbo.[User] INNER JOIN dbo.UserPermission ON dbo.[User].UserID = dbo.UserPermission.UserID COLLATE SQL_Latin1_General_CP1_CI_AS INNER JOIN dbo.Menu ON dbo.UserPermission.MenuID = dbo.Menu.MenuID WHERE (dbo.[User].UserID = '" + myUser.SUserName + "') AND (dbo.[User].Password = '" + myUser.SUserPassword + "')", CommandType.Text, paramList);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListUserPermissionbyUserID(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = myUser.SUserName;
            paramList.Add(param);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Password"));
            dt.Columns.Add(new DataColumn("MenuName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.[User].Password, dbo.Menu.MenuItem FROM dbo.[User] INNER JOIN  dbo.UserPermission ON dbo.[User].UserID = dbo.UserPermission.UserID INNER JOIN "+
                                                " dbo.Menu ON dbo.UserPermission.MenuItemID = dbo.Menu.MenuItemID WHERE (dbo.[User].UserID = '"+myUser.SUserName+"')", CommandType.Text, paramList);

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

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListUserPermissionbyRole(String struser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@Role", SqlDbType.VarChar);
            param.Value = struser;
            paramList.Add(param);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MenuName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     dbo.Menu.MenuItem FROM  dbo.UserPermission INNER JOIN dbo.Menu ON dbo.UserPermission.MenuName = dbo.Menu.MenuItem WHERE     (dbo.UserPermission.UserRole = '" + struser + "')", CommandType.Text, paramList);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }               

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListAllUsers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("Password"));
            dt.Columns.Add(new DataColumn("Estate"));
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("CreatedDate"));
            dt.Columns.Add(new DataColumn("UserRole"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT UserID, Password,EstateID,DivisionID,CreateDatetime,UserRole FROM [User]", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = "Password encrypted";
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDateTime(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListAllUsersForNotAdmin()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("Password"));
            dt.Columns.Add(new DataColumn("Estate"));
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("CreatedDate"));
            dt.Columns.Add(new DataColumn("UserRole"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT UserID, Password,EstateID,DivisionID,CreateDatetime,UserRole FROM [User] WHERE UserID<>'ADMIN'", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = "Password encrypted";
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetDateTime(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }


                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public DataTable ListAllMenuItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MenuID"));
            dt.Columns.Add(new DataColumn("MenuName"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT Menu.MenuName,Menu.MenuItem FROM dbo.Menu order by MenuItemID", CommandType.Text);

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

        //public void InsertUser()
        //{
        //    SQLHelper.ExecuteNonQuery("insert into [User] (UserID,Password) values ('" + SUserName + "','" + SUserPassword  + "')", CommandType.Text);
        //}

        public void InsertUser(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = myUser.SUserName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Password", SqlDbType.VarChar);
            param.Value = myUser.SUserPassword;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
            param.Value = myUser.SEstate;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
            param.Value = myUser.SDivision;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@CreatedUserID", SqlDbType.VarChar);
            param.Value = FTSPayRollBL.User.StrUserName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@UserRole", SqlDbType.VarChar);
            param.Value = myUser.StrRole;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("SP_InsertUser", CommandType.StoredProcedure, paramList);            
        }

        public void UpdateUser(String strRole,String strUserName)
        {
            SQLHelper.ExecuteNonQuery("update dbo.[User] set UserRole='" + strRole + "' where UserID='" + strUserName + "'", CommandType.Text);
        }

        public void DeleteUserPermission(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = myUser.SUserName;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("delete from userpermission where userid = '" + myUser.SUserName + "'", CommandType.Text, paramList);
        }

        public void DeleteRolePermission(String strRole)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = strRole;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("delete from userpermission where UserRole = '" + strRole + "'", CommandType.Text, paramList);
        }

        public void DeleteUser(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = myUser.SUserName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DeletedUser", SqlDbType.VarChar);
            param.Value = FTSPayRollBL.User.strUserName;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("delete from [user] where userid = '" + myUser.SUserName + "'", CommandType.Text, paramList);
            SQLHelper.ExecuteNonQuery("INSERT INTO [UserAudit] (UserName, Password,EstateID,DivisionID,UserID,TransactionDate,TransactionType) VALUES ('" + myUser.SUserName + "','NA','NA','NA','" + FTSPayRollBL.User.strUserName + "',getdate(),'DELETED')", CommandType.Text);
        }

        public void InsertUserPermission(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@UserID", SqlDbType.VarChar);
            param.Value = myUser.SUserName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@MenuID", SqlDbType.Int);
            param.Value = myUser.IntMenuId;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@CreatedUser", SqlDbType.VarChar,50);
            param.Value = FTSPayRollBL.User.strUserName;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("SP_InsertUserPermission", CommandType.StoredProcedure, paramList);
        }

        public Int32 FindMenuIdbyMenuName(User myUser)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@MenuName", SqlDbType.VarChar);
            param.Value = myUser.StrMenuName;
            paramList.Add(param);

            Int32 MenuID=0;

            SqlDataReader dataReader;

            dataReader = SQLHelper.ExecuteReader("SELECT MenuItemID  FROM dbo.Menu WHERE ( MenuItem = '" + myUser.StrMenuName + "')", CommandType.Text, paramList);

            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    MenuID = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return MenuID;

        }

        public void DeleteMenu()
        {
            SQLHelper.ExecuteNonQuery("delete from Menu", CommandType.Text);
        }

        public void InsertMenu()
        {
            SQLHelper.ExecuteNonQuery("insert into Menu (MenuID,MenuName) values ('" + StrMenuID + "','" + StrMenuName + "')", CommandType.Text);
        }

        public void CreateLog(String TransactionName, String TransactionType, DateTime Date)
        {
            SQLHelper.ExecuteNonQuery("insert into UserLog (UserID,TransactionName,TransactionType,Date) values ('" + strUserName + "','" + TransactionName + "','" + TransactionType + "','" + Date + "')", CommandType.Text);
        }

        public Boolean IsAdminUser(String Struser)
        {
            Boolean IsAdmin = false;
            try
            {
                SqlDataReader dataReader;
                dataReader = SQLHelper.ExecuteReader("SELECT IsAdmin FROM dbo.[User] WHERE (UserID = '" + Struser + "') ", CommandType.Text);
                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        IsAdmin = dataReader.GetBoolean(0);
                    }
                }
                dataReader.Close();
                return IsAdmin;
            }
            catch (Exception exUser)
            {
                return IsAdmin;
            }
        }

        public DataTable ListAllRoles()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RoleName"));
            dt.Columns.Add(new DataColumn("IsActive"));
            dt.Columns.Add(new DataColumn("IsAdmin"));
            dt.Columns.Add(new DataColumn("IsSuperUser"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT  UserRole, ActiveStatus, IsAdmin FROM dbo.UserRole where UserRole<>'Admin'", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT  UserRole, ActiveStatus, IsAdmin,IsSuperUser FROM dbo.UserRole ", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }               
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetBoolean(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetBoolean(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetBoolean(3);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }
        public DataTable ListAllRolesAdmin()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RoleName"));
            dt.Columns.Add(new DataColumn("IsActive"));
            dt.Columns.Add(new DataColumn("IsAdmin"));
            dt.Columns.Add(new DataColumn("IsSuperUser"));

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     UserRole, ActiveStatus, IsAdmin,IsSuperUser FROM dbo.UserRole", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetBoolean(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetBoolean(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetBoolean(3);
                }

                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }

        public void InsertRole(String RoleName,Boolean ActiveRole,Boolean isAdmin)
        {
            SQLHelper.ExecuteNonQuery("insert into UserRole(UserRole,ActiveStatus,IsAdmin) Values('"+RoleName+"','"+ActiveRole+"','"+isAdmin+"')", CommandType.Text);
        }

        public void InsertRolePermission(String RoleName,String MenuName)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@RoleName", SqlDbType.VarChar,50);
            param.Value = RoleName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@MenuName", SqlDbType.VarChar,50);
            param.Value = MenuName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@CreatedUser", SqlDbType.VarChar, 50);
            param.Value = FTSPayRollBL.User.strUserName;
            paramList.Add(param);

            SQLHelper.ExecuteNonQuery("SP_InsertRolePermission", CommandType.StoredProcedure, paramList);
        }

        public void DeleteUserRolePermission(String strUserRole)
        {
            SQLHelper.ExecuteNonQuery("delete from userpermission where UserRole = '" + strUserRole + "'", CommandType.Text);
        }

        public void DeleteRole(String RoleName)
        {

            SQLHelper.ExecuteNonQuery("delete from [UserRole] where UserRole = '" +RoleName+ "'", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO [UserAudit] (UserName, Password,EstateID,DivisionID,UserID,TransactionDate,TransactionType) VALUES ('" + RoleName + "','NA','NA','NA','" + FTSPayRollBL.User.strUserName + "',getdate(),'DELETED')", CommandType.Text);
        }

        public DataSet getUserRole()
        {
            DataSet roleDs = SQLHelper.FillDataSet("SELECT top 1 UserRole FROM dbo.[User] WHERE (UserID = '" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);

            return roleDs;
        }

        //public void InsertCurrentDate()
        //{
        //    DataAccess.SQLHelper.ExecuteNonQuery("", CommandType.Text);
        //}
    



        //'''''''''''''''''
    }
}
