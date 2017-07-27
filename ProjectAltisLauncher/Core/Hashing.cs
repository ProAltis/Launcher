using System;
using System.IO;
using System.Security.Cryptography;

namespace ProjectAltis.Core
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
                return new FileInfo(filePath).Length == size;
            }
            catch (Exception ex)
            {
                // The file doesn't exist
                Log.Error("{0} does not exist!", filePath);
                Log.Error(ex);
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
                SHA256 sha256 = SHA256.Create();

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    byte[] hashValue = sha256.ComputeHash(fileStream);
                    string strHashValue = "";
                    foreach (byte x in hashValue)
                    {
                        strHashValue += x.ToString("x2");
                    }
                    // Comparing the hash now
                    Log.Info("The SHA256 of {0} is: {1}", filePath, strHashValue);

                    return strHashValue == hash;
                }

            }
            catch (Exception ex)
            {
                // File doesn't exist
                Log.Error("{0} does not exist!", filePath);
                Log.Error(ex);
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
            try
            {
                SHA256 sha256 = SHA256.Create();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] hashValueBytes = sha256.ComputeHash(fileStream);
                    string hashValue = "";
                    foreach (byte x in hashValueBytes)
                    {
                        hashValue += x.ToString("x2");
                    }
                    return hashValue;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to calculate hash of file " + filePath);
                Log.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Calculates the size of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.Int32.</returns>
        public static int CalculateFileSize(string filePath)
        {
            FileInfo myFile = new FileInfo(filePath);
            return Convert.ToInt32(myFile.Length);
        }
    }
}


