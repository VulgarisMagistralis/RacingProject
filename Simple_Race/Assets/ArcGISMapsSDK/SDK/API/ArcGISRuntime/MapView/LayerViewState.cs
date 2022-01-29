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

namespace Esri.ArcGISRuntime.MapView
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class LayerViewState
    {
        #region Properties
        /// The layer view error from the layer view state.
        /// 
        /// - SeeAlso: LayerViewState
        /// - Since: 100.0.0
        public Exception Error
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_LayerViewState_getError(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Convert.FromError(new Standard.Error(localResult));
            }
        }
        
        /// The layer view status from the layer view state.
        /// 
        /// - SeeAlso: LayerViewState
        /// - Since: 100.0.0
        public LayerViewStatus Status
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_LayerViewState_getStatus(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal LayerViewState(IntPtr handle) => Handle = handle;
        
        ~LayerViewState()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_LayerViewState_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_LayerViewState_getError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern LayerViewStatus RT_LayerViewState_getStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_LayerViewState_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}