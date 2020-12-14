using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;
using IND.Core.Shared.Statics;
using IND.Core.WorldInteractions;

namespace IND.Core.SpawnPoints.Weapons
{
    public class WeaponSpawnPoint : IND_Mono
    {
        [InlineEditor] public WeaponSpawnObjectList weaponSpawnList;
        private WeaponSpawnManager weaponSpawnManager;
        public override void Init()
        {
            weaponSpawnManager = FindObjectOfType<WeaponSpawnManager>();
            SetupWeaponWorldInteraction();
        }

        void SetupWeaponWorldInteraction()
        {
            WeaponItem weaponToSpawn = GetWeaponToSpawn();

            GameObject worldInteractionGeo = Instantiate(weaponSpawnManager.managerData.worldInteractionForWeaponPickupPrefab, transform);
            WorldInteractionsManager.singleton.AddNewInteraction(worldInteractionGeo.GetComponent<WorldInteractionController>());
            worldInteractionGeo.transform.localPosition = Vector3.zero;
            GameObject weaponMesh =  WeaponStaticUtils.SetupWeaponToBePickableInWorld(worldInteractionGeo, weaponToSpawn);
            weaponMesh.transform.localPosition = Vector3.zero;
            
            WorldInteractionEvent_PickupWeaponRuntime pickupInteraction = worldInteractionGeo.GetComponent<WorldInteractionEvent_PickupWeaponRuntime>();
            pickupInteraction.weaponToPickup = weaponToSpawn;
            pickupInteraction.GetRuntimeWeapon();
            pickupInteraction.runtimeWeapon.FreshSpawnWeapon();
        }

        WeaponItem GetWeaponToSpawn()
        {
            WeaponItem weaponToSpawn = null;

            while (weaponToSpawn == null)
            {
                for (int i = 0; i < weaponSpawnList.potentialWeaponsToSpawn.Count; i++)
                {
                    bool isSuccess = GetRollValue.Execute(0, 100, weaponSpawnList.potentialWeaponsToSpawn[i].chanceOfSpawning);

                    if (isSuccess)
                    {
                        weaponToSpawn = weaponSpawnList.potentialWeaponsToSpawn[i].weaponToSpawn;
                        break;
                    }
                }
            }

            return weaponToSpawn;
        }

    }
}