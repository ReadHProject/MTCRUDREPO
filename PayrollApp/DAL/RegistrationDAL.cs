using PayrollApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PayrollApp.DAL
{
    public class RegistrationDAL
    {
        public static int InsertEmployeeMaster(EmployeeMasterModel Model)
        {
            SqlParameter[] sqlparam = new SqlParameter[4];
            sqlparam[0] = new SqlParameter("@MODE","InsertEmployeeMaster");
            sqlparam[1] = new SqlParameter("@EmpName", Model.EMPNAME);
            sqlparam[2] = new SqlParameter("@EmpDesignation", Model.EMPDESIGNATION);
            sqlparam[3] = new SqlParameter("@EmpMobile", Model.MOBILE);
            return DBHelper.ExecuteNonQuery("SP_INSERTEMPLOYEE", sqlparam);
        }
    }
}