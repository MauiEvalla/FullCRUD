using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataAccess;
using BusinessObject;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace BusinessLogic //This technically serves as the man that calls for the DA or calling of action
{
    /*
     * This man calls for the UserDA and it's methods/calls He is the middle between Main form and DataAccess
     * 
     * Form <- UserBL -> <- UserDA -><- DBHelper
     * 
     * 
     */
    public class UserBL
    {
        public MySqlDataReader GetUserByUsernameAndPassword(string username,string password) //Login page storage procedure.
        {
            UserDA userDA = new UserDA();
            try
            {
                return userDA.GetUserByUsernameAndPassword(username,password);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertUser(User user) { 
            UserDA userDA = new UserDA();
            try
            {
                Regex phoneRegex = new Regex("^[0-9]+$");
                Regex emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"); //Regex checks for patterns

                if (phoneRegex.IsMatch(user.Phone)) //Checks if the entered phone number is valid
                {
                    if (emailRegex.IsMatch(user.Email))//Checks if the new email is valid.
                    {
                        userDA.InsertUser(user);
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("Please add a valid email!");
                        return 0;
                    }
                }
                else
                {
                    MessageBox.Show("Please add a valid phone number!");
                    return 0;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader SearchUsersByUsername(string searchName) //Search User storage procedure
        {
            UserDA userDA = new UserDA(); //Creating new userDA object for access
            try
            {
               return userDA.SearchUsersByUsername(searchName);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader GetAllUser() //GetAllUser Storage Procedure
        {
            UserDA userDA = new DataAccess.UserDA(); //Creating new userDA object for access
            try
            {
              return userDA.GetAllUser();
            }

            catch (Exception ex)
            {
                throw ex;
           
            }
        }

        public int DeleteUserById(int id) //GridView Display!
        {
            UserDA userDA = new DataAccess.UserDA(); //Creating new userDA object for access
            try
            {
                userDA.DeleteUserById(id);
                return 1;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateUserById(User user) //Search User storage procedure
        {
            UserDA userDA = new UserDA(); //Creating new userDA object for access
            try
            {
                userDA.UpdateUserById(user);
                return 1;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExportToExcel(DataGridView gridView, string fileName)
        {
            using (var wb = new XLWorkbook(fileName)) // Load existing Excel file
            {
                var ws = wb.Worksheet(1); // Get the first worksheet in the existing file

                // Clear existing data (if any)
                ws.Clear(XLClearOptions.Contents);

                // Export DataGridView Header Row (only first 4 columns) and apply formatting
                for (int i = 0; i < Math.Min(gridView.Columns.Count, 4); i++)
                {
                    ws.Cell(1, i + 1).Value = gridView.Columns[i].HeaderText;
                    ws.Cell(1, i + 1).Style.Font.Bold = true;
                }

                // Export DataGridView Data Rows (only first 4 columns)
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    for (int j = 0; j < Math.Min(gridView.Columns.Count, 4); j++)
                    {
                        object cellValue = gridView.Rows[i].Cells[j].Value;
                        ws.Cell(i + 2, j + 1).Value = cellValue != null ? cellValue.ToString() : string.Empty;
                    }
                }

                // Auto-adjust column widths, Basic Formatting
                ws.Columns().AdjustToContents();

                // Save the Excel file (existing file gets updated)
                wb.Save();
            }
        }


    }
}
