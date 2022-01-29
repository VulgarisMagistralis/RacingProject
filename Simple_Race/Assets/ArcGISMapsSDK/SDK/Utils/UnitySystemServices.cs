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
	/// Various system services exposed to the ArcGIS plugin protected code.
	/// </summary>
	public class UnitySystemServices : ISystemServices
	{
		/// <summary>
		/// Gets the Unity Debug log.
		/// </summary>
		public ILog Log { get; } = new UnityLogWrapper();

		/// <summary>
		/// MemoryAvailability stats obtained through the Unity API.
		/// </summary>
		public MemoryAvailability GetMemoryAvailability()
		{
			var memoryAvailability = new MemoryAvailability
			{
				TotalSystemMemory = (long)SystemInfo.systemMemorySize * 1024 * 1024,
				TotalVideoMemory = (long)SystemInfo.graphicsMemorySize * 1024 * 1024,
			};
			return memoryAvailability;
		}
	}
}
