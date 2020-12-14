using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.WorldInteractions;

namespace IND.Core.Weapons
{
    public static class WeaponStaticUtils
    {
        public static GameObject SetupWeaponToBePickableInWorld(GameObject objectToSpawnAt, WeaponItem weaponData)
        {
            GameObject geo = Object.Instantiate(weaponData.weaponPrefab, objectToSpawnAt.transform);
            return geo;
        }

        public static WorldInteractionEvent_PickupWeaponRuntime DropWeaponToGround(WeaponItemRuntime weaponToDrop, Transform worldInteractionParent, GameObject worldInteractionPrefab)
        {
            GameObject createdWorldInteraction = Object.Instantiate(worldInteractionPrefab, worldInteractionParent);
            createdWorldInteraction.transform.position = weaponToDrop.transform.position;
            WorldInteractionsManager.singleton.AddNewInteraction(createdWorldInteraction.GetComponent<WorldInteractionController>());

            WorldInteractionEvent_PickupWeaponRuntime pickupWeaponRuntime = createdWorldInteraction.GetComponent<WorldInteractionEvent_PickupWeaponRuntime>();
            pickupWeaponRuntime.runtimeWeapon = weaponToDrop;
            pickupWeaponRuntime.weaponToPickup = weaponToDrop.weaponItemData;
            pickupWeaponRuntime.SetupInteractionSubSystems();   
            
           

            weaponToDrop.transform.SetParent(createdWorldInteraction.transform);

            return pickupWeaponRuntime;
        }

        public static void ThrowWeaponWithForce(WorldInteractionEvent_PickupWeaponRuntime interaction, float throwDistanceForce, Vector3 direction)
        {
            interaction.createdRigidBody.AddForce(direction * throwDistanceForce);
        }

    }
}