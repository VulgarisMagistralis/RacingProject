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
    public partial class ArcGISExtent
    {
        #region Properties
        /// The center of the extent.
        /// 
        /// - Since: 100.12.0
        public GameEngine.Location.ArcGISPosition Center
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISExtent_getCenter(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                GameEngine.Location.ArcGISPosition localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.Location.ArcGISPosition(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// Extent type
        /// 
        /// - Since: 100.10.0
        internal ArcGISExtentType ObjectType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISExtent_getObjectType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISExtent(IntPtr handle) => Handle = handle;
        
        ~ArcGISExtent()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISExtent_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISExtent_getCenter(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ArcGISExtentType RT_ArcGISExtent_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISExtent_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}