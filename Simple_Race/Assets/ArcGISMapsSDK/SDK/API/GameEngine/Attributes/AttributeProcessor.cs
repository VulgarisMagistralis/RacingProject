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

namespace Esri.GameEngine.Attributes
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class AttributeProcessor
    {
        #region Constructors
        /// Creates an attribute processor object.
        /// 
        /// - Since: 100.12.0
        public AttributeProcessor()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_AttributeProcessor_create(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Events
        /// Set an AttributeProcessorEvent which is invoked when the requested attributes are available to
        ///  be processed.
        /// 
        /// - Since: 100.12.0
        public AttributeProcessorEvent ProcessEvent
        {
            get
            {
                return _processEventHandler.Delegate;
            }
            set
            {
                _processEventHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_processEventHandler.Delegate != null)
                {
                    PInvoke.RT_AttributeProcessor_setProcessEventCallback(Handle, AttributeProcessorEventHandler.HandlerFunction, _processEventHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_AttributeProcessor_setProcessEventCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal AttributeProcessor(IntPtr handle) => Handle = handle;
        
        ~AttributeProcessor()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_processEventHandler.Delegate != null)
                {
                    PInvoke.RT_AttributeProcessor_setProcessEventCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_AttributeProcessor_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal AttributeProcessorEventHandler _processEventHandler = new AttributeProcessorEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_AttributeProcessor_create(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_AttributeProcessor_setProcessEventCallback(IntPtr handle, AttributeProcessorEventInternal processEvent, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_AttributeProcessor_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}