using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Cryptography;
using System.Text;



namespace CryptographyCore.Pages.hash
{
    public class hashClassesModel : PageModel
    {
        public string plaintext { get; private set; }
        public string hex { get; private set; }

        public void OnGet()
        {
            plaintext = "This is a simple demonstration of hashing";
            //plaintext = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

            SHA512 hashSvc = SHA512.Create();
            //SHA512Managed hashSvc = new SHA512Managed();
            //SHA512CryptoServiceProvider hashSvc = new SHA512CryptoServiceProvider();

            //SHA512 returns 512 bits (8 bits/byte, 64 bytes) for the hash
            byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

            hex = conversions.ByteArrayToHex(hash);
        }
    }
}