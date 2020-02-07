using System;
using System.Collections.Generic;
using System.Text;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Filters to get audits
    /// </summary>
    public class AuditFilter : TimeFilter, IFilterAudit
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

        /// <summary>
        /// The page number of items to return Default: 1.
        /// </summary>
       public int? Page { get; set; }

        /// <summary>
        /// The maximum number of items to return in the response. 
        /// Valid range is 1-10,000. Default: 100
        /// </summary>
       public int? PageSize { get; set; }

        /// <summary>
        /// The criteria to use when sorting results
        /// Accepted Values: creationTime
        /// </summary>
       public string Sort { get; set; }
    }
}