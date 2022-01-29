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
    public partial class ArcGISAuthenticationManager
    {
        #region Events
        /// Sets the global callback invoked when an authentication challenge is issued
        ///
        /// - Since: 100.11.0
        public static ArcGISOAuthAuthenticationChallengeIssuedEvent OAuthAuthenticationChallengeIssued
        {
            get
            {
                return _oauthAuthenticationChallengeIssuedHandler.Delegate;
            }
            set
            {
                _oauthAuthenticationChallengeIssuedHandler.Delegate = value;

                var errorHandler = ErrorManager.CreateHandler();

                if (_oauthAuthenticationChallengeIssuedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISAuthenticationManager_setAuthenticationChallengeIssuedCallback(ArcGISOAuthAuthenticationChallengeIssuedEventHandler.HandlerFunction, _oauthAuthenticationChallengeIssuedHandler.UserData, errorHandler);
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
        internal static ArcGISOAuthAuthenticationChallengeIssuedEventHandler _oauthAuthenticationChallengeIssuedHandler = new ArcGISOAuthAuthenticationChallengeIssuedEventHandler();
        #endregion // Internal Members
    }
}
