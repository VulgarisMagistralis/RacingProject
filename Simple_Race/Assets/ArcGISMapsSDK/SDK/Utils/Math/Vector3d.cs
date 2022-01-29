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
	public struct Vector3d
	{
		public double x;
		public double y;
		public double z;

		public Vector3d(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3d Set(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;

			return this;
		}

		public double Length()
		{
			return System.Math.Sqrt(x*x + y*y + z*z);
		}

		public double SqrtLength()
		{
			return x * x + y * y + z * z;
		}

		public static Vector3d zero => new Vector3d(0, 0, 0);

		public static double Distance(Vector3d a, Vector3d b)
		{
			return System.Math.Sqrt(System.Math.Pow(a.x - b.x, 2.0) + System.Math.Pow(a.y - b.y, 2.0) + System.Math.Pow(a.z - b.z, 2.0));
		}

		public override bool Equals(object o)
		{
			var v = (Vector3d)o;
			const double epsilon = 1e-11;

			return System.Math.Abs(v.x - x) < epsilon &&
					System.Math.Abs(v.y - y) < epsilon &&
					System.Math.Abs(v.z - z) < epsilon;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
		}

		public static bool operator !=(Vector3d lhs, Vector3d rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static bool operator ==(Vector3d lhs, Vector3d rhs)
		{
			return lhs.Equals(rhs);
		}

		public static Vector3d operator -(Vector3d v)
		{
			return new Vector3d(-v.x, -v.y, -v.z);
		}

		public static Vector3d operator +(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}
		public static Vector3d operator +(Vector3d v, double a)
		{
			return new Vector3d(v.x + a, v.y + a, v.z + a);
		}
		public static Vector3d operator +(double a, Vector3d v)
		{
			return new Vector3d(v.x + a, v.y + a, v.z + a);
		}

		public static Vector3d operator -(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}
		public static Vector3d operator -(Vector3d v, double a)
		{
			return new Vector3d(v.x - a, v.y - a, v.z - a);
		}
		public static Vector3d operator -(double a, Vector3d v)
		{
			return new Vector3d(a - v.x, a - v.y, a - v.z);
		}

		public static Vector3d operator *(Vector3d v, float a)
		{
			return new Vector3d(v.x * a, v.y * a, v.z * a);
		}
		public static Vector3d operator *(float a, Vector3d v)
		{
			return new Vector3d(v.x * a, v.y * a, v.z * a);
		}
		public static Vector3d operator *(Vector3d v, double a)
		{
			return new Vector3d(v.x * a, v.y * a, v.z * a);
		}
		public static Vector3d operator *(double a, Vector3d v)
		{
			return new Vector3d(v.x * a, v.y * a, v.z * a);
		}

		public static Vector3d operator /(Vector3d v, float a)
		{
			return new Vector3d(v.x / a, v.y / a, v.z / a);
		}
		public static Vector3d operator /(float a, Vector3d v)
		{
			return new Vector3d(v.x / a, v.y / a, v.z / a);
		}
		public static Vector3d operator /(Vector3d v, double a)
		{
			return new Vector3d(v.x / a, v.y / a, v.z / a);
		}
		public static Vector3d operator /(double a, Vector3d v)
		{
			return new Vector3d(v.x / a, v.y / a, v.z / a);
		}

		public static explicit operator Vector3d(UnityEngine.Vector3 v)
		{
			return new Vector3d(v.x, v.y, v.z);
		}

		public static explicit operator Vector3d(Esri.HPFramework.DVector3 v)
		{
			return new Vector3d(v.x, v.y, v.z);
		}

		public static double Dot(Vector3d lhs, Vector3d rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }

		public static Vector3d Cross(Vector3d lhs, Vector3d rhs)
		{
			return new Vector3d(
				lhs.y * rhs.z - lhs.z * rhs.y,
				lhs.z * rhs.x - lhs.x * rhs.z,
				lhs.x * rhs.y - lhs.y * rhs.x);
		}

		public static Vector3d Normalize(Vector3d v)
		{
			var inverseLength = 1.0/v.Length();
			return new Vector3d(v.x * inverseLength, v.y * inverseLength, v.z * inverseLength);
		}

		public override string ToString()
		{
			return " ( " + x + " " + y + " " + z + " ) ";
		}

		public static Vector3d Zero => new Vector3d(0, 0, 0);
		public static Vector3d One => new Vector3d(1, 1, 1);
		public static Vector3d Right => new Vector3d(1, 0, 0);
		public static Vector3d Left => new Vector3d(-1, 0, 0);
		public static Vector3d Up => new Vector3d(0, 1, 0);
		public static Vector3d Down => new Vector3d(0, -1, 0);
		public static Vector3d Forward => new Vector3d(0, 0, 1);
		public static Vector3d Back => new Vector3d(0, 0, -1);
	}
}
