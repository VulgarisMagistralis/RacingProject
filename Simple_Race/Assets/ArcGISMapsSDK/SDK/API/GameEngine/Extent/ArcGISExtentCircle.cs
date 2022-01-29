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
    public partial class ArcGISExtentCircle :
        ArcGISExtent
    {
        #region Constructors
        /// Creates an circle extent centered on provided coordinates.
        /// 
        /// - Parameters:
        ///   - center: Circle center
        ///   - radius: Size of radius in meters
        /// - Since: 100.12.0
        public ArcGISExtentCircle(GameEngine.Location.ArcGISPosition center, double radius) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localCenter = center.Handle;
            
            Handle = PInvoke.RT_ArcGISExtentCircle_create(localCenter, radius, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// Size of radius in meters
        /// 
        /// - Since: 100.10.0
        public double Radius
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISExtentCircle_getRadius(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISExtentCircle(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISExtentCircle_create(IntPtr center, double radius, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern double RT_ArcGISExtentCircle_getRadius(IntPtr handle, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}