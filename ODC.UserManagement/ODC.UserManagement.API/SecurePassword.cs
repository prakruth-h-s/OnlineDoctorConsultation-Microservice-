using System;
using System.Collections.Generic;
using System.Text;

namespace ODC.UserManagement
{
    public static class SecurePassword
    {
        private static string Key = "hsdfasnjk@@bdsc";

        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string Decrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            return result.Substring(0, result.Length - Key.Length);
        }
    }
}
