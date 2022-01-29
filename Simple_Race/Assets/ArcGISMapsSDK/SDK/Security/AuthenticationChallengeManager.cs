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

namespace Esri.ArcGISMapsSDK.Security
{
	public class AuthenticationChallengeManager
	{
		private static OAuthAuthenticationChallengeHandler oauthChallengeHandler;

		public static OAuthAuthenticationChallengeHandler OAuthChallengeHandler
		{
			get
			{
				return oauthChallengeHandler;
			}
			set
			{
				oauthChallengeHandler = value;

				GameEngine.Security.ArcGISAuthenticationManager.OAuthAuthenticationChallengeIssued = delegate (GameEngine.Security.ArcGISOAuthAuthenticationChallenge authChallenge)
				{
					Utils.MainThreadScheduler.Instance().Schedule(() =>
					{
						oauthChallengeHandler.HandleChallenge(authChallenge.AuthorizeURI).ContinueWith(authorizationCodeTask =>
						{
							if (authorizationCodeTask.IsFaulted)
							{
								Debug.LogError(authorizationCodeTask.Exception.Message);

								authChallenge.Cancel();
							}
							else if (authorizationCodeTask.IsCanceled)
							{
								authChallenge.Cancel();
							}
							else
							{
								var authorizationCode = authorizationCodeTask.Result;

								if (authorizationCode != null)
								{
									authChallenge.Respond(authorizationCode);
								}
								else
								{
									authChallenge.Cancel();
								}
							}
						});
					});
				};
			}
		}
	}
}
