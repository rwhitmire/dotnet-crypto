using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.hash
{
    public class limitedValuesModel : PageModel
    {
        public string data { get; private set; }
        public string hash { get; private set; }

        public void OnGet()
        {
            data = "a=yes";
            string data2 = "";
            string data3 = "";

            //Add something to help randomize
            //data2 = Request.Headers["User-Agent"].ToString();
            //data3 = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            hash = hashing.ComputeHash(data, hashing.HashOutputType.hex, true, data2, data3);
        }
    }
}