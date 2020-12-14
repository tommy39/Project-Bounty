using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;

namespace IND.Core.SpawnPoints.Weapons
{
    [System.Serializable]
    public class WeaponSpawnObject : IND_Base
    {
        [Required] [InlineEditor] public WeaponItem weaponToSpawn;
        [PropertyRange(0, 100)] public int chanceOfSpawning;
    }
}
