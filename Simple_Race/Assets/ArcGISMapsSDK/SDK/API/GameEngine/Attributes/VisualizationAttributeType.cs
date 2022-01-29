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

namespace Esri.GameEngine.Attributes
{
    public enum VisualizationAttributeType
    {
        /// A signed 32-bit integer value.
        /// 
        /// - Remark: Visualization attributes of this type are passed to the game engine shader in a texture format that preserves precision.
        /// For example: PF_R32_SINT in Unreal Engine, and UnityEngine.TextureFormat.RGBA32 in Unity.
        /// - Since: 100.12.0
        Int32 = 0,
        
        /// A 32-bit float value.
        /// 
        /// - Remark: Visualization attributes of this type are passed to the game engine shader as a float texture.
        /// - Since: 100.12.0
        Float32 = 1
    };
}