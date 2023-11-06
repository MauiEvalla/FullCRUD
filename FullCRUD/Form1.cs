using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using DataAccess;
using BusinessLogic;
using BusinessObject;

namespace FullCRUD
{
    public partial class Form1 : Form
    {
        UserBL userBL = new UserBL();

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string inputHashedPassword = Encrypt.EncryptPassword(this.txtPassword.Text);

            using (MySqlDataReader drUser = userBL.GetUserByUsernameAndPassword(this.txtUsername.Text,inputHashedPassword))
            {
                try
                {
                    if (drUser.HasRows)
                    {
                        while (drUser.Read())
                        {
                            //Storing found info into variables
                            int id = Convert.ToInt32(drUser["id"]);
                            string userName = drUser["user_name"].ToString();
                            string password = drUser["password"].ToString();
                            string phone = drUser["phone"].ToString();
                            string email = drUser["email"].ToString();

                            //Storing inputs into user object
                            User loggedInUser = new User(id,userName, password, phone, email);

                            int role = loggedInUser.Id; //if user is an admin (With ID=1)
                            if (role == 1) //Login to Admin Dashboard
                            {
                                MessageBox.Show("Successfully logged in Admin Dashboard!");
                                AdminPage adminPage = new AdminPage();
                                adminPage.Show();
                                this.Hide();
                            }
                            else //Else login to main page.
                            {
                                MessageBox.Show("Successfully logged in!");
                                MainPage mainPage = new MainPage();
                                mainPage.Show();
                                this.Hide();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid User!");
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    drUser.Close();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}


