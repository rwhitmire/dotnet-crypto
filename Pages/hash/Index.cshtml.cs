using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Cryptography;
using System.Text;

namespace CryptographyCore.Pages.hash
{
    public class IndexModel : PageModel
    {
        public string plaintext { get; private set; }
        public string hex { get; private set; }

        public void OnGet()
        {
            plaintext = "This is a simple demonstration of hashing";
            //plaintext = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

            SHA512 hashSvc = SHA512.Create();

            //SHA512 returns 512 bits (8 bits/byte, 64 bytes) for the hash
            byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            
            //This converts the 64 byte hash into the string hex representation of byte values 
            // (shown by default as 2 hex characters per byte) that looks like 
            // "FB-2F-85-C8-85-67-F3-C8-CE-9B-79-9C-7C-54-64-2D-0C-7B-41-F6...", each pair represents
            // the byte value of 0-255.  Removing the "-" separator creates a 128 character string 
            // representation in hex
            hex = BitConverter.ToString(hash).Replace("-", "");
        }
    }
}