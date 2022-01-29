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
    internal partial class GameEngineViewViewState
    {
        #region Properties
        /// The GameEngineView view error from the GameEngineViewViewState.
        /// 
        /// - SeeAlso: GameEngineViewViewState
        /// - Since: 100.11.0
        internal Exception Error
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_GeoViewViewState_getError(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Convert.FromError(new Standard.Error(localResult));
            }
        }
        
        /// The GameEngineView view status from the GameEngineViewViewState.
        /// 
        /// - SeeAlso: GameEngineViewViewStatus
        /// - Since: 100.11.0
        internal GameEngineViewViewStatus Status
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_GeoViewViewState_getStatus(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal GameEngineViewViewState(IntPtr handle) => Handle = handle;
        
        ~GameEngineViewViewState()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_GeoViewViewState_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_GeoViewViewState_getError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern GameEngineViewViewStatus RT_GeoViewViewState_getStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GeoViewViewState_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}