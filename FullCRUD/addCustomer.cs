using BusinessLogic;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullCRUD
{
    public partial class addCustomer : Form
    {
        private string selectedGender = "Other"; // Set a default value

        customerBL customerbl= new customerBL();

        public addCustomer()
        {
            InitializeComponent();
        }

        private void addCustomer_Load(object sender, EventArgs e)
        {
            radioButton1.CheckedChanged += radioButton_CheckedChanged;
            radioButton2.CheckedChanged += radioButton_CheckedChanged;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                selectedGender = "Male";
            }
            else if (radioButton2.Checked)
            {
                selectedGender = "Female";
            }
        }



        private void ClearInputFields()
        {
            // Clear the input fields
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            pictureBoxProfile.Image = null;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;

                    // Create a Bitmap from the selected image file
                    Bitmap originalImage = new Bitmap(imagePath);

                    if (originalImage.PropertyIdList.Contains(0x0112))
                    {
                        var orientation = (int)originalImage.GetPropertyItem(0x0112).Value[0];
                        if (orientation == 2 || orientation == 4 || orientation == 5 || orientation == 7)
                        {
                            originalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        else if (orientation == 3 || orientation == 6 || orientation == 8)
                        {
                            originalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        }
                    }

                    // Create a new Bitmap with a scaled size to fit the PictureBox
                    int maxWidth = pictureBoxProfile.Width;
                    int maxHeight = pictureBoxProfile.Height;
                    double widthRatio = (double)maxWidth / originalImage.Width;
                    double heightRatio = (double)maxHeight / originalImage.Height;
                    double ratio = Math.Min(widthRatio, heightRatio);
                    int newWidth = (int)(originalImage.Width * ratio);
                    int newHeight = (int)(originalImage.Height * ratio);
                    Bitmap scaledImage = new Bitmap(originalImage, newWidth, newHeight);

                    // Set the PictureBox Image and adjust the SizeMode property
                    pictureBoxProfile.Image = scaledImage;
                    pictureBoxProfile.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text;
            int age = Convert.ToInt32(this.ageUpDown.Value);
            DateTime birthday = birthdayPicker.Value.Date;
            string address = this.txtAddress.Text;
            string email = this.txtEmail.Text;
            string phone = this.txtPhone.Text;
            string gender = selectedGender;

            

            try
            {

                byte[] imageBytes = ImageToByteArray(pictureBoxProfile.Image);
                Customer customer = new Customer(name, age, gender, birthday, address, email, phone,imageBytes);
                int rowsAffected = customerbl.InsertCustomer(customer);

                if (rowsAffected > 0) //Checks if any rows were changed or added.
                {
                    MessageBox.Show("Customer added successfully.");

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


        //Converts the image to a byte array!
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Determine image format (you can change this to the desired format)
                ImageFormat format = ImageFormat.Jpeg;

                // Save the image to the MemoryStream with the specified format
                image.Save(ms, format);

                return ms.ToArray();
            }
        }

        private void btnViewCustomers_Click(object sender, EventArgs e)
        {
            customerView customerView = new customerView();
            customerView.Show();
            this.Hide();
        }
    }
}
