using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Esri.HPFramework
{
    [System.Serializable]
    public struct DBounds
    {
        public DBounds(DVector3 center, DVector3 size)
        {
            this.center = center;
            this.extents = 0.5 * size;
        }

        public DVector3 center;
        public DVector3 extents;
        

        public bool Intersects(DBounds bounds)
        {
            DVector3 ourMin = center - extents;
            DVector3 ourMax = center + extents;

            DVector3 theirMin = bounds.center - bounds.extents;
            DVector3 theirMax = bounds.center + bounds.extents;

            return 
                (ourMin.x <= theirMax.x && theirMin.x <= ourMax.x) &&
                (ourMin.y <= theirMax.y && theirMin.y <= ourMax.y) &&
                (ourMin.z <= theirMax.z && theirMin.z <= ourMax.z);
        }

        public bool Contains(DBounds bounds)
        {
            DVector3 ourMin = center - extents;
            DVector3 ourMax = center + extents;

            DVector3 theirMin = bounds.center - bounds.extents;
            DVector3 theirMax = bounds.center + bounds.extents;

            return
                ourMin.x <= theirMin.x &&
                ourMin.y <= theirMin.y &&
                ourMin.z <= theirMin.z &&
                ourMax.x >= theirMax.x &&
                ourMax.y >= theirMax.y &&
                ourMax.z >= theirMax.z;

        }


        public static explicit operator DBounds(Bounds bounds)
        {
            DBounds result;

            result.center = (DVector3)bounds.center;
            result.extents = (DVector3)bounds.extents;

            return result;
        }

        

        public static DBounds Transform3x4(DBounds bounds, DMatrix4x4 transformationMatrix)
        {
            //
            //  FIXME - Implement optimized version of this
            //
            return Transform(bounds, transformationMatrix, default(DPlane), useClipPlane: false);
        }

        public static DBounds Transform(DBounds bounds, DMatrix4x4 transformMatrix, DPlane clipPlane)
        {
            return Transform(bounds, transformMatrix, clipPlane, useClipPlane: true);
        }

        private static unsafe DBounds Transform(DBounds bounds, DMatrix4x4 transformationMatrix, DPlane clipPlane, bool useClipPlane)
        {
            DVector3* universeVertices = stackalloc DVector3[8];

            ref DVector3 extents = ref bounds.extents;
            ref DVector3 center = ref bounds.center;

            //
            //  Compute corner vertices
            //
            universeVertices[0] = center + new DVector3(-extents.x, -extents.y, -extents.z);
            universeVertices[1] = center + new DVector3(-extents.x, -extents.y, extents.z);
            universeVertices[2] = center + new DVector3(-extents.x, extents.y, -extents.z);
            universeVertices[3] = center + new DVector3(-extents.x, extents.y, extents.z);

            universeVertices[4] = center + new DVector3(extents.x, -extents.y, -extents.z);
            universeVertices[5] = center + new DVector3(extents.x, -extents.y, extents.z);
            universeVertices[6] = center + new DVector3(extents.x, extents.y, -extents.z);
            universeVertices[7] = center + new DVector3(extents.x, extents.y, extents.z);


            DVector3 min = new DVector3(double.MaxValue, double.MaxValue, double.MaxValue);
            DVector3 max = new DVector3(double.MinValue, double.MinValue, double.MinValue);

            for (int i = 0; i < 8; i++)
            {
                DVector3 v = universeVertices[i];

                if (!useClipPlane || clipPlane.GetSide(v))
                {
                    DVector3 clip = transformationMatrix.MultiplyPoint(v);
                    min = DVector3.Min(min, clip);
                    max = DVector3.Max(max, clip);
                }
                else
                {
                    int a = i ^ 0x01;
                    int b = i ^ 0x02;
                    int c = i ^ 0x04;

                    if (clipPlane.GetSide(universeVertices[a]))
                    {
                        DVector3 raycast = clipPlane.Raycast(v, universeVertices[a]);
                        DVector3 clip = transformationMatrix.MultiplyPoint(raycast);
                        min = DVector3.Min(min, clip);
                        max = DVector3.Max(max, clip);
                    }

                    if (clipPlane.GetSide(universeVertices[b]))
                    {
                        DVector3 raycast = clipPlane.Raycast(v, universeVertices[b]);
                        DVector3 clip = transformationMatrix.MultiplyPoint(raycast);
                        min = DVector3.Min(min, clip);
                        max = DVector3.Max(max, clip);
                    }

                    if (clipPlane.GetSide(universeVertices[c]))
                    {
                        DVector3 raycast = clipPlane.Raycast(v, universeVertices[c]);
                        DVector3 clip = transformationMatrix.MultiplyPoint(raycast);
                        min = DVector3.Min(min, clip);
                        max = DVector3.Max(max, clip);
                    }
                }
            }

            return new DBounds(0.5 * (min + max), (max - min));
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }


    }
}