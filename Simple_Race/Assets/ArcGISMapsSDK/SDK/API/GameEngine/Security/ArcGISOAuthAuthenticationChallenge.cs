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
    public partial class ArcGISOAuthAuthenticationChallenge :
        ArcGISAuthenticationChallenge
    {
        #region Properties
        /// The current authorization endpoint uri
        /// 
        /// - Since: 100.11.0
        public string AuthorizeURI
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISOAuthAuthenticationChallenge_getAuthorizeURI(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Respond to the challenge with a token
        /// 
        /// - Since: 100.11.0
        public void Respond(string token)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISOAuthAuthenticationChallenge_respond(Handle, token, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISOAuthAuthenticationChallenge(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISOAuthAuthenticationChallenge_getAuthorizeURI(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISOAuthAuthenticationChallenge_respond(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string token, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}