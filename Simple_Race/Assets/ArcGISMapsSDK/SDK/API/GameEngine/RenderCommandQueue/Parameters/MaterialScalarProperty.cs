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

namespace Esri.GameEngine.RenderCommandQueue.Parameters
{
    public enum MaterialScalarProperty
    {
        /// Clipping mode
        /// 
        /// - Since: 100.12.0
        ClippingMode = 0,
        
        /// Use uv region lut
        /// 
        /// - Since: 100.12.0
        UseUvRegionLut = 1,
        
        /// Blend factor
        /// 
        /// - Since: 100.12.0
        BlendFactor = 2,
        
        /// Positions blend factor
        /// 
        /// - Since: 100.12.0
        PositionsBlendFactor = 3,
        
        /// Opacity factor
        /// 
        /// - Since: 100.12.0
        Opacity = 4
    };
}