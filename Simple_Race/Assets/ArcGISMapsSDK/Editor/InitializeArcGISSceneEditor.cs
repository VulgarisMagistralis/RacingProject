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
using Esri.ArcGISMapsSDK.UX;

// Unity

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class InitializeArcGISSceneEditor : MonoBehaviour
{
	[MenuItem("ArcGIS Maps SDK/Initialize Scene")]
	static void Initialize()
	{
		var sceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();

		foreach (var sceneObject in sceneObjects)
		{
			var mapController = sceneObject.GetComponentInChildren<ArcGISMapController>();

			if (mapController)
			{
				Selection.activeGameObject = mapController.gameObject;
				return;
			}
		}

		CreateMapControllerInstance();
	}

	static void CreateMapControllerInstance()
	{
		LatLon position = new LatLon(40.691242, -74.054921, 3000);
		Rotator rotation = new Rotator(65, 68, 0);

		// Init ArcGISMapController

		var mapControllerObj = new GameObject("ArcGISMapController");
		var mapController = mapControllerObj.AddComponent<ArcGISMapController>();
		mapControllerObj.AddComponent<OAuthChallengeHandlersInitializer>();

		// Init ArcGISCam

		GameObject arcGISCamObj = null;

		if (Camera.main == null)
		{
			arcGISCamObj = new GameObject("ArcGISCamera");
			arcGISCamObj.AddComponent<Camera>();
			arcGISCamObj.tag = "MainCamera";
		}
		else
		{
			arcGISCamObj = Camera.main.gameObject;
		}

		arcGISCamObj.transform.parent = mapControllerObj.transform;

		arcGISCamObj.AddComponent<ArcGISCameraComponent>();

		var arcGISCamController = arcGISCamObj.AddComponent<ArcGISCameraControllerComponent>();
		arcGISCamController.MaxSpeed = 2000000;
		arcGISCamController.MinSpeed = 1000;

		var locationComponent = arcGISCamObj.AddComponent<ArcGISLocationComponent>();
		locationComponent.Position = position;
		locationComponent.Rotation = rotation;
		arcGISCamObj.AddComponent<ArcGISRebaseComponent>();

		// Init ArcGISMap and Renderer

		var arcGISMapObj = new GameObject("ArcGISMap");
		arcGISMapObj.transform.parent = mapControllerObj.transform;
		arcGISMapObj.AddComponent<ArcGISRendererComponent>();

		// Set up default MapController values

		mapController.ViewMode = Esri.GameEngine.Map.ArcGISMapType.Global;

		mapController.OriginLocation = position;
		mapController.UpdateOriginPosition();

		mapController.CameraLocation.position = position;
		mapController.CameraLocation.rotation = rotation;

		// Select ArcGISMapController

		Selection.activeGameObject = mapController.gameObject;
	}
}
