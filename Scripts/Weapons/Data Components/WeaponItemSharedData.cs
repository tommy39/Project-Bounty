using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.HitEffects;

namespace IND.Core.Weapons
{
    [CreateAssetMenu(fileName = "WeaponItemSharedData", menuName = "IND/Core/Weapons/WeaponItemSharedData")]
    public class WeaponItemSharedData : ScriptableObject
    {
        [InlineEditor] public ScriptableLayerMask layersPlayerCanHitWithWeapon;
        [InlineEditor] public ScriptableLayerMask layersEnemiesCanHitWithWeapon;

    }
}