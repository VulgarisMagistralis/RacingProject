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

namespace Esri.ArcGISRuntime
{
    public enum LoadStatus
    {
        /// The object is fully loaded and ready to use.
        /// 
        /// - Since: 100.0.0
        Loaded = 0,
        
        /// The object is currently being loaded and some functionality may not work.
        /// 
        /// - Since: 100.0.0
        Loading = 1,
        
        /// The object failed to load and some functionality may not work.
        /// 
        /// - Since: 100.0.0
        FailedToLoad = 2,
        
        /// The object is not loaded and some functionality may not work.
        /// 
        /// - Since: 100.0.0
        NotLoaded = 3,
        
        /// Unknown load state. Only used if an error occurs and we have to return a value. Should not expose it.
        /// 
        /// - Since: 100.0.0
        Unknown = -1
    };
}