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
// ArcGISMapsSDK

using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.UX.Security;

// Esri

using Esri.GameEngine.Elevation;
using Esri.GameEngine.Extent;
using Esri.GameEngine.Layers;
using Esri.GameEngine.Layers.Base;
using Esri.GameEngine.Location;
using Esri.GameEngine.Map;
using Esri.GameEngine.View.Event;
using Esri.Unity;

// .Net

using System;
using System.Collections.Generic;

// Unity

using UnityEngine;

namespace Esri.ArcGISMapsSDK.UX
{
	public enum BaseMapType
	{
		Imagery,
		Streets,
		Topographic,
		NatGeo,
		Oceans,
		LightGrayCanvas,
		DarkGrayCanvas,
	}

	[DisallowMultipleComponent]
	public class ArcGISMapController : ArcGISMapViewComponent
	{
		[Serializable]
		public class MapLocation
		{
			public LatLon position;
			public Rotator rotation;
		}

		[SerializeField]
		private LatLon originLocation = new LatLon();

		public LatLon OriginLocation
		{
			get
			{
				originLocation = Position;
				return originLocation;
			}
			set
			{
				originLocation = value;
				Position = originLocation;
			}
		}

		[SerializeField]
		public MapLocation CameraLocation = new MapLocation();

		public string APIKey;

		public List<Layer> Layers = new List<Layer>();

		public BaseMapType BaseMap;

		public Extent Extent = new Extent();

		public bool TerrainElevation = true;

		[SerializeReference]
		public List<OAuthAuthenticationConfiguration> Configurations = new List<OAuthAuthenticationConfiguration>();

		public List<OAuthAuthenticationConfigurationMapping> ConfigMappings = new List<OAuthAuthenticationConfigurationMapping>();

		public void UpdateOriginPosition()
		{
			Position = originLocation;
		}

		private void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			CreateAuthConfigurations();

			var arcGISMap = new ArcGISMap(ViewMode);

			arcGISMap.Basemap = new ArcGISBasemap(GetBaseMapSource(), Esri.GameEngine.Layers.Base.ArcGISLayerType.ArcGISImageLayer, APIKey);

			if (TerrainElevation)
			{
				arcGISMap.Elevation = new ArcGISMapElevation(new ArcGISImageElevationSource("https://elevation3d.arcgis.com/arcgis/rest/services/WorldElevation3D/Terrain3D/ImageServer", "Elevation", APIKey));
			}

			CreateArcGISLayers(arcGISMap);

			if (ViewMode == ArcGISMapType.Local)
			{
				SetExtent(arcGISMap);
			}

			UpdateOriginPosition();
			var cameraComponent = InitializeArcGISCamera();

			if (cameraComponent == null)
			{
				return;
			}

			var rendererComponent = GetComponentInChildren<ArcGISRendererComponent>();

			if (rendererComponent == null)
			{
				Debug.LogError("An ArcGIS Renderer component is required to render the Map");
				return;
			}

			RendererView.Map = arcGISMap;

			// Adding events to show information on console
			RendererView.ArcGISElevationSourceViewStateChanged += (object sender, ArcGISElevationSourceViewStateEventArgs data) =>
			{
				Debug.Log("ArcGISElevationSourceViewState " + data.ArcGISElevationSource.Name + " changed to : " + data.Status.ToString());
			};

			RendererView.ArcGISLayerViewStateChanged += (object sender, ArcGISLayerViewStateEventArgs data) =>
			{
				Debug.Log("ArcGISLayerViewState " + data.ArcGISLayer.Name + " changed to : " + data.Status.ToString());
			};

			RendererView.ArcGISRendererViewStateChanged += (object sender, ArcGISRendererViewStateEventArgs data) =>
			{
				Debug.Log("ArcGISRendererViewState changed to : " + data.Status.ToString());
			};

#if !UNITY_ANDROID && !UNITY_IOS
			// Add Sky Component
			var currentSky = GameObject.FindObjectOfType<UnityEngine.Rendering.Volume>();
			if (currentSky)
			{
				ArcGISSkyRepositionComponent skyComponent = currentSky.gameObject.AddComponent<ArcGISSkyRepositionComponent>();
				skyComponent.CameraComponent = cameraComponent;
				skyComponent.MapViewComponent = this;
			}
#endif
		}

		private string GetBaseMapSource()
		{
			switch (BaseMap)
			{
				case BaseMapType.Imagery:
					return "https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer";
				case BaseMapType.Streets:
					return "https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
				case BaseMapType.Topographic:
					return "https://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer";
				case BaseMapType.NatGeo:
					return "https://services.arcgisonline.com/ArcGIS/rest/services/NatGeo_World_Map/MapServer";
				case BaseMapType.Oceans:
					return "https://services.arcgisonline.com/arcgis/rest/services/Ocean/World_Ocean_Base/MapServer";
				case BaseMapType.LightGrayCanvas:
					return "https://services.arcgisonline.com/arcgis/rest/services/Canvas/World_Light_Gray_Base/MapServer";
				case BaseMapType.DarkGrayCanvas:
					return "https://services.arcgisonline.com/arcgis/rest/services/Canvas/World_Dark_Gray_Base/MapServer";
				default:
					return "https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer";
			}
		}

		private void CheckLayerLoadStatus(ArcGISLayer layer)
		{
			layer.LoadStatusChanged = delegate (Esri.ArcGISRuntime.LoadStatus loadStatus)
			{
				if (loadStatus == Esri.ArcGISRuntime.LoadStatus.FailedToLoad)
				{
					var loadError = layer.LoadError;

					Debug.Log("Failed to load the ArcGISImageLayer: " + loadError.Message);
				}
			};
		}

		private void CreateAuthConfigurations()
		{
			Esri.GameEngine.Security.ArcGISAuthenticationManager.AuthenticationConfigurations.Clear();

			foreach (var config in Configurations)
			{
				Esri.GameEngine.Security.ArcGISAuthenticationConfiguration authenticationConfiguration;

				authenticationConfiguration = new Esri.GameEngine.Security.ArcGISOAuthAuthenticationConfiguration(config.ClientID.Trim(), "", config.RedirectURI.Trim());

				foreach (var mapping in ConfigMappings)
				{
					if (mapping.Configuration == config)
					{
						Esri.GameEngine.Security.ArcGISAuthenticationManager.AuthenticationConfigurations.Add(mapping.ServiceURL, authenticationConfiguration);
					}
				}
			}
		}

		private void SetExtent(ArcGISMap arcGISMap)
		{
			if (Extent.Width == 0 || (Extent.ExtentType == ExtentType.Rectangle && Extent.Length == 0))
			{
				Debug.LogWarning("An extent needs to have an area greater than zero");
			}

			try
			{
				arcGISMap.ClippingArea = CreateExent();
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}

		private ArcGISExtent CreateExent()
		{
			switch (Extent.ExtentType)
			{
				case ExtentType.Rectangle:
					return new ArcGISExtentRectangle(new ArcGISPosition(Extent.Longitude, Extent.Latitude, Extent.Altitude, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84()), Extent.Width, Extent.Length);
				case ExtentType.Square:
					return new ArcGISExtentRectangle(new ArcGISPosition(Extent.Longitude, Extent.Latitude, Extent.Altitude, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84()), Extent.Width, Extent.Width);
				case ExtentType.Circle:
					return new ArcGISExtentCircle(new ArcGISPosition(Extent.Longitude, Extent.Latitude, Extent.Altitude, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84()), Extent.Width);
				default:
					return new ArcGISExtentRectangle(new ArcGISPosition(Extent.Longitude, Extent.Latitude, Extent.Altitude, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84()), Extent.Width, Extent.Width);
			}
		}

		private void CreateArcGISLayers(ArcGISMap arcGISMap)
		{
			foreach (Layer viewLayer in Layers)
			{
				ArcGISLayer arcLayer = null;

				switch (viewLayer.LayerType)
				{
					case ArcGISLayerType.ArcGISImageLayer:
						arcLayer = new ArcGISImageLayer(viewLayer.Source, viewLayer.Name, viewLayer.Opacity / 100.0f, viewLayer.Visible, APIKey);
						break;
					case ArcGISLayerType.ArcGIS3DModelLayer:
						arcLayer = new ArcGIS3DModelLayer(viewLayer.Source, viewLayer.Name, viewLayer.Opacity / 100.0f, viewLayer.Visible, APIKey);
						break;
					case ArcGISLayerType.ArcGISIntegratedMeshLayer:
						arcLayer = new ArcGISIntegratedMeshLayer(viewLayer.Source, viewLayer.Name, viewLayer.Opacity / 100.0f, viewLayer.Visible, APIKey);
						break;
				}

				if (arcLayer != null)
				{
					arcGISMap.Layers.Add(arcLayer);
					CheckLayerLoadStatus(arcLayer);
				}
			}
		}

		private ArcGISCameraComponent InitializeArcGISCamera()
		{
			var cameraComponent = GetComponentInChildren<ArcGISCameraComponent>();

			if (cameraComponent == null)
			{
				Debug.LogError("An ArcGIS Camera component is required to render the Map");
				return null;
			}

			var cameraGameObject = cameraComponent.gameObject;
			var locationComponent = cameraGameObject.GetComponent<ArcGISLocationComponent>();

			locationComponent.Position = CameraLocation.position;
			locationComponent.Rotation = CameraLocation.rotation;

			return cameraComponent;
		}
	}
}
