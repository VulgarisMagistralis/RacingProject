

namespace Esri.HPFramework
{

    /// <summary>
    /// The DVector3 is similar to Unity's Vector3, except that it is composed of double
    /// precision values rather than single precision values. Note that in its current state, it is
    /// missing many Vector3 math operations.
    /// </summary>
    [System.Serializable]
    public struct DVector3
    {
        public DVector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// The x coordinate, in double precision
        /// </summary>
        public double x;

        /// <summary>
        /// The y coordinate, in double precision
        /// </summary>
        public double y;

        /// <summary>
        /// The z coordinate, in double precision
        /// </summary>
        public double z;

        public double magnitude => System.Math.Sqrt(sqrMagnitude);
        public double sqrMagnitude => x * x + y * y + z * z;




        /// <summary>
        /// Explicit cast from UnityEngine.Vector3
        /// </summary>
        /// <param name="v">Single precision vector which will be converted to double precision</param>
        public static explicit operator DVector3(UnityEngine.Vector3 v)
        {
            return new DVector3(v.x, v.y, v.z);
        }

        /// <summary>
        /// Explicit cast to UnityEngine.Vector3
        /// </summary>
        /// <param name="v">Double precision vector which will be converted to single precision</param>
        public static explicit operator UnityEngine.Vector3(DVector3 v)
        {
            return new UnityEngine.Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        /// <summary>
        /// Adds one DVector3 with another DVector3 component-wise.
        /// </summary>
        /// <param name="a">left hand side of the add operator</param>
        /// <param name="b">right hand side of the add operator</param>
        /// <returns>Component-wise addition of vectors a and b</returns>
        public static DVector3 operator +(DVector3 a, DVector3 b)
        {
            a.x += b.x;
            a.y += b.y;
            a.z += b.z;
            return a;
        }

        public static DVector3 operator -(DVector3 a, DVector3 b)
        {
            a.x -= b.x;
            a.y -= b.y;
            a.z -= b.z;
            return a;
        }

        public static DVector3 operator -(DVector3 a)
        {
            a.x = -a.x;
            a.y = -a.y;
            a.z = -a.z;
            return a;
        }

        public static bool operator ==(DVector3 a, DVector3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(DVector3 a, DVector3 b)
        {
            return !(a == b);
        }

        public bool Equals(DVector3 other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            if (other is DVector3)
                return this == (DVector3)other;

            return false;
        }


        /// <summary>
        /// Multiplies each component of the DVector3 with a scalar value
        /// </summary>
        /// <param name="k">left hand scalar value</param>
        /// <param name="a">right hand vector value</param>
        /// <returns>Vector multiplied by the scalar value</returns>
        public static DVector3 operator *(double k, DVector3 a)
        {
            a.x *= k;
            a.y *= k;
            a.z *= k;
            return a;
        }

        public static double Dot(DVector3 a, DVector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static DVector3 LerpUnclamped(DVector3 a, DVector3 b, double k)
        {
            return k * b + (1.0 - k) * a;
        }


        public static DVector3 Max(DVector3 a, DVector3 b)
        {
            return new DVector3
            {
                x = System.Math.Max(a.x, b.x),
                y = System.Math.Max(a.y, b.y),
                z = System.Math.Max(a.z, b.z),
            };
        }

        public static DVector3 Min(DVector3 a, DVector3 b)
        {
            return new DVector3
            {
                x = System.Math.Min(a.x, b.x),
                y = System.Math.Min(a.y, b.y),
                z = System.Math.Min(a.z, b.z),
            };
        }

        /// <summary>
        /// A vector initialized with all values at zero.
        /// </summary>
        public static DVector3 zero => new DVector3(0, 0, 0);
        public static DVector3 up => new DVector3(0, 1, 0);
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public override int GetHashCode()
        {
            int hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }
    }

}