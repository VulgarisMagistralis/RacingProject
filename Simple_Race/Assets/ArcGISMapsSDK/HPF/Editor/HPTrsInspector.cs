using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Esri.HPFramework.Editor
{
    //
    //  TODO - Clean this up
    //
    //
    //  TODO - Make labels draggable
    //
    public class HPTrsInspector
    {
        public static bool Draw(ref DVector3 position, ref Quaternion rotation, ref Vector3 scale)
        {

            bool result = false;

            DVector3 oldPosition = position;
            position = Double3Field("Position", position);
            if (oldPosition != position)
                result = true;


            Vector3 vRotation = rotation.eulerAngles;
            Vector3 oldRotation = vRotation;
            vRotation = Float3Field("Rotation", vRotation);
            if (oldRotation != vRotation)
            {
                rotation = Quaternion.Euler(vRotation);
                result = true;
            }


            Vector3 oldScale = scale;
            scale = Float3Field("Scale", scale);
            if (oldScale != scale)
                result = true;


            return result;

        }


        public static bool Draw(ref DVector3 position, ref Quaternion rotation)
        {

            bool result = false;

            DVector3 oldPosition = position;
            position = Double3Field("Position", position);
            if (oldPosition != position)
                result = true;


            Vector3 vRotation = rotation.eulerAngles;
            Vector3 oldRotation = vRotation;
            vRotation = Float3Field("Rotation", vRotation);
            if (oldRotation != vRotation)
            {
                rotation = Quaternion.Euler(vRotation);
                result = true;
            }

            return result;

        }

        public static bool Draw(ref DVector3 position, ref Quaternion rotation, ref float uniformScale)
        {

            bool result = false;

            DVector3 oldPosition = position;
            position = Double3Field("Position", position);
            if (oldPosition != position)
                result = true;


            Vector3 vRotation = rotation.eulerAngles;
            Vector3 oldRotation = vRotation;
            vRotation = Float3Field("Rotation", vRotation);
            if (oldRotation != vRotation)
            {
                rotation = Quaternion.Euler(vRotation);
                result = true;
            }


            float oldScale = uniformScale;
            uniformScale = Float1Field("Scale", uniformScale);
            if (oldScale != uniformScale)
                result = true;


            return result;

        }

        private static Vector3 Float3Field(string label, Vector3 value)
        {
            Rect rect = EditorGUILayout.BeginHorizontal();

            GUILayout.Label(label, GUILayout.Width(150.0f));

            Label("X");
            Float(ref value.x);
            Label("Y");
            Float(ref value.y);
            Label("Z");
            Float(ref value.z);

            EditorGUILayout.EndHorizontal();

            return value;
        }

        private static float Float1Field(string label, float value)
        {
            Rect rect = EditorGUILayout.BeginHorizontal();

            GUILayout.Label(label, GUILayout.Width(150.0f));
            
            Float(ref value);

            EditorGUILayout.EndHorizontal();

            return value;
        }

        private static DVector3 Double3Field(string label, DVector3 value)
        {
            Rect rect = EditorGUILayout.BeginHorizontal();

            GUILayout.Label(label, GUILayout.Width(150.0f));

            Label("X");
            Double(ref value.x);
            Label("Y");
            Double(ref value.y);
            Label("Z");
            Double(ref value.z);

            EditorGUILayout.EndHorizontal();

            return value;
        }

        private static void Float(ref float value)
        {
            value = EditorGUILayout.FloatField(
                                value,
                                GUILayout.ExpandWidth(true));
        }

        private static void Double(ref double value)
        {
            value = EditorGUILayout.DoubleField(
                                value,
                                GUILayout.ExpandWidth(true));
        }

        private static void Label(string str)
        {
            GUIContent labelContent = new GUIContent(str);
            float width = GUI.skin.GetStyle("Label").CalcSize(labelContent).x;
            GUILayout.Label(labelContent, GUILayout.Width(width));
        }
    }

}