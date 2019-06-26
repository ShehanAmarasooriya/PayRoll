using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace FTSPayRollBL
{
    public class Validation
    {
        public Boolean ExpenditureJournalValidation(DateTime date)
        {
            Boolean boolAvail = false;
            //string JournalNo = "EX/" + date.Year.ToString() + "/" + date.ToString("MMMM").Substring(0, 3);
            //SqlDataReader dataReader = SQLHelperGL.ExecuteReader("SELECT COUNT(VoucherNo) AS Expr1 FROM [MadulsimaEstateGL].[dbo].[JournalVoucher] WHERE (VoucherNo = '" + JournalNo.Trim() + "')", CommandType.Text);

            //while (dataReader.Read())
            //{
            //    if (!dataReader.IsDBNull(0))
            //    {
            //        if (dataReader.GetInt32(0) > 0)
            //        {
            //            boolAvail = true;
            //        }
            //    }
            //}
            boolAvail = false;

            return boolAvail;
        }

        public Boolean ExpenditureJournalValidation(Int32 intYear, Int32 intMonth)
        {
            //DateTime dtDate=new DateTime(intYear,intMonth,1);
            Boolean boolRtnVal = false;
            //boolRtnVal =ExpenditureJournalValidation(dtDate);
            boolRtnVal = false;

            return boolRtnVal;
        }
    }
}
