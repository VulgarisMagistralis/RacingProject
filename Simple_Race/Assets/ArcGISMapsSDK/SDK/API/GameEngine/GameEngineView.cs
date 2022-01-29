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

namespace Esri.GameEngine
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial class GameEngineView
    {
        #region Constructors
        /// Create a new GameEngineView.
        /// 
        /// - Remark: Defining the public API of the "Game Engine View" is not the goal currently, so something closely resembling the existing mockup API is defined here
        /// - Parameters:
        ///   - gameEngineType: Specifies the client game engine type
        ///   - globeModel: Specifies the model used to represent a 3D globe
        /// - Since: 100.7.0
        internal GameEngineView(GameEngineType gameEngineType, ArcGISRuntime.MapView.GlobeModel globeModel)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_GameEngineView_create(gameEngineType, globeModel, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// The current camera
        /// 
        /// - Since: 100.7.0
        internal ArcGISRuntime.MapView.Camera Camera
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_GameEngineView_getCamera(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                ArcGISRuntime.MapView.Camera localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISRuntime.MapView.Camera(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_GameEngineView_setCamera(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// The current map document.
        /// 
        /// - Since: 100.10.0
        internal GameEngine.Map.ArcGISMap Map
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_GameEngineView_getMap(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                GameEngine.Map.ArcGISMap localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.Map.ArcGISMap(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_GameEngineView_setMap(Handle, localValue, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Serves render commands
        /// 
        /// - Since: 100.12.0
        internal GameEngine.RenderCommandQueue.RenderCommandServer RenderCommandServer
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_GameEngineView_getRenderCommandServer(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                GameEngine.RenderCommandQueue.RenderCommandServer localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.RenderCommandQueue.RenderCommandServer(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// The current view options for the game engine view
        /// 
        /// - Since: 100.10.0
        internal GameEngine.View.ArcGISRendererViewOptions ViewOptions
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_GameEngineView_getViewOptions(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_GameEngineView_setViewOptions(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Retrieve the view's view state.
        /// 
        /// - Returns: A GameEngineViewViewState.
        /// - Since: 100.11.0
        internal GameEngineViewViewState GetViewState()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_GameEngineView_getViewViewState(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            GameEngineViewViewState localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new GameEngineViewViewState(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Retrieve the elevation source's view state.
        /// 
        /// - Parameter elevation: A elevation object to get the view state for.
        /// - Returns: A ElevationSourceViewState.
        /// - Since: 100.11.0
        internal ArcGISRuntime.MapView.ElevationSourceViewState GetViewState(GameEngine.Elevation.Base.ArcGISElevationSource elevation)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localElevation = elevation.Handle;
            
            var localResult = PInvoke.RT_GameEngineView_getElevationSourceViewState(Handle, localElevation, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISRuntime.MapView.ElevationSourceViewState localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISRuntime.MapView.ElevationSourceViewState(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Retrieve the layer's view state.
        /// 
        /// - Parameter layer: A layer object to get the view state for.
        /// - Returns: A LayerViewState.
        /// - Since: 100.11.0
        internal ArcGISRuntime.MapView.LayerViewState GetViewState(GameEngine.Layers.Base.ArcGISLayer layer)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localLayer = layer.Handle;
            
            var localResult = PInvoke.RT_GameEngineView_getLayerViewState(Handle, localLayer, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISRuntime.MapView.LayerViewState localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISRuntime.MapView.LayerViewState(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Handle a platform low memory event
        /// 
        /// - Since: 100.11.0
        internal void HandleLowMemoryWarning()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_GameEngineView_handleLowMemoryWarning(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Communicate memory availability metrics to the runtime
        /// 
        /// - Parameters:
        ///   - totalSystemMemory: The total system memory in bytes (-1 if not known)
        ///   - usedSystemMemory: The in-use system memory in bytes (-1 if not known)
        ///   - totalVideoMemory: The total video memory in bytes (-1 if not known)
        ///   - usedVideoMemory: The in-use video memory in bytes (-1 if not known)
        /// - Since: 100.11.0
        internal void SetMemoryAvailability(long totalSystemMemory, long usedSystemMemory, long totalVideoMemory, long usedVideoMemory)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_GameEngineView_setMemoryAvailability(Handle, totalSystemMemory, usedSystemMemory, totalVideoMemory, usedVideoMemory, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Sets the viewport size and field of view. 
        /// Either field of view angle can be set to 0 to indicate "unset".
        /// For example, if verticalFieldOfViewDegrees is 0.0, but horizontalFieldOfViewDegrees is greater than zero, then the viewport vertical field
        /// of view will be set to the appropriate value given the horizontal FOV and distortion factor. And vice versa.
        /// 
        /// - Parameters:
        ///   - viewportWidthPixels: used in visible tile calculation, on the basis that DPI is 96.
        ///   - viewportHeightPixels: used in visible tile calculation, on the basis that DPI is 96.
        ///   - horizontalFieldOfViewDegrees: A value in degrees. The valid range is 0 to 120
        ///   - verticalFieldOfViewDegrees: A value in degrees. The valid range is 0 to 120
        ///   - verticalDistortionFactor: Determines how much the vertical field of view is distorted. A distortion factor of 1.0 is default. A distortion factor less than 1.0 will cause the visuals to be stretched taller in comparison to their width. A distortion factor greater than 1.0 will cause the visuals to be shrunk shorter in comparison to their width.
        /// - Since: 100.7.0
        internal void SetViewportProperties(uint viewportWidthPixels, uint viewportHeightPixels, float horizontalFieldOfViewDegrees, float verticalFieldOfViewDegrees, float verticalDistortionFactor)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_GameEngineView_setViewportProperties(Handle, viewportWidthPixels, viewportHeightPixels, horizontalFieldOfViewDegrees, verticalFieldOfViewDegrees, verticalDistortionFactor, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Events
        /// Sets a callback to be invoked when the elevation source view state changes for the view.
        /// 
        /// - Since: 100.11.0
        internal GameEngineViewElevationSourceViewStateChangedEvent ElevationSourceViewStateChanged
        {
            get
            {
                return _elevationSourceViewStateChangedHandler.Delegate;
            }
            set
            {
                _elevationSourceViewStateChangedHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_elevationSourceViewStateChangedHandler.Delegate != null)
                {
                    PInvoke.RT_GameEngineView_setElevationSourceViewStateChangedCallback(Handle, GameEngineViewElevationSourceViewStateChangedEventHandler.HandlerFunction, _elevationSourceViewStateChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_GameEngineView_setElevationSourceViewStateChangedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Sets a callback to be invoked when the layer view state changes for the view.
        /// 
        /// - Since: 100.11.0
        internal GameEngineViewLayerViewStateChangedEvent LayerViewStateChanged
        {
            get
            {
                return _layerViewStateChangedHandler.Delegate;
            }
            set
            {
                _layerViewStateChangedHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_layerViewStateChangedHandler.Delegate != null)
                {
                    PInvoke.RT_GameEngineView_setLayerViewStateChangedCallback(Handle, GameEngineViewLayerViewStateChangedEventHandler.HandlerFunction, _layerViewStateChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_GameEngineView_setLayerViewStateChangedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Sets a callback to be invoked when the view view state changes for the view.
        /// 
        /// - Since: 100.11.0
        internal GameEngineViewStateChangedEvent ViewViewStateChanged
        {
            get
            {
                return _viewViewStateChangedHandler.Delegate;
            }
            set
            {
                _viewViewStateChangedHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_viewViewStateChangedHandler.Delegate != null)
                {
                    PInvoke.RT_GameEngineView_setViewViewStateChangedCallback(Handle, GameEngineViewStateChangedEventHandler.HandlerFunction, _viewViewStateChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_GameEngineView_setViewViewStateChangedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal GameEngineView(IntPtr handle) => Handle = handle;
        
        ~GameEngineView()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_elevationSourceViewStateChangedHandler.Delegate != null)
                {
                    PInvoke.RT_GameEngineView_setElevationSourceViewStateChangedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                if (_layerViewStateChangedHandler.Delegate != null)
                {
                    PInvoke.RT_GameEngineView_setLayerViewStateChangedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                if (_viewViewStateChangedHandler.Delegate != null)
                {
                    PInvoke.RT_GameEngineView_setViewViewStateChangedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_GameEngineView_destroy(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal GameEngineViewElevationSourceViewStateChangedEventHandler _elevationSourceViewStateChangedHandler = new GameEngineViewElevationSourceViewStateChangedEventHandler();
        
        internal GameEngineViewLayerViewStateChangedEventHandler _layerViewStateChangedHandler = new GameEngineViewLayerViewStateChangedEventHandler();
        
        internal GameEngineViewStateChangedEventHandler _viewViewStateChangedHandler = new GameEngineViewStateChangedEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_create(GameEngineType gameEngineType, ArcGISRuntime.MapView.GlobeModel globeModel, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_getCamera(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setCamera(IntPtr handle, IntPtr camera, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_getMap(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setMap(IntPtr handle, IntPtr map, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_getRenderCommandServer(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern GameEngine.View.ArcGISRendererViewOptions RT_GameEngineView_getViewOptions(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setViewOptions(IntPtr handle, GameEngine.View.ArcGISRendererViewOptions viewOptions, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_getViewViewState(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_getElevationSourceViewState(IntPtr handle, IntPtr elevation, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_GameEngineView_getLayerViewState(IntPtr handle, IntPtr layer, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_handleLowMemoryWarning(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setMemoryAvailability(IntPtr handle, long totalSystemMemory, long usedSystemMemory, long totalVideoMemory, long usedVideoMemory, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setViewportProperties(IntPtr handle, uint viewportWidthPixels, uint viewportHeightPixels, float horizontalFieldOfViewDegrees, float verticalFieldOfViewDegrees, float verticalDistortionFactor, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setElevationSourceViewStateChangedCallback(IntPtr handle, GameEngineViewElevationSourceViewStateChangedEventInternal elevationSourceViewStateChanged, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setLayerViewStateChangedCallback(IntPtr handle, GameEngineViewLayerViewStateChangedEventInternal layerViewStateChanged, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_setViewViewStateChangedCallback(IntPtr handle, GameEngineViewStateChangedEventInternal viewViewStateChanged, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_GameEngineView_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}