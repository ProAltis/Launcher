using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Security;
using Windows.Security.Authentication;
using Windows.Security.Authentication.Identity;
using Windows.Security.Authentication.Identity.Core;
using Windows.Security.Credentials;
using Windows.Security.Credentials.UI;
using Windows.System;
using CredentialManagement;
using Microsoft.Win32;

namespace ProjectAltis.Core
{
    public static class UwpHelper
    {
        // UwpHelper
        // Easily interface with Uwp/Windows 10 Apis in this win forms app.
        // 
        // Code styling:
        // Have every public method make sure someone is on Windows 10.
        // 
        // if(!IsWindows10()) return <type>;
        //
        //
        // give summary (///) to public members
        //

        /// <summary>
        /// Sets credentials for securely saving password.
        /// Returns empty username/password if not windows 10.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        public static void SetCredentials(string username, string password)
        {
            SetCredentials("Altis", username, password, PersistanceType.Enterprise);
        }

        /// <summary>
        /// Gets A previously saved password.
        /// </summary>
        /// <returns>Password OR null</returns>
        public static string GetPassword()
        {
            return GetCredential("Altis").Password;
        }

        /// <summary>
        /// Checks Windows 10 status
        /// </summary>
        /// <returns>Yes if Win10</returns>
        public static bool IsWindows10()
        {
            try
            {
                var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                return reg != null && ((string)reg.GetValue("ProductName")).Contains("Windows 10");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        private static Credential GetCredential(string target)
        {
            var cm = new Credential { Target = target };
            if (!cm.Load())
            {
                return null;
            }

            //UserPass is just a class with two string properties for user and pass
            return new Credential(cm.Username, cm.Password);
        }

        private static bool SetCredentials(
            string target, string username, string password, PersistanceType persistenceType)
        {
            return new Credential
            {
                Target = target,
                Username = username,
                Password = password,
                PersistanceType = persistenceType
            }.Save();
        }

        private static bool RemoveCredentials(string target)
        {
            return new Credential { Target = target }.Delete();
        }
    }
    public static class CredentialUtil
    {
        
    }
}