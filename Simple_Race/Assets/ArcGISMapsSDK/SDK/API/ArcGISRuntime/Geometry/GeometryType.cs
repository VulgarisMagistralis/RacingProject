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

namespace Esri.ArcGISRuntime.Geometry
{
    public enum GeometryType
    {
        /// Point geometry.
        /// 
        /// - Since: 100.0.0
        Point = 1,
        
        /// Envelope geometry.
        /// 
        /// - Since: 100.0.0
        Envelope = 2,
        
        /// Polyline geometry.
        /// 
        /// - Since: 100.0.0
        Polyline = 3,
        
        /// Polygon geometry.
        /// 
        /// - Since: 100.0.0
        Polygon = 4,
        
        /// Multipoint geometry.
        /// 
        /// - Since: 100.0.0
        Multipoint = 5,
        
        /// An unknown geometry type.
        /// 
        /// - Since: 100.0.0
        Unknown = -1
    };
}