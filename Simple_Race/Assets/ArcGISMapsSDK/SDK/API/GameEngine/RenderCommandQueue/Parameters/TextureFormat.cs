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
    public enum TextureFormat
    {
        /// Each texture cel consists of one float value (e.g. an elevation in meters)
        /// 
        /// - Since: 100.7.0
        R32Float = 2,
        
        /// Each texture cel consists of 4 unsigned bytes (0 to 255) TODO verify order
        /// 
        /// - Since: 100.7.0
        RGBA8UNorm = 3,
        
        /// Each texture cel consists of 3 unsigned bytes (0 to 255) TODO verify order
        /// 
        /// - Since: 100.7.0
        RGB8UNorm = 4,
        
        /// Each texture cel consists of 4 float values
        /// 
        /// - Since: 100.11.0
        RGBA32Float = 5,
        
        /// DXT compressed texture format, v1
        /// 
        /// - Since: 100.7.0
        DXT1 = 6,
        
        /// DXT compressed texture format, v3
        /// 
        /// - Since: 100.7.0
        DXT3 = 7,
        
        /// DXT compressed texture format, v5
        /// 
        /// - Since: 100.7.0
        DXT5 = 8,
        
        /// Each texture cel consists of 1 uint32 value
        /// 
        /// - Since: 100.7.0
        R32UNorm = 9,
        
        /// Each texture cel consists of 1 int32 value
        /// 
        /// - Since: 100.7.0
        R32Norm = 10,
        
        /// Each texture cel consists of 4 uint16 values
        /// 
        /// - Since: 100.7.0
        RGBA16UNorm = 11,
        
        /// ETC2 compressed texture format
        /// 
        /// - Since: 100.12.0
        ETC2RGB8 = 12,
        
        /// ETC2 compressed texture format
        /// 
        /// - Since: 100.12.0
        ETC2SRGB8 = 13,
        
        /// ETC2 compressed texture format
        /// 
        /// - Since: 100.12.0
        ETC2RGB8PunchthroughAlpha1 = 14,
        
        /// ETC2 compressed texture format
        /// 
        /// - Since: 100.12.0
        ETC2SRGB8PunchthroughAlpha1 = 15,
        
        /// ETC2 compressed texture format
        /// 
        /// - Since: 100.12.0
        ETC2EacRGBA8 = 16,
        
        /// ETC2 compressed texture format
        /// 
        /// - Since: 100.12.0
        ETC2EacSRGB8 = 17
    };
}