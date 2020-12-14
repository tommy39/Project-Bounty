using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Player.Inventory;

namespace IND.Core.WorldInteractions
{
    [CreateAssetMenu(fileName = "Interaction_PickupWeapon", menuName = "IND/Core/World Interactions/Pickup Weapon")]
    public class WorldInteractionEvent_PickupWeapon : WorldInteractionEvent
    {
        public override void ExecuteWorldInteraction(WorldInteractionController interaction, InteractionSearcherController playerInteracting)
        {
            interaction.GetComponent<WorldInteractionEvent_PickupWeaponRuntime>().PickupWeapon(playerInteracting);
        }
    }
}