using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Customer
    {
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] ProfileImage { get; set; } // Byte array to store image data

        // Constructor to initialize the object

        public Customer(int id, string name, int age, string gender, DateTime birthday, string address, string email, string phone, byte[] profileImage)
        {
            IdCustomer = id;
            Name = name;
            Age = age;
            Gender = gender;
            Birthday = birthday;
            Address = address;
            Email = email;
            Phone = phone;
            ProfileImage = profileImage; // Assign the image data to the property
        }

        public Customer(string name, int age, string gender, DateTime birthday, string address, string email, string phone, byte[] profileImage)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Birthday = birthday;
            Address = address;
            Email = email;
            Phone = phone;
            ProfileImage = profileImage; // Assign the image data to the property
        }
    }
}
