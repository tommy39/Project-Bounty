using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.Weapons
{
    [CreateAssetMenu(fileName = "Weapon_Ranged_Name", menuName = "IND/Core/Weapons/WeaponItemRanged_DualWielded")]
    public class WeaponItemRanged_DualWielded : WeaponItemRanged
    {
        [Title("Dual Wield Data")]
        public WeaponItemRanged childRangedItem;
    }
}