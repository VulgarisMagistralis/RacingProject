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
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using UnityEditor;

namespace ArcGISMapsSDK.Editor.Components
{
	[CustomEditor(typeof(ArcGISLocationComponent))]
	public class ArcGISLocationComponentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var arcGISLocationComponent = target as ArcGISLocationComponent;

			var position = arcGISLocationComponent.Position;
			var rotation = arcGISLocationComponent.Rotation;
			var scale = arcGISLocationComponent.Scale;

			if (Draw(ref position, ref rotation, ref scale))
			{
				arcGISLocationComponent.Position = position;
				arcGISLocationComponent.Rotation = rotation;
				arcGISLocationComponent.Scale = scale;

				EditorUtility.SetDirty(arcGISLocationComponent);
			}
		}

		private static bool Draw(ref LatLon position, ref Rotator rotation, ref float scale)
		{
			bool result = false;

			LatLon oldPosition = position;
			position = EditorUtilities.LatLonField("Position", position);

			if (oldPosition != position)
			{
				result = true;
			}

			Rotator oldRotation = rotation;
			rotation = EditorUtilities.RotatorField("Rotation", rotation);

			if (oldRotation != rotation)
			{
				result = true;
			}

			float oldScale = scale;
			scale = EditorUtilities.FloatField("Scale", scale);

			if (oldScale != scale)
			{
				result = true;
			}

			return result;
		}
	}
}
