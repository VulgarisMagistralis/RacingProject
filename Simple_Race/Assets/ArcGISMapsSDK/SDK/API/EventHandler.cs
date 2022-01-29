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

namespace Esri
{
    internal class EventHandler<T>
    {
        protected T m_delegate;
    
        internal T Delegate
        {
            get
            {
                return m_delegate;
            }
            set
            {
                m_delegate = value;
    
                if (gcHandle == null || !gcHandle.IsAllocated)
                {
                    gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
                }
            }
        }
    
        private GCHandle gcHandle;
    
        internal IntPtr UserData
        {
            get
            {
                return (IntPtr)gcHandle;
            }
        }
    
        ~EventHandler()
        {
            if (gcHandle != null && gcHandle.IsAllocated)
            {
                gcHandle.Free();
            }
        }
    }
}