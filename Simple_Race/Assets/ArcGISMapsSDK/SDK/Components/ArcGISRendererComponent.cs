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
using Esri.ArcGISMapsSDK.Renderer;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	public class ArcGISRendererComponent : MonoBehaviour
	{
		private ArcGISMapViewComponent arcGISMapViewComponent;
		private ArcGISRenderer arcGISRenderer = null;

		/// <summary>
		/// Services which make use of the Unity API and therefore must be implemented in the public source code.
		/// </summary>
		internal ISystemServices Services { get; private set; }

		void Awake()
		{
			Application.lowMemory += OnLowMemoryCallback;

#if UNITY_ANDROID
				Services = new UnityAndroidSystemServices();
#else
				Services = new UnitySystemServices();
#endif
		}

		void OnEnable()
		{
			arcGISMapViewComponent = gameObject.GetComponentInParent<ArcGISMapViewComponent>();

			if (arcGISMapViewComponent)
			{
				// Get memory availability from the game engine and pass on to the runtimecore
				var memoryAvailability = Services.GetMemoryAvailability();
				arcGISMapViewComponent.RendererView.UpdateMemoryAvailability(
					memoryAvailability.TotalSystemMemory.GetValueOrDefault(-1L),
					memoryAvailability.InUseSystemMemory.GetValueOrDefault(-1L),
					memoryAvailability.TotalVideoMemory.GetValueOrDefault(-1L),
					memoryAvailability.InUseVideoMemory.GetValueOrDefault(-1L));
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

			arcGISRenderer = new ArcGISRenderer(arcGISMapViewComponent.RendererView, gameObject);
		}

		void Update()
		{
			if (arcGISRenderer != null)
			{
				arcGISRenderer.Update();
			}
		}

		private void OnDestroy()
		{
			if (arcGISRenderer != null)
			{
				arcGISRenderer.Release();
				Application.lowMemory -= OnLowMemoryCallback;
			}
		}

		void OnTransformParentChanged()
		{
			OnEnable();
		}

		private void OnLowMemoryCallback()
		{
			if (arcGISMapViewComponent.RendererView != null)
			{
				arcGISMapViewComponent.RendererView.HandleLowMemoryWarning();
			}
		}
	}
}
