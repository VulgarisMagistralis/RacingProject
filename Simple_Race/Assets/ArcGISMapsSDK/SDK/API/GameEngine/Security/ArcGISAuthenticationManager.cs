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
    public partial class ArcGISAuthenticationManager
    {
        #region Properties
        /// The authentication configurations for urls.
        /// 
        /// - Since: 100.11.0
        public static Unity.Dictionary<string, ArcGISAuthenticationConfiguration> AuthenticationConfigurations
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISAuthenticationManager_getAuthenticationConfigurations(errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                Unity.Dictionary<string, ArcGISAuthenticationConfiguration> localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Unity.Dictionary<string, ArcGISAuthenticationConfiguration>(localResult);
                }
                
                return localLocalResult;
            }
        }
        #endregion // Properties
        
        #region Events
        /// Sets the global callback invoked when an authentication challenge is issued
        /// 
        /// - Since: 100.11.0
        public static ArcGISAuthenticationChallengeIssuedEvent AuthenticationChallengeIssued
        {
            get
            {
                return _authenticationChallengeIssuedHandler.Delegate;
            }
            set
            {
                _authenticationChallengeIssuedHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_authenticationChallengeIssuedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISAuthenticationManager_setAuthenticationChallengeIssuedCallback(ArcGISAuthenticationChallengeIssuedEventHandler.HandlerFunction, _authenticationChallengeIssuedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISAuthenticationManager_setAuthenticationChallengeIssuedCallback(null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal static ArcGISAuthenticationChallengeIssuedEventHandler _authenticationChallengeIssuedHandler = new ArcGISAuthenticationChallengeIssuedEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISAuthenticationManager_getAuthenticationConfigurations(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISAuthenticationManager_setAuthenticationChallengeIssuedCallback(ArcGISAuthenticationChallengeIssuedEventInternal authenticationChallengeIssued, IntPtr userData, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}