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
    public partial class Geometry
    {
        #region Properties
        /// The number of dimensions for the geometry.
        /// 
        /// - Remark: Will return GeometryDimension.unknown if an error occurs.
        /// - SeeAlso: GeometryDimension
        /// - Since: 100.0.0
        public GeometryDimension Dimension
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getDimension(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The extent for the geometry.
        /// 
        /// - Remark: The extent for the geometry which is a envelope and contains the same spatial reference
        /// as the input geometry.
        /// - SeeAlso: Envelope
        /// - Since: 100.0.0
        public Envelope Extent
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getExtent(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                Envelope localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Envelope(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// True if this geometry contains curve segments; false otherwise.
        /// 
        /// - Remark: The ArcGIS Platform supports polygon and polyline geometries that contain curve segments (sometimes known as
        /// true curves or nonlinear segments). Curves may be present in certain types of data - for example Mobile Map
        /// Packages (MMPK), or geometry JSON. When connecting to ArcGIS feature services that support curves, ArcGIS Runtime
        /// retrieves densified versions of curve feature geometries.
        /// 
        /// If a polygon or polyline geometry contains curve segments, this property returns true. Prior to v100.12,
        /// it was not possible to access curve segments, and only LineSegment instances would be returned
        /// when iterating through the segments in a Polygon or Polyline object, irrespective of this property.
        /// 
        /// From v100.12, you can use curve segments when using a MultipartBuilder to create or edit polygon and
        /// polyline geometries, and also get curve segments when iterating through the segments of existing
        /// Multipart geometries when this property returns true.
        /// - SeeAlso: GeometryBuilder.hasCurves, ImmutablePart.hasCurves, Segment.isCurve, CubicBezierSegment, EllipticArcSegment
        /// - Since: 100.0.0
        public bool HasCurves
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getHasCurves(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// A value indicating if the geometry has M.
        /// 
        /// - Remark: If an error occurs false will be returned.
        /// M is a vertex attributes that are stored with the geometry.
        /// - Since: 100.0.0
        public bool HasM
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getHasM(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// A value indicating if the geometry has Z.
        /// 
        /// - Remark: If an error occurs false will be returned.
        /// Z typically represent elevations or heights.
        /// - Since: 100.0.0
        public bool HasZ
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getHasZ(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Check if a geometry is empty or not.
        /// 
        /// - Remark: Only check the geometry to see if it is empty. Does not check the spatial reference. Will return true if an error occurs.
        /// - Since: 100.0.0
        public bool IsEmpty
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getIsEmpty(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The type of geometry.
        /// 
        /// - Remark: The geometry type for a specific geometry. Will return GeometryType.unknown if an error occurs.
        /// - SeeAlso: GeometryType
        /// - Since: 100.0.0
        internal GeometryType ObjectType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getObjectType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The spatial reference for the geometry.
        /// 
        /// - Remark: If the geometry does not have a spatial reference null is returned.
        /// - SeeAlso: SpatialReference
        /// - Since: 100.0.0
        public SpatialReference SpatialReference
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getSpatialReference(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                SpatialReference localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new SpatialReference(localResult);
                }
                
                return localLocalResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Check if two geometries are equal.
        /// 
        /// - Remark: Returns false if an error occurs.
        /// - Parameter right: The second geometry.
        /// - Returns: True if the geometries (including their spatial references) are equal, otherwise returns false.
        /// - Since: 100.0.0
        public bool Equals(Geometry right)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localRight = right.Handle;
            
            var localResult = PInvoke.RT_Geometry_equals(Handle, localRight, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Check if two geometries are equal to within some tolerance.
        /// 
        /// - Remark: This function performs a lightweight comparison of two geometries,
        /// such as might be useful when writing test code.
        /// It uses the tolerance to compare each of x, y, and any other values
        /// the geometries possess (such as z or m) independently in the manner:
        /// abs(value1 - value2) <= tolerance.
        /// Returns true if the difference of each is within the tolerance and
        /// all other properties of the geometries are exactly equal (spatial
        /// reference, vertex count, etc.).
        /// A single tolerance is used even if the units for the horizontal
        /// coordinates and other values differ, e.g horizontal coordinates in
        /// degrees and vertical coordinates in meters. This function does not
        /// respect modular arithmetic of spatial references which wrap around,
        /// so longitudes of -180 and +180 degrees are considered to differ by
        /// 360 degrees.
        /// Returns false if an error occurs.
        /// For topological equality, use relational operators
        /// instead of this function. See GeometryEngine.equals.
        /// - Parameters:
        ///   - right: The second geometry.
        ///   - tolerance: The tolerance.
        /// - Returns: True if the geometries are equal, within the tolerance, otherwise false.
        /// - Since: 100.1.0
        public bool Equals(Geometry right, double tolerance)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localRight = right.Handle;
            
            var localResult = PInvoke.RT_Geometry_equalsWithTolerance(Handle, localRight, tolerance, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Creates a geometry from an ArcGIS JSON geometry representation.
        /// 
        /// - Remark: Geometry can be serialized and de-serialized to and from JSON. The 
        /// ref-external@[ArcGIS REST API documentation](https://developers.arcgis.com/documentation/common-data-types/geometry-objects.htm)
        /// describes the JSON representation of geometry objects. 
        /// You can use this encoding and decoding mechanism to exchange geometries with REST Web services 
        /// or to store them in text files.
        /// - Parameters:
        ///   - inputJSON: JSON representation of geometry.
        ///   - spatialReference: The geometry's spatial reference.
        /// - Returns: Geometry converted from a JSON String.
        /// - SeeAlso: Geometry
        /// - Since: 100.0.0
        public static Geometry FromJSON(string inputJSON, SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            var localResult = PInvoke.RT_Geometry_fromJSONWithSpatialReference(inputJSON, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Geometry localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Geometry(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal Geometry(IntPtr handle) => Handle = handle;
        
        ~Geometry()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_Geometry_destroy(Handle, errorHandler);
                
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
        internal static extern GeometryDimension RT_Geometry_getDimension(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Geometry_getExtent(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getHasCurves(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getHasM(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getHasZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getIsEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern GeometryType RT_Geometry_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Geometry_getSpatialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_equals(IntPtr handle, IntPtr right, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_equalsWithTolerance(IntPtr handle, IntPtr right, double tolerance, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Geometry_fromJSONWithSpatialReference([MarshalAs(UnmanagedType.LPStr)]string inputJSON, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_Geometry_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}