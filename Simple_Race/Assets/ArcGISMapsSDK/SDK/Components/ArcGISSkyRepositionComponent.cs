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
using Esri.ArcGISMapsSDK.Utils.Math;
using UnityEngine;
#if !UNITY_ANDROID && !UNITY_IOS
using UnityEngine.Rendering;
#if USE_HDRP_PACKAGE
using UnityEngine.Rendering.HighDefinition;
#endif
#endif

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	public class ArcGISSkyRepositionComponent : MonoBehaviour
	{
		private Vector3d localOrigin = Vector3d.Zero;

#if !UNITY_ANDROID && !UNITY_IOS && USE_HDRP_PACKAGE
		private PhysicallyBasedSky sky = null;
		private Fog fog = null;
#endif

		public ArcGISCameraComponent CameraComponent = null;
		public ArcGISMapViewComponent MapViewComponent = null;

		private UnityEngine.Events.UnityAction MapViewComponentChangedAction;

		void Start()
		{
			if (CameraComponent == null)
			{
				Debug.LogError("CameraComponent cannot be null");
			}
			else if (MapViewComponent == null)
			{
				Debug.LogError("MapViewComponent cannot be null");
			}

#if !UNITY_ANDROID && !UNITY_IOS && USE_HDRP_PACKAGE
			if (GameObject.FindObjectOfType<Volume>())
			{
				Volume volume = GameObject.FindObjectOfType<Volume>();

				if (volume.profile.TryGet(out PhysicallyBasedSky tmpSky))
				{
					sky = tmpSky;
				}

				if (volume.profile.TryGet(out Fog tmpFog))
				{
					fog = tmpFog;
				}
			}
			MapViewComponentChangedAction += UpdateSky;
			MapViewComponent.RootChanged.AddListener(MapViewComponentChangedAction);
#endif
		}


		private void UpdateSky()
		{
#if !UNITY_ANDROID && !UNITY_IOS && USE_HDRP_PACKAGE
			var currentLocalOrigin = MapViewComponent.Scene.ToCartesianPosition(MapViewComponent.Position);

			if (localOrigin != currentLocalOrigin)
			{
				localOrigin = currentLocalOrigin;

				if (MapViewComponent.ViewMode == Esri.GameEngine.Map.ArcGISMapType.Local)
				{
					if (sky != null)
					{
						sky.sphericalMode.value = false;
						sky.planetaryRadius.value = 7e8f;
						sky.planetCenterPosition.value = new Vector3(0, (float)-(7e8 + localOrigin.y), 0);
					}
					if (fog != null && fog.enabled.value)
					{
						double height = localOrigin.y;
						fog.meanFreePath.value = (float)height + (float)CameraComponent.Far * 0.15f;
					}
				}
				else
				{
					if (sky != null)
					{
						sky.planetCenterPosition.value = new Vector3(0, (float)-localOrigin.Length(), 0);
					}
					if (fog != null && fog.enabled.value)
					{
						double height = MapViewComponent.RendererView.Camera.Position.Z;
						fog.baseHeight.value = -(float)height;
						fog.maximumHeight.value = 10000;
						fog.meanFreePath.value = (float)height + (float)CameraComponent.Far * 0.15f;
					}
				}
			}
#endif
		}
	}
}
