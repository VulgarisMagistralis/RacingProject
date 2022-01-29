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
    public enum ElevationSourceViewStatus
    {
        /// = 1, The elevation source in the view is active.
        /// 
        /// - Remark: A status of ElevationSourceViewStatus.active indicates that the elevation source is being displayed in the view.
        /// - Since: 100.11.0
        Active = 1,
        
        /// = 2, The elevation source in the view is not enabled.
        /// 
        /// - Since: 100.11.0
        NotEnabled = 2,
        
        /// = 4, The elevation source in the view is out of scale.
        /// 
        /// - Remark: A status of ElevationSourceViewStatus.outOfScale indicates that the view is zoomed outside of the scale range
        /// range of the elevation source. If the view is zoomed too far in (e.g. to a street level) it is beyond the max scale defined 
        /// for the elevation source. If the view has zoomed to far out (e.g. to global scale) it is beyond the min scale defined for the elevation source.
        /// - Since: 100.11.0
        OutOfScale = 4,
        
        /// = 8, The elevation source in the view is loading.
        /// 
        /// - Remark: Once loading has completed, the elevation source will be available for display in the view.
        /// If there was a problem loading the elevation source, the status will be set to ElevationSourceViewStatus.error
        /// and the ElevationSourceViewState.error property will provide details on the specific problem.
        /// - Since: 100.11.0
        Loading = 8,
        
        /// = 16, The elevation source in the view has an unrecoverable error.
        /// 
        /// - Remark: When the status is ElevationSourceViewStatus.error, the elevation source cannot be rendered in the view.
        /// For example, it may have failed to load, be an unsupported elevation source type or contain invalid data.
        /// 
        /// The ElevationSourceViewState.error property will provide more details about the specific problem which was encountered.
        /// Depending on the type of problem, you could:
        /// - call ElevationSource.retryLoad
        /// - remove the elevation source from the Map or Scene
        /// - inspect the data
        /// - Since: 100.11.0
        Error = 16,
        
        /// = 32, The elevation source in the view has encountered an error which may be temporary.
        /// 
        /// - Remark: When the status is ElevationSourceViewStatus.warning, the elevation source may still be displayed in the view.
        /// It is possible for the status to be both ElevationSourceViewStatus.active and ElevationSourceViewStatus.warning.
        /// 
        /// A warning status indicates that the elevation source has encountered a problem but may still be usable.
        /// 
        /// You should be aware that when a ElevationSourceViewStatus.warning is received, the elevation source may not be showing
        /// all data or it may be showing data which is not up-to-date.
        /// 
        /// The ElevationSourceViewState.error property will provide more details about the specific problem which was encountered.
        /// Depending on the type of problem, you could:
        /// - check your network connection
        /// - check whether an online service is experiencing problems
        /// - Since: 100.11.0
        Warning = 32
    };
}