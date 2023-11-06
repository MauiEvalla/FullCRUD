using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FullCRUD
{
    public class DBHelper
    {
        public static DataSet GetData(string MySQLQuery, CommandType storedProcedure, MySqlParameter mySqlParameter)
        {
            string xCONN = System.Configuration.ConfigurationManager.ConnectionStrings["xCONN"].ConnectionString;

            System.Data.DataSet dsCONN = new System.Data.DataSet();
            MySqlConnection MySQLConn = new MySqlConnection(xCONN);
            MySqlDataAdapter MySQLDataAdapt = new MySqlDataAdapter();

            try
            {
                MySQLConn.Open();
                MySQLDataAdapt.SelectCommand = new MySqlCommand(MySQLQuery, MySQLConn);
                MySQLDataAdapt.Fill(dsCONN);

                return dsCONN;
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

        public static MySqlDataReader ExecuteReader(string CommandName, CommandType CmdType)
        {
            string xCONN = ConfigurationManager.ConnectionStrings["xCONN"].ConnectionString;

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
            string xCONN = ConfigurationManager.ConnectionStrings["xCONN"].ConnectionString;

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
            string xCONN = ConfigurationManager.ConnectionStrings["xCONN"].ConnectionString;

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
            string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["xCONN"].ConnectionString;

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
