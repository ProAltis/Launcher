using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAltisLauncher.Core
{
    public static class Crypto
    {
        /// <summary>
        /// Gets the SHA256 of a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String.</returns>
        public static string CalculateSHA256(string filePath)
        {
            SHA256 def = SHA256.Create();
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] hashValue = def.ComputeHash(fileStream);
                string strHashValue = "";
                foreach (byte x in hashValue)
                {
                    strHashValue += x.ToString("x2");
                }
                return strHashValue;
            }
        }
    }
}
