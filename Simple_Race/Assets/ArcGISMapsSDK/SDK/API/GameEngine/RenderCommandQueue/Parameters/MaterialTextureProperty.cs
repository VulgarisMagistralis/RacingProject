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
    public enum MaterialTextureProperty
    {
        /// Base map
        /// 
        /// - Since: 100.12.0
        BaseMap = 0,
        
        /// Uv region lut
        /// 
        /// - Since: 100.12.0
        UvRegionLut = 1,
        
        /// Positions map 0
        /// 
        /// - Since: 100.12.0
        PositionsMap0 = 2,
        
        /// Positions map 1
        /// 
        /// - Since: 100.12.0
        PositionsMap1 = 3,
        
        /// Feature IDs.
        /// 
        /// - Remark: Present on scene node meshes with feature data.
        /// The feature id for a given feature index (see SetMeshCommandParameters.featureIndices) is stored at:
        /// x = feature_index % (tex_width / 2)
        /// y = floor(feature_index / (tex_width / 2))
        /// - Since: 100.12.0
        FeatureIds = 4
    };
}