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
namespace Esri.ArcGISMapsSDK.Utils.Math
{
	public static class MathUtils
	{
		public static double RadiansToDegrees = 180.0 / System.Math.PI;
		public static double DegreesToRadians = System.Math.PI / 180.0;

		public static double NormalizeAngleDegrees(double angle)
		{
			while (angle >= 360)
				angle -= 360;
			while (angle < 0)
				angle += 360;
			return angle;
		}
	}
}
