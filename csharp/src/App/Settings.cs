using System.ComponentModel.DataAnnotations;
using System.Security;

namespace App
{
    internal class Settings
    {
        [Required]
        public string SafeUrl { get; set; }

        [Required]
        public string ApiUsername { get; set; }

        [Required]
        public SecureString ApiPassword { get; set; }

        public string ExamplesDirectoryPath { get; set; }

    }
}
