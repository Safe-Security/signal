using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security;

namespace Signals.Library.Utility
{
    /// <summary>
    /// Utility contains common helper methods
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Gets all files from directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Directory {directoryPath} doesn't exist please check path
        /// or
        /// Directory {directoryPath} is empty, please place some sample signals in it
        /// </exception>
        public static List<string> GetAllFilesFromDirectory(string directoryPath)
        {

            if (Directory.Exists(directoryPath))
            {
                List<string> files = new List<string>();
                string[] supportedFileExtensions = { "*.json", "*.zip" };
                foreach (var fileExt in supportedFileExtensions)
                {
                    files.AddRange(Directory.GetFiles(directoryPath, fileExt, SearchOption.TopDirectoryOnly).ToList());
                }
                if (files.Count == 0)
                {
                    throw new Exception($"Directory {directoryPath} is empty, please place some sample signals in it");
                }
                return files;
            }

            throw new Exception($"Directory {directoryPath} doesn't exist please check path");

        }

        /// <summary>
        /// Converts to securestring.
        /// </summary>
        /// <param name="plainString">The plain string.</param>
        /// <returns></returns>
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
        /// <param name="secureString">The secure string.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
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

        /// <summary>
        /// Validates the specified object using data annotation.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>bool true/false</returns>
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
