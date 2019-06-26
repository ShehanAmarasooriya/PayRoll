using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace FTSPayRollBL
{
    public class EmployeeMovements
    {
        

        public DateTime GetStartDate(int intYear, int intMonth)
        {
            
            
                return new DateTime(intYear, intMonth, 1);
            

        }

        public DateTime GetEndDateOfMonth(DateTime dtDate)
        {
            if (dtDate != null)
            {
                return dtDate.AddMonths(1).AddDays(-1);
            }
            
        }

        public DataSet GetEmployeeMovements1(int intYear, int intMonth)
        {            
            DateTime dtSDate;
            DateTime dtEDate;
            dtSDate = GetStartDate(intYear, intMonth);
            dtEDate = GetEndDateOfMonth(dtSDate);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand(" SELECT ISNULL(ResignedDt.Resigned, 0) AS Resigned, ISNULL(DateJoinedDt.Joined, 0) AS Joined, ResignedDt.EmpDivision  "+
                                                                  " FROM (SELECT COUNT(EmpNo) AS Resigned, EmpDivision FROM dbo.EmployeeCurrentStatus WHERE (ResignedDate BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') " +
                                                                  " GROUP BY EmpDivision) AS ResignedDt FULL OUTER JOIN (SELECT COUNT(EmpNo) AS Joined, DivisionID FROM dbo.EmployeeMaster AS EmployeeMaster_1  "+
                                                                  " WHERE (DateJoined BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') GROUP BY DivisionID) AS DateJoinedDt ON ResignedDt.EmpDivision = DateJoinedDt.DivisionID ", CommandType.Text); 

            //(" SELECT ISNULL(ResignedDt.Resigned, 0) AS Resigned, ISNULL(DateJoinedDt.Joined, 0) AS Joined, ResignedDt.DivisionID AS EmpDivisionId FROM (SELECT COUNT(EmpNo) " +
            //                                      " AS Resigned, DivisionID FROM EmployeeMaster WHERE (ResignedDate BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') GROUP BY DivisionID ) " +
            //                                      " AS ResignedDt FULL OUTER JOIN (SELECT COUNT(EmpNo) AS Joined, DivisionID FROM EmployeeMaster AS EmployeeMaster_1 WHERE (DateJoined " +
            //                                      " BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') GROUP BY DivisionID) AS DateJoinedDt ON ResignedDt.DivisionID = DateJoinedDt.DivisionID " +
            //                                      " ", CommandType.Text);
            da.Fill(ds, "empMovement");
            return ds;




            //ds = DataAccess.SQLHelper.FillDataSet(" SELECT isnull(ResignedDt.c2,0) as c2, isnull(DateJoinedDt.c1,0) as c1 ,DateJoinedDt.DivisionID " +
            //                                      " FROM (SELECT COUNT(EmpNo) AS c2 ,DivisionID FROM dbo.EmployeeMaster " +
            //                                      " WHERE (ResignedDate BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') group by DivisionID) " +
            //                                      " AS ResignedDt full outer JOIN (SELECT COUNT(EmpNo) AS c1 ,DivisionID " +
            //                                      " FROM dbo.EmployeeMaster WHERE (DateJoined BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') group by DivisionID) AS DateJoinedDt on ResignedDt.DivisionID = DateJoinedDt.DivisionID " +
            //                                      " ", CommandType.Text);

            //return ds;


        }

        public DataSet GetEmployeeMovements(int intYear, int intMonth, String strDivisionID)
        {
            DateTime dtSDate;
            DateTime dtEDate;
            dtSDate = GetStartDate(intYear, intMonth);
            dtEDate = GetEndDateOfMonth(dtSDate);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = DataAccess.SQLHelper.CreateCommand(" SELECT ISNULL(ResignedDt.Resigned, 0) AS Resigned, ISNULL(DateJoinedDt.Joined, 0) AS Joined, ResignedDt.EmpDivision AS EmpDivisionId FROM (SELECT COUNT(EmpNo) "+
                                                                  " AS Resigned, EmpDivision FROM EmployeeCurrentStatus WHERE (ResignedDate BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') AND (EmpDivision = '" + strDivisionID + "') GROUP BY EmpDivision) "+
                                                                  " AS ResignedDt FULL OUTER JOIN (SELECT COUNT(EmpNo) AS Joined, DivisionID FROM EmployeeMaster AS EmployeeMaster_1 WHERE (DateJoined "+
                                                                  " BETWEEN '" + dtSDate + "' AND '" + dtEDate + "') AND (DivisionID = '" + strDivisionID + "') GROUP BY DivisionID) AS DateJoinedDt ON ResignedDt.EmpDivision = DateJoinedDt.DivisionID    " + 
                                                                  " ", CommandType.Text);
            da.Fill(ds, "empMovement");
            return ds;
            
            
        }

        


        
    }
}
