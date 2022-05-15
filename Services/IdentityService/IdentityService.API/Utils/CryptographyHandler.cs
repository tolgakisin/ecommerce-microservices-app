using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IdentityService.API.Utils
{
    public static class CryptographyHandler
    {
        public static (byte[] Hash, byte[] Salt) HashText(string plainText, byte[] salt = null)
        {
            if (salt == null)
                salt = CreateSalt(8);

            byte[] textBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(plainText);
            byte[] unifiedTextBytes = textBytes.Concat(salt).ToArray();

            SHA512Managed sHA512Managed = new();
            byte[] computedHash = sHA512Managed.ComputeHash(unifiedTextBytes);

            return (computedHash, salt);
        }

        public static bool VerifyHashed(string password, byte[] hash, byte[] salt)
        {
            var newHashedBuffer = HashText(password, salt).Hash;

            return Utils.Common.CompareByteArrays(newHashedBuffer, hash);
        }

        private static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return buff;
        }
    }
}
