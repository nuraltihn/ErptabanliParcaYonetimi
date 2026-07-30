using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace Erpyonetimi.Helpers
{
    public static class PasswordHelper
    {
      public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();

            byte[] bytes =Encoding.UTF8.GetBytes(password);
            byte[]hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);

            
        }
        public static bool SifreDogrulama(string password, string hash)
        {
            return (HashPassword(password) == hash);
        }
    }
}
