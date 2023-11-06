using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DataAccess
{
    public class DBHelper //Literally the brain of the SQL commands 
    {
        /*This is the class that you will find all of the required commands for SQL! 
         * Basically all the things you need for execution, SQL connection, DataSets and readers.
         * Just as the name suggests - DB HELPER, this is just a supporting class for UserDA. 
         * UserDA basically acts as the one to talk to this class, mediator si UserDA to this database class
         * 
         * 
         * */
        public static DataSet GetData(string storedProcedureName, params MySqlParameter[] parameters)
        {
            string xCONN = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            DataSet dsCONN = new DataSet();

            using (MySqlConnection MySQLConn = new MySqlConnection(xCONN))
            {
                using (MySqlCommand cmd = new MySqlCommand(storedProcedureName, MySQLConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        try
                        {
                            adapter.Fill(dsCONN);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            return dsCONN;
        }

        public static MySqlDataReader ExecuteReader(string CommandName, CommandType CmdType)
        {
            string xCONN = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            MySqlConnection MySQLConn = new MySqlConnection(xCONN);

            try
            {
                //Open connection
                MySQLConn.Open();

                MySqlCommand cmd = new MySqlCommand(CommandName, MySQLConn);
                cmd.CommandType = CommandType.StoredProcedure;

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MySqlDataReader ExecuteParameterizedReader(string CommandName, CommandType CmdType, MySqlParameter[] param)
        {
            string xCONN = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            MySqlConnection MySQLConn = new MySqlConnection(xCONN);

            try
            {
                //Open connection
                MySQLConn.Open();

                MySqlCommand cmd = new MySqlCommand(CommandName, MySQLConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        static internal string ExecuteNonQueryParam(string CommandName, CommandType cmdType, MySqlParameter[] param)
        {
            string xCONN = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            var result = 0;
            System.Data.DataSet dsCONN = new System.Data.DataSet();
            MySqlConnection MySQLConn = new MySqlConnection(xCONN);
            MySQLConn.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(CommandName, MySQLConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                result = cmd.ExecuteNonQuery();

                return result.ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                MySQLConn.Close();
            }
        }

        static internal DataSet ExecuteReaders(string CommandName, CommandType CmdType)
        {
            string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            MySqlConnection MySQLConn = new MySqlConnection(CONNECTION_STRING);
            DataSet ds = new DataSet();
            MySQLConn.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(CommandName, MySQLConn);
                cmd.CommandType = CmdType;
                cmd.CommandText = CommandName;

                MySqlDataAdapter MySQLlAdap = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                MySQLlAdap.Fill(ds);

                return ds;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MySQLConn.Close();
            }

        }

    }
}
