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

namespace Esri.GameEngine.Elevation.Base
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISElevationSource :
        ArcGISRuntime.Loadable
    {
        #region Constructors
        /// Creates a new elevation source.
        /// 
        /// - Remark: Creates a new elevation source.
        /// - Parameters:
        ///   - source: Elevation source
        ///   - type: ArcGISElevationSource type definition.
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISElevationSource(string source, ArcGISElevationSourceType type, string APIKey)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISElevationSource_create(source, type, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// API Key will be sended on loading process to match the new credit system.
        /// 
        /// - Since: 100.10.0
        public string APIKey
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getAPIKey(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// The full extent of this ArcGISElevationSource, which is the extent where all ArcGISElevationSource data is contained.
        /// 
        /// - Remark: You can use this to zoom
        /// to all of the data contained in this ArcGISElevationSource.
        /// - Since: 100.10.0
        public GameEngine.Extent.ArcGISExtentRectangle Extent
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getExtent(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                GameEngine.Extent.ArcGISExtentRectangle localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.Extent.ArcGISExtentRectangle(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// Define if this elevation source is enabled or not.
        /// 
        /// - Since: 100.10.0
        public bool IsEnabled
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getIsEnabled(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISElevationSource_setIsEnabled(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Identifier for elevation source
        /// 
        /// - Since: 100.10.0
        public string Name
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getName(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISElevationSource_setName(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// Defines what type of ArcGISElevationSource is it. Is read only and it will be setup on the constructor
        /// 
        /// - Since: 100.10.0
        internal ArcGISElevationSourceType ObjectType
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getObjectType(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// Source property will be a read only, it will be setup on the constructor
        /// 
        /// - Since: 100.10.0
        public string Source
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getSource(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// The spatial reference of the elevation source.
        /// 
        /// - Remark: ArcGISImageElevationSource must be Web Mercator SpatialReference in ArcGISMapType.local mode.
        /// ArcGISImageElevationSource must be Web Mercator or WGS84 SpatialReference in ArcGISMapType.global mode.
        /// ArcGISImageElevationSource SpatialReference must match the SpatialReference of the ArcGISMap.
        /// ArcGISImageElevationSource tiling scheme must be compatible with the tiling scheme of the ArcGISMap.
        /// If any of the above constraints are violated, an ElevationSourceViewState error is generated.
        /// - Since: 100.10.0
        public ArcGISRuntime.Geometry.SpatialReference SpatialReference
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getSpatialReference(Handle, errorHandler);
                
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
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getLoadError(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Convert.FromError(new Standard.Error(localResult));
            }
        }
        
        public ArcGISRuntime.LoadStatus LoadStatus
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISElevationSource_getLoadStatus(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        public void CancelLoad()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISElevationSource_cancelLoad(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public void Load()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISElevationSource_load(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public void RetryLoad()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISElevationSource_retryLoad(Handle, errorHandler);
            
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
                    PInvoke.RT_ArcGISElevationSource_setDoneLoadingCallback(Handle, ArcGISRuntime.LoadableDoneLoadingEventHandler.HandlerFunction, _doneLoadingHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISElevationSource_setDoneLoadingCallback(Handle, null, IntPtr.Zero, errorHandler);
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
                    PInvoke.RT_ArcGISElevationSource_setLoadStatusChangedCallback(Handle, ArcGISRuntime.LoadableLoadStatusChangedEventHandler.HandlerFunction, _loadStatusChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISElevationSource_setLoadStatusChangedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // ArcGISRuntime.Loadable
        
        #region Internal Members
        internal ArcGISElevationSource(IntPtr handle) => Handle = handle;
        
        ~ArcGISElevationSource()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_doneLoadingHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISElevationSource_setDoneLoadingCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                if (_loadStatusChangedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISElevationSource_setLoadStatusChangedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISElevationSource_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISElevationSource_create([MarshalAs(UnmanagedType.LPStr)]string source, ArcGISElevationSourceType type, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSource_getAPIKey(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSource_getExtent(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISElevationSource_getIsEnabled(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_setIsEnabled(IntPtr handle, [MarshalAs(UnmanagedType.I1)]bool isEnabled, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSource_getName(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_setName(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ArcGISElevationSourceType RT_ArcGISElevationSource_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSource_getSource(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSource_getSpatialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
        
        #region ArcGISRuntime.Loadable P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISElevationSource_getLoadError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ArcGISRuntime.LoadStatus RT_ArcGISElevationSource_getLoadStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_cancelLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_load(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_retryLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_setDoneLoadingCallback(IntPtr handle, ArcGISRuntime.LoadableDoneLoadingEventInternal doneLoading, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISElevationSource_setLoadStatusChangedCallback(IntPtr handle, ArcGISRuntime.LoadableLoadStatusChangedEventInternal loadStatusChanged, IntPtr userData, IntPtr errorHandler);
        #endregion // ArcGISRuntime.Loadable P-Invoke Declarations
    }
}