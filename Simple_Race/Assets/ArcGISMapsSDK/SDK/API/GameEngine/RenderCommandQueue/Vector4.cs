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

namespace Esri.GameEngine.RenderCommandQueue
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Vector4
    {
        /// The x parameter
        /// 
        public float X;
        
        /// The y parameter
        /// 
        public float Y;
        
        /// The z parameter
        /// 
        public float Z;
        
        /// The w parameter
        /// 
        public float W;
    }
}