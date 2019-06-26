using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class DownloadData
    {
        public String InsertDailyHarvestEntry(String EstateID, String DivisionID, DateTime EnteredDate, String EmpNo, Int32 CropType, String WorkCodeID, Decimal WorkQty1, String WorkQty1_DivisionID, String WorkQty1_FieldID, DateTime WorkQty1_Time, Decimal WorkQty2, String WorkQty2_DivisionID, String WorkQty2_FieldID, DateTime WorkQty2_Time, Decimal WorkQty3, String WorkQty3_DivisionID, String WorkQty3_FieldID, DateTime WorkQty3_Time,Int32 intWorkType,Int32 intFullHalf)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
            param.Value = EstateID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@DivisionID", SqlDbType.VarChar);
            param.Value = DivisionID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("EnteredDate", SqlDbType.DateTime);
            param.Value = EnteredDate;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@EmpNo", SqlDbType.VarChar);
            param.Value = EmpNo;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@CropType", SqlDbType.Int);
            param.Value = CropType;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkCodeID", SqlDbType.VarChar);
            param.Value = WorkCodeID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty1", SqlDbType.Decimal);
            param.Value = WorkQty1;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty1_DivisionID", SqlDbType.VarChar);
            param.Value = WorkQty1_DivisionID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty1_FieldID", SqlDbType.VarChar);
            param.Value = WorkQty1_FieldID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty1_Time", SqlDbType.DateTime);
            param.Value = WorkQty1_Time;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty2", SqlDbType.Decimal);
            param.Value = WorkQty2;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty2_DivisionID", SqlDbType.VarChar);
            param.Value = WorkQty2_DivisionID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty2_FieldID", SqlDbType.VarChar);
            param.Value = WorkQty2_FieldID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty2_Time", SqlDbType.DateTime);
            param.Value = WorkQty2_Time;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty3", SqlDbType.Decimal);
            param.Value = WorkQty3;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty3_DivisionID", SqlDbType.VarChar);
            param.Value = WorkQty3_DivisionID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty3_FieldID", SqlDbType.VarChar);
            param.Value = WorkQty3_FieldID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkQty3_Time", SqlDbType.DateTime);
            param.Value = WorkQty3_Time;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@USERID", SqlDbType.VarChar);
            param.Value = FTSPayRollBL.User.StrUserName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@WorkType", SqlDbType.Int);
            param.Value = intWorkType;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@FullHalf", SqlDbType.Int);
            param.Value = intFullHalf;
            paramList.Add(param);

            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spAddDailyHarvestTemp", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();

            return Status;
        }

        public DataTable GetDailyGroundTransactionTemp(DateTime Date)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateID");//0
            dt.Columns.Add("DivisionID");//1
            dt.Columns.Add("Entered\nDate");//2
            dt.Columns.Add("EmpNo");//3
            dt.Columns.Add("CropType");//4
            dt.Columns.Add("JobID");//5
            dt.Columns.Add("Qty1");//6
            dt.Columns.Add("WorkQty1_DivisionID");//7
            dt.Columns.Add("Qty1\nFieldID");//8
            dt.Columns.Add("Qty1\nTime");//9
            dt.Columns.Add("Qty2");//10
            dt.Columns.Add("WorkQty2_DivisionID");//11
            dt.Columns.Add("Qty2\nFieldID");//12
            dt.Columns.Add("Qty2\nTime");//13
            dt.Columns.Add("Qty3");//14
            dt.Columns.Add("WorkQty3_DivisionID");//15
            dt.Columns.Add("Qty3\nFieldID");//16
            dt.Columns.Add("Qty3\nTime");//17
            dt.Columns.Add("UserID");//18
            dt.Columns.Add("CreatedDate");//19
            dt.Columns.Add("WorkType");//20
            dt.Columns.Add("FullHalf");//21

            SqlDataReader Reader;

            DataRow drRow;

            Reader = SQLHelper.ExecuteReader("SELECT * FROM DailyGroundTransactionsTemp WHERE CONVERT(DATETIME,EnteredDate,102)=CONVERT(DATETIME,'" + Date + "',102)", CommandType.Text);

            while (Reader.Read())
            {
                drRow = dt.NewRow();

                if (!Reader.IsDBNull(0))
                {
                    drRow[0] = Reader.GetString(0).Trim();
                }
                if (!Reader.IsDBNull(1))
                {
                    drRow[1] = Reader.GetString(1).Trim();
                }
                if (!Reader.IsDBNull(2))
                {
                    drRow[2] = Reader.GetDateTime(2);
                }
                if (!Reader.IsDBNull(3))
                {
                    drRow[3] = Reader.GetString(3).Trim();
                }
                if (!Reader.IsDBNull(4))
                {
                    drRow[4] = Reader.GetInt32(4);
                }
                if (!Reader.IsDBNull(5))
                {
                    drRow[5] = Reader.GetString(5).Trim();
                }
                if (!Reader.IsDBNull(6))
                {
                    drRow[6] = Reader.GetDecimal(6);
                }
                if (!Reader.IsDBNull(7))
                {
                    drRow[7] = Reader.GetString(7).Trim();
                }
                if (!Reader.IsDBNull(8))
                {
                    drRow[8] = Reader.GetString(8).Trim();
                }
                if (!Reader.IsDBNull(9))
                {
                    drRow[9] = Reader.GetDateTime(9);
                }
                if (!Reader.IsDBNull(10))
                {
                    drRow[10] = Reader.GetDecimal(10);
                }
                if (!Reader.IsDBNull(11))
                {
                    drRow[11] = Reader.GetString(11).Trim();
                }
                if (!Reader.IsDBNull(12))
                {
                    drRow[12] = Reader.GetString(12).Trim();
                }
                if (!Reader.IsDBNull(13))
                {
                    drRow[13] = Reader.GetDateTime(13);
                }
                if (!Reader.IsDBNull(14))
                {
                    drRow[14] = Reader.GetDecimal(14);
                }
                if (!Reader.IsDBNull(15))
                {
                    drRow[15] = Reader.GetString(15).Trim();
                }
                if (!Reader.IsDBNull(16))
                {
                    drRow[16] = Reader.GetString(16).Trim();
                }
                if (!Reader.IsDBNull(17))
                {
                    drRow[17] = Reader.GetDateTime(17);
                }
                if (!Reader.IsDBNull(18))
                {
                    drRow[18] = Reader.GetString(18).Trim();
                }
                if (!Reader.IsDBNull(19))
                {
                    drRow[19] = Reader.GetDateTime(19);
                }
                if (!Reader.IsDBNull(20))
                {
                    drRow[20] = Reader.GetInt32(20);
                }
                if (!Reader.IsDBNull(21))
                {
                    drRow[21] = Reader.GetInt32(21);
                }

                dt.Rows.Add(drRow);
            }

            return dt;
        }

        public DataSet getEasyWeighHarvestRegister(String strDivision, DateTime dtDate)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));//0
            dt.Columns.Add(new DataColumn("EmpName"));//1
            dt.Columns.Add(new DataColumn("WorkQTY"));//2
            dt.Columns.Add(new DataColumn("Holiday"));//3
            dt.Columns.Add(new DataColumn("FieldID"));//4
            dt.Columns.Add(new DataColumn("WorkCodeID"));//5
            dt.Columns.Add(new DataColumn("User"));//6
            dt.Columns.Add(new DataColumn("CreatedDate"));//7
            dt.Columns.Add(new DataColumn("QTY1"));//8
            dt.Columns.Add(new DataColumn("QTY2"));//9
            dt.Columns.Add(new DataColumn("QTY3"));//10
            dt.Columns.Add(new DataColumn("AreaCovered"));//11
            dt.Columns.Add(new DataColumn("FieldWeight"));//12

            SqlDataReader reader;
            DataRow dtRow;

            reader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.DailyGroundTransactionsTemp.EmpNo, dbo.EmployeeMaster.EMPName, dbo.DailyGroundTransactionsTemp.WorkQty1 + dbo.DailyGroundTransactionsTemp.WorkQty2 + dbo.DailyGroundTransactionsTemp.WorkQty3 AS Workqty, CASE WHEN (SELECT COUNT(Date) FROM MonthlyHolidays WHERE (Date = CONVERT(DATETIME, dbo.DailyGroundTransactionsTemp.EnteredDate, 102))) > 0  THEN 'Yes' ELSE 'No' END AS Holiday, CASE WHEN dbo.DailyGroundTransactionsTemp.WorkQty1_FieldID<>'NA' THEN dbo.DailyGroundTransactionsTemp.WorkQty1_FieldID ELSE (CASE WHEN dbo.DailyGroundTransactionsTemp.WorkQty2_FieldID<>'NA' THEN dbo.DailyGroundTransactionsTemp.WorkQty2_FieldID ELSE (CASE WHEN dbo.DailyGroundTransactionsTemp.WorkQty3_FieldID<>'NA' THEN dbo.DailyGroundTransactionsTemp.WorkQty3_FieldID END) END)END AS FieldID, dbo.DailyGroundTransactionsTemp.WorkCodeID, dbo.DailyGroundTransactionsTemp.UserID, dbo.DailyGroundTransactionsTemp.CreatedDate, dbo.DailyGroundTransactionsTemp.WorkQty1, dbo.DailyGroundTransactionsTemp.WorkQty2, dbo.DailyGroundTransactionsTemp.WorkQty3 FROM dbo.DailyGroundTransactionsTemp INNER JOIN dbo.EmployeeMaster ON dbo.DailyGroundTransactionsTemp.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.DailyGroundTransactionsTemp.DivisionID = dbo.EmployeeMaster.DivisionID WHERE CONVERT(DATETIME,dbo.DailyGroundTransactionsTemp.EnteredDate,102)=CONVERT(DATETIME,'" + dtDate + "',102) AND (dbo.DailyGroundTransactionsTemp.DivisionID='" + strDivision + "')", CommandType.Text);

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
                if (!reader.IsDBNull(2))
                {
                    dtRow[2] = reader.GetDecimal(2);
                }
                if (!reader.IsDBNull(3))
                {
                    dtRow[3] = reader.GetString(3).Trim();
                }
                if (!reader.IsDBNull(4))
                {
                    dtRow[4] = reader.GetString(4).Trim();
                }
                if (!reader.IsDBNull(5))
                {
                    dtRow[5] = reader.GetString(5).Trim();
                }
                if (!reader.IsDBNull(6))
                {
                    dtRow[6] = reader.GetString(6).Trim();
                }
                if (!reader.IsDBNull(7))
                {
                    dtRow[7] = reader.GetDateTime(7);
                }
                if (!reader.IsDBNull(8))
                {
                    dtRow[8] = reader.GetDecimal(8);
                }
                if (!reader.IsDBNull(9))
                {
                    dtRow[9] = reader.GetDecimal(9);
                }
                if (!reader.IsDBNull(10))
                {
                    dtRow[10] = reader.GetDecimal(10);
                }
                dtRow[11] = 0;
                dtRow[12] = 0;

                dt.Rows.Add(dtRow);
            }
            reader.Dispose();

            ds.Tables.Add(dt);

            return ds;
        }

        public void deleteDailyHarvestTemp()
        {
            SQLHelper.ExecuteNonQuery("delete FROM DailyGroundTransactionsTemp ", CommandType.Text);
        }

        
        public String InsertCheckRollDivisionField(String EstateID, String DivisionID,  Int32 CropType, String FieldID, String Remaks, String Type, String FieldType, String FieldName, String MapField,String UserID)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
            param.Value = EstateID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar);
            param.Value = DivisionID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@CropType", SqlDbType.VarChar);
            if (CropType == 1)
            {
                param.Value = "Tea";
            }
            else
                param.Value = "Other";
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@FieldID", SqlDbType.VarChar);
            param.Value = FieldID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Remarks", SqlDbType.VarChar);
            param.Value = Remaks;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Type", SqlDbType.VarChar);
            param.Value = Type;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@FieldType", SqlDbType.VarChar);
            param.Value = FieldType;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@FieldName", SqlDbType.VarChar);
            param.Value = FieldName;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@MapField", SqlDbType.VarChar);
            param.Value = MapField;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar);
            param.Value = UserID;
            paramList.Add(param);


            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertDownloadedDivisionField", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();

            return Status;
        }

        public String InsertCheckRollDivision(String EstateID, String DivisionID, String DivisionName)
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@EstateID", SqlDbType.VarChar);
            param.Value = EstateID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@Division", SqlDbType.VarChar);
            param.Value = DivisionID;
            paramList.Add(param);

            param = SQLHelper.CreateParameter("@DivisionName", SqlDbType.VarChar);
            param.Value = DivisionName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar);
            param.Value = User.StrUserName;
            paramList.Add(param);


            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertDownloadedDivision", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();

            return Status;
            
        }
    }
}
