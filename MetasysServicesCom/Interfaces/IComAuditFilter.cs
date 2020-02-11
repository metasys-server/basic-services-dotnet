using System;
using System.Runtime.InteropServices;

namespace JohnsonControls.Metasys.ComServices
{
    /// <summary>
    /// Provides attribute to filter Audits.
    /// </summary>
    [ComVisible(true)]
    [Guid("2D01CA3C-2053-4BEE-8B55-CACBDE1B9847")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComAuditFilter
    {
        /// <summary>
        /// Earliest start time ISO8601 string
        /// </summary>
        string StartTime { get; set; }

        /// <summary>
        /// Latest end time ISO8601 string
        /// </summary>
         string EndTime { get; set; }

        /// <summary>
        /// The page number of items to return Default: 1.
        /// </summary>
         string Page { get; set; }

        /// <summary>
        /// The maximum number of items to return in the response. 
        /// Valid range is 1-10,000. Default: 100
        /// </summary>
         string PageSize { get; set; }

        /// <summary>
        /// The criteria to use when sorting results
        /// Accepted Values: itemReference, priority, creationTime
        /// </summary>
         string Sort { get; set; }

        /// <summary>
        /// Filter by comma-separated list of origin applications.
        /// See /enumSets/578/members for possible values.
        /// Example: 1,2.
        /// </summary>
        string OriginApplications { get; set; }

        /// <summary>
        /// Filter by comma-separated list of class levels.
        /// See /enumSets/578/members for possible values.
        /// Example: 1,2.
        /// </summary>
        string ClassLevels { get; set; }

        /// <summary>
        /// Filter by comma-separated list of the action types.
        /// See /enumSets/578/members for possible values.
        /// Example: 1,2.
        /// </summary>
        string ActionTypes { get; set; }

        /// <summary>
        /// Determines whether discarded audits will be excluded from results.
        /// Default is false (discarded audits will not be excluded).
        /// </summary>
        bool ExcludeDiscarded { get; set; }
    }
}
