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
using System;

namespace Esri.ArcGISMapsSDK.Utils.GeoCoord
{
	// This is the equivalent for LatLon and is used to avoid instancing API entities
	[Serializable]
	public struct Rotator
	{
		public double Heading;
		public double Pitch;
		public double Roll;

		public Rotator(double heading, double pitch, double roll)
		{
			Heading = heading;
			Pitch = pitch;
			Roll = roll;
		}

		public override int GetHashCode()
		{
			return Heading.GetHashCode() ^ (Pitch.GetHashCode() << 2) ^ (Roll.GetHashCode() >> 2);
		}

		public override bool Equals(object o)
		{
			var rotator = (Rotator)o;
			const double epsilon = 1e-11;

			return System.Math.Abs(rotator.Heading - Heading) < epsilon &&
					System.Math.Abs(rotator.Pitch - Pitch) < epsilon &&
					System.Math.Abs(rotator.Roll - Roll) < epsilon;
		}

		public static bool operator ==(Rotator lhs, Rotator rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Rotator lhs, Rotator rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Rotator Normalize(Rotator rotator)
		{
			Rotator outputRotator;

			outputRotator.Heading = MathUtils.NormalizeAngleDegrees(rotator.Heading);
			outputRotator.Pitch = MathUtils.NormalizeAngleDegrees(rotator.Pitch);
			outputRotator.Roll = MathUtils.NormalizeAngleDegrees(rotator.Roll);

			return outputRotator;
		}
	}
}
