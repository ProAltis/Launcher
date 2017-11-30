using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.Update
{
    public static class Cryptography
    {
        /// <summary>
        ///     Calculates the Sha256 of a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String.</returns>
        public static string CalculateSha256(string filePath)
        {
            var sha256 = SHA256.Create();
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var hashValueBytes = sha256.ComputeHash(fileStream);
                var sb = new StringBuilder();
                foreach (var x in hashValueBytes)
                {
                    sb.Append(x.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
