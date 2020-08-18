using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.asymmetric
{
    public class CreateRsaKeysModel : PageModel
    {
        public string rsaKeysPublicPrivateXml { get; private set; }
        public string rsaKeysPublicXml { get; private set; }

        public void OnGet()
        {
            RSA cipher = RSA.Create();
            rsaKeysPublicPrivateXml = rsa.ToXmlString(cipher, true); 
            rsaKeysPublicXml = rsa.ToXmlString(cipher, true);    
        }
    }
}