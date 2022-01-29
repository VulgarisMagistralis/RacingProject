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
    internal struct DestroySceneComponentCommandParameters
    {
        /// The scene component id from the object that will be destroyed.
        /// 
        public uint SceneComponentId;
    }
}