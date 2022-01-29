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
    public partial class Envelope :
        Geometry
    {
        #region Constructors
        /// Creates an envelope based on the x and y values with a null spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        /// - Since: 100.0.0
        public Envelope(double xMin, double yMin, double xMax, double yMax) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Envelope_createWithXY(xMin, yMin, xMax, yMax, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an envelope based on the x,y and z values with a null spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - zMin: The minimal z value.
        ///   - zMax: The maximum z value.
        /// - Since: 100.0.0
        public Envelope(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Envelope_createWithXYZ(xMin, yMin, xMax, yMax, zMin, zMax, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an envelope based on x, y, and z values with the spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - zMin: The minimal z value.
        ///   - zMax: The maximum z value.
        ///   - spatialReference: The spatial reference for the envelope.
        /// - Since: 100.0.0
        public Envelope(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, SpatialReference spatialReference) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_Envelope_createWithXYZSpatialReference(xMin, yMin, xMax, yMax, zMin, zMax, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an envelope based on the x and y values with a spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - spatialReference: The spatial reference for the envelope.
        /// - Since: 100.0.0
        public Envelope(double xMin, double yMin, double xMax, double yMax, SpatialReference spatialReference) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_Envelope_createWithXYSpatialReference(xMin, yMin, xMax, yMax, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an envelope from a center point and a width and height.
        /// 
        /// - Remark: The spatial reference of the resulting envelope will come from the center point.
        /// - Parameters:
        ///   - center: The center point for the envelope.
        ///   - width: The width of the envelope around the center point.
        ///   - height: The height of the envelope around the center point.
        /// - SeeAlso: Point
        /// - Since: 100.1.0
        public Envelope(Point center, double width, double height) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localCenter = center.Handle;
            
            Handle = PInvoke.RT_Envelope_createWithCenterPoint(localCenter, width, height, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an envelope from a center point and a width, height, and depth.
        /// 
        /// - Remark: The spatial reference of the resulting envelope will come from the center point.
        /// - Parameters:
        ///   - center: The center point for the envelope.
        ///   - width: The width of the envelope around the center point.
        ///   - height: The height of the envelope around the center point.
        ///   - depth: The depth of the envelope around the center point.
        /// - SeeAlso: Point
        /// - Since: 100.1.0
        public Envelope(Point center, double width, double height, double depth) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localCenter = center.Handle;
            
            Handle = PInvoke.RT_Envelope_createWithCenterPointAndDepth(localCenter, width, height, depth, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates an envelope from any two points.
        /// 
        /// - Remark: The spatial reference of the points must be the same. The spatial reference of the result envelope will come from the points.
        /// If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - min: The minimal values for the envelope.
        ///   - max: The maximum values for the envelope.
        /// - SeeAlso: Point
        /// - Since: 100.0.0
        public Envelope(Point min, Point max) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localMin = min.Handle;
            var localMax = max.Handle;
            
            Handle = PInvoke.RT_Envelope_createWithPoints(localMin, localMax, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The center point for the envelope.
        /// 
        /// - Remark: Creates a new Point.
        /// - SeeAlso: Point
        /// - Since: 100.0.0
        public Point Center
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getCenter(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                Point localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Point(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// The depth (ZMax - ZMin) for the envelope.
        /// 
        /// - Remark: A 2D envelope has zero
        /// depth. Returns NAN if the envelope is empty or if an error occurs.
        /// - Since: 100.1.0
        public double Depth
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getDepth(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The height for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double Height
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getHeight(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The m maximum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double MMax
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getMMax(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The m minimum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double MMin
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getMMin(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The width for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double Width
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getWidth(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The x maximum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double XMax
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getXMax(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The x minimum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double XMin
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getXMin(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The y maximum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double YMax
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getYMax(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The y minimum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double YMin
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getYMin(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The z maximum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double ZMax
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getZMax(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The z minimum value for the envelope.
        /// 
        /// - Remark: Will return NAN if an error occurs.
        /// - Since: 100.0.0
        public double ZMin
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Envelope_getZMin(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Creates an envelope based on the x, y and m values with a null spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - mMin: The minimal m value.
        ///   - mMax: The maximum m value.
        /// - Returns: A envelope. This is passed to geometry or envelope functions.
        /// - Since: 100.0.0
        public static Envelope CreateWithM(double xMin, double yMin, double xMax, double yMax, double mMin, double mMax)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Envelope_createWithM(xMin, yMin, xMax, yMax, mMin, mMax, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Envelope localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Envelope(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates an envelope based on the x, y, z and m values with a null spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - zMin: The minimal z value.
        ///   - zMax: The maximum z value.
        ///   - mMin: The minimal m value.
        ///   - mMax: The maximum m value.
        /// - Returns: A envelope. This is passed to geometry or envelope functions.
        /// - Since: 100.0.0
        public static Envelope CreateWithM(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, double mMin, double mMax)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Envelope_createWithZM(xMin, yMin, xMax, yMax, zMin, zMax, mMin, mMax, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Envelope localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Envelope(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates an envelope based on the x, y, z and m values with a spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - zMin: The minimal z value.
        ///   - zMax: The maximum z value.
        ///   - mMin: The minimal m value.
        ///   - mMax: The maximum m value.
        ///   - spatialReference: The spatial reference for the envelope.
        /// - Returns: A envelope. This is passed to geometry or envelope functions.
        /// - Since: 100.0.0
        public static Envelope CreateWithM(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, double mMin, double mMax, SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            var localResult = PInvoke.RT_Envelope_createWithZMSpatialReference(xMin, yMin, xMax, yMax, zMin, zMax, mMin, mMax, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Envelope localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Envelope(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Creates an envelope based on the x, y and m values with a spatial reference.
        /// 
        /// - Remark: If the values for min parameters are bigger than max parameters then they will be re-ordered. The resulting envelope will always have min less than or equal to max.
        /// - Parameters:
        ///   - xMin: The minimal x value.
        ///   - yMin: The minimal y value.
        ///   - xMax: The maximum x value.
        ///   - yMax: The maximum y value.
        ///   - mMin: The minimal m value.
        ///   - mMax: The maximum m value.
        ///   - spatialReference: The spatial reference for the envelope.
        /// - Returns: A envelope. This is passed to geometry or envelope functions.
        /// - Since: 100.0.0
        public static Envelope CreateWithM(double xMin, double yMin, double xMax, double yMax, double mMin, double mMax, SpatialReference spatialReference)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            var localResult = PInvoke.RT_Envelope_createWithMSpatialReference(xMin, yMin, xMax, yMax, mMin, mMax, localSpatialReference, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            Envelope localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Envelope(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal Envelope(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithXY(double xMin, double yMin, double xMax, double yMax, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithXYZ(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithXYZSpatialReference(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithXYSpatialReference(double xMin, double yMin, double xMax, double yMax, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithCenterPoint(IntPtr center, double width, double height, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithCenterPointAndDepth(IntPtr center, double width, double height, double depth, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithPoints(IntPtr min, IntPtr max, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_getCenter(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getDepth(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getHeight(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getMMax(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getMMin(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getWidth(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getXMax(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getXMin(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getYMax(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getYMin(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getZMax(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_Envelope_getZMin(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithM(double xMin, double yMin, double xMax, double yMax, double mMin, double mMax, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithZM(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, double mMin, double mMax, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithZMSpatialReference(double xMin, double yMin, double xMax, double yMax, double zMin, double zMax, double mMin, double mMax, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_Envelope_createWithMSpatialReference(double xMin, double yMin, double xMax, double yMax, double mMin, double mMax, IntPtr spatialReference, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}