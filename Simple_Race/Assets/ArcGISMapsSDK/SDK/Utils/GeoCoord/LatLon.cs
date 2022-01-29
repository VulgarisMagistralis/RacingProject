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
using System;

namespace Esri.ArcGISMapsSDK.Utils.GeoCoord
{
	[Serializable]
	public struct LatLon
	{
		public double Latitude;
		public double Longitude;
		public double Altitude;

		public LatLon(double latitude, double longitude, double altitude = 0)
		{
			Latitude = latitude;
			Longitude = longitude;
			Altitude = altitude;
		}

		public override int GetHashCode()
		{
			return Latitude.GetHashCode() ^ (Longitude.GetHashCode() << 2) ^ (Altitude.GetHashCode() >> 2);
		}

		public override bool Equals(object o)
		{
			var latLon = (LatLon)o;
			const double epsilon = 1e-11;

			return System.Math.Abs(latLon.Latitude - Latitude) < epsilon &&
					System.Math.Abs(latLon.Longitude - Longitude) < epsilon &&
					System.Math.Abs(latLon.Altitude - Altitude) < epsilon;
		}

		public static bool operator ==(LatLon lhs, LatLon rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(LatLon lhs, LatLon rhs)
		{
			return !lhs.Equals(rhs);
		}
	}
}
