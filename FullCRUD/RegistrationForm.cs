using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FullCRUD;
using System.Text.RegularExpressions;
using BusinessLogic;
using BusinessObject;
using DocumentFormat.OpenXml.Office.Word;

namespace FullCRUD
{
    public partial class RegistrationForm : Form
    {
        UserBL userBL = new UserBL();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Go back to login form when clicking login button.
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string hashedPassword = Encrypt.EncryptPassword(this.txtPassword.Text);

                        try
                        {
                        User user = new User(this.txtUsername.Text, hashedPassword, this.txtPhone.Text, this.txtEmail.Text);
                        int rowsAffected = userBL.InsertUser(user);

                            if(rowsAffected>0)
                           {
                            MessageBox.Show("Successfully Registered User!");

                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide();
                            }
                            else
                            {
                            MessageBox.Show("ERROR encountered registering new user!");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
        }
        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
