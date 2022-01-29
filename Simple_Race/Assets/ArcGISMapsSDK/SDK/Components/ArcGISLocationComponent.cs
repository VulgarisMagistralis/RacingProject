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
using Esri.HPFramework;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(HPTransform))]
	public class ArcGISLocationComponent : MonoBehaviour
	{
		[SerializeField]
		private bool isInitialized = false;

		[SerializeField]
		private LatLon position;

		[SerializeField]
		private Rotator rotation;

		private ArcGISMapViewComponent arcGISMapViewComponent;
		private HPTransform hpTransform;
		private bool internalHasChanged = false;
		private DVector3 universePosition;
		private Quaternion universeRotation;

		public LatLon Position
		{
			get => position;
			set
			{
				position = value;

				internalHasChanged = true;
			}
		}

		public Rotator Rotation
		{
			get => rotation;
			set
			{
				rotation = value;

				internalHasChanged = true;
			}
		}

		public float Scale
		{
			get
			{
				return hpTransform.LocalScale.x;
			}
			set
			{
				hpTransform.LocalScale = new Vector3(value, value, value);
			}
		}

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

			if (internalHasChanged)
			{
				PushChangesToHPTransform();
			} else if (universePosition != hpTransform.DUniversePosition || universeRotation != hpTransform.UniverseRotation)
			{
				PullChangesFromHPTransform();
			}
		}

		void OnTransformParentChanged()
		{
			UpdateMapViewComponent();
		}

		private void PullChangesFromHPTransform()
		{
			isInitialized = true;

			universePosition = hpTransform.DUniversePosition;
			universeRotation = hpTransform.UniverseRotation;

			var cartesianPosition = new Vector3d(universePosition.x, universePosition.y, universePosition.z);
			var cartesianRotation = universeRotation.ToQuaterniond();

			this.position = arcGISMapViewComponent.Scene.FromCartesianPosition(cartesianPosition);
			this.rotation = arcGISMapViewComponent.Scene.FromCartesianRotation(cartesianPosition, cartesianRotation);
		}

		private void PushChangesToHPTransform()
		{
			internalHasChanged = false;

			var cartesianPosition = arcGISMapViewComponent.Scene.ToCartesianPosition(position);
			var cartesianRotation = arcGISMapViewComponent.Scene.ToCartesianRotation(cartesianPosition, rotation);

			universePosition = new DVector3(cartesianPosition.x, cartesianPosition.y, cartesianPosition.z);
			universeRotation = cartesianRotation.ToQuaternion();

			hpTransform.DUniversePosition = universePosition;
			hpTransform.UniverseRotation = universeRotation;
		}

		private void UpdateMapViewComponent()
		{
			arcGISMapViewComponent = gameObject.GetComponentInParent<ArcGISMapViewComponent>();

			if (arcGISMapViewComponent)
			{
				arcGISMapViewComponent.ViewModeChanged += PushChangesToHPTransform;

				if (!isInitialized && !internalHasChanged)
				{
					PullChangesFromHPTransform();
				} else
				{
					PushChangesToHPTransform();
				}
			}
		}
	}
}
