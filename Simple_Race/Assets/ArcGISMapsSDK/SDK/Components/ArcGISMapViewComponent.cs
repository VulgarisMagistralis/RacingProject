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
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Map;
using Esri.GameEngine.View;
using Esri.HPFramework;
using UnityEngine;
using UnityEngine.Events;

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(HPRoot))]
	public class ArcGISMapViewComponent : MonoBehaviour
	{
		[SerializeField]
		private bool isInitialized = false;

		[SerializeField]
		private LatLon position;

		[SerializeField]
		private ArcGISMapType viewMode = ArcGISMapType.Local;

		private ArcGISRendererView rendererView = null;
		private HPRoot hpRoot;
		private bool internalHasChanged = false;
		private Scene scene;
		private DVector3 universePosition;
		private Quaternion universeRotation;

		public delegate void ViewModeChangedEventHandler();

		public event ViewModeChangedEventHandler ViewModeChanged;

		public UnityEvent RootChanged = new UnityEvent();

		public LatLon Position
		{
			get => position;
			set
			{
				position = value;

				internalHasChanged = true;
			}
		}

		public ArcGISRendererView RendererView
		{
			get
			{
				if (rendererView == null)
				{
					rendererView = new ArcGISRendererView();
				}

				return rendererView;
			}
		}

		public Scene Scene
		{
			get
			{
				if (scene == null)
				{
					scene = new Scene(ViewMode);
				}

				return scene;
			}
		}

		public ArcGISMapType ViewMode
		{
			get => viewMode;
			set
			{
				if (viewMode != value)
				{
					viewMode = value;

					OnViewModeChanged();
				}
			}
		}

		public double WorldScale
		{
			get
			{
				return viewMode == ArcGISMapType.Global ? GeoUtils.EarthRadius : GeoUtils.EarthEquatorLongitude;
			}
		}

		void OnEnable()
		{
			hpRoot = GetComponent<HPRoot>();

			if (!isInitialized)
			{
				PullChangesFromHPRoot();
			} else
			{
				PushChangesToHPRoot();
			}
		}

		private void Update()
		{
			if (internalHasChanged)
			{
				PushChangesToHPRoot();
			} else if (universePosition != hpRoot.DRootUniversePosition || universeRotation != hpRoot.RootUniverseRotation)
			{
				PullChangesFromHPRoot();
			}
		}

		private void OnViewModeChanged()
		{
			scene = new Scene(ViewMode);

			PushChangesToHPRoot();

			if (ViewModeChanged != null)
			{
				ViewModeChanged();
			}
		}

		private void PullChangesFromHPRoot()
		{
			isInitialized = true;

			universePosition = hpRoot.DRootUniversePosition;
			universeRotation = hpRoot.RootUniverseRotation;

			var cartesianPosition = new Vector3d(universePosition.x, universePosition.y, universePosition.z);

			this.position = Scene.FromCartesianPosition(cartesianPosition);
		}

		private void PushChangesToHPRoot()
		{
			internalHasChanged = false;

			var cartesianPosition = Scene.ToCartesianPosition(position);
			var tangentToWorld = Scene.GetENUReference(cartesianPosition).ToQuaterniond();

			universePosition = new DVector3(cartesianPosition.x, cartesianPosition.y, cartesianPosition.z);
			universeRotation = tangentToWorld.ToQuaternion();

			hpRoot.DRootUniversePosition = universePosition;
			hpRoot.RootUniverseRotation = universeRotation;

			RootChanged.Invoke();
		}
	}
}
