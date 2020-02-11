using System;
using System.Runtime.InteropServices;

namespace JohnsonControls.Metasys.ComServices
{
    /// <summary>
    /// Filters to get Audits
    /// </summary>
    [Guid("02E91329-DA34-446F-9D9C-396D805C6966")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class ComAuditFilter : ComTimeFilter, IComAuditFilter
    {
        /// <summary>
        /// Filter by comma-separated list of origin applications.
        /// See /enumSets/578/members for possible values.
        /// Example: 1,2.
        /// </summary>
        public string OriginApplications { get; set; }

        /// <summary>
        /// Filter by comma-separated list of class levels.
        /// See /enumSets/578/members for possible values.
        /// Example: 1,2.
        /// </summary>
        public string ClassLevels { get; set; }

        /// <summary>
        /// Filter by comma-separated list of the action types.
        /// See /enumSets/578/members for possible values.
        /// Example: 1,2.
        /// </summary>
        public string ActionTypes { get; set; }

        /// <summary>
        /// Determines whether discarded audits will be excluded from results.
        /// Default is false (discarded audits will not be excluded).
        /// </summary>
        public bool? ExcludeDiscarded { get; set; }
    }
}
