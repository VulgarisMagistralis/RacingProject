using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esri.HPFramework.Editor
{
    public class DefaultCoordinateSystemInspector : CoordinateSystemInspector
    {
        public static readonly string k_DefaultName = "Default";

        public override string Name => k_DefaultName;

        public override void OnInspectorGUI()
        {
            switch (ScaleType)
            {
                case ScaleTypes.None:
                    DrawTRS_NoScale();
                    break;

                case ScaleTypes.Uniform:
                    DrawTRS_UniformScale();
                    break;

                case ScaleTypes.Vector3:
                    DrawTRS_NonUniformScale();
                    break;
            }
        }

        private void DrawTRS_NonUniformScale()
        {
            GetTRS(out DVector3 position, out Quaternion rotation, out Vector3 scale);

            if (HPTrsInspector.Draw(ref position, ref rotation, ref scale))
                SetTRS(position, rotation, scale);
        }

        private void DrawTRS_UniformScale()
        {
            GetTRS(out DVector3 position, out Quaternion rotation, out Vector3 vScale);
            float scale = vScale.x;

            if (HPTrsInspector.Draw(ref position, ref rotation, ref scale))
                SetTRS(position, rotation, scale * Vector3.one);
        }

        private void DrawTRS_NoScale()
        {
            GetTRS(out DVector3 position, out Quaternion rotation, out Vector3 vScale);
            float scale = vScale.x;

            if (HPTrsInspector.Draw(ref position, ref rotation))
                SetTRS(position, rotation, Vector3.one);
        }
    }
}
