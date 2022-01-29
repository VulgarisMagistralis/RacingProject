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
using Esri.ArcGISMapsSDK.Utils;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Camera;
using Esri.GameEngine.Elevation;
using Esri.GameEngine.Layers.Base;
using Esri.GameEngine.Location;
using Esri.GameEngine.RenderCommandQueue;
using Esri.GameEngine.View.State;
using System;

namespace Esri.GameEngine.View
{
	public class ArcGISRendererView
	{
		private ArcGISCamera camera;
		private GameEngine.Map.ArcGISMap map;

		public ArcGISCamera Camera
		{
			get
			{
				if (camera == null)
				{
					var location = GameEngineView.Camera.Location;

					var arcGISPosition = new ArcGISPosition(location.X, location.Y, location.Z);
					var arcGISRotation = new ArcGISRotation(GameEngineView.Camera.Pitch, GameEngineView.Camera.Roll, GameEngineView.Camera.Heading);

					camera = new ArcGISCamera("ArcGISRendererView", arcGISPosition, arcGISRotation);
				}

				return camera;
			}
			set
			{
				camera = value;

				var locationPoint = new Esri.ArcGISRuntime.Geometry.Point(camera.Position.X, camera.Position.Y, camera.Position.Z, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84());

				GameEngineView.Camera = new Esri.ArcGISRuntime.MapView.Camera(locationPoint, camera.Orientation.Heading, camera.Orientation.Pitch, camera.Orientation.Roll);
			}
		}

		public GameEngine.Map.ArcGISMap Map
		{
			get
			{
				return map;
			}
			set
			{
				map = value;

				GameEngineView.Map = map;
			}
		}

		public ArcGISRendererViewOptions Options
		{
			get
			{
				return GameEngineView.ViewOptions;
			}
			set
			{
				GameEngineView.ViewOptions = value;
			}
		}

		public event ArcGISElevationSourceViewStateChangeEventHandler ArcGISElevationSourceViewStateChanged;
		public event ArcGISLayerViewStateChangeEventHandler ArcGISLayerViewStateChanged;
		public event ArcGISRendererViewStateChangeEventHandler ArcGISRendererViewStateChanged;

		public delegate void ArcGISElevationSourceViewStateChangeEventHandler(object sender, Event.ArcGISElevationSourceViewStateEventArgs args);
		public delegate void ArcGISLayerViewStateChangeEventHandler(object sender, Event.ArcGISLayerViewStateEventArgs args);
		public delegate void ArcGISRendererViewStateChangeEventHandler(object sender, Event.ArcGISRendererViewStateEventArgs args);

		internal GameEngineView GameEngineView { get; set; }

		public ArcGISRendererView()
		{
			GameEngineView = new GameEngineView(GameEngineType.Unity, Esri.ArcGISRuntime.MapView.GlobeModel.Ellipsoid);

			// Wire events with GameEngineView
			GameEngineView.ViewViewStateChanged += OnViewViewStateChange;
			GameEngineView.ElevationSourceViewStateChanged += OnElevationSourceViewStateChange;
			GameEngineView.LayerViewStateChanged += OnLayerViewStateChange;
		}

		public void HandleLowMemoryWarning()
		{
			GameEngineView.HandleLowMemoryWarning();
		}

		internal void OnViewViewStateChange(GameEngineViewViewState state)
		{
			var data = new Esri.GameEngine.View.Event.ArcGISRendererViewStateEventArgs(ConvertEnum(state.Status), state.Error);
			ArcGISRendererViewStateChanged?.Invoke(this, data);
		}

		internal void OnElevationSourceViewStateChange(Elevation.Base.ArcGISElevationSource elevationSource, ArcGISRuntime.MapView.ElevationSourceViewState state)
		{
			var data = new Esri.GameEngine.View.Event.ArcGISElevationSourceViewStateEventArgs(elevationSource, ConvertEnum(state.Status), state.Error);
			ArcGISElevationSourceViewStateChanged?.Invoke(this, data);
		}

		internal void OnLayerViewStateChange(Layers.Base.ArcGISLayer ArcGISLayer, ArcGISRuntime.MapView.LayerViewState state)
		{
			var data = new Esri.GameEngine.View.Event.ArcGISLayerViewStateEventArgs(ArcGISLayer, ConvertEnum(state.Status), state.Error);
			ArcGISLayerViewStateChanged?.Invoke(this, data);
		}

		public Esri.GameEngine.View.State.ArcGISRendererViewState GetRendererViewState()
		{
			var response = GameEngineView.GetViewState();

			var viewState = new Esri.GameEngine.View.State.ArcGISRendererViewState(ConvertEnum(response.Status), response.Error);

			return viewState;
		}

		public Esri.GameEngine.View.State.ArcGISLayerViewState GetRendererViewState(ArcGISLayer layer)
		{
			var response = GameEngineView.GetViewState(layer);

			var viewState = new Esri.GameEngine.View.State.ArcGISLayerViewState(ConvertEnum(response.Status), response.Error);

			return viewState;
		}

		public Esri.GameEngine.View.State.ArcGISElevationSourceViewState GetRendererViewState(ArcGISImageElevationSource elevationSource)
		{
			var response = GameEngineView.GetViewState(elevationSource);

			var viewState = new Esri.GameEngine.View.State.ArcGISElevationSourceViewState(ConvertEnum(response.Status), response.Error);

			return viewState;
		}

		internal DecodedRenderCommandQueue GetCurrentDecodedRenderCommandQueue()
		{
			return new DecodedRenderCommandQueue(GameEngineView.RenderCommandServer.GetRenderCommands());
		}

		internal ArcGISRendererViewStatus ConvertEnum(GameEngineViewViewStatus status)
		{
			ArcGISRendererViewStatus newStatus = ArcGISRendererViewStatus.Active;
			Enum.TryParse<ArcGISRendererViewStatus>(status.ToString(), false, out newStatus);

			return newStatus;
		}

		internal ArcGISLayerViewStatus ConvertEnum(ArcGISRuntime.MapView.LayerViewStatus status)
		{
			ArcGISLayerViewStatus newStatus = ArcGISLayerViewStatus.Active;
			Enum.TryParse<ArcGISLayerViewStatus>(status.ToString(), false, out newStatus);

			return newStatus;
		}

		internal State.ArcGISElevationSourceViewStatus ConvertEnum(ArcGISRuntime.MapView.ElevationSourceViewStatus status)
		{
			ArcGISElevationSourceViewStatus newStatus = ArcGISElevationSourceViewStatus.Active;
			Enum.TryParse<ArcGISElevationSourceViewStatus>(status.ToString(), false, out newStatus) ;

			return newStatus;
		}

		public void SetViewportProperties(uint viewportWidthPixels, uint viewportHeightPixels, float horizontalFieldOfViewDegrees, float verticalFieldOfViewDegrees, float verticalDistortionFactor)
		{
			GameEngineView.SetViewportProperties(viewportWidthPixels, viewportHeightPixels, horizontalFieldOfViewDegrees, verticalFieldOfViewDegrees, verticalDistortionFactor);
		}

		public void UpdateMemoryAvailability(long totalSystemMemory, long inUseSystemMemory, long totalVideoMemory, long inUseVideoMemory)
		{
			GameEngineView.SetMemoryAvailability(totalSystemMemory, inUseSystemMemory, totalVideoMemory, inUseVideoMemory);
		}
	}
}
