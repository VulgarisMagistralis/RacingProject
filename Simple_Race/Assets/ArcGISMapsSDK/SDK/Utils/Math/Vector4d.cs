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
	public struct Vector4d
	{
		public double x;
		public double y;
		public double z;
		public double w;

		public Vector4d(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public Vector4d Set(double x, double y, double z, double w = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;

			return this;
		}

		public double Length()
		{
			return System.Math.Sqrt(x * x + y * y + z * z + w * w);
		}

		public static double Distance(Vector4d a, Vector4d b)
		{
			return System.Math.Sqrt(System.Math.Pow(a.x - b.x, 2.0) + System.Math.Pow(a.y - b.y, 2.0) + System.Math.Pow(a.z - b.z, 2.0) + System.Math.Pow(a.w - b.w, 2.0));
		}

		public override bool Equals(object o)
		{
			var v = (Vector4d)o;
			return v.x == x && v.y == y && v.z == z && v.w == w;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
		}

		public static bool operator !=(Vector4d lhs, Vector4d rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static bool operator ==(Vector4d lhs, Vector4d rhs)
		{
			return lhs.Equals(rhs);
		}

		public static Vector4d operator +(Vector4d lhs, Vector4d rhs)
		{
			return new Vector4d(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}
		public static Vector4d operator +(Vector4d v, double a)
		{
			return new Vector4d(v.x + a, v.y + a, v.z + a, v.w + a);
		}
		public static Vector4d operator +(double a, Vector4d v)
		{
			return new Vector4d(v.x + a, v.y + a, v.z + a, v.w + a);
		}

		public static Vector4d operator -(Vector4d lhs, Vector4d rhs)
		{
			return new Vector4d(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}
		public static Vector4d operator -(Vector4d v, double a)
		{
			return new Vector4d(v.x - a, v.y - a, v.z - a, v.w - a);
		}
		public static Vector4d operator -(double a, Vector4d v)
		{
			return new Vector4d(a - v.x, a - v.y, a - v.z, a - v.w);
		}

		public static Vector4d operator *(Vector4d v, float a)
		{
			return new Vector4d(v.x * a, v.y * a, v.z * a, v.w * a);
		}
		public static Vector4d operator *(float a, Vector4d v)
		{
			return new Vector4d(v.x * a, v.y * a, v.z * a, v.w * a);
		}
		public static Vector4d operator *(Vector4d v, double a)
		{
			return new Vector4d(v.x * a, v.y * a, v.z * a, v.w * a);
		}
		public static Vector4d operator *(double a, Vector4d v)
		{
			return new Vector4d(v.x * a, v.y * a, v.z * a, v.w * a);
		}

		public static Vector4d operator /(Vector4d v, float a)
		{
			return new Vector4d(v.x / a, v.y / a, v.z / a, v.w / a);
		}
		public static Vector4d operator /(float a, Vector4d v)
		{
			return new Vector4d(v.x / a, v.y / a, v.z / a, v.w / a);
		}
		public static Vector4d operator /(Vector4d v, double a)
		{
			return new Vector4d(v.x / a, v.y / a, v.z / a, v.w / a);
		}
		public static Vector4d operator /(double a, Vector4d v)
		{
			return new Vector4d(v.x / a, v.y / a, v.z / a, v.w / a);
		}

		public static double Dot(Vector4d lhs, Vector4d rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }

		public static Vector4d Normalize(Vector4d v)
		{
			var inverseLength = 1.0 / v.Length();
			return new Vector4d(v.x * inverseLength, v.y * inverseLength, v.z * inverseLength, v.w * inverseLength);
		}

		public static Vector3d ToVector3d(Vector4d v)
		{
			Vector3d output;
			output.x = v.x;
			output.y = v.y;
			output.z = v.z;

			return output;
		}

		public static implicit operator Vector3d(Vector4d v)
		{
			return ToVector3d(v);
		}
	}
}
