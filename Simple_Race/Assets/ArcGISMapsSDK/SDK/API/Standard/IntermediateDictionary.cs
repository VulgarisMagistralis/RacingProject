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
    internal partial class IntermediateDictionary<Key, Value>
    {
        #region Constructors
        /// Creates a dictionary. This allocates memory that must be deleted.
        /// 
        /// - Parameters:
        ///   - keyType: The type of the dictionary's key.
        ///   - valueType: The type of the dictionary's value.
        /// - Since: 100.0.0
        internal IntermediateDictionary(ElementType keyType, ElementType valueType)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Dictionary_create(keyType, valueType, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The type of the keys this dictionary holds.
        /// 
        /// - Since: 100.0.0
        internal ElementType KeyType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Dictionary_getKeyType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Determines the number of values in the dictionary.
        /// 
        /// - Remark: The number of values in the dictionary. If an error occurs a 0 will be returned.
        /// - Since: 100.0.0
        internal ulong Size
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Dictionary_getSize(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        
        /// The type of the values this dictionary holds.
        /// 
        /// - Since: 100.0.0
        internal ElementType ValueType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Dictionary_getValueType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Get a value for a specific key.
        /// 
        /// - Remark: Retrieves the value for the specified key.
        /// - Parameter key: The key which you want to get the value.
        /// - Returns: The value for the key requested. A null if an error occurs or the key doesn't exist.
        /// - Since: 100.0.0
        internal Value At(Key key)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localKey = Convert.ToElement(key);
            
            var localResult = PInvoke.RT_Dictionary_at(Handle, localKey.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Standard.Element localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.Element(localResult);
            }
            
            return Convert.FromElement<Value>(localLocalResult);
        }
        
        /// Does the dictionary contain a key.
        /// 
        /// - Remark: Does the dictionary contain a specific key.
        /// - Parameter key: The key you want to find.
        /// - Returns: True if the key is in the dictionary otherwise false.
        /// - Since: 100.0.0
        internal bool Contains(Key key)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localKey = Convert.ToElement(key);
            
            var localResult = PInvoke.RT_Dictionary_contains(Handle, localKey.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Returns true if the two dictionaries are equal, false otherwise.
        /// 
        /// - Parameter dictionary2: The second dictionary.
        /// - Returns: Returns true if the two dictionaries are equal, false otherwise.
        /// - Since: 100.0.0
        internal bool Equals(Unity.Dictionary<Key, Value> dictionary2)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localDictionary2 = dictionary2.Handle;
            
            var localResult = PInvoke.RT_Dictionary_equals(Handle, localDictionary2, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Returns the type that the value of a given key should be, if the dictionary instance supports it.  Otherwise, ElementType.variant will be returned.
        /// 
        /// - Parameter key: The key you want to now the value type for.
        /// - Returns: An ElementType value, or ElementType.variant if an error occurs or the dictionary does not support the type lookup.
        /// - Since: 100.0.0
        internal ElementType GetTypeForKey(Key key)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localKey = Convert.ToElement(key);
            
            var localResult = PInvoke.RT_Dictionary_getTypeForKey(Handle, localKey.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Insert a value for a given key in the dictionary.
        /// 
        /// - Remark: Insert a value at a specified key position to the dictionary.
        /// - Parameters:
        ///   - key: The key position which you want to insert the value.
        ///   - value: The value that is to be added.
        /// - Since: 100.0.0
        internal void Insert(Key key, Value value)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localKey = Convert.ToElement(key);
            var localValue = Convert.ToElement(value);
            
            PInvoke.RT_Dictionary_insert(Handle, localKey.Handle, localValue.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Determines if there are any values in the dictionary.
        /// 
        /// - Remark: Check if the dictionary object has any values in it.
        /// - Returns: true if the dictionary object contains no values otherwise false. Will return true if an error occurs.
        /// - Since: 100.0.0
        internal bool IsEmpty()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Dictionary_isEmpty(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Remove a value at a specific key position in the dictionary.
        /// 
        /// - Remark: Remove a value at a specific position in the dictionary.
        /// - Parameter key: The key position which you want to remove the value.
        /// - Since: 100.0.0
        internal void Remove(Key key)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localKey = Convert.ToElement(key);
            
            PInvoke.RT_Dictionary_remove(Handle, localKey.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Remove all values from the dictionary.
        /// 
        /// - Since: 100.0.0
        internal void RemoveAll()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_Dictionary_removeAll(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Replace a value at a specific key position in the dictionary.
        /// 
        /// - Parameters:
        ///   - key: The key position which you want to replace the value.
        ///   - newValue: The new value.
        /// - Since: 100.0.0
        internal void Replace(Key key, Value newValue)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localKey = Convert.ToElement(key);
            var localNewValue = Convert.ToElement(newValue);
            
            PInvoke.RT_Dictionary_replace(Handle, localKey.Handle, localNewValue.Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal IntermediateDictionary(IntPtr handle) => Handle = handle;
        
        ~IntermediateDictionary()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_Dictionary_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_Dictionary_create(ElementType keyType, ElementType valueType, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ElementType RT_Dictionary_getKeyType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern UIntPtr RT_Dictionary_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ElementType RT_Dictionary_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Dictionary_at(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Dictionary_contains(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Dictionary_equals(IntPtr handle, IntPtr dictionary2, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ElementType RT_Dictionary_getTypeForKey(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Dictionary_insert(IntPtr handle, IntPtr key, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Dictionary_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Dictionary_remove(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Dictionary_removeAll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Dictionary_replace(IntPtr handle, IntPtr key, IntPtr newValue, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Dictionary_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}