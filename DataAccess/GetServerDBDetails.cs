using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace DataAccess
{
    public class GetServerDBDetails
    {



        String rootFolder = @"D:\OLAX\";
        String XMLFolder = @"D:\OLAX\DatabaseSettings\";
        String EncriptKey = "pass1234";

        //String rootFolder = @"C:\OLAX\";
        //String XMLFolder = @"C:\OLAX\DatabaseSettings\";
        //String EncriptKey = "pass1234";

        public string _server;
        public string _dbName = "CheckrollDB";
        public string _estate;
        public string _password;
        public string _username;
        public string _company;

       static GetServerDBDetails myIntance;


        private GetServerDBDetails()
        {
            DataTable dt = ReadeDatabaseSettingXml();  
           _server =  DecryptString(dt.Rows[0]["SVR"].ToString(),true);
           _company =  DecryptString(dt.Rows[0]["COMPANY"].ToString(),true);
           _username =  DecryptString(dt.Rows[0]["UN"].ToString(),true);
           _password =  DecryptString(dt.Rows[0]["PW"].ToString(),true);
           _estate =  DecryptString(dt.Rows[0]["EST"].ToString(),true);
           _dbName = _company + _dbName;


          
        }



        public static GetServerDBDetails dbDetails()
        {
            if (myIntance == null)
            {
                myIntance = new GetServerDBDetails();
            }

            return myIntance;
        }




         public DataTable ReadeDatabaseSettingXml()
        {
            DataTable DTtable = new DataTable("DatabaseSettings");
            DTtable.ReadXmlSchema(XMLFolder + "DatabaseSettings.xsd");
            DTtable.ReadXml(XMLFolder + "DatabaseSettings.xml");
            return DTtable;
        }



        public  string DecryptString(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = EncriptKey;// (string)settingsReader.GetValue("SecurityKey", typeof(String));


            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        public static String isConnectionAvalable()
        {
            GetServerDBDetails dbdetails = GetServerDBDetails.dbDetails();


            using (SqlConnection connection = new SqlConnection("Data Source='" + dbdetails._server + "';Initial Catalog='" + dbdetails._dbName + "';User ID='" + dbdetails._username + "';Password='" + dbdetails._password + "'"))
            {

                connection.Open();

                if (connection.State == ConnectionState.Open)
                {

                    //MessageBox.Show("");
                }
                connection.Close();
                if (connection.State == ConnectionState.Closed)
                {
                    //MessageBox.Show("");
                }
            }

            return null;

        }




    }
}
