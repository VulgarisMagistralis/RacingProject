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
    public partial class ArcGIS3DModelLayer :
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
        public ArcGIS3DModelLayer(string source, string APIKey) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGIS3DModelLayer_create(source, APIKey, errorHandler);
            
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
        public ArcGIS3DModelLayer(string source, string name, float opacity, bool visible, string APIKey) :
            base(IntPtr.Zero)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGIS3DModelLayer_createWithProperties(source, name, opacity, visible, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The user defined material reference to render the layer
        /// 
        /// - Remark: This is required to be set before the layer is loaded or an error will occur.
        /// - Since: 100.12.0
        public UnityEngine.Material MaterialReference
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGIS3DModelLayer_getMaterialReference(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromUnityMaterial(localResult);
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGIS3DModelLayer_setMaterialReference(Handle, Interop.ToUnityMaterial(value), errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Properties
        
        #region Methods
        /// A Array of the strings that are used for retrieving the specified attributes for visualization
        /// 
        /// - Remark: Before loading the layer ensure that ArcGIS3DModelLayer.setAttributesToVisualize is set. 
        /// 
        /// To select all attributes use `*`. 
        /// 
        /// Attribute names should be passed in as string values, not as attribute keys. Empty strings will be ignored,
        /// and attribute strings that don't match exactly or don't exist will be considered invalid. Invalid attribute
        /// strings will result in LayerViewState warnings. Duplicate and extraneous strings will be removed,
        /// although the order in which removal occurs is undefined. Feature IDs will always be requested.
        /// 
        /// At present, the only supported attribute types are int and float. 
        /// 
        /// Calling this function after the layer has loaded will result in an error.
        /// - Parameter layerAttributes: The attribute names to pass through for visualization.
        /// - Since: 100.12.0
        public void SetAttributesToVisualize(Unity.ImmutableArray<string> layerAttributes)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localLayerAttributes = layerAttributes.Handle;
            
            PInvoke.RT_ArcGIS3DModelLayer_setAttributesToVisualize(Handle, localLayerAttributes, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// A Array of the strings that are used for retrieving the specified attributes from the layer,
        /// the corresponding VisualizationAttributeDescription to describe the attributes to be visualized and the
        /// AttributeProcessor definition.
        /// 
        /// - Remark: Before loading the layer ensure that ArcGIS3DModelLayer.setAttributesToVisualize is set. 
        /// 
        /// To select all attributes use `*`. 
        /// 
        /// Attribute names should be passed in as string values, not as attribute keys. Empty strings will be ignored,
        /// and attribute strings that don't match exactly or don't exist will be considered invalid. Invalid attribute
        /// strings will result in LayerViewState warnings. Duplicate and extraneous strings will be removed,
        /// although the order in which removal occurs is undefined. Feature IDs will always be requested.
        /// 
        /// The order of the input attributes provided to the AttributeProcessor will match the order of
        /// valid, non-empty VisualizationAttributeDescription provided as the first argument to this function. 
        /// 
        /// At present, the only supported attribute types are int and float. 
        /// 
        /// Calling this function after the layer has loaded will result in an error.
        /// - Parameters:
        ///   - layerAttributes: The attribute names requested and provided to the AttributeProcessorEvent as input.
        ///   - visualizationAttributeDescriptions: The visualization attribute descriptions to use for visualization.
        ///   - attributeProcessor: The AttributeProcessor defines an event which is invoked when the requested layer attributes are available to be processed.
        /// - Since: 100.12.0
        public void SetAttributesToVisualize(Unity.ImmutableArray<string> layerAttributes, Unity.ImmutableArray<GameEngine.Attributes.VisualizationAttributeDescription> visualizationAttributeDescriptions, GameEngine.Attributes.AttributeProcessor attributeProcessor)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localLayerAttributes = layerAttributes.Handle;
            var localVisualizationAttributeDescriptions = visualizationAttributeDescriptions.Handle;
            var localAttributeProcessor = attributeProcessor.Handle;
            
            PInvoke.RT_ArcGIS3DModelLayer_setAttributesToVisualizeAndTransform(Handle, localLayerAttributes, localVisualizationAttributeDescriptions, localAttributeProcessor, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGIS3DModelLayer(IntPtr handle) : base(handle)
        {
        }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGIS3DModelLayer_create([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGIS3DModelLayer_createWithProperties([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string name, float opacity, [MarshalAs(UnmanagedType.I1)]bool visible, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGIS3DModelLayer_getMaterialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGIS3DModelLayer_setMaterialReference(IntPtr handle, IntPtr materialReference, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGIS3DModelLayer_setAttributesToVisualize(IntPtr handle, IntPtr layerAttributes, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGIS3DModelLayer_setAttributesToVisualizeAndTransform(IntPtr handle, IntPtr layerAttributes, IntPtr visualizationAttributeDescriptions, IntPtr attributeProcessor, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}