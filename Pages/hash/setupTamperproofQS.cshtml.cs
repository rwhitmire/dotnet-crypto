using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.hash
{
    public class setupTamperproofQSModel : PageModel
    {
        public string hash { get; private set; }
        public string qs { get; private set; }

        public void OnGet()
        {
            qs = "a=1&b=2&c=3";
            hash = hashing.ComputeHash(qs, hashing.HashOutputType.hex, true);
        }
    }
}