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
    public enum GeometryDimension
    {
        /// The geometry is a point or multipoint.
        /// 
        /// - Since: 100.0.0
        Point = 0,
        
        /// The geometry is a curve.
        /// 
        /// - Since: 100.0.0
        Curve = 1,
        
        /// The geometry has an area.
        /// 
        /// - Since: 100.0.0
        Area = 2,
        
        /// The geometry has a volume.
        /// 
        /// - Since: 100.0.0
        Volume = 3,
        
        /// Unknown geometry dimensions.
        /// 
        /// - Since: 100.0.0
        Unknown = -1
    };
}