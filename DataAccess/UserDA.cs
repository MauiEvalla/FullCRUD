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
    public class UserDA //This man will connect and execute the commands and parameters of Storage Procedure.
    {
        /*This class technically is under DataAccess, think of it as the middle man between 
         * Business logic to DBHelper, dito mo na ilalagay yung mga details like parameters, 
         * storage procedure name and command type, tas ipapasa mo lang sa mga methods/readers
         * sa DBHelper. It is the one that calls for the Database connection/commands basically.
         * 
         * 
         */

        public MySqlDataReader InsertUser(User user)
        {
            try
            {
                MySqlParameter[] myparams = new MySqlParameter[] //We set the parameters if meron man, basically 1 variable has 2 parameters.
                {
                    new MySqlParameter("p_user_name", user.UserName),
                    new MySqlParameter("p_password", user.Password),
                    new MySqlParameter("p_phone", user.Phone),
                    new MySqlParameter("p_email", user.Email)
                };
                                                           //Storage procedure           , Command Type               , parameters
                return DBHelper.ExecuteParameterizedReader("InsertUser", CommandType.StoredProcedure, myparams);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader GetUserByUsernameAndPassword(string username,string password)
        {
            try
            {
                MySqlParameter[] myparams = new MySqlParameter[] //We set the parameters if meron man, basically 1 variable has 2 parameters.
                {
                    new MySqlParameter("p_username", username),
                    new MySqlParameter("p_password", password)
                };
                                                           //Storage procedure           , Command Type               , parameters
                return DBHelper.ExecuteParameterizedReader("GetUserByUsernameAndPassword", CommandType.StoredProcedure,myparams);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader SearchUsersByUsername(string searchName) //Search User storage Procedure.
        {
            try
            {
                MySqlParameter[] myparams = new MySqlParameter[]
                {
                    new MySqlParameter("p_searchQuery", searchName),
                };

                return DBHelper.ExecuteParameterizedReader("SearchUsersByUsername", CommandType.StoredProcedure, myparams);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader GetAllUser() //GridView Display!
        {
            try
            {
                //Since no parameters, just call for execution.
                return DBHelper.ExecuteReader("GetAllUsers", CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader DeleteUserById(int id) //GridView Display!
        {
            try
            {
                MySqlParameter[] myparams = new MySqlParameter[] //We set the parameters if meron man, basically 1 variable has 2 parameters.
              {
                    new MySqlParameter("p_id", id),
              };
                //Storage procedure           , Command Type               , parameters
                return DBHelper.ExecuteParameterizedReader("DeleteUserById", CommandType.StoredProcedure, myparams);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader UpdateUserById(User user) //Update User.
        {
            try
            {
                MySqlParameter[] myparams = new MySqlParameter[]
                {
                    new MySqlParameter("p_id", user.Id),
                    new MySqlParameter("p_username", user.UserName),
                    new MySqlParameter("p_phone", user.Phone),
                    new MySqlParameter("p_email", user.Email),
                };

                return DBHelper.ExecuteParameterizedReader("UpdateUserById", CommandType.StoredProcedure, myparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
