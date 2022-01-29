using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Esri.HPFramework
{
    //
    //  Changing the scale of the root resets the HPTransform's values!!!
    //
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [AddComponentMenu("HPFramework/HPTransform")]
    public class HPTransform : MonoBehaviour, HPNode
    {
        /// <summary>
        /// The type of scale that the HPNode's current configuration allows. When it is 
        /// a leaf node, it will accept non-uniform scales. Otherwise, it will only apply
        /// a uniform scale.
        /// </summary>
        public enum ScaleTypes
        {
            /// <summary>
            /// A non uniform scale, which has an x, y and z component
            /// </summary>
            Vector3,

            /// <summary>
            /// A uniform scale, where only the x component is considered
            /// </summary>
            Uniform,
        }


        void OnEnable()
        {
            Assert.IsNull(m_Parent);
            UpdateParentRelation();
            

            //
            //  TODO - Find a way of initializing the HPTransform from the Unity Transform without
            //              using serialized variables.
            //
            if (!m_IsInitialized)
                UpdateHPTransformFromUnityTransform();

            InvalidateLocalCache();
        }

        void OnDisable()
        {
            if (m_Parent != null)
            {
                m_Parent.UnregisterChild(this);
                m_Parent = null;
            }
        }

        [SerializeField]
        private bool m_IsInitialized = false;

        [SerializeField]
        private DVector3 m_LocalPosition = DVector3.zero;

        [SerializeField]
        private Quaternion m_LocalRotation = Quaternion.identity;

        [SerializeField]
        private Vector3 m_LocalScale = Vector3.one;


        private HPNode m_Parent;
        private List<HPTransform> m_Children = new List<HPTransform>();

        // Cache Unity's transform to reduce overhead of retrieving it everytime
        private Transform m_CachedUnityTransform = null;
        private Transform CachedUnityTransform => m_CachedUnityTransform ?? (m_CachedUnityTransform = transform);

        private bool m_CachedUniverseMatrixIsValid = false;
        private DMatrix4x4 m_CachedUniverseMatrix;

        private bool m_CachedUniverseRotationIsValid = false;
        private Quaternion m_CachedUniverseRotation;

        private bool m_CachedWorldMatrixIsValid = false;
        private DMatrix4x4 m_CachedWorldMatrix;

        private bool m_CachedLocalMatrixIsValid = false;
        private DMatrix4x4 m_CachedLocalMatrix;

        private bool m_UnityTransformIsValid = false;

        private bool m_LocalHasChanged = false;


        /// <summary>
        /// The position of the HPTransform relative to its parent HPRoot or HPTransform
        /// </summary>
        public Vector3 LocalPosition
        {
            get => (Vector3)m_LocalPosition;
            set
            {
                m_LocalPosition = (DVector3)value;

                InvalidateLocalCache();
            }
        }

        /// <summary>
        /// The position of the HPTransform relative to its parent HPRoot or HPTransform
        /// </summary>
        public DVector3 DLocalPosition
        {
            get => m_LocalPosition;
            set
            {
                Assert.IsFalse(double.IsNaN(value.x), "Cannot set HP Transform with Null values");
                Assert.IsFalse(double.IsNaN(value.y), "Cannot set HP Transform with Null values");
                Assert.IsFalse(double.IsNaN(value.z), "Cannot set HP Transform with Null values");

                m_LocalPosition = value;

                InvalidateLocalCache();
            }
        }

        /// <summary>
        /// The position of the HPTransform, in universe space.
        /// </summary>
        public DVector3 DUniversePosition
        {
            get
            {
                if (m_Parent == null)
                    return m_LocalPosition;
                else
                    return m_Parent.DUniverseMatrix.MultiplyPoint(m_LocalPosition);
            }
            set
            {
                Assert.IsFalse(double.IsNaN(value.x), "Cannot set HP Transform with Null values");
                Assert.IsFalse(double.IsNaN(value.y), "Cannot set HP Transform with Null values");
                Assert.IsFalse(double.IsNaN(value.z), "Cannot set HP Transform with Null values");

                if (m_Parent == null)
                    m_LocalPosition = value;
                else
                    m_LocalPosition = m_Parent.DUniverseMatrix.inverse.MultiplyPoint(value);

                InvalidateLocalCache();
            }
        }

        /// <summary>
        /// The position of the HPTransform, in universe space
        /// </summary>
        public Vector3 UniversePosition
        {
            get => (Vector3)DUniversePosition;
            set => DUniversePosition = (DVector3)value;
        }


        /// <summary>
        /// The rotation of the HPTransform relative to its parent HPTransform or HPRoot
        /// </summary>
        public Quaternion LocalRotation
        {
            get => m_LocalRotation;
            set
            {
                Assert.IsFalse(float.IsNaN(value.x), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.y), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.z), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.w), "Cannot set HP Transform with Null values");

                m_LocalRotation = value;

                InvalidateLocalCache();
            }
        }

        /// <summary>
        /// The rotation of the HPTransform, in universe space
        /// </summary>
        public Quaternion UniverseRotation
        {
            get
            {
                if (!m_CachedUniverseRotationIsValid)
                {
                    if (m_Parent == null)
                        m_CachedUniverseRotation =  m_LocalRotation;
                    else
                        m_CachedUniverseRotation = m_Parent.UniverseRotation * m_LocalRotation;

                    m_CachedUniverseRotationIsValid = true;
                }
                return m_CachedUniverseRotation;
            }
            set
            {
                Assert.IsFalse(float.IsNaN(value.x), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.y), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.z), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.w), "Cannot set HP Transform with Null values");

                if (m_Parent == null)
                    m_LocalRotation = value;
                else
                    m_LocalRotation = Quaternion.Inverse(m_Parent.UniverseRotation) * value;

                InvalidateLocalCache();
            }
        }


        //
        //  TODO - When scale is set to zero, quaternion method is printing messages in the console
        //
        /// <summary>
        /// The scale of the HPTransform, relative to its parent HPTransform or HPRoot. If the HPTransform is
        /// not a leaf node (i.e. if it contains another HPTransform) only uniform scales will be possible. Under
        /// these circumstances, only the x component of the scale will count towards the uniform scale.
        /// </summary>
        public Vector3 LocalScale
        {
            get
            {
                if (ScaleType == ScaleTypes.Vector3)
                    return m_LocalScale;
                else
                {
                    float uniformScale = m_LocalScale.x;
                    return new Vector3(uniformScale, uniformScale, uniformScale);
                }
            }
            set
            {
                Assert.IsFalse(float.IsNaN(value.x), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.y), "Cannot set HP Transform with Null values");
                Assert.IsFalse(float.IsNaN(value.z), "Cannot set HP Transform with Null values");

                m_LocalScale = value;

                InvalidateLocalCache();
            }
        }

        /// <summary>
        /// Set or get the parent of the HPTransform. When changing the parent, the HPTransform's position in the world
        /// will be preserved.
        /// </summary>
        public Transform Parent
        {
            get => CachedUnityTransform.parent;
            set
            {
                CachedUnityTransform.parent = value;
            }
        }

        /// <summary>
        /// The HPTransform's forward vector, in universe space.
        /// </summary>
        public Vector3 Forward { get => UniverseRotation * Vector3.forward; }

        /// <summary>
        /// The HPTransform's right vector, in universe space.
        /// </summary>
        public Vector3 Right { get => UniverseRotation * Vector3.right; }

        /// <summary>
        /// The HPTransform's up vector, in universe space.
        /// </summary>
        public Vector3 Up { get => UniverseRotation * Vector3.up; }

        /// <summary>
        /// The type of scale that is currently supported by the HPTransform. When in uniform scale,
        /// only the x component of the set scale will be considered for the uniform scale.
        /// </summary>
        public ScaleTypes ScaleType { get => HasChildren ? ScaleTypes.Uniform : ScaleTypes.Vector3; }


        DMatrix4x4 HPNode.DLocalMatrix => DLocalMatrix;
        internal DMatrix4x4 DLocalMatrix
        {
            get
            {
                if (!m_CachedLocalMatrixIsValid)
                {
                    m_CachedLocalMatrix = DMatrix4x4.TRS(m_LocalPosition, m_LocalRotation, m_LocalScale);
                    m_CachedLocalMatrixIsValid = true;
                }
                return m_CachedLocalMatrix;
            }
        }


        DMatrix4x4 HPNode.DUniverseMatrix => DUniverseMatrix;
        internal DMatrix4x4 DUniverseMatrix
        {
            get
            {
                if (!m_CachedUniverseMatrixIsValid)
                {
                    if (m_Parent == null)
                        m_CachedUniverseMatrix = DLocalMatrix;
                    else
                        m_CachedUniverseMatrix = m_Parent.DUniverseMatrix * DLocalMatrix;

                    m_CachedUniverseMatrixIsValid = true;
                }
                return m_CachedUniverseMatrix;
            }
        }


        DMatrix4x4 HPNode.DWorldMatrix => DWorldMatrix;
        internal DMatrix4x4 DWorldMatrix
        {
            get
            {
                if (!m_CachedWorldMatrixIsValid)
                {
                    if (m_Parent == null)
                        m_CachedWorldMatrix = DLocalMatrix;
                    else
                        m_CachedWorldMatrix = m_Parent.DWorldMatrix * DLocalMatrix;

                    m_CachedWorldMatrixIsValid = true;
                }
                return m_CachedWorldMatrix;
            }
        }


        private bool HasChildren { get => m_Children.Count > 0; }


        NodeType HPNode.Type => Type;
        internal NodeType Type => NodeType.HPTransform;

        internal bool IsSceneEditable => m_Children.Count == 0;

        /// <summary>
        /// Updates m_HPRoot so that it points towards the corret HPRoot
        /// component. Returns true if it has changed and false otherwise.
        /// </summary>
        /// <returns></returns>
        private bool UpdateParentRelation()
        {
            if (!isActiveAndEnabled)
                return false;

            HPNode parent = CachedUnityTransform.parent?.GetComponentInParent<HPNode>();

            if(parent != m_Parent)
            {
                m_Parent?.UnregisterChild(this);
                m_Parent = parent;
                m_Parent?.RegisterChild(this);
                return true;
            }

            return false;
        }


        internal void UpdateUnityTransform()
        {
            if (HasChildren)
                UpdateUnityTransform_IntermediateTransform();
            else
                UpdateUnityTransform_WorldSpace();

            ClearUnityTransformChange();

            for (int i = 0; i < m_Children.Count; ++i)
                m_Children[i].UpdateUnityTransform();

            m_UnityTransformIsValid = true;
        }

        internal void ClearUnityTransformChange()
        {
            CachedUnityTransform.hasChanged = false;
            for (int i = 0; i < m_Children.Count; ++i)
                m_Children[i].ClearUnityTransformChange();
        }

        private void UpdateUnityTransform_IntermediateTransform()
        {
            Transform t = CachedUnityTransform;

            if (t.localPosition != Vector3.zero)
                t.localPosition = Vector3.zero;

            if (t.localRotation != Quaternion.identity)
                t.localRotation = Quaternion.identity;

            if (t.localScale != LocalScale)
                t.localScale = LocalScale;
        }

        private void UpdateUnityTransform_WorldSpace()
        {
            //
            //  TODO - Optimize this, don't need scale
            //
            DMatrix4x4 worldMatrix = DWorldMatrix;
            /*
            if (double.IsNaN(worldMatrix.m00))
            {
                Debug.LogError("Invalid root matrix");
                return;
            }
            */

            worldMatrix.GetTRS(
                out DVector3 translation, 
                out Quaternion rotation, 
                out Vector3 scale);

            

            CachedUnityTransform.position = (Vector3)translation;
            CachedUnityTransform.rotation = rotation;
            CachedUnityTransform.localScale = m_LocalScale;
        }

        private void UpdateHPTransformFromUnityTransform()
        {
            DMatrix4x4 parentFromWorld = (m_Parent != null) ? m_Parent.DWorldMatrix.inverse : DMatrix4x4.identity;
            DMatrix4x4 worldFromObject = DMatrix4x4.TRS(
                                            (DVector3)CachedUnityTransform.position,
                                            CachedUnityTransform.rotation,
                                            CachedUnityTransform.lossyScale);

            DMatrix4x4 parentFromObject = parentFromWorld * worldFromObject;

            //
            //  TODO - Optimize this, we don't use scale.
            //
            parentFromObject.GetTRS(
                out DVector3 position, 
                out Quaternion rotation, 
                out Vector3 scale);

            m_LocalPosition = position;
            m_LocalRotation = rotation;
            m_LocalScale = CachedUnityTransform.localScale;

            InvalidateLocalCache();
        }

        private void LateUpdate()
        {
            if (m_Parent == null)
                UpdateFromParent();
        }

        private void OnTransformParentChanged()
        {
            DMatrix4x4 worldFromLocal = DWorldMatrix;

            UpdateParentRelation();
            CachedUnityTransform.hasChanged = false;

            DMatrix4x4 worldFromParent = m_Parent == null ? DMatrix4x4.identity : m_Parent.DWorldMatrix;
            DMatrix4x4 parentFromWorld = worldFromParent.inverse;
            DMatrix4x4 parentFromLocal = parentFromWorld * worldFromLocal;

            parentFromLocal.GetTRS(out DVector3 translation, out Quaternion rotation, out Vector3 scale);

            m_LocalPosition = translation;
            m_LocalRotation = rotation;
            m_LocalScale = scale;

            InvalidateLocalCache();
        }


        internal void UpdateFromParent()
        {
            //
            //  Give priority to changes made directly to the HPTransform over changes
            //      made to the CachedUnityTransform. This facilitates initializations.
            //
            if (!m_LocalHasChanged && CachedUnityTransform.hasChanged)
            {
                UpdateHPTransformFromUnityTransform();
                ClearUnityTransformChange();
            }

            if (!m_UnityTransformIsValid)
                UpdateUnityTransform();

            for (int i = 0; i < m_Children.Count; ++i)
                m_Children[i].UpdateFromParent();

            m_LocalHasChanged = false;
        }

        void HPNode.RegisterChild(HPTransform child)
        {
            //
            //  When first child is added, scale type transitions from
            //      uniform to non-uniform.
            //
            if (m_Children.Count == 0)
                InvalidateLocalCache();

            m_Children.Add(child);
            UpdateUnityTransform();
        }

        void HPNode.UnregisterChild(HPTransform child)
        {
            m_Children.Remove(child);

            //
            //  When last child is removed, scale type transitions from
            //      uniform to non-uniform.
            //
            if (m_Children.Count == 0)
                InvalidateLocalCache();
        }

        private void InvalidateLocalCache()
        {
            m_IsInitialized = true;
            m_LocalHasChanged = true;
            m_CachedLocalMatrixIsValid = false;
            InvalidateUniverseCache();
        }

        internal void InvalidateUniverseCache()
        {
            m_CachedUniverseRotationIsValid = false;
            m_CachedUniverseMatrixIsValid = false;
            InvalidateWorldCache_SelfOnly();

            for (int i = 0; i < m_Children.Count; ++i)
                m_Children[i].InvalidateUniverseCache();
        }

        internal void InvalidateWorldCache()
        {
            InvalidateWorldCache_SelfOnly();

            for (int i = 0; i < m_Children.Count; ++i)
                m_Children[i].InvalidateWorldCache();
        }

        private void InvalidateWorldCache_SelfOnly()
        {
            m_CachedWorldMatrixIsValid = false;
            m_UnityTransformIsValid = false;
        }
    }
}