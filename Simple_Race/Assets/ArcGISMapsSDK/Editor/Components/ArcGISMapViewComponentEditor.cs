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
using Esri.GameEngine.Map;
using UnityEditor;

namespace ArcGISMapsSDK.Editor.Components
{
	[CustomEditor(typeof(ArcGISMapViewComponent))]
	public class ArcGISMapViewComponentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var arcGISMapViewComponent = target as ArcGISMapViewComponent;

			var position = arcGISMapViewComponent.Position;
			var viewMode = arcGISMapViewComponent.ViewMode;

			if (Draw(ref viewMode, ref position))
			{
				arcGISMapViewComponent.Position = position;
				arcGISMapViewComponent.ViewMode = viewMode;

				EditorUtility.SetDirty(arcGISMapViewComponent);
			}
		}

		private static bool Draw(ref ArcGISMapType viewMode, ref LatLon position)
		{
			bool result = false;

			ArcGISMapType oldViewMode = viewMode;
			viewMode = EditorUtilities.EnumField<ArcGISMapType>("View Mode", viewMode);

			if (oldViewMode != viewMode)
			{
				result = true;
			}

			LatLon oldPosition = position;
			position = EditorUtilities.LatLonField("Position", position);

			if (oldPosition != position)
			{
				result = true;
			}

			return result;
		}
	}
}
