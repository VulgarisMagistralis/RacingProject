// COPYRIGHT 1995-2020 ESRI
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
using System.Numerics;

namespace Esri
{
    internal struct ErrorInteropHelper
    {
        internal IntPtr Error;
    }
    
    internal struct ErrorHandler
    {
        /// The function pointer which will be called when an error occurs. ArcGISRuntimeEnvironmentErrorEvent
        internal ArcGISRuntime.ArcGISRuntimeEnvironmentErrorEventInternal Handler;
    
        /// This is a pointer to the error
        internal IntPtr UserData;
    }
    
    internal class ErrorManager
    {
        #region Methods
        public static IntPtr CreateHandler()
        {
            ErrorInteropHelper errorInteropHelper;
    
            errorInteropHelper.Error = IntPtr.Zero;

            IntPtr errorInteropHelperPtr = GCHandle.ToIntPtr(GCHandle.Alloc(errorInteropHelper, GCHandleType.Pinned));
    
            ErrorHandler errorHandler;
    
            errorHandler.Handler = HandlerFunction;
            errorHandler.UserData = errorInteropHelperPtr;
    
            IntPtr errorHandlerPtr = Marshal.AllocHGlobal(Marshal.SizeOf(errorHandler));
    
            Marshal.StructureToPtr(errorHandler, errorHandlerPtr, true);
    
            return errorHandlerPtr;
        }
    
        [MonoPInvokeCallback(typeof(ArcGISRuntime.ArcGISRuntimeEnvironmentErrorEventInternal))]
        internal static void HandlerFunction(IntPtr userData, IntPtr error)
        {
            if (error == IntPtr.Zero)
            {
                return;
            }
    
            GCHandle errorInteropHelperHandle = GCHandle.FromIntPtr(userData);
    
            ErrorInteropHelper errorInteropHelper = (ErrorInteropHelper)errorInteropHelperHandle.Target;

            errorInteropHelper.Error = error;

            errorInteropHelperHandle.Target = errorInteropHelper;
        }
    
        public static void CheckError(IntPtr errorHandlerPtr)
        {
            ErrorHandler errorHandler = Marshal.PtrToStructure<ErrorHandler>(errorHandlerPtr);
    
            GCHandle errorInteropHelperHandle = GCHandle.FromIntPtr(errorHandler.UserData);
    
            ErrorInteropHelper errorInteropHelper = (ErrorInteropHelper)errorInteropHelperHandle.Target;

            var error = errorInteropHelper.Error;

            errorInteropHelperHandle.Free();

            Marshal.FreeHGlobal(errorHandlerPtr);

            if (error != IntPtr.Zero)
            {
                throw Convert.FromError(new Standard.Error(error));
            }
        }
        #endregion // Methods
    }
}
