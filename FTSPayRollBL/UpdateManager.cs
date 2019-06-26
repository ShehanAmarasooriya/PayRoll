using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using System.IO;
using System.IO.Compression;
//using ICSharpCode.SharpZipLib.Zip;
using System.Net;
using System.Globalization;

namespace FTSPayRollBL
{
    public class UpdateManager
    {

        public void ExecuteScript(String fileName)
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo(fileName);
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");
            SQLHelper.ExecuteNonQuery(script, CommandType.Text);

        }
       
        public void ExecuteScript1()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate1.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }

        public void ExecuteScript2()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate2.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript3()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate3.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript4()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate4.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript5()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate5.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript6()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate6.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript7()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate7.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript8()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate8.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript9()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate9.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript10()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate10.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript11()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate11.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript12()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate12.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript13()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate13.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript14()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate14.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript15()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate15.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript16()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate16.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void ExecuteScript17()
        {
            string sqlConnectionString = SQLHelper.connectionstring();
            FileInfo file = new FileInfo("V1.27sqlUpdate17.sql");
            string script = file.OpenText().ReadToEnd();
            script = script.Replace("GO", "");

            SQLHelper.ExecuteNonQuery(script, CommandType.Text);
        }
        public void test(FileInfo file)
        {
            FileStream fs = new FileStream(file.FullName + ".tmp", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            StreamReader streamreader = file.OpenText();
            String originalPath = file.FullName;
            string input = streamreader.ReadToEnd();
            String tempString = input.Replace("GO", "");
            SQLHelper.ExecuteNonQuery(tempString, CommandType.Text);
            try
            {
                sw.Write(tempString);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                streamreader.Close();
                streamreader.Dispose();
                File.Copy(originalPath, originalPath + ".old", true);
                //FileInfo newFile = new FileInfo(originalPath + ".tmp");
                File.Delete("SqlUpdate.sql");
                //File.Copy(originalPath + ".tmp", originalPath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            } 

        }

        public void ExecuteUpdate(FileInfo file)
        {            
            StreamReader streamreader = file.OpenText();
            String originalPath = file.FullName;
            string input = streamreader.ReadToEnd();
            String tempString = input.Replace("GO", "");
            SQLHelper.ExecuteNonQuery(tempString, CommandType.Text);
            
            streamreader.Close();
            streamreader.Dispose();
            File.Delete("SqlUpdate.sql");
        }

        public void BackUpDataBase(String filePath)
        {
            SqlParameter param = new SqlParameter();
            List<SqlParameter> paramList = new List<SqlParameter>();
            param = SQLHelper.CreateParameter("@fullpath", SqlDbType.VarChar, 200);
            param.Value = filePath;
            paramList.Add(param);
            SqlCommand cmd = SQLHelper.CreateCommand("SPBackupDB", CommandType.StoredProcedure, paramList);            
            SQLHelper.ExecuteNonQuery(cmd);
        }

        public  void Compress(string strPath, String NewFileName)
        {
            String DirectoryPath = strPath;

            DateTime current;
            string dstFile = "";
            FileStream fsIn = null;
            FileStream fsOut = null;
            GZipStream gzip = null;
            byte[] buffer;
            int count = 0;
            try
            {
                current = DateTime.Now;
                dstFile = strPath + "\\" + NewFileName;
                //fsOut = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None);
                fsOut = new FileStream(DirectoryPath + NewFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsOut, CompressionMode.Compress, true);
                fsIn = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[fsIn.Length];
                count = fsIn.Read(buffer, 0, buffer.Length);
                fsIn.Close();
                fsIn = null;
                gzip.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.ToString());
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }

                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }

                if (fsIn != null)
                {
                    fsIn.Close();
                    fsIn = null;
                }
            }
        }
        public Boolean DeleteSqlFileExist(FileInfo sqlfile)
        {
            Boolean bl = false;
            String originalPath = sqlfile.DirectoryName;
            string[] filePaths = Directory.GetFiles(originalPath);
            try
            {
                foreach (string filePath in filePaths)
                {
                    if (filePath.Contains(".sql"))
                        File.Delete(filePath);
                    bl = true;
                }
            }
            catch (Exception ex)
            {

            }

            return bl;
        }

        public DataTable GetUpdateFiles(String FolderName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileName"));
            DataRow dtrow;
            dtrow = dt.NewRow();
            DirectoryInfo di = new DirectoryInfo(FolderName);
            FileInfo[] rgFiles = di.GetFiles("*.*");

            String FullFileName = "";
            String FileName = "";
            foreach (FileInfo fi in rgFiles)
            {
                dtrow = dt.NewRow();
                FullFileName = fi.FullName;
                FileName = fi.Name;
                dtrow[0] = FileName;
                dt.Rows.Add(dtrow);
                FullFileName = "";
                FileName = "";
            }
            return dt;
        }

        public DataTable GetServerName(string str)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("server"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("select *  from master.dbo.sysservers where master.dbo.sysservers.srvproduct='"+str+"'", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(2))
                {
                    dtrow[0] = dataReader.GetString(2).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable GetDataBaseName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Database"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("select * from master.dbo.sysdatabases", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(2))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public void BackupSelectedDatabase(String filepath,String dbName)
        {
            SQLHelper.ExecuteNonQuery("Backup database " + dbName + " to disk='" + filepath+ "'", CommandType.Text);
        }

       

        public  String BackupProcessedData()
        {
            string strError = "OK";
            SqlDataReader dr;
            //SqlCommand cmd=SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.* FROM dbo.DailyGroundTransactions WHERE     (YEAR(DateEntered) = 2014)",CommandType.Text);
            dr = SQLHelper.ExecuteReader("SELECT dbo.DailyGroundTransactions.* FROM dbo.DailyGroundTransactions WHERE     (YEAR(DateEntered) = 2014)", CommandType.Text);
            SqlBulkCopy sbc = new SqlBulkCopy(SQLHelperBackUp.connectionstring(),SqlBulkCopyOptions.KeepIdentity);
            sbc.DestinationTableName = "DailyGroundTransactions";
            try
            {
                sbc.WriteToServer(dr);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 2627)
                {
                    strError = "PKE";
                }
                else
                {
                    strError = "Err";
                }
            }
            return strError;
        }

        public String BackupProcessedFixedDeductionsData()
        {
            string strError = "OK";
            SqlDataReader dr;
            //SqlCommand cmd=SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.* FROM dbo.DailyGroundTransactions WHERE     (YEAR(DateEntered) = 2014)",CommandType.Text);
            dr = SQLHelper.ExecuteReader("SELECT *  FROM dbo.CHKFixedDeductions WHERE (CloseYesNo = 1) OR (BalanceAmount <= 0)", CommandType.Text);
            SqlBulkCopy sbc = new SqlBulkCopy(SQLHelperBackUp.connectionstring(), SqlBulkCopyOptions.KeepIdentity);
            sbc.DestinationTableName = "dbo.CHKFixedDeductions";
            try
            {
                sbc.WriteToServer(dr);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 2627)
                {
                    strError = "PKE";
                }
            }
            return strError;
        }

        public String BackupProcessedLoanDeductionsData()
        {
            string strError = "OK";
            SqlDataReader dr;
            //SqlCommand cmd=SQLHelper.CreateCommand("SELECT dbo.DailyGroundTransactions.* FROM dbo.DailyGroundTransactions WHERE     (YEAR(DateEntered) = 2014)",CommandType.Text);
            dr = SQLHelper.ExecuteReader("SELECT dbo.CHKLoan.* FROM dbo.CHKLoan WHERE (ClosedYesNo = 1) OR (BalanceAmount <= 0)", CommandType.Text);
            SqlBulkCopy sbc = new SqlBulkCopy(SQLHelperBackUp.connectionstring(), SqlBulkCopyOptions.KeepIdentity);
            sbc.DestinationTableName = " dbo.CHKLoan";
            try
            {
                sbc.WriteToServer(dr);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 2627)
                {
                    strError = "PKE";
                }
            }
            return strError;
        }

        public String GetBackUpLocation(String strType)
        {
            SqlDataReader reader1;
            String strLocation = "NA";
            reader1 = SQLHelper.ExecuteReader("SELECT Name FROM dbo.FTSCheckRollSettings WHERE (Type = '" + strType + "')", CommandType.Text);
            while (reader1.Read())
            {
                if (!reader1.IsDBNull(0))
                {
                    strLocation = reader1.GetString(0).Trim();
                }
            }
            return strLocation;
        }
       
            

        
    }
}
