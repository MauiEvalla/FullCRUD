using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataAccess;
using BusinessObject;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace BusinessLogic
{
    public class customerBL
    {
        public int InsertCustomer(Customer customer)
        {
            customerDA userDA = new customerDA();
            try
            {
                userDA.InsertCustomer(customer);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader GetAllCustomers() //GetAllUser Storage Procedure
        {
            customerDA customerDA = new customerDA();
            try
            {
                return customerDA.GetAllCustomers();
            }

            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
