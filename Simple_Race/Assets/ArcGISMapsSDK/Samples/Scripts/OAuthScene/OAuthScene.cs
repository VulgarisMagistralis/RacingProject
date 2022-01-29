// Copyright 2021 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Camera;
using Esri.GameEngine.Location;
using Esri.GameEngine.View;
using Esri.GameEngine.View.Event;
using Esri.Unity;
using UnityEngine;

public class OAuthScene : MonoBehaviour
{
	private Esri.ArcGISMapsSDK.Security.OAuthAuthenticationChallengeHandler oauthAuthenticationChallengeHandler;

	public string clientID = "Enter Client ID";
	public string redirectURI = "Enter Redirect URI";
	public string serviceURL = "Enter Service URL";

	void Start()
	{
#if (UNITY_ANDROID || UNITY_IOS || UNITY_WSA) && !UNITY_EDITOR
		oauthAuthenticationChallengeHandler = new MobileOAuthAuthenticationChallengeHandler();
#else
		oauthAuthenticationChallengeHandler = new DesktopOAuthAuthenticationChallengeHandler();
#endif

		Esri.ArcGISMapsSDK.Security.AuthenticationChallengeManager.OAuthChallengeHandler = oauthAuthenticationChallengeHandler;

		Esri.GameEngine.Security.ArcGISAuthenticationManager.AuthenticationConfigurations.Clear();

		Esri.GameEngine.Security.ArcGISAuthenticationConfiguration authenticationConfiguration;

		// Named user login
		authenticationConfiguration = new Esri.GameEngine.Security.ArcGISOAuthAuthenticationConfiguration(clientID.Trim(), "", redirectURI.Trim());

		Esri.GameEngine.Security.ArcGISAuthenticationManager.AuthenticationConfigurations.Add(serviceURL, authenticationConfiguration);
	}

	void OnDestroy()
	{
		if (oauthAuthenticationChallengeHandler != null)
		{
			oauthAuthenticationChallengeHandler.Dispose();
		}
	}
}
