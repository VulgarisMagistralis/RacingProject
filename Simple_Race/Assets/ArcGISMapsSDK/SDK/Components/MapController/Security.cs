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

using UnityEngine;

namespace Esri.ArcGISMapsSDK.UX.Security
{
	[System.Serializable]
	public class OAuthAuthenticationConfiguration
	{
		public string Name;
		public string ClientID;
		public string RedirectURI;
	}

	[System.Serializable]
	public class OAuthAuthenticationConfigurationMapping
	{
		public string ServiceURL;

		[SerializeReference]
		public OAuthAuthenticationConfiguration Configuration;
	}
}
