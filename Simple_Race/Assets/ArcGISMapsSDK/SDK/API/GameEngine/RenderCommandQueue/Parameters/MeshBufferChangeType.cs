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
    public enum MeshBufferChangeType
    {
        /// Vertex positions
        /// 
        /// - Since: 100.12.0
        Positions = 0,
        
        /// Vertex normals
        /// 
        /// - Since: 100.12.0
        Normals = 1,
        
        /// Vertex tangents
        /// 
        /// - Since: 100.12.0
        Tangents = 2,
        
        /// Vertex colors
        /// 
        /// - Since: 100.12.0
        Colors = 3,
        
        /// Vertex uvs, channel 0
        /// 
        /// - Since: 100.12.0
        Uv0 = 4,
        
        /// Vertex uvs, channel 1
        /// 
        /// - Since: 100.12.0
        Uv1 = 5,
        
        /// Vertex uvs, channel 2
        /// 
        /// - Since: 100.12.0
        Uv2 = 6,
        
        /// Vertex uvs, channel 3
        /// 
        /// - Since: 100.12.0
        Uv3 = 7
    };
}