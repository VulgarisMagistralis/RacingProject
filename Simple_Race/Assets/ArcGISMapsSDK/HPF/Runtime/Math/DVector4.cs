

namespace Esri.HPFramework
{

    public struct DVector4
    {
        public DVector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public double x;
        public double y;
        public double z;
        public double w;

        public static explicit operator DVector4(UnityEngine.Vector4 v)
        {
            return new DVector4(v.x, v.y, v.z, v.w);
        }

        public static explicit operator UnityEngine.Vector4(DVector4 v)
        {
            return new UnityEngine.Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
        }
        
        /// <summary>
        /// Adds one DVector4 with another DVector4 component-wise.
        /// </summary>
        /// <param name="a">left hand side of the add operator</param>
        /// <param name="b">right hand side of the add operator</param>
        /// <returns>Component-wise addition of vectors a and b</returns>
        public static DVector4 operator +(DVector4 a, DVector4 b)
        {
            a.x += b.x;
            a.y += b.y;
            a.z += b.z;
            a.w += b.w;
            return a;
        }

        public static double Dot(DVector4 a, DVector4 b)
        {
            return      a.x * b.x + 
                        a.y * b.y + 
                        a.z * b.z + 
                        a.w * b.w;
        }

        public static bool operator ==(DVector4 a, DVector4 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(DVector4 a, DVector4 b)
        {
            return !(a == b);
        }

        public bool Equals(DVector4 other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            if (other is DVector4)
                return this == (DVector4)other;

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = -1743314642;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            hashCode = hashCode * -1521134295 + w.GetHashCode();
            return hashCode;
        }
    }

}