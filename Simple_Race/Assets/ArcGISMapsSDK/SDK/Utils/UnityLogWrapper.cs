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
namespace Esri.ArcGISMapsSDK.Utils
{
	/// <summary>
	/// Implementation of ILog that writes to the Unity DebugLog.
	/// </summary>
	internal class UnityLogWrapper : ILog
	{
		/// <summary>
		/// Write a Debug level message to the log.
		/// </summary>
		public void Debug(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		/// <summary>
		/// Write an Info level message to the log.
		/// </summary>
		public void Info(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		/// <summary>
		/// Write a Warning level message to the log.
		/// </summary>
		public void Warning(string message)
		{
			UnityEngine.Debug.LogWarning(message);
		}

		/// <summary>
		/// Write an Error level message to the log.
		/// </summary>
		public void Error(string message)
		{
			UnityEngine.Debug.LogError(message);
		}
	}
}
