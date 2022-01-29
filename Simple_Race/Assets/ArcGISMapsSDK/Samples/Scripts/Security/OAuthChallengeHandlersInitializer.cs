// Copyright 2021 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
using Esri.ArcGISMapsSDK.Security;
using UnityEngine;

public class OAuthChallengeHandlersInitializer : MonoBehaviour
{
	private OAuthAuthenticationChallengeHandler oauthAuthenticationChallengeHandler;

	void Awake()
	{
#if (UNITY_ANDROID || UNITY_IOS || UNITY_WSA) && !UNITY_EDITOR
		oauthAuthenticationChallengeHandler = new MobileOAuthAuthenticationChallengeHandler();
#else
		oauthAuthenticationChallengeHandler = new DesktopOAuthAuthenticationChallengeHandler();
#endif

		Esri.ArcGISMapsSDK.Security.AuthenticationChallengeManager.OAuthChallengeHandler = oauthAuthenticationChallengeHandler;
	}

	void OnDestroy()
	{
		if (oauthAuthenticationChallengeHandler != null)
		{
			oauthAuthenticationChallengeHandler.Dispose();
		}
	}
}
