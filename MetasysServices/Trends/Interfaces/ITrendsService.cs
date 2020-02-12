using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITrendsService
    {
        /// <summary>
        /// Get the list of trended attributes for the given object.
        /// </summary>    
        /// <param name="id">The Guid of the object.</param>
        /// <returns>The list of trended attributes.</returns>
        /// <exception cref="MetasysHttpNotFoundException"></exception>
        /// <exception cref="MetasysHttpException"></exception>
        /// <exception cref="MetasysHttpTimeoutException"></exception>
        /// <exception cref="MetasysHttpParsingException"></exception>
        /// <exception cref="MetasysHttpNotFoundException"></exception>
        /// <exception cref="MetasysObjectException"></exception>
        List<Attribute>GetTrendedAttributes(Guid id);

        /// <inheritdoc cref="ITrendsService.GetTrendedAttributes(Guid)"/>
        Task<List<Attribute>>GetTrendedAttributesAsync(Guid id);

        /// <summary>
        /// Retrieves available samples for the given object attribute, filtered by startTime and endTime.
        /// </summary>
        /// <remarks>StartTime and EndTime are mandatory parameters.</remarks>
        /// <param name="objectId"></param>
        /// <param name="attributeId"></param>
        /// <param name="filter"></param>
        /// <returns>The list of samples.</returns>
        /// <exception cref="MetasysHttpNotFoundException"></exception>
        /// <exception cref="MetasysHttpException"></exception>
        /// <exception cref="MetasysHttpTimeoutException"></exception>
        /// <exception cref="MetasysHttpParsingException"></exception>
        /// <exception cref="MetasysHttpNotFoundException"></exception>
        /// <exception cref="MetasysObjectException"></exception>
        PagedResult<Sample> GetSamples(Guid objectId, int attributeId, TimeFilter filter);

        /// <inheritdoc cref="ITrendsService.GetSamples(Guid, int, TimeFilter)"/>
        Task<PagedResult<Sample>> GetSamplesAsync(Guid objectId, int attributeId, TimeFilter filter);
    }
}
