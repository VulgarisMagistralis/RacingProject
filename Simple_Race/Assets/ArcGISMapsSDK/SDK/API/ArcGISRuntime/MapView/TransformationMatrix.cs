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

namespace Esri.ArcGISRuntime.MapView
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class TransformationMatrix
    {
        #region Properties
        /// W quaternion.
        /// 
        /// - Since: 100.6.0
        public double QuaternionW
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getQuaternionW(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// X quaternion.
        /// 
        /// - Since: 100.6.0
        public double QuaternionX
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getQuaternionX(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Y quaternion.
        /// 
        /// - Since: 100.6.0
        public double QuaternionY
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getQuaternionY(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Z quaternion.
        /// 
        /// - Since: 100.6.0
        public double QuaternionZ
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getQuaternionZ(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// X translation.
        /// 
        /// - Since: 100.6.0
        public double TranslationX
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getTranslationX(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Y translation.
        /// 
        /// - Since: 100.6.0
        public double TranslationY
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getTranslationY(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Z translation.
        /// 
        /// - Since: 100.6.0
        public double TranslationZ
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_TransformationMatrix_getTranslationZ(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Adds this and transformation together and returns the result. return = (this + parameter)
        /// 
        /// - Parameter transformation: The TransformationMatrix to be added onto this TransformationMatrix.
        /// - Returns: A new TransformationMatrix object which is the result of adding two TransformationMatrix
        /// - Since: 100.6.0
        public TransformationMatrix AddTransformation(TransformationMatrix transformation)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localTransformation = transformation.Handle;
            
            var localResult = PInvoke.RT_TransformationMatrix_addTransformation(Handle, localTransformation, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            TransformationMatrix localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new TransformationMatrix(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates a TransformationMatrix object with an identity transform.
        /// 
        /// - Remark: Subtracting a TransformationMatrix from an identity matrix is useful for getting the 
        /// inverse of that transformation matrix, i.e., identity matrix - other matrix = inverse(other matrix).
        /// - Returns: A TransformationMatrix.
        /// - Since: 100.6.0
        public static TransformationMatrix CreateIdentityMatrix()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_TransformationMatrix_createIdentityMatrix(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            TransformationMatrix localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new TransformationMatrix(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a TransformationMatrix object using x, y, z, w quaternion and x, y, z translations.
        /// 
        /// - Parameters:
        ///   - quaternionX: The x quaternion of the transformation matrix.
        ///   - quaternionY: The y quaternion of the transformation matrix.
        ///   - quaternionZ: The z quaternion of the transformation matrix.
        ///   - quaternionW: The w quaternion of the transformation matrix.
        ///   - translationX: The x position of the transformation matrix.
        ///   - translationY: The y position of the transformation matrix.
        ///   - translationZ: The z position of the transformation matrix.
        /// - Returns: A TransformationMatrix.
        /// - Since: 100.6.0
        public static TransformationMatrix CreateWithQuaternionAndTranslation(double quaternionX, double quaternionY, double quaternionZ, double quaternionW, double translationX, double translationY, double translationZ)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_TransformationMatrix_createWithQuaternionAndTranslation(quaternionX, quaternionY, quaternionZ, quaternionW, translationX, translationY, translationZ, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            TransformationMatrix localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new TransformationMatrix(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Subtracts the parameter from this object and returns the result. return = (this - parameter).
        /// 
        /// - Parameter transformation: The TransformationMatrix to be subtracted from this TransformationMatrix.
        /// - Returns: A new TransformationMatrix object which is the result of subtracting two TransformationMatrix.
        /// - Since: 100.6.0
        public TransformationMatrix SubtractTransformation(TransformationMatrix transformation)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localTransformation = transformation.Handle;
            
            var localResult = PInvoke.RT_TransformationMatrix_subtractTransformation(Handle, localTransformation, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            TransformationMatrix localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new TransformationMatrix(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal TransformationMatrix(IntPtr handle) => Handle = handle;
        
        ~TransformationMatrix()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_TransformationMatrix_destroy(Handle, errorHandler);
                
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
        internal static extern double RT_TransformationMatrix_getQuaternionW(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_TransformationMatrix_getQuaternionX(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_TransformationMatrix_getQuaternionY(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_TransformationMatrix_getQuaternionZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_TransformationMatrix_getTranslationX(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_TransformationMatrix_getTranslationY(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_TransformationMatrix_getTranslationZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_TransformationMatrix_addTransformation(IntPtr handle, IntPtr transformation, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_TransformationMatrix_createIdentityMatrix(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_TransformationMatrix_createWithQuaternionAndTranslation(double quaternionX, double quaternionY, double quaternionZ, double quaternionW, double translationX, double translationY, double translationZ, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_TransformationMatrix_subtractTransformation(IntPtr handle, IntPtr transformation, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_TransformationMatrix_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}