using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.asymmetric
{
    public class RsaKeyContainersModel : PageModel
    {
        private const string KEY_CONTAINER = "CryptographyCoreRsaKeyContainer";

        public string rsaKeys { get; private set; }

        public void OnGet()
        {
            CreateOrGetKey(KEY_CONTAINER);

            //DeleteKey(KEY_CONTAINER);
        }

        //Functions from https://docs.microsoft.com/en-us/dotnet/standard/security/how-to-store-asymmetric-keys-in-a-key-container (with slight modifications)

        private void CreateOrGetKey(string keyContainerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = keyContainerName;

            RSACryptoServiceProvider cipher = new RSACryptoServiceProvider(cp);

            rsaKeys = rsa.ToXmlString(cipher, true);
        }

        private void DeleteKey(string keyContainerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = keyContainerName;

            RSACryptoServiceProvider cipher = new RSACryptoServiceProvider(cp);

            cipher.PersistKeyInCsp = false;
            cipher.Clear();
        }
    }
}