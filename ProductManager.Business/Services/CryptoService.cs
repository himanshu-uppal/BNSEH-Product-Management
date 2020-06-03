using ProductManager.Business.Services.Interface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProductManager.Business.Services
{
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// to genrate the salt for the password
        /// </summary>
        /// <returns></returns>
        public string GenerateSalt()
        {
            var data = new byte[0x10];  //hexadecimal number with base 16 , 0x10 - 16
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())  //Random Number Generator Service
            {
                //fills the array data with random number
                cryptoServiceProvider.GetBytes(data);

                //converts array of bytes - data into string
                return Convert.ToBase64String(data);
            }
        }


        public string EncryptPassword(string password, string salt)
        {
            //process of mapping a binary string of an arbitrary length to a small binary string of a fixed length


            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentNullException("salt");
            }

            using (var sha256 = SHA256.Create())
            {
                //combing salt and password into one string in particular format
                var saltedPassword = string.Format("{0}{1}", salt, password);

                //because it takes a byte array or stream as an input
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);

                //returns a hash in the form of a byte array of 256 bits and converted into string
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));

            }
        }
    }
}
