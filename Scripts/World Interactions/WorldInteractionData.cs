using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.WorldInteractions
{
    [CreateAssetMenu(fileName = "Interactable Data", menuName = "IND/Core/World Interactions/Interactable Data")]
    public class WorldInteractionData : ScriptableObject
    {
        public string hudDisplayMessage;
        public Vector3 hudOffset;
        [InlineEditor] public WorldInteractionEvent executionEvent;

    }
}