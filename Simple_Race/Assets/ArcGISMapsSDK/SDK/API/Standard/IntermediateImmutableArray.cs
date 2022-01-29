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
    internal partial class IntermediateImmutableArray<T>
    {
        #region Properties
        /// The type of the array.
        /// 
        /// - Remark: The type of the array object.
        /// - SeeAlso: ArrayType
        /// - Since: 100.0.0
        internal ArrayType ObjectType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Array_getObjectType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Determines the number of values in the array.
        /// 
        /// - Remark: The number of values in the array. If an error occurs a 0 will be returned.
        /// - Since: 100.0.0
        internal ulong Size
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Array_getSize(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        
        /// The type of the values this array holds.
        /// 
        /// - Since: 100.0.0
        internal ElementType ValueType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Array_getValueType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Get a value at a specific position.
        /// 
        /// - Remark: Retrieves the value at the specified position.
        /// - Parameter position: The position which you want to get the value.
        /// - Returns: The value, Element, at the position requested.
        /// - Since: 100.0.0
        internal T At(ulong position)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            var localResult = PInvoke.RT_Array_at(Handle, localPosition, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Standard.Element localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.Element(localResult);
            }
            
            return Convert.FromElement<T>(localLocalResult);
        }
        
        /// Does the array contain the given value.
        /// 
        /// - Remark: Does the array contain a specific value.
        /// - Parameter value: The value you want to find.
        /// - Returns: True if the value is in the array otherwise false.
        /// - Since: 100.0.0
        internal bool Contains(T value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localValue = Convert.ToElement(value);
            
            var localResult = PInvoke.RT_Array_contains(Handle, localValue.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Creates a ArrayBuilder.
        /// 
        /// - Parameter valueType: The type of the values the returned ArrayBuilder holds.
        /// - Returns: A ArrayBuilder
        /// - SeeAlso: ArrayBuilder
        /// - Since: 100.9.0
        internal static IntermediateImmutableArrayBuilder<T> CreateBuilder(ElementType valueType)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_createBuilder(valueType, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            IntermediateImmutableArrayBuilder<T> localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new IntermediateImmutableArrayBuilder<T>(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Returns true if the two arrays are equal, false otherwise.
        /// 
        /// - Parameter array2: The second array.
        /// - Returns: Returns true if the two arrays are equal, false otherwise.
        /// - Since: 100.0.0
        internal bool Equals(Unity.ImmutableArray<T> array2)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localArray2 = array2.Handle;
            
            var localResult = PInvoke.RT_Array_equals(Handle, localArray2, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Get the first value in the array.
        /// 
        /// - Returns: The value, Element, at the position requested.
        /// - Since: 100.0.0
        internal T First()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_first(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Standard.Element localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.Element(localResult);
            }
            
            return Convert.FromElement<T>(localLocalResult);
        }
        
        /// Retrieves the position of the given value in the array.
        /// 
        /// - Parameter value: The value you want to find.
        /// - Returns: The position of the value in the array, or the max value of size_t otherwise.
        /// - Since: 100.0.0
        internal ulong IndexOf(T value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localValue = Convert.ToElement(value);
            
            var localResult = PInvoke.RT_Array_indexOf(Handle, localValue.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// Determines if there are any values in the array.
        /// 
        /// - Remark: Check if the array object has any values in it.
        /// - Returns: true if the  array object contains no values otherwise false. Will return true if an error occurs.
        /// - Since: 100.0.0
        internal bool IsEmpty()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_isEmpty(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Get the last value in the array.
        /// 
        /// - Returns: The value, Element, at the position requested.
        /// - Since: 100.0.0
        internal T Last()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_last(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Standard.Element localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.Element(localResult);
            }
            
            return Convert.FromElement<T>(localLocalResult);
        }
        #endregion // Methods
        
        #region Internal Members
        internal IntermediateImmutableArray(IntPtr handle) => Handle = handle;
        
        ~IntermediateImmutableArray()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_Array_destroy(Handle, errorHandler);
                
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
        internal static extern ArrayType RT_Array_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_Array_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ElementType RT_Array_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Array_at(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Array_contains(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Array_createBuilder(ElementType valueType, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Array_equals(IntPtr handle, IntPtr array2, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Array_first(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_Array_indexOf(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Array_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Array_last(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Array_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}