using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAltisLauncher.Core
{
    class Hashing
    {
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
        public static string CalculateSHA256(string filePath)
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

                return strHashValue;
            }
        }
        public static int CalculateFileSize(string filePath)
        {
            System.IO.FileInfo myFile = new System.IO.FileInfo(filePath);
            long sizeInBytes = myFile.Length;
            return Convert.ToInt32(sizeInBytes);
        }
    }
}
    

