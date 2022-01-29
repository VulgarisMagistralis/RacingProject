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
    public enum ElementType
    {
        /// An ArcGISFeature object.
        /// 
        /// - Since: 100.0.0
        ArcGISFeature = 0,
        
        /// An ArcGISFeatureServiceInfo object.
        /// 
        /// - Since: 100.0.0
        ArcGISFeatureServiceInfo = 1,
        
        /// Deprecated. For internal use within C-API only.
        /// 
        /// - Since: 100.0.0
        ArcGISMapServiceInfo = 2,
        
        /// An ArcGIS sublayer object.
        /// 
        /// - Since: 100.0.0
        ArcGISSublayer = 3,
        
        /// An array.
        /// 
        /// - Since: 100.0.0
        Array = 4,
        
        /// An attachment value.
        /// 
        /// - Since: 100.0.0
        Attachment = 5,
        
        /// A network analyst attribute parameter value.
        /// 
        /// - Since: 100.0.0
        AttributeParameterValue = 6,
        
        /// An Basemap object
        /// 
        /// - Since: 100.0.0
        Basemap = 7,
        
        /// A bookmark object.
        /// 
        /// - Since: 100.0.0
        Bookmark = 8,
        
        /// A boolean value.
        /// 
        /// - Since: 100.0.0
        Bool = 9,
        
        /// A buffer value.
        /// 
        /// - Since: 100.0.0
        Buffer = 10,
        
        /// A class break object.
        /// 
        /// - Since: 100.0.0
        ClassBreak = 11,
        
        /// An CodedValue object.
        /// 
        /// - Since: 100.0.0
        CodedValue = 12,
        
        /// A class closest facility parameters.
        /// 
        /// - Since: 100.0.0
        ClosestFacilityParameters = 13,
        
        /// A class closest facility result.
        /// 
        /// - Since: 100.0.0
        ClosestFacilityResult = 14,
        
        /// A class closest facility route.
        /// 
        /// - Since: 100.0.0
        ClosestFacilityRoute = 15,
        
        /// A class closest facility task.
        /// 
        /// - Since: 100.0.0
        ClosestFacilityTask = 16,
        
        /// A color object.
        /// 
        /// - Since: 100.0.0
        Color = 17,
        
        /// A network analyst cost attribute.
        /// 
        /// - Since: 100.0.0
        CostAttribute = 18,
        
        /// A date time value.
        /// 
        /// - Since: 100.0.0
        DateTime = 19,
        
        /// Element holds a dictionary.
        /// 
        /// - Since: 100.0.0
        Dictionary = 20,
        
        /// A network analyst direction event.
        /// 
        /// - Since: 100.0.0
        DirectionEvent = 21,
        
        /// A network analyst direction maneuver.
        /// 
        /// - Since: 100.0.0
        DirectionManeuver = 22,
        
        /// A network analyst direction message.
        /// 
        /// - Since: 100.0.0
        DirectionMessage = 23,
        
        /// An DistanceSymbolRange object.
        /// 
        /// - Since: 100.0.0
        DistanceSymbolRange = 24,
        
        /// A domain object.
        /// 
        /// - Since: 100.0.0
        Domain = 25,
        
        /// The result of an edit to an attachment.
        /// 
        /// - Since: 100.0.0
        EditResult = 26,
        
        /// An ElevationSource object.
        /// 
        /// - Since: 100.0.0
        ElevationSource = 27,
        
        /// An EstimateTileCacheSizeResult object.
        /// 
        /// - Since: 100.0.0
        EstimateTileCacheSizeResult = 28,
        
        /// An ExportTileCacheTask object.
        /// 
        /// - Since: 100.0.0
        ExportTileCacheTask = 29,
        
        /// An ExportTileCacheParameters object.
        /// 
        /// - Since: 100.0.0
        ExportTileCacheParameters = 30,
        
        /// An ExtensionLicense object.
        /// 
        /// - Since: 100.0.0
        ExtensionLicense = 31,
        
        /// A closest facility.
        /// 
        /// - Since: 100.0.0
        Facility = 32,
        
        /// A feature object.
        /// 
        /// - Since: 100.0.0
        Feature = 33,
        
        /// A feature collection object.
        /// 
        /// - Since: 100.0.0
        FeatureCollection = 34,
        
        /// A feature collection table object.
        /// 
        /// - Since: 100.0.0
        FeatureCollectionTable = 35,
        
        /// The result of an edit to a feature.
        /// 
        /// - Since: 100.0.0
        FeatureEditResult = 36,
        
        /// A feature query result object.
        /// 
        /// - Since: 100.0.0
        FeatureQueryResult = 37,
        
        /// An FeatureTable object.
        /// 
        /// - Since: 100.0.0
        FeatureTable = 38,
        
        /// A feature template object.
        /// 
        /// - Since: 100.0.0
        FeatureTemplate = 39,
        
        /// A feature type object.
        /// 
        /// - Since: 100.0.0
        FeatureType = 40,
        
        /// A field value.
        /// 
        /// - Since: 100.0.0
        Field = 41,
        
        /// A 32 bit float value.
        /// 
        /// - Since: 100.0.0
        Float32 = 42,
        
        /// A 64 bit float value.
        /// 
        /// - Since: 100.0.0
        Float64 = 43,
        
        /// An GenerateGeodatabaseParameters object.
        /// 
        /// - Since: 100.0.0
        GenerateGeodatabaseParameters = 44,
        
        /// Options for a layer when generating a geodatabase using the sync task.
        /// 
        /// - Since: 100.0.0
        GenerateLayerOption = 45,
        
        /// A result of geocode operation.
        /// 
        /// - Since: 100.0.0
        GeocodeResult = 46,
        
        /// A geodatabase.
        /// 
        /// - Since: 100.0.0
        Geodatabase = 47,
        
        /// A geodatabase feature table.
        /// 
        /// - Since: 100.0.0
        GeodatabaseFeatureTable = 48,
        
        /// An GeodatabaseSyncTask object
        /// 
        /// - Since: 100.0.0
        GeodatabaseSyncTask = 49,
        
        /// A geometry value.
        /// 
        /// - Since: 100.0.0
        Geometry = 50,
        
        /// An GeoprocessingFeatureSet object.
        /// 
        /// - Since: 100.0.0
        GeoprocessingFeatureSet = 51,
        
        /// An GeoprocessingParameter object.
        /// 
        /// - Since: 100.0.0
        GeoprocessingParameter = 52,
        
        /// A graphic object.
        /// 
        /// - Since: 100.0.0
        Graphic = 53,
        
        /// A graphics overlay object.
        /// 
        /// - Since: 100.0.0
        GraphicsOverlay = 54,
        
        /// A GUID value.
        /// 
        /// - Since: 100.0.0
        GUID = 55,
        
        /// An object containing the results of an identify on a graphics overlay.
        /// 
        /// - Since: 100.0.0
        IdentifyGraphicsOverlayResult = 56,
        
        /// An object containing the results of an identify on a layer.
        /// 
        /// - Since: 100.0.0
        IdentifyLayerResult = 57,
        
        /// An IdInfo object.
        /// 
        /// - Since: 100.0.0
        IdInfo = 58,
        
        /// An image object.
        /// 
        /// - Since: 100.0.0
        Image = 59,
        
        /// A closest facility incident.
        /// 
        /// - Since: 100.0.0
        Incident = 60,
        
        /// A 16-bit integer value.
        /// 
        /// - Since: 100.0.0
        Int16 = 61,
        
        /// A 32-bit integer value.
        /// 
        /// - Since: 100.0.0
        Int32 = 62,
        
        /// A 64-bit integer value.
        /// 
        /// - Since: 100.0.0
        Int64 = 63,
        
        /// A 8-bit integer value.
        /// 
        /// - Since: 100.0.0
        Int8 = 64,
        
        /// An Job object.
        /// 
        /// - Since: 100.0.0
        Job = 65,
        
        /// A job message.
        /// 
        /// - Since: 100.0.0
        JobMessage = 66,
        
        /// A KML node object.
        /// 
        /// - Since: 100.2.0
        KMLNode = 67,
        
        /// A KML geometry object.
        /// 
        /// - Since: 100.0.0
        KMLGeometry = 68,
        
        /// A label class object.
        /// 
        /// - Since: 100.0.0
        LabelingInfo = 69,
        
        /// A layer object.
        /// 
        /// - Since: 100.0.0
        Layer = 70,
        
        /// A legend info object.
        /// 
        /// - Since: 100.0.0
        LegendInfo = 71,
        
        /// A tile info level of detail (LOD).
        /// 
        /// - Since: 100.0.0
        LevelOfDetail = 72,
        
        /// An LoadableImage object. For internal use within C-API only.
        /// 
        /// - Since: 100.0.0
        LoadableImage = 73,
        
        /// An LocatorAttribute object.
        /// 
        /// - Since: 100.0.0
        LocatorAttribute = 74,
        
        /// An LocatorTask object.
        /// 
        /// - Since: 100.0.0
        LocatorTask = 75,
        
        /// An Map object.
        /// 
        /// - Since: 100.0.0
        Map = 76,
        
        /// An MobileBasemapLayer object.
        /// 
        /// - Since: 100.0.0
        MobileBasemapLayer = 77,
        
        /// An MobileMapPackage object.
        /// 
        /// - Since: 100.0.0
        MobileMapPackage = 78,
        
        /// An ModelSceneSymbol object.
        /// 
        /// - Since: 100.0.0
        ModelSceneSymbol = 79,
        
        /// An order by enum value.
        /// 
        /// - Since: 100.0.0
        OrderBy = 80,
        
        /// An PictureMarkerSymbol object.
        /// 
        /// - Since: 100.0.0
        PictureMarkerSymbol = 81,
        
        /// A network analyst point barrier.
        /// 
        /// - Since: 100.0.0
        PointBarrier = 82,
        
        /// A network analyst polygon barrier.
        /// 
        /// - Since: 100.0.0
        PolygonBarrier = 83,
        
        /// A network analyst polyline barrier.
        /// 
        /// - Since: 100.0.0
        PolylineBarrier = 84,
        
        /// A Popup object.
        /// 
        /// - Since: 100.0.0
        Popup = 85,
        
        /// A popup field representing how a geo-element's attribute (field) should be displayed in a pop-up.
        /// 
        /// - Since: 100.0.0
        PopupField = 86,
        
        /// A popup media representing the media that is displayed in a pop-up for a geo-element.
        /// 
        /// - Since: 100.0.0
        PopupMedia = 87,
        
        /// A Portal object. For internal use within C-API only.
        /// 
        /// - Since: 100.0.0
        Portal = 88,
        
        /// A PortalItem object. For internal use within C-API only.
        /// 
        /// - Since: 100.0.0
        PortalItem = 89,
        
        /// A Raster object. For internal use within C-API only.
        /// 
        /// - Since: 100.0.0
        Raster = 90,
        
        /// An attachment on a request object.
        /// 
        /// - Since: 100.0.0
        RequestAttachment = 91,
        
        /// A network analyst restriction attribute.
        /// 
        /// - Since: 100.0.0
        RestrictionAttribute = 92,
        
        /// A network analyst route.
        /// 
        /// - Since: 100.0.0
        Route = 93,
        
        /// A network analyst route parameters.
        /// 
        /// - Since: 100.0.0
        RouteParameters = 94,
        
        /// A network analyst route result.
        /// 
        /// - Since: 100.0.0
        RouteResult = 95,
        
        /// An RouteTask object.
        /// 
        /// - Since: 100.0.0
        RouteTask = 96,
        
        /// An Scene object.
        /// 
        /// - Since: 100.0.0
        Scene = 97,
        
        /// A service area facility.
        /// 
        /// - Since: 100.0.0
        ServiceAreaFacility = 98,
        
        /// A service area parameters.
        /// 
        /// - Since: 100.0.0
        ServiceAreaParameters = 99,
        
        /// A service area polygon.
        /// 
        /// - Since: 100.0.0
        ServiceAreaPolygon = 100,
        
        /// A service area polyline.
        /// 
        /// - Since: 100.0.0
        ServiceAreaPolyline = 101,
        
        /// A service area result.
        /// 
        /// - Since: 100.0.0
        ServiceAreaResult = 102,
        
        /// A service area task.
        /// 
        /// - Since: 100.0.0
        ServiceAreaTask = 103,
        
        /// A network analyst stop.
        /// 
        /// - Since: 100.0.0
        Stop = 104,
        
        /// A string value.
        /// 
        /// - Since: 100.0.0
        String = 105,
        
        /// A result of suggest operation.
        /// 
        /// - Since: 100.0.0
        SuggestResult = 106,
        
        /// An Surface object.
        /// 
        /// - Since: 100.0.0
        Surface = 107,
        
        /// A symbol object.
        /// 
        /// - Since: 100.0.0
        Symbol = 108,
        
        /// An SymbolStyle object.
        /// 
        /// - Since: 100.0.0
        SymbolStyle = 109,
        
        /// An SymbolStyleSearchParameters object.
        /// 
        /// - Since: 100.0.0
        SymbolStyleSearchParameters = 110,
        
        /// An SymbolStyleSearchResult object.
        /// 
        /// - Since: 100.0.0
        SymbolStyleSearchResult = 111,
        
        /// An SyncGeodatabaseParameters object.
        /// 
        /// - Since: 100.0.0
        SyncGeodatabaseParameters = 112,
        
        /// An SyncLayerOption object.
        /// 
        /// - Since: 100.0.0
        SyncLayerOption = 113,
        
        /// An SyncLayerResult object.
        /// 
        /// - Since: 100.0.0
        SyncLayerResult = 114,
        
        /// A TileCache object.
        /// 
        /// - Since: 100.0.0
        TileCache = 115,
        
        /// An TransportationNetworkDataset object.
        /// 
        /// - Since: 100.0.0
        TransportationNetworkDataset = 116,
        
        /// A travel mode.
        /// 
        /// - Since: 100.0.0
        TravelMode = 117,
        
        /// An unsigned 16-bit integer value.
        /// 
        /// - Since: 100.0.0
        UInt16 = 118,
        
        /// An unsigned 32-bit integer value.
        /// 
        /// - Since: 100.0.0
        UInt32 = 119,
        
        /// An unsigned 64-bit integer value.
        /// 
        /// - Since: 100.0.0
        UInt64 = 120,
        
        /// An unsigned 8-bit integer value.
        /// 
        /// - Since: 100.0.0
        UInt8 = 121,
        
        /// A unique value object.
        /// 
        /// - Since: 100.0.0
        UniqueValue = 122,
        
        /// An variant type.
        /// 
        /// - Since: 100.0.0
        Variant = 123,
        
        /// Element holds a vector.
        /// 
        /// - Since: 100.0.0
        Vector = 124,
        
        /// An VectorTileSourceInfo object. For internal use within C-API only.
        /// 
        /// - Since: 100.0.0
        VectorTileSourceInfo = 125,
        
        /// An GeoprocessingParameterInfo object.
        /// 
        /// - Since: 100.1.0
        GeoprocessingParameterInfo = 126,
        
        /// An GeoprocessingTask object.
        /// 
        /// - Since: 100.1.0
        GeoprocessingTask = 127,
        
        /// An GeoprocessingParameters object.
        /// 
        /// - Since: 100.1.0
        GeoprocessingParameters = 128,
        
        /// An WMTSLayerInfo object.
        /// 
        /// - Since: 100.1.0
        WMTSLayerInfo = 129,
        
        /// An WMTSServiceInfo object.
        /// 
        /// - Since: 100.1.0
        WMTSServiceInfo = 130,
        
        /// An WMTSTileMatrix object.
        /// 
        /// - Since: 100.1.0
        WMTSTileMatrix = 131,
        
        /// An WMTSTileMatrixSet object.
        /// 
        /// - Since: 100.1.0
        WMTSTileMatrixSet = 132,
        
        /// An TileImageFormat enum value.
        /// 
        /// - Since: 100.1.0
        TileImageFormat = 133,
        
        /// An OfflineMapTask object.
        /// 
        /// - Since: 100.1.0
        OfflineMapTask = 134,
        
        /// An ExportVectorTilesTask object.
        /// 
        /// - Since: 100.1.0
        ExportVectorTilesTask = 135,
        
        /// An ExportVectorTilesParameters object.
        /// 
        /// - Since: 100.1.0
        ExportVectorTilesParameters = 136,
        
        /// An ArcGISFeatureTable object.
        /// 
        /// - Since: 100.1.0
        ArcGISFeatureTable = 137,
        
        /// An RelationshipInfo object.
        /// 
        /// - Since: 100.1.0
        RelationshipInfo = 138,
        
        /// An RelatedFeatureQueryResult object.
        /// 
        /// - Since: 100.1.0
        RelatedFeatureQueryResult = 139,
        
        /// An WMTSService object.
        /// 
        /// - Since: 100.1.0
        WMTSService = 140,
        
        /// A Error object.
        /// 
        /// - Since: 100.1.0
        Error = 141,
        
        /// A ServiceFeatureTable object.
        /// 
        /// - Since: 100.1.0
        ServiceFeatureTable = 142,
        
        /// A GenerateOfflineMapParameters object.
        /// 
        /// - Since: 100.1.0
        GenerateOfflineMapParameters = 143,
        
        /// An PictureFillSymbol object.
        /// 
        /// - Since: 100.1.0
        PictureFillSymbol = 145,
        
        /// A OfflineCapability object.
        /// 
        /// - Since: 100.1.0
        OfflineCapability = 146,
        
        /// A OfflineCapability object.
        /// 
        /// - Since: 100.1.0
        OfflineMapCapabilities = 147,
        
        /// An RenderingRuleInfo object.
        /// 
        /// - Since: 100.1.0
        RenderingRuleInfo = 148,
        
        /// A LabelDefinition object.
        /// 
        /// - Since: 100.1.0
        LabelDefinition = 149,
        
        /// A RelationshipConstraintViolationType value.
        /// 
        /// - Since: 100.1.0
        RelationshipConstraintViolation = 150,
        
        /// A OfflineMapSyncTask object.
        /// 
        /// - Since: 100.1.0
        OfflineMapSyncTask = 151,
        
        /// An OfflineMapSyncLayerResult object.
        /// 
        /// - Since: 100.1.0
        OfflineMapSyncLayerResult = 152,
        
        /// A PopupRelatedFeaturesSortOrder object.
        /// 
        /// - Since: 100.1.0
        PopupRelatedFeaturesSortOrder = 153,
        
        /// An StatisticDefinition object.
        /// 
        /// - Since: 100.2.0
        StatisticDefinition = 154,
        
        /// An StatisticsQueryResult object.
        /// 
        /// - Since: 100.2.0
        StatisticsQueryResult = 155,
        
        /// An StatisticRecord object.
        /// 
        /// - Since: 100.2.0
        StatisticRecord = 156,
        
        /// A KMLDataset object.
        /// 
        /// - Since: 100.2.0
        KMLDataset = 157,
        
        /// A PreplannedMapArea object.
        /// 
        /// - Since: 100.2.0
        PreplannedMapArea = 158,
        
        /// An WMSService object.
        /// 
        /// - Since: 100.2.0
        WMSService = 159,
        
        /// An WMSServiceInfo object.
        /// 
        /// - Since: 100.2.0
        WMSServiceInfo = 160,
        
        /// An WMSLayerInfo object.
        /// 
        /// - Since: 100.2.0
        WMSLayerInfo = 161,
        
        /// An MapServiceImageFormat enum value.
        /// 
        /// - Since: 100.2.0
        MapServiceImageFormat = 162,
        
        /// An SpatialReference object.
        /// 
        /// - Since: 100.2.0
        SpatialReference = 163,
        
        /// A GeoPackage object.
        /// 
        /// - Since: 100.2.0
        Geopackage = 164,
        
        /// A GeoPackageFeatureTable object.
        /// 
        /// - Since: 100.2.0
        GeopackageFeatureTable = 165,
        
        /// A GeoPackageRaster object.
        /// 
        /// - Since: 100.2.0
        GeopackageRaster = 166,
        
        /// An WMSSublayer object.
        /// 
        /// - Since: 100.2.0
        WMSSublayer = 167,
        
        /// A VectorTileCache object.
        /// 
        /// - Since: 100.2.0
        VectorTileCache = 168,
        
        /// A Analysis object.
        /// 
        /// - Since: 100.2.0
        Analysis = 169,
        
        /// A AnalysisOverlay object.
        /// 
        /// - Since: 100.2.0
        AnalysisOverlay = 170,
        
        /// An ItemResourceCache object.
        /// 
        /// - Since: 100.2.0
        ItemResourceCache = 171,
        
        /// An WMSFeature object.
        /// 
        /// - Since: 100.2.0
        WMSFeature = 172,
        
        /// A NMEASatelliteInfo object.
        /// 
        /// - Since: 100.2.1
        NMEASatelliteInfo = 173,
        
        /// A symbol layer object.
        /// 
        /// - Since: 100.5.0
        SymbolLayer = 174,
        
        /// A vector marker symbol element object.
        /// 
        /// - Since: 100.5.0
        VectorMarkerSymbolElement = 175,
        
        /// A geometric effect object.
        /// 
        /// - Since: 100.5.0
        GeometricEffect = 176,
        
        /// A picture marker symbol layer object.
        /// 
        /// - Since: 100.5.0
        PictureMarkerSymbolLayer = 177,
        
        /// A picture fill symbol layer object.
        /// 
        /// - Since: 100.5.0
        PictureFillSymbolLayer = 178,
        
        /// A feature subtype object.
        /// 
        /// - Since: 100.3.0
        FeatureSubtype = 179,
        
        /// A LabelStackSeparator object.
        /// 
        /// - Since: 100.11.0
        LabelStackSeparator = 180,
        
        /// An WFSService object.
        /// 
        /// - Since: 100.6.0
        WFSService = 181,
        
        /// An WFSLayerInfo object.
        /// 
        /// - Since: 100.5.0
        WFSLayerInfo = 182,
        
        /// A OfflineMapParametersKey object.
        /// 
        /// - Since: 100.4.0
        OfflineMapParametersKey = 183,
        
        /// A GenerateOfflineMapParameterOverrides object.
        /// 
        /// - Since: 100.4.0
        GenerateOfflineMapParameterOverrides = 184,
        
        /// An QueryParameters object.
        /// 
        /// - Since: 100.5.0
        QueryParameters = 186,
        
        /// An StatisticsQueryParameters object.
        /// 
        /// - Since: 100.5.0
        StatisticsQueryParameters = 187,
        
        /// A RouteTracker object.
        /// 
        /// - Since: 100.6.0
        RouteTracker = 188,
        
        /// A WFSFeatureTable object.
        /// 
        /// - Since: 100.5.0
        WFSFeatureTable = 189,
        
        /// A DownloadPreplannedOfflineMapParameters object.
        /// 
        /// - Since: 100.5.0
        DownloadPreplannedOfflineMapParameters = 190,
        
        /// A AnnotationSublayer object.
        /// 
        /// - Since: 100.6.0
        AnnotationSublayer = 192,
        
        /// A OfflineMapSyncParameters object.
        /// 
        /// - Since: 100.6.0
        OfflineMapSyncParameters = 193,
        
        /// A OfflineMapUpdatesInfo object.
        /// 
        /// - Since: 100.6.0
        OfflineMapUpdatesInfo = 194,
        
        /// A DictionarySymbolStyleConfiguration object.
        /// 
        /// - Since: 100.6.0
        DictionarySymbolStyleConfiguration = 195,
        
        /// A Location object.
        /// 
        /// - Since: 100.8.0
        Location = 197,
        
        /// A ImageOverlay object.
        /// 
        /// - Since: 100.8.0
        ImageFrame = 198,
        
        /// A ImageOverlay object.
        /// 
        /// - Since: 100.8.0
        ImageOverlay = 199,
        
        /// A GeodatabaseDeltaInfo.
        /// 
        /// - Since: 100.10.0
        GeodatabaseDeltaInfo = 200,
        
        /// An OGCFeatureService object.
        /// 
        /// - Since: 100.9.0
        OGCFeatureService = 300,
        
        /// An OGCFeatureCollectionInfo object.
        /// 
        /// - Since: 100.9.0
        OGCFeatureCollectionInfo = 301,
        
        /// A OGCFeatureCollectionTable object.
        /// 
        /// - Since: 100.9.0
        OGCFeatureCollectionTable = 302,
        
        /// A DatumTransformation object.
        /// 
        /// - Since: 100.2.0
        DatumTransformation = 500,
        
        /// A GeographicTransformationStep object.
        /// 
        /// - Since: 100.2.0
        GeographicTransformationStep = 501,
        
        /// A HorizontalVerticalTransformationStep object.
        /// 
        /// - Since: 100.9.0
        HorizontalVerticalTransformationStep = 502,
        
        /// A ENCCell object.
        /// 
        /// - Since: 100.2.0
        ENCCell = 1000,
        
        /// A ENCDataset object.
        /// 
        /// - Since: 100.2.0
        ENCDataset = 1001,
        
        /// A ENCExchangeSet object.
        /// 
        /// - Since: 100.2.0
        ENCExchangeSet = 1002,
        
        /// A ENCFeature object.
        /// 
        /// - Since: 100.1.0
        ENCFeature = 1003,
        
        /// A MobileScenePackage object.
        /// 
        /// - Since: 100.5.0
        MobileScenePackage = 1004,
        
        /// A popup expression defining an arcade expression on a popup.
        /// 
        /// - Since: 100.3.0
        PopupExpression = 1005,
        
        /// A UtilityNetwork object.
        /// 
        /// - Since: 100.6.0
        UtilityNetwork = 1006,
        
        /// A UtilityAssetType object.
        /// 
        /// - Since: 100.6.0
        UtilityAssetType = 1007,
        
        /// A UtilityAssetGroup object.
        /// 
        /// - Since: 100.6.0
        UtilityAssetGroup = 1008,
        
        /// A UtilityCategory object.
        /// 
        /// - Since: 100.6.0
        UtilityCategory = 1009,
        
        /// A UtilityTerminal object.
        /// 
        /// - Since: 100.6.0
        UtilityTerminal = 1010,
        
        /// A UtilityNetworkAttribute object.
        /// 
        /// - Since: 100.6.0
        UtilityNetworkAttribute = 1011,
        
        /// A UtilityNetworkSource object.
        /// 
        /// - Since: 100.6.0
        UtilityNetworkSource = 1012,
        
        /// A UtilityElement object.
        /// 
        /// - Since: 100.6.0
        UtilityElement = 1013,
        
        /// A UtilityTraceResultType value.
        /// 
        /// - Since: 100.6.0
        UtilityTraceResultType = 1014,
        
        /// A TrackingStatus object.
        /// 
        /// - Since: 100.6.0
        TrackingStatus = 1015,
        
        /// A UtilityTraceResult value.
        /// 
        /// - Since: 100.6.0
        UtilityTraceResult = 1016,
        
        /// A UtilityDomainNetwork object.
        /// 
        /// - Since: 100.7.0
        UtilityDomainNetwork = 1017,
        
        /// A UtilityTerminalConfiguration object.
        /// 
        /// - Since: 100.7.0
        UtilityTerminalConfiguration = 1018,
        
        /// A UtilityTier object.
        /// 
        /// - Since: 100.7.0
        UtilityTier = 1019,
        
        /// A UtilityTierGroup object.
        /// 
        /// - Since: 100.7.0
        UtilityTierGroup = 1020,
        
        /// A UtilityPropagator object.
        /// 
        /// - Since: 100.7.0
        UtilityPropagator = 1021,
        
        /// A SubtypeSublayer object.
        /// 
        /// - Since: 100.7.0
        SubtypeSublayer = 1022,
        
        /// A UtilityAssociation object.
        /// 
        /// - Since: 100.7.0
        UtilityAssociation = 1023,
        
        /// A UtilityTraceFunctionBarrier object.
        /// 
        /// - Since: 100.7.0
        UtilityTraceFunctionBarrier = 1024,
        
        /// A LicenseInfo object.
        /// 
        /// - Since: 100.7.0
        LicenseInfo = 1025,
        
        /// A RasterCell object.
        /// 
        /// - Since: 100.8.0
        RasterCell = 1026,
        
        /// A UtilityTerminalPath object.
        /// 
        /// - Since: 100.8.0
        UtilityTerminalPath = 1027,
        
        /// A UtilityTerminalConfigurationPath object.
        /// 
        /// - Since: 100.8.0
        UtilityTerminalConfigurationPath = 1028,
        
        /// The element is currently not holding any value.
        /// 
        /// - Since: 100.0.0
        None = -1,
        
        /// An object that represents the result of an attempt to evaluate a popup expression.
        /// 
        /// - Since: 100.8.0
        PopupExpressionEvaluation = 1029,
        
        /// A UtilityTraceFunction object.
        /// 
        /// - Since: 100.9.0
        UtilityTraceFunction = 1030,
        
        /// A UtilityFunctionTraceResult object.
        /// 
        /// - Since: 100.9.0
        UtilityFunctionTraceResult = 1031,
        
        /// A UtilityTraceFunctionOutput object.
        /// 
        /// - Since: 100.9.0
        UtilityTraceFunctionOutput = 1032,
        
        /// A ServiceVersionInfo object.
        /// 
        /// - Since: 100.9.0
        ServiceVersionInfo = 1033,
        
        /// A UtilityGeometryTraceResult object.
        /// 
        /// - Since: 100.9.0
        UtilityGeometryTraceResult = 1034,
        
        /// A FeatureTableEditResult object.
        /// 
        /// - Since: 100.9.0
        FeatureTableEditResult = 1035,
        
        /// A ArcGISAuthenticationConfiguration object.
        /// 
        /// - Since: 100.11.0
        ArcGISAuthenticationConfiguration = 1036,
        
        /// A UtilityNamedTraceConfiguration object.
        /// 
        /// - Since: 100.11.0
        UtilityNamedTraceConfiguration = 1037,
        
        /// A WifiReading object.
        /// 
        /// - Since: 100.12.0
        WifiReading = 1038,
        
        /// A LocalFeatureEdit object.
        /// 
        /// - Since: 100.12.0
        LocalFeatureEdit = 1039,
        
        /// A Iterator object.
        /// 
        /// - Since: 100.12.0
        Iterator = 1040,
        
        /// A FloorFacility object.
        /// 
        /// - Since: 100.12.0
        FloorFacility = 1041,
        
        /// A FloorLevel object.
        /// 
        /// - Since: 100.12.0
        FloorLevel = 1042,
        
        /// A FloorSite object.
        /// 
        /// - Since: 100.12.0
        FloorSite = 1043,
        
        /// A VisualizationAttributeDescription object.
        /// 
        /// - Since: 100.12.0
        VisualizationAttributeDescription = 1044,
        
        /// A VisualizationAttribute object.
        /// 
        /// - Since: 100.12.0
        VisualizationAttribute = 1045,
        
        /// A Attribute object.
        /// 
        /// - Since: 100.12.0
        Attribute = 1046,
        
        /// A LocalFeatureEditsResult object.
        /// 
        /// - Since: 100.12.0
        LocalFeatureEditsResult = 1047
    };
}