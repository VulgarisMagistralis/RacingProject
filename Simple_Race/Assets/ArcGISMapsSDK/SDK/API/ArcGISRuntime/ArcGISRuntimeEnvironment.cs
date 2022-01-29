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

namespace Esri.ArcGISRuntime
{
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISRuntimeEnvironment
    {
        #region Properties
        /// The default API key to access API key enabled services and resources in ArcGIS Online.
        /// 
        /// - Remark: An API key is a unique key used to authorize access to specific services and resources in ArcGIS Online.
        /// It is also used to monitor access to those services. An API key is created and managed in the ArcGIS developer
        /// dashboard and is tied to a specific ArcGIS account.
        /// 
        /// In addition to setting an API key at a global level for your application using the
        /// ArcGISRuntimeEnvironment.APIKey property, you can
        /// set an APIKeyResource.APIKey property on any ArcGIS Runtime class that implements APIKeyResource.
        /// When you set an individual APIKeyResource.APIKey property on an APIKeyResource it will override the
        /// default key at the global level (on the ArcGISRuntimeEnvironment.APIKey property, in other words),
        /// enabling more granular usage telemetry and management for ArcGIS Online
        /// resources used by your app.
        /// 
        /// Classes that expose an API key property by implementing APIKeyResource include:
        /// * Basemap
        /// * ArcGISSceneLayer
        /// * ArcGISTiledLayer
        /// * ArcGISVectorTiledLayer
        /// * ServiceFeatureTable
        /// * ExportVectorTilesTask
        /// * LocatorTask
        /// * GeodatabaseSyncTask
        /// * ClosestFacilityTask
        /// * RouteTask
        /// * ServiceAreaTask
        /// * ExportTileCacheTask
        /// - Since: 100.10.0
        public static string APIKey
        {
            get
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISRuntimeEnvironment_getAPIKey(errorHandler);
                
                ErrorManager.CheckError(errorHandler);
                
                return Interop.FromString(localResult);
            }
            set
            {
                var errorHandler = ErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISRuntimeEnvironment_setAPIKey(value, errorHandler);
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Properties
        
        #region Methods
        /// Enables/disables breaking on exceptions.
        /// 
        /// - Parameter enable: true if the runtime should break on an exception, false otherwise.
        /// - Since: 100.0.0
        internal static void EnableBreakOnException(bool enable)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_enableBreakOnException(enable, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Enables/disables memory leak detection.
        /// 
        /// - Remark: Disabling will cause the runtime to dump all of the object instances that were currently being tracked and
        /// it will not track object instances from the point of disabling.  Enabling leak detection will make the
        /// the runtime track all object instances allocated from the point of enabling.  By default, leak detection is turned off.
        /// - Parameter enable: true if the runtime should be tracking object allocations and deallocations, false otherwise.
        /// - Since: 100.0.0
        internal static void EnableLeakDetection(bool enable)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_enableLeakDetection(enable, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Enables/disables raising assertion.
        /// 
        /// - Remark: This is enabled by default in debug builds and disabled by default in release builds.
        /// If disable abort will not be called.
        /// - Parameter enable: true if the runtime should turn assertions on and abort, false otherwise.
        /// - Since: 100.2.0
        internal static void EnableRaiseAssertion(bool enable)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_enableRaiseAssertion(enable, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Enables/disables the assert/abort dialog on Windows Desktop.
        /// 
        /// - Parameter enable: true the assert/abort dialog should appear with abort, break and continue options. enable false if all asserts and errors should go to the debug console.
        /// - Since: 100.0.0
        internal static void EnableShowAssertDialog(bool enable)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_enableShowAssertDialog(enable, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Get the path of the directory containing the feature toggle file.
        /// 
        /// - Remark: Returns the path of the directory used to find the feature toggle file 'arcgis_runtime_feature_set.txt'.
        /// This directory is set by calling ArcGISRuntimeEnvironment.setFeatureToggleDirectory
        /// 
        /// Additionally when ArcGISRuntimeEnvironment.setInstallDirectory is called, if the feature toggle directory has not already been set,
        /// then it is set to the install directory.
        /// 
        /// The feature toggle file is plain text. Each line contains the name of a feature (no spaces) and '=' a boolean value.
        /// For example:
        /// <code>
        /// enable_rendering_engine_mr3d=true
        /// </code>
        /// - Returns: The location of the directory containing the feature toggle file.
        /// - Since: 100.8.0
        internal static string GetFeatureToggleDirectory()
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISRuntimeEnvironment_getFeatureToggleDirectory(errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return Interop.FromString(localResult);
        }
        
        /// Indicates if a specified feature is enabled in the feature toggle file.
        /// 
        /// - Remark: If the feature toggle file contains the specified name then toggle value is returned.
        /// Otherwise false is returned where the file or toggle does not exist.
        /// See ArcGISRuntimeEnvironment.setFeatureToggleDirectory and ArcGISRuntimeEnvironment.getFeatureToggleDirectory.
        /// - Parameter featureToggle: The name of the feature in the feature toggle file.
        /// - Returns: The location of the directory containing the feature toggle file.
        /// - Since: 100.8.0
        internal static bool IsFeatureEnabled(string featureToggle)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISRuntimeEnvironment_isFeatureEnabled(featureToggle, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// This will override the license watermark text with the beta text info.
        /// 
        /// - Remark: * 'true' the water mark will always appear even if you set a license
        /// * 'false' (default) the water mark will not appear. The developer license will appear if a license is not set.
        /// - Parameter set: Set to true if you wish the beta watermark to appear, false if you wish the license level text to appear.
        /// - Since: 100.0.0
        internal static void SetBetaWatermark(bool set)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setBetaWatermark(set, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Sets the location of the directory containing the feature toggle file and reads the content of the file.
        /// 
        /// - Remark: Sets the directory containing the feature toggle file 'arcgis_runtime_feature_set.txt'.
        /// If the file exists it will be read into memory. If the file is not formatted an error is returned, for example if a toggle is missing an '='.
        /// The feature toggles are used to enable or disable features that are tested ArcGISRuntimeEnvironment.isFeatureEnabled.
        /// 
        /// If the directory or file does not exist, no error is returned and tests for features will return false.
        /// This function should be called once at the start of runtime initialization.
        /// 
        /// Additionally when ArcGISRuntimeEnvironment.setInstallDirectory is called, if the feature toggle directory has not already been set,
        /// then it is set to the install directory.
        /// 
        /// The feature toggle file is plain text. Each line contains the name of a feature (no spaces) and '=' a boolean value.
        /// For example:
        /// <code>
        /// enable_rendering_engine_mr3d=true
        /// </code>
        /// - Parameter featureToggleDirectory: The path to the directory containing the feature toggle file.
        /// - Since: 100.8.0
        internal static void SetFeatureToggleDirectory(string featureToggleDirectory)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setFeatureToggleDirectory(featureToggleDirectory, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Set the location of the root folder for the deployment resources.
        /// 
        /// - Remark: This is used for the default location to find file resources as follows:
        /// - DirectX shaders default location.
        /// - <i><b>install_path</b></i>/resources/shaders
        /// - military dictionary symbol style default location
        /// - <i><b>install_path</b></i>/resources/symbols/mil2525c
        /// - navigation localized resources
        /// - <i><b>install_path</b></i>/resources/navigation
        /// - Parameter installPath: The path to the root folder of the deployment.
        /// - Since: 100.0.0
        internal static void SetInstallDirectory(string installPath)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setInstallDirectory(installPath, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Set the product name and version.
        /// 
        /// - Remark: Sets the product information to be used to build the user-agent string.
        /// This should be called before the runtime environment is created. Calling it after may have no effect.
        /// The values are global to the process and defaults to an empty string.
        /// - Since: 100.11.0
        internal static void SetProductInfo(string name, string version)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setProductInfo(name, version, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Sets the resources directory for the process.
        /// 
        /// - Remark: If not set, it will default to "<install_directory>\resources\".
        /// - Parameter resourcesPath: Full pathname of the resources directory.
        /// - Since: 100.9.0
        internal static void SetResourcesDirectory(string resourcesPath)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setResourcesDirectory(resourcesPath, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Set the runtime client type and version.
        /// 
        /// - Remark: This is to support a specific use case. The Unity game engine requires a different thread pool implementation and
        /// this allows to identify the runtime client and instantiate the right thread pool implementation at startup.
        /// This should be called before the runtime environment is created. Calling it after that has no effect.
        /// The values are global to the process and default to RuntimeClient.unknown and an empty string respectively when not set.
        /// Both values are also used to build the user-agent string.
        /// - SeeAlso: RuntimeClient
        /// - Since: 100.9.0
        internal static void SetRuntimeClient(Standard.RuntimeClient runtimeClient, string version)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setRuntimeClient(runtimeClient, version, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        
        /// Sets the temp directory for the process.
        /// 
        /// - Parameter tempPath: Full pathname of the temporary file.
        /// - Since: 100.0.0
        internal static void SetTempDirectory(string tempPath)
        {
            var errorHandler = ErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISRuntimeEnvironment_setTempDirectory(tempPath, errorHandler);
            
            ErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Events
        /// Set a global error handler.
        /// 
        /// - Remark: The global error handler can be overridden by a function error handler. At least one must be set.
        /// A exception will be thrown if which will cause a crash if the error handler has not been
        /// set globally or per function call.
        /// - SeeAlso: ErrorHandler
        /// - Since: 100.0.0
        internal static ArcGISRuntimeEnvironmentErrorEvent Error
        {
            get
            {
                return _errorHandler.Delegate;
            }
            set
            {
                _errorHandler.Delegate = value;
                
                var errorHandler = ErrorManager.CreateHandler();
                
                if (_errorHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISRuntimeEnvironment_setErrorCallback(ArcGISRuntimeEnvironmentErrorEventHandler.HandlerFunction, _errorHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISRuntimeEnvironment_setErrorCallback(null, IntPtr.Zero, errorHandler);
                }
                
                ErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal static ArcGISRuntimeEnvironmentErrorEventHandler _errorHandler = new ArcGISRuntimeEnvironmentErrorEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISRuntimeEnvironment_getAPIKey(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setAPIKey([MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_enableBreakOnException([MarshalAs(UnmanagedType.I1)]bool enable, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_enableLeakDetection([MarshalAs(UnmanagedType.I1)]bool enable, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_enableRaiseAssertion([MarshalAs(UnmanagedType.I1)]bool enable, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_enableShowAssertDialog([MarshalAs(UnmanagedType.I1)]bool enable, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern IntPtr RT_ArcGISRuntimeEnvironment_getFeatureToggleDirectory(IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISRuntimeEnvironment_isFeatureEnabled([MarshalAs(UnmanagedType.LPStr)]string featureToggle, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setBetaWatermark([MarshalAs(UnmanagedType.I1)]bool set, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setFeatureToggleDirectory([MarshalAs(UnmanagedType.LPStr)]string featureToggleDirectory, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setInstallDirectory([MarshalAs(UnmanagedType.LPStr)]string installPath, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setProductInfo([MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string version, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setResourcesDirectory([MarshalAs(UnmanagedType.LPStr)]string resourcesPath, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setRuntimeClient(Standard.RuntimeClient runtimeClient, [MarshalAs(UnmanagedType.LPStr)]string version, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setTempDirectory([MarshalAs(UnmanagedType.LPStr)]string tempPath, IntPtr errorHandler);
        
        [DllImport(Interop.Dll)]
        internal static extern void RT_ArcGISRuntimeEnvironment_setErrorCallback(ArcGISRuntimeEnvironmentErrorEventInternal error, IntPtr userData, IntPtr errorHandler);
        #endregion // P-Invoke Declarations
    }
}