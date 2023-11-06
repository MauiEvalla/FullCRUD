using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccess
{

    public class ReportHelper
    {
        public MySqlDataReader GetAllCustomers() //GridView Display!
        {
            try
            {
                //Since no parameters, just call for execution.
                return DBHelper.ExecuteReader("GetAllCustomers", CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
