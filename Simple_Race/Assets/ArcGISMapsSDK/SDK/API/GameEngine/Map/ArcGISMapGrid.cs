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
    public partial class ArcGISMapGrid
    {
        #region Constructors
        /// Create grid for the map.
        /// 
        /// - Remark: Display a grid on the top of the map.
        /// - Parameters:
        ///   - visible: Visible or not.
        ///   - color: Vector with 3 elements to the RGB.
        /// - Since: 100.10.0
        public ArcGISMapGrid(bool visible, Standard.Color color)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localColor = color.Handle;
            
            Handle = PInvoke.RT_ArcGISMapGrid_create(visible, localColor, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// Grid color.
        /// 
        /// - Since: 100.10.0
        public Standard.Color Color
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMapGrid_getColor(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                Standard.Color localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Standard.Color(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMapGrid_setColor(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Visible or not.
        /// 
        /// - Since: 100.10.0
        public bool IsVisible
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMapGrid_getIsVisible(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISMapGrid_setIsVisible(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISMapGrid(IntPtr handle) => Handle = handle;
        
        ~ArcGISMapGrid()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISMapGrid_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISMapGrid_create([MarshalAs(UnmanagedType.I1)]bool visible, IntPtr color, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMapGrid_getColor(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMapGrid_setColor(IntPtr handle, IntPtr color, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISMapGrid_getIsVisible(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMapGrid_setIsVisible(IntPtr handle, [MarshalAs(UnmanagedType.I1)]bool isVisible, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMapGrid_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}