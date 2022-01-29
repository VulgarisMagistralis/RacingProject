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
    public enum AttributeType
    {
        /// A string
        /// 
        /// - Remark: Attributes of this type are not passed to the game engine shader.
        /// - Since: 100.12.0
        String = 1,
        
        /// A signed 8-bit integer value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// - Since: 100.12.0
        Int8 = 2,
        
        /// A unsigned 8-bit integer value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// - Since: 100.12.0
        Uint8 = 3,
        
        /// A signed 16-bit integer value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// - Since: 100.12.0
        Int16 = 4,
        
        /// A unsigned 16-bit integer value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// - Since: 100.12.0
        Uint16 = 5,
        
        /// A signed 32-bit integer value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture. 
        /// Note that this conversion may result in a loss of precision.
        /// (The attribute can be accessed at full precision by attaching an AttributeProcessor to the layer).
        /// - Since: 100.12.0
        Int32 = 6,
        
        /// A unsigned 32-bit integer value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// Note that this conversion may result in a loss of precision.
        /// (The attribute can be accessed at full precision by attaching an AttributeProcessor to the layer).
        /// - Since: 100.12.0
        Uint32 = 7,
        
        /// A float value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// - Since: 100.12.0
        Float32 = 8,
        
        /// A double value.
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader as a float texture.
        /// Note that this conversion may result in a loss of precision.
        /// (The attribute can be accessed at full precision by attaching an AttributeProcessor to the layer).
        /// - Since: 100.12.0
        Float64 = 9,
        
        /// An unsigned 32-bit integer Object ID (OID)
        /// 
        /// - Remark: Attributes of this type are passed to the game engine shader in a texture format that preserves precision.
        /// For example: PF_R32_SINT in Unreal Engine, and UnityEngine.TextureFormat.RGBA32 in Unity.
        /// - Since: 100.12.0
        OID32 = 10
    };
}