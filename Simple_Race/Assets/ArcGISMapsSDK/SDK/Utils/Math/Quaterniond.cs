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
	public struct Quaterniond
	{
		const double radToDeg = (180.0 / System.Math.PI);
		const double degToRad = (System.Math.PI / 180.0);

		Vector3d xyz
		{
			get
			{
				return new Vector3d(x, y, z);
			}

			set
			{
				x = value.x;
				y = value.y;
				z = value.z;
			}
		}

		public const double kEpsilon = 1E-06f;

		public double x;
		public double y;
		public double z;
		public double w;

		public static Quaterniond Identity => new Quaterniond(0, 0, 0, 1);

		public Vector3d eulerAngles
		{
			get
			{
				return Quaterniond.ToEulerRad(this);
			}
			set
			{
				this = Quaterniond.FromEulerRad(value * degToRad);
			}
		}

		public double Length => System.Math.Sqrt(x * x + y * y + z * z + w * w);

		public double LengthSquared => x * x + y * y + z * z + w * w;

		public Quaterniond(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public Quaterniond(Vector3d v, double w)
		{
			x = v.x;
			y = v.y;
			z = v.z;
			this.w = w;
		}

		public void Set(double new_x, double new_y, double new_z, double new_w)
		{
			x = new_x;
			y = new_y;
			z = new_z;
			w = new_w;
		}

		public void Normalize()
		{
			double scale = 1.0 / Length;
			x *= scale;
			y *= scale;
			z *= scale;
			w *= scale;
		}

		public static Quaterniond Normalize(Quaterniond q)
		{
			Quaterniond result;
			Normalize(ref q, out result);
			return result;
		}

		public static void Normalize(ref Quaterniond q, out Quaterniond result)
		{
			double scale = 1.0 / q.Length;
			result = new Quaterniond(q.x * scale, q.y * scale, q.z * scale, q.w * scale);
		}

		public static double Dot(Quaterniond a, Quaterniond b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public static Quaterniond AngleAxis(double angle, Vector3d axis)
		{
			return Quaterniond.AngleAxis(angle, ref axis);
		}
		private static Quaterniond AngleAxis(double degress, ref Vector3d axis)
		{
			if (axis.SqrtLength() == 0.0)
				return Identity;

			Quaterniond result = Identity;
			var radians = degress * degToRad;
			radians *= 0.5f;
			Vector3d.Normalize(axis);
			axis = axis * System.Math.Sin(radians);
			result.x = axis.x;
			result.y = axis.y;
			result.z = axis.z;
			result.w = System.Math.Cos(radians);

			return Normalize(result);
		}
		public void ToAngleAxis(out double angle, out Vector3d axis)
		{
			Quaterniond.ToAxisAngleRad(this, out axis, out angle);
			angle *= radToDeg;
		}

		public static Quaterniond FromToRotation(Vector3d fromDirection, Vector3d toDirection)
		{
			return RotateTowards(LookRotation(fromDirection), LookRotation(toDirection), double.MaxValue);
		}

		public void SetFromToRotation(Vector3d fromDirection, Vector3d toDirection)
		{
			this = Quaterniond.FromToRotation(fromDirection, toDirection);
		}

		public static Quaterniond LookRotation(Vector3d forward, Vector3d upwards)
		{
			return Quaterniond.LookRotation(ref forward, ref upwards);
		}

		public static Quaterniond LookRotation(Vector3d forward)
		{
			Vector3d up = Vector3d.Up;
			return Quaterniond.LookRotation(ref forward, ref up);
		}

		private static Quaterniond LookRotation(ref Vector3d forward, ref Vector3d up)
		{

			forward = Vector3d.Normalize(forward);
			Vector3d right = Vector3d.Normalize(Vector3d.Cross(up, forward));
			up = Vector3d.Cross(forward, right);
			var m00 = right.x;
			var m01 = right.y;
			var m02 = right.z;
			var m10 = up.x;
			var m11 = up.y;
			var m12 = up.z;
			var m20 = forward.x;
			var m21 = forward.y;
			var m22 = forward.z;

			double num8 = (m00 + m11) + m22;
			var quaternion = new Quaterniond();
			if (num8 > 0f)
			{
				var num = System.Math.Sqrt(num8 + 1f);
				quaternion.w = num * 0.5f;
				num = 0.5f / num;
				quaternion.x = (m12 - m21) * num;
				quaternion.y = (m20 - m02) * num;
				quaternion.z = (m01 - m10) * num;
				return quaternion;
			}
			if ((m00 >= m11) && (m00 >= m22))
			{
				var num7 = System.Math.Sqrt(((1f + m00) - m11) - m22);
				var num4 = 0.5f / num7;
				quaternion.x = 0.5f * num7;
				quaternion.y = (m01 + m10) * num4;
				quaternion.z = (m02 + m20) * num4;
				quaternion.w = (m12 - m21) * num4;
				return quaternion;
			}
			if (m11 > m22)
			{
				var num6 = System.Math.Sqrt(((1f + m11) - m00) - m22);
				var num3 = 0.5f / num6;
				quaternion.x = (m10 + m01) * num3;
				quaternion.y = 0.5f * num6;
				quaternion.z = (m21 + m12) * num3;
				quaternion.w = (m20 - m02) * num3;
				return quaternion;
			}
			var num5 = System.Math.Sqrt(((1f + m22) - m00) - m11);
			var num2 = 0.5f / num5;
			quaternion.x = (m20 + m02) * num2;
			quaternion.y = (m21 + m12) * num2;
			quaternion.z = 0.5f * num5;
			quaternion.w = (m01 - m10) * num2;
			return quaternion;
		}
		public void SetLookRotation(Vector3d view)
		{
			Vector3d up = Vector3d.Up;
			SetLookRotation(view, up);
		}

		public void SetLookRotation(Vector3d view, Vector3d up)
		{
			this = Quaterniond.LookRotation(view, up);
		}

		public static Quaterniond Slerp(Quaterniond a, Quaterniond b, double t)
		{
			return Quaterniond.Slerp(ref a, ref b, t);
		}
		private static Quaterniond Slerp(ref Quaterniond a, ref Quaterniond b, double t)
		{
			if (t > 1) t = 1;
			if (t < 0) t = 0;
			return SlerpUnclamped(ref a, ref b, t);
		}

		public static Quaterniond SlerpUnclamped(Quaterniond a, Quaterniond b, double t)
		{
			return Quaterniond.SlerpUnclamped(ref a, ref b, t);
		}

		private static Quaterniond SlerpUnclamped(ref Quaterniond a, ref Quaterniond b, double t)
		{
			// if either input is zero, return the other.
			if (a.LengthSquared == 0.0)
			{
				if (b.LengthSquared == 0.0)
				{
					return Identity;
				}
				return b;
			}
			else if (b.LengthSquared == 0.0)
			{
				return a;
			}


			double cosHalfAngle = a.w * b.w + Vector3d.Dot(a.xyz, b.xyz);

			if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
			{
				// angle = 0.0f, so just return one input.
				return a;
			}
			else if (cosHalfAngle < 0.0f)
			{
				b.xyz = -b.xyz;
				b.w = -b.w;
				cosHalfAngle = -cosHalfAngle;
			}

			double blendA;
			double blendB;
			if (cosHalfAngle < 0.99f)
			{
				// do proper slerp for big angles
				double halfAngle = System.Math.Acos(cosHalfAngle);
				double sinHalfAngle = System.Math.Sin(halfAngle);
				double oneOverSinHalfAngle = 1.0 / sinHalfAngle;
				blendA = System.Math.Sin(halfAngle * (1.0 - t)) * oneOverSinHalfAngle;
				blendB = System.Math.Sin(halfAngle * t) * oneOverSinHalfAngle;
			}
			else
			{
				// do lerp if angle is really small.
				blendA = 1.0 - t;
				blendB = t;
			}

			Quaterniond result = new Quaterniond(blendA * a.xyz + blendB * b.xyz, blendA * a.w + blendB * b.w);
			if (result.LengthSquared > 0.0f)
				return Normalize(result);
			else
				return Identity;
		}

		public static Quaterniond Lerp(Quaterniond a, Quaterniond b, double t)
		{
			if (t > 1) t = 1;
			if (t < 0) t = 0;
			return Slerp(ref a, ref b, t); // TODO: use lerp not slerp, "Because quaternion works in 4D. Rotation in 4D are linear" ???
		}

		public static Quaterniond LerpUnclamped(Quaterniond a, Quaterniond b, double t)
		{
			return Slerp(ref a, ref b, t);
		}

		public static Quaterniond RotateTowards(Quaterniond from, Quaterniond to, double maxDegreesDelta)
		{
			double num = Quaterniond.Angle(from, to);
			if (num == 0f)
			{
				return to;
			}
			double t = System.Math.Min(1f, maxDegreesDelta / num);
			return Quaterniond.SlerpUnclamped(from, to, t);
		}

		public static Quaterniond Inverse(Quaterniond rotation)
		{
			double lengthSq = rotation.LengthSquared;
			if (lengthSq != 0.0)
			{
				double i = 1.0 / lengthSq;
				return new Quaterniond(rotation.xyz * -i, rotation.w * i);
			}
			return rotation;
		}

		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", this.x, this.y, this.z, this.w);
		}

		public string ToString(string format)
		{
			return string.Format("({0}, {1}, {2}, {3})", this.x.ToString(format), this.y.ToString(format), this.z.ToString(format), this.w.ToString(format));
		}

		public static double Angle(Quaterniond a, Quaterniond b)
		{
			double f = Quaterniond.Dot(a, b);
			return System.Math.Acos(System.Math.Min(System.Math.Abs(f), 1f)) * 2f * radToDeg;
		}

		public static Quaterniond Euler(double x, double y, double z)
		{
			return Quaterniond.FromEulerRad(new Vector3d(x, y, z) * degToRad);
		}

		public static Quaterniond Euler(Vector3d euler)
		{
			return Quaterniond.FromEulerRad(euler * degToRad);
		}

		private static Vector3d ToEulerRad(Quaterniond rotation)
		{
			double sqw = rotation.w * rotation.w;
			double sqx = rotation.x * rotation.x;
			double sqy = rotation.y * rotation.y;
			double sqz = rotation.z * rotation.z;
			double unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
			double test = rotation.x * rotation.w - rotation.y * rotation.z;
			Vector3d v;

			if (test > 0.4995 * unit)
			{ // singularity at north pole
				v.y = 2 * System.Math.Atan2(rotation.y, rotation.x);
				v.x = -System.Math.PI / 2;
				v.z = 0;
				return NormalizeAngles(v * radToDeg);
			}
			if (test < -0.4995 * unit)
			{ // singularity at south pole
				v.y = -2 * System.Math.Atan2(rotation.y, rotation.x);
				v.x = System.Math.PI / 2;
				v.z = 0;
				return NormalizeAngles(v * radToDeg);
			}

			// Quaternion to Left-Handled rotation matrix
			// Given a quaternion q = qw + qx * i + qy * j + qz * k, we can create the following rotation matrix
			// Q = {{1 - 2*(qy*qy + qz*qz), 2*(qx*qy - qz*qw),		2*(qx*qz + qy*qw)},
			//		{2*(qx*qy + qz*qw),		1 - 2*(qx*qx + qz*qz),	2*(qy*qz - qx*qw)},
			//		{2*(qx*qz - qy*qw),		2*(qy*qz + qx*qw),		1 - 2*(qx*qx + qy*qy)}}

			// The following expresion is come from euler rotation matrix YXZ used by Unity and the above matrix.

			Quaterniond q = new Quaterniond(rotation.x, rotation.y, rotation.z, rotation.w);
			v.x = System.Math.Asin(-2 * (q.w * q.x - q.y * q.z));                                       // Pitch
			v.y = System.Math.Atan2(2 * q.w * q.y + 2 * q.z * q.x, 1 - 2 * (q.x * q.x + q.y * q.y));	// Heading			
			v.z = System.Math.Atan2(2 * q.w * q.z + 2 * q.x * q.y, 1 - 2 * (q.z * q.z + q.x * q.x));    // Roll

			return NormalizeAngles(v * radToDeg);
		}
		private static Vector3d NormalizeAngles(Vector3d angles)
		{
			angles.x = MathUtils.NormalizeAngleDegrees(angles.x);
			angles.y = MathUtils.NormalizeAngleDegrees(angles.y);
			angles.z = MathUtils.NormalizeAngleDegrees(angles.z);
			return angles;
		}

		private static Quaterniond FromEulerRad(Vector3d euler)
		{
			var pitch = euler.x;
			var yaw = euler.y;
			var roll = euler.z;

			double yawOver2 = yaw * 0.5;
			double pitchOver2 = pitch * 0.5;
			double rollOver2 = roll * 0.5;

			Quaterniond qx = new Quaterniond(System.Math.Sin(pitchOver2), 0, 0, System.Math.Cos(pitchOver2));
			Quaterniond qy = new Quaterniond(0, System.Math.Sin(yawOver2), 0, System.Math.Cos(yawOver2));
			Quaterniond qz = new Quaterniond(0, 0, System.Math.Sin(rollOver2), System.Math.Cos(rollOver2));

			// Euler rotation order YXZ used by Unity.
			return qy*qx*qz;
		}

		private static void ToAxisAngleRad(Quaterniond q, out Vector3d axis, out double angle)
		{
			if (System.Math.Abs(q.w) > 1.0f)
				q.Normalize();
			angle = 2.0f * (float)System.Math.Acos(q.w); // angle
			double den = (float)System.Math.Sqrt(1.0 - q.w * q.w);
			if (den > 0.0001f)
			{
				axis.x = q.x / den;
				axis.y = q.y / den;
				axis.z = q.z / den;
			}
			else
			{
				// This occurs when the angle is zero.
				// Not a problem: just set an arbitrary normalized axis.
				axis = new Vector3d(1, 0, 0);
			}
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		public override bool Equals(object other)
		{
			if (!(other is Quaterniond))
			{
				return false;
			}
			Quaterniond quaternion = (Quaterniond)other;
			return x.Equals(quaternion.x) && y.Equals(quaternion.y) && z.Equals(quaternion.z) && w.Equals(quaternion.w);
		}

		public bool Equals(Quaterniond other)
		{
			return x.Equals(x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
		}

		public static Quaterniond operator *(Quaterniond lhs, Quaterniond rhs)
		{
			return new Quaterniond(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		public static Vector3d operator *(Quaterniond rotation, Vector3d point)
		{
			double num = rotation.x * 2f;
			double num2 = rotation.y * 2f;
			double num3 = rotation.z * 2f;
			double num4 = rotation.x * num;
			double num5 = rotation.y * num2;
			double num6 = rotation.z * num3;
			double num7 = rotation.x * num2;
			double num8 = rotation.x * num3;
			double num9 = rotation.y * num3;
			double num10 = rotation.w * num;
			double num11 = rotation.w * num2;
			double num12 = rotation.w * num3;
			Vector3d result;
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		public static bool operator ==(Quaterniond lhs, Quaterniond rhs)
		{
			return Quaterniond.Dot(lhs, rhs) > 0.999999f;
		}

		public static bool operator !=(Quaterniond lhs, Quaterniond rhs)
		{
			return Quaterniond.Dot(lhs, rhs) <= 0.999999f;
		}
	}
}
