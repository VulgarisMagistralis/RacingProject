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

using System.Runtime.InteropServices;

namespace Esri.GameEngine.RenderCommandQueue.Parameters
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SetMeshCommandParameters
    {
        /// The scene component parameter of this render command
        /// 
        public uint SceneComponentId;
        
        /// The triangles parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView Triangles;
        
        /// The positions parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView Positions;
        
        /// The normals parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView Normals;
        
        /// The tangents parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView Tangents;
        
        /// The uvs parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView Uvs;
        
        /// The colors parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView Colors;
        
        /// The ID of the uv region parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.DataBufferView UvRegionIds;
        
        /// The mesh's feature indices
        /// 
        /// - Remark: A zero-based id that is unique for each feature contained in the scene node. 
        /// Used to look up feature ID in the MaterialTextureProperty.featureIds texture.
        public GameEngine.RenderCommandQueue.DataBufferView FeatureIndices;
    }
}