using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.hash
{
    public class hashedPasswordsWithSaltModel : PageModel
    {
        public string data { get; private set; }
        public string salt { get; private set; }
        public string hash { get; private set; }
        public string hashWithSalt { get; private set; }
        public string hashWithSaltAndRandom { get; private set; } 

        public void OnGet()
        {
            data = "HelloWorld";

            //Generate some random "salt", would typically be done when first compute the hash and stored in separate
            // column in same row with hashed password
            salt = GenerateSalt(8);

            //Default hash (no salt or random data)
            hash = hashing.ComputeHash(data, hashing.HashOutputType.base64);

            //Hash with added known public salt (complicates dictionary attacks against all passwords but not individual password)
            hashWithSalt = hashing.ComputeHash(data, hashing.HashOutputType.base64, false, salt, "");

            //Hash with additional randomness added (stored securely, example just shows inline for ease, can be same for all)
            hashWithSaltAndRandom = hashing.ComputeHash(data, hashing.HashOutputType.base64, false, salt, "y0icvO+pZWU=");   
        }

        private string GenerateSalt(int byteCount)
        {
            //Use cryptographically strong random number generator
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[byteCount];
            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }
}