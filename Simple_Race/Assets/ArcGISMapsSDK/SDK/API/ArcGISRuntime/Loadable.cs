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

using System;

namespace Esri.ArcGISRuntime
{
    public partial interface Loadable
    {
        #region Properties
        /// The load error.
        /// 
        /// - SeeAlso: Error
        /// - Since: 100.0.0
        Exception LoadError
        {
            get;
        }
        
        /// The load status.
        /// 
        /// - SeeAlso: LoadStatus
        /// - Since: 100.0.0
        LoadStatus LoadStatus
        {
            get;
        }
        #endregion // Properties
        
        #region Methods
        /// Cancels loading metadata for the object.
        /// 
        /// - Remark: Cancels loading the metadata if the object is loading and always calls Loadable.doneLoading.
        /// - Since: 100.0.0
        void CancelLoad();
        
        /// Loads the metadata for the object asynchronously.
        /// 
        /// - Remark: Loads the metadata if the object is not loaded and always calls Loadable.doneLoading.
        /// 
        /// DOCUMENTATION FOR THE ArcGISFeature IMPLEMENTATION OF Loadable.load ONLY
        /// 
        /// An ArcGISFeature is loadable. Depending on a ServiceFeatureTable's feature request mode, queries on the table can return ArcGISFeature objects which have
        /// the minimum set of attributes required for rendering and labeling (and geometries do not include m-values). See ServiceFeatureTable.queryFeaturesAsync(QueryParameters, QueryFeatureFields), FeatureRequestMode.onInteractionCache, and FeatureRequestMode.onInteractionNoCache.
        /// To access all attributes in the returned ArcGISFeatures, call this method on each feature. For alternative ways to load all attributes in the returned features, see ServiceFeatureTable.loadOrRefreshFeaturesAsync(MutableArray<Feature>) or ServiceFeatureTable.queryFeaturesAsync(QueryParameters, QueryFeatureFields).
        /// - Since: 100.0.0
        void Load();
        
        /// Loads or retries loading metadata for the object asynchronously.
        /// 
        /// - Remark: Will retry loading the metadata if the object is failed to load. Will load the object if it is not loaded. Will not retry to load the object if the object is loaded.
        /// Will always call the done loading if this is called.
        /// - Since: 100.0.0
        void RetryLoad();
        #endregion // Methods
        
        #region Events
        /// Callback, called when the object is done loading.
        /// 
        /// - Since: 100.0.0
        LoadableDoneLoadingEvent DoneLoading
        {
            get;
            set;
        }
        
        /// Callback, called when the loadable load status changed.
        /// 
        /// - Since: 100.0.0
        LoadableLoadStatusChangedEvent LoadStatusChanged
        {
            get;
            set;
        }
        #endregion // Events
    }
}