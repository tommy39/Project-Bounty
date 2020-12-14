using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems.FieldOfView
{
    [CreateAssetMenu(fileName = "Field Of View Data", menuName = "IND/Core/AI/FieldOfView/Data")]
    public class FieldOfViewData : ScriptableObject
    {
        public int tickRate = 60;

        [InlineEditor] public Material visualisationMaterial;

        public float meshResolution = 0.5f;
        public float edgeDistanceThreshold = 5;
        public float edgeResolveIterations = 0.5f;

        [InlineEditor] public ScriptableLayerMask targetMask;
        [InlineEditor] public ScriptableLayerMask obstacleMask;
    }
}