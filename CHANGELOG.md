# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [3.1.0] - 2020-02-13

### Added

- Dedicated Service Provider for Audits.
- Get Audits method with filter object.
- Get Single Audit by guid method.
- Get Audits for a specific object method.

### Fixed

- Empty Guid check on Get Equipment points.

## [3.0.0] - 2020-01-30

### Added

- Dedicated Service Provider for Trends
- Get Trended Attributes method for a specific object.
- Get Samples for a specific object given the attribute ID and the Time filter.

### Changed

- Alarms services are now exposed by a dedicated Alarms provider of MetasysClient class.
- GetAlarms methods now returns a PagedResult of type AlarmItemProvider.
- GetSingleAlarm, GetAlarmsForAnObject, GetAlarmsForNetworkDevice methods now accept Guid object as input in Metasys Services.

### Fixed

- COM methods for Alarms accept input filter object and return results using proper mapping.

## [2.3.0] - 2020-01-20

### Added

- Get Alarms method with filter object.
- Get Single Alarm by guid method.
- Get Alarms for a specific object or network devices methods.

## [2.0.0] - 2019-11-26

### Added

- Get Spaces, Get Space Types and Get Equipment methods.
- Get Space Equipment and GetEquipment Points methods.
- Caching for Get Object Identifier.

### Changed

- MetasysHttpNotFoundException is raised when attempt to read from a nonexistent resource. ReadProperty and GetObjectIdentifier methods do not use nullable return type anymore.
- Factory for COM: GetLegacyClient is now the main method to get the instance.
- Host parameter was moved from TryLogin method to GetLegacyClient Factory.

## [1.0.0] - 2019-10-31

First Release.

### Added

- MetasysClient to perform basic Http requests on a Metasys server.
- LegacyMetasysClient for compatibility with COM services.
- Automatic enumeration translations for supported languages.
