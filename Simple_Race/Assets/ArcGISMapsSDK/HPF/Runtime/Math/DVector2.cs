

namespace Esri.HPFramework
{

    /// <summary>
    /// The DVector2 is similar to Unity's Vector2, except that it is composed of double
    /// precision values rather than single precision values. Note that in its current state, it is
    /// missing many Vector2 math operations.
    /// </summary>
    [System.Serializable]
    public struct DVector2
    {
        public DVector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// The x coordinate, in double precision
        /// </summary>
        public double x;

        /// <summary>
        /// The y coordinate, in double precision
        /// </summary>
        public double y;

        public double magnitude => System.Math.Sqrt(sqrMagnitude);
        public double sqrMagnitude => x * x + y * y;




        /// <summary>
        /// Explicit cast from UnityEngine.Vector2
        /// </summary>
        /// <param name="v">Single precision vector which will be converted to double precision</param>
        public static explicit operator DVector2(UnityEngine.Vector2 v)
        {
            return new DVector2(v.x, v.y);
        }

        /// <summary>
        /// Explicit cast to UnityEngine.Vector2
        /// </summary>
        /// <param name="v">Double precision vector which will be converted to single precision</param>
        public static explicit operator UnityEngine.Vector2(DVector2 v)
        {
            return new UnityEngine.Vector2((float)v.x, (float)v.y);
        }

        /// <summary>
        /// Adds one DVector2 with another DVector2 component-wise.
        /// </summary>
        /// <param name="a">left hand side of the add operator</param>
        /// <param name="b">right hand side of the add operator</param>
        /// <returns>Component-wise addition of vectors a and b</returns>
        public static DVector2 operator +(DVector2 a, DVector2 b)
        {
            a.x += b.x;
            a.y += b.y;
            return a;
        }

        public static DVector2 operator -(DVector2 a, DVector2 b)
        {
            a.x -= b.x;
            a.y -= b.y;
            return a;
        }

        public static DVector2 operator -(DVector2 a)
        {
            a.x = -a.x;
            a.y = -a.y;
            return a;
        }

        public static DVector2 Max(DVector2 a, DVector2 b)
        {
            return new DVector2
            {
                x = System.Math.Max(a.x, b.x),
                y = System.Math.Max(a.y, b.y),
            };
        }

        public static DVector2 Min(DVector2 a, DVector2 b)
        {
            return new DVector2
            {
                x = System.Math.Min(a.x, b.x),
                y = System.Math.Min(a.y, b.y),
            };
        }

        public static bool operator ==(DVector2 a, DVector2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(DVector2 a, DVector2 b)
        {
            return !(a == b);
        }

        public bool Equals(DVector2 other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            if (other is DVector2)
                return this == (DVector2)other;

            return false;
        }


        /// <summary>
        /// Multiplies each component of the DVector2 with a scalar value
        /// </summary>
        /// <param name="k">left hand scalar value</param>
        /// <param name="a">right hand vector value</param>
        /// <returns>Vector multiplied by the scalar value</returns>
        public static DVector2 operator *(double k, DVector2 a)
        {
            a.x *= k;
            a.y *= k;
            return a;
        }

        /// <summary>
        /// A vector initialized with all values at zero.
        /// </summary>
        public static DVector2 zero => new DVector2(0, 0);

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public override int GetHashCode()
        {
            int hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }

}