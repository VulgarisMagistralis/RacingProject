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
    internal struct CreateTextureCommandParameters
    {
        /// The id that will be use for the created texture.
        /// 
        public uint TextureId;
        
        /// The width parameter of the texture
        /// 
        public uint Width;
        
        /// The height parameter of the texture
        /// 
        public uint Height;
        
        /// The format parameter of the texture
        /// 
        public TextureFormat TextureFormat;
        
        /// Indicate whether color data is stored in the sRGB color space or not
        /// 
        [MarshalAs(UnmanagedType.I1)]
        public bool IsSRGB;
    }
}