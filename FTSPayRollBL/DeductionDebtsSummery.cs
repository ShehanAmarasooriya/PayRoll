using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class DeductionDebtsSummery
    {

        public void insertToDeductionDetails(String Years, String Months, String Divisions, String EmpNo1, String EmpName1, String DeductionGrp, String Deduction, decimal amount, string EmpNo2, String EmpName2, String Type, String UserId)
        {

            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[DeductionDebitorSeparateDetails] ([Year] ,[Month] ,[Division] ,[EmpNo1] ,[EmpName1] ,[DeductionGroup]  ,[DeductionCode] ,[Amount] ,[EmpNo2] ,[EmpName2] ,[Type] ,[userID]) VALUES('" + Years + "','" + Months + "','" + Divisions + "','" + EmpNo1 + "','" + EmpName1 + "','" + DeductionGrp + "','" + Deduction + "' ,'" + amount + "' ,'" + EmpNo2 + "','" + EmpName2 + "','" + Type + "','" + UserId + "')", CommandType.Text);
        }



        public bool CheckEmpNo(String Years, String Months, String Divisions, String EmpNo1, String DeductionGrp, String Deduction, String Type)
        {


            SqlDataReader Reader;
            bool Status = false;

            Reader = SQLHelper.ExecuteReader("SELECT EmpNo1 FROM dbo.DeductionDebitorSeparateDetails WHERE (EmpNo1 = '" + EmpNo1 + "') AND (DeductionGroup = '" + DeductionGrp + "') AND (DeductionCode = '" + Deduction + "')  AND (Type = '" + Type + "') AND (Division = '" + Divisions + "') AND (Month = '" + Months + "') AND (Year = '" + Years + "')", CommandType.Text);




            while (Reader.Read())
            {
                if (!Reader.IsDBNull(0))
                    Status = true;
            }

            return Status;
        }


        public void makeDebtsSummery()
        {
            SqlDataReader Reader;
            Reader = SQLHelper.ExecuteReader("SELECT     Year, Month ,Division, DeductionGroup, DeductionCode, EmpNo2, EmpName2, Type, SUM(Amount) AS NetAmount FROM         dbo.DeductionDebitorSeparateDetails GROUP BY Year,Month, Division, DeductionGroup, DeductionCode, EmpNo2, EmpName2, Type", CommandType.Text);
            while (Reader.Read())
            {
                if (!Reader.IsDBNull(0))
                {
                    SQLHelper.ExecuteNonQuery("DELETE FROM dbo.DeductionDebitorSummery WHERE(EmpNo2 = '" + Reader["EmpNo2"] + "') AND (EmpName2 = '" + Reader["EmpName2"] + "') AND (Year = '" + Reader["Year"] + "') AND (Month = '" + Reader["Month"] + "') AND (Division = '" + Reader["Division"] + "') AND (DeductionGroup = '" + Reader["DeductionGroup"] + "') AND (Type = '" + Reader["Type"] + "')", CommandType.Text);
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[DeductionDebitorSummery]([EmpNo2],[EmpName2],[Year],[Month],[Division],[DeductionGroup],[Amount],[Type],[userID])VALUES('" + Reader["EmpNo2"] + "','" + Reader["EmpName2"] + "','" + Reader["Year"] + "','" + Reader["Month"] + "','" + Reader["Division"] + "','" + Reader["DeductionGroup"] + "','" + Reader["NetAmount"] + "','" + Reader["Type"] + "','ADMIN')", CommandType.Text);
                }

            }


        }



        public DataTable setGuarantees(String Years, String Months, String Divisions, String EmpNo1, String DeductionGrp, String Deduction, decimal amount, String Type)
        {
            DataTable dt = new DataTable();
            DataRow dtrow;
            dt.Columns.Add("EmpNo2", typeof(string));
            dt.Columns.Add("EmpName2", typeof(string));

            SqlDataReader Reader;
            Reader = SQLHelper.ExecuteReader("SELECT EmpNo2,EmpName2 FROM dbo.DeductionDebitorSeparateDetails WHERE (EmpNo1 = '" + EmpNo1 + "') AND (DeductionGroup = '" + DeductionGrp + "') AND (DeductionCode = '" + Deduction + "')   AND (Type = '" + Type + "') AND (Division = '" + Divisions + "') AND (Month = '" + Months + "') AND (Year = '" + Years + "')", CommandType.Text);

            while (Reader.Read())
            {
                dtrow = dt.NewRow();

                if (!Reader.IsDBNull(0))
                {
                    dtrow[0] = Reader.GetString(0);
                }

                if (!Reader.IsDBNull(1))
                {
                    dtrow[1] = Reader.GetString(1);
                }
                dt.Rows.Add(dtrow);
            }
            Reader.Close();

            return dt;
        }



        public void DeleteTable(String Years, String Months, String Divisions)
        {

            SQLHelper.ExecuteNonQuery("DELETE FROM [dbo].[DeductionDebitorSeparateDetails] WHERE (Division = '" + Divisions + "') AND (Month = '" + Months + "') AND (Year = '" + Years + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("DELETE FROM dbo.DeductionDebitorSummery WHERE (Division = '" + Divisions + "') AND (Month = '" + Months + "') AND (Year = '" + Years + "')", CommandType.Text);
        }




        public DataTable getDetailsDeductionDebitorSummery(String Years, String Months, String Divisions)
        {
            DataTable dt = new DataTable();
            DataRow dtrow;
            dt.Columns.Add("Emp No", typeof(string));
            dt.Columns.Add("Emp Name", typeof(string));
            dt.Columns.Add("Deduction Group", typeof(string));
            dt.Columns.Add("Amount", typeof(string));



            SqlDataReader Reader;
            Reader = SQLHelper.ExecuteReader("SELECT EmpNo2, EmpName2, DeductionGroup, Amount, Type FROM dbo.DeductionDebitorSummery  WHERE (Year = '" + Years + "') AND (Month = '" + Months + "') AND (Division = '" + Divisions + "')", CommandType.Text);

            while (Reader.Read())
            {
                dtrow = dt.NewRow();

                if (!Reader.IsDBNull(0))
                {
                    dtrow[0] = Reader.GetString(0);
                }

                if (!Reader.IsDBNull(1))
                {
                    dtrow[1] = Reader.GetString(1);
                }

                if (!Reader.IsDBNull(2))
                {
                    dtrow[2] = Reader.GetString(2);
                }
                if (!Reader.IsDBNull(3))
                {
                    if ((Reader.GetString(4)).Equals("AD"))
                    {
                        dtrow[3] = Reader.GetDecimal(3).ToString();
                    }
                    if ((Reader.GetString(4)).Equals("DE"))
                    {
                        dtrow[3] = "-" + Reader.GetDecimal(3).ToString();
                    }
                }
                dt.Rows.Add(dtrow);
            }
            Reader.Close();

            return dt;
        }








        public DataTable getDetailsDeductionDebitordeatails(String Years, String Months, String Divisions)
        {
            DataTable dt = new DataTable();
            DataRow dtrow;
            dt.Columns.Add("Emp No", typeof(string));
            dt.Columns.Add("Emp Name", typeof(string));
            dt.Columns.Add("Deduction Group", typeof(string));
            dt.Columns.Add("Deduction Code", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Emp No2", typeof(string));
            dt.Columns.Add("Emp Name2", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            SqlDataReader Reader;
            Reader = SQLHelper.ExecuteReader("SELECT [EmpNo1],[EmpName1],[DeductionGroup],[DeductionCode],[Amount],[EmpNo2],[EmpName2],[Type] FROM [dbo].[DeductionDebitorSeparateDetails] WHERE (Year = '" + Years + "') AND (Month = '" + Months + "') AND (Division = '" + Divisions + "')", CommandType.Text);

            while (Reader.Read())
            {
                dtrow = dt.NewRow();

                if (!Reader.IsDBNull(0))
                {
                    dtrow[0] = Reader.GetString(0);
                }

                if (!Reader.IsDBNull(1))
                {
                    dtrow[1] = Reader.GetString(1);
                }

                if (!Reader.IsDBNull(2))
                {
                    dtrow[2] = Reader.GetString(2);
                }

                if (!Reader.IsDBNull(3))
                {
                    dtrow[3] = Reader.GetString(3);
                }

                if (!Reader.IsDBNull(4))
                {
                    dtrow[4] = Reader.GetDecimal(4).ToString();
                }



                if (!Reader.IsDBNull(5))
                {
                    dtrow[5] = Reader.GetString(5);
                }

                if (!Reader.IsDBNull(6))
                {
                    dtrow[6] = Reader.GetString(6);
                }

                if ((!Reader.IsDBNull(7)))
                {
                    dtrow[7] = Reader.GetString(7);
                }

                dt.Rows.Add(dtrow);
            }


            Reader.Close();

            return dt;
        }

        public DataTable ListGuarantorRecoverySummaryFromTable(Int32 DeductYear, Int32 DeductMonth, String DivisionID)
        {
            DataTable dt = new DataTable();
            DataRow dtrow;
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("ProcessYear");
            dt.Columns.Add("ProcessMonth");
            dt.Columns.Add("Division");
            dt.Columns.Add("DeductGroup");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Type");


            SqlDataReader Reader;
            Reader = SQLHelper.ExecuteReader("SELECT     EmpNo2, Year, Month, Division, DeductionGroup, Amount, Type FROM dbo.DeductionDebitorSummery WHERE     (Division = '"+DivisionID+"') AND (Year = '"+DeductYear+"') AND (Month = '"+DeductMonth+"') ", CommandType.Text);

            while (Reader.Read())
            {
                dtrow = dt.NewRow();

                if (!Reader.IsDBNull(0))
                {
                    dtrow[0] = Reader.GetString(0);
                }
                if (!Reader.IsDBNull(1))
                {
                    dtrow[1] = Reader.GetString(1);
                }
                if (!Reader.IsDBNull(2))
                {
                    dtrow[2] = Reader.GetString(2);
                }
                if (!Reader.IsDBNull(3))
                {
                    dtrow[3] = Reader.GetString(3);
                }
                if (!Reader.IsDBNull(4))
                {
                    dtrow[4] = Reader.GetString(4);
                }
                if (!Reader.IsDBNull(5))
                {
                    dtrow[5] = Reader.GetDecimal(5);
                }
                if (!Reader.IsDBNull(6))
                {
                    dtrow[6] = Reader.GetString(6);
                }
                dt.Rows.Add(dtrow);
            }
            Reader.Close();

            return dt;
        }



    }
}
