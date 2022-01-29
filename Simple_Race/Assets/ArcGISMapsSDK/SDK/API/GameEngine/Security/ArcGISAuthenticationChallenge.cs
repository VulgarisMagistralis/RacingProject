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

namespace Esri.GameEngine.Security
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISAuthenticationChallenge
    {
        #region Properties
        /// The current authentication challenge type
        /// 
        /// - Since: 100.11.0
        internal ArcGISAuthenticationChallengeType ObjectType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISAuthenticationChallenge_getObjectType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Cancels the authentication challenge
        /// 
        /// - Since: 100.11.0
        public void Cancel()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISAuthenticationChallenge_cancel(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISAuthenticationChallenge(IntPtr handle) => Handle = handle;
        
        ~ArcGISAuthenticationChallenge()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISAuthenticationChallenge_destroy(Handle, errorHandler);
                
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
        internal static extern ArcGISAuthenticationChallengeType RT_ArcGISAuthenticationChallenge_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISAuthenticationChallenge_cancel(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISAuthenticationChallenge_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}