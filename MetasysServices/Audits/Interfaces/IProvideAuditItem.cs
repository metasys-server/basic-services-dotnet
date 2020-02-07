using System;
using System.Collections.Generic;
using System.Text;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Provides attribute for audits
    /// </summary>
    public interface IProvideAuditItem
    {
        /// <summary>
        /// The identifier of the audit(GUID).
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The dateTime representing the creation time when this audit message was created.
        /// </summary>
        string CreationTime { get; set; }

        /// <summary>
        /// The action performed that initiated the audit.
        /// https://{hostname}/api/v2/enumSets/577/members for possible values
        /// </summary>
        string ActionTypeUrl { get; set; }

        /// <summary>
        /// Indicates if the audit has been discarded.
        /// </summary>
        bool Discarded { get; set; }

        /// <summary>
        /// Enumeration representing status.
        /// https://{hostname}/api/v2/enumSets/516/members for possible values
        /// </summary>
        string StatusUrl { get; set; }

        /// <summary>
        /// Data value prior to the Audit.
        /// </summary>
        dynamic PreData { get; set; }

        /// <summary>
        /// Data value after the Audit.
        /// </summary>
        dynamic PostData { get; set; }

        /// <summary>
        /// Parameters for the Audit.
        /// </summary>
        dynamic Parameters { get; set; }

        /// <summary>
        /// The error that may have occurred during an audit.
        /// </summary>
        string ErrorString { get; set; }

        /// <summary>
        /// The userName of the user that initiated the audit.
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// The user who created this audit
        /// </summary>
        dynamic Signature { get; set; }

        /// <summary>
        /// A link to the object on which the activity was generated.
        /// </summary>
        string ObjectUrl { get; set; }

        /// <summary>
        /// Link to annotations.
        /// </summary>
        string AnnotationsUrl { get; set; }

        /// <summary>
        /// Metasys specific data.
        /// </summary>
        dynamic Legacy { get; set; }
    }
}