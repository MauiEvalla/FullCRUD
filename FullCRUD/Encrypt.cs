using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FullCRUD
{
    public class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create()) //A crypthographic hash function from cryptography package
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); //Takes the password, turns into a series of bytes, then turned into a unique set of bytes.

                StringBuilder builder = new StringBuilder(); //Its an empty box where characters can be added
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));//Each byte is converted to a string of coe
                }

                return builder.ToString(); //Returns the final encrypted password as string
            }
        }
    }


}
