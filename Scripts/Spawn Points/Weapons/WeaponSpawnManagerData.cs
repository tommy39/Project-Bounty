using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.SpawnPoints.Weapons
{
    [CreateAssetMenu(fileName = "WeaponSpawnManagerData", menuName = "IND/Core/Spawn Points/Weapons/Weapon Spawn Manager Data")]
    public class WeaponSpawnManagerData : ScriptableObject
    {
        public GameObject worldInteractionForWeaponPickupPrefab;
    }
}