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
    public partial class ArcGISMap :
        ArcGISRuntime.Loadable
    {
        #region Constructors
        /// Create a new Map document.
        /// 
        /// - Remark: Creates map view for displaying a map. We only be able to have one map by scene, it will be automatically rendered on the ArcGISRenderComponent.
        /// - Parameters:
        ///   - basemap: Specifies the basemap
        ///   - mapType: Specifies the map type.
        /// - Since: 100.10.0
        public ArcGISMap(ArcGISBasemap basemap, ArcGISMapType mapType)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localBasemap = basemap.Handle;
            
            Handle = PInvoke.RT_ArcGISMap_createWithBasemapAndMapType(localBasemap, mapType, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Create a new Map document.
        /// 
        /// - Remark: Creates map view for displaying a map. We only be able to have one map by scene, it will be automatically rendered on the ArcGISRenderComponent.
        /// - Parameter mapType: Specifies the map type.
        /// - Since: 100.10.0
        public ArcGISMap(ArcGISMapType mapType)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISMap_createWithMapType(mapType, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a map with spatial reference and map type.
        /// 
        /// - Remark: Supported SpatialReference are WGS84 or Web Mercator. Invalid inputs will result in a GameEngineViewViewState warning.
        /// - Parameters:
        ///   - spatialReference: A spatial reference object.
        ///   - mapType: Specifies the map type.
        /// - Since: 100.10.0
        public ArcGISMap(ArcGISRuntime.Geometry.SpatialReference spatialReference, ArcGISMapType mapType)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference.Handle;
            
            Handle = PInvoke.RT_ArcGISMap_createWithSpatialReferenceAndMapType(localSpatialReference, mapType, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// Definition for basemap.
        /// 
        /// - Since: 100.10.0
        public ArcGISBasemap Basemap
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getBasemap(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISBasemap localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISBasemap(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setBasemap(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Definition of map's clipping area. This property will be applied in Local mode only.
        /// 
        /// - Since: 100.10.0
        public GameEngine.Extent.ArcGISExtent ClippingArea
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getClippingArea(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                GameEngine.Extent.ArcGISExtent localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.Extent.ArcGISExtent(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setClippingArea(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Definition of map elevation.
        /// 
        /// - Since: 100.10.0
        public ArcGISMapElevation Elevation
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getElevation(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISMapElevation localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISMapElevation(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setElevation(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Definition for grid on map.
        /// 
        /// - Since: 100.10.0
        public ArcGISMapGrid Grid
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getGrid(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISMapGrid localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISMapGrid(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setGrid(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// List of ArcGISLayer included on the map
        /// 
        /// - Since: 100.10.0
        public Unity.Collection<GameEngine.Layers.Base.ArcGISLayer> Layers
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getLayers(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                Unity.Collection<GameEngine.Layers.Base.ArcGISLayer> localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Unity.Collection<GameEngine.Layers.Base.ArcGISLayer>(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setLayers(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Definition for the map, if it's local or global.
        /// 
        /// - Since: 100.10.0
        public ArcGISMapType MapType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getMapType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// The spatial reference for the map.
        /// 
        /// - SeeAlso: SpatialReference
        /// - Since: 100.10.0
        public ArcGISRuntime.Geometry.SpatialReference SpatialReference
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getSpatialReference(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISRuntime.Geometry.SpatialReference localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISRuntime.Geometry.SpatialReference(localResult);
                }
                
                return localLocalResult;
            }
        }
        #endregion // Properties
        
        #region ArcGISRuntime.Loadable
        public Exception LoadError
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getLoadError(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Convert.FromError(new Standard.Error(localResult));
            }
        }
        
        public ArcGISRuntime.LoadStatus LoadStatus
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getLoadStatus(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        public void CancelLoad()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISMap_cancelLoad(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public void Load()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISMap_load(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public void RetryLoad()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISMap_retryLoad(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public ArcGISRuntime.LoadableDoneLoadingEvent DoneLoading
        {
            get
            {
                return _doneLoadingHandler.Delegate;
            }
            set
            {
                _doneLoadingHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_doneLoadingHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISMap_setDoneLoadingCallback(Handle, ArcGISRuntime.LoadableDoneLoadingEventHandler.HandlerFunction, _doneLoadingHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISMap_setDoneLoadingCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        public ArcGISRuntime.LoadableLoadStatusChangedEvent LoadStatusChanged
        {
            get
            {
                return _loadStatusChangedHandler.Delegate;
            }
            set
            {
                _loadStatusChangedHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_loadStatusChangedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISMap_setLoadStatusChangedCallback(Handle, ArcGISRuntime.LoadableLoadStatusChangedEventHandler.HandlerFunction, _loadStatusChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISMap_setLoadStatusChangedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // ArcGISRuntime.Loadable
        
        #region Internal Members
        internal ArcGISMap(IntPtr handle) => Handle = handle;
        
        ~ArcGISMap()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_doneLoadingHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISMap_setDoneLoadingCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                if (_loadStatusChangedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISMap_setLoadStatusChangedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISMap_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal ArcGISRuntime.LoadableDoneLoadingEventHandler _doneLoadingHandler = new ArcGISRuntime.LoadableDoneLoadingEventHandler();
        
        internal ArcGISRuntime.LoadableLoadStatusChangedEventHandler _loadStatusChangedHandler = new ArcGISRuntime.LoadableLoadStatusChangedEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_createWithBasemapAndMapType(IntPtr basemap, ArcGISMapType mapType, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_createWithMapType(ArcGISMapType mapType, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_createWithSpatialReferenceAndMapType(IntPtr spatialReference, ArcGISMapType mapType, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getBasemap(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setBasemap(IntPtr handle, IntPtr basemap, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getClippingArea(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setClippingArea(IntPtr handle, IntPtr clippingArea, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getElevation(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setElevation(IntPtr handle, IntPtr elevation, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getGrid(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setGrid(IntPtr handle, IntPtr grid, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getLayers(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setLayers(IntPtr handle, IntPtr layers, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ArcGISMapType RT_ArcGISMap_getMapType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getSpatialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
        
        #region ArcGISRuntime.Loadable P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getLoadError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ArcGISRuntime.LoadStatus RT_ArcGISMap_getLoadStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_cancelLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_load(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_retryLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setDoneLoadingCallback(IntPtr handle, ArcGISRuntime.LoadableDoneLoadingEventInternal doneLoading, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISMap_setLoadStatusChangedCallback(IntPtr handle, ArcGISRuntime.LoadableLoadStatusChangedEventInternal loadStatusChanged, IntPtr userData, IntPtr errorHandler);
        #endregion // ArcGISRuntime.Loadable P-Invoke Declarations
    }
}