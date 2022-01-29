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

namespace Esri.GameEngine
{
    public enum GameEngineViewViewStatus
    {
        /// = 1, The view is active.
        /// 
        /// - Remark: A status of GameEngineViewViewStatus.active indicates that the view is being displayed.
        /// - Since: 100.11.0
        Active = 1,
        
        /// = 2, The map is not ready to display.
        /// 
        /// - Remark: This status applies if there is no map set on the view, or if the map has been set but doesn't contain enough information to be loaded.
        /// For instance, it may not have any layers or it may not have a basemap yet.
        /// - Since: 100.11.0
        MapNotReady = 2,
        
        /// = 4, There is not a valid camera, viewport properties, or spatial reference.
        /// 
        /// - Remark: A status of GameEngineViewViewStatus.noViewport indicates that the viewport properties, camera, or spatial reference are either invalid or not set yet.
        /// On initialization of the GameEngineView, if noViewport is reported after setting the camera and viewport properties, check that mapNotReady is not set. 
        /// Data from the Map is required to initialize the GameEngineView.
        /// - Since: 100.11.0
        NoViewport = 4,
        
        /// = 8, The view is loading.
        /// 
        /// - Remark: Once loading has completed, the view will display.
        /// If there was a problem loading, the status will be set to GameEngineViewViewStatus.error
        /// and the GameEngineViewViewState.error property will provide details on the specific problem.
        /// - Since: 100.11.0
        Loading = 8,
        
        /// = 16, The view has an error preventing it from rendering.
        /// 
        /// - Remark: When the status is GameEngineViewViewStatus.error, the view cannot be rendered.
        /// 
        /// The GameEngineViewViewState.error property will provide more details about the specific problem which was encountered.
        /// - Since: 100.11.0
        Error = 16,
        
        /// = 32, The view has encountered an error which may be temporary.
        /// 
        /// - Remark: When the status is GameEngineViewViewStatus.warning, the view may still be rendered although it may be rendered incompletely.
        /// It is possible for the status to be both GameEngineViewViewStatus.active and GameEngineViewViewStatus.warning.
        /// 
        /// The GameEngineViewViewState.error property will provide more details about the specific problem which was encountered.
        /// Depending on the type of problem, you could:
        /// - check your network connection
        /// - check whether an online service is experiencing problems
        /// - Since: 100.11.0
        Warning = 32
    };
}