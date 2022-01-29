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
    internal struct ComposeCommandParameters
    {
        /// The source 1 parameter of this render command
        /// 
        public uint SourceId1;
        
        /// The source 2 parameter of this render command
        /// 
        public uint SourceId2;
        
        /// The alpha parameter of this render command
        /// 
        public float Alpha;
        
        /// Id of the render target to be used as the output of the compose.
        /// 
        public uint TargetId;
        
        /// The region parameter of this render command
        /// 
        public GameEngine.RenderCommandQueue.Vector4 Region;
    }
}