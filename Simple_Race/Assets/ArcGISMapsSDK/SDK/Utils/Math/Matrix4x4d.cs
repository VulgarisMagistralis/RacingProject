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

namespace Esri.ArcGISMapsSDK.Utils.Math
{
	public struct Matrix4x4d
	{
		public double m00;
		public double m10;
		public double m20;
		public double m30;

		public double m01;
		public double m11;
		public double m21;
		public double m31;

		public double m02;
		public double m12;
		public double m22;
		public double m32;

		public double m03;
		public double m13;
		public double m23;
		public double m33;

		public Matrix4x4d(Vector4d column0, Vector4d column1, Vector4d column2, Vector4d column3)
		{
			this.m00 = column0.x; this.m01 = column1.x; this.m02 = column2.x; this.m03 = column3.x;
			this.m10 = column0.y; this.m11 = column1.y; this.m12 = column2.y; this.m13 = column3.y;
			this.m20 = column0.z; this.m21 = column1.z; this.m22 = column2.z; this.m23 = column3.z;
			this.m30 = column0.w; this.m31 = column1.w; this.m32 = column2.w; this.m33 = column3.w;
		}

		public double this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}

			set
			{
				this[row + column * 4] = value;
			}
		}

		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return m00;
					case 1: return m10;
					case 2: return m20;
					case 3: return m30;
					case 4: return m01;
					case 5: return m11;
					case 6: return m21;
					case 7: return m31;
					case 8: return m02;
					case 9: return m12;
					case 10: return m22;
					case 11: return m32;
					case 12: return m03;
					case 13: return m13;
					case 14: return m23;
					case 15: return m33;
					default:
						throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}

			set
			{
				switch (index)
				{
					case 0: m00 = value; break;
					case 1: m10 = value; break;
					case 2: m20 = value; break;
					case 3: m30 = value; break;
					case 4: m01 = value; break;
					case 5: m11 = value; break;
					case 6: m21 = value; break;
					case 7: m31 = value; break;
					case 8: m02 = value; break;
					case 9: m12 = value; break;
					case 10: m22 = value; break;
					case 11: m32 = value; break;
					case 12: m03 = value; break;
					case 13: m13 = value; break;
					case 14: m23 = value; break;
					case 15: m33 = value; break;

					default:
						throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		public override int GetHashCode()
		{
			return GetColumn(0).GetHashCode() ^ (GetColumn(1).GetHashCode() << 2) ^ (GetColumn(2).GetHashCode() >> 2) ^ (GetColumn(3).GetHashCode() >> 1);
		}

		public override bool Equals(object other)
		{
			if (!(other is Matrix4x4d)) return false;

			return Equals((Matrix4x4d)other);
		}

		public bool Equals(Matrix4x4d other)
		{
			return GetColumn(0).Equals(other.GetColumn(0))
				&& GetColumn(1).Equals(other.GetColumn(1))
				&& GetColumn(2).Equals(other.GetColumn(2))
				&& GetColumn(3).Equals(other.GetColumn(3));
		}

		public static Matrix4x4d operator *(Matrix4x4d lhs, Matrix4x4d rhs)
		{
			Matrix4x4d res = new Matrix4x4d();
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;

			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;

			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;

			res.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			res.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			res.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			res.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;

			return res;
		}

		public static Vector4d operator *(Matrix4x4d lhs, Vector4d vector)
		{
			Vector4d res = new Vector4d();
			res.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			res.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			res.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			res.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return res;
		}

		public static bool operator ==(Matrix4x4d lhs, Matrix4x4d rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0)
				&& lhs.GetColumn(1) == rhs.GetColumn(1)
				&& lhs.GetColumn(2) == rhs.GetColumn(2)
				&& lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		public static bool operator !=(Matrix4x4d lhs, Matrix4x4d rhs)
		{
			return !(lhs == rhs);
		}

		public Vector4d GetColumn(int index)
		{
			switch (index)
			{
				case 0: return new Vector4d(m00, m10, m20, m30);
				case 1: return new Vector4d(m01, m11, m21, m31);
				case 2: return new Vector4d(m02, m12, m22, m32);
				case 3: return new Vector4d(m03, m13, m23, m33);
				default:
					throw new IndexOutOfRangeException("Invalid column index!");
			}
		}

		public Vector4d GetRow(int index)
		{
			switch (index)
			{
				case 0: return new Vector4d(m00, m01, m02, m03);
				case 1: return new Vector4d(m10, m11, m12, m13);
				case 2: return new Vector4d(m20, m21, m22, m23);
				case 3: return new Vector4d(m30, m31, m32, m33);
				default:
					throw new IndexOutOfRangeException("Invalid row index!");
			}
		}

		public void SetColumn(int index, Vector4d column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}

		public void SetRow(int index, Vector4d row)
		{
			this[index, 0] = row.x;
			this[index, 1] = row.y;
			this[index, 2] = row.z;
			this[index, 3] = row.w;
		}

		public Vector3d MultiplyPoint(Vector3d point)
		{
			Vector3d res = new Vector3d();
			double w;
			res.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			res.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			res.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			w = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;

			w = 1 / w;
			res.x *= w;
			res.y *= w;
			res.z *= w;
			return res;
		}

		public Vector3d MultiplyPoint3x4(Vector3d point)
		{
			Vector3d res = new Vector3d();
			res.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			res.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			res.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			return res;
		}

		public Vector3d MultiplyVector(Vector3d vector)
		{
			Vector3d res = new Vector3d();
			res.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
			res.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
			res.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
			return res;
		}

		public static Matrix4x4d Scale(Vector3d vector)
		{
			Matrix4x4d m = new Matrix4x4d();
			m.m00 = vector.x; m.m01 = 0; m.m02 = 0; m.m03 = 0;
			m.m10 = 0; m.m11 = vector.y; m.m12 = 0; m.m13 = 0;
			m.m20 = 0; m.m21 = 0; m.m22 = vector.z; m.m23 = 0;
			m.m30 = 0; m.m31 = 0; m.m32 = 0; m.m33 = 1;
			return m;
		}

		public static Matrix4x4d Translate(Vector3d vector)
		{
			Matrix4x4d m = new Matrix4x4d();
			m.m00 = 1; m.m01 = 0; m.m02 = 0; m.m03 = vector.x;
			m.m10 = 0; m.m11 = 1; m.m12 = 0; m.m13 = vector.y;
			m.m20 = 0; m.m21 = 0; m.m22 = 1; m.m23 = vector.z;
			m.m30 = 0; m.m31 = 0; m.m32 = 0; m.m33 = 1;
			return m;
		}

		public Quaterniond ToQuaterniond()
		{
			Quaterniond result = new Quaterniond();

			result.w = System.Math.Sqrt(System.Math.Max(0, 1 + this[0, 0] + this[1, 1] + this[2, 2])) / 2;
			result.x = System.Math.Sqrt(System.Math.Max(0, 1 + this[0, 0] - this[1, 1] - this[2, 2])) / 2;
			result.y = System.Math.Sqrt(System.Math.Max(0, 1 - this[0, 0] + this[1, 1] - this[2, 2])) / 2;
			result.z = System.Math.Sqrt(System.Math.Max(0, 1 - this[0, 0] - this[1, 1] + this[2, 2])) / 2;

			result.x *= System.Math.Sign(result.x * (this[2, 1] - this[1, 2]));
			result.y *= System.Math.Sign(result.y * (this[0, 2] - this[2, 0]));
			result.z *= System.Math.Sign(result.z * (this[1, 0] - this[0, 1]));

			return result;
		}

		public static Matrix4x4d Rotate(Vector3d axis, double angle)
		{
			double radAngle = angle * MathUtils.DegreesToRadians;
			double sina = System.Math.Sin(0.5 * radAngle);
			double cosa = System.Math.Cos(0.5 * radAngle);

			double qx = axis.x*sina, qy = axis.y*sina, qz = axis.z*sina, qw = cosa;

			double x = qx * 2.0;
			double y = qy * 2.0;
			double z = qz * 2.0;
			double xx = qx * x;
			double yy = qy * y;
			double zz = qz * z;
			double xy = qx * y;
			double xz = qx * z;
			double yz = qy * z;
			double wx = qw * x;
			double wy = qw * y;
			double wz = qw * z;

			Matrix4x4d m = new Matrix4x4d();
			m.m00 = 1.0f - (yy + zz); m.m10 = xy + wz; m.m20 = xz - wy; m.m30 = 0.0;
			m.m01 = xy - wz; m.m11 = 1.0f - (xx + zz); m.m21 = yz + wx; m.m31 = 0.0;
			m.m02 = xz + wy; m.m12 = yz - wx; m.m22 = 1.0f - (xx + yy); m.m32 = 0.0;
			m.m03 = 0.0; m.m13 = 0.0; m.m23 = 0.0; m.m33 = 1.0;
			return m;
		}

		public static Matrix4x4d Zero => new Matrix4x4d();

		public static Matrix4x4d Identity => new Matrix4x4d(new Vector4d(1, 0, 0, 0),
															new Vector4d(0, 1, 0, 0),
															new Vector4d(0, 0, 1, 0),
															new Vector4d(0, 0, 0, 1));
	}
}
