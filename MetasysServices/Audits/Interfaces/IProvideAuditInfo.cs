using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Flurl.Http;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Defines method to provide audit infos for endpoints of the Metasys Audit API.
    /// </summary>
    public interface IProvideAuditInfo
    {
        /// <summary>
        /// Retrieves the specified audit.
        /// </summary>
        /// <param name="auditId">The identifier of the audit.</param>
        /// <returns>The specified audit details.</returns>
        AuditItemProvider GetSingleAudit(Guid auditId);


        /// <summary>
        /// Retrieves the specified audit.
        /// </summary>
        /// <param name="auditId">The identifier of the audit.</param>
        /// <returns>The specified audit details.</returns>
        /// <inheritdoc cref="IProvideAuditInfo.GetSingleAudit(Guid)"/>
		Task<AuditItemProvider> GetSingleAuditAsync(Guid auditId);

        /// <summary>
        /// Retrieves a collection of audits.
        /// </summary>
        /// <param name="auditFilter">The audit model to filter audits.</param>
        /// <returns>The list of audits with details.</returns>
        PagedResult<AuditItemProvider> GetAudits(AuditFilter auditFilter);

        /// <summary>
        /// Retrieves a collection of audits.
        /// </summary>
        /// <param name="auditFilter">The audit model to filter audits.</param>
        /// <returns>The list of audits with details.</returns>
        /// <inheritdoc cref="AuditInfoProvider.GetAudits(AuditFilter)"/>
		Task<PagedResult<AuditItemProvider>> GetAuditsAsync(AuditFilter auditFilter);

        /// <summary>
        /// Retrieves a collection of audits for the specified object.
        /// </summary>
        /// <param name="objectId">The identifier of the object.</param>
        /// <param name="auditFilter">The filter to be applied to audit list.</param>
        /// <returns>The list of audit with details.</returns>
        PagedResult<AuditItemProvider> GetAuditsForAnObject(Guid objectId, AuditFilter auditFilter);

        /// <summary>
        /// Retrieves a collection of audits for the specified object.
        /// </summary>
        /// <param name="objectId">The identifier of the object.</param>
        /// <param name="auditFilter">The filter to be applied to audit list.</param>
        /// <returns>The list of audits with details.</returns>
        /// <inheritdoc cref="AuditInfoProvider.GetAuditsForAnObject(Guid, AuditFilter)"/> 
        Task<PagedResult<AuditItemProvider>> GetAuditsForAnObjectAsync(Guid objectId, AuditFilter auditFilter);
    }
}