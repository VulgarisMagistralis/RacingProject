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
using System;

namespace Esri.GameEngine.RenderCommandQueue
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial class RenderCommandServer
    {
        #region Methods
        /// Get an object with the render command cache since the last time the method was called
        /// 
        /// - Remark: The DataBuffer contains the render commands serialized as a consecutive array of [type_of_command, render_command_parameters].
        /// Since the length of each render command is different, the de-serialization has to be done dynamically, reading the commands one by one
        /// and dynamically advancing the pointer to the DataBuffer.
        /// - Returns: An object with the render command cache since the last time the method was called
        /// - Since: 100.12.0
        internal Unity.DataBuffer<byte> GetRenderCommands()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_RenderCommandServer_getRenderCommands(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Unity.DataBuffer<byte> localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Unity.DataBuffer<byte>(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal RenderCommandServer(IntPtr handle) => Handle = handle;
        
        ~RenderCommandServer()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_RenderCommandServer_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_RenderCommandServer_getRenderCommands(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_RenderCommandServer_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}