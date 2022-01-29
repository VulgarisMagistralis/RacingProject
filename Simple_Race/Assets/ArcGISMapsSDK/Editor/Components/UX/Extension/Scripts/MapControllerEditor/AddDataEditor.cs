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

using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ArcGISMapsSDK.Editor
{
	public class AddDataEditor
	{
		private const string AddNewLayerFromFile = "from-file-box-save";
		private const string ButtonFromFileName = "button-from-file";
		private const string ButtonFromUrlName = "button-from-url";
		private const string FromFileBoxName = "from-file-box";
		private const string FromUrlBoxName = "from-url-box";
		private const string AddDataSelectedName = "add-data-from-button-selected";
		private const string OpenDataDialogueName = "button-open-data-dialog";
		private const string ClearAddDataFormsButtonName = "button-clear-add-data-forms";
		private const string AddDataLayerNameFromFile = "layer-name-from-file";
		private const string AddDataLayerNameFromUrl = "layer-name-from-url";
		private const string AddDataLayerFilePath = "layer-file-path";
		private const string AddDataLayerFileUrl = "layer-url-from";
		private const string EditorAddDataStylesFileName = "AddDataStyles";
		private const string LayerTypeSelectorWrapper = "add-data-layer-type-selector-wrapper";

		private VisualElement mapEditor;
		private ArcGISMapController mapController;
		private LayerEditor layerEditor;
		private VisualElement addDataLayerTypeSelectorWrapper;
		private PopupField<ArcGISLayerType> addLayerOptions;
		private ArcGISLayerType currentSelectedLayerType = ArcGISLayerType.ArcGISImageLayer;

		private int currentAddDataTab = 1;

		public AddDataEditor(VisualElement editor, ArcGISMapController controller)
		{
			mapEditor = editor;
			mapController = controller;
			var addDataStylePath = MapControllerUtilities.FindAssetPath(EditorAddDataStylesFileName);
			mapEditor.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(addDataStylePath));
			Initialize();
		}

		public void SetLayerEditor(LayerEditor editor)
		{
			layerEditor = editor;
		}

		private void Initialize()
		{
			Button addNewLayerFromFileButton = mapEditor.Query<Button>(AddNewLayerFromFile);
			addNewLayerFromFileButton.RegisterCallback<MouseUpEvent>(envt =>
			{
				TextField layerName = (currentAddDataTab == 0) ? mapEditor.Query<TextField>(name: AddDataLayerNameFromFile) : mapEditor.Query<TextField>(name: AddDataLayerNameFromUrl);
				TextField path = (currentAddDataTab == 0) ? mapEditor.Query<TextField>(name: AddDataLayerFilePath) : mapEditor.Query<TextField>(name: AddDataLayerFileUrl);
				Layer layer = new Layer(layerName.value.Trim(), path.value.Trim(), true, 100, currentSelectedLayerType);
				mapController.Layers.Add(layer);
				layerEditor.CreateMapLayerTab(layer);
				ClearAddDataForms();
				MapControllerUtilities.MarkDirty(mapController);
			});

			Button addNewLayerFromFileTab = mapEditor.Query<Button>(name: ButtonFromFileName);
			addNewLayerFromFileTab.RegisterCallback<MouseUpEvent>(envt =>
			{
				currentAddDataTab = 0;
				ClearActiveTab(ButtonFromUrlName);
				addNewLayerFromFileTab.AddToClassList(AddDataSelectedName);
				ToggleLayersFromBox(FromFileBoxName);
			});

			Button addNewLayerFromUrlTab = mapEditor.Query<Button>(name: ButtonFromUrlName);
			addNewLayerFromUrlTab.RegisterCallback<MouseUpEvent>(envt =>
			{
				currentAddDataTab = 1;
				ClearActiveTab(ButtonFromFileName);
				addNewLayerFromUrlTab.AddToClassList(AddDataSelectedName);
				ToggleLayersFromBox(FromUrlBoxName);
			});

			addDataLayerTypeSelectorWrapper = mapEditor.Query<VisualElement>(name: LayerTypeSelectorWrapper);
			addLayerOptions = new PopupField<ArcGISLayerType>("Select Layer Type", MapControllerUtilities.CreateLayerTypeChoices(), ArcGISLayerType.ArcGISImageLayer);
			addDataLayerTypeSelectorWrapper.Add(addLayerOptions);
			addDataLayerTypeSelectorWrapper.RegisterCallback<ChangeEvent<ArcGISLayerType>>(evnt =>
			{
				currentSelectedLayerType = evnt.newValue;
			});

			Button addNewLayerDialogButton = mapEditor.Query<Button>(name: OpenDataDialogueName);
			addNewLayerDialogButton.RegisterCallback<MouseUpEvent>(envt =>
			{
				string filePath = MapControllerUtilities.SelectFile();
				if (filePath.Length != 0)
				{
					TextField filePathLabel = mapEditor.Query<TextField>(name: AddDataLayerFilePath);
					filePathLabel.value = filePath;
				}
			});

			Button clearFormsButton = mapEditor.Query<Button>(name: ClearAddDataFormsButtonName);
			clearFormsButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				ClearAddDataForms();
			});
		}

		private void ToggleLayersFromBox(string id)
		{
			Box boxWrapper;
			if (id != FromUrlBoxName)
			{
				boxWrapper = mapEditor.Query<Box>(name: FromUrlBoxName);
				boxWrapper.style.display = DisplayStyle.None;
			}
			else
			{
				boxWrapper = mapEditor.Query<Box>(name: FromFileBoxName);
				boxWrapper.style.display = DisplayStyle.None;
			}

			boxWrapper = mapEditor.Query<Box>(name: id);
			boxWrapper.style.display = DisplayStyle.Flex;
		}

		private void ClearActiveTab(string id)
		{
			Button button = mapEditor.Query<Button>(name: id);
			button.RemoveFromClassList(AddDataSelectedName);
			ClearAddDataForms();
		}

		private void ClearAddDataForms()
		{
			TextField toClear;
			toClear = mapEditor.Query<TextField>(name: AddDataLayerFileUrl);
			toClear.value = "";
			toClear = mapEditor.Query<TextField>(name: AddDataLayerNameFromUrl);
			toClear.value = "";
			toClear = mapEditor.Query<TextField>(name: AddDataLayerFilePath);
			toClear.value = "";
			toClear = mapEditor.Query<TextField>(name: AddDataLayerNameFromFile);
			toClear.value = "";
			currentSelectedLayerType = ArcGISLayerType.ArcGISImageLayer;
			addLayerOptions.value = currentSelectedLayerType;
		}
	}
}
