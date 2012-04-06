using System;
using System.Security.Cryptography;
using System.Text;

namespace VideoHelp.UI.Domain.LoginzaAuthentication
{
    public static class HashExtentions
    {
         public static string GetMd5Hash(this string inputString)
         {
             using (var md5 = new MD5CryptoServiceProvider())
             {
                 var buf = Encoding.Default.GetBytes(inputString);
                 var hash = md5.ComputeHash(buf);
                 return BitConverter.ToString(hash).Replace("-", "").ToLower();
             }
         }
    }
}