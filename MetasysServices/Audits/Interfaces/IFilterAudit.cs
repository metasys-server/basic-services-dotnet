using System;
using System.Collections.Generic;
using System.Text;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Provides attribute to filter audit.
    /// </summary>
    public interface IFilterAudit
    {
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
        /// Earliest start time ISO8601 
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Latest end time ISO8601
        /// </summary>
        DateTime EndTime { get; set; }

        /// <summary>
        /// Determines whether discarded audits will be excluded from results.
        /// Default is false (discarded audits will not be excluded).
        /// </summary>
        bool? ExcludeDiscarded { get; set; }

        /// <summary>
        /// The page number of items to return Default: 1.
        /// </summary>
        int? Page { get; set; }

        /// <summary>
        /// The maximum number of items to return in the response. 
        /// Valid range is 1-10,000. Default: 100
        /// </summary>
        int? PageSize { get; set; }

        /// <summary>
        /// The criteria to use when sorting results
        /// Accepted Values: creationTime
        /// </summary>
        string Sort { get; set; }
    }
}