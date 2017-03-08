using System;
using System.IO;

namespace ProjectAltisLauncher.Core
{
    public static class Hashing
    {
        /// <summary>
        /// Compares the size of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="size">The size.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CompareFileSize(string filePath, int size)
        {
            try
            {
                System.IO.FileInfo myFile = new System.IO.FileInfo(filePath);
                long sizeInBytes = myFile.Length;
                return sizeInBytes == size; // returns true or false

            }
            catch (Exception)
            {
                // The file doesn't exist
                Console.WriteLine("{0} does not exist!", filePath);
                return false;
            }
        }
        /// <summary>
        /// Compares the sha256.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="hash">The hash.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CompareSHA256(string filePath, string hash)
        {
            try
            {
                System.Security.Cryptography.SHA256 mySHA256 = System.Security.Cryptography.SHA256.Create();

                using (System.IO.FileStream fileStream = System.IO.File.OpenRead(filePath))
                {
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    string strHashValue = "";
                    foreach (byte x in hashValue)
                    {
                        strHashValue += x.ToString("x2");
                    }
                    // Comparing the hash now
                    Console.WriteLine("The SHA256 of {0} is: {1}", filePath, strHashValue);

                    return strHashValue == hash;
                }

            }
            catch (Exception)
            {
                // File doesn't exist
                Console.WriteLine("{0} does not exist!", filePath);
                return false;
            }
        }
        /// <summary>
        /// Calculates the sha256.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String.</returns>
        public static string CalculateSHA256(string filePath)
        {
            System.Security.Cryptography.SHA256 mySHA256 = System.Security.Cryptography.SHA256.Create();
            using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] hashValue = mySHA256.ComputeHash(fileStream);
                string strHashValue = "";
                foreach (byte x in hashValue)
                {
                    strHashValue += x.ToString("x2");
                }
                // Comparing the hash now
                Console.WriteLine("The SHA256 of {0} is: {1}", filePath, strHashValue);

                return strHashValue;
            }
        }
        /// <summary>
        /// Calculates the size of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.Int32.</returns>
        public static int CalculateFileSize(string filePath)
        {
            System.IO.FileInfo myFile = new System.IO.FileInfo(filePath);
            long sizeInBytes = myFile.Length;
            return Convert.ToInt32(sizeInBytes);
        }
    }
}
    

