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
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Camera;
using Esri.GameEngine.Location;
using Esri.HPFramework;
using System;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(HPTransform))]
	public class ArcGISCameraComponent : MonoBehaviour
	{
		private ArcGISCamera Camera;
		private ArcGISMapViewComponent arcGISMapViewComponent;
		private Bounds cameraBounds;
		private Camera cameraComponent = null;

		public double Near { get; private set; }

		public double Far { get; private set; }

		void Awake()
		{
			cameraComponent = GetComponent<Camera>();

			if (cameraComponent == null)
			{
				Debug.LogError("ArcGISCameraComponent must be attached to a Camera");
			}
		}

		void OnEnable()
		{
			arcGISMapViewComponent = gameObject.GetComponentInParent<ArcGISMapViewComponent>();

			if (arcGISMapViewComponent && arcGISMapViewComponent.ViewMode == GameEngine.Map.ArcGISMapType.Local)
			{
				// Camera's geo-position should not leave the bounds that are supported by the SR.
				cameraBounds = new Bounds(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(2.0f * 179.0f, 0.0f, 2.0f * 89.0f));
			}
		}

		void Start()
		{
			if (arcGISMapViewComponent == null)
			{
				Debug.LogError("An ArcGISMapViewComponent could not be found. Please make sure this GameObject is a child of a GameObject with an ArcGISMapViewComponent attached");

				enabled = false;
				return;
			}
		}

		private void Update()
		{
			if (arcGISMapViewComponent == null)
			{
				return;
			}

			var hpTransform = GetComponent<HPTransform>();

			var cartesianPosition = new Vector3d(hpTransform.DUniversePosition.x, hpTransform.DUniversePosition.y, hpTransform.DUniversePosition.z);
			var cartesianRotation = hpTransform.UniverseRotation.ToQuaterniond();

			var latLon = arcGISMapViewComponent.Scene.FromCartesianPosition(cartesianPosition);
			if (arcGISMapViewComponent.ViewMode == GameEngine.Map.ArcGISMapType.Local)
			{
				var closestPoint = cameraBounds.ClosestPoint(new Vector3((float)latLon.Longitude, (float)latLon.Altitude, (float)latLon.Latitude));
				latLon = new LatLon(closestPoint.z, closestPoint.x, latLon.Altitude);
			}
			var rotator = arcGISMapViewComponent.Scene.FromCartesianRotation(cartesianPosition, cartesianRotation);

			var arcGISPosition = new ArcGISPosition(latLon.Longitude, latLon.Latitude, latLon.Altitude, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84());
			var arcGISRotation = new ArcGISRotation(rotator.Pitch, rotator.Roll, rotator.Heading);

			if (arcGISMapViewComponent.RendererView.Camera.Position != arcGISPosition || arcGISMapViewComponent.RendererView.Camera.Orientation != arcGISRotation)
			{
				arcGISMapViewComponent.RendererView.Camera = new ArcGISCamera("ArcGISCameraComponent", arcGISPosition, arcGISRotation);
			}

			Near = arcGISMapViewComponent.Scene.GetCameraNearPlane(latLon.Altitude, cameraComponent.fieldOfView, cameraComponent.aspect);
			Far = arcGISMapViewComponent.Scene.GetCameraFarPlane(Near, latLon.Altitude);

			cameraComponent.farClipPlane = GeoUtils.EarthScaleToUnityScale(Far, arcGISMapViewComponent.Scene.MapType == Esri.GameEngine.Map.ArcGISMapType.Local, arcGISMapViewComponent.WorldScale);
			cameraComponent.nearClipPlane = GeoUtils.EarthScaleToUnityScale(Math.Min(500000.0, Near), arcGISMapViewComponent.Scene.MapType == Esri.GameEngine.Map.ArcGISMapType.Local, arcGISMapViewComponent.WorldScale);

			UpdateViewport();
		}

		void OnTransformParentChanged()
		{
			OnEnable();

			Update();
		}

		private void UpdateViewport()
		{
			var radAngle = cameraComponent.fieldOfView * MathUtils.DegreesToRadians;
			var radHFOV = 2 * Math.Atan(Math.Tan(radAngle / 2) * cameraComponent.aspect);
			var hFOV = MathUtils.RadiansToDegrees * radHFOV;

			arcGISMapViewComponent.RendererView.SetViewportProperties((uint)Screen.currentResolution.width, (uint)Screen.currentResolution.height, (float)hFOV, cameraComponent.fieldOfView, 1);
		}
	}
}
