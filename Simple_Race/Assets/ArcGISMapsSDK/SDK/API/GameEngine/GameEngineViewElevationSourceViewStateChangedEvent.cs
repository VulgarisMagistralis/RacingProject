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
    internal delegate void GameEngineViewElevationSourceViewStateChangedEvent(GameEngine.Elevation.Base.ArcGISElevationSource elevationSource, ArcGISRuntime.MapView.ElevationSourceViewState elevationSourceViewState);
    
    internal delegate void GameEngineViewElevationSourceViewStateChangedEventInternal(IntPtr userData, IntPtr elevationSource, IntPtr elevationSourceViewState);
    
    internal class GameEngineViewElevationSourceViewStateChangedEventHandler : EventHandler<GameEngineViewElevationSourceViewStateChangedEvent>
    {
        [MonoPInvokeCallback(typeof(GameEngineViewElevationSourceViewStateChangedEventInternal))]
        internal static void HandlerFunction(IntPtr userData, IntPtr elevationSource, IntPtr elevationSourceViewState)
        {
            if (userData == IntPtr.Zero)
            {
                return;
            }
    
            var callbackObject = (GameEngineViewElevationSourceViewStateChangedEventHandler)((GCHandle)userData).Target;
    
            var callback = callbackObject.m_delegate;
    
            if (callback == null)
            {
                return;
            }
    
            GameEngine.Elevation.Base.ArcGISElevationSource localElevationSource = null;
            
            if (elevationSource != IntPtr.Zero)
            {
                localElevationSource = new GameEngine.Elevation.Base.ArcGISElevationSource(elevationSource);
            }
            
            ArcGISRuntime.MapView.ElevationSourceViewState localElevationSourceViewState = null;
            
            if (elevationSourceViewState != IntPtr.Zero)
            {
                localElevationSourceViewState = new ArcGISRuntime.MapView.ElevationSourceViewState(elevationSourceViewState);
            }
            
            callback(localElevationSource, localElevationSourceViewState);
        }
    }
}