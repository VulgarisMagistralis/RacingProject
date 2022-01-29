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
	/// <summary>
	/// Implementation of ILog that writes to the Android log.
	/// </summary>
	internal class AndroidLogWrapper : ILog
	{
		private const string Tag = "ArcGISMapsSDK";

		private readonly AndroidJavaClass log = new AndroidJavaClass("android.util.Log");

		/// <summary>
		/// Write a Debug level message to the log.
		/// </summary>
		public void Debug(string message)
		{
			log.CallStatic<int>("d", Tag, message);
		}

		/// <summary>
		/// Write an Info level message to the log.
		/// </summary>
		public void Info(string message)
		{
			log.CallStatic<int>("i", Tag, message);
		}

		/// <summary>
		/// Write a Warning level message to the log.
		/// </summary>
		public void Warning(string message)
		{
			log.CallStatic<int>("w", Tag, message);
		}

		/// <summary>
		/// Write an Error level message to the log.
		/// </summary>
		public void Error(string message)
		{
			log.CallStatic<int>("e", Tag, message);
		}
	}
}
