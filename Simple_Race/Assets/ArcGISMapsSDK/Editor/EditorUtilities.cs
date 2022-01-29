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
using System;
using UnityEditor;
using UnityEngine;

namespace ArcGISMapsSDK.Editor.Components
{
	public class EditorUtilities
	{
		private static void Double(ref double value)
		{
			value = EditorGUILayout.DoubleField(value, GUILayout.ExpandWidth(true));
		}

		public static T EnumField<T>(string label, T value) where T : Enum
		{
			Rect rect = EditorGUILayout.BeginHorizontal();

			GUILayout.Label(label, GUILayout.Width(150.0f));

			var result = EditorGUILayout.EnumPopup(value);

			EditorGUILayout.EndHorizontal();

			return (T)result;
		}

		private static void Float(ref float value)
		{
			value = EditorGUILayout.FloatField(value, GUILayout.ExpandWidth(true));
		}

		public static float FloatField(string label, float value)
		{
			Rect rect = EditorGUILayout.BeginHorizontal();

			GUILayout.Label(label, GUILayout.Width(150.0f));

			Float(ref value);

			EditorGUILayout.EndHorizontal();

			return value;
		}

		private static void Label(string str)
		{
			GUIContent labelContent = new GUIContent(str);
			float width = GUI.skin.GetStyle("Label").CalcSize(labelContent).x;
			GUILayout.Label(labelContent, GUILayout.Width(width));
		}

		public static LatLon LatLonField(string label, LatLon value)
		{
			Rect rect = EditorGUILayout.BeginHorizontal();

			GUILayout.Label(label, GUILayout.Width(150.0f));

			Label("Latitude");
			Double(ref value.Latitude);
			Label("Longitude");
			Double(ref value.Longitude);
			Label("Altitude");
			Double(ref value.Altitude);

			EditorGUILayout.EndHorizontal();

			return value;
		}

		public static Rotator RotatorField(string label, Rotator value)
		{
			Rect rect = EditorGUILayout.BeginHorizontal();

			GUILayout.Label(label, GUILayout.Width(150.0f));

			Label("Heading");
			Double(ref value.Heading);
			Label("Pitch");
			Double(ref value.Pitch);
			Label("Roll");
			Double(ref value.Roll);

			EditorGUILayout.EndHorizontal();

			return value;
		}
	}
}
