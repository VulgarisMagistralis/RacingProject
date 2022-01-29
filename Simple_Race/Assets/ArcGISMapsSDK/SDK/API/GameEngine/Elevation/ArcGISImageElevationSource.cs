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

namespace Esri.GameEngine.Elevation
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISImageElevationSource :
        GameEngine.Elevation.Base.ArcGISElevationSource
    {
        #region Constructors
        /// Creates a new ElevationSource.
        /// 
        /// - Remark: Creates a new ElevationSource.
        /// - Parameters:
        ///   - source: ElevationSource source
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISImageElevationSource(string source, string APIKey) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISImageElevationSource_create(source, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a new ElevationSource.
        /// 
        /// - Remark: Creates a new ElevationSource.
        /// - Parameters:
        ///   - source: ElevationSource source.
        ///   - name: ElevationSource name
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISImageElevationSource(string source, string name, string APIKey) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISImageElevationSource_createWithName(source, name, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Internal Members
        internal ArcGISImageElevationSource(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISImageElevationSource_create([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISImageElevationSource_createWithName([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}