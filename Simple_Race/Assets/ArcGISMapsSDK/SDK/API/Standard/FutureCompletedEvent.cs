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

namespace Esri.Standard
{
    public delegate void FutureCompletedEvent();
    
    internal delegate void FutureCompletedEventInternal(IntPtr userData);
    
    internal class FutureCompletedEventHandler : EventHandler<FutureCompletedEvent>
    {
        [MonoPInvokeCallback(typeof(FutureCompletedEventInternal))]
        internal static void HandlerFunction(IntPtr userData)
        {
            if (userData == IntPtr.Zero)
            {
                return;
            }
    
            var callbackObject = (FutureCompletedEventHandler)((GCHandle)userData).Target;
    
            var callback = callbackObject.m_delegate;
    
            if (callback == null)
            {
                return;
            }
    
            callback();
        }
    }
}