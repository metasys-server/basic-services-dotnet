# Metasys Basic Services <!-- omit in toc --> <a href="https://www.nuget.org/packages/JohnsonControls.Metasys.BasicServices/" rel="Johnson Controls Metasys BasicServices">![Nuget](https://img.shields.io/nuget/v/JohnsonControls.Metasys.BasicServices)</a>

This project provides a library for accessing the most common services of the Metasys Server API.
The intent is to provide an API that is very similar to the original MSSDA
(Metasys System Secure Data Access) library.

For versioning information see the [changelog](CHANGELOG.md).

## Contents <!-- omit in toc -->

- [Installation](#installation)
  - [Prerequisites](#prerequisites)
  - [.NET](#net)
  - [COM (Component Object Model)](#com-component-object-model)
  - [Excel and Visual Basic for Applications (VBA)](#excel-and-visual-basic-for-applications-vba)
- [Usage (.NET)](#usage-net)
  - [Creating a Client](#creating-a-client)
  - [Login and Access Tokens](#login-and-access-tokens)
  - [Get an Object Id](#get-an-object-id)
  - [Get a Property](#get-a-property)
  - [Write a Property](#write-a-property)
  - [Get and Send Commands](#get-and-send-commands)
  - [Get Network Devices and other Objects](#get-network-devices-and-other-objects)
  - [Localization of Metasys Enumerations](#localization-of-metasys-enumerations)
  - [Spaces and equipment](#spaces-and-equipment)
  - [Alarms](#alarms)
  - [Trends](#trends)
  - [Audits](#audits)
- [Usage (COM)](#usage-com)
  - [Creating a Client](#creating-a-client-1)
  - [Login and Access Tokens](#login-and-access-tokens-1)
  - [Get an Object Id](#get-an-object-id-1)
  - [Get a Property](#get-a-property-1)
  - [Write a Property](#write-a-property-1)
  - [Send Commands](#send-commands)
  - [Get Network Devices and other Objects](#get-network-devices-and-other-objects-1)
  - [Spaces and equipment](#spaces-and-equipment-1)
  - [Alarms](#alarms-1)
  - [Trends](#trends-1)
  - [Audits](#audits-1)
- [License](#license)
- [Contributing](#contributing)
- [Additional Information](#additional-information)
  - [Supported Localization Languages](#supported-localization-languages)

## Installation

### Prerequisites

- A method of installing NuGet packages as described by the Microsoft
[Package Consumpion Workflow](https://docs.microsoft.com/en-us/nuget/consume-packages/overview-and-workflow).

### .NET

To use in a .NET application include the following in the target source files:

```csharp
using JohnsonControls.Metasys.BasicServices;
```

Option 1: Consume the package by adding a PackageReference.

1. Add the following to the project's .csproj file:

    ```csharp
    <ItemGroup>
        <PackageReference Include="JohnsonControls.Metasys.BasicServices" Version="<target version>" />
    </ItemGroup>
    ```

Option 2: Consume the package by installing on the command line.
This workflow shows how to use the nuget.exe CLI (command line interface).

1. Open a command line and navigate to your project's main directory.
2. Execute the following command:

    ```cli
    nuget install JohnsonControls.Metasys.BasicServices -OutputDirectory packages
    ```

3. Update all packages with the command:

    ```cli
    nuget restore
    ```

### COM (Component Object Model)

Use the one click setup provided with the release tag to install the required dependencies in your system and register the COM DLL.
Keep in mind that for developing purposes the solution is already configured to register with the COM interop when the project is built. Alternatively, you can manually register or unregister the DLL use the RegCOM.bat and UnregCOM.bat scripts located in the Scripts directory.

To manually register for COM interop you can also use the Regasm.exe (Assembly Registration Tool) or similar tool.
[See more information here](https://docs.microsoft.com/en-us/dotnet/framework/tools/regasm-exe-assembly-registration-tool).

### Excel and Visual Basic for Applications (VBA)

LegacyMetasysClient allows you to interact with your Metasys server from a VBA application using COM interop.
To get start, enable the Developer Tab in Excel from the menu File-->Options-->Customize Ribbon.
[See more information here](https://support.office.com/en-us/article/show-the-developer-tab-e1192344-5e56-4d45-931b-e5fd9bea2d45).
Finally, from the Developer Tab click the Visual Basic button to open the editor and add the reference to the Metasys Services Object Library from the references list.
[See more information here](https://docs.microsoft.com/en-us/office/vba/language/how-to/check-or-add-an-object-library-reference).

## Usage (.NET)

This section demonstrates how to use the MetasysClient to interact with your Metasys server from a .NET application.
There is an example on [Github](https://github.com/metasys-server/basic-services-dotnet/tree/master/MetasysServicesExampleApp) that can be run from the command line and a specific prototype of an API to API integration about a  [Weather Forecast application](https://github.com/metasys-server/basic-services-dotnet/tree/master/WeatherForecastApp).

### Creating a Client

To create a new client and connect to a Metasys server with the default settings use:

```csharp
var client = new MetasysClient("hostname");
```

There are three optional parameters when creating a new client:

- ignoreCertificateErrors: If your server does not have a valid certificate the MetasysClient will not behave as expected and will likely block the connection. Setting the ignoreCertificateErrors = true will ignore this error and make an insecure connection with the server. To avoid this problem ensure the Metasys server has a valid certificate.
- apiVersion: If your server is not a current 10.1 Metasys server or later this SDK will not function correctly. The version parameter takes in an ApiVersion enumeration value that defaults to the most current release of Metasys. For Metasys 10.1 the api version is V2.
- cultureInfo: To set the language for localization specify the target culture with a CultureInfo object. The default culture is en-US.

To create a client that ignores certificate errors for a 10.1 Metasys server with Italian translations of values:

```csharp
CultureInfo culture = new CultureInfo("it-IT");
var client = new MetasysClient(hostname, true, ApiVersion.V2, culture);
```

### Login and Access Tokens

After creating the client, to login use the TryLogin method which takes a username, password, and an optional parameter to automatically refresh the access token during the client's lifetime. The default token refresh policy is true.

```csharp
// Automatically refresh token
client.TryLogin("username", "password");

// Do not automatically refresh token
client.TryLogin("username", "password", false);
```

At any time you want to manually refresh the access token before it expires use the following:

```csharp
client.Refresh();
```

To use the authorization token in an different HttpClient use the AccessToken object returned by these methods or use the GetAccessToken method. A successful token will be in the form "Bearer ...".

```csharp
AccessToken token = client.GetAccessToken();
Console.WriteLine($"Token: {token.Token}\nExpires: {dateToDisplay.ToString("g", token.Expires)}");

// Token: Bearer eyJ0eXAi...
// Expires: 10/1/2019 5:04 PM
```

### Get an Object Id

In order to use most of the methods in MetasysClient the id of the target object must be known. This id is in the form of a Guid and can be requested using the following given you know the item reference of the object:

```csharp
Guid id = client.GetObjectIdentifier("siteName:naeName/Folder1.AV1");
```

### Get a Property

In order to get a property you must know the Guid of the target object. An object called "Variant" is returned when getting a property from an object. Variant contains the property received in many different forms. There is a bit of intuition when handling a Variant since it will not explicitly provide the type of object received by the server. If the server cannot find the target object or attribute on the object this method will throw a MetasysHttpNotFoundException.

```csharp
Variant result = client.ReadProperty(id, "presentValue");
string stringValue = result.StringValue;
double numericValue = result.NumericValue;
bool booleanValue = result.BooleanValue;
Variant[] array = result.ArrayValue;
```

There is a method to get multiple properties from multiple objects. This can be very useful if the objects all are of the same type or have the same target properties.

```csharp
List<Guid> ids = new List<Guid> { id1, id2 };
List<string> attributes = new List<string> { "name", "description" };
IEnumerable<VariantMultiple> results = client.ReadPropertyMultiple(ids, attributes);
IEnumerable<Variant> id1Variants = results.ElementAt(0).Variants;
```

### Write a Property

In order to write a property you must have the Guid of the object and know the attribute name and type. This method contains an optional priority parameter to specify the write priority of the value. This priority is in the form of an enumeration such as "writePriorityEnumSet.priorityNone". To see more options use the "api/v2/enumSets/1/members" or "api/v2/schemas/enums/writePriorityEnumSet" http requests defined in the [Metasys API](https://metasys-server.github.io/api-landing/api/v2/).

```csharp
Guid id = client.GetObjectIdentifier("siteName:naeName/Folder1.AV1");
client.WriteProperty(id, "description", "This is an AV.");
client.WriteProperty(id, "description", "This is an AV.", "writePriorityEnumSet.priorityNone");
```

To change the same attribute values of many objects use the WritePropertyMultiple method. This method also accepts an optional write priority.

```csharp
List<Guid> ids = new List<Guid> { id1, id2 };
List<string> attributes = new List<string> { ("description", "This is an AV.") };
client.WritePropertyMultiple(ids, attributes);
```

### Get and Send Commands

To get all available commands on an object use the GetCommands method. This method will return a list of Command objects. The ToString() method is a useful tool to display the available commands and any information associated with it. When sending a command the Command.CommandId attribute is used as the parameter:

```csharp
var commands = client.GetCommands(id).ToList();
var command = commands[0].CommandId; // EnableAlarms, DisableAlarms, ReleaseAll, etc.
client.SendCommand(command);
```

When sending a command there may or may not be a single value or list of values that needs to be sent with the command. The Command.Items property will list all of these values as Items which contains the Title and Type of the value to change. If the type of an Item is "oneOf" this indicates the values is an enumeration and the possible values will be contained in the EnumerationValues list. Keep in mind the values to be sent in the command is the TitleEnumerationKey not the Title. The Title is the user friendly translated value that describes the enumeration. For example, an "AV Mapper" object has the following commands:

- "Adjust" that accepts a number value to adjust the present value.
- "TemporaryOperatorOverride" that accepts the value to adjust the present value, the hours, and the minutes for the temporary adjustment.
- "Release" which accepts two enumeration values for the attribute and new priority level.

The values on the Commands will model the following:

```csharp
var commands = client.GetCommands(id).ToList();
var commandAdjust = commands[0].CommandId; // Adjust
var commandOverride = commands[1].CommandId; // TemporaryOperatorOverride
var commandRelease = commands[2].CommandId; // Release

var adjustItems = commands[0].Items.ToList();
var overrideItems = commands[1].Items.ToList();
var releaseItems = commands[2].Items.ToList();

adjustItems[0].Title; // Value
adjustItems[0].Type; // number

overrideItems[0].Title; // Value
overrideItems[0].Type; // number
overrideItems[1].Title; // Hours
overrideItems[1].Type; // number
overrideItems[2].Title; // Minutes
overrideItems[2].Type; // number

releaseItems[0].Title; // oneOf
releaseItems[0].Type; // enum
var enumValues = releaseItems[0].EnumerationValues.ToList();
enumValues[0].TitleEnumerationKey; // attributeEnumSet.presentValue
releaseItems[1].Title; // oneOf
releaseItems[1].Type; // enum
var enumValues2 = releaseItems[1].EnumerationValues.ToList();
enumValues2[0].TitleEnumerationKey; // writePriorityEnumSet.priorityNone
enumValues2[1].TitleEnumerationKey; // writePriorityEnumSet.priorityManualEmergency
// ...etc
```

To send the command for each of these it would model the following:

```csharp
var list1 = new List<object> { 70 };
client.SendCommand(commandAdjust, list1);

var list2 = new List<object> { 70, 1, 30 };
client.SendCommand(commandOverride, list2);

var list3 = new List<object> { "attributeEnumSet.presentValue", "writePriorityEnumSet.priorityNone" };
client.SendCommand(commandRelease, list3);
```

### Get Network Devices and other Objects

To get all the available network devices use the GetNetworkDevices method which returns a list of MetasysObjects. This accepts an optional type number as a string to filter the response. To get all of the available types on your server use the GetNetworkDeviceTypes method which returns a list of MetasysObjectType.

```csharp
List<MetasysObjectType> types = client.GetNetworkDeviceTypes().ToList();
int type1 = types[0].Id;
List<MetasysObject> devices = client.GetNetworkDevices(type1.ToString()).ToList();
```

To get the child devices or objects of an object use the GetObjects method. This takes the Guid of the parent object and an optional number of levels to retrieve. The default is 1 level or just the immediate children of the object. Depending on the number of objects on your server this method can take a very long time to complete.

```csharp
List<MetasysObject> devices = client.GetObjects(id).ToList();
```

### Localization of Metasys Enumerations

To get automatically translated enumerations on enumerations returned from a Metasys server you must specify the culture during client creation, or set the "Culture" property before using the "get" methods. The default language for translations will be the machine's current culture ([see more information here](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.currentculture)) or en-US (American English) if the language is not supported (see [Supported Localization Languages](#supported-localization-languages)).

```csharp
client.Culture = new CultureInfo("it-IT");
```

Enumerations returned from a Metasys server will be in a format similar to: "reliabilityEnumSet.reliable". MetasysClient has a method to translate these enumerations manually. This method can be very useful if using an external HttpClient since Metasys servers do not hold translation information.

```csharp
// Access from the client object
string translated = client.Localize("reliabilityEnumSet.reliable"); // Reliable

// Access without instantiating a client
string translated = MetasysClient.StaticLocalize("reliabilityEnumSet.reliable",
    new CultureInfo("it-IT"));  // Affidabile
```

Note: If an automatically translated value (such as Variant.StringValue) contains an enumeration value and not a translated string there could be an error with the MetasysClient globalization setup.

If the enumeration key is desired over the translated value use the EnumerationKey attribute. For example, the translated Variant.Reliability has the enumeration key under the attribute: Variant.ReliabilityEnumerationKey. See the documentation of each Model for more information.

### Spaces and equipment

To get all available spaces on an object use the GetSpaces method. This method will return a list of MetasysObjects. This accepts an optional type number as a string to filter the response. To get all of the available types on your server use the GetSpaceTypes method which returns a list of MetasysObjectType. To get all of the equipment on your server use the GetEquipment method which returns a list of MetasysObjects.
```csharp
List<MetasysObjectType> types = client.GetSpaceTypes().ToList();
int type1 = types[0].Id;
List<MetasysObject> spaces = client.GetSpaces(type1.ToString()).ToList();
List<MetasysObject> equipment = client.GetEquipment().ToList();
```
To get the children objects of Spaces and Equipment use the GetObjects method. This takes the Guid of the parent object and an optional number of levels to retrieve. The default is 1 level or just the immediate children of the object. Depending on the number of objects on your server this method can take a very long time to complete.
If you wish to retrieve equipment for a given space you can use the GetSpaceEquipment method. The deeper element in the hierarchy is the point, use the getEquipmentPoints method to retrieve the points mapped to an equipment. The Point object contains PresentValue when available. 

```csharp
Enumerable<MetasysObject> spaceEquipment = client.GetSpaceEquipment(new Guid(spaceID));
var equipment= spaceEquipment[0];
IEnumerable<Point> equipmentPoints = client.GetEquipmentPoints(equipment.Id);
var point=equipmentPoints[0];
var presentValue=point.PresentValue?.StringValue
```

### Alarms

All services about alarms are provided by Alarms local instance of MetasysClient.
To get all available alarms use the GetAlarms method. This method will return a PagedResult with a list of AlarmItemProvider. This accepts an AlarmFilter object to filter the response. To get a single alarm use the GetSingleAlarm method which returns an AlarmItemProvider object with all the details given the Guid.

```csharp
AlarmFilter alarmFilter = new AlarmFilter
{
    StartTime = new DateTime(2019, 12, 12).ToString(),
    EndTime = new DateTime(2020, 1, 12).ToString(),
    ExcludeAcknowledged=true
};
var alarms = client.GetAlarms(alarmFilter);
var alarmId = alarms.Items.ElementAt(0).Id;
var alarm = client.GetSingleAlarm(alarmId);
var message= alarm.Message;
```
To get the alarms of a specific Object or NetworkDevice use the GetAlarmsForAnObject and GetAlarmsForNetworkDevice methods. The Guid of the parent object is required as input.

```csharp
AlarmFilter alarmFilter = new AlarmFilter{};
var objectId="f5fe6054-d0b0-55b6-b03f-d4554f80d8e6";
var objectAlarms = client.Alarms.GetAlarmsForAnObject(objectId, alarmFilter);
var networkDeviceId="2aefbd18-9088-54ee-b6ef-6d9312da3c33";
var networkDevicesAlarms = client.Alarms.GetAlarmsForNetworkDevice(networkDeviceId, alarmFilter);
```

### Trends

All services about trends are provided by Trends local instance of MetasysClient. To get all available samples given a time filter use the GetSamples method. This method will return a PagedResult with a list of Sample. This accepts the Guid of the object, the attribute ID and a TimeFilter object to filter the response. To get all of the available trended attributes of an object given the ID use the GetTrendedAttributes method. 

```csharp
List<Attribute> trendedAttributes=client.Trends.GetTrendedAttributes(objectId);
int attributeId=trendedAttributes[0].Id;
TimeFilter timeFilter = new TimeFilter
{
    StartTime = new DateTime(2020, 1, 20).ToString(),
    EndTime = new DateTime(2020, 1, 21).ToString()
};
PagedResult<Sample> samples = client.Trends.GetSamples(objectId, attributeId, timeFilter);
int pages= samples.PageCount;
int samplesCount= sample.Total;
Sample firstSample = samples.Items.First();
double value= firstSample.Value;
```
Keep in mind that the object must be properly configured with trended attributes and samples are sent to the ADS/ADX. If you try to retrieve values from an object that has no valid trended attributes a MetasysHttpNotFoundException is raised.

### Audits

All services about audits are provided by Audits local instance of MetasysClient.
To get all available audits use the GetAudits method. This method will return a PagedResult with a list of AuditItemProvider. This accepts an AuditFilter object to filter the response. To get a single audit use the GetSingleAudit method which returns an AuditItemProvider object with all the details given the Guid.

```csharp
AuditFilter auditFilter = new AuditFilter
{
    StartTime = new DateTime(2019, 12, 12).ToString(),
    EndTime = new DateTime(2020, 1, 12).ToString(),
    OriginApplications="0,1",
    ActionTypes="1,2,4",
};
var audits = client.GetAudits(auditFilter);
var auditId = audits.Items.ElementAt(0).Id;
var audit = client.GetSingleAudit(auditId);
var message= audit.Message;
```
To get the audits of a specific Object use the GetAuditsForAnObject method. The Guid of the parent object is required as input.

```csharp
AuditFilter auditFilter = new AuditFilter{};
var objectId="17ac1932-18d8-518c-8012-420c77bea86b";
var objectAudits = client.Audits.GetAuditsForAnObject(objectId, auditFilter);
```

## Usage (COM)

This section demonstrates how to use the LegacyMetasysClient to interact with your Metasys server from a VBA application.
<!-- Download the sample project [here](https://github.com/metasys-server). -->

### Creating a Client

To create a new client and connect to a Metasys server with the default settings use the ComMetasysClientFactory:

```vb
Dim clientFactory As New ComMetasysClientFactory
Dim client As LegacyMetasysClient
Set client = clientFactory.GetLegacyClient("host")
```
There are three optional parameters when creating a new client:

- ignoreCertificateErrors: If your server does not have a valid certificate the MetasysClient will not behave as expected and will likely block the connection. Setting the ignoreCertificateErrors = true will ignore this error and make an insecure connection with the server. To avoid this problem ensure the Metasys server has a valid certificate.
- apiVersion: If your server is not a current 10.1 Metasys server or later this SDK will not function correctly. The version parameter takes in an ApiVersion string value that defaults to the most current release of Metasys. For Metasys 10.1 the api version is V2.
- cultureInfo: To set the language for localization specify the target culture with the ISO Language Code string. The default culture is en-US.

To create a client that ignores certificate errors for a 10.1 Metasys server with Italian translations of values:

```vb
Set client = clientFactory.GetLegacyClient("host", true, "V2", "it-IT")
```
### Login and Access Tokens

After creating the client, to login use the TryLogin method which takes a username, password, and an optional parameter to automatically refresh the access token during the client's lifetime. The default token refresh policy is true.

```vb
Dim token As IComAccessToken
'Automatically refresh token
Set token = client.TryLogin("user", "password")
'Do not automatically refresh token
Set token = client.TryLogin("user", "password", false)
```

### Get an Object Id

In order to use most of the methods in LegacyMetasysClient the id of the target object must be known. This id is in the form of a Guid and can be requested using the following given you know the item reference of the object:

```vb
Dim id As String
id = client.GetObjectIdentifier("siteName:naeName/Folder1.AV1")
```

### Get a Property

In order to get a property you must know the Guid of the target object. An object called "ComVariant" is returned when getting a property from an object. ComVariant contains the property received in many different forms. There is a bit of intuition when handling a ComVariant since it will not explicitly provide the type of object received by the server. If the server cannot find the target object or attribute on the object this method will return a null value.

```vb
Dim result As ComVariant
Set result = client.ReadProperty(id, "presentValue")
Dim stringValue as String
stringValue = result.StringValue
Dim numericValue as Double
numericValue = result.NumericValue
Dim booleanValue as Boolean
booleanValue = result.BooleanValue
```

There is a method to get multiple properties from multiple objects. This can be very useful if the objects all are of the same type or have the same target properties.

```vb
Dim ids() As String
ids = Split("id1,id2", ",")
Dim attributes() As String
ids = Split("name,description", ",")
Dim results() As Object
results = client.ReadPropertyMultiple(ids, attributes)
Dim id1 As IComVariantMultiple
Set id1 = results(0)
Dim variants() As Object
variants=id1.Variants
```

### Write a Property

In order to write a property you must have the Guid of the object and know the attribute name and type. This method contains an optional priority parameter to specify the write priority of the value. This priority is in the form of an enumeration such as "writePriorityEnumSet.priorityNone". To see more options use the "api/v2/enumSets/1/members" or "api/v2/schemas/enums/writePriorityEnumSet" http requests defined in the [Metasys API](https://metasys-server.github.io/api-landing/api/v2/).

```vb
Dim id As String
id = client.GetObjectIdentifier("siteName:naeName/Folder1.AV1")
client.WriteProperty id, "description", "This is an AV."
client.WriteProperty id, "description", "This is an AV.", "writePriorityEnumSet.priorityNone"
```

To change the same attribute values of many objects use the WritePropertyMultiple method. This method also accepts an optional write priority.

```vb
Dim ids() As String
ids = Split("id1,id2", ",")
Dim attributes() As String
attributes = Split("name,description", ",")
Dim attributeValues() As String
attributeValues = Split("AV,This is an AV", ",")
client.WritePropertyMultiple ids, attributes, attributeValues
client.WritePropertyMultiple ids, attributes, attributeValues, "writePriorityEnumSet.priorityNone"
```

### Send Commands

When sending a command there may or may not be a single value or list of values that needs to be sent with the command. The Command.Items property will list all of these values as Items which contains the Title and Type of the value to change. If the type of an Item is "oneOf" this indicates the values is an enumeration and the possible values will be contained in the EnumerationValues list. Keep in mind the values to be sent in the command is the TitleEnumerationKey not the Title. The Title is the user friendly translated value that describes the enumeration. 
```vb

Dim parameters() As String
parameters = Split("offonEnumSet.0,", ",")
client.SendCommand id, "OperatorOverride", parameters

```

### Get Network Devices and other Objects

To get all the available network devices use the GetNetworkDevices method which returns a list of MetasysObjects. This accepts an optional type number as a string to filter the response. To get all of the available types on your server use the GetNetworkDeviceTypes method which returns a list of MetasysObjectType.

```vb
Dim devices() As Object
devices = client.GetNetworkDevices()
Dim device As IComMetasysObject
Set device = devices(0)
Dim itemReference as String
itemReference=device.itemReference
```

To get the child devices or objects of an object use the GetObjects method. This takes the Guid of the parent object and an optional number of levels to retrieve. The default is 1 level or just the immediate children of the object. Depending on the number of objects on your server this method can take a very long time to complete.

```vb
Dim devices() As Object
devices = client.GetObjects(id)
Dim device As IComMetasysObject
Set device = devices(0)
Dim children() As Object
children=device.children
```

### Spaces and equipment

To get all available spaces on an object use the GetSpaces method. This method will return a list of MetasysObjects. This accepts an optional type number as a string to filter the response. To get all of the available types on your server use the GetSpaceTypes method which returns a list of MetasysObjectType. To get all of the equipment on your server use the GetEquipment method which returns a list of MetasysObjects.
```vb
Dim spaceTypes() As Object
spaceTypes = client.GetSpaceTypes()
Dim spaceType As IComMetasysObjectType
Set spaceType = spaceTypes(0)
Dim spaces() As Object
spaces = client.GetSpaces(spaceType.Id)
Dim space As IComMetasysObject
Set space = spaces(0)
Dim equipment() As Object
equipment = client.GetEquipment()
Dim e As IComMetasysObject
Set e = equipment(0)
```
To get the children objects of Spaces and Equipment use the GetObjects method. This takes the Guid of the parent object and an optional number of levels to retrieve. The default is 1 level or just the immediate children of the object. Depending on the number of objects on your server this method can take a very long time to complete. 

### Alarms

To get all available alarms use the GetAlarms method. This method will return a PagedResult with a list of AlarmItemProvider. This accepts an AlarmFilter object to filter the response. To get a single alarm use the GetSingleAlarm method which returns an AlarmItemProvider object with all the details given the Guid.

```vb
'Prepare Alarm filter
Dim filter As New ComAlarmFilter
filter.StartTime = "2020-01-10T08:10:20.243Z"
filter.EndTime = "2020-01-10T09:10:20.243Z"
filter.ExcludeAcknowledged=true
Dim alarmsPager As ComPagedResult
Set alarmsPager = client.GetAlarms(objId, filter)
'Iterate paged results
Dim alarm As ComProvideAlarmItem
Dim alarms() As Object
alarms = alarmsPager.Items
Set alarm = alarms(0)
Dim message as String
message = alarm.message
'Read paging properties
Dim pages as integer
pages=alarmsPager.PageCount
```
To get the alarms of a specific Object or NetworkDevice use the GetAlarmsForAnObject and GetAlarmsForNetworkDevice methods. The Guid of the parent object is required as input.

```vb
Set objectAlarmsPager = client.GetAlarmsForAnObject(objId, filter)
Dim objectAlarms() As Object
ReDim objectAlarms(objectAlarmsPager.Items)
objectAlarms = objectAlarmsPager.Items
Set deviceAlarmsPager = client.GetAlarmsForNetworkDevice(networkDeviceId, filter)
Dim deviceAlarms() As Object
ReDim deviceAlarms(deviceAlarmsPager.Items)
deviceAlarms = deviceAlarmsPager.Items
```
### Trends

 To get all available samples given a time filter use the GetSamples method. This method will return a PagedResult with a list of Sample. This accepts the Guid of the object, the attribute ID and a TimeFilter object to filter the response. To get all of the available trended attributes of an object given the ID use the GetTrendedAttributes method. 

```vb
'Get Trended attributes
Dim attrs() As ComAttribute
attrs = client.GetTrendedAttributes(objId)
Dim attr As ComAttribute
Set attr = attrs(0)
Dim attrId As Integer
attrId = attr.id
'Prepare Time filter
Dim filter As New ComTimeFilter
filter.StartTime = "2020-01-10T08:10:20.243Z"
filter.EndTime = "2020-01-10T09:10:20.243Z"
Dim samplesPager As ComPagedResult
Set samplesPager = client.GetSamples(objId, attrId, filter)
'Iterate paged results
Dim sample As ComSample
Dim samples() As Object
samples = samplesPager.Items
Set sample = samples(0)
Dim value as String
value = sample.Value
'Read paging properties
Dim pages as integer
pages=samplesPager.PageCount
Dim SamplesCount as integer
samplesCount=samplesPager.Total
```
### Audits

To get all available audits use the GetAudits method. This method will return a PagedResult with a list of AuditItemProvider. This accepts an AuditFilter object to filter the response. To get a single audit use the GetSingleAudit method which returns an AuditItemProvider object with all the details given the Guid.

```vb
'Prepare Alarm filter
Dim filter As New ComAuditFilter
filter.StartTime = "2020-01-10T08:10:20.243Z"
filter.EndTime = "2020-01-10T09:10:20.243Z"
filter.OriginApplications="1,2"
filter.ActionTypes="0,1"
Dim auditsPager As ComPagedResult
Set auditsPager = client.GetAudits(objId, filter)
'Iterate paged results
Dim audit As ComProvideAuditItem
Dim audits() As Object
audits = auditsPager.Items
Set audit = audits(0)
Dim user as String
user = audit.user
'Read paging properties
Dim pages as integer
pages=auditsPager.PageCount
```
To get the audits of a specific Object use the GetAuditsForAnObject methods. The Guid of the parent object is required as input.

```vb
Set objectAuditsPager = client.GetAuditsForAnObject(objId, filter)
Dim objectAudits() As Object
ReDim objectAudits(objectAuditsPager.Items)
objectAudits = objectAuditsPager.Items
```

## License

See [LICENSE](LICENSE).

## Contributing

See [CONTRIBUTING](CONTRIBUTING).

## Additional Information

### Supported Localization Languages

- en-US
- cs-CZ
- de-DE
- es-ES
- fr-FR
- hu-HU
- it-IT
- ja-JP
- ko-KR
- nb-NO
- nl-NL
- pl-PL
- pt-BR
- ru-RU
- sv-SE
- tr-TR
- zh-CN
- zh-Hans-CN
- zh-Hant-TW
- zh-TW
