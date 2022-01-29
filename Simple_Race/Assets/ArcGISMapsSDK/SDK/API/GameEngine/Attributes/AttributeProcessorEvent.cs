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

namespace Esri.GameEngine.Attributes
{
    public delegate void AttributeProcessorEvent(Unity.ImmutableArray<Attribute> layerAttributes, Unity.ImmutableArray<VisualizationAttribute> visualizationAttributes);
    
    internal delegate void AttributeProcessorEventInternal(IntPtr userData, IntPtr layerAttributes, IntPtr visualizationAttributes);
    
    internal class AttributeProcessorEventHandler : EventHandler<AttributeProcessorEvent>
    {
        [MonoPInvokeCallback(typeof(AttributeProcessorEventInternal))]
        internal static void HandlerFunction(IntPtr userData, IntPtr layerAttributes, IntPtr visualizationAttributes)
        {
            if (userData == IntPtr.Zero)
            {
                return;
            }
    
            var callbackObject = (AttributeProcessorEventHandler)((GCHandle)userData).Target;
    
            var callback = callbackObject.m_delegate;
    
            if (callback == null)
            {
                return;
            }
    
            Unity.ImmutableArray<Attribute> localLayerAttributes = null;
            
            if (layerAttributes != IntPtr.Zero)
            {
                localLayerAttributes = new Unity.ImmutableArray<Attribute>(layerAttributes);
            }
            
            Unity.ImmutableArray<VisualizationAttribute> localVisualizationAttributes = null;
            
            if (visualizationAttributes != IntPtr.Zero)
            {
                localVisualizationAttributes = new Unity.ImmutableArray<VisualizationAttribute>(visualizationAttributes);
            }
            
            callback(localLayerAttributes, localVisualizationAttributes);
        }
    }
}