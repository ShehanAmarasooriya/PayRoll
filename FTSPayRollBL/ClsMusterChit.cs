using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace FTSPayRollBL
{

    public class ClsMusterChit
    {
        FTSCheckRollSettings FTSSettings = new FTSCheckRollSettings();
        DateTime dtDate;

        public DateTime DtDate
        {
            get { return dtDate; }
            set { dtDate = value; }
        }
        String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
        }
        String strField;

        public String StrField
        {
            get { return strField; }
            set { strField = value; }
        }
        String strACCode;

        public String StrACCode
        {
            get { return strACCode; }
            set { strACCode = value; }
        }
        Int32 intEmpCount;

        public Int32 IntEmpCount
        {
            get { return intEmpCount; }
            set { intEmpCount = value; }
        }
        private String strUser;

        public String StrUser
        {
          get { return strUser; }
          set { strUser = value; }
        }

        private String strChitNo;

        public String StrChitNo
        {
            get { return strChitNo; }
            set { strChitNo = value; }
        }

        private String strGangNumber;

        public String StrGangNumber
        {
            get { return strGangNumber; }
            set { strGangNumber = value; }
        }
        private String strLabourType;

        public String StrLabourType
        {
            get { return strLabourType; }
            set { strLabourType = value; }
        }
        private String strLabourEstate;

        public String StrLabourEstate
        {
            get { return strLabourEstate; }
            set { strLabourEstate = value; }
        }
        private String strLabourDivision;

        public String StrLabourDivision
        {
            get { return strLabourDivision; }
            set { strLabourDivision = value; }
        }
        private String strLabourField;

        public String StrLabourField
        {
            get { return strLabourField; }
            set { strLabourField = value; }
        }

        private String strJobShortName;

        public String StrJobShortName
        {
            get { return strJobShortName; }
            set { strJobShortName = value; }
        }

        private String strCropType;

        public String StrCropType
        {
            get { return strCropType; }
            set { strCropType = value; }
        }

        private Int32 intCropType;

        public Int32 IntCropType
        {
            get { return intCropType; }
            set { intCropType = value; }
        }


        public String InsertMusterChitEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldId", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MainACCode", SqlDbType.VarChar, 50);
            param.Value = StrACCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@GangNumber", SqlDbType.VarChar, 50);
            param.Value = StrGangNumber;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ChitNumber", SqlDbType.VarChar, 50);
            param.Value = StrChitNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NoOfEmployees", SqlDbType.Int);
            param.Value = IntEmpCount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userID", SqlDbType.VarChar, 50);
            param.Value = StrUser;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourType", SqlDbType.VarChar, 50);
            param.Value = StrLabourType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourEstate", SqlDbType.VarChar, 50);
            param.Value = StrLabourEstate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourDivision", SqlDbType.VarChar, 50);
            param.Value = StrLabourDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourField", SqlDbType.VarChar, 50);
            param.Value = StrLabourField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Job", SqlDbType.VarChar, 50);
            param.Value = StrJobShortName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Crop", SqlDbType.Int);
            param.Value = IntCropType;
            paramList.Add(param);
         
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spInsertMusterChitEntry", CommandType.StoredProcedure, paramList);
            identityParam = cmd.Parameters.Add("@scopeId", SqlDbType.Int, 4);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            identityParam.Direction = ParameterDirection.ReturnValue;
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            int trnScope = int.Parse(identityParam.Value.ToString());
            Status = statusParam.Value.ToString();
            cmd.Dispose();
            return Status;
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKMusterChit] ([DateEntered] ,[DivisionID] ,[FieldID] ,[MainAccountCode] ,[NoOfEmployees] ,[CreateDateTime] ,[UserID],[ChitNo],GangNumber) VALUES ('" + DtDate + "' ,'" + StrDivision + "' ,'" + StrField + "' ,'" + StrACCode + "' ,'" + IntEmpCount + "' ,getdate() ,'" + StrUser + "','" + StrChitNo + "','"+StrGangNumber+"')", CommandType.Text);
        }

        public String UpdateMusterChitEntry()
        {
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldId", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MainACCode", SqlDbType.VarChar, 50);
            param.Value = StrACCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@GangNumber", SqlDbType.VarChar, 50);
            param.Value = StrGangNumber;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ChitNumber", SqlDbType.VarChar, 50);
            param.Value = StrChitNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@NoOfEmployees", SqlDbType.Int);
            param.Value = IntEmpCount;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@userID", SqlDbType.VarChar, 50);
            param.Value = StrUser;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourType", SqlDbType.VarChar, 50);
            param.Value = StrLabourType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourEstate", SqlDbType.VarChar, 50);
            param.Value = StrLabourEstate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourDivision", SqlDbType.VarChar, 50);
            param.Value = StrLabourDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@labourField", SqlDbType.VarChar, 50);
            param.Value = StrLabourField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Job", SqlDbType.VarChar, 50);
            param.Value = StrJobShortName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Crop", SqlDbType.VarChar, 50);
            param.Value = StrCropType;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spUpdateMusterChitEntry", CommandType.StoredProcedure, paramList);
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

        public String DeleteMusterChitEntry()
        {
            
            String Status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();

            param = SQLHelper.CreateParameter("@dateEntered", SqlDbType.DateTime);
            param.Value = Convert.ToDateTime(DtDate.ToShortDateString());
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@divisionId", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@FieldId", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MainACCode", SqlDbType.VarChar, 50);
            param.Value = StrACCode;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@GangNumber", SqlDbType.VarChar, 50);
            param.Value = StrGangNumber;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@ChitNumber", SqlDbType.VarChar, 50);
            param.Value = StrChitNo;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Job", SqlDbType.VarChar, 50);
            param.Value = StrJobShortName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropType", SqlDbType.Int);
            param.Value=IntCropType;
            paramList.Add(param);
            SqlCommand cmd = new SqlCommand();
            cmd = SQLHelper.CreateCommand("spDeleteMusterChitEntry", CommandType.StoredProcedure, paramList);
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

        public DataTable ListMusterChitEntry(DateTime dtEnteredDate,String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DateEntered"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("ChitNo"));
            dt.Columns.Add(new DataColumn("MainAccount"));
            dt.Columns.Add(new DataColumn("EmpCount"));
            dt.Columns.Add(new DataColumn("CreateDate"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("LabourType"));
            dt.Columns.Add(new DataColumn("LabourEstate"));
            dt.Columns.Add(new DataColumn("LabourDivision"));
            dt.Columns.Add(new DataColumn("LabourField"));
            dt.Columns.Add(new DataColumn("Job"));
            dt.Columns.Add(new DataColumn("Crop"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DateEntered, DivisionID, FieldID,GangNumber,ChitNo, MainAccountCode, NoOfEmployees, CreateDateTime, UserID,[LabourType] ,[LabourEstate] ,[LabourDivision] ,[LabourField],Job,CropType FROM dbo.CHKMusterChit WHERE (DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (DivisionID = '" + strDiv + "')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetInt32(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetString(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetInt32(14);
                    //dtrow[14] = dataReader.GetString(14);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListGangNumbersForSelectedDate(DateTime dtEnteredDate, String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GangNumber"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT GangNumber FROM dbo.CHKMusterChit WHERE     (DivisionID = '"+strDiv+"') AND (DateEntered = CONVERT(DATETIME, '"+dtEnteredDate+"', 102)) ORDER BY GangNumber", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
              
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListChitNumbersForSelectedGang(DateTime dtEnteredDate, String strDiv, String strGang)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ChitNumber"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT ChitNo FROM dbo.CHKMusterChit WHERE     (DivisionID = '"+strDiv+"') AND (DateEntered = CONVERT(DATETIME, '"+dtEnteredDate+"', 102)) AND (GangNumber = '"+strGang+"') ORDER BY ChitNo", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListChitNumbersForSelectedDate(DateTime dtEnteredDate, String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ChitNumber"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT ChitNo FROM dbo.CHKMusterChit WHERE     (DivisionID = '" + strDiv + "') AND (DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102))  ORDER BY ChitNo", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListGangNumbersForSelectedChitNumber(DateTime dtEnteredDate, String strDiv,String ChitNumber)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("MainAccountCode"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT GangNumber, FieldID, MainAccountCode FROM dbo.CHKMusterChit WHERE     (DivisionID = '" + strDiv + "') AND (DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (ChitNo = '" + ChitNumber + "') ORDER BY GangNumber", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Int32 getNoOfPHEmployees(DateTime dtPHDate,String strDivision)
        {
            SqlDataReader reader;
            Int32 intPHEmpCount=0 ;
            reader = SQLHelper.ExecuteReader("SELECT COUNT(EmpNo) AS Expr1 FROM dbo.DailyGroundTransactions WHERE (WorkCodeID = 'PH') AND (DivisionID = '" + strDivision + "') AND (DateEntered = CONVERT(DATETIME, '" + dtPHDate + "', 102))", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    intPHEmpCount = reader.GetInt32(0);
                }
            }
            reader.Close();
            return intPHEmpCount;
        }

        public DataTable ListMusterChitForSelectedDate(DateTime dtEnteredDate, String strDiv,Int32 Crop)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AutoMusterID"));
            dt.Columns.Add(new DataColumn("ChitNumber"));
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("MainACCode"));
            dt.Columns.Add(new DataColumn("MChitName"));
            dt.Columns.Add(new DataColumn("Job"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            if (Crop == 999)
            {
                dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID,  dbo.CHKMusterChit.MainAccountCode,  'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND  (dbo.CHKMusterChit.CropType like '%') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
                //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND (dbo.CHKMusterChit.CropType LIKE '%') AND (dbo.CHKMusterChit.LabourType = 'General') union SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.LabourField, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.CropType LIKE '%') AND (dbo.CHKMusterChit.LabourType = 'Lent Labour') AND (dbo.CHKMusterChit.LabourDivision = '" + strDiv + "') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
            }
            else
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT AutoMusterID, ChitNo, GangNumber, FieldID, MainAccountCode,'Chit:'+ChitNo + ' ' + 'Gang:'+GangNumber + ' ' +'Field:'+ FieldID + ' ' +'Job:'+ Job AS ChitName,Job FROM dbo.CHKMusterChit WHERE (DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (DivisionID = '" + strDiv + "') ORDER BY AutoMusterID", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID,  dbo.CHKMusterChit.MainAccountCode,  'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND  (dbo.CHKMusterChit.CropType = '" + Crop + "') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
                //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND (dbo.CHKMusterChit.CropType = '" + Crop + "') union SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.LabourField, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.LabourField+ ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.CropType = '" + Crop + "') AND (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.LabourDivision = '" + strDiv + "') AND (dbo.CHKMusterChit.LabourType = 'Lent Labour') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
            }

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListGeneraLentMusterChitForSelectedDate(DateTime dtEnteredDate, String strDiv, Int32 Crop)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AutoMusterID"));
            dt.Columns.Add(new DataColumn("ChitNumber"));
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("MainACCode"));
            dt.Columns.Add(new DataColumn("MChitName"));
            dt.Columns.Add(new DataColumn("Job"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            if (Crop == 999)
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID,  dbo.CHKMusterChit.MainAccountCode,  'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND  (dbo.CHKMusterChit.CropType like '%') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND (dbo.CHKMusterChit.CropType LIKE '%') AND (dbo.CHKMusterChit.LabourType = 'General') union SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.LabourField, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.CropType LIKE '%') AND (dbo.CHKMusterChit.LabourType = 'Lent Labour') AND (dbo.CHKMusterChit.LabourDivision = '" + strDiv + "') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
            }
            else
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT AutoMusterID, ChitNo, GangNumber, FieldID, MainAccountCode,'Chit:'+ChitNo + ' ' + 'Gang:'+GangNumber + ' ' +'Field:'+ FieldID + ' ' +'Job:'+ Job AS ChitName,Job FROM dbo.CHKMusterChit WHERE (DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (DivisionID = '" + strDiv + "') ORDER BY AutoMusterID", CommandType.Text);
                //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID,  dbo.CHKMusterChit.MainAccountCode,  'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND  (dbo.CHKMusterChit.CropType = '" + Crop + "') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.FieldID, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.FieldID + ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.DivisionID = '" + strDiv + "') AND (dbo.CHKMusterChit.CropType = '" + Crop + "') union SELECT TOP (100) PERCENT dbo.CHKMusterChit.AutoMusterID, dbo.CHKMusterChit.ChitNo, dbo.CHKMusterChit.GangNumber, dbo.CHKMusterChit.LabourField, dbo.CHKMusterChit.MainAccountCode, 'Chit:' + dbo.CHKMusterChit.ChitNo + ' ' + 'Gang:' + dbo.CHKMusterChit.GangNumber + ' ' + 'Field:' + dbo.CHKMusterChit.LabourField+ ' ' + 'Job:' + dbo.CHKMusterChit.Job AS ChitName, dbo.CHKMusterChit.Job FROM dbo.CHKMusterChit INNER JOIN dbo.EstateField ON dbo.CHKMusterChit.DivisionID = dbo.EstateField.DivisionID AND dbo.CHKMusterChit.FieldID = dbo.EstateField.FieldID WHERE (dbo.CHKMusterChit.CropType = '" + Crop + "') AND (dbo.CHKMusterChit.DateEntered = CONVERT(DATETIME, '" + dtEnteredDate + "', 102)) AND (dbo.CHKMusterChit.LabourDivision = '" + strDiv + "') AND (dbo.CHKMusterChit.LabourType = 'Lent Labour') ORDER BY dbo.CHKMusterChit.AutoMusterID", CommandType.Text);
            }

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable GetMusterDetailsForSelectedMuster(Int32 intMusterID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AutoMusterID"));//0
            dt.Columns.Add(new DataColumn("ChitNumber"));
            dt.Columns.Add(new DataColumn("GangNumber"));
            dt.Columns.Add(new DataColumn("FieldID"));
            dt.Columns.Add(new DataColumn("MainACCode"));
            dt.Columns.Add(new DataColumn("MChitName"));
            dt.Columns.Add(new DataColumn("LabourType"));//6
            dt.Columns.Add(new DataColumn("BorrowingEst"));//7
            dt.Columns.Add(new DataColumn("BorrowingDiv"));//8
            dt.Columns.Add(new DataColumn("BorrowingField"));//9
            dt.Columns.Add(new DataColumn("Job"));//10
            dt.Columns.Add(new DataColumn("NoOfEmployee"));//11
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT AutoMusterID, ChitNo, GangNumber, FieldID, MainAccountCode,ChitNo + '- ' + GangNumber + '- ' + FieldID + '- ' + Job AS ChitName, LabourType, LabourEstate, LabourDivision, LabourField, Job,NoOfEmployees FROM dbo.CHKMusterChit WHERE (AutoMusterID = '" + intMusterID + "') ORDER BY AutoMusterID", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetInt32(11);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Int32 getMusterChitNumber(DateTime dtDate,String strDiv,String strChitNO,String strGangNo,String strField,String strAC, String strJob)
        {
            SqlDataReader reader;
            Int32 intMusterID = 0;
            reader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT AutoMusterID FROM dbo.CHKMusterChit WHERE (DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND  (DivisionID = '" + strDiv + "') AND  (ChitNo = '" + strChitNO + "') AND (GangNumber = '" + strGangNo + "') AND (FieldID = '" + strField + "')  AND (Job='" + strJob + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    intMusterID = reader.GetInt32(0);
                }
            }
            reader.Close();
            return intMusterID;
        }

        public DataTable ListChitNumbersForSelectedMuster(Int32 intMusterID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ChitNumber"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT ChitNo FROM dbo.CHKMusterChit WHERE (AutoMusterID = '"+intMusterID+"') ORDER BY AutoMusterID", CommandType.Text);

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

        public DataTable ListGangNumbersForSelectedMuster(Int32 intMusterID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GangNumber"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT GangNumber FROM dbo.CHKMusterChit WHERE (AutoMusterID = '" + intMusterID + "') ORDER BY AutoMusterID", CommandType.Text);

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

        public DataTable ListFieldIdForSelectedMuster(Int32 intMusterID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FieldID"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT FieldID FROM dbo.CHKMusterChit WHERE (AutoMusterID = '" + intMusterID + "') ORDER BY AutoMusterID", CommandType.Text);

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

        public DataTable ListMusterChitEntryDayTotal(DateTime dtEnteredDate, String strDiv)
        {
            DataTable dt = new DataTable();
           
            dt.Columns.Add(new DataColumn("EmpCount"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     SUM(NoOfEmployees) AS Expr1 FROM dbo.CHKMusterChit WHERE (DateEntered = CONVERT(DATETIME, '"+dtEnteredDate+"', 102)) AND (DivisionID = '"+strDiv+"')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                
                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetInt32(0);
                }
                
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet GetJobOfMuster(DateTime dtDate,String strDiv,String strField,String strLabourType,String strLabourEstate,String strLabourDiv,String strLabourField,String strJob,String strChkList,String strGangNo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT Job, NoOfEmployees FROM dbo.CHKMusterChit WHERE (DateEntered = '" + dtDate + "') AND (DivisionID = '" + strDiv + "') AND (FieldID = '" + strField + "') AND (Job = '" + strJob + "') AND (GangNumber = '" + strGangNo + "') AND  (ChitNo = '" + strChkList + "') AND (LabourType = '" + strLabourType + "') AND (LabourEstate = '" + strLabourEstate + "') AND (LabourDivision = '" + strLabourDivision + "') AND (LabourField = '" + strLabourField + "')", CommandType.Text);
            return ds;
        }

        public DataSet GetMusterSummary(DateTime dtFrom, DateTime dtTo, String strDiv)
        {
            DataSet dsMusterSummary = new DataSet("MusterSummary");
            dsMusterSummary = SQLHelper.FillDataSet("SELECT DateEntered, DivisionID, FieldID, ChitNo, GangNumber, Job, LabourType, LabourEstate, LabourDivision, LabourField, NoOfEmployees, UserID,  CreateDateTime, CropType,(SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = 'CropType') AND (Code = dbo.CHKMusterChit.CropType)) AS CropName FROM dbo.CHKMusterChit WHERE (DateEntered BETWEEN CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (DivisionID LIKE '" + strDiv + "')", CommandType.Text);
            return dsMusterSummary;
        }

        public Boolean IsEmpHeadCountExceed(DateTime dtDate,String strDiv,Int32 intMusterID,Int32 intMusterEmpCount)
        {                  
            Int32 intDGTEmpcount = 0;
            intDGTEmpcount = intGetDailyEntryEmployeeCountForMuster(dtDate, strDiv, intMusterID);
            if (intDGTEmpcount >= intMusterEmpCount)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }

        public Int32 intGetDailyEntryEmployeeCountForMuster(DateTime dtDate, String strDiv, Int32 intMusterID)
        {
            Int32 intDGTEmpCount = 0;
            DataTable dtMusterData = new DataTable();
            dtMusterData = GetMusterDetailsForSelectedMuster(intMusterID);
            intDGTEmpCount = Convert.ToInt32(SQLHelper.FillDataSet("SELECT COUNT(dbo.DailyGroundTransactions.EmpNo) AS Expr1 FROM            dbo.DailyGroundTransactions INNER JOIN dbo.CHKMusterChit ON dbo.DailyGroundTransactions.DateEntered = dbo.CHKMusterChit.DateEntered AND  dbo.DailyGroundTransactions.DivisionID = dbo.CHKMusterChit.DivisionID AND dbo.DailyGroundTransactions.FieldID = dbo.CHKMusterChit.FieldID AND  dbo.DailyGroundTransactions.SubCategoryCode = dbo.CHKMusterChit.MainAccountCode AND  dbo.DailyGroundTransactions.GangNumber = dbo.CHKMusterChit.GangNumber AND  dbo.DailyGroundTransactions.MusterChitNumber = dbo.CHKMusterChit.ChitNo AND  dbo.DailyGroundTransactions.LabourType = dbo.CHKMusterChit.LabourType WHERE        (dbo.DailyGroundTransactions.DateEntered = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (dbo.DailyGroundTransactions.DivisionID = '" + strDiv + "') AND  (dbo.DailyGroundTransactions.FieldID = '" + dtMusterData.Rows[0][3].ToString() + "') AND (dbo.DailyGroundTransactions.MusterChitNumber = '" + dtMusterData.Rows[0][1].ToString() + "') AND  (dbo.DailyGroundTransactions.GangNumber = '" + dtMusterData.Rows[0][2].ToString() + "') AND (dbo.DailyGroundTransactions.WorkCodeID = '" + dtMusterData.Rows[0][10].ToString() + "') AND (dbo.CHKMusterChit.AutoMusterID = '" + intMusterID + "')", CommandType.Text).Tables[0].Rows[0][0].ToString());
            return intDGTEmpCount;
        }


    }

    
}
