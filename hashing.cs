using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for hashing
/// </summary>
public static class hashing
{
    public enum HashOutputType { hex, base64}

    public static string ComputeHash(string data, HashOutputType hashOutputType = HashOutputType.base64, bool useKey = false, string data2 = "", string data3 = "")
    {
        HashAlgorithm hashSvc = null;
        if (!useKey)
        {
            hashSvc = SHA512.Create();
        }
        else
        {
            hashSvc = new HMACSHA512(conversions.HexToByteArray("16E87A1B0A52133D21257D54CA31AD430188017E7B75BD27470090E9344F96F5A7678BAD0FE6F56AF0CC39BF3BE31C83C922B1370D2FE091694349352CBD5B27"));
            //hashSvc = new HMACSHA512();
        }

        byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(data + data2 + data3));

        switch(hashOutputType)
        {
            case HashOutputType.hex:
                return conversions.ByteArrayToHex(hash);
            case HashOutputType.base64:
                return Convert.ToBase64String(hash);
        }

        return "";
     }
}
