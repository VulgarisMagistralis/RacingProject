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
    internal delegate void GameEngineViewLayerViewStateChangedEvent(GameEngine.Layers.Base.ArcGISLayer layer, ArcGISRuntime.MapView.LayerViewState layerViewState);
    
    internal delegate void GameEngineViewLayerViewStateChangedEventInternal(IntPtr userData, IntPtr layer, IntPtr layerViewState);
    
    internal class GameEngineViewLayerViewStateChangedEventHandler : EventHandler<GameEngineViewLayerViewStateChangedEvent>
    {
        [MonoPInvokeCallback(typeof(GameEngineViewLayerViewStateChangedEventInternal))]
        internal static void HandlerFunction(IntPtr userData, IntPtr layer, IntPtr layerViewState)
        {
            if (userData == IntPtr.Zero)
            {
                return;
            }
    
            var callbackObject = (GameEngineViewLayerViewStateChangedEventHandler)((GCHandle)userData).Target;
    
            var callback = callbackObject.m_delegate;
    
            if (callback == null)
            {
                return;
            }
    
            GameEngine.Layers.Base.ArcGISLayer localLayer = null;
            
            if (layer != IntPtr.Zero)
            {
                localLayer = new GameEngine.Layers.Base.ArcGISLayer(layer);
            }
            
            ArcGISRuntime.MapView.LayerViewState localLayerViewState = null;
            
            if (layerViewState != IntPtr.Zero)
            {
                localLayerViewState = new ArcGISRuntime.MapView.LayerViewState(layerViewState);
            }
            
            callback(localLayer, localLayerViewState);
        }
    }
}