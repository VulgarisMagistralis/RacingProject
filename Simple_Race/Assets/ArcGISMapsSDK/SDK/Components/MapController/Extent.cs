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
// Unity

using System;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.UX
{
    public enum ExtentType
    {
        Square = 0,
        Rectangle = 1,
        Circle = 2
    }

    [Serializable]
    public class Extent
    {
        [SerializeField]
        public double Latitude;

        [SerializeField]
        public double Longitude;

        [SerializeField]
        public float Altitude;

        [SerializeField]
        public double Width;

        [SerializeField]
        public double Length;

        [SerializeField]
        public ExtentType ExtentType = ExtentType.Square;
    }
}
