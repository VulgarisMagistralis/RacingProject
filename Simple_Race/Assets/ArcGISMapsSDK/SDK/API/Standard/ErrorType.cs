// COPYRIGHT 1995-2021 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Environmental Systems Research Institute, Inc.
// Attn: Contracts and Legal Services Department
// 380 New York Street
// Redlands, California, 92373
// USA
//
// email: contracts@esri.com

namespace Esri.Standard
{
    public enum ErrorType
    {
        /// Unknown error.
        /// 
        /// - Remark: The catch-all for unknown errors.
        /// - Since: 100.0.0
        Unknown = -1,
        
        /// Success.
        /// 
        /// - Remark: Indicates success, not an error.
        /// - Since: 100.0.0
        Success = 0,
        
        /// A null pointer.
        /// 
        /// - Since: 100.0.0
        CommonNullPtr = 1,
        
        /// Invalid argument.
        /// 
        /// - Since: 100.0.0
        CommonInvalidArgument = 2,
        
        /// Not implemented.
        /// 
        /// - Since: 100.0.0
        CommonNotImplemented = 3,
        
        /// Out of range.
        /// 
        /// - Since: 100.0.0
        CommonOutOfRange = 4,
        
        /// Invalid access.
        /// 
        /// - Since: 100.0.0
        CommonInvalidAccess = 5,
        
        /// Illegal state.
        /// 
        /// - Since: 100.0.0
        CommonIllegalState = 6,
        
        /// Not found.
        /// 
        /// - Since: 100.0.0
        CommonNotFound = 7,
        
        /// Entity exists.
        /// 
        /// - Since: 100.0.0
        CommonExists = 8,
        
        /// Timeout.
        /// 
        /// - Since: 100.0.0
        CommonTimeout = 9,
        
        /// Regular expression error.
        /// 
        /// - Since: 100.0.0
        CommonRegularExpression = 10,
        
        /// Property not supported.
        /// 
        /// - Since: 100.0.0
        CommonPropertyNotSupported = 11,
        
        /// No permission.
        /// 
        /// - Since: 100.0.0
        CommonNoPermission = 12,
        
        /// File error.
        /// 
        /// - Since: 100.0.0
        CommonFile = 13,
        
        /// File not found.
        /// 
        /// - Since: 100.0.0
        CommonFileNotFound = 14,
        
        /// Invalid call.
        /// 
        /// - Since: 100.0.0
        CommonInvalidCall = 15,
        
        /// IO error.
        /// 
        /// - Since: 100.0.0
        CommonIO = 16,
        
        /// User canceled.
        /// 
        /// - Since: 100.0.0
        CommonUserCanceled = 17,
        
        /// Internal error.
        /// 
        /// - Since: 100.0.0
        CommonInternalError = 18,
        
        /// Conversion failed.
        /// 
        /// - Since: 100.0.0
        CommonConversionFailed = 19,
        
        /// No data.
        /// 
        /// - Since: 100.0.0
        CommonNoData = 20,
        
        /// Invalid JSON.
        /// 
        /// - Since: 100.0.0
        CommonInvalidJSON = 21,
        
        /// Propagated error.
        /// 
        /// - Since: 100.0.0
        CommonUserDefinedFailure = 22,
        
        /// Invalid XML.
        /// 
        /// - Since: 100.1.0
        CommonBadXML = 23,
        
        /// Object is already owned.
        /// 
        /// - Since: 100.1.0
        CommonObjectAlreadyOwned = 24,
        
        /// The resource is past its expiry date.
        /// 
        /// - Since: 100.5.0
        CommonExpired = 26,
        
        /// Nullability violation.
        /// 
        /// - Remark: A null was returned from a property or method which is
        /// expected to be non-nullable.
        /// - Since: 100.8.0
        CommonNullabilityViolation = 27,
        
        /// Invalid property.
        /// 
        /// - Remark: The value of a property is invalid.
        /// - Since: 100.12.0
        CommonInvalidProperty = 28,
        
        /// SQLite error.
        /// 
        /// - Since: 100.0.0
        SQLiteError = 1001,
        
        /// SQLite internal error.
        /// 
        /// - Since: 100.0.0
        SQLiteInternal = 1002,
        
        /// SQLite permission.
        /// 
        /// - Since: 100.0.0
        SQLitePerm = 1003,
        
        /// SQLite operation aborted.
        /// 
        /// - Since: 100.0.0
        SQLiteAbort = 1004,
        
        /// SQLite database busy.
        /// 
        /// - Since: 100.0.0
        SQLiteBusy = 1005,
        
        /// SQLite database locked.
        /// 
        /// - Since: 100.0.0
        SQLiteLocked = 1006,
        
        /// SQLite out of memory.
        /// 
        /// - Since: 100.0.0
        SQLiteNoMem = 1007,
        
        /// SQLite read only.
        /// 
        /// - Since: 100.0.0
        SQLiteReadonly = 1008,
        
        /// SQLite operation interrupted.
        /// 
        /// - Since: 100.0.0
        SQLiteInterrupt = 1009,
        
        /// SQLite IO error.
        /// 
        /// - Since: 100.0.0
        SQLiteIOErr = 1010,
        
        /// SQLite corrupt database.
        /// 
        /// - Since: 100.0.0
        SQLiteCorrupt = 1011,
        
        /// SQLite not found.
        /// 
        /// - Since: 100.0.0
        SQLiteNotFound = 1012,
        
        /// SQLite disk full.
        /// 
        /// - Since: 100.0.0
        SQLiteFull = 1013,
        
        /// SQLite cannot open.
        /// 
        /// - Since: 100.0.0
        SQLiteCantOpen = 1014,
        
        /// SQLite file locking protocol.
        /// 
        /// - Since: 100.0.0
        SQLiteProtocol = 1015,
        
        /// SQLite empty error.
        /// 
        /// - Remark: This code is not currently used.
        /// - Since: 100.0.0
        SQLiteEmpty = 1016,
        
        /// SQLite schema changed.
        /// 
        /// - Since: 100.0.0
        SQLiteSchema = 1017,
        
        /// SQLite string or data blob too large.
        /// 
        /// - Since: 100.0.0
        SQLiteTooBig = 1018,
        
        /// SQLite constraint violation.
        /// 
        /// - Since: 100.0.0
        SQLiteConstraint = 1019,
        
        /// SQLite data type mismatch.
        /// 
        /// - Since: 100.0.0
        SQLiteMismatch = 1020,
        
        /// SQLite interface misuse.
        /// 
        /// - Since: 100.0.0
        SQLiteMisuse = 1021,
        
        /// SQLite no large file support.
        /// 
        /// - Since: 100.0.0
        SQLiteNolfs = 1022,
        
        /// SQLite statement not authorized.
        /// 
        /// - Since: 100.0.0
        SQLiteAuth = 1023,
        
        /// SQLite format error.
        /// 
        /// - Remark: This code is not currently used.
        /// - Since: 100.0.0
        SQLiteFormat = 1024,
        
        /// SQLite out of range.
        /// 
        /// - Since: 100.0.0
        SQLiteRange = 1025,
        
        /// Not an SQLite database.
        /// 
        /// - Since: 100.0.0
        SQLiteNotADatabase = 1026,
        
        /// SQLite unusual operation notice.
        /// 
        /// - Since: 100.0.0
        SQLiteNotice = 1027,
        
        /// SQLite unusual operation warning.
        /// 
        /// - Since: 100.0.0
        SQLiteWarning = 1028,
        
        /// SQLite row is available.
        /// 
        /// - Since: 100.0.0
        SQLiteRow = 1029,
        
        /// SQLite operation is complete.
        /// 
        /// - Since: 100.0.0
        SQLiteDone = 1030,
        
        /// Unknown geometry error.
        /// 
        /// - Since: 100.1.0
        GeometryUnknownError = 2000,
        
        /// Corrupt geometry.
        /// 
        /// - Since: 100.0.0
        GeometryCorruptedGeometry = 2001,
        
        /// Empty geometry.
        /// 
        /// - Since: 100.0.0
        GeometryEmptyGeometry = 2002,
        
        /// Math singularity.
        /// 
        /// - Since: 100.0.0
        GeometryMathSingularity = 2003,
        
        /// Geometry buffer too small.
        /// 
        /// - Since: 100.0.0
        GeometryBufferIsTooSmall = 2004,
        
        /// Geometry invalid shape type.
        /// 
        /// - Since: 100.0.0
        GeometryInvalidShapeType = 2005,
        
        /// Geometry projection out of supported range.
        /// 
        /// - Since: 100.0.0
        GeometryProjectionOutOfSupportedRange = 2006,
        
        /// Non simple geometry.
        /// 
        /// - Since: 100.0.0
        GeometryNonSimpleGeometry = 2007,
        
        /// Cannot calculate geodesic.
        /// 
        /// - Since: 100.0.0
        GeometryCannotCalculateGeodesic = 2008,
        
        /// Geometry notation conversion.
        /// 
        /// - Since: 100.0.0
        GeometryNotationConversion = 2009,
        
        /// Missing grid file.
        /// 
        /// - Since: 100.0.0
        GeometryMissingGridFile = 2010,
        
        /// Geodatabase value out of range.
        /// 
        /// - Since: 100.0.0
        GeodatabaseValueOutOfRange = 3001,
        
        /// Geodatabase data type mismatch.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDataTypeMismatch = 3002,
        
        /// Geodatabase invalid XML.
        /// 
        /// - Since: 100.0.0
        GeodatabaseBadXML = 3003,
        
        /// Database already exists.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDatabaseAlreadyExists = 3004,
        
        /// Database does not exist.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDatabaseDoesNotExist = 3005,
        
        /// Geodatabase name longer than 128 characters.
        /// 
        /// - Since: 100.0.0
        GeodatabaseNameLongerThan128Characters = 3006,
        
        /// Geodatabase invalid shape type.
        /// 
        /// - Since: 100.0.0
        GeodatabaseInvalidShapeType = 3007,
        
        /// Geodatabase raster not supported.
        /// 
        /// - Since: 100.0.0
        GeodatabaseRasterNotSupported = 3008,
        
        /// Geodatabase relationship class one to one.
        /// 
        /// - Since: 100.0.0
        GeodatabaseRelationshipClassOneToOne = 3009,
        
        /// Geodatabase item not found.
        /// 
        /// - Since: 100.0.0
        GeodatabaseItemNotFound = 3010,
        
        /// Geodatabase duplicate code.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDuplicateCode = 3011,
        
        /// Geodatabase missing code.
        /// 
        /// - Since: 100.0.0
        GeodatabaseMissingCode = 3012,
        
        /// Geodatabase wrong item type.
        /// 
        /// - Since: 100.0.0
        GeodatabaseWrongItemType = 3013,
        
        /// Geodatabase Id field not nullable.
        /// 
        /// - Since: 100.0.0
        GeodatabaseIdFieldNotNullable = 3014,
        
        /// Geodatabase default value not supported.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDefaultValueNotSupported = 3015,
        
        /// Geodatabase table not editable.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTableNotEditable = 3016,
        
        /// Geodatabase field not found.
        /// 
        /// - Since: 100.0.0
        GeodatabaseFieldNotFound = 3017,
        
        /// Geodatabase field exists.
        /// 
        /// - Since: 100.0.0
        GeodatabaseFieldExists = 3018,
        
        /// Geodatabase cannot alter field type.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotAlterFieldType = 3019,
        
        /// Geodatabase cannot alter field width.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotAlterFieldWidth = 3020,
        
        /// Geodatabase cannot alter field to nullable.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotAlterFieldToNullable = 3021,
        
        /// Geodatabase cannot alter field to editable.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotAlterFieldToEditable = 3022,
        
        /// Geodatabase cannot alter field to deletable.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotAlterFieldToDeletable = 3023,
        
        /// Geodatabase cannot alter geometry properties.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotAlterGeometryProperties = 3024,
        
        /// Geodatabase unnamed table.
        /// 
        /// - Since: 100.0.0
        GeodatabaseUnnamedTable = 3025,
        
        /// Geodatabase invalid type for domain.
        /// 
        /// - Since: 100.0.0
        GeodatabaseInvalidTypeForDomain = 3026,
        
        /// Geodatabase min max reversed.
        /// 
        /// - Since: 100.0.0
        GeodatabaseMinMaxReversed = 3027,
        
        /// Geodatabase field not supported on relationship class.
        /// 
        /// - Since: 100.0.0
        GeodatabaseFieldNotSupportedOnRelationshipClass = 3028,
        
        /// Geodatabase relationship class key.
        /// 
        /// - Since: 100.0.0
        GeodatabaseRelationshipClassKey = 3029,
        
        /// Geodatabase value is null.
        /// 
        /// - Since: 100.0.0
        GeodatabaseValueIsNull = 3030,
        
        /// Geodatabase multiple SQL statements.
        /// 
        /// - Since: 100.0.0
        GeodatabaseMultipleSQLStatements = 3031,
        
        /// Geodatabase no SQL statements.
        /// 
        /// - Since: 100.0.0
        GeodatabaseNoSQLStatements = 3032,
        
        /// Geodatabase geometry field missing.
        /// 
        /// - Since: 100.0.0
        GeodatabaseGeometryFieldMissing = 3033,
        
        /// Geodatabase transaction started.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTransactionStarted = 3034,
        
        /// Geodatabase transaction not started.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTransactionNotStarted = 3035,
        
        /// Geodatabase shape requires z.
        /// 
        /// - Since: 100.0.0
        GeodatabaseShapeRequiresZ = 3036,
        
        /// Geodatabase shape requires m.
        /// 
        /// - Since: 100.0.0
        GeodatabaseShapeRequiresM = 3037,
        
        /// Geodatabase shape no z.
        /// 
        /// - Since: 100.0.0
        GeodatabaseShapeNoZ = 3038,
        
        /// Geodatabase shape no m.
        /// 
        /// - Since: 100.0.0
        GeodatabaseShapeNoM = 3039,
        
        /// Geodatabase shape wrong type.
        /// 
        /// - Since: 100.0.0
        GeodatabaseShapeWrongType = 3040,
        
        /// Geodatabase cannot update field type.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotUpdateFieldType = 3041,
        
        /// Geodatabase no rows affected.
        /// 
        /// - Since: 100.0.0
        GeodatabaseNoRowsAffected = 3042,
        
        /// Geodatabase subtype invalid.
        /// 
        /// - Since: 100.0.0
        GeodatabaseSubtypeInvalid = 3043,
        
        /// Geodatabase subtype must be integer.
        /// 
        /// - Since: 100.0.0
        GeodatabaseSubtypeMustBeInteger = 3044,
        
        /// Geodatabase subtypes not enabled.
        /// 
        /// - Since: 100.0.0
        GeodatabaseSubtypesNotEnabled = 3045,
        
        /// Geodatabase subtype exists.
        /// 
        /// - Since: 100.0.0
        GeodatabaseSubtypeExists = 3046,
        
        /// Geodatabase duplicate field not allowed.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDuplicateFieldNotAllowed = 3047,
        
        /// Geodatabase cannot delete field.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotDeleteField = 3048,
        
        /// Geodatabase index exists.
        /// 
        /// - Since: 100.0.0
        GeodatabaseIndexExists = 3049,
        
        /// Geodatabase index not found.
        /// 
        /// - Since: 100.0.0
        GeodatabaseIndexNotFound = 3050,
        
        /// Geodatabase cursor not on row.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCursorNotOnRow = 3051,
        
        /// Geodatabase internal error.
        /// 
        /// - Since: 100.0.0
        GeodatabaseInternalError = 3052,
        
        /// Geodatabase cannot write geodatabase managed fields.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotWriteGeodatabaseManagedFields = 3053,
        
        /// Geodatabase item already exists.
        /// 
        /// - Since: 100.0.0
        GeodatabaseItemAlreadyExists = 3054,
        
        /// Geodatabase invalid spatial index name.
        /// 
        /// - Since: 100.0.0
        GeodatabaseInvalidSpatialIndexName = 3055,
        
        /// Geodatabase requires spatial index.
        /// 
        /// - Since: 100.0.0
        GeodatabaseRequiresSpatialIndex = 3056,
        
        /// Geodatabase reserved name.
        /// 
        /// - Since: 100.0.0
        GeodatabaseReservedName = 3057,
        
        /// Geodatabase cannot update schema if change tracking.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotUpdateSchemaIfChangeTracking = 3058,
        
        /// Geodatabase invalid date.
        /// 
        /// - Since: 100.0.0
        GeodatabaseInvalidDate = 3059,
        
        /// Geodatabase database does not have changes.
        /// 
        /// - Since: 100.0.0
        GeodatabaseDatabaseDoesNotHaveChanges = 3060,
        
        /// Geodatabase replica does not exist.
        /// 
        /// - Since: 100.0.0
        GeodatabaseReplicaDoesNotExist = 3061,
        
        /// Geodatabase storage type not supported.
        /// 
        /// - Since: 100.0.0
        GeodatabaseStorageTypeNotSupported = 3062,
        
        /// Geodatabase replica model error.
        /// 
        /// - Since: 100.0.0
        GeodatabaseReplicaModelError = 3063,
        
        /// Geodatabase replica client generation error.
        /// 
        /// - Since: 100.0.0
        GeodatabaseReplicaClientGenError = 3064,
        
        /// Geodatabase replica no upload to acknowledge.
        /// 
        /// - Since: 100.0.0
        GeodatabaseReplicaNoUploadToAcknowledge = 3065,
        
        /// Geodatabase last write time in the future.
        /// 
        /// - Since: 100.0.0
        GeodatabaseLastWriteTimeInTheFuture = 3066,
        
        /// Geodatabase invalid argument.
        /// 
        /// - Since: 100.0.0
        GeodatabaseInvalidArgument = 3067,
        
        /// Geodatabase transportation network corrupt.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTransportationNetworkCorrupt = 3068,
        
        /// Geodatabase transportation network file IO error.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTransportationNetworkFileIO = 3069,
        
        /// Geodatabase feature has pending edits.
        /// 
        /// - Since: 100.0.0
        GeodatabaseFeatureHasPendingEdits = 3070,
        
        /// Geodatabase change tracking not enabled.
        /// 
        /// - Since: 100.0.0
        GeodatabaseChangeTrackingNotEnabled = 3071,
        
        /// Geodatabase transportation network file open.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTransportationNetworkFileOpen = 3072,
        
        /// Geodatabase transportation network unsupported.
        /// 
        /// - Since: 100.0.0
        GeodatabaseTransportationNetworkUnsupported = 3073,
        
        /// Geodatabase cannot sync copy.
        /// 
        /// - Since: 100.0.0
        GeodatabaseCannotSyncCopy = 3074,
        
        /// Geodatabase access control denied.
        /// 
        /// - Since: 100.0.0
        GeodatabaseAccessControlDenied = 3075,
        
        /// Geodatabase geometry outside replica extent.
        /// 
        /// - Since: 100.0.0
        GeodatabaseGeometryOutsideReplicaExtent = 3076,
        
        /// Geodatabase upload already in progress.
        /// 
        /// - Since: 100.0.0
        GeodatabaseUploadAlreadyInProgress = 3077,
        
        /// Geodatabase is closed.
        /// 
        /// - Since: 100.5.0
        GeodatabaseDatabaseClosed = 3078,
        
        /// Domain exists.
        /// 
        /// - Since: 100.1.0
        GeodatabaseDomainAlreadyExists = 3079,
        
        /// Geodatabase geometry type not supported.
        /// 
        /// - Since: 100.5.0
        GeodatabaseGeometryTypeNotSupported = 3080,
        
        /// ArcGISFeatureTable requires a global Id field to support adding bulk attachments.
        /// 
        /// - Since: 100.9.0
        GeodatabaseAttachmentsRequireGlobalIds = 3081,
        
        /// Geocode unsupported file format.
        /// 
        /// - Since: 100.0.0
        GeocodeUnsupportedFileFormat = 4001,
        
        /// Geocode unsupported spatial reference.
        /// 
        /// - Since: 100.0.0
        GeocodeUnsupportedSpatialReference = 4002,
        
        /// Geocode unsupported projection transformation.
        /// 
        /// - Since: 100.0.0
        GeocodeUnsupportedProjectionTransformation = 4003,
        
        /// Geocoder creation error.
        /// 
        /// - Since: 100.0.0
        GeocodeGeocoderCreation = 4004,
        
        /// Geocode intersections not supported.
        /// 
        /// - Since: 100.0.0
        GeocodeIntersectionsNotSupported = 4005,
        
        /// Uninitialized geocode task.
        /// 
        /// - Since: 100.0.0
        GeocodeUninitializedGeocodeTask = 4006,
        
        /// Invalid geocode locator properties.
        /// 
        /// - Since: 100.0.0
        GeocodeInvalidLocatorProperties = 4007,
        
        /// Geocode required field missing.
        /// 
        /// - Since: 100.0.0
        GeocodeRequiredFieldMissing = 4008,
        
        /// Geocode cannot read address.
        /// 
        /// - Since: 100.0.0
        GeocodeCannotReadAddress = 4009,
        
        /// Geocoding not supported.
        /// 
        /// - Since: 100.0.0
        GeocodeReverseGeocodingNotSupported = 4010,
        
        /// Geocode geometry type not supported.
        /// 
        /// - Since: 100.10.0
        GeocodeGeometryTypeNotSupported = 4011,
        
        /// Geocode suggest address not supported.
        /// 
        /// - Since: 100.10.0
        GeocodeSuggestAddressNotSupported = 4012,
        
        /// Geocode suggest result corrupt.
        /// 
        /// - Since: 100.10.0
        GeocodeSuggestResultCorrupted = 4013,
        
        /// Network analyst invalid route settings.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidRouteSettings = 5001,
        
        /// Network analyst no solution.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystNoSolution = 5002,
        
        /// Network analyst task canceled.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystTaskCanceled = 5003,
        
        /// Network analyst invalid network.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidNetwork = 5004,
        
        /// Network analyst directions error.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystDirectionsError = 5005,
        
        /// Network analyst insufficient number of stops.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInsufficientNumberOfStops = 5006,
        
        /// Network analyst stop unlocated.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStopUnlocated = 5007,
        
        /// Network analyst stop located on non traversable element.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStopLocatedOnNonTraversableElement = 5008,
        
        /// Network analyst point barrier invalid added cost attribute name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPointBarrierInvalidAddedCostAttributeName = 5009,
        
        /// Network analyst line barrier invalid scaled cost attribute name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystLineBarrierInvalidScaledCostAttributeName = 5010,
        
        /// Network analyst polygon barrier invalid scaled cost attribute name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPolygonBarrierInvalidScaledCostAttributeName = 5011,
        
        /// Network analyst polygon barrier invalid scaled cost attribute value.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPolygonBarrierInvalidScaledCostAttributeValue = 5012,
        
        /// Network analyst polyline barrier invalid scaled cost attribute value.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPolylineBarrierInvalidScaledCostAttributeValue = 5013,
        
        /// Network analyst invalid impedance attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidImpedanceAttribute = 5014,
        
        /// Network analyst invalid restriction attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidRestrictionAttribute = 5015,
        
        /// Network analyst invalid accumulate attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidAccumulateAttribute = 5016,
        
        /// Network analyst invalid directions time attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidDirectionsTimeAttribute = 5017,
        
        /// Network analyst invalid directions distance attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidDirectionsDistanceAttribute = 5018,
        
        /// Network analyst invalid attribute parameters attribute name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidAttributeParametersAttributeName = 5019,
        
        /// Network analyst invalid attributes parameters parameter name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidAttributeParametersParameterName = 5020,
        
        /// Network analyst invalid attributes parameters value type.
        /// 
        /// - Since: 100.0.0
        /// - Attention: Deprecated at 100.6.0. No longer used.
        NetworkAnalystInvalidAttributeParametersValueType = 5021,
        
        /// Network analyst invalid attribute parameters restriction usage value.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidAttributeParametersRestrictionUsageValue = 5022,
        
        /// Network analyst network has no hierarchy attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystNetworkHasNoHierarchyAttribute = 5023,
        
        /// Network analyst no path found between stops.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystNoPathFoundBetweenStops = 5024,
        
        /// Network analyst undefined input spatial reference.
        /// 
        /// - Since: 100.0.0
        /// - Attention: Deprecated at 100.6.0. No longer used.
        NetworkAnalystUndefinedInputSpatialReference = 5025,
        
        /// Network analyst undefined output spatial reference.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystUndefinedOutputSpatialReference = 5026,
        
        /// Network analyst invalid directions style.
        /// 
        /// - Since: 100.0.0
        /// - Attention: Deprecated at 100.6.0. No longer used.
        NetworkAnalystInvalidDirectionsStyle = 5027,
        
        /// Deprecated. Network analyst invalid directions language.
        /// 
        /// - Since: 100.0.0
        /// - Attention: Deprecated at 100.2.0. No longer used.
        NetworkAnalystInvalidDirectionsLanguage = 5028,
        
        /// Network analyst directions time and impedance attribute mismatch.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystDirectionsTimeAndImpedanceAttributeMismatch = 5029,
        
        /// Network analyst invalid directions road class attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidDirectionsRoadClassAttribute = 5030,
        
        /// Network analyst stop cannot be reached.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStopIsUnreachable = 5031,
        
        /// Network analyst stop time window starts before unix epoch.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStopTimeWindowStartsBeforeUnixEpoch = 5032,
        
        /// Network analyst stop time window is inverted.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStopTimeWindowIsInverted = 5033,
        
        /// Walking mode route too large.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystWalkingModeRouteTooLarge = 5034,
        
        /// Stop has null geometry.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStopHasNullGeometry = 5035,
        
        /// Point barrier has null geometry.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPointBarrierHasNullGeometry = 5036,
        
        /// Polyline barrier has null geometry.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPolylineBarrierHasNullGeometry = 5037,
        
        /// Polygon barrier has null geometry.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPolygonBarrierHasNullGeometry = 5038,
        
        /// Online route task does not support search_where_clause condition.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystUnsupportedSearchWhereClause = 5039,
        
        /// Network analyst insufficient number of facilities.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInsufficientNumberOfFacilities = 5040,
        
        /// Network analyst facility has null geometry.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystFacilityHasNullGeometry = 5041,
        
        /// Network analyst facility has invalid added cost attribute name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystFacilityHasInvalidAddedCostAttributeName = 5042,
        
        /// Network analyst facility has negative added cost attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystFacilityHasNegativeAddedCostAttribute = 5043,
        
        /// Network analyst facility has invalid impedance cutoff.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystFacilityHasInvalidImpedanceCutoff = 5044,
        
        /// Network analyst insufficient number of incidents.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInsufficientNumberOfIncidents = 5045,
        
        /// Network analyst incident has null geometry.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystIncidentHasNullGeometry = 5046,
        
        /// Network analyst incident has invalid added cost attribute name.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystIncidentHasInvalidAddedCostAttributeName = 5047,
        
        /// Network analyst incident has negative added cost attribute.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystIncidentHasNegativeAddedCostAttribute = 5048,
        
        /// Network analyst invalid target facility count.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidTargetFacilityCount = 5049,
        
        /// Network analyst incident has invalid impedance cutoff.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystIncidentHasInvalidImpedanceCutoff = 5050,
        
        /// Network analyst start time is before Unix epoch.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystStartTimeIsBeforeUnixEpoch = 5051,
        
        /// Network analyst invalid default impedance cutoff.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidDefaultImpedanceCutoff = 5052,
        
        /// Network analyst invalid default target facility count.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidDefaultTargetFacilityCount = 5053,
        
        /// Network analyst invalid polygon buffer distance.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystInvalidPolygonBufferDistance = 5054,
        
        /// Network analyst polylines cannot be returned.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystPolylinesCannotBeReturned = 5055,
        
        /// Network analyst solving non time impedance, but time windows applied.
        /// 
        /// - Since: 100.0.0
        NetworkAnalystTimeWindowsWithNonTimeImpedance = 5056,
        
        /// One or more stops have unsupported type.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystUnsupportedStopType = 5057,
        
        /// Network analyst route starts or ends on a waypoint.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystRouteStartsOrEndsOnWaypoint = 5058,
        
        /// Network analyst reordering stops (Traveling Salesman Problem) is not supported when the collection of stops contains waypoints or rest breaks.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystWaypointsAndRestBreaksForbiddenReordering = 5059,
        
        /// Network analyst waypoint contains time windows.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystWaypointHasTimeWindows = 5060,
        
        /// Network analyst waypoint contains added cost attribute.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystWaypointHasAddedCostAttribute = 5061,
        
        /// Network analyst stop has unknown curb approach.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystStopHasInvalidCurbApproach = 5062,
        
        /// Network analyst point barrier has unknown curb approach.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystPointBarrierHasInvalidCurbApproach = 5063,
        
        /// Network analyst facility has unknown curb approach.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystFacilityHasInvalidCurbApproach = 5064,
        
        /// Network analyst incident has unknown curb approach.
        /// 
        /// - Since: 100.1.0
        NetworkAnalystIncidentHasInvalidCurbApproach = 5065,
        
        /// Network dataset has no directions attributes.
        /// 
        /// - Since: 100.3.0
        NetworkAnalystNetworkDoesNotSupportDirections = 5066,
        
        /// Desired direction language not supported by platform.
        /// 
        /// - Since: 100.3.0
        NetworkAnalystDirectionsLanguageNotFound = 5067,
        
        /// Route result requires re-solving with current route task.
        /// 
        /// - Since: 100.6.0
        NetworkAnalystRouteResultCannotBeUpdated = 5068,
        
        /// Input route result does not have directions.
        /// 
        /// - Since: 100.6.0
        NetworkAnalystNoDirections = 5069,
        
        /// Input route result does not have stops.
        /// 
        /// - Since: 100.6.0
        NetworkAnalystNoStops = 5070,
        
        /// Input route result doesn't have the route with passed route index.
        /// 
        /// - Since: 100.6.0
        NetworkAnalystInvalidRouteIndex = 5071,
        
        /// Input remaining destinations count doesn't match with input routes stops collection.
        /// 
        /// - Since: 100.6.0
        NetworkAnalystInvalidRemainingDestinationsCount = 5072,
        
        /// JSON parser invalid token.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidToken = 6001,
        
        /// JSON parser invalid character.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidCharacter = 6002,
        
        /// JSON parser invalid unicode.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidUnicode = 6003,
        
        /// JSON parser invalid start of JSON.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidJSONStart = 6004,
        
        /// JSON parser invalid end of pair.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidEndOfPair = 6005,
        
        /// JSON parser invalid end of element.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidEndOfElement = 6006,
        
        /// JSON parser invalid escape sequence.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidEscapeSequence = 6007,
        
        /// JSON parser invalid end of field name.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidEndOfFieldName = 6008,
        
        /// JSON parser invalid start of field name.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidStartOfFieldName = 6009,
        
        /// JSON parser invalid start of comment.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidStartOfComment = 6010,
        
        /// JSON parser invalid decimal digit.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidDecDigit = 6011,
        
        /// JSON parser invalid hex digit.
        /// 
        /// - Since: 100.0.0
        JSONParserInvalidHexDigit = 6012,
        
        /// JSON parser expecting null.
        /// 
        /// - Since: 100.0.0
        JSONParserExpectingNull = 6013,
        
        /// JSON parser expecting true.
        /// 
        /// - Since: 100.0.0
        JSONParserExpectingTrue = 6014,
        
        /// JSON parser expecting false.
        /// 
        /// - Since: 100.0.0
        JSONParserExpectingFalse = 6015,
        
        /// JSON parser expecting closing quote.
        /// 
        /// - Since: 100.0.0
        JSONParserExpectingClosingQuote = 6016,
        
        /// JSON parser expecting not a number.
        /// 
        /// - Since: 100.0.0
        JSONParserExpectingNan = 6017,
        
        /// JSON parser expecting end of comment.
        /// 
        /// - Since: 100.0.0
        JSONParserExpectingEndOfComment = 6018,
        
        /// JSON parser unexpected end of data.
        /// 
        /// - Since: 100.0.0
        JSONParserUnexpectedEndOfData = 6019,
        
        /// JSON object expecting start object.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingStartObject = 6020,
        
        /// JSON object expecting start array.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingStartArray = 6021,
        
        /// JSON object expecting value object.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingValueObject = 6022,
        
        /// JSON object expecting value array.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingValueArray = 6023,
        
        /// JSON object expecting value int32.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingValueInt32 = 6024,
        
        /// JSON object expecting integer type.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingIntegerType = 6025,
        
        /// JSON object expecting number type.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingNumberType = 6026,
        
        /// JSON object expecting value string.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingValueString = 6027,
        
        /// JSON object expecting value bool.
        /// 
        /// - Since: 100.0.0
        JSONObjectExpectingValueBool = 6028,
        
        /// JSON object iterator not started.
        /// 
        /// - Since: 100.0.0
        JSONObjectIteratorNotStarted = 6029,
        
        /// JSON object iterator is finished.
        /// 
        /// - Since: 100.0.0
        JSONObjectIteratorIsFinished = 6030,
        
        /// JSON object key not found.
        /// 
        /// - Since: 100.0.0
        JSONObjectKeyNotFound = 6031,
        
        /// JSON object index out of range.
        /// 
        /// - Since: 100.0.0
        JSONObjectIndexOutOfRange = 6032,
        
        /// JSON string writer JSON is complete.
        /// 
        /// - Since: 100.0.0
        JSONStringWriterJSONIsComplete = 6033,
        
        /// JSON string writer invalid JSON input.
        /// 
        /// - Since: 100.0.0
        JSONStringWriterInvalidJSONInput = 6034,
        
        /// JSON string writer expecting container.
        /// 
        /// - Since: 100.0.0
        JSONStringWriterExpectingContainer = 6035,
        
        /// JSON string writer expecting key or end object.
        /// 
        /// - Since: 100.0.0
        JSONStringWriterExpectingKeyOrEndObject = 6036,
        
        /// JSON string writer expecting value or end array.
        /// 
        /// - Since: 100.0.0
        JSONStringWriterExpectingValueOrEndArray = 6037,
        
        /// JSON string writer expecting value.
        /// 
        /// - Since: 100.0.0
        JSONStringWriterExpectingValue = 6038,
        
        /// Spatial reference is missing.
        /// 
        /// - Since: 100.0.0
        MappingMissingSpatialReference = 7001,
        
        /// Initial viewpoint is missing.
        /// 
        /// - Since: 100.0.0
        MappingMissingInitialViewpoint = 7002,
        
        /// Invalid request response.
        /// 
        /// - Since: 100.0.0
        MappingInvalidResponse = 7003,
        
        /// Bing maps key is missing.
        /// 
        /// - Since: 100.1.0
        MappingMissingBingMapsKey = 7004,
        
        /// Layer type is not supported.
        /// 
        /// - Since: 100.0.0
        MappingUnsupportedLayerType = 7005,
        
        /// Sync not enabled.
        /// 
        /// - Since: 100.0.0
        MappingSyncNotEnabled = 7006,
        
        /// Tile export is not enabled.
        /// 
        /// - Since: 100.0.0
        MappingTileExportNotEnabled = 7007,
        
        /// Required item property is missing.
        /// 
        /// - Since: 100.0.0
        MappingMissingItemProperty = 7008,
        
        /// Web map version is not supported.
        /// 
        /// - Since: 100.0.0
        MappingWebmapNotSupported = 7009,
        
        /// Spatial reference invalid or incompatible.
        /// 
        /// - Since: 100.1.0
        MappingSpatialReferenceInvalid = 7011,
        
        /// Package needs to be unpacked before it can be used.
        /// 
        /// - Since: 100.2.1
        MappingPackageUnpackRequired = 7012,
        
        /// Elevation source data format is not supported.
        /// 
        /// - Since: 100.3.0
        MappingUnsupportedElevationFormat = 7013,
        
        /// Web scene version or viewing mode is not supported.
        /// 
        /// - Since: 100.3.0
        MappingWebsceneNotSupported = 7014,
        
        /// Loadable object is not loaded when it is expected to be loaded.
        /// 
        /// - Since: 100.6.0
        MappingNotLoaded = 7015,
        
        /// Scheduled updates for an offline preplanned map area are not supported.
        /// 
        /// - Since: 100.6.0
        MappingScheduledUpdatesNotSupported = 7016,
        
        /// Trace operation executed by the service failed.
        /// 
        /// - Since: 100.7.0
        MappingUtilityNetworkTraceFailed = 7017,
        
        /// Arcade expression is invalid.
        /// 
        /// - Since: 100.8.0
        MappingInvalidArcadeExpression = 7018,
        
        /// Requested extent contains too many associations.
        /// 
        /// - Since: 100.8.0
        MappingUtilityNetworkTooManyAssociations = 7019,
        
        /// A layer has requested more features than the service maximum feature count.
        /// 
        /// - Since: 100.9.0
        MappingMaxFeatureCountExceeded = 7020,
        
        /// Feature service does not support branch versioning.
        /// 
        /// - Since: 100.9.0
        MappingBranchVersioningNotSupportedByService = 7021,
        
        /// Packaging of data for the preplanned map area is not complete and it is not ready for download.
        /// 
        /// - Since: 100.9.0
        MappingPackagingNotComplete = 7022,
        
        /// An upload sync direction is not supported.
        /// 
        /// - Since: 100.9.0
        MappingSyncDirectionUploadNotSupported = 7023,
        
        /// Tile export in .tpkx format is not supported.
        /// 
        /// - Since: 100.10.0
        MappingTileCacheCompactV2ExportNotEnabled = 7024,
        
        /// The specified layer does not intersect the area of interest.
        /// 
        /// - Since: 100.10.12
        MappingLayerDoesNotIntersectAreaOfInterest = 7025,
        
        /// Unlicensed feature.
        /// 
        /// - Since: 100.0.0
        LicensingUnlicensedFeature = 8001,
        
        /// License level fixed.
        /// 
        /// - Since: 100.0.0
        LicensingLicenseLevelFixed = 8002,
        
        /// License level is already set.
        /// 
        /// - Since: 100.0.0
        LicensingLicenseLevelAlreadySet = 8003,
        
        /// Main license is not set.
        /// 
        /// - Since: 100.0.0
        LicensingMainLicenseNotSet = 8004,
        
        /// Unlicensed extension.
        /// 
        /// - Since: 100.0.0
        LicensingUnlicensedExtension = 8005,
        
        /// Portal user with no license.
        /// 
        /// - Since: 100.7.0
        LicensingPortalUserWithNoLicense = 8006,
        
        /// IO error.
        /// 
        /// - Since: 100.0.0
        StdIOSBaseFailure = 10001,
        
        /// Invalid array length.
        /// 
        /// - Since: 100.0.0
        StdBadArrayNewLength = 10002,
        
        /// Arithmetic underflow.
        /// 
        /// - Since: 100.0.0
        StdUnderflowError = 10003,
        
        /// System error.
        /// 
        /// - Since: 100.0.0
        StdSystemError = 10004,
        
        /// Range error.
        /// 
        /// - Since: 100.0.0
        StdRangeError = 10005,
        
        /// Arithmetic overflow.
        /// 
        /// - Since: 100.0.0
        StdOverflowError = 10006,
        
        /// Out of range.
        /// 
        /// - Since: 100.0.0
        StdOutOfRange = 10007,
        
        /// Length error.
        /// 
        /// - Since: 100.0.0
        StdLengthError = 10008,
        
        /// Invalid argument.
        /// 
        /// - Since: 100.0.0
        StdInvalidArgument = 10009,
        
        /// Asynchronous error.
        /// 
        /// - Since: 100.0.0
        StdFutureError = 10010,
        
        /// Math domain error.
        /// 
        /// - Since: 100.0.0
        StdDomainError = 10011,
        
        /// Unknown error.
        /// 
        /// - Since: 100.0.0
        StdRuntimeError = 10012,
        
        /// Logic error.
        /// 
        /// - Since: 100.0.0
        StdLogicError = 10013,
        
        /// Invalid weak reference.
        /// 
        /// - Since: 100.0.0
        StdBadWeakPtr = 10014,
        
        /// Invalid type Id.
        /// 
        /// - Since: 100.0.0
        StdBadTypeId = 10015,
        
        /// Invalid function call.
        /// 
        /// - Since: 100.0.0
        StdBadFunctionCall = 10016,
        
        /// Invalid error management.
        /// 
        /// - Since: 100.0.0
        StdBadException = 10017,
        
        /// Invalid cast.
        /// 
        /// - Since: 100.0.0
        StdBadCast = 10018,
        
        /// Out of memory.
        /// 
        /// - Since: 100.0.0
        StdBadAlloc = 10019,
        
        /// Unknown error.
        /// 
        /// - Since: 100.0.0
        StdException = 10020,
        
        /// Service doesn't support rerouting.
        /// 
        /// - Since: 100.6.0
        NavigationReroutingNotSupportedByService = 13001,
        
        /// HTTP client operation canceled.
        /// 
        /// - Since: 100.10.0
        HTTPClientOperationCanceled = 14001,
        
        /// HTTP client timed out.
        /// 
        /// - Since: 100.10.0
        HTTPClientTimedOut = 14002,
        
        /// HTTP client unsupported method.
        /// 
        /// - Since: 100.10.0
        HTTPClientUnsupportedMethod = 14003,
        
        /// HTTP client unsupported protocol scheme.
        /// 
        /// - Since: 100.10.0
        HTTPClientUnsupportedProtocolScheme = 14004,
        
        /// Authentication configuration required.
        /// 
        /// - Since: 100.11.0
        SecurityAuthenticationConfigurationRequired = 15001,
        
        /// Authentication configuration type not supported.
        /// 
        /// - Since: 100.11.0
        SecurityAuthenticationConfigurationTypeNotSupported = 15002,
        
        /// Authentication failed.
        /// 
        /// - Since: 100.11.0
        SecurityAuthenticationFailed = 15003,
        
        /// A problem was encountered with a GeotriggerFeed.
        /// 
        /// - Remark: An invalid GeotriggerFeed indicates that a GeotriggerMonitor is unable to
        /// perform checks. No GeotriggerNotificationInfo events will be sent.
        /// - Since: 100.12.0
        GeotriggersFeedError = 16001,
        
        /// A problem was encountered with the FenceParameters for a FenceGeotrigger.
        /// 
        /// - Remark: An invalid FenceParameters indicates that a GeotriggerMonitor is
        /// unable to perform checks. No GeotriggerNotificationInfo events will be sent.
        /// - Since: 100.12.0
        GeotriggersFenceParametersError = 16002,
        
        /// A problem was encountered with the fence data for a Geotrigger.
        /// 
        /// - Remark: There is a problem with some of the fence data and these will not be checked by a
        /// GeotriggerMonitor. However, other data is valid and so
        /// GeotriggerNotificationInfo events can be sent.
        /// - Since: 100.12.0
        GeotriggersFenceDataWarning = 16003,
        
        /// Device doesn't support accelerometer.
        /// 
        /// - Since: 100.12.0
        MotionSensorAccelerometerNotSupported = 17000,
        
        /// Device doesn't support gyroscope.
        /// 
        /// - Since: 100.12.0
        MotionSensorGyroscopeNotSupported = 17001,
        
        /// Device doesn't support magnetometer.
        /// 
        /// - Since: 100.12.0
        MotionSensorMagnetometerNotSupported = 17002
    };
}