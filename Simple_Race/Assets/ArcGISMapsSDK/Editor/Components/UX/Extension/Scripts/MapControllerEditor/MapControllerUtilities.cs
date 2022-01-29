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

using Esri.ArcGISMapsSDK.UX;
using Esri.GameEngine.Layers.Base;

// Unity

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArcGISMapsSDK.Editor
{
	public static class MapControllerUtilities
	{
		public static Slider CreateSlider(VisualElement element, string sliderName, float defaultValue)
		{
			Slider slider = element.Query<Slider>(name: sliderName);
			slider.value = defaultValue;
			return slider;
		}

		public static double ParseDouble(string value)
		{
			double result;
			if (Double.TryParse(value, out result))
			{
				return result;
			}
			return 0;
		}

		public static string FindAssetPath(string name)
		{
			var results = AssetDatabase.FindAssets(name);

			if (results.Length == 0 || results == null)
			{
				UnityEngine.Debug.LogError("Asset " + name + " not found");
			}
			else if (results.Length > 1)
			{
				UnityEngine.Debug.LogError("Found more than one asset named " + name + ".\nPlease give the asset a unique name");
			}

			return AssetDatabase.GUIDToAssetPath(results[0]);
		}

		public static void InitializeDoubleField(VisualElement element, string name, SerializedProperty serializedProperty, Action<double> valueChangedCallback = null)
		{
			var doubleField = (DoubleField)element.Query<DoubleField>($"{name}-text");

			if (doubleField == null)
			{
				return;
			}

			doubleField.value = serializedProperty.doubleValue;

			doubleField.RegisterValueChangedCallback(@event =>
			{
				if (@event.newValue != @event.previousValue)
				{
					serializedProperty.doubleValue = @event.newValue;
					serializedProperty.serializedObject.ApplyModifiedProperties();

					if (valueChangedCallback != null)
					{
						valueChangedCallback(@event.newValue);
					}
				}
			});
		}

		public static void InitializeDoubleFieldWithSlider(VisualElement element, string name, SerializedProperty serializedProperty, Action<double> valueChangedCallback = null)
		{
			var slider = (Slider)element.Query<Slider>($"{name}-slider");
			var doubleField = (DoubleField)element.Query<DoubleField>($"{name}-text");

			if (slider == null || doubleField == null)
			{
				return;
			}

			slider.value = (float)serializedProperty.doubleValue;
			doubleField.value = serializedProperty.doubleValue;

			doubleField.RegisterValueChangedCallback(@event =>
			{
				if (@event.newValue != @event.previousValue)
				{
					slider.SetValueWithoutNotify((float)@event.newValue);
					slider.MarkDirtyRepaint();

					serializedProperty.doubleValue = @event.newValue;
					serializedProperty.serializedObject.ApplyModifiedProperties();

					if (valueChangedCallback != null)
					{
						valueChangedCallback(@event.newValue);
					}
				}
			});

			slider.RegisterValueChangedCallback(@event =>
			{
				if (@event.newValue != @event.previousValue)
				{
					doubleField.SetValueWithoutNotify(@event.newValue);

					serializedProperty.doubleValue = @event.newValue;
					serializedProperty.serializedObject.ApplyModifiedProperties();

					if (valueChangedCallback != null)
					{
						valueChangedCallback(@event.newValue);
					}
				}
			});
		}

		public static void ToggleCheckbox(Button toggle, bool value)
		{
			if (value)
			{
				toggle.AddToClassList("custom-toggle-enabled");
			}
			else
			{
				toggle.RemoveFromClassList("custom-toggle-enabled");
			}
		}

		public static void MarkDirty(ArcGISMapController mapController)
		{
			EditorUtility.SetDirty(mapController);
			EditorSceneManager.MarkSceneDirty(mapController.gameObject.scene);
		}

		public static List<ArcGISLayerType> CreateLayerTypeChoices()
		{
			List<ArcGISLayerType> choices = new List<ArcGISLayerType>();

			choices.Add(ArcGISLayerType.ArcGISImageLayer);
			choices.Add(ArcGISLayerType.ArcGIS3DModelLayer);
			choices.Add(ArcGISLayerType.ArcGISIntegratedMeshLayer);

			return choices;
		}

		public static string SelectFile()
		{
			return EditorUtility.OpenFilePanel("", Application.dataPath, "");
		}
	}
}
