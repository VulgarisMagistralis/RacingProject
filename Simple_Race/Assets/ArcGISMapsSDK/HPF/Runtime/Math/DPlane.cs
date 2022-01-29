using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Esri.HPFramework
{
    public struct DPlane
    {
        public DVector3 normal;
        public double distance;

        public DPlane(DVector3 inNormal, DVector3 inPoint)
        {
            double k = 1.0 / inNormal.magnitude;
            normal = k * inNormal;
            distance = DVector3.Dot(normal, inPoint);
        }

        public bool GetSide(DVector3 point)
        {
            DVector3 origin = distance * normal;
            return DVector3.Dot(point - origin, normal) >= 0.0;
        }

        public DVector3 Raycast(DVector3 p1, DVector3 p2)
        {
            DVector3 origin = distance * normal;
            double proj1 = DVector3.Dot(p1 - origin, normal);
            double proj2 = DVector3.Dot(p2 - origin, normal);
            double k = proj1 / (proj1 - proj2);
            return DVector3.LerpUnclamped(p1, p2, k);
        }
    }
}