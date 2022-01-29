// Copyright 2021 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Extent;
using Esri.GameEngine.Location;
using Esri.GameEngine.View.Event;
using Esri.Unity;
using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

public class APIScene_NY : MonoBehaviour
{
	[ReadOnlyWhenPlaying]
	public Esri.GameEngine.Map.ArcGISMapType mapType = Esri.GameEngine.Map.ArcGISMapType.Local;

	public enum AttributeType
	{
		None,
		ConstructionYear,
		BuildingName
	};

	[ReadOnlyWhenPlaying]
	public AttributeType attributeType = AttributeType.None;

	private Esri.GameEngine.Attributes.AttributeProcessor attributeProcessor;

	void Start()
	{
		// API Key
		string apiKey = ""; // "6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b";

		// Create the Map Document
		var arcGISMap = new Esri.GameEngine.Map.ArcGISMap(mapType);

		// Set the Basemap
		arcGISMap.Basemap = new Esri.GameEngine.Map.ArcGISBasemap("https://www.arcgis.com/sharing/rest/content/items/8d569fbc4dc34f68abae8d72178cee05/data", apiKey);

		// Create the Elevation
		arcGISMap.Elevation = new Esri.GameEngine.Map.ArcGISMapElevation(new Esri.GameEngine.Elevation.ArcGISImageElevationSource("https://elevation3d.arcgis.com/arcgis/rest/services/WorldElevation3D/Terrain3D/ImageServer", "Elevation", apiKey));

		// Create layers
		var layer_1 = new Esri.GameEngine.Layers.ArcGISImageLayer("https://tiles.arcgis.com/tiles/nGt4QxSblgDfeJn9/arcgis/rest/services/UrbanObservatory_NYC_TransitFrequency/MapServer", "MyLayer_1", 1.0f, true, apiKey);
		arcGISMap.Layers.Add(layer_1);

		var layer_2 = new Esri.GameEngine.Layers.ArcGISImageLayer("https://tiles.arcgis.com/tiles/nGt4QxSblgDfeJn9/arcgis/rest/services/New_York_Industrial/MapServer", "MyLayer_2", 1.0f, true, apiKey);
		arcGISMap.Layers.Add(layer_2);

		var layer_3 = new Esri.GameEngine.Layers.ArcGIS3DModelLayer("https://tiles.arcgis.com/tiles/P3ePLMYs2RVChkJx/arcgis/rest/services/Buildings_NewYork_17/SceneServer", "MyLayer_4", 1.0f, true, apiKey);
		arcGISMap.Layers.Add(layer_3);

		if (attributeType == AttributeType.ConstructionYear)
		{
			Setup3DAttributesFloatAndIntegerType(layer_3);
		}
		else if (attributeType == AttributeType.BuildingName)
		{
			Setup3DAttributesOtherType(layer_3);
		}

		var layer_4 = new Esri.GameEngine.Layers.ArcGISImageLayer("https://tiles.arcgis.com/tiles/4yjifSiIG17X0gW4/arcgis/rest/services/NewYorkCity_PopDensity/MapServer", "MyLayer_3", 1.0f, true, apiKey);
		arcGISMap.Layers.Add(layer_4);

		// Remove a layer
		arcGISMap.Layers.Remove(arcGISMap.Layers.IndexOf(layer_4));

		// Update properties
		layer_1.Opacity = 0.9f;
		layer_2.Opacity = 0.6f;
		layer_4.Opacity = 1.0f;

		if (mapType == Esri.GameEngine.Map.ArcGISMapType.Local)
		{
			// Create Circle Extent
			var extentCenter = new ArcGISPosition(-74.054921, 40.691242, 3000, Esri.ArcGISRuntime.Geometry.SpatialReference.WGS84());
			var extent = new ArcGISExtentCircle(extentCenter, 100000);

			try
			{
				arcGISMap.ClippingArea = extent;
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}

		var arcGISMapViewComponent = gameObject.AddComponent<ArcGISMapViewComponent>();

		arcGISMapViewComponent.Position = new LatLon(40.730610, -73.935242, 0);
		arcGISMapViewComponent.ViewMode = mapType;

		var cameraGameObject = Camera.main.gameObject;
		var cameraComponent = cameraGameObject.AddComponent<ArcGISCameraComponent>();
		var locationComponent = cameraGameObject.AddComponent<ArcGISLocationComponent>();
		cameraGameObject.AddComponent<ArcGISCameraControllerComponent>();
		cameraGameObject.AddComponent<ArcGISRebaseComponent>();

		locationComponent.Position = new LatLon(40.691242, -74.054921, 3000);
		locationComponent.Rotation = new Rotator(65, 68, 0);

		var rendererGameObject = new GameObject("ArcGISMap");
		rendererGameObject.AddComponent<ArcGISRendererComponent>();

		cameraGameObject.transform.SetParent(arcGISMapViewComponent.transform, false);
		rendererGameObject.transform.SetParent(arcGISMapViewComponent.transform, false);

		arcGISMapViewComponent.RendererView.Map = arcGISMap;

		// Adding events to show information on console
		arcGISMapViewComponent.RendererView.ArcGISElevationSourceViewStateChanged += (object sender, ArcGISElevationSourceViewStateEventArgs data) =>
		{
			Debug.Log("ArcGISElevationSourceViewState " + data.ArcGISElevationSource.Name + " changed to : " + data.Status.ToString());
		};

		arcGISMapViewComponent.RendererView.ArcGISLayerViewStateChanged += (object sender, ArcGISLayerViewStateEventArgs data) =>
		{
			Debug.Log("ArcGISLayerViewState " + data.ArcGISLayer.Name + " changed to : " + data.Status.ToString());
		};

		arcGISMapViewComponent.RendererView.ArcGISRendererViewStateChanged += (object sender, ArcGISRendererViewStateEventArgs data) =>
		{
			Debug.Log("ArcGISRendererViewState changed to : " + data.Status.ToString());
		};

#if !UNITY_ANDROID && !UNITY_IOS
		// Add Sky Component
		var currentSky = GameObject.FindObjectOfType<UnityEngine.Rendering.Volume>();
		if (currentSky)
		{
			ArcGISSkyRepositionComponent skyComponent = currentSky.gameObject.AddComponent<ArcGISSkyRepositionComponent>();
			skyComponent.CameraComponent = cameraComponent;
			skyComponent.MapViewComponent = arcGISMapViewComponent;
		}
#endif
	}

	// This function is an example of how to use attributes WITHOUT the attribute processor
	void Setup3DAttributesFloatAndIntegerType(Esri.GameEngine.Layers.ArcGIS3DModelLayer layer)
	{
		// Setup an array with the attributes we want forwarded to the material below
		// Because CNSTRCT_YR is of type esriFieldTypeInteger the values can be passed directly to the material.
		// See Setup3DAttributesOtherType below for an example on how to pass other types to the material.
		// esriFieldTypeSingle, esriFieldTypeSmallInteger, esriFieldTypeInteger and esriFieldTypeDouble can be passed
		// directly to the shader without processing. Although esriFieldTypeDouble is converted to a float so it is a lossy conversion.
		var layerAttributes = ImmutableArray<String>.CreateBuilder();
		layerAttributes.Add("CNSTRCT_YR");
		layer.SetAttributesToVisualize(layerAttributes.MoveToArray());

		// Set the material we will use to visualize this layer on the layer. In Unity open this material to see exactly how that is accomplished.
		// As a note you can use this function alone to change the material used to render all of the buildings.
		layer.MaterialReference = new Material(Resources.Load<Material>("Materials/" + DetectRenderPipeline() + "/ConstructionYearRenderer"));
	}

	// This function is an example of how to use attributes WITH the attribute processor
	void Setup3DAttributesOtherType(Esri.GameEngine.Layers.ArcGIS3DModelLayer layer)
	{
		// Setup an array with the attributes we want forwarded to the material below
		// Because NAME is of type esriFieldTypeString we will need to configure the AttributeProcessor to pass
		// usable and meaningful values to the shader.
		var layerAttributes = ImmutableArray<String>.CreateBuilder();
		layerAttributes.Add("NAME");

		// The attribute description is the buffer that is output to the shader. Think of "NAME" in the layer attributes as an input to the
		// attribute processor while "IsBuildingOfInterest" is how we are choosing to convert "NAME" into a type usable in the shader with
		// values we can use to render the models in a way we see fit.
		// In this case we are using "IsBuildingOfInterest" to output either a 0 or a 1 depending on if the buildings "NAME" is a name of interest.
		var renderAttributeDescriptions = ImmutableArray<Esri.GameEngine.Attributes.VisualizationAttributeDescription>.CreateBuilder();
		renderAttributeDescriptions.Add(new Esri.GameEngine.Attributes.VisualizationAttributeDescription("IsBuildingOfInterest", Esri.GameEngine.Attributes.VisualizationAttributeType.Float32));

		// The attribute processor is what does the work on the CPU of converting the attribute into a value that can be used in the shader which is executed on the GPU.
		// Integers and floats can be processed in the same way as other types although it is not normally necessary.
		attributeProcessor = new Esri.GameEngine.Attributes.AttributeProcessor();

		attributeProcessor.ProcessEvent += (ImmutableArray<Esri.GameEngine.Attributes.Attribute> layerNodeAttributes, ImmutableArray<Esri.GameEngine.Attributes.VisualizationAttribute> renderNodeAttributes) =>
		{
			// Buffers will be provided in the same order they appear in the layer metadata. So if layerAttributes had an additional element it would be at inputAttributes.At(1)
			var nameAttribute = layerNodeAttributes.At(0);
			// The outputVisualizationAttributes array expects that its data is indexed the same way as the attributeDescriptions above.
			var isBuildingOfInterestAttribute = renderNodeAttributes.At(0);

			var isBuildingOfInterestBuffer = isBuildingOfInterestAttribute.Data;
			var isBuildingOfInterestData = isBuildingOfInterestBuffer.Reinterpret<float>(sizeof(byte));

			// Go over each attribute and if its name is one of the four buildings of interest set its "isBuildingOfInterest" value to 1, otherwise set it to 0.
			ForEachString(nameAttribute, (string element, Int32 index) =>
			{
				isBuildingOfInterestData[index] = IsBuildingOfInterest(element);
			});
		};

		// Pass the layer attributes, attribute descriptions and the attribute processor to the layer
		layer.SetAttributesToVisualize(layerAttributes.MoveToArray(), renderAttributeDescriptions.MoveToArray(), attributeProcessor);

		// Check out this material in Unity to learn what work is needed in the material to enable this work
		layer.MaterialReference = new Material(Resources.Load<Material>("Materials/" + DetectRenderPipeline() + "/BuildingNameRenderer"));
	}

	// Checks if the building name is a building of interest
	int IsBuildingOfInterest(string element)
	{
		if (element == null)
		{
			return 0;
		}
		else if (element.Equals("Empire State Building") || element.Equals("Chrysler Building") || element.Equals("Tower 1 World Trade Ctr") ||
				element.Equals("One Chase Manhattan Plaza"))
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	// ForEachString takes care of converting the attribute buffer into a readable string value
	void ForEachString(Esri.GameEngine.Attributes.Attribute attribute, Action<string, Int32> predicate)
	{
		unsafe
		{
			var buffer = attribute.Data;
			var unsafePtr = NativeArrayUnsafeUtility.GetUnsafePtr(buffer);
			var metadata = (int*)unsafePtr;

			var count = metadata[0];

			// First integer = number of string on this array
			// Second integer = sum(strings length)
			// Next N-values (N = value of index 0 ) = Length of each string

			IntPtr stringPtr = (IntPtr)unsafePtr + (2 + count) * sizeof(int);

			for (var i = 0; i < count; ++i)
			{
				string element = null;

				// If the length of the string element is 0, it means the element is null
				if (metadata[2 + i] > 0)
				{
					element = Marshal.PtrToStringAnsi(stringPtr, metadata[2 + i] - 1);
				}

				predicate(element, i);

				stringPtr += metadata[2 + i];
			}
		}
	}

	string DetectRenderPipeline()
	{
		if (GraphicsSettings.renderPipelineAsset != null)
		{
			var renderType = GraphicsSettings.renderPipelineAsset.GetType().ToString();

			if (renderType == "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset")
			{
				return "URP";
			}
			else if (renderType == "UnityEngine.Rendering.HighDefinition.HDRenderPipelineAsset")
			{
				return "HDRP";
			}
		}

		return "Legacy";
	}
}
