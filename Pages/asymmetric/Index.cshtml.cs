using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Cryptography;
using System.Text;

namespace CryptographyCore.Pages.asymmetric
{
    public class IndexModel : PageModel
    {
        //Got the XML representation from exporting key container using aspnet_regiis
        private const string RJB_RSA_KEYS = "<RSAKeyValue><Modulus>6/7PGT9AO1ADPdnBZT3ZSImsuXfcuX9UvCjB1Zu0UliZha4bHZNcBj4VtuPJLF6ERpgJDfBqVTrT7yOMkVn4orfTlOExPedK8AWj9gTYBumGrFTDZwko1iQ5YZQ2kZGxg3QGpJhqeiEs8beFW672kXNyj5+UyeYp6R7su+fuiz8=</Modulus><Exponent>AQAB</Exponent><P>/AY3OtnrdVp6t5zVVpTqFVVHd88xgRgaO+Y4KIjzJKua1I0B8PEfIohUUai8vRbRTJZGHxwmfryQ8bkpmKXgEw==</P><Q>77fchIo2EP8jwwil//ZS+oD78eN6luKlGRljs/8wXMqaK3/x0qQHfjcBvVu0jLk1MzdNZZPiNMaxZczbJeBFpQ==</Q><DP>hZ3eBkunNE7GJTb3PLIy8SCHhZPKEUFwFzXVrFf/YP/CVNJ1pwKPmUViPvERL8c7LDm376KDHkpnJmEfFplLFQ==</DP><DQ>bniy3Tm8dNS/rE++AFmKH/t1ICIPCp3kK87xja/an8iWh9lsngANm/LJkHREnl1z0Oh5eIhQRLYUZq+jhq72KQ==</DQ><InverseQ>cXngkJq+LxEl4GzRLyk1nVcHBsKUKmqSkEgWm1qD+PtEtHMW25RR8Am2AYWU0YGa35T/hbRVG9WQhl3ZOB3pCw==</InverseQ><D>oxVWNoNANvzHELHvdLA1/GuvogeTz9iPTOv5b00HYrR5eyji8iBIQsQaq2VkOzYhwMsFzs0qHjXmCWcOl8+OAkimP7OFE6xmd0xPKaZTyFWYFBkFWbqbeAXCTcDuWH79DO0WSr35oZeppxzd4Zz+8p0GkXVgSaZgBfbcI40I6Mk=</D></RSAKeyValue>";

        [BindProperty]
        public string PlainText { get; set; }
        [BindProperty]
        public string CipherText { get; set; }
        [BindProperty]
        public string OriginalPlainText { get; set; }

        private RSA CreateCipher()
        {
            RSA cipher = RSA.Create();

            //Read from a previously exported RSA set of keys in XML (public and private), does not use the key container...        
            rsa.FromXmlString(cipher, RJB_RSA_KEYS);

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

        public async Task<IActionResult> OnPostDecryptAsync()
        {
            RSA cipher = CreateCipher();

            //Decrypt the data
            byte[] original = cipher.Decrypt(Convert.FromBase64String(CipherText), RSAEncryptionPadding.Pkcs1);
            OriginalPlainText = Encoding.UTF8.GetString(original);

            return Page();
        }
    }
}