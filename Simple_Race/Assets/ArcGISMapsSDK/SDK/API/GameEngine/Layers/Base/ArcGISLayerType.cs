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

namespace Esri.GameEngine.Layers.Base
{
    public enum ArcGISLayerType
    {
        /// An imagery layer
        /// 
        /// - Since: 100.10.0
        ArcGISImageLayer = 0,
        
        /// A 3D model layer
        /// 
        /// - Since: 100.10.0
        ArcGIS3DModelLayer = 1,
        
        /// An integrated mesh layer
        /// 
        /// - Since: 100.10.0
        ArcGISIntegratedMeshLayer = 2,
        
        /// An unsupported layer
        /// 
        /// - Since: 100.10.0
        ArcGISUnsupportedLayer = 4,
        
        /// An unknown layer
        /// 
        /// - Since: 100.10.0
        ArcGISUnknownLayer = 5
    };
}