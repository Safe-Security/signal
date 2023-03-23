using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security;

namespace Signals.Library.Utility
{
    public static class Util
    {
        public static List<string> GetAllFilesFromDirectory(string directoryPath)
        {
            List<string> files = new List<string>();
            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetFiles(directoryPath, @"*.json", SearchOption.TopDirectoryOnly).ToList();
            }
            else
            {
                throw new Exception($"Directory {directoryPath} doesn't exist please check path");
            }
            if (files.Count == 0)
            {
                throw new Exception($"Directory {directoryPath} is empty, please place some sample signals in it");
            }
            return files;
        }

        public static SecureString ToSecureString(string plainString)
        {
            if (string.IsNullOrEmpty(plainString))
            {
                return null;
            }

            SecureString secureString = null;
            secureString = new SecureString();
            foreach (char c in plainString)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        /// <summary>
        /// Converts a secure string to a plain string
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string ToString(SecureString secureString)
        {
            if (secureString == null)
            {
                return String.Empty;
            }

            string result = string.Empty;
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                result = Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
            return result;
        }

        public static bool Validate(object obj)
        {
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), results, validateAllProperties: true);
            var errorMessages = results.Select(x => x.ErrorMessage);

            if (!isValid)
            {
                Console.WriteLine(string.Join("\n", errorMessages));
            }
            return isValid;
        }
    }
}
