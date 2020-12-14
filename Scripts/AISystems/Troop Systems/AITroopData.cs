using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;

namespace IND.Core.AISystems.TroopSystems
{
    [CreateAssetMenu(fileName = "AI_Troop_Data_Generic", menuName = "IND/Core/AI/AI Troop Data")]
    public class AITroopData : ScriptableObject
    {
        public string troopName;
        public AITroopType troopType;
        public GameObject characterPrefab;

        [InlineEditor] public AINavMeshSettings navMeshSettings;
        [PropertyRange(0, 10)] public float knockDownDuration = 4f;
        [InlineEditor] public WeaponItem defaultWeaponToSpawnWith;

    }
}