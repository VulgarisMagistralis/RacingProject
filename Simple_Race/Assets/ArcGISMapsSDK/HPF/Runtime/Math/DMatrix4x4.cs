using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.Assertions;

namespace Esri.HPFramework
{
    //
    //  TODO - Unit test further
    //
    [System.Serializable]
    public struct DMatrix4x4
    {
        public DMatrix4x4(params double[] columnMajor)
        {
            Assert.AreEqual(16, columnMajor.Length);

            unsafe
            {
                fixed (double* p = columnMajor)
                    Copy(p, out this);
            }
        }

        public override string ToString()
        {
            return $"{m00}\t{m01}\t{m02}\t{m03}\n{m10}\t{m11}\t{m12}\t{m13}\n{m20}\t{m21}\t{m22}\t{m23}\n{m30}\t{m31}\t{m32}\t{m33}\n";
        }

        private const double k_NilDeterminantThreshold = 1e-50;

        public double m00, m01, m02, m03,
                        m10, m11, m12, m13,
                        m20, m21, m22, m23,
                        m30, m31, m32, m33;

        //public Matrix4x4 lowPrecision;
        //private Matrix<double> m_MathnetMatrix;

        private static unsafe void Copy(double* src, out DMatrix4x4 dst)
        {
            dst.m00 = src[0];
            dst.m10 = src[1];
            dst.m20 = src[2];
            dst.m30 = src[3];

            dst.m01 = src[4];
            dst.m11 = src[5];
            dst.m21 = src[6];
            dst.m31 = src[7];

            dst.m02 = src[8];
            dst.m12 = src[9];
            dst.m22 = src[10];
            dst.m32 = src[11];

            dst.m03 = src[12];
            dst.m13 = src[13];
            dst.m23 = src[14];
            dst.m33 = src[15];
        }

        private unsafe void Copy(ref DMatrix4x4 src, double* dst)
        {
            dst[0] = src.m00;
            dst[1] = src.m10;
            dst[2] = src.m20;
            dst[3] = src.m30;

            dst[4] = src.m01;
            dst[5] = src.m11;
            dst[6] = src.m21;
            dst[7] = src.m31;

            dst[8] = src.m02;
            dst[9] = src.m12;
            dst[10] = src.m22;
            dst[11] = src.m32;

            dst[12] = src.m03;
            dst[13] = src.m13;
            dst[14] = src.m23;
            dst[15] = src.m33;
        }

        internal unsafe DMatrix4x4 inverse3d
        {
            get
            {
                //
                //  Code adapted from Matrix4x4.cpp
                //
                double det = 0;

                // Calculate the determinant of upper left 3x3 sub-matrix and
                // determine if the matrix is singular.
                det += m00 * m11 * m22;
                det += m10 * m21 * m02;
                det += m20 * m01 * m12;
                det -= m20 * m11 * m02;
                det -= m10 * m01 * m22;
                det -= m00 * m21 * m12;

                if (det * det < k_NilDeterminantThreshold)
                    return DMatrix4x4.NaN;

                det = 1.0 / det;

                DMatrix4x4 result;

                result.m00 = ((m11 * m22 - m21 * m12) * det);
                result.m01 = (-(m01 * m22 - m21 * m02) * det);
                result.m02 = ((m01 * m12 - m11 * m02) * det);
                result.m10 = (-(m10 * m22 - m20 * m12) * det);
                result.m11 = ((m00 * m22 - m20 * m02) * det);
                result.m12 = (-(m00 * m12 - m10 * m02) * det);
                result.m20 = ((m10 * m21 - m20 * m11) * det);
                result.m21 = (-(m00 * m21 - m20 * m01) * det);
                result.m22 = ((m00 * m11 - m10 * m01) * det);


                // Do the translation part
                result.m03 = -(m03 * result.m00 +
                    m13 * result.m01 +
                    m23 * result.m02);
                result.m13 = -(m03 * result.m10 +
                    m13 * result.m11 +
                    m23 * result.m12);
                result.m23 = -(m03 * result.m20 +
                    m13 * result.m21 +
                    m23 * result.m22);

                result.m30 = 0.0f;
                result.m31 = 0.0f;
                result.m32 = 0.0f;
                result.m33 = 1.0f;

                return result;
            }
            
        }
        internal unsafe struct WorkingBuffer4x8
        {
            public fixed double buffer[4*8];
        }

        private unsafe static void SwapRows(ref double* a, ref double* b)
        {
            double* tmp = a;
            a = b;
            b = tmp;
        }

        public unsafe DMatrix4x4 inverse
        {
            get
            {
                WorkingBuffer4x8 wtmp;

                double m0, m1, m2, m3, s;
                double* r0, r1, r2, r3;

                r0 = &wtmp.buffer[0 * 8];
                r1 = &wtmp.buffer[1 * 8];
                r2 = &wtmp.buffer[2 * 8];
                r3 = &wtmp.buffer[3 * 8];

                r0[0] = m00; r0[1] = m01;
                r0[2] = m02; r0[3] = m03;
                r0[4] = 1.0; r0[5] = r0[6] = r0[7] = 0.0;

                r1[0] = m10; r1[1] = m11;
                r1[2] = m12; r1[3] = m13;
                r1[5] = 1.0; r1[4] = r1[6] = r1[7] = 0.0;

                r2[0] = m20; r2[1] = m21;
                r2[2] = m22; r2[3] = m23;
                r2[6] = 1.0; r2[4] = r2[5] = r2[7] = 0.0;

                r3[0] = m30; r3[1] = m31;
                r3[2] = m32; r3[3] = m33;
                r3[7] = 1.0; r3[4] = r3[5] = r3[6] = 0.0;

                /* choose pivot - or die */
                if (System.Math.Abs(r3[0]) > System.Math.Abs(r2[0]))
                    SwapRows(ref r3, ref r2);
                if (System.Math.Abs(r2[0]) > System.Math.Abs(r1[0]))
                    SwapRows(ref r2, ref r1);
                if (System.Math.Abs(r1[0]) > System.Math.Abs(r0[0]))
                    SwapRows(ref r1, ref r0);
                if (0.0F == r0[0])
                    return NaN;

                /* eliminate first variable     */
                m1 = r1[0] / r0[0]; m2 = r2[0] / r0[0]; m3 = r3[0] / r0[0];
                s = r0[1]; r1[1] -= m1 * s; r2[1] -= m2 * s; r3[1] -= m3 * s;
                s = r0[2]; r1[2] -= m1 * s; r2[2] -= m2 * s; r3[2] -= m3 * s;
                s = r0[3]; r1[3] -= m1 * s; r2[3] -= m2 * s; r3[3] -= m3 * s;
                s = r0[4];
                if (s != 0.0)
                {
                    r1[4] -= m1 * s; r2[4] -= m2 * s; r3[4] -= m3 * s;
                }
                s = r0[5];
                if (s != 0.0F)
                {
                    r1[5] -= m1 * s; r2[5] -= m2 * s; r3[5] -= m3 * s;
                }
                s = r0[6];
                if (s != 0.0F)
                {
                    r1[6] -= m1 * s; r2[6] -= m2 * s; r3[6] -= m3 * s;
                }
                s = r0[7];
                if (s != 0.0F)
                {
                    r1[7] -= m1 * s; r2[7] -= m2 * s; r3[7] -= m3 * s;
                }

                /* choose pivot - or die */
                if (System.Math.Abs(r3[1]) > System.Math.Abs(r2[1]))
                    SwapRows(ref r3, ref r2);
                if (System.Math.Abs(r2[1]) > System.Math.Abs(r1[1]))
                    SwapRows(ref r2, ref r1);
                if (0.0 == r1[1])
                    return NaN;

                /* eliminate second variable */
                m2 = r2[1] / r1[1]; m3 = r3[1] / r1[1];
                r2[2] -= m2 * r1[2]; r3[2] -= m3 * r1[2];
                r2[3] -= m2 * r1[3]; r3[3] -= m3 * r1[3];
                s = r1[4]; if (0.0F != s)
                {
                    r2[4] -= m2 * s; r3[4] -= m3 * s;
                }
                s = r1[5]; if (0.0F != s)
                {
                    r2[5] -= m2 * s; r3[5] -= m3 * s;
                }
                s = r1[6]; if (0.0F != s)
                {
                    r2[6] -= m2 * s; r3[6] -= m3 * s;
                }
                s = r1[7]; if (0.0F != s)
                {
                    r2[7] -= m2 * s; r3[7] -= m3 * s;
                }

                /* choose pivot - or die */
                if (System.Math.Abs(r3[2]) > System.Math.Abs(r2[2]))
                    SwapRows(ref r3, ref r2);
                if (0.0 == r2[2])
                    return NaN;

                /* eliminate third variable */
                m3 = r3[2] / r2[2];
                r3[3] -= m3 * r2[3]; r3[4] -= m3 * r2[4];
                r3[5] -= m3 * r2[5]; r3[6] -= m3 * r2[6];
                r3[7] -= m3 * r2[7];

                /* last check */
                if (0.0 == r3[3])
                    return NaN;

                s = 1.0 / r3[3];          /* now back substitute row 3 */
                r3[4] *= s; r3[5] *= s; r3[6] *= s; r3[7] *= s;

                m2 = r2[3];                /* now back substitute row 2 */
                s = 1.0F / r2[2];
                r2[4] = s * (r2[4] - r3[4] * m2); r2[5] = s * (r2[5] - r3[5] * m2);
                r2[6] = s * (r2[6] - r3[6] * m2); r2[7] = s * (r2[7] - r3[7] * m2);
                m1 = r1[3];
                r1[4] -= r3[4] * m1; r1[5] -= r3[5] * m1;
                r1[6] -= r3[6] * m1; r1[7] -= r3[7] * m1;
                m0 = r0[3];
                r0[4] -= r3[4] * m0; r0[5] -= r3[5] * m0;
                r0[6] -= r3[6] * m0; r0[7] -= r3[7] * m0;

                m1 = r1[2];                /* now back substitute row 1 */
                s = 1.0F / r1[1];
                r1[4] = s * (r1[4] - r2[4] * m1); r1[5] = s * (r1[5] - r2[5] * m1);
                r1[6] = s * (r1[6] - r2[6] * m1); r1[7] = s * (r1[7] - r2[7] * m1);
                m0 = r0[2];
                r0[4] -= r2[4] * m0; r0[5] -= r2[5] * m0;
                r0[6] -= r2[6] * m0; r0[7] -= r2[7] * m0;

                m0 = r0[1];                /* now back substitute row 0 */
                s = 1.0F / r0[0];
                r0[4] = s * (r0[4] - r1[4] * m0); r0[5] = s * (r0[5] - r1[5] * m0);
                r0[6] = s * (r0[6] - r1[6] * m0); r0[7] = s * (r0[7] - r1[7] * m0);

                DMatrix4x4 result;

                result.m00 = r0[4]; result.m01 = r0[5]; result.m02 = r0[6]; result.m03 = r0[7];
                result.m10 = r1[4]; result.m11 = r1[5]; result.m12 = r1[6]; result.m13 = r1[7];
                result.m20 = r2[4]; result.m21 = r2[5]; result.m22 = r2[6]; result.m23 = r2[7];
                result.m30 = r3[4]; result.m31 = r3[5]; result.m32 = r3[6]; result.m33 = r3[7];

                return result;
            }

        }



        public static DMatrix4x4 operator *(DMatrix4x4 a, DMatrix4x4 b)
        {
            DMatrix4x4 result;

            DVector4 r0 = a.GetRow(0);
            DVector4 r1 = a.GetRow(1);
            DVector4 r2 = a.GetRow(2);
            DVector4 r3 = a.GetRow(3);

            DVector4 c0 = b.GetColumn(0);
            DVector4 c1 = b.GetColumn(1);
            DVector4 c2 = b.GetColumn(2);
            DVector4 c3 = b.GetColumn(3);

            result.m00 = DVector4.Dot(r0, c0);
            result.m01 = DVector4.Dot(r0, c1);
            result.m02 = DVector4.Dot(r0, c2);
            result.m03 = DVector4.Dot(r0, c3);

            result.m10 = DVector4.Dot(r1, c0);
            result.m11 = DVector4.Dot(r1, c1);
            result.m12 = DVector4.Dot(r1, c2);
            result.m13 = DVector4.Dot(r1, c3);

            result.m20 = DVector4.Dot(r2, c0);
            result.m21 = DVector4.Dot(r2, c1);
            result.m22 = DVector4.Dot(r2, c2);
            result.m23 = DVector4.Dot(r2, c3);

            result.m30 = DVector4.Dot(r3, c0);
            result.m31 = DVector4.Dot(r3, c1);
            result.m32 = DVector4.Dot(r3, c2);
            result.m33 = DVector4.Dot(r3, c3);

            return result;
        }

        

        public static explicit operator UnityEngine.Matrix4x4(DMatrix4x4 m)
        {
            return new Matrix4x4
            {
                m00 = (float)m.m00,
                m01 = (float)m.m01,
                m02 = (float)m.m02,
                m03 = (float)m.m03,
                m10 = (float)m.m10,
                m11 = (float)m.m11,
                m12 = (float)m.m12,
                m13 = (float)m.m13,
                m20 = (float)m.m20,
                m21 = (float)m.m21,
                m22 = (float)m.m22,
                m23 = (float)m.m23,
                m30 = (float)m.m30,
                m31 = (float)m.m31,
                m32 = (float)m.m32,
                m33 = (float)m.m33,
            };
        }

        public DVector4 GetColumn(int index)
        {
            if (index < 0 || index >= 4)
                throw new System.IndexOutOfRangeException();

            switch(index)
            {
                default: 
                case 0: return new DVector4(m00, m10, m20, m30);
                case 1: return new DVector4(m01, m11, m21, m31);
                case 2: return new DVector4(m02, m12, m22, m32);
                case 3: return new DVector4(m03, m13, m23, m33);
            }    
        }

        public DVector4 GetRow(int index)
        {
            if (index < 0 || index >= 4)
                throw new System.IndexOutOfRangeException();

            switch (index)
            {
                default:
                case 0: return new DVector4(m00, m01, m02, m03);
                case 1: return new DVector4(m10, m11, m12, m13);
                case 2: return new DVector4(m20, m21, m22, m23);
                case 3: return new DVector4(m30, m31, m32, m33);
            }
        }


        public DVector3 MultiplyPoint(DVector3 point)
        {
            
            double x = m00 * point.x + m01 * point.y + m02 * point.z + m03;
            double y = m10 * point.x + m11 * point.y + m12 * point.z + m13;
            double z = m20 * point.x + m21 * point.y + m22 * point.z + m23;
            double w = m30 * point.x + m31 * point.y + m32 * point.z + m33;
            w = 1.0 / w;
            return new DVector3(w * x, w * y, w * z);
        }



        public DVector3 MultiplyVector(DVector3 vector)
        {
            DVector3 result;
            result.x = m00 * vector.x + m01 * vector.y + m02 * vector.z;
            result.y = m10 * vector.x + m11 * vector.y + m12 * vector.z;
            result.z = m20 * vector.x + m21 * vector.y + m22 * vector.z;
            return result;
            
        }


        public static DMatrix4x4 Translate(DVector3 v)
        {
            return new DMatrix4x4
            {
                m00 = 1,        m01 = 0,        m02 = 0,        m03 = v.x,
                m10 = 0,        m11 = 1,        m12 = 0,        m13 = v.y,
                m20 = 0,        m21 = 0,        m22 = 1,        m23 = v.z,
                m30 = 0,        m31 = 0,        m32 = 0,        m33 = 1,
            };
        }

        public static DMatrix4x4 Rotate(Quaternion quaternion)
        {
            return (DMatrix4x4)Matrix4x4.Rotate(quaternion);
        }

        public static DMatrix4x4 Scale(Vector3 scale)
        {
            return new DMatrix4x4
            {
                m00 = scale.x,  m01 = 0,        m02 = 0,        m03 = 0,  
                m10 = 0,        m11 = scale.y,  m12 = 0,        m13 = 0,  
                m20 = 0,        m21 = 0,        m22 = scale.z,  m23 = 0,  
                m30 = 0,        m31 = 0,        m32 = 0,        m33 = 1,
            };
        }

        public static DMatrix4x4 TRS(DVector3 pos, Quaternion rot, Vector3 scale)
        {
            //
            //  TODO - Optimize this
            //
            return Translate(pos) * Rotate(rot) * Scale(scale);
        }

        public void GetTRS(out DVector3 translation, out Quaternion rotation, out Vector3 scale)
        {
            //
            //  TODO - Properly implement double precision
            //
            var col0 = (Vector4)GetColumn(0);
            var col1 = (Vector4)GetColumn(1);
            var col2 = (Vector4)GetColumn(2);
            var dcol3 = GetColumn(3);
            var col3 = (Vector4)dcol3;

            translation = new DVector3(dcol3.x, dcol3.y, dcol3.z);

            if (col2.sqrMagnitude > 0 && col1.sqrMagnitude > 0)
                rotation = Quaternion.LookRotation(col2, col1);
            else
                rotation = Quaternion.identity;

            scale = new Vector3(
                    Vector3.Dot(col0, rotation * Vector3.right),
                    Vector3.Dot(col1, rotation * Vector3.up),
                    Vector3.Dot(col2, rotation * Vector3.forward));
        }




        public DMatrix4x4 swapYZinput
        {
            get
            {
                return new DMatrix4x4
                {
                    m00 = m00,       m01 = m02,       m02 = m01,       m03 = m03,
                    m10 = m10,       m11 = m12,       m12 = m11,       m13 = m13,
                    m20 = m20,       m21 = m22,       m22 = m21,       m23 = m23,
                    m30 = m30,       m31 = m32,       m32 = m31,       m33 = m33
                };
            }
        }

        public DMatrix4x4 swapYZoutput
        {
            get
            {
                return new DMatrix4x4
                {
                    m00 = m00,       m01 = m01,       m02 = m02,       m03 = m03,
                    m10 = m20,       m11 = m21,       m12 = m22,       m13 = m23,
                    m20 = m10,       m21 = m11,       m22 = m12,       m23 = m13,
                    m30 = m30,       m31 = m31,       m32 = m32,       m33 = m33
                };
            }
        }

        public static explicit operator DMatrix4x4(Matrix4x4 matrix)
        {
            return new DMatrix4x4
            {
                m00 = matrix.m00,       m01 = matrix.m01,       m02 = matrix.m02,       m03 = matrix.m03,
                m10 = matrix.m10,       m11 = matrix.m11,       m12 = matrix.m12,       m13 = matrix.m13,
                m20 = matrix.m20,       m21 = matrix.m21,       m22 = matrix.m22,       m23 = matrix.m23,
                m30 = matrix.m30,       m31 = matrix.m31,       m32 = matrix.m32,       m33 = matrix.m33
            };
        }

        public static DMatrix4x4 identity
        {
            get
            {
                return new DMatrix4x4
                {
                    m00 = 1,                m01 = 0,                m02 = 0,                m03 = 0,         
                    m10 = 0,                m11 = 1,                m12 = 0,                m13 = 0,          
                    m20 = 0,                m21 = 0,                m22 = 1,                m23 = 0,         
                    m30 = 0,                m31 = 0,                m32 = 0,                m33 = 1,         
                };
            }
        }

    public static DMatrix4x4 NaN
    {
        get
        {
            return new DMatrix4x4
            {
                m00 = double.NaN,
                m01 = double.NaN,
                m02 = double.NaN,
                m03 = double.NaN,
                m10 = double.NaN,
                m11 = double.NaN,
                m12 = double.NaN,
                m13 = double.NaN,
                m20 = double.NaN,
                m21 = double.NaN,
                m22 = double.NaN,
                m23 = double.NaN,
                m30 = double.NaN,
                m31 = double.NaN,
                m32 = double.NaN,
                m33 = double.NaN,
            };
        }
    }
}
}