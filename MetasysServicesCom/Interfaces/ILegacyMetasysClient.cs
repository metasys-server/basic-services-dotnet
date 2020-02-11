using System;
using System.Runtime.InteropServices;
using JohnsonControls.Metasys.BasicServices;

namespace JohnsonControls.Metasys.ComServices
{
    /// <summary>
    /// A COM HTTP client for consuming the most commonly used endpoints of the Metasys API.
    /// </summary>
    [ComVisible(true)]
    [Guid("B1AF1A67-42A0-4E4A-8A07-97AA53B42D02")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ILegacyMetasysClient
    {
        /// <summary>
        /// Attempts to login to the given host.
        /// </summary>
        IComAccessToken TryLogin(string username, string password, bool refresh = true);

        /// <summary>
        /// Requests a new access token from the server before the current token expires.
        /// </summary>
        /// <returns>Access Token.</returns>
        /// <exception cref="MetasysHttpException"></exception>
        /// <exception cref="MetasysTokenException"></exception>
        IComAccessToken Refresh();

        /// <summary>
        /// Read one attribute value given the Guid of the object.
        /// </summary>
        IComVariant ReadProperty(string id, string attributeName);

        /// <summary>
        /// Read many attribute values given the Guids of the objects.
        /// </summary>
        object ReadPropertyMultiple([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]string[] ids, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] string[] attributeNames);

        /// <summary>
        /// Write a single attribute given the Guid of the object. 
        /// </summary>
        void WriteProperty(string id, string attributeName, string newValue, string priority = null);

        /// <summary>
        /// Write to many attribute values given the Guids of the objects.
        /// </summary>
        void WritePropertyMultiple([In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] string[] ids, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] string[] attributes, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] string[] attributeValues, string priority = null);

        /// <summary>
        /// Get all available commands given the Guid of the object.
        /// </summary>
        object GetCommands(string id);

        /// <summary>
        /// Send a command to an object.
        /// </summary>
        void SendCommand(string id, string command, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] string[] values = null);

        /// <summary>
        /// Gets all network devices.
        /// </summary>
        object GetNetworkDevices(string type = null);

        /// <summary>
        /// Gets all available network device types.
        /// </summary>
        object GetNetworkDeviceTypes(string type = null);

        /// <summary>
        /// Gets all child objects given a parent Guid.
        /// Level indicates how deep to retrieve objects.
        /// </summary>
        object GetObjects(string id, int levels = 1);

        /// <summary>
        /// Given the Item Reference of an object, returns the object identifier.
        /// </summary>
        string GetObjectIdentifier(string itemReference);

        /// <summary>
        /// Gets all network devices.
        /// </summary>
        object GetSpaces(string type = null);

        /// <summary>
        /// Gets all space types.
        /// </summary>
        object GetSpaceTypes();

        /// <summary>
        ///  Gets all Equipment for the given space
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        object GetSpaceEquipment(string spaceId);

        /// <summary>
        /// Gets all network devices.
        /// </summary>
        object GetEquipment();

        /// <summary>
        /// Gets all points for the given Equipment
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        object GetEquipmentPoints(string equipmentId);


        /// <summary>
        /// Retrieves the specified alarm.
        /// </summary>
        /// <param name="alarmId">The identifier of the alarm.</param>
        /// <returns>The specified alarm details.</returns>
        object GetSingleAlarm(string alarmId);

        /// <summary>
        /// Retrieves a collection of alarms.
        /// </summary>
        /// <param name="alarmFilter">The alarm model to filter alarms.</param>
        /// <returns>The list of alarms with details.</returns>
        IComPagedResult GetAlarms(IComFilterAlarm alarmFilter);

        /// <summary>
        /// Retrieves a collection of alarms for the specified object.
        /// </summary>
        /// <param name="objectId">The identifier of the object.</param>
        /// <param name="alarmFilter">The alarm model to filter alarms.</param>
        /// <returns>The list of alarms for the specified object.</returns>
        IComPagedResult GetAlarmsForAnObject(string objectId, IComFilterAlarm alarmFilter);

        /// <summary>
        /// Retrieves a collection of alarms for the specified object.
        /// </summary>
        /// <param name="networkDeviceId">The identifier of the network device.</param>
        /// <param name="alarmFilter">The alarm model to filter alarms.</param>
        /// <returns>The list of alarms for the specified object.</returns>
        IComPagedResult GetAlarmsForNetworkDevice(string networkDeviceId, IComFilterAlarm alarmFilter);

        /// <summary>
        /// Retrieves a collection of attributes under the specified object for which samples are available.
        /// </summary>
        /// <param name="id">The identifier of the object</param>
        /// <returns>The list of attributes for the specified object for which samples are available.</returns>
        object GetTrendedAttributes(string id);

        /// <summary>
        /// Retrieves a collection of samples for the specified object attribute during a particular date and time range.
        /// </summary>
        /// <param name="objectId">The identifier of the object</param>
        /// <param name="attributeId">The identifier of the attribute for which to retrieve sample information</param>
        /// <param name="filter">Filter for a timeline based request</param>
        /// <returns>The list of samples for the specified objectduring a particular date and time range.</returns>
        IComPagedResult GetSamples(string objectId, int attributeId, IComTimeFilter filter);

        /// <summary>
        /// Retrieves the specified audit.
        /// </summary>
        /// <param name="auditId">The identifier of the audit.</param>
        /// <returns>The specified audit details.</returns>
        object GetSingleAudit(string auditId);

        /// <summary>
        /// Retrieves a collection of audits.
        /// </summary>
        /// <param name="auditFilter">The audit model to filter audits.</param>
        /// <returns>The list of audits with details.</returns>
        IComPagedResult GetAudits(IComAuditFilter auditFilter);

        /// <summary>
        /// Retrieves a collection of audits for the specified object.
        /// </summary>
        /// <param name="objectId">The identifier of the object.</param>
        /// <param name="auditFilter">The filter to be applied to audit list.</param>
        /// <returns>The list of audit with details.</returns>
        IComPagedResult GetAuditsForAnObject(string objectId, IComAuditFilter auditFilter);
    }
}