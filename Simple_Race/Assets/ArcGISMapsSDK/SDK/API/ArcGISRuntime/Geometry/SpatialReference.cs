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
    public partial class SpatialReference
    {
        #region Constructors
        /// Creates a spatial reference based on WKID.
        /// 
        /// - Remark: The method will create a spatial reference that has only horizontal coordinate system and does not have vertical
        /// coordinate system associated with it.
        /// - Parameter WKID: The well known ID of the horizontal coordinate system. Must be a positive value.
        /// - Since: 100.0.0
        public SpatialReference(int WKID)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_SpatialReference_create(WKID, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a spatial reference based on WKID for the horizontal coordinate system and vertical coordinate system.
        /// 
        /// - Remark: The method will create a spatial reference that has both horizontal and vertical coordinate systems.
        /// When the vertical_WKID is 0, the method is equivalent to calling SpatialReference.SpatialReference(int32),
        /// and does not define a vertical coordinate system part.
        /// - Parameters:
        ///   - WKID: The well known ID of the horizontal coordinate system. Must be a positive value.
        ///   - verticalWKID: The well known ID of the vertical  coordinate system. Must be a non negative value.
        /// - Since: 100.0.0
        public SpatialReference(int WKID, int verticalWKID)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_SpatialReference_createVerticalWKID(WKID, verticalWKID, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a spatial reference based on well known text.
        /// 
        /// - Parameter wkText: The well known text of the spatial reference to create.
        /// - Since: 100.0.0
        public SpatialReference(string wkText)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_SpatialReference_createFromWKText(wkText, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The well-known ID for the horizontal coordinate system.
        /// 
        /// - Remark: The well-known ID for the horizontal coordinate system. Will return 0 if an error occurs.
        /// - Since: 100.0.0
        public int WKID
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getWKID(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The well known string for the horizontal and vertical coordinate system.
        /// 
        /// - Remark: The well known string for the horizontal and vertical coordinate system.
        /// - Since: 100.0.0
        public string WKText
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getWKText(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// If the given spatial reference is a projected coordinate system, then this will return the geographic coordinate system of that system.
        /// 
        /// - Remark: If the spatial reference is a projected coordinate system, then a spatial reference object representing the underlying geographic
        /// coordinate system is returned. Every projected coordinate system has an underlying geographic coordinate system. If the
        /// spatial reference is a geographic coordinate system, then a reference to itself is returned.
        /// If the spatial reference is a local spatial reference, a null is returned with an error.
        /// - Since: 100.0.0
        public SpatialReference BaseGeographic
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getBaseGeographic(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                SpatialReference localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new SpatialReference(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// True if spatial reference has a vertical coordinate system set; false otherwise.
        /// 
        /// - Remark: A spatial reference can optionally include a definition for a vertical coordinate system (VCS), which
        /// can be used to interpret the z-values of a geometry. A VCS defines linear units of measure, the origin of
        /// z-values, and whether z-values are "positive up" (representing heights above a surface) or "positive down"
        /// (indicating that values are depths below a surface).
        /// 
        /// A SpatialReference may have a VCS set, for example by calling the
        /// SpatialReference.SpatialReference(int32, int32) constructor. SpatialReference.verticalWKID and
        /// SpatialReference.WKText provide more information about the specific VCS set on this instance.
        /// 
        /// VCSs are used when projecting geometries using a HorizontalVerticalTransformation.
        /// - SeeAlso: SpatialReference.verticalWKID
        /// - Since: 100.9.0
        public bool HasVertical
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getHasVertical(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// True if spatial reference is a Geographic Coordinate System.
        /// 
        /// - Since: 100.0.0
        public bool IsGeographic
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getIsGeographic(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// True if coordinate system is horizontally pannable.
        /// 
        /// - Since: 100.0.0
        public bool IsPannable
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getIsPannable(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// True if spatial reference is a Projected Coordinate System.
        /// 
        /// - Since: 100.0.0
        public bool IsProjected
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getIsProjected(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The well-known ID for the vertical coordinate system.
        /// 
        /// - Remark: The well-known ID for the vertical coordinate system (VCS) set on this SpatialReference.
        /// 
        /// Returns 0 if there is no VCS set, if there is a custom VCS set, or on error.
        /// - SeeAlso: SpatialReference.SpatialReference(int32, int32)
        /// - Since: 100.0.0
        public int VerticalWKID
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_SpatialReference_getVerticalWKID(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Creates a spatial reference based on WGS84.
        /// 
        /// - Remark: The method will create a WGS84 spatial reference.
        /// - Returns: A spatial reference. This is passed to spatial reference functions.
        /// null if an error occurs.
        /// - Since: 100.0.0
        public static SpatialReference WGS84()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_SpatialReference_WGS84(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            SpatialReference localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new SpatialReference(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Check if the 2 spatial references are equal.
        /// 
        /// - Remark: Check spatial references to see if they are equal. Will return false if an error occurs.
        /// - Parameter right: The 2nd spatial reference to check to see if equal to the 1st.
        /// - Returns: True if the spatial references are equal otherwise false.
        /// - Since: 100.0.0
        public bool Equals(SpatialReference right)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localRight = right.Handle;
            
            var localResult = PInvoke.RT_SpatialReference_equals(Handle, localRight, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Calculate the grid convergence for a spatial reference at a given point.
        /// 
        /// - Remark: The grid convergence is the angle between True North and Grid North
        /// at a point on a map. The grid convergence can be used to convert a
        /// horizontal direction expressed as an azimuth in a geographic
        /// coordinate system (relative to True North) to a direction expressed
        /// as a bearing in a projected coordinate system (relative to Grid
        /// North), and vice versa.
        /// 
        /// Sign convention
        /// 
        /// The grid convergence returned by this method is positive when Grid
        /// North lies east of True North. The following formula demonstrates
        /// how to obtain a bearing (b) from an azimuth (a) using the grid
        /// convergence (c) returned by this method:
        /// 
        /// b = a - c
        /// 
        /// This sign convention is sometimes named the Gauss-Bomford convention.
        /// 
        /// Other notes:
        /// * Returns 0 if the spatial reference is a geographic coordinate system
        /// * Returns NAN if the point is outside the projection's horizon or on error
        /// * If the point has no spatial reference, it is assumed to be in the
        ///   given spatial reference
        /// * If the point's spatial reference differs from the spatial
        ///   reference given, it's location will be transformed automatically to
        ///   the given spatial reference
        /// - Parameter point: The point
        /// - Returns: The grid convergence in degrees.
        /// - Since: 100.3.0
        public double GetConvergenceAngle(Point point)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localPoint = point.Handle;
            
            var localResult = PInvoke.RT_SpatialReference_getConvergenceAngle(Handle, localPoint, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// Creates a spatial reference based on web Mercator.
        /// 
        /// - Remark: The method will create a web Mercator spatial reference.
        /// - Returns: A spatial reference. This is passed to spatial reference functions.
        /// null if an error occurs.
        /// - Since: 100.0.0
        public static SpatialReference WebMercator()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_SpatialReference_webMercator(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            SpatialReference localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new SpatialReference(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal SpatialReference(IntPtr handle) => Handle = handle;
        
        ~SpatialReference()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_SpatialReference_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_SpatialReference_create(int WKID, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_SpatialReference_createVerticalWKID(int WKID, int verticalWKID, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_SpatialReference_createFromWKText([MarshalAs(UnmanagedType.LPStr)]string wkText, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern int RT_SpatialReference_getWKID(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_SpatialReference_getWKText(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_SpatialReference_getBaseGeographic(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_SpatialReference_getHasVertical(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_SpatialReference_getIsGeographic(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_SpatialReference_getIsPannable(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_SpatialReference_getIsProjected(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern int RT_SpatialReference_getVerticalWKID(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_SpatialReference_WGS84(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_SpatialReference_equals(IntPtr handle, IntPtr right, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_SpatialReference_getConvergenceAngle(IntPtr handle, IntPtr point, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_SpatialReference_webMercator(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_SpatialReference_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}