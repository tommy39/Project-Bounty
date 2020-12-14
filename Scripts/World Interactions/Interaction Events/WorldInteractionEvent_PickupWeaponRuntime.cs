using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using IND.Core.Weapons;

namespace IND.Core.WorldInteractions
{
    public class WorldInteractionEvent_PickupWeaponRuntime : IND_Mono
    {
        public WeaponItem weaponToPickup;
        public WeaponItemRuntime runtimeWeapon;
        public Rigidbody createdRigidBody;
        public void GetRuntimeWeapon()
        {
            runtimeWeapon = GetComponentInChildren<WeaponItemRuntime>();
        }

        public void SetupInteractionSubSystems()
        {
            SetupRigidBody();
            runtimeWeapon.EnableCollisionColliders();
            gameObject.SetActive(true);
        }

        private void SetupRigidBody()
        {
            createdRigidBody = gameObject.AddComponent<Rigidbody>();
            createdRigidBody.mass = weaponToPickup.rigidBodyData.mass;
            createdRigidBody.drag = weaponToPickup.rigidBodyData.drag;
            createdRigidBody.angularDrag = weaponToPickup.rigidBodyData.angularDrag;
        }

  
        
        public void PickupWeapon(InteractionSearcherController playerInteracting)
        {
            playerInteracting.inventoryController.EquipNewWeaponToInventory(runtimeWeapon);
            RemoveInteraction();
        }

        void RemoveInteraction()
        {
            GetComponent<WorldInteractionController>().DestroyWorldInteraction();
        }
    }
}