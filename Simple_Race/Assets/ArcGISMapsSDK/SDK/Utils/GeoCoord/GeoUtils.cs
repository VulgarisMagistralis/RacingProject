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
using Esri.ArcGISMapsSDK.Utils.Math;

namespace Esri.ArcGISMapsSDK.Utils.GeoCoord
{
	public enum SpatialReference
	{
		WebMercator = 3857,
		WGS84 = 4326
	}

	public struct TileCoord
	{
		public uint X;
		public uint Y;
		public uint LOD;

		public TileCoord(uint x, uint y, uint lod)
		{
			X = x;
			Y = y;
			LOD = lod;
		}
	}

	public static class GeoUtils
	{
		public static double Rad2Deg = 180.0 / System.Math.PI;
		public static double Deg2Rad = System.Math.PI / 180.0;

		// Reference: https://en.wikipedia.org/wiki/World_Geodetic_System
		public static double EarthRadius = 6378137.0;
		public static double EarthEquatorLongitude = 40075000.0;
		public static double EarthFlattening = 1.0 / 298.257223563;

		private static double earthSemiMajorAxis = EarthRadius;
		private static double earthEccentricitySquared = 2.0 * EarthFlattening - EarthFlattening * EarthFlattening;

		public static double Clamp(double value, double min, double max)
		{
			return System.Math.Min(System.Math.Max(value, min), max);
		}

		public static LatLon WebMercatorTileToWGS84LatLon(double x, double y, uint lod, double altitude = 0)
		{
			var n = 1 << (int)lod;
			var lon = x / n * 360.0 - 180.0;
			var lat = System.Math.Atan(System.Math.Sinh(System.Math.PI * (1 - 2 * y / n))) * Rad2Deg;

			LatLon latLon;

			latLon.Longitude = lon;
			latLon.Latitude = lat;
			latLon.Altitude = altitude;

			return latLon;
		}

		public static LatLon WebMercatorTileToWGS84LatLon(uint x, uint y, uint lod, double altitude = 0)
		{
			return WebMercatorTileToWGS84LatLon((float)x, (float)y, lod, altitude);
		}

		public static LatLon WGS84TileToWGS84LatLon(double x, double y, uint lod, double altitude = 0)
		{
			var n = 1 << (int)lod;
			var lon = (-1.0 + +x / n) * 180;
			var lat = (+0.5 + -y / n) * 180;

			LatLon latLon;

			latLon.Longitude = lon;
			latLon.Latitude = lat;
			latLon.Altitude = altitude;

			return latLon;
		}

		public static LatLon WGS84TileToWGS84LatLon(uint x, uint y, uint lod, double altitude = 0)
		{
			return WGS84TileToWGS84LatLon(x, y, lod, altitude);
		}

		public static LatLon TileToWGS84LatLon(SpatialReference spatialReference, double x, double y, uint lod, double altitude = 0)
		{
			return spatialReference == SpatialReference.WebMercator ?
						GeoUtils.WebMercatorTileToWGS84LatLon(x, y, lod)
						: GeoUtils.WGS84TileToWGS84LatLon(x, y, lod);
		}

		public static LatLon TileToWGS84LatLon(SpatialReference spatialReference, uint x, uint y, uint lod, double altitude = 0)
		{
			return spatialReference == SpatialReference.WebMercator ?
						GeoUtils.WebMercatorTileToWGS84LatLon(x, y, lod)
						: GeoUtils.WGS84TileToWGS84LatLon(x, y, lod);
		}

		public static LatLon WebMercatorToWGS84LatLon(double x, double y, double z)
		{
			double latitude = System.Math.Atan(System.Math.Sinh(y));
			double longitude = x;
			double altitude = z * EarthRadius;

			latitude = Rad2Deg * latitude;
			longitude = Rad2Deg * longitude;

			LatLon latLon;

			latLon.Longitude = longitude;
			latLon.Latitude = latitude;
			latLon.Altitude = altitude;

			return latLon;
		}

		public static Vector3d WGS84LatLonToWebMercator(LatLon latLon)
		{
			// A GCS->PCS projection is done using the Web Mercator projection
			// This function returns the projected coordinates normalized to the Earth's radius
			const double minLatitude = -85.05112878;
			const double maxLatitude = 85.05112878;
			const double minLongitude = -180;
			const double maxLongitude = 180;

			double latitude = Clamp(latLon.Latitude, minLatitude, maxLatitude);
			double longitude = Clamp(latLon.Longitude, minLongitude, maxLongitude);

			latitude = Deg2Rad * latitude;
			longitude = Deg2Rad * longitude;

			Vector3d v;

			v.x = longitude;
			v.y = System.Math.Log(System.Math.Tan(latitude) + (1 / System.Math.Cos(latitude)));
			v.z = latLon.Altitude / EarthRadius;

			return v;
		}

		public static Vector3d SphericalWGS84LatLonToECEF(LatLon latLon)
		{
			double theta = Deg2Rad * latLon.Latitude;
			double phi = Deg2Rad * latLon.Longitude;
			double r = 1.0 + latLon.Altitude / EarthRadius;

			Vector3d v;

			v.x = r * System.Math.Cos(theta) * System.Math.Cos(phi);
			v.y = r * System.Math.Cos(theta) * System.Math.Sin(phi);
			v.z = r * System.Math.Sin(theta);

			return v;
		}

		public static Vector3d EllipsoidWGS84LatLonToECEF(LatLon latLon)
		{
			// Reference: https://gssc.esa.int/navipedia/index.php/Ellipsoidal_and_Cartesian_Coordinates_Conversion

			double theta = Deg2Rad * latLon.Latitude;
			double phi = Deg2Rad * latLon.Longitude;

			double N = earthSemiMajorAxis / System.Math.Sqrt(1.0 - earthEccentricitySquared * System.Math.Pow(System.Math.Sin(theta), 2.0));
			double r = (N + latLon.Altitude) / EarthRadius;

			Vector3d v;

			v.x = r * System.Math.Cos(theta) * System.Math.Cos(phi);
			v.y = r * System.Math.Cos(theta) * System.Math.Sin(phi);
			v.z = ((1 - earthEccentricitySquared)*N + latLon.Altitude) / EarthRadius * System.Math.Sin(theta);

			return v;
		}

		public static Vector3d WGS84LatLonToUnityCartesian(LatLon latLon, bool isSpherical = false)
		{
			// ECEF defines that the origin is the center of the earth. Z goes from the center of the Earth
			// to the north pole and XZ define the plane of the prime meridian and XY define the plane of
			// the equator.
			// Reference: https://en.wikipedia.org/wiki/ECEF
			// As Unity has a different axis configuration, we will adopt that: Y goes from the center of
			// the Earth to the north pole and XY define the plane of the prime meridian and ZX define the
			// plane of the equator. This means we are switching the Y and Z axes compared to ECEF.

			var ecef = isSpherical ? SphericalWGS84LatLonToECEF(latLon) : EllipsoidWGS84LatLonToECEF(latLon);

			Vector3d v;

			// Unity space
			v.x = ecef.x;
			v.y = ecef.z;
			v.z = ecef.y;

			return v;
		}

		public static LatLon ECEFToSphericalWGS84LatLon(double x, double y, double z)
		{
			var altitudePlusEarthRadius = System.Math.Sqrt(x * x + y * y + z * z);

			LatLon latLon;

			latLon.Latitude = Rad2Deg * System.Math.Asin(z / altitudePlusEarthRadius);
			latLon.Longitude = Rad2Deg * System.Math.Atan2(y, x);
			latLon.Altitude = altitudePlusEarthRadius - EarthRadius;

			return latLon;
		}

		public static LatLon ECEFToEllipsoidWGS84LatLon(double x, double y, double z)
		{
			// Reference: https://gssc.esa.int/navipedia/index.php/Ellipsoidal_and_Cartesian_Coordinates_Conversion

			LatLon latLon;

			double p = System.Math.Sqrt(x * x + y * y);

			latLon.Longitude = Rad2Deg * System.Math.Atan2(y, x);

			double iterationError;
			double minimunError = 1e-10;

			latLon.Latitude = System.Math.Atan2(z, (1.0 - earthEccentricitySquared) * p);
			uint iterations = 0;

			do
			{
				double iterationN = earthSemiMajorAxis / System.Math.Sqrt(1.0 - earthEccentricitySquared * System.Math.Pow(System.Math.Sin(latLon.Latitude), 2.0));
				latLon.Altitude = (p / System.Math.Cos(latLon.Latitude)) - iterationN;
				double iterationLatitude = System.Math.Atan2(z, (1.0 - earthEccentricitySquared*(iterationN / (iterationN + latLon.Altitude)))*p);

				iterationError = System.Math.Abs(iterationLatitude - latLon.Latitude);
				latLon.Latitude = iterationLatitude;

				iterations++;
			}
			while (iterationError > minimunError && iterations < 10);

			latLon.Latitude *= Rad2Deg;

			return latLon;
		}

		public static LatLon UnityCartesianToWGS84LatLon(Vector3d v, bool isSpherical = false)
		{
			return isSpherical ? ECEFToSphericalWGS84LatLon(v.x, v.z, v.y) : ECEFToEllipsoidWGS84LatLon(v.x, v.z, v.y);
		}

		public static LatLon UnityCartesianToWGS84LatLon(double x, double y, double z, bool isSpherical = false)
		{
			return isSpherical ? ECEFToSphericalWGS84LatLon(x, z, y) : ECEFToEllipsoidWGS84LatLon(x, z, y);
		}

		public static Vector3d GetCartesianLocalCoords(LatLon latLon)
		{
			var coord = GeoUtils.WGS84LatLonToWebMercator(latLon);
			Vector3d v;
			v.x = 0.5 * coord.x * GeoUtils.EarthEquatorLongitude;
			v.y = latLon.Altitude;
			v.z = 0.5 * coord.y * GeoUtils.EarthEquatorLongitude;

			return v;
		}

		public static float EarthScaleToUnityScale(double v, bool local = false, double scale = 1.0)
		{
			return local ? (float)((v / EarthEquatorLongitude) * scale) : (float)((v / EarthRadius) * scale);
		}

		public static Matrix4x4d GetENUReferenceMatrix(Vector3d camLocation)
		{
			var up = Vector3d.Normalize(camLocation);
			var east = Vector3d.Right;
			var north = Vector3d.Forward;

			if (up == -Vector3d.Up)
			{
				east = -Vector3d.Right;
				north = -Vector3d.Forward;
			}
			else if (up != Vector3d.Up)
			{
				east = Vector3d.Cross(up, Vector3d.Up);
				east = east.SqrtLength() < 0.000f ? (camLocation.y < 0 ? Vector3d.Forward : -Vector3d.Forward) : Vector3d.Normalize(east);
				north = Vector3d.Normalize(Vector3d.Cross(east, up));
			}

			Matrix4x4d m;

			m.m00 = east.x; m.m01 = up.x; m.m02 = north.x; m.m03 = 0;
			m.m10 = east.y; m.m11 = up.y; m.m12 = north.y; m.m13 = 0;
			m.m20 = east.z; m.m21 = up.z; m.m22 = north.z; m.m23 = 0;
			m.m30 = 0; m.m31 = 0; m.m32 = 0; m.m33 = 1;

			return m;
		}
	}
}
