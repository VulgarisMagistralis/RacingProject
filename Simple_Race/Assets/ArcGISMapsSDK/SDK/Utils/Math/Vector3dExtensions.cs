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

namespace Esri.ArcGISMapsSDK.Utils.Math
{
	public static class Vector3dExtensions
	{
		public static Vector3 ToVector3(this Vector3d value)
		{
			return new Vector3((float)value.x, (float)value.y, (float)value.z);
		}
	}
}

