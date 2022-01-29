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
    public partial class Attribute
    {
        #region Properties
        /// The type of the attribute.
        /// 
        /// - SeeAlso: AttributeType
        /// - Since: 100.12.0
        public AttributeType AttributeType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Attribute_getAttributeType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// A column worth of attribute data.
        /// 
        /// - Remark: Data is only valid during the scope of AttributeProcessorEvent.
        /// - Since: 100.12.0
        public global::Unity.Collections.NativeArray<byte> Data
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Attribute_getData(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromByteArrayStruct(localResult);
            }
        }
        
        /// The attribute name
        /// 
        /// - Since: 100.12.0
        public string Name
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Attribute_getName(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal Attribute(IntPtr handle) => Handle = handle;
        
        ~Attribute()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_Attribute_destroy(Handle, errorHandler);
                
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
        internal static extern AttributeType RT_Attribute_getAttributeType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern Standard.IntermediateByteArrayStruct RT_Attribute_getData(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Attribute_getName(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Attribute_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}