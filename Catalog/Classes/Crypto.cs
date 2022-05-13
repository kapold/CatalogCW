using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Catalog.Classes
{
    public static class Crypto
    {
        public static string GetHash(string source)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
            byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            sha256Hash.Dispose();
            return hash;
        }
    }
}
