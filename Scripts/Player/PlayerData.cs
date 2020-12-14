using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Player.Abilities;
using IND.Core.Player.Inventory;

namespace IND.Core.Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "IND/Core/Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [InlineEditor] public ScriptableGameObject playerPrefab;
        [InlineEditor] public PlayerInventoryData inventoryData;
        [InlineEditor] public Ability equippedAbility;
    }
}