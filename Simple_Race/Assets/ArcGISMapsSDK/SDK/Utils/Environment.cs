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
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Utils
{
	class Environment
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			string productName = Application.productName;
			string productVersion = Application.version;
			string tempDirectory = Application.temporaryCachePath;
			string installDirectory = Application.dataPath;

#if UNITY_ANDROID && !UNITY_EDITOR
			var logger = new AndroidLogWrapper();
#else
			var logger = new UnityLogWrapper();
#endif

			ArcGISMapsSDKLib.Environment.Initialize(logger, productName, productVersion, tempDirectory, installDirectory);

			MainThreadScheduler.Instance();
		}
	}
}
