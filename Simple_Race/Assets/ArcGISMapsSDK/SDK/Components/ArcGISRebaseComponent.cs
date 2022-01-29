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
using Esri.HPFramework;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(HPTransform))]
	public class ArcGISRebaseComponent : MonoBehaviour
	{
		private ArcGISMapViewComponent arcGISMapViewComponent;
		private HPTransform hpTransform;
		private DVector3 lastPosition;

		void OnEnable()
		{
			hpTransform = GetComponent<HPTransform>();

			UpdateMapViewComponent();
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

		private void LateUpdate()
		{
			if (arcGISMapViewComponent == null)
			{
				return;
			}

			if (lastPosition != hpTransform.DUniversePosition)
			{
				lastPosition = hpTransform.DUniversePosition;

				var cartesianPosition = new Vector3d(lastPosition.x, lastPosition.y, lastPosition.z);

				arcGISMapViewComponent.Position = arcGISMapViewComponent.Scene.FromCartesianPosition(cartesianPosition);
			}
		}

		void OnTransformParentChanged()
		{
			UpdateMapViewComponent();
		}

		private void UpdateMapViewComponent()
		{
			arcGISMapViewComponent = gameObject.GetComponentInParent<ArcGISMapViewComponent>();
		}
	}
}
