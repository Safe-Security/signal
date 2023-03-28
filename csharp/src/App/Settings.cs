using System.ComponentModel.DataAnnotations;
using System.Security;

namespace App
{
    /// <summary>
    /// 
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Gets or sets the safe URL.
        /// </summary>
        /// <value>
        /// The safe URL.
        /// </value>
        [Required]
        public string SafeUrl { get; set; }

        /// <summary>
        /// Gets or sets the API username.
        /// </summary>
        /// <value>
        /// The API username.
        /// </value>
        [Required]
        public string ApiUsername { get; set; }

        /// <summary>
        /// Gets or sets the API password.
        /// </summary>
        /// <value>
        /// The API password.
        /// </value>
        [Required]
        public SecureString ApiPassword { get; set; }

        /// <summary>
        /// Gets or sets the examples directory path.
        /// </summary>
        /// <value>
        /// The examples directory path.
        /// </value>
        public string ExamplesDirectoryPath { get; set; }

    }
}
