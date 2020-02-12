using System;
using System.Collections.Generic;
using System.Text;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Temporal filter for a timeline based request.
    /// </summary>
    public class TimeFilter:BasicFilter
    {
        /// <summary>
        /// Earliest start time.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Latest end time.
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
