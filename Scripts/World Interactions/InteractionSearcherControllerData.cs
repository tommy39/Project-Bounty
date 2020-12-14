using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.WorldInteractions
{
    [CreateAssetMenu(fileName = "Interaction Searcher Data", menuName = "IND/Core/World Interactions/Searcher Data")]
    public class InteractionSearcherControllerData : ScriptableObject
    {
        public float maxSearchDistance = 5f;
        public int searchFrameRate = 1;
        [Required] [InlineEditor] public ScriptableLayerMask worldInteractionsLayerMask;
        public GameObject worldInteractionPrefab;
    }
}