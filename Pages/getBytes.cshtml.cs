using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Cryptography;

namespace CryptographyCore.Pages
{
    public class getBytesModel : PageModel
    {
        public string hex { get; private set; }

        public void OnGet()
        {            
            byte[] key = new byte[32];      //256 bits (1 byte = 8 bits)

            //Use cryptographically strong random number generator, 
            RandomNumberGenerator rng = RNGCryptoServiceProvider.Create();

            //Get enough random bytes to fill the given buffer
            rng.GetBytes(key);

            //Convert to hex for key storage (can also use base64)
            hex = conversions.ByteArrayToHex(key);
        }
    }
}