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
    public partial class ArcGISOAuthAuthenticationConfiguration :
        ArcGISAuthenticationConfiguration
    {
        #region Constructors
        /// Creates a authentication information object for OAuth 2
        /// 
        /// - Parameters:
        ///   - clientId: The client identifier.
        ///   - clientSecret: The client secret. Mandatory for App Login.
        ///   - redirectURI: The redirect URI. Mandatory for Named User Login.
        /// - Since: 100.11.0
        public ArcGISOAuthAuthenticationConfiguration(string clientId, string clientSecret, string redirectURI) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISOAuthAuthenticationConfiguration_create(clientId, clientSecret, redirectURI, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The current client id
        /// 
        /// - Since: 100.11.0
        public string ClientId
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISOAuthAuthenticationConfiguration_getClientId(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// The current client secret
        /// 
        /// - Since: 100.11.0
        public string ClientSecret
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISOAuthAuthenticationConfiguration_getClientSecret(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// The current redirect uri
        /// 
        /// - Since: 100.11.0
        public string RedirectURI
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISOAuthAuthenticationConfiguration_getRedirectURI(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISOAuthAuthenticationConfiguration(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISOAuthAuthenticationConfiguration_create([MarshalAs(UnmanagedType.LPStr)]string clientId, [MarshalAs(UnmanagedType.LPStr)]string clientSecret, [MarshalAs(UnmanagedType.LPStr)]string redirectURI, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISOAuthAuthenticationConfiguration_getClientId(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISOAuthAuthenticationConfiguration_getClientSecret(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISOAuthAuthenticationConfiguration_getRedirectURI(IntPtr handle, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}