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
// Unity

using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace ArcGISMapsSDK.Editor
{
	public class OriginEditor
	{
		private static string LatitudeName = "lat-key";
		private static string LongitudeName = "lng-key";
		private static string AltitudeSliderName = "slider-altitude";

		private static string EditorCameraSettingsStylesFileName = "CameraSettingsStyles";

		public OriginEditor(VisualElement visualElement, SerializedProperty serializedProperty, Action valueChangedCallback = null)
		{
			var cameraSettingsStylesPath = MapControllerUtilities.FindAssetPath(EditorCameraSettingsStylesFileName);
			visualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(cameraSettingsStylesPath));

			Action<double> fieldValueChangedCallback = (double value) =>
			{
				if (valueChangedCallback != null)
				{
					valueChangedCallback();
				}
			};

			MapControllerUtilities.InitializeDoubleField(visualElement, LatitudeName, serializedProperty.FindPropertyRelative("Latitude"), fieldValueChangedCallback);
			MapControllerUtilities.InitializeDoubleField(visualElement, LongitudeName, serializedProperty.FindPropertyRelative("Longitude"), fieldValueChangedCallback);
			MapControllerUtilities.InitializeDoubleFieldWithSlider(visualElement, AltitudeSliderName, serializedProperty.FindPropertyRelative("Altitude"), fieldValueChangedCallback);
		}
	}
}
