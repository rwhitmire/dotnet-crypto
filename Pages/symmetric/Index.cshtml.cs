using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Cryptography;

namespace CryptographyCore.Pages.symmetric
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string PlainText { get; set; }
        [BindProperty]
        public string CipherText { get; set; }
        [BindProperty]
        public string IV { get; set; }
        [BindProperty]
        public string OriginalPlainText { get; set; }

        public void OnGet()
        {            
        }

        private Aes CreateCipher()
        {
            Aes cipher = Aes.Create();  //Defaults - Keysize 256, Mode CBC, Padding PKC27
            //Aes cipher = new AesManaged();
            //Aes cipher = new AesCryptoServiceProvider();

            cipher.Padding = PaddingMode.ISO10126;

            //cipher.Padding = PaddingMode.Zeros;
            //cipher.Mode = CipherMode.ECB;

            //Create() makes new key each time, use a consistent key for encrypt/decrypt
            cipher.Key = conversions.HexToByteArray("892C8E496E1E33355E858B327BC238A939B7601E96159F9E9CAD0519BA5055CD"); ;

            return cipher;
        }

        public async Task<IActionResult> OnPostEncryptAsync()
        {
            Aes cipher = CreateCipher();

            //Show the IV on page (will use for decrypt, normally send in clear along with ciphertext)
            IV = Convert.ToBase64String(cipher.IV);

            //Create the encryptor, convert to bytes, and encrypt
            ICryptoTransform cryptTransform = cipher.CreateEncryptor();
            byte[] plaintext = Encoding.UTF8.GetBytes(PlainText);
            byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

            //Convert to base64 for display
            CipherText = Convert.ToBase64String(cipherText);
            return Page();
        }

        public async Task<IActionResult> OnPostDecryptAsync()
        {
            Aes cipher = CreateCipher();

            //Read back in the IV used to randomize the first block
            cipher.IV = Convert.FromBase64String(IV);

            //Create the decryptor, convert from base64 to bytes, decrypt
            ICryptoTransform cryptTransform = cipher.CreateDecryptor();
            byte[] cipherText = Convert.FromBase64String(CipherText);
            byte[] plainText = cryptTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);

            OriginalPlainText = Encoding.UTF8.GetString(plainText);

            return Page();
        }
    }
}