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

namespace Esri.GameEngine.Map
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISMapElevation
    {
        #region Constructors
        /// Create a elevation for the map with one elevation source
        /// 
        /// - Remark: Create elevation with a single elevation source
        /// - Parameter elevationSource: Elevation source
        /// - Since: 100.10.0
        public ArcGISMapElevation(GameEngine.Elevation.Base.ArcGISElevationSource elevationSource)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localElevationSource = elevationSource.Handle;
            
            Handle = PInvoke.RT_ArcGISMapElevation_create(localElevationSource, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// List of elevation sources included on the elevation.
        /// 
        /// - Since: 100.10.0
        public Unity.Collection<GameEngine.Elevation.Base.ArcGISElevationSource> ElevationSources
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMapElevation_getElevationSources(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                Unity.Collection<GameEngine.Elevation.Base.ArcGISElevationSource> localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Unity.Collection<GameEngine.Elevation.Base.ArcGISElevationSource>(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMapElevation_setElevationSources(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISMapElevation(IntPtr handle) => Handle = handle;
        
        ~ArcGISMapElevation()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISMapElevation_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISMapElevation_create(IntPtr elevationSource, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMapElevation_getElevationSources(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMapElevation_setElevationSources(IntPtr handle, IntPtr elevationSources, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMapElevation_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}