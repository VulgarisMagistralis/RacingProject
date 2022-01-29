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
	public static class Matrix4x4dExtensions
	{
		public static Matrix4x4 ToMatrix4x4(this Matrix4x4d value)
		{
			return new Matrix4x4(new Vector4((float)value.m00, (float)value.m10, (float)value.m20, (float)value.m30),
								new Vector4((float)value.m01, (float)value.m11, (float)value.m21, (float)value.m31),
								new Vector4((float)value.m02, (float)value.m12, (float)value.m22, (float)value.m32),
								new Vector4((float)value.m03, (float)value.m13, (float)value.m23, (float)value.m33));
		}
	}
}
