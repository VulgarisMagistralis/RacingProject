using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.Assertions;
using Esri.HPFramework.Internal;
using System.Reflection;

namespace Esri.HPFramework.Editor
{

    //
    //  TODO - Support Multi Object Editing
    //
    [CustomEditor(typeof(HPTransform))]
    public class HPTransformInspector : UnityEditor.Editor
    {
        private static List<System.Func<HPTransform, CoordinateSystemInspector>> s_CoordinateSystemConstructors = null;

        public static readonly string k_CoordinateSystemPreference = "Esri.HPFramework.CoordinateSystem";

        private List<CoordinateSystemInspector> m_Inspectors;

        private struct TRS
        {
            public DVector3 position;
            public Quaternion rotation;
            public Vector3 scale;

            public TRS(DVector3 position, Quaternion rotation, Vector3 scale)
            {
                this.position = position;
                this.rotation = rotation;
                this.scale = scale;
            }
        }

        

        public void OnEnable()
        {
            
            //
            //  TODO - Uses interal editor tools, may not be stable
            //
            HPTransform hpTransform = target as HPTransform;

            switch (PrefabUtility.GetPrefabAssetType(hpTransform.gameObject))
            {
                case PrefabAssetType.NotAPrefab:
                    MoveToTop(hpTransform);
                    break;

                case PrefabAssetType.Regular:
                case PrefabAssetType.Variant:
                case PrefabAssetType.Model:
                case PrefabAssetType.MissingAsset:
                    //
                    //  Intentionally left blank
                    //
                    break;
            }
            

            
            
            //
            

            if (s_CoordinateSystemConstructors == null)
            {
                //
                //  TODO - Don't look through every assembly, might get really long
                //
                System.Type[] constructorTypes = new System.Type[] {};
                s_CoordinateSystemConstructors = System.AppDomain
                                              .CurrentDomain
                                              .GetAssemblies()
                                              .SelectMany(a => a.GetTypes())
                                              .Where(type => type.IsSubclassOf(typeof(CoordinateSystemInspector)))
                                              .Select(type => type.GetConstructor(constructorTypes))
                                              .Select<ConstructorInfo, System.Func<HPTransform, CoordinateSystemInspector>>(c =>
                                              {
                                                  return target =>
                                                  {
                                                      CoordinateSystemInspector inspector = (CoordinateSystemInspector)c.Invoke(null);
                                                      inspector.TargetTransform = target;
                                                      return inspector;
                                                  };
                                              })
                                              .ToList();
            }

            m_Inspectors = s_CoordinateSystemConstructors
                                .Select(c => c.Invoke(hpTransform))
                                .OrderByDescending(i => i.Name == DefaultCoordinateSystemInspector.k_DefaultName)
                                .ThenBy(i => i.Name)
                                .ToList();

            

            Tools.hidden = !hpTransform.IsUnityTransformEditable();
        }

        public void OnDisable()
        {
            Tools.hidden = false;
        }

        private static void MoveToTop(HPTransform hpTransform)
        {
            int lastIndex;
            int index = GetIndex(hpTransform);
            do
            {
                lastIndex = index;
                UnityEditorInternal.ComponentUtility.MoveComponentUp(hpTransform);
                index = GetIndex(hpTransform);
            } while (index != lastIndex && index > 1);
        }

        public static int GetIndex(Component c)
        {
            return System.Array.IndexOf(c.GetComponents<Component>(), c);
        }


        public override void OnInspectorGUI()
        {
            int oldCoordinateSystemIndex = EditorPrefs.GetInt(k_CoordinateSystemPreference);
            int coordinateSystemIndex = EditorGUILayout.Popup("Coordinate System", oldCoordinateSystemIndex, m_Inspectors.Select(i => i.Name).ToArray());
            if (oldCoordinateSystemIndex != coordinateSystemIndex)
                EditorPrefs.SetInt(k_CoordinateSystemPreference, coordinateSystemIndex);

            m_Inspectors[coordinateSystemIndex].OnInspectorGUI();

        }
    }
}