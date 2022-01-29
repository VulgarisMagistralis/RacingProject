using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Esri.HPFramework.Editor
{

    public abstract class CoordinateSystemInspector
    {
        public enum ScaleTypes
        { 
            None, Uniform, Vector3
        }


        private const string k_UndoString = "Inspector";
        internal HPTransform TargetTransform { private get; set; }
        internal HPRoot TargetRoot { private get; set; }

        public abstract string Name { get; }

        public abstract void OnInspectorGUI();

        protected ScaleTypes ScaleType
        {
            get
            {
                if (TargetTransform != null)
                {
                    return TargetTransform.ScaleType == HPTransform.ScaleTypes.Uniform ? ScaleTypes.Uniform : ScaleTypes.Vector3;
                }
                else
                {
                    return ScaleTypes.None;
                }
            }
        }

        protected void GetTRS(out DVector3 translation, out Quaternion rotation, out Vector3 scale)
        {
            if (TargetTransform != null)
            {
                translation = TargetTransform.DLocalPosition;
                rotation = TargetTransform.LocalRotation;
                scale = TargetTransform.LocalScale;
                return;
            }

            if (TargetRoot != null)
            {
                translation = TargetRoot.DRootUniversePosition;
                rotation = TargetRoot.RootUniverseRotation;
                scale = Vector3.one;
                return;
            }

            throw new System.InvalidOperationException("Coordinate System Inspector cannot be instantiated with null values");
        }

        protected void SetTRS(DVector3 translation, Quaternion rotation)
        {
            SetTRS(translation, rotation, Vector3.one);
        }

        protected void SetTRS(DVector3 translation, Quaternion rotation, Vector3 scale)
        {
            if (TargetTransform != null)
            {
                SetTRS_Transform(translation, rotation, scale);
                return;
            }

            if (TargetRoot != null)
            {
                SetTRS_Root(translation, rotation, scale);
                return;
            }

            throw new System.InvalidOperationException("Coordinate System Inspector cannot be instantiated with null values");
        }

        private void SetTRS_Transform(DVector3 translation, Quaternion rotation, Vector3 scale)
        {
            Undo.RecordObject(TargetTransform, k_UndoString);
            Undo.RecordObject(TargetTransform.transform, k_UndoString);
            foreach (Transform child in TargetTransform.transform)
                Undo.RecordObject(child, k_UndoString);

            TargetTransform.DLocalPosition = translation;
            TargetTransform.LocalRotation = rotation;
            TargetTransform.LocalScale = scale;

            EditorUtility.SetDirty(TargetTransform);
        }

        private void SetTRS_Root(DVector3 translation, Quaternion rotation, Vector3 scale)
        {
            Undo.RecordObject(TargetRoot, k_UndoString);
            Undo.RecordObject(TargetRoot.transform, k_UndoString);
            foreach (Transform child in TargetRoot.transform)
                Undo.RecordObject(child, k_UndoString);

            TargetRoot.SetRootTR(translation, rotation);

            EditorUtility.SetDirty(TargetRoot);
        }
    }
}
