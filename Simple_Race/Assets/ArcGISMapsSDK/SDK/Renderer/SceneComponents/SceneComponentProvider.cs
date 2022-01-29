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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Esri.ArcGISMapsSDK.Renderer.SceneComponents
{
	internal class SceneComponentProvider
	{
		private readonly Dictionary<uint, SceneComponent> activeSceneComponents = new Dictionary<uint, SceneComponent>();
		private readonly List<SceneComponent> freeSceneComponents = new List<SceneComponent>();

		private readonly GameObject unused = null;

		private readonly GameObject parent;

		public IReadOnlyDictionary<uint, SceneComponent> SceneComponents => activeSceneComponents;

		public SceneComponentProvider(int initSize, GameObject parent)
		{
			this.parent = parent;

			unused = new GameObject();
			unused.name = "UnusedPoolGOs";
			unused.transform.SetParent(parent.transform, false);

			for (int i = 0; i < initSize; i++)
			{
				var sceneComponent = new SceneComponent(CreateGameObject(i));
				sceneComponent.SceneComponentGameObject.transform.SetParent(unused.transform, false);
				freeSceneComponents.Add(sceneComponent);
			}
		}

		public SceneComponent CreateSceneComponent(uint id)
		{
			if (freeSceneComponents.Count > 0)
			{
				var sceneComponent = freeSceneComponents[0];
				sceneComponent.SceneComponentGameObject.transform.SetParent(parent.transform, false);
				sceneComponent.IsVisible = false;
				sceneComponent.Name = "ArcGISGameObject_" + id;

				freeSceneComponents.RemoveAt(0);
				activeSceneComponents.Add(id, sceneComponent);

				return sceneComponent;
			}
			else
			{
				var sceneComponent = new SceneComponent(CreateGameObject(activeSceneComponents.Count + freeSceneComponents.Count));
				sceneComponent.SceneComponentGameObject.transform.SetParent(parent.transform, false);
				sceneComponent.Name = "ArcGISGameObject_" + id;
				activeSceneComponents.Add(id, sceneComponent);

				return sceneComponent;
			}
		}

		public void DestroySceneComponent(uint id)
		{
			var sceneComponent = activeSceneComponents[id];

			sceneComponent.SceneComponentGameObject.transform.SetParent(unused.transform, false);
			sceneComponent.IsVisible = false;

			activeSceneComponents.Remove(id);
			freeSceneComponents.Add(sceneComponent);
		}

		private static GameObject CreateGameObject(int id)
		{
			var gameObject = new GameObject();
			gameObject.name = "ArcGISGameObject" + id;
			gameObject.SetActive(false);
			var renderer = gameObject.AddComponent<MeshRenderer>();
			renderer.shadowCastingMode = ShadowCastingMode.TwoSided;
			renderer.enabled = true;
			gameObject.AddComponent<MeshFilter>();
			gameObject.AddComponent<HPTransform>();

			return gameObject;
		}
	}
}
