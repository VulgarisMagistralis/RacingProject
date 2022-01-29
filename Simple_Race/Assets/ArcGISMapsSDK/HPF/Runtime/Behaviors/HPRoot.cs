using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Esri.HPFramework
{
    /// <summary>
    /// The HPRoot determines how the universe space will be converted into world
    /// space. It defines the coordinate in universe space which corresponds to
    /// the position of the GameObject, in world space.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("HPFramework/HPRoot")]
    public class HPRoot : MonoBehaviour, HPNode
    {
        [SerializeField]
        private DVector3 m_LocalPosition;

        [SerializeField]
        private Quaternion m_LocalRotation;


        private List<HPTransform> m_Children = new List<HPTransform>();


        private bool m_CachedWorldMatrixIsValid = false;
        private DMatrix4x4 m_CachedWorldMatrix;


        DVector3 HPNode.DLocalPosition => DLocalPosition;
        internal DVector3 DLocalPosition
        {
            get => DVector3.zero;
        }
        
        DVector3 HPNode.DUniversePosition => DUniversePosition;
        internal DVector3 DUniversePosition
        {
            get => DVector3.zero;
        }

        /// <summary>
        /// Set the position in universe space which corresponds to the HPRoot's
        /// position in the scene.
        /// </summary>
        public DVector3 DRootUniversePosition
        {
            get
            {
                return m_LocalPosition;
            }

            set
            {
                m_LocalPosition = value;
                InvalidateWorldCache();
            }
        }

        /// <summary>
        /// Set the position in universe space which corresponds to the HPRoot's
        /// position in the scene.
        /// </summary>
        public Vector3 RootUniversePosition
        {
            get
            {
                return (Vector3)DRootUniversePosition;
            }

            set
            {
                DRootUniversePosition = (DVector3)value;
            }
        }

        /// <summary>
        /// Set the rotation of the universe space which corresponds to the HPRoot's
        /// rotation in the scene.
        /// </summary>
        public Quaternion RootUniverseRotation
        {
            get
            {
                return m_LocalRotation;
            }

            set
            {
                m_LocalRotation = value;
                InvalidateWorldCache();
            }
        }

        /// <summary>
        /// Simultaneously set root universe position and root universe rotation
        /// in a single call, updating underlying transforms only once.
        /// </summary>
        /// <param name="position">The position in universe space which corresponds to the HPRoot's position in the scene</param>
        /// <param name="rotation">The position in universe space which corresponds to the HPRoot's rotation in the scene</param>
        public void SetRootTR(DVector3 position, Quaternion rotation)
        {
            m_LocalPosition = position;
            m_LocalRotation = rotation;
            InvalidateWorldCache();
        }


        Quaternion HPNode.LocalRotation => LocalRotation;
        internal Quaternion LocalRotation
        {
            get => Quaternion.identity;
        }


        Quaternion HPNode.UniverseRotation => UniverseRotation;
        internal Quaternion UniverseRotation
        {
            get => Quaternion.identity;
        }


        Vector3 HPNode.LocalScale => LocalScale;
        internal Vector3 LocalScale
        {
            get => Vector3.one;
        }


        DMatrix4x4 HPNode.DLocalMatrix => DMatrix4x4.identity;

        DMatrix4x4 HPNode.DUniverseMatrix => DMatrix4x4.identity;

        DMatrix4x4 HPNode.DWorldMatrix => DWorldMatrix;
        public DMatrix4x4 DWorldMatrix
        {
            get
            {
                if (!m_CachedWorldMatrixIsValid || transform.hasChanged)
                {
                    DMatrix4x4 worldFromRoot = (DMatrix4x4)transform.localToWorldMatrix;
                    DMatrix4x4 rootFromUniverse = DMatrix4x4.TRS(m_LocalPosition, m_LocalRotation, LocalScale).inverse;
                    m_CachedWorldMatrix = worldFromRoot * rootFromUniverse;
                    m_CachedWorldMatrixIsValid = true;
                    transform.hasChanged = false;
                }
                return m_CachedWorldMatrix;
            }
        }

        NodeType HPNode.Type => NodeType.HPRoot;

        

        void HPNode.RegisterChild(HPTransform hpTransform)
        {
            m_Children.Add(hpTransform);
        }

        void HPNode.UnregisterChild(HPTransform hPTransform)
        {
            m_Children.Remove(hPTransform); 
        }

        private void UpdateTransforms()
        {
            foreach (var child in m_Children)
                child.UpdateFromParent();
        }

        private void InvalidateWorldCache()
        {
            m_CachedWorldMatrixIsValid = false;
            foreach (var child in m_Children)
                child.InvalidateWorldCache();
        }

        void OnEnable()
        {
            InvalidateWorldCache();
        }

        private void LateUpdate()
        {
            if (transform.hasChanged)
            {
                transform.hasChanged = false;
                foreach (var child in m_Children)
                    child.ClearUnityTransformChange();
            }

            UpdateTransforms();
        }

    }
}