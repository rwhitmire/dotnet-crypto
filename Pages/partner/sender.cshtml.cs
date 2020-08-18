using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptographyCore.Pages.partner
{
    public class senderModel : PageModel
    {
        private const string RECEIVER_PUBLIC_KEY = "<RSAKeyValue><Modulus>vU3Yfu1Z4nFknj9daoDmh+I0CzR+aLnTjUSejQyNJ0IgMb59x4mVe17C6U+bl4Cry7gXAk3LEmmE/BRxjlF8HKlXixoBWak1dpmr89Ye7iaD2UWwl5Dmn07Q9s27NGdywy0BsD1vDcFSgno3LUbVznkw/0hypbnOPxWKlBCao2c=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const string SENDER_KEYS = "<RSAKeyValue><Modulus>rW0Prd+S+Z6Wv0gEakgSp/v8Pu4xJ6OjaVCHKTIcf/C5nZvE77454lii3Ne6odV+76oaM2Pn3I9kKehK7CtqklI7rc1+05WRE3u8O5tC5v2ECjEDPMULAcZVTjXSyZtSAOiqk+6nEcJGRED65aGXwFgZuxEY8y4FbUma3I311aM=</Modulus><Exponent>AQAB</Exponent><P>5TYzDyoQBT4C8eqyuWlfNbg0XfnJAUHzonOiz/5az86E9y8V3oxDH3B3GMECDzvcLRJnp5x/G1Lectu1p3ckDw==</P><Q>wbHOTIh7l/p9FszFj/uMdvLlITyABeOZVJEPJhw6fkMSqiRqnx4F2dtqRcGUDBhpWbG6kbTXi9ijMVL8u+iRLQ==</Q><DP>h0KOqvo1bgKEFmJbiZKm/rpvHK3UcguLTGhUwczlpg/G419D1oqK6biib1cmcfrvGSHtTTnKwEMMxlblQafK/Q==</DP><DQ>u80hQFVouF+Xn16mA0eb1s0FWmdlndAin7sSHBpsoHV6CFvMwUCD3cp/TOk3GU8l/mBzi8jy4NYIzM8w2yTQdQ==</DQ><InverseQ>1rYDocFlo3EEs28Miieqa/fE8uzESz6YWONuZPoKHWO/1m9Tf0K01+TtPqDBFRhFBaTNKBJ2lyCGGRIEA41CYg==</InverseQ><D>dZvsciGYbqfZ20ZfmCPgYwNEAPlPZG5Yt2bhAlL1eN4rQnMMjvkWECXD7Lhv3KgIOUfGFOu/pZeoebMKfDbFQe6uA9f4jSYiC3yI0lyGiZQ+SpyJPRKetSSSqiOcK/vnnn2+03RgOVnyU3T52hRXVsb3oXtT5xacWm4IeGABB2E=</D></RSAKeyValue>";

        public string cipherText { get; private set; }
        public string signatureText { get; private set; }

        public void OnGet()
        {
            //Setup cipher with sender's private (and public) key
            RSA senderCipher = RSA.Create();
            rsa.FromXmlString(senderCipher, SENDER_KEYS);
 
            //Setup cipher with receiver's public key
            RSA receiverCipher = RSA.Create();
            rsa.FromXmlString(receiverCipher, RECEIVER_PUBLIC_KEY);

            //Take the secret and get bytes
            string plaintext = "4444111122223333";
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            //Encrypt the secret with the receiver's public key (so only they can decrypt), this provides
            // the confidentiality if needed (some only want integrity and non-repudiation)
            byte[] ciphertextBytes = receiverCipher.Encrypt(plaintextBytes, RSAEncryptionPadding.Pkcs1);

            //Compute a hash for the message
            SHA512 hashAlg = SHA512.Create();
            byte[] hash = hashAlg.ComputeHash(ciphertextBytes);

            //Sign the hash with sender's private key
            RSAPKCS1SignatureFormatter sigFormatter = new RSAPKCS1SignatureFormatter(senderCipher);
            sigFormatter.SetHashAlgorithm("SHA512");
            byte[] signature = sigFormatter.CreateSignature(hash);

            //Convert the encrypted message and the signature to base64 
            cipherText = System.Net.WebUtility.UrlEncode(Convert.ToBase64String(ciphertextBytes));
            signatureText = System.Net.WebUtility.UrlEncode(Convert.ToBase64String(signature));
        }
    }
}