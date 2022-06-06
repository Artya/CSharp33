using System.Text;
using System.Security.Cryptography;

namespace Cryptography
{
    public static class CryptographyHelper
    {
        public const byte MD5HashLength = 16;
        public const byte HMACSHA256HashLength = 32;

        public static byte[] MD5Hash(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }

        public static bool MD5HashCheck(byte[] data, byte[] hash)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(data);

                for (var i = 0; i < MD5HashLength; i++)
                    if (bytes[i] != hash[i])
                        return false;

                return true;
            }
        }

        public static byte[] Sign(byte[] data, string password)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(password)))
            {
                return hmac.ComputeHash(data);
            }
        }

        public static bool SignCheck(byte[] data, string password, byte[] hash)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(password)))
            {
                var bytes = hmac.ComputeHash(data);

                for (var i = 0; i < HMACSHA256HashLength; i++)
                    if (bytes[i] != hash[i])
                        return false;

                return true;
            }
        }
    }
}
