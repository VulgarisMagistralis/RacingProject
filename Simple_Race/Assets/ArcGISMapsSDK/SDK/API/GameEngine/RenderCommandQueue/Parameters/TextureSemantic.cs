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
    public enum TextureSemantic
    {
        /// i3sMeshPyramidImagery. An i3s imagery texture
        /// 
        /// - Since: 100.7.0
        MeshPyramidImagery = 5,
        
        /// An i3s UVRegions texture.
        /// 
        /// - Since: 100.7.0
        MeshPyramidUvRegions = 6,
        
        /// An i3s Features texture.
        /// 
        /// - Since: 100.12.0
        MeshPyramidFeatureIds = 7,
        
        /// An i3s attribute value texture.
        /// 
        /// - Since: 100.12.0
        MeshPyramidAttributeValues = 8
    };
}