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
using Esri.ArcGISMapsSDK.Renderer.GPUResources;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.HPFramework;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.SceneComponents
{
	internal class SceneComponent
	{
		public GameObject SceneComponentGameObject { get; }

		public GPUResourceMaterial Material
		{
			get
			{
				var material = SceneComponentGameObject.GetComponent<MeshRenderer>().material;

				if (material != null)
				{
					return new GPUResourceMaterial(material);
				}

				return null;
			}

			set
			{
				SceneComponentGameObject.GetComponent<MeshRenderer>().material = ((GPUResourceMaterial)value).NativeMaterial;
			}
		}

		public GPUResourceMesh Mesh
		{
			get
			{
				var mesh = SceneComponentGameObject.GetComponent<MeshFilter>().mesh;

				if (mesh != null)
				{
					return new GPUResourceMesh(mesh);
				}

				return null;
			}

			set
			{
				SceneComponentGameObject.GetComponent<MeshFilter>().mesh = ((GPUResourceMesh)value).NativeMesh;
			}
		}

		public Vector3d Location
		{
			set
			{
				var hpTransform = SceneComponentGameObject.GetComponent<HPTransform>();

				hpTransform.DUniversePosition = new DVector3(value.x, value.y, value.z);
				hpTransform.UniverseRotation = Quaternion.identity;
			}
		}

		public string Name
		{
			get
			{
				return SceneComponentGameObject.name;
			}

			set
			{
				SceneComponentGameObject.name = value;
			}
		}

		public bool IsVisible
		{
			get
			{
				return SceneComponentGameObject.activeInHierarchy;
			}

			set
			{
				SceneComponentGameObject.SetActive(value);
			}
		}

		public SceneComponent(GameObject gameObject)
		{
			SceneComponentGameObject = gameObject;
		}
	}
}
