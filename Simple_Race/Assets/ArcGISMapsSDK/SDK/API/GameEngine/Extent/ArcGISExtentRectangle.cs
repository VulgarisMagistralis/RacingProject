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

namespace Esri.GameEngine.Extent
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISExtentRectangle :
        ArcGISExtent
    {
        #region Constructors
        /// Creates an rectangle extent centered on provided coordinates.
        /// 
        /// - Parameters:
        ///   - center: Rectangle center
        ///   - width: Side length along the east-to-west axis, in meters
        ///   - height: Side length along the north-to-south axis, in meters
        /// - Since: 100.12.0
        public ArcGISExtentRectangle(GameEngine.Location.ArcGISPosition center, double width, double height) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localCenter = center.Handle;
            
            Handle = PInvoke.RT_ArcGISExtentRectangle_create(localCenter, width, height, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// Side length along the north-to-south axis, in meters
        /// 
        /// - Since: 100.12.0
        public double Height
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISExtentRectangle_getHeight(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Side length along the east-to-west axis, in meters
        /// 
        /// - Since: 100.12.0
        public double Width
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISExtentRectangle_getWidth(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISExtentRectangle(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISExtentRectangle_create(IntPtr center, double width, double height, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_ArcGISExtentRectangle_getHeight(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_ArcGISExtentRectangle_getWidth(IntPtr handle, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}