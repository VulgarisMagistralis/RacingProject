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
    public partial class ArcGISBasemap :
        ArcGISRuntime.Loadable
    {
        #region Constructors
        /// Creates a ArcGISBasemap from a URI and ArcGISLayerType
        /// 
        /// - Parameters:
        ///   - source: ArcGISLayer source
        ///   - type: Layer type definition.
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISBasemap(string source, GameEngine.Layers.Base.ArcGISLayerType type, string APIKey)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISBasemap_createWithLayerSourceAndType(source, type, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a ArcGISBasemap from a basemap URI
        /// 
        /// - Parameters:
        ///   - source: ArcGISBasemap source
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISBasemap(string source, string APIKey)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISBasemap_create(source, APIKey, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Creates a ArcGISBasemap from a basemap URI and all initial parameters.
        /// 
        /// - Parameters:
        ///   - source: ArcGISBasemap source.
        ///   - name: ArcGISBasemap name
        ///   - opacity: ArcGISBasemap opacity.
        ///   - visible: ArcGISBasemap visible or not.
        ///   - APIKey: API Key used to load data.
        /// - Since: 100.10.0
        public ArcGISBasemap(string source, string name, float opacity, bool visible, string APIKey)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISBasemap_createWithProperties(source, name, opacity, visible, APIKey, errorHandler);
            
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
                
                var localResult = PInvoke.RT_ArcGISBasemap_getAPIKey(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        
        /// ArcGISBasemap visible true or false
        /// 
        /// - Since: 100.10.0
        public bool IsVisible
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getIsVisible(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISBasemap_setIsVisible(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// This property will help the user to identify the layer on his application.
        /// 
        /// - Since: 100.10.0
        public string Name
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getName(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISBasemap_setName(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        
        /// ArcGISBasemap Opacity
        /// 
        /// - Since: 100.10.0
        public float Opacity
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getOpacity(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISBasemap_setOpacity(Handle, value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
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
                
                var localResult = PInvoke.RT_ArcGISBasemap_getSource(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Create a ArcGISBasemap Imagery Type
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateImagery()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createImagery(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap Imagery Type with labels
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateImageryWithLabels()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createImageryWithLabels(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap Light gray canvas Type
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateLightGrayCanvas()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createLightGrayCanvas(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap National Geographic Type
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateNationalGeographic()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createNationalGeographic(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap Oceans Type
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateOceans()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createOceans(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap Streets Type
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateStreets()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createStreets(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap Terrain Type with labels
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateTerrainWithLabels()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createTerrainWithLabels(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// Create a ArcGISBasemap Topographic Type
        /// 
        /// - Returns: A ArcGISBasemap
        /// - Since: 100.10.0
        public static ArcGISBasemap CreateTopographic()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createTopographic(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region ArcGISRuntime.Loadable
        public Exception LoadError
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getLoadError(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Convert.FromError(new Standard.Error(localResult));
            }
        }
        
        public ArcGISRuntime.LoadStatus LoadStatus
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getLoadStatus(Handle, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        public void CancelLoad()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISBasemap_cancelLoad(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public void Load()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISBasemap_load(Handle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        public void RetryLoad()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISBasemap_retryLoad(Handle, errorHandler);
            
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
                    PInvoke.RT_ArcGISBasemap_setDoneLoadingCallback(Handle, ArcGISRuntime.LoadableDoneLoadingEventHandler.HandlerFunction, _doneLoadingHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISBasemap_setDoneLoadingCallback(Handle, null, IntPtr.Zero, errorHandler);
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
                    PInvoke.RT_ArcGISBasemap_setLoadStatusChangedCallback(Handle, ArcGISRuntime.LoadableLoadStatusChangedEventHandler.HandlerFunction, _loadStatusChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISBasemap_setLoadStatusChangedCallback(Handle, null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // ArcGISRuntime.Loadable
        
        #region Internal Members
        internal ArcGISBasemap(IntPtr handle) => Handle = handle;
        
        ~ArcGISBasemap()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_doneLoadingHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISBasemap_setDoneLoadingCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                if (_loadStatusChangedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISBasemap_setLoadStatusChangedCallback(Handle, null, IntPtr.Zero, IntPtr.Zero);
                }
                
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISBasemap_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISBasemap_createWithLayerSourceAndType([MarshalAs(UnmanagedType.LPStr)]string source, GameEngine.Layers.Base.ArcGISLayerType type, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_create([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createWithProperties([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string name, float opacity, [MarshalAs(UnmanagedType.I1)]bool visible, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getAPIKey(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISBasemap_getIsVisible(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setIsVisible(IntPtr handle, [MarshalAs(UnmanagedType.I1)]bool isVisible, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getName(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setName(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern float RT_ArcGISBasemap_getOpacity(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setOpacity(IntPtr handle, float opacity, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getSource(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createImagery(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createImageryWithLabels(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createLightGrayCanvas(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createNationalGeographic(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createOceans(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createStreets(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createTerrainWithLabels(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createTopographic(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
        
        #region ArcGISRuntime.Loadable P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getLoadError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern ArcGISRuntime.LoadStatus RT_ArcGISBasemap_getLoadStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_cancelLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_load(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_retryLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setDoneLoadingCallback(IntPtr handle, ArcGISRuntime.LoadableDoneLoadingEventInternal doneLoading, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setLoadStatusChangedCallback(IntPtr handle, ArcGISRuntime.LoadableLoadStatusChangedEventInternal loadStatusChanged, IntPtr userData, IntPtr errorHandler);
        #endregion // ArcGISRuntime.Loadable P-Invoke Declarations
    }
}