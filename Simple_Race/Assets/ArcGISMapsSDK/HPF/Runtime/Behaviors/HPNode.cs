using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Esri.HPFramework
{
    public enum NodeType
    {
        HPTransform,
        HPRoot,
    }
    

    public interface HPNode
    {
        DVector3 DLocalPosition { get; }
        DVector3 DUniversePosition { get; }

        Quaternion LocalRotation { get; }
        Quaternion UniverseRotation { get; }

        Vector3 LocalScale { get; }

        DMatrix4x4 DLocalMatrix { get; }

        DMatrix4x4 DUniverseMatrix { get; }

        DMatrix4x4 DWorldMatrix { get; }

        NodeType Type { get; }

        void RegisterChild(HPTransform child);
        void UnregisterChild(HPTransform child);

    }
}