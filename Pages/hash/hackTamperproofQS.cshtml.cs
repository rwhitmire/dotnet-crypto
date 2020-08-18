using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.hash
{
    public class hackTamperproofQSModel : PageModel
    {
        public string hash {get; private set;}

        public void OnGet()
        {
            hash = hashing.ComputeHash("a=2&b=2&c=3", hashing.HashOutputType.hex);
        }
    }
}