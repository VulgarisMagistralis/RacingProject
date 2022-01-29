// COPYRIGHT 1995-2021 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Attn: Contracts and Legal Department
// Environmental Systems Research Institute, Inc.
// 380 New York Street
// Redlands, California 92373
// USA
//
// email: legal@esri.com
// Esri

using Esri.GameEngine.Layers.Base;

// Unity

using System;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.UX
{
	[Serializable]
	public class Layer
	{
		[SerializeField]
		public string Name;

		[SerializeField]
		public string Source;

		[SerializeField]
		public float Opacity;

		[SerializeField]
		public bool Visible;

		[SerializeField]
		public ArcGISLayerType LayerType;

		public Layer(string name, string source, bool visible, float opacity, ArcGISLayerType layerType)
		{
			Name = name;
			Source = source;
			Visible = visible;
			Opacity = opacity;
			LayerType = layerType;
		}
	}
}
