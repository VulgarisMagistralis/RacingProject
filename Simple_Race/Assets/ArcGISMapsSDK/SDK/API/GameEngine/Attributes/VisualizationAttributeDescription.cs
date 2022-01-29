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

namespace Esri.GameEngine.Attributes
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class VisualizationAttributeDescription
    {
        #region Constructors
        /// Creates a VisualizationAttributeDescription object.
        /// 
        /// - Parameters:
        ///   - name: The attribute name
        ///   - visualizationAttributeType: The type of the visualization attribute.
        /// - Since: 100.12.0
        public VisualizationAttributeDescription(string name, VisualizationAttributeType visualizationAttributeType)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_VisualizationAttributeDescription_create(name, visualizationAttributeType, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The attribute name
        /// 
        /// - Since: 100.12.0
        public string Name
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_VisualizationAttributeDescription_getName(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// The type of the visualization attribute.
        /// 
        /// - SeeAlso: VisualizationAttributeType
        /// - Since: 100.12.0
        public VisualizationAttributeType VisualizationAttributeType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_VisualizationAttributeDescription_getVisualizationAttributeType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal VisualizationAttributeDescription(IntPtr handle) => Handle = handle;
        
        ~VisualizationAttributeDescription()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_VisualizationAttributeDescription_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_VisualizationAttributeDescription_create([MarshalAs(UnmanagedType.LPStr)]string name, VisualizationAttributeType visualizationAttributeType, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_VisualizationAttributeDescription_getName(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern VisualizationAttributeType RT_VisualizationAttributeDescription_getVisualizationAttributeType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_VisualizationAttributeDescription_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}