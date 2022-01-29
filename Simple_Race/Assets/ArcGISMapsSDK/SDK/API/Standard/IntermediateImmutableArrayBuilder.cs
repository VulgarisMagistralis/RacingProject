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
    [StructLayout(LayoutKind.Sequential)]
    internal partial class IntermediateImmutableArrayBuilder<T>
    {
        #region Properties
        /// The type of the values this ArrayBuilder holds.
        /// 
        /// - Since: 100.9.0
        internal ElementType ValueType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArrayBuilder_getValueType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Add a value to the ArrayBuilder.
        /// 
        /// - Parameter value: The value that is to be added.
        /// - Since: 100.9.0
        internal void Add(T value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localValue = Convert.ToElement(value);
            
            PInvoke.RT_ArrayBuilder_add(Handle, localValue.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a Array containing the values added to this ArrayBuilder.
        /// 
        /// - Remark: The order of the values in the returned Array matches the order in which the
        /// values were added to this ArrayBuilder.
        /// 
        /// This call empties this ArrayBuilder of values, but leaves it in a valid
        /// (re-usable) state.
        /// - Returns: A Array containing the values added to this ArrayBuilder.
        /// - Since: 100.9.0
        internal Unity.ImmutableArray<T> MoveToArray()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArrayBuilder_moveToArray(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Unity.ImmutableArray<T> localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Unity.ImmutableArray<T>(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal IntermediateImmutableArrayBuilder(IntPtr handle) => Handle = handle;
        
        ~IntermediateImmutableArrayBuilder()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArrayBuilder_destroy(Handle, errorHandler);
                
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
        internal static extern ElementType RT_ArrayBuilder_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArrayBuilder_add(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArrayBuilder_moveToArray(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArrayBuilder_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}