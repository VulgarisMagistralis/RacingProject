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
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Location;
using Esri.GameEngine.Map;

namespace Esri.ArcGISMapsSDK.Utils
{
	public class Scene
	{
		public ArcGISMapType MapType { get; }

		public Scene(ArcGISMapType mapType)
		{
			MapType = mapType;
		}

		public Vector3d ToCartesianPosition(LatLon latLon)
		{
			if (MapType == ArcGISMapType.Global)
			{
				return GeoUtils.WGS84LatLonToUnityCartesian(latLon) * GeoUtils.EarthRadius;
			}
			else
			{
				var xyz = GeoUtils.WGS84LatLonToWebMercator(latLon);

				// Axis change
				var X = xyz.x;
				var Y = xyz.z;
				var Z = xyz.y;

				return new Vector3d(X, Y, Z) * GeoUtils.EarthRadius;
			}
		}

		public LatLon FromCartesianPosition(Vector3d cartesianPosition)
		{
			if (MapType == ArcGISMapType.Global)
			{
				return GeoUtils.UnityCartesianToWGS84LatLon(cartesianPosition);
			}
			else
			{
				// Axis change
				var X = cartesianPosition.x / GeoUtils.EarthRadius;
				var Y = cartesianPosition.z / GeoUtils.EarthRadius;
				var Z = cartesianPosition.y / GeoUtils.EarthRadius;

				return GeoUtils.WebMercatorToWGS84LatLon(X, Y, Z);
			}
		}

		public Quaterniond ToCartesianRotation(Vector3d position, Rotator rotation)
		{
			var eulerAngles = new Vector3d();

			eulerAngles.x = 90 - rotation.Pitch;
			eulerAngles.y = rotation.Heading;
			eulerAngles.z = rotation.Roll;

			var result = new Quaterniond();

			result.eulerAngles = eulerAngles;

			var tangentToWorld = GetENUReference(position);

			return tangentToWorld.ToQuaterniond() * result;
		}

		public Rotator FromCartesianRotation(Vector3d position, Quaterniond rotation)
		{
			var tangentToWorld = GetENUReference(position);
			var worldToTangent = Quaterniond.Inverse(tangentToWorld.ToQuaterniond());

			var localRotation = worldToTangent * rotation;

			var eulerAngles = localRotation.eulerAngles;

			var pitch = 90 + eulerAngles.x;
			var heading = eulerAngles.y;
			var roll = eulerAngles.z;

			return Rotator.Normalize(new Rotator(heading, pitch, roll));
		}

		public Quaterniond GetRotationToLocal(Vector3d position)
		{
			var enuReference = GetENUReference(position);
			return Quaterniond.Inverse(enuReference.ToQuaterniond());
		}

		public Matrix4x4d GetENUReference(Vector3d position)
		{
			if (MapType == ArcGISMapType.Global)
			{
				return GeoUtils.GetENUReferenceMatrix(position);
			}
			else
			{
				return Matrix4x4d.Identity;
			}
		}

		public double GetCameraNearPlane(double altitude, double hfieldOfView, double aspect)
		{
			double vFov = 0.5 * hfieldOfView * GeoUtils.Deg2Rad;
			double hFov = System.Math.Atan(System.Math.Tan(vFov) * aspect);

			Vector3d frustumCornerDir;
			frustumCornerDir.x = System.Math.Cos(hFov);
			frustumCornerDir.y = System.Math.Sin(vFov);
			frustumCornerDir.z = System.Math.Sin(hFov) + System.Math.Cos(vFov);
			frustumCornerDir = Vector3d.Normalize(frustumCornerDir);

			Vector3d zAxis;
			zAxis.x = 0;
			zAxis.y = 0;
			zAxis.z = 1;

			//Approximate height of mt everest
			double HighestAltitude = 9000.0;
			double distance = (altitude - HighestAltitude) / Vector3d.Dot(frustumCornerDir, zAxis);

			//Multiply highest altitude by 2 to give a buffer where we still use 0.5 as the near plane
			return System.Math.Min(2e5, altitude > HighestAltitude * 2.0 ? 2.0 * altitude - 2.0 * HighestAltitude - distance : 0.5);
		}

		public double GetLocalScale()
		{
			return MapType == ArcGISMapType.Global ? GeoUtils.EarthRadius : GeoUtils.EarthEquatorLongitude;
		}

		public double GetCameraFarPlane(double near, double Altitude)
		{
			if (MapType == ArcGISMapType.Global)
			{
				return System.Math.Max(near + 0.01f, (GeoUtils.EarthRadius + Altitude) * System.Math.Sqrt(1.0 - System.Math.Pow(GeoUtils.EarthRadius / (GeoUtils.EarthRadius + Altitude), 2)));
			}
			else
			{
				double epsilon = 0.01;
				return System.Math.Max(near + 0.01f, System.Math.Sqrt(System.Math.Pow(Altitude / epsilon, 2) - Altitude * Altitude));
			}
		}
	}
}
