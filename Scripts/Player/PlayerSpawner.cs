using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Player.Inventory;
using IND.Core.Player.Abilities;

namespace IND.Core.Player
{
    public class PlayerSpawner : IND_Mono
    {
        [InlineEditor] public PlayerData playerData;

        public PlayerController SpawnPlayer()
        {
            GameObject prefabCreated = Instantiate(playerData.playerPrefab.value);
            prefabCreated.transform.position = transform.position;
            prefabCreated.transform.rotation = transform.rotation;

            //     PlayerInventoryController inventoryController = prefabCreated.GetComponent<PlayerInventoryController>();
            AbilityController abilityController = prefabCreated.GetComponent<AbilityController>();
            abilityController.currentAbility = playerData.equippedAbility;

            return prefabCreated.GetComponent<PlayerController>();
        }
    }
}