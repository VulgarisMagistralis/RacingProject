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

namespace Esri.ArcGISRuntime.Geometry
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class Point :
        Geometry
    {
        #region Constructors
        /// Creates a point with an x, y and a null spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public Point(double x, double y) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Point_createWithXY(x, y, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a point with an x, y, z and a null spatial reference.
        /// 
        /// - Remark: The minimum z-value is -6,356,752 meters, which is the approximate radius of the earth (the WGS 84 datum semi-minor axis).
        /// The maximum z-value is 55,000,000 meters.
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - z: The z coordinate for the point.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public Point(double x, double y, double z) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Point_createWithXYZ(x, y, z, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a point with an x, y, z and spatial reference.
        /// 
        /// - Remark: The minimum z-value is -6,356,752 meters, which is the approximate radius of the earth (the WGS 84 datum semi-minor axis).
        /// The maximum z-value is 55,000,000 meters.
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - z: The z coordinate for the point.
        ///   - spatialReference: The spatial reference for the point.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public Point(double x, double y, double z, SpatialReference spatialReference) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_Point_createWithXYZSpatialReference(x, y, z, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a point with an x, y and a spatial reference.
        /// 
        /// - Remark: Creates a point with x, y for the coordinates and a spatial reference.
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - spatialReference: The spatial reference for the point.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public Point(double x, double y, SpatialReference spatialReference) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_Point_createWithXYSpatialReference(x, y, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// An optional coordinate to define a measure value for the point.
        /// 
        /// - Remark: M-values are used in linear referencing scenarios and may represent things like mile markers along a highway. Like 
        /// z-values, every geometry can optionally store m-values with the point coordinates that comprise it. The default 
        /// m-value is NaN. If an m-value is specified when a geometry is created, the new geometry will have m-values 
        /// (Geometry.hasM will be true). Note that when you get m-values back from a geometry, the default value of 
        /// NAN is returned for vertices that do not have m-values. A geometry with m-values is sometimes known as an 
        /// m-aware geometry.
        /// - Since: 100.0.0
        public double M
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Point_getM(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The x coordinate for the point.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double X
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Point_getX(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The y coordinate for the point.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double Y
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Point_getY(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The z coordinate for the point.
        /// 
        /// - Remark: Geometries can have z-values, indicating values along the z-axis, which is perpendicular to both 
        /// the x-axis and y-axis. Z-values indicate height above or depth below a surface, or an absolute 
        /// elevation. For example, z-values are used to draw the locations of geometries in a SceneView. 
        /// Note that geometries are not considered true 3D shapes and are draped onto surfaces in the view, 
        /// or in some cases, drawn in a single plane by using z-values. Z-values are stored on Point and 
        /// Envelope. Since Multipoint, Polyline, and Polygon are created from a collection of
        /// Point, all types of geometry can have z-values.
        /// 
        /// Whether or not a geometry has z-values is determined when the geometry is created; if you use a 
        /// method that has a z-value parameter, the new geometry will have z-values (Geometry.hasZ will 
        /// be true). If you create geometries using constructors that take z-value parameters, or if you pass 
        /// into the constructor points or segments that have z-values, the new geometry will have z-values. A 
        /// Geometry with z-values is sometimes known as a z-aware geometry.
        /// 
        /// It may be that not all vertices in your geometry have a z-value defined. NAN is a valid z-value used 
        /// to indicate an unknown z-value. However, the default z-value is 0. When you get z-values from a geometry 
        /// that does not have z-values, the default is 0. Check the Geometry.hasZ to determine whether a 
        /// z-value of 0 means that there are no z-values in the geometry or that the z-value in the geometry's 
        /// coordinates really is 0.
        /// - Since: 100.0.0
        public double Z
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Point_getZ(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Creates a point with an x, y, m coordinates and a null spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - m: The m value for the point.
        /// - Returns: A point. This is passed to geometry or point functions.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public static Point CreateWithM(double x, double y, double m)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Point_createWithM(x, y, m, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Point localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Point(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates a point with an x, y, z, and m coordinate.
        /// 
        /// - Remark: The minimum z-value is -6,356,752 meters, which is the approximate radius of the earth (the WGS 84 datum semi-minor axis).
        /// The maximum z-value is 55,000,000 meters.
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - z: The z coordinate for the point.
        ///   - m: The m value for the point.
        /// - Returns: A point. This is passed to geometry or point functions.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public static Point CreateWithM(double x, double y, double z, double m)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Point_createWithZM(x, y, z, m, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Point localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Point(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates a point with an x, y, z, m and a spatial reference.
        /// 
        /// - Remark: The minimum z-value is -6,356,752 meters, which is the approximate radius of the earth (the WGS 84 datum semi-minor axis).
        /// The maximum z-value is 55,000,000 meters.
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - z: The z coordinate for the point.
        ///   - m: The m value for the point.
        ///   - spatialReference: The spatial reference for the point.
        /// - Returns: A point. This is passed to geometry or point functions.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public static Point CreateWithM(double x, double y, double z, double m, SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            var localResult = PInvoke.RT_Point_createWithZMSpatialReference(x, y, z, m, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Point localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Point(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates a point with an x, y, m and spatial reference.
        /// 
        /// - Parameters:
        ///   - x: The x coordinate for the point.
        ///   - y: The y coordinate for the point.
        ///   - m: The m value for the point.
        ///   - spatialReference: The spatial reference for the point.
        /// - Returns: A point. This is passed to geometry or point functions.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public static Point CreateWithM(double x, double y, double m, SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            var localResult = PInvoke.RT_Point_createWithMSpatialReference(x, y, m, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Point localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Point(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal Point(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithXY(double x, double y, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithXYZ(double x, double y, double z, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithXYZSpatialReference(double x, double y, double z, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithXYSpatialReference(double x, double y, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Point_getM(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Point_getX(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Point_getY(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Point_getZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithM(double x, double y, double m, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithZM(double x, double y, double z, double m, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithZMSpatialReference(double x, double y, double z, double m, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Point_createWithMSpatialReference(double x, double y, double m, IntPtr spatialReference, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}