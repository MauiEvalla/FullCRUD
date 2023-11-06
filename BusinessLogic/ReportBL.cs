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
using System.Configuration;
using System.Data;


namespace BusinessObject
{
    public class ReportBL
    {
        DataTable dataTable = new DataTable();
        MySqlConnection MySQLConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);


        public DataTable viewData()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM users.customers", MySQLConn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);

            return dataTable;
            
        }
    }
}
