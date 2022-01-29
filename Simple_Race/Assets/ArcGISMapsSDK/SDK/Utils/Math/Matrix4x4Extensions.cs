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
	public static class Matrix4x4Extensions
	{
		public static Quaternion ToQuaternion(this Matrix4x4 matrix)
		{
			Quaternion result = new Quaternion();

			result.w = Mathf.Sqrt(Mathf.Max(0, 1 + matrix[0, 0] + matrix[1, 1] + matrix[2, 2])) / 2;
			result.x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix[0, 0] - matrix[1, 1] - matrix[2, 2])) / 2;
			result.y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix[0, 0] + matrix[1, 1] - matrix[2, 2])) / 2;
			result.z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix[0, 0] - matrix[1, 1] + matrix[2, 2])) / 2;

			result.x *= Mathf.Sign(result.x * (matrix[2, 1] - matrix[1, 2]));
			result.y *= Mathf.Sign(result.y * (matrix[0, 2] - matrix[2, 0]));
			result.z *= Mathf.Sign(result.z * (matrix[1, 0] - matrix[0, 1]));

			return result;
		}
	}
}
