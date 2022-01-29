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
using Esri.ArcGISRuntime;
using System;

namespace Esri.ArcGISMapsSDKLib
{
	public static class Environment
	{
		public static void Initialize(ArcGISMapsSDK.Utils.ILog logger, string productName, string productVersion, string tempDirectory, string installDirectory)
		{
			var version = typeof(Environment).Assembly.GetName().Version.ToString();
			version = version.Substring(0, version.LastIndexOf('.'));

			ArcGISRuntimeEnvironment.SetRuntimeClient(Standard.RuntimeClient.Unity, version);
			ArcGISRuntimeEnvironment.SetProductInfo(productName, productVersion);
			ArcGISRuntimeEnvironment.EnableBreakOnException(false);
			ArcGISRuntimeEnvironment.EnableLeakDetection(false);
			ArcGISRuntimeEnvironment.SetTempDirectory(tempDirectory);
			ArcGISRuntimeEnvironment.SetInstallDirectory(installDirectory);

			if (logger != null)
			{
				ArcGISRuntimeEnvironment.Error = delegate(Exception error)
				{
					logger.Error(error.Message);
				};
			}
		}
	}
}
