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
    public class ComAuditFilter :  IComAuditFilter
    {
        /// <summary>
        /// Earliest start time ISO8601 string
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// Latest end time ISO8601 string
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// The page number of items to return Default: 1.
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// The maximum number of items to return in the response. 
        /// Valid range is 1-10,000. Default: 100
        /// </summary>
        public string PageSize { get; set; }

        /// <summary>
        /// The criteria to use when sorting results
        /// Accepted Values: itemReference, priority, creationTime
        /// </summary>
        public string Sort { get; set; }
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
        public bool ExcludeDiscarded { get; set; }
    }
}
