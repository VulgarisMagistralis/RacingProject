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
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Map;
using Esri.GameEngine.Layers;
using Esri.GameEngine.Layers.Base;
using Esri.Unity;
using System.Collections.Generic;
using UnityEngine;

namespace ArcGISMapsSDK.Components
{
	[System.Serializable]
	public struct ArcGISMapLayer
	{
		public ArcGISLayerType Type;
		public string Url;
		public float Opacity;
	}

	[DisallowMultipleComponent]
	[RequireComponent(typeof(ArcGISRendererComponent))]
	public class ArcGISMapComponent : MonoBehaviour
	{
		public string APIKey;
		public string Basemap;

		public string Elevation;
		public List<ArcGISMapLayer> Layers;

		private ArcGISMapViewComponent arcGISMapViewComponent;

		void OnEnable()
		{
			arcGISMapViewComponent = gameObject.GetComponentInParent<ArcGISMapViewComponent>();
		}

		void Start()
		{
			if (arcGISMapViewComponent == null)
			{
				Debug.LogError("An ArcGISMapViewComponent could not be found. Please make sure this GameObject is a child of a GameObject with an ArcGISMapViewComponent attached");

				enabled = false;
				return;
			}

			var arcGISMap = new ArcGISMap(arcGISMapViewComponent.ViewMode);

			if (Basemap != "")
			{
				// Set the Basemap
				arcGISMap.Basemap = new ArcGISBasemap(Basemap, APIKey);
			}

			if (Elevation != "")
			{
				// Set the Elevation
				arcGISMap.Elevation = new ArcGISMapElevation(new Esri.GameEngine.Elevation.ArcGISImageElevationSource(Elevation, "Elevation", APIKey));
			}

			if (Layers.Count > 0)
			{
				foreach (var LayerDefinition in Layers)
				{
					ArcGISLayer layer = null;

					if (LayerDefinition.Url != "")
					{
						if (LayerDefinition.Type == ArcGISLayerType.ArcGIS3DModelLayer)
						{
							layer = new ArcGIS3DModelLayer(LayerDefinition.Url, APIKey);
						}
						else if (LayerDefinition.Type == ArcGISLayerType.ArcGISImageLayer)
						{
							layer = new ArcGISImageLayer(LayerDefinition.Url, APIKey);
						}
						else if (LayerDefinition.Type == ArcGISLayerType.ArcGISIntegratedMeshLayer)
						{
							layer = new ArcGISIntegratedMeshLayer(LayerDefinition.Url, APIKey);
						}
					}

					if (layer != null)
					{
						layer.Opacity = LayerDefinition.Opacity;

						arcGISMap.Layers.Add(layer);
					}
				}
			}

			arcGISMapViewComponent.RendererView.Map = arcGISMap;
		}
	}
}
