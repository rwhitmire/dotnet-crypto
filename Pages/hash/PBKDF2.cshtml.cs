using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Cryptography;

namespace CryptographyCore.Pages.hash
{
    public class PBKDF2Model : PageModel
    {
        public string hex { get; private set; }
        public string saltHex { get; private set; }

        public void OnGet()
        {
            string pwd = "$tairwayToHeaven";

            Rfc2898DeriveBytes oHash = new Rfc2898DeriveBytes(pwd, 32);     //32 byte salt
            oHash.IterationCount = 100000;       //Make as slow as can tolerate in login...
            byte[] hash = oHash.GetBytes(20);   
            byte[] salt = oHash.Salt;

            hex = Convert.ToBase64String(hash);
            saltHex = Convert.ToBase64String(salt);
        }
    }
}