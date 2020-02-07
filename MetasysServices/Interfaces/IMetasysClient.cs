using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// An HTTP client for consuming the most commonly used endpoints of the Metasys API.
    /// </summary>
    public interface IMetasysClient
    {
        /// <summary>The current Culture Used for Metasys client localization.</summary>
        CultureInfo Culture { get; set; }

        /// <summary>
        /// Localizes the specified resource key for the current MetasysClient locale or specified culture.
        /// </summary>
        string Localize(string resource, CultureInfo cultureInfo = null);

        /// <summary>
        /// Attempts to login to the given host and retrieve an access token.
        /// </summary>
        AccessToken TryLogin(string username, string password, bool refresh = true);

        /// <summary>
        /// Attempts to login to the given host and retrieve an access token asynchronously.
        /// </summary>
        Task<AccessToken> TryLoginAsync(string username, string password, bool refresh = true);

        /// <summary>
        /// Requests a new access token from the server before the current token expires.
        /// </summary>
        AccessToken Refresh();

        /// <summary>
        /// Requests a new access token from the server before the current token expires asynchronously.
        /// </summary>
        Task<AccessToken> RefreshAsync();

        /// <summary>
        /// Returns the current session access token.
        /// </summary>
        AccessToken GetAccessToken();

        /// <summary>
        /// Given the Item Reference of an object, returns the object identifier.
        /// </summary>
        Guid GetObjectIdentifier(string itemReference);

        /// <summary>
        /// Given the Item Reference of an object, returns the object identifier asynchronously.
        /// </summary>
        Task<Guid> GetObjectIdentifierAsync(string itemReference);

        /// <summary>
        /// Read one attribute value given the Guid of the object.
        /// </summary>
        Variant ReadProperty(Guid id, string attributeName);

        /// <summary>
        /// Read one attribute value given the Guid of the object asynchronously.
        /// </summary>
        Task<Variant> ReadPropertyAsync(Guid id, string attributeName);

        /// <summary>
        /// Read many attribute values given the Guids of the objects.
        /// </summary>
        IEnumerable<VariantMultiple> ReadPropertyMultiple(IEnumerable<Guid> ids,
            IEnumerable<string> attributeNames);

        /// <summary>
        /// Read many attribute values given the Guids of the objects asynchronously.
        /// </summary>
        Task<IEnumerable<VariantMultiple>> ReadPropertyMultipleAsync(IEnumerable<Guid> ids,
            IEnumerable<string> attributeNames);

        /// <summary>
        /// Write a single attribute given the Guid of the object. 
        /// </summary>
        void WriteProperty(Guid id, string attributeName, object newValue, string priority = null);

        /// <summary>
        /// Write a single attribute given the Guid of the object asynchronously.
        /// </summary>
        Task WritePropertyAsync(Guid id, string attributeName, object newValue, string priority = null);

        /// <summary>
        /// Write to many attribute values given the Guids of the objects.
        /// </summary>
        void WritePropertyMultiple(IEnumerable<Guid> ids,

            IEnumerable<(string Attribute, object Value)> attributeValues, string priority = null);

        /// <summary>
        /// Write to many attribute values given the Guids of the objects asynchronously.
        /// </summary>
        Task WritePropertyMultipleAsync(IEnumerable<Guid> ids,

            IEnumerable<(string Attribute, object Value)> attributeValues, string priority = null);

        /// <summary>
        /// Get all available commands given the Guid of the object.
        /// </summary>
        IEnumerable<Command> GetCommands(Guid id);

        /// <summary>
        /// Get all available commands given the Guid of the object asynchronously.
        /// </summary>
        Task<IEnumerable<Command>> GetCommandsAsync(Guid id);

        /// <summary>
        /// Send a command to an object.
        /// </summary>
        void SendCommand(Guid id, string command, IEnumerable<object> values = null);
        
        /// <summary>
        /// Send a command to an object asynchronously.
        /// </summary>
        Task SendCommandAsync(Guid id, string command, IEnumerable<object> values = null);

        /// <summary>
        /// Gets all network devices.
        /// </summary>
        IEnumerable<MetasysObject> GetNetworkDevices(string type = null);

        /// <summary>
        /// Gets all network devices asynchronously.
        /// </summary>
        Task<IEnumerable<MetasysObject>> GetNetworkDevicesAsync(string type = null);

        /// <summary>
        /// Gets all available network device types.
        /// </summary>
        IEnumerable<MetasysObjectType> GetNetworkDeviceTypes();

        /// <summary>
        /// Gets all available network device types asynchronously.
        /// </summary>
        Task<IEnumerable<MetasysObjectType>> GetNetworkDeviceTypesAsync();

        /// <summary>
        /// Gets all child objects given a parent Guid.
        /// Level indicates how deep to retrieve objects.
        /// </summary>
        IEnumerable<MetasysObject> GetObjects(Guid id, int levels = 1);

        /// <summary>
        /// Gets all child objects given a parent Guid asynchronously.
        /// Level indicates how deep to retrieve objects.
        /// </summary>
        Task<IEnumerable<MetasysObject>> GetObjectsAsync(Guid id, int levels = 1);

        /// <summary>
        /// Gets all spaces.
        /// </summary>
        IEnumerable<MetasysObject> GetSpaces(string type = null);

        /// <summary>
        /// Gets all spaces asynchronously.
        /// </summary>
        Task<IEnumerable<MetasysObject>> GetSpacesAsync(string type = null);

        /// <summary>
        /// Gets all Equipment for the given space
        /// </summary>
        IEnumerable<MetasysObject> GetSpaceEquipment(Guid spaceId);

        /// <summary>
        /// Gets all spaces asynchronously.
        /// </summary>
        Task<IEnumerable<MetasysObject>> GetSpaceEquipmentAsync(Guid spaceId);    

        /// <summary>
        /// Gets all spaces types.
        /// </summary>
        IEnumerable<MetasysObjectType> GetSpaceTypes();

        /// <summary>
        /// Gets all space types asynchronously.
        /// </summary>
        Task<IEnumerable<MetasysObjectType>> GetSpaceTypesAsync();

        /// <summary>
        /// Gets all equipment.
        /// </summary>
        IEnumerable<MetasysObject> GetEquipment();

        /// <summary>
        /// Gets all equipment asynchronously.
        /// </summary>
        Task<IEnumerable<MetasysObject>> GetEquipmentAsync();

        /// <summary>
        /// Gets all points for the given Equipment
        /// </summary>
        IEnumerable<Point> GetEquipmentPoints(Guid equipmentId);

        /// <summary>
        /// Gets all points for the given equipment asynchronously.
        /// </summary>
        Task<IEnumerable<Point>> GetEquipmentPointsAsync(Guid equipmentId);   

        /// <summary>
        /// Services for Trends and Samples.
        /// </summary>
        ITrendsService Trends { get; set; }

        /// <summary>
        /// Services for Alarms.
        /// </summary>
        IProvideAlarmInfo Alarms { get; set; }

        /// <summary>
        /// Services for Audits.
        /// </summary>
        IProvideAuditInfo Audits { get; set; }
    }
}
