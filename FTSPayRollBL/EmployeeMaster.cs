using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace FTSPayRollBL
{
    public class EmployeeMaster
    {
        CheckErrors chkErrors = new CheckErrors();
        public static String DHarvestDivision;
        public DataTable ListAllEmployees()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("EMPName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster ORDER BY EPFNo", CommandType.Text);
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
                else
                {
                    dtRow[1] = "NA";
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }
        public DataTable ListAllEmployees(String DivisionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("EMPName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  EmpNo, EPFNo, EMPName FROM  EmployeeMaster WHERE (DivisionID = '" + DivisionID + "') ORDER BY EmpNo", CommandType.Text);
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
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEmployeesDetails(String DivisionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EMPName");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("NIC");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Initials");
            dt.Columns.Add("OCGrade");
            dt.Columns.Add("ZoneCode");
            dt.Columns.Add("MemStatus");
            dt.Columns.Add("EmployerNo");
            dt.Columns.Add("DivisionID");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT EmpNo, EMPName,EPFNo, NICNo,LastName,Initials, OCGrade, ZoneCode, MemberStatus,EmployerNo,DivisionID FROM  EmployeeMaster WHERE (DivisionID like '" + DivisionID + "') ORDER BY EmpNo", CommandType.Text);
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
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtRow[7] = dataReader.GetString(7).Trim();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtRow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtRow[10] = dataReader.GetString(10).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEmployeesDetailsForEform(String DivisionID,Int32 intYear,Int32 intMonth)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EMPName");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("NIC");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Initials");
            dt.Columns.Add("OCGrade");
            dt.Columns.Add("ZoneCode");
            dt.Columns.Add("MemStatus");
            dt.Columns.Add("EmployerNo");
            dt.Columns.Add("DivisionID");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT        TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo,  dbo.EmployeeMaster.LastName, dbo.EmployeeMaster.Initials, dbo.EmployeeMaster.OCGrade, dbo.EmployeeMaster.ZoneCode,  dbo.EmployeeMaster.MemberStatus, dbo.EmployeeMaster.EmployerNo, dbo.EmployeeMaster.DivisionID FROM            dbo.EmployeeMaster INNER JOIN dbo.EmpMonthlyEarnings ON dbo.EmployeeMaster.DivisionID = dbo.EmpMonthlyEarnings.DivisionId AND  dbo.EmployeeMaster.EmpNo = dbo.EmpMonthlyEarnings.EmpNO WHERE        (dbo.EmployeeMaster.DivisionID LIKE '" + DivisionID + "') AND (dbo.EmpMonthlyEarnings.Year = '"+intYear+"') AND (dbo.EmpMonthlyEarnings.Month = '"+intMonth+"') AND  (dbo.EmpMonthlyEarnings.EPF10 > 0) ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
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
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtRow[7] = dataReader.GetString(7).Trim();
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtRow[8] = dataReader.GetString(8).Trim();
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtRow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtRow[10] = dataReader.GetString(10).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }


        public DataTable ListAllContractors()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("EMPName");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.EmployeeCategory.CategoryShortName = 'c') ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
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
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }
        public DataTable ListEmployeesList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("EMPName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Status");
            dt.Columns.Add("Category");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, "+
                     " dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeCategory.CategoryShortName "+
                     " FROM         dbo.EmployeeMaster INNER JOIN "+
                     " dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID "+
                       " ORDER BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo", CommandType.Text);
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
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEmployeesList(String Div)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("EMPName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Status");
            dt.Columns.Add("Category");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, " +
                     " dbo.EmployeeMaster.Gender, dbo.EmployeeMaster.EmployeeStatus, dbo.EmployeeCategory.CategoryShortName " +
                     " FROM         dbo.EmployeeMaster INNER JOIN " +
                     " dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID " +
                       //"WHERE     (dbo.EmployeeMaster.DivisionID = '"+Div+"') AND (ActiveEmployee = 1) ORDER BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo", CommandType.Text);
                        "WHERE     (dbo.EmployeeMaster.DivisionID = '" + Div + "') ORDER BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo", CommandType.Text);
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
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEmployeesDetailsbyCat(String DivisionID,Int32 empCat)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EMPName");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("NIC");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT EmpNo, EMPName, EPFNo, NICNo FROM dbo.EmployeeMaster WHERE (DivisionID = '" + DivisionID + "') AND (EmpCategory = '" + empCat + "') ORDER BY EmpNo", CommandType.Text);
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
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListEPFEntitledEmployeesDetails(String DivisionID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo");
            dt.Columns.Add("EMPName");
            dt.Columns.Add("EPFNo");
            dt.Columns.Add("NIC");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.NICNo, dbo.EmployeeCategory.EPFEntitled FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.EmployeeMaster.DivisionID = '" + DivisionID + "') AND (dbo.EmployeeCategory.EPFEntitled = 1) ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
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
                if (!dataReader.IsDBNull(2))
                {
                    dtRow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtRow[3] = dataReader.GetString(3).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }
        public String GetEmployeeNameByEmpNo( String empno, String StrDiv)
        {
            String empname = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster where (DivisionID='" + StrDiv + "') and  (EmpNo='" + empno + "')  ORDER BY EPFNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(2))
                {
                    empname = dataReader.GetString(2).Trim();
                }
            }
            dataReader.Close();
            return empname;
        }

        public Boolean IsNotInactive(String empno,String strdiv)
        {
            Boolean  boolActive = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT top 1 ActiveEmployee FROM dbo.EmployeeMaster WHERE  (DivisionID = '"+strdiv+"') AND (EmpNo = '" + empno + "') ORDER BY EPFNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    boolActive = dataReader.GetBoolean(0);
                }
            }
            dataReader.Close();
            return boolActive;
        }

        //public String GetEmployeeNameByEmpNo(String empno,String strDiv)
        //{
        //    String empname = "";
        //    SqlDataReader dataReader;
        //    dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster where  EmpNo='" + empno + "' AND DivisionID='"+strDiv+"' ORDER BY EPFNo", CommandType.Text);
        //    while (dataReader.Read())
        //    {
        //        if (!dataReader.IsDBNull(2))
        //        {
        //            empname = dataReader.GetString(2).Trim();
        //        }
        //    }
        //    dataReader.Close();
        //    return empname;
        //}


        public String GetEmployeeGenderByEmpNo(String empno,String strDiv)
        {
            String empGender = "";
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName,Gender FROM  dbo.EmployeeMaster where  EmpNo='" + empno + "'and DivisionID='"+strDiv+"' ORDER BY EPFNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(3))
                {
                    empGender = dataReader.GetString(3).Trim();
                }
            }
            dataReader.Close();
            return empGender;
        }

        

        public String GetEmployeeNameByEmpNoAndCategory(String empno, String Category)
        {
            String empname = "";
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster where DivisionID='"+DivId+"' and   EmpNo='" + empno + "' AND (ActiveEmployee = 1) ORDER BY EPFNo", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE  (dbo.EmployeeMaster.EmpNo = '" + empno + "') AND (dbo.EmployeeMaster.ActiveEmployee = 1) AND  (dbo.EmployeeCategory.CategoryShortName = '" + Category + "') ORDER BY dbo.EmployeeMaster.EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(2))
                {
                    empname = dataReader.GetString(2).Trim();
                }
            }
            dataReader.Close();
            return empname;
        }

        public String GetEmployeeNameByEmpNoAndEmpType(String empno, Int32 intEmpType)
        {
            String empname = "";
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster where DivisionID='"+DivId+"' and   EmpNo='" + empno + "' AND (ActiveEmployee = 1) ORDER BY EPFNo", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT TOP (100) PERCENT EmpNo, EPFNo, EMPName FROM dbo.EmployeeMaster WHERE (EmpNo = '" + empno + "') AND (ActiveEmployee = 1) AND (EmpType = '"+intEmpType+"') ORDER BY EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(2))
                {
                    empname = dataReader.GetString(2).Trim();
                }
            }
            dataReader.Close();
            return empname;
        }

        public String GetEmployeeUnionByEmpNo(String empno, string DivId)
        {
            String empname = "";
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster where DivisionID='"+DivId+"' and   EmpNo='" + empno + "' AND (ActiveEmployee = 1) ORDER BY EPFNo", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT UnionNameCode FROM dbo.EmployeeMaster WHERE  (DivisionID = '" + DivId + "') AND (EmpNo = '" + empno + "') AND (ActiveEmployee = 1) ORDER BY EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    empname = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return empname;
        }


        public Int32 GetEmployeeCategoryByEmpNo(String empno, String DivId)
        {
            int empCat=0 ;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT   TOP (100) PERCENT EmpNo, EPFNo, EMPName, EmpCategory FROM  dbo.EmployeeMaster where DivisionID='" + DivId + "' and   EmpNo='" + empno + "' ORDER BY EPFNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(3))
                {
                    empCat = dataReader.GetInt32(3);
                }
            }
            dataReader.Close();
            return empCat;
        }
       
        private String strEstateID;
        public String StrEstateID
        {
            get { return strEstateID; }
            set { strEstateID = value; }
        }

        private String strDivisionID;
        public String StrDivisionID
        {
            get { return strDivisionID; }
            set { strDivisionID = value; }
        }

        private String strEmpno;
        public String StrEmpno
        {
            get { return strEmpno; }
            set { strEmpno = value; }
        }

        private String strEpfNo;
        public String StrEpfNo
        {
            get { return strEpfNo; }
            set { strEpfNo = value; }
        }

        private String strEmpName;
        public String StrEmpName
        {
            get { return strEmpName; }
            set { strEmpName = value; }
        }

        private String strGender;
        public String StrGender
        {
            get { return strGender; }
            set { strGender = value; }
        }

        private Boolean ActiveEmp;
        public Boolean ActiveEmp1
        {
            get { return ActiveEmp; }
            set { ActiveEmp = value; }
        }

        private String strStatus;
        public String StrStatus
        {
            get { return strStatus; }
            set { strStatus = value; }
        }

        private DateTime datDateJoin;
        public DateTime DatDateJoin
        {
            get { return datDateJoin; }
            set { datDateJoin = value; }
        }
        private String strNICNo;

        public String StrNICNo
        {
            get { return strNICNo; }
            set { strNICNo = value; }
        }

        //private String strEmpCategory;

        //public String StrEmpCategory
        //{
        //    get { return strEmpCategory; }
        //    set { strEmpCategory = value; }
        //}

        private Int32 intEmpCategory;

        public Int32 IntEmpCategory
        {
            get { return intEmpCategory; }
            set { intEmpCategory = value; }
        }

        private String strMaritalStatus;

        public String StrMaritalStatus
        {
            get { return strMaritalStatus; }
            set { strMaritalStatus = value; }
        }

        private Int32 intUnionCode;

        public Int32 IntUnionCode
        {
            get { return intUnionCode; }
            set { intUnionCode = value; }
        }

        private Int32 intGangcode;

        public Int32 IntGangcode
        {
            get { return intGangcode; }
            set { intGangcode = value; }
        }

        private Int32 intBasicJob;

        public Int32 IntBasicJob
        {
            get { return intBasicJob; }
            set { intBasicJob = value; }
        }

        private DateTime ConfirmDate;

        public DateTime ConfirmDate1
        {
            get { return ConfirmDate; }
            set { ConfirmDate = value; }
        }

        private DateTime ResignedDate;

        public DateTime ResignedDate1
        {
            get { return ResignedDate; }
            set { ResignedDate = value; }
        }

        private DateTime DOB;

        public DateTime DOB1
        {
            get { return DOB; }
            set { DOB = value; }
        }
        private String strContractorNo;

        public String StrContractorNo
        {
            get { return strContractorNo; }
            set { strContractorNo = value; }
        }

        private Int32 intEmptype;

        public Int32 IntEmptype
        {
            get { return intEmptype; }
            set { intEmptype = value; }
        }

        private String strAccountNo;

        public String StrAccountNo
        {
            get { return strAccountNo; }
            set { strAccountNo = value; }
        }
        private String strBranchCode;

        public String StrBranchCode
        {
            get { return strBranchCode; }
            set { strBranchCode = value; }
        }

        public void InsertEmployee()
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeMaster(EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, UnionCode,GangCode, BasicJob, MaritalStatus, ConfirmDate, dateOfBirth, NICNo) VALUES ('" + StrEstateID + "','" + StrDivisionID + "','" + strEmpno + "','" + strEpfNo + "','" + strEmpName + "','" + StrGender + "','" + strStatus + "','" + datDateJoin + "','" + ActiveEmp + "','" + FTSPayRollBL.User.StrUserName + "','" + intEmpCategory + "','" + IntUnionCode + "','" + intGangcode + "','" + intBasicJob + "','" + strMaritalStatus + "','" + ConfirmDate + "','" + DOB + "','" + StrNICNo + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeMaster(EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, MaritalStatus, ConfirmDate, dateOfBirth, NICNo,Contractor,EmpType) VALUES ('" + StrEstateID + "','" + StrDivisionID + "','" + strEmpno + "','" + strEpfNo + "','" + strEmpName + "','" + StrGender + "','" + strStatus + "','" + datDateJoin + "','" + ActiveEmp + "','" + FTSPayRollBL.User.StrUserName + "','" + intEmpCategory + "','"  + strMaritalStatus + "','" + ConfirmDate + "','" + DOB + "','" + StrNICNo + "','"+StrContractorNo+"','"+IntEmptype+"')", CommandType.Text);
        }
        public void UpdateEmployee()
        {
            //SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET EstateID ='" + StrEstateID + "', DivisionID ='" + StrDivisionID + "', EPFNo ='" + strEpfNo + "', EMPName ='" + StrEmpName + "', Gender ='" + StrGender + "', EmployeeStatus ='" + strStatus + "', DateJoined ='" + datDateJoin + "', ActiveEmployee ='" + ActiveEmp + "', UserID ='" + FTSPayRollBL.User.StrUserName + "', EmpCategory ='" + intEmpCategory + "',UnionCode ='" + IntUnionCode + "', GangCode ='" + IntGangcode + "', BasicJob ='" + IntBasicJob + "', MaritalStatus ='" + StrMaritalStatus + "', ConfirmDate ='" + ConfirmDate + "', dateOfBirth ='" + DOB + "',NICNo ='" + StrNICNo + "' WHERE (EmpNo = '" + StrEmpno + "') and (DivisionID='"+StrDivisionID+"')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser])  SELECT  GETDATE() AS Expr1, 0 AS Expr2, 'EmployeeMaster' AS Expr3, DivisionID, EmpNo, EPFNo, Gender, ActiveEmployee, UnionNameCode, UserID,  '"+FTSPayRollBL.User.StrUserName+"' AS Expr4 FROM dbo.EmployeeMaster where (EmpNo = '" + strEmpno + "') and (DivisionID='" + StrDivisionID + "') ", CommandType.Text);            
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  EPFNo ='" + strEpfNo + "', EMPName ='" + StrEmpName + "', Gender ='" + StrGender + "', EmployeeStatus ='" + strStatus + "', DateJoined ='" + datDateJoin + "', ActiveEmployee ='" + ActiveEmp + "', UserID ='" + FTSPayRollBL.User.StrUserName + "', EmpCategory ='" + intEmpCategory + "', MaritalStatus ='" + StrMaritalStatus + "', ConfirmDate ='" + ConfirmDate + "', dateOfBirth ='" + DOB + "',NICNo ='" + StrNICNo + "',Contractor='"+StrContractorNo+"',EmpType='"+IntEmptype+"' WHERE (EmpNo = '" + StrEmpno + "') and (DivisionID='" + StrDivisionID + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser])  SELECT  GETDATE() AS Expr1, 0 AS Expr2, 'EmployeeMaster' AS Expr3, DivisionID, EmpNo, EPFNo, Gender, ActiveEmployee, UnionNameCode, UserID,  '" + FTSPayRollBL.User.StrUserName + "' AS Expr4 FROM dbo.EmployeeMaster where (EmpNo = '" + strEmpno + "') and (DivisionID='" + StrDivisionID + "') ", CommandType.Text);            
        }
        public void UpdateEmployeeActiveState(String strDiv,String empNo,Boolean activeState)
        {
           
                SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + activeState + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
                SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  ActiveEmployee='" + activeState + "' WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
            
        }
        public String CheckAndUpdateEmployeeActiveState(String strDiv, String empNo, Boolean activeState)
        {
            String result = "";
            if (activeState == false)
            {
                if (chkErrors.CheckDailyEntriesOfEmployee(empNo, strDiv))
                {
                    result = "Available";
                }
                else
                {
                    SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + activeState + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
                    SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  ActiveEmployee='" + activeState + "' WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
                    result = "OK";
                }
            }
            else
            {
                SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + activeState + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
                SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  ActiveEmployee='" + activeState + "' WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
                result = "OK";
            }
            return result;
        }

        public void UpdateEmployeeDetailsGrid(String strDiv, String empNo, String strEpf, String nic, String LastName, String Initials, String OCGrade, String Zone, String MemberStatus, String employer, String acNo)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO  [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', '" + acNo + "', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  EPFNo='" + strEpf + "',NICNo='" + nic + "',LastName='" + LastName + "',Initials='" + Initials + "', OCGrade='" + OCGrade + "', ZoneCode='" + Zone + "', MemberStatus='" + MemberStatus + "', EmployerNo='" + employer + "',AccountNo='" + acNo + "' WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
        }

        public void UpdateEmployeeDetails(String strDiv, String empNo, String strEpf, String nic, String LastName, String Initials, String OCGrade, String Zone, String MemberStatus, String employer)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  EPFNo='" + strEpf + "' ,NICNo='" + nic + "',LastName='" + LastName + "',Initials='" + Initials + "', OCGrade='" + OCGrade + "', ZoneCode='" + Zone + "', MemberStatus='" + MemberStatus + "', EmployerNo='" + employer + "' WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
        }
        public void UpdateEmployeeDetails(String strDiv, String empNo, String strEpf, String nic, DateTime dtDOB, DateTime dtJoined, String LastName, String Initials, String OCGrade, String Zone, String MemberStatus, String employer)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  EPFNo='" + strEpf + "' ,NICNo='" + nic + "',dateOfBirth='" + dtDOB + "',DateJoined='" + dtJoined + "',LastName='" + LastName + "',Initials='" + Initials + "', OCGrade='" + OCGrade + "', ZoneCode='" + Zone + "', MemberStatus='" + MemberStatus + "', EmployerNo='" + employer + "' WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
        }

        public void UpdateEmployerNoToAllDivision(String strDiv, String empNo, String strEmployer)
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  EmployerNo='" + strEmployer + "'  WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
        }

        public void UpdateZoneCodeToAllDivision(String strDiv, String empNo, String strZone)
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  ZoneCode='" + strZone + "'  WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
        }
        public void UpdateOCGradeToAllDivision(String strDiv, String empNo, String strOC)
        {
            //SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[UpdateLog] ([UpdatedDate] ,[RefNo] ,[ReferenceTable] ,[Division] ,[EmpNo] ,[Narration1] ,[Narration2] ,[Narration3] ,[Narration4] ,[Narration5] ,[UpdatedUser]) VALUES ( GETDATE(), 0 , 'ActivateEmployee' ,'" + strDiv + "', '" + empNo + "',  '" + strEpf + "', 'NA', 'NA',  'NA' ,'NA','" + FTSPayRollBL.User.StrUserName + "') ", CommandType.Text);
            SQLHelper.ExecuteNonQuery("UPDATE EmployeeMaster SET  OCGrade='" + strOC + "'  WHERE (EmpNo = '" + empNo + "') and (DivisionID='" + strDiv + "')", CommandType.Text);
        }
        public Boolean CheckUnionAvailability(String unionCode)
        {
            Boolean boolAvail=false;
            DataTable dt=new DataTable();
            SqlDataReader reader;
            dt.Columns.Add("UnionName");
            reader = SQLHelper.ExecuteReader("SELECT Deduction FROM dbo.EmployeeUnions WHERE (Deduction = '"+unionCode+"')",CommandType.Text);
            while(reader.Read())
            {
                if(!reader.IsDBNull(0))
                {
                    if(reader.GetString(0).Trim().ToUpper().Equals(unionCode.ToUpper()))
                    {
                        boolAvail=true;
                    }
                }
            }
            reader.Close();
            return boolAvail;            
        }

        public void DeleteEmployee()
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO [dbo].[DeleteLog]([DeletedDate],[RefNo],[ReferenceTable],[EmpNo],[Amount],[Narration1],[Naration2],DeletedBy) VALUES(getdate(),'0','EmployeeMaster','" + StrEmpno + "' ,'0.00','" + StrEmpName + "','" + StrDivisionID + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
            SQLHelper.ExecuteNonQuery("DELETE FROM EmployeeMaster WHERE (EmpNo = '" + strEmpno + "') and (DivisionID='"+StrDivisionID+"')", CommandType.Text);
        }
        

        public DataTable ListAllEmployess()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EMPNO"));
            dt.Columns.Add(new DataColumn("EPFNO"));
            dt.Columns.Add(new DataColumn("EmployeeName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("EmployeeStatus"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("EmpCategory"));
            //dt.Columns.Add(new DataColumn("Gang"));
            //dt.Columns.Add(new DataColumn("BasicJob"));
            dt.Columns.Add(new DataColumn("MaritalStatus"));
            dt.Columns.Add(new DataColumn("ConfirmDate"));
            //dt.Columns.Add(new DataColumn("ResignedDate"));
            dt.Columns.Add(new DataColumn("DateOfBirth"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("NIC"));
            dt.Columns.Add(new DataColumn("Union"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("EmpType"));
            //kalana begins
            dt.Columns.Add(new DataColumn("LastName"));
            dt.Columns.Add(new DataColumn("Initials"));
            dt.Columns.Add(new DataColumn("OCGrade"));
            dt.Columns.Add(new DataColumn("ZoneCode"));
            dt.Columns.Add(new DataColumn("MemStatus"));
            dt.Columns.Add(new DataColumn("EmployerNo"));
            //kalana end

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, UnionCode,GangCode, BasicJob, MaritalStatus, ConfirmDate, dateOfBirth, CreateDateTime,NICNo FROM EmployeeMaster", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, MaritalStatus, ConfirmDate, dateOfBirth, CreateDateTime,NICNo,UnionNameCode, Contractor,EmpType,LastName,Initials,OCGrade,ZoneCode,MemberStatus,EmployerNo FROM EmployeeMaster", CommandType.Text);

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
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetBoolean(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetInt32(10);
                }
                //if (!dataReader.IsDBNull(11))
                //{
                //    dtrow[11] = dataReader.GetInt32(11);
                //}
                //if (!dataReader.IsDBNull(12))
                //{
                //    dtrow[12] = dataReader.GetInt32(12);
                //}
                //if (!dataReader.IsDBNull(13))
                //{
                //    dtrow[13] = dataReader.GetInt32(13);
                //}
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetString(11).Trim();
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDateTime(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDateTime(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDateTime(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15).Trim();
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetString(17).Trim();
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetInt32(18);
                }
                //kalana begins
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19).Trim();
                }
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetString(20).Trim();
                }
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetString(21).Trim();
                }
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetString(22).Trim();
                }
                if (!dataReader.IsDBNull(23))
                {
                    dtrow[23] = dataReader.GetString(23).Trim();
                }
                if (!dataReader.IsDBNull(24))
                {
                    dtrow[24] = dataReader.GetString(24).Trim();
                }
                //kalana end
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListAllEmployess(String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EMPNO"));
            dt.Columns.Add(new DataColumn("EPFNO"));
            dt.Columns.Add(new DataColumn("EmployeeName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("EmployeeStatus"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("EmpCategory"));
            //dt.Columns.Add(new DataColumn("Gang"));
            //dt.Columns.Add(new DataColumn("BasicJob"));
            dt.Columns.Add(new DataColumn("MaritalStatus"));
            dt.Columns.Add(new DataColumn("ConfirmDate"));
            //dt.Columns.Add(new DataColumn("ResignedDate"));
            dt.Columns.Add(new DataColumn("DateOfBirth"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("NIC"));
            dt.Columns.Add(new DataColumn("Union"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("EmpType"));
            //kalana begins
            dt.Columns.Add(new DataColumn("LastName"));
            dt.Columns.Add(new DataColumn("Initials"));
            dt.Columns.Add(new DataColumn("OCGrade"));
            dt.Columns.Add(new DataColumn("ZoneCode"));
            dt.Columns.Add(new DataColumn("MemStatus"));
            dt.Columns.Add(new DataColumn("EmployerNo"));
            //kalana end

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, UnionCode,GangCode, BasicJob, MaritalStatus, ConfirmDate, dateOfBirth, CreateDateTime,NICNo FROM EmployeeMaster", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, MaritalStatus, ConfirmDate, dateOfBirth, CreateDateTime,NICNo,UnionNameCode, Contractor,EmpType, LastName, Initials, OCGrade, ZoneCode, MemberStatus, EmployerNo FROM EmployeeMaster WHERE (DivisionID = '" + strDiv + "') ", CommandType.Text);

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
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetBoolean(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetInt32(10);
                }
                //if (!dataReader.IsDBNull(11))
                //{
                //    dtrow[11] = dataReader.GetInt32(11);
                //}
                //if (!dataReader.IsDBNull(12))
                //{
                //    dtrow[12] = dataReader.GetInt32(12);
                //}
                //if (!dataReader.IsDBNull(13))
                //{
                //    dtrow[13] = dataReader.GetInt32(13);
                //}
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetString(11).Trim();
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDateTime(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDateTime(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDateTime(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15).Trim();
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetString(17).Trim();
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetInt32(18);
                }
                //kalana begins
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19).Trim();
                }
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetString(20).Trim();
                }
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetString(21).Trim();
                }
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetString(22).Trim();
                }
                if (!dataReader.IsDBNull(23))
                {
                    dtrow[23] = dataReader.GetString(23).Trim();
                }
                if (!dataReader.IsDBNull(24))
                {
                    dtrow[24] = dataReader.GetString(24).Trim();
                }
                //kalana end
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable ListAllEmployess(String strDiv,String strEmp)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EstateID"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("EMPNO"));
            dt.Columns.Add(new DataColumn("EPFNO"));
            dt.Columns.Add(new DataColumn("EmployeeName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("EmployeeStatus"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("EmpCategory"));
            //dt.Columns.Add(new DataColumn("Gang"));
            //dt.Columns.Add(new DataColumn("BasicJob"));
            dt.Columns.Add(new DataColumn("MaritalStatus"));
            dt.Columns.Add(new DataColumn("ConfirmDate"));
            //dt.Columns.Add(new DataColumn("ResignedDate"));
            dt.Columns.Add(new DataColumn("DateOfBirth"));
            dt.Columns.Add(new DataColumn("CreateDateTime"));
            dt.Columns.Add(new DataColumn("NIC"));
            dt.Columns.Add(new DataColumn("Union"));
            dt.Columns.Add(new DataColumn("Contractor"));
            dt.Columns.Add(new DataColumn("EmpType"));
            //kalana begins
            dt.Columns.Add(new DataColumn("LastName"));
            dt.Columns.Add(new DataColumn("Initials"));
            dt.Columns.Add(new DataColumn("OCGrade"));
            dt.Columns.Add(new DataColumn("ZoneCode"));
            dt.Columns.Add(new DataColumn("MemStatus"));
            dt.Columns.Add(new DataColumn("EmployerNo"));
            //kalana end

            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, UnionCode,GangCode, BasicJob, MaritalStatus, ConfirmDate, dateOfBirth, CreateDateTime,NICNo FROM EmployeeMaster", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmpNo, EPFNo, EMPName, Gender, EmployeeStatus, DateJoined, ActiveEmployee, UserID, EmpCategory, MaritalStatus, ConfirmDate, dateOfBirth, CreateDateTime,NICNo,UnionNameCode, Contractor,EmpType, LastName, Initials, OCGrade, ZoneCode, MemberStatus, EmployerNo FROM EmployeeMaster WHERE (DivisionID = '" + strDiv + "') and (EmpNo='" + strEmp + "')", CommandType.Text);

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
                    dtrow[6] = dataReader.GetString(6).Trim();
                }
                if (!dataReader.IsDBNull(7))
                {
                    dtrow[7] = dataReader.GetDateTime(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    dtrow[8] = dataReader.GetBoolean(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    dtrow[9] = dataReader.GetString(9).Trim();
                }
                if (!dataReader.IsDBNull(10))
                {
                    dtrow[10] = dataReader.GetInt32(10);
                }
                //if (!dataReader.IsDBNull(11))
                //{
                //    dtrow[11] = dataReader.GetInt32(11);
                //}
                //if (!dataReader.IsDBNull(12))
                //{
                //    dtrow[12] = dataReader.GetInt32(12);
                //}
                //if (!dataReader.IsDBNull(13))
                //{
                //    dtrow[13] = dataReader.GetInt32(13);
                //}
                if (!dataReader.IsDBNull(11))
                {
                    dtrow[11] = dataReader.GetString(11).Trim();
                }
                if (!dataReader.IsDBNull(12))
                {
                    dtrow[12] = dataReader.GetDateTime(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    dtrow[13] = dataReader.GetDateTime(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    dtrow[14] = dataReader.GetDateTime(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    dtrow[15] = dataReader.GetString(15).Trim();
                }
                if (!dataReader.IsDBNull(16))
                {
                    dtrow[16] = dataReader.GetString(16).Trim();
                }
                if (!dataReader.IsDBNull(17))
                {
                    dtrow[17] = dataReader.GetString(17).Trim();
                }
                if (!dataReader.IsDBNull(18))
                {
                    dtrow[18] = dataReader.GetInt32(18);
                }
                //kalana begins
                if (!dataReader.IsDBNull(19))
                {
                    dtrow[19] = dataReader.GetString(19).Trim();
                }
                if (!dataReader.IsDBNull(20))
                {
                    dtrow[20] = dataReader.GetString(20).Trim();
                }
                if (!dataReader.IsDBNull(21))
                {
                    dtrow[21] = dataReader.GetString(21).Trim();
                }
                if (!dataReader.IsDBNull(22))
                {
                    dtrow[22] = dataReader.GetString(22).Trim();
                }
                if (!dataReader.IsDBNull(23))
                {
                    dtrow[23] = dataReader.GetString(23).Trim();
                }
                if (!dataReader.IsDBNull(24))
                {
                    dtrow[24] = dataReader.GetString(24).Trim();
                }
                //kalana end
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getEmployeeDetailsByDivision(String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EPFN0"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryName, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.DivisionID = '"+strDiv+"')", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getEmployeeDetailsByDivision(String strDiv, Boolean boolActive)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EPFN0"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND (dbo.EmployeeMaster.ActiveEmployee = 1)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getContractorDetailsByDivision(String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EPFN0"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            //dataReader = SQLHelper.ExecuteReader("SELECT  dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryName,  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.DivisionID = '"+strDiv+"') AND (dbo.EmployeeCategory.CategoryShortName = 'C')", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT  dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName,  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND (dbo.EmployeeMaster.EmpType = 1)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getEmpTypeContractorDetailsByDivision(String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EPFN0"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName,  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND  (EmpType = 1)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getContractorDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EPFN0"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName,  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.EmpType = 1)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;
        }

        public DataTable getActiveEmployeeDetailsByDivision(String strDiv, DateTime dtFrom,DateTime dtTo)
        {
            DataTable dtUnique = new DataTable();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EPFN0"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Gender"));
            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("DivisionID"));
            dt.Columns.Add(new DataColumn("DateJoined"));
            dt.Columns.Add(new DataColumn("ActiveEmployee"));
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName, dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID CROSS JOIN dbo.CHKCompany WHERE (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND (ActiveEmployee=1)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            //Add inactive employees
            dataReader = SQLHelper.ExecuteReader("SELECT        dbo.EmployeeMaster.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName,  dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM            dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID INNER JOIN dbo.EstateDivision ON dbo.EmployeeMaster.DivisionID = dbo.EstateDivision.DivisionID INNER JOIN dbo.DailyGroundTransactions ON dbo.EmployeeMaster.DivisionID = dbo.DailyGroundTransactions.DivisionID AND  dbo.EmployeeMaster.EmpNo = dbo.DailyGroundTransactions.EmpNo CROSS JOIN dbo.CHKCompany WHERE        (dbo.EmployeeMaster.DivisionID = '" + strDiv + "') AND (dbo.EmployeeMaster.ActiveEmployee = 0) AND (dbo.DailyGroundTransactions.DateEntered BETWEEN  CONVERT(DATETIME, '" + dtFrom + "', 102) AND CONVERT(DATETIME, '" + dtTo + "', 102)) AND (dbo.EmployeeMaster.EmployeeStatus IN ('Inactive', 'Terminated'))", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            //Add Inactive But Recoveries Available
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKFixedDeductions.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName, dbo.CHKFixedDeductions.DivisionId, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.CHKFixedDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKFixedDeductions.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.CHKFixedDeductions.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.CHKFixedDeductions.DivisionId = '" + strDiv + "')  AND (dbo.CHKFixedDeductions.CloseYesNo = 0) AND (dbo.CHKFixedDeductions.BalanceAmount > 0) AND (dbo.CHKFixedDeductions.BalanceAmount IS NOT NULL) AND (dbo.CHKFixedDeductions.OldEntryYesNo = 0) AND (dbo.CHKFixedDeductions.DeductionGroupId  IN (SELECT DeductionGroupCode FROM dbo.CHKDeductionGroup WHERE (ShortName IN ('MA','FA','FS','TCO')))) AND (CONVERT(datetime, CONVERT(varchar(50), dbo.CHKFixedDeductions.StartYear) + '-' + CONVERT(varchar(50), dbo.CHKFixedDeductions.StartMonth) + '-1', 102) < DATEADD(MM, 1, '" + dtFrom + "')) AND (dbo.EmployeeMaster.ActiveEmployee = 0)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            //Add Inactive But RFT Deductions Available
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKRFTDeductions.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName, dbo.CHKRFTDeductions.Division, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.CHKRFTDeductions INNER JOIN dbo.EmployeeMaster ON dbo.CHKRFTDeductions.EmpNo = dbo.EmployeeMaster.EmpNo AND dbo.CHKRFTDeductions.Division = dbo.EmployeeMaster.DivisionID INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.CHKRFTDeductions.Division = '" + strDiv + "')  AND (dbo.CHKRFTDeductions.DYear = '" + dtFrom.Year + "') AND (dbo.CHKRFTDeductions.DMonth = '" + dtTo.Month + "') AND (dbo.CHKRFTDeductions.OldEntryYesNo = 0) AND (dbo.EmployeeMaster.ActiveEmployee = 0)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            //Add Inactive But Debts Available
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.ChkDebtors.EmpNO, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName, dbo.ChkDebtors.DivisionId, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.ChkDebtors INNER JOIN dbo.EmployeeMaster ON dbo.ChkDebtors.DivisionId = dbo.EmployeeMaster.DivisionID AND dbo.ChkDebtors.EmpNO = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.ChkDebtors.DivisionId = '" + strDiv + "')  AND (dbo.ChkDebtors.DebtYear = '" + dtFrom.AddMonths(-1).Year + "') AND (dbo.ChkDebtors.DebtMonth = '" + dtFrom.AddMonths(-1).Month + "') AND (dbo.ChkDebtors.PaidAdditionYesNo = 0) AND (dbo.ChkDebtors.RecoveredYesNO = 0) AND (dbo.ChkDebtors.DebtAmount > 0) AND (dbo.EmployeeMaster.ActiveEmployee = 0)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            //Add Inactive But Coins Available
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.CHKMadeUpCoins.EmpNo, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName, dbo.CHKMadeUpCoins.DivisionID, dbo.EmployeeMaster.DateJoined, dbo.EmployeeMaster.ActiveEmployee FROM dbo.CHKMadeUpCoins INNER JOIN dbo.EmployeeMaster ON dbo.CHKMadeUpCoins.DivisionID = dbo.EmployeeMaster.DivisionID AND dbo.CHKMadeUpCoins.EmpNo = dbo.EmployeeMaster.EmpNo INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (dbo.CHKMadeUpCoins.DivisionID = '" + strDiv + "') AND (dbo.CHKMadeUpCoins.ProcessYear ='" + dtFrom.AddMonths(-1).Year + "') AND (dbo.CHKMadeUpCoins.ProcessMonth = '" + dtFrom.AddMonths(-1).Month + "') AND (dbo.EmployeeMaster.ActiveEmployee = 0) AND (dbo.CHKMadeUpCoins.MadeUpCoins > 0)", CommandType.Text);

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
                    dtrow[7] = dataReader.GetBoolean(7);
                }
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();

            string[] TobeDistinct = { "EmpNo", "EPFN0", "EmpName", "Gender", "Category", "DivisionID", "DateJoined", "ActiveEmployee" };
            dtUnique = dt.DefaultView.ToTable(true, TobeDistinct);
            dt.Dispose();
            return dtUnique;
            //return dt;
        }

        public DataTable ListEmployeeUnions()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Union"));

            DataRow dtrow;
            SqlDataReader reader;
            dtrow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT DivisionID, EmpNo, EMPName, UnionNameCode FROM dbo.EmployeeMaster WHERE (UnionNameCode IS NOT NULL)", CommandType.Text);

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
                    dtrow[3] = reader.GetString(3).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public DataTable ListEmployeeUnions(String strDiv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Division"));
            dt.Columns.Add(new DataColumn("EmpNo"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("Union"));

            DataRow dtrow;
            SqlDataReader reader;
            dtrow = dt.NewRow();
            reader = SQLHelper.ExecuteReader("SELECT DivisionID, EmpNo, EMPName, UnionNameCode FROM dbo.EmployeeMaster WHERE (UnionNameCode IS NOT NULL) AND (DivisionID = '"+strDiv+"')", CommandType.Text);

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
                    dtrow[3] = reader.GetString(3).Trim();
                }

                dt.Rows.Add(dtrow);
            }
            reader.Close();
            return dt;
        }

        public String GetContractorNoByEmpNo(String strDiv,String empno)
        {
            String contractor = "";
            SqlDataReader dataReader;
            //dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT EmpNo,EPFNo, EMPName FROM  dbo.EmployeeMaster where DivisionID='"+DivId+"' and   EmpNo='" + empno + "' AND (ActiveEmployee = 1) ORDER BY EPFNo", CommandType.Text);
            dataReader = SQLHelper.ExecuteReader("SELECT  TOP (100) PERCENT Contractor FROM dbo.EmployeeMaster WHERE (DivisionID='" + strDiv+ "') AND (EmpNo = '" + empno + "') AND (ActiveEmployee = 1) ORDER BY EmpNo", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    contractor = dataReader.GetString(0).Trim();
                }
            }
            dataReader.Close();
            return contractor;
        }

        /*Employee Children details*/

        public void InsertEmployeeChildDetails(String DivisionID, String EmpNo, String ChildName, DateTime DOB, String Gender)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO EmployeeChildrenRegister (EmpNo,DivisionID,ChildName,DateofBirth,Gender,UserID) VALUES ('" + EmpNo + "','" + DivisionID + "','" + ChildName + "','" + DOB + "','" + Gender + "','" + FTSPayRollBL.User.StrUserName + "')", CommandType.Text);
        }

        public void DeleteEmployeeChildDetails(String DivisionID, String EmpNo, String ChildName, DateTime DOB)
        {
            SQLHelper.ExecuteNonQuery("DELETE EmployeeChildrenRegister WHERE (EmpNo='" + EmpNo + "') AND (DivisionID='" + DivisionID + "') AND (ChildName='" + ChildName + "') AND (DateofBirth='" + DOB + "')", CommandType.Text);
        }

        public DataSet GetChildrenDetails(String DivisionID)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM EmployeeChildrenRegister WHERE (DivisionID LIKE '" + DivisionID + "')", CommandType.Text);
            return ds;
        }

        public DataSet GetChildrenDetails(String DivisionID, String EmpNo)
        {
            DataSet ds = new DataSet();
            ds = SQLHelper.FillDataSet("SELECT * FROM EmployeeChildrenRegister WHERE (DivisionID LIKE'" + DivisionID + "') AND (EmpNO='" + EmpNo + "')", CommandType.Text);
            return ds;
        }

        public DataSet ListEmployeesAboveToRetire()
        {
            DataSet ds = new DataSet("EmployeesAboveToRetire");
            ds = SQLHelper.FillDataSet("SELECT     TOP (100) PERCENT dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo, DATEDIFF(YY, dbo.EmployeeMaster.dateOfBirth, GETDATE()) AS Age, dbo.EmployeeMaster.EPFNo, dbo.EmployeeMaster.EMPName, dbo.EmployeeMaster.Gender, dbo.EmployeeCategory.CategoryShortName,  dbo.EmployeeMaster.dateOfBirth, dbo.EmployeeMaster.ActiveEmployee FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE (DATEDIFF(YY, dbo.EmployeeMaster.dateOfBirth, GETDATE()) >= 59) ORDER BY dbo.EmployeeMaster.DivisionID, dbo.EmployeeMaster.EmpNo", CommandType.Text);
            return ds;
        }

        public DataTable ListActiveEmployees(String strDiv, String strGender)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("EmpNo");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID,EmpNo FROM  dbo.EmployeeMaster WHERE (DivisionID LIKE '" + strDiv + "') AND ActiveEmployee=1  AND (Gender LIKE '" + strGender + "')    ORDER BY EmpNo", CommandType.Text);
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

        public DataTable ListActiveEmployees(String strDiv, String strGender,String strWorkCat)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DivisionID");
            dt.Columns.Add("EmpNo");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT DivisionID,EmpNo FROM  dbo.EmployeeMaster WHERE (DivisionID LIKE '" + strDiv + "') AND ActiveEmployee=1  AND (Gender LIKE '" + strGender + "') AND (EmpWorkCategory like '" + strWorkCat + "')   ORDER BY EmpNo", CommandType.Text);
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

        public DataTable ListEmployers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EstateID");
            dt.Columns.Add("Division");
            dt.Columns.Add("EmployerNO");
            dt.Columns.Add("ZoneCode");
            dt.Columns.Add("PayMode");
            dt.Columns.Add("PaymentRef");
            dt.Columns.Add("DOCode");
            DataRow dtRow;
            SqlDataReader dataReader;
            dtRow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT EstateID, DivisionID, EmployerNO, ZoneCode, PayMode, PaymentRef, DistrictOfficeCode FROM dbo.CHKEPFEmployer ORDER BY EmployerNO", CommandType.Text);
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
                    dtRow[5] = dataReader.GetString(5).Trim();
                }
                if (!dataReader.IsDBNull(6))
                {
                    dtRow[6] = dataReader.GetString(6).Trim();
                }
                dt.Rows.Add(dtRow);
            }
            dataReader.Close();
            return dt;
        }

        public Boolean IsSLMFEmployee(String strDiv,String strEmp)
        {
            SqlDataReader SLMFEmployeeReader;
            Boolean boolIsSLMF = false;
            SLMFEmployeeReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeCategory.CategoryShortName FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE     (dbo.EmployeeMaster.DivisionID = '"+strDiv+"') AND (dbo.EmployeeMaster.EmpNo = '"+strEmp+"')",CommandType.Text);
            while (SLMFEmployeeReader.Read())
            {
                if (!SLMFEmployeeReader.IsDBNull(0))
                {
                    if (SLMFEmployeeReader.GetString(0).Trim().Equals("SLMF"))
                    {
                        boolIsSLMF = true;
                    }
                }
            }
            return boolIsSLMF;
        }

        public Boolean IsEPFEntitled(String empno, String strdiv)
        {
            Boolean boolEntitled = false;
            SqlDataReader dataReader;
            dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeCategory.EPFEntitled FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE        (dbo.EmployeeMaster.DivisionID = '" + strdiv + "') AND (dbo.EmployeeMaster.EmpNo = '" + empno + "')", CommandType.Text);
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    boolEntitled = dataReader.GetBoolean(0);
                }
            }
            dataReader.Close();
            return boolEntitled;
        }

        public Boolean IsEPFEntitled(String empno, String strdiv, Int16 intWorkType)
        {
            Boolean boolEntitled = false;
            if (intWorkType == 1)
            {
                SqlDataReader dataReader;
                dataReader = SQLHelper.ExecuteReader("SELECT dbo.EmployeeCategory.EPFEntitled FROM dbo.EmployeeMaster INNER JOIN dbo.EmployeeCategory ON dbo.EmployeeMaster.EmpCategory = dbo.EmployeeCategory.CategoryID WHERE        (dbo.EmployeeMaster.DivisionID = '" + strdiv + "') AND (dbo.EmployeeMaster.EmpNo = '" + empno + "')", CommandType.Text);
                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        boolEntitled = dataReader.GetBoolean(0);
                    }
                }
                dataReader.Close();
            }
            else
            {
                boolEntitled = true;
            }
            return boolEntitled;
        }

        public DataSet GetEmployeeDetailsByEmpNo(String StrDiv, String empno)
        {
            DataSet dsEmpDetails = new DataSet();
            dsEmpDetails = SQLHelper.FillDataSet("SELECT  TOP (100) PERCENT DivisionID,EmpNo,EPFNo, EMPName,EmployeeStatus,ActiveEmployee FROM  dbo.EmployeeMaster where (DivisionID='" + StrDiv + "') and  (EmpNo='" + empno + "')  ", CommandType.Text);
            
            return dsEmpDetails;
        }
        public DataSet getEmployeeLastWorkDetails(String strDiv, String empno)
        {
            DataSet dsEmpLastWork = new DataSet();
            dsEmpLastWork = SQLHelper.FillDataSet("SELECT TOP (1) DateEntered AS LastWorkDate, DailyBasicAmount FROM dbo.DailyGroundTransactions WHERE (NOT (WorkCodeID LIKE 'X%')) AND (NOT (WorkCodeID IN ('ABS', 'PH'))) AND (DivisionID = '" + strDiv + "') AND (EmpNo = '" + empno + "') ORDER BY LastWorkDate DESC", CommandType.Text);
            return dsEmpLastWork;
        }


    }
}
