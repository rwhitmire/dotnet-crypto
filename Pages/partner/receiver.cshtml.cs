using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text;

namespace CryptographyCore.Pages.partner
{
    public class receiverModel : PageModel
    {
        private const string RECEIVER_KEYS = "<RSAKeyValue><Modulus>vU3Yfu1Z4nFknj9daoDmh+I0CzR+aLnTjUSejQyNJ0IgMb59x4mVe17C6U+bl4Cry7gXAk3LEmmE/BRxjlF8HKlXixoBWak1dpmr89Ye7iaD2UWwl5Dmn07Q9s27NGdywy0BsD1vDcFSgno3LUbVznkw/0hypbnOPxWKlBCao2c=</Modulus><Exponent>AQAB</Exponent><P>6veL+pbUjOr0PAiFcvBRwNlTz/+8T1iLHqkCggRPDSsTg25ybSqDa98mP5NQj9LHSYCECjOGZkiN4NoxgPPDxw==</P><Q>zj/l0Z36A/iD2IrVQzrEsvp31cmU6f9VCyPIGiM0FSEXbj23JuPNUPCzSo5oAAiSZfs/hR9uuAx1xQFAfTzjYQ==</Q><DP>dsW7VGh5+OGro80K6BbivIEfBL1ZCyLO8Ciuw9o5u4ZSztU9skETPawHQYvN5WW+p0D3fdCd14ZFcavZ6j1OcQ==</DP><DQ>YSQBRzgjsEkVOCEzjsWYLUAAvwWBiLCEyolgzsaz2hvK4FZa9AspAa1MlJn768Ady8CJS1bhm/fqZA5R5GqQIQ==</DQ><InverseQ>zEGFnyMtfxSYHwRv8nZ4xVcFctnU2pYmmXXYv8NV5FvhZi8Z1f1GE3tmS8qDyIuDTrXjmII2cffLMjPOVmLKoQ==</InverseQ><D>Ii97qDg+oijuDbHNsd0DRIix81AQf+MG9BzvMPOSTgOgAruuxSjwaK4NLsrkgzCGVayx4wWfZXzOuiMK+rN2YPr6IPeut3O14uuwLH7brxkit+MnhclsCtKpdT2iuUGOnbEhWccepCO7YLyyczhT9GE0rEtbEK6S7wvVKab/osE=</D></RSAKeyValue>";
        private const string SENDER_PUBLIC_KEY = "<RSAKeyValue><Modulus>rW0Prd+S+Z6Wv0gEakgSp/v8Pu4xJ6OjaVCHKTIcf/C5nZvE77454lii3Ne6odV+76oaM2Pn3I9kKehK7CtqklI7rc1+05WRE3u8O5tC5v2ECjEDPMULAcZVTjXSyZtSAOiqk+6nEcJGRED65aGXwFgZuxEY8y4FbUma3I311aM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public string secretSent { get; private set; }

        public void OnGet()
        {
            //Url decoding is done automatically!!  Pull the encrypted message off the querystring
            string secret = Request.Query["secret"];
            if (null == secret)
            {
                throw new ApplicationException("Secret querystring was not provided");
            }

            //Retrieve the signature that was sent
            string signature = Request.Query["signature"];
            if (null == signature)
            {
                throw new ApplicationException("Signature was not found");
            }

            //Undo the base64 encoding on both
            byte[] ciphertext = Convert.FromBase64String(secret);
            byte[] signatureBytes = Convert.FromBase64String(signature);

            //Recompute the hash for the cipherText
            SHA512 hashAlg = SHA512.Create();
            byte[] computedHash = hashAlg.ComputeHash(ciphertext);

            //Check the signature using senders public key
            RSA senderCipher = RSA.Create();
            rsa.FromXmlString(senderCipher, SENDER_PUBLIC_KEY);

            RSAPKCS1SignatureDeformatter sigDeformatter = new RSAPKCS1SignatureDeformatter(senderCipher);
            sigDeformatter.SetHashAlgorithm("SHA512");
            if (!sigDeformatter.VerifySignature(computedHash, signatureBytes))
            {
                throw new ApplicationException("Signature did not match from sender");
            }

            //Signature and hash shows that from the sender and not tampered with, so decrypt
            byte[] plaintextBytes = Decrypt(ciphertext, RECEIVER_KEYS);
            secretSent = Encoding.UTF8.GetString(plaintextBytes);
        }

        private byte[] Decrypt(byte[] ciphertext, string key)
        {
            RSA cipher = RSA.Create();
            rsa.FromXmlString(cipher, key);

            //Decrypt the data
            return cipher.Decrypt(ciphertext, RSAEncryptionPadding.Pkcs1);
        }
    }
}