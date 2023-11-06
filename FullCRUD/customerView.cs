using BusinessLogic;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using DataAccess;

namespace FullCRUD
{
    public partial class customerView : Form
    {

        customerBL customerBL = new customerBL();
        public customerView()
        {
            InitializeComponent();

            dataGridViewCustomer.RowTemplate.Height = 100;
            

            List<Customer> customers = GetAllCustomersFromDatabase();
            dataGridViewCustomer.DataSource = customers;

            // Subscribe to the CellFormatting event
            dataGridViewCustomer.CellFormatting += DataGridViewCustomer_CellFormatting;


        }

        private List<Customer> GetAllCustomersFromDatabase()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (MySqlDataReader reader = customerBL.GetAllCustomers())
                {
                    while (reader.Read())
                    {
                        int idcustomers = reader.GetInt32("idcustomers");
                        string name = reader.GetString("Name");
                        int age = reader.GetInt32("Age");
                        string gender = reader.GetString("Gender");
                        DateTime birthday = reader.GetDateTime("Birthday");
                        string address = reader.GetString("Address");
                        string email = reader.GetString("Email");
                        string phone = reader.GetString("Phone");

                        // Assuming the profile image is in the 9th column (index 8)
                        byte[] profileImage = (byte[])reader["profilePicture"];

                        Customer customer = new Customer(idcustomers, name, age, gender, birthday, address, email, phone, profileImage);
                        customers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (logging, showing an error message, etc.)
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return customers;
        }

        private void DataGridViewCustomer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Assuming the profile image column is the 9th column (index 8)
            int imageColumnIndex = 8;

            if (e.ColumnIndex == imageColumnIndex && e.Value != null && e.Value != DBNull.Value)
            {
                byte[] imageData = (byte[])e.Value;
                Image originalImage = ByteArrayToImage(imageData);

                // Set the cell value to the resized image
                e.Value = ResizeImage(originalImage, dataGridViewCustomer.Columns[imageColumnIndex].Width, dataGridViewCustomer.RowTemplate.Height);
            }
        }
        private Image ResizeImage(Image originalImage, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.DrawImage(originalImage, 0, 0, width, height);
            }
            return resizedImage;
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            report report = new report();
            report.Show();
            this.Hide();
        }

        private void btnExportDatabaseCustomer_Click(object sender, EventArgs e)
        {
            string filePath = "customer.csv";

            using (MySqlDataReader reader = customerBL.GetAllCustomers())
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the column headers
                    writer.WriteLine("Name,Age,Gender,Birthday,Address,Phone,Email");

                    // Write the data
                    while (reader.Read())
                    {
                        writer.Write(reader["name"].ToString() + ",");
                        writer.Write(reader["age"].ToString() + ",");
                        writer.Write(reader["gender"].ToString() + ",");
                        writer.Write(reader["birthday"].ToString() + ",");
                        writer.Write(reader["address"].ToString() + ",");
                        writer.Write(reader["phone"].ToString() + ",");
                        writer.Write(reader["email"].ToString());
                        writer.WriteLine();
                    }
                }
            }
            MessageBox.Show("Data exported to customer.csv");
        }
    }
}


