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

namespace Esri.ArcGISRuntime.MapView
{
    public enum LayerViewStatus
    {
        /// = 1, The layer in the view is active.
        /// 
        /// - Remark: A status of LayerViewStatus.active indicates that the layer is being displayed in the view.
        /// - Since: 100.0.0
        Active = 1,
        
        /// = 2, The layer in the view is not visible.
        /// 
        /// - Since: 100.0.0
        NotVisible = 2,
        
        /// = 4, The layer in the view is out of scale.
        /// 
        /// - Remark: A status of LayerViewStatus.outOfScale indicates that the view is zoomed outside of the scale range
        /// range of the layer. If the view is zoomed too far in (e.g. to a street level) it is beyond the max scale defined 
        /// for the layer. If the view has zoomed to far out (e.g. to global scale) it is beyond the min scale defined for the layer.
        /// - Since: 100.0.0
        OutOfScale = 4,
        
        /// = 8, The layer in the view is loading.
        /// 
        /// - Remark: Once loading has completed, the layer will be available for display in the view.
        /// If there was a problem loading the layer, the status will be set to LayerViewStatus.error
        /// and the LayerViewState.error property will provide details on the specific problem.
        /// - Since: 100.0.0
        Loading = 8,
        
        /// = 16, The layer in the view has an unrecoverable error.
        /// 
        /// - Remark: When the status is LayerViewStatus.error, the layer cannot be rendered in the view.
        /// For example, it may have failed to load, be an unsupported layer type or contain
        /// invalid data.
        /// 
        /// The LayerViewState.error property will provide more details about the specific
        /// problem which was encountered. Depending on the type of problem, you could:
        /// * Call Layer.retryLoad
        /// * Remove the layer from the Map or Scene
        /// * Inspect the data
        /// - Since: 100.0.0
        Error = 16,
        
        /// = 32, The layer in the view has encountered an error which may be temporary.
        /// 
        /// - Remark: When the status is LayerViewStatus.warning, the layer may still be displayed in the
        /// view. It is possible for the status to be both LayerViewStatus.active and
        /// LayerViewStatus.warning.
        /// 
        /// A warning status indicates that the layer has encountered a problem but may still be
        /// usable. For example, some tiles or features may be failing to load due to network
        /// failure or server error.
        /// 
        /// You should be aware that when a LayerViewStatus.warning is received, the layer may
        /// not be showing all data or it may be showing data which is not up-to-date.
        /// 
        /// The LayerViewState.error property will provide more details about the specific
        /// problem which was encountered. Depending on the type of problem, you could:
        /// * Check your network connection
        /// * Check whether an online service is experiencing problems
        /// - Since: 100.9.0
        Warning = 32
    };
}