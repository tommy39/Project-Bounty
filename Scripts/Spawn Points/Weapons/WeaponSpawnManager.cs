using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace IND.Core.SpawnPoints.Weapons
{
    public class WeaponSpawnManager : IND_Mono
    {
        public bool fetchAllSceneSpawnPointsOnStart = true;
        [ShowIf("fetchAllSceneSpawnPointsOnStart")] public bool clearListOnStart = false;

        public List<WeaponSpawnPoint> spawnPoints = new List<WeaponSpawnPoint>();

        [InlineEditor] public WeaponSpawnManagerData managerData;
        public override void Init()
        {
            if (fetchAllSceneSpawnPointsOnStart)
            {
                if (clearListOnStart)
                {
                    spawnPoints.Clear();
                }

                WeaponSpawnPoint[] spawns = FindObjectsOfType<WeaponSpawnPoint>();
                foreach (WeaponSpawnPoint item in spawns)
                {
                    spawnPoints.Add(item);
                }
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Init();
            }
        }
    }
}