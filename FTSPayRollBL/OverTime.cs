using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class OverTime
    {
        private Int32 intOTId;

        public Int32 IntOTId
        {
            get { return intOTId; }
            set { intOTId = value; }
        }
        private Int32 intOTType;

        public Int32 IntOTType
        {
            get { return intOTType; }
            set { intOTType = value; }
        }
        private DateTime dtDate;

        public DateTime DtDate
        {
            get { return dtDate; }
            set { dtDate = value; }

        }
        private Boolean boolHolidayYesNO;

        public Boolean BoolHolidayYesNO
        {
            get { return boolHolidayYesNO; }
            set { boolHolidayYesNO = value; }
        }
        private Int32 intWorkType;

        public Int32 IntWorkType
        {
            get { return intWorkType; }
            set { intWorkType = value; }
        }
        private Int32 intCategory;

        public Int32 IntCategory
        {
            get { return intCategory; }
            set { intCategory = value; }
        }
        private String strEstate;

        public String StrEstate
        {
            get { return strEstate; }
            set { strEstate = value; }
        }
        private String strDivision;

        public String StrDivision
        {
            get { return strDivision; }
            set { strDivision = value; }
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
        private String strEmpNO;

        public String StrEmpNO
        {
            get { return strEmpNO; }
            set { strEmpNO = value; }
        }
        private Int32 intJob;

        public Int32 IntJob
        {
            get { return intJob; }
            set { intJob = value; }
        }
        private String strJobShortName;

        public String StrJobShortName
        {
            get { return strJobShortName; }
            set { strJobShortName = value; }
        }
        private Int32 intCrop;

        public Int32 IntCrop
        {
            get { return intCrop; }
            set { intCrop = value; }
        }
        private String strName;

        public String StrName
        {
            get { return strName; }
            set { strName = value; }
        }
        private String strField;

        public String StrField
        {
            get { return strField; }
            set { strField = value; }
        }
        private float flHours;

        public float FlHours
        {
            get { return flHours; }
            set { flHours = value; }
        }
        private float flAmount;

        public float FlAmount
        {
            get { return flAmount; }
            set { flAmount = value; }
        }

        private String strMainCode;

        public String StrMainCode
        {
            get { return strMainCode; }
            set { strMainCode = value; }
        }

        public String InsertOverTime()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();           
            param = SQLHelper.CreateParameter("@OtDate", SqlDbType.DateTime);
            param.Value = DtDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@WorkType", SqlDbType.Int,4);
            param.Value = IntWorkType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CategoryCode", SqlDbType.Int, 4);
            param.Value =IntCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmployeeNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DivisionCode", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Field", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Job", SqlDbType.VarChar,50);
            param.Value = StrJobShortName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropCode", SqlDbType.Int,4);
            param.Value = IntCrop;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Hours", SqlDbType.Float);
            param.Value = FlHours;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@otType", SqlDbType.Int);
            param.Value = IntOTType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Labourtype", SqlDbType.VarChar, 50);
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
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@MainCode", SqlDbType.VarChar, 50);
            param.Value = strMainCode;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spInsertOvertTime", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }
        public String UpdateOverTime()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@OverTimeId", SqlDbType.Int, 4);
            param.Value = IntOTId;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@OtDate", SqlDbType.DateTime);
            param.Value = DtDate;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@WorkType", SqlDbType.Int, 4);
            param.Value = IntWorkType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CategoryCode", SqlDbType.Int, 4);
            param.Value = IntCategory;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@EmployeeNo", SqlDbType.VarChar, 50);
            param.Value = StrEmpNO;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@DivisionCode", SqlDbType.VarChar, 50);
            param.Value = StrDivision;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Field", SqlDbType.VarChar, 50);
            param.Value = StrField;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Job", SqlDbType.VarChar, 50);
            param.Value = StrJobShortName;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@CropCode", SqlDbType.Int, 4);
            param.Value = IntCrop;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Hours", SqlDbType.Float);
            param.Value = FlHours;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@otType", SqlDbType.Int);
            param.Value = IntOTType;
            paramList.Add(param);
            param = SQLHelper.CreateParameter("@Labourtype", SqlDbType.VarChar, 50);
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
            param = SQLHelper.CreateParameter("@UserId", SqlDbType.VarChar, 50);
            param.Value = User.StrUserName;
            paramList.Add(param); 
            param = SQLHelper.CreateParameter("@MainCode", SqlDbType.VarChar, 50);
            param.Value = strMainCode;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spUpdateOvertTime", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }
        public string DeleteOverTime()
        {
            String status = "";
            SqlParameter param = new SqlParameter();
            SqlParameter identityParam = new SqlParameter();
            SqlParameter statusParam = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@OverTimeId", SqlDbType.Int, 4);
            param.Value = IntOTId;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("spDeleteOvertTime", CommandType.StoredProcedure, paramList);
            statusParam = cmd.Parameters.Add("@state", SqlDbType.VarChar, 50);
            statusParam.Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(cmd);
            status = statusParam.Value.ToString();
            return status;
        }
        public DataTable ListOverTime()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ref#");
            dt.Columns.Add("OtDate");
            dt.Columns.Add("WorkType");
            dt.Columns.Add("CategoryCode");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("Field");
            dt.Columns.Add("Job");
            dt.Columns.Add("CropCode");
            dt.Columns.Add("Hours");
            dt.Columns.Add("Expenditure");
            dt.Columns.Add("LabourType");
            dt.Columns.Add("OTFactor");
            dt.Columns.Add("LabourEstate");
            dt.Columns.Add("LabourDivision");
            dt.Columns.Add("LabourField");
            dt.Columns.Add("MainCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     OverTimeId, OtDate, WorkType, CategoryCode, EmployeeNo, DivisionCode, Field, Job, CropCode, Hours, Expenditure, LabourType, OTFactor, LabourEstate,LabourDivision, LabourField,dbo.CHKOvertime.SubCategoryCode  FROM dbo.CHKOvertime",CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
                }
                if(!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetDateTime(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetInt32(3);
                }
                if(!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtRow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetInt32(8);
                }
                if(!dataReader.IsDBNull(9))
                {
                    dtRow[9] = dataReader.GetDecimal(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtRow[10] = dataReader.GetDecimal(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtRow[11] = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtRow[12] = dataReader.GetInt32(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtRow[13] = dataReader.GetString(13).Trim();
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtRow[14] = dataReader.GetString(14).Trim();
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtRow[15] = dataReader.GetString(15).Trim();
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtRow[16] = dataReader.GetString(16).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }
        public DataTable ListOverTime(DateTime otDate,String strdiv,Int32 intOTFactor)
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("OtDate");
            dt.Columns.Add("DivisionCode");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("Field");
            dt.Columns.Add("Job");
            dt.Columns.Add("Hours");
            dt.Columns.Add("OTType");
            dt.Columns.Add("Expenditure");
            dt.Columns.Add("OTFactor");
            dt.Columns.Add("LabourType");
            dt.Columns.Add("LabourEstate");
            dt.Columns.Add("LabourDivision");
            dt.Columns.Add("LabourField");
            dt.Columns.Add("Ref#");
            dt.Columns.Add("MainCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT OtDate,DivisionCode, EmployeeNo, Field, Job,Hours, dbo.CHKOTParameters.OTType, Expenditure, dbo.CHKOvertime.OTFactor, LabourType, LabourEstate, LabourDivision, LabourField,OverTimeId, dbo.CHKOTParameters.OTType,dbo.CHKOvertime.SubCategoryCode " +
                                                " FROM  dbo.CHKOvertime INNER JOIN dbo.CHKOTParameters ON dbo.CHKOvertime.OTFactor = dbo.CHKOTParameters.OtSettingId WHERE (OtDate = CONVERT(DATETIME, '" + otDate + "', 102)) AND DivisionCode='" + strdiv + "' AND (dbo.CHKOvertime.OTFactor = '" + intOTFactor + "') ORDER BY dbo.CHKOvertime.OverTimeId DESC", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
               
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetDateTime(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtRow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtRow[5] = dataReader.GetDecimal(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtRow[7] = dataReader.GetDecimal(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetInt32(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtRow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtRow[10] = dataReader.GetString(10).Trim();
                }
                if (!dataReader.IsDBNull(11))
                {
                    dtRow[11] = dataReader.GetString(11).Trim();
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtRow[12] = dataReader.GetString(12).Trim();
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtRow[13] = dataReader.GetInt32(13);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtRow[14] = dataReader.GetString(15).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public DataTable ListOTtypes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OTSettingId");
            dt.Columns.Add("OTType");
            dt.Columns.Add("OTFactor");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT OtSettingId, OTType, OTFactor FROM dbo.CHKOTParameters", CommandType.Text);
            while (dataReader.Read())
            {
                dtRow = dt.NewRow();
                if (!dataReader.IsDBNull(0))
                {
                    dtRow[0] = dataReader.GetInt32(0);
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtRow[1] = dataReader.GetString(1);
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetDecimal(2);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public String GetOTFactorByText(String strText)
        {
            SqlDataReader dataReader;
            String strOTFactor = "";
            dataReader = SQLHelper.ExecuteReader("SELECT  OTFactor FROM dbo.CHKOTParameters WHERE (OTType='" + strText + "')", CommandType.Text);
            while (dataReader.Read())
            {
                strOTFactor = dataReader.GetDecimal(0).ToString();
            }
            return strOTFactor;
        }

        public DataTable GetOTSummary(DateTime dtDate, String strDiv, Int32 otFactor)
        {
            SqlDataReader otSummaryReader;
            DataTable dt=new DataTable();
            dt.Columns.Add("OTHours");
            dt.Columns.Add("OTAmount");
            DataRow dtRow;
            dtRow = dt.NewRow();
            otSummaryReader = SQLHelper.ExecuteReader("SELECT     SUM(Hours) AS Expr1, SUM(Expenditure) AS Expr2 FROM dbo.CHKOvertime WHERE     (OtDate = CONVERT(DATETIME, '" + dtDate + "', 102)) AND (DivisionCode = '" + strDiv + "') AND (OTFactor = '" + otFactor + "')", CommandType.Text);
            while (otSummaryReader.Read())
            {
                dtRow = dt.NewRow();
                if (!otSummaryReader.IsDBNull(0))
                {
                    dtRow[0] = otSummaryReader.GetDecimal(0);
                }
                if (!otSummaryReader.IsDBNull(1))
                {
                    dtRow[1] = otSummaryReader.GetDecimal(1);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        
    }
}
