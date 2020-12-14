using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.WorldInteractions
{
    [CreateAssetMenu(fileName = "InteractableEventBase", menuName = "IND/Core/World Interactions/Interactable Event Base")]
    public class WorldInteractionEvent : ScriptableObject
    {
        public virtual void ExecuteWorldInteraction(WorldInteractionController interaction, InteractionSearcherController playerInteracting)
        {

        }
    }
}