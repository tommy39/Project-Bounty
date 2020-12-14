using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Weapons.Grenades;

namespace IND.Core.Player.Inventory
{
    [CreateAssetMenu(fileName = "Player Inventory", menuName = "IND/Core/Player/Inventory Data")]
    public class PlayerInventoryData : ScriptableObject
    {
        [InlineEditor] public List<WeaponItem> inventoryWeapons = new List<WeaponItem>();
        public int maxInventoryWeaponsAmount = 2;

        [PropertySpace]
        [Title("INVENTORY")] public PlayerInventoryAmmo inventoryAmmo = new PlayerInventoryAmmo();

        [InlineEditor] [FoldoutGroup("Dev Settings")] public ScriptableBool unlimitedAmmo;

        private void OnValidate()
        {
            if (inventoryWeapons.Count > maxInventoryWeaponsAmount)
            {
                inventoryWeapons.RemoveRange(maxInventoryWeaponsAmount, inventoryWeapons.Count - maxInventoryWeaponsAmount);
            }
        }
    }
}