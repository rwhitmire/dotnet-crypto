using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.hash
{
    public class useTamperproofQSModel : PageModel
    {
        public string aQS { get; private set; }

        public void OnGet()
        {
            ValidateQuerystring();

            aQS = Request.Query["a"];
        }

        private void ValidateQuerystring()
        {
            string queryString = Request.QueryString.ToString().Replace("?", "");

            //Get just our hash value from the querystring collection, if none present throw exception
            string submittedHash = Request.Query["qtpval"];
            if (null == submittedHash)
            {
                throw new ApplicationException("Querystring validation hash was not sent!");
            }

            //Take the original querystring and get all of it except our hash (we need to recompute the hash
            // just like it was done on the original querystring)
            int hashPos = queryString.IndexOf("&qtpval=");
            queryString = queryString.Substring(0, hashPos);

            //If the hash that was sent on the querystring does not match our compute of that hash given the 
            // current data in the querystring, then throw an exception
            if (hashing.ComputeHash(queryString, hashing.HashOutputType.hex, true) != submittedHash)
            {
                throw new ApplicationException("Querystring hash values don't match");
            }
        }
    }
}