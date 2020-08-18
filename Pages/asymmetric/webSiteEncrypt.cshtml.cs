using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.asymmetric
{
    public class webSiteEncryptModel : PageModel
    {
        private const string RJB_RSA_PUBLIC_KEY = "<RSAKeyValue><Modulus>6/7PGT9AO1ADPdnBZT3ZSImsuXfcuX9UvCjB1Zu0UliZha4bHZNcBj4VtuPJLF6ERpgJDfBqVTrT7yOMkVn4orfTlOExPedK8AWj9gTYBumGrFTDZwko1iQ5YZQ2kZGxg3QGpJhqeiEs8beFW672kXNyj5+UyeYp6R7su+fuiz8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        [BindProperty]
        public string PlainText { get; set; }
        [BindProperty]
        public string CipherText { get; set; }

        private RSA CreateCipher()
        {
            RSA cipher = RSA.Create();

            //Read from a previously exported RSA set of keys in XML (public and private), does not use the key container...        
            rsa.FromXmlString(cipher, RJB_RSA_PUBLIC_KEY);

            return cipher;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostEncryptAsync()
        {
            RSA cipher = CreateCipher();

            ////Encrypt the data
            byte[] data = Encoding.UTF8.GetBytes(PlainText);
            byte[] cipherText = cipher.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            CipherText = Convert.ToBase64String(cipherText);

            return Page();
        }
    }
}