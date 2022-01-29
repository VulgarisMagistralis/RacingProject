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

namespace Esri.ArcGISRuntime
{
    public delegate void ArcGISRuntimeEnvironmentErrorEvent(Exception error);
    
    internal delegate void ArcGISRuntimeEnvironmentErrorEventInternal(IntPtr userData, IntPtr error);
    
    internal class ArcGISRuntimeEnvironmentErrorEventHandler : EventHandler<ArcGISRuntimeEnvironmentErrorEvent>
    {
        [MonoPInvokeCallback(typeof(ArcGISRuntimeEnvironmentErrorEventInternal))]
        internal static void HandlerFunction(IntPtr userData, IntPtr error)
        {
            if (userData == IntPtr.Zero)
            {
                return;
            }
    
            var callbackObject = (ArcGISRuntimeEnvironmentErrorEventHandler)((GCHandle)userData).Target;
    
            var callback = callbackObject.m_delegate;
    
            if (callback == null)
            {
                return;
            }
    
            callback(Convert.FromError(new Standard.Error(error)));
        }
    }
}