using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using BusinessObject;
using MySql.Data.MySqlClient;

namespace FullCRUD
{
    public partial class InsertForm : Form
    {
        UserBL userBL = new UserBL();

        public InsertForm()
        {
            InitializeComponent();
        }

        private void InsertForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Put user inputs here
            string username = txtUsername.Text;
            string hashedPassword = Encrypt.EncryptPassword(this.txtPassword.Text); //Encrypt the password
            string phone = txtPhone.Text;
            string email = txtEmail.Text;

            // Check if txt boxes have inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            try
            {
                //Inserts new data into the new stored procedure command.
                 User user = new User(username, hashedPassword, phone, email);
                 int rowsAffected = userBL.InsertUser(user);

                 if (rowsAffected > 0) //Checks if any rows were changed or added.
                 {
                    MessageBox.Show("User added successfully.");

                    // Clear the input fields
                    ClearInputFields();
                 }
                 else
                 {
                     MessageBox.Show("Failed to add user.");
                 }
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e) //Button to go back to admin page
        {
            AdminPage admin = new AdminPage();
            admin.Show();
            this.Hide();
        }

        private void ClearInputFields()
        {
            // Clear the input fields
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
        }
    }
}
