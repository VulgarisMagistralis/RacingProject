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
    public static class QuaterniondExtensions
    {
        public static Quaternion ToQuaternion(this Quaterniond value)
        {
            return new Quaternion((float)value.x, (float)value.y, (float)value.z, (float)value.w);
        }

        public static Quaterniond ToQuaterniond(this Quaternion value)
        {
            return new Quaterniond(value.x, value.y, value.z, value.w);
        }
    }
}
