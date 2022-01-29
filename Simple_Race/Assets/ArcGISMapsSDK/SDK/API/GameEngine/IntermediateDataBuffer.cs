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

namespace Esri.GameEngine
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial class IntermediateDataBuffer<T>
    {
        #region Properties
        /// A pointer to the beginning of the block of memory.
        /// 
        /// - Since: 100.7.0
        internal IntPtr Data
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_DataBuffer_getData(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The size of one item, in bytes
        /// 
        /// - Remark: Possibly not needed
        /// - Since: 100.7.0
        internal ulong ItemSize
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_DataBuffer_getItemSize(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        
        /// The size of the block of memory, in bytes
        /// 
        /// - Since: 100.7.0
        internal ulong SizeBytes
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_DataBuffer_getSizeBytes(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal IntermediateDataBuffer(IntPtr handle) => Handle = handle;
        
        ~IntermediateDataBuffer()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_DataBuffer_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_DataBuffer_getData(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_DataBuffer_getItemSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_DataBuffer_getSizeBytes(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_DataBuffer_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}