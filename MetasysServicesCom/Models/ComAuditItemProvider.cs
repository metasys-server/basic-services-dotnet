using System;
using System.Runtime.InteropServices;

namespace JohnsonControls.Metasys.ComServices
{
    /// <summary>
    /// A specialized DTO for COM that provides audit Item
    /// </summary>
    [ComVisible(true)]
    [Guid("C6B20D4F-F1FA-44EB-AF17-3D09FB43D066")]
    [ClassInterface(ClassInterfaceType.None)]
    public class ComAuditItemProvider : IComProvideAuditItem
    {
        /// <summary>
        /// The identifier of the audit(GUID).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The dateTime representing the creation time when this audit message was created.
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// The action performed that initiated the audit.
        /// https://{hostname}/api/v2/enumSets/577/members for possible values
        /// </summary>
        public string ActionTypeUrl { get; set; }

        /// <summary>
        /// Indicates if the audit has been discarded.
        /// </summary>
        public bool Discarded { get; set; }

        /// <summary>
        /// Enumeration representing status.
        /// https://{hostname}/api/v2/enumSets/516/members for possible values
        /// </summary>
        public string StatusUrl { get; set; }

        /// <summary>
        /// Data value prior to the Audit.
        /// </summary>
        public dynamic PreData { get; set; }

        /// <summary>
        /// Data value after the Audit.
        /// </summary>
        public dynamic PostData { get; set; }

        /// <summary>
        /// Parameters for the Audit.
        /// </summary>
        public dynamic Parameters { get; set; }

        /// <summary>
        /// The error that may have occurred during an audit.
        /// </summary>
        public string ErrorString { get; set; }

        /// <summary>
        /// The userName of the user that initiated the audit.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The user who created this audit
        /// </summary>
        public dynamic Signature { get; set; }

        /// <summary>
        /// A link to the object on which the activity was generated.
        /// </summary>
        public string ObjectUrl { get; set; }

        /// <summary>
        /// Link to annotations.
        /// </summary>
        public string AnnotationsUrl { get; set; }

        /// <summary>
        /// Metasys specific data.
        /// </summary>
        public dynamic Legacy { get; set; }

        /// <summary>
        /// URI that points back to this resource
        /// </summary>
        public string Self { get; set; }
    }
}
