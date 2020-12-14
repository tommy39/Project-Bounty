using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.AISystems
{
    [CreateAssetMenu(fileName = "AI_Data_Generic", menuName = "IND/Core/AI/AI Data")]
    public class AIData : ScriptableObject
    {
        public GameObject worldInteractionPrefab;
        [InlineEditor] public ScriptableLayerMask enemyToPlayerLayerMask;
        [InlineEditor] public ScriptableLayerMask friendlyToPlayerLayerMask;
    }
}