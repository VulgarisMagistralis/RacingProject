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

// Unity

using UnityEditor;
using UnityEngine.UIElements;

namespace ArcGISMapsSDK.Editor
{
	[CustomEditor(typeof(ArcGISMapController))]
	public class ArcGISMapControllerEditor : UnityEditor.Editor
	{
		private const string EditorStylesFileName = "ArcGISMapControllerStyles";
		private const string EditorTemplateFileName = "ArcGISMapControllerTemplate";

		private ArcGISMapController mapController;
		private VisualElement mapEditor;

		private AddDataEditor addDataEditor;
		private LayerEditor layerEditor;
		private MapExtentEditor mapExtentEditor;
		private ViewModeEditor viewModeEditor;

		public override VisualElement CreateInspectorGUI()
		{
			mapController = target as ArcGISMapController;
			mapEditor = new VisualElement();

			var templatePath = MapControllerUtilities.FindAssetPath(EditorTemplateFileName);
			var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(templatePath);
			template.CloneTree(mapEditor);

			var styleSheet = MapControllerUtilities.FindAssetPath(EditorStylesFileName);
			mapEditor.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheet));

			viewModeEditor = new ViewModeEditor(mapEditor, mapController);
			new OriginEditor(mapEditor, serializedObject.FindProperty("originLocation"), () =>
			{
				mapController.UpdateOriginPosition();
			});
			new CameraEditor(mapEditor, serializedObject.FindProperty("CameraLocation"));
			mapExtentEditor = new MapExtentEditor(mapEditor, mapController);
			new BaseMapEditor(mapEditor, mapController);
			addDataEditor = new AddDataEditor(mapEditor, mapController);
			layerEditor = new LayerEditor(mapEditor, mapController);
			new AuthConfigEditor(mapEditor, mapController);

			viewModeEditor.SetMapExtentEditor(mapExtentEditor);
			addDataEditor.SetLayerEditor(layerEditor);

			return mapEditor;
		}
	}
}
