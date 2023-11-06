using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BusinessObject;

namespace DataAccess
{
    public class customerDA
    {
        public MySqlDataReader InsertCustomer(Customer customer)
        {
            try
            {
                MySqlParameter[] myparams = new MySqlParameter[] //We set the parameters if meron man, basically 1 variable has 2 parameters.
                {
                    new MySqlParameter("p_Name", customer.Name),
                    new MySqlParameter("p_Age", customer.Age),
                    new MySqlParameter("p_Gender", customer.Gender),
                    new MySqlParameter("p_Birthday", customer.Birthday),
                    new MySqlParameter("p_Address", customer.Address),
                    new MySqlParameter("p_Email", customer.Email),
                    new MySqlParameter("p_Phone", customer.Phone),
                    new MySqlParameter("p_profilePicture", customer.ProfileImage),
                };
                //Storage procedure           , Command Type               , parameters
                return DBHelper.ExecuteParameterizedReader("InsertCustomer", CommandType.StoredProcedure, myparams);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

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
