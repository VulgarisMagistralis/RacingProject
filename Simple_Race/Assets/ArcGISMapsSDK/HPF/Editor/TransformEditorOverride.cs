using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Esri.HPFramework.Internal;

namespace Esri.HPFramework.Editor
{

    [CustomEditor(typeof(Transform), true)]
    [CanEditMultipleObjects]
    public class TransformEditorOverride : UnityEditor.Editor
    {
        UnityEditor.Editor defaultEditor;
        Transform transform;
        HPTransform hpTransform;

        private bool m_EnableEdit = false;

        void OnEnable()
        {
            defaultEditor = UnityEditor.Editor.CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));
            transform = target as Transform;
            hpTransform = transform?.GetComponent<HPTransform>();
        }

        void OnDisable()
        {
            //
            //  TODO - Not sure this is necessary. (The destoy part yes, but not the disable method)
            //
            MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (disableMethod != null)
                disableMethod.Invoke(defaultEditor, null);
            DestroyImmediate(defaultEditor);
        }

        public override void OnInspectorGUI()
        {
            if (hpTransform == null)
                DrawDefault();
            else if(hpTransform.IsUnityTransformEditable())
                DrawHighPrecision();
        }

        private void DrawDefault()
        {
            defaultEditor.OnInspectorGUI();
        }

        private void DrawHighPrecision()
        {
            m_EnableEdit = EditorGUILayout.BeginFoldoutHeaderGroup(m_EnableEdit, "Edit Transform");
            if (m_EnableEdit)
            {
                EditorGUILayout.HelpBox("For most applications using the HP Transform, you should not be editing the Transform component.", MessageType.Warning);
                defaultEditor.OnInspectorGUI();
            }
        }
    }
}