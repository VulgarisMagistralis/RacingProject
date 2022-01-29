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
	public class CameraEditor
	{
		private static string LatitudeName = "cam-lat-key";
		private static string LongitudeName = "cam-lng-key";
		private static string AltitudeSliderName = "cam-slider-camera-altitude";
		private static string HeadingSliderName = "cam-slider-camera-heading";
		private static string PitchSliderName = "cam-slider-camera-pitch";
		private static string RollSliderName = "cam-slider-camera-roll";

		private static string EditorCameraSettingsStylesFileName = "CameraSettingsStyles";

		public CameraEditor(VisualElement visualElement, SerializedProperty serializedProperty, Action valueChangedCallback = null)
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

			var serializedPositionProperty = serializedProperty.FindPropertyRelative("position");

			MapControllerUtilities.InitializeDoubleField(visualElement, LatitudeName, serializedPositionProperty.FindPropertyRelative("Latitude"), fieldValueChangedCallback);
			MapControllerUtilities.InitializeDoubleField(visualElement, LongitudeName, serializedPositionProperty.FindPropertyRelative("Longitude"), fieldValueChangedCallback);
			MapControllerUtilities.InitializeDoubleFieldWithSlider(visualElement, AltitudeSliderName, serializedPositionProperty.FindPropertyRelative("Altitude"), fieldValueChangedCallback);

			var serializedRotationProperty = serializedProperty.FindPropertyRelative("rotation");

			MapControllerUtilities.InitializeDoubleFieldWithSlider(visualElement, HeadingSliderName, serializedRotationProperty.FindPropertyRelative("Heading"), fieldValueChangedCallback);
			MapControllerUtilities.InitializeDoubleFieldWithSlider(visualElement, PitchSliderName, serializedRotationProperty.FindPropertyRelative("Pitch"), fieldValueChangedCallback);
			MapControllerUtilities.InitializeDoubleFieldWithSlider(visualElement, RollSliderName, serializedRotationProperty.FindPropertyRelative("Roll"), fieldValueChangedCallback);
		}
	}
}
