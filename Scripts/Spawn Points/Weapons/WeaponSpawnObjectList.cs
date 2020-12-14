using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.SpawnPoints.Weapons
{
    [CreateAssetMenu(fileName = "SpawnPoint_Weapons_01", menuName = "IND/Core/Spawn Points/Weapons/Weapons Spawn Point List")]
    public class WeaponSpawnObjectList : ScriptableObject
    {
       [MultiLineProperty(3)] public string devDescription;

        public List<WeaponSpawnObject> potentialWeaponsToSpawn = new List<WeaponSpawnObject>();
    }
}