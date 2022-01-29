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

namespace Esri.GameEngine.Location
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISPosition
    {
        #region Constructors
        /// Creates an ArcGISPosition with x and y coordinates and a null spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate of the ArcGISPosition.
        ///   - y: The y coordinate of the ArcGISPosition.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.12.0
        public ArcGISPosition(double x, double y)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISPosition_createWithXY(x, y, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an ArcGISPosition with x, y, and z coordinates and a null spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate of the ArcGISPosition.
        ///   - y: The y coordinate of the ArcGISPosition.
        ///   - z: The z coordinate of the ArcGISPosition.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.12.0
        public ArcGISPosition(double x, double y, double z)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISPosition_createWithXYZ(x, y, z, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an ArcGISPosition with x, y, and z coordinates and a spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate of the ArcGISPosition.
        ///   - y: The y coordinate of the ArcGISPosition.
        ///   - z: The z coordinate of the ArcGISPosition.
        ///   - spatialReference: The spatial reference for the ArcGISPosition.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.12.0
        public ArcGISPosition(double x, double y, double z, ArcGISRuntime.Geometry.SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_ArcGISPosition_createWithXYZSpatialReference(x, y, z, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an ArcGISPosition with x and y coordinates and a spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate for the ArcGISPosition.
        ///   - y: The y coordinate for the ArcGISPosition.
        ///   - spatialReference: The spatial reference for the ArcGISPosition.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.12.0
        public ArcGISPosition(double x, double y, ArcGISRuntime.Geometry.SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_ArcGISPosition_createWithXYSpatialReference(x, y, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// A value indicating if the geometry has a valid z coordinate.
        /// 
        /// - Remark: If an error occurs false will be returned.
        /// Z typically represents elevations or heights.
        /// - Since: 100.12.0
        public bool HasZ
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISPosition_getHasZ(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// SpatialReference of this ArcGISPosition.
        /// 
        /// - SeeAlso: SpatialReference
        /// - Since: 100.12.0
        public ArcGISRuntime.Geometry.SpatialReference SpatialReference
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISPosition_getSpatialReference(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISRuntime.Geometry.SpatialReference localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISRuntime.Geometry.SpatialReference(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// The x coordinate of the ArcGISPosition. Corresponds to degrees longitude when the
        /// spatial reference is in a global coordinate system.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.12.0
        public double X
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISPosition_getX(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The y coordinate of the ArcGISPosition. Corresponds to degrees latitude when the
        /// spatial reference is in a global coordinate system.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.12.0
        public double Y
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISPosition_getY(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The z coordinate of the ArcGISPosition, in meters. Corresponds to
        /// altitude when the spatial reference is in a global coordinate system.
        /// 
        /// - Remark: The z-axis is considered to be perpendicular to both the x-axis and y-axis. Z-values
        /// indicate height above or depth below a surface, or an absolute elevation.
        /// - Since: 100.12.0
        public double Z
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISPosition_getZ(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISPosition(IntPtr handle) => Handle = handle;
        
        ~ArcGISPosition()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISPosition_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISPosition_createWithXY(double x, double y, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISPosition_createWithXYZ(double x, double y, double z, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISPosition_createWithXYZSpatialReference(double x, double y, double z, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISPosition_createWithXYSpatialReference(double x, double y, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISPosition_getHasZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISPosition_getSpatialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_ArcGISPosition_getX(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_ArcGISPosition_getY(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_ArcGISPosition_getZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISPosition_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}