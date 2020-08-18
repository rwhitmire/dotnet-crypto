using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;

namespace CryptographyCore.Pages.hash
{
    public class hashedPasswordsModel : PageModel
    {
        public string password { get; private set; }

        public void OnGet()
        {
            //Pre-compute hashes for some common passwords (or an entire dictionary)
            Dictionary<string, string> passwordHashes = new Dictionary<string, string>();
            passwordHashes.Add(hashing.ComputeHash("nothing"), "nothing");
            passwordHashes.Add(hashing.ComputeHash("password"), "password");
            passwordHashes.Add(hashing.ComputeHash("secret"), "secret");

            password = CrackPassword("vSsar3708Jvp9Szi2NWZZ02Bqp1qRCFpbcTZPdBhnWgs5WtNZKnvCXdhztmeD2cmW192CF5bDufKRpayrW/isg==", passwordHashes);
        }

        private string CrackPassword(string hashedPassword, Dictionary<string, string> passwordHashes)
        {
            string password;

            if (true == passwordHashes.TryGetValue(hashedPassword, out password))
            {
                return password;
            }
            else
            {
                return "";
            }
        }
    }
}