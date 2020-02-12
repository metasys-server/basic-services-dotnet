using System;
using System.Collections.Generic;
using System.Text;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// The user who created this audit.
    /// </summary>
    public class SignatureModel
    {
        /// <summary>
        /// The name of the person "signing".
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The reason for signing.
        /// </summary>
        public string Reason { get; set; }
    }
}
