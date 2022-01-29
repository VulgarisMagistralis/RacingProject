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
using Esri.ArcGISMapsSDK.Utils.GeoCoord;

namespace Esri.ArcGISMapsSDK.Utils.Math
{
	public static class Geometry
	{
		public static bool RayWGS84EllipsoidIntersection(Vector3d rayOrig, Vector3d rayDir, double Altitude, out double t)
		{
			t = 0;
			var ellipsoidScaling = 1.0 / (1.0 - GeoUtils.EarthFlattening);

			// Scale the space of the ray in Y-axis to trait the ellipsoid as a Sphere 
			var scaledRayOrig = new Vector3d(rayOrig.x, rayOrig.y * ellipsoidScaling, rayOrig.z);
			var scaledRayDir = new Vector3d(rayDir.x, rayDir.y * ellipsoidScaling, rayDir.z);

			double scaledLength;
			var result = RaySphereIntersection(scaledRayOrig, scaledRayDir, Vector3d.zero, GeoUtils.EarthRadius + Altitude, out scaledLength);

			if (result)
			{
				// Back to non-scaled world coordinate and compute the real length between hitPoint and rayOrig
				var scaledHitPoint = scaledRayOrig + scaledLength * scaledRayDir;
				var hitPoint = new Vector3d(scaledHitPoint.x, scaledHitPoint.y / ellipsoidScaling, scaledHitPoint.z);

				t = System.Math.Sign(scaledLength) * (hitPoint - rayOrig).Length();
			}

			return result;
		}

		private static bool RaySphereIntersection(Vector3d rayOrig, Vector3d rayDir, Vector3d sphereCenter, double sphereRadius, out double t)
		{
			double a = Vector3d.Dot(rayDir, rayDir);
			Vector3d s0_r0 = rayOrig - sphereCenter;
			double b = 2.0 * Vector3d.Dot(rayDir, s0_r0);
			double c = Vector3d.Dot(s0_r0, s0_r0) - (sphereRadius * sphereRadius);

			if (b * b - 4.0 * a * c < 0.0)
			{
				t = 0;
				return false;
			}
			else
			{
				t = (-b - System.Math.Sqrt((b * b) - 4.0 * a * c)) / (2.0 * a);
				return true;
			}
		}

		public static bool RayPlaneIntersection(Vector3d rayOrig, Vector3d rayDir, Vector3d planePosition, Vector3d planeNormal, out double t)
		{
			double denominator = Vector3d.Dot(rayDir, planeNormal);

			if (System.Math.Abs(denominator) > 0.00001)
			{
				t = Vector3d.Dot(planePosition - rayOrig, planeNormal) / denominator;
				return true;
			}
			else
			{
				t = 0;
				return false;
			}
		}
	}
}
