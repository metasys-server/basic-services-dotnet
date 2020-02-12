using System;
using System.Collections.Generic;
using System.Text;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Metasys specific data.
    /// </summary>
    public class LegacyModel
    {
        /// <summary>
        /// The fully qualified item reference of the object that created this audit.
        /// </summary>
        public string FullyQualifiedItemReference { get; set; }

        /// <summary>
        /// The short name of the object that created this audit.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Indicates the class level in which this audit belongs.
        /// See /enumSets/568/members for possible values
        /// </summary>
        public string ClassLevel { get; set; }

        /// <summary>
        /// Indicates the application that performed action that generated this audit.
        /// See /enumSets/578/members for possible values
        /// </summary>
        public string OriginApplication { get; set; }

        /// <summary>
        /// Provides the description of the action that generated this audit.
        /// See /enumSets/580/members for possible values
        /// </summary>
        public string Description { get; set; }
    }
}
