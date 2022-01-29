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
	public struct Vector2d
	{
		public double x;
		public double y;

		public Vector2d(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2d Set(double x, double y)
		{
			this.x = x;
			this.y = y;

			return this;
		}

		public double Length()
		{
			return System.Math.Sqrt(x * x + y * y);
		}

		public static double Distance(Vector2d a, Vector2d b)
		{
			return System.Math.Sqrt(System.Math.Pow(a.x - b.x, 2.0) + System.Math.Pow(a.y - b.y, 2.0));
		}

		public override bool Equals(object o)
		{
			var v = (Vector2d)o;
			return v.x == x && v.y == y;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2);
		}

		public static bool operator !=(Vector2d lhs, Vector2d rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static bool operator ==(Vector2d lhs, Vector2d rhs)
		{
			return lhs.Equals(rhs);
		}

		public static Vector2d operator +(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(lhs.x + rhs.x, lhs.y + rhs.y);
		}
		public static Vector2d operator +(Vector2d v, double a)
		{
			return new Vector2d(v.x + a, v.y + a);
		}
		public static Vector2d operator +(double a, Vector2d v)
		{
			return new Vector2d(v.x + a, v.y + a);
		}

		public static Vector2d operator -(Vector2d lhs, Vector2d rhs)
		{
			return new Vector2d(lhs.x - rhs.x, lhs.y - rhs.y);
		}
		public static Vector2d operator -(Vector2d v, double a)
		{
			return new Vector2d(v.x - a, v.y - a);
		}
		public static Vector2d operator -(double a, Vector2d v)
		{
			return new Vector2d(a - v.x, a - v.y);
		}

		public static Vector2d operator *(Vector2d v, float a)
		{
			return new Vector2d(v.x * a, v.y * a);
		}
		public static Vector2d operator *(float a, Vector2d v)
		{
			return new Vector2d(v.x * a, v.y * a);
		}
		public static Vector2d operator *(Vector2d v, double a)
		{
			return new Vector2d(v.x * a, v.y * a);
		}
		public static Vector2d operator *(double a, Vector2d v)
		{
			return new Vector2d(v.x * a, v.y * a);
		}

		public static Vector2d operator /(Vector2d v, float a)
		{
			return new Vector2d(v.x / a, v.y / a);
		}
		public static Vector2d operator /(float a, Vector2d v)
		{
			return new Vector2d(v.x / a, v.y / a);
		}
		public static Vector2d operator /(Vector2d v, double a)
		{
			return new Vector2d(v.x / a, v.y / a);
		}
		public static Vector2d operator /(double a, Vector2d v)
		{
			return new Vector2d(v.x / a, v.y / a);
		}

		public static double Dot(Vector2d lhs, Vector2d rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }

		public static Vector2d Normalize(Vector2d v)
		{
			var inverseLength = 1.0 / v.Length();
			return new Vector2d(v.x * inverseLength, v.y * inverseLength);
		}
	}
}
