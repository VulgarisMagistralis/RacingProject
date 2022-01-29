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

namespace Esri.GameEngine.Layers
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISIntegratedMeshLayer :
        GameEngine.Layers.Base.ArcGISLayer
    {
        #region Constructors
        /// Creates a new layer.
        /// 
        /// - Remark: Creates a new layer.
        /// - Parameters:
        ///   - source: Layer source
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISIntegratedMeshLayer(string source, string APIKey) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISIntegratedMeshLayer_create(source, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a new layer.
        /// 
        /// - Remark: Creates a new layer.
        /// - Parameters:
        ///   - source: Layer source.
        ///   - name: Layer name
        ///   - opacity: Layer opacity.
        ///   - visible: Layer visible or not.
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISIntegratedMeshLayer(string source, string name, float opacity, bool visible, string APIKey) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISIntegratedMeshLayer_createWithProperties(source, name, opacity, visible, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Internal Members
        internal ArcGISIntegratedMeshLayer(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISIntegratedMeshLayer_create([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISIntegratedMeshLayer_createWithProperties([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string name, float opacity, [MarshalAs(UnmanagedType.I1)]bool visible, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}