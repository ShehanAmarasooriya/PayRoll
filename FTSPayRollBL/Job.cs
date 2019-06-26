using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    
    public class Job
    {
        FTSCheckRollSettings objSettings = new FTSCheckRollSettings();

        private Int32 intJobID;
        public Int32 IntJobID
        {
            get { return intJobID; }
            set { intJobID = value; }
        }

        private Int32 intGroupID;
        public Int32 IntGroupID
        {
            get { return intGroupID; }
            set { intGroupID = value; }
        }

        private String strJobDesc;
        public String StrJobDesc
        {
            get { return strJobDesc; }
            set { strJobDesc = value; }
        }

        private string strShortName;
        public String StrShortName
        {
            get { return strShortName; }
            set { strShortName = value; }
        }

        private String strJobType;
        public String StrJobType
        {
            get { return strJobType; }
            set { strJobType = value; }
        }

        private String strAccType;
        public String StrAccType
        {
            get { return strAccType; }
            set { strAccType = value; }
        }

        private String strAnalyzeShortCode;

        public String StrAnalyzeShortCode
        {
            get { return strAnalyzeShortCode; }
            set { strAnalyzeShortCode = value; }
        }
        private String strDescription;

        public String StrDescription
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        private Int32 intSequence;

        public Int32 IntSequence
        {
            get { return intSequence; }
            set { intSequence = value; }
        }
        private Int32 intAnalyseAutoKey;

        public Int32 IntAnalyseAutoKey
        {
            get { return intAnalyseAutoKey; }
            set { intAnalyseAutoKey = value; }
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

        public void InsertJob()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO JobMaster  (JobGroup, JobName, JobShortName, JobType, ExpenditureType, UserId,AnalyzeCode,CropType) VALUES     ('" + intGroupID + "','" + strJobDesc + "','" + strShortName + "','" + strJobType + "','" + StrAccType + "','" + FTSPayRollBL.User.StrUserName + "', '" + StrAnalyzeShortCode + "','"+IntCropType+"')", CommandType.Text);
        }
        public void DeleteJob()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3]  ,[UpdatedUser],[Narration5])   SELECT  GETDATE() AS Expr1, dbo.JobMaster.JobID, 'JobMaster' AS Expr2, 'NA' AS Expr3, 'NA' AS Expr4, dbo.JobGroup.GroupShortName, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName,'" + FTSPayRollBL.User.StrUserName + "','Deleted' FROM  dbo.JobMaster INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE (dbo.JobMaster.JobShortName = '" + strShortName + "')", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("DELETE FROM JobMaster WHERE (JobShortName = '" + strShortName + "')", CommandType.Text);
        }
        public void UpdateJob()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3]  ,[UpdatedUser],[Narration5])   SELECT  GETDATE() AS Expr1, dbo.JobMaster.JobID, 'JobMaster' AS Expr2, 'NA' AS Expr3, 'NA' AS Expr4, dbo.JobGroup.GroupShortName, dbo.JobMaster.JobShortName, dbo.JobMaster.JobName,'" + FTSPayRollBL.User.StrUserName + "','Updated' FROM  dbo.JobMaster INNER JOIN dbo.JobGroup ON dbo.JobMaster.JobGroup = dbo.JobGroup.GroupID WHERE (dbo.JobMaster.JobShortName = '" + strShortName + "')", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("UPDATE    JobMaster SET JobGroup ='" + intGroupID + "', JobName ='" + strJobDesc + "', JobType ='" + strJobType + "', ExpenditureType ='" + strAccType + "', UserId ='" + FTSPayRollBL.User.StrUserName + "',AnalyzeCode='" + StrAnalyzeShortCode + "',CropType='"+IntCropType+"' WHERE     (JobShortName = '" + strShortName + "')", CommandType.Text);
        }
        public String JobNameById(Int32 JobId)
        {
            String JobName = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT  JobName FROM dbo.JobMaster WHERE (JobID = '" + JobId + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    JobName = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return JobName;
        }

        public String JobNameByShortName(String JobShortName)
        {
            String JobName = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT JobName FROM dbo.JobMaster WHERE (JobShortName = '" + JobShortName+ "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    JobName = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return JobName;
        }


        public DataSet JobNameCropAndExTypeByShortName(String JobShortName,String strCrop,String strExType)
        {
            DataSet dsJobNameCrop=new DataSet();
            if (strCrop.ToUpper().Equals("NONE") && strExType.ToUpper().Equals("NONE"))
            {
                dsJobNameCrop = SQLHelper.FillDataSet("SELECT        'NONE' AS Crop, JobName, 'NONE' AS ExType, JobShortName FROM dbo.JobMaster WHERE        (JobShortName = '"+JobShortName+"')", CommandType.Text);
            }
            else if (strCrop.ToUpper().Equals("NONE") && !strExType.ToUpper().Equals("NONE"))
            {
               dsJobNameCrop = SQLHelper.FillDataSet("SELECT 'NONE' AS Crop,dbo.JobMaster.JobName, dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.JobShortName FROM dbo.JobMaster INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode WHERE (dbo.JobMaster.JobShortName = '" + JobShortName + "') AND (dbo.JobGroupExpenditureType.ExpenditureType = '" + strExType + "')", CommandType.Text);
            }
            else
            dsJobNameCrop = SQLHelper.FillDataSet("SELECT        dbo.JobWithCropType.CropType, dbo.JobMaster.JobName, dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.JobShortName FROM dbo.JobMaster INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode WHERE (dbo.JobMaster.JobShortName = '" + JobShortName + "') AND  (dbo.JobWithCropType.CropType = '"+strCrop+"') AND (dbo.JobGroupExpenditureType.ExpenditureType = '" + strExType + "')", CommandType.Text);
            return dsJobNameCrop;
        }

        public DataSet InterEstateJobNameCropAndExTypeByShortName(String JobShortName, String strCrop)
        {
            DataSet dsJobNameCrop = new DataSet();
            
                dsJobNameCrop = SQLHelper.FillDataSet("SELECT        dbo.JobWithCropType.CropType, dbo.JobMaster.JobName, dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.JobShortName FROM dbo.JobMaster INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode WHERE (dbo.JobMaster.JobShortName = '" + JobShortName + "') AND  (dbo.JobWithCropType.CropType = '" + strCrop + "') ", CommandType.Text);
            return dsJobNameCrop;
        }

        public Int32 JobIdByShortName(String JobShortName)
        {
            Int32 JobID=111;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT JobID FROM dbo.JobMaster WHERE (JobShortName = '" + JobShortName + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    JobID = dataReader.GetInt32(0);
                }
            }
            dataReader.Close();
            return JobID;
        }

        public DataTable ListJobMaster()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT JobShortName, JobName, JobGroup, JobType, ExpenditureType, UserId, CreateDateTime, JobID, ISNULL(AnalyzeCode, '0') AS Expr1 FROM dbo.JobMaster ORDER BY JobShortName", CommandType.Text);

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
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListJobMasterByCropAndExType(String strCrop,String exType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("Crop"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            if (strCrop.ToUpper().Equals("NONE") && exType.ToUpper().Equals("NONE"))
            {
                dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode, dbo.JobWithCropType.CropType AS Crop FROM            dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode  ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            }
            else if (strCrop.ToUpper().Equals("NONE") && !exType.ToUpper().Equals("NONE"))
            {
                dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode,'None' FROM            dbo.JobMaster INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode WHERE        (dbo.JobGroupExpenditureType.ExpenditureType = '" + exType + "') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            }
            else
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType, dbo.JobMaster.ExpenditureType,  dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, ISNULL(dbo.JobMaster.AnalyzeCode, '0') AS Expr1 FROM dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode WHERE (dbo.JobWithCropType.CropType LIKE '"+strCrop+"') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode, dbo.JobWithCropType.CropType AS Crop FROM            dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode WHERE (dbo.JobWithCropType.CropType  LIKE '" + strCrop + "')  AND (dbo.JobGroupExpenditureType.ExpenditureType = '" + exType + "') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            }

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
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }


        public DataTable ListJobMasterByCropAndExType(String strCrop, String exType, String SearchText)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("Crop"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            if (strCrop.ToUpper().Equals("NONE") && exType.ToUpper().Equals("NONE"))
            {
                dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode, dbo.JobWithCropType.CropType AS Crop FROM            dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode where dbo.JobMaster.JobShortName like '%" + SearchText + "%' or dbo.JobMaster.JobName  like '%" + SearchText + "%'   ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            }
            else if (strCrop.ToUpper().Equals("NONE") && !exType.ToUpper().Equals("NONE"))
            {
                dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode,'None' FROM            dbo.JobMaster INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode WHERE        (dbo.JobGroupExpenditureType.ExpenditureType = '" + exType + "') AND (dbo.JobMaster.JobShortName like '%" + SearchText + "%' or dbo.JobMaster.JobName  like '%" + SearchText + "%') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            }
            else
            {
                //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType, dbo.JobMaster.ExpenditureType,  dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, ISNULL(dbo.JobMaster.AnalyzeCode, '0') AS Expr1 FROM dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode WHERE (dbo.JobWithCropType.CropType LIKE '"+strCrop+"') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
                dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode, dbo.JobWithCropType.CropType AS Crop FROM            dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode WHERE (dbo.JobWithCropType.CropType  LIKE '" + strCrop + "')  AND (dbo.JobGroupExpenditureType.ExpenditureType = '" + exType + "') AND (dbo.JobMaster.JobShortName like '%" + SearchText + "%' or dbo.JobMaster.JobName  like '%" + SearchText + "%') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            }

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
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }
        


        public DataTable ListJobMaster(String strJob)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("Crop"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT JobShortName, JobName, JobGroup, JobType, ExpenditureType, UserId, CreateDateTime, JobID, ISNULL(AnalyzeCode, '0') AS Expr1,CropType FROM dbo.JobMaster where JobShortName='"+strJob+"' ORDER BY JobShortName", CommandType.Text);

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
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetInt32(9);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListJobs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("CropType"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT JobGroup, JobName, JobShortName, JobType, ExpenditureType, UserId, CreateDateTime,JobID,isnull(AnalyzeCode,'0'),CropType FROM  JobMaster ORDER BY JobShortName", CommandType.Text);

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
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetInt32(9);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListJobs(Int32 intJobGroup)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("CropType"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT JobGroup, JobName, JobShortName, JobType, ExpenditureType, UserId, CreateDateTime,JobID,isnull(AnalyzeCode,'0'),CropType FROM  JobMaster WHERE     (JobGroup = '" + intJobGroup + "') ORDER BY JobShortName", CommandType.Text);

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
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetInt32(9);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListJobs(String strSearchBy)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("Crop"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType, dbo.JobMaster.ExpenditureType,  dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, ISNULL(dbo.JobMaster.AnalyzeCode, '0') AS Expr1 FROM dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode WHERE (dbo.JobWithCropType.CropType LIKE '"+strCrop+"') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT dbo.JobMaster.JobShortName, dbo.JobMaster.JobName, dbo.JobMaster.JobGroup, dbo.JobMaster.JobType,  dbo.JobGroupExpenditureType.ExpenditureType, dbo.JobMaster.UserId, dbo.JobMaster.CreateDateTime, dbo.JobMaster.JobID, '0' AS analyzeCode, dbo.JobWithCropType.CropType AS Crop FROM            dbo.JobMaster INNER JOIN dbo.JobWithCropType ON dbo.JobMaster.JobShortName = dbo.JobWithCropType.JobCode INNER JOIN dbo.JobGroupExpenditureType ON dbo.JobMaster.JobGroup = dbo.JobGroupExpenditureType.JobGroupCode WHERE (dbo.JobMaster.JobShortName LIKE '%" + strSearchBy + "%') or (dbo.JobMaster.JobName LIKE '%" + strSearchBy + "%') ORDER BY dbo.JobMaster.JobShortName", CommandType.Text);

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
                    dtrow[2] = dataReader.GetInt32(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetString(3).Trim();
                }
                if (!dataReader.IsDBNull(4))
                {
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getGroupname()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobGroupID"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT GroupName,GroupID FROM  JobGroup WHERE (GroupID = '" + intGroupID + "')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getJobID()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobID"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT JobName, JobID FROM JobMaster", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getNotOfferedJobs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobID"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT JobShortName,JobID FROM dbo.JobMaster WHERE (JobShortName like 'x%')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetInt32(1);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Boolean IsSpecialHalfNameCode(String JobShortName)
        {
            Boolean IsSpecialCode = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT  SpecialHalfYesNo FROM dbo.JobMaster WHERE (JobShortName = '" + JobShortName + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    if (dataReader.GetBoolean(0) == true)
                    {
                        IsSpecialCode = true;
                    }
                }
            }
            dataReader.Close();
            return IsSpecialCode;
        }

        public void InsertAnalysisCode()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[CHKJobAnalysisCodes] ([AnalyzeShortCode] ,[Description] ,[CreatedDateTime] ,[UserID],[SequenceNO]) VALUES ('" + StrAnalyzeShortCode + "' ,'" + StrDescription + "' ,getdate() , '" + User.StrUserName + "','"+IntSequence+"')", CommandType.Text);
        }
        public void DeleteAnalysisCode()
        {
            SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[CHKJobAnalysisCodes] WHERE (AutoKey = '" + IntAnalyseAutoKey + "')", CommandType.Text);
        }
        public void UpdateAnalysisCode()
        {
            SQLHelper.ExecuteNonQuery("UPDATE    CHKJobAnalysisCodes SET AnalyzeShortCode ='" + StrAnalyzeShortCode + "', Description ='" + StrDescription + "', SequenceNO ='" + IntSequence + "'  WHERE     (AutoKey = '" + IntAnalyseAutoKey + "')", CommandType.Text);
        }
        public DataTable ListAnalyzeCodes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SequenceNo"));
            dt.Columns.Add(new DataColumn("ShortCode"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("Ref#"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT [SequenceNO] ,[AnalyzeShortCode] ,[Description] ,[UserID] ,[CreatedDateTime],[AutoKey]  FROM [CHKJobAnalysisCodes] ORDER BY SequenceNO", CommandType.Text);

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
                    dtrow[5] = dataReader.GetInt32(5);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public Boolean IsJobAvailable(String JobID)
        {
            String strJob = "";
            Boolean boolAvail = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT JobShortName FROM dbo.JobMaster WHERE (JobShortName = '" + JobID + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    strJob = dataReader.GetString(0).Trim();
                    boolAvail = true;
                }

            }
            dataReader.Close();
            return boolAvail;
        }

        public DataTable ListJobsByACCode(String MainAC)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("JobGroup"));
            dt.Columns.Add(new DataColumn("JobName"));
            dt.Columns.Add(new DataColumn("JobShortName"));
            dt.Columns.Add(new DataColumn("JobType"));
            dt.Columns.Add(new DataColumn("ExpenditureType"));
            dt.Columns.Add(new DataColumn("UserId"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("JobID"));
            dt.Columns.Add(new DataColumn("AnalyzeCode"));
            dt.Columns.Add(new DataColumn("CropType"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT JobGroup, JobName, JobShortName, JobType, ExpenditureType, UserId, CreateDateTime,JobID,isnull(AnalyzeCode,'0'),CropType FROM  JobMaster ORDER BY JobShortName", CommandType.Text);

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
                    dtrow[4] = dataReader.GetString(4).Trim();
                }
                if (!dataReader.IsDBNull(5))
                {
                    dtrow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtrow[6] = dataReader.GetDateTime(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetInt32(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetInt32(9);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataSet GetSundryTask(String strJob,Int16 intCrop)
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT UnitCode, Task FROM  dbo.CHKTaskList WHERE (Job = '" + strJob + "') AND (CropType = '"+intCrop+"')", CommandType.Text);
             return ds;            
        }

        public DataSet ListSundryTasks()
        {
            DataSet ds = SQLHelper.FillDataSet("SELECT case when (CropType=1) then 'TEA' else 'RUBBER' end as CropTyper, Job, Task, UnitCode FROM dbo.CHKTaskList", CommandType.Text);
            return ds;
        }
    }
}
