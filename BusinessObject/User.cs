using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public User(int id,string userName, string password, string phone, string email)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Phone = phone;
            Email = email;
        }

        public User(int id, string userName, string phone, string email)
        {
            Id = id;
            UserName = userName;
            Phone = phone;
            Email = email;
        }

        // Constructor for creating a User object
        public User(string userName,string password, string phone, string email)
        {
            UserName = userName;
            Password = password;
            Phone = phone;
            Email = email;
        }

    }
}
